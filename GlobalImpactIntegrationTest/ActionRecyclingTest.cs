using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GlobalImpact.Controllers;
using GlobalImpact.Data;
using GlobalImpact.Models;
using GlobalImpact.Utils;
using GlobalImpact.ViewModels.Account;
using GlobalImpact.ViewModels.RecyclingBin;
using GlobalImpactTest.FakeManagers;
using GlobalImpactTest.IClassFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace GlobalImpactIntegrationTest
{
    public class ActionRecyclingTest : IClassFixture<ApplicationDbContextFixture>
    {
        private ApplicationDbContext _context;
        private Mock<RoleManager<IdentityRole>> roleManagerMock;
        private Mock<FakeUserManager> userManagerMock;
        private Mock<FakeSignInManager> signInManagerMock;
        private AccountController controllerAccount;
        private RecyclingBinsController controllerRecBin;
        private RecyclingTransactionController controllerRecBinTrans;

        public ActionRecyclingTest(ApplicationDbContextFixture context)
        {
            _context = context.DbContext;

            var listRoles = _context.Roles.AsQueryable();

            roleManagerMock = new Mock<RoleManager<IdentityRole>>(
               new Mock<IRoleStore<IdentityRole>>().Object,
               new IRoleValidator<IdentityRole>[0],
               new Mock<ILookupNormalizer>().Object,
               new Mock<IdentityErrorDescriber>().Object,
               new Mock<ILogger<RoleManager<IdentityRole>>>().Object);

            roleManagerMock
                .Setup(r => r.Roles).Returns(listRoles);

            var users = _context.Users.AsQueryable();

            userManagerMock = new Mock<FakeUserManager>();
            userManagerMock.Setup(x => x.Users)
                .Returns(users);
            userManagerMock.Setup(x => x.DeleteAsync(It.IsAny<AppUser>()))
                .ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<AppUser>()))
                .ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(x =>
                    x.ChangeEmailAsync(It.IsAny<AppUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            signInManagerMock = new Mock<FakeSignInManager>();
            signInManagerMock.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<AppUser>(), "123", It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);

            controllerAccount = new AccountController(userManagerMock.Object, signInManagerMock.Object,
                roleManagerMock.Object, new EmailSender(), _context);

            controllerRecBin = new RecyclingBinsController(_context, userManagerMock.Object);
            controllerRecBinTrans = new RecyclingTransactionController(_context, signInManagerMock.Object);

            //userManagerMock.Setup(u => u.GetUserAsync(controllerAccount.User)).ReturnsAsync(users.First());
        }


        [Fact]
        public async void CheckIntegracion_Account_EcoBin_EcoTrans()
        {
            //Registar Novo Utilizador
            RegisterViewModel registerViewModel = new RegisterViewModel()
            {
                Email = "test1@gmail.com",
                UserName = "test1",
                FirstName = "Test1",
                LastName = "User1",
                Age = 20,
                NIF = 123456789,
                Password = "!Qq1234",
                ConfirmPassword = "!Qq1234"
            };

            //Açao Register
            var result = await controllerAccount.Register(registerViewModel, "/");

            //Registo com sucesso
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("EmailSending", redirectResult.ActionName);
            Assert.Equal("Account", redirectResult.ControllerName);

            var role = _context.Roles.FirstOrDefault(r => r.Name.Equals("client"));

            //Adicionar Novo User a BD
            var user = new AppUser
            {
                UniqueCode = new Guid().ToString(),
                UserName = "test1",
                FirstName = "Test1",
                LastName = "User1",
                Age = 20,
                NIF = 123456789,
                Email = "test1@gmail.com",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "!Qq1234"),
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var user1 = userManagerMock.Object.Users.FirstOrDefault(u => u.UserName.Equals(user.UserName));
            Assert.NotNull(user1);


            //User Login
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                UserName = "test1",
                Password = registerViewModel.Password
            };

            result = await controllerAccount.Login(loginViewModel, "");
            Assert.IsType<ViewResult>(result);

            //User Verifica User Page
            result = controllerAccount.UserPage(user1.Id.ToString());
            var redirectResult2 = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AppUser>(redirectResult2.ViewData.Model);
            Assert.NotNull(model);

            //User Ativa o Ecoponto
            EcoLogViewModel modelView = new EcoLogViewModel
            {
                IdInput = user1.UniqueCode
            };

            var result2 = controllerRecBin.EcoLogin(modelView);
            Assert.IsType<Task<IActionResult>>(result2);

            //User Simula Reciclagem de um item
            var bin = _context.RecyclingBins.First();
            var type = _context.RecyclingBinType.FirstOrDefault(r => r.RecyclingBinTypeId == new Guid(bin.RecyclingBinTypeId)).Type;
            var nome = user1.UserName;
            var itemName = "Garrafa";

            var result3 = controllerRecBinTrans.Rec(bin.Id.ToString(), type, nome, itemName, new List<RecItems>());
            var resultView = Assert.IsType<Task<IActionResult>>(result3);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);

            //User Termina Resciclagem
            var peso = 0.5;
            var pontos = 5;
            var result4 = controllerRecBinTrans.FinishRecycling(bin.Id.ToString(), nome, peso, pontos);
            var resultView2 = Assert.IsType<Task<IActionResult>>(result4);
            var mod2 = Assert.IsAssignableFrom<RedirectToActionResult>(resultView2.Result);

            //User Termina Resciclagem segunda reciclagem
            var peso2 = 10.5;
            var pontos2 = 55;
            var result5 = controllerRecBinTrans.FinishRecycling(bin.Id.ToString(), nome, peso2, pontos2);
            var resultView3 = Assert.IsType<Task<IActionResult>>(result5);
            var mod3 = Assert.IsAssignableFrom<RedirectToActionResult>(resultView3.Result);

            Assert.Equal("EcoLogin", mod3.ActionName);
            Assert.Equal("RecyclingBins", mod3.ControllerName);

            //Transaçoes Guardadas na Base de Dados
            var trans = _context.RecyclingTransactions.ToList();
            Assert.NotNull(trans);

            //User Pontos
            var finalUser = _context.Users.FirstOrDefault(u => u.UserName.Equals(user.UserName));
            Assert.Equal(finalUser.Points, 60);

            //Ecoponto Peso
            var finalBin = _context.RecyclingBins.First();
            Assert.Equal(finalBin.CurrentCapacity, 11);
        }
    }
}
