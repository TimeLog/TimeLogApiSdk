using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeLog.Api.Documentation.Controllers
{
    using TimeLog.Api.Documentation.Models;

    public class ReportingController : Controller
    {
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
            return this.View(ReportingManager.Instance.GetMethods());
        }

        public ActionResult Method(string id)
        {
            return this.View(ReportingManager.Instance.GetMethod(id));
        }

        public ActionResult EnumerableTypes()
        {
            return View();
        }
    }
}