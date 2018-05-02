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
    }
}