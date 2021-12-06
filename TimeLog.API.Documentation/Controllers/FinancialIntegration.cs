using Microsoft.AspNetCore.Mvc;

namespace TimeLog.Api.Documentation.Controllers;

public class FinancialIntegration : Controller
{
    public ActionResult Index()
    {
        return View();
    }

    public ActionResult PreinstallDocumentForIt()
    {
        return View();
    }

    public ActionResult PreinstallDocumentForNAVPartner()
    {
        return View();
    }

    public ActionResult PreinstallDocumentForCustomer()
    {
        return View();
    }

    public ActionResult NAVInstallDocument()
    {
        return View();
    }

    public ActionResult DynamicsNav()
    {
        return View("DynamicsNav");
    }

    public ActionResult MiddlewareSecurity()
    {
        return View("MiddlewareSecurity");
    }

    public ActionResult DynamicsAx()
    {
        return View("Index");
    }

    public ActionResult ExactOnline()
    {
        return View("ExactOnline");
    }

    public ActionResult Fortnox()
    {
        return View("Fortnox");
    }

    public ActionResult BjornLunden()
    {
        return View("BjornLunden");
    }

    public ActionResult Economic()
    {
        return View("e-conomic");
    }

    public ActionResult Flows()
    {
        return View("Index");
    }

    public ActionResult Tests()
    {
        return View("Index");
    }

    public ActionResult ConfigurationFile()
    {
        return View();
    }

    public ActionResult Dinero()
    {
        return View("Dinero");
    }

    public ActionResult Uniconta()
    {
        return View("Uniconta");
    }
}