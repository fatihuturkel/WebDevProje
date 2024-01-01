using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebDevProje.Models;

namespace WebDevProje.Controllers
{
    public class DoktorController : Controller
    {
        private readonly HastaneContext _context;

        public DoktorController(HastaneContext context)
        {
            _context = context;
        }

        // GET: Doktor
        public async Task<IActionResult> Index()
        {

            // get session data from cookie and if it is null, redirect to login page or if it is not doktor show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }
            var kisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Kisi>(kisiJson);
            ViewBag.kisiNavbar = kisi; // navbar için

            if (kisi.Yonetici != true)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            var hastaneContext = _context.Doktorlar.Include(d => d.Kisi).Include(d => d.Poliklinik);
            return View(await hastaneContext.ToListAsync());
        }

        // GET: Doktor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            if (id == null || _context.Doktorlar == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktorlar
                .Include(d => d.Kisi)
                .Include(d => d.Poliklinik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // GET: Doktor/Create
        public IActionResult Create()
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            // Get all kisiler from Kisi table where doktor is true
            var kisiler = _context.Kisiler
                .Where(k => k.Doktor)
                .Select(k => new { Id = k.Id, DisplayName = k.Ad + " " + k.Soyad + " (" + k.TcKimlikNo + ")" }) // Create an anonymous object
                .ToList();
            ViewData["Id"] = new SelectList(kisiler, "Id", "DisplayName");
            ViewData["PoliklinikId"] = new SelectList(_context.Poliklinikler, "Id", "Ad");
            return View();
        }

        // POST: Doktor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Maas,PoliklinikId")] Doktor doktor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doktor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Get all kisiler from Kisi table where doktor is true
            var kisiler = _context.Kisiler
                .Where(k => k.Doktor)
                .Select(k => new { Id = k.Id, DisplayName = k.Ad + " " + k.Soyad + " (" + k.TcKimlikNo + ")" }) // Create an anonymous object
                .ToList();
            ViewData["Id"] = new SelectList(kisiler, "Id", "DisplayName");
            ViewData["PoliklinikId"] = new SelectList(_context.Poliklinikler, "Id", "Ad", doktor.PoliklinikId);
            return View(doktor);
        }

        // GET: Doktor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            if (id == null || _context.Doktorlar == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktorlar.FindAsync(id);
            if (doktor == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Kisiler, "Id", "Ad", doktor.Id);
            ViewData["PoliklinikId"] = new SelectList(_context.Poliklinikler, "Id", "Ad", doktor.PoliklinikId);
            return View(doktor);
        }

        // POST: Doktor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Maas,PoliklinikId")] Doktor doktor)
        {
            if (id != doktor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doktor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoktorExists(doktor.Id))
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
            ViewData["Id"] = new SelectList(_context.Kisiler, "Id", "Ad", doktor.Id);
            ViewData["PoliklinikId"] = new SelectList(_context.Poliklinikler, "Id", "Ad", doktor.PoliklinikId);
            return View(doktor);
        }

        // GET: Doktor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            if (id == null || _context.Doktorlar == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktorlar
                .Include(d => d.Kisi)
                .Include(d => d.Poliklinik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // POST: Doktor/Delete/5
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

            if (_context.Doktorlar == null)
            {
                return Problem("Entity set 'HastaneContext.Doktorlar'  is null.");
            }
            var doktor = await _context.Doktorlar.FindAsync(id);
            if (doktor != null)
            {
                _context.Doktorlar.Remove(doktor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoktorExists(int id)
        {
            return _context.Doktorlar.Any(e => e.Id == id);
        }
    }
}