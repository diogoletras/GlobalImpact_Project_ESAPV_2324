﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalImpact.Controllers;
using GlobalImpact.Data;
using GlobalImpact.Models;
using GlobalImpactTest.FakeManagers;
using GlobalImpactTest.IClassFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
            var viewResult = Assert.IsType<ViewResult>(result.Result);
			var model = Assert.IsType<Product>(viewResult.Model);
            Assert.NotNull(model);

		}


        [Fact]
        public async void DeleteProduct_GetPage()
        {
            var prod1 = dbContext.Products.FirstOrDefault();
			var result = controller.Delete(prod1.Id);
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
			var model = Assert.IsType<Product>(mod.Model);
		}

		[Fact]
		public async void CanDeleteProduct_Success()
		{
			var prod1 = dbContext.Products.FirstOrDefault();
			var result = controller.Delete(prod1.Id);
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
			var model = Assert.IsType<Product>(mod.Model);

            dbContext.Products.Remove(prod1);
            dbContext.SaveChanges();

			var result2 = controller.Delete(prod1.Id);
			var resultView2 = Assert.IsType<Task<IActionResult>>(result2);
			var mod2 = Assert.IsAssignableFrom<NotFoundResult>(resultView2.Result);
		}

        [Fact]
        public async void EditProduct_CanGetPage()
        {
			var prod1 = dbContext.Products.FirstOrDefault();
			var result = controller.Edit(prod1.Id);
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
			var model = Assert.IsType<Product>(mod.Model);
		}

		[Fact]
		public async void CanEditProduct_Success()
		{
			var prod1 = dbContext.Products.FirstOrDefault();
			var result = controller.Edit(prod1.Id);
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
			var model = Assert.IsType<Product>(mod.Model);

            prod1.Description = "Editado";
			dbContext.Products.Update(prod1);
			dbContext.SaveChanges();

			var result2 = controller.Edit(prod1.Id);
			var resultView2 = Assert.IsType<Task<IActionResult>>(result2);
			var mod2 = Assert.IsAssignableFrom<ViewResult>(resultView2.Result);
			var model2 = Assert.IsType<Product>(mod2.Model);
		}
	}
}