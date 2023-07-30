using Polly;
using Polly.Retry;

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
