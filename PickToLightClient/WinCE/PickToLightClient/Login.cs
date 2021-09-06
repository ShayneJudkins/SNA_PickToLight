using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SNA.Mobile.PickToLightClient
{
    public partial class Login : Form
    {
        [DllImport("coredll.dll")]
        public static extern IntPtr GetCapture();

        private enum FullScreenState : uint
        {
            SHFS_SHOWTASKBAR = 0x0001,
            SHFS_HIDETASKBAR = 0x0002,
            SHFS_SHOWSIPBUTTON = 0x0004,
            SHFS_HIDESIPBUTTON = 0x0008,
            SHFS_SHOWSTARTICON = 0x0010,
            SHFS_HIDESTARTICON = 0x0020
        }

        [DllImport("aygshell.dll", EntryPoint = "SHFullScreen", CharSet = CharSet.Auto)]
        private static extern bool SHFullScreen(UInt32 hWnd, FullScreenState dwState);

        private IntPtr _WindowHandle;

        public string UserName { get; set; }

        private static PickToLightData _pickToLightData;

        public Login(PickToLightData pickToLightData)
        {
            InitializeComponent();

            _pickToLightData = pickToLightData;

            // trick for querying the main window handle
            this.Capture = true;
            _WindowHandle = GetCapture();
            this.Capture = false;

            //SHFullScreen((uint)WindowHandle,FullScreenState.SHFS_HIDETASKBAR);
            //SHFullScreen((uint)WindowHandle,FullScreenState.SHFS_HIDESTARTICON);
            //// must make two calls to reliably hide menu bar and sip
            //this.Menu = null;
            //Hide the SIP Button!
            SHFullScreen((uint)_WindowHandle, FullScreenState.SHFS_HIDESIPBUTTON);

            lblErrorMessage.Text = "";

            //YES!???
            //this.MaximizeBox = true;
            //this.MinimizeBox = true;
            //this.WindowState = FormWindowState.Maximized;
            //this.TopMost = true;

            //IntPtr hWndTaskbar = FindWindow("HHTaskbar", InPtr.Zero);

            //SHAPI.showStart(this.Handle, false);

            //SHAPI.FullScreen(this.Handle);

            //IntPtr Hwnd = API.MyFindWindow("SipWndClass", "Input Panel");
            //if (Hwnd != IntPtr.Zero)
            //{
            //    if (Visible)
            //    {
            //        API.MyShowWindow(Hwnd, 1);
            //    }
            //}

            while (true)
            {
                if (Helpers.IsWIFIConnected())
                {
                    //string ip = "";
                    //string strHostName = "";
                    //strHostName = System.Net.Dns.GetHostName();
                    //System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
                    //System.Net.IPAddress[] addr = ipEntry.AddressList;
                    //foreach (var ipAddress in addr)
                    //{
                    //    ip += ipAddress.ToString() + " ";
                    //}
                    //MessageBox.Show("IP = " + ip);
                    break;
                }
                else if (MessageBox.Show("ERROR with Wireless Network Connection!!!\r\n\r\nSelect Retry or Cancel (to Abort).", "Wireless Error!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.UserName = "";
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.UserName = "";
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

        private void DoLogin()
        {
            lblErrorMessage.Text = "";

            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            bool isUserLoggedIn = ValidateLogin(userName, password);
            if (isUserLoggedIn)
            {
                inputPanel1.Enabled = false;
                this.DialogResult = DialogResult.OK;
                this.UserName = userName; //setting global variable (really just a class property), so it can be retrieved later (from the calling Form)
                this.Close(); //close Login form, and return to calling Form.
            }
            else
            {
                //you can show here a messageBox that userName and password do not match if you like!
                //AND the login form still stays open!!
                //MessageBox.Show("Login Failed!");
                lblErrorMessage.Text = "Login Failed!";
                if (txtPassword.Text.Length > 0)
                {
                    txtPassword.Focus();
                }
                else
                {
                    txtUserName.Focus();
                }
                inputPanel1.Enabled = true;
            }
        }

        private bool ValidateLogin(string userName, string password)
        {
            bool validLogin = false;
            //do the login checking, if username and password match, return true, else false
            if (string.IsNullOrEmpty(txtUserName.Text) == false && string.IsNullOrEmpty(txtPassword.Text) == false)
            {
                //if (txtUserName.Text.Length > 4 && txtPassword.Text.Length > 3)
                //{
                //    validLogin = true;
                //}
                //if (UserSQL.ValidateCredentials(userName, password))
                if (_pickToLightData.ValidateCredentials(userName, password))
                {
                    validLogin = true;
                }
            }
            return validLogin;
        }

        private void txtUserName_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
            txtUserName.SelectAll();
        }

        private void txtUserName_LostFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = false;
        }

        private void txtPassword_GotFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = true;
            txtPassword.SelectAll();
        }

        private void txtPassword_LostFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = false;
        }

        private void txtUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                e.Handled = true;
                //this.SelectNextControl((Control)sender, true, true, true, true);
                DoLogin();
            }
        }

    }
}