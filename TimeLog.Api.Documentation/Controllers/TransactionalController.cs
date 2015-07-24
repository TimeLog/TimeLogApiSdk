using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeLog.Api.Documentation.Controllers
{
    using TimeLog.Api.Documentation.Models;

    public class TransactionalController : Controller
    {
        // GET: Transaction
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

        public ActionResult Services()
        {
            return View(TransactionalManager.Instance.GetServices());
        }

        public ActionResult Service(string id)
        {
            return View(TransactionalManager.Instance.GetService(id));
        }

        public ActionResult ErrorCodes()
        {
            return View();
        }
    }
}