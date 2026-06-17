using Business_Layer.Models;
using Data_Accesst_Layer;
using Microsoft.Extensions.Options;
using System.Data;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace Business_Layer
{
    public class FawaterakPaymentService : IFawaterakPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly string ApiKey;
        private readonly string BaseUrl;
        private readonly string ProviderKey;


        public FawaterakPaymentService(HttpClient httpClient, IOptions<FawaterakOptions> options)
        {
            _httpClient = httpClient;

            //  appsettings.json قراءة القيم من ملف
            var cfg = options.Value;

            ApiKey = cfg.ApiKey;
            BaseUrl = cfg.BaseUrl;
            ProviderKey = cfg.ProviderKey; 
        }

        public async Task<EInvoiceResponseDataModel?> CreateEInvoiceAsync(EInvoiceRequestModel eInvoice)
        {
    

            var url = "invoiceInitPay";

            var response = await _httpClient.PostAsJsonAsync(url, eInvoice);

            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine(error);

            if (response.IsSuccessStatusCode)
            {
                //  قراءة الرد

                var result = await response.Content.ReadFromJsonAsync<EInvoiceResponseModel>();
                return result?.Data;
            }

            return null;
        }
        public async Task<string?> GeneralPay(EInvoiceRequestModel invoice)
        {
            invoice.PaymentMethodId = 2;

            var url = "invoiceInitPay";
            var response = await _httpClient.PostAsJsonAsync(url, invoice);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<CardPaymentResponse>();

                return result?.Data?.paymentData?.RedirectTo;
            }

            //  Bad Request
            var errorBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Payment Error Detail: {errorBody}");
            return null;
        }

        public async Task<string?> ProcessOrderPayment(int orderId, int userId)
        {
            DataTable dtOrder = PaymentD.GetPaymentData(orderId, userId);
            if (dtOrder == null || dtOrder.Rows.Count == 0) return null;
            DataRow orderRow = dtOrder.Rows[0];

            // Procuct name + Details Cart
            DataTable dtItems = PaymentD.GetOrderItems(orderId);
            var cartItemsList = new List<CartItem>();

            foreach (DataRow itemRow in dtItems.Rows)
            {
                cartItemsList.Add(new CartItem
                {
                    // هنا بنسحب اسم المنتج الحقيقي من الداتابيز بدل الرقم

                    Name = itemRow["Name"].ToString(),
                    Price = itemRow["PriceAtPurchase"].ToString(),
                    Quantity = itemRow["Quantity"].ToString()
                });
            }


            // Json of Fawaterak 
            var request = new EInvoiceRequestModel
            {
                PaymentMethodId = 2,
                CartTotal = orderRow["TotalAmount"].ToString(),
                Currency = "EGP",
                invoice_number = orderId.ToString(),

                Customer = new CustomerDetails
                {
                    FirstName = orderRow["FirstName"].ToString(),
                    LastName = orderRow["LastName"].ToString(),
                    Email = orderRow["Email"].ToString(),
                    Phone = orderRow["Phone"].ToString(),
                    Address = orderRow["Address"].ToString()
                },

                CartItems = cartItemsList, 

                RedirectionUrls = new RedirectionUrls
                {
                    SuccessUrl = "https://dev.fawaterk.com/success",
                    FailUrl = "https://dev.fawaterk.com/fail",
                    PendingUrl = "https://dev.fawaterk.com/pending",
                }
            };
            return await GeneralPay(request);
        }

        public async Task<bool> WebhookAsync(FawaterakWebhookModel model)
        {
            if (int.TryParse(model.OrderId, out int orderId))
            {
                string status = model.InvoiceStatus?.ToLower();

                // 2 معالجة الحالة بناءً على اللي جاي من فواتيرك
                switch (status)
                {
                    case "paid":
                        return PaymentD.UpdateOrderStatus(orderId, "Paid");

                    case "failed":
                        return PaymentD.UpdateOrderStatus(orderId, "Failed");

                    case "cancelled":
                        return PaymentD.UpdateOrderStatus(orderId, "Cancelled");

                    default:
                        // لو حالة تانية مش متعرفة، نرجع false عشان الـ Controller يدي 400
                        return false;
                }
            }

            return false;
        }

    }
}
