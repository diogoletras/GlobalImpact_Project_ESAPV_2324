using System.Collections;
using GlobalImpact.Controllers;
using GlobalImpact.Data;
using GlobalImpact.Models;
using GlobalImpact.Utils;
using GlobalImpact.ViewModels.Account;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace GlobalImpactTest
{
    public class AccountControllerTest
    {
        [Fact]
        public async Task Register_CanRegisterWithSuccess()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GlobalImpactDB")
                .Options;

            ApplicationDbContext dbContext = new ApplicationDbContext(options);

            var listRoles = new List<IdentityRole>()
            {
                new IdentityRole("admin"),
                new IdentityRole("client")
            }.AsQueryable();

            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(
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
                    UniqueCode = Guid.NewGuid().ToString(),
                    FirstName = "Test",
                    LastName = "User",
                    Age = 20,
                    Points = 0,
                    NIF = 123456789
                },
                new AppUser
                {
                    UniqueCode = Guid.NewGuid().ToString(),
                    FirstName = "Test2",
                    LastName = "User2",
                    Age = 30,
                    Points = 0,
                    NIF = 987654321
                },
                new AppUser()
                {
                    UniqueCode = Guid.NewGuid().ToString(),
                    FirstName = "Test3",
                    LastName = "User3",
                    Age = 40,
                    Points = 0,
                    NIF = 987654321
                }
            }.AsQueryable();

            var userManagerMock = new Mock<FakeUserManager>();
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

            var signInManagerMock = new Mock<FakeSignInManager>();
            signInManagerMock.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<AppUser>(), It.IsAny<string>(), It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);

            var controller = new AccountController(userManagerMock.Object, signInManagerMock.Object,
                roleManagerMock.Object, new EmailSender(), dbContext);
            userManagerMock.Setup(u => u.GetUserAsync(controller.User)).ReturnsAsync(users.First());

            RegisterViewModel registerViewModel = new RegisterViewModel()
            {
                Email = "test1@gmail.com",
                UserName = "test1",
                FirstName = "Test1",
                LastName = "User1",
                Age = 20,
                NIF = 123456789,
                Password = "test1",
                ConfirmPassword = "test1",
                ReturnUrl = ""
            };

            var result = await controller.Register(registerViewModel, registerViewModel.ReturnUrl);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("EmailSending", redirectResult.ActionName);
            Assert.Equal("Account", redirectResult.ControllerName);
        }
    }
}