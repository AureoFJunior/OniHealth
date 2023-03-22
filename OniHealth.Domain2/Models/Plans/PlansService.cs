using OniHealth.Domain.Interfaces.Repositories;
using OniHealth.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Domain.Models
{
    public class PlansService : IPlanService<Plans>
    {
        private readonly IRepository<Plans> _customerRepository;

        public PlansService(IRepository<Plans> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Plans> CreateAsync(Plans plan)
        {
            Plans existentPlan = _customerRepository.GetById(plan.Id);
            Plans includedPlan = new Plans();

            if (existentPlan == null)
            {
                includedPlan = await _customerRepository.CreateAsync(plan);
                await _customerRepository.CommitAsync();
                return includedPlan;
            }

            throw new InsertDatabaseException();
        }

        public Plans Update(Plans plan)
        {
            Plans existentPlan = _customerRepository.GetById(plan.Id);
            Plans updatedPlan = new Plans();

            if (existentPlan != null)
            {
                updatedPlan = _customerRepository.Update(plan);
                _customerRepository.Commit();
                return updatedPlan;
            }
            else
                return null;
        }

        public Plans Delete(int id)
        {
            Plans plan = _customerRepository.GetById(id);
            Plans deletedPlan = new Plans();

            if (plan != null)
            {
                deletedPlan = _customerRepository.Delete(plan);
                _customerRepository.Commit();
                return deletedPlan;
            }
            else
                return null;
        }

    }
}
