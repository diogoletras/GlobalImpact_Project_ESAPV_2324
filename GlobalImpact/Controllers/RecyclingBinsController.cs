﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GlobalImpact.Data;
using GlobalImpact.Enumerates;
using GlobalImpact.Models;
using GlobalImpact.ViewModels.NewFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GlobalImpact.Controllers
{
    public class RecyclingBinsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RecyclingBinsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

		[HttpGet]
		public async Task<IActionResult> EcoLog()
        {
			return View();
        }

        [HttpGet]
        public async Task<IActionResult> EcoLogin(EcoLogViewModel model)
        {
            if (model.IdInput != null && Guid.TryParse(model.IdInput, out Guid id))
            {
                // Recupera o ecoponto com o ID correspondente
                var ecoponto = await _context.RecyclingBins.FirstOrDefaultAsync(e => e.Id == id);

                // Verifica se o ecoponto foi encontrado
                if (ecoponto != null)
                {
                    if (ecoponto.Status || ecoponto.Capacity <= ecoponto.CurrentCapacity)
                    {
                        ecoponto.Status = false;
                        _context.RecyclingBins.Update(ecoponto);
                        _context.SaveChanges();
                        ModelState.AddModelError("IdInput", "Recycling bin is already in use");
                        return RedirectToAction("EcoLogin", "RecyclingBins", new {model = model});
                        
                    }
                    else
                    {
                        ecoponto.Status = true;
                        _context.RecyclingBins.Update(ecoponto);
                        _context.SaveChanges();
                        return View(ecoponto);
                    }
                        
                }
                else
                {
                    // Se o ecoponto não foi encontrado, você pode exibir uma mensagem de erro ou redirecionar para uma página de erro
                    return NotFound(); // Retorna uma página 404
                }
            }
            else
            {
                // Se o ID não pôde ser convertido para Guid ou se nenhum ID foi enviado, você pode retornar uma view vazia ou redirecionar para outra página
                return RedirectToAction("EcoLog"); // Retorna a view EcoLog sem nenhum modelo
            }
        }

        [HttpPost]
        public async Task<IActionResult> EcoLogin(Guid? binId, string? uniqueCode)
        {
            if (uniqueCode != null && binId != null )
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UniqueCode == uniqueCode);
                var ecoponto = await _context.RecyclingBins.FirstOrDefaultAsync(e => e.Id == binId);
                var recyclingList = _context.RecyclingBins.ToList();
                var recyclingBinTypeList = _context.RecyclingBinType.ToList();

                if (user != null)
                {
                    foreach (var recyclingBin in recyclingList)
                    {
                        if (recyclingBin.Id.Equals(binId))
                        {
                            var ecoType = recyclingBinTypeList.FirstOrDefault(r => r.RecyclingBinTypeId == recyclingBin.RecyclingBinType.RecyclingBinTypeId);
                            return RedirectToAction("Reciclar", "RecyclingTransaction", new { binid = binId.ToString(), type = ecoType.Type, userName = user.UserName });
                        }
                    }
                    
                }
                else
                {
                    return View(ecoponto);
                }
            }
            return RedirectToAction("EcoLogin" , new { idInput = binId.ToString()});
        }


		// GET: RecyclingBins
	    [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.RecyclingBins.ToListAsync());
        }

        // GET: RecyclingBins/Details/5
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recyclingBin = await _context.RecyclingBins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recyclingBin == null)
            {
                return NotFound();
            }

            return View(recyclingBin);
        }

        // GET: RecyclingBins/Create
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var res = new RecyclingBin
            {
                RBTList = new List<RecyclingBinType>
                {
                    new RecyclingBinType { RecyclingBinTypeId = Guid.NewGuid(), Type = BinType.paper.ToString() },
                    new RecyclingBinType { RecyclingBinTypeId = Guid.NewGuid(), Type = BinType.plastic.ToString() },
                    new RecyclingBinType { RecyclingBinTypeId = Guid.NewGuid(), Type = BinType.glass.ToString() },
                }
            };
            return View(res);
        }

        // POST: RecyclingBins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RecyclingBinType,Latitude,Longitude,Description,Capacity,CurrentCapacity,Status")] RecyclingBin recyclingBin)
        {
            if (ModelState.IsValid)
            {
                recyclingBin.Id = Guid.NewGuid();
                _context.Add(recyclingBin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var res = new RecyclingBin
            {
                RBTList = new List<RecyclingBinType>
                {
                    new RecyclingBinType { RecyclingBinTypeId = Guid.NewGuid(), Type = BinType.paper.ToString() },
                    new RecyclingBinType { RecyclingBinTypeId = Guid.NewGuid(), Type = BinType.plastic.ToString() },
                    new RecyclingBinType { RecyclingBinTypeId = Guid.NewGuid(), Type = BinType.glass.ToString() }
                }
            };

            recyclingBin.RBTList = res.RBTList;
            return View(recyclingBin);
        }

        // GET: RecyclingBins/Edit/5
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recyclingBin = await _context.RecyclingBins.FindAsync(id);
            if (recyclingBin == null)
            {
                return NotFound();
            }
            return View(recyclingBin);
        }

        // POST: RecyclingBins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Type,Latitude,Longitude,Description,Capacity,CurrentCapacity,Status")] RecyclingBin recyclingBin)
        {
            if (id != recyclingBin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recyclingBin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!recyclingBinExists(recyclingBin.Id))
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
            return View(recyclingBin);
        }

        // GET: RecyclingBins/Delete/5
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recyclingBin = await _context.RecyclingBins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recyclingBin == null)
            {
                return NotFound();
            }

            return View(recyclingBin);
        }

        // POST: RecyclingBins/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var recyclingBin = await _context.RecyclingBins.FindAsync(id);
            if (recyclingBin != null)
            {
                _context.RecyclingBins.Remove(recyclingBin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool recyclingBinExists(Guid id)
        {
            return _context.RecyclingBins.Any(e => e.Id == id);
        }
    }
}
