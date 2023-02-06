using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;
using OniHealth.Domain.Utils;

namespace OniHealth.Domain.Models
{
    public class ConsultTimeService : IConsultTimeService<ConsultTime>
    {
        private readonly IRepository<ConsultTime> _consultTimeRepository;

        public ConsultTimeService(IRepository<ConsultTime> consultTimeRepository)
        {
            _consultTimeRepository = consultTimeRepository;
        }

        public async Task<ConsultTime> CreateAsync(string queueName)
        {
            ConsultTime consult = await SharedFunctions.DequeueAndProcessAsync<ConsultTime>(queueName);

            if (consult == null)
                throw new InsertDatabaseException();

            ConsultTime existentConsultTime = _consultTimeRepository.GetById(consult.Id);

            if (existentConsultTime == null)
                return  await _consultTimeRepository.CreateAsync(consult);

                throw new InsertDatabaseException();
        }

        public ConsultTime Update(ConsultTime consultTime)
        {
            ConsultTime existentConsultTime = _consultTimeRepository.GetById(consultTime.Id);
            ConsultTime updatedConsultTime = new ConsultTime();

            if (existentConsultTime != null)
            {
                updatedConsultTime = _consultTimeRepository.Update(consultTime);
                return updatedConsultTime;
            }
            else
                return null;
        }
    }
}