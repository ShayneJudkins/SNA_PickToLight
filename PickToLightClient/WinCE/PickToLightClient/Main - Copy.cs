using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HSM.Embedded.Decoding;
using System.Net;
using System.Data.SqlClient;

namespace SNA.Mobile.PickToLightClient
{
    public partial class Main : Form
    {
        private static Logger _logger;

        DecodeAssembly _DecodeAssembly;
        int _ScanKey = 42; //Code for Scan Key on Dolphin (All Windows Mobile??) is 42

        public enum WhatToScan { ManifestBegin, InternalKanban, ToyotaOWK, ManifestEnd };
        WhatToScan _WhatToScan = WhatToScan.ManifestBegin; //Initialize "ManifestBegin", because the user needs to scan a Manifest to start (or continue) a kit!

        public enum WhatWasScanned { UnKnown, Manifest, InternalKanban, ToyotaOWK2D, ToyotaOWK1D };
        WhatWasScanned _WhatWasScanned = WhatWasScanned.UnKnown;

        public class ServerCommandResponse
        {
            public WhatToScan NowScanThis { get; set; }
            public bool ShowErrorDialog { get; set; }
            public string UserInstructions { get; set; }
            public string ErrorMessage { get; set; }
            public string ProgramOutput { get; set; }
        };
        ServerCommandResponse _serverCommandResponse = new ServerCommandResponse();

        const int iGS = 29; //ASCII Group Separator
        const char cGS = (char)29;
        const byte bGS = 0x1D;
        const string sGS = "\x1D";
        string[] saGS = { "\x1D" };
        char[] caGS = { (char)29 };

        //TODO: READ THESE AS "app.config" settings!!!
        static string hq_sptest01 = "10.1.8.23";
        static string _pickToLightServerIP = hq_sptest01;
        //static string _shayneHomePickToLightServerIP = "192.168.1.4";
        //static string _shayneHomeTestDeviceName = "HomeTest";
        //static string _pickToLightServerIP = _shayneHomePickToLightServerIP;
        //TODO: Use Production Server
        //static string GT_PickToLight = "10.22.1.50";
        //static string _pickToLightServerIP = GT_PickToLight;

        //string gt_PickToLight = "X.X.X.X";
        IPAddress _pickToLightServerIPAddress = null;
        //The port number for the remote device.  
        private int _port = 11000;

        //TODO: Populate these from Device???
        string _appName = "PickToLightClient";
        string _deviceName = "TestingHoneywell";
        string _deviceIdentifier = "FillWithDeviceID";
        string _userName = "Test User";

        public const string P2L_COMMAND_ScanManifest = "SM";
        public const string P2L_COMMAND_Ping = "PING";  //Test communication.

        //public class Manifest
        //{
        //    //public string namc;
        //    //public string ourSupplierCode;
        //    //public string dockCode;
        //    //public string orderNumber;
        //    //public string pickUpTruckRoute;
        //    //public string palletizationCode;
        //    //public string orderSequence;
        //    //public string scannedManifest;
        //    string Barcode;
        //    string OrderNumber;
        //    string NAMC;
        //    string SupplierCode;
        //    string DockCode;
        //    string MainRoute;
        //    string SupplierShipDock;
        //    string PalletizationCode;
        //    string OrderSequence;
        //    string DeviceName;
        //    string DeviceIdentifier;
        //    System.DateTime Scanned;
        //    string ScannedBy;
        //    System.DateTime Created;
        //    string CreatedBy;
        //    bool Decoded;
        //    string ImagePath;
        //};
        //Manifest _manifest = new Manifest();

        //public class InternalKanban
        //{
        //    //public string namc;
        //    //public string ourSupplierCode;
        //    //public string dockCode;
        //    //public string orderNumber;
        //    //public string pickUpTruckRoute;
        //    //public string palletizationCode;
        //    //public string orderSequence;
        //    public string scannedInternalKanban;
        //};
        //InternalKanban _InternalKanban = new InternalKanban();

        public Main()
        {
            try
            {
                InitializeComponent();

                //Clear the Error Message
                lblErrorMessage.Text = String.Empty;

                //Clear the Output
                txtOutput.Text = String.Empty;
                //txtOutput.Text = string.Concat("Test Outputting Multiple Lines:"
                //                        ,"\r\n          Line1"
                //                        ,"\r\n          Line2");

                _deviceName = Helpers.GetDeviceName();
                _deviceIdentifier = _deviceName;
                
                //--- Instantiate and Setup the Decode Assembly ---
                //SetupScanner(DecodeAssembly_DecodeEvent);
                SetupScanner();



                ////Check for "Home Test"
                //if (_pickToLightServerIP.Equals(_shayneHomePickToLightServerIP))
                //{
                //    _deviceName = _shayneHomeTestDeviceName;
                //}



                //Load Some Settings from somewhere!
                _pickToLightServerIPAddress = System.Net.IPAddress.Parse(_pickToLightServerIP);
                _logger = new Logger(_appName, _deviceName);
                _logger.LogMode = Logger.LogModes.SQLLog;
                
                //Code/Method to determine what we need to Scan next (first) and Inform the User.
                if (_WhatToScan.Equals(WhatToScan.ManifestBegin)) //This is kind of useless, but to make a point...
                {
                    lblUserInstructions.Text = "Scan the Manifest to start a new (or continue an old) kit.";
                }

                txtOutput.Text = txtOutput.Text + "Pinging Server...";
                if (SendPingCommand())
                {
                    txtOutput.Text = txtOutput.Text + "Server Pinged!";
                }
                else
                {
                    lblErrorMessage.Text = "ERROR! Unable to \"Ping\" the PickToLightServer (" + _pickToLightServerIPAddress.ToString() + ").";
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Helpers.ShowError("Exception in Main(). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")");
                }
                else
                {
                    //LogToEventLog("Exception in Logger.LogToSQL() _serviceName (" + _serviceName + "), Thread.CurrentThread.Name (" + Thread.CurrentThread.Name + "), Thread.CurrentThread.ManagedThreadId (" + Thread.CurrentThread.ManagedThreadId + "), stringToLog(" + stringToLog + "). Exception: " + ex.Message, true);
                    Helpers.ShowError("Exception in Main(). Exception: " + ex.Message);
                }
            }
        }

