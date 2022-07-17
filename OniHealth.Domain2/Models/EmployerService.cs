using OniHealth.Domain.Interfaces;

namespace OniHealth.Domain.Models
{
    public class EmployerService
    {
        private readonly IRepository<Employer> _employerRepository;

        public EmployerService(IRepository<Employer> employerRepository)
        {
            _employerRepository = employerRepository;
        }

        public void Create(int id, string nome, string email)
        {
            var employer = _employerRepository.GetById(id);

            if (employer == null)
            {
                employer = new Employer(nome, email);
                _employerRepository.Save(employer);
            }
            else
                employer.Update(nome, email);
        }
    }
}