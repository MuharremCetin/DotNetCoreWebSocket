using BasicWebSocketApp.WebSocketMiddleware.Contracts;
using BasicWebSocketApp.WebSocketMiddleware.DTO;
using BasicWebSocketApp.WebSocketMiddleware.Extensions;
using BasicWebSocketApp.WebSocketMiddleware.Helpers;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace BasicWebSocketApp.WebSocketMiddleware.Handlers
{
    public class WebSocketMessageHandler : IWebSocketMessageHandler
    {
        private readonly IConnectionManager connectionManager;
        public WebSocketMessageHandler(IConnectionManager manager)
        {
            connectionManager = manager;
        }
        public async Task onConnected(WebSocketClient client)
        {
            connectionManager.addSocketClient(client);
            await sendMessageToAll(SystemMessages.getUserConnectedMessage(client.username));

        }

        public async Task onDisconnected(WebSocketClient client)
        {
            bool result = await connectionManager.removeSocketClient(client);
            if (result)
            {
                await sendMessageToAll(SystemMessages.getUserDisconnectedMessage(client.username));
            }
        }

        public async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            WebSocketMessage? messageObj = new WebSocketMessage().fromJson(message);
            if (messageObj != null)
            {
                string? targetUser = messageObj.toUsername;
                if (targetUser != null)
                {
                    if (targetUser == "All")
                    {
                        await sendMessageToAll(messageObj);
                        return;
                    }

                    WebSocketClient? client = connectionManager.GetSocketClientByUsername(targetUser);
                    if (client != null)
                    {
                        await sendMessageAsync(client, messageObj);
                    }
                }
            }
        }

        public async Task sendMessageAsync(WebSocketClient client, WebSocketMessage message)
        {
            if (client.websocket.State != WebSocketState.Open)
                return;

            string jsonMessage = JsonSerializer.Serialize<WebSocketMessage>(message);

            await client.websocket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(jsonMessage),
                                                                  offset: 0,
                                                                  count: jsonMessage.Length),
                                   messageType: message.messageType,
                                   endOfMessage: true,
                                   cancellationToken: CancellationToken.None);
        }

        public async Task sendMessageAsync(string username, WebSocketMessage message)
        {
            WebSocketClient? client = connectionManager.GetSocketClientByUsername(username);
            if (client != null)
            {
                await sendMessageAsync(client, message);
            }
            
        }

        public async Task sendMessageToAll(WebSocketMessage message)
        {
            ConcurrentBag<WebSocketClient> clients = await connectionManager.getAllAsync();
            Parallel.ForEach(clients, async (client) =>
            {
                if (client.websocket.State == WebSocketState.Open)
                {
                    await sendMessageAsync(client, message);
                }
            });

        }
    }
}
