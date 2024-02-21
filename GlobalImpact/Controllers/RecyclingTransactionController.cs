using GlobalImpact.Data;
using GlobalImpact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GlobalImpact.Controllers
{
	public class RecyclingTransactionController(ApplicationDbContext db, SignInManager<AppUser> signInManager)
		: Controller
	{
		private readonly ApplicationDbContext _db = db;
		private readonly SignInManager<AppUser> _signInManager = signInManager;

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
        [Authorize(Roles = "client")]
        public IActionResult Reciclar(Guid? binid)
        {
            return View(binid);
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public IActionResult Reciclar(int?[] recycleQuantity, double?[]recycleCapacity, Guid? binId, string? userName)
        {
            if (!recycleQuantity.IsNullOrEmpty())
            {
                return View();
            }
            return View();
        }
    }
}
