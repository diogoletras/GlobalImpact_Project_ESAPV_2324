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
using System;
using GlobalImpact.ViewModels.RecyclingBin;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;

namespace GlobalImpact.Controllers
{
    /// <summary>
    /// Controller da gestão de Transaçoes de reciclagem
    /// </summary>
	public class RecyclingTransactionController: Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly SignInManager<AppUser> _signInManager;
        private List<RecItems> recItems = StationeryItems.Items;
        private List<RecItems> items = StationeryDb.Items;

        /// <summary>
        /// Construtor do Controller RecyclingTransactionController
        /// </summary>
        /// <param name="db">Base de Dados</param>
        /// <param name="userManager">Fornece APIs para gestao de utilizadores</param>
        public RecyclingTransactionController(ApplicationDbContext db, SignInManager<AppUser> userManager)
        {
            _db = db;
            _signInManager = userManager;
           
        }

        /// <summary>
        /// Funçao HTTPGet que retorna uma view com a lista dos ecopontos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Authorize(Roles = "client")]
		public async Task<IActionResult> Index()
		{
			var result = await _db.RecyclingBins.ToListAsync();

            foreach (var rBin in result)
            {
                RecyclingBinType rType = await _db.RecyclingBinType.FirstOrDefaultAsync(t => t.RecyclingBinTypeId == new Guid(rBin.RecyclingBinTypeId));
                rBin.RecyclingBinType = rType;
                rBin.Type = rType.Type;
            }

			var recyclingBins = await _db.RecyclingBins.ToListAsync();
			var recyclingBinTypes = await _db.RecyclingBinType.ToListAsync();

			return View(result);
		}

        /// <summary>
        ///Funçao HTTPpost que recebe o id do utilizador e retorna uma view com as transaçoes de reciclagem do respetivo utilizador
        /// </summary>
        /// <param name="userId"> id do utilizador</param>
        /// <returns></returns>
        public async Task<IActionResult> TransacionList(string userId)
        {
            var trans = await _db.RecyclingTransactions.Where(r => r.User.Id == userId).ToArrayAsync();
            var binId = await _db.RecyclingTransactions.Where(r => r.User.Id == userId).Select(t => t.RecyclingBinId).ToArrayAsync();
            for (int i=0; i<binId.Length; i++)
            {
                AppUser user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
                trans[i].User = user;
                trans[i].RecyclingBin = await _db.RecyclingBins.FirstOrDefaultAsync(b => b.Id == binId[i]);
                trans[i].RecyclingBin.RecyclingBinType = await _db.RecyclingBinType.FirstOrDefaultAsync(t => t.RecyclingBinTypeId == new Guid(trans[i].RecyclingBin.RecyclingBinTypeId));
            }
            return View(trans);
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
                ModelState.AddModelError("Type", "O Residuo introduzido não corresponde com o tipo do Ecoponto ");
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


        /// <summary>
        /// Função HTTPPost que finaliza o processo de reciclagem e guarda na base de dados
        /// </summary>
        /// <param name="idEco">id do ecoponto utilizado</param>
        /// <param name="nome">user name do utilizador</param>
        /// <param name="peso">peso do processo de reciclagem</param>
        /// <param name="pontos">pontos do processo de reciclagem</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> FinishRecycling(string idEco, string nome, double peso, int pontos)
        {
            var ecoId = new Guid(idEco);
            var ecoponto = await _db.RecyclingBins.FirstOrDefaultAsync(e => e.Id == ecoId);
            var user =  _db.Users.FirstOrDefault(u => u.UserName == nome);
            if(user != null && ecoponto != null)
            {
                var ecoTransacion = new RecyclingTransaction
                {
                    Id = new Guid(),
                    User = user,
                    RecyclingBinId = ecoponto.Id,
                    Weight = peso,
                    Points = pontos,
                    Date = DateTime.Now
                };

                _db.RecyclingTransactions.Add(ecoTransacion);
                user.Points += pontos;
                _db.Users.Update(user);

                if((ecoponto.CurrentCapacity + peso) >= ecoponto.Capacity)
                {
	                ecoponto.Status = true;
					ecoponto.CurrentCapacity = ecoponto.Capacity;
                }
                else
                {
	                ecoponto.Status = false;
					ecoponto.CurrentCapacity += peso;
                }
                
				_db.RecyclingBins.Update(ecoponto);

                await _db.SaveChangesAsync();

                var model = new EcoLogViewModel
                {
                    IdInput = idEco
                };

                return RedirectToAction("EcoLogin", "RecyclingBins", model);
            }
            return View();
        }

        /// <summary>
        /// Funçao HTTPPost de cancelamento do pocesso de reciclagem
        /// </summary>
        /// <param name="idEco">id do ecoponto utilizado</param>
        /// <param name="nome">user name do utilizador</param>
        /// <param name="type">tipo de ecoponto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CancelTrans(string idEco, string nome, string type)
        {
			var ecoId = new Guid(idEco);
			var ecoponto = await _db.RecyclingBins.FirstOrDefaultAsync(e => e.Id == ecoId);

			var model = new ReciclarViewModel
			{
				EcoPonto = ecoponto,
				Type = type,
				UserName = nome,
				RecItems = recItems,
				Reciclados = items
			};

			return View(model);
        }

        /// <summary>
        /// Função HTTPPost que confirma o cancelarmento da transação de reciclagem
        /// </summary>
        /// <param name="idEco">id do ecoponto que esta a ser utilizado</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CancelConfirm(string idEco)
        {
			var model = new EcoLogViewModel
			{
				IdInput = idEco,
			};

            var ecoponto = await _db.RecyclingBins.FirstOrDefaultAsync(e => e.Id == new Guid(idEco));

            ecoponto.Status = false;
            _db.RecyclingBins.Update(ecoponto);
            await _db.SaveChangesAsync();

			return RedirectToAction("EcoLogin", "RecyclingBins", model);
		}

        [HttpPost]
        public async Task<IActionResult> UpdateBin(string id)
        {
            var bin = await _db.RecyclingBins.FirstOrDefaultAsync(b => b.Id == new Guid(id));
            bin.Status = false;
            bin.CurrentCapacity = 0;
            _db.RecyclingBins.Update(bin);
            await _db.SaveChangesAsync();
            return RedirectToAction("FullBins", "RecyclingBins");
        }

	}
}
