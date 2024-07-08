using ChatApplication.Repositories.RepositoryInterfaces;
using ChatApplication.Repositories;
using ChatApplication.Services.ServiceInterfaces;
using ChatApplication.Services;

namespace ChatApplication.Extensions
{
    public static class ServicesRegistrationExtension
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChatService, ChatService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddSingleton<IConnectionManager, ConnectionManagerService>();
        }
    }
}
