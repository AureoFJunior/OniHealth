using OniHealth.Domain.Models;
using OniHealth.Infra.Repositories;

namespace OniHealth.Test
{
    public class UserTest
    {
        private readonly UserService UserService;
        private readonly UserRepository UserRepository;
        public UserTest()
        {
            UserRepository = new UserRepository(new ContextFactory().Context);
            UserService = new UserService(UserRepository);
        }

        [Fact]
        public async void GetByIdAssync()
        {
            int userId = await UserRepository.GetLastId();

            Assert.NotNull(await UserRepository.GetByIdAsync(userId));
        }

        [Fact]
        public async void GetAllAsync()
        {
            Assert.NotEmpty(await UserRepository.GetAllAsync());
        }

        [Fact]
        public async void CreateAsync()
        {
            User user = new User("Teste", "Unitário", "unitarinho", "user@1234", "Teste@gmail.com", new DateTime(1995, 12, 6).ToUniversalTime());
            Assert.NotNull(await UserService.CreateAsync(user));
        }

        [Fact]
        public async void Update()
        {
            int userId = await UserRepository.GetLastId();

            User user = UserRepository.GetById(userId);
            user.LastName = "Unitário Atualização";

            Assert.NotNull(UserService.Update(user));
        }

        [Fact]
        public async void Delete()
        {
            int userId = await UserRepository.GetLastId();

            Assert.NotNull(UserService.Delete(userId));
        }
    }
}