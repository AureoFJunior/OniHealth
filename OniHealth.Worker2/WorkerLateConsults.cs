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

            while (true)
            {
                cache = new MemoryCache(new MemoryCacheOptions());
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30)
                };

                UserLogin user = WorkerSharedFunctions.ConvertObject<UserLogin>(await WorkerSharedFunctions.GetAsync("User/LogInto/admin/1234"));
                await WorkerSharedFunctions.GetAsync("Consult/SetLateConsultAppointments","", user.Token);
                var result = await WorkerSharedFunctions.GetAsync("Consult/SetLateConsultAppointments", "", user.Token);
                ConsultAppointment lateConsults = WorkerSharedFunctions.ConvertObject<ConsultAppointment>(result.ToString() == "[]" ? null : result);
                cache.Set(cacheKey, lateConsults, cacheOptions);
                await Console.Out.WriteLineAsync($"Succeded job at {DateTime.Now}");
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }
    }
}