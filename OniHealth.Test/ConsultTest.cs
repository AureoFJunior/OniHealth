using OniHealth.Domain.Enums;
using OniHealth.Domain.Models;
using OniHealth.Infra.Repositories;

namespace OniHealth.Test
{
    public class ConsultTest
    {
        private readonly ConsultService _consultService;
        private readonly ConsultRepository _consultRepository;
        public ConsultTest()
        {
            _consultRepository = new ConsultRepository(new ContextFactory().Context);
            _consultService = new ConsultService(_consultRepository);
        }

        [Fact]
        public async void GetByIdAssync()
        {
            int consultId = await _consultRepository.GetLastId();

            Assert.NotNull(await _consultRepository.GetByIdAsync(consultId));
        }

        [Fact]
        public async void GetAllAsync()
        {
            Assert.NotEmpty(await _consultRepository.GetAllAsync());
        }

        [Fact]
        public async void CreateAsync()
        {
            Consult consult = new Consult("Test consult", true, null, null, null, null, null, null, null);
            Assert.NotNull(await _consultRepository.CreateAsync(consult));
        }

        [Fact]
        public async void Update()
        {
            int consultId = await _consultRepository.GetLastId();

            Consult consult = _consultRepository.GetById(consultId);

            Assert.NotNull(_consultService.Update(consult));
        }

        [Fact]
        public async void Delete()
        {
            int consultId = await _consultRepository.GetLastId();

            Assert.NotNull(_consultService.Delete(consultId));
        }
    }
}
