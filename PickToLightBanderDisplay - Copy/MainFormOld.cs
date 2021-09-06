using SNA.WinService.PickToLightServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PickToLightData;

namespace PickToLightBanderDisplay
{
    //
    //Moved to UIMultiThreadExtensions.cs
    //
    ////using System;
    ////using System.Windows.Forms;

    //public static class ControlExtensions
    //{
    //    /// <summary>
    //    /// Executes the Action asynchronously on the UI thread, does not block execution on the calling thread.
    //    /// </summary>
    //    /// <param name="control"></param>
    //    /// <param name="code"></param>
    //    public static void UIThread(this Control @this, Action code)
    //    {
    //        if (@this.InvokeRequired)
    //        {
    //            @this.BeginInvoke(code);
    //        }
    //        else
    //        {
    //            code.Invoke();
    //        }
    //    }
    //}

    public partial class MainFormOld : Form
    {
        int _startingOWKID = 6323878; //4167250;

        const int iGS = 29; //ASCII Group Separator
        const char cGS = (char)29;
        const byte bGS = 0x1D;
        const string sGS = "\x1D";
        string[] saGS = { "\x1D" };
        char[] caGS = { (char)29 };

        //private TCPServer _tcpServer = null;
        private TCPServerV2 _tcpServer = null;
        int _port = 12000;   //Use 12000 for Bander Display!!!

        private static Logger _logger;

        public MainFormOld()
        {
            InitializeComponent();

            Thread.CurrentThread.Name = "Main Thread";

            _logger = new Logger("Pick To Light Bander Display");

            // Log a service start message to the Application log.
            //Even though LoadSettings() hasn't been called, the LogMode defaults to NoLogging.
            //So, this will work as expected!
            _logger.LogMainEvent("PickToLightBanderDisplay is Starting.");

            //Load the Service Settings from the Config File!
            LoadSettings();

            //Set the Windows Form Properties that don't show up in intellisense...
            //pnlPlantTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pnlPlantTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlConfirmationTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlBoxTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;


            // Harcoded / Static ScanOWK IDs with delay for testing Form layout...
            //
            //PickToLightData.ScanOWK scanOWK = PickToLightData.ScanOWK.GetScanEntry(4167250);
            //if(scanOWK != null) //Is this even possible?
            //{
            //    Populate(scanOWK);
            //}
            //Thread.Sleep(5000);
            //scanOWK = PickToLightData.ScanOWK.GetScanEntry(4167251);
            //if(scanOWK != null) //Is this even possible?
            //{
            //    Populate(scanOWK);
            //}
            //Thread.Sleep(5000);
            //scanOWK = PickToLightData.ScanOWK.GetScanEntry(4167252);
            //if(scanOWK != null) //Is this even possible?
            //{
            //    Populate(scanOWK);
            //}
            //Thread.Sleep(5000);
            PickToLightData.ScanOWK scanOWK = PickToLightData.ScanOWK.GetScanEntry(_startingOWKID);
            if(scanOWK != null) //Is this even possible?
            {
                string confirmationNumber = "";
                SkidBuildOrder skidBuildOrder = SkidBuildOrder.GetSkidBuildOrder(scanOWK.NAMCDestination, scanOWK.SupplierCode, scanOWK.OrderNumber, scanOWK.DockCode);
                if(skidBuildOrder != null) confirmationNumber = skidBuildOrder.ConfirmationNumber;
                Populate(scanOWK, confirmationNumber);
            }


            //TESTING!!!
            //this.UIThread(() => this.top1Label.Text = "2017101921");
            //this.UIThread(() => this.top1Label.ForeColor = Color.Black);
            //this.UIThread(() => this.top2Label.Text = "IMPE21 -  SPCL02");
            //this.UIThread(() => this.top2Label.ForeColor = Color.Black);
            //this.UIThread(() => this.bottomLabel.Text = "1400-ALFR DR LH, T2");
            //this.UIThread(() => this.bottomLabel.ForeColor = Color.Black);
            //this.UIThread(() => this.pictureBox.Image = Image.FromFile("//hq-media/Public/GT PickLine Pictures/1411-AL/1.jpg"));

            //Start with "not found" image.
            //pictureBox.Image = _notFoundImage;

            //// Create the Server Object and Start it.
            //_tcpServer = new TCPServerV2(_logger, _port); //We can pass a port here or use the Default of 11000
            //_tcpServer.CommandReceived += TCPServer_CommandReceived;
            //_tcpServer.StartServer();
        }

