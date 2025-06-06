using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpClientApp.Interfaces;

namespace TcpClientApp.IO
{
    public class ConsoleMessageWriter : IMessageWriter
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine($"Server response: {message}");
        }
    }
}
