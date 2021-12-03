namespace TimeLog.ApiConsoleApp
{
    using System;
    using System.Linq;

    using log4net;

    using TimeLog.TransactionalApi.SDK;
    using TimeLog.TransactionalApi.SDK.FinancialService;
    using TimeLog.TransactionalApi.SDK.OrganisationService;
    using TimeLog.TransactionalApi.SDK.RawHelper;
    using TimeLog.TransactionalApi.SDK.SalaryService;

    using ExecutionStatus = TimeLog.TransactionalApi.SDK.OrganisationService.ExecutionStatus;

    /// <summary>
    /// Template class for consuming the transactional API
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

            if (SecurityHandler.Instance.TryAuthenticate(out var _messages))
            {
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Sucessfully authenticated on transactional API");
                }

                var _employeeGuid = Guid.NewGuid();
                Department _department = null;

                var _departmentResult = OrganisationHandler.Instance.OrganisationClient.GetDepartments(OrganisationHandler.Instance.Token);

                foreach (var _apiMessage in _departmentResult.Messages)
                {
                    if (Logger.IsDebugEnabled)
                    {
                        Logger.Debug(_apiMessage.Message);
                    }
                }

                if (_departmentResult.ResponseState == ExecutionStatus.Success)
                {
                    _department = _departmentResult.Return.FirstOrDefault();
                }

                if (_department == null)
                {
                    throw new ArgumentException("No department found");
                }

                NormalWorkingTime _normalWorkingTime = GetNormalWorkingTime();
                HolidayCalendar _holidayCalendar = GetHolidayCalendar();
                Allowance _allowance = GetAllowance();
                HourlyRate _hourlyRate = GetHourlyRate();
                EmployeeCostRate _employeeCostRate = GetEmployeeCostRate();

                var _employeeResult = OrganisationHandler.Instance.OrganisationClient.CreateEmployee(
                    _employeeGuid,
                    "peter.nielsen@contoso.com",
                    "PN",
                    "Peter",
                    "Nielsen",
                    "007",
                    "peter.nielsen@contoso.com",
                    "Senior Developer",
                    "+45 12345678",
                    "+45 87654321",
                    _normalWorkingTime.Name,
                    _holidayCalendar.Name,
                    _allowance.Name,
                    "Employee",
                    _hourlyRate.Name,
                    _employeeCostRate.Name,
                    "peter.nielsen",
                    _department.ID,
                    OrganisationHandler.Instance.Token);

                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\InsertEmployee.txt");
                if (_employeeResult.ResponseState == ExecutionStatus.Success)
                {
                    Logger.Info("Employee created");
                    var _employee = _employeeResult.Return.FirstOrDefault();
                    if (_employee != null)
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
                    foreach (var _apiMessage in _employeeResult.Messages)
                    {
                        if (Logger.IsErrorEnabled)
                        {
                            Logger.Error(_apiMessage.Message);
                        }
                    }
                }
            }
            else
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("Failed to authenticate to transactional API");
                    Logger.Warn(string.Join(",", _messages));
                }
            }
        }

        private static EmployeeCostRate GetEmployeeCostRate()
        {
            EmployeeCostRate _costPrice = null;
            var _costPriceResult = FinancialHandler.Instance.FinancialClient.GetEmployeeCostRates(FinancialHandler.Instance.Token);

            foreach (var _apiMessage in _costPriceResult.Messages)
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug(_apiMessage.Message);
                }
            }

            if (_costPriceResult.ResponseState == TransactionalApi.SDK.FinancialService.ExecutionStatus.Success)
            {
                _costPrice = _costPriceResult.Return.FirstOrDefault();
            }

            if (_costPrice == null)
            {
                throw new ArgumentException("No employee cost rate found");
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Employee cost rate selected: " + _costPrice);
            }

            return _costPrice;
        }

        private static HourlyRate GetHourlyRate()
        {
            HourlyRate _hourlyRate = null;
            var _hourlyRateResult = FinancialHandler.Instance.FinancialClient.GetHourlyRates(string.Empty, -1, FinancialHandler.Instance.Token);

            foreach (var _apiMessage in _hourlyRateResult.Messages)
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug(_apiMessage.Message);
                }
            }

            if (_hourlyRateResult.ResponseState == TransactionalApi.SDK.FinancialService.ExecutionStatus.Success)
            {
                _hourlyRate = _hourlyRateResult.Return.FirstOrDefault();
            }

            if (_hourlyRate == null)
            {
                throw new ArgumentException("No hourly rate found");
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Hourly rate selected: " + _hourlyRate);
            }

            return _hourlyRate;
        }

        private static HolidayCalendar GetHolidayCalendar()
        {
            HolidayCalendar _calendar = null;
            var _calendarResult = SalaryHandler.Instance.SalaryClient.GetHolidayCalendars(SalaryHandler.Instance.Token);

            foreach (var _apiMessage in _calendarResult.Messages)
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug(_apiMessage.Message);
                }
            }

            if (_calendarResult.ResponseState == TransactionalApi.SDK.SalaryService.ExecutionStatus.Success)
            {
                _calendar = _calendarResult.Return.FirstOrDefault();
            }

            if (_calendar == null)
            {
                throw new ArgumentException("No holiday calendar found");
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Calendar selected: " + _calendar.Name);
            }

            return _calendar;
        }

        private static Allowance GetAllowance()
        {
            Allowance _allowance = null;
            var _allowanceResult = SalaryHandler.Instance.SalaryClient.GetAllowances(SalaryHandler.Instance.Token);

            foreach (var _apiMessage in _allowanceResult.Messages)
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug(_apiMessage.Message);
                }
            }

            if (_allowanceResult.ResponseState == TransactionalApi.SDK.SalaryService.ExecutionStatus.Success)
            {
                _allowance = _allowanceResult.Return.FirstOrDefault();
            }

            if (_allowance == null)
            {
                throw new ArgumentException("No allowance found");
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Allowance selected: " + _allowance.Name);
            }

            return _allowance;
        }

        private static NormalWorkingTime GetNormalWorkingTime()
        {
            NormalWorkingTime _normalWorkingTime = null;
            var _normalWorkWeeksResult = SalaryHandler.Instance.SalaryClient.GetNormalWorkweeks(SalaryHandler.Instance.Token);

            foreach (var _apiMessage in _normalWorkWeeksResult.Messages)
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug(_apiMessage.Message);
                }
            }

            if (_normalWorkWeeksResult.ResponseState == TransactionalApi.SDK.SalaryService.ExecutionStatus.Success)
            {
                _normalWorkingTime = _normalWorkWeeksResult.Return.FirstOrDefault();
            }

            if (_normalWorkingTime == null)
            {
                throw new ArgumentException("No normal working time found");
            }

            if (Logger.IsDebugEnabled)
            {
                Logger.Debug("Working time selected: " + _normalWorkingTime.Name);
            }

            return _normalWorkingTime;
        }
    }
}