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

        services.AddSingleton<IMessageProcessor, EchoMessageProcessor>();
        services.AddSingleton<IClientHandler, ClientHandler>();

        services.AddSingleton<IServer>(sp =>
            new TcpServer(
                8080,  //port
                sp.GetRequiredService<IClientHandler>()
            )
        );

        var provider = services.BuildServiceProvider();

        var server = provider.GetRequiredService<IServer>();

        await server.StartAsync();
    }
}



