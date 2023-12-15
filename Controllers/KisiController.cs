﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevProje.Models;

namespace WebDevProje.Controllers
{
    public class KisiController : Controller
    {
        private readonly HastaneContext _context;

        public KisiController(HastaneContext context)
        {
            _context = context;
        }

        // GET: Kisi
        public async Task<IActionResult> Index()
        {
            return _context.Kisiler != null ?
                        View(await _context.Kisiler.ToListAsync()) :
                        Problem("Entity set 'HastaneContext.Kisiler'  is null.");
        }

        // GET: Kisi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kisiler == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisiler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kisi == null)
            {
                return NotFound();
            }

            return View(kisi);
        }

        // GET: Kisi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kisi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Soyad,Cinsiyet,DogumTarihi,TelefonNo,Eposta,TcKimlikNo,Doktor,Hasta,Hemsire,Isci,Yonetici,Sifre")] Kisi kisi)
        {
            if (ModelState.IsValid)
            {
                // bu blokta eğer doktor, hemşire, işçi ve yönetici görevlerinden sadece birinin veya hiçbirinin seçilmemesini sağlar
                var selectedRolesCount = new[] { kisi.Doktor, kisi.Hemsire, kisi.Isci, kisi.Yonetici }.Count(x => x);

                if (selectedRolesCount > 1)
                {
                    ModelState.AddModelError("", "Doktor, hemşire, işçi ve yönetici görevleri arasından biri seçilmelidir.");
                    return View(kisi);
                }

                _context.Add(kisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(kisi);
        }

        // GET: Kisi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kisiler == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisiler.FindAsync(id);
            if (kisi == null)
            {
                return NotFound();
            }
            return View(kisi);
        }

        // POST: Kisi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Soyad,Cinsiyet,DogumTarihi,TelefonNo,Eposta,TcKimlikNo,Doktor,Hasta,Hemsire,Isci,Yonetici,Sifre,adminMi")] Kisi kisi)
        {
            if (id != kisi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // bu blokta eğer doktor, hemşire, işçi ve yönetici görevlerinden sadece birinin veya hiçbirinin seçilmemesini sağlar
                var selectedRolesCount = new[] { kisi.Doktor, kisi.Hemsire, kisi.Isci, kisi.Yonetici }.Count(x => x);

                if (selectedRolesCount > 1)
                {
                    ModelState.AddModelError("", "Doktor, hemşire, işçi ve yönetici görevleri arasından biri seçilmelidir.");
                    return View(kisi);
                }

                try
                {
                    _context.Update(kisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KisiExists(kisi.Id))
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
            return View(kisi);
        }

        // GET: Kisi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kisiler == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisiler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kisi == null)
            {
                return NotFound();
            }

            return View(kisi);
        }

        // POST: Kisi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kisiler == null)
            {
                return Problem("Entity set 'HastaneContext.Kisiler'  is null.");
            }
            var kisi = await _context.Kisiler.FindAsync(id);
            if (kisi != null)
            {
                _context.Kisiler.Remove(kisi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KisiExists(int id)
        {
            return (_context.Kisiler?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // login
        // GET: Kisi/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Kisi/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("TcKimlikNo,Sifre")] Kisi Kisi)
        {
            // remove all errors from model state except tc kimlik no and password
            foreach (var key in ModelState.Keys.ToList())
            {
                if (key != "TcKimlikNo" && key != "Sifre")
                {
                    ModelState.Remove(key);
                }
            }

            // if model state is valid then check tc kimlik no and password
            if (ModelState.IsValid)
            {
                // search tc kimlik no in database
                var kisi = await _context.Kisiler.FirstOrDefaultAsync(m => m.TcKimlikNo == Kisi.TcKimlikNo);
                if (kisi == null)
                {
                    ModelState.AddModelError("TcKimlikNo", "Bu TC kimlik numarası ile kayıtlı bir kişi bulunmamaktadır. Lütfen kayıt olunuz.");
                    return View(Kisi);
                }
                else
                if (kisi.Hasta == true)
                {
                    // check password
                    if (Kisi.Sifre == kisi.Sifre)
                    {
                        // login
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Sifre", "Şifre hatalı.");
                        return View(Kisi);
                    }
                }
                else
                {
                    ModelState.AddModelError("TcKimlikNo", "Bu TC kimlik numarası ile kayıtlı bir hasta bulunmamaktadır. Lütfen kayıt olunuz.");
                    return View(Kisi);
                }
            }
            else
            {
                return View(Kisi);
            }
        }
    }
}