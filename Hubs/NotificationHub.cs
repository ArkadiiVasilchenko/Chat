using ChatApplication.Hubs.HubsInterfaces;
using ChatApplication.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Text.RegularExpressions;

namespace ChatApplication.Hubs
{
    public class NotificationHub : Hub<INotificationClient>
    {
        IChatService chatService;
        IConnectionManager connectionManager;

        public NotificationHub(IConnectionManager connectionManager, IChatService chatService)
        {
            this.connectionManager = connectionManager;
            this.chatService = chatService;
        }

        public async Task SendMessage(string message, string group)
        {
            await Clients.Group(group).Send(message);            
        }

        public override async Task OnConnectedAsync()
        {
            string group = Context.GetHttpContext().Request.Headers["id"].ToString();
            string userId = Context.GetHttpContext().Request.Headers["userId"].ToString();

            Subscribe(group, userId);

            await Clients.Group(group).Send($"{Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string group = Context.GetHttpContext().Request.Headers["id"].ToString();
            string userId = Context.GetHttpContext().Request.Headers["userId"].ToString();

            await Unsubscribe(group, userId, exception);
        }

        public async Task Subscribe(string group, string userId)
        {
            connectionManager.AddToGroup(group, Context.ConnectionId, userId);

            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task Unsubscribe(string group, string userId, Exception? exception)
        {
            var chat = chatService.ReadById(int.Parse(group));

            if (chat.Owner.Id.ToString() == userId)
            {
                Dictionary<string, string> connections = connectionManager.GetConnectionsByGroup(chat.Id.ToString());
                foreach (var c in connections.Keys)
                {
                    
                    await Clients.Group(group).Send($"{c} disconnected");
                    await Groups.RemoveFromGroupAsync(c, group);
                    connectionManager.RemoveFromGroup(group, c);
                    await base.OnDisconnectedAsync(exception);
                }
                
                 chatService.Delete(chat.Id, chat.Owner.Id);
            }
            else
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chat.Id.ToString());
                connectionManager.RemoveFromGroup(chat.Id.ToString(), Context.ConnectionId);
            }
        }

    }
}
