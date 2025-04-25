using Microsoft.AspNetCore.Mvc;
using GezGorTurkiye.DataAccess;
using System.Linq;
using GezGorTurkiye.Entities;

namespace GezGorTurkiye.Controllers
{
    public class SehirController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SehirController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sehirler = _context.Sehirler.ToList();
            return View(sehirler);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(GezGorTurkiye.Entities.Sehir sehir)
        {
            if (ModelState.IsValid)
            {
                _context.Sehirler.Add(sehir);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sehir);
        }
        public IActionResult Sil(int id)
        {
            var sehir = _context.Sehirler.Find(id);
            if (sehir != null)
            {
                _context.Sehirler.Remove(sehir);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var sehir = _context.Sehirler.Find(id);
            if (sehir == null)
            {
                return NotFound();
            }
            return View(sehir);
        }
        [HttpPost]
        public IActionResult Guncelle(Sehir sehir)
        {
            _context.Sehirler.Update(sehir);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
