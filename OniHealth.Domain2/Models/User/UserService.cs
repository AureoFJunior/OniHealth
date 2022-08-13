using OniHealth.Domain.Interfaces;

namespace OniHealth.Domain.Models
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateAsync(User user)
        {
            try
            {
                var existentUser = _userRepository.GetById(user.Id);
                User includedUser = new User();

                if (existentUser == null)
                {
                    includedUser = await _userRepository.CreateAsync(user);
                    return includedUser;
                }

                throw new ConflictDatabaseException("User already exists.");
            }
            catch (Exception ex) { throw ex; }
        }

        public User Update(User user)
        {
            try
            {
                User existentUser = _userRepository.GetById(user.Id);
                User updatedUser = new User();

                if (existentUser != null)
                {
                    updatedUser = _userRepository.Update(user);
                    return updatedUser;
                }

                throw new NotFoundDatabaseException("User don't exists yet.");
            }
            catch (Exception ex) { throw ex; }
        }

        public User Delete(int id)
        {
            try
            {
                User user = _userRepository.GetById(id);
                User deletedUser = new User();

                if (user != null)
                {
                    deletedUser = _userRepository.Delete(user);
                    return deletedUser;
                }
                throw new NotFoundDatabaseException("User don't exists yet.");
            }
            catch (Exception ex) { throw ex; }
        }
    }
}