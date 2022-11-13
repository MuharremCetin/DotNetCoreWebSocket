using BasicWebSocketApp.WebSocketMiddleware.DTO;
using System.Net.WebSockets;

namespace BasicWebSocketApp.WebSocketMiddleware.Contracts
{
    public interface IWebSocketMessageHandler
    {
        Task onConnected(WebSocketClient client);
        Task onDisconnected(WebSocketClient client);
        Task sendMessageAsync(WebSocketClient client, WebSocketMessage message);
        Task sendMessageAsync(string username, WebSocketMessage message);
        Task sendMessageToAll(WebSocketMessage message);
        Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}
