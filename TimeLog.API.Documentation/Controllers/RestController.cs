using Microsoft.AspNetCore.Mvc;
using TimeLog.Api.Documentation.Models;

namespace TimeLog.Api.Documentation.Controllers;

public class RestController : Controller
{
    private readonly IRestManager _restManager;

    public RestController(IRestManager restManager)
    {
        _restManager = restManager;
    }

    public ActionResult Index()
    {
        return View();
    }

    public ActionResult Method(string id)
    {
        return View(_restManager.GetMethod(id));
    }

    public ActionResult Service(string id)
    {
        return View(_restManager.GetService(id));
    }

    public ActionResult Services()
    {
        return View(_restManager.GetServices());
    }

    public ActionResult Swagger()
    {
        return View();
    }
}