using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO.Ports;
using System.Threading;

//dotnet add package System.IO.Ports --version 5.0.1 -Run in command line before dotnet run to avoid System.IO.Ports issues

namespace serialToUDP
{
       class Program
    {
         static SerialPort _serialPort;

         public static void Main()
        {
            UdpClient client = new UdpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.177"),8080));
            _serialPort = new SerialPort();
             _serialPort.PortName = "/dev/cu.wchusbserial1420";
            _serialPort.BaudRate =115200;
            _serialPort.Open();
            
        while (true)
        {
                string a = _serialPort.ReadExisting();
                Console.WriteLine(a);
                Thread.Sleep(200);
                string inputs = a; 
                
                if (inputs!=null)
                {
                    byte[] bytesent = Encoding.ASCII.GetBytes(inputs);
                    client.Send(bytesent,bytesent.Length);
                    Console.WriteLine("data string sent with success");
                }
            } 
        }
    }
}