using OniHealth.Domain.Enums;
using OniHealth.Domain.Models;
using OniHealth.Infra.Repositories;

namespace OniHealth.Test
{
    public class ConsultTimeTest
    {
        private readonly ConsultTimeService _consultTimeService;
        private readonly ConsultTimeRepository _consultTimeRepository;
        public ConsultTimeTest()
        {
            _consultTimeRepository = new ConsultTimeRepository(new ContextFactory().Context);
            _consultTimeService = new ConsultTimeService(_consultTimeRepository);
        }

        [Fact]
        public async void GetByIdAssync()
        {
            int consultTimeId = await _consultTimeRepository.GetLastId();

            Assert.NotNull(await _consultTimeRepository.GetByIdAsync(consultTimeId));
        }

        [Fact]
        public async void GetAllAsync()
        {
            Assert.NotEmpty(await _consultTimeRepository.GetAllAsync());
        }

        [Fact]
        public async void CreateAsync()
        {
            ConsultTime consultTime = new ConsultTime(DateTime.Now, DateTime.Now, DateTime.Now);
            Assert.NotNull(await _consultTimeRepository.CreateAsync(consultTime));
        }

        [Fact]
        public async void Update()
        {
            int consultTimeId = await _consultTimeRepository.GetLastId();

            ConsultTime consultTime = _consultTimeRepository.GetById(consultTimeId);

            Assert.NotNull(_consultTimeService.Update(consultTime));
        }

        [Fact]
        public async void Delete()
        {
            int consultTimeId = await _consultTimeRepository.GetLastId();

            Assert.NotNull(_consultTimeService.Delete(consultTimeId));
        }
    }
}
