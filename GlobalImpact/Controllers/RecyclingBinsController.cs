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
    public class RecyclingBinsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecyclingBinsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RecyclingBins
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.RecyclingBins.ToListAsync());
        }

        // GET: RecyclingBins/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecyclingBins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Latitude,Longitude,Description,Capacity,CurrentCapacity,Status")] RecyclingBin recyclingBin)
        {
            if (ModelState.IsValid)
            {
                recyclingBin.Id = Guid.NewGuid();
                _context.Add(recyclingBin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recyclingBin);
        }

        // GET: RecyclingBins/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
