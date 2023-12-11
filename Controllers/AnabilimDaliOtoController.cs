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
    public class AnabilimDaliOtoController : Controller
    {
        private readonly HastaneContext _context;

        public AnabilimDaliOtoController(HastaneContext context)
        {
            _context = context;
        }

        // GET: AnabilimDaliOto
        public async Task<IActionResult> Index()
        {
            var hastaneContext = _context.AnabilimDallari.Include(a => a.Yonetici);
            return View(await hastaneContext.ToListAsync());
        }

        // GET: AnabilimDaliOto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnabilimDallari == null)
            {
                return NotFound();
            }

            var anabilimDali = await _context.AnabilimDallari
                .Include(a => a.Yonetici)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anabilimDali == null)
            {
                return NotFound();
            }

            return View(anabilimDali);
        }

        // GET: AnabilimDaliOto/Create
        public IActionResult Create()
        {
            ViewData["YoneticiId"] = new SelectList(_context.Kisiler, "Id", "Ad");
            return View();
        }

        // POST: AnabilimDaliOto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Aciklama,YoneticiId,Adres,TelefonNo,FaxNo,Eposta,KurulusTarihi,AktiflikDurumu")] AnabilimDali anabilimDali)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anabilimDali);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["YoneticiId"] = new SelectList(_context.Kisiler, "Id", "Ad", anabilimDali.YoneticiId);
            return View(anabilimDali);
        }

        // GET: AnabilimDaliOto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnabilimDallari == null)
            {
                return NotFound();
            }

            var anabilimDali = await _context.AnabilimDallari.FindAsync(id);
            if (anabilimDali == null)
            {
                return NotFound();
            }
            ViewData["YoneticiId"] = new SelectList(_context.Kisiler, "Id", "Ad", anabilimDali.YoneticiId);
            return View(anabilimDali);
        }

        // POST: AnabilimDaliOto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Aciklama,YoneticiId,Adres,TelefonNo,FaxNo,Eposta,KurulusTarihi,AktiflikDurumu")] AnabilimDali anabilimDali)
        {
            if (id != anabilimDali.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anabilimDali);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnabilimDaliExists(anabilimDali.Id))
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
            ViewData["YoneticiId"] = new SelectList(_context.Kisiler, "Id", "Ad", anabilimDali.YoneticiId);
            return View(anabilimDali);
        }

        // GET: AnabilimDaliOto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnabilimDallari == null)
            {
                return NotFound();
            }

            var anabilimDali = await _context.AnabilimDallari
                .Include(a => a.Yonetici)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anabilimDali == null)
            {
                return NotFound();
            }

            return View(anabilimDali);
        }

        // POST: AnabilimDaliOto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnabilimDallari == null)
            {
                return Problem("Entity set 'HastaneContext.AnabilimDallari'  is null.");
            }
            var anabilimDali = await _context.AnabilimDallari.FindAsync(id);
            if (anabilimDali != null)
            {
                _context.AnabilimDallari.Remove(anabilimDali);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnabilimDaliExists(int id)
        {
          return (_context.AnabilimDallari?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
