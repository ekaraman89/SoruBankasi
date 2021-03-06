﻿using SoruBankasi.Helpers;
using SoruBankasi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SoruBankasi.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Kullanici model, string ReturnUrl)
        {
            ModelState.Remove("KullaniciAdi");
            if (ModelState.IsValid)
            {
                using (SoruBankasiDbContext db = new SoruBankasiDbContext())
                {
                    Kullanici kul = db.Kullanici.SingleOrDefault(x => x.Mail == model.Mail && x.Sifre == model.Sifre);
                    if (kul != null)
                    {
                        FormsAuthentication.SetAuthCookie(kul.KullaniciAdi, true);

                        if (!string.IsNullOrWhiteSpace(ReturnUrl))
                            return Redirect(ReturnUrl);
                        return RedirectToRoute("Home");
                    }
                }
                ViewBag.Message = "<div class='alert alert-danger display'><button class='close' data-close='alert'></button><span> Böyle bir kullanıcı yoktur... </span></div>";
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Login");
        }

        [Authorize]
        public ActionResult ProfileDetail()
        {
            using (SoruBankasiDbContext db = new SoruBankasiDbContext())
            {
                if (TempData["Message"] != null)
                    ViewBag.Message = TempData["Message"].ToString();

                return View(db.Kullanici.SingleOrDefault(x => x.KullaniciAdi.Equals(User.Identity.Name)));
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangeMail(Kullanici model)
        {
            ModelState.Remove("Sifre");
            ModelState.Remove("KullaniciAdi");
            if (ModelState.IsValid)
            {
                using (SoruBankasiDbContext db = new SoruBankasiDbContext())
                {
                    List<Kullanici> lst = db.Kullanici.ToList();

                    Kullanici kullanici = lst.SingleOrDefault(x => x.KullaniciAdi.Equals(User.Identity.Name));
                    if (lst.SingleOrDefault(x => x.Mail.Equals(model.Mail) && x.ID != kullanici.ID) == null)
                    {
                        kullanici.Mail = model.Mail;
                        db.Entry(kullanici).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        TempData["Message"] = $"<div class='alert alert-success'><strong>Başarılı!</strong> Bilgileriniz Başarıyla Güncellendi... </div>";
                    }
                    else
                    {
                        TempData["Message"] = $"<div class='alert alert-danger'><strong>Hata!</strong> Mail adresi kullanılıyor... </div>";
                    }
                }
            }
            return RedirectToRoute("ProfileDetail");
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePhoto(HttpPostedFileBase image)
        {
            if (image != null)
            {
                using (SoruBankasiDbContext db = new SoruBankasiDbContext())
                {
                    Kullanici kullanici = db.Kullanici.SingleOrDefault(x => x.KullaniciAdi.Equals(User.Identity.Name));
                    kullanici.Foto = FileUpload.FileName(image, FileUpload.UploadFolder.Profile);
                    db.Entry(kullanici).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = $"<div class='alert alert-success'><strong>Başarılı!</strong> Profil Fotoğrafınız Başarıyla Güncellendi... </div>";
                }
            }
            return Redirect("Profil#tab_1_2");
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(Kullanici model)
        {
            ModelState.Remove("KullaniciAdi");
            ModelState.Remove("Adi");
            ModelState.Remove("Soyadi");
            ModelState.Remove("Mail");
            if (ModelState.IsValid)
            {
                using (SoruBankasiDbContext db = new SoruBankasiDbContext())
                {
                    Kullanici kullanici = db.Kullanici.SingleOrDefault(x => x.KullaniciAdi.Equals(User.Identity.Name));

                    if (kullanici.Sifre.Equals(model.Sifre))
                    {
                        kullanici.Sifre = model.YeniSifre;
                        db.Entry(kullanici).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        TempData["Message"] = $"<div class='alert alert-success'><strong>Başarılı!</strong> Profil Fotoğrafınız Başarıyla Güncellendi... </div>";
                    }
                    else
                    {
                        TempData["Message"] = $"<div class='alert alert-danger'><strong>Hata!</strong> MEvcut Şifreniz yanlış... </div>";
                    }
                }
            }
            else
            {
                TempData["Message"] = $"<div class='alert alert-danger'><strong>Hata!</strong> Şifre Bilgilerinde hata oluştu... </div>";
            }
            return Redirect("Profil#tab_1_3");
        }


    }
}