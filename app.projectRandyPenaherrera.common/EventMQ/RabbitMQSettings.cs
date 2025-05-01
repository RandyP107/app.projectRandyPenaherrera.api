using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.projectRandyPenaherrera.common.EventMQ
{
    public class RabbitMQSettings
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? VirtualHost { get; set; }
        public int Port { get; set; }
        public string? Hostname { get; set; }
    }
}
