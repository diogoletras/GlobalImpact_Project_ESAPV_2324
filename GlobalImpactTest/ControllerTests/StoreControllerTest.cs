using GlobalImpact.Controllers;
using GlobalImpact.Data;
using GlobalImpactTest.IClassFixture;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalImpactTest.ControllerTests
{
    public class StoreControllerTest : IClassFixture<ApplicationDbContextFixture>
    {
        private ApplicationDbContext dbContext;
        private StoreController controller;


        public StoreControllerTest(ApplicationDbContextFixture context)
        {
            dbContext = context.DbContext;

            controller = new StoreController(dbContext);
        }


        [Fact]
        public void Products_CanGetPageWithSuccess()
        {
            var result = controller.Index();
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }

        [Fact]
        public void Products_CanAddProductWithSuccess()
        {
            var id = dbContext.Products.First().Id;
            var result = controller.Add(id.ToString());
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);  
        }

        [Fact]
        public void Products_CanFilterProductWithSuccess()
        {
            var nome = dbContext.Products.First().Name;
            var cat = dbContext.ProductsCategory.First().Category;
            var minp = 2;
            var maxp = 5;
            var result = controller.Filtra(nome, maxp, minp, cat);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }

        [Fact]
        public void Products_CanOrderProductWithSuccess()
        {
            var order = "PriceBaixoaAlto";
            var result = controller.Order(order);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);

             order = "PriceAltoaBaixo";
             result = controller.Order(order);
             resultView = Assert.IsType<Task<IActionResult>>(result);
             mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);

             order = "NomeA-Z";
             result = controller.Order(order);
             resultView = Assert.IsType<Task<IActionResult>>(result);
             mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);

             order = "NomeZ-A";
             result = controller.Order(order);
             resultView = Assert.IsType<Task<IActionResult>>(result);
             mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);

             order = "StockAltoaBaixo";
             result = controller.Order(order);
             resultView = Assert.IsType<Task<IActionResult>>(result);
             mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);

             order = "StockBaixoaAlto";
             result = controller.Order(order);
             resultView = Assert.IsType<Task<IActionResult>>(result);
             mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }

        [Fact]
        public void Products_CanDeleteProductWithSuccess()
        {
            var id = dbContext.Products.First().Id;
            var result = controller.DeleteFromCart(id.ToString());
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }

        [Fact]
        public void Products_CanDeleteAllProductsWithSuccess()
        {
            var result = controller.DeleteAllFromCart();
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }

        [Fact]
        public void Products_UpdateQuantityProductsWithSuccess()
        {
            var id = dbContext.Products.First().Id;
            controller.Add(id.ToString());
            var result = controller.UpdateQuantity(id.ToString(), 2);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<JsonResult>(resultView.Result);
        }



    }
}