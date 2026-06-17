using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business_Layer.Models
{
    public class CardPaymentResponse
    {

     
       [JsonPropertyName("data")]
       public CardData Data { get; set; }
     

        public class CardData
        {
            [JsonPropertyName("Payment_data")]
            public CardPaymentData paymentData{ get; set; }
        }

        public class CardPaymentData
        {
            [JsonPropertyName("redirectTo")] 
            public string RedirectTo { get; set; }
        }
    }
}
