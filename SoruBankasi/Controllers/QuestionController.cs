using SoruBankasi.Infrastructure;
using System.Web.Mvc;

namespace SoruBankasi.Controllers
{
    [SelectedTab("Topic")]
    public class QuestionController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}