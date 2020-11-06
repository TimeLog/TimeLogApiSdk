using System.Web.Mvc;

namespace TimeLog.Api.Documentation.Controllers
{
    using TimeLog.Api.Documentation.Models;

    public class ReportingController : Controller
    {
        // GET: Reporting
        public ActionResult Index()
        {
            return this.View();
        }
        public ActionResult GettingStarted()
        {
            return this.View();
        }

        public ActionResult Security()
        {
            return this.View();
        }

        public ActionResult Methods()
        {
            return this.View(ReportingManager.Instance.GetMethods());
        }

        public ActionResult Method(string id)
        {
            return this.View(ReportingManager.Instance.GetMethod(id));
        }

        public ActionResult EnumerableTypes()
        {
            return this.View();
        }

        public ActionResult PowerBi()
        {
            return this.View();
        }

        public ActionResult SynchronizingData()
        {
            return this.View();
        }
    }
}