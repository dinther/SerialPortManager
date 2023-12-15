//  SerialPort manager demo for SerialPortManager library class
//  This simnple demo demonstrates how to make use of the SerialPortManager
//  and handles the "PortAdded", "PortRemoved" and "PortFound" events
//  optionally filter the VendorID and ProductID in the constructor:
//
//  The demo will show in real-time when a serial device is added or removed
//  and will also show all the serial ports that are currently available.
//
//  SerialPortManager serialPortManager = new SerialPortManager(0x2e8a, 0x0722);
//
//  The above ensures that only the ports with the matching VendorID and ProductID
//  are reported.
//
//  Make sure to add project SerialPortManager to your project references.
//  Or add SerialPortManager.dll to your project references
//
//  github https://github.com/dinther/SerialPortManager
//  By Paul van Dinther

SerialPortManager serialPortManager = new SerialPortManager();
serialPortManager.OnPortFoundEvent += SerialPortManager_OnPortFoundEvent;
serialPortManager.OnPortAddedEvent += SerialPortManager_OnPortAddedEvent;
serialPortManager.OnPortRemovedEvent += SerialPortManager_OnPortRemovedEvent;
serialPortManager.scanPorts(true);
Console.WriteLine("Press CTL C to Exit");
while (true) ;

void SerialPortManager_OnPortFoundEvent(object sender, SerialPortEventArgs e)
{
    Console.WriteLine($"{e.DeviceID} VendorID: {e.VendorID} ProductID: {e.ProductID} Found");
}

void SerialPortManager_OnPortRemovedEvent(object sender, SerialPortEventArgs e)
{
    Console.WriteLine($"{e.DeviceID} VendorID: {e.VendorID} ProductID: {e.ProductID} Removed");
}

void SerialPortManager_OnPortAddedEvent(object sender, SerialPortEventArgs e)
{
    Console.WriteLine($"{e.DeviceID} VendorID: {e.VendorID} ProductID: {e.ProductID} Added");
}