using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebDevProje.Models;

namespace WebDevProje.Controllers
{
    public class HastaController : Controller
    {
        private readonly HastaneContext _context;

        public HastaController(HastaneContext context)
        {
            _context = context;
        }

        // GET: Hasta
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

            var hastaneContext = _context.Hastalar.Include(h => h.Kisi);
            return View(await hastaneContext.ToListAsync());
        }

        // GET: Hasta/Details/5
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

            if (id == null || _context.Hastalar == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hastalar
                .Include(h => h.Kisi)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hasta == null)
            {
                return NotFound();
            }

            return View(hasta);
        }

        // GET: Hasta/Create
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

            ViewData["Id"] = new SelectList(_context.Kisiler, "Id", "Ad");
            return View();
        }

        // POST: Hasta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Hasta hasta)
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
                _context.Add(hasta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Kisiler, "Id", "Ad", hasta.Id);
            return View(hasta);
        }

        // GET: Hasta/Edit/5
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

            if (id == null || _context.Hastalar == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hastalar.FindAsync(id);
            if (hasta == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Kisiler, "Id", "Ad", hasta.Id);
            return View(hasta);
        }

        // POST: Hasta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Hasta hasta)
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

            if (id != hasta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hasta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastaExists(hasta.Id))
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
            ViewData["Id"] = new SelectList(_context.Kisiler, "Id", "Ad", hasta.Id);
            return View(hasta);
        }

        // GET: Hasta/Delete/5
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

            if (id == null || _context.Hastalar == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hastalar
                .Include(h => h.Kisi)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hasta == null)
            {
                return NotFound();
            }

            return View(hasta);
        }

        // POST: Hasta/Delete/5
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

            if (_context.Hastalar == null)
            {
                return Problem("Entity set 'HastaneContext.Hastalar'  is null.");
            }
            var hasta = await _context.Hastalar.FindAsync(id);
            if (hasta != null)
            {
                _context.Hastalar.Remove(hasta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HastaExists(int id)
        {
            return _context.Hastalar.Any(e => e.Id == id);
        }

        public async Task<IActionResult> UpdateList()
        {
            var kisiler = await _context.Kisiler.ToListAsync();

            foreach (var kisi in kisiler)
            {
                // if the person is hasta add this person to Hastalar table if not already added
                if (kisi.Hasta && !_context.Hastalar.Any(h => h.Id == kisi.Id))
                {
                    _context.Hastalar.Add(new Hasta { Id = kisi.Id });
                }
            }

            await _context.SaveChangesAsync();

            var hastaneContext = _context.Hastalar.Include(h => h.Kisi);
            return View(nameof(Index), await hastaneContext.ToListAsync());
        }
    }
}
