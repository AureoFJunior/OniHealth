using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;
using OniHealth.Domain.Utils;

namespace OniHealth.Domain.Models
{
    public class ConsultService : IConsultService<Consult>
    {
        private readonly IRepository<Consult> _consultRepository;

        public ConsultService(IRepository<Consult> consultRepository)
        {
            _consultRepository = consultRepository;
        }

        public async Task CreateAsync(string queueName)
        {
            while(true)
            {
                Consult consult = await SharedFunctions.DequeueAndProcessAsync<Consult>(queueName);

                if (consult == null)
                    return;

                Consult existentConsult = _consultRepository.GetById(consult.Id);

                if (existentConsult == null)
                {
                    await _consultRepository.CreateAsync(consult);
                }
                else
                {
                    throw new InsertDatabaseException();
                }
            }
        }

        public Consult Update(Consult consult)
        {
            Consult existentConsult = _consultRepository.GetById(consult.Id);
            Consult updatedConsult = new Consult();

            if (existentConsult != null)
            {
                updatedConsult = _consultRepository.Update(consult);
                return updatedConsult;
            }
            else
                return null;
        }

        public Consult Delete(int id)
        {
            Consult consult = _consultRepository.GetById(id);
            Consult deletedConsult = new Consult();

            if (consult != null)
            {
                deletedConsult = _consultRepository.Delete(consult);
                return deletedConsult;
            }
            else
                return null;
        }
    }
}