        public void LoadSettings()
        {
            string appSetting;

            try
            {
                //Read in the LogMode (NoLogging, FileLog, EventLog, SQLLog)
                appSetting = GetSetting("LogMode");
                //if(String.IsNullOrEmpty(appSetting))
                if (appSetting.Length == 0)
                {
                    _logger.LogMode = Logger.LogModes.NoLogging;
                    _logger.LogFolder = "LogMode=NoLogging";
                }
                else
                {
                    if (appSetting.ToUpper().Equals("NOLOGGING"))
                    {
                        _logger.LogMode = Logger.LogModes.NoLogging;
                        _logger.LogFolder = "LogMode=NoLogging";
                    }
                    else if (appSetting.ToUpper().Equals("FILELOG"))
                    {
                        _logger.LogMode = Logger.LogModes.FileLog;
                        //Read in the LogFolder (Used when LogMode = FileLog)
                        appSetting = GetSetting("LogFolder");
                        //if(String.IsNullOrEmpty(appSetting))
                        if (appSetting.Length > 0)
                        {
                            if (Directory.Exists(appSetting))
                            {
                                _logger.LogFolder = appSetting;
                            }
                            else
                            {
                                _logger.LogError("LogFolder (" + _logger.LogFolder + ") does not exist!");
                            }
                        }
                    }
                    else if (appSetting.ToUpper().Equals("EVENTLOG"))
                    {
                        _logger.LogMode = Logger.LogModes.EventLog;
                        _logger.LogFolder = "LogMode=EventLog";
                    }
                    else if (appSetting.ToUpper().Equals("SQLLOG"))
                    {
                        _logger.LogMode = Logger.LogModes.SQLLog;
                        _logger.LogFolder = "LogMode=SQLLog";
                    }
                    else // Unrecognized, so lets do EventLog!
                    {
                        _logger.LogMode = Logger.LogModes.EventLog;
                        _logger.LogFolder = "LogMode=EventLog";
                    }
                }

                //Log all the Settings loaded to the EventLog
                _logger.LogMainEvent("LogMode = (" + _logger.LogMode.ToString() + "), LogFolder = (" + _logger.LogFolder + ")");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception in LoadSettings: " + e.Message);
                throw new ConfigurationErrorsException("Exception in LoadSettings: " + e.Message);
            }
        }

        private static string GetSetting(string appSetting)
        {

            if (ConfigurationManager.AppSettings == null)
            {
                return "";
            }
            if (ConfigurationManager.AppSettings.Get(appSetting) == null)
            {
                return "";
            }

            return ConfigurationManager.AppSettings.Get(appSetting);
        }

        void TCPServer_CommandReceived(object sender, PickToLightCommandEventArgs e)
        {
            _logger.Log("PickToLightBanderDisplay CommandReceived Event, Command = (" + e.Command + ") Length = (" + e.Command.Length + ")");
            if (ProcessP2LCommand(e) == false)
            {
                string commandResponse = String.Format("Command Received and Processed: <{0}> Length: <{1}>", e.Command, e.Command.Length.ToString());
                e.SendWithProtocol(commandResponse, e.ClientSocket);
            }
            //e.ClientSocket.Send(Encoding.ASCII.GetBytes(String.Format("Command Received and Processed: <{0}> Length: <{1}>", e.Command, e.Command.Length.ToString())));
        }

