using System;
using System.Management;

//  SerialPort manager for C# WPF using Windows Management Instrumentation (WMI)
//  This monitor will produce "Port added", "Port Removed" and "Port Found" events
//  and include the portname when an event is raised.
//
//  Make sure to install System.Management in your projects references.
//
//  Start the SerialPortManager with SerialPortManager.scanPorts()
//  Call SerialPortManager.scanPorts(false) if you don't want Added or Removed events
//  after the initial scan.
//
//  You can set the VendorID and / or ProductID to filter for matching USB Virtual com ports.
//
//  The reason for this class is to obtain an accurate report on what serial ports are
//  available. The standard method: System.IO.Ports.Serialport.getportnames() just
//  reads the Registry and suffers from caching lag.
//
//  By Paul van Dinther


public class SerialPortEventArgs : EventArgs
{
    public SerialPortEventArgs(string portName)
    {
        PortName = portName;
    }
    public string PortName;
}

public class SerialPortManager
{
    public event EventHandler<SerialPortEventArgs> OnPortFoundEvent;
    public event EventHandler<SerialPortEventArgs> OnPortAddedEvent;
    public event EventHandler<SerialPortEventArgs> OnPortRemovedEvent;
    static ManagementEventWatcher watchingAddedObject = null;
    static ManagementEventWatcher watchingRemovedObject = null;
    static WqlEventQuery watcherQuery;
    static ManagementScope scope;
    public int VendorID = 0;
    public int ProductID = 0;
    public SerialPortManager(int vendorID = 0, int productID = 0)
    {
        VendorID = vendorID;
        ProductID = productID;
        scope = new ManagementScope("root/CIMV2");
        scope.Options.EnablePrivileges = true;
        AddInsertUSBHandler();
        AddRemoveUSBHandler();
    }

    public void scanPorts(bool watchForChanges = true)
    {
        try
        {
            bool checkID = VendorID + ProductID != 0;
            string queryString = "SELECT DeviceID, PNPDeviceID FROM Win32_SerialPort";
            if (checkID) queryString += " WHERE ";
            if (VendorID != 0) queryString += "PNPDeviceID Like '%VID_" + VendorID.ToString("X4") + "%'";
            if (VendorID != 0 && ProductID != 0) queryString += " AND ";
            if (ProductID != 0) queryString += "PNPDeviceID Like '%PID_" + ProductID.ToString("X4") + "%'";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", queryString);
            foreach (ManagementObject queryObj in searcher.Get())
            {
                  DoPortFoundEvent((string)queryObj["DeviceID"]);
            }
            if (watchForChanges)
            {
                watchingAddedObject.Start();
                watchingRemovedObject.Start();
            }
        }
        catch (ManagementException e)
        {
            Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
        }
    }

    public void stop()
    {
        watchingAddedObject.Stop();
        watchingRemovedObject.Stop();
    }

    private void AddInsertUSBHandler()
    {
        try
        {
            watchingAddedObject = USBWatcherSetUp("__InstanceCreationEvent");
            watchingAddedObject.EventArrived += new EventArrivedEventHandler(HandlePortAdded);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            if (watchingAddedObject != null)
                watchingAddedObject.Stop();
        }
    }

    private void AddRemoveUSBHandler()
    {
        try
        {
            watchingRemovedObject = USBWatcherSetUp("__InstanceDeletionEvent");
            watchingRemovedObject.EventArrived += new EventArrivedEventHandler(HandlePortRemoved);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            if (watchingRemovedObject != null)
                watchingRemovedObject.Stop();
        }
    }

    private ManagementEventWatcher USBWatcherSetUp(string eventType)
    {
        watcherQuery = new WqlEventQuery();
        watcherQuery.EventClassName = eventType;
        watcherQuery.WithinInterval = new TimeSpan(0, 0, 2);
        watcherQuery.Condition = @"TargetInstance ISA 'Win32_SerialPort'";
        return new ManagementEventWatcher(scope, watcherQuery);
    }

    private void HandlePortAdded(object sender, EventArrivedEventArgs e)
    {
        var instance = e.NewEvent.GetPropertyValue("TargetInstance") as ManagementBaseObject;


        bool checkID = VendorID + ProductID != 0;
        if (checkID)
        {
            string PNPDeviceID = (string)instance.GetPropertyValue("PNPDeviceID");
            if ((VendorID==0 || PNPDeviceID.Contains("VID_" + VendorID.ToString("X4"))) &&
                (ProductID == 0 || PNPDeviceID.Contains("VID_" + ProductID.ToString("X4")))){
                DoPortAddedEvent((string)instance.GetPropertyValue("DeviceID"));
            }
        }
        else
        {
            DoPortAddedEvent((string)instance.GetPropertyValue("DeviceID"));
        }
    }

    private void HandlePortRemoved(object sender, EventArrivedEventArgs e)
    {
        var instance = e.NewEvent.GetPropertyValue("TargetInstance") as ManagementBaseObject;
        DoPortRemovedEvent((string)instance.GetPropertyValue("DeviceID"));
    }

    private void DoPortFoundEvent(string portName)
    {
        if (OnPortFoundEvent != null)
        {
            OnPortFoundEvent(this, new SerialPortEventArgs(portName));
        }
    }

    private void DoPortAddedEvent(string portName)
    {
        if (OnPortAddedEvent != null)
        {
            OnPortAddedEvent(this, new SerialPortEventArgs(portName));
        }
    }

    private void DoPortRemovedEvent(string portName)
    {
        if (OnPortRemovedEvent != null)
        {
            OnPortRemovedEvent(this, new SerialPortEventArgs(portName));
        }
    }
}