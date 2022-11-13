using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebSocketApp.WebSocketMiddleware.DTO
{
    public class WebSocketClient
    {
        public string username { get; set; }
        public WebSocket websocket { get; set; }
    }
}
