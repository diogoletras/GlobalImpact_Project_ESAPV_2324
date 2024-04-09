using GlobalImpact.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GlobalImpact.Controllers
{
    /// <summary>
    /// Classe de controlo da HomePage.
    /// </summary>
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="logger">parametro que guarda os logs feitos.</param>
        /// <param name="roleManager">Fornece APIs para gestao de roles de utilizadores</param>
        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Função de criação de Role, caso não exista.
        /// </summary>
        /// <returns>Retorno da página principal.</returns>
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("GoogleMaps", "RecyclingBins");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Função usada em caso de erro.
        /// </summary>
        /// <returns> Retorna uma página com o erro ocorrido.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
