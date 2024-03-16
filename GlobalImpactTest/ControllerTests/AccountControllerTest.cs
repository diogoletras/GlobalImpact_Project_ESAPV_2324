using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using GlobalImpact.Controllers;
using GlobalImpact.Data;
using GlobalImpact.Models;
using GlobalImpact.Utils;
using GlobalImpact.ViewModels.Account;
using GlobalImpactTest.FakeManagers;
using GlobalImpactTest.IClassFixture;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace GlobalImpactTest.ControllerTests
{
    /// <summary>
    /// Classe testes unitários do accountController.
    /// </summary>
    public class AccountControllerTest : IClassFixture<ApplicationDbContextFixture>
    {
        private ApplicationDbContext dbContext;
        private Mock<RoleManager<IdentityRole>> roleManagerMock;
        private Mock<FakeUserManager> userManagerMock;
        private Mock<FakeSignInManager> signInManagerMock;
        private AccountController controller;
        public AccountControllerTest(ApplicationDbContextFixture context)
        {
            dbContext = context.DbContext;

            var listRoles = new List<IdentityRole>()
            {
                new IdentityRole("admin"),
                new IdentityRole("client")
            }.AsQueryable();

            roleManagerMock = new Mock<RoleManager<IdentityRole>>(
               new Mock<IRoleStore<IdentityRole>>().Object,
               new IRoleValidator<IdentityRole>[0],
               new Mock<ILookupNormalizer>().Object,
               new Mock<IdentityErrorDescriber>().Object,
               new Mock<ILogger<RoleManager<IdentityRole>>>().Object);

            roleManagerMock
                .Setup(r => r.Roles).Returns(listRoles);

            var users = new List<AppUser>
            {
                new AppUser
                {
                    UserName = "test1",
                    UniqueCode = Guid.NewGuid().ToString(),
                    FirstName = "Test1",
                    LastName = "User",
                    Age = 20,
                    Points = 0,
                    Email = "234@gmail.com",
                    NIF = 123456789,
                    NormalizedEmail = "234@GMAIL.COM",
                    NormalizedUserName = "TEST1",
                    PasswordHash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("123")).ToString()
                },
                new AppUser
                {
                    UserName = "test2",
                    UniqueCode = Guid.NewGuid().ToString(),
                    FirstName = "Test2",
                    LastName = "User2",
                    Age = 30,
                    Points = 0,
                    Email = "123@gmail.com",
                    NIF = 987654321,
                    NormalizedEmail = "123@GMAIL.COM",
                    NormalizedUserName = "TEST2",
                    PasswordHash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("123")).ToString()
                },
                new AppUser()
                {
                    UserName = "test3",
                    UniqueCode = Guid.NewGuid().ToString(),
                    FirstName = "Test3",
                    LastName = "User3",
                    Age = 40,
                    Points = 0,
                    Email = "345@gmail.com",
                    NIF = 987654321,
                    NormalizedEmail = "123@GMAIL.COM",
                    NormalizedUserName = "TEST3",
                    PasswordHash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("123")).ToString()
                }
            }.AsQueryable();

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

            controller = new AccountController(userManagerMock.Object, signInManagerMock.Object,
                roleManagerMock.Object, new EmailSender(), dbContext);
            userManagerMock.Setup(u => u.GetUserAsync(controller.User)).ReturnsAsync(users.First());

        }

        [Fact]
        public void UserPage_CanGetPageWithSuccess()
        {
            var user = userManagerMock.Object.Users.FirstOrDefault(u => u.UserName == "test1");
            var result = controller.UserPage(user.Id);
            var redirectResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AppUser>(redirectResult.ViewData.Model);
            Assert.NotNull(model);
        }


        [Fact]
        public async void Register_CanPostWithSuccess()
        {

            var users = await dbContext.AppUser.ToListAsync();

            RegisterViewModel registerViewModel = new RegisterViewModel()
            {
                Email = "test1@gmail.com",
                UserName = "test1",
                FirstName = "Test1",
                LastName = "User1",
                Age = 20,
                NIF = 123456789,
                Password = "!Qq1234",
                ConfirmPassword = "!Qq1234",
                ReturnUrl = ""
            };

            var result = await controller.Register(registerViewModel, "/");
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("EmailSending", redirectResult.ActionName);
            Assert.Equal("Account", redirectResult.ControllerName);

        }

        [Fact]
        public async void Register_CanGetPageWithSuccess()
        {
            var result = await controller.Register("");
            var redirectResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<RegisterViewModel>(redirectResult.ViewData.Model);
            Assert.NotNull(model);

        }

        [Fact]
        public void EmailSending_CanGetPageWithSuccess()
        {
            var result = controller.EmailSending();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ConfirmEmailTask_CanGetPageWithSuccess()
        {
            var user = userManagerMock.Object.Users.FirstOrDefault(u => u.UserName == "test1");

            var result = controller.ConfirmEmailTask(user.Id, user.UniqueCode);

            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Login_CanGetPageWithSuccess()
        {
            var result = controller.Login("");
            var redirectResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public async void Login_CanPostWithSuccess()
        {
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                UserName = "test1",
                Password = "123",
            };

            var result = await controller.Login(loginViewModel, "");
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void LogOff_CanGetPageWithSuccess()
        {
            var result = controller.LogOff();
            Assert.IsType<Task<IActionResult>>(result);
        }
    }
}