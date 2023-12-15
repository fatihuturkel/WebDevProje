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

        // GET: AnabilimDali
        public async Task<IActionResult> Index()
        {
            var hastaneContext = _context.AnabilimDallari.Include(a => a.Yonetici);
            return View(await hastaneContext.ToListAsync());
        }

        // GET: AnabilimDali/Details/5
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

        // GET: AnabilimDali/Create
        public async Task<IActionResult> Create()
        {
            // Get all yöneticiler from Kisi table
            var yoneticiler = await _context.Kisiler
                .Where(k => k.Yonetici)
                .Select(k => new { Id = k.Id, DisplayName = k.Ad + " " + k.Soyad + " (" + k.TcKimlikNo + ")" }) // Create an anonymous object
                .ToListAsync();

            // Create a SelectList for yöneticiler dropdown with both Ad and Soyad
            var yoneticilerDropdown = new SelectList(yoneticiler, "Id", "DisplayName");

            // Pass the yoneticilerDropdown to the view
            ViewData["Yoneticiler"] = yoneticilerDropdown;

            return View();
        }


        // POST: AnabilimDali/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Aciklama,YoneticiId,Adres,TelefonNo,FaxNo,Eposta,KurulusTarihi,AktiflikDurumu")] AnabilimDali anabilimDali)
        {
            if (ModelState.IsValid)
            {
                /* Gerekli gibi durmuyor.
                // Assuming _context is your database context
                // Retrieve the selected Yonetici from the database
                var selectedYonetici = await _context.Kisiler.FindAsync(anabilimDali.YoneticiId);

                // Set the Yonetici property of AnabilimDali
                anabilimDali.Yonetici = selectedYonetici;*/

                // Add AnabilimDali to the context and save changes
                _context.Add(anabilimDali);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Get all yöneticiler from Kisi table
            var yoneticiler = await _context.Kisiler
                .Where(k => k.Yonetici)
                .Select(k => new { Id = k.Id, DisplayName = k.Ad + " " + k.Soyad + " (" + k.TcKimlikNo + ")" }) // Create an anonymous object
                .ToListAsync();

            // Create a SelectList for yöneticiler dropdown with both Ad and Soyad
            var yoneticilerDropdown = new SelectList(yoneticiler, "Id", "DisplayName");

            // Pass the yoneticilerDropdown to the view
            ViewData["Yoneticiler"] = yoneticilerDropdown;

            return View(anabilimDali);
        }



        // GET: AnabilimDali/Edit/5
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
            // Get all yöneticiler from Kisi table
            var yoneticiler = await _context.Kisiler
                .Where(k => k.Yonetici)
                .Select(k => new { Id = k.Id, DisplayName = k.Ad + " " + k.Soyad + " (" + k.TcKimlikNo + ")" }) // Create an anonymous object
                .ToListAsync();

            // Create a SelectList for yöneticiler dropdown with both Ad and Soyad
            var yoneticilerDropdown = new SelectList(yoneticiler, "Id", "DisplayName");

            // Pass the yoneticilerDropdown to the view
            ViewData["Yoneticiler"] = yoneticilerDropdown;

            return View(anabilimDali);
        }

        // POST: AnabilimDali/Edit/5
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
            
            // Get all yöneticiler from Kisi table
            var yoneticiler = await _context.Kisiler
                .Where(k => k.Yonetici)
                .Select(k => new { Id = k.Id, DisplayName = k.Ad + " " + k.Soyad + " (" + k.TcKimlikNo + ")" }) // Create an anonymous object
                .ToListAsync();

            // Create a SelectList for yöneticiler dropdown with both Ad and Soyad
            var yoneticilerDropdown = new SelectList(yoneticiler, "Id", "DisplayName");

            // Pass the yoneticilerDropdown to the view
            ViewData["Yoneticiler"] = yoneticilerDropdown;
            return View(anabilimDali);
        }

        // GET: AnabilimDali/Delete/5
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

        // POST: AnabilimDali/Delete/5
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
