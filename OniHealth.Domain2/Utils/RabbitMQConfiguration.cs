using RabbitMQ.Client;
using System;

namespace OniHealth.Domain.Models
{
    public class RabbitMQConfiguration
    {
        public RabbitMQConfiguration()
        {
            HostName= "172.17.0.3";
            UserName = "guest";
            Password = "guest";
            VirtualHost= "/";
            Port = 5672;
            Uri = new Uri("amqp://guest:guest@localhost:5672");
        }

        public RabbitMQConfiguration(string hostName, string userName, string password, int port)
        {
            HostName = hostName;
            UserName = userName;
            Password = password;
            Port = port;
            VirtualHost = "/";
        }

        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
        public Uri Uri { get; set; }
    }
}