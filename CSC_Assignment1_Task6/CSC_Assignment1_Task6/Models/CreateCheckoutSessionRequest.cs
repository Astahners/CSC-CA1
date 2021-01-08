using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC_Assignment1_Task6.Models
{
    public class CreateCheckoutSessionRequest
    {
        [JsonProperty("priceId")]
        public string PriceId { get; set; }
    }
}
