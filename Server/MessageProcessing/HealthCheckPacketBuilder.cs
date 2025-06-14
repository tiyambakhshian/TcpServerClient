using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerApp.MessageProcessing
{
    public static class HealthCheckPacketBuilder
    {
        public static byte[] BuildPacket()
        {
            List<byte> packet = new List<byte>();


            byte start = 165;
            byte type = 161;
            byte size1 = 8;
            byte size2 = 0;
            byte index1 = 0;
            byte index2 = 0;

          
            DateTime now = DateTime.Now;
            byte day = (byte)now.Day;
            byte month = (byte)now.Month;
            byte year = (byte)(now.Year % 100); 
            byte hour = (byte)now.Hour;
            byte minute = (byte)now.Minute;
            byte second = (byte)now.Second;

            byte end = 90;

           
            packet.Add(start);
            packet.Add(type);
            packet.Add(size1);
            packet.Add(size2);
            packet.Add(index1);
            packet.Add(index2);
            packet.Add(day);
            packet.Add(month);
            packet.Add(year);
            packet.Add(hour);
            packet.Add(minute);
            packet.Add(second);

           
            byte checksum = CalculateChecksum(packet.ToArray());
            packet.Add(checksum);

            packet.Add(end);

            return packet.ToArray();
        }

        public static string BuildFormattedPacketString(byte[] packet)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss:fff");
            string byteList = string.Join(", ", packet);
            return $"RX:  [{timestamp}]  {byteList}";
        }

       
        private static byte CalculateChecksum(byte[] data)
        {
            byte checksum = 0;
            foreach (byte b in data)
                checksum ^= b;
            return checksum;
        }
    }
}
