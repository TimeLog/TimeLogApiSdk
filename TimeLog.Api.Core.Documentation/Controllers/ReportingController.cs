using Microsoft.AspNetCore.Mvc;
using TimeLog.Api.Core.Documentation.Models;

namespace TimeLog.Api.Core.Documentation.Controllers
{
    public class ReportingController : Controller
    {
        private readonly IReportingManager _reportingManager;

        public ReportingController(IReportingManager reportingManager)
        {
            _reportingManager = reportingManager;
        }
        
        // GET: Reporting
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GettingStarted()
        {
            return View();
        }

        public ActionResult Security()
        {
            return View();
        }

        public ActionResult Methods()
        {
            return View(_reportingManager.GetMethods());
        }

        public ActionResult Method(string id)
        {
            return View(_reportingManager.GetMethod(id));
        }

        public ActionResult EnumerableTypes()
        {
            return View();
        }

        public ActionResult PowerBi()
        {
            return View();
        }

        public ActionResult SynchronizingData()
        {
            return View();
        }
    }
}