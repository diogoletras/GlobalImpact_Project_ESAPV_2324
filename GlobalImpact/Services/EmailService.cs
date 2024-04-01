using Microsoft.AspNetCore.Mvc;

namespace GlobalImpact.Services
{
    public class EmailService : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
