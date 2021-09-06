using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using HSM.Embedded.Decoding;
using System.Net;
using System.Data.SqlClient;
using System.Diagnostics;
//using System.Runtime.InteropServices;
//using HSM.Embedded.Hardware;

namespace SNA.WinForm.PickToLightClient
{
    public partial class Main : Form
    {
        //6-18-18 - Version 3. Pull the Database server using the "Ping" command!
        string _clientVersion = "3";
        bool _logBarcodes = true;
        bool _logNetworkTraffic = true;
        
        //DeviceHardwareAssembly _dolphin;

        DateTime _lastKeystroke = new DateTime(0);
        string _barcode = string.Empty;

        //public static string prodConnectionString = @"Server=GT-transACTION\SQLExpress;Database=PickToLight; Uid=PickToLight; pwd=P1ckT0L1ght;";
        //public static string devConnectionString = @"Server=GT-transACTION\SQLExpress;Database=PickToLightDev; Uid=PickToLight; pwd=P1ckT0L1ght;";
        //public static string prodConnectionString = @"Server=HQ-SQL02;Database=PickToLight; Uid=PickToLight; pwd=P1ckT0L1ght;";
        //public static string devConnectionString = @"Server=HQ-SQL02;Database=PickToLightDev; Uid=PickToLight; pwd=P1ckT0L1ght;";
        ////public static string shayneHomeServerConnectionString = @"Server=192.168.1.4;Database=PickToLightDev; Uid=PickToLight; pwd=P1ckT0L1ght;";
        //string _sqlConnectionString = @"Server=GT-transACTION\SQLExpress;Database=PickToLight; Uid=PickToLight; pwd=P1ckT0L1ght;";
        string _sqlConnectionString = @"Server=HQ-SQL02;Database=PickToLight; Uid=PickToLight; pwd=P1ckT0L1ght;";
        private static PickToLightData _pickToLightData;
        private static Logger _logger;

        public enum WhatToScan { ManifestBegin, InternalKanban, ToyotaOWK, ManifestEnd };
        //TODO: Really need this? May be able to just use NowScanThis in ServerCommandResponse...
        //No, we should just use the WhatToScan/NowScanThis from the ServerCommandResponse!
        WhatToScan _WhatToScan = WhatToScan.ManifestBegin; //Initialize "ManifestBegin", because the user needs to scan a Manifest to start (or continue) a kit!

        public enum WhatWasScanned { UnKnown, Manifest, InternalKanban, ToyotaOWK2D}; //, ToyotaOWK1D }; //On second thought, let's NOT try to detect the ToyotaOWK1D, because there is no real way to determine it.
        WhatWasScanned _WhatWasScanned = WhatWasScanned.UnKnown;

        public class ServerCommandResponse
        {
            public WhatToScan NowScanThis { get; set; }
            public bool ShowErrorDialog { get; set; }
            public string LogErrorMessage { get; set; }        //Blank/Empty when we don't want to write to the Event Log!
            public string DisplayErrorMessage { get; set; }        //Blank/Empty when the ErrorMessage to display on the Client Display should be Blank/Empty!
            //
            //May need to use these two when the Client/Server Architecture is implemented.
            //
            //public string UserInstructions { get; set; }    //Blank/Empty when the UserInstructions don't need to be changed on the Client Display!
            //public string ProgramOutput { get; set; }       //Blank/Empty when the ProgtamOutput doesn't need to be changed on the Client Display!

            public ServerCommandResponse()
            {
                NowScanThis = WhatToScan.ManifestBegin;
                ShowErrorDialog = false;
                LogErrorMessage = "";
                DisplayErrorMessage = "";
                //UserInstructions = "Scan the Manifest to start a new (or continue an old) kit.";
                //ProgramOutput = "";
            }
        };
        ServerCommandResponse _serverCommandResponse = new ServerCommandResponse();
        
        ManifestSQL _manifestPulling = new ManifestSQL();
        bool _manifestHeld = false;
        string _lastScannedPartNumber = "?????";

        const int iGS = 29; //ASCII Group Separator
        const char cGS = (char)29;
        const byte bGS = 0x1D;
        const string sGS = "\x1D";
        string[] saGS = { "\x1D" };
        char[] caGS = { (char)29 };

        //TODO: READ THESE AS "app.config" settings!!!
        //
        //Next two lines are for the Production Server
        static string GT_PickToLight = "10.22.1.50";
        static string _pickToLightServerIP = GT_PickToLight;
        //Next two lines are for the Development Server
        //static string hq_sptest01 = "10.2.9.23";
        //static string _pickToLightServerIP = hq_sptest01;
        //
        //static string _shayneHomePickToLightServerIP = "192.168.1.4";
        //static string _shayneHomeTestDeviceName = "HomeTest";
        //static string _pickToLightServerIP = _shayneHomePickToLightServerIP;

        //string gt_PickToLight = "X.X.X.X";
        IPAddress _pickToLightServerIPAddress = null;
        //The port number for the remote device.  
        private int _port = 11000;

        //TODO: Populate these from Device???
        string _appName = "PickToLightClient";
        string _deviceName = "TestingSurfacePro";
        string _deviceIdentifier = "FillWithDeviceID";
        string _userName = "NO LOGIN"; //"Test User";   // Add Login and get real User Name!!!!
        int _kanbansMatched = 0;

        public const string P2L_COMMAND_Ping = "PING";  //Test communication.
        //public const string P2L_COMMAND_ScanManifest = "SM";  //Leave this for now, but it SHOULD be removed.
        //public const string P2L_COMMAND_ScanInternalKanban = "SIK";
        //public const string P2L_COMMAND_ScanToyotaOWK = "SOWK";
        public const string P2L_COMMAND_ScanBarcode = "SB";
        public const string P2L_COMMAND_ScanManifestBegin = "SMB"; //Remove this one soon, just temporary for now!
        public const string P2L_COMMAND_ScanManifestEnd = "SME"; //Remove this one soon, just temporary for now!

        //[DllImport("coredll.dll")]
        //public static extern IntPtr GetCapture();

        //private enum FullScreenState : uint
        //{
        //    SHFS_SHOWTASKBAR = 0x0001,
        //    SHFS_HIDETASKBAR = 0x0002,
        //    SHFS_SHOWSIPBUTTON = 0x0004,
        //    SHFS_HIDESIPBUTTON = 0x0008,
        //    SHFS_SHOWSTARTICON = 0x0010,
        //    SHFS_HIDESTARTICON = 0x0020
        //}

        //[DllImport("aygshell.dll", EntryPoint = "SHFullScreen", CharSet = CharSet.Auto)]
        //private static extern bool SHFullScreen(UInt32 hWnd, FullScreenState dwState);

        //private IntPtr _WindowHandle;

