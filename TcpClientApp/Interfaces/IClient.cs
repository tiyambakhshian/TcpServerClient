using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpClientApp.Interfaces
{
    public interface IClient
    {
        Task RunAsync();
    }
}
