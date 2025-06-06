using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerApp.Interfaces
{
    public interface IServer
    {
        Task StartAsync();
    }
}

