namespace ChatApplication.Hubs.HubsInterfaces
{
    public interface INotificationClient
    {
        Task Send(string comment);
    }
}
