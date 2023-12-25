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
    public class RandevuOtoController : Controller
    {
        private readonly HastaneContext _context;

        public RandevuOtoController(HastaneContext context)
        {
            _context = context;
        }

        // GET: RandevuOto
        public async Task<IActionResult> Index()
        {
            var hastaneContext = _context.Randevular.Include(r => r.Doktor).Include(r => r.Hasta).Include(r => r.Poliklinik);
            return View(await hastaneContext.ToListAsync());
        }

        // GET: RandevuOto/Details/5
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

        // GET: RandevuOto/Create
        public IActionResult Create()
        {
            ViewData["DoktorId"] = new SelectList(_context.Doktorlar, "Id", "Id");
            ViewData["HastaId"] = new SelectList(_context.Hastalar, "Id", "Id");
            ViewData["PoliklinikId"] = new SelectList(_context.Poliklinikler, "Id", "Ad");
            return View();
        }

        // POST: RandevuOto/Create
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

        // GET: RandevuOto/Edit/5
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

        // POST: RandevuOto/Edit/5
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

        // GET: RandevuOto/Delete/5
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

        // POST: RandevuOto/Delete/5
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
    }
}
