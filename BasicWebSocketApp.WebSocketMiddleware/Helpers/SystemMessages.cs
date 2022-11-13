using BasicWebSocketApp.WebSocketMiddleware.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebSocketApp.WebSocketMiddleware.Helpers
{
    public class SystemMessages
    {
        public static WebSocketMessage getUserConnectedMessage(string username)
        {
            WebSocketMessage message = new WebSocketMessage()
            {
                data = username + " connected",
                executeFunction = "onConnected",
                fromUsername = username,
                toUsername = "All",
                messagDateTime = DateTime.Now,
                messageType = WebSocketMessageType.Text
            };

            return message;
        }

        public static WebSocketMessage getUserDisconnectedMessage(string username)
        {
            WebSocketMessage message = new WebSocketMessage()
            {
                data = username + " connected",
                executeFunction = "onDisconnected",
                fromUsername = username,
                toUsername = "All",
                messagDateTime = DateTime.Now,
                messageType = WebSocketMessageType.Close
            };

            return message;
        }
    }
}
