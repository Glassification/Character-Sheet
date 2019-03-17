using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MyCharacterSheet.Lists;
using MyCharacterSheet.Utility;
using static MyCharacterSheet.Utility.Constants;

namespace MyCharacterSheet
{
    public partial class TertiaryPage : UserControl
    {

        #region Constants

        private const int CLASS_HEIGHT  = 20;
        private const int SPELL_HEIGHT  = 50;
        private const int ANIMAL_HEIGHT = 30;

        #endregion

        #region Members

        private List<float> oTertiaryLabelSizes = new List<float>();
        private List<Label> oTertiaryLabels     = new List<Label>();

        private Rectangle   dragBoxFromMouseDown;
        private int         rowIndexFromMouseDown;
        private int         rowIndexOfItemUnderMouseToDrop;

        #endregion

        #region Constructor

        public TertiaryPage()
        {
            //Initialize components
            InitializeComponent();
            Drawing = false;
            RowIndex = 1;

            //Set initial state
            FillLabelList();
            FillSizes();
            FormatContextMenus();
        }

        #endregion

        #region Methods

        /// =========================================
        /// FillSizes()
        /// =========================================
        private void FillSizes()
        {
            foreach (Label l in oTertiaryLabels)
            {
                oTertiaryLabelSizes.Add(l.Font.Size);
            }

            MagicHeaderSize = oMagicGridView.ColumnHeadersDefaultCellStyle.Font.Size;
            SpellHeaderSize = oSpellListDataView.ColumnHeadersDefaultCellStyle.Font.Size;

            MagicRowSize = oMagicGridView.DefaultCellStyle.Font.Size;
            SpellRowSize = oSpellListDataView.DefaultCellStyle.Font.Size;
        }

        /// =========================================
        /// FormatContextMenus()
        /// =========================================
        private void FormatContextMenus()
        {
            oSpellClassContextMenu.BackColor = DarkGrey;
            oSpellClassContextMenu.ForeColor = Color.White;

            oAddClassContextMenu.BackColor = DarkGrey;
            oAddClassContextMenu.ForeColor = Color.White;

            oSpellListContextMenu.BackColor = DarkGrey;
            oSpellListContextMenu.ForeColor = Color.White;

            oAddSpellContextMenu.BackColor = DarkGrey;
            oAddSpellContextMenu.ForeColor = Color.White;
        }

        /// =========================================
        /// SetAnimalCompanion()
        /// =========================================
        public void SetAnimalCompanionVisibility()
        {
            if (Settings.HideAnimalCompanion)
            {
                // New sheet is loaded with companion already hidden
                if (oTertiaryTableLayoutPanel.Controls.Count > 2)
                {
                    AnimalCompanion = oTertiaryTableLayoutPanel.Controls[RowIndex];
                    oTertiaryTableLayoutPanel.Controls.RemoveAt(RowIndex);
                    oTertiaryTableLayoutPanel.RowStyles.RemoveAt(RowIndex);

                    oTertiaryTableLayoutPanel.RowStyles[0].Height = CLASS_HEIGHT;
                    oTertiaryTableLayoutPanel.RowStyles[1].Height = SPELL_HEIGHT + ANIMAL_HEIGHT;
                    oTertiaryTableLayoutPanel.RowCount--;

                    //The last row switches to 2 after starting at 1 for some reason.
                    //This whole thing doesn't make sense.
                    if (RowIndex == 1)
                        RowIndex = 2;
                }
            }
            else if (oTertiaryTableLayoutPanel.RowCount == 2)
            {
                oTertiaryTableLayoutPanel.RowStyles.Add(new RowStyle());
                oTertiaryTableLayoutPanel.Controls.Add(AnimalCompanion, 0, 2);

                oTertiaryTableLayoutPanel.RowStyles[0].Height = CLASS_HEIGHT;
                oTertiaryTableLayoutPanel.RowStyles[1].Height = SPELL_HEIGHT;
                oTertiaryTableLayoutPanel.RowStyles[2].Height = ANIMAL_HEIGHT;
                oTertiaryTableLayoutPanel.RowCount++;
            }
        }

        /// =========================================
        /// ResizeLabels()
        /// =========================================
        public void ResizeLabels()
        {
            float ratio = Program.MainForm.Ratio;

            for (int i = 0; i < oTertiaryLabels.Count; i++)
            {
                oTertiaryLabels[i].Font = new Font(oTertiaryLabels[i].Font.FontFamily, oTertiaryLabelSizes[i] * (ratio == 1 ? ratio : (ratio / SIZE_MOD)), oTertiaryLabels[i].Font.Style);

                ScaleFont(oTertiaryLabels[i]);
            }
        }

