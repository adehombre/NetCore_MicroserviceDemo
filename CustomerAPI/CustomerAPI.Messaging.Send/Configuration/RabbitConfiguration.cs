using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerAPI.Messaging.Send.Configuration
{
    public class RabbitConfiguration
    {
        public string Hostname { get; set; }

        public string QueueName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
