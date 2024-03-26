using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalImpact.Controllers;
using GlobalImpact.Data;
using GlobalImpact.Models;
using GlobalImpactTest.FakeManagers;
using GlobalImpactTest.IClassFixture;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GlobalImpactTest.ControllerTests
{
    public class ProductsTest : IClassFixture<ApplicationDbContextFixture> 
    {
        private Mock<FakeUserManager> userManagerMock;
        private ApplicationDbContext dbContext;
        private ProductsController controller;

        public ProductsTest(ApplicationDbContextFixture context)
        {
            dbContext = context.DbContext;

            var users = new List<AppUser>
            {
                new AppUser
                {
                    UniqueCode = Guid.NewGuid().ToString(),
                    UserName = "test1",
                    FirstName = "Test",
                    LastName = "User",
                    Age = 20,
                    Points = 0,
                    Email = "234@gmail.com",
                    NIF = 123456789
                },
                new AppUser
                {
                    UniqueCode = Guid.NewGuid().ToString(),
                    UserName = "test2",
                    FirstName = "Test2",
                    LastName = "User2",
                    Age = 30,
                    Points = 0,
                    Email = "123@gmail.com",
                    NIF = 987654321
                },
                new AppUser()
                {
                    UniqueCode = Guid.NewGuid().ToString(),
                    UserName = "test3",
                    FirstName = "Test3",
                    LastName = "User3",
                    Age = 40,
                    Points = 0,
                    Email = "345@gmail.com",
                    NIF = 987654321
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

            controller = new ProductsController(dbContext);
            userManagerMock.Setup(u => u.GetUserAsync(controller.User)).ReturnsAsync(users.First());
        }

        [Fact]
        public void Index_CanGetPageSuccess()
        {
            var result = controller.Index();
            var redirectResult = Assert.IsType<Task<IActionResult>>(result);
            var model = Assert.IsAssignableFrom<ViewResult>(redirectResult.Result);
            Assert.NotNull(model);
        }

        [Fact]
        public void CanFilterProducts()
        {
            string categoria = dbContext.ProductsCategory.First().ProductCategoryId.ToString();
            var products = dbContext.Products.ToList();
            Assert.Equal(5, products.Count);

            var filterProducts = dbContext.Products.Where(p => p.ProductCategoryId.ToString().Equals(categoria)).ToList() ;
            Assert.Equal(1, filterProducts.Count);

            var result = controller.Filtra(null,float.NaN,float.NaN,categoria);
            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void Create_CanGetPageSuccess()
        {
            var result = controller.Create();
            var redirectResult = Assert.IsType<Task<IActionResult>>(result);
            var model = Assert.IsAssignableFrom<ViewResult>(redirectResult.Result);
            Assert.NotNull(model);
        }

        [Fact]
        public void CanCreateProductsSuccess()
        {
            var products = dbContext.Products.ToList();
            Assert.Equal(5, products.Count);

            string categoria = dbContext.ProductsCategory.First().ProductCategoryId.ToString();
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Batatas",
                Description = "",
                Points = 2,
                Stock = 30,
                ProductCategoryId = categoria,
            };

            var result = controller.Create(product);
            var productsCreate = dbContext.Products.ToList();
            Assert.Equal(6, productsCreate.Count);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void CanCreateProductsFail()
        {
            var products = dbContext.Products.ToList();
            Assert.Equal(5, products.Count);

            var result = controller.Create(null);
            var productsCreate = dbContext.Products.ToList();
            Assert.Equal(5, productsCreate.Count);

            var model = Assert.IsType<ViewResult>(result.Result);
            Assert.NotNull(model);
        }


    }
}
