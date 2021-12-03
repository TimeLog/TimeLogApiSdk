using Microsoft.AspNetCore.Mvc;
using TimeLog.Api.Documentation.Models;

namespace TimeLog.Api.Documentation.Controllers;

public class TransactionalController : Controller
{
    private readonly ITransactionalManager transactionalManager;

    public TransactionalController(ITransactionalManager transactionalManager)
    {
        this.transactionalManager = transactionalManager;
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
        return View(transactionalManager.GetServices());
    }

    public ActionResult Service(string id)
    {
        return View(transactionalManager.GetService(id));
    }

    public ActionResult Method(string id)
    {
        return View(transactionalManager.GetMethod(id));
    }
}