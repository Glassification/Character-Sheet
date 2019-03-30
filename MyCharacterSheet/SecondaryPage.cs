using MyCharacterSheet.Lists;
using MyCharacterSheet.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static MyCharacterSheet.Utility.Constants;

namespace MyCharacterSheet
{
    public partial class SecondaryPage : UserControl
    {

        #region Members

        private List<float> oSecondaryLabelSizes = new List<float>();
        private List<Label> oSecondaryLabels     = new List<Label>();

        private Rectangle   dragBoxFromMouseDown;
        private int         rowIndexFromMouseDown;
        private int         rowIndexOfItemUnderMouseToDrop;

        #endregion

        #region Constructor

        public SecondaryPage()
        {
            //Initialize components
            InitializeComponent();
            Drawing = false;
            
            //Set initial state
            FillLabelList();
            FillSizes();
            FormatContextMenus();
            FormatInputBoxes();

            //Format datagrid
            oAbilitiesGridView.Columns[Names.Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            oAbilitiesGridView.Columns[Notes.Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            oInventoryGrid.Columns[Equipment.Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        #endregion

        #region Methods

        /// =========================================
        /// FillSizes()
        /// =========================================
        private void FillSizes()
        {
            foreach (Label l in oSecondaryLabels)
            {
                oSecondaryLabelSizes.Add(l.Font.Size);
            }

            AbilityHeaderSize = oAbilitiesGridView.ColumnHeadersDefaultCellStyle.Font.Size;
            InventoryHeaderSize = oInventoryGrid.ColumnHeadersDefaultCellStyle.Font.Size;

            AbilityRowSize = oAbilitiesGridView.DefaultCellStyle.Font.Size;
            InventoryRowSize = oInventoryGrid.DefaultCellStyle.Font.Size;

            CurrencySize = oInputGP.Font.Size;
        }

        /// =========================================
        /// FormatCurrencyInput()
        /// =========================================
        public void FormatInputBoxes()
        {
            oInputCP.Width = oPanelCP.Width;
            oInputSP.Width = oPanelSP.Width;
            oInputEP.Width = oPanelEP.Width;
            oInputGP.Width = oPanelGP.Width;
            oInputPP.Width = oPanelPP.Width;

            oInputCP.Location = new Point(0, (oPanelCP.Height / 2) - (oInputCP.Height / 2));
            oInputSP.Location = new Point(0, (oPanelSP.Height / 2) - (oInputSP.Height / 2));
            oInputEP.Location = new Point(0, (oPanelEP.Height / 2) - (oInputEP.Height / 2));
            oInputGP.Location = new Point(0, (oPanelGP.Height / 2) - (oInputGP.Height / 2));
            oInputPP.Location = new Point(0, (oPanelPP.Height / 2) - (oInputPP.Height / 2));
        }

        /// =========================================
        /// FormatContextMenus()
        /// =========================================
        private void FormatContextMenus()
        {
            oAbilitiesContextMenu.BackColor = DarkGrey;
            oAbilitiesContextMenu.ForeColor = Color.White;

            oAddAbilityContextMenu.BackColor = DarkGrey;
            oAddAbilityContextMenu.ForeColor = Color.White;

            oInventoryContextMenu.BackColor = DarkGrey;
            oInventoryContextMenu.ForeColor = Color.White;

            oAddInventoryContextMenu.BackColor = DarkGrey;
            oAddInventoryContextMenu.ForeColor = Color.White;
        }

        /// =========================================
        /// ResizeLabels()
        /// =========================================
        public void ResizeLabels()
        {
            float ratio = Program.MainForm.Ratio;

            for (int i = 0; i < oSecondaryLabels.Count; i++)
            {
                oSecondaryLabels[i].Font = new Font(oSecondaryLabels[i].Font.FontFamily, oSecondaryLabelSizes[i] * (ratio == 1 ? ratio : (ratio / SIZE_MOD)), oSecondaryLabels[i].Font.Style);

                ScaleFont(oSecondaryLabels[i]);
            }
        }

        /// =========================================
        /// ResizeText()
        /// =========================================
        public void ResizeText()
        {
            float ratio = Program.MainForm.Ratio;

            FormatInputBoxes();
            ResizeLabels();

            //Resize Currency Input
            oInputCP.Font = new Font(oInputCP.Font.FontFamily, CurrencySize * (ratio == 1 ? ratio : (ratio / SIZE_MOD)), oInputCP.Font.Style);
            oInputSP.Font = new Font(oInputCP.Font.FontFamily, CurrencySize * (ratio == 1 ? ratio : (ratio / SIZE_MOD)), oInputCP.Font.Style);
            oInputEP.Font = new Font(oInputCP.Font.FontFamily, CurrencySize * (ratio == 1 ? ratio : (ratio / SIZE_MOD)), oInputCP.Font.Style);
            oInputGP.Font = new Font(oInputCP.Font.FontFamily, CurrencySize * (ratio == 1 ? ratio : (ratio / SIZE_MOD)), oInputCP.Font.Style);
            oInputPP.Font = new Font(oInputCP.Font.FontFamily, CurrencySize * (ratio == 1 ? ratio : (ratio / SIZE_MOD)), oInputCP.Font.Style);

            oAbilitiesGridView.ColumnHeadersDefaultCellStyle.Font = new Font(oAbilitiesGridView.ColumnHeadersDefaultCellStyle.Font.FontFamily, AbilityHeaderSize * (ratio == 1 ? ratio : (ratio / SIZE_MOD)),   oAbilitiesGridView.ColumnHeadersDefaultCellStyle.Font.Style);
            oInventoryGrid.ColumnHeadersDefaultCellStyle.Font     = new Font(oInventoryGrid.ColumnHeadersDefaultCellStyle.Font.FontFamily,     InventoryHeaderSize * (ratio == 1 ? ratio : (ratio / SIZE_MOD)), oInventoryGrid.ColumnHeadersDefaultCellStyle.Font.Style);

            oAbilitiesGridView.DefaultCellStyle.Font = new Font(oAbilitiesGridView.DefaultCellStyle.Font.FontFamily, AbilityRowSize * (ratio == 1 ? ratio : (ratio / SIZE_MOD)),   oAbilitiesGridView.Font.Style);
            oInventoryGrid.DefaultCellStyle.Font     = new Font(oInventoryGrid.DefaultCellStyle.Font.FontFamily,     InventoryRowSize * (ratio == 1 ? ratio : (ratio / SIZE_MOD)), oInventoryGrid.Font.Style);
        }

        /// =========================================
        /// TrimLeadingZero()
        /// =========================================
        private string TrimLeadingZero(string text)
        {
            text = text.TrimStart(new char[] { '0' });

            if (text.Equals(""))
            {
                text = "0";
            }

            return text;
        }

        /// =========================================
        /// Fill()
        /// =========================================
        public void FillAbility()
        {
            oAbilitiesGridView.Rows.Clear();

            foreach (Ability ability in Program.Character.oAbility)
            {
                int index = oAbilitiesGridView.Rows.Add();
                DataGridViewRow row = oAbilitiesGridView.Rows[index];

                row.Cells[Names.Index].Value = ability.Name;
                row.Cells[Level.Index].Value = ability.Level;
                row.Cells[Uses.Index].Value = ability.Uses;
                row.Cells[Recovery.Index].Value = ability.Recovery;
                row.Cells[ActionType.Index].Value = ability.Action;
                row.Cells[Notes.Index].Value = ability.Note;

                row.Tag = ability.ID;
            }
        }

        /// =========================================
        /// FillInventory()
        /// =========================================
        public void FillInventory()
        {
            oInventoryGrid.Rows.Clear();

            foreach (Inventory inventory in Program.Character.oInventory)
            {
                int index = oInventoryGrid.Rows.Add();
                DataGridViewRow row = oInventoryGrid.Rows[index];

                row.Cells[Equipment.Index].Value = inventory.Name;
                row.Cells[Qty.Index].Value = inventory.Amount;
                row.Cells[Wgt.Index].Value = inventory.Weight;
                row.Cells[oNote.Index].Value = inventory.Note;

                row.Tag = inventory.ID;
            }
        }

        /// =========================================
        /// FormatCarryWeight()
        /// =========================================
        private void FormatCarryWeight()
        {
            double Weight = Program.Character.CarryWeight;
            //Light weight
            if (Weight <= Program.Character.Light)
            {
                oWeightCarried.BackColor = LightGreen;
                oWeightCarried.ForeColor = Color.Black;
            }
            //Medium weight
            else if (Weight > Program.Character.Light && Weight <= Program.Character.Medium)
            {
                oWeightCarried.BackColor = LightYellow;
                oWeightCarried.ForeColor = MediumRed;
            }
            //Heavy weight
            else if (Weight > Program.Character.Medium && Weight <= Program.Character.Heavy)
            {
                oWeightCarried.BackColor = Color.Pink;
                oWeightCarried.ForeColor = Color.DarkRed;
            }
            //Encumbered weight
            else
            {
                oWeightCarried.BackColor = Color.DarkGray;
                oWeightCarried.ForeColor = Color.Red;
            }
        }

        /// =========================================
        /// DefaultFocus()
        /// =========================================
        public void DefaultFocus()
        {
            oSecondaryFocus.Focus();
        }

        /// =========================================
        /// AbilityGridView()
        /// =========================================
        public Control AbilityGridView()
        {
            return oAbilitiesGridView;
        }

        /// =========================================
        /// InventoryGridView()
        /// =========================================
        public Control InventoryGridView()
        {
            return oInventoryGrid;
        }

        #endregion

        #region Accessors

        private int Row
        {
            get;
            set;
        }

        private string WgtValue
        {
            get;
            set;
        }

        private string QtyValue
        {
            get;
            set;
        }

        private float AbilityHeaderSize
        {
            get;
            set;
        }

        private float AbilityRowSize
        {
            get;
            set;
        }

        private float InventoryHeaderSize
        {
            get;
            set;
        }

        private float InventoryRowSize
        {
            get;
            set;
        }

        private bool Drawing
        {
            get;
            set;
        }

        private float CurrencySize
        {
            get;
            set;
        }

        #endregion

        #region Frame Events

        /// =========================================
        /// SecondaryPage_Paint()
        /// =========================================
        private void SecondaryPage_Paint(object sender, PaintEventArgs e)
        {
            if (!Program.Loading)
            {
                Drawing = true;

                oGender.Text = Program.Character.Gender;
                oAge.Text = Program.Character.Age;
                oHeight.Text = Program.Character.Height;
                oWeight.Text = Program.Character.Weight;
                oHairColour.Text = Program.Character.HairColour;
                oSkinColour.Text = Program.Character.SkinColour;
                oEyeColour.Text = Program.Character.EyeColour;
                oMarks.Text = Program.Character.Marks;
                oTrait1.Text = Program.Character.Trait1;
                oTrait2.Text = Program.Character.Trait2;
                oIdeal.Text = Program.Character.Ideal;
                oBond.Text = Program.Character.Bond;
                oFlaw.Text = Program.Character.Flaw;
                oBackgroundFeature.Text = Program.Character.PersonalityBackground;
                oNotes.Text = Program.Character.PersonalityNotes;

                oArmorProficiency.Text = Program.Character.Armor;
                oShieldProficiency.Text = Program.Character.Shields;
                oWeaponProficiency.Text = Program.Character.Weapons;
                oToolProficiency.Text = Program.Character.Tools;
                oProficiencyBonus.Text = Program.Character.ProficiencyBonus + "";

                FormatCarryWeight();
                oWeightCarried.Text = Program.Character.CarryWeight + "";
                oLightLimit.Text = Program.Character.Light + "";
                oMediumLimit.Text = Program.Character.Medium + "";
                oHeavyLimit.Text = Program.Character.Heavy + "";

                oInputCP.Text = Program.Character.CP + "";
                oInputSP.Text = Program.Character.SP + "";
                oInputEP.Text = Program.Character.EP + "";
                oInputGP.Text = Program.Character.GP + "";
                oInputPP.Text = Program.Character.PP + "";
                oGoldValue.Text = "¤ " + string.Format("{0:0.00}", Program.Character.TotalGold);

                ResizeLabels();

                Drawing = false;
            }
        }

        /// =========================================
        /// SecondaryPage_VisibleChanged()
        /// =========================================
        private void SecondaryPage_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                Drawing = true;

                Invalidate();

                Drawing = false;
            }
        }

        #endregion

        #region Ability Events

        /// =========================================
        /// oAbilitiesGridView_CellMouseClick()
        /// =========================================
        private void oAbilitiesGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < oAbilitiesGridView.RowCount)
            {
                Rectangle rect = oAbilitiesGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Row = e.RowIndex;
                oAbilitiesContextMenu.Show(oAbilitiesGridView, new Point(rect.X + e.X + OFFSET, rect.Y + e.Y + OFFSET));
            }
        }

