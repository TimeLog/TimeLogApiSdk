using log4net;
using TimeLog.TransactionalAPI.SDK;
using TimeLog.TransactionalAPI.SDK.InvoicingService;

namespace TimeLog.API.ConsoleApp;

public class InvoiceManipulation
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(InvoiceManipulation));

    public static void Consume()
    {
        if (SecurityHandler.Instance.TryAuthenticate(out var messages))
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Info("Sucessfully authenticated on transactional API");
            }

            var invoiceRaw =
                InvoicingHandler.Instance.InvoicingClient.GetInvoiceByID(315, InvoicingHandler.Instance.Token);

            if (invoiceRaw.ResponseState == ExecutionStatus.Success && invoiceRaw.Return.Any())
            {
                var invoice = invoiceRaw.Return.Single();

                var resultMarkInvoiceAsChecked =
                    InvoicingHandler.Instance.InvoicingClient.MarkInvoiceAsChecked(
                        Guid.Parse("50A7D344-FAEC-447E-8585-96426EAC1AD6"), InvoicingHandler.Instance.Token);
                Logger.Info(string.Join(",", resultMarkInvoiceAsChecked.Messages.Select(m => m.Message)));

                var resultUpdateExternalData = InvoicingHandler.Instance.InvoicingClient.UpdateExternalData(
                    Guid.Parse("50A7D344-FAEC-447E-8585-96426EAC1AD6"), "12341234", new InvoiceState {Name = "Booked"},
                    InvoicingHandler.Instance.Token);
                Logger.Info(string.Join(",", resultUpdateExternalData.Messages.Select(m => m.Message)));
            }
            else
            {
                Logger.Warn("Invoice not found");
                Logger.Warn(string.Join(",", invoiceRaw.Messages.Select(m => m.Message)));
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
}