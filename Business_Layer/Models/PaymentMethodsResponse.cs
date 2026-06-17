using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business_Layer.Models
{
    public class PaymentMethodsResponse
    {


        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("data")]
        public List<PaymentMethod>? Data { get; set; }

    }
    public class PaymentMethod
        {
            public int Id { get; set; }

            [JsonPropertyName("paymentId")]
            public int PaymentId { get; set; }

            [JsonPropertyName("name_en")]
            public string NameEn { get; set; }

            [JsonPropertyName("name_ar")]
            public string NameAr { get; set; }

            [JsonPropertyName("redirect")]
            public string Redirect { get; set; }

            [JsonPropertyName("logo")]
            public string Logo { get; set; }
        }

    
}
