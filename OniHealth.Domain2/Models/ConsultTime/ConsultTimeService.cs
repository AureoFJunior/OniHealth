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
            {
                ConsultTime consultTime = await _consultTimeRepository.CreateAsync(consult);
                await _consultTimeRepository.CommitAsync();
                return consult;
            }
            throw new InsertDatabaseException();
        }

        public ConsultTime Update(ConsultTime consultTime)
        {
            ConsultTime existentConsultTime = _consultTimeRepository.GetById(consultTime.Id);
            ConsultTime updatedConsultTime = new ConsultTime();

            if (existentConsultTime != null)
            {
                updatedConsultTime = _consultTimeRepository.Update(consultTime);
                _consultTimeRepository.Commit();
                return updatedConsultTime;
            }
            else
                return null;
        }

        public ConsultTime Delete(int id)
        {
            ConsultTime consultTime = _consultTimeRepository.GetById(id);
            ConsultTime deletedConsultTime = new ConsultTime();

            if (consultTime != null)
            {
                deletedConsultTime = _consultTimeRepository.Delete(consultTime);
                _consultTimeRepository.Commit();
                return deletedConsultTime;
            }
            else
                return null;
        }
    }
}