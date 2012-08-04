using System.Web.Mvc;

namespace KelliPokerPlanning.Controllers
{
    public class EstimateController : Controller
    {

        public ActionResult Index(int id, string joinCode)
        {
            // id is the planning session id
            return Content("Hi!");
        }

    }
}
