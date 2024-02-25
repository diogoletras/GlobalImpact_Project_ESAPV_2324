using GlobalImpact.Data;
using GlobalImpact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using GlobalImpact.ViewModels.NewFolder;

namespace GlobalImpact.Controllers
{
	public class RecyclingTransactionController: Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly SignInManager<AppUser> _signInManager;
        private List<RecItems> recItems;
        private List<RecItems> items = StationeryDb.Items;

        public RecyclingTransactionController(ApplicationDbContext db, SignInManager<AppUser> userManager)
        {
            _db = db;
            _signInManager = userManager;
            recItems = new List<RecItems> {
                new RecItems
                {
                    Tipo = "Glass",
                    Nome = "Garrafa",
                    Peso = 0.5,
                    Pontos = 3
                },
                new RecItems
                {
                    Tipo = "Paper",
                    Nome = "Caixa",
                    Peso = 0.3,
                    Pontos = 2
                },
                new RecItems
                {
                    Tipo = "Plastic",
                    Nome = "Lata",
                    Peso = 0.2,
                    Pontos = 1
                },
                new RecItems
                {
                    Tipo = "Organic",
                    Nome = "Chocolate",
                    Peso = 0.2,
                    Pontos = 0
                },
            };
        }

        [HttpGet]
		[Authorize(Roles = "client")]
		public async Task<IActionResult> Index()
		{
			var result = await _db.RecyclingBins.ToListAsync();

			var recyclingBins = await _db.RecyclingBins.ToListAsync();
			var recyclingBinTypes = await _db.RecyclingBinType.ToListAsync();

			return View(result);
		}

        [HttpGet]
        public async Task<IActionResult> Reciclar(Guid? binid, string? type , string? userName)
        {

            var ecoponto = await _db.RecyclingBins.FirstOrDefaultAsync(e => e.Id == binid);

            var model = new ReciclarViewModel
            {
                EcoPonto = ecoponto,
                Type = type,
                UserName = userName,
                RecItems = recItems,
                Reciclados = items
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Rec(string idEco, string type, string nome,string itemName, List<RecItems> reciclados)
        {

            RecItems item = recItems.Find(item => item.Nome == itemName);
            items.Add(item);

            var model = new ReciclarViewModel
            {
                Type = type,
                UserName = nome,
                RecItems = recItems,
                Reciclados = items
            };


            if (Guid.TryParse(idEco, out Guid idGuid))
            {
                // Agora idGuid contém o valor GUID convertido
                // Você pode usá-lo da maneira que precisar
                var ecoponto = await _db.RecyclingBins.FirstOrDefaultAsync(e => e.Id == idGuid);
                model.EcoPonto = ecoponto;
            }
           
            // Atualiza o modelo para incluir a lista de itens reciclados atualizada

            return View("Reciclar", model);
        }
    }
}
