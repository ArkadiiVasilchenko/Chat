using ChatApplication.Data;
using ChatApplication.Models;
using ChatApplication.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        public AppDbContext dbContext { get; set; }

        public UserRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var user = dbContext.User.FirstOrDefault(a => a.Id == id);
            if (user != null)
            {
                dbContext.Remove(user);
                dbContext.SaveChanges();
            }
        }

        public List<User> Read()
        {
            List<User> users = dbContext.User.ToList();
            return users;
        }

        public User ReadById(int id)
        {
            return dbContext.User.Where(c => c.Id == id).Include(c => c.Chats).FirstOrDefault();
        }

        public void Update(int id, string newName)
        {
            User user = ReadById(id);
            if (user != null)
            {
                user.Name = newName;
                dbContext.SaveChanges();
            }
        }

        public void Сreate(User user)
        {
            dbContext.Add(user);
            dbContext.SaveChanges();
        }
    }
}
