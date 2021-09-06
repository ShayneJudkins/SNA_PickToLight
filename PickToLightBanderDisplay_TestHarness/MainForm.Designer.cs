namespace PickToLightBanderDisplay_TestHarness
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.comboBander = new System.Windows.Forms.ComboBox();
            this.comboPickStartingOWK = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBander
            // 
            this.comboBander.FormattingEnabled = true;
            this.comboBander.Items.AddRange(new object[] {
            "Bander1",
            "Bander2"});
            this.comboBander.Location = new System.Drawing.Point(35, 24);
            this.comboBander.Name = "comboBander";
            this.comboBander.Size = new System.Drawing.Size(109, 21);
            this.comboBander.TabIndex = 1;
            this.comboBander.SelectedIndexChanged += new System.EventHandler(this.comboBander_SelectedIndexChanged);
            // 
            // comboPickStartingOWK
            // 
            this.comboPickStartingOWK.FormattingEnabled = true;
            this.comboPickStartingOWK.Location = new System.Drawing.Point(214, 28);
            this.comboPickStartingOWK.Name = "comboPickStartingOWK";
            this.comboPickStartingOWK.Size = new System.Drawing.Size(120, 21);
            this.comboPickStartingOWK.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBander);
            this.Controls.Add(this.comboPickStartingOWK);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBander;
        private System.Windows.Forms.ComboBox comboPickStartingOWK;
    }
}

