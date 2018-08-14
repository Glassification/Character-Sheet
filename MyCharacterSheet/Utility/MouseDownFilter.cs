using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyCharacterSheet.Utility
{
    class MouseDownFilter : IMessageFilter
    {

        #region Members

        public  event    EventHandler FormClicked;
        private readonly Form         form           = null;
        private readonly int          WM_LBUTTONDOWN = 0x201;

        [DllImport("user32.dll")]
        public static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);

        #endregion

        #region Constructor

        public MouseDownFilter(Form f)
        {
            form = f;
        }

        #endregion

        #region Methods

        /// =========================================
        /// PreFilterMessage()
        /// =========================================
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDOWN)
            {
                if (Form.ActiveForm != null && Form.ActiveForm.Equals(form))
                {
                    OnFormClicked();
                }
            }
            return false;
        }

        /// =========================================
        /// OnFormClicked()
        /// =========================================
        protected void OnFormClicked()
        {
            FormClicked(form, EventArgs.Empty);
        }

        #endregion

    }
}
