using ChatApplication.Models;
using ChatApplication.Repositories;
using ChatApplication.Repositories.RepositoryInterfaces;
using ChatApplication.Services.ServiceInterfaces;

namespace ChatApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Delete(int id)
        {
            userRepository.Delete(id);
        }

        public List<User> Read()
        {
            return userRepository.Read();
        }

        public User ReadById(int id)
        {
            User user = userRepository.ReadById(id);
            return user;
        }

        public void Update(int id, string newName)
        {
            userRepository.Update(id, newName);
        }

        public void Сreate(string name)
        {
            User user = new User(name);
            userRepository.Сreate(user);
        }
    }
}