        /// =========================================
        /// ResizeText()
        /// =========================================
        public void ResizeText()
        {
            float ratio = Program.MainForm.Ratio;

            ResizeLabels();

            oMagicGridView.ColumnHeadersDefaultCellStyle.Font = new Font(oMagicGridView.ColumnHeadersDefaultCellStyle.Font.FontFamily, MagicHeaderSize * (ratio == 1 ? ratio : (ratio / SIZE_MOD)), oMagicGridView.ColumnHeadersDefaultCellStyle.Font.Style);
            oSpellListDataView.ColumnHeadersDefaultCellStyle.Font = new Font(oSpellListDataView.ColumnHeadersDefaultCellStyle.Font.FontFamily, SpellHeaderSize * (ratio == 1 ? ratio : (ratio / SIZE_MOD)), oSpellListDataView.ColumnHeadersDefaultCellStyle.Font.Style);

            oMagicGridView.DefaultCellStyle.Font = new Font(oMagicGridView.DefaultCellStyle.Font.FontFamily, MagicRowSize * (ratio == 1 ? ratio : (ratio / SIZE_MOD)), oMagicGridView.Font.Style);
            oSpellListDataView.DefaultCellStyle.Font = new Font(oSpellListDataView.DefaultCellStyle.Font.FontFamily, SpellRowSize * (ratio == 1 ? ratio : (ratio / SIZE_MOD)), oSpellListDataView.Font.Style);
        }

        /// =========================================
        /// FillSpellclass()
        /// =========================================
        public void FillSpellclass()
        {
            oMagicGridView.Rows.Clear();

            foreach (Magic magic in Program.Character.Spellcasting.oMagic)
            {
                int index = oMagicGridView.Rows.Add();
                DataGridViewRow row = oMagicGridView.Rows[index];

                row.Cells[SpellClass.Index].Value = magic.Class;
                row.Cells[MagicAbility.Index].Value = magic.Ability;
                row.Cells[SpellAttackBonus.Index].Value = Program.Character.GetBonus(magic.Ability);
                row.Cells[SpellSaveDC.Index].Value = Program.Character.GetDC(magic.Ability);
                row.Cells[Cantrips.Index].Value = magic.Cantrips;
                row.Cells[Spells.Index].Value = magic.Spells;
                row.Cells[Prepared.Index].Value = magic.Prepared;

                row.Tag = magic.ID;
            }
        }

        /// =========================================
        /// FillSpellList()
        /// =========================================
        public void FillSpellList()
        {
            oSpellListDataView.Rows.Clear();

            foreach (Spell spell in Program.Character.Spellcasting.oSpells)
            {
                int index = oSpellListDataView.Rows.Add();
                DataGridViewRow row = oSpellListDataView.Rows[index];

                row.Cells[oName.Index].Value = spell.Name;
                row.Cells[oLevel.Index].Value = spell.Level;
                row.Cells[oPage.Index].Value = spell.Page;
                row.Cells[oSchool.Index].Value = spell.School;
                row.Cells[oRitual.Index].Value = spell.Ritual;
                row.Cells[oComp.Index].Value = spell.Components;
                row.Cells[oConcen.Index].Value = spell.Concentration;
                row.Cells[oRange.Index].Value = spell.Range;
                row.Cells[oDuration.Index].Value = spell.Duration;
                row.Cells[oArea.Index].Value = spell.Area;
                row.Cells[oSave.Index].Value = spell.Save;
                row.Cells[oDamage.Index].Value = spell.Damage;
                row.Cells[oDescription.Index].Value = spell.Description;
                row.Cells[oPrepared.Index].Value = spell.Prepared;

                row.Tag = spell.ID;
            }
        }

        /// =========================================
        /// DefaultFocus()
        /// =========================================
        public void DefaultFocus()
        {
            oTertiaryFocus.Focus();
        }

        /// =========================================
        /// SpellListGridView()
        /// =========================================
        public Control SpellListGridView()
        {
            return oSpellListDataView;
        }

        /// =========================================
        /// MagicGridView()
        /// =========================================
        public Control MagicGridView()
        {
            return oMagicGridView;
        }

        #endregion

        #region Accessors

        private int Row
        {
            get;
            set;
        }

        private int RowIndex
        {
            get;
            set;
        }

        private float MagicHeaderSize
        {
            get;
            set;
        }

        private float MagicRowSize
        {
            get;
            set;
        }

        private float SpellHeaderSize
        {
            get;
            set;
        }

        private float SpellRowSize
        {
            get;
            set;
        }

        private bool Drawing
        {
            get;
            set;
        }

        private Control AnimalCompanion
        {
            get;
            set;
        }

        #endregion

        #region Frame Events

