using OniHealth.Domain.Enums;
using OniHealth.Domain.Models;
using OniHealth.Infra.Repositories;

namespace OniHealth.Test
{
    public class ConsultTypeTest
    {
        private readonly ConsultTypeService _consultTypeService;
        private readonly ConsultTypeRepository _consultTypeRepository;
        public ConsultTypeTest()
        {
            _consultTypeRepository = new ConsultTypeRepository(new ContextFactory().Context);
            _consultTypeService = new ConsultTypeService(_consultTypeRepository);
        }

        [Fact]
        public async void GetByIdAssync()
        {
            int consultTypeId = await _consultTypeRepository.GetLastId();

            Assert.NotNull(await _consultTypeRepository.GetByIdAsync(consultTypeId));
        }

        [Fact]
        public async void GetAllAsync()
        {
            Assert.NotEmpty(await _consultTypeRepository.GetAllAsync());
        }

        [Fact]
        public async void CreateAsync()
        {
            ConsultType consultType = new ConsultType("Test", "Teste consult type");
            Assert.NotNull(await _consultTypeRepository.CreateAsync(consultType));
        }

        [Fact]
        public async void Update()
        {
            int consultTypeId = await _consultTypeRepository.GetLastId();

            ConsultType consultType = _consultTypeRepository.GetById(consultTypeId);

            Assert.NotNull(_consultTypeService.Update(consultType));
        }

        [Fact]
        public async void Delete()
        {
            int consultTypeId = await _consultTypeRepository.GetLastId();

            Assert.NotNull(_consultTypeService.Delete(consultTypeId));
        }
    }
}
