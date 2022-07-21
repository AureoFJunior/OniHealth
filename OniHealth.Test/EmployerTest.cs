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
            Employer employer = new Employer("Teste Unitário", "Teste@gmail.com", (short)EmployerRole.Worker);

            Assert.NotNull(await EmployerRepository.CreateAsync(employer));
        }

        [Fact]
        public async void CreateRangeAsync()
        {
            ICollection<Employer> employers = new List<Employer>();
            employers.Add(new Employer("Teste Unitário Inclusão em Lote", "Teste@gmail.com", (short)EmployerRole.Worker));
            employers.Add(new Employer("Teste Unitário Inclusão em Lote 2", "Teste@gmail.com", (short)EmployerRole.Worker));

            Assert.NotEmpty(await EmployerRepository.CreateRangeAsync(employers));
        }

        [Fact]
        public async void Update()
        {
            int employerId = await EmployerRepository.GetLastId();

            Employer employer = EmployerRepository.GetById(employerId);
            employer.Name = "Teste Unitário Atualização";

            Assert.NotNull(EmployerRepository.Update(employer));
        }

        [Fact]
        public async void UpdateRange()
        {
            ICollection<Employer> employers = new List<Employer>();

            int employerId = await EmployerRepository.GetLastId();

            Employer employer = EmployerRepository.GetById(employerId);
            employer.Name = "Teste Unitário Atualização em Lote";

            employers.Add(employer);

            Assert.NotEmpty(EmployerRepository.UpdateRange(employers));
        }

        [Fact]
        public async void Delete()
        {
            int employerId = await EmployerRepository.GetLastId();

            Employer employer = EmployerRepository.GetById(employerId);

            Assert.NotNull(EmployerRepository.Delete(employer));
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