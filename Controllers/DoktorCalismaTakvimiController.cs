using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebDevProje.Models;

namespace WebDevProje.Controllers
{
    public class DoktorCalismaTakvimiController : Controller
    {
        private readonly HastaneContext _context;

        public DoktorCalismaTakvimiController(HastaneContext context)
        {
            _context = context;
        }

        // GET: DoktorCalismaTakvimi
        public async Task<IActionResult> Index()
        {

            // get session data from cookie and if it is null, redirect to login page or if it is not doktor show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);
            ViewBag.kisiNavbar = kisi; // navbar için

            if (kisi.adminMi == false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }


            var hastaneContext = _context.DoktorCalismaTakvimleri.Include(d => d.Doktor);
            return View(await hastaneContext.ToListAsync());
        }

        // GET: DoktorCalismaTakvimi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not doktor show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisi.Doktor == false && kisi.adminMi == false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            if (id == null || _context.DoktorCalismaTakvimleri == null)
            {
                return NotFound();
            }

            var doktorCalismaTakvimi = await _context.DoktorCalismaTakvimleri
                .Include(d => d.Doktor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doktorCalismaTakvimi == null)
            {
                return NotFound();
            }

            return View(doktorCalismaTakvimi);
        }

        // GET: DoktorCalismaTakvimi/Create
        public IActionResult Create()
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not doktor show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisi.adminMi == false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            ViewData["DoktorId"] = new SelectList(_context.Doktorlar, "Id", "Id");
            return View();
        }

        // POST: DoktorCalismaTakvimi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DoktorId,Tarih,dokuz_on,on_onbir,onbir_oniki,onuc_ondort,ondort_onbes,onbes_onalti,onalti_onyedi")] DoktorCalismaTakvimi doktorCalismaTakvimi)
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not doktor show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisi.adminMi == false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            if (ModelState.IsValid)
            {
                _context.Add(doktorCalismaTakvimi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoktorId"] = new SelectList(_context.Doktorlar, "Id", "Id", doktorCalismaTakvimi.DoktorId);
            return View(doktorCalismaTakvimi);
        }

        // GET: DoktorCalismaTakvimi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not doktor show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisi.adminMi == false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            if (id == null || _context.DoktorCalismaTakvimleri == null)
            {
                return NotFound();
            }

            var doktorCalismaTakvimi = await _context.DoktorCalismaTakvimleri.FindAsync(id);
            if (doktorCalismaTakvimi == null)
            {
                return NotFound();
            }
            ViewData["DoktorId"] = new SelectList(_context.Doktorlar, "Id", "Id", doktorCalismaTakvimi.DoktorId);
            return View(doktorCalismaTakvimi);
        }

        // POST: DoktorCalismaTakvimi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DoktorId,Tarih,dokuz_on,on_onbir,onbir_oniki,onuc_ondort,ondort_onbes,onbes_onalti,onalti_onyedi")] DoktorCalismaTakvimi doktorCalismaTakvimi)
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            if (id != doktorCalismaTakvimi.Id)
            {
                return NotFound();
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not doktor show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisi.adminMi == false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doktorCalismaTakvimi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoktorCalismaTakvimiExists(doktorCalismaTakvimi.Id))
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
            ViewData["DoktorId"] = new SelectList(_context.Doktorlar, "Id", "Id", doktorCalismaTakvimi.DoktorId);
            return View(doktorCalismaTakvimi);
        }

        // GET: DoktorCalismaTakvimi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not doktor show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisi.Doktor == false && kisi.adminMi == false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            if (id == null || _context.DoktorCalismaTakvimleri == null)
            {
                return NotFound();
            }

            var doktorCalismaTakvimi = await _context.DoktorCalismaTakvimleri
                .Include(d => d.Doktor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doktorCalismaTakvimi == null)
            {
                return NotFound();
            }

            // check if doktor id is same as kisi id in session data or if kisi is admin and if not, show not authorized page in kisi controller
            if(kisi.Id != doktorCalismaTakvimi.DoktorId && kisi.adminMi == false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            if(doktorCalismaTakvimi.dokuz_on == 2 || doktorCalismaTakvimi.on_onbir == 2 || doktorCalismaTakvimi.onbir_oniki == 2 || doktorCalismaTakvimi.onuc_ondort == 2 || doktorCalismaTakvimi.ondort_onbes == 2 || doktorCalismaTakvimi.onbes_onalti == 2 || doktorCalismaTakvimi.onalti_onyedi == 2)
            {
                ViewBag.Error = "Bu çalışma takvimini silemezsiniz. Çünkü bu çalışma takvimine ait randevular var.";
                return View(doktorCalismaTakvimi);
            }
            
            return View(doktorCalismaTakvimi);
        }

        // POST: DoktorCalismaTakvimi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not doktor show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisi.Doktor == false && kisi.adminMi == false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            if (_context.DoktorCalismaTakvimleri == null)
            {
                return Problem("Entity set 'HastaneContext.DoktorCalismaTakvimleri'  is null.");
            }
            var doktorCalismaTakvimi = await _context.DoktorCalismaTakvimleri.FindAsync(id);

            // check if doktor id is same as kisi id in session data or if kisi is admin and if not, show not authorized page in kisi controller
            if (kisi.Id != doktorCalismaTakvimi.DoktorId && kisi.adminMi == false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }


            // check if any of hours is equal to 2 even one of them is return calisma takvimi delete page with error message
            if (doktorCalismaTakvimi.dokuz_on == 2 || doktorCalismaTakvimi.on_onbir == 2 || doktorCalismaTakvimi.onbir_oniki == 2 || doktorCalismaTakvimi.onuc_ondort == 2 || doktorCalismaTakvimi.ondort_onbes == 2 || doktorCalismaTakvimi.onbes_onalti == 2 || doktorCalismaTakvimi.onalti_onyedi == 2)
            {
                ViewBag.Error = "Bu çalışma takvimini silemezsiniz. Çünkü bu çalışma takvimine ait randevular var.";
                return View(doktorCalismaTakvimi);
            }

            if (doktorCalismaTakvimi != null)
            {
                _context.DoktorCalismaTakvimleri.Remove(doktorCalismaTakvimi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("CalismaTakvimi", "DoktorCalismaTakvimi");
        }

        private bool DoktorCalismaTakvimiExists(int id)
        {
            return _context.DoktorCalismaTakvimleri.Any(e => e.Id == id);
        }


        // Get: DoktorCalismaTakvimi/Olustur
        public async Task<IActionResult> Olustur()
        {
            // get session data from cookie and if it is null, redirect to login page or if it is not doktor show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);
            ViewBag.kisiNavbar = kisi; // navbar için

            if (kisi.Doktor == false && kisi.adminMi == false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            return View();
        }

        // Post: DoktorCalismaTakvimi/Olustur
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Olustur(string Date, string workIn9am, string workIn10am, string workIn11am, string workIn1pm, string workIn2pm, string workIn3pm, string workIn4pm)
        {

            // get session data from cookie and if it is null, redirect to login page or if it is not doktor show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);
            ViewBag.kisiNavbar = kisi; // navbar için

            if (kisi.Doktor == false && kisi.adminMi == false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            // get doktor id from session data
            int doktorId = kisi.Id;

            // check if there is already a record for the selected date
            var doktorCalismaTakvimi = await _context.DoktorCalismaTakvimleri
                .FirstOrDefaultAsync(m => m.DoktorId == doktorId && m.Tarih == DateTime.Parse(Date));

            // if there is no record for the selected date, create a new one
            if (doktorCalismaTakvimi == null)
            {
                doktorCalismaTakvimi = new DoktorCalismaTakvimi();
                doktorCalismaTakvimi.DoktorId = doktorId;
                doktorCalismaTakvimi.Tarih = DateTime.Parse(Date);
                doktorCalismaTakvimi.dokuz_on = Int32.Parse(workIn9am);
                doktorCalismaTakvimi.on_onbir = Int32.Parse(workIn10am);
                doktorCalismaTakvimi.onbir_oniki = Int32.Parse(workIn11am);
                doktorCalismaTakvimi.onuc_ondort = Int32.Parse(workIn1pm);
                doktorCalismaTakvimi.ondort_onbes = Int32.Parse(workIn2pm);
                doktorCalismaTakvimi.onbes_onalti = Int32.Parse(workIn3pm);
                doktorCalismaTakvimi.onalti_onyedi = Int32.Parse(workIn4pm);
            }
            else
            {
                // add model state error that say there is already a record for the selected date
                ModelState.AddModelError("Tarih", "Bu tarihe ait bir çalışma takvimi zaten var.");
                return View();
            }

            // check if the model is valid
            if (ModelState.IsValid)
            {
                // add the new record to the database
                _context.Add(doktorCalismaTakvimi);
                await _context.SaveChangesAsync();
                return RedirectToAction("Success", "DoktorCalismaTakvimi", new { id = doktorCalismaTakvimi.Id });
            }
            else
            {
                return View();
            }
        }


        // show success page after creating a new calisma takvimi
        public IActionResult Success(int? id)
        {

            if (id == null || _context.Randevular == null)
            {
                return NotFound();
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not doktor show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);
            ViewBag.kisiNavbar = kisi; // navbar için

            if (kisi.Doktor == false && kisi.adminMi == false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            var doktorCalismaTakvimi = _context.DoktorCalismaTakvimleri
                .Include(d => d.Doktor)
                .Include(d => d.Doktor.Kisi)
                .FirstOrDefault(m => m.Id == id);

            if (doktorCalismaTakvimi == null)
            {
                return NotFound();
            }

            return View(doktorCalismaTakvimi);

        }

        //show all calisma takvims for given doktor
        public async Task<IActionResult> CalismaTakvimi()
        {

            // get session data from cookie and if it is null, redirect to login page or if it is not doktor show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);
            ViewBag.kisiNavbar = kisi; // navbar için

            if (kisi.Doktor == false && kisi.adminMi == false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            var doktorid = kisi.Id;

            var doktorCalismaTakvimi = await _context.DoktorCalismaTakvimleri
                .Include(d => d.Doktor)
                .Include(d => d.Doktor.Kisi)
                .Where(m => m.DoktorId == doktorid)
                .ToListAsync();

            // remove the time part from the date
            foreach (var item in doktorCalismaTakvimi)
            {
                item.Tarih = item.Tarih.Date;
            }

            // dont show the past calisma takvims
            doktorCalismaTakvimi = doktorCalismaTakvimi.Where(m => m.Tarih >= DateTime.Now.Date).ToList();


            if (doktorCalismaTakvimi == null)
            {
                return NotFound();
            }

            return View(doktorCalismaTakvimi);
        }

    }
}
