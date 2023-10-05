using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortManagerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialPortManager serialPortManager = new SerialPortManager();
            serialPortManager.OnPortAddedEvent += SerialPortManager_OnPortAddedEvent;
            serialPortManager.OnPortFoundEvent += SerialPortManager_OnPortAddedEvent;
            serialPortManager.OnPortRemovedEvent += SerialPortManager_OnPortRemovedEvent;
            serialPortManager.scanPorts(true);
            Console.WriteLine("Press CTL C to Exit");
            while (true);
        }

        private static void SerialPortManager_OnPortRemovedEvent(object sender, SerialPortEventArgs e)
        {
            Console.WriteLine($"{e.PortName} Removed");
        }

        private static void SerialPortManager_OnPortAddedEvent(object sender, SerialPortEventArgs e)
        {
            Console.WriteLine($"{e.PortName} Added");
        }
    }
}
