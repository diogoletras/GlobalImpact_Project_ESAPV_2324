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
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;


namespace GlobalImpactTest.ControllerTests
{
    public class RecyclingBinsTransactionTest : IClassFixture<ApplicationDbContextFixture>
    {
        private Mock<FakeSignInManager> signInManagerMock;
        private ApplicationDbContext dbContext;
        private RecyclingTransactionController controller;

        public RecyclingBinsTransactionTest(ApplicationDbContextFixture context)
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

            signInManagerMock = new Mock<FakeSignInManager>();
            signInManagerMock.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<AppUser>(), "123", It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);

            controller = new RecyclingTransactionController(dbContext, signInManagerMock.Object);

        }
        [Fact]
        public void RecyclingTransaction_CanGetPageWithSuccess()
        {
            var result = controller.Index();
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }

        [Fact]
        public void RecyclingTransaction_CanGetTransactionList_WithSuccess()
        {
            var userId = dbContext.Users.First().Id;
            var result = controller.TransacionList(userId);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }

        [Fact]
        public void RecyclingTransaction_CanGetRecycle_WithSuccess()
        {
            var bin = dbContext.RecyclingBins.First();
            var type = dbContext.RecyclingBinType.FirstOrDefault(r => r.RecyclingBinTypeId == new Guid(bin.RecyclingBinTypeId)).Type;
            var user = dbContext.Users.First().UserName;
            var result = controller.Reciclar(bin.Id, type, user);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }


        [Fact]
        public void RecyclingTransaction_CanPostRecycle_WithSuccess()
        {
            var bin = dbContext.RecyclingBins.First();
            var type = dbContext.RecyclingBinType.FirstOrDefault(r => r.RecyclingBinTypeId == new Guid(bin.RecyclingBinTypeId)).Type;
            var nome = dbContext.Users.First().UserName;
            var itemName = "Garrafa";

            var result = controller.Rec(bin.Id.ToString(), type,nome, itemName, new List<RecItems>());
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }

        [Fact]
        public void RecyclingTransaction_CanPostFinishRecycle_WithSuccess()
        {
            var bin = dbContext.RecyclingBins.First();
            var type = dbContext.RecyclingBinType.FirstOrDefault(r => r.RecyclingBinTypeId == new Guid(bin.RecyclingBinTypeId)).Type;
            var nome = dbContext.Users.First().UserName;
            var peso = 0.5;
            var pontos = 5;
            var result = controller.FinishRecycling(bin.Id.ToString(), nome, peso, pontos);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<RedirectToActionResult>(resultView.Result);

            Assert.Equal("EcoLogin", mod.ActionName);
            Assert.Equal("RecyclingBins", mod.ControllerName);

        }

        [Fact]
        public void RecyclingTransaction_CanPostFinishRecycle_WithSuccessView()
        {
            
            var nome = dbContext.Users.First().UserName;
            var peso = 0.5;
            var pontos = 5;
            var result = controller.FinishRecycling(new Guid().ToString(), "", peso, pontos);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }

        [Fact]
        public void RecyclingTransaction_CanGetCancelTransaction_WithSuccessView()
        {

            var bin = dbContext.RecyclingBins.First();
            var type = dbContext.RecyclingBinType.FirstOrDefault(r => r.RecyclingBinTypeId == new Guid(bin.RecyclingBinTypeId)).Type;
            var nome = dbContext.Users.First().UserName;
            var result = controller.CancelTrans(bin.Id.ToString(), nome, type);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }

        [Fact]
        public void RecyclingTransaction_CanPostCancelConfirm_WithSuccess()
        {
            var bin = dbContext.RecyclingBins.First();
   
            var result = controller.CancelConfirm(bin.Id.ToString());
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<RedirectToActionResult>(resultView.Result);

            Assert.Equal("EcoLogin", mod.ActionName);
            Assert.Equal("RecyclingBins", mod.ControllerName);

        }







    }
}
