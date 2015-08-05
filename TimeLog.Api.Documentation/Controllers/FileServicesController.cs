namespace TimeLog.Api.Documentation.Controllers
{
    using System.Web.Mvc;

    public class FileServicesController : Controller
    {
        // GET: FileServices
        public ActionResult Index()
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

        public ActionResult InvoiceLineExport()
        {
            return this.View();
        }
    }
}