        //Return false if we didn't process it and just need to send the regular reply back.
        bool ProcessP2LCommand(PickToLightCommandEventArgs e)
        {
            string[] commandFields = e.Command.Split(caGS);

            if (commandFields.Length < 1) //No GS found!
            {
                _logger.LogError("*****ProcessP2LCommand()***** ERROR: No GS in command!");
                return (false);
            }

            switch (commandFields[0])
            {
                case PickToLightCommands.P2L_COMMAND_BanderDisplay:
                    if (commandFields.Length < 2)
                    {
                        _logger.LogError("*****ProcessP2LCommand()***** ERROR: P2L_COMMAND_BanderDisplay command received, but no ScanOWK entryID!");
                        return (false);
                    }
                    int idScanOWK = -1;
                    if (int.TryParse(commandFields[1], out idScanOWK) == false)
                    {
                        _logger.LogError("*****ProcessP2LCommand()***** ERROR: P2L_COMMAND_BanderDisplay command received, but ScanOWK entryID is NOT an integer!");
                        return (false);
                    }

                    //Keep this??
                    _logger.Log("-----ProcessP2LCommand()----- P2L_COMMAND_BanderDisplay was received for ScanOWK ID [" + idScanOWK.ToString() + "]...");

                    //TODO: Create a new SQL Table to track "P2L_COMMAND" received and add a new row here??

                    //Read in the ID from the Table and decide what to do.
                    try
                    {
                        ScanOWK scanOWK = ScanOWK.GetScanEntry(idScanOWK);
                        if (scanOWK != null) //Is this even possible?
                        {
                            BanderScanned(scanOWK);

                            string commandResponse = PickToLightCommands.PopulateBanderDisplayCommandResponse(idScanOWK);
                            e.SendWithProtocol(commandResponse, e.ClientSocket);
                            return (true);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            _logger.LogError("Exception in ProcessP2LCommand(): " + ex.Message + " (Inner Exception: " + ex.InnerException + ")");
                        }
                        else
                        {
                            _logger.LogError("Exception in ProcessP2LCommand(): " + ex.Message);
                        }
                        return (false);
                        throw;
                    }

                    return (false); //Should never get here.

                    break;
                default:
                    break;
            }
            return (false);
        }

        public void BanderScanned(ScanOWK scanOWK)
        {
            if (scanOWK.Decoded)
            {
                _logger.Log("-----ProcessP2LCommand()----- Good Scan! Barcode was decoded. ScanOWK value:"
                    + " ID [" + scanOWK.ID.ToString() + "],"
                    + " Order/Manifest # [" + scanOWK.OrderNumber + "],"
                    + " Part Num & Description [" + scanOWK.SupplierInformation + "],"
                    + " SubRoute [" + scanOWK.SubRoute + "],"
                    + " MainRoute [" + scanOWK.MainRoute + "],"
                    );
            }
            else
            {
                _logger.Log("-----ProcessP2LCommand()----- Bad Scan! Barcode was NOT decoded! ScanOWK ID = [" + scanOWK.ID + "]");
            }

            string confirmationNumber = "";
            SkidBuildOrder skidBuildOrder = SkidBuildOrder.GetSkidBuildOrder(scanOWK.NAMCDestination, scanOWK.SupplierCode, scanOWK.OrderNumber, scanOWK.DockCode);
            if(skidBuildOrder != null) confirmationNumber = skidBuildOrder.ConfirmationNumber;
            Populate(scanOWK, confirmationNumber);
        }

