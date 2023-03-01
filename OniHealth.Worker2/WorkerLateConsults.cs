using Microsoft.Extensions.Caching.Memory;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Models;
using OniHealth.Domain.Utils;

namespace OniHealth.Worker2
{
    public class WorkerLateConsults : BackgroundService
    {
        private readonly ILogger<WorkerLateConsults> _logger;
        private readonly IRepositoryConsult _repositoryConsult;

        public WorkerLateConsults(ILogger<WorkerLateConsults> logger,
            IRepositoryConsult repositoryConsult)
        {
            _logger = logger;
            _repositoryConsult = repositoryConsult;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            var cacheKey = "consultAppointments";

            while (!stoppingToken.IsCancellationRequested)
            {
                cache = new MemoryCache(new MemoryCacheOptions());
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30)
                };

                await SharedFunctions.GetAsync("Consult/SetLateConsultAppointments", "");
                IEnumerable<Domain.Models.ConsultAppointment> lateConsults = await _repositoryConsult.GetFromQueueLateConsultAppointments();
                cache.Set(cacheKey, lateConsults, cacheOptions);
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }
}