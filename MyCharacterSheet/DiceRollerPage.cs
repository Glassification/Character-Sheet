using MyCharacterSheet.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyCharacterSheet
{
    public partial class DiceRollerPage : Form
    {

        #region Constants

        private const int MAX_DICE = 6;

        #endregion

        #region Members

        private List<int>[] oDiceList = new List<int>[MAX_DICE];

        #endregion

        #region Constructor

        public DiceRollerPage()
        {
            InitializeComponent();
            createLists();
            formatInputBoxes();
            formatContextMenu();
        }

        #endregion

        #region Methods

        /// =========================================
        /// SaveCharacterSheetXML()
        /// =========================================
        public void ShowPane()
        {
            reset();
            ShowDialog();
        }

        /// =========================================
        /// createLists()
        /// =========================================
        private void createLists()
        {
            for (int i = 0; i < oDiceList.Length; i++)
            {
                oDiceList[i] = new List<int>();
            }
        }

        /// =========================================
        /// reset()
        /// =========================================
        private void reset()
        {
            oTotalRollValue.Text = "0";

            chkD4.Checked = false;
            chkD6.Checked = false;
            chkD8.Checked = false;
            chkD10.Checked = false;
            chkD12.Checked = false;
            chkD20.Checked = false;

            oInputD4.Value = 0;
            oInputD6.Value = 0;
            oInputD8.Value = 0;
            oInputD10.Value = 0;
            oInputD12.Value = 0;
            oInputD20.Value = 0;
        }

        /// =========================================
        /// fillDiceList()
        /// =========================================
        private void fillDiceList(int diceNumber, int sides, int index, bool check)
        {
            if (check)
            {
                Random rand = new Random();

                for (int i = 0; i < diceNumber; i++)
                {
                    oDiceList[index].Add(rand.Next(1, sides + 1));
                }
            }
        }

        /// =========================================
        /// rollDice()
        /// =========================================
        private void rollDice()
        {
            oDiceList[0].Clear();
            fillDiceList((int)oInputD4.Value, 4, 0, chkD4.Checked);

            oDiceList[1].Clear();
            fillDiceList((int)oInputD6.Value, 6, 1, chkD6.Checked);

            oDiceList[2].Clear();
            fillDiceList((int)oInputD8.Value, 8, 2, chkD8.Checked);

            oDiceList[3].Clear();
            fillDiceList((int)oInputD10.Value, 10, 3, chkD10.Checked);

            oDiceList[4].Clear();
            fillDiceList((int)oInputD12.Value, 12, 4, chkD12.Checked);

            oDiceList[5].Clear();
            fillDiceList((int)oInputD20.Value, 20, 5, chkD20.Checked);
        }

        /// =========================================
        /// diceTotal()
        /// =========================================
        private int diceTotal()
        {
            int total = 0;

            for (int i = 0; i < oDiceList.Length; i++)
            {
                foreach (int item in oDiceList[i])
                {
                    total += item;
                }
            }

            return total;
        }

        /// =========================================
        /// diceValueTotal()
        /// =========================================
        private string diceValueTotal()
        {
            string strValues = "";

            for (int i = 0; i < oDiceList.Length; i++)
            {
                foreach (int item in oDiceList[i])
                {
                    strValues += item + "+";
                }
            }

            strValues = strValues.TrimEnd(new char[] { '+' });

            return strValues;
        }

        /// =========================================
        /// fillDiceHistory()
        /// =========================================
        private void fillDiceHistory()
        {
            int index = oDiceHistoryGridView.Rows.Add();
            DataGridViewRow row = oDiceHistoryGridView.Rows[index];

            row.Cells[oRoll.Index].Value = index + 1;
            row.Cells[oValues.Index].Value = diceValueTotal();
            row.Cells[oTotal.Index].Value = diceTotal();

            oDiceHistoryGridView.FirstDisplayedScrollingRowIndex = oDiceHistoryGridView.RowCount - 1;
        }

        /// =========================================
        /// IsValid()
        /// =========================================
        private bool IsValid()
        {
            bool validCheck, validInput = true;

            validCheck = chkD4.Checked || chkD6.Checked || chkD8.Checked || chkD10.Checked || chkD12.Checked || chkD20.Checked;

            if      (chkD4.Checked && oInputD4.Value <= 0)   { validInput = false; }
            else if (chkD6.Checked && oInputD6.Value <= 0)   { validInput = false; }
            else if (chkD8.Checked && oInputD8.Value <= 0)   { validInput = false; }
            else if (chkD10.Checked && oInputD10.Value <= 0) { validInput = false; }
            else if (chkD12.Checked && oInputD12.Value <= 0) { validInput = false; }
            else if (chkD20.Checked && oInputD20.Value <= 0) { validInput = false; }

            return validCheck && validInput;
        }

        /// =========================================
        /// formatInputBoxes()
        /// =========================================
        private void formatInputBoxes()
        {
            oInputD4.Width = oPanelD4.Width;
            oInputD6.Width = oPanelD6.Width;
            oInputD8.Width = oPanelD8.Width;
            oInputD10.Width = oPanelD10.Width;
            oInputD12.Width = oPanelD12.Width;
            oInputD20.Width = oPanelD20.Width;

            oInputD4.Location = new Point(0, (oPanelD4.Height / 2) - (oInputD4.Height / 2));
            oInputD6.Location = new Point(0, (oPanelD6.Height / 2) - (oInputD6.Height / 2));
            oInputD8.Location = new Point(0, (oPanelD8.Height / 2) - (oInputD8.Height / 2));
            oInputD10.Location = new Point(0, (oPanelD10.Height / 2) - (oInputD10.Height / 2));
            oInputD12.Location = new Point(0, (oPanelD12.Height / 2) - (oInputD12.Height / 2));
            oInputD20.Location = new Point(0, (oPanelD20.Height / 2) - (oInputD20.Height / 2));
        }

        /// =========================================
        /// formatContextMenu()
        /// =========================================
        private void formatContextMenu()
        {
            oContextMenuStrip.BackColor = Constants.DarkGrey;
            oContextMenuStrip.ForeColor = Color.White;
        }

        #endregion

        #region Events

        /// =========================================
        /// btnRollDice_Click()
        /// =========================================
        private void btnRollDice_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();

            if (IsValid())
            {
                rollDice();
                fillDiceHistory();
                oTotalRollValue.Text = diceTotal() + "";
            }
        }

        /// =========================================
        /// DiceRollerPage_KeyPress()
        /// =========================================
        private void DiceRollerPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                Close();
            }
        }

        /// =========================================
        /// chkD4_CheckedChanged()
        /// =========================================
        private void chkD4_CheckedChanged(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// chkD6_CheckedChanged()
        /// =========================================
        private void chkD6_CheckedChanged(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// chkD8_CheckedChanged()
        /// =========================================
        private void chkD8_CheckedChanged(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// chkD10_CheckedChanged()
        /// =========================================
        private void chkD10_CheckedChanged(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// chkD12_CheckedChanged()
        /// =========================================
        private void chkD12_CheckedChanged(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// chkD20_CheckedChanged()
        /// =========================================
        private void chkD20_CheckedChanged(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// oInputD4_ValueChanged()
        /// =========================================
        private void oInputD4_ValueChanged(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// oInputD6_ValueChanged()
        /// =========================================
        private void oInputD6_ValueChanged(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// oInputD8_ValueChanged()
        /// =========================================
        private void oInputD8_ValueChanged(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// oInputD10_ValueChanged()
        /// =========================================
        private void oInputD10_ValueChanged(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// oInputD12_ValueChanged()
        /// =========================================
        private void oInputD12_ValueChanged(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// oInputD20_ValueChanged()
        /// =========================================
        private void oInputD20_ValueChanged(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// oDiceHistoryGridView_MouseDown()
        /// =========================================
        private void oDiceHistoryGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                oContextMenuStrip.Show(oDiceHistoryGridView, new Point(e.X + Constants.OFFSET, e.Y + Constants.OFFSET));
            }
        }

        /// =========================================
        /// oContextMenuStrip_MouseEnter()
        /// =========================================
        private void oContextMenuStrip_MouseEnter(object sender, EventArgs e)
        {
            oContextMenuStrip.ForeColor = Color.Black;
        }

        /// =========================================
        /// oContextMenuStrip_MouseLeave()
        /// =========================================
        private void oContextMenuStrip_MouseLeave(object sender, EventArgs e)
        {
            oContextMenuStrip.ForeColor = Color.White;
        }

        /// =========================================
        /// oClearHistoryToolStripMenuItem_Click()
        /// =========================================
        private void oClearHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oDiceHistoryGridView.Rows.Clear();
        }

        #endregion

    }
}
