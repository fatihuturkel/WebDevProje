using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
                if (kisi.Yonetici == true)
                {
                    ModelState.AddModelError("Yonetici", "Yönetici silinemez.");
                    return View(kisi);
                }
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
                {
                    // check password
                    if (Kisi.Sifre == kisi.Sifre)
                    {
                        // login
                        // save kisi object as json in session
                        string kisiJson = JsonConvert.SerializeObject(kisi);
                        HttpContext.Session.SetString("kisi", kisiJson);
                        return RedirectToAction("Profile", "Kisi");
                    }
                    else
                    {
                        ModelState.AddModelError("Sifre", "Şifre hatalı.");
                        return View(Kisi);
                    }
                }
            }
            else
            {
                return View(Kisi);
            }
        }


        // Get: Kisi/Kayit
        public IActionResult Kayit()
        {
            return View();
        }

        // Post: Kisi/Kayit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Kayit([Bind("Id,Ad,Soyad,Cinsiyet,DogumTarihi,TelefonNo,Eposta,TcKimlikNo,Sifre")] Kisi kisi)
        {
            //search if tc kimlik no is already in database and if it is then return hasta attribute of that person
            var kisiInDb = await _context.Kisiler.FirstOrDefaultAsync(m => m.TcKimlikNo == kisi.TcKimlikNo);
            if (kisiInDb is not null)
            {
                if (kisiInDb.Hasta == true)
                {
                    ModelState.AddModelError("TcKimlikNo", "Bu TC kimlik numarası ile kayıtlı bir hasta bulunmaktadır. Lütfen giriş yapınız.");
                    return View(kisi);
                }
            }

            if (ModelState.IsValid)
            {
                kisi.Hasta = true;
                _context.Add(kisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(kisi);
        }

        // logout
        // GET: Kisi/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("kisi");
            return RedirectToAction("Index", "Home");
        }

        // GET: Kisi/Profil
        public IActionResult Profile()
        {
            // get kisi object from session
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson is null)
            {
                return RedirectToAction("Login", "Kisi");
            }
            else
            {
                var kisi = JsonConvert.DeserializeObject<Kisi>(kisiJson);
                return View(kisi);
            }
        }


        // GET: Kisi/NotAuthorized
        public IActionResult NotAuthorized()
        {
            return View();
        }

        // get: adminlogin
        public IActionResult AdminLogin()
        {
            return View();
        }

        // post: adminlogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin([Bind("TcKimlikNo,Sifre")] Kisi Kisi)
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
                    ModelState.AddModelError("TcKimlikNo", "Bu TC kimlik numarası ile kayıtlı bir kişi bulunmamaktadır.");
                    return View(Kisi);
                }
                else
                {
                    if (kisi.adminMi == false)
                    {
                        ModelState.AddModelError("TcKimlikNo", "Bu TC kimlik numarası ile kayıtlı bir admin bulunmamaktadır.");
                        return View(Kisi);
                    }
                    // check password
                    if (Kisi.Sifre == kisi.Sifre)
                    {
                        // login
                        // save kisi object as json in session
                        string kisiJson = JsonConvert.SerializeObject(kisi);
                        HttpContext.Session.SetString("kisi", kisiJson);
                        return RedirectToAction("SuccessfulLogin", "Kisi");
                    }
                    else
                    {
                        ModelState.AddModelError("Sifre", "Şifre hatalı.");
                        return View(Kisi);
                    }
                }
            }
            else
            {
                return View(Kisi);
            }
        }


        // get: successfull login
        public IActionResult SuccessfulLogin()
        {
            string kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson is null)
            {
                return RedirectToAction("Login", "Kisi");
            }
            else
            {
                var kisi = JsonConvert.DeserializeObject<Kisi>(kisiJson);
                return View(kisi);
            }
        }
    }
}
