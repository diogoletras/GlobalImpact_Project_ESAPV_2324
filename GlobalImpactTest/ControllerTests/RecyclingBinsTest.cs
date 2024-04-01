using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;

namespace GlobalImpactTest.ControllerTests
{
    public class RecyclingBinsTest : IClassFixture<ApplicationDbContextFixture>
    {
        private Mock<FakeUserManager> userManagerMock;
        private ApplicationDbContext dbContext;
        private RecyclingBinsController controller;

        public RecyclingBinsTest(ApplicationDbContextFixture context)
        {
            dbContext = context.DbContext;

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

            controller = new RecyclingBinsController(dbContext, userManagerMock.Object);
            userManagerMock.Setup(u => u.GetUserAsync(controller.User)).ReturnsAsync(users.First());
        }

        [Fact]
        public void EcoLog_CanGetPageWithSuccess()
        {
            var result = controller.EcoLog();
            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void EcoLogin_CanGetPageWithSuccess()
        {
            var user = userManagerMock.Object.Users.FirstOrDefault();
            EcoLogViewModel model = new EcoLogViewModel
            {
                IdInput = user.UniqueCode
            };

            var result = controller.EcoLogin(model);

            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public async void RecyclingBins_CanGetCreatePage()
        {
            string option = null;
            var result = controller.Create(option);
            var redirectResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<RecyclingBin>(redirectResult.ViewData.Model);
            Assert.NotNull(model);
        }

        [Fact]
        public async void RecyclingBins_CanCreateEco()
        {
            string option = null;
            RecyclingBin recyclingBin = new RecyclingBin()
            {
                Id = Guid.NewGuid(),
                Type = "plastic",
                Latitude = 38.1,
                Longitude = -9.01,
                Description = "BLABLA",
                Capacity = 500,
                CurrentCapacity = 0,
                Status = true,
                RecyclingBinTypeId = "7c09a9ac-39ba-42ae-a25e-f5b4057c38b9"
            };

            var result = controller.Create(recyclingBin);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", redirectResult.ActionName);
            //Assert.Equal("RecyclingBins", );
        }

        [Fact]
        public async void RecyclingBin_CanCreateEco_GetPage()
        {
            string option = null;
            var result = controller.Create(option);
            var resultView = Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public async void RecyclingBin_CanGetDetails_WithSuccess()
        {
            var id = dbContext.RecyclingBins.First().Id;
            var result = controller.Details(id);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);

        }

        [Fact]
        public async void RecyclingBin_CanGetDetails_WithNoID()
        {

            var result = controller.Details(null);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<NotFoundResult>(resultView.Result);
        }


        [Fact]
        public async void RecyclingBin_CanGetDetails_WithNoEco()
        {
            var result = controller.Details(new Guid());
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<NotFoundResult>(resultView.Result);
        }
        [Fact]
        public async void RecyclingBin_CanUpdateType_WithSuccess()
        {
            var typeid = dbContext.RecyclingBinType.First().RecyclingBinTypeId;
            var result = controller.UpdateTypeChoise(typeid.ToString());
            var resultView = Assert.IsType<RecyclingBin>(result);
        }

        [Fact]
        public async void RecyclingBin_CanUpdateType_WithNoSuccess()
        {
            var result = controller.UpdateTypeChoise(null);
            var resultView = Assert.IsType<RecyclingBin>(result);

        }

        [Fact]
        public async void RecyclingBin_Can_Edit_WithSuccess()
        {
            var id = dbContext.RecyclingBins.First().Id;
            var result = controller.Edit(id);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }

        [Fact]
        public async void RecyclingBin_Can_Edit_WithNoID()
        {
            
            var result = controller.Edit(null);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<NotFoundResult>(resultView.Result);
        }

        [Fact]
        public async void RecyclingBin_Can_Edit_WithID_DoesntExist()
        {

            var result = controller.Edit(new Guid());
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<NotFoundResult>(resultView.Result);
        }

        [Fact]
        public async void RecyclingBin_Can_PostEdit_WithSuccess()
        {
            var recyclingBin = dbContext.RecyclingBins.First();
            var result = controller.Edit(recyclingBin.Id, recyclingBin);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<RedirectToActionResult>(resultView.Result);
            Assert.Equal("Index", mod.ActionName);
        }

        [Fact]
        public async void RecyclingBin_Can_PostEdit_WithDifferentID()
        {
            var recyclingBin = dbContext.RecyclingBins.First();
            var result = controller.Edit(new Guid(), recyclingBin);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<NotFoundResult>(resultView.Result);
        }

        [Fact]
        public async void RecyclingBin_Can_PostEdit_NoExistsRecyclingBin()
        { 
            var type = dbContext.RecyclingBinType.First();
            var recyclingBinNew = new RecyclingBin
            {
                Id = Guid.NewGuid(),
                Latitude = 0,
                Longitude = 0,
                Description = string.Empty,
                Capacity = 0,
                CurrentCapacity = 0,
                RecyclingBinTypeId = type.RecyclingBinTypeId.ToString(),
                Type = type.Type,
                Status = true

            };
            var result = controller.Edit(recyclingBinNew.Id, recyclingBinNew);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<NotFoundResult>(resultView.Result);
        }


        [Fact]
        public async void RecyclingBin_Can_PostEdit_ModelStateInvalid()
        {
            var type = dbContext.RecyclingBinType.First();
            var recyclingBinNew = new RecyclingBin
            {
                Id = Guid.NewGuid()
            };
            var result = controller.Edit(recyclingBinNew.Id, recyclingBinNew);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<NotFoundResult>(resultView.Result);
        }


        [Fact]
        public async void RecyclingBin_Can_Delete_WithSuccess()
        {
            var id = dbContext.RecyclingBins.First().Id;
            var result = controller.Delete(id);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }

        [Fact]
        public async void RecyclingBin_Can_Delete_WithIDNull()
        {
            
            var result = controller.Delete(null);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<NotFoundResult>(resultView.Result);
        }

        [Fact]
        public async void RecyclingBin_Can_Delete_WithnoRecyclingBin()
        {

            var result = controller.Delete(new Guid());
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<NotFoundResult>(resultView.Result);
        }


        [Fact]
        public async void RecyclingBin_Can_PostDelete_WithNoRecyclingBin()
        {

            var result = controller.DeleteConfirmed(new Guid());
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<NotFoundResult>(resultView.Result);
        }

        [Fact]
        public async void RecyclingBin_Can_PostDelete_WithNoId()
        {
            var result = controller.DeleteConfirmed(null);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<NotFoundResult>(resultView.Result);
        }

        [Fact]
        public async void RecyclingBin_Can_PostDelete_WithSuccess()
        {
            var id = dbContext.RecyclingBins.First().Id;
            var result = controller.DeleteConfirmed(id);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<RedirectToActionResult>(resultView.Result);
            Assert.Equal("Index", mod.ActionName);
        }

        [Fact]
        public async void RecyclingBin_Can_Filter_WithSuccess()
        {
            var type = dbContext.RecyclingBinType.First();
            FilterViewModel filter = new FilterViewModel
            {
                RecyclingBins = dbContext.RecyclingBins,
                RecyclingBin = dbContext.RecyclingBins.First(),
                Capacity = 100,
                CurrentCapacity = 100,
                Status = "false",
                Type = type.Type
            };
            var result = controller.Filter(filter);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }


        [Fact]
        public async void GoogleMaps_WithSuccess()
        {
            var result = controller.GoogleMaps();
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
            Assert.NotNull(mod);
        }

        [Fact]
        public async void Filtrar_GoogleMaps_WithSuccess()
        {
            var type = dbContext.RecyclingBinType.First();
            var result = controller.FiltrarMapa(null, -1, -1, type.Type);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
            var model = Assert.IsType<List<RecyclingBin>>(mod.Model);
            Assert.NotNull(mod);
        }

        [Fact]
        public async void Filtrar_GoogleMaps_WithNoFilter()
        {
            var result = controller.FiltrarMapa(null, 0, 0, null);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<RedirectToActionResult>(resultView.Result);
            Assert.Equal("GoogleMaps", mod.ActionName);
        }

        [Fact]
        public async void FullBins_CanGetPage()
        {
            var result = controller.FullBins();
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
			var model = Assert.IsType<RecyclingBin[]>(mod.Model);
            Assert.Equal(2,model.Count());
			Assert.NotNull(mod);

		}

		[Fact]
		public async void FullBins_CheckAfterService()
		{
			var result = controller.FullBins();
			var resultView = Assert.IsType<Task<IActionResult>>(result);
			var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
			var model = Assert.IsType<RecyclingBin[]>(mod.Model);
			Assert.Equal(2, model.Count());
			Assert.NotNull(mod);

            var bin = dbContext.RecyclingBins.FirstOrDefault(b => b.Status == true);
            bin.Status = false;
            dbContext.Update(bin);
            dbContext.SaveChanges();

			var result2 = controller.FullBins();
			var resultView2 = Assert.IsType<Task<IActionResult>>(result2);
			var mod2 = Assert.IsAssignableFrom<ViewResult>(resultView2.Result);
			var model2 = Assert.IsType<RecyclingBin[]>(mod.Model);
			Assert.Equal(1, model2.Count());
			Assert.NotNull(mod2);

		}


	}
}
