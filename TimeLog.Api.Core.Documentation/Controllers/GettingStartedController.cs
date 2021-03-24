using Microsoft.AspNetCore.Mvc;

namespace TimeLog.Api.Core.Documentation.Controllers
{
    public class GettingStartedController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}