        //public Main(string userName)
        public Main()
        {
            try
            {
                InitializeComponent();

                try
                {
                    if (EventLog.SourceExists(_appName) == false)
                    {
                        EventLog.CreateEventSource(_appName, "Application");
                        if (EventLog.SourceExists(_appName) == false)
                        {
                            Helpers.ShowError("Error! The Source (" + _appName + ") doesn't exist in the Application Event Log. You may have to create the Source as an Adminstrator.");
                            Application.Exit();
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helpers.ShowError("Error! The Source (" + _appName + ") doesn't exist in the Application Event Log. You may have to create the Source as an Adminstrator. (Exception Message: " + ex.Message + ")");
                    Application.Exit();
                    this.Close();
                }


                //// trick for querying the main window handle
                //this.Capture = true;
                //_WindowHandle = GetCapture();
                //this.Capture = false;
                ////SHFullScreen((uint)WindowHandle,FullScreenState.SHFS_HIDETASKBAR);
                ////SHFullScreen((uint)WindowHandle,FullScreenState.SHFS_HIDESTARTICON);
                ////// must make two calls to reliably hide menu bar and sip
                ////this.Menu = null;
                ////Hide the SIP Button!
                //SHFullScreen((uint)_WindowHandle, FullScreenState.SHFS_HIDESIPBUTTON);

            //    _userName = userName;

            //    try
            //    {
            //        _dolphin = new DeviceHardwareAssembly();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }

            //    bool wifiConnected = false;
            //    bool keepChecking = true;
            //    while (keepChecking)
            //    {
            //        if (Helpers.IsWIFIConnected())
            //        {
            //            wifiConnected = true;
            //            keepChecking = false;
            //        }
            //        else
            //        {
            //            if (MessageBox.Show("ERROR with Wireless Network Connection!!!\r\n\r\nSelect Retry or Cancel (to Abort).", "Wireless Error!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
            //            {
            //                keepChecking = false;
            //                //Neither of these work. I don't understand....
            //                //Application.Exit();
            //                //this.Close();
            //            }
            //        }
            //    }
            //    if (wifiConnected)
            //    {
            //        _deviceName = Helpers.GetDeviceName();
            //        //_deviceIdentifier = _deviceName;
            //        _deviceIdentifier = Helpers.GetWIFIMac();

            //        ////Check for "Home Test"
            //        //if (_pickToLightServerIP.Equals(_shayneHomePickToLightServerIP))
            //        //{
            //        //    _deviceName = _shayneHomeTestDeviceName;
            //        //}

            //        //Load Some Settings from somewhere!
            //        _pickToLightServerIPAddress = System.Net.IPAddress.Parse(_pickToLightServerIP);
            //        _logger = new Logger(_appName, _deviceName);
            //        _logger.LogMode = Logger.LogModes.SQLLog;

            //        InitMainForm(); //Use this to set the defaults.

            //        //txtOutput.Text = txtOutput.Text + "Pinging Server...";
            //        if (SendPingCommand() == false)
            //        {
            //            lblErrorMessage.Text = "ERROR! Unable to \"Ping\" the PickToLightServer (" + _pickToLightServerIPAddress.ToString() + ").";
            //        }
            //        else
            //        {
            //            //txtOutput.Text = txtOutput.Text + "Server Pinged!";                    
            //        }

            //        //Can we try putting the focus on a label (non input control) to prevent the little icon for the SIP (software keyabord) from appearing?
            //        lblManifest.Focus();

            //        this.KeyPreview = true;
            //        this.KeyPress += new KeyPressEventHandler(Main_KeyPress);
            //    }
            //    else
            //    {
            //        Application.Exit();
            //        this.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (ex.InnerException != null)
            //    {
            //        //Helpers.ShowError("Exception in Main(). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")");
            //        _logger.LogError("Exception in Main(). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")");
            //    }
            //    else
            //    {
            //        //LogToEventLog("Exception in Logger.LogToSQL() _serviceName (" + _serviceName + "), Thread.CurrentThread.Name (" + Thread.CurrentThread.Name + "), Thread.CurrentThread.ManagedThreadId (" + Thread.CurrentThread.ManagedThreadId + "), stringToLog(" + stringToLog + "). Exception: " + ex.Message, true);
            //        //Helpers.ShowError("Exception in Main(). Exception: " + ex.Message);
            //        _logger.LogError("Exception in Main(). Exception: " + ex.Message);
            //    }
            //}

                //try
                //{
                //    _dolphin = new DeviceHardwareAssembly();
                //}
                //catch (Exception ex)
                //{
                //    //MessageBox.Show(ex.Message);
                //    Helpers.ShowError("\r\n" + ex.Message);
                //}

                bool wifiConnected = false;
                bool keepChecking = true;
                while (keepChecking)
                {
                    if (Helpers.IsWIFIConnected())
                    {
                        wifiConnected = true;
                        keepChecking = false;
                    }
                    else
                    {
                        //if (MessageBox.Show("ERROR with Wireless Network Connection!!!\r\n\r\nSelect Retry or Cancel (to Abort).", "Wireless Error!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                        //{
                        //    keepChecking = false;
                        //    //Neither of these work. I don't understand....
                        //    //Application.Exit();
                        //    //this.Close();
                        //}
                        using (ErrorDialog errorDialog = new ErrorDialog("WIFI ERROR!!", "\r\nERROR with Wireless Network Connection!!!\r\n\r\nClick OK to Retry or CANCEL (to Abort).", true))
                        {
                            if (errorDialog.ShowDialog() == DialogResult.Cancel)
                            {
                                keepChecking = false;
                            }
                        }

                    }
                }
                if (wifiConnected)
                {
                    _deviceName = Helpers.GetDeviceName();
                    //_deviceIdentifier = _deviceName;
                    _deviceIdentifier = Helpers.GetWIFIMac();

                    ////Check for "Home Test"
                    //if (_pickToLightServerIP.Equals(_shayneHomePickToLightServerIP))
                    //{
                    //    _deviceName = _shayneHomeTestDeviceName;
                    //}

                    //Load Some Settings from somewhere!
                    _pickToLightServerIPAddress = System.Net.IPAddress.Parse(_pickToLightServerIP);

                    ///
                    ///Let's not replace the SQL Connection String received from the Server,
                    ///at least until I get the Auto-Update figured out!
                    ///
                    _pickToLightData = new PickToLightData(_sqlConnectionString);
                    _logger = new Logger(_appName, _deviceName, _pickToLightData);
                    _logger.LogMode = Logger.LogModes.SQLLog;
                    ///
                    ////Determine the "_sqlConnectionStrng"!!!
                    ////Get it from the "PickToLightServer"???
                    ////_pickToLightData = new PickToLightData(_sqlConnectionString);
                    ////_logger = new Logger(_appName, _deviceName, _pickToLightData);
                    ////If we don't have a SQL Connection String yet, crete the Logger with a null PickToLightData instance.
                    //_logger = new Logger(_appName, _deviceName, null);
                    ////_logger.LogMode = Logger.LogModes.SQLLog;
                    ////Use EventLog until we have a SQL Connection String!! (It will be changed in "SendPingCommand()".)
                    //_logger.LogMode = Logger.LogModes.EventLog;

                    InitMainForm(); //Use this to set the defaults.

                    //txtOutput.Text = txtOutput.Text + "Pinging Server...";
                    if (SendPingCommand() == false)
                    {
                        lblErrorMessage.Text = "ERROR! Unable to \"Ping\" the PickToLightServer (" + _pickToLightServerIPAddress.ToString() + ").";
                        Application.Exit();
                        this.Close();
                    }
                    else
                    {
                        //txtOutput.Text = txtOutput.Text + "Server Pinged!"; 
                    }

                    ////Can we try putting the focus on a label (non input control) to prevent the little icon for the SIP (software keyabord) from appearing?
                    //lblManifest.Focus();

                    //this.KeyPreview = true;
                    //this.KeyPress += new KeyPressEventHandler(Main_KeyPress);
                }
                else
                {
                    Application.Exit();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _logger.LogError("Error starting up! (Check EventLog for Exception Mesage.)", "Exception in Main(). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")");
                }
                else
                {
                    _logger.LogError("Error starting up! (Check EventLog for Exception Mesage.)", "Exception in Main(). Exception: " + ex.Message);
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                //Lets just test the ErrorDialog here! First, lets test the helper version:
                //
                //Helpers.ShowError("\r\n\r\nTesting Helper Version!\r\n\r\n\r\n(Try scanning something!!!)\r\n");

                //using (ErrorDialog errorDialog = new ErrorDialog("Direct Call Test with Cancel!!", "\r\n\r\nTesting Direct Call\r\n(Did Cancel Button Show?)\r\n" , true))
                //{
                //    if (errorDialog.ShowDialog() == DialogResult.Cancel)
                //    {
                //        MessageBox.Show("Cancel was Pressed!");
                //    }
                //    else
                //    {
                //        MessageBox.Show("OK was Pressed!");
                //    }
                //}
                //Application.Exit();
                //this.Close();

                using (Login login = new Login(_pickToLightData))
                {
                    if (login.ShowDialog() == DialogResult.OK)
                    {
                        _userName = login.UserName;

                        //Can we try putting the focus on a label (non input control) to prevent the little icon for the SIP (software keyabord) from appearing?
                        lblManifest.Focus();

                        this.KeyPreview = true;
                        this.KeyPress += new KeyPressEventHandler(Main_KeyPress);

                        InitMainForm(); //Use this to set the defaults.
                    }
                    else
                    {
                        Application.Exit();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _logger.LogError("Error loading! (Check EventLog for Exception Mesage.)", "Exception in Main_Load(). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")");
                }
                else
                {
                    _logger.LogError("Error loading! (Check EventLog for Exception Mesage.)", "Exception in Main_Load(). Exception: " + ex.Message);
                }
            }
        }

        void Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            //If more than 500ms since character received, clear the barcode buffer.
            TimeSpan elapsed = (DateTime.Now - _lastKeystroke);
            if (elapsed.TotalMilliseconds > 500)
            {
                _barcode = string.Empty;
            }

            // record keystroke & timestamp -- do NOT add return at the end of the barcode line, instead send it to the barcode handler.
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (_barcode.Length > 0)
                {
                    BarcodeScanned(_barcode);
                }
                _barcode = string.Empty; //Clear the barcode buffer.
            }
            else //If not return, just add to the barcode buffer.
            {
                _barcode += e.KeyChar; //.ToString();
            }
            _lastKeystroke = DateTime.Now;

            e.Handled = true;
        }

        /// <summary>
        /// Set the default values and properties (Color, Visibility, etc) for the MainForm UI elements (Labels, Buttons, etc).
        /// </summary>
        /// <param name="response"></param>
        private void InitMainForm()
        {
            ///////////////////////
            //INITIAL DEFAULTS
            ///////////////////////
            //
            // lblManifest
            // 
            //this.lblManifest.Location = new System.Drawing.Point(5, 10);
            //this.lblManifest.Size = new System.Drawing.Size(440, 50);
            //this.lblManifest.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //lblManifest.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            lblManifest.Text = "Scan Manifest";
            lblManifest.BackColor = System.Drawing.Color.Black;
            lblManifest.ForeColor = System.Drawing.Color.White;
            lblManifest.Visible = true;

            // 
            // lblInternalKanban
            // 
            //this.lblInternalKanban.Location = new System.Drawing.Point(5, 70);
            //this.lblInternalKanban.Size = new System.Drawing.Size(440, 50);
            //this.lblInternalKanban.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //lblInternalKanban.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            lblInternalKanban.Text = "Scan Internal Kanban";
            lblInternalKanban.BackColor = System.Drawing.Color.Black;
            lblInternalKanban.ForeColor = System.Drawing.Color.White;
            lblInternalKanban.Visible = false;

            // 
            // lblToyotaOWK
            // 
            //this.lblToyotaOWK.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            //this.lblToyotaOWK.Location = new System.Drawing.Point(5, 130);
            //this.lblToyotaOWK.Size = new System.Drawing.Size(440, 50);
            //this.lblToyotaOWK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            lblToyotaOWK.Text = "Scan Toyota Kanban";
            lblToyotaOWK.BackColor = System.Drawing.Color.Black;
            lblToyotaOWK.ForeColor = System.Drawing.Color.White;
            lblToyotaOWK.Visible = false;

            // 
            // lblErrorMessage
            // 
            //this.lblErrorMessage.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            //this.lblErrorMessage.Location = new System.Drawing.Point(5, 363);
            //this.lblErrorMessage.Size = new System.Drawing.Size(788, 50);
            //this.lblErrorMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblErrorMessage.Text = "";
            this.lblErrorMessage.BackColor = System.Drawing.Color.Black;
            this.lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            lblErrorMessage.Visible = true; ;

            // 
            // lblKanbanMatch
            // 
            //this.lblKanbanMatch.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            //this.lblKanbanMatch.Location = new System.Drawing.Point(5, 183);
            //this.lblKanbanMatch.Size = new System.Drawing.Size(440, 125);
            //this.lblKanbanMatch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblKanbanMatch.Text = "KANBAN MATCH";
            this.lblKanbanMatch.BackColor = System.Drawing.Color.Green;
            this.lblKanbanMatch.ForeColor = System.Drawing.Color.Black;
            this.lblKanbanMatch.Visible = false;

            // 
            // btnHoldManifest
            // 
            //this.btnHoldManifest.BackColor = System.Drawing.Color.Red;
            //this.btnHoldManifest.ForeColor = System.Drawing.Color.White;
            //this.btnHoldManifest.Location = new System.Drawing.Point(170, 315);
            //this.btnHoldManifest.Size = new System.Drawing.Size(275, 45);
            //this.btnHoldManifest.TabIndex = 16;
            //this.btnHoldManifest.Text = "Hold Manifest";
            btnHoldManifest.Enabled = false;
            btnHoldManifest.Visible = false;

            // 
            // btnReset
            // 
            //this.btnReset.BackColor = System.Drawing.Color.Red;
            //this.btnReset.ForeColor = System.Drawing.Color.White;
            //this.btnReset.Location = new System.Drawing.Point(5, 315);
            //this.btnReset.Size = new System.Drawing.Size(107, 45);
            //this.btnReset.TabIndex = 15;
            //this.btnReset.Text = "RESET";
            btnReset.Text = "EXIT";  //Make it the Exit button when the app starts up and we are waiting for a Manifest!!!
            btnReset.Enabled = true;  //Make it the Exit button when the app starts up and we are waiting for a Manifest!!!
            btnReset.Visible = true;  //Make it the Exit button when the app starts up and we are waiting for a Manifest!!!

            // 
            // txtOutput
            // 
            //this.txtOutput.Location = new System.Drawing.Point(463, 10);
            //this.txtOutput.Multiline = true;
            //this.txtOutput.Name = "txtOutput";
            //this.txtOutput.ReadOnly = true;
            //this.txtOutput.Size = new System.Drawing.Size(330, 350);
            //this.txtOutput.TabIndex = 13;
            //this.txtOutput.Text = "User: SJudkins\r\nOrder #: 12345\r\nDock: 2A\r\n--Kanbans Scanned--\r\nA151: 99\r\nD666: 11";
            //txtOutput.Text = "User: " + _userName;
            PopulateOutput();
        }

        /// <summary>
        /// Based on _WhatToScan, _WhatWasScanned and response.NowScanThis: set the values and properties (Color, Visibility, etc) for the MainForm UI elements (Labels, Buttons, etc).
        /// </summary>
        /// <param name="response"></param>
        private void FixUpMainForm(ServerCommandResponse response)
        {
            ///////////////////////
            //Based on _WhatToScan, _WhatWasScanned and response.NowScanThis:
            //  Set the Text, Colors, Visibility, "Active or Not", etc for the Labels, Buttons and Text fields in the User Interface.
            //  Set the new value for _WhatToScan. (_WhatToScan = response.NowScanThis)
            ///////////////////////

            //ServerCommandResponse:
            //public WhatToScan NowScanThis { get; set; }
            //public bool ShowErrorDialog { get; set; }
            //public string ErrorMessage { get; set; }        //Blank/Empty when the ErrorMessage to display on the Client Display should be Blank/Empty!

            //Possibilities:
            //_WhatToScan = WhatToScan.ManifestBegin
            //_WhatToScan = WhatToScan.InternalKanban
            //_WhatToScan = WhatToScan.ToyotaOWK
            //_WhatToScan = WhatToScan.ManifestEnd
            //_WhatWasScanned = WhatWasScanned.Manifest
            //_WhatWasScanned = WhatWasScanned.InternalKanban
            //_WhatWasScanned = WhatWasScanned.ToyotaOWK2D
            //_WhatWasScanned = WhatWasScanned.UnKnown
            //response.NowScanThis = WhatToScan.ManifestBegin
            //response.NowScanThis = WhatToScan.InternalKanban
            //response.NowScanThis = WhatToScan.ToyotaOWK
            //response.NowScanThis = WhatToScan.ManifestEnd

            //_WhatToScan - What the user should have just scanned.
            //_WhatWasScanned - What the user just scanned.
            //response.NowScanThis - What to scan next, based on _WhatToScan, _WhatWasScanned and if the scan just done was CORRECT.
            //(In Client/Server Architedcture, this will usually be determined by the Server using the ServerCommandResponse.)

            ////
            //// MainForm UI Elements to manage
            ////
            //// lblManifest - "Scan Manifest", Black on White, Visible
            //// lblInternalKanban - Hidden
            //// lblToyotaOWK - Hidden
            //// lblErrorMessage -  Show Error, if one! (and PopUp if ShowErrorDialog = true)
            //// lblKanbanMatch - Hidden
            //// btnHoldManifest - Hidden
            //// btnReset - Hidden
            //// txtOutput - (Depends on Scenario)
            ////
            //// lblManifest
            //lblManifest.Text = "Scan Manifest";
            //lblManifest.BackColor = System.Drawing.Color.Black;
            //lblManifest.ForeColor = System.Drawing.Color.White;
            //lblManifest.Visible = true;
            //// lblInternalKanban
            //lblInternalKanban.Text = "Scan Internal Kanban";
            //lblInternalKanban.BackColor = System.Drawing.Color.Black;
            //lblInternalKanban.ForeColor = System.Drawing.Color.White;
            //lblInternalKanban.Visible = false;
            //// lblToyotaOWK
            //lblToyotaOWK.Text = "Scan Toyota Kanban";
            //lblToyotaOWK.BackColor = System.Drawing.Color.Black;
            //lblToyotaOWK.ForeColor = System.Drawing.Color.White;
            //lblToyotaOWK.Visible = false;
            //// lblErrorMessage
            //this.lblErrorMessage.Text = "";
            //this.lblErrorMessage.BackColor = System.Drawing.Color.Black;
            //this.lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            //lblErrorMessage.Visible = true;
            //// lblKanbanMatch
            //this.lblKanbanMatch.Text = "KANBAN\r\nMATCH";
            //this.lblKanbanMatch.BackColor = System.Drawing.Color.Green;
            //this.lblKanbanMatch.ForeColor = System.Drawing.Color.Black;
            //this.lblKanbanMatch.Visible = false;
            //// btnHoldManifest
            //btnHoldManifest.Enabled = false;
            //btnHoldManifest.Visible = false;
            //// btnReset
            //btnReset.Enabled = false;
            //btnReset.Visible = false;
            //// txtOutput
            //txtOutput.Text = "User: " + _userName;

            //_logger.Log("===Painting the screen in FixUpMainForm(): response ( " + (response.ShowErrorDialog ? "ERROR, " : "") + response.NowScanThis.ToString() + ") _WhatToScan = (" + _WhatToScan.ToString() + ") _WhatWasScanned = (" + _WhatWasScanned.ToString() + ").");

            //Adjust the User Interface based on:
            //  what we have determined should be scanned next (response.NowScanThis),
            //  what was just scanned (_WhatWasScanned) and
            //  what we WANTED to scan (_WhatToScan).
            //RIGHT??????
            switch (response.NowScanThis)
            {
                //Scenarios when NowScanThis = ManifestBegin:
                // Normal Flow: Program just started/the user just logged in. (This SHOULDN'T happen, because this method shouldn't be called until at least one Scan has been performed, we should use "InitMainForm" on startup.)
                // Normal Flow: The user just finished an order by scanning ManifestEnd and VALIDATING it with (_manifestPulling) the Manifest scanned in ManifestBegin. (_WhatToScan = InternalKanban, _WhatWasScanned = Manifest)
                // Normal Flow: The user pressed the "Hold Manifest" buttton (btnHoldManifest) and then chose "Yes" to confirm.
                //               (NOTE: The click handler for the button DOES call this (FixUpMainForm()) method.)
                // Error: *****NOTE***** Any of the scenarios below could happen just after an Order was completed!!
                // Error: User scanned wrong barcode (_WhatToScan = ManifestBegin, but _WhatWasScanned != Manifest)
                // Error: System or Device Error (_WhatToScan = ManifestBegin, _WhatWasScanned = Manifest, but response.ErrorMessage NOT empty)
                // Error: Error communicating with Server (_WhatToScan = ManifestBegin, _WhatWasScanned = Manifest, but response.ErrorMessage NOT empty)
                // Error: Error validating Manifest on Server (_WhatToScan = ManifestBegin, _WhatWasScanned = Manifest, but response.ErrorMessage NOT empty)
                case WhatToScan.ManifestBegin:
                    //
                    // All Scenarios??
                    //
                    // lblManifest - "Scan Manifest", Black on White, Visible
                    // lblInternalKanban - Hidden
                    // lblToyotaOWK - Hidden
                    // lblErrorMessage - Show Error, if one! (and PopUp if ShowErrorDialog = true)
                    // lblKanbanMatch - Hidden
                    // btnHoldManifest - Hidden
                    // btnReset - Make it the Exit button when we are waiting for a Manifest!!
                    // txtOutput - Depends on Scenario
                    //
                    // lblManifest - "Scan Manifest", Black on White, Visible
                    //lblManifest.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
                    lblManifest.Text = "Scan Manifest";
                    lblManifest.BackColor = System.Drawing.Color.Black;
                    lblManifest.ForeColor = System.Drawing.Color.White;
                    lblManifest.Visible = true;
                    // lblInternalKanban - Hidden
                    lblInternalKanban.Visible = false;
                    // lblToyotaOWK - Hidden
                    lblToyotaOWK.Visible = false;
                    // lblErrorMessage - Show Error, if one! (and PopUp if ShowErrorDialog = true)
                    lblErrorMessage.BackColor = System.Drawing.Color.Black;
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                    lblErrorMessage.Text = "";
                    //lblErrorMessage.Visible = false;
                    // lblKanbanMatch - Visible AND "ORDER COMPLETE" if we just finished an Order!
                    //Do we have Manifest Information?
                    lblKanbanMatch.Visible = false;
                    if (string.IsNullOrEmpty(_manifestPulling.OrderNumber) == false)
                    {
                        //We are going to ask the user to scan the ManifestBegin, so this means:
                        //      The program just started (NO! Or "OrderNumber" would be empty in previous IF statememt!).
                        //      A Manifest was Completed.
                        //      A Manifest was put on Hold.
                        if (_serverCommandResponse.NowScanThis.Equals(WhatToScan.ManifestBegin))
                        {
                            // txtOutput - Depends on Scenario
                            //  Normal Flow: Program just started/the user just logged in. (This SHOULDN'T happen, because this method shouldn't be called until at least one Scan has been performed, we should use "InitMainForm" on startup.)
                            //  Error: User scanned wrong barcode (_WhatToScan = ManifestBegin, but _WhatWasScanned != Manifest)
                            //
                            //Did the User Complete or Hold the Manifest?
                            if (string.IsNullOrEmpty(_manifestPulling.OrderNumber) == false)
                            {
                                if (_manifestHeld)
                                {
                                    lblKanbanMatch.Text = "MANIFEST ON HOLD";
                                    lblKanbanMatch.BackColor = System.Drawing.Color.Yellow;
                                    lblKanbanMatch.ForeColor = System.Drawing.Color.Black;
                                    lblKanbanMatch.Visible = true;
                                }
                                else
                                {
                                    lblKanbanMatch.Text = "MANIFEST COMPLETE";
                                    lblKanbanMatch.BackColor = System.Drawing.Color.Green;
                                    lblKanbanMatch.ForeColor = System.Drawing.Color.Black;
                                    lblKanbanMatch.Visible = true;
                                }
                            }
                        }
                    }
                    // btnHoldManifest - Hidden
                    btnHoldManifest.Enabled = false;
                    btnHoldManifest.Visible = false;
                    // btnReset - Change it to the "Exit" button!
                    btnReset.Text = "EXIT";
                    btnReset.Enabled = true;
                    btnReset.Visible = true;
                    // txtOutput - Depends on Scenario
                    //  Normal Flow: Program just started/the user just logged in. (This SHOULDN'T happen, because this method shouldn't be called until at least one Scan has been performed, we should use "InitMainForm" on startup.)
                    //  Error: User scanned wrong barcode (_WhatToScan = ManifestBegin, but _WhatWasScanned != Manifest)
                    //PopulateOutput();
                    break;

                //Scenarios when NowScanThis = ManifestEnd:
                // Normal Flow: Currently, this can't happen!!!! Although, in the future, the Server could set "NowScanThis" = "ManifestEnd")
                case WhatToScan.ManifestEnd:
                    //
                    // All Scenarios??
                    //
                    // lblManifest - "Scan Manifest (2nd Copy)", Black on White, Visible
                    // lblInternalKanban - Hidden
                    // lblToyotaOWK - Hidden
                    // lblErrorMessage - Show Error, if one! (and PopUp if ShowErrorDialog = true)
                    // lblKanbanMatch - Hidden
                    // btnHoldManifest - Hidden
                    // btnReset - Hidden
                    // txtOutput - Depends on Scenario
                    //
                    // lblManifest - "Scan Manifest (2nd Copy)", Black on White, Visible
                    //lblManifest.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
                    lblManifest.Text = "Scan Manifest (Copy)";
                    lblManifest.BackColor = System.Drawing.Color.Black;
                    lblManifest.ForeColor = System.Drawing.Color.White;
                    lblManifest.Visible = true;
                    // lblInternalKanban - Hidden
                    lblInternalKanban.Visible = false;
                    // lblToyotaOWK - Hidden
                    lblToyotaOWK.Visible = false;
                    // lblErrorMessage - Show Error, if one! (and PopUp if ShowErrorDialog = true)
                    lblErrorMessage.BackColor = System.Drawing.Color.Black;
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                    lblErrorMessage.Text = "";
                    //lblErrorMessage.Visible = false;
                    // lblKanbanMatch - Hidden
                    lblKanbanMatch.Visible = false;
                    // btnHoldManifest - Hidden
                    btnHoldManifest.Enabled = false;
                    btnHoldManifest.Visible = false;
                    // btnReset - Hidden
                    btnReset.Enabled = false;
                    btnReset.Visible = false;
                    // txtOutput - Depends on Scenario
                    //PopulateOutput();
                    break;
                
                //Scenarios when NowScanThis = InternalKanban:
                // Normal Flow: The user just started an order by scanning ManifestBegin (_WhatToScan = ManifestBegin, _WhatWasScanned = ManifestBegin).
                // Normal Flow: The user just completed a "Kanban Match" by scanning the correct ToyotaOWK (_WhatToScan = ToyotaOWK, _WhatWasScanned = ToyotaOWK2D, response.ErrorMessage IS empty!). 
                // Normal Flow: The user pressed the "Reset" button, AFTER scanning the InternalKanban (_WhatToScan = ToyotaOWK, _WhatWasScanned = ???). 
                //              (NOTE: Do we want the click handler for the button to call this method, or just handle the UI????)
                // Normal Flow: The user wasn't able to match the PREVIOUS InternalKanban scan with the proper ToyotaOWK scan. (_WhatToScan = ToyotaOWK, _WhatWasScanned = InternalKanban or ??? if button click handler is used). 
                //              (Does this require the user to press the "Reset" button? YES! Let's use that logic...they can continue trying to scan the ToyotaOWK, until they press "RESET". If so, just use item above!)
                //              (NOTE: Do we want the click handler for the button to call this method, or just handle the UI????)
                // btnReset???  See the "NOTE""s above!!
                // Error: User scanned wrong barcode (_WhatToScan = InternalKanban, but _WhatWasScanned != InternalKanban)
                // Error: System or Device Error (_WhatToScan = InternalKanban, _WhatWasScanned = InternalKanban, but response.ErrorMessage NOT empty)
                // Error: Error communicating with Server (_WhatToScan = InternalKanban, _WhatWasScanned = InternalKanban, but response.ErrorMessage NOT empty)
                // Error: Error validating InternalKanban on Server (_WhatToScan = InternalKanban, _WhatWasScanned = InternalKanban, but response.ErrorMessage NOT empty)
                case WhatToScan.InternalKanban:
                    //
                    // All Scenarios??
                    //
                    // lblManifest - "Manifest #<OrderNumber>", Black on Green, Visible
                    // lblInternalKanban - "Scan Internal Kanban", Black on White, Visible
                    // lblToyotaOWK - Hidden
                    // lblErrorMessage -  Show Error, if one! (and PopUp if ShowErrorDialog = true)
                    // lblKanbanMatch - Visible if Match just performed! (_WhatToScan = ToyotaOWK AND _WhatWasScanned AND response.ErrorMessage IS empty!)
                    // btnHoldManifest - Visible and Enabled!
                    // btnReset - Hidden
                    // txtOutput - Normal Output (UserName, Order information, Kanban information)
                    //
                    // lblManifest - "Manifest #<OrderNumber>", Black on Green, Visible
                    //lblManifest.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
                    lblManifest.Text = "Manifest #" + _manifestPulling.OrderNumber;
                    lblManifest.BackColor = System.Drawing.Color.Green;
                    lblManifest.ForeColor = System.Drawing.Color.Black;
                    lblManifest.Visible = true;
                    // lblInternalKanban - "Scan Internal Kanban", Black on White, Visible
                    //lblInternalKanban.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
                    lblInternalKanban.Text = "Scan Internal Kanban";
                    lblInternalKanban.BackColor = System.Drawing.Color.Black;
                    lblInternalKanban.ForeColor = System.Drawing.Color.White;
                    lblInternalKanban.Visible = true;
                    // lblToyotaOWK - Hidden
                    lblToyotaOWK.Visible = false;
                    // lblErrorMessage -  Show Error, if one! (and PopUp if ShowErrorDialog = true)
                    lblErrorMessage.BackColor = System.Drawing.Color.Black;
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                    lblErrorMessage.Text = "";
                    //lblErrorMessage.Visible = false;
                    // lblKanbanMatch -
                    // Don't have to check "ErrorMessage", because If we are scanning "InternalKanban" next
                    //   AND we were just supposed to scan "ToyotaOWK"
                    //   AND "ToyotaOWK" was just scanned
                    //   THEN we MUST have just gotten a MATCH!
                    // lblKanbanMatch - Visible if Match just performed! (_WhatToScan = ToyotaOWK AND _WhatWasScanned = ToyotaOWK2D)
                    if (_WhatToScan.Equals(WhatToScan.ToyotaOWK) && _WhatWasScanned.Equals(WhatWasScanned.ToyotaOWK2D) && string.IsNullOrEmpty(response.LogErrorMessage) && string.IsNullOrEmpty(response.DisplayErrorMessage))
                    {
                        lblKanbanMatch.Text = "KANBAN MATCH";
                        lblKanbanMatch.BackColor = System.Drawing.Color.Green;
                        lblKanbanMatch.ForeColor = System.Drawing.Color.Black;
                        lblKanbanMatch.Visible = true;
                    }
                    else
                    {
                        lblKanbanMatch.Visible = false;
                    }
                    // btnHoldManifest - Visible and Enabled!
                    btnHoldManifest.Enabled = true;
                    btnHoldManifest.Visible = true;
                    // btnReset - Hidden
                    btnReset.Enabled = false;
                    btnReset.Visible = false;
                    // txtOutput - Normal Output (UserName, Order information, Kanban information)
                    //PopulateOutput();
                    break;

                //Scenarios when NowScanThis = ToyotaOWK:
                // Normal Flow: The user is pulling an order and just scanned the InternalKanban (_WhatToScan = InternalKanban, _WhatWasScanned = InternalKanban).
                // Normal Flow: The user just completed a "Kanban MisMatch" by scanning the wrong ToyotaOWK (_WhatToScan = ToyotaOWK, _WhatWasScanned = ToyotaOWK2D). 
                //              (What didn't match? From the Manifest: Order #? Supplier Code? Dock Code? NAMC Destination? Or the Internal Part Number on the InternalKanban?)
                //              (Show the User a "PopUp", which will require him/her to stop scanning and click on it...the user can continue trying to scan the correct ToyotaOWK until he/she presses the "Reset" button.)
                // Normal Flow: The user just completed a "Kanban MisMatch" by scanning an UnKnown barcode (_WhatToScan = ToyotaOWK, _WhatWasScanned = UnKnown). 
                //              (Show the User a "PopUp", which will require him/her to stop scanning and click on it...the user can continue trying to scan the correct ToyotaOWK until he/she presses the "Reset" button.)
                // Error: User scanned wrong barcode (actually, the same as a "Kanban MisMatch" above) (_WhatToScan = ToyotaOWK, but _WhatWasScanned != InternalKanban)
                // Error: System or Device Error (_WhatToScan = ToyotaOWK, _WhatWasScanned = ToyotaOWK2D, but response.ErrorMessage NOT empty -- THIS IS THE SAME AS A "Kanban MisMatch!")
                // Error: Error communicating with Server (_WhatToScan = ToyotaOWK, _WhatWasScanned = ToyotaOWK2D, but response.ErrorMessage NOT empty -- THIS IS THE SAME AS A "Kanban MisMatch!")
                // Error: Error validating ToyotaOWK on Server (_WhatToScan = ToyotaOWK, _WhatWasScanned = ToyotaOWK2D, but response.ErrorMessage NOT empty -- THIS IS THE SAME AS A "Kanban MisMatch!")
                //
                //*****(In DoToyotaOWK, increment the kanban in KanbansScanned!!!)*****
                case WhatToScan.ToyotaOWK:
                    //
                    // All Scenarios??
                    //
                    // lblManifest - "Manifest #<OrderNumber>", Black on Green, Visible
                    // lblInternalKanban - "Internal Part #<InternalPartNumber>", Black on Green, Visible
                    // lblToyotaOWK - "Scan Toyota Kanban"
                    // lblErrorMessage -  Show Error, if one! (and PopUp if ShowErrorDialog = true)
                    // lblKanbanMatch - Hidden
                    // btnHoldManifest - Hidden
                    // btnReset - Visible and Enabled!
                    // txtOutput - Normal Output (UserName, Order information, Kanban information)
                    //
                    // lblManifest - "Manifest #<OrderNumber>", Black on Green, Visible
                    //lblManifest.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
                    lblManifest.Text = "Manifest #" + _manifestPulling.OrderNumber;
                    lblManifest.BackColor = System.Drawing.Color.Green;
                    lblManifest.ForeColor = System.Drawing.Color.Black;
                    lblManifest.Visible = true;
                    // lblInternalKanban - "Internal Part #<InternalPartNumber>", Black on Green, Visible
                    //lblInternalKanban.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
                    lblInternalKanban.Text = "Internal Part #" + _lastScannedPartNumber;
                    lblInternalKanban.BackColor = System.Drawing.Color.Green;
                    lblInternalKanban.ForeColor = System.Drawing.Color.Black;
                    lblInternalKanban.Visible = true;
                    // lblToyotaOWK - "Scan Toyota Kanban"
                    lblToyotaOWK.Text = "Scan Toyota Kanban";
                    lblToyotaOWK.BackColor = System.Drawing.Color.Black;
                    lblToyotaOWK.ForeColor = System.Drawing.Color.White;
                    lblToyotaOWK.Visible = true;
                    // lblErrorMessage -  Show Error, if one! (and PopUp if ShowErrorDialog = true)
                    lblErrorMessage.BackColor = System.Drawing.Color.Black;
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                    lblErrorMessage.Text = "";
                    //lblErrorMessage.Visible = false;
                    // lblKanbanMatch - Hidden
                    lblKanbanMatch.Visible = false;
                    // btnHoldManifest - Hidden
                    btnHoldManifest.Enabled = false;
                    btnHoldManifest.Visible = false;
                    // btnReset - Visible and Enabled!
                    btnReset.Text = "RESET";
                    btnReset.Enabled = true;
                    btnReset.Visible = true;
                    // txtOutput - Normal Output (UserName, Order information, Kanban information)
                    //PopulateOutput();
                    break;

                default: // Should only catch WhatWasScanned.UnKnown
                    //MessageBox.Show(e.DecodeException.Message);
                    Helpers.ShowError("Error in FixUpMainForm(), response.NowScanThis value unknown (" + response.NowScanThis.ToString() + ")");
                    break;
            }

            PopulateOutput(); // CAN'T WE JUST DO IT HERE??????????

            //The DisplayErrorMessage/LogErrorMessage and ShowErrorDialog are handled the same, regerdless of what is being scanned.
            if (string.IsNullOrEmpty(response.DisplayErrorMessage) == false)
            {
                lblErrorMessage.Text = response.DisplayErrorMessage;
                //lblErrorMessage.Visible = true; //Actually, its always Visible.
                //_dolphin.Sound.Play(HSM.Embedded.Hardware.Sound.SoundTypes.Failure);
                //_dolphin.Device.SetVibration(250); //Vibrate for 1/4 of a second.
            }
            else
            {
                //_dolphin.Sound.Play(HSM.Embedded.Hardware.Sound.SoundTypes.Success);
            }
            if (response.ShowErrorDialog)
            {
                //Helpers.ShowError(lblErrorMessage.Text);
                //_logger.LogError("Error from Server: " + lblErrorMessage.Text, "Error from Server: " + lblErrorMessage.Text); //Logs the Error and Shows a "PopUp" Type Dialog with the ERROR Message.
                _logger.LogError(response.DisplayErrorMessage, response.LogErrorMessage); //Logs the Error and Shows a "PopUp" Type Dialog with the ERROR Message.
            }
            else if (string.IsNullOrEmpty(response.LogErrorMessage) == false) //If there is an ErrorMessage, but we aren't using the Dialog PopUp, we should Log it!
            {
                _logger.LogToEventLog(response.LogErrorMessage, true);
            }
            else if (string.IsNullOrEmpty(response.DisplayErrorMessage) == false) // We're not showing the PopUp Error and there isn't an error to log, is there an error to show (just in the "lblErrorMessage")? 
            {
                //...go ahead and log it too (Not using a "Pop Up", but are showing it in the "lblErrorMessage".
                _logger.LogToEventLog("ERROR shown to user: " + response.LogErrorMessage, false);
            }

            //Set _manifestHeld to false.
            _manifestHeld = false;
            //Set _WhatToScan = response.response.NowScanThis???
            _WhatToScan = response.NowScanThis;
        }

        private void PopulateOutput()
        {
            txtOutput.Text = _userName + ", MATCHES: " + _kanbansMatched.ToString() + "\r\n" +
                             "=====Version " + _clientVersion + "=====\r\n";

            //Do we have Manifest Information?
            if (string.IsNullOrEmpty(_manifestPulling.OrderNumber) == false)
            {
                //We are going to ask the user to scan the ManifestBegin, so this means:
                //      The program just started (NO! Or "OrderNumber" would be empty in previous IF statememt!).
                //      A Manifest was Completed.
                //      A Manifest was put on Hold.
                if (_serverCommandResponse.NowScanThis.Equals(WhatToScan.ManifestBegin))
                {
                    // txtOutput - Depends on Scenario
                    //  Normal Flow: Program just started/the user just logged in. (This SHOULDN'T happen, because this method shouldn't be called until at least one Scan has been performed, we should use "InitMainForm" on startup.)
                    //  Error: User scanned wrong barcode (_WhatToScan = ManifestBegin, but _WhatWasScanned != Manifest)
                    //
                    //Did the User Complete or Hold the Manifest?
                    if (string.IsNullOrEmpty(_manifestPulling.OrderNumber) == false)
                    {
                        if (_manifestHeld)
                        {
                            txtOutput.Text += "*MANIFEST ON HOLD!!*\r\n" +
                                              "======================\r\n";
                        }
                        else
                        {
                            txtOutput.Text += "*MANIFEST COMPLETED*\r\n" +
                                              "======================\r\n";
                        }
                    }
                }
                txtOutput.Text += "Order #: " + _manifestPulling.OrderNumber + "\r\n" +
                                  "NAMC: " + _manifestPulling.NAMC + "\r\n" +
                                  //"Dock: " + _manifestPulling.DockCode + "\r\n" +
                                  //"Main Route: " + _manifestPulling.MainRoute + "\r\n" +
                                  "Supplier Code: " + _manifestPulling.SupplierCode + "\r\n" +
                                  //"Palletization Code: " + _manifestPulling.PalletizationCode + "\r\n" +
                                  "--Parts Scanned--\r\n";
                //loop through Kanban's Scanned
                foreach (var item in _manifestPulling.KanbansScanned)
                {
                    txtOutput.Text += item.Key + ": " + item.Value + "\r\n";
                }
            }
        }

        private void BarcodeScanned(string barcode)
        {

            //6-18-18 - Minimize the logging, move this to the "What was Scanned" below...
            //_logger.Log("***Barcode Scanned*** barcode: " + barcode);

            //Clear any previous Error.
            //OR.. should we do this in "FixUpMainForm()"??
            _serverCommandResponse.LogErrorMessage = "";
            _serverCommandResponse.DisplayErrorMessage = "";
            _serverCommandResponse.ShowErrorDialog = false;

            //=====================================
            //=== Process the Barcode ===
            //=====================================
            try
            {
                //Determine WhatWasScanned
                _WhatWasScanned = WhichBarcode(barcode);
                //6-4-18 - Minimize the logging, moved barcode from above, to here.
                if (_logBarcodes)
                {
                    _logger.Log("WhatWasScanned =  " + _WhatWasScanned.ToString() + "  ***Barcode Scanned*** barcode: " + barcode);
                }

                //A Manifest may be scanned when waiting for three different scan types:
                //  ManifestBegin,
                //  ManifestEnd or
                //  InternalKanban.
                if (_WhatWasScanned.Equals(WhatWasScanned.Manifest) &&
                            (_WhatToScan.Equals(WhatToScan.ManifestBegin) ||
                            _WhatToScan.Equals(WhatToScan.ManifestEnd) ||
                            _WhatToScan.Equals(WhatToScan.InternalKanban)))
                {
                    if (_WhatToScan.Equals(WhatToScan.ManifestBegin))
                    {
                        DoManifestBeginScan(barcode);
                    }
                    else if (_WhatToScan.Equals(WhatToScan.ManifestEnd))
                    {
                        DoManifestEndScan(barcode);
                    }
                    else if (_WhatToScan.Equals(WhatToScan.InternalKanban))
                    {
                        //TODO: If we are looking for a "new" KanBan (Internal or Toyota) and the user scans a manifest,
                        //TOTO: just ask the user if this is the "End Manifest"!
                        //TODO: Actually, this decision should be made on the Server!! Right??? Or Here??
                        DoManifestEndScan(barcode);
                    }
                }
                else if (_WhatWasScanned.Equals(WhatWasScanned.InternalKanban) && _WhatToScan.Equals(WhatToScan.InternalKanban))
                {
                    DoInternalKanbanScan(barcode);
                }
                else if (_WhatWasScanned.Equals(WhatWasScanned.ToyotaOWK2D) && _WhatToScan.Equals(WhatToScan.ToyotaOWK))
                {

                    DoToyotaOWKScan(barcode);
                }
                else //Either the Scan was UnKnown or "Out of Order" make sure we log it to "ScanBad"!!
                {

                    //Either the Scan was UnKnown or "Out of Order" make sure we log it to "ScanBad"!!
                    DoUnKnownScan(barcode, false);
                }

                //Update the Main Form (User Interface)!
                FixUpMainForm(_serverCommandResponse);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _logger.LogError("An unexpected error occured, while processing a scanned barcode! (Check EventLog for Exception Mesage.)", "Exception in BarcodeScanned(): " + ex.Message + " (Inner Exception: " + ex.InnerException + ")");
                }
                else
                {
                    _logger.LogError("An unexpected error occured, while processing a scanned barcode! (Check EventLog for Exception Mesage.)", "Exception in BarcodeScanned(): " + ex.Message);
                }
            }
        }

        //Can be removed when we swtich to "straight dumb scanning controlled by Server!"
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
            if (barcode.Length == 250)
            {
                return (WhatWasScanned.ToyotaOWK2D);
            }
            //Or the Internal Kanban? (Length < 50 AND starts with a "P")
            if (barcode.Length < 50 && barcode.StartsWith("P"))
            {
                return (WhatWasScanned.InternalKanban);
            }
            //On second thought, let's NOT try to detect the ToyotaOWK1D, because there is no real way to determine it.
            //Or the OWK 1D barcode? (Length < 50 AND does NOT start with a "P")
            //if (barcode.Length < 50 && (barcode.StartsWith("P") == false))
            //{
            //    return (WhatWasScanned.ToyotaOWK1D);
            //}

            return (WhatWasScanned.UnKnown);
        }

