using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business_Layer
{
   
    
        public class EInvoiceResponseModel
        {
            [JsonPropertyName("status")]
            public string? Status { get; set; }

            [JsonPropertyName("data")]
            public EInvoiceResponseDataModel? Data { get; set; }
        }

    public class EInvoiceResponseDataModel
    {
        [JsonPropertyName("invoice_id")]
        public int? InvoiceId { get; set; }

        [JsonPropertyName("invoice_Key")]
        public string? InvoiceKey { get; set; }

        [JsonPropertyName("payment_data")]
        public  Url redirectTo { get; set; } 
        public class Url
        {
            [JsonPropertyName("redirectTo")]
            public string redirectTo { get; set; }
        }

        
    }
    
}

