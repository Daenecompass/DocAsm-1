namespace DocAsm
{
    partial class ExportForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dockContainer = new GEV.Layouts.Docking.GCLDockingContainer(this.components);
            this.dockPreview = new GEV.Layouts.Docking.GCLDockablePanel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.dockOutline = new GEV.Layouts.Docking.GCLDockablePanel();
            this.dockFields = new GEV.Layouts.Docking.GCLDockablePanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.header = new GEV.Layouts.GCLWindowHeader();
            this.grab = new GEV.Layouts.GCLWindowGrab();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.gclPanel1 = new GEV.Layouts.GCLPanel();
            this.btnCancel = new GEV.Layouts.GCLButton();
            this.btnExport = new GEV.Layouts.GCLButton();
            this.dockContainer.SuspendLayout();
            this.dockPreview.SuspendLayout();
            this.dockOutline.SuspendLayout();
            this.dockFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gclPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.treeView1.Location = new System.Drawing.Point(1, 1);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(279, 446);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // dockContainer
            // 
            this.dockContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(84)))), ((int)(((byte)(86)))));
            this.dockContainer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.dockContainer.Controls.Add(this.dockPreview);
            this.dockContainer.Controls.Add(this.dockOutline);
            this.dockContainer.Controls.Add(this.dockFields);
            this.dockContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockContainer.Location = new System.Drawing.Point(1, 33);
            this.dockContainer.Name = "dockContainer";
            this.dockContainer.Size = new System.Drawing.Size(1285, 448);
            this.dockContainer.TabIndex = 2;
            this.dockContainer.UseThemeColors = true;
            // 
            // dockPreview
            // 
            this.dockPreview.BackColor = System.Drawing.Color.White;
            this.dockPreview.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.dockPreview.Controls.Add(this.webBrowser1);
            this.dockPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPreview.Location = new System.Drawing.Point(281, 0);
            this.dockPreview.Name = "dockPreview";
            this.dockPreview.Padding = new System.Windows.Forms.Padding(1);
            this.dockPreview.Size = new System.Drawing.Size(705, 448);
            this.dockPreview.TabIndex = 4;
            this.dockPreview.Title = "Preview";
            this.dockPreview.UseThemeColors = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(1, 1);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(703, 446);
            this.webBrowser1.TabIndex = 2;
            // 
            // dockOutline
            // 
            this.dockOutline.BackColor = System.Drawing.Color.White;
            this.dockOutline.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.dockOutline.Controls.Add(this.treeView1);
            this.dockOutline.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockOutline.Location = new System.Drawing.Point(0, 0);
            this.dockOutline.Name = "dockOutline";
            this.dockOutline.Padding = new System.Windows.Forms.Padding(1);
            this.dockOutline.Size = new System.Drawing.Size(281, 448);
            this.dockOutline.TabIndex = 0;
            this.dockOutline.Title = "Outline";
            this.dockOutline.UseThemeColors = true;
            // 
            // dockFields
            // 
            this.dockFields.BackColor = System.Drawing.Color.White;
            this.dockFields.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.dockFields.Controls.Add(this.dataGridView1);
            this.dockFields.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockFields.Location = new System.Drawing.Point(986, 0);
            this.dockFields.Name = "dockFields";
            this.dockFields.Padding = new System.Windows.Forms.Padding(1);
            this.dockFields.Size = new System.Drawing.Size(299, 448);
            this.dockFields.TabIndex = 2;
            this.dockFields.Title = "Fields";
            this.dockFields.UseThemeColors = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(1, 1);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Size = new System.Drawing.Size(297, 446);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // header
            // 
            this.header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Location = new System.Drawing.Point(1, 1);
            this.header.Margin = new System.Windows.Forms.Padding(2);
            this.header.Name = "header";
            this.header.ShowCloseButton = true;
            this.header.ShowWindowButtons = true;
            this.header.Size = new System.Drawing.Size(1285, 32);
            this.header.TabIndex = 3;
            this.header.Title = "Document Export";
            // 
            // grab
            // 
            this.grab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.grab.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grab.Location = new System.Drawing.Point(1, 598);
            this.grab.Margin = new System.Windows.Forms.Padding(2);
            this.grab.Name = "grab";
            this.grab.Size = new System.Drawing.Size(1285, 26);
            this.grab.TabIndex = 4;
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 500;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // gclPanel1
            // 
            this.gclPanel1.BackColor = System.Drawing.Color.White;
            this.gclPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.gclPanel1.Controls.Add(this.btnCancel);
            this.gclPanel1.Controls.Add(this.btnExport);
            this.gclPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gclPanel1.Location = new System.Drawing.Point(1, 481);
            this.gclPanel1.Name = "gclPanel1";
            this.gclPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.gclPanel1.Size = new System.Drawing.Size(1285, 117);
            this.gclPanel1.TabIndex = 5;
            this.gclPanel1.UseThemeColors = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCancel.Checked = false;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(134)))), ((int)(((byte)(171)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(158)))), ((int)(((byte)(202)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btnCancel.Location = new System.Drawing.Point(1197, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseThemeColors = true;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnExport.Checked = false;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(134)))), ((int)(((byte)(171)))));
            this.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(158)))), ((int)(((byte)(202)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.btnExport.Location = new System.Drawing.Point(1116, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export";
            this.btnExport.UseThemeColors = true;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 625);
            this.Controls.Add(this.dockContainer);
            this.Controls.Add(this.gclPanel1);
            this.Controls.Add(this.header);
            this.Controls.Add(this.grab);
            this.Name = "ExportForm";
            this.Text = "Document Export";
            this.dockContainer.ResumeLayout(false);
            this.dockPreview.ResumeLayout(false);
            this.dockOutline.ResumeLayout(false);
            this.dockFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gclPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private GEV.Layouts.Docking.GCLDockingContainer dockContainer;
        private GEV.Layouts.Docking.GCLDockablePanel dockPreview;
        private GEV.Layouts.Docking.GCLDockablePanel dockFields;
        private GEV.Layouts.Docking.GCLDockablePanel dockOutline;
        private GEV.Layouts.GCLWindowHeader header;
        private GEV.Layouts.GCLWindowGrab grab;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private GEV.Layouts.GCLPanel gclPanel1;
        private GEV.Layouts.GCLButton btnCancel;
        private GEV.Layouts.GCLButton btnExport;
    }
}

