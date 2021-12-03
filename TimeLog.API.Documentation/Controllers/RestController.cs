using Microsoft.AspNetCore.Mvc;
using TimeLog.Api.Documentation.Models;

namespace TimeLog.Api.Documentation.Controllers;

public class RestController : Controller
{
    private readonly IRestManager restManager;

    public RestController(IRestManager restManager)
    {
        this.restManager = restManager;
    }

    public ActionResult Index()
    {
        return View();
    }

    public ActionResult Method(string id)
    {
        return View(restManager.GetMethod(id));
    }

    public ActionResult Service(string id)
    {
        return View(restManager.GetService(id));
    }

    public ActionResult Services()
    {
        return View(restManager.GetServices());
    }
}