        /// =========================================
        /// TertiaryPage_Paint()
        /// =========================================
        private void TertiaryPage_Paint(object sender, PaintEventArgs e)
        {
            if (!Program.Loading)
            {
                Drawing = true;

                oCasterLevel.Text = Program.Character.Spellcasting.Level + "";

                oSlotPact.Text = Program.Character.Spellcasting.PactTotal + "";
                oSlot1.Text = Program.Character.Spellcasting.OneTotal + "";
                oSlot2.Text = Program.Character.Spellcasting.TwoTotal + "";
                oSlot3.Text = Program.Character.Spellcasting.ThreeTotal + "";
                oSlot4.Text = Program.Character.Spellcasting.FourTotal + "";
                oSlot5.Text = Program.Character.Spellcasting.FiveTotal + "";
                oSlot6.Text = Program.Character.Spellcasting.SixTotal + "";
                oSlot7.Text = Program.Character.Spellcasting.SevenTotal + "";
                oSlot8.Text = Program.Character.Spellcasting.EightTotal + "";
                oSlot9.Text = Program.Character.Spellcasting.NineTotal + "";

                oUsedPact.Text = Program.Character.Spellcasting.PactUsed + "";
                oUsed1.Text = Program.Character.Spellcasting.OneUsed + "";
                oUsed2.Text = Program.Character.Spellcasting.TwoUsed + "";
                oUsed3.Text = Program.Character.Spellcasting.ThreeUsed + "";
                oUsed4.Text = Program.Character.Spellcasting.FourUsed + "";
                oUsed5.Text = Program.Character.Spellcasting.FiveUsed + "";
                oUsed6.Text = Program.Character.Spellcasting.SixUsed + "";
                oUsed7.Text = Program.Character.Spellcasting.SevenUsed + "";
                oUsed8.Text = Program.Character.Spellcasting.EightUsed + "";
                oUsed9.Text = Program.Character.Spellcasting.NineUsed + "";

                oSlotPact.BackColor = Constants.TotalBoxColour(Program.Character.Spellcasting.PactTotal, Program.Character.Spellcasting.PactUsed);
                oSlot1.BackColor = Constants.TotalBoxColour(Program.Character.Spellcasting.OneTotal, Program.Character.Spellcasting.OneUsed);
                oSlot2.BackColor = Constants.TotalBoxColour(Program.Character.Spellcasting.TwoTotal, Program.Character.Spellcasting.TwoUsed);
                oSlot3.BackColor = Constants.TotalBoxColour(Program.Character.Spellcasting.ThreeTotal, Program.Character.Spellcasting.ThreeUsed);
                oSlot4.BackColor = Constants.TotalBoxColour(Program.Character.Spellcasting.FourTotal, Program.Character.Spellcasting.FourUsed);
                oSlot5.BackColor = Constants.TotalBoxColour(Program.Character.Spellcasting.FiveTotal, Program.Character.Spellcasting.FiveUsed);
                oSlot6.BackColor = Constants.TotalBoxColour(Program.Character.Spellcasting.SixTotal, Program.Character.Spellcasting.SixUsed);
                oSlot7.BackColor = Constants.TotalBoxColour(Program.Character.Spellcasting.SevenTotal, Program.Character.Spellcasting.SevenUsed);
                oSlot8.BackColor = Constants.TotalBoxColour(Program.Character.Spellcasting.EightTotal, Program.Character.Spellcasting.EightUsed);
                oSlot9.BackColor = Constants.TotalBoxColour(Program.Character.Spellcasting.NineTotal, Program.Character.Spellcasting.NineUsed);

                oUsedPact.BackColor = Constants.UsedBoxColour(Program.Character.Spellcasting.PactTotal, Program.Character.Spellcasting.PactUsed);
                oUsed1.BackColor = Constants.UsedBoxColour(Program.Character.Spellcasting.OneTotal, Program.Character.Spellcasting.OneUsed);
                oUsed2.BackColor = Constants.UsedBoxColour(Program.Character.Spellcasting.TwoTotal, Program.Character.Spellcasting.TwoUsed);
                oUsed3.BackColor = Constants.UsedBoxColour(Program.Character.Spellcasting.ThreeTotal, Program.Character.Spellcasting.ThreeUsed);
                oUsed4.BackColor = Constants.UsedBoxColour(Program.Character.Spellcasting.FourTotal, Program.Character.Spellcasting.FourUsed);
                oUsed5.BackColor = Constants.UsedBoxColour(Program.Character.Spellcasting.FiveTotal, Program.Character.Spellcasting.FiveUsed);
                oUsed6.BackColor = Constants.UsedBoxColour(Program.Character.Spellcasting.SixTotal, Program.Character.Spellcasting.SixUsed);
                oUsed7.BackColor = Constants.UsedBoxColour(Program.Character.Spellcasting.SevenTotal, Program.Character.Spellcasting.SevenUsed);
                oUsed8.BackColor = Constants.UsedBoxColour(Program.Character.Spellcasting.EightTotal, Program.Character.Spellcasting.EightUsed);
                oUsed9.BackColor = Constants.UsedBoxColour(Program.Character.Spellcasting.NineTotal, Program.Character.Spellcasting.NineUsed);

                oUsedPact.ForeColor = Constants.TextColour(Program.Character.Spellcasting.PactTotal, Program.Character.Spellcasting.PactUsed);
                oUsed1.ForeColor = Constants.TextColour(Program.Character.Spellcasting.OneTotal, Program.Character.Spellcasting.OneUsed);
                oUsed2.ForeColor = Constants.TextColour(Program.Character.Spellcasting.TwoTotal, Program.Character.Spellcasting.TwoUsed);
                oUsed3.ForeColor = Constants.TextColour(Program.Character.Spellcasting.ThreeTotal, Program.Character.Spellcasting.ThreeUsed);
                oUsed4.ForeColor = Constants.TextColour(Program.Character.Spellcasting.FourTotal, Program.Character.Spellcasting.FourUsed);
                oUsed5.ForeColor = Constants.TextColour(Program.Character.Spellcasting.FiveTotal, Program.Character.Spellcasting.FiveUsed);
                oUsed6.ForeColor = Constants.TextColour(Program.Character.Spellcasting.SixTotal, Program.Character.Spellcasting.SixUsed);
                oUsed7.ForeColor = Constants.TextColour(Program.Character.Spellcasting.SevenTotal, Program.Character.Spellcasting.SevenUsed);
                oUsed8.ForeColor = Constants.TextColour(Program.Character.Spellcasting.EightTotal, Program.Character.Spellcasting.EightUsed);
                oUsed9.ForeColor = Constants.TextColour(Program.Character.Spellcasting.NineTotal, Program.Character.Spellcasting.NineUsed);

                oCompanionName.Text = Program.Character.Companion.Name;
                oCompanionAC.Text = Program.Character.Companion.AC + "";
                oCompanionHitDice.Text = Program.Character.Companion.HitDice;
                oCompanionHP.Text = Program.Character.Companion.HP + "";
                oCompanionCurrentHP.Text = Program.Character.Companion.CurrentHP + "";
                oCompanionSpeed.Text = Program.Character.Companion.Speed;
                oCompanionStrScore.Text = Program.Character.Companion.Strength + "";
                oCompanionDexScore.Text = Program.Character.Companion.Dexterity + "";
                oCompanionConScore.Text = Program.Character.Companion.Constitution + "";
                oCompanionIntScore.Text = Program.Character.Companion.Intelligence + "";
                oCompanionWisScore.Text = Program.Character.Companion.Wisdom + "";
                oCompanionChaScore.Text = Program.Character.Companion.Charisma + "";
                oCompanionPerception.Text = Program.Character.Companion.Perception + "";
                oCompanionSenses.Text = Program.Character.Companion.Senses;

                oCompanionStrBonus.Text = Constants.Bonus(Program.Character.Companion.Strength) + "";
                oCompanionDexBonus.Text = Constants.Bonus(Program.Character.Companion.Dexterity) + "";
                oCompanionConBonus.Text = Constants.Bonus(Program.Character.Companion.Constitution) + "";
                oCompanionIntBonus.Text = Constants.Bonus(Program.Character.Companion.Intelligence) + "";
                oCompanionWisBonus.Text = Constants.Bonus(Program.Character.Companion.Wisdom) + "";
                oCompanionChaBonus.Text = Constants.Bonus(Program.Character.Companion.Charisma) + "";

                oCompanionAttack1.Text = Program.Character.Companion.Attack.First;
                oCompanionAttack2.Text = Program.Character.Companion.Attack.Second;
                oCompanionType1.Text = Program.Character.Companion.Type.First;
                oCompanionType2.Text = Program.Character.Companion.Type.Second;
                oCompanionAtkBonus1.Text = Program.Character.Companion.AtkBonus.First;
                oCompanionAtkBonus2.Text = Program.Character.Companion.AtkBonus.Second;
                oCompanionDamage1.Text = Program.Character.Companion.Damage.First;
                oCompanionDamage2.Text = Program.Character.Companion.Damage.Second;
                oCompanionDmgType1.Text = Program.Character.Companion.DmgType.First;
                oCompanionDmgType2.Text = Program.Character.Companion.DmgType.Second;
                oCompanionReach1.Text = Program.Character.Companion.Reach.First;
                oCompanionReach2.Text = Program.Character.Companion.Reach.Second;
                oCompanionNote1.Text = Program.Character.Companion.Notes.First;
                oCompanionNote2.Text = Program.Character.Companion.Notes.Second;

                ResizeLabels();

                Drawing = false;
            }
        }

