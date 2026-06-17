using Business_Layer.Models;
using System.Reflection;

namespace Business_Layer;

public interface IFawaterakPaymentService
{
    // Create EInvoice Link
    Task<EInvoiceResponseDataModel?> CreateEInvoiceAsync(EInvoiceRequestModel eInvoice);


    Task<string> GeneralPay(EInvoiceRequestModel invoice);

    // The method that collects order data from the database and prepares the process
    Task<string> ProcessOrderPayment(int orderId, int userId);

   Task<bool> WebhookAsync(FawaterakWebhookModel Model);

}