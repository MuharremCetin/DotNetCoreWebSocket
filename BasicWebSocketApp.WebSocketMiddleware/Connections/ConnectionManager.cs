using BasicWebSocketApp.WebSocketMiddleware.Contracts;
using BasicWebSocketApp.WebSocketMiddleware.DTO;
using System.Collections.Concurrent;

namespace BasicWebSocketApp.WebSocketMiddleware.Connections
{
    public class ConnectionManager : IConnectionManager
    {
        ConcurrentBag<WebSocketClient> clients;
        public ConnectionManager()
        {
            clients = new ConcurrentBag<WebSocketClient>();
        }
        public void addSocketClient(WebSocketClient client)
        {
            clients.Add(client);
        }

        public Task<ConcurrentBag<WebSocketClient>> getAllAsync()
        {
            return Task.FromResult<ConcurrentBag<WebSocketClient>>(clients);
        }

        public WebSocketClient? GetSocketClientByUsername(string username)
        {
            return clients.FirstOrDefault(m => m.username == username);
        }

        public Task<bool> removeSocketClient(WebSocketClient client)
        {
            throw new NotImplementedException();
        }
    }
}
