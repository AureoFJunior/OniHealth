using Microsoft.Extensions.Configuration;
using OniHealth.Domain.Utils;

namespace OniHealth.Domain.Models
{
    public class RabbitMQConfiguration
    {
        private readonly IConfiguration _configuration;

        public RabbitMQConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            var queueSection = _configuration.GetSection("RabbitMQQueueConfig");
            var exchangeSection = _configuration.GetSection("RabbitMQExchangeConfig");

            QueueHostName = queueSection["HostName"]; //localhost??
            QueueUserName = queueSection["UserName"];
            QueuePassword = queueSection["Password"];
            QueueVirtualHost= queueSection["VirtualHost"];
            QueuePort = SharedFunctions.SafeConvertToNumber<int>(queueSection["Port"]);
            QueueUri = new Uri(queueSection["Uri"]);

            ExchangeHostName = exchangeSection["HostName"]; //localhost??
            ExchangeUserName = exchangeSection["UserName"];
            ExchangePassword = exchangeSection["Password"];
            ExchangeVirtualHost = exchangeSection["VirtualHost"];
            ExchangePort = SharedFunctions.SafeConvertToNumber<int>(exchangeSection["Port"]);
            ExchangeUri = new Uri(exchangeSection["Uri"]);


        }

        public RabbitMQConfiguration(string queueHostName, string queueUserName, string queuePassword, int queuePort,
            string exchangeHostName, string exchangeUserName, string exchangePassword, int exchangePort)
        {
            QueueHostName = queueHostName;
            QueueUserName = queueUserName;
            QueuePassword = queuePassword;
            QueuePort = queuePort;
            QueueVirtualHost = "/";

            ExchangeHostName = exchangeHostName;
            ExchangeUserName = exchangeUserName;
            ExchangePassword = exchangePassword;
            ExchangePort = exchangePort;
            ExchangeVirtualHost = "/";
        }

        public string QueueHostName { get; set; }
        public string QueueUserName { get; set; }
        public string QueuePassword { get; set; }
        public int QueuePort { get; set; }
        public string QueueVirtualHost { get; set; }
        public Uri QueueUri { get; set; }

        public string ExchangeHostName { get; set; }
        public string ExchangeUserName { get; set; }
        public string ExchangePassword { get; set; }
        public int ExchangePort { get; set; }
        public string ExchangeVirtualHost { get; set; }
        public Uri ExchangeUri { get; set; }
    }
}