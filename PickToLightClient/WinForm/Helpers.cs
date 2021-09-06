using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
//using HSM.Embedded.WirelessAssembly;
//using System.Runtime.InteropServices;
//using System.ComponentModel;

namespace SNA.WinForm.PickToLightClient
{
    public class Helpers
    {
        public static void ShowError(string displayString)
        {
            //MessageBox.Show(displayString, "There was an ERROR!!!", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            //Always prepend two empty lines!
            using (ErrorDialog errorDialog = new ErrorDialog("ERROR Encountered!!", "\r\n\r\n" + displayString, false))
            {
                errorDialog.ShowDialog();
            }
        }

        public static string GetDeviceName()
        {
            return(System.Net.Dns.GetHostName());
        }

        //private static Int32 METHOD_BUFFERED = 0;
        //private static Int32 FILE_ANY_ACCESS = 0;
        //private static Int32 FILE_DEVICE_HAL = 0x00000101;

        //private const Int32 ERROR_NOT_SUPPORTED = 0x32;
        //private const Int32 ERROR_INSUFFICIENT_BUFFER = 0x7A;

        //private static Int32 IOCTL_HAL_GET_DEVICEID = 
        //    ((FILE_DEVICE_HAL) << 16) | ((FILE_ANY_ACCESS) << 14) 
        //    | ((21) << 2) | (METHOD_BUFFERED);

        //[DllImport("coredll.dll", SetLastError=true)]
        //private static extern bool KernelIoControl(Int32 dwIoControlCode, 
        //    IntPtr lpInBuf, Int32 nInBufSize, byte[] lpOutBuf, 
        //    Int32 nOutBufSize, ref Int32 lpBytesReturned);

        //public static string GetDeviceID()
        //{
        //    // Initialize the output buffer to the size of a  
        //    // Win32 DEVICE_ID structure. 
        //    byte[] outbuff = new byte[20];
        //    Int32  dwOutBytes;
        //    bool done = false;

        //    Int32 nBuffSize = outbuff.Length;

        //    // Set DEVICEID.dwSize to size of buffer.  Some platforms look at 
        //    // this field rather than the nOutBufSize param of KernelIoControl 
        //    // when determining if the buffer is large enough.
        //    BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0);  
        //    dwOutBytes = 0;

        //    // Loop until the device ID is retrieved or an error occurs. 
        //    while (! done)
        //    {
        //        if (KernelIoControl(IOCTL_HAL_GET_DEVICEID, IntPtr.Zero, 0, outbuff, nBuffSize, ref dwOutBytes))
        //        {
        //            done = true;
        //        }
        //        else
        //        {
        //            int error = Marshal.GetLastWin32Error();
        //            switch (error)
        //            {
        //            case ERROR_NOT_SUPPORTED:
        //                throw new NotSupportedException(
        //                    "IOCTL_HAL_GET_DEVICEID is not supported on this device",
        //                    new Win32Exception(error));

        //            case ERROR_INSUFFICIENT_BUFFER:

        //                // The buffer is not big enough for the data.  The 
        //                // required size is in the first 4 bytes of the output 
        //                // buffer (DEVICE_ID.dwSize).
        //                nBuffSize = BitConverter.ToInt32(outbuff, 0);
        //                outbuff = new byte[nBuffSize];

        //                // Set DEVICEID.dwSize to size of buffer.  Some 
        //                // platforms look at this field rather than the 
        //                // nOutBufSize param of KernelIoControl when 
        //                // determining if the buffer is large enough.
        //                BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0);
        //                break;

        //            default:
        //                throw new Win32Exception(error, "Unexpected error");
        //            }
        //        }
        //    }

        //    // Copy the elements of the DEVICE_ID structure.
        //    Int32 dwPresetIDOffset = BitConverter.ToInt32(outbuff, 0x4);
        //    Int32 dwPresetIDSize = BitConverter.ToInt32(outbuff, 0x8);
        //    Int32 dwPlatformIDOffset = BitConverter.ToInt32(outbuff, 0xc);
        //    Int32 dwPlatformIDSize = BitConverter.ToInt32(outbuff, 0x10);
        //    StringBuilder sb = new StringBuilder();

        //    for (int i = dwPresetIDOffset; 
        //        i < dwPresetIDOffset + dwPresetIDSize; i++)
        //    {
        //        sb.Append(String.Format("{0:X2}", outbuff[i]));
        //    }

        //    sb.Append("-");

        //    for (int i = dwPlatformIDOffset; 
        //        i < dwPlatformIDOffset + dwPlatformIDSize; i ++ )  
        //    {
        //        sb.Append( String.Format("{0:X2}", outbuff[i]));
        //    }
        //    return sb.ToString();
        //}

