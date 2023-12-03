using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebDevProje.Models;

namespace WebDevProje.Controllers
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
			AnabilimDali anabilimDalı = new AnabilimDali();
			anabilimDalı.Ad = "Kardiyoloji";
			HastaneContext dbTest = new HastaneContext();

			if (dbTest.IsConnectionOpen())
			{
				ViewData["DatabaseConnection"] = "Veritabanı bağlantısı başarılı.";
            }
            else
			{
				ViewData["DatabaseConnection"] = "Veritabanı bağlantısı başarısız.";
            }
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
	}
}
