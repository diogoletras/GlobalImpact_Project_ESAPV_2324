using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GlobalImpact.Controllers;
using GlobalImpact.Data;
using GlobalImpact.Models;
using GlobalImpact.ViewModels.Account;
using GlobalImpactTest.FakeManagers;
using GlobalImpactTest.IClassFixture;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace GlobalImpactTest.ControllerTests
{
    public class AdminControllerTest : IClassFixture<ApplicationDbContextFixture>
    {
        public ApplicationDbContext dbContext;
        private Mock<FakeUserManager> userManagerMock;
        private Mock<FakeSignInManager> signInManagerMock;
        private AdminController controller;

        public AdminControllerTest(ApplicationDbContextFixture context)
        {
            dbContext = context.DbContext;

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

            controller = new AdminController(dbContext, userManagerMock.Object, signInManagerMock.Object);

            userManagerMock.Setup(u => u.GetUserAsync(controller.User)).ReturnsAsync(users.First());
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfUsers()
        {
            var result = controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<AppUser>>(viewResult.Model);
            Assert.Equal(1, model.Count());
        }

        [Fact]
        public void Create_ReturnsAViewResult_WithAListOfUsers()
        {
            var result = controller.Create("");
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Create_ReturnsAViewResult_WithAListOfUsers_WhenUserExists()
        {
            var result = controller.Create("test1");
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Create_ReturnsAViewResult_WithAListOfUsers_WhenUserDoesNotExist()
        {
            var result = controller.Create("test4");
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Create_ReturnsAViewResult_WithAListOfUsers_WhenUserExistsAndEmailIsInvalid()
        {
            RegisterViewModel model = new RegisterViewModel
            {
                UserName = "test4",
                Email = "test4@gmail.com",
                Password = "123",
                ConfirmPassword = "123",
                FirstName = "Test1",
                LastName = "User",
                Age = 20,
                NIF = 123456789
            };
            var result = controller.Create(model, "");
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<RedirectToActionResult>(viewResult.Result);

            Assert.Equal("Index", mod.ActionName);
        }

        [Fact]
        public void Create_ReturnsAViewResult_WithAListOfUsers_WhenEmailAlreadyExists()
        {
            RegisterViewModel model = new RegisterViewModel
            {
                UserName = "test4",
                Email = "admin@gmail.com",
                Password = "123",
                ConfirmPassword = "123",
                FirstName = "Test4",
                LastName = "User",
                Age = 20,
                NIF = 123456789
            };
            var result = controller.Create(model, "");
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Create_ReturnsAViewResult_WithAListOfUsers_WhenUserAlreadyExists()
        {
            RegisterViewModel model = new RegisterViewModel
            {
                UserName = "admin",
                Email = "afsafsafsa@gmail.com",
                Password = "123",
                ConfirmPassword = "123",
                FirstName = "Test4",
                LastName = "User",
                Age = 20,
                NIF = 123456789
            };
            var result = controller.Create(model, "");
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Edit_ReturnsAViewResult_WithAListOfUsers_WhenUserDoesNotExist()
        {
            var result = controller.Edit("test4");
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_ReturnsAViewResult_WithAListOfUsers_WhenUserExists()
        {
            var user = dbContext.AppUser.First();
            var result = controller.Edit(user.Id);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AppUser>(viewResult.Model);
        }

        [Fact]
        public async void Edit_ReturnsAViewResult_WithAChangedParameter()
        {
            var user = dbContext.AppUser.First();
            user.FirstName = "TestTest";
            var result = controller.Edit(user);
            var viewResult = Assert.IsType<Task<IActionResult>>(result);

            var confirmUserChange = await dbContext.AppUser.FindAsync(user.Id);
            Assert.Equal("TestTest", confirmUserChange.FirstName);
        }

        [Fact]
        public async void Edit_ReturnsNotFount_WhenUserDoesntExist()
        {
            var user = new AppUser();
            var result = controller.Edit(user);
            var viewResult = Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Delete_ReturnsAViewResult_WithAListOfUsers_WhenUserDoesNotExist()
        {
            var result = controller.Delete("test4");
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ReturnsAViewResult_WithAListOfUsers_WhenUserExists()
        {
            var user = dbContext.AppUser.First();
            var result = controller.Delete(user.Id);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
