using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpClientApp.Interfaces;

namespace TcpClientApp.IO
{
    public class ConsoleMessageReader : IMessageReader
    {
        public string? ReadMessage()
        {
            Console.Write("Enter message (or empty to exit): ");
            return Console.ReadLine();
        }
    }

}