        /// <summary>
        /// Do everything that needs to be done for a ManifestBegin ScanType.
        /// Populate the _serverCommandResponse with how to proceed using NowScanThis, ShowErrorDialog and ErrorMessage.
        /// </summary>
        /// <param name="?"></param>
        private void DoManifestBeginScan(string barcode)
        {
            //6-18-18 - Minimize the logging, don't really need this.
            //_logger.Log("*DoManifestBeginScan()* using barcode: " + barcode);

            //If just handling everything locally:
            //Else (Client/Server architecture): send Command to Server and wait for reply!

            ManifestSQL manifest = new ManifestSQL();
            if (ParseManifestBarcode(barcode, manifest))
            {
                //int manifestID = ProcessManifest(e.Message, manifest);
                int manifestID = -1;
                try
                {
                    //Create the SQL entry, get the new ID and create a new _manifestPulling object!
                    //manifestID = ManifestSQL.CreateScanManifestEntry(manifest);
                    manifestID = _pickToLightData.CreateScanManifestEntry(manifest);
                    if (manifestID < 0)
                    {
                        //_logger.LogError("ERROR Creating Manifest in SQL Database!");
                        _serverCommandResponse.DisplayErrorMessage = "ERROR Creating Manifest in SQL Database!";
                        _serverCommandResponse.LogErrorMessage = "ERROR Creating Manifest in SQL Database!";
                        _serverCommandResponse.ShowErrorDialog = true;
                        return;
                    }
                    else
                    {
                        _serverCommandResponse.NowScanThis = WhatToScan.InternalKanban;
                        _manifestPulling = new ManifestSQL(manifest); //Create a new one using the values from the parameter.
                        //_manifestPulling.Copy(manifest);
                    }
                }
                catch (SqlException ex)
                {
                    //_logger.LogError("ERROR Creating Manifest in SQL Database!");
                    _serverCommandResponse.DisplayErrorMessage = "ERROR Creating Manifest in SQL Database! (Check the Event Log for more details.)";
                    _serverCommandResponse.ShowErrorDialog = true;
                    _serverCommandResponse.LogErrorMessage = "ERROR Creating Manifest in SQL Database! SQL Exceptions: ";
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        //_logger.LogError("Index #" + i +
                        _serverCommandResponse.LogErrorMessage = _serverCommandResponse.LogErrorMessage +
                            "Index #" + i +
                            " Message: " + ex.Errors[i].Message +
                            " LineNumber: " + ex.Errors[i].LineNumber +
                            " Source: " + ex.Errors[i].Source + 
                            " Procedure: " + ex.Errors[i].Procedure;
                    }
                    return;
                }
                //
                //Else (Client/Server architecture): send Command to Server and wait for reply!
                //
                //Send the ManifestScanned Command to the PickToLightServer and get the ServerCommandRespone
                //which includes WhatToScan, UserInstructions, ErrorMessage, ProgramOutput, etc.
                //_serverCommandResponse = SendManifestCommand(manifest, manifestID);
            }
            else
            {
                //TODO: Check to see if they scanned one of the KanBan's instead and let them know its "Manifest Time".
                //TODO: But.. what if the don't have the Maifest? (They would at least have the "End" copy of the Manifest, wouldn't they???
                //lblErrorMessage.Text = "Error Parsing Manifest Scan";
                //txtOutput.Text = string.Empty;
                _serverCommandResponse.DisplayErrorMessage = "Error Parsing Manifest Barcode (not 50 characters)!";
                _serverCommandResponse.LogErrorMessage = "Error Parsing Manifest Barcode (not 50 characters)!";
                _serverCommandResponse.ShowErrorDialog = true;
            }
        }

