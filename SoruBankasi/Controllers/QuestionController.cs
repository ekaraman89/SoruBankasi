using Newtonsoft.Json;
using SoruBankasi.Infrastructure;
using SoruBankasi.Models;
using SoruBankasi.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static SoruBankasi.Models.ViewModel.SoruEkleJsonModel;

namespace SoruBankasi.Controllers
{
    [SelectedTab("Questions")]
    public class QuestionController : BaseController
    {
        public ActionResult Index()
        {
            SoruBankasiDbContext db = new SoruBankasiDbContext();
            List<Soru> questions = db.Soru.ToList();
            return View(questions);
        }

        public ActionResult Add()
        {
            SoruBankasiDbContext db = new SoruBankasiDbContext();
            List<Ders> lst = db.Ders.ToList();
            if (!User.IsInRole("Admin"))
            {
                int[] userLessons = db.KullaniciDers.Where(x => x.Kullanici.KullaniciAdi.Equals(User.Identity.Name)).Select(x => x.DersID).ToArray();

                lst = db.Ders.Where(x => userLessons.Contains(x.ID)).ToList();
            }
            return View(lst);
        }

        [HttpPost]
        public string GetLessonsTopic(int ID)
        {
            SoruBankasiDbContext db = new SoruBankasiDbContext();
            List<Konu> lst = db.Konu.Where(x => x.DersID.Equals(ID)).ToList();

            List<SoruEkleJsonModel> jsonLst = new List<SoruEkleJsonModel>();

            foreach (var ders in lst)
            {
                SoruEkleJsonModel model = new SoruEkleJsonModel();
                model.KonuID = ders.ID;
                model.KonuAdi = ders.KonuAdi;
                foreach (var item in ders.KonuSoruDonemi)
                {
                    DonemJson donemJson = new DonemJson();
                    donemJson.DonemID = item.SoruDonemi.ID;
                    donemJson.DonemAdi = item.SoruDonemi.SoruDonemAdi;
                    model.Donem.Add(donemJson);
                }
                jsonLst.Add(model);
                //model.DersID = ders.DersID;
                // model.DersAdi = ders.Ders.DersAdi;
                //foreach (var konu in ders.KonuSoruDonemi)
                //{
                //    KonuJson konuJson = new KonuJson();
                //    konuJson.KonuID = konu.KonuID;
                //    konuJson.KonuAdi = konu.Konu.KonuAdi;

                //    foreach (var item in konu.SoruDonemi.KonuSoruDonemi)
                //    {
                //        DonemJson donemJson = new DonemJson();
                //        donemJson.DonemID = item.SoruDonemi.ID;
                //        donemJson.DonemAdi = item.SoruDonemi.SoruDonemAdi;
                //        konuJson.Donem.Add(donemJson);
                //    }
                //    model.Konular.Add(konuJson);
                //    jsonLst.Add(model);
                //}
            }
            return JsonConvert.SerializeObject(jsonLst);
            //return JsonConvert.SerializeObject(lst, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
        }


        [HttpPost]
        public string Delete(int ID)
        {
            string message = string.Empty;
            using (SoruBankasiDbContext db = new SoruBankasiDbContext())
            {
                List<Cevaplar> lst = db.Cevaplar.Where(x => x.SoruID.Equals(ID)).ToList();
                db.Cevaplar.RemoveRange(lst);

                Soru soru = db.Soru.SingleOrDefault(x => x.ID.Equals(ID));
                if (soru != null)
                {
                    db.Soru.Remove(soru);
                    db.SaveChanges();
                    message = JsonConvert.SerializeObject(new { durum = "OK", mesaj = "Soru Silindi" });
                }
                else
                {
                    message = JsonConvert.SerializeObject(new { durum = "No", mesaj = "Soru Silinemedi" });
                }
            }
            return message;
        }

        [HttpPost]
        public string AddQuestion(SoruEkleViewModel model)
        {
            Soru soru = new Soru
            {
                Sorular = model.Soru,
                SoruTipID = model.SoruTipiID,
                SoruDonemID = model.DonemID,
                KonuID = model.KonuID
            };
            SoruBankasiDbContext db = new SoruBankasiDbContext();
            db.Soru.Add(soru);
            db.SaveChanges();
            List<Cevaplar> lst = new List<Cevaplar>();
            foreach (var item in model.Cevaplar)
            {
                Cevaplar cevap = new Cevaplar
                {
                    SoruID = soru.ID,
                    Cevap = item.CevapIcerik,
                    DogruMu = item.Val
                };
                lst.Add(cevap);
            }
            db.Cevaplar.AddRange(lst);
            db.SaveChanges();

            return JsonConvert.SerializeObject(new { durum = "OK", mesaj = "Soru başarıyla eklendi" });
        }
    }
}