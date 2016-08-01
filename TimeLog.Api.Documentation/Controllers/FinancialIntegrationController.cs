namespace TimeLog.Api.Documentation.Controllers
{
    using System.Web.Mvc;

    public class FinancialIntegrationController : Controller
    {
        // GET: FinancialIntegration
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DynamicsNav()
        {
            return View("Index");
        }

        public ActionResult DynamicsAx()
        {
            return View("Index");
        }

        public ActionResult ExactOnline()
        {
            return View("ExactOnline");
        }

        public ActionResult VismaAdministration()
        {
            return View("Index");
        }

        public ActionResult Economic()
        {
            return View("e-conomic");
        }

        public ActionResult Flows()
        {
            return View("Index");
        }

        public ActionResult Tests()
        {
            return View("Index");
        }

        public ActionResult Install()
        {
            return View("Index");
        }
    }
}