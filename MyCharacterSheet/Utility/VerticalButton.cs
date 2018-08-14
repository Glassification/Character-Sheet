using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyCharacterSheet.Utility
{
    class VerticalButton : Button
    {

        #region Members

        private StringFormat format = new StringFormat();

        #endregion

        #region Constructor

        public VerticalButton()
        {
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
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
            e.Graphics.DrawString(VerticalText, Font, Brushes.White, new Rectangle(0, 0, Height, Width), format);
        }

        #endregion

        #region Accessors

        public string VerticalText
        {
            get;
            set;
        }

        #endregion

    }
}
