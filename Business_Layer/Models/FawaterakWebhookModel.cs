using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business_Layer.Models
{
    public class FawaterakWebhookModel
    {
      

        [JsonPropertyName("invoice_status")]
        public string InvoiceStatus { get; set; }



        [JsonPropertyName("OrderId")]
        public string OrderId { get; set; }


      
    }


}
