using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WebDevProje.Models;
using WebDevProje.Services;

namespace WebDevProje.Controllers
{
	public class HomeController : Controller

	{   
		
		private readonly ILogger<HomeController> _logger;
		//dil değiştirme
		private LanguageService _localization;

		// Dil değiştirme için LanguageService nesnesini constructor'da alın
		public HomeController(ILogger<HomeController> logger, LanguageService localization)
		{
			_logger = logger;
			_localization = localization; // _localization nesnesini initialize edin
		}

		
		public IActionResult Index()
		{
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            //dil değiştirme
            ViewBag.Welcome = _localization.Getkey("Welcome").Value;
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
			ViewBag.Culture = currentCulture;
			
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

		//dil değiştirme
		public IActionResult ChangeLanguage(string culture)
		{
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
			);

			// Özel Redirect metodunu çağırmak yerine, Controller sınıfının kendi Redirect metodunu kullanın.
			return Redirect(Request.Headers["Referer"].ToString());
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