        public void Populate(ScanOWK scanOWK, string confirmationNumber)
        {
            //show part #, order #, route, ship times, and NAMC


            //
            //ADD THIS!!! If SkidId = "???", we have a problem. We don't know which Skid the Part belongs to!
            //
            //This logic includes processing an rfID for ALL Orders.
            //For now, we also need logic that can execute for NAMC's that don't require an rfID at this time...
            //SO WHAT ARE YOU WAITING ON? ADD THAT LOGIC!!
            //
            //For this OWK(Skid), show what we think are all the Boxes for this particular Skid, in "Skid Details" just below the "Header".
            //Then, show the rest of the "Order Summary" below the "Skid Details".
            //
            //So, right here, if its the SAME OWK as last time:
            //  Just "refresh" the screen.
            //Otherwise, if its a new OWK, but for the SAME Order (NAMCDestination OR SupplierCode, OrderNumber and DockCode)...
            //  Check to see if this is a new Skid. Is there a way?? If it is a new Skid:
            //      Does this Skid have an rfID assigned? If it does NOT:
            //          Leave the screen populated as before (but refresh that OWK, just in case...).
            //          But tell the Kitter "Don't start another Skid until this Skid has an RFid assigned or has been marked with a NON CONFORMANCE sticker!!"
            //          And add a message to the bottom about the parts waiting for the next Order (NAMC, OrderNumber, DockCode)
            //      Otherwise, it DOES have an rfID assigned, so:
            //          Just "refresh" the screen.
            //  Otherwise, it is NOT a new Skid:
            //      Just "refresh" the screen.
            //Otherwise, a NEW OWK/Order is scanned:
            //  Was the Previous Order Confirmed? If it WAS:
            //      Populate the screen with the OWK as normal, but at the bottom of the screen add: NAMC, OrderNumber, DockCode and Confirmation#!!
            //  Otherwise, the previous order has NOT been confirmed!
            //      Leave the screen populated as before (but refresh that OWK, just in case...).
            //      But tell the Kitter "Don't start another Order until this Order has an RFid assigned or has been marked with a NON CONFORMANCE sticker!!"
            //      And add a message to the bottom about the parts waiting for the next Order (NAMC, OrderNumber, DockCode)
            //
            //Using this logic, after the Kitter scans the Manifest and then the rfID....
            //  PickToLightServer should send the proper Bander a "Skid_RFid_Scanned" along with the NAMC, OrderNumber, DockCode and the SkidId??

            if(scanOWK.Decoded)
            {
                string snaPartNum = "";

                //Try using the new Extension to get rid of Exceptions.
                lblPlantValue.UIThread(ctl => { ctl.Text = scanOWK.NAMCDestination; ctl.ForeColor = Color.Black; });
                lblDockCodeValue.UIThread(ctl => { ctl.Text = scanOWK.DockCode; ctl.ForeColor = Color.Black; });
                lblOrderNumberValue.UIThread(ctl => { ctl.Text = scanOWK.OrderNumber; ctl.ForeColor = Color.Black; });
                lblPalletizationCodeValue.UIThread(ctl => { ctl.Text = scanOWK.PalletizationCode; ctl.ForeColor = Color.Black; });

                lblBoxNumValue.UIThread(ctl => { ctl.Text = scanOWK.BoxNumber; ctl.ForeColor = Color.Black; });
                lblKanbanValue.UIThread(ctl => { ctl.Text = scanOWK.KanbanNumber; ctl.ForeColor = Color.Black; });
                lblMainRouteValue.UIThread(ctl => { ctl.Text = scanOWK.MainRoute; ctl.ForeColor = Color.Black; });
                lblSubRouteValue.UIThread(ctl => { ctl.Text = scanOWK.SubRoute; ctl.ForeColor = Color.Black; });

                if(confirmationNumber.Length > 0)
                {
                    lblConfirmationValue.UIThread(ctl => { ctl.Text = confirmationNumber; ctl.ForeColor = Color.Black; });
                }
                else
                {
                    lblConfirmationValue.UIThread(ctl => { ctl.Text = "<NEEDED!>"; ctl.ForeColor = Color.Red; });
                }

                lblError.UIThread(ctl => { ctl.Text = "Read " + scanOWK.ID; ctl.ForeColor = Color.Black; });

                //lblPlantValue.UIThread(ctl => { ctl.Text = scanOWK.OrderNumber; ctl.ForeColor = Color.Black; });
                //top2Label.UIThread(ctl => { ctl.Text = scanOWK.MainRoute + "  -  " + scanOWK.SubRoute; ctl.ForeColor = Color.Black; });
                //labelKanBan.UIThread(ctl => { ctl.Text = scanOWK.KanbanNumber; ctl.ForeColor = Color.Black; });
                //labelNAMC.UIThread(ctl => { ctl.Text = scanOWK.NAMCDestination; ctl.ForeColor = Color.Black; });
                //bottomLabel.UIThread(ctl => { ctl.Text = scanOWK.SupplierInformation; ctl.ForeColor = Color.Black; });

                //COMMENT OUT, CRASHING  - pictureBox.UIThread(ctl => { ctl.Image = Image.FromFile(picturePath); });
                //pictureBox.UIThread(ctl => { ctl.Image = _notFoundImage; });
            }
            else
            {
                //Try using the new Extension to get rid of Exceptions.
                lblPlantValue.UIThread(ctl => { ctl.Text = ""; ctl.ForeColor = Color.Black; });
                lblDockCodeValue.UIThread(ctl => { ctl.Text = ""; ctl.ForeColor = Color.Black; });
                lblOrderNumberValue.UIThread(ctl => { ctl.Text = ""; ctl.ForeColor = Color.Black; });
                lblPalletizationCodeValue.UIThread(ctl => { ctl.Text = ""; ctl.ForeColor = Color.Black; });

                lblBoxNumValue.UIThread(ctl => { ctl.Text = ""; ctl.ForeColor = Color.Black; });
                lblKanbanValue.UIThread(ctl => { ctl.Text = ""; ctl.ForeColor = Color.Black; });
                lblMainRouteValue.UIThread(ctl => { ctl.Text = ""; ctl.ForeColor = Color.Black; });
                lblSubRouteValue.UIThread(ctl => { ctl.Text = ""; ctl.ForeColor = Color.Black; });

                //if(confirmationNumber.Length > 0)
                //{
                //    lblConfirmationValue.UIThread(ctl => { ctl.Text = confirmationNumber; ctl.ForeColor = Color.Black; });
                //}
                //else
                //{
                //    lblConfirmationValue.UIThread(ctl => { ctl.Text = "<NEEDED!>"; ctl.ForeColor = Color.Red; });
                //}

                lblConfirmationValue.UIThread(ctl => { ctl.Text = ""; ctl.ForeColor = Color.Black; });

                lblError.UIThread(ctl => { ctl.Text = "*****NO READ*****"; ctl.ForeColor = Color.Red; });

                //top1Label.UIThread(ctl => { ctl.Text = "No Read!"; ctl.ForeColor = Color.Red; });
                //top2Label.UIThread(ctl => { ctl.Text = ""; ctl.ForeColor = Color.Red; });
                //labelKanBan.UIThread(ctl => { ctl.Text = ""; ctl.ForeColor = Color.Red; });
                //labelNAMC.UIThread(ctl => { ctl.Text = ""; ctl.ForeColor = Color.Red; });
                //bottomLabel.UIThread(ctl => { ctl.Text = "Scan Failed!"; ctl.ForeColor = Color.Red; });
                //COMMENT OUT, CRASHING  - pictureBox.UIThread(ctl => { ctl.Image = Image.FromFile("//hq-media/Public/GT PickLine Pictures/BadRead.jpg"); });
                //pictureBox.UIThread(ctl => { ctl.Image = _badReadImage; });
            }

        }

