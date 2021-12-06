using Microsoft.AspNetCore.Mvc;
using TimeLog.Api.Documentation.Models;

namespace TimeLog.Api.Documentation.Controllers;

public class ReportingController : Controller
{
    private readonly IReportingManager reportingManager;

    public ReportingController(IReportingManager reportingManager)
    {
        this.reportingManager = reportingManager;
    }

    // GET: Reporting
    public ActionResult Index()
    {
        return View();
    }

    public ActionResult GettingStarted()
    {
        return View();
    }

    public ActionResult Methods()
    {
        return View(reportingManager.GetMethods());
    }

    public ActionResult Method(string id)
    {
        return View(reportingManager.GetMethod(id));
    }

    public ActionResult EnumerableTypes()
    {
        return View();
    }

    public ActionResult PowerBi()
    {
        return View();
    }

    public ActionResult SynchronizingData()
    {
        return View();
    }
}