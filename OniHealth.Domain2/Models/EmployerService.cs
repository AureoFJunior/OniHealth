using OniHealth.Domain.Interfaces;

namespace OniHealth.Domain.Models
{
    public class EmployerService
    {
        private readonly IRepository<Employer> _employerRepository;

        public EmployerService(IRepository<Employer> contatoRepository)
        {
            _employerRepository = contatoRepository;
        }

        public void Create(int id, string nome, string email)
        {
            var contato = _employerRepository.GetById(id);

            if (contato == null)
            {
                contato = new Employer(nome, email);
                _employerRepository.Save(contato);
            }
            else
                contato.Update(nome, email);
        }
    }
}