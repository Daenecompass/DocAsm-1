using GEV.Layouts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocAsm.GUI
{
    public partial class ExportForm : GCLForm
    {
        public ExportForm()
        {
            InitializeComponent();
        }

        public ExportForm(string source, string sourcePath)
        {
            InitializeComponent();

            this.exportWizard1.Source = source;
            this.exportWizard1.SourcePath = sourcePath;
        }
    }
}
