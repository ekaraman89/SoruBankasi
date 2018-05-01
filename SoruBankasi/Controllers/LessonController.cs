using SoruBankasi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoruBankasi.Controllers
{
    [Authorize(Roles ="admin")]
    public class LessonController : Controller
    {
        public ActionResult Index()
        {
            return View();
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
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Ders model)
        {
            return View();
        }

        public string Delete(int ID)
        {

            return "";
        }
    }
}