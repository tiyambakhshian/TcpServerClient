using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpServerApp.Interfaces;

namespace TcpServerApp.Services
{


    public class ClientHandler : IClientHandler
    {
        private readonly IMessageProcessor _messageProcessor;

        public ClientHandler(IMessageProcessor messageProcessor)
        {
            _messageProcessor = messageProcessor;
        }

        public async Task HandleClientAsync(TcpClient client)
        {
            Console.WriteLine("Client connected.");

            using NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    Console.WriteLine("Client disconnected.");
                    break;
                }

                string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {received}");

                string response = _messageProcessor.Process(received);
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
            }

            client.Close();
        }
    }

}
