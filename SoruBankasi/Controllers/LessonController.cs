using Newtonsoft.Json;
using SoruBankasi.Infrastructure;
using SoruBankasi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoruBankasi.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("Lesson")]
    public class LessonController : Controller
    {
        public ActionResult Index()
        {
            using (SoruBankasiDbContext db = new SoruBankasiDbContext())
            {
                return View(db.Ders.ToList());
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Ders model)
        {
            if (ModelState.IsValid)
            {
                using (SoruBankasiDbContext db = new SoruBankasiDbContext())
                {
                    if (db.Ders.SingleOrDefault(x => x.DersAdi.Equals(model.DersAdi)) == null)
                    {
                        db.Entry(model).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                        ViewBag.Message = $"<div class='alert alert-success'><strong>Başarılı!</strong> Ders Başarıyla Eklendi... </div>";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = $"<div class='alert alert-danger'><strong>Hata!</strong> Bu ders adı zaten kullanılıyor... </div>";
                    }
                }
            }
            return View(model);
        }

        public ActionResult Edit(int ID)
        {
            using (SoruBankasiDbContext db = new SoruBankasiDbContext())
            {
                Ders ders = db.Ders.SingleOrDefault(x => x.ID.Equals(ID));
                if (ders != null)
                {
                    return View(ders);
                }
            }
            return RedirectToRoute("Lessons");
        }
        [HttpPost]
        public ActionResult Edit(Ders model)
        {
            if (ModelState.IsValid)
            {
                using (SoruBankasiDbContext db = new SoruBankasiDbContext())
                {
                    if (db.Ders.SingleOrDefault(x => x.DersAdi.Equals(model.DersAdi) && x.ID != model.ID) == null)
                    {
                        db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.Message = $"<div class='alert alert-success'><strong>Başarılı!</strong> Ders Başarıyla Güncellendi... </div>";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = $"<div class='alert alert-danger'><strong>Hata!</strong> Bu ders adı zaten kullanılıyor... </div>";
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public string Delete(int ID)
        {
            string message = string.Empty;
            using (SoruBankasiDbContext db = new SoruBankasiDbContext())
            {
                Ders ders = db.Ders.SingleOrDefault(x => x.ID.Equals(ID));
                if (ders != null)
                {
                    db.Ders.Remove(ders);
                    db.SaveChanges();
                    message = JsonConvert.SerializeObject(new { durum = "OK", mesaj = "Ders Silindi" });
                }
                else
                {
                    message = JsonConvert.SerializeObject(new { durum = "No", mesaj = "Ders Silinemedi" });
                }
            }
            return message;
        }
    }
}