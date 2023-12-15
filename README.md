# SerialPortManager for WPF and WinForms

When you work with WinForms or WPF you will be limited to work with the SerialPort API. Although there is the ScanPorts API call, all that does is read the registry with all the caching issues that come along with that. The VendorId and ProductID from the serial USB device are completely hidden.

SerialPortManager seeks to overcome that by using Windows Management Instrumentation (WMI). It returns a deviceID (A port name) which you can then use with the old SerialPort API.

Add the SerialPortManager.cs to your project and create the object like:
```C#
SerialPortManager serialPortManager = new SerialPortManager();
```

or if you are only interested in certain devices like your Arduino or Raspberry Pico you can pass either VendorID and or ProductID in base 10 or hex.
```C#
SerialPortManager serialPortManager = new SerialPortManager(0x2e8a, 0x0722);
```

or just ProductID.
```C#
SerialPortManager serialPortManager = new SerialPortManager(0, 0x0722);
```

or just VendorID here in base 10
```C#
SerialPortManager serialPortManager = new SerialPortManager(11914);
```

To start a port scan and the port monitoring process you call 
```C#
serialPortManager.scan();
```

This starts the port monitoring AND find all the ports currently connected.

If are not interested in changes to the ports available and only want to find the current ports you call:
```C#
serialPortManager.scan(false);
```

This will perform a single port scan and report on each matching port through the `OnPortFoundEvent`

## Complete program

```C#
using System;

namespace SerialPortManagerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialPortManager serialPortManager = new SerialPortManager();
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
```

In most cases you probably handle port found and port added with the same event handler but the differentiation is there if you need it.

![image](https://github.com/dinther/SerialPortManager/assets/1192916/4b4744de-5da8-4bae-9087-b4058a48fbee)


