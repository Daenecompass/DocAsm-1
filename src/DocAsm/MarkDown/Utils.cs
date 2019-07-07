using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocAsm.MarkDown
{
    public class Utils
    {
        public static List<TextSegment> CreateOutline(string text, int start, int length, int indentLevel)
        {
            List<TextSegment> result = new List<TextSegment>();

            string source = text.Substring(start, length);
            Regex regx = new Regex(@"^ {0,3}#{" + indentLevel + @"} (.+)$", RegexOptions.Multiline);

            MatchCollection matches = regx.Matches(source);

            for (int i = 0; i < matches.Count; i++)
            {
                length = -1;
                if (i < matches.Count - 1)
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

                seg.SubSegments = Utils.CreateOutline(text, seg.Start, seg.Length, indentLevel + 1);
                result.Add(seg);
            }

            return result;
        }

        public static void BuildTree(TreeView treeview, List<TextSegment> segments)
        {
            TreeNode node = new TreeNode("ROOT");

            BuildTree(segments, node);

            treeview.SuspendLayout();
            treeview.Nodes.Clear();
            treeview.Nodes.Add(node);
            treeview.ResumeLayout();
        }

        private static void BuildTree(List<TextSegment> segments, TreeNode root)
        {
            foreach (TextSegment seg in segments)
            {
                TreeNode node = root.Nodes.Add(String.Format("{0}", seg.Title));
                node.Checked = true;
                node.Tag = seg;
                BuildTree(seg.SubSegments, node);
            }
        }

        public static List<FieldInfo> SearchFields(string text)
        {
            List<FieldInfo> result = new List<FieldInfo>();

            Regex regx = new Regex(@"\$([a-z][a-z0-9_]+)", RegexOptions.IgnoreCase);

            MatchCollection matches = regx.Matches(text);

            foreach (Match match in matches)
            {
                string field = match.Groups[1].Value;

                if (!result.Exists(f => f.Name == field))
                {
                    result.Add(new FieldInfo(field, ""));
                }
            }

            return result;
        }

        public static string FilterSegments(string text, TreeView selector)
        {
            string result = text;
            List<TextSegment> segmentToRemove = FilterSegments(selector.Nodes[0]);

            segmentToRemove = segmentToRemove.OrderBy(segment => segment.Start).ToList();

            int startOffset = 0;
            foreach (TextSegment segment in segmentToRemove)
            {
                result = result.Remove(segment.Start - startOffset, segment.Length);

                startOffset += segment.Length;
            }

            return result;
        }

        private static List<TextSegment> FilterSegments(TreeNode node)
        {
            List<TextSegment> toRemove = new List<TextSegment>();

            if (node.Checked)
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    toRemove.AddRange(FilterSegments(childNode));
                }
            }
            else
            {
                toRemove.Add(node.Tag as TextSegment);
            }

            return toRemove;
        }
    }
}
