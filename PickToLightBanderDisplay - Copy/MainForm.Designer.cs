namespace PickToLightBanderDisplay
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.mainTLPanel = new System.Windows.Forms.TableLayoutPanel();
            this.manifestTLPanel = new System.Windows.Forms.TableLayoutPanel();
            this.supplierTLPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblSupplierValue = new System.Windows.Forms.Label();
            this.dockTLPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblDock = new System.Windows.Forms.Label();
            this.lblPlantValue = new System.Windows.Forms.Label();
            this.lblDockValue = new System.Windows.Forms.Label();
            this.subRouteTLPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblSubRoute = new System.Windows.Forms.Label();
            this.lblShipDateTime = new System.Windows.Forms.Label();
            this.lblSubRouteValue = new System.Windows.Forms.Label();
            this.lblSkidNumOfTotal = new System.Windows.Forms.Label();
            this.orderNumTLPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblOrderNumber = new System.Windows.Forms.Label();
            this.lblOrderNumberValue = new System.Windows.Forms.Label();
            this.rfidTLPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblRFid = new System.Windows.Forms.Label();
            this.lblRFidValue = new System.Windows.Forms.Label();
            this.palletCodeTLPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblPalletizationCode = new System.Windows.Forms.Label();
            this.lblPalletizationCodeValue = new System.Windows.Forms.Label();
            this.skidIDTLPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblSkidID = new System.Windows.Forms.Label();
            this.lblSkidIDValue = new System.Windows.Forms.Label();
            this.confirmationNumberTLPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblConfirmationNumber = new System.Windows.Forms.Label();
            this.lblConfirmationNumberValue = new System.Windows.Forms.Label();
            this.messageFLPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.skidsFLPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lvKanbans = new System.Windows.Forms.ListView();
            this.statusBarFLPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.dgvCurrentSkid = new System.Windows.Forms.DataGridView();
            this.dgvCurrentSkidKanban = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCurrentSkidBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCurrentSkidPicker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCurrentSkidTransfer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCurrentSkidKitter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSkidMinus1 = new System.Windows.Forms.DataGridView();
            this.dgvSkidMinus1Kanban = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSkidMinus1Box = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSkidMinus1Picker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSkidMinus1Transfer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSkidMinus1Kitter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.savedComponentsPanel = new System.Windows.Forms.Panel();
            this.mainPanel.SuspendLayout();
            this.mainTLPanel.SuspendLayout();
            this.manifestTLPanel.SuspendLayout();
            this.supplierTLPanel.SuspendLayout();
            this.dockTLPanel.SuspendLayout();
            this.subRouteTLPanel.SuspendLayout();
            this.orderNumTLPanel.SuspendLayout();
            this.rfidTLPanel.SuspendLayout();
            this.palletCodeTLPanel.SuspendLayout();
            this.skidIDTLPanel.SuspendLayout();
            this.confirmationNumberTLPanel.SuspendLayout();
            this.messageFLPanel.SuspendLayout();
            this.skidsFLPanel.SuspendLayout();
            this.statusBarFLPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentSkid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkidMinus1)).BeginInit();
            this.savedComponentsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.AutoSize = true;
            this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainPanel.Controls.Add(this.mainTLPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1284, 711);
            this.mainPanel.TabIndex = 0;
            // 
            // mainTLPanel
            // 
            this.mainTLPanel.AutoSize = true;
            this.mainTLPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainTLPanel.ColumnCount = 2;
            this.mainTLPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTLPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTLPanel.Controls.Add(this.manifestTLPanel, 0, 0);
            this.mainTLPanel.Controls.Add(this.messageFLPanel, 1, 0);
            this.mainTLPanel.Controls.Add(this.skidsFLPanel, 0, 1);
            this.mainTLPanel.Controls.Add(this.statusBarFLPanel, 0, 2);
            this.mainTLPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTLPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTLPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainTLPanel.Name = "mainTLPanel";
            this.mainTLPanel.RowCount = 3;
            this.mainTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.mainTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.mainTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.mainTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTLPanel.Size = new System.Drawing.Size(1284, 711);
            this.mainTLPanel.TabIndex = 10;
            // 
            // manifestTLPanel
            // 
            this.manifestTLPanel.ColumnCount = 3;
            this.manifestTLPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.manifestTLPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.manifestTLPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 342F));
            this.manifestTLPanel.Controls.Add(this.supplierTLPanel, 0, 0);
            this.manifestTLPanel.Controls.Add(this.dockTLPanel, 1, 0);
            this.manifestTLPanel.Controls.Add(this.subRouteTLPanel, 2, 0);
            this.manifestTLPanel.Controls.Add(this.lblSkidNumOfTotal, 0, 1);
            this.manifestTLPanel.Controls.Add(this.orderNumTLPanel, 0, 2);
            this.manifestTLPanel.Controls.Add(this.rfidTLPanel, 2, 2);
            this.manifestTLPanel.Controls.Add(this.palletCodeTLPanel, 0, 3);
            this.manifestTLPanel.Controls.Add(this.skidIDTLPanel, 1, 3);
            this.manifestTLPanel.Controls.Add(this.confirmationNumberTLPanel, 2, 3);
            this.manifestTLPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manifestTLPanel.Location = new System.Drawing.Point(0, 0);
            this.manifestTLPanel.Margin = new System.Windows.Forms.Padding(0);
            this.manifestTLPanel.Name = "manifestTLPanel";
            this.manifestTLPanel.RowCount = 4;
            this.manifestTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.manifestTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.manifestTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.manifestTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.manifestTLPanel.Size = new System.Drawing.Size(642, 230);
            this.manifestTLPanel.TabIndex = 0;
            // 
            // supplierTLPanel
            // 
            this.supplierTLPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.supplierTLPanel.ColumnCount = 1;
            this.supplierTLPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.supplierTLPanel.Controls.Add(this.lblSupplier, 0, 0);
            this.supplierTLPanel.Controls.Add(this.lblSupplierValue, 0, 1);
            this.supplierTLPanel.Location = new System.Drawing.Point(0, 0);
            this.supplierTLPanel.Margin = new System.Windows.Forms.Padding(0);
            this.supplierTLPanel.Name = "supplierTLPanel";
            this.supplierTLPanel.RowCount = 2;
            this.supplierTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.supplierTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.supplierTLPanel.Size = new System.Drawing.Size(150, 50);
            this.supplierTLPanel.TabIndex = 8;
            // 
            // lblSupplier
            // 
            this.lblSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplier.Location = new System.Drawing.Point(0, 0);
            this.lblSupplier.Margin = new System.Windows.Forms.Padding(0);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(150, 20);
            this.lblSupplier.TabIndex = 0;
            this.lblSupplier.Text = "Supplier";
            this.lblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSupplierValue
            // 
            this.lblSupplierValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSupplierValue.AutoSize = true;
            this.lblSupplierValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSupplierValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierValue.Location = new System.Drawing.Point(0, 20);
            this.lblSupplierValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblSupplierValue.Name = "lblSupplierValue";
            this.lblSupplierValue.Size = new System.Drawing.Size(150, 30);
            this.lblSupplierValue.TabIndex = 1;
            this.lblSupplierValue.Text = "22602";
            this.lblSupplierValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dockTLPanel
            // 
            this.dockTLPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dockTLPanel.ColumnCount = 1;
            this.dockTLPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dockTLPanel.Controls.Add(this.lblDock, 0, 0);
            this.dockTLPanel.Controls.Add(this.lblPlantValue, 0, 1);
            this.dockTLPanel.Controls.Add(this.lblDockValue, 0, 2);
            this.dockTLPanel.Location = new System.Drawing.Point(150, 0);
            this.dockTLPanel.Margin = new System.Windows.Forms.Padding(0);
            this.dockTLPanel.Name = "dockTLPanel";
            this.dockTLPanel.RowCount = 5;
            this.manifestTLPanel.SetRowSpan(this.dockTLPanel, 2);
            this.dockTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.dockTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.dockTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.dockTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.dockTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.dockTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.dockTLPanel.Size = new System.Drawing.Size(150, 100);
            this.dockTLPanel.TabIndex = 9;
            // 
            // lblDock
            // 
            this.lblDock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDock.AutoSize = true;
            this.lblDock.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblDock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDock.Location = new System.Drawing.Point(0, 0);
            this.lblDock.Margin = new System.Windows.Forms.Padding(0);
            this.lblDock.Name = "lblDock";
            this.lblDock.Size = new System.Drawing.Size(150, 20);
            this.lblDock.TabIndex = 1;
            this.lblDock.Text = "Dock";
            this.lblDock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPlantValue
            // 
            this.lblPlantValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlantValue.AutoSize = true;
            this.lblPlantValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPlantValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlantValue.Location = new System.Drawing.Point(0, 20);
            this.lblPlantValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblPlantValue.Name = "lblPlantValue";
            this.lblPlantValue.Size = new System.Drawing.Size(150, 30);
            this.lblPlantValue.TabIndex = 1;
            this.lblPlantValue.Text = "TMMTX";
            this.lblPlantValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDockValue
            // 
            this.lblDockValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDockValue.AutoSize = true;
            this.lblDockValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDockValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDockValue.Location = new System.Drawing.Point(0, 50);
            this.lblDockValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblDockValue.Name = "lblDockValue";
            this.lblDockValue.Size = new System.Drawing.Size(150, 50);
            this.lblDockValue.TabIndex = 2;
            this.lblDockValue.Text = "G1";
            this.lblDockValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subRouteTLPanel
            // 
            this.subRouteTLPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subRouteTLPanel.ColumnCount = 1;
            this.subRouteTLPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.subRouteTLPanel.Controls.Add(this.lblSubRoute, 0, 0);
            this.subRouteTLPanel.Controls.Add(this.lblShipDateTime, 0, 1);
            this.subRouteTLPanel.Controls.Add(this.lblSubRouteValue, 0, 2);
            this.subRouteTLPanel.Location = new System.Drawing.Point(300, 0);
            this.subRouteTLPanel.Margin = new System.Windows.Forms.Padding(0);
            this.subRouteTLPanel.Name = "subRouteTLPanel";
            this.subRouteTLPanel.RowCount = 3;
            this.manifestTLPanel.SetRowSpan(this.subRouteTLPanel, 2);
            this.subRouteTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.subRouteTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.subRouteTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.subRouteTLPanel.Size = new System.Drawing.Size(342, 100);
            this.subRouteTLPanel.TabIndex = 10;
            // 
            // lblSubRoute
            // 
            this.lblSubRoute.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubRoute.AutoSize = true;
            this.lblSubRoute.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSubRoute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSubRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubRoute.Location = new System.Drawing.Point(0, 0);
            this.lblSubRoute.Margin = new System.Windows.Forms.Padding(0);
            this.lblSubRoute.Name = "lblSubRoute";
            this.lblSubRoute.Size = new System.Drawing.Size(342, 20);
            this.lblSubRoute.TabIndex = 1;
            this.lblSubRoute.Text = "Sub-Route";
            this.lblSubRoute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShipDateTime
            // 
            this.lblShipDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShipDateTime.AutoSize = true;
            this.lblShipDateTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblShipDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShipDateTime.Location = new System.Drawing.Point(0, 20);
            this.lblShipDateTime.Margin = new System.Windows.Forms.Padding(0);
            this.lblShipDateTime.Name = "lblShipDateTime";
            this.lblShipDateTime.Size = new System.Drawing.Size(342, 30);
            this.lblShipDateTime.TabIndex = 1;
            this.lblShipDateTime.Text = "Load: 08/08/2019 07:02";
            this.lblShipDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubRouteValue
            // 
            this.lblSubRouteValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubRouteValue.AutoSize = true;
            this.lblSubRouteValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSubRouteValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubRouteValue.Location = new System.Drawing.Point(0, 50);
            this.lblSubRouteValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblSubRouteValue.Name = "lblSubRouteValue";
            this.lblSubRouteValue.Size = new System.Drawing.Size(342, 50);
            this.lblSubRouteValue.TabIndex = 2;
            this.lblSubRouteValue.Text = "XDSK01";
            this.lblSubRouteValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSkidNumOfTotal
            // 
            this.lblSkidNumOfTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSkidNumOfTotal.AutoSize = true;
            this.lblSkidNumOfTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSkidNumOfTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkidNumOfTotal.Location = new System.Drawing.Point(0, 50);
            this.lblSkidNumOfTotal.Margin = new System.Windows.Forms.Padding(0);
            this.lblSkidNumOfTotal.Name = "lblSkidNumOfTotal";
            this.lblSkidNumOfTotal.Size = new System.Drawing.Size(150, 50);
            this.lblSkidNumOfTotal.TabIndex = 12;
            this.lblSkidNumOfTotal.Text = "Skid X of X";
            this.lblSkidNumOfTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // orderNumTLPanel
            // 
            this.orderNumTLPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.orderNumTLPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.orderNumTLPanel.ColumnCount = 1;
            this.manifestTLPanel.SetColumnSpan(this.orderNumTLPanel, 2);
            this.orderNumTLPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.orderNumTLPanel.Controls.Add(this.lblOrderNumber, 0, 0);
            this.orderNumTLPanel.Controls.Add(this.lblOrderNumberValue, 0, 1);
            this.orderNumTLPanel.Location = new System.Drawing.Point(0, 100);
            this.orderNumTLPanel.Margin = new System.Windows.Forms.Padding(0);
            this.orderNumTLPanel.Name = "orderNumTLPanel";
            this.orderNumTLPanel.RowCount = 2;
            this.orderNumTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.orderNumTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.orderNumTLPanel.Size = new System.Drawing.Size(300, 50);
            this.orderNumTLPanel.TabIndex = 13;
            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrderNumber.AutoSize = true;
            this.lblOrderNumber.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblOrderNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNumber.Location = new System.Drawing.Point(1, 1);
            this.lblOrderNumber.Margin = new System.Windows.Forms.Padding(0);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new System.Drawing.Size(298, 20);
            this.lblOrderNumber.TabIndex = 0;
            this.lblOrderNumber.Text = "Order Number";
            this.lblOrderNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrderNumberValue
            // 
            this.lblOrderNumberValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrderNumberValue.AutoSize = true;
            this.lblOrderNumberValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNumberValue.Location = new System.Drawing.Point(1, 22);
            this.lblOrderNumberValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblOrderNumberValue.Name = "lblOrderNumberValue";
            this.lblOrderNumberValue.Size = new System.Drawing.Size(298, 30);
            this.lblOrderNumberValue.TabIndex = 1;
            this.lblOrderNumberValue.Text = "2019081219";
            this.lblOrderNumberValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rfidTLPanel
            // 
            this.rfidTLPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rfidTLPanel.ColumnCount = 1;
            this.rfidTLPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rfidTLPanel.Controls.Add(this.lblRFid, 0, 0);
            this.rfidTLPanel.Controls.Add(this.lblRFidValue, 0, 1);
            this.rfidTLPanel.Location = new System.Drawing.Point(300, 100);
            this.rfidTLPanel.Margin = new System.Windows.Forms.Padding(0);
            this.rfidTLPanel.Name = "rfidTLPanel";
            this.rfidTLPanel.RowCount = 2;
            this.rfidTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.rfidTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.rfidTLPanel.Size = new System.Drawing.Size(342, 50);
            this.rfidTLPanel.TabIndex = 14;
            // 
            // lblRFid
            // 
            this.lblRFid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRFid.AutoSize = true;
            this.lblRFid.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblRFid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRFid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFid.Location = new System.Drawing.Point(0, 0);
            this.lblRFid.Margin = new System.Windows.Forms.Padding(0);
            this.lblRFid.Name = "lblRFid";
            this.lblRFid.Size = new System.Drawing.Size(342, 20);
            this.lblRFid.TabIndex = 0;
            this.lblRFid.Text = "RF Tag";
            this.lblRFid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRFidValue
            // 
            this.lblRFidValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRFidValue.AutoSize = true;
            this.lblRFidValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRFidValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFidValue.Location = new System.Drawing.Point(0, 20);
            this.lblRFidValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblRFidValue.Name = "lblRFidValue";
            this.lblRFidValue.Size = new System.Drawing.Size(342, 30);
            this.lblRFidValue.TabIndex = 1;
            this.lblRFidValue.Text = "----------";
            this.lblRFidValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // palletCodeTLPanel
            // 
            this.palletCodeTLPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.palletCodeTLPanel.ColumnCount = 1;
            this.palletCodeTLPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.palletCodeTLPanel.Controls.Add(this.lblPalletizationCode, 0, 0);
            this.palletCodeTLPanel.Controls.Add(this.lblPalletizationCodeValue, 0, 1);
            this.palletCodeTLPanel.Location = new System.Drawing.Point(0, 150);
            this.palletCodeTLPanel.Margin = new System.Windows.Forms.Padding(0);
            this.palletCodeTLPanel.Name = "palletCodeTLPanel";
            this.palletCodeTLPanel.RowCount = 2;
            this.palletCodeTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.palletCodeTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.palletCodeTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.palletCodeTLPanel.Size = new System.Drawing.Size(150, 80);
            this.palletCodeTLPanel.TabIndex = 15;
            // 
            // lblPalletizationCode
            // 
            this.lblPalletizationCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPalletizationCode.AutoSize = true;
            this.lblPalletizationCode.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblPalletizationCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPalletizationCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPalletizationCode.Location = new System.Drawing.Point(0, 0);
            this.lblPalletizationCode.Margin = new System.Windows.Forms.Padding(0);
            this.lblPalletizationCode.Name = "lblPalletizationCode";
            this.lblPalletizationCode.Size = new System.Drawing.Size(150, 20);
            this.lblPalletizationCode.TabIndex = 0;
            this.lblPalletizationCode.Text = "Palletization Code";
            this.lblPalletizationCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPalletizationCodeValue
            // 
            this.lblPalletizationCodeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPalletizationCodeValue.AutoSize = true;
            this.lblPalletizationCodeValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPalletizationCodeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPalletizationCodeValue.Location = new System.Drawing.Point(0, 20);
            this.lblPalletizationCodeValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblPalletizationCodeValue.Name = "lblPalletizationCodeValue";
            this.lblPalletizationCodeValue.Size = new System.Drawing.Size(150, 60);
            this.lblPalletizationCodeValue.TabIndex = 1;
            this.lblPalletizationCodeValue.Text = "DD";
            this.lblPalletizationCodeValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skidIDTLPanel
            // 
            this.skidIDTLPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skidIDTLPanel.ColumnCount = 1;
            this.skidIDTLPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.skidIDTLPanel.Controls.Add(this.lblSkidID, 0, 0);
            this.skidIDTLPanel.Controls.Add(this.lblSkidIDValue, 0, 1);
            this.skidIDTLPanel.Location = new System.Drawing.Point(150, 150);
            this.skidIDTLPanel.Margin = new System.Windows.Forms.Padding(0);
            this.skidIDTLPanel.Name = "skidIDTLPanel";
            this.skidIDTLPanel.RowCount = 2;
            this.skidIDTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.skidIDTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.skidIDTLPanel.Size = new System.Drawing.Size(150, 80);
            this.skidIDTLPanel.TabIndex = 16;
            // 
            // lblSkidID
            // 
            this.lblSkidID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSkidID.AutoSize = true;
            this.lblSkidID.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSkidID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSkidID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkidID.Location = new System.Drawing.Point(0, 0);
            this.lblSkidID.Margin = new System.Windows.Forms.Padding(0);
            this.lblSkidID.Name = "lblSkidID";
            this.lblSkidID.Size = new System.Drawing.Size(150, 20);
            this.lblSkidID.TabIndex = 0;
            this.lblSkidID.Text = "Skid I.D.";
            this.lblSkidID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSkidIDValue
            // 
            this.lblSkidIDValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSkidIDValue.AutoSize = true;
            this.lblSkidIDValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSkidIDValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkidIDValue.Location = new System.Drawing.Point(0, 20);
            this.lblSkidIDValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblSkidIDValue.Name = "lblSkidIDValue";
            this.lblSkidIDValue.Size = new System.Drawing.Size(150, 60);
            this.lblSkidIDValue.TabIndex = 1;
            this.lblSkidIDValue.Text = "001A";
            this.lblSkidIDValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // confirmationNumberTLPanel
            // 
            this.confirmationNumberTLPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmationNumberTLPanel.ColumnCount = 1;
            this.confirmationNumberTLPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.confirmationNumberTLPanel.Controls.Add(this.lblConfirmationNumber, 0, 0);
            this.confirmationNumberTLPanel.Controls.Add(this.lblConfirmationNumberValue, 0, 1);
            this.confirmationNumberTLPanel.Location = new System.Drawing.Point(300, 150);
            this.confirmationNumberTLPanel.Margin = new System.Windows.Forms.Padding(0);
            this.confirmationNumberTLPanel.Name = "confirmationNumberTLPanel";
            this.confirmationNumberTLPanel.RowCount = 2;
            this.confirmationNumberTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.confirmationNumberTLPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.confirmationNumberTLPanel.Size = new System.Drawing.Size(342, 80);
            this.confirmationNumberTLPanel.TabIndex = 17;
            // 
            // lblConfirmationNumber
            // 
            this.lblConfirmationNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConfirmationNumber.AutoSize = true;
            this.lblConfirmationNumber.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblConfirmationNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConfirmationNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmationNumber.Location = new System.Drawing.Point(0, 0);
            this.lblConfirmationNumber.Margin = new System.Windows.Forms.Padding(0);
            this.lblConfirmationNumber.Name = "lblConfirmationNumber";
            this.lblConfirmationNumber.Size = new System.Drawing.Size(342, 20);
            this.lblConfirmationNumber.TabIndex = 0;
            this.lblConfirmationNumber.Text = "Confirmation Number";
            this.lblConfirmationNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConfirmationNumberValue
            // 
            this.lblConfirmationNumberValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConfirmationNumberValue.AutoSize = true;
            this.lblConfirmationNumberValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConfirmationNumberValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmationNumberValue.Location = new System.Drawing.Point(0, 20);
            this.lblConfirmationNumberValue.Margin = new System.Windows.Forms.Padding(0);
            this.lblConfirmationNumberValue.Name = "lblConfirmationNumberValue";
            this.lblConfirmationNumberValue.Size = new System.Drawing.Size(342, 60);
            this.lblConfirmationNumberValue.TabIndex = 1;
            this.lblConfirmationNumberValue.Text = "2260218110717550007P";
            this.lblConfirmationNumberValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // messageFLPanel
            // 
            this.messageFLPanel.AutoSize = true;
            this.messageFLPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.messageFLPanel.Controls.Add(this.lblMessage);
            this.messageFLPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.messageFLPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageFLPanel.Location = new System.Drawing.Point(642, 0);
            this.messageFLPanel.Margin = new System.Windows.Forms.Padding(0);
            this.messageFLPanel.Name = "messageFLPanel";
            this.messageFLPanel.Size = new System.Drawing.Size(642, 230);
            this.messageFLPanel.TabIndex = 14;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(3, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(609, 87);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "This area displays messages such as a SkidBuildOrder Failure, or why the Order ca" +
    "n\'t be sent (missing Scan), etc...";
            // 
            // skidsFLPanel
            // 
            this.skidsFLPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skidsFLPanel.AutoSize = true;
            this.mainTLPanel.SetColumnSpan(this.skidsFLPanel, 2);
            this.skidsFLPanel.Controls.Add(this.lvKanbans);
            this.skidsFLPanel.Location = new System.Drawing.Point(0, 230);
            this.skidsFLPanel.Margin = new System.Windows.Forms.Padding(0);
            this.skidsFLPanel.Name = "skidsFLPanel";
            this.skidsFLPanel.Size = new System.Drawing.Size(1284, 400);
            this.skidsFLPanel.TabIndex = 13;
            this.skidsFLPanel.WrapContents = false;
            // 
            // lvKanbans
            // 
            this.lvKanbans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvKanbans.HideSelection = false;
            this.lvKanbans.Location = new System.Drawing.Point(0, 0);
            this.lvKanbans.Margin = new System.Windows.Forms.Padding(0);
            this.lvKanbans.Name = "lvKanbans";
            this.lvKanbans.Size = new System.Drawing.Size(1265, 0);
            this.lvKanbans.TabIndex = 0;
            this.lvKanbans.UseCompatibleStateImageBehavior = false;
            // 
            // statusBarFLPanel
            // 
            this.statusBarFLPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTLPanel.SetColumnSpan(this.statusBarFLPanel, 2);
            this.statusBarFLPanel.Controls.Add(this.btnNext);
            this.statusBarFLPanel.Controls.Add(this.btnPrev);
            this.statusBarFLPanel.Controls.Add(this.btnExit);
            this.statusBarFLPanel.Controls.Add(this.lblError);
            this.statusBarFLPanel.Location = new System.Drawing.Point(3, 633);
            this.statusBarFLPanel.Name = "statusBarFLPanel";
            this.statusBarFLPanel.Size = new System.Drawing.Size(1278, 75);
            this.statusBarFLPanel.TabIndex = 11;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(0, 0);
            this.btnNext.Margin = new System.Windows.Forms.Padding(0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(81, 41);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.Location = new System.Drawing.Point(81, 0);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(81, 41);
            this.btnPrev.TabIndex = 8;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(162, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(73, 41);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(235, 0);
            this.lblError.Margin = new System.Windows.Forms.Padding(0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(573, 73);
            this.lblError.TabIndex = 1;
            this.lblError.Text = "*****NO READ*****";
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblError.UseMnemonic = false;
            // 
            // dgvCurrentSkid
            // 
            this.dgvCurrentSkid.AllowUserToAddRows = false;
            this.dgvCurrentSkid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvCurrentSkid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCurrentSkid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCurrentSkid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCurrentSkid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCurrentSkid.ColumnHeadersHeight = 22;
            this.dgvCurrentSkid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCurrentSkid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvCurrentSkidKanban,
            this.dgvCurrentSkidBox,
            this.dgvCurrentSkidPicker,
            this.dgvCurrentSkidTransfer,
            this.dgvCurrentSkidKitter});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCurrentSkid.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCurrentSkid.Location = new System.Drawing.Point(642, 0);
            this.dgvCurrentSkid.Margin = new System.Windows.Forms.Padding(0);
            this.dgvCurrentSkid.Name = "dgvCurrentSkid";
            this.dgvCurrentSkid.ReadOnly = true;
            this.dgvCurrentSkid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCurrentSkid.RowHeadersVisible = false;
            this.dgvCurrentSkid.RowTemplate.Height = 32;
            this.dgvCurrentSkid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvCurrentSkid.ShowCellErrors = false;
            this.dgvCurrentSkid.ShowCellToolTips = false;
            this.dgvCurrentSkid.ShowEditingIcon = false;
            this.dgvCurrentSkid.ShowRowErrors = false;
            this.dgvCurrentSkid.Size = new System.Drawing.Size(642, 230);
            this.dgvCurrentSkid.TabIndex = 12;
            // 
            // dgvCurrentSkidKanban
            // 
            this.dgvCurrentSkidKanban.HeaderText = "Kanban#";
            this.dgvCurrentSkidKanban.MinimumWidth = 80;
            this.dgvCurrentSkidKanban.Name = "dgvCurrentSkidKanban";
            this.dgvCurrentSkidKanban.ReadOnly = true;
            this.dgvCurrentSkidKanban.Width = 80;
            // 
            // dgvCurrentSkidBox
            // 
            this.dgvCurrentSkidBox.HeaderText = "Box";
            this.dgvCurrentSkidBox.MinimumWidth = 60;
            this.dgvCurrentSkidBox.Name = "dgvCurrentSkidBox";
            this.dgvCurrentSkidBox.ReadOnly = true;
            this.dgvCurrentSkidBox.Width = 60;
            // 
            // dgvCurrentSkidPicker
            // 
            this.dgvCurrentSkidPicker.HeaderText = "Picker";
            this.dgvCurrentSkidPicker.MinimumWidth = 180;
            this.dgvCurrentSkidPicker.Name = "dgvCurrentSkidPicker";
            this.dgvCurrentSkidPicker.ReadOnly = true;
            this.dgvCurrentSkidPicker.Width = 180;
            // 
            // dgvCurrentSkidTransfer
            // 
            this.dgvCurrentSkidTransfer.HeaderText = "Transfer";
            this.dgvCurrentSkidTransfer.MinimumWidth = 100;
            this.dgvCurrentSkidTransfer.Name = "dgvCurrentSkidTransfer";
            this.dgvCurrentSkidTransfer.ReadOnly = true;
            this.dgvCurrentSkidTransfer.Width = 140;
            // 
            // dgvCurrentSkidKitter
            // 
            this.dgvCurrentSkidKitter.HeaderText = "Kitter";
            this.dgvCurrentSkidKitter.MinimumWidth = 180;
            this.dgvCurrentSkidKitter.Name = "dgvCurrentSkidKitter";
            this.dgvCurrentSkidKitter.ReadOnly = true;
            this.dgvCurrentSkidKitter.Width = 180;
            // 
            // dgvSkidMinus1
            // 
            this.dgvSkidMinus1.AllowUserToAddRows = false;
            this.dgvSkidMinus1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvSkidMinus1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSkidMinus1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSkidMinus1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSkidMinus1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSkidMinus1.ColumnHeadersHeight = 22;
            this.dgvSkidMinus1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSkidMinus1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvSkidMinus1Kanban,
            this.dgvSkidMinus1Box,
            this.dgvSkidMinus1Picker,
            this.dgvSkidMinus1Transfer,
            this.dgvSkidMinus1Kitter});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSkidMinus1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSkidMinus1.Location = new System.Drawing.Point(0, 0);
            this.dgvSkidMinus1.Margin = new System.Windows.Forms.Padding(0);
            this.dgvSkidMinus1.MinimumSize = new System.Drawing.Size(0, 50);
            this.dgvSkidMinus1.Name = "dgvSkidMinus1";
            this.dgvSkidMinus1.ReadOnly = true;
            this.dgvSkidMinus1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSkidMinus1.RowHeadersVisible = false;
            this.dgvSkidMinus1.RowTemplate.Height = 32;
            this.dgvSkidMinus1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvSkidMinus1.ShowCellErrors = false;
            this.dgvSkidMinus1.ShowCellToolTips = false;
            this.dgvSkidMinus1.ShowEditingIcon = false;
            this.dgvSkidMinus1.ShowRowErrors = false;
            this.dgvSkidMinus1.Size = new System.Drawing.Size(642, 50);
            this.dgvSkidMinus1.TabIndex = 13;
            // 
            // dgvSkidMinus1Kanban
            // 
            this.dgvSkidMinus1Kanban.HeaderText = "Kanban#";
            this.dgvSkidMinus1Kanban.MinimumWidth = 80;
            this.dgvSkidMinus1Kanban.Name = "dgvSkidMinus1Kanban";
            this.dgvSkidMinus1Kanban.ReadOnly = true;
            this.dgvSkidMinus1Kanban.Width = 80;
            // 
            // dgvSkidMinus1Box
            // 
            this.dgvSkidMinus1Box.HeaderText = "Box";
            this.dgvSkidMinus1Box.MinimumWidth = 60;
            this.dgvSkidMinus1Box.Name = "dgvSkidMinus1Box";
            this.dgvSkidMinus1Box.ReadOnly = true;
            this.dgvSkidMinus1Box.Width = 60;
            // 
            // dgvSkidMinus1Picker
            // 
            this.dgvSkidMinus1Picker.HeaderText = "Picker";
            this.dgvSkidMinus1Picker.MinimumWidth = 180;
            this.dgvSkidMinus1Picker.Name = "dgvSkidMinus1Picker";
            this.dgvSkidMinus1Picker.ReadOnly = true;
            this.dgvSkidMinus1Picker.Width = 180;
            // 
            // dgvSkidMinus1Transfer
            // 
            this.dgvSkidMinus1Transfer.HeaderText = "Transfer";
            this.dgvSkidMinus1Transfer.MinimumWidth = 100;
            this.dgvSkidMinus1Transfer.Name = "dgvSkidMinus1Transfer";
            this.dgvSkidMinus1Transfer.ReadOnly = true;
            this.dgvSkidMinus1Transfer.Width = 140;
            // 
            // dgvSkidMinus1Kitter
            // 
            this.dgvSkidMinus1Kitter.HeaderText = "Kitter";
            this.dgvSkidMinus1Kitter.MinimumWidth = 180;
            this.dgvSkidMinus1Kitter.Name = "dgvSkidMinus1Kitter";
            this.dgvSkidMinus1Kitter.ReadOnly = true;
            this.dgvSkidMinus1Kitter.Width = 180;
            // 
            // savedComponentsPanel
            // 
            this.savedComponentsPanel.Controls.Add(this.dgvCurrentSkid);
            this.savedComponentsPanel.Controls.Add(this.dgvSkidMinus1);
            this.savedComponentsPanel.Location = new System.Drawing.Point(0, 0);
            this.savedComponentsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.savedComponentsPanel.Name = "savedComponentsPanel";
            this.savedComponentsPanel.Size = new System.Drawing.Size(1284, 711);
            this.savedComponentsPanel.TabIndex = 14;
            this.savedComponentsPanel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 711);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.savedComponentsPanel);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.mainTLPanel.ResumeLayout(false);
            this.mainTLPanel.PerformLayout();
            this.manifestTLPanel.ResumeLayout(false);
            this.manifestTLPanel.PerformLayout();
            this.supplierTLPanel.ResumeLayout(false);
            this.supplierTLPanel.PerformLayout();
            this.dockTLPanel.ResumeLayout(false);
            this.dockTLPanel.PerformLayout();
            this.subRouteTLPanel.ResumeLayout(false);
            this.subRouteTLPanel.PerformLayout();
            this.orderNumTLPanel.ResumeLayout(false);
            this.orderNumTLPanel.PerformLayout();
            this.rfidTLPanel.ResumeLayout(false);
            this.rfidTLPanel.PerformLayout();
            this.palletCodeTLPanel.ResumeLayout(false);
            this.palletCodeTLPanel.PerformLayout();
            this.skidIDTLPanel.ResumeLayout(false);
            this.skidIDTLPanel.PerformLayout();
            this.confirmationNumberTLPanel.ResumeLayout(false);
            this.confirmationNumberTLPanel.PerformLayout();
            this.messageFLPanel.ResumeLayout(false);
            this.messageFLPanel.PerformLayout();
            this.skidsFLPanel.ResumeLayout(false);
            this.statusBarFLPanel.ResumeLayout(false);
            this.statusBarFLPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentSkid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkidMinus1)).EndInit();
            this.savedComponentsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TableLayoutPanel manifestTLPanel;
        private System.Windows.Forms.Label lblPlantValue;
        private System.Windows.Forms.TableLayoutPanel mainTLPanel;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TableLayoutPanel supplierTLPanel;
        private System.Windows.Forms.Label lblSupplierValue;
        private System.Windows.Forms.TableLayoutPanel dockTLPanel;
        private System.Windows.Forms.Label lblDock;
        private System.Windows.Forms.Label lblDockValue;
        private System.Windows.Forms.TableLayoutPanel subRouteTLPanel;
        private System.Windows.Forms.Label lblSubRoute;
        private System.Windows.Forms.Label lblShipDateTime;
        private System.Windows.Forms.Label lblSubRouteValue;
        private System.Windows.Forms.Label lblSkidNumOfTotal;
        private System.Windows.Forms.TableLayoutPanel orderNumTLPanel;
        private System.Windows.Forms.Label lblOrderNumber;
        private System.Windows.Forms.Label lblOrderNumberValue;
        private System.Windows.Forms.TableLayoutPanel rfidTLPanel;
        private System.Windows.Forms.Label lblRFid;
        private System.Windows.Forms.Label lblRFidValue;
        private System.Windows.Forms.TableLayoutPanel palletCodeTLPanel;
        private System.Windows.Forms.Label lblPalletizationCode;
        private System.Windows.Forms.Label lblPalletizationCodeValue;
        private System.Windows.Forms.TableLayoutPanel skidIDTLPanel;
        private System.Windows.Forms.Label lblSkidID;
        private System.Windows.Forms.Label lblSkidIDValue;
        private System.Windows.Forms.TableLayoutPanel confirmationNumberTLPanel;
        private System.Windows.Forms.Label lblConfirmationNumber;
        private System.Windows.Forms.Label lblConfirmationNumberValue;
        private System.Windows.Forms.FlowLayoutPanel statusBarFLPanel;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.DataGridView dgvCurrentSkid;
        private System.Windows.Forms.FlowLayoutPanel skidsFLPanel;
        private System.Windows.Forms.DataGridView dgvSkidMinus1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSkidMinus1Kanban;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSkidMinus1Box;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSkidMinus1Picker;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSkidMinus1Transfer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSkidMinus1Kitter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCurrentSkidKanban;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCurrentSkidBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCurrentSkidPicker;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCurrentSkidTransfer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCurrentSkidKitter;
        private System.Windows.Forms.FlowLayoutPanel messageFLPanel;
        private System.Windows.Forms.Panel savedComponentsPanel;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ListView lvKanbans;
    }
}

