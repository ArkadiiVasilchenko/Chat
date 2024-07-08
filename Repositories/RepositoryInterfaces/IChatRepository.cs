using ChatApplication.Models;

namespace ChatApplication.Repositories.RepositoryInterfaces
{
    public interface IChatRepository
    {
        Chat ReadById(int id);
        void Сreate(Chat chat);
        void Delete(int id);
    }
}
