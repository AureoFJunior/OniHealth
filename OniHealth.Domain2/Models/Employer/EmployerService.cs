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

        public async Task<Employer> CreateAsync(Employer employer)
        {
            Employer existentEmployer = _employerRepository.GetById(employer.Id);
            Employer includedEmployer = new Employer();

            if (existentEmployer == null)
            {
                includedEmployer = await _employerRepository.CreateAsync(employer);
                return includedEmployer;
            }
            else
                return null;
        }

        public Employer Update(Employer employer)
        {
            Employer existentEmployer = _employerRepository.GetById(employer.Id);
            Employer updatedEmployer = new Employer();

            if (employer != null)
            {
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