        /// <summary>
        /// Do everything that needs to be done for a ManifestEnd ScanType.
        /// </summary>
        /// <param name="?"></param>
        private void DoManifestEndScan(string barcode)
        {
            //6-18-18 - Minimize the logging, don't really need this.
            //_logger.Log("*DoManifestEndScan()* using barcode: " + barcode);

            //This can be done when WhatToScan = ManifestEnd OR InternalKanban.

            //If just handling everything locally:
            //ManifestEnd, show PopUp and require User to choose:
            //  "Manifest Complete" (Black on Green)
            //  or "Continue Scanning" (White on Red).
            //Else (Client/Server architecture): send Command to Server and wait for reply!

            //If just handling everything locally:
            //Else (Client/Server architecture): send Command to Server and wait for reply!

            ManifestSQL manifest = new ManifestSQL();
            if (ParseManifestBarcode(barcode, manifest))
            {
                //Make sure the Manifest scanned matches the one we are working on!
                if (_manifestPulling.IsDifferent(manifest))
                {
                    _serverCommandResponse.LogErrorMessage = "WRONG Manifest!\r\nYou must scan the second copy of the Manifest to complete the order!";
                    _serverCommandResponse.DisplayErrorMessage = "WRONG Manifest!\r\nYou must scan the second copy of the Manifest to complete the order!";
                    _serverCommandResponse.ShowErrorDialog = true;
                    return;
                }
                //If just handling everything locally:
                //ManifestEnd, show PopUp and require User to choose:
                //  "Manifest Complete" (Black on Green)
                //  or "Continue Scanning" (White on Red).
                //if (MessageBox.Show("You scanned the same Manifest. Has this Order been Completed?", "Order Complete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                //{
                //    _serverCommandResponse.ErrorMessage = "You chose to continue scanning KanBans...";
                //    _serverCommandResponse.ShowErrorDialog = false;
                //    return;
                //}
                //
                //Elizabeth decided to just show "Order Complete" in the KANBAN MATCH and not prompt them here!
                //
                //using (ErrorDialog errorDialog = new ErrorDialog("Order Complete?", "You scanned the same Manifest.\r\nHas this Order been Completed?\r\nClick OK if Order is Complete\r\n or CANCEL to keep scanning\r\n", true))
                //{
                //    if (errorDialog.ShowDialog() == DialogResult.Cancel)
                //    {
                //        _serverCommandResponse.ErrorMessage = "You chose to continue scanning KanBans...";
                //        _serverCommandResponse.ShowErrorDialog = false;
                //        return;
                //    }
                //}
                //int manifestID = ProcessManifest(e.Message, manifest);
                int manifestID = -1;
                try
                {
                    //Create the SQL entry.
                    //manifestID = ManifestSQL.CreateScanManifestEntry(manifest);
                    manifestID = _pickToLightData.CreateScanManifestEntry(manifest);
                    if (manifestID < 0)
                    {
                        //_logger.LogError("ERROR Creating Manifest in SQL Database!");
                        _serverCommandResponse.LogErrorMessage = "ERROR Creating Manifest in SQL Database!";
                        _serverCommandResponse.DisplayErrorMessage = "ERROR Creating Manifest in SQL Database!";
                        _serverCommandResponse.ShowErrorDialog = true;
                        return;
                    }
                    else
                    {
                        _serverCommandResponse.NowScanThis = WhatToScan.ManifestBegin;
                        //It's the same manifest, so just leave it.
                        //_manifestPulling.Copy(manifest);
                        //_manifestPulling.Copy(manifest);
                        //Wait.... in this new version of the Class, I think we want to copy it!
                        //(The constructor also copies the KanbanMatches, so the screen count will be accurate until a new Manifest is scanned...)
                        //(At least I think so...Heck, its possible the "logic" will prevent it from showing anyway, unstil a new Begin is scanned....)
                        //We don't compare EVERY single field when comparing the manifests, so some will be different on the "End" ("B") Manifest!
                        //Therefore, I'm going to copy it, so I know "_manifestPulling" contains the "A" copy then the "B" copy.....
                        _manifestPulling = new ManifestSQL(manifest); //Create a new one using the values from the parameter.
                    }
                }
                catch (SqlException ex)
                {
                    //_logger.LogError("ERROR Creating Manifest in SQL Database!");
                    _serverCommandResponse.DisplayErrorMessage = "ERROR Creating Manifest in SQL Database!";
                    _serverCommandResponse.ShowErrorDialog = true;
                    _serverCommandResponse.LogErrorMessage = "ERROR Creating Manifest in SQL Database! SQL Exceptions: ";
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        //_logger.LogError("Index #" + i +
                        _serverCommandResponse.LogErrorMessage = _serverCommandResponse.LogErrorMessage +
                            "Index #" + i +
                            " Message: " + ex.Errors[i].Message +
                            " LineNumber: " + ex.Errors[i].LineNumber +
                            " Source: " + ex.Errors[i].Source +
                            " Procedure: " + ex.Errors[i].Procedure;
                    }
                    return;
                }
                //
                //Else (Client/Server architecture): send Command to Server and wait for reply!
                //
                //Send the ManifestEndScanned Command to the PickToLightServer and get the ServerCommandRespone
                //which includes WhatToScan, UserInstructions, ErrorMessage, ProgramOutput, etc.
                //_serverCommandResponse = SendManifestEndCommand(manifest, manifestID);
            }
            else
            {
                //TODO: Check to see if they scanned one of the KanBan's instead and let them know its "Manifest Time".
                //TODO: But.. what if the don't have the Maifest? (They would at least have the "End" copy of the Manifest, wouldn't they???
                //lblErrorMessage.Text = "Error Parsing Manifest Scan";
                //txtOutput.Text = string.Empty;
                _serverCommandResponse.DisplayErrorMessage = "Error Parsing Manifest Barcode (not 50 characters)!";
                _serverCommandResponse.LogErrorMessage = "Error Parsing Manifest Barcode (not 50 characters)!";
                _serverCommandResponse.ShowErrorDialog = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manifestBarcode"></param>
        /// <param name="manifest"></param>
        /// <returns></returns>
        private void DoInternalKanbanScan(string barcode)
        {
            //6-18-18 - Minimize the logging, don't really need this.
            //_logger.Log("*DoInternalKanbanScan()* using barcode: " + barcode);

            //If just handling everything locally:
            //Else (Client/Server architecture): send Command to Server and wait for reply!

            InternalKanbanSQL internalKanban = new InternalKanbanSQL();
            if (ParseInternalKanbanBarcode(barcode, internalKanban))
            {
                //int manifestID = ProcessManifest(e.Message, manifest);
                int internalKanbanID = -1;
                try
                {
                    //Create the SQL entry.
                    //internalKanbanID = InternalKanbanSQL.CreateScanInternalKanbanEntry(internalKanban);
                    internalKanbanID = _pickToLightData.CreateScanInternalKanbanEntry(internalKanban);
                    if (internalKanbanID < 0)
                    {
                        _serverCommandResponse.LogErrorMessage = "ERROR Creating InternalKanban in SQL Database!";
                        _serverCommandResponse.DisplayErrorMessage = "ERROR Creating InternalKanban in SQL Database!";
                        _serverCommandResponse.ShowErrorDialog = true;
                        return;
                    }
                    else
                    {
                        _serverCommandResponse.NowScanThis = WhatToScan.ToyotaOWK;
                        _lastScannedPartNumber = internalKanban.InternalPartNumber;
                    }
                }
                catch (SqlException ex)
                {
                    _serverCommandResponse.DisplayErrorMessage = "ERROR Creating InternalKanban in SQL Database!";
                    _serverCommandResponse.ShowErrorDialog = true;
                    _serverCommandResponse.DisplayErrorMessage = "ERROR Creating InternalKanban in SQL Database! SQL Exceptions: ";
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        //_logger.LogError("Index #" + i +
                        _serverCommandResponse.LogErrorMessage = _serverCommandResponse.LogErrorMessage +
                            "Index #" + i +
                            " Message: " + ex.Errors[i].Message +
                            " LineNumber: " + ex.Errors[i].LineNumber +
                            " Source: " + ex.Errors[i].Source +
                            " Procedure: " + ex.Errors[i].Procedure;
                    }
                    return;
                }
                //
                //Else (Client/Server architecture): send Command to Server and wait for reply!
                //
                //Send the InternalKanbanScanned Command to the PickToLightServer and get the ServerCommandRespone
                //which includes WhatToScan, UserInstructions, ErrorMessage, ProgramOutput, etc.
                //_serverCommandResponse = SendInternalKanbanScannedCommand(internalKanban, internalKanbanID);
            }
            else
            {
                _serverCommandResponse.LogErrorMessage = "Error Parsing InternalKanban Scan";
                _serverCommandResponse.DisplayErrorMessage = "Error Parsing InternalKanban Scan";
                _serverCommandResponse.ShowErrorDialog = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manifestBarcode"></param>
        /// <param name="manifest"></param>
        /// <returns></returns>
        private void DoToyotaOWKScan(string barcode)
        {
            //6-18-18 - Minimize the logging, don't really need this.
            //_logger.Log("*DoToyotaOWKScan()* using barcode: " + barcode);

            //CONTINUE HERE!

            //If just handling everything locally:
            //Else (Client/Server architecture): send Command to Server and wait for reply!

            ToyotaOWKSQL toyotaOWK = new ToyotaOWKSQL();
            if (ParseToyotaOWKBarcode(barcode, toyotaOWK))
            {
                //int toyotaOWKID = ProcessToyotaOWK(e.Message, toyotaOWK);
                int toyotaOWKID = -1;
                try
                {
                    //First, lets make sure this is a match with the InternalKanban!
                    if (toyotaOWK.SupplierInformation.StartsWith(_lastScannedPartNumber))
                    {
                        if (toyotaOWK.OrderNumber.Equals(_manifestPulling.OrderNumber))
                        {
                            //Create the SQL entry.
                            //toyotaOWKID = ToyotaOWKSQL.CreateToyotaOWKScanEntry(toyotaOWK);
                            toyotaOWKID = _pickToLightData.CreateToyotaOWKScanEntry(toyotaOWK);
                            if (toyotaOWKID < 0)
                            {
                                //_logger.LogError("ERROR Creating Manifest in SQL Database!");
                                _serverCommandResponse.LogErrorMessage = "ERROR Creating ToyotaOWK in SQL Database!";
                                _serverCommandResponse.DisplayErrorMessage = "ERROR Creating ToyotaOWK in SQL Database!";
                                _serverCommandResponse.ShowErrorDialog = true;
                                return;
                            }
                            else
                            {
                                _serverCommandResponse.NowScanThis = WhatToScan.InternalKanban;
                                //If Kanban Match, be sure we increment the count!
                                _manifestPulling.IncrementKanban(toyotaOWK.KanbanNumber);
                                //And increment the Total Matches
                                _kanbansMatched++;
                            }
                        }
                        else
                        {
                            _serverCommandResponse.DisplayErrorMessage = "WRONG ORDER!!!\r\nPulling Order (" + _manifestPulling.OrderNumber + ")  Label Order (" + toyotaOWK.OrderNumber + ")";
                            _serverCommandResponse.ShowErrorDialog = true;
                            DoUnKnownScan(barcode, true); //Log it so we have a history of a "bad" scan!!
                        }
                    }
                    else
                    {
                        _serverCommandResponse.DisplayErrorMessage = "KANBAN MISMATCH!\r\n\r\n(OWK and Internal Label (" + _lastScannedPartNumber + ") DO NOT match)";
                        _serverCommandResponse.ShowErrorDialog = true;
                        DoUnKnownScan(barcode, true); //Log it so we have a history of a "bad" scan!!
                    }
                }
                catch (SqlException ex)
                {
                    _serverCommandResponse.DisplayErrorMessage = "ERROR Creating ToyotaOWK in SQL Database!";
                    _serverCommandResponse.ShowErrorDialog = true;
                    _serverCommandResponse.LogErrorMessage = "ERROR Creating ToyotaOWK in SQL Database! SQL Exceptions: ";
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        //_logger.LogError("Index #" + i +
                        _serverCommandResponse.LogErrorMessage = _serverCommandResponse.LogErrorMessage +
                            "Index #" + i +
                            " Message: " + ex.Errors[i].Message +
                            " LineNumber: " + ex.Errors[i].LineNumber +
                            " Source: " + ex.Errors[i].Source +
                            " Procedure: " + ex.Errors[i].Procedure;
                    }
                    return;
                }
                //
                //Else (Client/Server architecture): send Command to Server and wait for reply!
                //
                //Send the ManifestScanned Command to the PickToLightServer and get the ServerCommandRespone
                //which includes WhatToScan, UserInstructions, ErrorMessage, ProgramOutput, etc.
                //_serverCommandResponse = SendManifestCommand(manifest, manifestID);
            }
            else
            {
                _serverCommandResponse.LogErrorMessage = "Error Parsing ToyotaOWK Scan";
                _serverCommandResponse.DisplayErrorMessage = "Error Parsing ToyotaOWK Scan";
                _serverCommandResponse.ShowErrorDialog = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manifestBarcode"></param>
        /// <param name="manifest"></param>
        /// <returns></returns>
        private void DoUnKnownScan(string barcode, bool kanbanMismatch)
        {
            //6-18-18 - Minimize the logging, don't really need this.
            //_logger.Log("*DoUnKnownScan()* using barcode: " + barcode);

            //Either the Scan was UnKnown or "Out of Order" make sure we log it to "ScanBad"!!

            //CONTINUE HERE!

            //If just handling everything locally:
            //Else (Client/Server architecture): send Command to Server and wait for reply!

            //ScanUnKnownSQL unknownSQL = new ScanUnKnownSQL();
            int unknownID = -1;
            try
            {
                //unknownID = ScanUnKnownSQL.CreateScanUnKnownEntry(barcode, _WhatToScan.ToString(), _WhatWasScanned.ToString(), _deviceName, _deviceIdentifier, _userName, _appName);
                unknownID = _pickToLightData.CreateScanUnKnownEntry(barcode, _WhatToScan.ToString(), _WhatWasScanned.ToString(), _deviceName, _deviceIdentifier, _userName, _appName);
                if (unknownID < 0)
                {
                    //_logger.LogError("ERROR Creating Manifest in SQL Database!");
                    _serverCommandResponse.LogErrorMessage = "ERROR Creating ScanUnknown in SQL Database!";
                    _serverCommandResponse.DisplayErrorMessage = "ERROR Creating ScanUnknown in SQL Database!";
                    _serverCommandResponse.ShowErrorDialog = true;
                    return;
                }
                else if (kanbanMismatch)
                {
                    return; //Use the error that describes the "mismatch".
                }
                else if (_WhatWasScanned.Equals(WhatWasScanned.UnKnown))
                {
                    _serverCommandResponse.LogErrorMessage = "SCAN ERROR: Unable to determine what was scanned!!";
                    _serverCommandResponse.DisplayErrorMessage = "SCAN ERROR: Unable to determine what was scanned!!";
                    _serverCommandResponse.ShowErrorDialog = true;
                }
                else
                {
                    _serverCommandResponse.LogErrorMessage = "SCAN ERROR: Was expecting " + _WhatToScan.ToString() + ", but scanned " + _WhatWasScanned.ToString();
                    _serverCommandResponse.DisplayErrorMessage = "SCAN ERROR: Was expecting " + _WhatToScan.ToString() + ", but scanned " + _WhatWasScanned.ToString();
                    _serverCommandResponse.ShowErrorDialog = true;
                }
            }
            catch (SqlException ex)
            {
                _serverCommandResponse.DisplayErrorMessage = "ERROR Creating ScanUnknown in SQL Database!";
                _serverCommandResponse.ShowErrorDialog = true;
                _serverCommandResponse.LogErrorMessage = "ERROR Creating ScanUnknown in SQL Database! SQL Exceptions: ";
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    //_logger.LogError("Index #" + i +
                    _serverCommandResponse.LogErrorMessage = _serverCommandResponse.LogErrorMessage +
                        "Index #" + i +
                        " Message: " + ex.Errors[i].Message +
                        " LineNumber: " + ex.Errors[i].LineNumber +
                        " Source: " + ex.Errors[i].Source +
                        " Procedure: " + ex.Errors[i].Procedure;
                }
                return;
            }

            //
            //Else (Client/Server architecture): send Command to Server and wait for reply!
            //
            //Send the ManifestScanned Command to the PickToLightServer and get the ServerCommandRespone
            //which includes WhatToScan, UserInstructions, ErrorMessage, ProgramOutput, etc.
            //_serverCommandResponse = SendManifestCommand(manifest, manifestID);
        }

        public bool ParseManifestBarcode(string manifestBarcode, ManifestSQL manifest)
        {
            //_logger.Log("*ParseManifestBarcode()* using barcode: " + manifestBarcode);

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
                //Helpers.ShowError("ERROR: The Manifest barcode should be 50 characters!");
                //_logger.LogError("ERROR: The Manifest barcode should be 50 characters!");
                return (false);
            }

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
            //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
            //manifest.Created = DateTime.Now;
            manifest.CreatedBy = _appName;
            manifest.Decoded = true;
            manifest.ImagePath = "";

            return (true);
        }

        public bool ParseInternalKanbanBarcode(string internalKanbanBarcode, InternalKanbanSQL internalKanban)
        {
            //_logger.Log("*ParseInternalKanbanBarcode()* using barcode: " + internalKanbanBarcode);

            //Only one field and it starts with a P.. strip it.

            //The internalKanbanBarcode should be at least 3 characters (probably more like 5 or 6??).
            if (internalKanbanBarcode.Length < 3)
            {
                _logger.LogToEventLog("ERROR: The InternalKanban barcode should be 3 characters or more!", true);
                return (false);
            }

            if (internalKanbanBarcode[0] != 'P')
            {
                _logger.LogToEventLog("ERROR: The InternalKanban barcode should start with a P!", true);
                return (false);
            }

            internalKanban.InternalPartNumber = internalKanbanBarcode.Substring(1); //Skip the first character which we have guaranteed is a 'P'.
            internalKanban.KanbanNumber = "";
            internalKanban.DeviceName = _deviceName;
            internalKanban.DeviceIdentifier = _deviceIdentifier;
            internalKanban.Scanned = DateTime.Now;
            internalKanban.ScannedBy = _userName;
            //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
            //internalKanban.Created = DateTime.Now;
            internalKanban.CreatedBy = _appName;

            return (true);
        }

        public bool ParseToyotaOWKBarcode(string toyotaOWKBarcode, ToyotaOWKSQL toyotaOWK)
        {
            //_logger.Log("*ParseToyotaOWKBarcode()* using barcode: " + toyotaOWKBarcode);

            /*
            if (Barcode.StartsWith("C"))
            {
                newScanOWK.Decoded = true;
                newScanOWK.SupplierInformation = Barcode.Substring(1, 30).Trim();
                newScanOWK.SupplierCode = Barcode.Substring(31, 5).Trim();
                newScanOWK.DockCode = Barcode.Substring(36, 2).Trim();
                newScanOWK.KanbanNumber = Barcode.Substring(38, 4).Trim();
                newScanOWK.PartNumber = Barcode.Substring(42, 12).Trim();
                newScanOWK.LineSideAddress = Barcode.Substring(54, 10).Trim();
                newScanOWK.StoreAddress = Barcode.Substring(64, 10).Trim();
                newScanOWK.LotSize = Barcode.Substring(74, 5).Trim();
                newScanOWK.SupplierName = Barcode.Substring(79, 20).Trim();
                newScanOWK.MainRoute = Barcode.Substring(99, 9).Trim();
                newScanOWK.SubRoute = Barcode.Substring(108, 9).Trim();
                newScanOWK.UnloadDate = Barcode.Substring(117,8).Trim();
                newScanOWK.ShipDate = Barcode.Substring(125, 8).Trim();
                newScanOWK.ShipTime = Barcode.Substring(133, 6).Trim();
                newScanOWK.OrderNumber = Barcode.Substring(139, 12).Trim();
                newScanOWK.BoxNumber = Barcode.Substring(151, 4).Trim();
                newScanOWK.BoxTotal = Barcode.Substring(155, 4).Trim();
                newScanOWK.NAMCDestination = Barcode.Substring(159, 5).Trim();
                newScanOWK.InternalKanbanKey = Barcode.Substring(164, 17).Trim();
                newScanOWK.LabelLocationIndicator = Barcode.Substring(181, 2).Trim();
                newScanOWK.NAMCData1 = Barcode.Substring(183, 10).Trim();
                newScanOWK.NAMCData2 = Barcode.Substring(193, 2).Trim();
                newScanOWK.PalletizationCode = Barcode.Substring(195, 2).Trim();
                newScanOWK.NAMCData3 = Barcode.Substring(197, 10).Trim();
                newScanOWK.NAMCData4 = Barcode.Substring(207, 2).Trim();
                newScanOWK.Filler = Barcode.Substring(209, 41).Trim();
            }
            else
            {
                newScanOWK.Decoded = false;
            }
         */

            //The toyotaOWKBarcode should be 50 characters.
            if (toyotaOWKBarcode.Length != 250)
            {
                _logger.LogToEventLog("ERROR: The ToyotaOWK barcode should be 250 characters!",true);
                return (false);
            }

            //The toyotaOWKBarcode should start with a 'C'!.
            if (toyotaOWKBarcode[0] != 'C')
            {
                _logger.LogToEventLog("ERROR: The ToyotaOWK barcode should start with a C!", true);
                return (false);
            }

            toyotaOWK.Barcode = toyotaOWKBarcode;
            toyotaOWK.SupplierInformation = toyotaOWKBarcode.Substring(1, 30).Trim();
            toyotaOWK.SupplierCode = toyotaOWKBarcode.Substring(31, 5).Trim();
            toyotaOWK.DockCode = toyotaOWKBarcode.Substring(36, 2).Trim();
            toyotaOWK.KanbanNumber = toyotaOWKBarcode.Substring(38, 4).Trim();
            toyotaOWK.PartNumber = toyotaOWKBarcode.Substring(42, 12).Trim();
            toyotaOWK.LineSideAddress = toyotaOWKBarcode.Substring(54, 10).Trim();
            toyotaOWK.StoreAddress = toyotaOWKBarcode.Substring(64, 10).Trim();
            toyotaOWK.LotSize = toyotaOWKBarcode.Substring(74, 5).Trim();
            toyotaOWK.SupplierName = toyotaOWKBarcode.Substring(79, 20).Trim();
            toyotaOWK.MainRoute = toyotaOWKBarcode.Substring(99, 9).Trim();
            toyotaOWK.SubRoute = toyotaOWKBarcode.Substring(108, 9).Trim();
            toyotaOWK.UnloadDate = toyotaOWKBarcode.Substring(117, 8).Trim();
            toyotaOWK.ShipDate = toyotaOWKBarcode.Substring(125, 8).Trim();
            toyotaOWK.ShipTime = toyotaOWKBarcode.Substring(133, 6).Trim();
            toyotaOWK.OrderNumber = toyotaOWKBarcode.Substring(139, 12).Trim();
            toyotaOWK.BoxNumber = toyotaOWKBarcode.Substring(151, 4).Trim();
            toyotaOWK.BoxTotal = toyotaOWKBarcode.Substring(155, 4).Trim();
            toyotaOWK.NAMCDestination = toyotaOWKBarcode.Substring(159, 5).Trim();
            toyotaOWK.InternalKanbanKey = toyotaOWKBarcode.Substring(164, 17).Trim();
            toyotaOWK.LabelLocationIndicator = toyotaOWKBarcode.Substring(181, 2).Trim();
            toyotaOWK.NAMCData1 = toyotaOWKBarcode.Substring(183, 10).Trim();
            toyotaOWK.NAMCData2 = toyotaOWKBarcode.Substring(193, 2).Trim();
            toyotaOWK.PalletizationCode = toyotaOWKBarcode.Substring(195, 2).Trim();
            toyotaOWK.NAMCData3 = toyotaOWKBarcode.Substring(197, 10).Trim();
            toyotaOWK.NAMCData4 = toyotaOWKBarcode.Substring(207, 2).Trim();
            toyotaOWK.Filler = toyotaOWKBarcode.Substring(209, 41).Trim();

            toyotaOWK.DeviceName = _deviceName;
            toyotaOWK.DeviceIdentifier = _deviceIdentifier;
            toyotaOWK.Scanned = DateTime.Now;
            toyotaOWK.ScannedBy = _userName;
            //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
            //toyotaOWK.Created = DateTime.Now;
            toyotaOWK.CreatedBy = _appName;
            toyotaOWK.Decoded = true;
            toyotaOWK.ImagePath = "";

            return (true);
        }

        //!!!!!!!!!!!
        //Change this to "RegisterClient()" and probably a new "Command" name other than "Ping"....
        //!!!!!!!!!!!
        private bool SendPingCommand()
        {
            try
            {
                string commandToSend = "";
                //6-18-18 - Add Version of software to PING command!
                commandToSend = P2L_COMMAND_Ping + sGS + _deviceName + sGS + _clientVersion;

                //if (_pickToLightServerIP.Length > 10 && _pickToLightServerIP.Split('.').Length == 4) //At least 11 characters (10.10.10.10) and 3 periods/dots.
                if (_pickToLightServerIPAddress != null)
                {
                    //TCPClient tcpClient = new TCPClient(_pickToLightServerIP, port);
                    TCPClient tcpClient = new TCPClient(_logger, _pickToLightServerIPAddress, _port, _logNetworkTraffic);
                    StringBuilder commandResponse = new StringBuilder();
                    tcpClient.SendCommand(commandToSend, commandResponse); // No SQL Logging, because no PickToLightData allocated yet!

                    string[] words = commandResponse.ToString().Split(caGS);

                    ///
                    ///Let's not replace the SQL Connection String received from the Server,
                    ///at least until I get the Auto-Update figured out!
                    ///
                    //if(words.Length > 2)
                    //{
                    //    //string _sqlConnectionString = @"Server=GT-transACTION\SQLExpress;Database=PickToLight; Uid=PickToLight; pwd=P1ckT0L1ght;";
                    //    //string _sqlConnectionString = @"Server=HQ-SQL02;Database=PickToLight; Uid=PickToLight; pwd=P1ckT0L1ght;";
                    //    if (string.IsNullOrEmpty(words[2]) == false)
                    //    {
                    //        _sqlConnectionString = words[2];
                    //    }
                    //}

                    ///*************************************************************************/
                    ///*************************************************************************/
                    ////Use the default SQL Connection String if one isn't received!
                    ///*************************************************************************/
                    ///*************************************************************************/
                    //_pickToLightData = new PickToLightData(_sqlConnectionString); //Create an instance of PickToLightData with the SQL Connection String.
                    //_logger = new Logger(_appName, _deviceName, _pickToLightData); //Create a new instance of _logger with the PickToLightData instance (instead of null).
                    //_logger.LogMode = Logger.LogModes.SQLLog;
                    //_logger.Log("The P2L_COMMAND_Ping returned the SQLDBConnectionString (" + _sqlConnectionString + ").");

                    //if (words.Length > 3)
                    //{
                    //    //Disable Logging for Barcode and/or Network Traffic
                    //    if (string.IsNullOrEmpty(words[3]) == false)
                    //    {
                    //        if (words[3].ToLower().Contains("disablebarcodelog"))
                    //        {
                    //            _logBarcodes = false;
                    //            _logger.Log("Turn Barcode Logging Off!");
                    //        }
                    //        if (words[3].ToLower().Contains("disablenetworklog"))
                    //        {
                    //            _logNetworkTraffic = false;
                    //            _logger.Log("Turn Network Logging Off!");
                    //        }
                    //    }
                    //}

                    if(words.Length < 1) //No GS found!
                    {
                        _logger.LogToEventLog("Error Pinging PickToLightServer, no GS received in response!", true);
                        return (false);
                    }
                    if(words[0].Equals(P2L_COMMAND_Ping) == false)
                    {
                        _logger.LogToEventLog("Error Pinging PickToLightServer, the P2L_COMMAND_Ping wasn't returned (" + words[0] + ").", true);
                        return (false);
                    }
                    if(words[1].Equals(_deviceName))
                    {
                        return (true);
                    }
                    else //The device was denied access!!!
                    {
                        _logger.Log("This device is being denied access by PickToLightServer!");  //Not LogError(), because we will "PopUp" a different Error Message.
                        Helpers.ShowError(words[1]);
                        return (false);
                    }
                }
                else
                {
                    _logger.LogError("The PickToLight Server IP is invalid or not specified!", "The PickToLightServerIP setting is invalid or not specified! (This shouldn't get this far, it should be caught when Loading the settings during startup!)");
                    return (false);
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
                ////LogToEventLog("Exception in Logger.LogToSQL() _serviceName (" + _serviceName + "), Thread.CurrentThread.Name (" + Thread.CurrentThread.Name + "), Thread.CurrentThread.ManagedThreadId (" + Thread.CurrentThread.ManagedThreadId + "), stringToLog(" + stringToLog + "). Exception: " + ex.Message, true);
                //_logger.Log(errorMessage);  //Not LogError(), because we will "PopUp" a different Error Message.
                //Helpers.ShowError("Exception in SendPingCommand().");
                _logger.LogError(errorMessage, "Error initializing Network Communictions with Pick To Light Server! (Check EventLog for Exception Mesage.)");
            }

            return(false);
        }

        private void btnHoldManifest_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Are you SURE you want to stop pulling the parts for this Manifest, before it has been completed?", "Hold Manifest?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            //{
            //    _manifestHeld = true;
            //    _serverCommandResponse.NowScanThis = WhatToScan.ManifestBegin;
            //    _serverCommandResponse.ShowErrorDialog = false;
            //    _serverCommandResponse.ErrorMessage = "The previous Manifest is now on HOLD...";
            //    _logger.Log("***HOLD Manifest*** The user (" + _userName + ") placed the current Manifest (" + _manifestPulling.OrderNumber + ") on hold.");
            //    FixUpMainForm(_serverCommandResponse);
            //}
            using (ErrorDialog errorDialog = new ErrorDialog("Hold Manifest?", "\r\nStop pulling parts for this Manifest, before it has been completed?\r\n\r\nClick OK to put Manifest on HOLD\r\n or CANCEL to keep scanning", true))
            {
                if (errorDialog.ShowDialog() == DialogResult.OK)
                {
                    _manifestHeld = true;
                    _serverCommandResponse.NowScanThis = WhatToScan.ManifestBegin;
                    _serverCommandResponse.ShowErrorDialog = false;
                    _serverCommandResponse.DisplayErrorMessage = "The previous Manifest is now on HOLD...";
                    _logger.Log("***HOLD Manifest*** The user (" + _userName + ") placed the current Manifest (" + _manifestPulling.OrderNumber + ") on hold.");
                    FixUpMainForm(_serverCommandResponse);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //First, see if the button is being used to Exit the application!
            if (btnReset.Text.Equals("EXIT"))
            {
                //if (MessageBox.Show("Are you SURE you want to Exit?", "Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                //{
                //    _logger.Log("***EXIT*** The user (" + _userName + ") chose to Exit the application on (" + _deviceName + ").");
                //    Application.Exit();
                //    //And just in case!
                //    this.Close();
                //}
                using (ErrorDialog errorDialog = new ErrorDialog("Exit?", "\r\nAre you SURE you want to Exit?\r\n\r\nClick OK to EXIT the program!\r\n or CANCEL to keep scanning", true))
                {
                    if (errorDialog.ShowDialog() == DialogResult.OK)
                    {
                        _logger.Log("***EXIT*** The user (" + _userName + ") chose to Exit the application on (" + _deviceName + ").");
                        Application.Exit();
                        //And just in case!
                        this.Close();
                    }
                }
            }
            else
            {
                _lastScannedPartNumber = "";
                _serverCommandResponse.NowScanThis = WhatToScan.InternalKanban;
                _serverCommandResponse.ShowErrorDialog = false; //Don't do a PopUp for this.
                _serverCommandResponse.DisplayErrorMessage = "You chose to Reset the Kanban scan.";
                _logger.Log("***Reset Kanban Match*** The user (" + _userName + ") clicked the Reset button to reset the Kanban scan/match.");
                FixUpMainForm(_serverCommandResponse); //Just handle it here???
            }
        }

        private void Main_Closed(object sender, EventArgs e)
        {
            //============================
            //=== Exit the Application ===
            //============================
            try
            {
                this.WindowState = FormWindowState.Normal;

                ////
                ////Is ANY of this necessary or does it even do anything???
                ////
                ////Restore the caption/menu bar for all the other programs!
                ////SHFullScreen((uint)WindowHandle,FullScreenState.SHFS_SHOWTASKBAR);
                ////SHFullScreen((uint)WindowHandle, FullScreenState.SHFS_SHOWSTARTICON);
                ////// must make two calls to reliably hide menu bar and sip
                ////this.Menu = mainMenu1;
                ////Restore the SIP Button.
                //SHFullScreen((uint)_WindowHandle, FullScreenState.SHFS_SHOWSIPBUTTON);

                ////--- Exit the Application ---
                ////Application.Exit();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _logger.LogError("Error exiting Application (Check EventLog for Exception Mesage.)", "Exception in Main_Closed(). Exception: " + ex.Message + " (Inner Exception: " + ex.InnerException + ")");
                }
                else
                {
                    //LogToEventLog("Exception in Logger.LogToSQL() _serviceName (" + _serviceName + "), Thread.CurrentThread.Name (" + Thread.CurrentThread.Name + "), Thread.CurrentThread.ManagedThreadId (" + Thread.CurrentThread.ManagedThreadId + "), stringToLog(" + stringToLog + "). Exception: " + ex.Message, true);
                    //Helpers.ShowError("Exception in mnuitmFileExit_Click(). Exception: " + ex.Message);
                    _logger.LogError("Error exiting Application (Check EventLog for Exception Mesage.)", "Exception in Main_Closed(). Exception: " + ex.Message);
                }
            }
        }

        private void btnHoldManifest_KeyDown(object sender, KeyEventArgs e)
        {
            //Swallow ALL Keys! We don't want the Enter key to "Click" one of the buttons, because the barcode scanner sends an Enter key at the end of the scanned text.
            e.Handled = true;
        }

        private void btnReset_KeyDown(object sender, KeyEventArgs e)
        {
            //Swallow ALL Keys! We don't want the Enter key to "Click" one of the buttons, because the barcode scanner sends an Enter key at the end of the scanned text.
            e.Handled = true;
        }
    }
}