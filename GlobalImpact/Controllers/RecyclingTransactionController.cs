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
                    EcoLogViewModel model = new EcoLogViewModel();
                    model.IdInput = binId.ToString();
                    if (user != null)
                    {
                        if (recyclingBin.CurrentCapacity + (double)recycleCapacity.Sum() < recyclingBin.Capacity)
                        {
                            recyclingBin.CurrentCapacity += (double)recycleCapacity.Sum();
                            recyclingBin.Status = false;
                            if (user.NIF != null && Regex.IsMatch(user.NIF.ToString(), @"^\d{9}$"))
                            {
                                var recyclingTransaction = new RecyclingTransaction
                                {
                                    Id = Guid.NewGuid(),
                                    RecyclingBin = recyclingBin,
                                    User = user,
                                    Weight = (double)recycleCapacity.Sum(),
                                    Date = DateTime.Now,
                                    isNIFRequired = true
                                };
                                _db.RecyclingTransactions.Add(recyclingTransaction);
                            }
                            else
                            {
                                var recyclingTransaction = new RecyclingTransaction
                                {
                                    Id = Guid.NewGuid(),
                                    RecyclingBin = recyclingBin,
                                    User = user,
                                    Weight = (double)recycleCapacity.Sum(),
                                    Date = DateTime.Now,
                                    isNIFRequired = false
                                };
                                _db.RecyclingTransactions.Add(recyclingTransaction);
                            }

                            recyclingBin.Status = false;
                            _db.RecyclingBins.Update(recyclingBin);
                            _db.Users.Update(user);
                            _db.SaveChanges();
                            return RedirectToAction("EcoLogin", "RecyclingBins", model);
                        }

                        if (recyclingBin.CurrentCapacity + (double)recycleCapacity.Sum() == recyclingBin.Capacity)
                        {
                            recyclingBin.CurrentCapacity += (double)recycleCapacity.Sum();
                            recyclingBin.Status = false;
                            if (user.NIF != null && Regex.IsMatch(user.NIF.ToString(), @"^\d{9}$"))
                            {
                                var recyclingTransaction = new RecyclingTransaction
                                {
                                    Id = Guid.NewGuid(),
                                    RecyclingBin = recyclingBin,
                                    User = user,
                                    Weight = (double)recycleCapacity.Sum(),
                                    Date = DateTime.Now,
                                    isNIFRequired = true
                                };
                                _db.RecyclingTransactions.Add(recyclingTransaction);
                            }
                            else
                            {
                                var recyclingTransaction = new RecyclingTransaction
                                {
                                    Id = Guid.NewGuid(),
                                    RecyclingBin = recyclingBin,
                                    User = user,
                                    Weight = (double)recycleCapacity.Sum(),
                                    Date = DateTime.Now,
                                    isNIFRequired = false
                                };
                                _db.RecyclingTransactions.Add(recyclingTransaction);
                            }
                            _db.RecyclingBins.Update(recyclingBin);
                            _db.Users.Update(user);
                            _db.SaveChanges();
                            return RedirectToAction("EcoLog", "RecyclingBins", model);
                        }
                        else
                        {
                            recyclingBin.CurrentCapacity = recyclingBin.Capacity;
                            recyclingBin.Status = false;
                            if (user.NIF != null && Regex.IsMatch(user.NIF.ToString(), @"^\d{9}$"))
                            {
                                var recyclingTransaction = new RecyclingTransaction
                                {
                                    Id = Guid.NewGuid(),
                                    RecyclingBin = recyclingBin,
                                    User = user,
                                    Weight = (double)recycleCapacity.Sum(),
                                    Date = DateTime.Now,
                                    isNIFRequired = true
                                };
                                _db.RecyclingTransactions.Add(recyclingTransaction);
                            }
                            else
                            {
                                var recyclingTransaction = new RecyclingTransaction
                                {
                                    Id = Guid.NewGuid(),
                                    RecyclingBin = recyclingBin,
                                    User = user,
                                    Weight = (double)recycleCapacity.Sum(),
                                    Date = DateTime.Now,
                                    isNIFRequired = false
                                };
                                _db.RecyclingTransactions.Add(recyclingTransaction);
                            }
                            _db.RecyclingBins.Update(recyclingBin);
                            _db.Users.Update(user);
                            _db.SaveChanges();
                            return RedirectToAction("EcoLog", "RecyclingBins", model);
                        }
                    }
                }
            }
            List<object> resList = new List<object>();
            resList.Add(binId);
            resList.Add(type);
            return View(binId);
        }
    }
}
