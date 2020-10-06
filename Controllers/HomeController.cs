using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebFrontend.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet, ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index(long? id)
        {
            await Task.CompletedTask;

            ViewData["Title"] = "Web Frontend - .Net Core";

            return View();
        }
    }
}