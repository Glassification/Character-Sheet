using MyCharacterSheet.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
            fillSizes();
            formatContextMenus();

            //Format datagrid
            oAbilitiesGridView.Columns[Names.Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            oAbilitiesGridView.Columns[Notes.Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            oInventoryGrid.Columns[Equipment.Index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        #endregion

        #region Methods

        /// =========================================
        /// fillSizes()
        /// =========================================
        private void fillSizes()
        {
            foreach (Label l in oSecondaryLabels)
            {
                oSecondaryLabelSizes.Add(l.Font.Size);
            }

            AbilityHeaderSize = oAbilitiesGridView.ColumnHeadersDefaultCellStyle.Font.Size;
            InventoryHeaderSize = oInventoryGrid.ColumnHeadersDefaultCellStyle.Font.Size;
            NotesHeaderSize = oNotesGridView.ColumnHeadersDefaultCellStyle.Font.Size;

            AbilityRowSize = oAbilitiesGridView.DefaultCellStyle.Font.Size;
            InventoryRowSize = oInventoryGrid.DefaultCellStyle.Font.Size;
            NotesRowSize = oNotesGridView.DefaultCellStyle.Font.Size;
        }

        /// =========================================
        /// formatContextMenus()
        /// =========================================
        private void formatContextMenus()
        {
            oAbilitiesContextMenu.BackColor = Constants.DarkGrey;
            oAbilitiesContextMenu.ForeColor = Color.White;

            oNotesContextMenu.BackColor = Constants.DarkGrey;
            oNotesContextMenu.ForeColor = Color.White;

            oInventoryContextMenu.BackColor = Constants.DarkGrey;
            oInventoryContextMenu.ForeColor = Color.White;
        }

        /// =========================================
        /// ResizeText()
        /// =========================================
        public void ResizeText(float mod, float ratio)
        {
            for (int i = 0; i < oSecondaryLabels.Count; i++)
            {
                oSecondaryLabels[i].Font = new Font(oSecondaryLabels[i].Font.FontFamily, oSecondaryLabelSizes[i] * (ratio == 1 ? ratio : (ratio / mod)), oSecondaryLabels[i].Font.Style);
            }

            oAbilitiesGridView.ColumnHeadersDefaultCellStyle.Font = new Font(oAbilitiesGridView.ColumnHeadersDefaultCellStyle.Font.FontFamily, AbilityHeaderSize * (ratio == 1 ? ratio : (ratio / mod)),   oAbilitiesGridView.ColumnHeadersDefaultCellStyle.Font.Style);
            oInventoryGrid.ColumnHeadersDefaultCellStyle.Font     = new Font(oInventoryGrid.ColumnHeadersDefaultCellStyle.Font.FontFamily,     InventoryHeaderSize * (ratio == 1 ? ratio : (ratio / mod)), oInventoryGrid.ColumnHeadersDefaultCellStyle.Font.Style);
            oNotesGridView.ColumnHeadersDefaultCellStyle.Font     = new Font(oNotesGridView.ColumnHeadersDefaultCellStyle.Font.FontFamily,     NotesHeaderSize * (ratio == 1 ? ratio : (ratio / mod)),     oNotesGridView.ColumnHeadersDefaultCellStyle.Font.Style);

            oAbilitiesGridView.DefaultCellStyle.Font = new Font(oAbilitiesGridView.DefaultCellStyle.Font.FontFamily, AbilityRowSize * (ratio == 1 ? ratio : (ratio / mod)),   oAbilitiesGridView.Font.Style);
            oInventoryGrid.DefaultCellStyle.Font     = new Font(oInventoryGrid.DefaultCellStyle.Font.FontFamily,     InventoryRowSize * (ratio == 1 ? ratio : (ratio / mod)), oInventoryGrid.Font.Style);
            oNotesGridView.DefaultCellStyle.Font     = new Font(oNotesGridView.DefaultCellStyle.Font.FontFamily,     NotesRowSize * (ratio == 1 ? ratio : (ratio / mod)),     oNotesGridView.Font.Style);
        }

        /// =========================================
        /// Fill()
        /// =========================================
        public void FillAbility()
        {
            string[] tokens;
            oAbilitiesGridView.Rows.Clear();

            foreach (string ability in Program.Character.oAbility)
            {
                int index = oAbilitiesGridView.Rows.Add();
                DataGridViewRow row = oAbilitiesGridView.Rows[index];
                tokens = ability.Split(Constants.DELIMITER);

                row.Cells[Names.Index].Value = tokens[0];
                row.Cells[Level.Index].Value = tokens[1];
                row.Cells[Uses.Index].Value = tokens[2];
                row.Cells[Recovery.Index].Value = tokens[3];
                row.Cells[ActionType.Index].Value = tokens[4];
                row.Cells[Notes.Index].Value = tokens[5];
            }
        }

        /// =========================================
        /// fillAbility()
        /// =========================================
        public void WriteAbility(List<string> list)
        {
            string str;

            foreach (DataGridViewRow row in oAbilitiesGridView.Rows)
            {
                str = (string)row.Cells[Names.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[Level.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[Uses.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[Recovery.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[ActionType.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[Notes.Index].Value;

                list.Add(str);
            }
            list.RemoveAt(list.Count - 1);
        }

        /// =========================================
        /// FillNotes()
        /// =========================================
        public void FillNotes()
        {
            oNotesGridView.Rows.Clear();

            foreach (string str in Program.Character.oNotes)
            {
                int index = oNotesGridView.Rows.Add();
                DataGridViewRow row = oNotesGridView.Rows[index];

                row.Cells[Note.Index].Value = str;
            }
        }

        /// =========================================
        /// WriteNotes()
        /// =========================================
        public void WriteNotes(List<string> list)
        {
            string str;

            foreach (DataGridViewRow row in oNotesGridView.Rows)
            {
                str = row.Cells[Note.Index].Value + "";

                list.Add(str);
            }
            list.RemoveAt(list.Count - 1);
        }

        /// =========================================
        /// FillInventory()
        /// =========================================
        public void FillInventory()
        {
            string[] tokens;
            oInventoryGrid.Rows.Clear();

            foreach (string inventory in Program.Character.oInventory)
            {
                int index = oInventoryGrid.Rows.Add();
                DataGridViewRow row = oInventoryGrid.Rows[index];
                tokens = inventory.Split(Constants.DELIMITER);

                row.Cells[Equipment.Index].Value = tokens[0];
                row.Cells[Qty.Index].Value = tokens[1];
                row.Cells[Wgt.Index].Value = tokens[2];
            }
        }

        /// =========================================
        /// WriteInventory()
        /// =========================================
        public void WriteInventory(List<string> list)
        {
            string str;

            foreach (DataGridViewRow row in oInventoryGrid.Rows)
            {
                str = (string)row.Cells[Equipment.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[Qty.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[Wgt.Index].Value;

                list.Add(str);
            }
            list.RemoveAt(list.Count - 1);
        }

        /// =========================================
        /// formatCarryWeight()
        /// =========================================
        private void formatCarryWeight()
        {
            //Light weight
            if (Weight <= Program.Character.Light)
            {
                oWeightCarried.BackColor = Constants.LightGreen;
                oWeightCarried.ForeColor = Color.Black;
            }
            //Medium weight
            else if (Weight > Program.Character.Light && Weight <= Program.Character.Medium)
            {
                oWeightCarried.BackColor = Constants.LightYellow;
                oWeightCarried.ForeColor = Constants.MediumRed;
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

        /// =========================================
        /// AbilityGridView()
        /// =========================================
        public Control NoteGridView()
        {
            return oNotesGridView;
        }

        #endregion

        #region Accessors

        private int Row
        {
            get;
            set;
        }

        public double Weight
        {
            get
            {
                double weight = 0, dVal;
                int iVal;
                bool wgt, qty;

                foreach (DataGridViewRow row in oInventoryGrid.Rows)
                {
                    wgt = double.TryParse((string)row.Cells[Wgt.Index].Value, out dVal);
                    qty = int.TryParse((string)row.Cells[Qty.Index].Value, out iVal);

                    if (wgt && qty)
                    {
                        weight += (dVal * iVal);
                    }
                }

                return weight;
            }
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

        private float NotesHeaderSize
        {
            get;
            set;
        }

        private float NotesRowSize
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

                formatCarryWeight();
                oWeightCarried.Text = Weight + "";
                oLightLimit.Text = Program.Character.Light + "";
                oMediumLimit.Text = Program.Character.Medium + "";
                oHeavyLimit.Text = Program.Character.Heavy + "";

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
        /// oAbilitiesGridView_CellEnter()
        /// =========================================
        private void oAbilitiesGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oAbilitiesGridView_CellLeave()
        /// =========================================
        private void oAbilitiesGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Program.Typing = false;
        }

        /// =========================================
        /// oAbilitiesGridView_CellMouseClick()
        /// =========================================
        private void oAbilitiesGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < oAbilitiesGridView.RowCount - 1)
            {
                Rectangle rect = oAbilitiesGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Row = e.RowIndex;
                oAbilitiesContextMenu.Show(oAbilitiesGridView, new Point(rect.X + e.X + Constants.OFFSET, rect.Y + e.Y + Constants.OFFSET));
            }
        }

        /// =========================================
        /// oAbilityDeleteRow_Click()
        /// =========================================
        private void oAbilityDeleteRow_Click(object sender, EventArgs e)
        {
            Program.Modified = true;
            oAbilitiesGridView.Rows.RemoveAt(Row);
        }

        /// =========================================
        /// oAbilitiesGridView_CellValueChanged()
        /// =========================================
        private void oAbilitiesGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!Drawing)
            {
                Program.Modified = true;
            }
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
        /// oAbilitiesContextMenu_MouseEnter()
        /// =========================================
        private void oAbilitiesContextMenu_MouseEnter(object sender, EventArgs e)
        {
            oAbilitiesContextMenu.ForeColor = Color.Black;
        }

        /// =========================================
        /// oAbilitiesContextMenu_MouseLeave()
        /// =========================================
        private void oAbilitiesContextMenu_MouseLeave(object sender, EventArgs e)
        {
            oAbilitiesContextMenu.ForeColor = Color.White;
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

            if (rowIndexFromMouseDown != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
            {
                dragBoxFromMouseDown = Rectangle.Empty;
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
            if (oAbilitiesGridView.Rows.Count > 1)
            {
                Point clientPoint = oAbilitiesGridView.PointToClient(new Point(e.X, e.Y));

                rowIndexOfItemUnderMouseToDrop = oAbilitiesGridView.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                if (e.Effect == DragDropEffects.Move)
                {

                    DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                    //set as last row
                    if (rowIndexOfItemUnderMouseToDrop < 0 || rowIndexOfItemUnderMouseToDrop >= oAbilitiesGridView.Rows.Count - 1)
                        rowIndexOfItemUnderMouseToDrop = oAbilitiesGridView.Rows.Count - 2;

                    if (rowIndexFromMouseDown != rowIndexOfItemUnderMouseToDrop)
                        Program.Modified = true;

                    oAbilitiesGridView.Rows.RemoveAt(rowIndexFromMouseDown);
                    oAbilitiesGridView.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);
                }
            }
        }

        #endregion

        #region Notes Events

        /// =========================================
        /// oNotesGridView_CellEnter()
        /// =========================================
        private void oNotesGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oNotesGridView_CellLeave()
        /// =========================================
        private void oNotesGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Program.Typing = false;
        }

        /// =========================================
        /// oNotesGridView_CellMouseClick()
        /// =========================================
        private void oNotesGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < oNotesGridView.RowCount - 1)
            {
                Rectangle rect = oNotesGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Row = e.RowIndex;
                oNotesContextMenu.Show(oNotesGridView, new Point(rect.X + e.X + Constants.OFFSET, rect.Y + e.Y + Constants.OFFSET));
            }
        }

        /// =========================================
        /// oNoteDeleteRow_Click()
        /// =========================================
        private void oNoteDeleteRow_Click(object sender, EventArgs e)
        {
            Program.Modified = true;
            oNotesGridView.Rows.RemoveAt(Row);
        }

        /// =========================================
        /// oNotesGridView_RowsAdded()
        /// =========================================
        private void oNotesGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!Drawing)
            {
                Program.Modified = true;
            }
        }

        /// =========================================
        /// oNotesGridView_CellValueChanged()
        /// =========================================
        private void oNotesGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!Drawing)
            {
                Program.Modified = true;
            }
        }

        /// =========================================
        /// oNotesContextMenu_MouseEnter()
        /// =========================================
        private void oNotesContextMenu_MouseEnter(object sender, EventArgs e)
        {
            oNotesContextMenu.ForeColor = Color.Black;
        }

        /// =========================================
        /// oNotesContextMenu_MouseLeave()
        /// =========================================
        private void oNotesContextMenu_MouseLeave(object sender, EventArgs e)
        {
            oNotesContextMenu.ForeColor = Color.White;
        }

        /// =========================================
        /// oNotesGridView_MouseMove()
        /// =========================================
        private void oNotesGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffects = oNotesGridView.DoDragDrop(oNotesGridView.Rows[rowIndexFromMouseDown], DragDropEffects.Move);
                }
            }
        }

        /// =========================================
        /// oNotesGridView_MouseDown()
        /// =========================================
        private void oNotesGridView_MouseDown(object sender, MouseEventArgs e)
        {
            rowIndexFromMouseDown = oNotesGridView.HitTest(e.X, e.Y).RowIndex;

            if (rowIndexFromMouseDown != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
            {
                dragBoxFromMouseDown = Rectangle.Empty;
            }
        }

        /// =========================================
        /// oNotesGridView_DragOver()
        /// =========================================
        private void oNotesGridView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// =========================================
        /// oNotesGridView_DragDrop()
        /// =========================================
        private void oNotesGridView_DragDrop(object sender, DragEventArgs e)
        {
            if (oNotesGridView.Rows.Count > 1)
            {
                Point clientPoint = oNotesGridView.PointToClient(new Point(e.X, e.Y));

                rowIndexOfItemUnderMouseToDrop = oNotesGridView.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                if (e.Effect == DragDropEffects.Move)
                {

                    DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                    //set as last row
                    if (rowIndexOfItemUnderMouseToDrop < 0 || rowIndexOfItemUnderMouseToDrop >= oNotesGridView.Rows.Count - 1)
                        rowIndexOfItemUnderMouseToDrop = oNotesGridView.Rows.Count - 2;

                    if (rowIndexFromMouseDown != rowIndexOfItemUnderMouseToDrop)
                        Program.Modified = true;

                    oNotesGridView.Rows.RemoveAt(rowIndexFromMouseDown);
                    oNotesGridView.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);
                }
            }
        }

        #endregion

        #region Inventory Events

        /// =========================================
        /// oInventoryGrid_CellEnter()
        /// =========================================
        private void oInventoryGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oInventoryGrid_CellLeave()
        /// =========================================
        private void oInventoryGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Program.Typing = false;
        }

        /// =========================================
        /// oInventoryGrid_CellMouseClick()
        /// =========================================
        private void oInventoryGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < oInventoryGrid.RowCount - 1)
            {
                Rectangle rect = oInventoryGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Row = e.RowIndex;
                oInventoryContextMenu.Show(oInventoryGrid, new Point(rect.X + e.X + Constants.OFFSET, rect.Y + e.Y + Constants.OFFSET));
            }
        }

        /// =========================================
        /// oInventoryDeleteRow_Click()
        /// =========================================
        private void oInventoryDeleteRow_Click(object sender, EventArgs e)
        {
            Program.Modified = true;
            oInventoryGrid.Rows.RemoveAt(Row);
        }

        /// =========================================
        /// oInventoryGrid_CellValueChanged()
        /// =========================================
        private void oInventoryGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double wgt;
            int qty;

            if (!Drawing)
            {
                Program.Modified = true;
            }

            if (!Program.Loading)
            {
                if (e.ColumnIndex == Wgt.Index)
                {
                    if (!double.TryParse(oInventoryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "", out wgt))
                    {
                        oInventoryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = WgtValue;
                    }
                }
                else if (e.ColumnIndex == Qty.Index)
                {
                    if (!int.TryParse(oInventoryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "", out qty))
                    {
                        oInventoryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = QtyValue;
                    }
                }
                Invalidate();
            }
        }

        /// =========================================
        /// oInventoryGrid_CellBeginEdit()
        /// =========================================
        private void oInventoryGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == Wgt.Index)
            {
                WgtValue = oInventoryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "";
            }
            else if (e.ColumnIndex == Qty.Index)
            {
                QtyValue = oInventoryGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "";
            }
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
        /// oInventoryContextMenu_MouseEnter()
        /// =========================================
        private void oInventoryContextMenu_MouseEnter(object sender, EventArgs e)
        {
            oInventoryContextMenu.ForeColor = Color.Black;
        }

        /// =========================================
        /// oInventoryContextMenu_MouseLeave()
        /// =========================================
        private void oInventoryContextMenu_MouseLeave(object sender, EventArgs e)
        {
            oInventoryContextMenu.ForeColor = Color.White;
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

            if (rowIndexFromMouseDown != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
            {
                dragBoxFromMouseDown = Rectangle.Empty;
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
            if (oInventoryGrid.Rows.Count > 1)
            {
                Point clientPoint = oInventoryGrid.PointToClient(new Point(e.X, e.Y));

                rowIndexOfItemUnderMouseToDrop = oInventoryGrid.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                if (e.Effect == DragDropEffects.Move)
                {

                    DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                    //set as last row
                    if (rowIndexOfItemUnderMouseToDrop < 0 || rowIndexOfItemUnderMouseToDrop >= oInventoryGrid.Rows.Count - 1)
                        rowIndexOfItemUnderMouseToDrop = oInventoryGrid.Rows.Count - 2;

                    if (rowIndexFromMouseDown != rowIndexOfItemUnderMouseToDrop)
                        Program.Modified = true;

                    oInventoryGrid.Rows.RemoveAt(rowIndexFromMouseDown);
                    oInventoryGrid.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);
                }
            }
        }

        #endregion

    }
}
