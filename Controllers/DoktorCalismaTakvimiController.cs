using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            if (kisi.Doktor != true)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }
            

            var hastaneContext = _context.DoktorCalismaTakvimleri.Include(d => d.Doktor);
            return View(await hastaneContext.ToListAsync());
        }

        // GET: DoktorCalismaTakvimi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
            if (id != doktorCalismaTakvimi.Id)
            {
                return NotFound();
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

        // POST: DoktorCalismaTakvimi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DoktorCalismaTakvimleri == null)
            {
                return Problem("Entity set 'HastaneContext.DoktorCalismaTakvimleri'  is null.");
            }
            var doktorCalismaTakvimi = await _context.DoktorCalismaTakvimleri.FindAsync(id);
            if (doktorCalismaTakvimi != null)
            {
                _context.DoktorCalismaTakvimleri.Remove(doktorCalismaTakvimi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoktorCalismaTakvimiExists(int id)
        {
          return _context.DoktorCalismaTakvimleri.Any(e => e.Id == id);
        }



    }
}
