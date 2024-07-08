using System.ComponentModel.DataAnnotations;

namespace ChatApplication.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Chat> Chats { get; set; }

        public User() { }
        public User(string name)
        {
            Name = name;
        }
    }
}
