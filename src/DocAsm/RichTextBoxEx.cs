using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocAsm
{
    public class RichTextBoxEx : RichTextBox
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);

        const int WM_USER = 0x400;

        const int EM_HIDESELECTION = WM_USER + 63;
        const int WM_SETFOCUS = 0x0007;
        const UInt32 EM_SETSEL = 0x00B1;

        private bool m_Paint = true;

        public void EndPaint()
        {
            //this.HideSelection = true;
            //this.SuspendLayout();
            //this.m_Paint = false;
            //this.GetNextControl(this, true)?.Focus();
            //SendMessage(this.Handle, EM_HIDESELECTION, 1, 0);
        }

        public void BeginPaint()
        {
            //this.HideSelection = false;
            //this.m_Paint = true;
            //SendMessage(this.Handle, EM_HIDESELECTION, 0, 0);
            //this.ResumeLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if(this.m_Paint)
            {
                base.OnPaint(e);
            }
        }
    }
}
