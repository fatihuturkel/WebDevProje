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

            return _context.Kisiler != null ?
                        View(await _context.Kisiler.ToListAsync()) :
                        Problem("Entity set 'HastaneContext.Kisiler'  is null.");
        }

        // GET: Kisi/Details/5
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

            var kisicontrol = JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisicontrol.adminMi is false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

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
            return View();
        }

        // POST: Kisi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Soyad,Cinsiyet,DogumTarihi,TelefonNo,Eposta,TcKimlikNo,Doktor,Hasta,Hemsire,Isci,Yonetici,Sifre")] Kisi kisi)
        {

            // get session data from cookie and if it is null, redirect to login page or if it is not admin show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisicontrol = JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisicontrol.adminMi is false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
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

                _context.Add(kisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(kisi);
        }

        // GET: Kisi/Edit/5
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

            var kisicontrol = JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisicontrol.adminMi is false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

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

            // get session data from cookie and if it is null, redirect to login page or if it is not admin show them "you are not authorized" page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson == null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            var kisicontrol = JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisicontrol.adminMi is false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

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

            var kisicontrol = JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisicontrol.adminMi is false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

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

            var kisicontrol = JsonConvert.DeserializeObject<Kisi>(kisiJson);
            if (kisicontrol.adminMi is false)
            {
                return RedirectToAction("NotAuthorized", "Kisi");
            }

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
            // if kisi is already logged in then redirect to profile page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson is not null)
            {
                return RedirectToAction("Profile", "Kisi");
            }

            return View();
        }

        // POST: Kisi/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("TcKimlikNo,Sifre")] Kisi Kisi)
        {
            // if kisi is already logged in then redirect to profile page
            var kisiJsonForLogedIn = HttpContext.Session.GetString("kisi");
            if (kisiJsonForLogedIn is not null)
            {
                return RedirectToAction("Profile", "Kisi");
            }

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

                    // check if kisi is admin
                    if (kisi.adminMi == true)
                    {
                        ModelState.AddModelError("TcKimlikNo", "Lütfen yetkili girişi yapınız.");
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


        // Get: Kisi/Kayit
        public IActionResult Kayit()
        {
            // if kisi is already logged in then redirect to profile page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson is not null)
            {
                return RedirectToAction("Profile", "Kisi");
            }
            return View();
        }

        // Post: Kisi/Kayit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Kayit([Bind("Id,Ad,Soyad,Cinsiyet,DogumTarihi,TelefonNo,Eposta,TcKimlikNo,Sifre")] Kisi kisi)
        {
            // if kisi is already logged in then redirect to profile page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson is not null)
            {
                return RedirectToAction("Profile", "Kisi");
            }

            //search if tc kimlik no is already in database and if it is then return hasta attribute of that person
            var kisiInDb = await _context.Kisiler.FirstOrDefaultAsync(m => m.TcKimlikNo == kisi.TcKimlikNo);
            if (kisiInDb is not null)
            {
                ModelState.AddModelError("TcKimlikNo", "Bu TC kimlik numarası ile kayıtlı bir kişi bulunmaktadır. Lütfen giriş yapınız. Eğer hastanemizde çalışıyorsanız lütfen Bilgi Teknolojileri Departmanı ile iletişime geçiniz.");
                return View(kisi);
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
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }

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
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }
            return View();
        }

        // get: adminlogin
        public IActionResult AdminLogin()
        {
            // if kisi is already logged in then redirect to profile page
            var kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson is not null)
            {
                return RedirectToAction("Profile", "Kisi");
            }
            return View();
        }

        // post: adminlogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin([Bind("TcKimlikNo,Sifre")] Kisi Kisi)
        {
            // if kisi is already logged in then redirect to profile page
            var kisiJsonForLogedIn = HttpContext.Session.GetString("kisi");
            if (kisiJsonForLogedIn is not null)
            {
                return RedirectToAction("Profile", "Kisi");
            }

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
                        ModelState.AddModelError("TcKimlikNo", "Bu TC kimlik numarası ile kayıtlı bir yetkili bulunmamaktadır. Lütfen normal giriş yapmayı deneyiniz.");
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
            // navbarda kisi bilgilerini göstermek için
            var kisiJsonNavbar = HttpContext.Session.GetString("kisi");
            if (kisiJsonNavbar is not null)
            {
                var kisiNavbar = JsonConvert.DeserializeObject<Kisi>(kisiJsonNavbar);
                ViewBag.kisiNavbar = kisiNavbar;
            }


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

        // get: change password
        public IActionResult ChangePassword()
        {
            string kisiJson = HttpContext.Session.GetString("kisi");
            if (kisiJson is null)
            {
                return RedirectToAction("Login", "Kisi");
            }

            return View();
        }

        // post: change password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword([Bind("TcKimlikNo,,Sifre")] Kisi Kisi)
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

            // remove all errors from model state except tc kimlik no and password
            foreach (var key in ModelState.Keys.ToList())
            {
                if (key != "TcKimlikNo" && key != "Sifre")
                {
                    ModelState.Remove(key);
                }
            }
        }
    }
}
