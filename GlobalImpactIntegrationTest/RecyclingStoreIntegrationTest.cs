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
	public class RecyclingStoreIntegrationTest : IClassFixture<ApplicationDbContextFixture>
	{
		private ApplicationDbContext dbContext;
		private Mock<RoleManager<IdentityRole>> roleManagerMock;
		private Mock<FakeUserManager> userManagerMock;
		private Mock<FakeSignInManager> signInManagerMock;
		private AccountController controllerAccount;
		private RecyclingBinsController controllerRecBin;
		private RecyclingTransactionController controllerRecBinTrans;
		private StoreController controllerStore;

		public RecyclingStoreIntegrationTest(ApplicationDbContextFixture context)
		{
			dbContext = context.DbContext;

			var listRoles = dbContext.Roles.AsQueryable();

			roleManagerMock = new Mock<RoleManager<IdentityRole>>(
			   new Mock<IRoleStore<IdentityRole>>().Object,
			   new IRoleValidator<IdentityRole>[0],
			   new Mock<ILookupNormalizer>().Object,
			   new Mock<IdentityErrorDescriber>().Object,
			   new Mock<ILogger<RoleManager<IdentityRole>>>().Object);

			roleManagerMock
				.Setup(r => r.Roles).Returns(listRoles);

			var users = dbContext.Users.AsQueryable();

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
				roleManagerMock.Object, new EmailSender(), dbContext);

			controllerRecBinTrans = new RecyclingTransactionController(dbContext, signInManagerMock.Object);
			controllerStore = new StoreController(dbContext);
		}

		[Fact]
		public void RecyclingTransaction_UserEcoLogin()
		{
			var bin = dbContext.RecyclingBins.First();
			var type = dbContext.RecyclingBinType.FirstOrDefault(r => r.RecyclingBinTypeId == new Guid(bin.RecyclingBinTypeId)).Type;
			var user = dbContext.Users.First().UserName;
			var result = controllerRecBinTrans.Reciclar(bin.Id, type, user);
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
		}

		[Fact]
		public void RecyclingTransaction_RecycleItem()
		{
			var bin = dbContext.RecyclingBins.First();
			var type = dbContext.RecyclingBinType.FirstOrDefault(r => r.RecyclingBinTypeId == new Guid(bin.RecyclingBinTypeId)).Type;
			var nome = dbContext.Users.First().UserName;
			var itemName = "Garrafa";

			var result = controllerRecBinTrans.Rec(bin.Id.ToString(), type, nome, itemName, new List<RecItems>());
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
		}

		[Fact]
		public void RecyclingTransaction_FinishRecycle()
		{
			var bin = dbContext.RecyclingBins.First();
			var type = dbContext.RecyclingBinType.FirstOrDefault(r => r.RecyclingBinTypeId == new Guid(bin.RecyclingBinTypeId)).Type;
			var nome = dbContext.Users.First().UserName;
			var peso = 0.5;
			var pontos = 5;
			var result = controllerRecBinTrans.FinishRecycling(bin.Id.ToString(), nome, peso, pontos);
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<RedirectToActionResult>(resultView.Result);

			Assert.Equal("EcoLogin", mod.ActionName);
			Assert.Equal("RecyclingBins", mod.ControllerName);

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
