using GlobalImpact.Data;
using GlobalImpact.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GlobalImpact.Controllers
{
	public class RecyclingTransactionController : Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly SignInManager<AppUser> _signInManager;

		public RecyclingTransactionController(ApplicationDbContext db, SignInManager<AppUser> signInManager)
		{
			_db = db;
			_signInManager = signInManager;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
