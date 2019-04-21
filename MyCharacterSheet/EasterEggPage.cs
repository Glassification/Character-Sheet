using MyCharacterSheet.Utility;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyCharacterSheet
{
    public partial class EasterEggPage : Form
    {

        #region Constructor

        public EasterEggPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// =========================================
        /// EasterEgg()
        /// =========================================
        public void ShowPane()
        {
            Sounds.HeMan();
            Size = new Size(oPictureBox.Image.Width, oPictureBox.Image.Height);
            ShowDialog();
            Sounds.StopLooping();
        }

        #endregion

        #region Events

        /// =========================================
        /// EasterEggPage_KeyDown()
        /// =========================================
        private void EasterEggPage_KeyDown(object sender, KeyEventArgs e)
        {
            // Let user change the volume
            if (e.KeyCode != Keys.VolumeUp && e.KeyCode != Keys.VolumeDown && e.KeyCode != Keys.VolumeMute)
            {
                Close();
            }
        }

        /// =========================================
        /// OPictureBox_Click()
        /// =========================================
        private void OPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

    }
}
