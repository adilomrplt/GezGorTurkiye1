using Microsoft.AspNetCore.Mvc;
using GezGorTurkiye.DataAccess;
using GezGorTurkiye.Entities;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GezGorTurkiye.Controllers
{
    public class MekanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MekanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Mekan Listesi
        public IActionResult Index(string rota)
        {
            var mekanlarList = _context.Mekanlar
                .Include(m => m.Sehir)
                .ToList();

            if (rota == "otomatik" && mekanlarList.Count > 1)
            {
                var baslangic = mekanlarList[0]; // İlk mekan başlangıç
                var rotaSirali = new List<Mekan> { baslangic };
                var kalanlar = mekanlarList.Skip(1).ToList();

                int siraNo = 1;
                baslangic.SiraNo = siraNo;

                while (kalanlar.Any())
                {
                    var sonMekan = rotaSirali.Last();
                    var sonKonum = sonMekan.Konum.Split(',');
                    if (sonKonum.Length < 2) break;

                    double lat1 = Convert.ToDouble(sonKonum[0]);
                    double lon1 = Convert.ToDouble(sonKonum[1]);

                    Mekan enYakin = null;
                    double minMesafe = double.MaxValue;

                    foreach (var m in kalanlar)
                    {
                        var konum = m.Konum.Split(',');
                        if (konum.Length < 2) continue;

                        double lat2 = Convert.ToDouble(konum[0]);
                        double lon2 = Convert.ToDouble(konum[1]);

                        var mesafe = Haversine(lat1, lon1, lat2, lon2);
                        if (mesafe < minMesafe)
                        {
                            minMesafe = mesafe;
                            enYakin = m;
                        }
                    }

                    if (enYakin == null) break;

                    siraNo++;
                    enYakin.SiraNo = siraNo;
                    rotaSirali.Add(enYakin);
                    kalanlar.Remove(enYakin);
                }

                // Veritabanını Güncelle
                _context.Mekanlar.UpdateRange(rotaSirali);
                _context.SaveChanges();

                // Güncellenmiş listeyi tekrar al
                mekanlarList = rotaSirali;
            }
            else if (rota == "manuel")
            {
                mekanlarList = mekanlarList.OrderBy(m => m.SiraNo ?? 0).ToList();
            }

            ViewBag.Mekanlar = mekanlarList.Select(m => new
            {
                m.Id,
                m.Ad,
                m.Kategori,
                m.Aciklama,
                m.Konum,
                m.SiraNo,
                SehirAdi = m.Sehir?.Ad ?? "Bilinmiyor"
            }).ToList();

            ViewBag.RotaTipi = rota;
            return View();
        }



        private double Haversine(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371; // Dünya yarıçapı km
            double dLat = (lat2 - lat1) * Math.PI / 180.0;
            double dLon = (lon2 - lon1) * Math.PI / 180.0;

            lat1 = lat1 * Math.PI / 180.0;
            lat2 = lat2 * Math.PI / 180.0;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c; // km cinsinden mesafe
        }


        // Mekan Ekle - GET
        [HttpGet]
        public IActionResult Ekle()
        {
            ViewBag.Sehirler = new SelectList(_context.Sehirler.ToList(), "Id", "Ad");
            return View();
        }

        // Mekan Ekle - POST
        [HttpPost]
        public IActionResult Ekle(Mekan mekan)
        {
            _context.Mekanlar.Add(mekan);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Mekan Sil
        public IActionResult Sil(int id)
        {
            var mekan = _context.Mekanlar.Find(id);
            if (mekan != null)
            {
                _context.Mekanlar.Remove(mekan);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Mekan Güncelle - GET
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var mekan = _context.Mekanlar.FirstOrDefault(m => m.Id == id);
            if (mekan == null)
            {
                return NotFound();
            }

            ViewBag.Sehirler = new SelectList(_context.Sehirler.ToList(), "Id", "Ad", mekan.SehirId);
            return View(mekan);
        }

        // Mekan Güncelle - POST
        [HttpPost]
        public IActionResult Guncelle(Mekan mekan)
        {
            _context.Mekanlar.Update(mekan);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // AJAX ile sıralama güncelleme
        [HttpPost]
        public IActionResult UpdateSira(List<MekanSira> mekanSiralama)
        {
            foreach (var mekan in mekanSiralama)
            {
                var m = _context.Mekanlar.Find(mekan.Id);
                if (m != null)
                {
                    m.SiraNo = mekan.SiraNo;
                }
            }

            _context.SaveChanges();
            return Ok();
        }
    }

    public class MekanSira
    {
        public int Id { get; set; }
        public int SiraNo { get; set; }
    }
}
