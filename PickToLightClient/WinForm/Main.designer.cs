namespace SNA.WinForm.PickToLightClient
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
            this.lblErrorMessage.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.lblErrorMessage.Location = new System.Drawing.Point(0, 568);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(1024, 200);
            this.lblErrorMessage.TabIndex = 17;
            this.lblErrorMessage.Text = "Error Message!\r\n(Line2)\r\n(Line3)\r\n\r\n(Line5)\r\n(Line6)";
            this.lblErrorMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblKanbanMatch
            // 
            this.lblKanbanMatch.BackColor = System.Drawing.Color.Green;
            this.lblKanbanMatch.Font = new System.Drawing.Font("Tahoma", 32.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKanbanMatch.ForeColor = System.Drawing.Color.Black;
            this.lblKanbanMatch.Location = new System.Drawing.Point(5, 181);
            this.lblKanbanMatch.Margin = new System.Windows.Forms.Padding(0);
            this.lblKanbanMatch.Name = "lblKanbanMatch";
            this.lblKanbanMatch.Size = new System.Drawing.Size(513, 335);
            this.lblKanbanMatch.TabIndex = 18;
            this.lblKanbanMatch.Text = "KANBAN MATCH";
            this.lblKanbanMatch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblKanbanMatch.Visible = false;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Red;
            this.btnReset.Enabled = false;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(5, 517);
            this.btnReset.Margin = new System.Windows.Forms.Padding(0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 50);
            this.btnReset.TabIndex = 15;
            this.btnReset.TabStop = false;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            this.btnReset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnReset_KeyDown);
            // 
            // lblToyotaOWK
            // 
            this.lblToyotaOWK.BackColor = System.Drawing.Color.Black;
            this.lblToyotaOWK.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToyotaOWK.ForeColor = System.Drawing.Color.White;
            this.lblToyotaOWK.Location = new System.Drawing.Point(5, 130);
            this.lblToyotaOWK.Name = "lblToyotaOWK";
            this.lblToyotaOWK.Size = new System.Drawing.Size(513, 50);
            this.lblToyotaOWK.TabIndex = 19;
            this.lblToyotaOWK.Text = "Scan Toyota Kanban";
            this.lblToyotaOWK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblInternalKanban
            // 
            this.lblInternalKanban.BackColor = System.Drawing.Color.Black;
            this.lblInternalKanban.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternalKanban.ForeColor = System.Drawing.Color.White;
            this.lblInternalKanban.Location = new System.Drawing.Point(5, 70);
            this.lblInternalKanban.Name = "lblInternalKanban";
            this.lblInternalKanban.Size = new System.Drawing.Size(513, 50);
            this.lblInternalKanban.TabIndex = 20;
            this.lblInternalKanban.Text = "Scan Internal KanBan";
            this.lblInternalKanban.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(524, 0);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(500, 567);
            this.txtOutput.TabIndex = 13;
            this.txtOutput.Text = "SJudkins, MATCHES: 999\r\n======================\r\nOrder #: 12345\r\nDock: 2A\r\n--Kanba" +
                "ns Scanned--\r\nA151: 99\r\nD666: 11";
            // 
            // lblManifest
            // 
            this.lblManifest.BackColor = System.Drawing.Color.Black;
            this.lblManifest.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManifest.ForeColor = System.Drawing.Color.White;
            this.lblManifest.Location = new System.Drawing.Point(5, 10);
            this.lblManifest.Name = "lblManifest";
            this.lblManifest.Size = new System.Drawing.Size(513, 50);
            this.lblManifest.TabIndex = 21;
            this.lblManifest.Text = "Scan Manifest";
            this.lblManifest.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnHoldManifest
            // 
            this.btnHoldManifest.BackColor = System.Drawing.Color.Red;
            this.btnHoldManifest.Enabled = false;
            this.btnHoldManifest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHoldManifest.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoldManifest.ForeColor = System.Drawing.Color.White;
            this.btnHoldManifest.Location = new System.Drawing.Point(243, 517);
            this.btnHoldManifest.Margin = new System.Windows.Forms.Padding(0);
            this.btnHoldManifest.Name = "btnHoldManifest";
            this.btnHoldManifest.Size = new System.Drawing.Size(275, 50);
            this.btnHoldManifest.TabIndex = 16;
            this.btnHoldManifest.TabStop = false;
            this.btnHoldManifest.Text = "HOLD Manifest";
            this.btnHoldManifest.UseVisualStyleBackColor = false;
            this.btnHoldManifest.Click += new System.EventHandler(this.btnHoldManifest_Click);
            this.btnHoldManifest.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnHoldManifest_KeyDown);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1024, 768);
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pick To Light Client";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.Closed += new System.EventHandler(this.Main_Closed);
            this.ResumeLayout(false);
            this.PerformLayout();

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