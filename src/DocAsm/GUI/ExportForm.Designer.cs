namespace DocAsm.GUI
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
            this.header = new GEV.Layouts.GCLWindowHeader();
            this.grab = new GEV.Layouts.GCLWindowGrab();
            this.exportWizard1 = new DocAsm.GUI.ExportWizard();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
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
            this.header.Size = new System.Drawing.Size(927, 32);
            this.header.TabIndex = 4;
            this.header.Title = "Document Export";
            // 
            // grab
            // 
            this.grab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.grab.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grab.Location = new System.Drawing.Point(1, 629);
            this.grab.Margin = new System.Windows.Forms.Padding(2);
            this.grab.Name = "grab";
            this.grab.Size = new System.Drawing.Size(927, 26);
            this.grab.TabIndex = 5;
            // 
            // exportWizard1
            // 
            this.exportWizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exportWizard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exportWizard1.Location = new System.Drawing.Point(1, 33);
            this.exportWizard1.Name = "exportWizard1";
            this.exportWizard1.Size = new System.Drawing.Size(927, 596);
            this.exportWizard1.Source = null;
            this.exportWizard1.TabIndex = 6;
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 656);
            this.Controls.Add(this.exportWizard1);
            this.Controls.Add(this.grab);
            this.Controls.Add(this.header);
            this.Name = "ExportForm";
            this.Text = "ExportForm";
            this.ResumeLayout(false);

        }

        #endregion

        private GEV.Layouts.GCLWindowHeader header;
        private GEV.Layouts.GCLWindowGrab grab;
        private ExportWizard exportWizard1;
        private System.Windows.Forms.Timer timer1;
    }
}