using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using FT.src.packets;

namespace FT.src
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                System.Console.WriteLine(port);
            }
            FtConnection ftConnection = new FtConnection(ports[0],2);
            Console.ReadKey();
        }

        
    }
}