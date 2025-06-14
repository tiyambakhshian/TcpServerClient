using Microsoft.Extensions.DependencyInjection;
using System;
using TcpServerApp.Interfaces;
using TcpServerApp.MessageProcessing;
using TcpServerApp.Services;
//SERVER

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        // Register dependencies
        services.AddSingleton<IMessageProcessor, EchoMessageProcessor>();
        services.AddSingleton<IHealthCheckSender, HealthCheckSender>(); 
        services.AddSingleton<IClientHandler, ClientHandler>();

        // Register server with injected dependencies
        services.AddSingleton<IServer>(sp =>
            new TcpServer(
                8080,
                sp.GetRequiredService<IClientHandler>()
            )
        );

        var provider = services.BuildServiceProvider();

        var server = provider.GetRequiredService<IServer>();

        Console.WriteLine("Server is starting...");
        await server.StartAsync();
    }
}



