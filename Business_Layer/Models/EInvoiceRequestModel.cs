
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;




namespace Business_Layer.Models
{


    public class EInvoiceRequestModel
    {


        [JsonPropertyName("payment_method_id")]
        public int PaymentMethodId { get; set; }

        [JsonPropertyName("cartTotal")] 
        public string CartTotal { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("invoice_number")]
        public string invoice_number { get; set; }
     
        [JsonPropertyName("customer")]
        public CustomerDetails Customer { get; set; }

        [JsonPropertyName("redirectionUrls")]
        public RedirectionUrls RedirectionUrls { get; set; }

        [JsonPropertyName("cartItems")]
        public List<CartItem> CartItems { get; set; }
    }



    public class CustomerDetails
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }
    }

    public class RedirectionUrls
    {
        [JsonPropertyName("successUrl")]
        public string SuccessUrl { get; set; }

        [JsonPropertyName("failUrl")]
        public string FailUrl { get; set; }

        [JsonPropertyName("pendingUrl")]
        public string PendingUrl { get; set; }
    }

    public class CartItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; } // لاحظ إنك بعتها في الـ JSON كـ String "25"

        [JsonPropertyName("quantity")]
        public string Quantity { get; set; } // لاحظ إنك بعتها في الـ JSON كـ String "1"
    }

}
