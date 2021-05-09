using Microsoft.AspNetCore.Mvc;
using TimeLog.Api.Core.Documentation.Models;

namespace TimeLog.Api.Core.Documentation.Controllers
{
    public class TransactionalController : Controller
    {
        private readonly ITransactionalManager _transactionalManager;

        public TransactionalController(ITransactionalManager transactionalManager)
        {
            _transactionalManager = transactionalManager;
        }
        
        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        public ActionResult GettingStarted()
        {
            return View();
        }

        public ActionResult ExternalSystems()
        {
            return View();
        }   

        public ActionResult Security()
        {
            return View();
        }
        
        public ActionResult Services()
        {
            return View(_transactionalManager.GetServices());
        }

        public ActionResult Service(string id)
        {
            return View(_transactionalManager.GetService(id));
        }

        public ActionResult Method(string id)
        {
            return View(_transactionalManager.GetMethod(id));
        }
    }
}