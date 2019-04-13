using System;
using System.Windows.Forms;

namespace MyCharacterSheet
{
    #nullable enable
    public partial class InputMessageBox : Form
    {

        #region Constants

        private const int MAX_NAME_LENGTH = 21;

        #endregion

        #region Constructor

        /// =========================================
        /// InputMessageBox()
        /// =========================================
        public InputMessageBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// =========================================
        /// ShowMessage()
        /// =========================================
        public string? ShowMessage(string message, string title, string defaultInput)
        {
            Text = title;
            Message.Text = message;
            oInputBox.Text = defaultInput;
            IsCancelled = true;
            ShowDialog();

            return InputText;
        }

        #endregion

        #region Accessors

        private string? InputText
        {
            get;
            set;
        }

        private bool IsCancelled
        {
            get;
            set;
        }

        #endregion

        #region Events

        /// =========================================
        /// btnOK_Click()
        /// =========================================
        private void btnOK_Click(object sender, EventArgs e)
        {
            InputText = oInputBox.Text;
            IsCancelled = false;
            Close();
        }

        /// =========================================
        /// btnCancel_Click()
        /// =========================================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            InputText = null;
            IsCancelled = true;
            Close();
        }

        /// =========================================
        /// InputMessageBox_FormClosing()
        /// =========================================
        private void InputMessageBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsCancelled)
            {
                InputText = null;
            }
        }

        /// =========================================
        /// InputMessageBox_KeyDown()
        /// =========================================
        private void InputMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                InputText = oInputBox.Text;
                IsCancelled = false;
                Close();
            }
        }

        #endregion

    }
}
