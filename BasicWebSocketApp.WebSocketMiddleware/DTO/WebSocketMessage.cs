using System.Net.WebSockets;

namespace BasicWebSocketApp.WebSocketMiddleware.DTO
{
    public class WebSocketMessage 
    {
        public object? data { get; set; }
        public string? fromUsername { get; set; }
        public string? toUsername { get; set; }
        public string? executeFunction { get; set; }
        public DateTime messagDateTime { get; set; }
        public WebSocketMessageType messageType { get; set; }
    }
}