        //public void Populate(PickToLightData.ScanOWK scanOWK)
        //{
        //    //Image image = new Image();
        //    //image = Image.FromFile("//hq-media/Public/GT PickLine Pictures/010B/1411-AL/Motor 1411-AL.jpg");
        //    //pictureBox.Image = image;
        //    //pictureBox.Image = Image.FromFile("//gt-fs01/gordonsville/GT-Production Control/Shared/2017/Finished Good Rollers 2017/Conveyor/010B/1411-AL/Motor 1411-AL.jpg");

        //    if(scanOWK.Decoded)
        //    {
        //        //See if the file is present, if it is, show it, if not show "No Picture Available".
        //        //Lookup SNA part using SUpplier Information and/or Part Number
        //        string snaPartNum = "";
        //        //snaPartNum = scanOWK.SupplierInformation
        //        //snaPartNum = scanOWK.PartNumber

        //        //TODO: Check for part image and load it... (For now, don't even check it.)
        //        //
        //        //string pictureFolder = "//hq-media/Public/GT PickLine Pictures/" + snaPartNum;
        //        //string picturePath = "//hq-media/Public/GT PickLine Pictures/NotFound.jpg";
        //        ////PATH: "//hq-media/Public/GT PickLine Pictures/<SNA PART#>/1.jpg"
        //        ////EXAMPLE: "//hq-media/Public/GT PickLine Pictures/1411-AL/1.jpg" (can have more than one image to cycle through...1.jpg...2.jpg...etc)
        //        //if (Directory.Exists(pictureFolder))
        //        //{
        //        //    if (File.Exists(pictureFolder + "/1.jpg")) //There can be multiple pics to cycle through... start with "1.jpg".
        //        //    {
        //        //        picturePath = pictureFolder + "/1.jpg";
        //        //    }
        //        //}

        //        //PickToLightData.ScanOWK scan = PickToLightData.ScanOWK.GetScanEntry(idScanOWK);
        //        //top1Label.Text = scan.MainRoute + "  -  " + scan.SubRoute;
        //        //pictureBox.Image = Image.FromFile("//hq-media/Public/GT PickLine Pictures/1411-AL/Pan.jpg");

