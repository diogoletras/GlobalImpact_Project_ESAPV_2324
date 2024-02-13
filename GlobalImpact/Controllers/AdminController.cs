using GlobalImpact.Data;
using GlobalImpact.Models;
using GlobalImpact.Utils;
using GlobalImpact.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GlobalImpact.Controllers
{
    /// <summary>
    /// Classe de Admin Controller.
    /// </summary>
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(ApplicationDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        /// <summary>
        /// Função para retornar uma lista de Users.
        /// </summary>
        /// <returns> Retorna uma página da lista de Users.</returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var userList = _db.AppUser.ToList();
            var roleList = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in userList)
            {
                var role = roleList.FirstOrDefault(r => r.UserId == user.Id);
                if (role == null)
                {
                    user.Role = "None";
                }
                else
                {
                    user.Role = roles.FirstOrDefault(r => r.Id == role.RoleId).Name;
                }
            }
            return View(userList);
        }
        /// <summary>
        /// Página para retorno do "Edit User".
        /// </summary>
        /// <param name="userId">iD do User.</param>
        /// <returns>Retorna a página de "Editar User".</returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string userId)
        {
            var user = _db.AppUser.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            var role = userRole.FirstOrDefault(u => u.UserId == user.Id);
            if (role != null)
            {
                user.RoleId = roles.FirstOrDefault(u => u.Id == role.RoleId).Id;
            }
            user.RoleList = _db.Roles.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            return View(user);
        }

        /// <summary>
        /// Função Post para o "Edit User".
        /// </summary>
        /// <param name="user">User a ser editado.</param>
        /// <returns>Página a página de gestão de Users.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppUser user)
        {
            if (ModelState.IsValid)
            {
                var userDbValue = _db.AppUser.FirstOrDefault(u => u.Id == user.Id);
                if (userDbValue == null)
                {
                    return NotFound();
                }
                var userRole = _db.UserRoles.FirstOrDefault(u => u.UserId == userDbValue.Id);
                if (userRole != null)
                {
                    var previousRoleName = _db.Roles.Where(u => u.Id == userRole.RoleId).Select(e => e.Name).FirstOrDefault();
                    await _userManager.RemoveFromRoleAsync(userDbValue, previousRoleName);

                }

                await _userManager.AddToRoleAsync(userDbValue, _db.Roles.FirstOrDefault(u => u.Id == user.RoleId).Name);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }


            user.RoleList = _db.Roles.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            return View(user);
        }

        /// <summary>
        /// Função Post de apagar User.
        /// </summary>
        /// <param name="userId">O iD do user a ser eliminado.</param>
        /// <returns>Retorna a página de Gestão dos Users.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(string userId)
        {
            var user = _db.AppUser.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }
            _db.AppUser.Remove(user);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Função Get para a página de criação de um User.
        /// </summary>
        /// <param name="returnUrl">retorna o url da página de gestão dos Users.</param>
        /// <returns>Retorna a página de craição de Users.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create(string? returnUrl)
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.ReturnUrl = returnUrl;
            return View(registerViewModel);
        }

        /// <summary>
        /// Função Post para a criação de um User.
        /// </summary>
        /// <param name="registerViewModel">Guarda todos os dados necessários para a criação de um User.</param>
        /// <param name="returnUrl">Retorna o url de página de Gestão de Users.</param>
        /// <returns>Em caso de sucesso retorna a página de gestão de Users; em caso de insucesso mantém na página de criação de User. </returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel registerViewModel, string? returnUrl = null)
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
                    var user = new AppUser { UserName = registerViewModel.UserName, Email = registerViewModel.Email };
                    var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                    if (result.Succeeded)
                    {

                        await _userManager.AddToRoleAsync(user, "Client");
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        if (!string.IsNullOrEmpty(code))
                        {
                            await _userManager.ConfirmEmailAsync(user, code);
                        }
                        return RedirectToAction("Index", "Admin");
                    }
                    ModelState.AddModelError("Email", "User could not be created. Password not unique enought");
                }
            }
            return View(registerViewModel);
        }
    }
}
