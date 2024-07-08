using ChatApplication.Models;

namespace ChatApplication.Repositories.RepositoryInterfaces
{
    public interface IUserRepository
    {
        void Сreate(User user);
        List<User> Read();
        User ReadById(int id);
        void Update(int id, string name);
        void Delete(int id);
    }
}
