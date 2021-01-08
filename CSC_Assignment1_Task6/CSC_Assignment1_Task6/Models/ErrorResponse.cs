using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC_Assignment1_Task6.Models
{
    public class ErrorMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class ErrorResponse
    {
        [JsonProperty("error")]
        public ErrorMessage ErrorMessage { get; set; }
    }
}
