using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UsbLibrary;

namespace RocketLauncher.Api
{
    public class Launcher
    {
        private bool _isConnected;
        private bool _isDeviceReady;
        private UsbHidPort _usb;

        public Launcher()
        {
            
        }

        public void Connect()
        {
            var container = new Container();
            _usb = new UsbHidPort(container);
            _usb.ProductId = 0;
            _usb.VendorId = 0;

            _usb.SpecifiedDevice = null;
            _usb.OnSpecifiedDeviceRemoved += OnSpecifiedDeviceRemoved;
            _usb.OnDataRecieved += OnDataRecieved;
            _usb.OnSpecifiedDeviceArrived += OnSpecifiedDeviceArrived;

            _usb.VID_List[0] = 2689;
            _usb.PID_List[0] = 1793;
            _usb.VID_List[1] = 8483;
            _usb.PID_List[1] = 4112;
            _usb.ID_List_Cnt = 2;
            _usb.RegisterHandle(Process.GetCurrentProcess().MainWindowHandle);
            _isConnected = true;
        }

        public void Disconnect()
        {
            
        }

        public void SendCommand(byte[] cmd)
        {
            if (!_isConnected)
            {
                throw new Exception("The Launcher has not connected yet.");
            }

            _usb.SpecifiedDevice.SendData(cmd);
        }

        private void OnSpecifiedDeviceArrived(object sender, EventArgs e)
        {
            _isDeviceReady = true;
        }

        private void OnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
            
        }

        private void OnDataRecieved(object sender, DataRecievedEventArgs args)
        {
            
        }
    }
}
