using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerApp.Services
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using TcpServerApp.Interfaces;

    public class TcpServer : IServer
    {
        private readonly int _port;
        private readonly IClientHandler _clientHandler;

        public TcpServer(int port, IClientHandler clientHandler)
        {
            _port = port;
            _clientHandler = clientHandler;
        }

        public async Task StartAsync()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            Console.WriteLine($"Server started on port {_port}");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = _clientHandler.HandleClientAsync(client);
            }
        }
    }

}
