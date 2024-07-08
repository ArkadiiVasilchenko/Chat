using ChatApplication.Models;
using ChatApplication.Services.ServiceInterfaces;

namespace ChatApplication.Services
{
    public class ConnectionManagerService : IConnectionManager
    {
        public Dictionary<string, Dictionary<string, string>> _connections = new Dictionary<string, Dictionary<string, string>>();

        public void AddToGroup(string groupName, string connectionId, string userId)
        {
            if (_connections.TryGetValue(groupName, out var connections))
            {
                connections.Add(connectionId, userId);
            }
            else
            {
                _connections[groupName] = new Dictionary<string, string> { { connectionId, userId } };
            }
        }

        public void RemoveFromGroup(string groupName, string connectionId)
        {
            if(_connections.TryGetValue(groupName, out var connections))
            {
                connections.Remove(connectionId);
            }
            else
            {
                throw new ArgumentException("There ara no connections in this group");
            }
        }

        public Dictionary<string, string> GetConnectionsByGroup(string groupName)
        {

            if (_connections.TryGetValue(groupName, out var connections))
            {
                return connections;
            }
            else
            {
                throw new ArgumentException("There ara no connections in this group");
            }

        }
    }
}
