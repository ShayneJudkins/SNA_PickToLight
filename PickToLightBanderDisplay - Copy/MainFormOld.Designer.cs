namespace PickToLightBanderDisplay
{
    partial class MainFormOld
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SkidId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SNAPartNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KanbanNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustPartNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoxNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kitter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Transfer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Picker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPrev = new System.Windows.Forms.Button();
            this.pnlBoxTable = new System.Windows.Forms.TableLayoutPanel();
            this.lblKanbanValue = new System.Windows.Forms.Label();
            this.lblKanban = new System.Windows.Forms.Label();
            this.lblBoxNum = new System.Windows.Forms.Label();
            this.lblBoxNumValue = new System.Windows.Forms.Label();
            this.lblSubRoute = new System.Windows.Forms.Label();
            this.lblMainRoute = new System.Windows.Forms.Label();
            this.lblMainRouteValue = new System.Windows.Forms.Label();
            this.lblSubRouteValue = new System.Windows.Forms.Label();
            this.pnlConfirmationTable = new System.Windows.Forms.TableLayoutPanel();
            this.lblConfirmation = new System.Windows.Forms.Label();
            this.lblConfirmationValue = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.pnlPlantTable = new System.Windows.Forms.TableLayoutPanel();
            this.lblPalletizationCode = new System.Windows.Forms.Label();
            this.lblPalletizationCodeValue = new System.Windows.Forms.Label();
            this.lblOrderNumberValue = new System.Windows.Forms.Label();
            this.lblOrderNumber = new System.Windows.Forms.Label();
            this.lblDockCodeValue = new System.Windows.Forms.Label();
            this.lblDock = new System.Windows.Forms.Label();
            this.lblPlantValue = new System.Windows.Forms.Label();
            this.lblPlant = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnlBoxTable.SuspendLayout();
            this.pnlConfirmationTable.SuspendLayout();
            this.pnlPlantTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.AutoSize = true;
            this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.dataGridView1);
            this.mainPanel.Controls.Add(this.btnPrev);
            this.mainPanel.Controls.Add(this.pnlBoxTable);
            this.mainPanel.Controls.Add(this.pnlConfirmationTable);
            this.mainPanel.Controls.Add(this.btnExit);
            this.mainPanel.Controls.Add(this.btnNext);
            this.mainPanel.Controls.Add(this.lblError);
            this.mainPanel.Controls.Add(this.pnlPlantTable);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1284, 711);
            this.mainPanel.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 32;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SkidId,
            this.SNAPartNum,
            this.KanbanNum,
            this.CustPartNum,
            this.BoxNum,
            this.Kitter,
            this.Transfer,
            this.Picker});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Location = new System.Drawing.Point(0, 174);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 32;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(1280, 459);
            this.dataGridView1.TabIndex = 9;
            // 
            // SkidId
            // 
            this.SkidId.HeaderText = "Skid";
            this.SkidId.MinimumWidth = 70;
            this.SkidId.Name = "SkidId";
            this.SkidId.ReadOnly = true;
            this.SkidId.Width = 70;
            // 
            // SNAPartNum
            // 
            this.SNAPartNum.HeaderText = "SNA#";
            this.SNAPartNum.MinimumWidth = 100;
            this.SNAPartNum.Name = "SNAPartNum";
            this.SNAPartNum.ReadOnly = true;
            // 
            // KanbanNum
            // 
            this.KanbanNum.HeaderText = "Kanban#";
            this.KanbanNum.MinimumWidth = 120;
            this.KanbanNum.Name = "KanbanNum";
            this.KanbanNum.ReadOnly = true;
            this.KanbanNum.Width = 120;
            // 
            // CustPartNum
            // 
            this.CustPartNum.HeaderText = "Cust Part#";
            this.CustPartNum.MinimumWidth = 200;
            this.CustPartNum.Name = "CustPartNum";
            this.CustPartNum.ReadOnly = true;
            this.CustPartNum.Width = 200;
            // 
            // BoxNum
            // 
            this.BoxNum.HeaderText = "Box#";
            this.BoxNum.MinimumWidth = 80;
            this.BoxNum.Name = "BoxNum";
            this.BoxNum.ReadOnly = true;
            this.BoxNum.Width = 80;
            // 
            // Kitter
            // 
            this.Kitter.HeaderText = "Kitter";
            this.Kitter.MinimumWidth = 250;
            this.Kitter.Name = "Kitter";
            this.Kitter.ReadOnly = true;
            this.Kitter.Width = 250;
            // 
            // Transfer
            // 
            this.Transfer.HeaderText = "Transfer";
            this.Transfer.MinimumWidth = 200;
            this.Transfer.Name = "Transfer";
            this.Transfer.ReadOnly = true;
            this.Transfer.Width = 200;
            // 
            // Picker
            // 
            this.Picker.HeaderText = "Picker";
            this.Picker.MinimumWidth = 250;
            this.Picker.Name = "Picker";
            this.Picker.ReadOnly = true;
            this.Picker.Width = 250;
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(3, 660);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(81, 41);
            this.btnPrev.TabIndex = 8;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // pnlBoxTable
            // 
            this.pnlBoxTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBoxTable.ColumnCount = 2;
            this.pnlBoxTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlBoxTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlBoxTable.Controls.Add(this.lblKanbanValue, 1, 1);
            this.pnlBoxTable.Controls.Add(this.lblKanban, 0, 1);
            this.pnlBoxTable.Controls.Add(this.lblBoxNum, 0, 0);
            this.pnlBoxTable.Controls.Add(this.lblBoxNumValue, 1, 0);
            this.pnlBoxTable.Controls.Add(this.lblSubRoute, 0, 2);
            this.pnlBoxTable.Controls.Add(this.lblMainRoute, 0, 3);
            this.pnlBoxTable.Controls.Add(this.lblMainRouteValue, 1, 3);
            this.pnlBoxTable.Controls.Add(this.lblSubRouteValue, 1, 2);
            this.pnlBoxTable.Location = new System.Drawing.Point(870, 10);
            this.pnlBoxTable.Name = "pnlBoxTable";
            this.pnlBoxTable.RowCount = 4;
            this.pnlBoxTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlBoxTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlBoxTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlBoxTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlBoxTable.Size = new System.Drawing.Size(400, 150);
            this.pnlBoxTable.TabIndex = 7;
            // 
            // lblKanbanValue
            // 
            this.lblKanbanValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKanbanValue.AutoSize = true;
            this.lblKanbanValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblKanbanValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKanbanValue.Location = new System.Drawing.Point(197, 37);
            this.lblKanbanValue.Name = "lblKanbanValue";
            this.lblKanbanValue.Size = new System.Drawing.Size(200, 37);
            this.lblKanbanValue.TabIndex = 9;
            this.lblKanbanValue.Text = "N4D9";
            this.lblKanbanValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKanban
            // 
            this.lblKanban.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKanban.AutoSize = true;
            this.lblKanban.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblKanban.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKanban.Location = new System.Drawing.Point(3, 37);
            this.lblKanban.Name = "lblKanban";
            this.lblKanban.Size = new System.Drawing.Size(188, 37);
            this.lblKanban.TabIndex = 3;
            this.lblKanban.Text = "Kanban#:";
            this.lblKanban.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBoxNum
            // 
            this.lblBoxNum.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBoxNum.AutoSize = true;
            this.lblBoxNum.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblBoxNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoxNum.Location = new System.Drawing.Point(3, 0);
            this.lblBoxNum.Name = "lblBoxNum";
            this.lblBoxNum.Size = new System.Drawing.Size(188, 37);
            this.lblBoxNum.TabIndex = 1;
            this.lblBoxNum.Text = "Box#:";
            this.lblBoxNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBoxNumValue
            // 
            this.lblBoxNumValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBoxNumValue.AutoSize = true;
            this.lblBoxNumValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBoxNumValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoxNumValue.Location = new System.Drawing.Point(197, 0);
            this.lblBoxNumValue.Name = "lblBoxNumValue";
            this.lblBoxNumValue.Size = new System.Drawing.Size(200, 37);
            this.lblBoxNumValue.TabIndex = 4;
            this.lblBoxNumValue.Text = "0001";
            this.lblBoxNumValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubRoute
            // 
            this.lblSubRoute.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubRoute.AutoSize = true;
            this.lblSubRoute.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblSubRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubRoute.Location = new System.Drawing.Point(3, 74);
            this.lblSubRoute.Name = "lblSubRoute";
            this.lblSubRoute.Size = new System.Drawing.Size(188, 37);
            this.lblSubRoute.TabIndex = 11;
            this.lblSubRoute.Text = "Sub Route:";
            this.lblSubRoute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMainRoute
            // 
            this.lblMainRoute.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMainRoute.AutoSize = true;
            this.lblMainRoute.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblMainRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainRoute.Location = new System.Drawing.Point(3, 111);
            this.lblMainRoute.Name = "lblMainRoute";
            this.lblMainRoute.Size = new System.Drawing.Size(188, 39);
            this.lblMainRoute.TabIndex = 7;
            this.lblMainRoute.Text = "Main Route:";
            this.lblMainRoute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMainRouteValue
            // 
            this.lblMainRouteValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMainRouteValue.AutoSize = true;
            this.lblMainRouteValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblMainRouteValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainRouteValue.Location = new System.Drawing.Point(197, 111);
            this.lblMainRouteValue.Name = "lblMainRouteValue";
            this.lblMainRouteValue.Size = new System.Drawing.Size(200, 39);
            this.lblMainRouteValue.TabIndex = 8;
            this.lblMainRouteValue.Text = "KYN223";
            this.lblMainRouteValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubRouteValue
            // 
            this.lblSubRouteValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubRouteValue.AutoSize = true;
            this.lblSubRouteValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSubRouteValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubRouteValue.Location = new System.Drawing.Point(197, 74);
            this.lblSubRouteValue.Name = "lblSubRouteValue";
            this.lblSubRouteValue.Size = new System.Drawing.Size(200, 37);
            this.lblSubRouteValue.TabIndex = 10;
            this.lblSubRouteValue.Text = "SYTL03";
            this.lblSubRouteValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlConfirmationTable
            // 
            this.pnlConfirmationTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlConfirmationTable.ColumnCount = 1;
            this.pnlConfirmationTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlConfirmationTable.Controls.Add(this.lblConfirmation, 0, 0);
            this.pnlConfirmationTable.Controls.Add(this.lblConfirmationValue, 0, 1);
            this.pnlConfirmationTable.Location = new System.Drawing.Point(420, 10);
            this.pnlConfirmationTable.Name = "pnlConfirmationTable";
            this.pnlConfirmationTable.RowCount = 2;
            this.pnlConfirmationTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlConfirmationTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlConfirmationTable.Size = new System.Drawing.Size(440, 150);
            this.pnlConfirmationTable.TabIndex = 6;
            // 
            // lblConfirmation
            // 
            this.lblConfirmation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConfirmation.AutoSize = true;
            this.lblConfirmation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblConfirmation.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmation.Location = new System.Drawing.Point(3, 0);
            this.lblConfirmation.Name = "lblConfirmation";
            this.lblConfirmation.Size = new System.Drawing.Size(434, 42);
            this.lblConfirmation.TabIndex = 4;
            this.lblConfirmation.Text = "Confirmation#";
            this.lblConfirmation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConfirmationValue
            // 
            this.lblConfirmationValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConfirmationValue.AutoSize = true;
            this.lblConfirmationValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblConfirmationValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmationValue.Location = new System.Drawing.Point(3, 42);
            this.lblConfirmationValue.Name = "lblConfirmationValue";
            this.lblConfirmationValue.Size = new System.Drawing.Size(434, 108);
            this.lblConfirmationValue.TabIndex = 5;
            this.lblConfirmationValue.Text = "2260218110717550007P";
            this.lblConfirmationValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(231, 660);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(73, 41);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(104, 660);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(81, 41);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(0, 636);
            this.lblError.MinimumSize = new System.Drawing.Size(1284, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(1284, 73);
            this.lblError.TabIndex = 1;
            this.lblError.Text = "*****NO READ*****";
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblError.UseMnemonic = false;
            // 
            // pnlPlantTable
            // 
            this.pnlPlantTable.ColumnCount = 2;
            this.pnlPlantTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlPlantTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlPlantTable.Controls.Add(this.lblPalletizationCode, 0, 3);
            this.pnlPlantTable.Controls.Add(this.lblPalletizationCodeValue, 0, 3);
            this.pnlPlantTable.Controls.Add(this.lblOrderNumberValue, 1, 2);
            this.pnlPlantTable.Controls.Add(this.lblOrderNumber, 0, 2);
            this.pnlPlantTable.Controls.Add(this.lblDockCodeValue, 1, 1);
            this.pnlPlantTable.Controls.Add(this.lblDock, 0, 1);
            this.pnlPlantTable.Controls.Add(this.lblPlantValue, 1, 0);
            this.pnlPlantTable.Controls.Add(this.lblPlant, 0, 0);
            this.pnlPlantTable.Location = new System.Drawing.Point(10, 10);
            this.pnlPlantTable.Name = "pnlPlantTable";
            this.pnlPlantTable.RowCount = 4;
            this.pnlPlantTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlPlantTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlPlantTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlPlantTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlPlantTable.Size = new System.Drawing.Size(400, 150);
            this.pnlPlantTable.TabIndex = 0;
            // 
            // lblPalletizationCode
            // 
            this.lblPalletizationCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPalletizationCode.AutoSize = true;
            this.lblPalletizationCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblPalletizationCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPalletizationCode.Location = new System.Drawing.Point(3, 111);
            this.lblPalletizationCode.Name = "lblPalletizationCode";
            this.lblPalletizationCode.Size = new System.Drawing.Size(190, 39);
            this.lblPalletizationCode.TabIndex = 7;
            this.lblPalletizationCode.Text = "Pallet Code:";
            this.lblPalletizationCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPalletizationCode.UseMnemonic = false;
            // 
            // lblPalletizationCodeValue
            // 
            this.lblPalletizationCodeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPalletizationCodeValue.AutoSize = true;
            this.lblPalletizationCodeValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPalletizationCodeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPalletizationCodeValue.Location = new System.Drawing.Point(199, 111);
            this.lblPalletizationCodeValue.Name = "lblPalletizationCodeValue";
            this.lblPalletizationCodeValue.Size = new System.Drawing.Size(198, 39);
            this.lblPalletizationCodeValue.TabIndex = 6;
            this.lblPalletizationCodeValue.Text = "B";
            this.lblPalletizationCodeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrderNumberValue
            // 
            this.lblOrderNumberValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrderNumberValue.AutoSize = true;
            this.lblOrderNumberValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOrderNumberValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNumberValue.Location = new System.Drawing.Point(199, 74);
            this.lblOrderNumberValue.Name = "lblOrderNumberValue";
            this.lblOrderNumberValue.Size = new System.Drawing.Size(198, 37);
            this.lblOrderNumberValue.TabIndex = 5;
            this.lblOrderNumberValue.Text = "2018011899";
            this.lblOrderNumberValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrderNumber.AutoSize = true;
            this.lblOrderNumber.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblOrderNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNumber.Location = new System.Drawing.Point(3, 74);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new System.Drawing.Size(190, 37);
            this.lblOrderNumber.TabIndex = 4;
            this.lblOrderNumber.Text = "Order#:";
            this.lblOrderNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblOrderNumber.UseMnemonic = false;
            // 
            // lblDockCodeValue
            // 
            this.lblDockCodeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDockCodeValue.AutoSize = true;
            this.lblDockCodeValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDockCodeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDockCodeValue.Location = new System.Drawing.Point(199, 37);
            this.lblDockCodeValue.Name = "lblDockCodeValue";
            this.lblDockCodeValue.Size = new System.Drawing.Size(198, 37);
            this.lblDockCodeValue.TabIndex = 3;
            this.lblDockCodeValue.Text = "H8";
            this.lblDockCodeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDock
            // 
            this.lblDock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDock.AutoSize = true;
            this.lblDock.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblDock.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDock.Location = new System.Drawing.Point(3, 37);
            this.lblDock.Name = "lblDock";
            this.lblDock.Size = new System.Drawing.Size(190, 37);
            this.lblDock.TabIndex = 2;
            this.lblDock.Text = "Dock:";
            this.lblDock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPlantValue
            // 
            this.lblPlantValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlantValue.AutoSize = true;
            this.lblPlantValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPlantValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlantValue.Location = new System.Drawing.Point(199, 0);
            this.lblPlantValue.Name = "lblPlantValue";
            this.lblPlantValue.Size = new System.Drawing.Size(198, 37);
            this.lblPlantValue.TabIndex = 1;
            this.lblPlantValue.Text = "02TMI";
            this.lblPlantValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPlant
            // 
            this.lblPlant.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlant.AutoSize = true;
            this.lblPlant.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblPlant.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlant.Location = new System.Drawing.Point(3, 0);
            this.lblPlant.Name = "lblPlant";
            this.lblPlant.Size = new System.Drawing.Size(190, 37);
            this.lblPlant.TabIndex = 0;
            this.lblPlant.Text = "Plant:";
            this.lblPlant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainFormOld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 711);
            this.Controls.Add(this.mainPanel);
            this.Name = "MainFormOld";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnlBoxTable.ResumeLayout(false);
            this.pnlBoxTable.PerformLayout();
            this.pnlConfirmationTable.ResumeLayout(false);
            this.pnlConfirmationTable.PerformLayout();
            this.pnlPlantTable.ResumeLayout(false);
            this.pnlPlantTable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TableLayoutPanel pnlPlantTable;
        private System.Windows.Forms.Label lblPlant;
        private System.Windows.Forms.Label lblOrderNumberValue;
        private System.Windows.Forms.Label lblOrderNumber;
        private System.Windows.Forms.Label lblDockCodeValue;
        private System.Windows.Forms.Label lblDock;
        private System.Windows.Forms.Label lblPlantValue;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TableLayoutPanel pnlBoxTable;
        private System.Windows.Forms.Label lblKanban;
        private System.Windows.Forms.Label lblBoxNum;
        private System.Windows.Forms.TableLayoutPanel pnlConfirmationTable;
        private System.Windows.Forms.Label lblConfirmation;
        private System.Windows.Forms.Label lblConfirmationValue;
        private System.Windows.Forms.Label lblBoxNumValue;
        private System.Windows.Forms.Label lblMainRoute;
        private System.Windows.Forms.Label lblKanbanValue;
        private System.Windows.Forms.Label lblMainRouteValue;
        private System.Windows.Forms.Label lblSubRoute;
        private System.Windows.Forms.Label lblSubRouteValue;
        private System.Windows.Forms.Label lblPalletizationCode;
        private System.Windows.Forms.Label lblPalletizationCodeValue;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SkidId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNAPartNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn KanbanNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustPartNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoxNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kitter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Transfer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Picker;
    }
}

