using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using TcpServerApp.Interfaces;

namespace TcpServerApp.Services
{


    public class ClientHandler : IClientHandler
    {
        private readonly IMessageProcessor _messageProcessor;
        private readonly IHealthCheckSender _healthCheckSender;

        public ClientHandler(IMessageProcessor messageProcessor, IHealthCheckSender healthCheckSender)
        {
            _messageProcessor = messageProcessor;
            _healthCheckSender = healthCheckSender;
        }

        public async Task HandleClientAsync(TcpClient client)
        {
            Console.WriteLine("Client connected.");
            using NetworkStream stream = client.GetStream();
            using CancellationTokenSource cts = new CancellationTokenSource();

            var receiveTask = ReceiveMessagesAsync(stream, cts);
            var healthTask = _healthCheckSender.StartSendingAsync(stream, cts.Token);

            await Task.WhenAny(receiveTask, healthTask);


            cts.Cancel();
            client.Close();
            Console.WriteLine("Client disconnected.");
        }

        private async Task ReceiveMessagesAsync(NetworkStream stream, CancellationTokenSource cts)
        {
            byte[] buffer = new byte[1024];

            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cts.Token);
                    if (bytesRead == 0) break;

                    string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"[Receive] {received}");

                    string response = _messageProcessor.Process(received);
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length, cts.Token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Receive] Error: {ex.Message}");
            }
        }
    }
}
