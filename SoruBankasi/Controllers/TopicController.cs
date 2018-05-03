using SoruBankasi.Infrastructure;
using SoruBankasi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoruBankasi.Controllers
{
    [SelectedTab("Topic")]
    public class TopicController : Controller
    {
        public ActionResult Index()
        {
            SoruBankasiDbContext db = new SoruBankasiDbContext();
            return View(db.Konu.ToList());
        }

        public ActionResult Add()
        {
            GetUserLessons();
            GetQuestionPeriod();

            return View();
        }

        private void GetUserLessons()
        {
            SoruBankasiDbContext db = new SoruBankasiDbContext();

            int[] userLessons = db.KullaniciDers.Where(x => x.Kullanici.KullaniciAdi.Equals(User.Identity.Name)).Select(x => x.DersID).ToArray();

            ViewBag.Lessons = db.Ders.Where(x => userLessons.Contains(x.ID));
        }

        private void GetQuestionPeriod()
        {
            SoruBankasiDbContext db = new SoruBankasiDbContext();

            ViewBag.Period = db.SoruDonemi.ToList();

        }

        [HttpPost]
        public ActionResult Add(Konu model, int[] periods)
        {
            SoruBankasiDbContext db = new SoruBankasiDbContext();
            if (ModelState.IsValid)
            {
                db.Konu.Add(model);
                db.SaveChanges();

                List<KonuSoruDonemi> lst = new List<KonuSoruDonemi>();
                foreach (int item in periods)
                {
                    KonuSoruDonemi konuSoruDonemi = new KonuSoruDonemi
                    {
                        KonuID = model.ID,
                        SoruDonemID = item
                    };
                    lst.Add(konuSoruDonemi);
                }
                db.KonuSoruDonemi.AddRange(lst);
                db.SaveChanges();
                ViewBag.Message = $"<div class='alert alert-success'><strong>Başarılı!</strong> Konu Başarıyla Eklendi... </div>";
                ModelState.Clear();
            }

            GetQuestionPeriod();
            GetUserLessons();

            return View(model);
        }
    }
}