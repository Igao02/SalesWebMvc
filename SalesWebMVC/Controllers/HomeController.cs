using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using System.Diagnostics;
using SalesWebMVC.Models.ViewsModels;

namespace SalesWebMVC.Controllers
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
            ViewData["Message"] = "Projeto Sales Web MVC App from C#";
            return View();
            //Colocando uma mensagem na página Index.
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


       
    }
}