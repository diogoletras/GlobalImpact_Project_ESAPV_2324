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
	public class ProductStoreIntegrationTest : IClassFixture<ApplicationDbContextFixture>
	{
		private ApplicationDbContext dbContext;
		private StoreController controllerStore;
		private ProductsController controllerProducts;

		public ProductStoreIntegrationTest(ApplicationDbContextFixture context)
		{
			dbContext = context.DbContext;
			controllerProducts = new ProductsController(dbContext);
			controllerStore = new StoreController(dbContext);
		}

		[Fact]
		public void CanCreateProduct()
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

			var result = controllerProducts.Create(product);
			var viewResult = Assert.IsType<ViewResult>(result.Result);
			var model = Assert.IsType<Product>(viewResult.Model);
			Assert.NotNull(model);
		}

		[Fact]
		public async void CanEditProduct()
		{
			var prod1 = dbContext.Products.FirstOrDefault();
			var result = controllerProducts.Edit(prod1.Id);
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
			var model = Assert.IsType<Product>(mod.Model);

			prod1.Description = "Editado";
			dbContext.Products.Update(prod1);
			dbContext.SaveChanges();

			var result2 = controllerProducts.Edit(prod1.Id);
			var resultView2 = Assert.IsType<Task<IActionResult>>(result2);
			var mod2 = Assert.IsAssignableFrom<ViewResult>(resultView2.Result);
			var model2 = Assert.IsType<Product>(mod2.Model);
		}

		[Fact]
		public async void CanDeleteProduct()
		{
			var prod1 = dbContext.Products.FirstOrDefault();
			var result = controllerProducts.Delete(prod1.Id);
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
			var model = Assert.IsType<Product>(mod.Model);

			dbContext.Products.Remove(prod1);
			dbContext.SaveChanges();

			var result2 = controllerProducts.Delete(prod1.Id);
			var resultView2 = Assert.IsType<Task<IActionResult>>(result2);
			var mod2 = Assert.IsAssignableFrom<NotFoundResult>(resultView2.Result);
		}

		[Fact]
		public void Store_CanGetPage()
		{
			var result = controllerStore.Index();
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
		}

		[Fact]
		public void Store_CanAddProduct()
		{
			var id = dbContext.Products.First().Id;
			var result = controllerStore.Add(id.ToString());
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
		}

		[Fact]
		public void Store_UpdateQuantityProduct()
		{
			var id = dbContext.Products.First().Id;
			controllerStore.Add(id.ToString());
			var result = controllerStore.UpdateQuantity(id.ToString(), 2);
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<JsonResult>(resultView.Result);
		}

		[Fact]
		public void Store_CanFinalizeCheckout()
		{
			var user = dbContext.Users.First();
			var productList = CartItems.ListItems;
			var total = 0;

			var products = dbContext.Products.ToList();

			foreach (var product in products)
			{
				controllerStore.Add(product.Id.ToString());
			}


			foreach (var product in productList)
			{
				total += product.Points * product.Quantity;
			}

			var result = controllerStore.FinalizeCheckout(user.UserName, total);
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<RedirectToActionResult>(resultView.Result);
		}

	}
}
