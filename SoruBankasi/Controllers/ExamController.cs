using SoruBankasi.Infrastructure;
using SoruBankasi.Models;
using SoruBankasi.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SoruBankasi.Controllers
{
    [SelectedTab("Exam")]
    public class ExamController : BaseController
    {
        public ActionResult Prepare()
        {
            SoruBankasiDbContext db = new SoruBankasiDbContext();
            List<Ders> lst = db.Ders.ToList();
            if (!User.IsInRole("Admin"))
            {
                int[] userLessons = db.KullaniciDers.Where(x => x.Kullanici.KullaniciAdi.Equals(User.Identity.Name)).Select(x => x.DersID).ToArray();

                lst = db.Ders.Where(x => userLessons.Contains(x.ID)).ToList();
            }
            ViewBag.Lessons = lst;
            return View();
        }



        [HttpPost]
        public ActionResult Prepare(SinavHazirlamaViewModel model)
        {
            SoruBankasiDbContext db = new SoruBankasiDbContext();

            List<Soru> sorular = new List<Soru>(); //db.Soru.ToList().Where(x => x.SoruDonemID.Equals(model.Donem) && x.Konu.Ders.ID.Equals(model.Ders)).ToList();

            foreach (var item in db.Soru.ToList().Where(x => x.SoruDonemID.Equals(model.Donem) && x.Konu.Ders.ID.Equals(model.Ders)).ToList())
            {
                int puan = 0;
                if (item.SoruTipi.SoruTipAdi.Equals("Klasik"))
                {
                    puan = model.KlasikSoruPuan;
                }
                else if (item.SoruTipi.SoruTipAdi.Equals("Test"))
                {
                    puan = model.TestSoruPuan;
                }
                else if (item.SoruTipi.SoruTipAdi.Equals("Bosluk Doldurma"))
                {
                    puan = model.BoslukSoruPuan;
                }
                item.Puan = puan;
                sorular.Add(item);

            }
            List<Soru> lst = new List<Soru>();

            lst.AddRange(sorular.Where(x => x.SoruTipi.SoruTipAdi.Equals("Klasik")).OrderBy(x => Guid.NewGuid()).Take(model.KlasikSoruAdet));
            lst.AddRange(sorular.Where(x => x.SoruTipi.SoruTipAdi.Equals("Test")).OrderBy(x => Guid.NewGuid()).Take(model.TestSoruAdet));
            lst.AddRange(sorular.Where(x => x.SoruTipi.SoruTipAdi.Equals("Bosluk Doldurma")).OrderBy(x => Guid.NewGuid()).Take(model.BoslukSoruAdet));

            TempData["list"] = lst;
            return RedirectToAction("Show");

        }

        [HttpGet]
        public ActionResult Show()
        {
            List<Soru> model = (List<Soru>)TempData["list"];
            return View(model);
        }

    }
}