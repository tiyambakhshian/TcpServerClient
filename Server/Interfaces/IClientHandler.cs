using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerApp.Interfaces
{
    public interface IClientHandler
    {
        Task HandleClientAsync(TcpClient client);
    }
}

