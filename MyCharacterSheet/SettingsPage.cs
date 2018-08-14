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
    public partial class SettingsPage : Form
    {

        #region Constructor

        public SettingsPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// =========================================
        /// ShowPane()
        /// =========================================
        public void ShowPane()
        {
            Mute          = Settings.RememberMute;
            RememberTab   = Settings.RememberLastTab;
            Autosave      = Settings.AutosaveEnable;
            Interval      = Settings.AutosaveInterval;
            DefaultTextColour = Settings.DefaultColour;
            DefaultTextFont   = Settings.DefaultFont;

            trkAutosave.Enabled = Autosave;
            oAutosaveInterval.Enabled = Autosave;

            ShowDialog();
        }

        /// =========================================
        /// SaveChanges()
        /// =========================================
        private void SaveChanges()
        {
            Settings.RememberMute     = Mute;
            Settings.RememberLastTab  = RememberTab;
            Settings.AutosaveEnable   = Autosave;
            Settings.AutosaveInterval = Interval;
            Settings.DefaultColour    = DefaultTextColour;
            Settings.DefaultFont      = DefaultTextFont;
        }

        /// =========================================
        /// FormatAutosvaeLabel()
        /// =========================================
        private string FormatAutosvaeLabel()
        {
            string str = "Autosave every ";

            str += Interval.ToString();
            if (Interval > 1)
                str += " minutes";
            else
                str += " minute";

            return str;
        }

        #endregion

        #region Accessors

        private bool Mute
        {
            get;
            set;
        }

        private bool RememberTab
        {
            get;
            set;
        }

        private bool Autosave
        {
            get;
            set;
        }

        private int Interval
        {
            get;
            set;
        }

        private Color DefaultTextColour
        {
            get;
            set;
        }

        private Font DefaultTextFont
        {
            get;
            set;
        }

        private bool Drawing
        {
            get;
            set;
        }

        #endregion

        #region Events

        /// =========================================
        /// SettingsPage_Paint()
        /// =========================================
        private void SettingsPage_Paint(object sender, PaintEventArgs e)
        {
            Drawing = true;

            chkMute.Checked = Mute;

            chkTab.Checked = RememberTab;

            chkAutosave.Checked = Autosave;
            trkAutosave.Value = Constants.AutosaveIndex(Interval);
            oAutosaveInterval.Text = FormatAutosvaeLabel();

            Drawing = false;
        }

        /// =========================================
        /// SettingsPage_KeyDown()
        /// =========================================
        private void SettingsPage_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Return:
                    btnOK_Click(new object(), new EventArgs());
                    break;
            }
        }

        /// =========================================
        /// btnColour_Click()
        /// =========================================
        private void btnColour_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();

            DialogResult result;

            oColorDialog.Color = DefaultTextColour;
            result = oColorDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                DefaultTextColour = oColorDialog.Color;
            }
        }

        /// =========================================
        /// btnFont_Click()
        /// =========================================
        private void btnFont_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();

            DialogResult result;

            oFontDialog.Font = DefaultTextFont;
            result = oFontDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                DefaultTextFont = oFontDialog.Font;
            }
        }

        /// =========================================
        /// btnOK_Click()
        /// =========================================
        private void btnOK_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();

            Program.Modified = true;
            SaveChanges();
            Close();
        }

        /// =========================================
        /// btnCancel_Click()
        /// =========================================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();

            Close();
        }

        /// =========================================
        /// chkMute_CheckedChanged()
        /// =========================================
        private void chkMute_CheckedChanged(object sender, EventArgs e)
        {
            if (!Drawing)
            {
                Sounds.ButtonClick();
            }
            
            Mute = chkMute.Checked;
        }

        /// =========================================
        /// chkTab_CheckedChanged()
        /// =========================================
        private void chkTab_CheckedChanged(object sender, EventArgs e)
        {
            if(!Drawing)
            {
                Sounds.ButtonClick();
            }

            RememberTab = chkTab.Checked;
        }

        /// =========================================
        /// chkAutosave_CheckedChanged()
        /// =========================================
        private void chkAutosave_CheckedChanged(object sender, EventArgs e)
        {
            if (!Drawing)
            {
                Sounds.ButtonClick();
            }

            Autosave = chkAutosave.Checked;

            trkAutosave.Enabled = Autosave;
            oAutosaveInterval.Enabled = Autosave;
        }

        /// =========================================
        /// trkAutosave_ValueChanged()
        /// =========================================
        private void trkAutosave_ValueChanged(object sender, EventArgs e)
        {
            Interval = Constants.AutosaveInterval(trkAutosave.Value);
            Invalidate();
        }

        #endregion
        
    }
}