        /// =========================================
        /// editAbilityToolStripMenuItem_Click()
        /// =========================================
        private void editAbilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Modified = true;

            Program.MainForm.oTablePage.ShowPane(Tables.Abilities, Program.Character.oAbility[Row]);
            FillAbility();
        }

        /// =========================================
        /// oAbilityDeleteRow_Click()
        /// =========================================
        private void oAbilityDeleteRow_Click(object sender, EventArgs e)
        {
            Program.Modified = true;

            Program.Character.RemoveAbilityItem((Guid)oAbilitiesGridView.Rows[Row].Tag);
            oAbilitiesGridView.Rows.RemoveAt(Row);
        }

        /// =========================================
        /// oAbilitiesGridView_RowsAdded()
        /// =========================================
        private void oAbilitiesGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!Drawing)
            {
                Program.Modified = true;
            }
        }

        /// =========================================
        /// editAbilityToolStripMenuItem_MouseEnter()
        /// =========================================
        private void editAbilityToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            editAbilityToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// editAbilityToolStripMenuItem_MouseLeave()
        /// =========================================
        private void editAbilityToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            editAbilityToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// oAbilityDeleteRow_MouseEnter()
        /// =========================================
        private void oAbilityDeleteRow_MouseEnter(object sender, EventArgs e)
        {
            oAbilityDeleteRow.ForeColor = Color.Black;
        }

        /// =========================================
        /// oAbilityDeleteRow_MouseLeave()
        /// =========================================
        private void oAbilityDeleteRow_MouseLeave(object sender, EventArgs e)
        {
            oAbilityDeleteRow.ForeColor = Color.White;
        }

        /// =========================================
        /// oAbilitiesGridView_MouseMove()
        /// =========================================
        private void oAbilitiesGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffects = oAbilitiesGridView.DoDragDrop(oAbilitiesGridView.Rows[rowIndexFromMouseDown], DragDropEffects.Move);
                }
            }
        }

        /// =========================================
        /// oAbilitiesGridView_MouseDown()
        /// =========================================
        private void oAbilitiesGridView_MouseDown(object sender, MouseEventArgs e)
        {
            rowIndexFromMouseDown = oAbilitiesGridView.HitTest(e.X, e.Y).RowIndex;

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
                        oAddAbilityContextMenu.Show(oAbilitiesGridView, new Point(e.X + OFFSET, e.Y + OFFSET));
                    break;
            }
        }

        /// =========================================
        /// oAbilitiesGridView_DragOver()
        /// =========================================
        private void oAbilitiesGridView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// =========================================
        /// oAbilitiesGridView_DragDrop()
        /// =========================================
        private void oAbilitiesGridView_DragDrop(object sender, DragEventArgs e)
        {
            Ability item;

            if (oAbilitiesGridView.Rows.Count > 1)
            {
                Point clientPoint = oAbilitiesGridView.PointToClient(new Point(e.X, e.Y));

                rowIndexOfItemUnderMouseToDrop = oAbilitiesGridView.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                if (e.Effect == DragDropEffects.Move)
                {

                    DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                    //set as last row
                    if (rowIndexOfItemUnderMouseToDrop < 0 || rowIndexOfItemUnderMouseToDrop >= oAbilitiesGridView.Rows.Count)
                        rowIndexOfItemUnderMouseToDrop = oAbilitiesGridView.Rows.Count - 1;

                    if (rowIndexFromMouseDown != rowIndexOfItemUnderMouseToDrop)
                        Program.Modified = true;

                    // Move list item
                    oAbilitiesGridView.Rows.RemoveAt(rowIndexFromMouseDown);
                    oAbilitiesGridView.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);

                    // Move data item
                    item = Program.Character.oAbility[rowIndexFromMouseDown];
                    Program.Character.oAbility.RemoveAt(rowIndexFromMouseDown);
                    Program.Character.oAbility.Insert(rowIndexOfItemUnderMouseToDrop, item);
                }
            }
        }

        /// =========================================
        /// oAbilitiesGridView_Sorted()
        /// =========================================
        private void oAbilitiesGridView_Sorted(object sender, EventArgs e)
        {
            int index;
            Guid rowID;
            Ability item;

            // Sort each item
            for (int i = 0; i < oAbilitiesGridView.Rows.Count; i++)
            {
                rowID = (Guid)oAbilitiesGridView.Rows[i].Tag;

                // Check if already in correct position 
                if (!rowID.Equals(Program.Character.oAbility[i].ID))
                {
                    index = Program.Character.GetAbilityIndex(rowID);
                    item = Program.Character.oAbility[index];

                    Program.Character.oAbility.RemoveAt(index);
                    Program.Character.oAbility.Insert(index, Program.Character.oAbility[i]);

                    Program.Character.oAbility.RemoveAt(i);
                    Program.Character.oAbility.Insert(i, item);
                }
            }
        }

        /// =========================================
        /// addAbilityToolStripMenuItem_Click()
        /// =========================================
        private void addAbilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.MainForm.oTablePage.ShowPane(Tables.Abilities);
            FillAbility();
        }

        /// =========================================
        /// addAbilityToolStripMenuItem_MouseEnter()
        /// =========================================
        private void addAbilityToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            addAbilityToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// addAbilityToolStripMenuItem_MouseLeave()
        /// =========================================
        private void addAbilityToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            addAbilityToolStripMenuItem.ForeColor = Color.White;
        }

        #endregion

        #region Inventory Events

        /// =========================================
        /// oInventoryGrid_CellMouseClick()
        /// =========================================
        private void oInventoryGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < oInventoryGrid.RowCount)
            {
                Rectangle rect = oInventoryGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Row = e.RowIndex;
                oInventoryContextMenu.Show(oInventoryGrid, new Point(rect.X + e.X + OFFSET, rect.Y + e.Y + OFFSET));
            }
        }

        /// =========================================
        /// oInventoryDeleteRow_Click()
        /// =========================================
        private void oInventoryDeleteRow_Click(object sender, EventArgs e)
        {
            Program.Modified = true;

            Program.Character.RemoveInventoryItem((Guid)oInventoryGrid.Rows[Row].Tag);
            oInventoryGrid.Rows.RemoveAt(Row);
        }

        /// =========================================
        /// editItemToolStripMenuItem_Click()
        /// =========================================
        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Modified = true;

            Program.MainForm.oTablePage.ShowPane(Tables.Inventory, Program.Character.oInventory[Row]);
            FillInventory();
        }

        /// =========================================
        /// oInventoryGrid_RowsAdded()
        /// =========================================
        private void oInventoryGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!Drawing)
            {
                Program.Modified = true;
            }
        }

        /// =========================================
        /// editItemToolStripMenuItem_MouseEnter()
        /// =========================================
        private void editItemToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            editItemToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// editItemToolStripMenuItem_MouseLeave()
        /// =========================================
        private void editItemToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            editItemToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// oInventoryDeleteRow_MouseEnter()
        /// =========================================
        private void oInventoryDeleteRow_MouseEnter(object sender, EventArgs e)
        {
            oInventoryDeleteRow.ForeColor = Color.Black;
        }

        /// =========================================
        /// oInventoryDeleteRow_MouseLeave()
        /// =========================================
        private void oInventoryDeleteRow_MouseLeave(object sender, EventArgs e)
        {
            oInventoryDeleteRow.ForeColor = Color.White;
        }

        /// =========================================
        /// oInventoryGrid_MouseMove()
        /// =========================================
        private void oInventoryGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffects = oInventoryGrid.DoDragDrop(oInventoryGrid.Rows[rowIndexFromMouseDown], DragDropEffects.Move);
                }
            }
        }

        /// =========================================
        /// oInventoryGrid_MouseDown()
        /// =========================================
        private void oInventoryGrid_MouseDown(object sender, MouseEventArgs e)
        {
            rowIndexFromMouseDown = oInventoryGrid.HitTest(e.X, e.Y).RowIndex;

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
                        oAddInventoryContextMenu.Show(oInventoryGrid, new Point(e.X + OFFSET, e.Y + OFFSET));
                    break;
            }
        }

        /// =========================================
        /// oInventoryGrid_DragOver()
        /// =========================================
        private void oInventoryGrid_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// =========================================
        /// oInventoryGrid_DragDrop()
        /// =========================================
        private void oInventoryGrid_DragDrop(object sender, DragEventArgs e)
        {
            Inventory item;

            if (oInventoryGrid.Rows.Count > 1)
            {
                Point clientPoint = oInventoryGrid.PointToClient(new Point(e.X, e.Y));

                rowIndexOfItemUnderMouseToDrop = oInventoryGrid.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                if (e.Effect == DragDropEffects.Move)
                {

                    DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                    //set as last row
                    if (rowIndexOfItemUnderMouseToDrop < 0 || rowIndexOfItemUnderMouseToDrop >= oInventoryGrid.Rows.Count)
                        rowIndexOfItemUnderMouseToDrop = oInventoryGrid.Rows.Count - 1;

                    if (rowIndexFromMouseDown != rowIndexOfItemUnderMouseToDrop)
                        Program.Modified = true;

                    // Move list item
                    oInventoryGrid.Rows.RemoveAt(rowIndexFromMouseDown);
                    oInventoryGrid.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);

                    // Move data item
                    item = Program.Character.oInventory[rowIndexFromMouseDown];
                    Program.Character.oInventory.RemoveAt(rowIndexFromMouseDown);
                    Program.Character.oInventory.Insert(rowIndexOfItemUnderMouseToDrop, item);
                }
            }
        }

        /// =========================================
        /// oInventoryGrid_Sorted()
        /// =========================================
        private void oInventoryGrid_Sorted(object sender, EventArgs e)
        {
            int index;
            Guid rowID;
            Inventory item;

            // Sort each item
            for (int i = 0; i < oInventoryGrid.Rows.Count; i++)
            {
                rowID = (Guid)oInventoryGrid.Rows[i].Tag;

                // Check if already in correct position 
                if (!rowID.Equals(Program.Character.oInventory[i].ID))
                {
                    index = Program.Character.GetInventoryIndex(rowID);
                    item = Program.Character.oInventory[index];

                    Program.Character.oInventory.RemoveAt(index);
                    Program.Character.oInventory.Insert(index, Program.Character.oInventory[i]);

                    Program.Character.oInventory.RemoveAt(i);
                    Program.Character.oInventory.Insert(i, item);
                }
            }
        }

        /// =========================================
        /// addItemToolStripMenuItem_Click()
        /// =========================================
        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.MainForm.oTablePage.ShowPane(Tables.Inventory);
            FillInventory();
        }

        /// =========================================
        /// addItemToolStripMenuItem_MouseEnter()
        /// =========================================
        private void addItemToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            addItemToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// addItemToolStripMenuItem_MouseLeave()
        /// =========================================
        private void addItemToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            addItemToolStripMenuItem.ForeColor = Color.White;
        }

        #endregion

        #region Wealth Events

        /// =========================================
        /// oPanelCP_Click()
        /// =========================================
        private void oPanelCP_Click(object sender, EventArgs e)
        {
            oInputCP.Select();
            oInputCP.SelectionStart = oInputCP.Text.Length;
            oInputCP.SelectionLength = 0;
        }

        /// =========================================
        /// oPanelSP_Click()
        /// =========================================
        private void oPanelSP_Click(object sender, EventArgs e)
        {
            oInputSP.Select();
            oInputSP.SelectionStart = oInputSP.Text.Length;
            oInputSP.SelectionLength = 0;
        }

        /// =========================================
        /// oPanelEP_Click()
        /// =========================================
        private void oPanelEP_Click(object sender, EventArgs e)
        {
            oInputEP.Select();
            oInputEP.SelectionStart = oInputEP.Text.Length;
            oInputEP.SelectionLength = 0;
        }

        /// =========================================
        /// oPanelGP_Click()
        /// =========================================
        private void oPanelGP_Click(object sender, EventArgs e)
        {
            oInputGP.Select();
            oInputGP.SelectionStart = oInputGP.Text.Length;
            oInputGP.SelectionLength = 0;
        }

        /// =========================================
        /// oPanelPP_Click()
        /// =========================================
        private void oPanelPP_Click(object sender, EventArgs e)
        {
            oInputPP.Select();
            oInputPP.SelectionStart = oInputPP.Text.Length;
            oInputPP.SelectionLength = 0;
        }

        /// =========================================
        /// oInputCP_Enter()
        /// =========================================
        private void oInputCP_Enter(object sender, EventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oInputCP_Leave()
        /// =========================================
        private void oInputCP_Leave(object sender, EventArgs e)
        {
            int cp;

            oInputCP.Text = TrimLeadingZero(oInputCP.Text);

            if (int.TryParse(oInputCP.Text, out cp))
            {
                if (!Drawing)
                {
                    Program.Modified = true;
                }

                Program.Character.CP = cp;
                oGoldValue.Text = Program.Character.TotalGold + "";
            }
            else
            {
                oInputCP.Text = Program.Character.CP + "";
            }

            Invalidate();
            Program.Typing = false;
        }

        /// =========================================
        /// oInputCP_KeyDown()
        /// =========================================
        private void oInputCP_KeyDown(object sender, KeyEventArgs e)
        {
            int cp;

            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                oInputCP.Text = TrimLeadingZero(oInputCP.Text);

                if (int.TryParse(oInputCP.Text, out cp))
                {
                    if (!Drawing)
                    {
                        Program.Modified = true;
                    }

                    Program.Character.CP = cp;
                    oGoldValue.Text = Program.Character.TotalGold + "";
                }
                else
                {
                    oInputCP.Text = Program.Character.CP + "";
                }
            }
        }

        /// =========================================
        /// oInputSP_Enter()
        /// =========================================
        private void oInputSP_Enter(object sender, EventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oInputSP_Leave()
        /// =========================================
        private void oInputSP_Leave(object sender, EventArgs e)
        {
            int sp;

            oInputSP.Text = TrimLeadingZero(oInputSP.Text);

            if (int.TryParse(oInputSP.Text, out sp))
            {
                if (!Drawing)
                {
                    Program.Modified = true;
                }

                Program.Character.SP = sp;
                oGoldValue.Text = Program.Character.TotalGold + "";
            }
            else
            {
                oInputSP.Text = Program.Character.SP + "";
            }

            Invalidate();
            Program.Typing = false;
        }

        /// =========================================
        /// oInputSP_KeyDown()
        /// =========================================
        private void oInputSP_KeyDown(object sender, KeyEventArgs e)
        {
            int sp;

            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                oInputSP.Text = TrimLeadingZero(oInputSP.Text);

                if (int.TryParse(oInputSP.Text, out sp))
                {
                    if (!Drawing)
                    {
                        Program.Modified = true;
                    }

                    Program.Character.SP = sp;
                    oGoldValue.Text = Program.Character.TotalGold + "";
                }
                else
                {
                    oInputSP.Text = Program.Character.SP + "";
                }
            }
        }

        /// =========================================
        /// oInputEP_Enter()
        /// =========================================
        private void oInputEP_Enter(object sender, EventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oInputEP_Leave()
        /// =========================================
        private void oInputEP_Leave(object sender, EventArgs e)
        {
            int ep;

            oInputEP.Text = TrimLeadingZero(oInputEP.Text);

            if (int.TryParse(oInputEP.Text, out ep))
            {
                if (!Drawing)
                {
                    Program.Modified = true;
                }

                Program.Character.EP = ep;
                oGoldValue.Text = Program.Character.TotalGold + "";
            }
            else
            {
                oInputEP.Text = Program.Character.EP + "";
            }

            Invalidate();
            Program.Typing = false;
        }

        /// =========================================
        /// oInputEP_KeyDown()
        /// =========================================
        private void oInputEP_KeyDown(object sender, KeyEventArgs e)
        {
            int ep;

            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                oInputEP.Text = TrimLeadingZero(oInputEP.Text);

                if (int.TryParse(oInputEP.Text, out ep))
                {
                    if (!Drawing)
                    {
                        Program.Modified = true;
                    }

                    Program.Character.EP = ep;
                    oGoldValue.Text = Program.Character.TotalGold + "";
                }
                else
                {
                    oInputEP.Text = Program.Character.EP + "";
                }
            }
        }

        /// =========================================
        /// oInputGP_Enter()
        /// =========================================
        private void oInputGP_Enter(object sender, EventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oInputGP_Leave()
        /// =========================================
        private void oInputGP_Leave(object sender, EventArgs e)
        {
            int gp;

            oInputGP.Text = TrimLeadingZero(oInputGP.Text);

            if (int.TryParse(oInputGP.Text, out gp))
            {
                if (!Drawing)
                {
                    Program.Modified = true;
                }

                Program.Character.GP = gp;
                oGoldValue.Text = Program.Character.TotalGold + "";
            }
            else
            {
                oInputGP.Text = Program.Character.GP + "";
            }

            Invalidate();
            Program.Typing = false;
        }

        /// =========================================
        /// oInputGP_KeyDown()
        /// =========================================
        private void oInputGP_KeyDown(object sender, KeyEventArgs e)
        {
            int gp;

            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                oInputGP.Text = TrimLeadingZero(oInputGP.Text);

                if (int.TryParse(oInputGP.Text, out gp))
                {
                    if (!Drawing)
                    {
                        Program.Modified = true;
                    }

                    Program.Character.GP = gp;
                    oGoldValue.Text = Program.Character.TotalGold + "";
                }
                else
                {
                    oInputGP.Text = Program.Character.GP + "";
                }
            }
        }

        /// =========================================
        /// oInputPP_Enter()
        /// =========================================
        private void oInputPP_Enter(object sender, EventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oInputPP_Leave()
        /// =========================================
        private void oInputPP_Leave(object sender, EventArgs e)
        {
            int pp;

            oInputPP.Text = TrimLeadingZero(oInputPP.Text);

            if (int.TryParse(oInputPP.Text, out pp))
            {
                if (!Drawing)
                {
                    Program.Modified = true;
                }

                Program.Character.PP = pp;
                oGoldValue.Text = Program.Character.TotalGold + "";
            }
            else
            {
                oInputPP.Text = Program.Character.PP + "";
            }

            Invalidate();
            Program.Typing = false;
        }

        /// =========================================
        /// oInputPP_KeyDown()
        /// =========================================
        private void oInputPP_KeyDown(object sender, KeyEventArgs e)
        {
            int pp;

            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                oInputPP.Text = TrimLeadingZero(oInputPP.Text);

                if (int.TryParse(oInputPP.Text, out pp))
                {
                    if (!Drawing)
                    {
                        Program.Modified = true;
                    }

                    Program.Character.PP = pp;
                    oGoldValue.Text = Program.Character.TotalGold + "";
                }
                else
                {
                    oInputPP.Text = Program.Character.PP + "";
                }
            }
        }


        #endregion

    }
}
