using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC_Assignment1_Task6.Configuration
{
    public class StripeOptions
    {
        public string PublishableKey { get; set; }
        public string SecretKey { get; set; }
        public string WebhookSecret { get; set; }
        public string ProPrice { get; set; }
        public string BasicPrice { get; set; }
        public string Domain { get; set; }
    }
}
