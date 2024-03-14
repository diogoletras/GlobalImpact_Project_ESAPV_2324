using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GlobalImpact.Data;
using GlobalImpact.Models;
using Microsoft.AspNetCore.Authorization;

namespace GlobalImpact.Controllers
{
    public class ProductsController : Controller
    {
        /// <summary>
        /// Classe de controlador de produtos.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constutor da classe.
        /// </summary>
        /// <param name="context"> parametro para a database</param>
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        // GET: Products
        /// <summary>
        /// Função HttpPost retorna a página da lista de produtos.
        /// </summary>
        /// <returns>retorna a página da lista de produtos.</returns>
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();


			List<SelectListItem> category = new List<SelectListItem>();
            category.Add(new SelectListItem { Text = "", Value = "" });
            var productsCat = await _context.ProductsCategory.ToListAsync();
            foreach(var cat in productsCat)
            {
                category.Add(new SelectListItem { Text = cat.Category.ToString(), Value = cat.ProductCategoryId.ToString() });
			}
            foreach(var prod in products)
            {
                foreach(var cat in productsCat)
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
			ViewBag.Categorias = category;
            return View(products);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Filtra(string nome , float maxp, float minp, string categoria)
        {
            var products = await _context.Products.ToListAsync();

            if (nome!= null)
            {
                products = products.Where(p => p.Name.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (maxp>0 && maxp!=null)
            {
                products = products.Where(p => p.Price <= maxp).ToList();
            }
            if (minp>0 && minp!=null)
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
			ViewBag.Categorias = category;
			return View("Index", products);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        // GET: Products/Details/5
        /// <summary>
        /// Função HttpGet retorna os detalhes do produto.
        /// </summary>
        /// <returns>retorna os detalhes do produto.</returns>

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        // GET: Products/Create
        /// <summary>
        /// Função HttpGet para a criação de um produto.
        /// </summary>
        /// <returns>retorna a criação do produto.</returns>

        public async Task<IActionResult> Create()
        {
            List<SelectListItem> category = new List<SelectListItem>();
            var productsCat = await _context.ProductsCategory.ToListAsync();
            foreach (var cat in productsCat)
            {
                category.Add(new SelectListItem { Text = cat.Category.ToString(), Value = cat.ProductCategoryId.ToString() });
            }
            ViewBag.Categorias = category;
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Funçãp HttpPost para criação de um produto.
        /// </summary>
        /// <param name="product">parametro para guardar os dados acerca do produto.</param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Tax,Stock,ProductCategoryId")] Product product)
        {
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                List<SelectListItem> category = new List<SelectListItem>();
                var productsCat = await _context.ProductsCategory.ToListAsync();
                foreach (var cat in productsCat)
                {
                    category.Add(new SelectListItem { Text = cat.Category.ToString(), Value = cat.ProductCategoryId.ToString() });
                }
                ViewBag.Categorias = category;
                return View(product);
            }
           
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        // GET: Products/Edit/5
        /// <summary>
        /// Função HttpGet retorna a página da ediçao de produto.
        /// </summary>
        /// <returns>retorna a página da edição de produto.</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Função HttpPost para retornar a página da lista de produtos quando o valor ja tiver sido editado.
        /// </summary>
        /// <param name="id">parametro que guarda o id do produto.</param>
        /// <param name="product">paramentro que guarda os valores do produto.</param>
        /// <returns>retorna a página da lista de produtos quando o valor ja tiver sido editado</returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Price,Tax,Stock,Category")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        /// <summary>
        /// Função HttpGet retorna a página de confirmação de delete.
        /// </summary>
        /// <param name="id">parametro que guarda o id do produto a ser eliminado.</param>
        /// <returns>retorna a página de confirmação de delete</returns>
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        /// <summary>
        /// Função HttpPost que retorna a página da lista de produtos.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>retorna a página da lista de produtos</returns>
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
