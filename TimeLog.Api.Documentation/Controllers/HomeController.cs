using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeLog.Api.Documentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Contact()
        {
            return this.View();
        }

        public ActionResult Restrictions()
        {
            return this.View();
        }

        public ActionResult DataModel()
        {
            return this.View();
        }
    }
}