using ChatApplication.Models;

namespace ChatApplication.Services.ServiceInterfaces
{
    public interface IChatService
    {
        void Create(int id, int userId);
        void Delete(int id, int userId);
        Chat ReadById(int id);

    }
}
