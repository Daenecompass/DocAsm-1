using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesztek;
using System.IO;
using DocAsm.MarkDown;
using Markdig;
using DocAsm.Exporters;
using GEV.Layouts.Vanilla;

namespace DocAsm.GUI
{
    public partial class ExportWizard : Wizard
    {
        public string SourcePath { get; set; }

        public string Source
        {
            get { return this.m_Source; }
            set
            {
                this.m_Source = value;

                if (this.m_Source != null)
                {
                    List<TextSegment> segments = Utils.CreateOutline(this.m_Source, 0, this.m_Source.Length, 1);
                    Utils.BuildTree(this.treeView1, segments);
                    this.gclDataGrid1.DataSource = new List<FieldInfo>()
                    {
                        new FieldInfo("Creator", ""),
                        new FieldInfo("Category", ""),
                        new FieldInfo("Description", ""),
                        new FieldInfo("Subject", ""),
                        new FieldInfo("Title", ""),
                    };

                    this.gclDataGrid2.DataSource = Utils.SearchFields(this.m_Source);
                }
            }
        }
        private string m_Source;

        private bool m_UpdateNecessary = false;

        public ExportWizard()
        {
            InitializeComponent();
        }

        protected override bool BackClicked()
        {
            if(this.tabsMain.SelectedIndex == 3)
            {
                this.btnNext.Text = "Next >";

            }
            return true;
        }

        protected override bool NextClicked()
        {
            switch(this.tabsMain.SelectedIndex)
            {
                case 0:
                    return File.Exists(this.tbxDotx.Text);
                case 2:
                    this.btnNext.Text = "Export";
                    return true;
                case 3:
                    this.Export();
                    this.ParentForm.Close();
                    return true;

            }
            return true;
        }

        private void Export()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Word Document | *.docx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                new DocxExporter()
                {
                    Source = this.m_Source,
                    TemplatePath = this.tbxDotx.Text,
                    SourcePath = this.SourcePath,
                    DocumentInfo = this.gclDataGrid1.DataSource as SortableBindingList<FieldInfo>,
                }.Export(sfd.FileName);
            }
        }

        private void btnBrowseDotx_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Filter = "Word Template files | *.dotx";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                this.tbxDotx.Text = ofd.FileName;
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                this.CheckTree(e.Node, e.Node.Checked);
                if (e.Node.Checked)
                {
                    e.Node.ExpandAll();
                }
                else
                {
                    e.Node.Collapse(false);
                }
            }

            this.m_UpdateNecessary = true;
        }

        private void CheckTree(TreeNode node, bool value)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                this.CheckTree(childNode, value);
                childNode.Checked = value;
            }
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (this.m_UpdateNecessary)
            {
                string result = Utils.FilterSegments(this.m_Source, this.treeView1);
                List<FieldInfo> fields = this.gclDataGrid2.DataSource as List<FieldInfo>;

                //foreach (FieldInfo field in fields)
                //{
                //    result = result.Replace("$" + field.Name, field.Value);
                //}

                var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseGridTables().UsePipeTables().Build();
                string html = Markdown.ToHtml(result, pipeline);

                this.webBrowser1.DocumentText = html;
                this.webBrowser2.DocumentText = html;

                this.m_UpdateNecessary = false;
            }
        }

        private void gclDataGrid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.m_UpdateNecessary = true;
        }
    }
}
