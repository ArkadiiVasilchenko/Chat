using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApplication.Models
{
    public class Chat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }
    }
} 
