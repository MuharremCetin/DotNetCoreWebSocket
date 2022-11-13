using BasicWebSocketApp.WebSocketMiddleware.Contracts;
using BasicWebSocketApp.WebSocketMiddleware.DTO;
using BasicWebSocketApp.WebSocketMiddleware.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.WebSockets;

namespace BasicWebSocketApp.WebSocketMiddleware
{
    public class WebSocketMiddleware
    {
        public readonly RequestDelegate _next;
        private IWebSocketMessageHandler _handler;
        public WebSocketMiddleware(RequestDelegate next, IWebSocketMessageHandler handler)
        {
            _next = next;
            _handler = handler;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                context.Response.StatusCode = 400;
                await _next.Invoke(context);
                return;
            }

            var socket = await context.WebSockets.AcceptWebSocketAsync();
            string username = context.User.Identity?.Name ?? "Unknown";
            WebSocketClient client = new WebSocketClient()
            {
                username = username,
                websocket = socket
            };

            await _handler.onConnected(client);

            await Receive(socket, async (result, buffer) =>
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    await _handler.ReceiveAsync(socket, result, buffer);
                    return;
                }

                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _handler.onDisconnected(client);
                    return;
                }

            });
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                       cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}