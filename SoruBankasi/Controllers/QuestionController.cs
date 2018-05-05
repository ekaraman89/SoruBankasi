using Newtonsoft.Json;
using SoruBankasi.Infrastructure;
using SoruBankasi.Models;
using SoruBankasi.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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

            return JsonConvert.SerializeObject(lst, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
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
            //to do
            return "";
        }
    }
}