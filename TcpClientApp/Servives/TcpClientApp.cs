using System.Text;
using System.Net.Sockets;
using TcpClientApp.Interfaces;


namespace TcpClientApp.Services
{
    public class TcpClientService : IClient
        {
            private readonly string _serverIp;
            private readonly int _port;
            private readonly IMessageReader _reader;
            private readonly IMessageWriter _writer;

            public TcpClientService(string serverIp, int port, IMessageReader reader, IMessageWriter writer)
            {
                _serverIp = serverIp;
                _port = port;
                _reader = reader;
                _writer = writer;
            }

            public async Task RunAsync()
            {
                using TcpClient client = new TcpClient();
                await client.ConnectAsync(_serverIp, _port);
                Console.WriteLine($"Connected to {_serverIp}:{_port}");

                using NetworkStream stream = client.GetStream();

                while (true)
                {
                    string? message = _reader.ReadMessage();
                    if (string.IsNullOrEmpty(message)) break;

                    byte[] data = Encoding.UTF8.GetBytes(message);
                    await stream.WriteAsync(data, 0, data.Length);

                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    _writer.WriteMessage(response);
                }

                Console.WriteLine("Disconnected.");
            }
        }
    }



