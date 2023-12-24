using Microsoft.AspNetCore.Mvc;

namespace MegaOne.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
