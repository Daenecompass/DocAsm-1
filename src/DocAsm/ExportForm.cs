using DocAsm.Exporters;
using DocAsm.Properties;
using GEV.Layouts;
using Markdig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocAsm
{
    public partial class ExportForm : GCLForm
    {
        private string m_Text;
        private string m_File;

        private bool m_UpdateNecessary = false;

        public ExportForm()
        {
            InitializeComponent();
        }

        public ExportForm(string path, string text)
        {
            InitializeComponent();

            this.m_Text = text;
            this.m_File = path;

            var result = this.RegenerateOutline(this.m_Text, 0, this.m_Text.Length, 1);

            this.treeView1.BeginUpdate();
            this.BuildTree(result);
            this.treeView1.ExpandAll();
            this.treeView1.EndUpdate();

            this.dataGridView1.DataSource = this.SearchFields(this.m_Text);

            this.treeView1.Nodes[0].Checked = true;

            this.m_UpdateNecessary = true;
        }

        private List<TextSegment> RegenerateOutline(string text, int start, int length, int indentLevel)
        {
            List < TextSegment > result = new List<TextSegment>();

            string source = text.Substring(start, length);
            Regex regx = new Regex(@"^ {0,3}#{" + indentLevel + @"} (.+)$", RegexOptions.Multiline);

            MatchCollection matches = regx.Matches(source);

            for(int i = 0; i < matches.Count; i++)
            {
                length = -1;
                if(i < matches.Count - 1)
                {
                    length = matches[i + 1].Index - matches[i].Index; 
                }
                else
                {
                    length = source.Length - matches[i].Index;
                }

                TextSegment seg = new TextSegment()
                {
                    Start = start + matches[i].Index,
                    Length = length,
                    Title = matches[i].Groups[1].Value
                };

                seg.SubSegments = this.RegenerateOutline(text, seg.Start, seg.Length, indentLevel + 1);
                result.Add(seg);
            }

            return result;
        }

        private void BuildTree(List<TextSegment> segments)
        {
            TreeNode node = new TreeNode("ROOT");

            this.BuildTree(segments, node);

            this.treeView1.SuspendLayout();
            this.treeView1.Nodes.Clear();
            this.treeView1.Nodes.Add(node);
            this.treeView1.ResumeLayout();
        }

        private void BuildTree(List<TextSegment> segments, TreeNode root)
        {
            foreach(TextSegment seg in segments)
            {
                TreeNode node = root.Nodes.Add(String.Format("{0}", seg.Title));
                node.Tag = seg;
                this.BuildTree(seg.SubSegments, node);
            }
        }

        private List<FieldInfo> SearchFields(string text)
        {
            List<FieldInfo> result = new List<FieldInfo>();

            Regex regx = new Regex(@"\$([a-z][a-z0-9_]+)", RegexOptions.IgnoreCase);

            MatchCollection matches = regx.Matches(text);

            foreach(Match match in matches)
            {
                string field = match.Groups[1].Value;

                if(!result.Exists(f => f.Name == field))
                {
                    result.Add(new FieldInfo(field, ""));
                }
            }

            return result;
        }

        private void CheckTree(TreeNode node, bool value)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                this.CheckTree(childNode, value);
                childNode.Checked = value;
            }
        }

        private string FilterSegments(string text)
        {
            string result = text;
            List<TextSegment> segmentToRemove = FilterSegments(this.treeView1.Nodes[0]);

            segmentToRemove = segmentToRemove.OrderBy(segment => segment.Start).ToList();

            int startOffset = 0;
            foreach(TextSegment segment in segmentToRemove)
            {
                result = result.Remove(segment.Start - startOffset, segment.Length);

                startOffset += segment.Length;
            }

            return result;
        }

        private List<TextSegment> FilterSegments(TreeNode node)
        {
            List<TextSegment> toRemove = new List<TextSegment>();

            if(node.Checked)
            {
                foreach(TreeNode childNode in node.Nodes)
                {
                    toRemove.AddRange(this.FilterSegments(childNode));
                }
            }
            else
            {
                toRemove.Add(node.Tag as TextSegment);
            }

            return toRemove;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //TextSegment seg = e.Node.Tag as TextSegment;

            //if (seg != null)
            //{
            //    this.richTextBox1.Select(seg.Start, 0);
            //    this.richTextBox1.Focus();
            //}
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if(this.m_UpdateNecessary)
            {
                string result = this.FilterSegments(this.m_Text);
                List<FieldInfo> fields = this.dataGridView1.DataSource as List<FieldInfo>;

                foreach(FieldInfo field in fields)
                {
                    result = result.Replace("$" + field.Name, field.Value);
                }

                var Scroll = 0;
                if (webBrowser1.Document != null)
                {
                    Scroll = webBrowser1.Document.GetElementsByTagName("HTML")[0].ScrollTop;
                }
                this.webBrowser1.DocumentText = Markdown.ToHtml(result);

                if (webBrowser1.Document != null)
                {
                    var test = webBrowser1.Document.GetElementsByTagName("HTML");
                    //webBrowser1.Document.GetElementsByTagName("HTML")[0].ScrollTop = Scroll;
                }

                this.m_UpdateNecessary = false;
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if(e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                this.CheckTree(e.Node, e.Node.Checked);
                if(e.Node.Checked)
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


        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.m_UpdateNecessary = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            new DocxExporter()
            {
                Browser = this.webBrowser1,
                Source = this.webBrowser1.DocumentText,
                SourcePath = this.m_File,
                TemplatePath = @"C:\Users\gkimmel\Documents\template.dotx",
            }.Export();
        }
    }
}
