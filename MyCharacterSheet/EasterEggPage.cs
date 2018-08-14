using MyCharacterSheet.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// EasterEgg()
        /// =========================================
        private void EasterEggPage_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        #endregion
    }
}
