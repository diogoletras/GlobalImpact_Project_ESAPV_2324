using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GlobalImpact.Controllers
{
    [Authorize]
    public class AccessController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Client")]
        public IActionResult ClientAccess()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminAccess()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Client")]
        public IActionResult AdminClientAccess()
        {
            return View();
        }

        [Authorize(Policy = "OnlyAdminChecker")]
        public IActionResult OnlyAdminChecker()
        {
            return View();
        }
    }
}