        public void SetupScanner()
        {

            //Catch Exceptions in "MAIN"
            //
            ////---------------//
            ////- Constructor -//
            ////---------------//
            //try
            //{
            //    //--- Instantiate the Decode Assembly ---
            //    _DecodeAssembly = new DecodeAssembly();
            //    _DecodeAssembly.Connect();

            //    //--- Add a Handler for the Decode Event ---
            //    _DecodeAssembly.DecodeEvent += new DecodeAssembly.DecodeEventHandler(DecodeAssembly_DecodeEvent);
            //    _DecodeAssembly.SetDecoderProperty(DecodeAssembly.LinearCodes.ISBT128Enabled, 1);
            //    _DecodeAssembly.SetDecoderProperty(DecodeAssembly.MatrixCodes.PDF417Enabled, 1);
            //    _DecodeAssembly.SetDecoderProperty(DecodeAssembly.LinearCodes.GS1_128Enabled, 1);
            //    _DecodeAssembly.SetDecoderProperty(DecodeAssembly.LinearCodes.Code39Enabled, 1);
            //    _DecodeAssembly.SetDecoderProperty(DecodeAssembly.LinearCodes.CodabarEnabled, 1);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //--- Instantiate the Decode Assembly ---
            _DecodeAssembly = new DecodeAssembly();
            _DecodeAssembly.Connect();

            //--- Add a Handler for the Decode Event ---
            _DecodeAssembly.DecodeEvent += new DecodeAssembly.DecodeEventHandler(DecodeAssembly_DecodeEvent);
            _DecodeAssembly.SetDecoderProperty(DecodeAssembly.LinearCodes.ISBT128Enabled, 1);
            _DecodeAssembly.SetDecoderProperty(DecodeAssembly.MatrixCodes.PDF417Enabled, 1);
            _DecodeAssembly.SetDecoderProperty(DecodeAssembly.LinearCodes.GS1_128Enabled, 1);
            _DecodeAssembly.SetDecoderProperty(DecodeAssembly.LinearCodes.Code39Enabled, 1);
            _DecodeAssembly.SetDecoderProperty(DecodeAssembly.LinearCodes.CodabarEnabled, 1);
        }

