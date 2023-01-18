using OniHealth.Domain.Enums;
using OniHealth.Domain.Models;
using OniHealth.Infra.Repositories;

namespace OniHealth.Test
{
    public class EmployerTest
    {
        private readonly EmployerService EmployerService;
        private readonly EmployerRepository EmployerRepository;
        public EmployerTest()
        {
            EmployerRepository = new EmployerRepository(new ContextFactory().Context);
            EmployerService = new EmployerService(EmployerRepository);
        }

        [Fact]
        public async void GetByIdAssync()
        {
            int employerId = await EmployerRepository.GetLastId();

            Assert.NotNull(await EmployerRepository.GetByIdAsync(employerId));
        }

        [Fact]
        public async void GetAllAsync()
        {
            Assert.NotEmpty(await EmployerRepository.GetAllAsync());
        }

        [Fact]
        public async void CreateAsync()
        {
            Employer employer = new Employer("Teste Unitário", "Teste@gmail.com", EmployerRole.Worker, 7000, "54996058917", "90050321");
            Assert.NotNull(await EmployerService.CreateAsync(employer));
        }

        [Fact]
        public async void Update()
        {
            int employerId = await EmployerRepository.GetLastId();

            Employer employer = EmployerRepository.GetById(employerId);

            Assert.NotNull(EmployerService.Update(employer));
        }

        [Fact]
        public async void Delete()
        {
            int employerId = await EmployerRepository.GetLastId();

            Assert.NotNull(EmployerService.Delete(employerId));
        }
    }
}