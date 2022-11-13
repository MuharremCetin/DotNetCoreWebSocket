using BasicWebSocketApp.WebSocketMiddleware.DTO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebSocketApp.WebSocketMiddleware.Contracts
{
    public interface IConnectionManager
    {
        WebSocketClient? GetSocketClientByUsername(string username);
        void addSocketClient(WebSocketClient client);
        Task<bool> removeSocketClient(WebSocketClient client);
        Task<ConcurrentBag<WebSocketClient>> getAllAsync();
    }
}
