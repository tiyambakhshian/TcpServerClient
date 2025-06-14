using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpServerApp.Interfaces;
using TcpServerApp.MessageProcessing;

namespace TcpServerApp.Services
{
    public class HealthCheckSender : IHealthCheckSender
    {
        public async Task StartSendingAsync(NetworkStream stream, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    byte[] packet = HealthCheckPacketBuilder.BuildPacket();
                    await stream.WriteAsync(packet, 0, packet.Length, cancellationToken);

                    
                    string log = HealthCheckPacketBuilder.BuildFormattedPacketString(packet);
                    Console.WriteLine(log);

                    await Task.Delay(2000, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Health check sending stopped.");
            }
        }
    }
}
