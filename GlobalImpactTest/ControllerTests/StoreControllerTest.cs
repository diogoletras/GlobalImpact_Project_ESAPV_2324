﻿using GlobalImpact.Controllers;
using GlobalImpact.Data;
using GlobalImpact.Interfaces;
using GlobalImpact.Utils;
using GlobalImpactTest.IClassFixture;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;
using System.Text;
using System.Threading.Tasks;

namespace GlobalImpactTest.ControllerTests
{
    public class StoreControllerTest : IClassFixture<ApplicationDbContextFixture>
    {
        private ApplicationDbContext dbContext;
        private StoreController controller;
        private IEmailService emailService;


        public StoreControllerTest(ApplicationDbContextFixture context)
        {
            dbContext = context.DbContext;
            emailService = new EmailService(new ConfigurationManager());

            controller = new StoreController(dbContext, emailService);
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
            var order = "PontoBaixoaAlto";
            var result = controller.Order(order);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);

             order = "PontoAltoaBaixo";
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

        [Fact]
        public void Products_CanGetCheckout()
        {
            var result = controller.Checkout();
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<ViewResult>(resultView.Result);
        }

        [Fact]
        public void Products_CanPostFinalizeCheckout()
        {
            var user = dbContext.Users.First();
            var productList = CartItems.ListItems;
            var total = 0;

            var products = dbContext.Products.ToList();

            foreach (var product in products)
            {
                controller.Add(product.Id.ToString());
            }
            

            foreach (var product in productList)
            {
                total += product.Points*product.Quantity;
            }

            var result = controller.FinalizeCheckout(user.UserName, total);
            var resultView = Assert.IsType<Task<IActionResult>>(result);
            var mod = Assert.IsAssignableFrom<RedirectToActionResult>(resultView.Result);
        }
    }
}