using System;
using System.Drawing;
using System.Windows.Forms;

namespace Concierge.Utility
{
    #nullable enable
    class VerticalButton : Button
    {

        #region Constructor

        public VerticalButton()
        {
            Format = new StringFormat();
            VerticalText = "";
            Format.Alignment = StringAlignment.Center;
            Format.LineAlignment = StringAlignment.Center;
        }

        #endregion

        #region Methods

        /// =========================================
        /// OnPaint()
        /// =========================================
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.TranslateTransform(0, Height);
            e.Graphics.RotateTransform(270);
            e.Graphics.DrawString(VerticalText, Font, Brushes.White, new Rectangle(0, 0, Height, Width), Format);
        }

        #endregion

        #region Accessors

        public string VerticalText
        {
            get;
            set;
        }

        private StringFormat Format
        {
            get;
            set;
        }

        #endregion

    }
}
