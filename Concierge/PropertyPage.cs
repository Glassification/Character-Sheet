using Concierge.Characters;
using Concierge.Utility;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Concierge
{
    #nullable enable
    public partial class PropertyPage : Form
    {

        #region Constructor

        /// =========================================
        /// PropertyPage()
        /// =========================================
        public PropertyPage()
        {
            InitializeComponent();
            OriginalSize = Size;
            Old = new Character();
            oPropertyGrid.PropertySort = PropertySort.Categorized;
        }

        #endregion

        #region Methods

        /// =========================================
        /// ShowPane()
        /// =========================================
        public void ShowPane()
        {
            Old = new Character();
            Size = OriginalSize;
            Confirm = false;

            Program.Character.Copy(Old);
            oPropertyGrid.SelectedObject = Program.Character;
            
            ShowDialog();
        }

        #endregion

        #region Accessors

        private Character Old
        {
            get;
            set;
        }

        private Size OriginalSize
        {
            get;
            set;
        }

        private bool Confirm
        {
            get;
            set;
        }

        #endregion

        #region Events

        /// =========================================
        /// btnOk_Click()
        /// =========================================
        private void btnOk_Click(object sender, EventArgs e)
        {
            Program.Modified = true;
            Confirm = true;
            Close();
        }

        /// =========================================
        /// btnCancel_Click()
        /// =========================================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Old.Copy(Program.Character);
            Confirm = false;
            Close();
        }

        /// =========================================
        /// btnReset_Click()
        /// =========================================
        private void btnReset_Click(object sender, EventArgs e)
        {
            Old.Copy(Program.Character);
            oPropertyGrid.SelectedObject = Program.Character;
            Invalidate();
        }

        /// =========================================
        /// PropertyPage_FormClosing()
        /// =========================================
        private void PropertyPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Confirm)
            {
                Old.Copy(Program.Character);
            }
        }

        /// =========================================
        /// PropertyPage_KeyDown()
        /// =========================================
        private void PropertyPage_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    btnCancel_Click(new object(), EventArgs.Empty);
                    break;
                case Keys.Enter:
                    btnOk_Click(new object(), EventArgs.Empty);
                    break;
            }
        }

        #endregion

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
