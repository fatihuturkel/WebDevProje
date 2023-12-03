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
    public class AnabilimDaliController : Controller
    {
        private readonly HastaneContext _context;

        public AnabilimDaliController(HastaneContext context)
        {
            _context = context;
        }

        // GET: AnabilimDalis
        public async Task<IActionResult> Index()
        {
              return _context.AnabilimDallari != null ? 
                          View(await _context.AnabilimDallari.ToListAsync()) :
                          Problem("Entity set 'HastaneContext.AnabilimDallari'  is null.");
        }

        // GET: AnabilimDalis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnabilimDallari == null)
            {
                return NotFound();
            }

            var anabilimDali = await _context.AnabilimDallari
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anabilimDali == null)
            {
                return NotFound();
            }

            return View(anabilimDali);
        }

        // GET: AnabilimDalis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnabilimDalis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Aciklama,Yonetici,Adres,Telefon,Eposta,Fax,KurulusTarihi,Statu")] AnabilimDali anabilimDali)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anabilimDali);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anabilimDali);
        }

        // GET: AnabilimDalis/Edit/5
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
            return View(anabilimDali);
        }

        // POST: AnabilimDalis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Aciklama,Yonetici,Adres,Telefon,Eposta,Fax,KurulusTarihi,Statu")] AnabilimDali anabilimDali)
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
            return View(anabilimDali);
        }

        // GET: AnabilimDalis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnabilimDallari == null)
            {
                return NotFound();
            }

            var anabilimDali = await _context.AnabilimDallari
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anabilimDali == null)
            {
                return NotFound();
            }

            return View(anabilimDali);
        }

        // POST: AnabilimDalis/Delete/5
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
