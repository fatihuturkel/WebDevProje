using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebDevProje.Models;

namespace WebDevProje.Controllers
{
    public class PoliklinikController : Controller
    {
        private readonly HastaneContext _context;

        public PoliklinikController(HastaneContext context)
        {
            _context = context;
        }

        // GET: Poliklinik
        public async Task<IActionResult> Index()
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not admin show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisi.adminMi is false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            var hastaneContext = _context.Poliklinikler.Include(p => p.AnabilimDali);
            return View(await hastaneContext.ToListAsync());
        }

        // GET: Poliklinik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not admin show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisi.adminMi is false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            if (id == null || _context.Poliklinikler == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Poliklinikler
                .Include(p => p.AnabilimDali)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poliklinik == null)
            {
                return NotFound();
            }

            return View(poliklinik);
        }

        // GET: Poliklinik/Create
        public IActionResult Create()
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not admin show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisi.adminMi is false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            ViewData["AnabilimDaliId"] = new SelectList(_context.AnabilimDallari, "Id", "Ad");
            return View();
        }

        // POST: Poliklinik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Aciklama,Adres,TelefonNo,FaxNo,Eposta,KurulusTarihi,AktiflikDurumu,AnabilimDaliId")] Poliklinik poliklinik)
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not admin show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisi.adminMi is false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            if (ModelState.IsValid)
            {
                _context.Add(poliklinik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnabilimDaliId"] = new SelectList(_context.AnabilimDallari, "Id", "Ad", poliklinik.AnabilimDaliId);
            return View(poliklinik);
        }

        // GET: Poliklinik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not admin show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisi.adminMi is false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            if (id == null || _context.Poliklinikler == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Poliklinikler.FindAsync(id);
            if (poliklinik == null)
            {
                return NotFound();
            }
            ViewData["AnabilimDaliId"] = new SelectList(_context.AnabilimDallari, "Id", "Ad", poliklinik.AnabilimDaliId);
            return View(poliklinik);
        }

        // POST: Poliklinik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Aciklama,Adres,TelefonNo,FaxNo,Eposta,KurulusTarihi,AktiflikDurumu,AnabilimDaliId")] Poliklinik poliklinik)
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not admin show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisi.adminMi is false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            if (id != poliklinik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poliklinik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoliklinikExists(poliklinik.Id))
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
            ViewData["AnabilimDaliId"] = new SelectList(_context.AnabilimDallari, "Id", "Ad", poliklinik.AnabilimDaliId);
            return View(poliklinik);
        }

        // GET: Poliklinik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

            // get session data from cookie and if it is null, redirect to login page or if it is not admin show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisi = JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisi.adminMi is false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

            if (id == null || _context.Poliklinikler == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Poliklinikler
                .Include(p => p.AnabilimDali)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poliklinik == null)
            {
                return NotFound();
            }

            return View(poliklinik);
        }

        // POST: Poliklinik/Delete/5
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

            if (_context.Poliklinikler == null)
            {
                return Problem("Entity set 'HastaneContext.Poliklinikler'  is null.");
            }
            var poliklinik = await _context.Poliklinikler.FindAsync(id);
            if (poliklinik != null)
            {
                _context.Poliklinikler.Remove(poliklinik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoliklinikExists(int id)
        {
            return _context.Poliklinikler.Any(e => e.Id == id);
        }
    }
}
