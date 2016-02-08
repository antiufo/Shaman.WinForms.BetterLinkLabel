using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.ComponentModel;

namespace Shaman.WinForms
{

    public class BetterLinkLabel : System.Windows.Forms.LinkLabel
    {

        private static bool windows;
        static BetterLinkLabel()
        {
            windows = Environment.OSVersion.Platform == PlatformID.Win32NT;
        }

        private static readonly Color DefaultNormalColor = Color.FromArgb(0x00, 0x66, 0xCC);
        private static readonly Color DefaultHoverColor = Color.FromArgb(0x33, 0x99, 0xff);

        public BetterLinkLabel()
        {
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                NormalColor = DefaultNormalColor;
                HoverColor = DefaultHoverColor;
                LinkColor = NormalColor;
                ActiveLinkColor = NormalColor;
            }
        }



        [System.ComponentModel.Browsable(true)]
        public Color NormalColor { get; set; }

        [System.ComponentModel.Browsable(true)]
        public Color HoverColor { get; set; }



        internal bool ShouldSerializeNormalColor()
        {
            return NormalColor != DefaultNormalColor && NormalColor != Color.Empty;
        }

        internal bool ShouldSerializeHoverColor()
        {
            return HoverColor != DefaultHoverColor && HoverColor != Color.Empty;
        }





        [DllImport("user32.dll")]
        static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);


        [DllImport("user32.dll")]
        static extern int SetCursor(IntPtr hCursor);


        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            LinkColor = HoverColor;
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            LinkColor = NormalColor;
        }

        protected override void WndProc(ref Message msg)
        {

            if (windows && msg.Msg == 0x20)
            {
                DefWndProc(ref msg);

                var cur = LoadCursor(IntPtr.Zero, 0x7F89);
                SetCursor(cur);
                return;
            }
            base.WndProc(ref msg);
        }

    }
}