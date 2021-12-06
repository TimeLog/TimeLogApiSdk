using log4net;
using TimeLog.TransactionalAPI.SDK;
using TimeLog.TransactionalAPI.SDK.FinancialService;
using TimeLog.TransactionalAPI.SDK.OrganisationService;
using TimeLog.TransactionalAPI.SDK.RawHelper;
using TimeLog.TransactionalAPI.SDK.SalaryService;
using ExecutionStatus = TimeLog.TransactionalAPI.SDK.OrganisationService.ExecutionStatus;

namespace TimeLog.ApiConsoleApp;

/// <summary>
///     Template class for consuming the transactional API
/// </summary>
public class InsertEmployeeTest
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(InsertEmployeeTest));

    public static void Consume()
    {
        // For getting the raw XML
        SecurityHandler.Instance.CollectRawRequestResponse = true;
        OrganisationHandler.Instance.CollectRawRequestResponse = true;
        SalaryHandler.Instance.CollectRawRequestResponse = true;

        if (SecurityHandler.Instance.TryAuthenticate(out var messages))
        {
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");
            if (Logger.IsInfoEnabled)
            {
                Logger.Info("Sucessfully authenticated on transactional API");
            }

            var employeeGuid = Guid.NewGuid();
            Department? department = null;

            var departmentResult =
                OrganisationHandler.Instance.OrganisationClient.GetDepartments(OrganisationHandler.Instance.Token);

            foreach (var apiMessage in departmentResult.Messages)
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug(apiMessage.Message);
                }
            }

            if (departmentResult.ResponseState == ExecutionStatus.Success)
            {
                department = departmentResult.Return.FirstOrDefault();
            }

            if (department == null)
            {
                throw new ArgumentException("No department found");
            }

            var normalWorkingTime = GetNormalWorkingTime();
            var holidayCalendar = GetHolidayCalendar();
            var allowance = GetAllowance();
            var hourlyRate = GetHourlyRate();
            var employeeCostRate = GetEmployeeCostRate();

            var employeeResult = OrganisationHandler.Instance.OrganisationClient.CreateEmployee(
                employeeGuid,
                "peter.nielsen@contoso.com",
                "PN",
                "Peter",
                "Nielsen",
                "007",
                "peter.nielsen@contoso.com",
                "Senior Developer",
                "+45 12345678",
                "+45 87654321",
                normalWorkingTime.Name,
                holidayCalendar.Name,
                allowance.Name,
                "Employee",
                hourlyRate.Name,
                employeeCostRate.Name,
                "peter.nielsen",
                department.ID,
                OrganisationHandler.Instance.Token);

            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\InsertEmployee.txt");
            if (employeeResult.ResponseState == ExecutionStatus.Success)
            {
                Logger.Info("Employee created");
                var employee = employeeResult.Return.FirstOrDefault();
                if (employee != null)
                {
                }
                else
                {
                    if (Logger.IsWarnEnabled)
                    {
                        Logger.Warn("Employee not created");
                    }
                }
            }
            else
            {
                foreach (var apiMessage in employeeResult.Messages)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(apiMessage.Message);
                    }
                }
            }
        }
        else
        {
            if (Logger.IsWarnEnabled)
            {
                Logger.Warn("Failed to authenticate to transactional API");
                Logger.Warn(string.Join(",", messages));
            }
        }
    }

    private static EmployeeCostRate? GetEmployeeCostRate()
    {
        EmployeeCostRate? costPrice = null;
        var costPriceResult =
            FinancialHandler.Instance.FinancialClient.GetEmployeeCostRates(FinancialHandler.Instance.Token);

        foreach (var apiMessage in costPriceResult.Messages)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(apiMessage.Message);
            }
        }

        if (costPriceResult.ResponseState == TransactionalAPI.SDK.FinancialService.ExecutionStatus.Success)
        {
            costPrice = costPriceResult.Return.FirstOrDefault();
        }

        if (costPrice == null)
        {
            throw new ArgumentException("No employee cost rate found");
        }

        if (Logger.IsDebugEnabled)
        {
            Logger.Debug("Employee cost rate selected: " + costPrice);
        }

        return costPrice;
    }

    private static HourlyRate? GetHourlyRate()
    {
        HourlyRate? hourlyRate = null;
        var hourlyRateResult =
            FinancialHandler.Instance.FinancialClient.GetHourlyRates(string.Empty, -1, FinancialHandler.Instance.Token);

        foreach (var apiMessage in hourlyRateResult.Messages)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(apiMessage.Message);
            }
        }

        if (hourlyRateResult.ResponseState == TransactionalAPI.SDK.FinancialService.ExecutionStatus.Success)
        {
            hourlyRate = hourlyRateResult.Return.FirstOrDefault();
        }

        if (hourlyRate == null)
        {
            throw new ArgumentException("No hourly rate found");
        }

        if (Logger.IsDebugEnabled)
        {
            Logger.Debug("Hourly rate selected: " + hourlyRate);
        }

        return hourlyRate;
    }

    private static HolidayCalendar? GetHolidayCalendar()
    {
        HolidayCalendar? calendar = null;
        var calendarResult = SalaryHandler.Instance.SalaryClient.GetHolidayCalendars(SalaryHandler.Instance.Token);

        foreach (var apiMessage in calendarResult.Messages)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(apiMessage.Message);
            }
        }

        if (calendarResult.ResponseState == TransactionalAPI.SDK.SalaryService.ExecutionStatus.Success)
        {
            calendar = calendarResult.Return.FirstOrDefault();
        }

        if (calendar == null)
        {
            throw new ArgumentException("No holiday calendar found");
        }

        if (Logger.IsDebugEnabled)
        {
            Logger.Debug("Calendar selected: " + calendar.Name);
        }

        return calendar;
    }

    private static Allowance? GetAllowance()
    {
        Allowance? allowance = null;
        var allowanceResult = SalaryHandler.Instance.SalaryClient.GetAllowances(SalaryHandler.Instance.Token);

        foreach (var apiMessage in allowanceResult.Messages)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(apiMessage.Message);
            }
        }

        if (allowanceResult.ResponseState == TransactionalAPI.SDK.SalaryService.ExecutionStatus.Success)
        {
            allowance = allowanceResult.Return.FirstOrDefault();
        }

        if (allowance == null)
        {
            throw new ArgumentException("No allowance found");
        }

        if (Logger.IsDebugEnabled)
        {
            Logger.Debug("Allowance selected: " + allowance.Name);
        }

        return allowance;
    }

    private static NormalWorkingTime? GetNormalWorkingTime()
    {
        NormalWorkingTime? normalWorkingTime = null;
        var normalWorkWeeksResult =
            SalaryHandler.Instance.SalaryClient.GetNormalWorkweeks(SalaryHandler.Instance.Token);

        foreach (var apiMessage in normalWorkWeeksResult.Messages)
        {
            if (Logger.IsDebugEnabled)
            {
                Logger.Debug(apiMessage.Message);
            }
        }

        if (normalWorkWeeksResult.ResponseState == TransactionalAPI.SDK.SalaryService.ExecutionStatus.Success)
        {
            normalWorkingTime = normalWorkWeeksResult.Return.FirstOrDefault();
        }

        if (normalWorkingTime == null)
        {
            throw new ArgumentException("No normal working time found");
        }

        if (Logger.IsDebugEnabled)
        {
            Logger.Debug("Working time selected: " + normalWorkingTime.Name);
        }

        return normalWorkingTime;
    }
}