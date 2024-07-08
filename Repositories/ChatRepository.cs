using ChatApplication.Data;
using ChatApplication.Models;
using ChatApplication.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Repositories
{
    public class ChatRepository : IChatRepository
    {
        public AppDbContext dbContext { get; set; }
        public ChatRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var chat = dbContext.Chat.FirstOrDefault(a => a.Id == id);

            dbContext.Remove(chat);
            dbContext.SaveChanges();
        }

        public void Сreate(Chat chat)
        {
            dbContext.Add(chat);
            dbContext.SaveChanges();
        }

        public Chat ReadById(int id)
        {
            return dbContext.Chat.Where(c => c.Id == id).Include(c => c.Owner).FirstOrDefault();
        }
    }
}
