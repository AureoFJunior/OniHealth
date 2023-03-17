using Microsoft.Extensions.Caching.Memory;
using OniHealth.Worker2.Models;
using OniHealth.Worker2.Utils;

namespace OniHealth.Worker2
{
    public class WorkerLateConsults : BackgroundService
    {
        private readonly ILogger<WorkerLateConsults> _logger;

        public WorkerLateConsults(ILogger<WorkerLateConsults> logger)
        {
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Console.Out.WriteLineAsync($"Starting late consults worker... at {DateTime.Now}");

            MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            var cacheKey = "consultAppointments";

            while (!stoppingToken.IsCancellationRequested)
            {
                cache = new MemoryCache(new MemoryCacheOptions());
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30)
                };

                await WorkerSharedFunctions.GetAsync("Consult/SetLateConsultAppointments");
                UserLogin user = WorkerSharedFunctions.ConvertObject<UserLogin>(await WorkerSharedFunctions.GetAsync("User/LogInto/admin/1234"));
                ConsultAppointment lateConsults = WorkerSharedFunctions.ConvertObject<ConsultAppointment>(await WorkerSharedFunctions.GetAsync("Consult/SetLateConsultAppointments", "", user.Token));
                cache.Set(cacheKey, lateConsults, cacheOptions);
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }
}