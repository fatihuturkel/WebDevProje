using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebDevProje.Models;

namespace WebDevProje.Controllers
{
    public class RandevuController : Controller
    {
        private readonly HastaneContext _context;

        public RandevuController(HastaneContext context)
        {
            _context = context;
        }

        // GET: Randevu
        public async Task<IActionResult> Index()
        {
            var hastaneContext = _context.Randevular.Include(r => r.Doktor).Include(r => r.Hasta).Include(r => r.Poliklinik);
            return View(await hastaneContext.ToListAsync());
        }

        // GET: Randevu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Randevular == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Doktor)
                .Include(r => r.Hasta)
                .Include(r => r.Poliklinik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // GET: Randevu/Create
        public IActionResult Create()
        {
            ViewData["DoktorId"] = new SelectList(_context.Doktorlar, "Id", "Id");
            ViewData["HastaId"] = new SelectList(_context.Hastalar, "Id", "Id");
            ViewData["PoliklinikId"] = new SelectList(_context.Poliklinikler, "Id", "Ad");
            return View();
        }

        // POST: Randevu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HastaId,DoktorId,PoliklinikId,Tarih")] Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoktorId"] = new SelectList(_context.Doktorlar, "Id", "Id", randevu.DoktorId);
            ViewData["HastaId"] = new SelectList(_context.Hastalar, "Id", "Id", randevu.HastaId);
            ViewData["PoliklinikId"] = new SelectList(_context.Poliklinikler, "Id", "Ad", randevu.PoliklinikId);
            return View(randevu);
        }

        // GET: Randevu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Randevular == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewData["DoktorId"] = new SelectList(_context.Doktorlar, "Id", "Id", randevu.DoktorId);
            ViewData["HastaId"] = new SelectList(_context.Hastalar, "Id", "Id", randevu.HastaId);
            ViewData["PoliklinikId"] = new SelectList(_context.Poliklinikler, "Id", "Ad", randevu.PoliklinikId);
            return View(randevu);
        }

