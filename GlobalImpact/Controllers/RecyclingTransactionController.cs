using GlobalImpact.Data;
using GlobalImpact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
			return View(result);
		}
	}
}
