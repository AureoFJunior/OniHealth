using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Domain.Utils
{
    public class PollyHandler
    {
        public RetryPolicy TestePolicy;
        public AsyncRetryPolicy AsyncTestePolicy;

        public PollyHandler()
        {
            TestePolicy = Policy.Handle<Exception>().WaitAndRetry(3, retry => TimeSpan.FromSeconds(1),
                (ex, timestamp) =>
                {
                    Console.WriteLine("Retrying...");
                });
        }
    }
}
