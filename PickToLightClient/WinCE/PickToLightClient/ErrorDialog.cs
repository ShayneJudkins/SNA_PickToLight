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
    public partial class ErrorDialog : Form
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

        public ErrorDialog(string title, string errorMessage, bool showCancel)
        {
            InitializeComponent();

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
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Main_KeyDown);
            this.KeyPress += new KeyPressEventHandler(Main_KeyPress);
            this.KeyUp += new KeyEventHandler(Main_KeyUp);

            this.Text = title;
            this.lblErrorMessage.Text = errorMessage;
            if (showCancel)
            {
                btnCancel.Visible = true;
            }
            else
            {
                btnCancel.Visible = false;
            }
        }

        void Main_KeyDown(object sender, KeyEventArgs e)
        {
            //Swallow ALL Keys! We don't want the Enter key to "Click" one of the buttons, because the barcode scanner sends an Enter key at the end of the scanned text.
            e.Handled = true;
        }

        void Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Swallow ALL Keypresses! We don't want the Enter key to "Click" one of the buttons, because the barcode scanner sends an Enter key at the end of the scanned text.
            e.Handled = true;
        }

        void Main_KeyUp(object sender, KeyEventArgs e)
        {
            //Swallow ALL Keys! We don't want the Enter key to "Click" one of the buttons, because the barcode scanner sends an Enter key at the end of the scanned text.
            e.Handled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close(); //close Login form, and return to calling Form.
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}