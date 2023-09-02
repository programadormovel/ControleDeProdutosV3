using ControleDeProdutosAula.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace ControleDeProdutosAula.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public const string SessionKeyUser = "_Usuario";
        public const string SessionKeyEmail = "_Email";
        public const string SessionKeyNivel = "_Nivel";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var usuario = HttpContext.Session.GetString(SessionKeyUser);
            if (!usuario.IsNullOrEmpty())
            {
                _logger.LogInformation("Session Name: {usuario}", usuario);
                return View();
            }
            return View();
        }

		public IActionResult Sair()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index");
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