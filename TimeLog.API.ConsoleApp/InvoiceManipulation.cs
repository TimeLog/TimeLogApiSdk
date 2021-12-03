namespace TimeLog.ApiConsoleApp
{
    using System;
    using System.Linq;

    using log4net;

    using TimeLog.TransactionalApi.SDK;
    using TimeLog.TransactionalApi.SDK.InvoicingService;

    public class InvoiceManipulation
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(InvoiceManipulation));

        public static void Consume()
        {
            if (SecurityHandler.Instance.TryAuthenticate(out var _messages))
            {
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Sucessfully authenticated on transactional API");
                }

                var _invoiceRaw = InvoicingHandler.Instance.InvoicingClient.GetInvoiceByID(315, InvoicingHandler.Instance.Token);

                if (_invoiceRaw.ResponseState == ExecutionStatus.Success && _invoiceRaw.Return.Any())
                {
                    var _invoice = _invoiceRaw.Return.Single();

                    var _resultMarkInvoiceAsChecked = InvoicingHandler.Instance.InvoicingClient.MarkInvoiceAsChecked(Guid.Parse("50A7D344-FAEC-447E-8585-96426EAC1AD6"), InvoicingHandler.Instance.Token);
                    Logger.Info(string.Join(",", _resultMarkInvoiceAsChecked.Messages.Select(m => m.Message)));

                    var _resultUpdateExternalData = InvoicingHandler.Instance.InvoicingClient.UpdateExternalData(Guid.Parse("50A7D344-FAEC-447E-8585-96426EAC1AD6"), "12341234", new InvoiceState { Name = "Booked" }, InvoicingHandler.Instance.Token);
                    Logger.Info(string.Join(",", _resultUpdateExternalData.Messages.Select(m => m.Message)));
                }
                else
                {
                    Logger.Warn("Invoice not found");
                    Logger.Warn(string.Join(",", _invoiceRaw.Messages.Select(m => m.Message)));
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
    }
}