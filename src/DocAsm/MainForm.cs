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
    public partial class MainForm : GCLForm
    {
        private MarkdownEdit m_Highligher;

        private bool m_UpdateReset = true;
        private bool m_UpdateNecessary = false;

        private bool m_DocumentChanged;
        private string m_CurrentlyEditedDocument = null;

        public MainForm()
        {
            InitializeComponent();
            this.m_Highligher = new MarkdownEdit(this.richTextBox1);

            //this.gclHtmlControl1.HandleCreated += GclHtmlControl1_HandleCreated;
        }

        private void GclHtmlControl1_HandleCreated(object sender, EventArgs e)
        {
            //this.gclHtmlControl1.HTML = Resources.previewPage;
            //this.gclHtmlControl1.ShowDebugger();
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

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TextSegment seg = e.Node.Tag as TextSegment;

            if (seg != null)
            {
                this.richTextBox1.Select(seg.Start, 0);
                this.richTextBox1.Focus();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.grab.BackColor = Color.Orange;

            this.m_DocumentChanged = true;
            this.m_UpdateNecessary = true;
        }

        private void NewFile()
        {
            this.m_CurrentlyEditedDocument = null;
            this.richTextBox1.Text = "";
            this.m_DocumentChanged = false;
            this.grab.BackColor = GCLColors.Border;
        }

        private void SaveFile()
        {
            try
            {
                File.WriteAllText(this.m_CurrentlyEditedDocument, this.richTextBox1.Text);

                this.grab.BackColor = GCLColors.SuccessGreen;
                this.m_DocumentChanged = false;
            }
            catch (Exception ex)
            {
                this.grab.BackColor = GCLColors.ErrorRed;
            }
        }

        private void SaveAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = this.m_CurrentlyEditedDocument;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.m_CurrentlyEditedDocument = sfd.FileName;
                this.SaveFile();
            }
        }


        private void OpenFile()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.richTextBox1.Text = File.ReadAllText(ofd.FileName);

                    this.grab.BackColor = GCLColors.Border;
                    this.m_DocumentChanged = false;
                    this.m_CurrentlyEditedDocument = ofd.FileName;
                }

            }
            catch (Exception ex)
            {
                this.grab.BackColor = GCLColors.ErrorRed;
            }
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuNew_Click(object sender, EventArgs e)
        {
            if(this.m_DocumentChanged)
            {
                DialogResult action = MessageBox.Show("Currently edited file is not saved. Save first?", "Unsaved file", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch(action)
                {
                    case DialogResult.Yes:
                        if (this.m_CurrentlyEditedDocument != null)
                        {
                            this.SaveFile();
                        }
                        else
                        {
                            this.SaveAs();
                        }
                        break;
                    case DialogResult.No:
                        this.NewFile();
                        break;
                }
            }
            else
            {
                this.NewFile();
            }
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            if (this.m_CurrentlyEditedDocument != null)
            {
                this.SaveFile();
            }
            else
            {
                this.SaveAs();
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            this.SaveAs();
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {
            if (this.m_CurrentlyEditedDocument != null && this.m_DocumentChanged)
            {
                DialogResult action = MessageBox.Show("Currently edited file is not saved. Save first?", "Unsaved file", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (action)
                {
                    case DialogResult.Yes:
                        if (this.m_CurrentlyEditedDocument != null)
                        {
                            this.SaveFile();
                        }
                        else
                        {
                            this.SaveAs();
                        }
                        break;
                    case DialogResult.No:
                        this.OpenFile();
                        break;
                }
            }
            else
            {
                this.OpenFile();
            }
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if(this.m_UpdateReset && this.m_UpdateNecessary)
            {
                var result = this.RegenerateOutline(this.richTextBox1.Text, 0, this.richTextBox1.Text.Length, 1);

                this.treeView1.BeginUpdate();
                this.BuildTree(result);
                this.treeView1.ExpandAll();
                this.treeView1.EndUpdate();

                //this.richTextBox1.EndPaint();
                //this.m_Highligher.Run();
                //this.richTextBox1.BeginPaint();

                this.dataGridView1.DataSource = this.SearchFields(this.richTextBox1.Text);

                //string script = String.Format(@"Preview('{0}');", this.richTextBox1.Text);
                //this.gclHtmlControl1.RunJavaScript(script);
                this.webBrowser1.DocumentText = Markdown.ToHtml(this.richTextBox1.Text);


                this.m_UpdateNecessary = false;
            }

            this.m_UpdateReset = true;
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_UpdateReset = false;
        }

        private void menuExport_Click(object sender, EventArgs e)
        {
            new ExportForm(this.m_CurrentlyEditedDocument, this.richTextBox1.Text).ShowDialog();
        }
    }
}
