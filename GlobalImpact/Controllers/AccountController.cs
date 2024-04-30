using GlobalImpact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GlobalImpact.Interfaces;
using GlobalImpact.Utils;
using GlobalImpact.ViewModels;
using GlobalImpact.ViewModels.Account;
using GlobalImpact.Data;
using Microsoft.EntityFrameworkCore;


namespace GlobalImpact.Controllers
{
    /// <summary>
    /// Controller da gestão de Acessos
    /// </summary>
    /// <remarks></remarks>
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Construtor do Controller AccountController
        /// </summary>
        /// <param name="userManager">Fornece APIs para gestao de utilizadores</param>
        /// <param name="signInManager">Fornece APIs para Login de utilizadores</param>
        /// <param name="roleManager">Fornece APIs para gestao de roles de utilizadores</param>
        /// <param name="emailService">Fornece Envio de Emails</param>
        /// <param name="db">Base de Dados</param>
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _db = db;
        }

        public IActionResult UserPage(string userId)
        {
			UserPageViewModel viewModel = new UserPageViewModel();

			viewModel.user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
			viewModel.produtos = new List<string>();
			viewModel.quantidades = new List<int>();
			viewModel.precoProduto = new List<int>();
			if (_db.ProductTransactions.Count() > 0)
            {
				viewModel.productTransactions = _db.ProductTransactions.OrderByDescending(r => r.Date).FirstOrDefault();

                var list = _db.ProductTransactions.OrderByDescending(r => r.Date).ToList();

				foreach (var trans in list)
                {
                    var productname = _db.Products.FirstOrDefault(p => p.Id.Equals(trans.ProductId)).Name;

                    viewModel.produtos.Add(productname);
                    viewModel.quantidades.Add(trans.Quantity);
                    viewModel.precoProduto.Add(trans.Points);
                }
                viewModel.confirmProductTransactions = true;
			}
            else
            {
                viewModel.productTransactions = new ProductTransactions();
                viewModel.productTransactions.Id = Guid.NewGuid();
                viewModel.productTransactions.ProductName = "";
                viewModel.productTransactions.TransStatus = "";
				viewModel.productTransactions.Points = 0;
                viewModel.productTransactions.Date = DateTime.Now;
                viewModel.productTransactions.TransStatus = "";
				viewModel.confirmProductTransactions = false;

			}
			if (_db.RecyclingTransactions.Count() > 0)
            {
				viewModel.recyclingTransaction = _db.RecyclingTransactions.OrderByDescending(r => r.Date).FirstOrDefault();
                var recyclingBin = _db.RecyclingBins.FirstOrDefault(r => r.Id.Equals(viewModel.recyclingTransaction.RecyclingBinId));
                viewModel.recyclingTransaction.RecyclingBin = recyclingBin;
                var types = _db.RecyclingBinType.ToList();

                foreach(var type in types)
                {
                    if (type.RecyclingBinTypeId.Equals(new Guid(viewModel.recyclingTransaction.RecyclingBin.RecyclingBinTypeId)))
                    {
						viewModel.recyclingTransaction.RecyclingBin.Type = type.Type;
					}
                }
				viewModel.confirmRecyclingTransaction = true;
			}
            else
            {
                viewModel.recyclingTransaction = new RecyclingTransaction();
				viewModel.recyclingTransaction.Id = Guid.NewGuid();
				viewModel.recyclingTransaction.Points = 0;
				viewModel.recyclingTransaction.Date = DateTime.Now;
                viewModel.recyclingTransaction.Weight = 0;
				viewModel.confirmRecyclingTransaction = false;
			}

			return View(viewModel);
        }

        /// <summary>
        /// Função Get para retornar a página de registo.
        /// </summary>
        /// <param name="returnUrl"> Url da página de registo.</param>
        /// <returns> retorna a página de registo.</returns>

        [HttpGet]
        public async Task<IActionResult> Register(string? returnUrl)
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.ReturnUrl = returnUrl;
            return View(registerViewModel);
        }

        /// <summary>
        /// Função HttpPost função para o registo de um user.
        /// </summary>
        /// <param name="registerViewModel"> parametro que contém todos os dados necessários para registar um user.</param>
        /// <param name="returnUrl"> redericiona para a homePage.</param>
        /// <returns> retorna a página de registo em caso de erro; Caso de sucesso retorna para a página de verificação de email.</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string? returnUrl = null)
        {
            registerViewModel.ReturnUrl = returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var isEmailExists = _db.Users.Any(x => x.Email == registerViewModel.Email);
                var isUserNameExists = _db.Users.Any(x => x.UserName == registerViewModel.UserName);
                if (isEmailExists)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return View(registerViewModel);
                }

                if (isUserNameExists)
                {
                    ModelState.AddModelError("UserName", "UserName already exists");
                    return View(registerViewModel);
                }
                else
                {
	                AppUser user = new AppUser
                    {
                        UserName = registerViewModel.UserName,
                        Email = registerViewModel.Email,
                        FirstName = registerViewModel.FirstName,
                        LastName = registerViewModel.LastName,
                        Age = registerViewModel.Age,
                        NIF = registerViewModel.NIF,
                        Points = 0,
                        UniqueCode = Guid.NewGuid().ToString()
                    };
                    var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                    if (result.Succeeded)
                    {

                        await _userManager.AddToRoleAsync(user, "Client");
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        if (!string.IsNullOrEmpty(code))
                        {
                            var callbackUrl = Url.Action("ConfirmEmailTask", "Account", new { userId = user.Id, code }, protocol: HttpContext.Request.Scheme);
                            string emailBody = $"Olá {user.UserName},\n" +
                                $"Obrigado por se registar na GlobalImpack! Para completar o processo de registro e começar a usar nossa plataforma de reciclagem, por favor, verifique sua conta clicando no link abaixo:\n" +
                                $"{callbackUrl}\n" +
                                $"Se você não solicitou esta verificação, por favor, ignore este e-mail. Sua conta permanecerá pendente de verificação.\n" +
                                $"Atenciosamente,\n" +
                                $"GlobalImpack Develop Team";
                            await _emailService.SendEmailAsync(user.Email, "Verificação de Conta - GlobalImpack", emailBody);
                        }
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("EmailSending", "Account");
                    }

                    ModelState.AddModelError("Email", "User could not be created. Password not unique enought");
                }
            }
            return View(registerViewModel);
        }
        /// <summary>
        /// Função Get que retorna a pagina de envio de email de confirmaçao
        /// </summary>
        /// <returns>página de verificação de email.</returns>
        [HttpGet]
        public IActionResult EmailSending()
        {
            return View();
        }

        /// <summary>
        /// Função Get que confirma a verificaçao do email
        /// </summary>
        /// <param name="userId">Id do user a ser verificado.</param>
        /// <param name="code">código de confirmação de email.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ConfirmEmailTask(string userId = null, string code = null)
        {
            if (code == null || userId == null)
            {
                return View("Error");

            }
            else
            {
                var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return View("Error");
                }
                else
                {
                    var result = await _userManager.ConfirmEmailAsync(user, code);
                    return RedirectToAction("EmailConfirmation");
                }
            }
        }
        public IActionResult EmailConfirmation()
        {
            return View();
        }


        /// <summary>
        /// Função Get da página de login.
        /// </summary>
        /// <param name="returnUrl"> Guarda o url da página de login.</param>
        /// <returns>Retorna a página de login.</returns>
        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl)
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.ReturnUrl = returnUrl;
            return View(loginViewModel);
        }

        /// <summary>
        /// Função Post do login.
        /// </summary>
        /// <param name="loginViewModel"> parâmetro que  guarda todos os dados necessários para o login de um user.</param>
        /// <param name="returnUrl"> retorna o url da página do user.</param>
        /// <returns>retorna a página de login em caso de erro; Caso de sucesso retorna para a página do user.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                if(user != null)
                {
                    if (user.EmailConfirmed)
                    {
                        var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: true);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        if (result.IsLockedOut)
                        {
                            return View("Lockout");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return View(loginViewModel);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Email is not Confirmed.");
                        return View(loginViewModel);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Utilizador não existe");
                    return View(loginViewModel);
                }
               

            }
            return View(loginViewModel);
        }

        /// <summary>
        /// Função Post com o auxilio da api de autenticação da Google.
        /// </summary>
        /// <param name="provider"> api da Google</param>
        /// <param name="returnUrl">retorna o url da página do google.</param>
        /// <returns>Retorna a página de auenticação do google.</returns>

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        /// <summary>
        /// Função Get, se o user já tiver a conta registada vai para o dashBoard; se não faz o registo.
        /// </summary>
        /// <param name="returnUrl">retorna o url da DashBoard.</param>
        /// <param name="remoteError">Se houver algum problema com o provider.</param>
        /// <returns>Se houver um problema retorna para a página de login; se o user já tiver uma conta vai para o DashBoard, se não vai para o registo.</returns>
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            //Sign in the user with this external login provider, if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //If the user does not have account, then we will ask the user to create an account.
                ViewData["ProviderDisplayName"] = info.ProviderDisplayName;

                var model = new ExternalLoginViewModel
                {
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                };
                return View("ExternalLoginConfirmation", model);

            }
        }

        /// <summary>
        /// Funçao Post que regista o user que fez o login pela API.
        /// </summary>
        /// <param name="externalLoginViewModel">Guarda todos os dados necessários de um User para efetuar o login.</param>
        /// <param name="returnUrl">Retorna o url do DashBoard.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel externalLoginViewModel,
            string? returnUrl = null, string? email=null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("Error");
                }
                var emailViewValidation = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (emailViewValidation != externalLoginViewModel.Email)
                {
					ModelState.AddModelError("Email", "Email not matching");
					return View(externalLoginViewModel);
				}
				var isEmailExists = _db.Users.Any(x => x.Email == externalLoginViewModel.Email);
                var isUserNameExists = _db.Users.Any(x => x.UserName == externalLoginViewModel.Name);
                if (isEmailExists)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return View(externalLoginViewModel);
                }

                if (isUserNameExists)
                {
                    ModelState.AddModelError("UserName", "UserName already exists");
                    return View(externalLoginViewModel);
                }
                else
                {
                    var user = new AppUser
                    {
                        UserName = externalLoginViewModel.Name,
                        Email = externalLoginViewModel.Email,
                        FirstName = externalLoginViewModel.FirstName,
                        LastName = externalLoginViewModel.LastName,
                        Age = externalLoginViewModel.Age,
                        NIF = externalLoginViewModel.NIF,
                        Points = 0,
                        UniqueCode = Guid.NewGuid().ToString()
                    };

                    var result = await _userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        result = await _userManager.AddLoginAsync(user, info);
                        if (result.Succeeded)
                        {

                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            await _userManager.AddToRoleAsync(user, "client");
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                            var res = await _userManager.ConfirmEmailAsync(user, code);
                            return LocalRedirect(returnUrl);
                        }
                    }
                    ModelState.AddModelError("Email", "User already exists");
                }
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(externalLoginViewModel);
        }
        /// <summary>
        /// Função Post para o logout de um user.
        /// </summary>
        /// <returns>Retorna o url para a página inicial.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// Função Get para retornar a página de forgot passsword.
        /// </summary>
        /// <returns>retorna a página de forgot password.</returns>
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Função Post para envio de email para troca de passWord.
        /// </summary>
        /// <param name="forgotPasswordViewModel">guarda os dados necessários para a troca de PassWord.</param>
        /// <returns>Retorna a página para a confirmação de email.</returns>
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);
                
                if (user == null)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }
                
                var logins = await _userManager.GetLoginsAsync(user);
                foreach (var login in logins)
                {
                    var provider = login.LoginProvider;
                    if (provider.Contains("Google"))
                    {
						ModelState.AddModelError("Email", "Can't Change Password of this email because its associated with a Google Account");
						return View(forgotPasswordViewModel);
					}
                }
                
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, protocol: HttpContext.Request.Scheme);
                await _emailService.SendEmailAsync(forgotPasswordViewModel.Email, "Reset Password",
                                       $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return RedirectToAction("ForgotPasswordConfirmation");
            }
            return View(forgotPasswordViewModel);
        }
        /// <summary>
        /// Função Get para retornar a página de confirmação do email da troca de PassWord.
        /// </summary>
        /// <returns> retorna a página de confirmação de email.</returns>
        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// Função Get para retornar a página de Reset PassWord.
        /// </summary>
        /// <param name="code">código de user.</param>
        /// <returns>retorna a página de Reset PassWord, caso o código seja válido.</returns>
        [HttpGet]
        public IActionResult ResetPassword(string code = null, string userId = null)
        {
            return code == null ? View("Error") : View();
        }

        /// <summary>
        /// Função Post para a trocar de PassWord.
        /// </summary>
        /// <param name="resetPasswordViewModel"> Guarda os dados necessários para a troca de PassWord.</param>
        /// <returns>Retorna a página de confirmação de troca de PassWord.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(resetPasswordViewModel.UserId);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "User not found");
                    return View();
                }
                var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code, resetPasswordViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
            }
            return View(resetPasswordViewModel);
        }

        /// <summary>
        /// Função Get para retornar a página de confirmação de troca de PassWord.
        /// </summary>
        /// <returns>Retorna a página de confirmação de troca de PassWord.</returns>
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
