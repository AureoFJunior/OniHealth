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

        public async Task<Employer> CreateAsync(int id, string nome, string email)
        {
            Employer employer = _employerRepository.GetById(id);
            Employer includedEmployer = new Employer();

            if (employer == null)
            {
                employer = new Employer(nome, email);
                includedEmployer = await _employerRepository.CreateAsync(employer);
                return includedEmployer;
            }
            else
                return null;
        }

        public Employer Update(int id, string nome, string email)
        {
            Employer employer = _employerRepository.GetById(id);
            Employer updatedEmployer = new Employer();

            if (employer != null)
            {
                employer = new Employer(nome, email);
                updatedEmployer = _employerRepository.Update(employer);
                return updatedEmployer;
            }
            else
                return null;
        }

        public Employer Delete(int id)
        {
            Employer employer = _employerRepository.GetById(id);
            Employer deletedEmployer = new Employer();

            if (employer != null)
            {
                deletedEmployer = _employerRepository.Delete(employer);
                return deletedEmployer;
            }
            else
                return null;
        }
    }
}