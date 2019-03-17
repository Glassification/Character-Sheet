using static MyCharacterSheet.Utility.Constants;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyCharacterSheet
{
    public partial class DiceRollerPage : Form
    {

        #region Constants

        private const int CONTEXT_OFFSET = 2;

        #endregion

        #region Members

        private List<int>   oDiceList   = new List<int>();
        private Random      oRandom     = new Random();

        #endregion

        #region Constructor

        public DiceRollerPage()
        {
            InitializeComponent();
            FormatContextMenus();
            Reset();
        }

        #endregion

        #region Methods

        /// =========================================
        /// ShowPane()
        /// =========================================
        public void ShowPane()
        {
            Show();
        }

        /// =========================================
        /// Reset()
        /// =========================================
        private void Reset()
        {
            oCustomDieValue.Value = 1;

            oNumberD4.Value = 1;
            oNumberD6.Value = 1;
            oNumberD8.Value = 1;
            oNumberD10.Value = 1;
            oNumberD100.Value = 1;
            oNumberD12.Value = 1;
            oNumberD20.Value = 1;
            oNumberDX.Value = 1;

            oRadioButtonPlus4.Checked = true;
            oRadioButtonPlus6.Checked = true;
            oRadioButtonPlus8.Checked = true;
            oRadioButtonPlus10.Checked = true;
            oRadioButtonPlus100.Checked = true;
            oRadioButtonPlus12.Checked = true;
            oRadioButtonPlus20.Checked = true;
            oRadioButtonPlusX.Checked = true;

            oModifierD4.Value = 0;
            oModifierD6.Value = 0;
            oModifierD8.Value = 0;
            oModifierD10.Value = 0;
            oModifierD100.Value = 0;
            oModifierD12.Value = 0;
            oModifierD20.Value = 0;
            oModifierDX.Value = 0;

            oResults4.Text = "0";
            oResults6.Text = "0";
            oResults8.Text = "0";
            oResults10.Text = "0";
            oResults100.Text = "0";
            oResults12.Text = "0";
            oResults20.Text = "0";
            oResultsX.Text = "0";

            oHistoryGridView.Rows.Clear();

            oDiceList.Clear();
        }

        /// =========================================
        /// AddDiceHistory()
        /// =========================================
        private void AddDiceHistory(int modifier, int total, int number, int die, string sign)
        {
            int index = oHistoryGridView.Rows.Add();
            DataGridViewRow row = oHistoryGridView.Rows[index];

            row.Cells[oRoll.Index].Value = "(" + number + "d" + die + ")" + sign + modifier;
            row.Cells[oDice.Index].Value = ListToString() + sign + modifier;
            row.Cells[oTotal.Index].Value = total.ToString();
        }

        /// =========================================
        /// ListToString()
        /// =========================================
        private string ListToString()
        {
            string str = "";

            foreach (int die in oDiceList)
            {
                str += die + ", ";
            }

            return str;
        }

        /// =========================================
        /// formatContextMenu()
        /// =========================================
        private void FormatContextMenus()
        {
            oHistoryContextMenu.BackColor = DarkGrey;
            oHistoryContextMenu.ForeColor = Color.White;
        }

        #endregion

        #region Accessors

        private int Row
        {
            get;
            set;
        }

        #endregion

        #region Events

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
        /// DiceRollerPage_FormClosing()
        /// =========================================
        private void DiceRollerPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        /// =========================================
        /// btnReset_Click()
        /// =========================================
        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        /// =========================================
        /// btnRoll4_Click()
        /// =========================================
        private void btnRoll4_Click(object sender, EventArgs e)
        {
            int total = 0, val;

            oDiceList.Clear();

            for (int i = 0; i < oNumberD4.Value; i++)
            {
                val = oRandom.Next(1, 5);
                total += val;
                oDiceList.Add(val);
            }

            if (oRadioButtonPlus4.Checked)
                total += (int)oModifierD4.Value;
            else
                total -= (int)oModifierD4.Value;

            total = Math.Max(1, total);

            oResults4.Text = total.ToString();

            AddDiceHistory((int)oModifierD4.Value, total, (int)oNumberD4.Value, 4, oRadioButtonPlus4.Checked ? "+" : "-");
        }

        /// =========================================
        /// btnRoll6_Click()
        /// =========================================
        private void btnRoll6_Click(object sender, EventArgs e)
        {
            int total = 0, val;

            oDiceList.Clear();

            for (int i = 0; i < oNumberD6.Value; i++)
            {
                val = oRandom.Next(1, 7);
                total += val;
                oDiceList.Add(val);
            }

            if (oRadioButtonPlus6.Checked)
                total += (int)oModifierD6.Value;
            else
                total -= (int)oModifierD6.Value;

            total = Math.Max(1, total);

            oResults6.Text = total.ToString();

            AddDiceHistory((int)oModifierD6.Value, total, (int)oNumberD6.Value, 6, oRadioButtonPlus6.Checked ? "+" : "-");
        }

        /// =========================================
        /// btnRoll8_Click()
        /// =========================================
        private void btnRoll8_Click(object sender, EventArgs e)
        {
            int total = 0, val;

            oDiceList.Clear();

            for (int i = 0; i < oNumberD8.Value; i++)
            {
                val = oRandom.Next(1, 9);
                total += val;
                oDiceList.Add(val);
            }

            if (oRadioButtonPlus8.Checked)
                total += (int)oModifierD8.Value;
            else
                total -= (int)oModifierD8.Value;

            total = Math.Max(1, total);

            oResults8.Text = total.ToString();

            AddDiceHistory((int)oModifierD8.Value, total, (int)oNumberD8.Value, 8, oRadioButtonPlus8.Checked ? "+" : "-");
        }

        /// =========================================
        /// btnRoll10_Click()
        /// =========================================
        private void btnRoll10_Click(object sender, EventArgs e)
        {
            int total = 0, val;

            oDiceList.Clear();

            for (int i = 0; i < oNumberD10.Value; i++)
            {
                val = oRandom.Next(1, 11);
                total += val;
                oDiceList.Add(val);
            }

            if (oRadioButtonPlus10.Checked)
                total += (int)oModifierD10.Value;
            else
                total -= (int)oModifierD10.Value;

            total = Math.Max(1, total);

            oResults10.Text = total.ToString();

            AddDiceHistory((int)oModifierD10.Value, total, (int)oNumberD10.Value, 10, oRadioButtonPlus10.Checked ? "+" : "-");
        }

        /// =========================================
        /// btnRoll100_Click()
        /// =========================================
        private void btnRoll100_Click(object sender, EventArgs e)
        {
            int total = 0, val;

            oDiceList.Clear();

            for (int i = 0; i < oNumberD100.Value; i++)
            {
                val = oRandom.Next(1, 101);
                total += val;
                oDiceList.Add(val);
            }

            if (oRadioButtonPlus100.Checked)
                total += (int)oModifierD100.Value;
            else
                total -= (int)oModifierD100.Value;

            total = Math.Max(1, total);

            oResults100.Text = total.ToString();

            AddDiceHistory((int)oModifierD100.Value, total, (int)oNumberD100.Value, 100, oRadioButtonPlus100.Checked ? "+" : "-");
        }

        /// =========================================
        /// btnRoll12_Click()
        /// =========================================
        private void btnRoll12_Click(object sender, EventArgs e)
        {
            int total = 0, val;

            oDiceList.Clear();

            for (int i = 0; i < oNumberD12.Value; i++)
            {
                val = oRandom.Next(1, 13);
                total += val;
                oDiceList.Add(val);
            }

            if (oRadioButtonPlus12.Checked)
                total += (int)oModifierD12.Value;
            else
                total -= (int)oModifierD12.Value;

            total = Math.Max(1, total);

            oResults12.Text = total.ToString();

            AddDiceHistory((int)oModifierD12.Value, total, (int)oNumberD12.Value, 12, oRadioButtonPlus12.Checked ? "+" : "-");
        }

        /// =========================================
        /// btnRoll20_Click()
        /// =========================================
        private void btnRoll20_Click(object sender, EventArgs e)
        {
            int total = 0, val;

            oDiceList.Clear();

            for (int i = 0; i < oNumberD20.Value; i++)
            {
                val = oRandom.Next(1, 21);
                total += val;
                oDiceList.Add(val);
            }

            if (oRadioButtonPlus20.Checked)
                total += (int)oModifierD20.Value;
            else
                total -= (int)oModifierD20.Value;

            total = Math.Max(1, total);

            oResults20.Text = total.ToString();

            AddDiceHistory((int)oModifierD20.Value, total, (int)oNumberD20.Value, 20, oRadioButtonPlus20.Checked ? "+" : "-");
        }

        /// =========================================
        /// btnRollX_Click()
        /// =========================================
        private void btnRollX_Click(object sender, EventArgs e)
        {
            int total = 0, val;

            oDiceList.Clear();

            for (int i = 0; i < oNumberDX.Value; i++)
            {
                val = oRandom.Next(1, ((int)oCustomDieValue.Value) + 1);
                total += val;
                oDiceList.Add(val);
            }

            if (oRadioButtonPlusX.Checked)
                total += (int)oModifierDX.Value;
            else
                total -= (int)oModifierDX.Value;

            total = Math.Max(1, total);

            oResultsX.Text = total.ToString();

            AddDiceHistory((int)oModifierDX.Value, total, (int)oNumberDX.Value, (int)oCustomDieValue.Value, oRadioButtonPlusX.Checked ? "+" : "-");
        }

        /// =========================================
        /// oHistoryGridView_CellMouseClick()
        /// =========================================
        private void oHistoryGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < oHistoryGridView.RowCount)
            {
                Rectangle rect = oHistoryGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Row = e.RowIndex;
                oHistoryContextMenu.Show(oHistoryGridView, new Point(rect.X + e.X + CONTEXT_OFFSET, rect.Y + e.Y + CONTEXT_OFFSET));
            }
        }

        /// =========================================
        /// removeToolStripMenuItem_Click()
        /// =========================================
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oHistoryGridView.Rows.RemoveAt(Row);
        }

        /// =========================================
        /// removeToolStripMenuItem_MouseEnter()
        /// =========================================
        private void removeToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            removeToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// removeToolStripMenuItem_MouseLeave()
        /// =========================================
        private void removeToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            removeToolStripMenuItem.ForeColor = Color.White;
        }

        #endregion

    }
}
