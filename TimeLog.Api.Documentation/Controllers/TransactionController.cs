using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeLog.Api.Documentation.Controllers
{
    public class TransactionController : Controller
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
            return View();
        }

        public ActionResult ErrorCodes()
        {
            return View();
        }
    }
}