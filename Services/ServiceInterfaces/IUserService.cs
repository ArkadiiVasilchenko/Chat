using ChatApplication.Models;

namespace ChatApplication.Services.ServiceInterfaces
{
    public interface IUserService
    {
        void Сreate(string name);
        List<User> Read();
        void Update(int id, string newName);
        void Delete(int id);
        User ReadById(int id);
    }
}
