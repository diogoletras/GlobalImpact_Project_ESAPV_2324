using GlobalImpact.Data;
using GlobalImpact.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

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
        //[Authorize(Roles = "client")]
        public IActionResult Reciclar(Guid? binid, string? type , string? userName)
        {
            List<object> resList= new List<object>();
            resList.Add(binid);
            resList.Add(type);
            resList.Add(userName);

            return View(resList);
        }

        [HttpPost]
        //[Authorize(Roles = "client")]
        public async Task<IActionResult> Reciclar(int?[] recycleQuantity, double?[]recycleCapacity, Guid? binId, string? type, string? userName)
        {
            if (ModelState.IsValid)
            {
                recycleCapacity = recycleCapacity.Where(x => x != null).ToArray();
                if (!recycleQuantity.IsNullOrEmpty() && !recycleCapacity.IsNullOrEmpty())
                {
                    AppUser user = await _db.AppUser.FirstOrDefaultAsync(u => u.UserName == userName);
                    user.Points += (int)Math.Round((double)recycleCapacity.Sum() + 1);

                    var recyclingBin = await _db.RecyclingBins.FirstOrDefaultAsync(r => r.Id == binId);
                    var recyclingTransaction = new RecyclingTransaction
                    {
                        Id = Guid.NewGuid(),
                        RecyclingBin = recyclingBin,
                        User = user,
                        Date = DateTime.Now,
                    };
                    _db.RecyclingTransactions.Add(recyclingTransaction);
                    _db.Users.Update(user);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "RecyclingTransaction");
                }
            }
            List<object> resList = new List<object>();
            resList.Add(binId);
            resList.Add(type);
            return View(binId);
        }
    }
}
