﻿using GlobalImpact.Data;
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
        private List<RecItems> recItems = StationeryItems.Items;
        private List<RecItems> items = StationeryDb.Items;

        public RecyclingTransactionController(ApplicationDbContext db, SignInManager<AppUser> userManager)
        {
            _db = db;
            _signInManager = userManager;
           
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

        /// <summary>
        /// Função HTTPGet retorna a pagina do processo de reciclagem
        /// </summary>
        /// <param name="binid">id do ecoponto a ser utilizado</param>
        /// <param name="type">tipo de residuos que o ecoponto recebe</param>
        /// <param name="userName">nome do utilizador que esta a usar o ecoponto</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Reciclar(Guid? binid, string? type , string? userName)
        {

            var ecoponto = await _db.RecyclingBins.FirstOrDefaultAsync(e => e.Id == binid);
            items.Clear();
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

        /// <summary>
        /// Função HTTPPost que recebe um item para reciclagem e verifica se é valido para o ecoponto que esta a ser utilizado
        /// </summary>
        /// <param name="idEco">id do ecoponto a ser utilizado</param>
        /// <param name="type">tipo de residuos que o ecoponto recebe</param>
        /// <param name="nome">nome do utilizador que esta a usar o ecoponto</param>
        /// <param name="itemName">nome do item a verificar</param>
        /// <param name="reciclados"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Rec(string idEco, string type, string nome,string itemName, List<RecItems> reciclados)
        {

            RecItems item = recItems.Find(item => item.Nome == itemName);
            if(item.Tipo.ToLower().Equals(type))
            {
                items.Add(item);
            }
            else if (item.Tipo.ToLower().Equals("other"))
            {
                ModelState.AddModelError("Type", "O Residuo introduzido não é reciclavel");
            }
            else
            {
                ModelState.AddModelError("Type", "O Residuo introduzido não é "+type);
            }
            

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
