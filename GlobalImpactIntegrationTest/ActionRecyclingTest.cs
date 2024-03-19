using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalImpact.Controllers;
using GlobalImpact.Data;
using GlobalImpact.Models;
using GlobalImpact.Utils;
using GlobalImpact.ViewModels.Account;
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
        private readonly ApplicationDbContext _context;
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
        public async void CheckInfo()
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

            var result = await controllerAccount.Register(registerViewModel, "/");

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("EmailSending", redirectResult.ActionName);
            Assert.Equal("Account", redirectResult.ControllerName);

            //Confirmar Email
            var user = _context.Users.FirstOrDefault(u => u.UserName.Equals("test1"));
            Assert.NotNull(user);
            //user.EmailConfirmed = true;
            //_context.Users.Update(user);
            //_context.SaveChanges();

            //controllerAccount.ConfirmEmailTask(user.Id, user.UniqueCode);

            var useragain = _context.Users.FirstOrDefault(u => u.UserName.Equals("test1"));

            Assert.Equal(true, useragain.EmailConfirmed);


        }


    }
}
