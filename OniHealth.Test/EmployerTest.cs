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
            Assert.NotNull(await EmployerService.CreateAsync(0, "Teste Unitário", "Teste@gmail.com", EmployerRole.Worker));
        }

        [Fact]
        public async void CreateRangeAsync()
        {
            IEnumerable<Employer> employers = new List<Employer>();
            employers.Append(new Employer("Teste Unitário Inclusão em Lote", "Teste@gmail.com", (short)EmployerRole.Worker));
            employers.Append(new Employer("Teste Unitário Inclusão em Lote 2", "Teste@gmail.com", (short)EmployerRole.Worker));

            Assert.NotEmpty(await EmployerRepository.CreateRangeAsync(employers));
        }

        [Fact]
        public async void Update()
        {
            int employerId = await EmployerRepository.GetLastId();

            Employer employer = EmployerRepository.GetById(employerId);

            Assert.NotNull(EmployerService.Update(employer.Id, "Teste Unitário Atualização", employer.Email, EmployerRole.Intern));
        }

        [Fact]
        public async void UpdateRange()
        {
            IEnumerable<Employer> employers = new List<Employer>();

            int employerId = await EmployerRepository.GetLastId();

            Employer employer = EmployerRepository.GetById(employerId);
            employer.Name = "Teste Unitário Atualização em Lote";

            employers.Append(employer);

            Assert.NotEmpty(EmployerRepository.UpdateRange(employers));
        }

        [Fact]
        public async void Delete()
        {
            int employerId = await EmployerRepository.GetLastId();

            Assert.NotNull(EmployerService.Delete(employerId));
        }

        [Fact]
        public async void DeleteRange()
        {
            ICollection<Employer> employers = new List<Employer>();
            int employerId = await EmployerRepository.GetLastId();

            employers.Add(EmployerRepository.GetById(employerId));

            Assert.NotEmpty(EmployerRepository.DeleteRange(employers));
        }
    }
}