        #region Scan Key Events

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            //======================================================================================
            //=== Note: In order to capture key events, set the Form.KeyPreview Property to True ===
            //======================================================================================
            try
            {
                //--------------------------
                //--- PDT Key Down Codes ---
                //---   ENTER Key = 13   ---
                //---   SCAN Key  = 42   ---
                //--------------------------
                if (e.KeyCode == (Keys)_ScanKey)
                {
                    //--- Remove the KeyDown Event Handler (Add back in KeyUp Event after it has been released.) ---
                    //+++++++++++++++++++++++++++++++++++++IS THIS NEEDED? CHECK IN DEBUG!++++++++++++++++++++++++++++
                    //+++++++++++++++++++++++++++++++++++++IS THIS NEEDED? CHECK IN DEBUG!++++++++++++++++++++++++++++
                    //+++++++++++++++++++++++++++++++++++++IS THIS NEEDED? CHECK IN DEBUG!++++++++++++++++++++++++++++
                    this.KeyDown -= new KeyEventHandler(Main_KeyDown);

                    //--- Clear the Output ---
                    txtOutput.Text = String.Empty;

                    //--- Perform an Asynchronous Decode Operation ---
                    _DecodeAssembly.ScanBarcode();

                    //--- The Key was Handled ---
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Helpers.ShowError("Exception in Main_KeyDown(). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")");
                }
                else
                {
                    //LogToEventLog("Exception in Logger.LogToSQL() _serviceName (" + _serviceName + "), Thread.CurrentThread.Name (" + Thread.CurrentThread.Name + "), Thread.CurrentThread.ManagedThreadId (" + Thread.CurrentThread.ManagedThreadId + "), stringToLog(" + stringToLog + "). Exception: " + ex.Message, true);
                    Helpers.ShowError("Exception in Main_KeyDown(). Exception: " + ex.Message);
                }
            }

        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == (Keys)_ScanKey)
                {
                    //--- If Still Trying to Decode, Cancel the Operation ---
                    _DecodeAssembly.CancelScanBarcode();

                    //--- Add the KeyDown Event Handler ---
                    //+++++++++++++++++++++++++++++++++++++IS THIS NEEDED? CHECK IN DEBUG!++++++++++++++++++++++++++++
                    //+++++++++++++++++++++++++++++++++++++IS THIS NEEDED? CHECK IN DEBUG!++++++++++++++++++++++++++++
                    //+++++++++++++++++++++++++++++++++++++IS THIS NEEDED? CHECK IN DEBUG!++++++++++++++++++++++++++++
                    this.KeyDown += new KeyEventHandler(Main_KeyDown);

                    //--- The Key was Handled ---
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Helpers.ShowError("Exception in Main_KeyUp(). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")");
                }
                else
                {
                    //LogToEventLog("Exception in Logger.LogToSQL() _serviceName (" + _serviceName + "), Thread.CurrentThread.Name (" + Thread.CurrentThread.Name + "), Thread.CurrentThread.ManagedThreadId (" + Thread.CurrentThread.ManagedThreadId + "), stringToLog(" + stringToLog + "). Exception: " + ex.Message, true);
                    Helpers.ShowError("Exception in Main_KeyUp(). Exception: " + ex.Message);
                }
            }

        }

        #endregion

        #region Menu Events

        private void mnuitmFileExit_Click(object sender, EventArgs e)
        {
            //============================
            //=== Exit the Application ===
            //============================
            try
            {
                //--- Release the Decode Assembly ---
                _DecodeAssembly.Dispose();

                //--- Exit the Application ---
                Application.Exit();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Helpers.ShowError("Exception in mnuitmFileExit_Click(). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")");
                }
                else
                {
                    //LogToEventLog("Exception in Logger.LogToSQL() _serviceName (" + _serviceName + "), Thread.CurrentThread.Name (" + Thread.CurrentThread.Name + "), Thread.CurrentThread.ManagedThreadId (" + Thread.CurrentThread.ManagedThreadId + "), stringToLog(" + stringToLog + "). Exception: " + ex.Message, true);
                    Helpers.ShowError("Exception in mnuitmFileExit_Click(). Exception: " + ex.Message);
                }
            }
        }

        #endregion

        //This isn't finished, but lets move the "Smarts" to the "PickToLightServer"
        //private void DecodeAssembly_DecodeEventSmartsInClient_NotFinished(object sender, DecodeAssembly.DecodeEventArgs e)
        //{

        //    //MessageBox.Show("ResultCode: " + e.ResultCode.ToString() + " Length: " + e.Length.ToString() + " Barcode: " + e.Message);
        //    _logger.Log("ResultCode: " + e.ResultCode.ToString() + " Length: " + e.Length.ToString() + " Barcode: " + e.Message);

        //    //=====================================
        //    //=== Process the Decode Event Data ===
        //    //=====================================
        //    try
        //    {
        //        //--- Was the Decode Attempt Successful? ---
        //        if (e.ResultCode == DecodeAssembly.ResultCodes.Success)
        //        {
        //            //Parse the scan
        //            if (_WhatToScan.Equals(WhatToScan.ManifestBegin) || _WhatToScan.Equals(WhatToScan.ManifestEnd))
        //            {
        //                ManifestSQL manifest = new ManifestSQL();
        //                if (ParseManifestBarcode(e.Message, manifest))
        //                {
        //                    //txtOutput.Text = parsedManifest;
        //                    if (_WhatToScan.Equals(WhatToScan.ManifestBegin))
        //                    {
        //                        //if (ProcessManifestBegin(e.Message, _Manifest))
        //                        //{
        //                        //    _WhatToScan = WhatToScan.InternalKanban;
        //                        //    lblUserInstructions.Text = "Scan Internal Kanban";
        //                        //}
        //                        //else
        //                        //{
        //                        //    lblErrorMessage.Text = "ERROR Processing " + _WhatToScan;
        //                        //}
        //                        ProcessManifestBegin(e.Message, manifest);
        //                    }
        //                    else if (_WhatToScan.Equals(WhatToScan.ManifestEnd)) //ManifestEnd or strange error!!!!
        //                    {
        //                        if (ProcessManifestEnd(e.Message, manifest))
        //                        {
        //                            _WhatToScan = WhatToScan.ManifestBegin;
        //                            lblUserInstructions.Text = "Scan another Manifest to start a new kit.";
        //                        }
        //                        else
        //                        {
        //                            lblErrorMessage.Text = "ERROR Processing " + _WhatToScan;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Helpers.ShowError("Adjust Source Code! ERROR: Expecting ManifestBegin or ManifestEnd!!!");
        //                    }
        //                }
        //                else
        //                {
        //                    //TODO: Check to see if they scanned one of the KanBan's instead and let them know its "Manifest Time".
        //                    //TODO: But.. what if the don't have the Maifest? (They would at least have the "End" copy of the Manifest, wouldn't they???
        //                    lblErrorMessage.Text = "Error Parsing Manifest Scan";
        //                    txtOutput.Text = string.Empty;
        //                }
        //            }
        //            else if (_WhatToScan.Equals(WhatToScan.InternalKanban))
        //            {
        //                if (ParseInternalKanban(e.Message, _InternalKanban))
        //                {
        //                    if (ProcessInternalKanban(_InternalKanban))
        //                    {
        //                        _WhatToScan = WhatToScan.ToyotaOWK;
        //                        lblUserInstructions.Text = "Scan Toyota 'One Way Kanban' (OWK)";
        //                    }
        //                    else
        //                    {
        //                        lblErrorMessage.Text = "ERROR Processing " + _WhatToScan;
        //                    }
        //                }
        //                else
        //                {
        //                    lblErrorMessage.Text = "Error Parsing Internal Kanban";
        //                    txtOutput.Text = string.Empty;
        //                }
        //            }
        //            else
        //            {
        //                Helpers.ShowError(_WhatToScan + " hasn't been implemented yet!!");
        //            }

        //        }
        //        else
        //        {
        //            if (e.DecodeException != null)
        //            {
        //                //--- Async Decode Exception ---
        //                switch (e.DecodeException.ResultCode)
        //                {
        //                    case DecodeAssembly.ResultCodes.Cancel:            // Async Decode was Canceled
        //                        return;
        //                    case DecodeAssembly.ResultCodes.NoDecode:          // Scan Timeout
        //                        //MessageBox.Show("Scan Timeout Exceeded");
        //                        Helpers.ShowError("Scan Timeout Exceeded");
        //                        break;
        //                    default:
        //                        //MessageBox.Show(e.DecodeException.Message);
        //                        Helpers.ShowError("Scan Error:" + e.DecodeException.Message);
        //                        break;
        //                }
        //            }
        //            else
        //            {
        //                //--- Generic Async Exception ---
        //                //MessageBox.Show(e.Exception.Message);
        //                Helpers.ShowError("Exception while Scanning:" + e.Exception.Message);
        //            }
        //            //--- Add the Scan Key Event Handler ---
        //            //+++++++++++++++++++++++++++++++++++++IS THIS NEEDED? CHECK IN DEBUG!++++++++++++++++++++++++++++
        //            //+++++++++++++++++++++++++++++++++++++IS THIS NEEDED? CHECK IN DEBUG!++++++++++++++++++++++++++++
        //            //+++++++++++++++++++++++++++++++++++++IS THIS NEEDED? CHECK IN DEBUG!++++++++++++++++++++++++++++
        //            this.KeyDown += new KeyEventHandler(Main_KeyDown);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException != null)
        //        {
        //            Helpers.ShowError("Exception in DecodeAssembly_DecodeEvent(). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")");
        //        }
        //        else
        //        {
        //            //LogToEventLog("Exception in Logger.LogToSQL() _serviceName (" + _serviceName + "), Thread.CurrentThread.Name (" + Thread.CurrentThread.Name + "), Thread.CurrentThread.ManagedThreadId (" + Thread.CurrentThread.ManagedThreadId + "), stringToLog(" + stringToLog + "). Exception: " + ex.Message, true);
        //            Helpers.ShowError("Exception in DecodeAssembly_DecodeEvent(). Exception: " + ex.Message);
        //        }
        //    }
        //}

        private void DecodeAssembly_DecodeEvent(object sender, DecodeAssembly.DecodeEventArgs e)
        {

            //MessageBox.Show("ResultCode: " + e.ResultCode.ToString() + " Length: " + e.Length.ToString() + " Barcode: " + e.Message);
            _logger.Log("ResultCode: " + e.ResultCode.ToString() + " Length: " + e.Length.ToString() + " Barcode: " + e.Message);

            //=====================================
            //=== Process the Decode Event Data ===
            //=====================================
            try
            {
                //--- Was the Decode Attempt Successful? ---
                if (e.ResultCode == DecodeAssembly.ResultCodes.Success)
                {
                    //Determine WhatWhatScanned
                    _WhatWasScanned = WhichBarcode(e.Message);

                    _logger.Log("WhatWasScanned =  " + _WhatWasScanned.ToString());
                    MessageBox.Show("WhatWasScanned =  " + _WhatWasScanned.ToString());

                    //TODO: If we are looking for a "new" KanBan (Internal or Toyota) and the user scans a manifest,
                    //TOTO: just ask the user if this is the "End Manifest"!
                    //TODO: Actually, this decision should be made on the Server!! Right??? Or Here??

                    if (_WhatWasScanned.Equals(WhatWasScanned.Manifest) && (_WhatToScan.Equals(WhatToScan.ManifestBegin) || _WhatToScan.Equals(WhatToScan.ManifestEnd)))
                    {
                        ManifestSQL manifest = new ManifestSQL();
                        if (ParseManifestBarcode(e.Message, manifest))
                        {
                            int manifestID = ProcessManifestBegin(e.Message, manifest);

                            //Send the ManifestScanned Command to the PickToLightServer and get the "WhatToScan".
                            //_WhatToScan = SendManifestCommand(manifest);
                            _serverCommandResponse = SendManifestCommand(manifest, manifestID);
                            lblUserInstructions.Text = _serverCommandResponse.UserInstructions;
                            _WhatToScan = _serverCommandResponse.NowScanThis;
                            if (_serverCommandResponse.ErrorMessage.Length > 0)
                            {
                                lblErrorMessage.Text = _serverCommandResponse.ErrorMessage;
                            }
                            if (_serverCommandResponse.ShowErrorDialog)
                            {
                                Helpers.ShowError(lblErrorMessage.Text);
                            }
                        }
                        else
                        {
                            //TODO: Check to see if they scanned one of the KanBan's instead and let them know its "Manifest Time".
                            //TODO: But.. what if the don't have the Maifest? (They would at least have the "End" copy of the Manifest, wouldn't they???
                            lblErrorMessage.Text = "Error Parsing Manifest Scan";
                            txtOutput.Text = string.Empty;
                        }
                    }
                    else
                    {
                        lblErrorMessage.Text = "You scanned " + _WhatWasScanned.ToString() + " but should have Scanned " + _WhatToScan.ToString() + "!!!";
                        Helpers.ShowError(lblErrorMessage.Text);
                    }

                    //if (_WhatToScan.Equals(WhatToScan.ManifestBegin) || _WhatToScan.Equals(WhatToScan.ManifestEnd))
                    //{
                    //    ManifestSQL manifest = new ManifestSQL();
                    //    if (ParseManifestBarcode(e.Message, manifest))
                    //    {
                    //        //txtOutput.Text = parsedManifest;
                    //        if (_WhatToScan.Equals(WhatToScan.ManifestBegin))
                    //        {
                    //            //if (ProcessManifestBegin(e.Message, _Manifest))
                    //            //{
                    //            //    _WhatToScan = WhatToScan.InternalKanban;
                    //            //    lblUserInstructions.Text = "Scan Internal Kanban";
                    //            //}
                    //            //else
                    //            //{
                    //            //    lblErrorMessage.Text = "ERROR Processing " + _WhatToScan;
                    //            //}
                    //            ProcessManifestBegin(e.Message, manifest);
                    //        }
                    //        else if (_WhatToScan.Equals(WhatToScan.ManifestEnd)) //ManifestEnd or strange error!!!!
                    //        {
                    //            if (ProcessManifestEnd(e.Message, manifest))
                    //            {
                    //                _WhatToScan = WhatToScan.ManifestBegin;
                    //                lblUserInstructions.Text = "Scan another Manifest to start a new kit.";
                    //            }
                    //            else
                    //            {
                    //                lblErrorMessage.Text = "ERROR Processing " + _WhatToScan;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            Helpers.ShowError("Adjust Source Code! ERROR: Expecting ManifestBegin or ManifestEnd!!!");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //TODO: Check to see if they scanned one of the KanBan's instead and let them know its "Manifest Time".
                    //        //TODO: But.. what if the don't have the Maifest? (They would at least have the "End" copy of the Manifest, wouldn't they???
                    //        lblErrorMessage.Text = "Error Parsing Manifest Scan";
                    //        txtOutput.Text = string.Empty;
                    //    }
                    //}
                    //else if (_WhatToScan.Equals(WhatToScan.InternalKanban))
                    //{
                    //    if (ParseInternalKanban(e.Message, _InternalKanban))
                    //    {
                    //        if (ProcessInternalKanban(_InternalKanban))
                    //        {
                    //            _WhatToScan = WhatToScan.ToyotaOWK;
                    //            lblUserInstructions.Text = "Scan Toyota 'One Way Kanban' (OWK)";
                    //        }
                    //        else
                    //        {
                    //            lblErrorMessage.Text = "ERROR Processing " + _WhatToScan;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        lblErrorMessage.Text = "Error Parsing Internal Kanban";
                    //        txtOutput.Text = string.Empty;
                    //    }
                    //}
                    //else
                    //{
                    //    Helpers.ShowError(_WhatToScan + " hasn't been implemented yet!!");
                    //}
                }
                else
                {
                    if (e.DecodeException != null)
                    {
                        //--- Async Decode Exception ---
                        switch (e.DecodeException.ResultCode)
                        {
                            case DecodeAssembly.ResultCodes.Cancel:            // Async Decode was Canceled
                                return;
                            case DecodeAssembly.ResultCodes.NoDecode:          // Scan Timeout
                                //MessageBox.Show("Scan Timeout Exceeded");
                                Helpers.ShowError("Scan Timeout Exceeded");
                                break;
                            default:
                                //MessageBox.Show(e.DecodeException.Message);
                                Helpers.ShowError("Scan Error:" + e.DecodeException.Message);
                                break;
                        }
                    }
                    else
                    {
                        //--- Generic Async Exception ---
                        //MessageBox.Show(e.Exception.Message);
                        Helpers.ShowError("Exception while Scanning:" + e.Exception.Message);
                    }
                    //--- Add the Scan Key Event Handler ---
                    //+++++++++++++++++++++++++++++++++++++IS THIS NEEDED? CHECK IN DEBUG!++++++++++++++++++++++++++++
                    //+++++++++++++++++++++++++++++++++++++IS THIS NEEDED? CHECK IN DEBUG!++++++++++++++++++++++++++++
                    //+++++++++++++++++++++++++++++++++++++IS THIS NEEDED? CHECK IN DEBUG!++++++++++++++++++++++++++++
                    this.KeyDown += new KeyEventHandler(Main_KeyDown);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Helpers.ShowError("Exception in DecodeAssembly_DecodeEvent(). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")");
                }
                else
                {
                    //LogToEventLog("Exception in Logger.LogToSQL() _serviceName (" + _serviceName + "), Thread.CurrentThread.Name (" + Thread.CurrentThread.Name + "), Thread.CurrentThread.ManagedThreadId (" + Thread.CurrentThread.ManagedThreadId + "), stringToLog(" + stringToLog + "). Exception: " + ex.Message, true);
                    Helpers.ShowError("Exception in DecodeAssembly_DecodeEvent(). Exception: " + ex.Message);
                }
            }
        }

        private WhatWasScanned WhichBarcode(string barcode)
        {
            //Parse the scan
            if (string.IsNullOrEmpty(barcode))
            {
                return (WhatWasScanned.UnKnown);
            }
            //Is it the Manifest barcode? (Length = 50)
            if (barcode.Length == 50)
            {
                return (WhatWasScanned.Manifest);
            }
            //Or the OWK 2D barcode? (Length = 250)
            if (barcode.Length == 50)
            {
                return (WhatWasScanned.ToyotaOWK2D);
            }
            //Or the Internal Kanban? (Length < 50 AND starts with a "P")
            if (barcode.Length < 50 && barcode.StartsWith("P"))
            {
                return (WhatWasScanned.InternalKanban);
            }
            //Or the OWK 1D barcode? (Length < 50 AND does NOT start with a "P")
            if (barcode.Length < 50 && barcode.StartsWith("P") == false)
            {
                return (WhatWasScanned.ToyotaOWK1D);
            }

            return (WhatWasScanned.UnKnown);
        }

        public bool ParseManifestBarcode(string manifestBarcode, ManifestSQL manifest)
        {
            /*
             * EXAMPLE VALUE:
             * 00000000001111111111222222222233333333334444444444
             * 01234567890123456789012345678901234567890123456789
             * ==================================================
             * 01TMK22602N12017050105  KPN101      T205          
             * 
             * <FIELDNAME> (<EXAMPLE VALUE>) = <CHARACTER POSITION> (<LENGTH>)
             * NAMC (01TMK) = 00-04 (5) 
             * Supplier Code (22602) = 05-09 (5)
             * Dock Code (N1) = 10-11 (2)
             * Order # (“2017050105  ”) = 12-23 (12) – Left justified with trailing spaces.
             * Main Route (“KPN101   ”) = 24-32 (9) – Left justified with trailing spaces.
             * Supplier Ship Dock (“   ”) = 33-35 (9) – Currently Not Used.
             * Palletization Code (“T2”) = 36-37 (2) – Left justified with trailing spaces. 
             * Order Sequence – (“05          ”) = 38-49 (12) – Left justified with trailing spaces. 
             * TOTAL LENGTH = 50 CHARACTERS
             * 
             * Another thing on this… are the first two characters  (“01” in the “01TMK” example below) “really” part of the NAMC?
             * Can you pass on the “official” spec/definition for this barcode?
             * I think that will help me match things up with “field” names in the CMS database tables.
             * 
            */

            //The manifestBarCode should be 50 characters.
            if (manifestBarcode.Length != 50)
            {
                Helpers.ShowError("ERROR: The Manifest barcode should be 50 characters!");
                return (false);
            }

            //parsedManifest = string.Concat("Manifest:"
            //                            ,"\r\n          NAMC:", namc
            //                            ,"\r\n          OurSupplierCode:", ourSupplierCode
            //                            ,"\r\n          Dock Code:", dockCode
            //                            ,"\r\n          Order #:", orderNumber
            //                            ,"\r\n          Pick Up Truck Route:", pickUpTruckRoute
            //                            ,"\r\n          Palletization Code:", palletizationCode
            //                            ,"\r\n          Order Sequence:", orderSequence);

            //MessageBox.Show(parsedManifest);

            //<Barcode, nvarchar(50),>
            //,<OrderNumber, nvarchar(12),>
            //,<NAMC, nvarchar(5),>
            //,<SupplierCode, nvarchar(5),>
            //,<DockCode, nvarchar(2),>
            //,<MainRoute, nvarchar(9),>
            //,<SupplierShipDock, nvarchar(3),>
            //,<PalletizationCode, nvarchar(2),>
            //,<OrderSequence, nvarchar(12),>
            //,<DeviceName, nvarchar(20),>
            //,<DeviceIdentifier, nvarchar(20),>
            //,<Scanned, datetime,>
            //,<ScannedBy, nvarchar(20),>
            //,<Created, datetime,>
            //,<CreatedBy, nvarchar(20),>
            //,<Decoded, bit,>
            //,<ImagePath, nvarchar(255),>)

            manifest.Barcode = manifestBarcode;
            manifest.NAMC = manifestBarcode.Substring(0, 5).Trim();
            manifest.SupplierCode = manifestBarcode.Substring(5, 5).Trim();
            manifest.DockCode = manifestBarcode.Substring(10, 2).Trim();
            manifest.OrderNumber = manifestBarcode.Substring(12, 12).Trim();
            manifest.MainRoute = manifestBarcode.Substring(24, 9).Trim();
            manifest.SupplierShipDock = manifestBarcode.Substring(33, 3).Trim();
            manifest.PalletizationCode = manifestBarcode.Substring(36, 2).Trim();
            manifest.OrderSequence = manifestBarcode.Substring(38, 12).Trim();
            manifest.DeviceName = _deviceName;
            manifest.DeviceIdentifier = _deviceIdentifier;
            manifest.Scanned = DateTime.Now;
            manifest.ScannedBy = _userName;
            manifest.Created = DateTime.Now;
            manifest.CreatedBy = _appName;
            manifest.Decoded = true;
            manifest.ImagePath = "";

            return (true);
        }

        private int ProcessManifestBegin(string barcode, ManifestSQL manifest)
        //private void ProcessManifestBegin(string barcode, PickToLightDB.ScanManifestRow manifest)
        {
            int entryID = -1;

            ////For now, just show the fields.
            ////txtOutput.Text = string.Concat( "Manifest:"
            ////                                , "\r\n          NAMC:", manifest.namc
            ////                                , "\r\n          OurSupplierCode:", manifest.ourSupplierCode
            ////                                , "\r\n          Dock Code:", manifest.dockCode
            ////                                , "\r\n          Order #:", manifest.orderNumber
            ////                                , "\r\n          Pick Up Truck Route:", manifest.pickUpTruckRoute
            ////                                , "\r\n          Palletization Code:", manifest.palletizationCode
            ////                                , "\r\n          Order Sequence:", manifest.orderSequence);

            //txtOutput.Text = string.Concat("MANIFEST: NAMC<", manifest.namc, "> OurSupplierCode<", manifest.ourSupplierCode
            //                                , "> Dock Code<", manifest.dockCode, "> Order #<", manifest.orderNumber
            //                                , "> Pick Up Truck Route<", manifest.pickUpTruckRoute, "> Palletization Code<"
            //                                , manifest.palletizationCode, "> Order Sequence <", manifest.orderSequence, ">");


            //Add it to the SQL Table ("Insert method")
            //PickToLightDBTableAdapters.ScanManifestTableAdapter scanManifestTableAdapter = new SNA.Mobile.PickToLightClient.PickToLightDBTableAdapters.ScanManifestTableAdapter();
            //scanManifestTableAdapter.Insert(barcode, manifest.orderNumber, manifest.namc, manifest.ourSupplierCode, manifest.dockCode, manifest.MainRoute, manifest.SupplierShipDoc, manifest.PalletizationCode, manifest.OrderSequence, manifest.DeviceName, manifest.DeviceIdentifier, manifest.Scanned, manifest.ScannedBy, manifest.Created, manifest.CreatedBy, manifest.Decoded, manifest.ImagePath);

            try
            {
                //Get the new ID!
                entryID = ManifestSQL.CreateScanManifestEntry(manifest);
                if (entryID < 0)
                {
                    _logger.LogError("ERROR Creating Manifest in SQL Database!");
                    return (-1);
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError("ERROR Creating Manifest in SQL Database!");
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    _logger.LogError("Index #" + i +
                        " Message: " + ex.Errors[i].Message +
                        " LineNumber: " + ex.Errors[i].LineNumber +
                        " Source: " + ex.Errors[i].Source + 
                        " Procedure: " + ex.Errors[i].Procedure );
                }
                
                return (-1);
            }


            //SendManifestCommand(barcode, _Manifest);

            //string commandToSend = "";
            ////
            ////Moved to Utility, PickToLightCommands class!
            ////
            ////if (barcodeDecoded)
            ////{
            ////    commandToSend = "ST" + entryID.ToString();
            ////}
            ////else
            ////{
            ////    commandToSend = "STXXX";
            ////}
            ////commandToSend = PickToLightCommands.PopulateScanTransferCommand(barcodeDecoded, entryID);
            //commandToSend = P2L_COMMAND_ScanManifest + sGS + barcode;

            ////if (_pickToLightServerIP.Length > 10 && _pickToLightServerIP.Split('.').Length == 4) //At least 11 characters (10.10.10.10) and 3 periods/dots.
            //if (_pickToLightServerIP != null)
            //{
            //    //TCPClient tcpClient = new TCPClient(_pickToLightServerIP, port);
            //    TCPClient tcpClient = new TCPClient(_logger, _pickToLightServerIP, _port);
            //    StringBuilder commandResponse = new StringBuilder();
            //    tcpClient.SendCommand(commandToSend, commandResponse);

            //    txtOutput.Text = commandResponse.ToString();

            //    //if(commandResponse.ToString() == "Now Scan Internal Kanban")
            //    //{
            //        _WhatToScan = WhatToScan.InternalKanban;
            //        lblUserInstructions.Text = "Scan Internal Kanban";
            //    //}
            //}
            //else
            //{
            //    //_logger.LogToEventLog("The TransferScannerIP setting has (" + _transferScannerIP.Split('.').Length.ToString() + ") periods!");
            //    _logger.LogError("The PickToLightServerIP setting is invalid or not specified! (This shouldn't get this far, it should be caught when Loading the settings during startup!)");
            //    lblErrorMessage.Text = "The PickToLightServerIP setting is invalid or not specified! (This shouldn't get this far, it should be caught when Loading the settings during startup!)";
            //    lblErrorMessage.Text = "ERROR Processing " + _WhatToScan;
            //    //return(false);
            //}

            return (entryID);
        }

        private bool ProcessManifestEnd(string barcode, ManifestSQL manifest)
        {

            //For now, just show the fields.
            txtOutput.Text = string.Concat("MANIFEST: NAMC<", manifest.NAMC
                                            , "> SupplierCode<", manifest.SupplierCode
                                            , "> Dock Code<", manifest.DockCode
                                            , "> Order #<", manifest.OrderNumber
                                            , "> MainRoute<", manifest.MainRoute
                                            , "> Supplier Ship Dock<", manifest.SupplierShipDock
                                            , "> Palletization Code<", manifest.PalletizationCode
                                            , "> Order Sequence <", manifest.OrderSequence, ">");

            return (true);
        }

        //private bool ProcessInternalKanban(InternalKanban kanban)
        //{
        //    //For now, just show the fields.
        //    //txtOutput.Text = string.Concat("MANIFEST: NAMC<", manifest.namc, "> OurSupplierCode<", manifest.ourSupplierCode
        //    //                                , "> Dock Code<", manifest.dockCode, "> Order #<", manifest.orderNumber
        //    //                                , "> Pick Up Truck Route<", manifest.pickUpTruckRoute, "> Palletization Code<"
        //    //                                , manifest.palletizationCode, "> Order Sequence <", manifest.orderSequence, ">");
        //    txtOutput.Text = kanban.scannedInternalKanban;

        //    return (true);
        //}

        //public bool ParseInternalKanban(string kanbanBarCode, InternalKanban kanban)
        //{
        //    //The kanbanBarCode should be XXX characters.
        //    //if (kanbanBarCode.Length != XXX)
        //    //{
        //    //    //parsedManifest = "ERROR: The Manifest barcode should be 50 characters!";
        //    //    return (false);
        //    //}

        //    //parsedManifest = string.Concat("Manifest:"
        //    //                            ,"\r\n          NAMC:", namc
        //    //                            ,"\r\n          OurSupplierCode:", ourSupplierCode
        //    //                            ,"\r\n          Dock Code:", dockCode
        //    //                            ,"\r\n          Order #:", orderNumber
        //    //                            ,"\r\n          Pick Up Truck Route:", pickUpTruckRoute
        //    //                            ,"\r\n          Palletization Code:", palletizationCode
        //    //                            ,"\r\n          Order Sequence:", orderSequence);

        //    //MessageBox.Show(parsedManifest);

        //    //kanban.namc = manifestBarcode.Substring(0, 5).Trim();
        //    //kanban.ourSupplierCode = manifestBarcode.Substring(5, 5).Trim();
        //    //kanban.dockCode = manifestBarcode.Substring(10, 2).Trim();
        //    //kanban.orderNumber = manifestBarcode.Substring(12, 10).Trim();
        //    //kanban.pickUpTruckRoute = manifestBarcode.Substring(22, 8).Trim();
        //    //kanban.palletizationCode = manifestBarcode.Substring(30, 8).Trim();
        //    //kanban.orderSequence = manifestBarcode.Substring(38, 12).Trim();
        //    kanban.scannedInternalKanban = kanbanBarCode;

        //    return (true);
        //}

        private ServerCommandResponse SendManifestCommand(ManifestSQL manifest, int entryID)
        {
            ServerCommandResponse response = new ServerCommandResponse();
            try
            {
                string commandToSend = "";
        //    //
        //    //Moved to Utility, PickToLightCommands class!
        //    //
        //    //if (barcodeDecoded)
        //    //{
        //    //    commandToSend = "ST" + entryID.ToString();
        //    //}
        //    //else
        //    //{
        //    //    commandToSend = "STXXX";
        //    //}
        //    //commandToSend = PickToLightCommands.PopulateScanTransferCommand(barcodeDecoded, entryID);
                commandToSend = P2L_COMMAND_ScanManifest + sGS + entryID.ToString();

                //if (_pickToLightServerIP.Length > 10 && _pickToLightServerIP.Split('.').Length == 4) //At least 11 characters (10.10.10.10) and 3 periods/dots.
                if (_pickToLightServerIPAddress != null)
                {
                    //TCPClient tcpClient = new TCPClient(_pickToLightServerIP, port);
                    TCPClient tcpClient = new TCPClient(_logger, _pickToLightServerIPAddress, _port);
                    StringBuilder commandResponse = new StringBuilder();
                    tcpClient.SendCommand(commandToSend, commandResponse);

                    //Parse out the ServerCommandResponse!
                    txtOutput.Text = commandResponse.ToString();
                    //But, for now, just show it and use the following logic for testing...
                    response.ShowErrorDialog = false;
                    response.UserInstructions = lblUserInstructions.Text;
                    response.ErrorMessage = "ERROR! Exception in SendManifestCommand()!";
                    switch (_WhatToScan) //For testing, just use the CURRENT _WhatToScan
                    {
                        case WhatToScan.ManifestBegin:
                            response.NowScanThis = WhatToScan.ToyotaOWK;
                            break;
                        case WhatToScan.ToyotaOWK:
                            response.NowScanThis = WhatToScan.InternalKanban;
                            break;
                        case WhatToScan.InternalKanban:
                            response.NowScanThis = WhatToScan.ManifestEnd;
                            break;
                        case WhatToScan.ManifestEnd:
                            response.NowScanThis = WhatToScan.ManifestBegin;
                            break;
                        default: // The Server could send "NoChange", in that case, this handles it.
                            response.NowScanThis = _WhatToScan;
                            break;
                    }
                }
                else
                {
                    //_logger.LogToEventLog("The TransferScannerIP setting has (" + _transferScannerIP.Split('.').Length.ToString() + ") periods!");
                    response.ErrorMessage = "The PickToLightServerIP setting is invalid or not specified! (This shouldn't get this far, it should be caught when Loading the settings during startup!)";
                    _logger.LogError(response.ErrorMessage);
                    //Done using the "ShowErrorDialog" boolean in the response.
                    //Helpers.ShowError(response.ErrorMessage);
                    response.NowScanThis = _WhatToScan; //Keep it the same as what we were looking for!
                    response.ShowErrorDialog = true;
                    response.UserInstructions = lblUserInstructions.Text;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "";
                if (ex.InnerException != null)
                {
                    errorMessage = "Exception in SendManifestCommand(). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")";
                }
                else
                {
                    errorMessage = "Exception in SendManifestCommand(). Exception: " + ex.Message;
                }
                //LogToEventLog("Exception in Logger.LogToSQL() _serviceName (" + _serviceName + "), Thread.CurrentThread.Name (" + Thread.CurrentThread.Name + "), Thread.CurrentThread.ManagedThreadId (" + Thread.CurrentThread.ManagedThreadId + "), stringToLog(" + stringToLog + "). Exception: " + ex.Message, true);
                _logger.LogError(errorMessage);
                //Done using the "ShowErrorDialog" boolean in the response.
                //Helpers.ShowError(errorMessage);

                response.NowScanThis = _WhatToScan; //Keep it the same as what we were looking for!
                response.ShowErrorDialog = true;
                response.UserInstructions = lblUserInstructions.Text;
                response.ErrorMessage = "ERROR! Exception in SendManifestCommand()!";
            }

            return response;
        }

        private bool SendPingCommand()
        {
            try
            {
                string commandToSend = "";
                commandToSend = P2L_COMMAND_Ping + sGS + _deviceName ;

                //if (_pickToLightServerIP.Length > 10 && _pickToLightServerIP.Split('.').Length == 4) //At least 11 characters (10.10.10.10) and 3 periods/dots.
                if (_pickToLightServerIPAddress != null)
                {
                    //TCPClient tcpClient = new TCPClient(_pickToLightServerIP, port);
                    TCPClient tcpClient = new TCPClient(_logger, _pickToLightServerIPAddress, _port);
                    StringBuilder commandResponse = new StringBuilder();
                    tcpClient.SendCommand(commandToSend, commandResponse);

                    string[] words = commandResponse.ToString().Split(caGS);

                    if(words.Length < 1) //Go GS found!
                    {
                        _logger.LogError("Error Pinging PickToLightServer, no GS received in response!");
                        return (false);
                    }
                    if(words[0].Equals(P2L_COMMAND_Ping) == false)
                    {
                        _logger.LogError("Error Pinging PickToLightServer, the P2L_COMMAND_Ping wasn't returned (" + words[0] + ").");
                        return (false);
                    }
                    if (words[1].Equals(_deviceName))
                    {
                        return true;
                    }
                }
                else
                {
                    //_logger.LogToEventLog("The TransferScannerIP setting has (" + _transferScannerIP.Split('.').Length.ToString() + ") periods!");
                    string errorMessage = "The PickToLightServerIP setting is invalid or not specified! (This shouldn't get this far, it should be caught when Loading the settings during startup!)";
                    _logger.LogError(errorMessage);
                    //Done using the "ShowErrorDialog" boolean in the response.
                    //Helpers.ShowError(response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "";
                if (ex.InnerException != null)
                {
                    errorMessage = "Exception in SendPingCommand(). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")";
                }
                else
                {
                    errorMessage = "Exception in SendPingCommand(). Exception: " + ex.Message;
                }
                //LogToEventLog("Exception in Logger.LogToSQL() _serviceName (" + _serviceName + "), Thread.CurrentThread.Name (" + Thread.CurrentThread.Name + "), Thread.CurrentThread.ManagedThreadId (" + Thread.CurrentThread.ManagedThreadId + "), stringToLog(" + stringToLog + "). Exception: " + ex.Message, true);
                _logger.LogError(errorMessage);
                //Done using the "ShowErrorDialog" boolean in the response.
                //Helpers.ShowError(errorMessage);
            }

            return(false);
        }
    }
}