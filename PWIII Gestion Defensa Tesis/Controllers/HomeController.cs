using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWIII_Gestion_Defensa_Tesis.Models;
using System.Diagnostics;

namespace PWIII_Gestion_Defensa_Tesis.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AccesoRestringido()
        {
            return View();
        }
    }
}
