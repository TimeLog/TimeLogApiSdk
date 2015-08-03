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
            return this.View();
        }
        public ActionResult GettingStarted()
        {
            return this.View();
        }

        public ActionResult ExternalSystems()
        {
            return this.View();
        }   

        public ActionResult Security()
        {
            return this.View();
        }

        public ActionResult Services()
        {
            return View(TransactionalManager.Instance.GetServices());
        }

        public ActionResult Service(string id)
        {
            return View(TransactionalManager.Instance.GetService(id));
        }

        public ActionResult Method(string id)
        {
            return View(TransactionalManager.Instance.GetMethod(id));
        }

        public ActionResult ErrorCodes()
        {
            return View();
        }
    }
}