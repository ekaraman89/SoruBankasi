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
    [SelectedTab("Topic")]
    public class TopicController : Controller
    {
        public ActionResult Index()
        {
            SoruBankasiDbContext db = new SoruBankasiDbContext();

            List<Konu> lst = db.Konu.ToList();
            if (!User.IsInRole("Admin"))
            {
                int userID = db.Kullanici.Single(x => x.KullaniciAdi.Equals(User.Identity.Name)).ID;
                int[] userLessons = db.KullaniciDers.Where(x => x.KullaniciID.Equals(userID)).Select(x => x.DersID).ToArray();
                lst = lst.Where(x => userLessons.Contains(x.DersID)).ToList();
            }
            return View(lst);
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

        [HttpPost]
        public string Delete(int ID)
        {
            string message = string.Empty;
            using (SoruBankasiDbContext db = new SoruBankasiDbContext())
            {
                List<KonuSoruDonemi> lst = db.KonuSoruDonemi.Where(x => x.KonuID.Equals(ID)).ToList();
                db.KonuSoruDonemi.RemoveRange(lst);

                Konu konu = db.Konu.SingleOrDefault(x => x.ID.Equals(ID));
                if (konu != null)
                {
                    db.Konu.Remove(konu);
                    db.SaveChanges();
                    message = JsonConvert.SerializeObject(new { durum = "OK", mesaj = "Konu Silindi" });
                }
                else
                {
                    message = JsonConvert.SerializeObject(new { durum = "No", mesaj = "Konu Silinemedi" });
                }
            }
            return message;
        }
    }
}