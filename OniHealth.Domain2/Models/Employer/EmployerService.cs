using OniHealth.Domain.Interfaces;
using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;
using System;

namespace OniHealth.Domain.Models
{
    public class EmployerService : IEmployerService<Employer>
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
                await _employerRepository.CommitAsync();
                return includedEmployer;
            }
            throw new InsertDatabaseException();
        }

        public Employer Update(Employer employer)
        {
            Employer existentEmployer = _employerRepository.GetById(employer.Id);
            Employer updatedEmployer = new Employer();

            if (existentEmployer != null)
            {
                updatedEmployer = _employerRepository.Update(employer);
                _employerRepository.Commit();
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
                _employerRepository.Commit();
                return deletedEmployer;
            }
            else
                return null;
        }
    }
}