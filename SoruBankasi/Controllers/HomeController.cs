using SoruBankasi.Infrastructure;
using System.Web.Mvc;

namespace SoruBankasi.Controllers
{
    [Authorize]
    [SelectedTab("Home")]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


    }
}