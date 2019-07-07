namespace DocAsm
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dockContainer = new GEV.Layouts.Docking.GCLDockingContainer(this.components);
            this.richTextBox1 = new DocAsm.RichTextBoxEx();
            this.dockPreview = new GEV.Layouts.Docking.GCLDockablePanel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.dockOutline = new GEV.Layouts.Docking.GCLDockablePanel();
            this.themeProvider = new GEV.Layouts.Theming.GCLThemeProvider(this.components);
            this.header = new GEV.Layouts.GCLWindowHeader();
            this.grab = new GEV.Layouts.GCLWindowGrab();
            this.menuMain = new GEV.Layouts.GCLMenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.dockContainer.SuspendLayout();
            this.dockPreview.SuspendLayout();
            this.dockOutline.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.treeView1.Location = new System.Drawing.Point(1, 18);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(198, 546);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // dockContainer
            // 
            this.dockContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(84)))), ((int)(((byte)(86)))));
            this.dockContainer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.dockContainer.Controls.Add(this.richTextBox1);
            this.dockContainer.Controls.Add(this.dockPreview);
            this.dockContainer.Controls.Add(this.dockOutline);
            this.dockContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockContainer.Location = new System.Drawing.Point(1, 33);
            this.dockContainer.Name = "dockContainer";
            this.dockContainer.Size = new System.Drawing.Size(1285, 565);
            this.dockContainer.TabIndex = 2;
            this.dockContainer.UseThemeColors = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.AutoWordSelection = true;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Segoe UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.HideSelection = false;
            this.richTextBox1.Location = new System.Drawing.Point(200, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ShowSelectionMargin = true;
            this.richTextBox1.Size = new System.Drawing.Size(693, 565);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            // 
            // dockPreview
            // 
            this.dockPreview.BackColor = System.Drawing.Color.White;
            this.dockPreview.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.dockPreview.Controls.Add(this.webBrowser1);
            this.dockPreview.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockPreview.Location = new System.Drawing.Point(893, 0);
            this.dockPreview.Name = "dockPreview";
            this.dockPreview.Padding = new System.Windows.Forms.Padding(1);
            this.dockPreview.Size = new System.Drawing.Size(392, 565);
            this.dockPreview.TabIndex = 4;
            this.dockPreview.Title = "Preview";
            this.dockPreview.UseThemeColors = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(1, 18);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(390, 546);
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
            this.dockOutline.Size = new System.Drawing.Size(200, 565);
            this.dockOutline.TabIndex = 0;
            this.dockOutline.Title = "Outline";
            this.dockOutline.UseThemeColors = true;
            // 
            // themeProvider
            // 
            this.themeProvider.AccentColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(134)))), ((int)(((byte)(171)))));
            this.themeProvider.AccentColor1Highlight = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(158)))), ((int)(((byte)(202)))));
            this.themeProvider.AccentColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(87)))), ((int)(((byte)(152)))));
            this.themeProvider.AccentColor2Highlight = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(106)))), ((int)(((byte)(170)))));
            this.themeProvider.AccentColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(36)))), ((int)(((byte)(37)))));
            this.themeProvider.AlertYellow = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(0)))));
            this.themeProvider.BasicTheme = GEV.Layouts.Theming.BasicThemes.Light;
            this.themeProvider.Border = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.themeProvider.Button = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.themeProvider.ErrorRed = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.themeProvider.FormBackground = System.Drawing.Color.Gainsboro;
            this.themeProvider.HeaderBackground = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(36)))), ((int)(((byte)(37)))));
            this.themeProvider.MenuBackground = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.themeProvider.PanelBackground = System.Drawing.Color.White;
            this.themeProvider.PrimaryText = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.themeProvider.SecondaryText = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.themeProvider.Shadow = System.Drawing.Color.White;
            this.themeProvider.SoftBorder = System.Drawing.Color.Gainsboro;
            this.themeProvider.SuccessGreen = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
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
            this.header.Title = "DocAsm";
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
            // menuMain
            // 
            this.menuMain.AutoSize = false;
            this.menuMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.menuMain.Dock = System.Windows.Forms.DockStyle.None;
            this.menuMain.ForeColor = System.Drawing.Color.White;
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(216, 33);
            this.menuMain.TabIndex = 2;
            this.menuMain.Text = "gclMenuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew,
            this.menuOpen,
            this.menuSave,
            this.menuSaveAs,
            this.menuExport,
            this.menuClose});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menuNew
            // 
            this.menuNew.ForeColor = System.Drawing.Color.White;
            this.menuNew.Name = "menuNew";
            this.menuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuNew.Size = new System.Drawing.Size(180, 22);
            this.menuNew.Text = "&New";
            this.menuNew.Click += new System.EventHandler(this.menuNew_Click);
            // 
            // menuOpen
            // 
            this.menuOpen.ForeColor = System.Drawing.Color.White;
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuOpen.Size = new System.Drawing.Size(180, 22);
            this.menuOpen.Text = "&Open...";
            this.menuOpen.Click += new System.EventHandler(this.menuOpen_Click);
            // 
            // menuSave
            // 
            this.menuSave.ForeColor = System.Drawing.Color.White;
            this.menuSave.Name = "menuSave";
            this.menuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuSave.Size = new System.Drawing.Size(180, 22);
            this.menuSave.Text = "&Save";
            this.menuSave.Click += new System.EventHandler(this.menuSave_Click);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.ForeColor = System.Drawing.Color.White;
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(180, 22);
            this.menuSaveAs.Text = "Save as...";
            this.menuSaveAs.Click += new System.EventHandler(this.menuSaveAs_Click);
            // 
            // menuExport
            // 
            this.menuExport.ForeColor = System.Drawing.Color.White;
            this.menuExport.Name = "menuExport";
            this.menuExport.Size = new System.Drawing.Size(180, 22);
            this.menuExport.Text = "&Export...";
            this.menuExport.Click += new System.EventHandler(this.menuExport_Click);
            // 
            // menuClose
            // 
            this.menuClose.ForeColor = System.Drawing.Color.White;
            this.menuClose.Name = "menuClose";
            this.menuClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuClose.Size = new System.Drawing.Size(180, 22);
            this.menuClose.Text = "&Close";
            this.menuClose.Click += new System.EventHandler(this.menuClose_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.outlineToolStripMenuItem,
            this.previewToolStripMenuItem});
            this.viewToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 29);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // outlineToolStripMenuItem
            // 
            this.outlineToolStripMenuItem.Checked = true;
            this.outlineToolStripMenuItem.CheckOnClick = true;
            this.outlineToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.outlineToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.outlineToolStripMenuItem.Name = "outlineToolStripMenuItem";
            this.outlineToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.outlineToolStripMenuItem.Text = "Outline";
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.Checked = true;
            this.previewToolStripMenuItem.CheckOnClick = true;
            this.previewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.previewToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.previewToolStripMenuItem.Text = "Preview";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(42, 29);
            this.helpToolStripMenuItem.Text = "help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.aboutToolStripMenuItem.Text = "about";
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 500;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 625);
            this.Controls.Add(this.dockContainer);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.header);
            this.Controls.Add(this.grab);
            this.MainMenuStrip = this.menuMain;
            this.Name = "MainForm";
            this.Text = "DocAsm";
            this.dockContainer.ResumeLayout(false);
            this.dockPreview.ResumeLayout(false);
            this.dockOutline.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private DocAsm.RichTextBoxEx richTextBox1;
        private GEV.Layouts.Docking.GCLDockingContainer dockContainer;
        private GEV.Layouts.Docking.GCLDockablePanel dockPreview;
        private GEV.Layouts.Docking.GCLDockablePanel dockOutline;
        private GEV.Layouts.Theming.GCLThemeProvider themeProvider;
        private GEV.Layouts.GCLWindowHeader header;
        private GEV.Layouts.GCLWindowGrab grab;
        private GEV.Layouts.GCLMenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuExport;
        private System.Windows.Forms.ToolStripMenuItem menuClose;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuNew;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
    }
}

