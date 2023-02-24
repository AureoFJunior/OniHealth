using OniHealth.Domain.Models;
using OniHealth.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Test
{
    public class PlansTest
    {
        private readonly PlansService PlanService;
        private readonly PlansRepository PlanRepository;
        public PlansTest()
        {
            PlanRepository = new PlansRepository(new ContextFactory().Context);
            PlanService = new PlansService(PlanRepository);
        }

        [Fact]
        public async void GetByIdAssync()
        {
            int planId = await PlanRepository.GetLastId();

            Assert.NotNull(await PlanRepository.GetByIdAsync(planId));
        }

        [Fact]
        public async void GetAllAsync()
        {
            Assert.NotEmpty(await PlanRepository.GetAllAsync());
        }

        [Fact]
        public async void CreateAsync()
        {
            Plans plan = new Plans("Teste Plan", "Detalhes poggers", 2000, true);
            Assert.NotNull(await PlanService.CreateAsync(plan));
        }

        [Fact]
        public async void Update()
        {
            int planId = await PlanRepository.GetLastId();

            Plans plan = PlanRepository.GetById(planId);

            Assert.NotNull(PlanService.Update(plan));
        }

        [Fact]
        public async void Delete()
        {
            int planId = await PlanRepository.GetLastId();

            Assert.NotNull(PlanService.Delete(planId));
        }
    }
}
