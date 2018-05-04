using SoruBankasi.Infrastructure;
using SoruBankasi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SoruBankasi.Controllers
{
    [SelectedTab("Home")]
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            List<KullaniciDers> ders;
            SoruBankasiDbContext db = new SoruBankasiDbContext();

            ders = db.KullaniciDers.Where(x => x.Kullanici.KullaniciAdi.Equals(User.Identity.Name)).ToList();

            return View(ders);
        }


    }
}