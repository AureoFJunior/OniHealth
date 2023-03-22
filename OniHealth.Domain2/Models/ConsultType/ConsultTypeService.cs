using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;
using OniHealth.Domain.Utils;

namespace OniHealth.Domain.Models
{
    public class ConsultTypeService : IConsultTypeService<ConsultType>
    {
        private readonly IRepository<ConsultType> _consultTypeRepository;

        public ConsultTypeService(IRepository<ConsultType> consultTimeRepository)
        {
            _consultTypeRepository = consultTimeRepository;
        }

        public async Task<ConsultType> CreateAsync(string queueName)
        {
            ConsultType consult = await SharedFunctions.DequeueAndProcessAsync<ConsultType>(queueName);

            if (consult == null)
                throw new InsertDatabaseException();

            ConsultType existentConsultTime = _consultTypeRepository.GetById(consult.Id);

            if (existentConsultTime == null)
            {
                ConsultType consultType = await _consultTypeRepository.CreateAsync(consult);
                await _consultTypeRepository.CommitAsync();
                return consult;
            }

            throw new InsertDatabaseException();
        }

        public ConsultType Update(ConsultType consultType)
        {
            ConsultType existentConsultType = _consultTypeRepository.GetById(consultType.Id);
            ConsultType updatedConsultType = new ConsultType();

            if (existentConsultType != null)
            {
                updatedConsultType = _consultTypeRepository.Update(consultType);
                _consultTypeRepository.Commit();
                return updatedConsultType;
            }
            else
                return null;
        }

        public ConsultType Delete(int id)
        {
            ConsultType consultType = _consultTypeRepository.GetById(id);
            ConsultType deletedConsultType = new ConsultType();

            if (consultType != null)
            {
                deletedConsultType = _consultTypeRepository.Delete(consultType);
                _consultTypeRepository.Commit();
                return deletedConsultType;
            }
            else
                return null;
        }
    }
}