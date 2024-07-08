namespace ChatApplication.Services.ServiceInterfaces
{
    public interface IConnectionManager
    {
        void AddToGroup(string groupName, string connectionId, string userId);
        void RemoveFromGroup(string groupName, string connectionId);
        Dictionary<string, string> GetConnectionsByGroup(string groupName);
    }
}
