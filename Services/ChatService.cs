using ChatApplication.Models;
using ChatApplication.Repositories.RepositoryInterfaces;
using ChatApplication.Services.ServiceInterfaces;

namespace ChatApplication.Services
{
    public class ChatService : IChatService
    {

        private readonly IChatRepository chatRepository;
        private readonly IUserService userService;

        public ChatService(IChatRepository chatRepository, IUserService userService)
        {
            this.chatRepository = chatRepository;
            this.userService = userService;
        }

        public void Create(int id, int userId)
        {
            User user = userService.ReadById(userId);
            Chat chat = new Chat() { Id = id, Owner = user };
            chatRepository.Сreate(chat);
        }

        public void Delete(int id, int userId)
        {
            Chat chat = chatRepository.ReadById(id);

            if(chat != null) {
                if (chat.OwnerId == userId)
                {
                    chatRepository.Delete(id);
                }
                else
                {
                    throw new ArgumentException($"{userId} doesn`t have such permision!");
                }
            }
            else
            {
                throw new NullReferenceException($"Group {id} doesn`t exist!");
            }  
        }

        public Chat ReadById(int id)
        {
            Chat chat = chatRepository.ReadById(id);
            return chat;
        }
    }
}
