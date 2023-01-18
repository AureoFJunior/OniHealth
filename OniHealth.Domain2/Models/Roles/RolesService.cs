using OniHealth.Domain.Interfaces.Repositories;

namespace OniHealth.Domain.Models
{
    public class RolesService
    {
        private readonly IRepository<Roles> _employerRepository;

        public RolesService(IRepository<Roles> employerRepository)
        {
            _employerRepository = employerRepository;
        }

        public async Task<Roles> CreateAsync(Roles roles)
        {
            Roles existentRoles = _employerRepository.GetById(roles.Id);
            Roles includedRoles = new Roles();

            if (existentRoles == null)
            {
                includedRoles = await _employerRepository.CreateAsync(roles);
                return includedRoles;
            }
            return null;
        }

        public Roles Update(Roles roles)
        {
            Roles existentRoles = _employerRepository.GetById(roles.Id);
            Roles updatedRoles = new Roles();

            if (roles != null)
            {
                updatedRoles = _employerRepository.Update(roles);
                return updatedRoles;
            }
            return null;
        }

        public Roles Delete(int id)
        {
            Roles roles = _employerRepository.GetById(id);
            Roles deletedRoles = new Roles();

            if (roles != null)
            {
                deletedRoles = _employerRepository.Delete(roles);
                return deletedRoles;
            }
            return null;
        }
    }
}