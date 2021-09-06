namespace SNA.Mobile.PickToLightClient
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.lblKanbanMatch = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblToyotaOWK = new System.Windows.Forms.Label();
            this.lblInternalKanban = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.lblManifest = new System.Windows.Forms.Label();
            this.btnHoldManifest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.BackColor = System.Drawing.Color.Black;
            this.lblErrorMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblErrorMessage.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.lblErrorMessage.Location = new System.Drawing.Point(0, 380);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(800, 100);
            this.lblErrorMessage.Text = "Error Message!\r\n(Line2)";
            this.lblErrorMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblKanbanMatch
            // 
            this.lblKanbanMatch.BackColor = System.Drawing.Color.Green;
            this.lblKanbanMatch.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblKanbanMatch.ForeColor = System.Drawing.Color.Black;
            this.lblKanbanMatch.Location = new System.Drawing.Point(5, 183);
            this.lblKanbanMatch.Name = "lblKanbanMatch";
            this.lblKanbanMatch.Size = new System.Drawing.Size(440, 145);
            this.lblKanbanMatch.Text = "KANBAN\r\nMATCH";
            this.lblKanbanMatch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblKanbanMatch.Visible = false;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Red;
            this.btnReset.Enabled = false;
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(5, 331);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(107, 45);
            this.btnReset.TabIndex = 15;
            this.btnReset.TabStop = false;
            this.btnReset.Text = "RESET";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            this.btnReset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnReset_KeyDown);
            // 
            // lblToyotaOWK
            // 
            this.lblToyotaOWK.BackColor = System.Drawing.Color.Black;
            this.lblToyotaOWK.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblToyotaOWK.ForeColor = System.Drawing.Color.White;
            this.lblToyotaOWK.Location = new System.Drawing.Point(5, 130);
            this.lblToyotaOWK.Name = "lblToyotaOWK";
            this.lblToyotaOWK.Size = new System.Drawing.Size(440, 50);
            this.lblToyotaOWK.Text = "Scan Toyota Kanban";
            this.lblToyotaOWK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblInternalKanban
            // 
            this.lblInternalKanban.BackColor = System.Drawing.Color.Black;
            this.lblInternalKanban.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblInternalKanban.ForeColor = System.Drawing.Color.White;
            this.lblInternalKanban.Location = new System.Drawing.Point(5, 70);
            this.lblInternalKanban.Name = "lblInternalKanban";
            this.lblInternalKanban.Size = new System.Drawing.Size(440, 50);
            this.lblInternalKanban.Text = "Scan Internal Kanban";
            this.lblInternalKanban.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(451, 5);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(344, 372);
            this.txtOutput.TabIndex = 13;
            this.txtOutput.Text = "User: SJudkins\r\nOrder #: 12345\r\nDock: 2A\r\n--Kanbans Scanned--\r\nA151: 99\r\nD666: 11" +
                "";
            // 
            // lblManifest
            // 
            this.lblManifest.BackColor = System.Drawing.Color.Black;
            this.lblManifest.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblManifest.ForeColor = System.Drawing.Color.White;
            this.lblManifest.Location = new System.Drawing.Point(5, 10);
            this.lblManifest.Name = "lblManifest";
            this.lblManifest.Size = new System.Drawing.Size(440, 50);
            this.lblManifest.Text = "Scan Manifest";
            this.lblManifest.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnHoldManifest
            // 
            this.btnHoldManifest.BackColor = System.Drawing.Color.Red;
            this.btnHoldManifest.Enabled = false;
            this.btnHoldManifest.ForeColor = System.Drawing.Color.White;
            this.btnHoldManifest.Location = new System.Drawing.Point(170, 331);
            this.btnHoldManifest.Name = "btnHoldManifest";
            this.btnHoldManifest.Size = new System.Drawing.Size(275, 45);
            this.btnHoldManifest.TabIndex = 16;
            this.btnHoldManifest.TabStop = false;
            this.btnHoldManifest.Text = "HOLD Manifest";
            this.btnHoldManifest.Click += new System.EventHandler(this.btnHoldManifest_Click);
            this.btnHoldManifest.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnHoldManifest_KeyDown);
            // 
            // Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.ControlBox = false;
            this.Controls.Add(this.btnHoldManifest);
            this.Controls.Add(this.lblErrorMessage);
            this.Controls.Add(this.lblKanbanMatch);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblToyotaOWK);
            this.Controls.Add(this.lblInternalKanban);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.lblManifest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "Pick To Light Client";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.Closed += new System.EventHandler(this.Main_Closed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblErrorMessage;
        private System.Windows.Forms.Label lblKanbanMatch;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblToyotaOWK;
        private System.Windows.Forms.Label lblInternalKanban;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label lblManifest;
        private System.Windows.Forms.Button btnHoldManifest;
    }
}