        /// =========================================
        /// TertiaryPage_VisibleChanged()
        /// =========================================
        private void TertiaryPage_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                Drawing = true;

                Invalidate();

                Drawing = false;
            }
        }

        #endregion

        #region SpellClass Events

        /// =========================================
        /// oMagicGridView_Sorted()
        /// =========================================
        private void oMagicGridView_Sorted(object sender, EventArgs e)
        {
            int index;
            string rowID;
            Magic item;

            // Sort each item
            for (int i = 0; i < oMagicGridView.Rows.Count; i++)
            {
                rowID = oMagicGridView.Rows[i].Tag as string;

                // Check if already in correct position 
                if (!rowID.Equals(Program.Character.Spellcasting.oMagic[i].ID))
                {
                    index = Program.Character.Spellcasting.GetMagicIndex(rowID);
                    item = Program.Character.Spellcasting.oMagic[index];

                    Program.Character.Spellcasting.oMagic.RemoveAt(index);
                    Program.Character.Spellcasting.oMagic.Insert(index, Program.Character.Spellcasting.oMagic[i]);

                    Program.Character.Spellcasting.oMagic.RemoveAt(i);
                    Program.Character.Spellcasting.oMagic.Insert(i, item);
                }
            }
        }

        /// =========================================
        /// oMagicGridView_CellValueChanged()
        /// =========================================
        private void oMagicGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!Drawing)
            {
                Program.Modified = true;
            }

            if (e.ColumnIndex == MagicAbility.Index && !Program.Loading)
            {
                oMagicGridView.Rows[e.RowIndex].Cells[SpellAttackBonus.Index].Value = Program.Character.GetBonus((string)oMagicGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                oMagicGridView.Rows[e.RowIndex].Cells[SpellSaveDC.Index].Value = Program.Character.GetDC((string)oMagicGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }

        /// =========================================
        /// oSpellClassDeleteRow_Click()
        /// =========================================
        private void oSpellClassDeleteRow_Click(object sender, EventArgs e)
        {
            Program.Modified = true;

            Program.Character.Spellcasting.RemoveMagicItem(oMagicGridView.Rows[Row].Tag as string);
            oMagicGridView.Rows.RemoveAt(Row);
        }

        /// =========================================
        /// editClassToolStripMenuItem_Click()
        /// =========================================
        private void editClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Modified = true;

            Program.MainForm.oTablePage.ShowPane(Tables.Magics, Program.Character.Spellcasting.oMagic[Row]);
            FillSpellclass();
        }

        /// =========================================
        /// editClassToolStripMenuItem_MouseEnter()
        /// =========================================
        private void editClassToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            editClassToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// editClassToolStripMenuItem_MouseLeave()
        /// =========================================
        private void editClassToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            editClassToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// oSpellClassDeleteRow_MouseEnter()
        /// =========================================
        private void oSpellClassDeleteRow_MouseEnter(object sender, EventArgs e)
        {
            oSpellClassDeleteRow.ForeColor = Color.Black;
        }

        /// =========================================
        /// oSpellClassDeleteRow_MouseLeave()
        /// =========================================
        private void oSpellClassDeleteRow_MouseLeave(object sender, EventArgs e)
        {
            oSpellClassDeleteRow.ForeColor = Color.White;
        }

        /// =========================================
        /// oMagicGridView_CellMouseClick()
        /// =========================================
        private void oMagicGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < oMagicGridView.RowCount)
            {
                Rectangle rect = oMagicGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Row = e.RowIndex;
                oSpellClassContextMenu.Show(oMagicGridView, new Point(rect.X + e.X + OFFSET, rect.Y + e.Y + OFFSET));
            }
        }

        /// =========================================
        /// oMagicGridView_MouseMove()
        /// =========================================
        private void oMagicGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffects = oMagicGridView.DoDragDrop(oMagicGridView.Rows[rowIndexFromMouseDown], DragDropEffects.Move);
                }
            }
        }

        /// =========================================
        /// oMagicGridView_MouseDown()
        /// =========================================
        private void oMagicGridView_MouseDown(object sender, MouseEventArgs e)
        {
            rowIndexFromMouseDown = oMagicGridView.HitTest(e.X, e.Y).RowIndex;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (rowIndexFromMouseDown != -1)
                    {
                        Size dragSize = SystemInformation.DragSize;
                        dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
                    }
                    else
                    {
                        dragBoxFromMouseDown = Rectangle.Empty;
                    }
                    break;
                case MouseButtons.Right:
                    if (rowIndexFromMouseDown == -1)
                        oAddClassContextMenu.Show(oMagicGridView, new Point(e.X + OFFSET, e.Y + OFFSET));
                    break;
            }
        }

        /// =========================================
        /// oMagicGridView_DragOver()
        /// =========================================
        private void oMagicGridView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// =========================================
        /// oMagicGridView_DragDrop()
        /// =========================================
        private void oMagicGridView_DragDrop(object sender, DragEventArgs e)
        {
            Magic item;

            if (oMagicGridView.Rows.Count > 1)
            {
                Point clientPoint = oMagicGridView.PointToClient(new Point(e.X, e.Y));

                rowIndexOfItemUnderMouseToDrop = oMagicGridView.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                if (e.Effect == DragDropEffects.Move)
                {

                    DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                    //set as last row
                    if (rowIndexOfItemUnderMouseToDrop < 0 || rowIndexOfItemUnderMouseToDrop >= oMagicGridView.Rows.Count)
                        rowIndexOfItemUnderMouseToDrop = oMagicGridView.Rows.Count - 1;

                    if (rowIndexFromMouseDown != rowIndexOfItemUnderMouseToDrop)
                        Program.Modified = true;

                    // Move list item
                    oMagicGridView.Rows.RemoveAt(rowIndexFromMouseDown);
                    oMagicGridView.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);

                    // Move data item
                    item = Program.Character.Spellcasting.oMagic[rowIndexFromMouseDown];
                    Program.Character.Spellcasting.oMagic.RemoveAt(rowIndexFromMouseDown);
                    Program.Character.Spellcasting.oMagic.Insert(rowIndexOfItemUnderMouseToDrop, item);
                }
            }
        }

        /// =========================================
        /// addClassToolStripMenuItem_Click()
        /// =========================================
        private void addClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.MainForm.oTablePage.ShowPane(Tables.Magics);
            FillSpellclass();
        }

        /// =========================================
        /// addClassToolStripMenuItem_MouseEnter()
        /// =========================================
        private void addClassToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            addClassToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// addClassToolStripMenuItem_MouseLeave()
        /// =========================================
        private void addClassToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            addClassToolStripMenuItem.ForeColor = Color.White;
        }

        #endregion

        #region SpellSlot Events

        /// =========================================
        /// oUsedPact_DoubleClick()
        /// =========================================
        private void oUsedPact_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.PactUsed + 1) <= Program.Character.Spellcasting.PactTotal)
            {
                Program.Character.Spellcasting.PactUsed++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.Spellcasting.PactUsed == Program.Character.Spellcasting.PactTotal)
                {
                    oUsedPact.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oUsed1_DoubleClick()
        /// =========================================
        private void oUsed1_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.OneUsed + 1) <= Program.Character.Spellcasting.OneTotal)
            {
                Program.Character.Spellcasting.OneUsed++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.Spellcasting.OneUsed == Program.Character.Spellcasting.OneTotal)
                {
                    oUsed1.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oUsed2_DoubleClick()
        /// =========================================
        private void oUsed2_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.TwoUsed + 1) <= Program.Character.Spellcasting.TwoTotal)
            {
                Program.Character.Spellcasting.TwoUsed++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.Spellcasting.TwoUsed == Program.Character.Spellcasting.TwoTotal)
                {
                    oUsed2.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oUsed3_DoubleClick()
        /// =========================================
        private void oUsed3_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.ThreeUsed + 1) <= Program.Character.Spellcasting.ThreeTotal)
            {
                Program.Character.Spellcasting.ThreeUsed++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.Spellcasting.ThreeUsed == Program.Character.Spellcasting.ThreeTotal)
                {
                    oUsed3.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oUsed4_DoubleClick()
        /// =========================================
        private void oUsed4_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.FourUsed + 1) <= Program.Character.Spellcasting.FourTotal)
            {
                Program.Character.Spellcasting.FourUsed++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.Spellcasting.FourUsed == Program.Character.Spellcasting.FourTotal)
                {
                    oUsed4.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oUsed5_DoubleClick()
        /// =========================================
        private void oUsed5_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.FiveUsed + 1) <= Program.Character.Spellcasting.FiveTotal)
            {
                Program.Character.Spellcasting.FiveUsed++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.Spellcasting.FiveUsed == Program.Character.Spellcasting.FiveTotal)
                {
                    oUsed5.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oUsed6_DoubleClick()
        /// =========================================
        private void oUsed6_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.SixUsed + 1) <= Program.Character.Spellcasting.SixTotal)
            {
                Program.Character.Spellcasting.SixUsed++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.Spellcasting.SixUsed == Program.Character.Spellcasting.SixTotal)
                {
                    oUsed6.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oUsed7_DoubleClick()
        /// =========================================
        private void oUsed7_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.SevenUsed + 1) <= Program.Character.Spellcasting.SevenTotal)
            {
                Program.Character.Spellcasting.SevenUsed++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.Spellcasting.SevenUsed == Program.Character.Spellcasting.SevenTotal)
                {
                    oUsed7.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oUsed8_DoubleClick()
        /// =========================================
        private void oUsed8_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.EightUsed + 1) <= Program.Character.Spellcasting.EightTotal)
            {
                Program.Character.Spellcasting.EightUsed++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.Spellcasting.EightUsed == Program.Character.Spellcasting.EightTotal)
                {
                    oUsed8.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oUsed9_DoubleClick()
        /// =========================================
        private void oUsed9_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.NineUsed + 1) <= Program.Character.Spellcasting.NineTotal)
            {
                Program.Character.Spellcasting.NineUsed++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.Spellcasting.NineUsed == Program.Character.Spellcasting.NineTotal)
                {
                    oUsed9.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oUsedPact_MouseEnter()
        /// =========================================
        private void oUsedPact_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.PactUsed + 1) <= Program.Character.Spellcasting.PactTotal)
            {
                oUsedPact.Cursor = Cursors.Hand;
            }
            else
            {
                oUsedPact.Cursor = Cursors.Default;
            }
        }

        /// =========================================
        /// oUsed1_MouseEnter()
        /// =========================================
        private void oUsed1_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.OneUsed + 1) <= Program.Character.Spellcasting.OneTotal)
            {
                oUsed1.Cursor = Cursors.Hand;
            }
            else
            {
                oUsed1.Cursor = Cursors.Default;
            }
        }

        /// =========================================
        /// oUsed2_MouseEnter()
        /// =========================================
        private void oUsed2_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.TwoUsed + 1) <= Program.Character.Spellcasting.TwoTotal)
            {
                oUsed2.Cursor = Cursors.Hand;
            }
            else
            {
                oUsed2.Cursor = Cursors.Default;
            }
        }

        /// =========================================
        /// oUsed3_MouseEnter()
        /// =========================================
        private void oUsed3_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.ThreeUsed + 1) <= Program.Character.Spellcasting.ThreeTotal)
            {
                oUsed3.Cursor = Cursors.Hand;
            }
            else
            {
                oUsed3.Cursor = Cursors.Default;
            }
        }

        /// =========================================
        /// oUsed4_MouseEnter()
        /// =========================================
        private void oUsed4_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.FourUsed + 1) <= Program.Character.Spellcasting.FourTotal)
            {
                oUsed4.Cursor = Cursors.Hand;
            }
            else
            {
                oUsed4.Cursor = Cursors.Default;
            }
        }

        /// =========================================
        /// oUsed5_MouseEnter()
        /// =========================================
        private void oUsed5_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.FiveUsed + 1) <= Program.Character.Spellcasting.FiveTotal)
            {
                oUsed5.Cursor = Cursors.Hand;
            }
            else
            {
                oUsed5.Cursor = Cursors.Default;
            }
        }

        /// =========================================
        /// oUsed6_MouseEnter()
        /// =========================================
        private void oUsed6_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.SixUsed + 1) <= Program.Character.Spellcasting.SixTotal)
            {
                oUsed6.Cursor = Cursors.Hand;
            }
            else
            {
                oUsed6.Cursor = Cursors.Default;
            }
        }

        /// =========================================
        /// oUsed7_MouseEnter()
        /// =========================================
        private void oUsed7_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.SevenUsed + 1) <= Program.Character.Spellcasting.SevenTotal)
            {
                oUsed7.Cursor = Cursors.Hand;
            }
            else
            {
                oUsed7.Cursor = Cursors.Default;
            }
        }

        /// =========================================
        /// oUsed8_MouseEnter()
        /// =========================================
        private void oUsed8_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.EightUsed + 1) <= Program.Character.Spellcasting.EightTotal)
            {
                oUsed8.Cursor = Cursors.Hand;
            }
            else
            {
                oUsed8.Cursor = Cursors.Default;
            }
        }

        /// =========================================
        /// oUsed9_MouseEnter()
        /// =========================================
        private void oUsed9_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.Spellcasting.NineUsed + 1) <= Program.Character.Spellcasting.NineTotal)
            {
                oUsed9.Cursor = Cursors.Hand;
            }
            else
            {
                oUsed9.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region SpellList Events

        /// =========================================
        /// oSpellListDataView_Sorted()
        /// =========================================
        private void oSpellListDataView_Sorted(object sender, EventArgs e)
        {
            int index;
            string rowID;
            Spell item;

            // Sort each item
            for (int i = 0; i < oSpellListDataView.Rows.Count; i++)
            {
                rowID = oSpellListDataView.Rows[i].Tag as string;

                // Check if already in correct position 
                if (!rowID.Equals(Program.Character.Spellcasting.oSpells[i].ID))
                {
                    index = Program.Character.Spellcasting.GetSpellIndex(rowID);
                    item = Program.Character.Spellcasting.oSpells[index];

                    Program.Character.Spellcasting.oSpells.RemoveAt(index);
                    Program.Character.Spellcasting.oSpells.Insert(index, Program.Character.Spellcasting.oSpells[i]);

                    Program.Character.Spellcasting.oSpells.RemoveAt(i);
                    Program.Character.Spellcasting.oSpells.Insert(i, item);
                }
            }
        }

        /// =========================================
        /// oSpellListDataView_CellMouseClick()
        /// =========================================
        private void oSpellListDataView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < oSpellListDataView.RowCount)
            {
                Rectangle rect = oSpellListDataView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Row = e.RowIndex;
                oSpellListContextMenu.Show(oSpellListDataView, new Point(rect.X + e.X + OFFSET, rect.Y + e.Y + OFFSET));
            }
        }

        /// =========================================
        /// oSpellListDeleteRow_Click()
        /// =========================================
        private void oSpellListDeleteRow_Click(object sender, EventArgs e)
        {
            Program.Modified = true;

            Program.Character.Spellcasting.RemoveSpellItem(oSpellListDataView.Rows[Row].Tag as string);
            oSpellListDataView.Rows.RemoveAt(Row);
        }

        /// =========================================
        /// editSpellToolStripMenuItem_Click()
        /// =========================================
        private void editSpellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Modified = true;

            Program.MainForm.oTablePage.ShowPane(Tables.Spells, Program.Character.Spellcasting.oSpells[Row]);
            FillSpellList();
        }

        /// =========================================
        /// editSpellToolStripMenuItem_MouseEnter()
        /// =========================================
        private void editSpellToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            editSpellToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// editSpellToolStripMenuItem_MouseLeave()
        /// =========================================
        private void editSpellToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            editSpellToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// oSpellListDeleteRow_MouseEnter()
        /// =========================================
        private void oSpellListDeleteRow_MouseEnter(object sender, EventArgs e)
        {
            oSpellListDeleteRow.ForeColor = Color.Black;
        }

        /// =========================================
        /// oSpellListDeleteRow_MouseLeave()
        /// =========================================
        private void oSpellListDeleteRow_MouseLeave(object sender, EventArgs e)
        {
            oSpellListDeleteRow.ForeColor = Color.White;
        }

        /// =========================================
        /// oSpellListDataView_MouseMove()
        /// =========================================
        private void oSpellListDataView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffects = oSpellListDataView.DoDragDrop(oSpellListDataView.Rows[rowIndexFromMouseDown], DragDropEffects.Move);
                }
            }
        }

        /// =========================================
        /// oSpellListDataView_MouseDown()
        /// =========================================
        private void oSpellListDataView_MouseDown(object sender, MouseEventArgs e)
        {
            rowIndexFromMouseDown = oSpellListDataView.HitTest(e.X, e.Y).RowIndex;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (rowIndexFromMouseDown != -1)
                    {
                        Size dragSize = SystemInformation.DragSize;
                        dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
                    }
                    else
                    {
                        dragBoxFromMouseDown = Rectangle.Empty;
                    }
                    break;
                case MouseButtons.Right:
                    if (rowIndexFromMouseDown == -1)
                        oAddSpellContextMenu.Show(oSpellListDataView, new Point(e.X + OFFSET, e.Y + OFFSET));
                    break;
            }
        }

        /// =========================================
        /// oSpellListDataView_DragOver()
        /// =========================================
        private void oSpellListDataView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// =========================================
        /// oSpellListDataView_DragDrop()
        /// =========================================
        private void oSpellListDataView_DragDrop(object sender, DragEventArgs e)
        {
            Spell item;

            if (oSpellListDataView.Rows.Count > 1)
            {
                Point clientPoint = oSpellListDataView.PointToClient(new Point(e.X, e.Y));

                rowIndexOfItemUnderMouseToDrop = oSpellListDataView.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                if (e.Effect == DragDropEffects.Move)
                {

                    DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                    //set as last row
                    if (rowIndexOfItemUnderMouseToDrop < 0 || rowIndexOfItemUnderMouseToDrop >= oSpellListDataView.Rows.Count)
                        rowIndexOfItemUnderMouseToDrop = oSpellListDataView.Rows.Count - 1;

                    if (rowIndexFromMouseDown != rowIndexOfItemUnderMouseToDrop)
                        Program.Modified = true;

                    // Move list item
                    oSpellListDataView.Rows.RemoveAt(rowIndexFromMouseDown);
                    oSpellListDataView.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);

                    // Move data item
                    item = Program.Character.Spellcasting.oSpells[rowIndexFromMouseDown];
                    Program.Character.Spellcasting.oSpells.RemoveAt(rowIndexFromMouseDown);
                    Program.Character.Spellcasting.oSpells.Insert(rowIndexOfItemUnderMouseToDrop, item);
                }
            }
        }

        /// =========================================
        /// addSpellToolStripMenuItem_Click()
        /// =========================================
        private void addSpellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.MainForm.oTablePage.ShowPane(Tables.Spells);
            FillSpellList();
        }

        /// =========================================
        /// addSpellToolStripMenuItem_MouseEnter()
        /// =========================================
        private void addSpellToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            addSpellToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// addSpellToolStripMenuItem_MouseLeave()
        /// =========================================
        private void addSpellToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            addSpellToolStripMenuItem.ForeColor = Color.White;
        }

        #endregion

    }
}
