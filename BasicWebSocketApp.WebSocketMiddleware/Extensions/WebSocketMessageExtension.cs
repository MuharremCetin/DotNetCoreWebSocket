using BasicWebSocketApp.WebSocketMiddleware.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BasicWebSocketApp.WebSocketMiddleware.Extensions
{
    public static class WebSocketMessageExtension
    {
        public static WebSocketMessage? fromJson(this WebSocketMessage? obj, string message)
        {
            obj = JsonSerializer.Deserialize<WebSocketMessage>(message);
            return obj;
        }
    }
}
