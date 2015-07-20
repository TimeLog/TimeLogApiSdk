using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeLog.Api.Documentation.Controllers
{
    public class FileServicesController : Controller
    {
        // GET: FileServices
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
            return this.View();
        }
    }
}