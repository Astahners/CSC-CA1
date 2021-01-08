using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC_Assignment1_Task6.Models
{
    public class CreateCheckoutSessionResponse
    {
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }
    }
}
