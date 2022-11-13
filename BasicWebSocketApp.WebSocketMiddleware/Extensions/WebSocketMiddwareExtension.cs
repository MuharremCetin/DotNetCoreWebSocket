using BasicWebSocketApp.WebSocketMiddleware.Connections;
using BasicWebSocketApp.WebSocketMiddleware.Contracts;
using BasicWebSocketApp.WebSocketMiddleware.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BasicWebSocketApp.WebSocketMiddleware.Extensions
{
    public static class WebSocketMiddwareExtension
    {
        public static IServiceCollection AddWebSocketServices(this IServiceCollection services)
        {
            services.AddTransient<IConnectionManager, ConnectionManager>();

            services.AddSingleton<IWebSocketMessageHandler>((provider) =>
            {
                return new WebSocketMessageHandler(provider.GetService<IConnectionManager>());
            });

            return services;
        }

        public static IApplicationBuilder MapWebSocketMiddleware(this IApplicationBuilder app,
                                                              PathString path,
                                                              IWebSocketMessageHandler? handler)
        {
            return app.Map(path, (_app) => _app.UseMiddleware<WebSocketMiddleware>(handler));
        }
    }
}