        //        //this.UIThread(() => this.top1Label.Text = scanOWK.OrderNumber);
        //        //this.UIThread(() => this.top1Label.ForeColor = Color.Black);
        //        //this.UIThread(() => this.top2Label.Text = scanOWK.MainRoute + "  -  " + scanOWK.SubRoute);
        //        //this.UIThread(() => this.top2Label.ForeColor = Color.Black);
        //        //this.UIThread(() => this.bottomLabel.Text = scanOWK.SupplierInformation);
        //        //this.UIThread(() => this.bottomLabel.ForeColor = Color.Black);
        //        //this.UIThread(() => this.pictureBox.Image = Image.FromFile("//hq-media/Public/GT PickLine Pictures/NotFound.jpg"));
        //        //this.UIThread(() => this.pictureBox.Image = Image.FromFile(picturePath));
        //        //
        //        //Try using the new Extension to get rid of Exceptions.
        //        top1Label.UIThread(ctl => { ctl.Text = scanOWK.OrderNumber; ctl.ForeColor = Color.Black; });
        //        top2Label.UIThread(ctl => { ctl.Text = scanOWK.MainRoute + "  -  " + scanOWK.SubRoute; ctl.ForeColor = Color.Black; });
        //        bottomLabel.UIThread(ctl => { ctl.Text = scanOWK.SupplierInformation; ctl.ForeColor = Color.Black; });
        //        //COMMENT OUT, CRASHING  - pictureBox.UIThread(ctl => { ctl.Image = Image.FromFile(picturePath); });
        //        pictureBox.UIThread(ctl => { ctl.Image = _notFoundImage; });
        //    }
        //    else
        //    {
        //        //this.UIThread(() => this.top1Label.Text = "No Read!");
        //        //this.UIThread(() => this.top1Label.ForeColor = Color.Red);
        //        //this.UIThread(() => this.top2Label.Text = "");
        //        //this.UIThread(() => this.top2Label.ForeColor = Color.Red);
        //        ////LOAD the "NO READ" image!
        //        //this.UIThread(() => this.pictureBox.Image = Image.FromFile("//hq-media/Public/GT PickLine Pictures/BadRead.jpg"));
        //        //this.UIThread(() => this.bottomLabel.Text = "Scan Failed!");
        //        //this.UIThread(() => this.bottomLabel.ForeColor = Color.Red);
        //        //
        //        //Try using the new Extension to get rid of Exceptions.
        //        top1Label.UIThread(ctl => { ctl.Text = "No Read!"; ctl.ForeColor = Color.Red; });
        //        top2Label.UIThread(ctl => { ctl.Text = ""; ctl.ForeColor = Color.Red; });
        //        bottomLabel.UIThread(ctl => { ctl.Text = "Scan Failed!"; ctl.ForeColor = Color.Red; });
        //        //COMMENT OUT, CRASHING  - pictureBox.UIThread(ctl => { ctl.Image = Image.FromFile("//hq-media/Public/GT PickLine Pictures/BadRead.jpg"); });
        //        pictureBox.UIThread(ctl => { ctl.Image = _badReadImage; });
        //    }

        //}

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (string.IsNullOrEmpty(Thread.CurrentThread.Name))
            {
                Thread.CurrentThread.Name = "Form Closing";
            }

            // Log a service stop message to the Application log.
            _logger.LogMainEvent("PickToLightBanderDisplay is Closing.");

            // Stop the Server. Release it.
            //_tcpServer.StopServer();
            //TODO: I don't think we should EVER set TCPServer instance to null AFTER calling StopServer(), because the TCPServer "deconstructor" handles that!
            // (It causes the Logger to throw an Exception when it tries to log to the db!)
            if (_tcpServer != null)
            {
                _tcpServer.StopServer();
                //_tcpServer = null;
            }
            //TODO: Is this necessary?? Removing it for now!
            //Thread.CurrentThread.Join(1000);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _startingOWKID++;
            ScanOWK scanOWK = ScanOWK.GetScanEntry(_startingOWKID);
            if(scanOWK != null) //Is this even possible?
            {
                string confirmationNumber = "";
                SkidBuildOrder skidBuildOrder = SkidBuildOrder.GetSkidBuildOrder(scanOWK.NAMCDestination, scanOWK.SupplierCode, scanOWK.OrderNumber, scanOWK.DockCode);
                if(skidBuildOrder != null) confirmationNumber = skidBuildOrder.ConfirmationNumber;
                Populate(scanOWK, confirmationNumber);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            _startingOWKID--;
            ScanOWK scanOWK = ScanOWK.GetScanEntry(_startingOWKID);
            if(scanOWK != null) //Is this even possible?
            {
                string confirmationNumber = "";
                SkidBuildOrder skidBuildOrder = SkidBuildOrder.GetSkidBuildOrder(scanOWK.NAMCDestination, scanOWK.SupplierCode, scanOWK.OrderNumber, scanOWK.DockCode);
                if(skidBuildOrder != null) confirmationNumber = skidBuildOrder.ConfirmationNumber;
                Populate(scanOWK, confirmationNumber);
            }
        }
    }
}
