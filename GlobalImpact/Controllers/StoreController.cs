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

        /// <summary>
        /// Funçao HTTPGet que retorna uma view com a lista dos produtos.
        /// </summary>
        /// <returns>retorna uma view que contém a lista de produtos.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
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

            List<SelectListItem> order = new List<SelectListItem>();
            order.Add(new SelectListItem { Text = "", Value = "" });
            order.Add(new SelectListItem { Text = "Nome A-Z", Value = "NomeA-Z" });
            order.Add(new SelectListItem { Text = "Ponto Baixo a Alto", Value = "PontoBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Stock Baixo a Alto", Value = "StockBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Nome Z-A", Value = "NomeZ-A" });
            order.Add(new SelectListItem { Text = "Ponto Alto a Baixo", Value = "PontoAltoaBaixo" });
            order.Add(new SelectListItem { Text = "Stock Alto a Baixo", Value = "StockAltoaBaixo" });



            ViewBag.Order = order;
            ViewBag.Categorias = category;
            ViewBag.Items = cartItems;
            return View(products);
        }
        /// <summary>
        /// Funçao HTTPGet que retorna uma view com a lista atualizada de produtos, depois de adicionar um.
        /// </summary>
        /// <param name="id">id do produto a ser adicionado.</param>
        /// <returns>retorna uma lista de produtos atualizada.</returns
        [HttpGet]
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
            order.Add(new SelectListItem { Text = "Ponto Baixo a Alto", Value = "PontoBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Stock Baixo a Alto", Value = "StockBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Nome Z-A", Value = "NomeZ-A" });
            order.Add(new SelectListItem { Text = "Ponto Alto a Baixo", Value = "PontoAltoaBaixo" });
            order.Add(new SelectListItem { Text = "Stock Alto a Baixo", Value = "StockAltoaBaixo" });



            ViewBag.Order = order;

            ViewBag.Categorias = category;
            ViewBag.Items = cartItems;
            return View("Index",products);
        }
        /// <summary>
        /// Funçao HTTPGet que retorna uma view com a lista atualizada de produtos, depois de filtrar pelos dados pretendidos pelo user.
        /// </summary>
        /// <param name="nome">parametro nome para filtragem dos produtos.</param>
        /// <param name="maxp">parametro máximo de preço, maxp, para filtragem dos produtos.</param>
        /// <param name="minp">parametro mínimo de preço, minp, para filtragem dos produtos.</param>
        /// <param name="categoria">parametro categoria, categoria, para filtragem dos produtos.</param>
        /// <returns>retorna uma view com a lista atualizada de produtos, depois de «filtrar pelos dados pretendidos pelo user.</returns>
        [HttpGet]
        public async Task<IActionResult> Filtra(string nome, float maxp, float minp, string categoria)
        {
            var products = await _context.Products.ToListAsync();

            if (nome != null)
            {
                products = products.Where(p => p.Name.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (maxp > 0 && maxp != null)
            {
                products = products.Where(p => p.Points <= maxp).ToList();
            }
            if (minp > 0 && minp != null)
            {
                products = products.Where(p => p.Points >= minp).ToList();
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
            order.Add(new SelectListItem { Text = "Ponto Baixo a Alto", Value = "PontoBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Stock Baixo a Alto", Value = "StockBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Nome Z-A", Value = "NomeZ-A" });
            order.Add(new SelectListItem { Text = "Ponto Alto a Baixo", Value = "PontoAltoaBaixo" });
            order.Add(new SelectListItem { Text = "Stock Alto a Baixo", Value = "StockAltoaBaixo" });



            ViewBag.Order = order;
            ViewBag.Categorias = category;
            ViewBag.Items = cartItems;
            return View("Index", products);
        }

        /// <summary>
        /// Funçao que retorna uma view com a lista atualizada de produtos, depois de ordenar pela forma pretendida.
        /// </summary>
        /// <param name="orderList">lista de produtos a ser ordenada.</param>
        /// <returns>retorna uma view com a lista atualizada de produtos, depois de ordenar pela forma pretendida.</returns>
        [HttpGet]
        public async Task<IActionResult> Order(string orderList)
        {
            var products = await _context.Products.ToListAsync();

            if(orderList != null)
            {
                if (orderList.Equals("NomeA-Z"))
                {
                    products = products.OrderBy(p => p.Name).ToList();
                }
                if (orderList.Equals("PontoBaixoaAlto"))
                {
                    products = products.OrderBy(p => p.Points).ToList();
                }
                if (orderList.Equals("StockBaixoaAlto"))
                {
                    products = products.OrderBy(p => p.Stock).ToList();
                }
                if (orderList.Equals("NomeZ-A"))
                {
                    products = products.OrderByDescending(p => p.Name).ToList();
                }
                if (orderList.Equals("PontoAltoaBaixo"))
                {
                    products = products.OrderByDescending(p => p.Points).ToList();
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
            order.Add(new SelectListItem { Text = "Ponto Baixo a Alto", Value = "PontoBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Stock Baixo a Alto", Value = "StockBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Nome Z-A", Value = "NomeZ-A" });
            order.Add(new SelectListItem { Text = "Ponto Alto a Baixo", Value = "PontoAltoaBaixo" });
            order.Add(new SelectListItem { Text = "Stock Alto a Baixo", Value = "StockAltoaBaixo" });

            ViewBag.Order = order;
            ViewBag.Categorias = category;
            ViewBag.Items = cartItems;
            return View("Index", products);
        }

        /// <summary>
        /// Funçao que retorna uma view com a lista atualizada de produtos, depois de um ser eliminado.
        /// </summary>
        /// <param name="Id">id do produto a ser eliminado.</param>
        /// <returns>retorna uma view com a lista atualizada de produtos, depois de um ser eliminado.</returns>
        [HttpGet]
        public async Task<IActionResult> DeleteFromCart(string Id)
        {
            cartItems.RemoveAll(p => p.Id == new Guid(Id));

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

            List<SelectListItem> order = new List<SelectListItem>();
            order.Add(new SelectListItem { Text = "", Value = "" });
            order.Add(new SelectListItem { Text = "Nome A-Z", Value = "NomeA-Z" });
            order.Add(new SelectListItem { Text = "Ponto Baixo a Alto", Value = "PontoBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Stock Baixo a Alto", Value = "StockBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Nome Z-A", Value = "NomeZ-A" });
            order.Add(new SelectListItem { Text = "Ponto Alto a Baixo", Value = "PontoAltoaBaixo" });
            order.Add(new SelectListItem { Text = "Stock Alto a Baixo", Value = "StockAltoaBaixo" });

            ViewBag.Order = order;
            ViewBag.Categorias = category;
            ViewBag.Items = cartItems;

            return View("Index", products);
        }
        /// <summary>
        /// Função que retorna uma view com a lista atualizada de produtos, depois desses serem eliminados.
        /// </summary>
        /// <returns>retorna uma view com a lista atualizada de produtos, depois desses serem eliminados.</returns>
        [HttpGet]
        public async Task<IActionResult> DeleteAllFromCart()
        {
            cartItems.Clear();

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

            List<SelectListItem> order = new List<SelectListItem>();
            order.Add(new SelectListItem { Text = "", Value = "" });
            order.Add(new SelectListItem { Text = "Nome A-Z", Value = "NomeA-Z" });
            order.Add(new SelectListItem { Text = "Ponto Baixo a Alto", Value = "PontoBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Stock Baixo a Alto", Value = "StockBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Nome Z-A", Value = "NomeZ-A" });
            order.Add(new SelectListItem { Text = "Ponto Alto a Baixo", Value = "PontoAltoaBaixo" });
            order.Add(new SelectListItem { Text = "Stock Alto a Baixo", Value = "StockAltoaBaixo" });

            ViewBag.Order = order;
            ViewBag.Categorias = category;
            ViewBag.Items = cartItems;

            return View("Index", products);
        }

        /// <summary>
        /// Função HTTPGet que retorna uma view com a lista atualizada de produtos, depois desses serem eliminados.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> UpdateQuantity(string id, int quantity)
        {
            var products = await _context.Products.ToListAsync();

            foreach (var prod in cartItems)
            {
                if (prod.Id == new Guid(id))
                {
                    prod.Quantity = quantity;
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
            order.Add(new SelectListItem { Text = "Ponto Baixo a Alto", Value = "PontoBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Stock Baixo a Alto", Value = "StockBaixoaAlto" });
            order.Add(new SelectListItem { Text = "Nome Z-A", Value = "NomeZ-A" });
            order.Add(new SelectListItem { Text = "Ponto Alto a Baixo", Value = "PontoAltoaBaixo" });
            order.Add(new SelectListItem { Text = "Stock Alto a Baixo", Value = "StockAltoaBaixo" });

            ViewBag.Order = order;
            ViewBag.Categorias = category;
            ViewBag.Items = cartItems;

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            ViewBag.Items = cartItems;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FinalizeCheckout(string name, int total)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == name);
            if (user.Points>=total)
            {
                ProductTransactions transaction = new ProductTransactions
                {
                    TransactionId = Guid.NewGuid(),
                    UserId = new Guid(user.Id),
                    Date = DateTime.Now
                };

                foreach (var item in cartItems)
                {
                    transaction.Id = Guid.NewGuid();
                    transaction.ProductId = item.Id;
                    transaction.Points = item.Points;
                    transaction.Quantity = item.Quantity;

                    user.Points -= item.Points * item.Quantity;

                    _context.ProductTransactions.Add(transaction);
                    _context.SaveChanges();
                }

                cartItems.Clear();
                return RedirectToAction("Index");

            }

            return View("Checkout");
        }
    }
}
