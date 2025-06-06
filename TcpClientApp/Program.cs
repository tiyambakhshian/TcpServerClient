using Microsoft.Extensions.DependencyInjection;
using TcpClientApp.Interfaces;
using TcpClientApp.IO;
using TcpClientApp.Services;

//CLIENT
class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddSingleton<IMessageReader, ConsoleMessageReader>();
        services.AddSingleton<IMessageWriter, ConsoleMessageWriter>();


        services.AddSingleton<IClient>(sp =>
            new TcpClientService(
                "127.0.0.1",
                8080,
                sp.GetRequiredService<IMessageReader>(),
                sp.GetRequiredService<IMessageWriter>()
            )
        );

        var serviceProvider = services.BuildServiceProvider();

        var client = serviceProvider.GetRequiredService<IClient>();

        await client.RunAsync();
    }
}

