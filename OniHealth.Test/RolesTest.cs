using OniHealth.Domain.Models;
using OniHealth.Infra.Repositories;

namespace OniHealth.Test
{
    public class RolesTest
    {
        private readonly RolesService RolesService;
        private readonly RolesRepository RolesRepository;
        public RolesTest()
        {
            RolesRepository = new RolesRepository(new ContextFactory().Context);
            RolesService = new RolesService(RolesRepository);
        }

        [Fact]
        public async void GetByIdAssync()
        {
            int rolesId = await RolesRepository.GetLastId();

            Assert.NotNull(await RolesRepository.GetByIdAsync(rolesId));
        }

        [Fact]
        public async void GetAllAsync()
        {
            Assert.NotEmpty(await RolesRepository.GetAllAsync());
        }

        [Fact]
        public async void CreateAsync()
        {
            Roles roles = new Roles("Teste Unitário");
            Assert.NotNull(await RolesService.CreateAsync(roles));
        }

        [Fact]
        public async void Update()
        {
            int rolesId = await RolesRepository.GetLastId();

            Roles roles = RolesRepository.GetById(rolesId);

            Assert.NotNull(RolesService.Update(roles));
        }

        [Fact]
        public async void Delete()
        {
            int rolesId = await RolesRepository.GetLastId();

            Assert.NotNull(RolesService.Delete(rolesId));
        }
    }
}