using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;

namespace OniHealth.Domain.Models
{
    public class ConsultService : IConsultService<Consult>
    {
        private readonly IRepository<Consult> _consultRepository;

        public ConsultService(IRepository<Consult> consultRepository)
        {
            _consultRepository = consultRepository;
        }

        public async Task<Consult> CreateAsync(Consult consult)
        {
            Consult existentConsult = _consultRepository.GetById(consult.Id);
            Consult includedConsult = new Consult();

            if (existentConsult == null)
            {
                includedConsult = await _consultRepository.CreateAsync(consult);
                return includedConsult;
            }
            throw new InsertDatabaseException();
        }

        public Consult Update(Consult consult)
        {
            Consult existentConsult = _consultRepository.GetById(consult.Id);
            Consult updatedConsult = new Consult();

            if (consult != null)
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