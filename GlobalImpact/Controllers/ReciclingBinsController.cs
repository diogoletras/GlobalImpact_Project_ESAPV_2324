using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GlobalImpact.Data;
using GlobalImpact.Models;

namespace GlobalImpact.Controllers
{
    public class ReciclingBinsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReciclingBinsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReciclingBins
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReciclingBins.ToListAsync());
        }

        // GET: ReciclingBins/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reciclingBin = await _context.ReciclingBins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reciclingBin == null)
            {
                return NotFound();
            }

            return View(reciclingBin);
        }

        // GET: ReciclingBins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReciclingBins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Latitude,Longitude,Description,Capacity,CurrentCapacity,Status")] ReciclingBin reciclingBin)
        {
            if (ModelState.IsValid)
            {
                reciclingBin.Id = Guid.NewGuid();
                _context.Add(reciclingBin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reciclingBin);
        }

        // GET: ReciclingBins/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reciclingBin = await _context.ReciclingBins.FindAsync(id);
            if (reciclingBin == null)
            {
                return NotFound();
            }
            return View(reciclingBin);
        }

        // POST: ReciclingBins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Type,Latitude,Longitude,Description,Capacity,CurrentCapacity,Status")] ReciclingBin reciclingBin)
        {
            if (id != reciclingBin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reciclingBin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReciclingBinExists(reciclingBin.Id))
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
            return View(reciclingBin);
        }

        // GET: ReciclingBins/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reciclingBin = await _context.ReciclingBins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reciclingBin == null)
            {
                return NotFound();
            }

            return View(reciclingBin);
        }

        // POST: ReciclingBins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reciclingBin = await _context.ReciclingBins.FindAsync(id);
            if (reciclingBin != null)
            {
                _context.ReciclingBins.Remove(reciclingBin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReciclingBinExists(Guid id)
        {
            return _context.ReciclingBins.Any(e => e.Id == id);
        }
    }
}
