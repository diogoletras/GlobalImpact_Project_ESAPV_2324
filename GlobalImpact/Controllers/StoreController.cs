using GlobalImpact.Data;
using GlobalImpact.Models;
using GlobalImpact.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GlobalImpact.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;
        private List<Product> cartItems = CartItems.ListItems;

        /// <summary>
        /// Constutor da classe.
        /// </summary>
        /// <param name="context"> parametro para a database</param>
        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            var productsCat = await _context.ProductsCategory.ToListAsync();

            foreach (var product in products)
            {
                product.Tax = product.Tax * 100;
            }

            List<SelectListItem> category = new List<SelectListItem>();
            category.Add(new SelectListItem { Text = "", Value = "" });
            foreach (var cat in productsCat)
            {
                category.Add(new SelectListItem { Text = cat.Category.ToString(), Value = cat.ProductCategoryId.ToString() });
            }

            foreach (var prod in products)
            {
                foreach (var cat in productsCat)
                {
                    if (prod.ProductCategoryId.Equals(cat.ProductCategoryId.ToString()))
                    {
                        prod.Category = new ProductCategory
                        {
                            Category = cat.Category
                        };
                    }
                }
            }

            List<SelectListItem> order = new List<SelectListItem>();
            order.Add(new SelectListItem { Text = "", Value = "" });
            order.Add(new SelectListItem { Text = "Nome A-Z", Value = "NomeA-Z" });
            order.Add(new SelectListItem { Text = "Price Baixo a Alto", Value = "PriceBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Stock Baixo a Alto", Value = "StockBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Nome Z-A", Value = "NomeZ-A" });
            order.Add(new SelectListItem { Text = "Price Alto a Baixo", Value = "PriceAltoaBaixo" });
            order.Add(new SelectListItem { Text = "Stock Alto a Baixo", Value = "StockAltoaBaixo" });



            ViewBag.Order = order;
            ViewBag.Categorias = category;
            ViewBag.Items = cartItems;
            return View(products);
        }

        public async Task<IActionResult> Add(string id)
        {
            var products = await _context.Products.ToListAsync();
            var productsCat = await _context.ProductsCategory.ToListAsync();

            List<SelectListItem> category = new List<SelectListItem>();
            category.Add(new SelectListItem { Text = "", Value = "" });
            foreach (var cat in productsCat)
            {
                category.Add(new SelectListItem { Text = cat.Category.ToString(), Value = cat.ProductCategoryId.ToString() });
            }

            foreach (var prod in products)
            {
                foreach (var cat in productsCat)
                {
                    if (prod.ProductCategoryId.Equals(cat.ProductCategoryId.ToString()))
                    {
                        prod.Category = new ProductCategory
                        {
                            Category = cat.Category
                        };
                    }
                }
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == new Guid(id));

            if (cartItems.Exists(p => p.Id == product.Id))
            {
                foreach (var prod in cartItems)
                {
                    if(prod.Id == product.Id)
                    {
                        prod.Quantity++;
                    }
                }
            }
            else
            {
                cartItems.Add(product);
            }

            List<SelectListItem> order = new List<SelectListItem>();
            order.Add(new SelectListItem { Text = "", Value = "" });
            order.Add(new SelectListItem { Text = "Nome A-Z", Value = "NomeA-Z" });
            order.Add(new SelectListItem { Text = "Price Baixo a Alto", Value = "PriceBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Stock Baixo a Alto", Value = "StockBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Nome Z-A", Value = "NomeZ-A" });
            order.Add(new SelectListItem { Text = "Price Alto a Baixo", Value = "PriceAltoaBaixo" });
            order.Add(new SelectListItem { Text = "Stock Alto a Baixo", Value = "StockAltoaBaixo" });



            ViewBag.Order = order;

            ViewBag.Categorias = category;
            ViewBag.Items = cartItems;
            return View("Index",products);
        }

        public async Task<IActionResult> Filtra(string nome, float maxp, float minp, string categoria)
        {
            var products = await _context.Products.ToListAsync();

            if (nome != null)
            {
                products = products.Where(p => p.Name.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (maxp > 0 && maxp != null)
            {
                products = products.Where(p => p.Price <= maxp).ToList();
            }
            if (minp > 0 && minp != null)
            {
                products = products.Where(p => p.Price >= minp).ToList();
            }
            if (categoria != null)
            {
                products = products.Where(p => p.ProductCategoryId.Equals(categoria)).ToList();
            }

            List<SelectListItem> category = new List<SelectListItem>();
            category.Add(new SelectListItem { Text = "", Value = "" });
            var productsCat = await _context.ProductsCategory.ToListAsync();
            foreach (var cat in productsCat)
            {
                category.Add(new SelectListItem { Text = cat.Category.ToString(), Value = cat.ProductCategoryId.ToString() });
            }

            foreach (var prod in products)
            {
                foreach (var cat in productsCat)
                {
                    if (prod.ProductCategoryId.Equals(cat.ProductCategoryId.ToString()))
                    {
                        prod.Category = new ProductCategory
                        {
                            Category = cat.Category
                        };
                    }
                }
            }



            List<SelectListItem> order = new List<SelectListItem>();
            order.Add(new SelectListItem { Text = "", Value = "" });
            order.Add(new SelectListItem { Text = "Nome A-Z", Value = "NomeA-Z" });
            order.Add(new SelectListItem { Text = "Price Baixo a Alto", Value = "PriceBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Stock Baixo a Alto", Value = "StockBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Nome Z-A", Value = "NomeZ-A" });
            order.Add(new SelectListItem { Text = "Price Alto a Baixo", Value = "PriceAltoaBaixo" });
            order.Add(new SelectListItem { Text = "Stock Alto a Baixo", Value = "StockAltoaBaixo" });



            ViewBag.Order = order;
            ViewBag.Categorias = category;
            ViewBag.Items = cartItems;
            return View("Index", products);
        }

        public async Task<IActionResult> Order(string orderList)
        {
            var products = await _context.Products.ToListAsync();

            if(orderList != null)
            {
                if (orderList.Equals("NomeA-Z"))
                {
                    products = products.OrderBy(p => p.Name).ToList();
                }
                if (orderList.Equals("PriceBaixoaAlto"))
                {
                    products = products.OrderBy(p => p.Price).ToList();
                }
                if (orderList.Equals("StockBaixoaAlto"))
                {
                    products = products.OrderBy(p => p.Stock).ToList();
                }
                if (orderList.Equals("NomeZ-A"))
                {
                    products = products.OrderByDescending(p => p.Name).ToList();
                }
                if (orderList.Equals("PriceAltoaBaixo"))
                {
                    products = products.OrderByDescending(p => p.Price).ToList();
                }
                if (orderList.Equals("StockAltoaBaixo"))
                {
                    products = products.OrderByDescending(p => p.Stock).ToList();
                }
            }

            var productsCat = await _context.ProductsCategory.ToListAsync();

            List<SelectListItem> category = new List<SelectListItem>();
            category.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var cat in productsCat)
            {
                category.Add(new SelectListItem { Text = cat.Category.ToString(), Value = cat.ProductCategoryId.ToString() });
            }

            foreach (var prod in products)
            {
                foreach (var cat in productsCat)
                {
                    if (prod.ProductCategoryId.Equals(cat.ProductCategoryId.ToString()))
                    {
                        prod.Category = new ProductCategory
                        {
                            Category = cat.Category
                        };
                    }
                }
            }

            List<SelectListItem> order = new List<SelectListItem>();
            order.Add(new SelectListItem { Text = "", Value = "" });
            order.Add(new SelectListItem { Text = "Nome A-Z", Value = "NomeA-Z" });
            order.Add(new SelectListItem { Text = "Price Baixo a Alto", Value = "PriceBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Stock Baixo a Alto", Value = "StockBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Nome Z-A", Value = "NomeZ-A" });
            order.Add(new SelectListItem { Text = "Price Alto a Baixo", Value = "PriceAltoaBaixo" });
            order.Add(new SelectListItem { Text = "Stock Alto a Baixo", Value = "StockAltoaBaixo" });

            ViewBag.Order = order;
            ViewBag.Categorias = category;
            ViewBag.Items = cartItems;
            return View("Index", products);
        }

        public IActionResult Test()
        {
            return View();
        }

    }
}
