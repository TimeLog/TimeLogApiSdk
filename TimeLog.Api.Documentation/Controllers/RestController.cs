using System.Web.Mvc;
using TimeLog.Api.Documentation.Models;

namespace TimeLog.Api.Documentation.Controllers
{
    public class RestController : Controller
    {
        #region Internal and Private Implementations

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Method(string id)
        {
            return View(RestManager.Instance.GetMethod(id));
        }

        public ActionResult Service(string id)
        {
            return View(RestManager.Instance.GetService(id));
        }

        public ActionResult Services()
        {
            return View(RestManager.Instance.GetServices());
        }

        #endregion
    }
}