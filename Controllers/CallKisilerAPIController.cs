using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebDevProje.Models; // Kisiler modelinizi burada dahil ettiğinizden emin olun // kişiler model ekleme
using Microsoft.Extensions.Localization;

namespace WebDevProje.Controllers
{
    public class CallKisilerApi : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Kisi> kisiler = new List<Kisi>();
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7148/api/KisilerAPI"); // API'nizin URL'sini buraya girin
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                kisiler = JsonConvert.DeserializeObject<List<Kisi>>(jsonResponse);
            }

            return View(kisiler);
        }
        
    }

}