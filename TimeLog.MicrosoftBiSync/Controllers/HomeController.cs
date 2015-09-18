namespace TimeLog.MicrosoftBiSync.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.Mvc;
    using System.Xml;

    using Microsoft.IdentityModel.Clients.ActiveDirectory;

    using Models;

    using TimeLog.Library.Configuration;
    using TimeLog.MicrosoftBiSync.Models.PowerBi;
    using TimeLog.ReportingApi.SDK;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (AzureAuthenticationHelper.IsAuthenticated())
            {
                return this.RedirectToAction("Transfer");
            }

            var values = this.Request.QueryString["code"];
            if (values != null)
            {
                string code = values;
                AuthenticationContext ac = new AuthenticationContext(PersonalConfigurationManager.AppSettings["AzureAuthorityUri"]);
                ClientCredential cc = new ClientCredential(PersonalConfigurationManager.AppSettings["AzureClientId"], PersonalConfigurationManager.AppSettings["AzureKey"]);
                AuthenticationResult ar = ac.AcquireTokenByAuthorizationCode(code, new Uri(PersonalConfigurationManager.AppSettings["AzureRedirectUrl"]), cc);

                AzureAuthenticationHelper.SetSession(ar);
                return this.RedirectToAction("Transfer");
            }

            return this.View();
        }

        public ActionResult Login()
        {
            if (AzureAuthenticationHelper.IsAuthenticated())
            {
                return this.RedirectToAction("Transfer");
            }

            return this.Redirect(AzureAuthenticationHelper.GetAuthString());
        }

        public ActionResult Transfer()
        {
            if (AzureAuthenticationHelper.IsAuthenticated())
            {
                var helper = new PowerBiHelper(AzureAuthenticationHelper.GetSession());
                this.ViewBag.DatasetStatus = helper.IsDefaultDatasetAvailable() ? "detected" : "created";
                helper.CreateDefaultDataset();

                // helper.AddRow("6ac4bc17-a85d-4f03-bc5a-e73460568d3c", "Customers");
                return this.View();
            }

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Transfer(string reportingUrl, string reportingCode, string reportingId, string reportingPassword)
        {
            ServiceHandler.Instance.OverwriteServiceUrl(reportingUrl);
            AzureAuthenticationHelper.SetReportingCredentials(new ReportingCredentials
                                                                  {
                                                                      ApiId = reportingId,
                                                                      SiteCode = reportingCode,
                                                                      Url = reportingUrl,
                                                                      ApiPassword = reportingPassword
                                                                  });

            return this.RedirectToAction("InProgress");
        }

        public ActionResult InProgress()
        {
            if (AzureAuthenticationHelper.IsAuthenticated())
            {
                return this.View();
            }

            return this.RedirectToAction("Index");
        }

        public ActionResult TransferCustomers()
        {
                //return this.Json("Yay", JsonRequestBehavior.AllowGet);
            var credentials = AzureAuthenticationHelper.GetReportingCredentials();

            if (ServiceHandler.Instance.TryAuthenticate(
                credentials.SiteCode,
                credentials.ApiId,
                credentials.ApiPassword))
            {
                var helper = new PowerBiHelper(AzureAuthenticationHelper.GetSession());
                var customersRaw = ServiceHandler.Instance.Client.GetCustomersRaw(
                    credentials.SiteCode,
                    credentials.ApiId,
                    credentials.ApiPassword,
                    Customer.All,
                    1,
                    AccountManager.All,
                    string.Empty);

                if (customersRaw.OwnerDocument != null)
                {
                    var namespaceManager = new XmlNamespaceManager(customersRaw.OwnerDocument.NameTable);
                    namespaceManager.AddNamespace("tlp", "http://www.timelog.com/XML/Schema/tlp/v4_4");
                    var customers = customersRaw.SelectNodes("tlp:Customer", namespaceManager);

                    if (customers != null)
                    {
                        var tableConstructed = false;
                        var rowsList = new List<string>();
                        foreach (XmlNode customer in customers)
                        {
                            if (!tableConstructed)
                            {
                                helper.DeleteRows(helper.GetDefaultDatasetId(), "Customers");
                                var tableSchemaJson = helper.BuildTableSchemaJson("Customers", typeof(Customer));
                                helper.UpdateTableSchema(helper.GetDefaultDatasetId(), "Customers", tableSchemaJson);
                                tableConstructed = true;
                            }

                            rowsList.Add(helper.BuildTableRowJson(new Customer(customer, namespaceManager)));
                        }

                        helper.AddRows(helper.GetDefaultDatasetId(), "Customers", rowsList, 100);
                    }
                }

                return this.Json("Yay", JsonRequestBehavior.AllowGet);
            }

            return this.Json("Failed", JsonRequestBehavior.AllowGet);
        }

        public ActionResult TransferContacts()
        {
                return this.Json("Yay", JsonRequestBehavior.AllowGet);
            var credentials = AzureAuthenticationHelper.GetReportingCredentials();

            if (ServiceHandler.Instance.TryAuthenticate(
                credentials.SiteCode,
                credentials.ApiId,
                credentials.ApiPassword))
            {
                var helper = new PowerBiHelper(AzureAuthenticationHelper.GetSession());
                var contactsRaw = ServiceHandler.Instance.Client.GetContactsRaw(
                    credentials.SiteCode,
                    credentials.ApiId,
                    credentials.ApiPassword,
                    Customer.All,
                    Contact.All);

                if (contactsRaw.OwnerDocument != null)
                {
                    var namespaceManager = new XmlNamespaceManager(contactsRaw.OwnerDocument.NameTable);
                    namespaceManager.AddNamespace("tlp", "http://www.timelog.com/XML/Schema/tlp/v4_4");
                    var contacts = contactsRaw.SelectNodes("tlp:Contact", namespaceManager);

                    if (contacts != null)
                    {
                        var tableConstructed = false;
                        var rowsList = new List<string>();
                        foreach (XmlNode contact in contacts)
                        {
                            if (!tableConstructed)
                            {
                                helper.DeleteRows(helper.GetDefaultDatasetId(), "Contacts");
                                var tableSchemaJson = helper.BuildTableSchemaJson("Contacts", typeof(Contact));
                                helper.UpdateTableSchema(helper.GetDefaultDatasetId(), "Contacts", tableSchemaJson);
                                tableConstructed = true;
                            }

                            rowsList.Add(helper.BuildTableRowJson(new Contact(contact, namespaceManager)));
                        }

                        helper.AddRows(helper.GetDefaultDatasetId(), "Contacts", rowsList, 10);
                    }
                }

                return this.Json("Yay", JsonRequestBehavior.AllowGet);
            }

            return this.Json("Failed", JsonRequestBehavior.AllowGet);
        }

        public ActionResult TransferWorkUnits()
        {
                return this.Json("Yay", JsonRequestBehavior.AllowGet);
            var credentials = AzureAuthenticationHelper.GetReportingCredentials();

            if (ServiceHandler.Instance.TryAuthenticate(
                credentials.SiteCode,
                credentials.ApiId,
                credentials.ApiPassword))
            {
                var helper = new PowerBiHelper(AzureAuthenticationHelper.GetSession());
                var workUnitsRaw = ServiceHandler.Instance.Client.GetWorkUnitsRaw(
                    credentials.SiteCode,
                    credentials.ApiId,
                    credentials.ApiPassword,
                    WorkUnit.All,
                    Employee.All,
                    Allocation.All,
                    Task.All,
                    Project.All,
                    Department.All,
                    DateTime.Now.AddMonths(-6),
                    DateTime.Now);

                if (workUnitsRaw.OwnerDocument != null)
                {
                    var namespaceManager = new XmlNamespaceManager(workUnitsRaw.OwnerDocument.NameTable);
                    namespaceManager.AddNamespace("tlp", "http://www.timelog.com/XML/Schema/tlp/v4_4");
                    var workUnits = workUnitsRaw.SelectNodes("tlp:WorkUnit", namespaceManager);

                    if (workUnits != null)
                    {
                        var tableConstructed = false;
                        var rowsList = new List<string>();
                        foreach (XmlNode workUnit in workUnits)
                        {
                            if (!tableConstructed)
                            {
                                try
                                {
                                    helper.DeleteRows(helper.GetDefaultDatasetId(), "WorkUnits");
                                }
                                catch (Exception)
                                {  
                                    // Ignore. Probably first request
                                    throw new Exception("PowerBI table definition out of sync. Please delete entire dataset and try transferring again.");
                                }

                                var tableSchemaJson = helper.BuildTableSchemaJson("WorkUnits", typeof(WorkUnit));
                                helper.UpdateTableSchema(helper.GetDefaultDatasetId(), "WorkUnits", tableSchemaJson);
                                tableConstructed = true;
                            }

                            rowsList.Add(helper.BuildTableRowJson(new WorkUnit(workUnit, namespaceManager)));
                        }

                        helper.AddRows(helper.GetDefaultDatasetId(), "WorkUnits", rowsList, 100);
                    }
                }

                return this.Json("Yay", JsonRequestBehavior.AllowGet);
            }

            return this.Json("Failed", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Tables(string datasetId)
        {
            if (AzureAuthenticationHelper.IsAuthenticated())
            {
                var helper = new PowerBiHelper(AzureAuthenticationHelper.GetSession());
                return this.View(helper.GetTables(datasetId).Value);
            }

            return this.RedirectToAction("Index");
        }
    }
}