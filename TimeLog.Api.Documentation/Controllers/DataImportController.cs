using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace TimeLog.Api.Documentation.Controllers
{
    public class DataImportController : Controller
    {
        public static CultureInfo CsvCulture = new CultureInfo("en-US");

        // GET: DataImport
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Customers(string download, string requiredColumnsOnly)
        {
            var _list = new List<ImportColumn>
            {
                new ImportColumn<string>("Name", true, string.Empty, "TimeLog A/S", string.Empty),
                new ImportColumn<string>("Country", true, string.Empty, "DK", "Country must be valid ISO"),
                new ImportColumn<string>("Customer Status",true, string.Empty, "Customer", "Must match existing customer status in TimeLog"),
                new ImportColumn<string>("Currency", true, string.Empty, "DKK", "Must be valid ISO code"),
                new ImportColumn<string>("Customer No", true, string.Empty, "2019.00001", ""),
                new ImportColumn<string>("Address1", false, string.Empty, "Vesterbrogade 149", ""),
                new ImportColumn<string>("Address2", false, string.Empty, "Bygn. 4, 1.", ""),
                new ImportColumn<string>("Address3", false, string.Empty, "", ""),
                new ImportColumn<string>("Postal Code", false, string.Empty, "1620", ""),
                new ImportColumn<string>("City", false, string.Empty, "København V", ""),
                new ImportColumn<string>("State", false, string.Empty, "Copenhagen", ""),
                new ImportColumn<string>("Fax", false, string.Empty, "+45", ""),
                new ImportColumn<string>("Website", false, string.Empty, "https://www.timelog.com", ""),
                new ImportColumn<string>("Phone", false, string.Empty, "+4570200645", ""),
                new ImportColumn<string>("Email", false, string.Empty, "info@timelog.com", ""),
                new ImportColumn<string>("Owner", false, string.Empty, "SOX", "Must match initials of existing employee in TimeLog"),
                new ImportColumn<string>("Industry Code", false, string.Empty, "Consultancy", "Must match existing industry name in TimeLog"),
                new ImportColumn<string>("VAT No", false, string.Empty, "DK25896939", ""),
                new ImportColumn<bool>("Use Invoicing Address", false, false, false, "Indicate if the customer has a custom invoicing address"),
                new ImportColumn<string>("Invoice Company Name", false, string.Empty, "", "Optional custom invoicing address"),
                new ImportColumn<string>("Invoice Address", false, string.Empty, "", "Optional custom invoicing address"),
                new ImportColumn<string>("Invoice Address2", false, string.Empty, "", "Optional custom invoicing address"),
                new ImportColumn<string>("Invoice Address3", false, string.Empty, "", "Optional custom invoicing address"),
                new ImportColumn<string>("Invoice City", false, string.Empty, "", "Optional custom invoicing address"),
                new ImportColumn<string>("Invoice State", false, string.Empty, "", "Optional custom invoicing address"),
                new ImportColumn<string>("Invoice Country", false, string.Empty, "", "Optional custom invoicing address"),
                new ImportColumn<DateTime?>("Customer Since", false, null, DateTime.Now, ""),
                new ImportColumn<string>("Nickname", false, string.Empty, "TLP", ""),
                new ImportColumn<string>("Organization Number", false, string.Empty, "DK25896939", ""),
                new ImportColumn<decimal>("VAT Percent", false, 0, 25, ""),
                new ImportColumn<bool>("Calculate VAT", false, true, true, ""),
                new ImportColumn<decimal>("Discount Percent", false, 0, 10, ""),
                new ImportColumn<string>("EAN Number", false, string.Empty, "0123456789123", "Must be 13 characters"),
                new ImportColumn<bool>("Is OIO Customer", false, false, false, "Indicate whether to use OIOUBL (e-invoicing) for this customer"),
                new ImportColumn<string>("Payment Terms", false, "Use System Default", "Netto 14 days", "Must match the name of existing payment term in TimeLog"),
                new ImportColumn<string>("Invoicing Contact", false, "Not set", "Peter Summersen", "Either \"Project contact\" or must match full name of existing contact in TimeLog"),
                new ImportColumn<string>("Invoicing Internal Reference", false, "Not set", "Project's project manager", "Either \"Customer's owner\", \"Project's project manager\", \"Project's account manager\" or must match initials of existing employee in TimeLog"),
                new ImportColumn<string>("Invoicing Customer Reference", false, "Not set", "Peter Summersen", "Either \"Project contact\" or must match full name of existing contact in TimeLog"),
                new ImportColumn<string>("Invoicing Template", false, "Use System Default", "System Invoice (DK)", "Must match name of existing invoicing template in TimeLog"),
                new ImportColumn<int>("Invoicing Address Type", false, 1, 1, "Either \"Customer's address\" = 1, \"Contact's address\" = 2 or \"Customer's invoicing address\" = 4."),
            };

            if (!string.IsNullOrWhiteSpace(download))
            {
                return File(
                    Encoding.GetEncoding(1252).GetBytes(GenerateCsvSample(_list, !string.IsNullOrWhiteSpace(requiredColumnsOnly))),
                    "text/csv", "timelog-customer-import.csv");
            }

            ViewBag.DataType = "Customers";
            ViewBag.CsvSampleWithRequiredColumns = GenerateCsvSample(_list);
            ViewBag.CsvSampleWithAllColumns = GenerateCsvSample(_list, false);
            return View("GenericView", _list);
        }

        public ActionResult Employees(string download, string requiredColumnsOnly)
        {
            var _list = new List<ImportColumn>
            {
                new ImportColumn<string>("First Name", true, string.Empty, "Peter", string.Empty),
                new ImportColumn<string>("Last Name", true, string.Empty, "Summersen", string.Empty),
                new ImportColumn<string>("User Name", true, string.Empty, "peter.summersen@timelog.com", "Email-address is recommended"),
                new ImportColumn<string>("Initials", true, string.Empty, "PSU", string.Empty),
                new ImportColumn<string>("Job Title", false, string.Empty, "CFO", string.Empty),
                new ImportColumn<string>("Email", true, string.Empty, "peter.summersen@timelog.com", string.Empty),
                new ImportColumn<string>("Comment", false, string.Empty, "Hired in the hot summer of 1995", "Simple HR related comments, no sensitive information is allowed!"),
                new ImportColumn<string>("Legal Entity Name", true, string.Empty, "TimeLog A/S", "Optional if legal entities is not in use"),
                new ImportColumn<string>("Department Name", true, string.Empty, "Marketing", "Optional if departments is not in use"),
                new ImportColumn<string>("Manager Name", true, string.Empty, "BMA", "Must match initial of existing employee in TimeLog. Add managers first"),
                new ImportColumn<string>("Employee ID", true, string.Empty, "1005", "Optional if turned off"),
                new ImportColumn<string>("Employee Type", false, string.Empty, "Manager", "Must match existing employee type in TimeLog"),
                new ImportColumn<DateTime?>("Employment Date", false, DateTime.Now, new DateTime(1995, 7, 1), ""),
                new ImportColumn<string>("Default Hourly Rate Name", true, "Internal", "Senior Consultancy", "Must match existing hourly rate in TimeLog"),
                new ImportColumn<string>("Internal Cost Rate Name", true, string.Empty, "Manager", "Must match existing internal cost rate in TimeLog"),
                new ImportColumn<string>("Public Holiday Calender Name", true, string.Empty, "Denmark", "Must match existing holiday calendar in TimeLog"),
                new ImportColumn<string>("Allowance Legislation Name", true, string.Empty, "DK", "Optional if turned off. Must match existing allowance legislation in TimeLog"),
                new ImportColumn<string>("Normal Working Time Name", true, string.Empty, "Normal", "Must match existing normal working time in TimeLog"),
                new ImportColumn<string>("Salary Group Name", true, string.Empty, "Standard", "Must match existing salary group in TimeLog"),
                new ImportColumn<string>("Roles", false, "No roles", "Project manager|Senior leadership|System administrator", "List of roles separated by \"|\". Must match existing roles in TimeLog"),
                new ImportColumn<DateTime?>("ActivationDate", false, DateTime.Now, DateTime.Now.AddMonths(1), "An optional date in the future where the employee should be activated and licensed. A date in the past will activate the employee immediately"),
            };

            if (!string.IsNullOrWhiteSpace(download))
            {
                return File(
                    Encoding.GetEncoding(1252).GetBytes(GenerateCsvSample(_list, !string.IsNullOrWhiteSpace(requiredColumnsOnly))),
                    "text/csv", "timelog-employee-import.csv");
            }

            ViewBag.DataType = "Employees";
            ViewBag.CsvSampleWithRequiredColumns = GenerateCsvSample(_list);
            ViewBag.CsvSampleWithAllColumns = GenerateCsvSample(_list, false);
            return View("GenericView", _list);
        }

        public ActionResult Contacts(string download, string requiredColumnsOnly)
        {
            var _list = new List<ImportColumn>
            {
                new ImportColumn<string>("Customer Number", true, string.Empty, "2019.00001", "Must match existing customer in TimeLog"),
                new ImportColumn<string>("First Name", true, string.Empty, "John", ""),
                new ImportColumn<string>("Last Name", true, string.Empty, "Nielsen", ""),
                new ImportColumn<string>("Initials", false, string.Empty, "JN", ""),
                new ImportColumn<string>("Job Title", false, string.Empty, "CEO", ""),
                new ImportColumn<string>("Shown Name", false, string.Empty, "Johnny", ""),
                new ImportColumn<string>("Reports To", false, string.Empty, "Emma Johnson", "Must match full name of existing contact in TimeLog. Add managers first"),
                new ImportColumn<DateTime?>("Birthday", false, null, new DateTime(1980, 4, 2), ""),
                new ImportColumn<bool>("Is Active", false, true, true, "Determine if the contact should show up in lists"),
                new ImportColumn<string>("Owner", false, string.Empty, "PSU", "Must match initials of existing employee in TimeLog"),
                new ImportColumn<string>("Department", false, string.Empty, "R&D", ""),
                new ImportColumn<string>("Phone", false, string.Empty, "+4570200645", ""),
                new ImportColumn<string>("Mobile Phone", false, string.Empty, "+4570200645", ""),
                new ImportColumn<string>("Home Phone", false, string.Empty, "+4570200645", ""),
                new ImportColumn<string>("Fax", false, string.Empty, "+4570200645", ""),
                new ImportColumn<string>("Email", false, string.Empty, "john.nielsen@timelog.com", ""),
                new ImportColumn<string>("Address1", false, string.Empty, "Vesterbrogade 149, bygn. 4, 1.", ""),
                new ImportColumn<string>("Address2", false, string.Empty, "", ""),
                new ImportColumn<string>("Postal Code", false, string.Empty, "1620", ""),
                new ImportColumn<string>("City", false, string.Empty, "København V", ""),
                new ImportColumn<string>("State", false, string.Empty, "", ""),
                new ImportColumn<string>("Country", true, string.Empty, "DK", "Country must be valid ISO"),
                new ImportColumn<string>("Comment", false, string.Empty, "Accepted our offer in May 2019", "Simple CRM related comments, no sensitive information is allowed!"),
            };

            if (!string.IsNullOrWhiteSpace(download))
            {
                return File(
                    Encoding.GetEncoding(1252).GetBytes(GenerateCsvSample(_list, !string.IsNullOrWhiteSpace(requiredColumnsOnly))),
                    "text/csv", "timelog-contact-import.csv");
            }

            ViewBag.DataType = "Contacts";
            ViewBag.CsvSampleWithRequiredColumns = GenerateCsvSample(_list);
            ViewBag.CsvSampleWithAllColumns = GenerateCsvSample(_list, false);
            return View("GenericView", _list);
        }

        public ActionResult Projects(string download, string requiredColumnsOnly)
        {
            var _list = new List<ImportColumn>
            {
                new ImportColumn<string>("Customer Number", true, string.Empty, "2019.00001", "Must match existing customer in TimeLog"),
                new ImportColumn<string>("Project Template Name", true, string.Empty, "Consultancy Project", "Must match existing project template in TimeLog"),
                new ImportColumn<string>("Name", true, string.Empty, "Q1 Consultancy for KHT Portal", ""),
                new ImportColumn<string>("Number", true, string.Empty, "P19.1235", "Optional if automatic number series has been turned on"),
                new ImportColumn<string>("Legal Entity Name", true, string.Empty, "TimeLog A/S", "Optional if legal entities is not in use"),
                new ImportColumn<string>("Currency", true, string.Empty, "DKK", "Must be valid ISO code"),
                new ImportColumn<string>("Project Manager Initials", false, string.Empty, "PSU", "Must match initials of existing employee in TimeLog"),
                new ImportColumn<string>("Account Manager Initials", false, string.Empty, "SOX", "Must match initials of existing employee in TimeLog"),
                new ImportColumn<string>("Department Name", false, string.Empty, "Sales", "Must match name of existing department in TimeLog"),
                new ImportColumn<bool>("Use Tasks and Milestones From Template", true, true, true, ""),
                new ImportColumn<bool>("Use Resource Group From Template", true, true, true, ""),
                new ImportColumn<bool>("Use Allocations From Template", true, true, true, ""),
                new ImportColumn<bool>("Use Payment Plan From Template", true, true, true, ""),
                new ImportColumn<DateTime?>("Start Date", false, DateTime.Now, new DateTime(2020, 2, 1), ""),
                new ImportColumn<DateTime?>("End Date", false, DateTime.Now.AddMonths(3), new DateTime(2020, 5, 1), "If empty, start date + 3 months"),
                new ImportColumn<string>("Project Type", false, string.Empty, "Implementation", "Must match name of existing project type in TimeLog"),
                new ImportColumn<string>("Project Category", false, string.Empty, "Cost Neutral", "Must match name of existing project category in TimeLog"),
            };

            if (!string.IsNullOrWhiteSpace(download))
            {
                return File(
                    Encoding.GetEncoding(1252).GetBytes(GenerateCsvSample(_list, !string.IsNullOrWhiteSpace(requiredColumnsOnly))),
                    "text/csv", "timelog-project-import.csv");
            }

            ViewBag.DataType = "Projects";
            ViewBag.CsvSampleWithRequiredColumns = GenerateCsvSample(_list);
            ViewBag.CsvSampleWithAllColumns = GenerateCsvSample(_list, false);
            return View("GenericView", _list);
        }

        public string GenerateCsvSample(IList<ImportColumn> columns, bool requiredColumnsOnly = true)
        {
            return string.Join(",",
                       columns.Where(column => !requiredColumnsOnly | (requiredColumnsOnly & column.IsMandatory)).Select(column => "\"" + column.Name + "\""))
                   + Environment.NewLine
                   + string.Join(",", columns.Where(column => !requiredColumnsOnly | (requiredColumnsOnly & column.IsMandatory)).Select(column => column.SampleValue));
        }
    }

    public abstract class ImportColumn
    {
        public string Name { get; set; }

        public bool IsMandatory { get; set; }

        public abstract string Note { get; }

        public abstract string SampleValue { get; }

        public abstract string DefaultValue { get; }

        public abstract string DataType { get; }
    }

    public class ImportColumn<T> : ImportColumn
    {
        private readonly T _defaultValue;
        private readonly T _sampleValue;
        private readonly string _note;

        public ImportColumn(string name, bool isMandatory, T defaultValue, T sampleValue, string note)
        {
            _defaultValue = defaultValue;
            _sampleValue = sampleValue;
            Name = name;
            IsMandatory = isMandatory;
            _note = note;
        }

        public override string Note
        {
            get
            {
                if (_sampleValue is decimal)
                {
                    return "Format: 0,000.00" + (string.IsNullOrWhiteSpace(_note) ? string.Empty : " | " + _note);
                }

                if (_sampleValue is int)
                {
                    return "Format: 0,000" + (string.IsNullOrWhiteSpace(_note) ? string.Empty : " | " + _note);
                }

                if (_sampleValue is DateTime)
                {
                    return "Format: yyyy-MM-dd" + (string.IsNullOrWhiteSpace(_note) ? string.Empty : " | " + _note);
                }

                if (_sampleValue is bool)
                {
                    return "Format: 0 (false) or 1 (true)" + (string.IsNullOrWhiteSpace(_note) ? string.Empty : " | " + _note);
                }

                return _note;
            }
        }

        public override string SampleValue
        {
            get
            {
                if (_sampleValue == null)
                {
                    return string.Empty;
                }

                if (_sampleValue is string)
                {
                    return "\"" + _sampleValue + "\"";
                }

                if (_sampleValue is DateTime)
                {
                    return "\"" + Convert.ToDateTime(_sampleValue).ToString("yyyy-MM-dd", DataImportController.CsvCulture) + "\"";
                }

                if (_sampleValue is bool)
                {
                    return Convert.ToBoolean(_sampleValue) ? "1" : "0";
                }

                if (_sampleValue is decimal)
                {
                    return "\"" + Convert.ToDecimal(_sampleValue).ToString("#,##0.00", DataImportController.CsvCulture) + "\"";
                }

                if (_sampleValue is int)
                {
                    return "\"" + Convert.ToInt32(_sampleValue).ToString("#,##0", DataImportController.CsvCulture) + "\"";
                }

                return string.Empty;
            }
        }

        public override string DefaultValue
        {
            get
            {
                if (_defaultValue == null)
                {
                    return string.Empty;
                }

                if (typeof(T) == typeof(string))
                {
                    return _defaultValue.ToString();
                }

                if (_defaultValue is DateTime)
                {
                    return Convert.ToDateTime(_defaultValue).ToString("yyyy-MM-dd", DataImportController.CsvCulture);
                }

                if (_defaultValue is bool)
                {
                    return Convert.ToBoolean(_defaultValue) ? "1" : "0";
                }

                if (_defaultValue is decimal)
                {
                    return Convert.ToDecimal(_defaultValue).ToString("#,##0.00", DataImportController.CsvCulture);
                }

                if (_defaultValue is int)
                {
                    return Convert.ToInt32(_defaultValue).ToString("#,##0", DataImportController.CsvCulture);
                }

                return string.Empty;
            }
        }

        public override string DataType
        {
            get
            {
                var _type = typeof(T);
                var _underlyingType = Nullable.GetUnderlyingType(_type);
                if (_underlyingType != null)
                {
                    return _underlyingType.Name;
                }

                return _type.Name;
            }
        }
    }
}