        // POST: Randevu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HastaId,DoktorId,PoliklinikId,Tarih")] Randevu randevu)
        {
            if (id != randevu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoktorId"] = new SelectList(_context.Doktorlar, "Id", "Id", randevu.DoktorId);
            ViewData["HastaId"] = new SelectList(_context.Hastalar, "Id", "Id", randevu.HastaId);
            ViewData["PoliklinikId"] = new SelectList(_context.Poliklinikler, "Id", "Ad", randevu.PoliklinikId);
            return View(randevu);
        }

        // GET: Randevu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Randevular == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Doktor)
                .Include(r => r.Hasta)
                .Include(r => r.Poliklinik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: Randevu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Randevular == null)
            {
                return Problem("Entity set 'HastaneContext.Randevular'  is null.");
            }
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevular.Remove(randevu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(int id)
        {
            return _context.Randevular.Any(e => e.Id == id);
        }

        public async Task<IActionResult> randevuSaati(int? id)
        {
            if (id == null || _context.Randevular == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevular
                .Include(r => r.Doktor)
                .Include(r => r.Hasta)
                .Include(r => r.Poliklinik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            // take randevu hour and minute from randevu.Tarih
            var randevuHour = randevu.Tarih.Hour;
            var randevuMinute = randevu.Tarih.Minute;

            var randevuTime = randevu.Tarih.TimeOfDay;
            var randevuDate = randevu.Tarih.Date;

            // get date but without time
            var randevuDateWithoutTime = randevu.Tarih.Day + "/" + randevu.Tarih.Month + "/" + randevu.Tarih.Year;

            return View(randevu);
        }

        // GET: Randevu/GetRandevu
        public IActionResult GetRandevu()
        {
            // get session data from cookie for current user and if it is null, redirect to login page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }
            // get hasta id from session data
            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);

            if (kisi.Hasta == false)
            {
                // show not authorized page in kisi controller
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            var hastaId = kisi.Id;

            ViewData["AnabilimDaliId"] = new SelectList(_context.AnabilimDallari, "Id", "Ad");

            return View();
        }


        // POST: Randevu/GetRandevu just for anabilim dali
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetRandevuAnaBilim()
        {
            // get session data from cookie for current user and if it is null, redirect to login page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }
            // get hasta id from session data
            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);

            if (kisi.Hasta == false)
            {
                // show not authorized page in kisi controller
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            // take data from form
            var anabilimDaliId = Int32.Parse(Request.Form["AnabilimDaliId"]);
            var tarih = DateTime.Parse(Request.Form["Date"]);

            // use anabilim dali id to get poliklinikler
            var poliklinikler = _context.Poliklinikler.Where(p => p.AnabilimDaliId == anabilimDaliId).ToList();

            // use poliklinikler to get doktorlar
            var doktorlar = new List<Doktor>();
            foreach (var poliklinik in poliklinikler)
            {
                var doktorlarInPoliklinik = _context.Doktorlar.Where(d => d.PoliklinikId == poliklinik.Id).ToList();
                doktorlar.AddRange(doktorlarInPoliklinik);
            }

            // add doktor names to doktorlar list for view
            foreach (var doktor in doktorlar)
            {
                doktor.Kisi = _context.Kisiler.FirstOrDefault(k => k.Id == doktor.Id);
            }

            // use doktorlar to get doktor calisma listesi
            var doktorCalismaTakvimleri = new List<DoktorCalismaTakvimi>();
            foreach (var doktor in doktorlar)
            {
                // if doktorcalismatakvimiindoktor's tarih is equal to tarih, add it to doktorCalismaTakvimleri
                var doktorCalismaTakvimleriInDoktor = _context.DoktorCalismaTakvimleri.Where(d => d.DoktorId == doktor.Id && d.Tarih == tarih).ToList();
                doktorCalismaTakvimleri.AddRange(doktorCalismaTakvimleriInDoktor);
            }

            /*
            // remove doktors from doktorlar list if they don't have doktor calisma takvimi in doktorCalismaTakvimleri
            var doktorlarToRemove = new List<Doktor>();
            foreach (var doktor in doktorlar)
            {
                var doktorCalismaTakvimleriInDoktor = _context.DoktorCalismaTakvimleri.Where(d => d.DoktorId == doktor.Id && d.Tarih == tarih).ToList();
                if (doktorCalismaTakvimleriInDoktor.Count == 0)
                {
                    doktorlarToRemove.Add(doktor);
                }
            }

            foreach (var doktor in doktorlarToRemove)
            {
                doktorlar.Remove(doktor);
            }
            */

            /*
            // remove poliklinikler from poliklinikler list if they don't have doktor in doktorlar
            var polikliniklerToRemove = new List<Poliklinik>();
            foreach (var poliklinik in poliklinikler)
            {
                var doktorlarInPoliklinik = _context.Doktorlar.Where(d => d.PoliklinikId == poliklinik.Id).ToList();
                if (doktorlarInPoliklinik.Count == 0)
                {
                    polikliniklerToRemove.Add(poliklinik);
                }
            }

            foreach (var poliklinik in polikliniklerToRemove)
            {
                poliklinikler.Remove(poliklinik);
            }
            */

            // send all poliklinikler, doktorlar and doktor calisma saatleri to view
            ViewData["poliklinikler"] = poliklinikler;
            ViewData["doktorlar"] = doktorlar;
            ViewData["doktorCalismaTakvimleri"] = doktorCalismaTakvimleri;
            TempData["tarih"] = tarih;

            return View();
        }


        public IActionResult Profile()
        {
            // get kisi object from session
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson is null)
            {
                return RedirectToAction("Login", "Kisi");
            }
            else
            {
                var kisi = JsonConvert.DeserializeObject<Kisi>(kisiJson);
                return View(kisi);
            }
        }

        // post/GetRandevu 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetRandevu(string selectedPoliklinik, string selectedDoktor, string selectedSaat, string selectedTarih)
        {

            // get session data from cookie for current user and if it is null, redirect to login page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                //return RedirectToAction("Login", "Kisi");
                return RedirectToAction("Login", "Kisi");
            }
            // get hasta id from session data
            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);

            if (kisi.Hasta == false)
            {
                // show not authorized page in kisi controller
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            int hastaId = kisi.Id;

            DateTime DTselectedTarih = DateTime.Parse(selectedTarih);

            // find doktor calisma takvimi in doktor calisma takvimi table
            var doktorCalismaTakvimi = _context.DoktorCalismaTakvimleri.FirstOrDefault(d => d.DoktorId == Int32.Parse(selectedDoktor) && d.Tarih == DTselectedTarih);


            // convert selectedSaat to time format and reserve that time in doktor calisma takvimi (set it to 2)
            switch (selectedSaat)
            {
                case "dokuz_on":
                    selectedSaat = "09:00:00";
                    doktorCalismaTakvimi.dokuz_on = 2;
                    break;
                case "on_onbir":
                    selectedSaat = "10:00:00";
                    doktorCalismaTakvimi.on_onbir = 2;
                    break;
                case "onbir_oniki":
                    selectedSaat = "11:00:00";
                    doktorCalismaTakvimi.onbir_oniki = 2;
                    break;
                case "onuc_ondort":
                    selectedSaat = "13:00:00";
                    doktorCalismaTakvimi.onuc_ondort = 2;
                    break;
                case "ondort_onbes":
                    selectedSaat = "14:00:00";
                    doktorCalismaTakvimi.ondort_onbes = 2;
                    break;
                case "onbes_onalti":
                    selectedSaat = "15:00:00";
                    doktorCalismaTakvimi.onbes_onalti = 2;
                    break;
                case "onalti_onyedi":
                    selectedSaat = "16:00:00";
                    doktorCalismaTakvimi.onalti_onyedi = 2;
                    break;
                default:
                    break;
            }

            // take selectedTarih and selectedSaat and combine them to create a DateTime object
            string selectedTarihandSaat = DateTime.Parse(selectedTarih).ToString("dd/MM/yyyy") + " " + selectedSaat;

            DateTime DTselectedTarihandSaat = DateTime.Parse(selectedTarihandSaat);

            // create a new randevu object
            var randevu = new Randevu
            {
                HastaId = hastaId,
                DoktorId = Int32.Parse(selectedDoktor),
                PoliklinikId = Int32.Parse(selectedPoliklinik),
                Tarih = DTselectedTarihandSaat
            };

            _context.Add(randevu);
            _context.SaveChanges();

            //return RedirectToAction("Success", "Randevu", new { id = randevu.Id });
            return RedirectToAction("Success", "Randevu", new { id = randevu.Id });
        }


        // show success page after randevu is taken and show randevu details
        public IActionResult Success(int? id)
        {
            if (id == null || _context.Randevular == null)
            {
                return NotFound();
            }

            var randevu = _context.Randevular
                .Include(r => r.Doktor)
                .Include(r => r.Hasta)
                .Include(r => r.Poliklinik)
                .Include(r => r.Doktor.Kisi)
                .Include(r => r.Hasta.Kisi)
                .FirstOrDefault(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

    }
}
