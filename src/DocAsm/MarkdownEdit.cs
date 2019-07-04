using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocAsm
{
    public class MarkdownEdit
    {
        private class Style
        {
            public Regex Regx { get; set; }
            public Font Font { get; set; }
            public Color FrontColor { get; set; } = Color.Transparent;
            public Color BackColor { get; set; } = Color.Transparent;
        }

        //private Regex List = new Regex(@"^ +(*|\d\.) (.+)$", RegexOptions.Multiline);
        //private Regex Block = new Regex(@"^> (.+)$", RegexOptions.Multiline);
        //private Regex Comment = new Regex(@"^ {0,3}#{6} (.+)$", RegexOptions.Multiline);
        //private Regex Code = new Regex(@"^ {0,3}#{6} (.+)$", RegexOptions.Multiline);

        private RichTextBox m_TextBox;
        private List<Style> m_Styles;

        public MarkdownEdit(RichTextBox rtb)
        {
            this.m_TextBox = rtb;

            this.m_Styles = new List<Style>()
            {
                new Style()
                {
                    Regx = new Regex(@"^ {0,3}#{1} (.+)$", RegexOptions.Multiline),
                    Font = new Font(rtb.Font.FontFamily, 20f),
                    FrontColor = Color.DarkMagenta
                },
                new Style()
                {
                    Regx = new Regex(@"^ {0,3}#{2} (.+)$", RegexOptions.Multiline),
                    Font = new Font(rtb.Font.FontFamily, 15f),
                    FrontColor = Color.DarkMagenta
                },
                new Style()
                {
                    Regx = new Regex(@"^ {0,3}#{3} (.+)$", RegexOptions.Multiline),
                    FrontColor = Color.DarkMagenta
                },
                new Style()
                {
                    Regx = new Regex(@"^ {0,3}#{4} (.+)$", RegexOptions.Multiline),
                    FrontColor = Color.DarkMagenta
                },
                new Style()
                {
                    Regx = new Regex(@"^ {0,3}#{5} (.+)$", RegexOptions.Multiline),
                    FrontColor = Color.DarkMagenta
                },
                new Style()
                {
                    Regx = new Regex(@"^ {0,3}#{6} (.+)$", RegexOptions.Multiline),
                    FrontColor = Color.DarkMagenta
                },
                new Style()
                {
                    Regx = new Regex(@"\*.+?\*", RegexOptions.Multiline),
                    Font = new Font(rtb.Font, FontStyle.Italic),
                },
                new Style()
                {
                    Regx = new Regex(@"\*\*.+?\*\*", RegexOptions.Multiline),
                    Font = new Font(rtb.Font, FontStyle.Bold),
                },
                new Style()
                {
                    Regx = new Regex(@"\*\*\*.+?\*\*\*", RegexOptions.Multiline),
                    Font = new Font(rtb.Font, FontStyle.Italic | FontStyle.Bold),
                },
                new Style()
                {
                    Regx = new Regex(@"__.+?__", RegexOptions.Multiline),
                    Font = new Font(rtb.Font, FontStyle.Italic | FontStyle.Underline),
                },

            };
        }

        public void Run()
        {
            int selectStart = this.m_TextBox.SelectionStart;
            int selectLength = this.m_TextBox.SelectionLength;

            foreach (Style s in this.m_Styles)
            {
                MatchCollection matches = s.Regx.Matches(this.m_TextBox.Text);
                foreach(Match m in matches)
                {
                    this.m_TextBox.Select(m.Index, m.Length);
                    if (s.Font != null){ this.m_TextBox.SelectionFont = s.Font; }
                    if (s.BackColor != Color.Transparent) { this.m_TextBox.SelectionBackColor = s.BackColor; }
                    if (s.FrontColor != Color.Transparent) { this.m_TextBox.SelectionColor = s.FrontColor; }
                }
            }

            this.m_TextBox.Select(selectStart, selectLength);

        }
    }
}
