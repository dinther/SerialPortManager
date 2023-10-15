using System;

namespace SerialPortManagerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialPortManager serialPortManager = new SerialPortManager(0x2E8A);
            serialPortManager.OnPortFoundEvent += SerialPortManager_OnPortFoundEvent;
            serialPortManager.OnPortAddedEvent += SerialPortManager_OnPortAddedEvent;
            serialPortManager.OnPortRemovedEvent += SerialPortManager_OnPortRemovedEvent;
            serialPortManager.scanPorts(true);
            Console.WriteLine("Press CTL C to Exit");
            while (true);
        }

        private static void SerialPortManager_OnPortFoundEvent(object sender, SerialPortEventArgs e)
        {
            Console.WriteLine($"{e.DeviceID} VendorID: {e.VendorID} ProductID: {e.ProductID} Found");
        }

        private static void SerialPortManager_OnPortRemovedEvent(object sender, SerialPortEventArgs e)
        {
            Console.WriteLine($"{e.DeviceID} VendorID: {e.VendorID} ProductID: {e.ProductID} Removed");
        }

        private static void SerialPortManager_OnPortAddedEvent(object sender, SerialPortEventArgs e)
        {
            Console.WriteLine($"{e.DeviceID} VendorID: {e.VendorID} ProductID: {e.ProductID} Added");
        }
    }
}
