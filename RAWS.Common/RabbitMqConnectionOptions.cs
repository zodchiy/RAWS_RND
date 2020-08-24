using System;

namespace RAWS.Common
{
    public class RabbitMqConnectionOptions
    {
        public string HostName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public bool AutomaticRecoveryEnabled { get; set; }
        public int RequestedHeartbeat { get; set; }
    }
}
