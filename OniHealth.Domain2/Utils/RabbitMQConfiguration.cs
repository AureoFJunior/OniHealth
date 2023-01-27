using System;

namespace OniHealth.Domain.Models
{
    public class RabbitMQConfiguration
    {
        public RabbitMQConfiguration()
        {
            HostName = "rabbit@my-rabbitmq";
            UserName = "ademe";
            Password = "123456";
            Port = 5672;
        }

        public RabbitMQConfiguration(string hostName, string userName, string password, int port)
        {
            HostName = hostName;
            UserName = userName;
            Password = password;
            Port = port;
        }

        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
}