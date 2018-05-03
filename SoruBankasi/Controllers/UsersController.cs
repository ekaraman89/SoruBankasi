using Newtonsoft.Json;
using SoruBankasi.Infrastructure;
using SoruBankasi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SoruBankasi.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("Users")]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            using (SoruBankasiDbContext db = new SoruBankasiDbContext())
            {
                return View(db.Kullanici.ToList());
            }
        }

        public ActionResult Add()
        {
            using (SoruBankasiDbContext db = new SoruBankasiDbContext())
            {
                ViewBag.Lessons = db.Ders.ToList();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Add(Kullanici model, int[] lessons)
        {
            if (ModelState.IsValid)
            {
                using (SoruBankasiDbContext db = new SoruBankasiDbContext())
                {
                    List<Kullanici> lst = db.Kullanici.ToList();

                    if (lst.SingleOrDefault(x => x.Mail.Equals(model.Mail)) == null)
                    {
                        if (lst.SingleOrDefault(x => x.KullaniciAdi.Equals(model.KullaniciAdi)) == null)
                        {
                            db.Entry(model).State = System.Data.Entity.EntityState.Added;
                            foreach (int item in lessons)
                            {
                                KullaniciDers kulDers = new KullaniciDers { DersID = item, KullaniciID = model.ID };
                                db.Entry(kulDers).State = System.Data.Entity.EntityState.Added;
                            }
                            db.SaveChanges();
                            ViewBag.Message = $"<div class='alert alert-success'><strong>Başarılı!</strong> Kullanıcı Başarıyla Eklendi... </div>";
                            ModelState.Clear();
                        }
                        else
                        {
                            ViewBag.Message = $"<div class='alert alert-danger'><strong>Hata!</strong> Bu kullanıcı adı zaten kullanılıyor... </div>";
                        }
                    }
                    else
                    {
                        ViewBag.Message = $"<div class='alert alert-danger'><strong>Hata!</strong> Bu mail zaten kullanılıyor... </div>";
                    }
                }
            }
            else
            {
                using (SoruBankasiDbContext db = new SoruBankasiDbContext())
                {
                    ViewBag.Lessons = db.Ders.ToList();
                }
            }
            return View(model);
        }

        public ActionResult Edit(int ID)
        {
            using (SoruBankasiDbContext db = new SoruBankasiDbContext())
            {
                Kullanici kullanici = db.Kullanici.SingleOrDefault(x => x.ID.Equals(ID));
                if (kullanici != null)
                {
                    return View(kullanici);
                }
            }
            return RedirectToRoute("Users");
        }

        [HttpPost]
        public ActionResult Edit(Kullanici model)
        {
            if (ModelState.IsValid)
            {
                using (SoruBankasiDbContext db = new SoruBankasiDbContext())
                {
                    List<Kullanici> lst = db.Kullanici.ToList();

                    if (lst.SingleOrDefault(x => x.Mail.Equals(model.Mail) && x.ID != model.ID) == null)
                    {
                        if (lst.SingleOrDefault(x => x.KullaniciAdi.Equals(model.KullaniciAdi) && x.ID != model.ID) == null)
                        {
                            Kullanici kullanici = lst.SingleOrDefault(x => x.ID.Equals(model.ID));
                            kullanici.KullaniciAdi = model.KullaniciAdi;
                            kullanici.Adi = model.Adi;
                            kullanici.Soyadi = model.Soyadi;
                            kullanici.Sifre = model.Sifre;
                            kullanici.Mail = model.Mail;

                            db.SaveChanges();
                            ViewBag.Message = $"<div class='alert alert-success'><strong>Başarılı!</strong> Kullanıcı Başarıyla Güncellendi... </div>";
                            ModelState.Clear();
                        }
                        else
                        {
                            ViewBag.Message = $"<div class='alert alert-danger'><strong>Hata!</strong> Bu kullanıcı adı zaten kullanılıyor... </div>";
                        }
                    }
                    else
                    {
                        ViewBag.Message = $"<div class='alert alert-danger'><strong>Hata!</strong> Bu mail zaten kullanılıyor... </div>";
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
                Kullanici kullanici = db.Kullanici.SingleOrDefault(x => x.ID.Equals(ID) && !x.KullaniciAdi.Equals(User.Identity.Name));
                if (kullanici != null)
                {
                    db.Kullanici.Remove(kullanici);
                    db.SaveChanges();
                    message = JsonConvert.SerializeObject(new { durum = "OK", mesaj = "Kullanıcı Silindi" });
                }
                else
                {
                    message = JsonConvert.SerializeObject(new { durum = "No", mesaj = "Kullanıcı Silinemedi" });
                }
            }
            return message;
        }
    }
}