        public static bool MyInt32TryParse(object parameter, out int value)
        {
            value = 0;
            try
            {
                value = Convert.ToInt32(parameter);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsWIFIConnected()
        {
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            //Console.WriteLine("Interface information for {0}.{1}     ", computerProperties.HostName, computerProperties.DomainName);
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.OperationalStatus == OperationalStatus.Up)
                {
                    return true;
                }
                //IPInterfaceProperties properties = adapter.GetIPProperties();
                //Console.WriteLine(adapter.Description);
                //Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length,'='));
                //Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
                //Console.WriteLine("  Physical Address ........................ : {0}", adapter.GetPhysicalAddress().ToString());
                //Console.WriteLine("  Is receive only.......................... : {0}", adapter.IsReceiveOnly);
                //Console.WriteLine("  Multicast................................ : {0}", adapter.SupportsMulticast);
                //Console.WriteLine();
            }
            return false;

            //try
            //{
            //    using(WirelessManager wm = new WirelessManager())
            //    {
            //        //bool bStatus = false;
            //        //if (WirelessManager.HasWLAN() == true)
            //        //{
            //        //    WirelessManager.GetWLANState(ref bStatus);
            //        //}
            //        //if (bStatus == false) //WLAN Not Enabled, let's enable it!
            //        //{
            //        //    WirelessManager.SetWLANState(true);
            //        //    WirelessManager.GetWLANState(ref bStatus);
            //        //}
            //        //else
            //        //{
            //        //    lblRadioState.Text = WirelessManager.WIFISTATUS.WIFI_CONNECTED.ToString();
            //        //}
                  
            //        //--- If we had to set the radio above, lets make sure it worked ---
            //        //bool currState = false;
            //        //WirelessManager.GetWLANState(ref currState);

            //        //--- If we are connectable, start the timer to update the gui ---
            //        //if (currState)
            //        //    //timer.Enabled = true;
            //        //    return true;
            //        //else
            //        //    //lblErrors.Text = "Unable to set the state of the RF radio.";
            //        //    return false;

            //        //--- Update the GUI ---
            //        //lblRadioState.Text = currState.ToString();
                    
            //        if (WirelessManager.HasWLAN() == false)
            //        {
            //            //MessageBox.Show("IsWIFIConnected() - HasWLAN is FALSE.");
            //            ShowError("\r\n\r\nIsWIFIConnected() - HasWLAN is FALSE.");
            //            return false;
            //        }
            //        bool bStatus = false;
            //        if( WirelessManager.GetWLANState(ref bStatus) != 0 )
            //        {
            //            //MessageBox.Show("IsWIFIConnected() - GetWLANState() FAILED..");
            //            ShowError("\r\n\r\nIsWIFIConnected() - GetWLANState() FAILED..");
            //            return false;
            //        }

            //        if (bStatus == false) //WLAN is ENABLED.
            //        {
            //            //MessageBox.Show("IsWIFIConnected() - GetWLANState() is DISABLED.");
            //            ShowError("\r\n\r\nIsWIFIConnected() - GetWLANState() is DISABLED.");
            //            return false;
            //        }

            //        WirelessManager.WIFISTATUS wifiStatus = new WirelessManager.WIFISTATUS();
            //        if (wm.GetWiFiAdapterStatus(ref wifiStatus)==0)
            //        {
            //            if (wifiStatus.Equals(WirelessManager.WIFISTATUS.WIFI_CONNECTED) || wifiStatus.Equals(WirelessManager.WIFISTATUS.WIFI_AUTHENTICATED))
            //            {
            //                return true;
            //            }
            //            else
            //            {
            //                //MessageBox.Show("IsWIFIConnected() - Status: " + wifiStatus.ToString());
            //                ShowError("\r\n\r\nIsWIFIConnected() - Status: " + wifiStatus.ToString());
            //                return false;
            //            }
            //        }
            //        //MessageBox.Show("IsWIFIConnected() - GetWiFiAdapterStatus() FAILED!");
            //        ShowError("\r\n\r\nIsWIFIConnected() - GetWiFiAdapterStatus() FAILED!");
            //        return false;

            //        //WirelessManager.IP_ADAPTER_INFO info = new WirelessManager.IP_ADAPTER_INFO();
            //        //WireLessManagerM.GetWIFIAdapterIPInfomation(ref info);
            //        //String str2 = Encoding.ASCII.GetString(info.AdapterName, 0, info.AdapterName.Length);
            //        //String str3 = Encoding.ASCII.GetString(info.AdapterDescription, 0, info.AdapterDescription.Length);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message);
            //    ShowError("\r\n" + ex.Message);
            //    return false;
            //}
        }

        //public static string GetWIFIMac()
        //{
        //    string wifiMac = "";
        //    try
        //    {
        //        using(WirelessManager wm = new WirelessManager())
        //        {
        //            byte[] macAddressBytes = wm.GetWiFiAdapterMacAddress();
        //            wifiMac = Encoding.ASCII.GetString(macAddressBytes, 0, macAddressBytes.Length);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message);
        //        ShowError("\r\n" + ex.Message);
        //    }
        //    return wifiMac;
        //}

        /// <summary>
        /// Finds the MAC address of the NIC with maximum speed.
        /// </summary>
        /// <returns>The MAC address.</returns>
        public static string GetWIFIMac()
        {
            const int MIN_MAC_ADDR_LENGTH = 12;
            string macAddress = string.Empty;
            long maxSpeed = -1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                //_logger.Log(
                //    "Found MAC Address: " + nic.GetPhysicalAddress() +
                //    " Type: " + nic.NetworkInterfaceType);

                string tempMac = nic.GetPhysicalAddress().ToString();
                if (nic.Speed > maxSpeed &&
                    !string.IsNullOrEmpty(tempMac) &&
                    tempMac.Length >= MIN_MAC_ADDR_LENGTH)
                {
                    //_logger.Log("New Max Speed = " + nic.Speed + ", MAC: " + tempMac);
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }

            return macAddress;
        }
    }
}
