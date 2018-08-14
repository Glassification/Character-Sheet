using System;
using System.Windows.Forms;
using MyCharacterSheet.Characters;
using System.Drawing;
using MyCharacterSheet.Utility;

namespace MyCharacterSheet
{
    public partial class CampainPage : UserControl
    {

        #region Constructor

        public CampainPage()
        {
            InitializeComponent();

            formatContextMenus();

            SelectIndex = 0;
            HoverIndex = -1;
            DeleteIndex = -1;
            Deleting = false;
            Changing = false;

            OriginalHeight = oDocumentList.ItemHeight;
            OriginalListTextSize = oDocumentList.Font.Size;
            OriginalLabelSize = oCampainFocus.Font.Size;
        }

        #endregion

        #region Methods

        /// =========================================
        /// FillDocumentList()
        /// =========================================
        public void FillDocumentList()
        {
            if (Program.Character.oDocuments.Count > 0)
            {
                oDocumentList.Items.AddRange(Program.Character.oDocuments.ToArray());
                oCampainTextBox.Rtf = (oDocumentList.Items[0] as Document).Rtf;
                oDocumentList.SelectedIndex = 0;
            }
        }

        /// =========================================
        /// ClearDocumentList()
        /// =========================================
        public void ClearDocumentList()
        {
            SelectIndex = 0;
            HoverIndex = -1;
            DeleteIndex = -1;
            Deleting = false;
            Changing = false;

            oDocumentList.Items.Clear();
            oCampainTextBox.Clear();
        }

        /// =========================================
        /// CommitChanges()
        /// =========================================
        public void WriteCampainNotes()
        {
            if (oDocumentList.SelectedIndex != -1)
            {
                (oDocumentList.Items[oDocumentList.SelectedIndex] as Document).Rtf = oCampainTextBox.Rtf;
            }
            foreach (Document doc in oDocumentList.Items)
            {
                Program.Character.oDocuments.Add(doc);
            }
        }

        /// =========================================
        /// ResizeText()
        /// =========================================
        public void ResizeText(float mod, float ratio)
        {
            oDocumentList.ItemHeight = (int) (OriginalHeight * ratio);
            oDocumentList.Font = new Font(oDocumentList.Font.FontFamily, OriginalListTextSize * ratio, oDocumentList.Font.Style);

            oCampainFocus.Font = new Font(oCampainFocus.Font.FontFamily, OriginalLabelSize * ratio, oCampainFocus.Font.Style);
            label2.Font = new Font(label2.Font.FontFamily, OriginalLabelSize * ratio, label2.Font.Style);
        }

        /// =========================================
        /// DefaultFocus()
        /// =========================================
        public void DefaultFocus()
        {
            oCampainFocus.Focus();
        }

        /// =========================================
        /// formatContextMenu()
        /// =========================================
        private void formatContextMenus()
        {
            oTextBoxContextMenu.ForeColor = Color.White;
            oTextBoxContextMenu.BackColor = Constants.DarkGrey;

            oDeleteDocumentContextMenu.ForeColor = Color.White;
            oDeleteDocumentContextMenu.BackColor = Constants.DarkGrey;

            oNewDocumentContextMenu.ForeColor = Color.White;
            oNewDocumentContextMenu.BackColor = Constants.DarkGrey;

            foreach (ToolStripMenuItem item in alignmentToolStripMenuItem.DropDownItems)
            {
                item.ForeColor = Color.White;
                item.BackColor = Constants.DarkGrey;
            }
        }

        /// =========================================
        /// textLocation()
        /// =========================================
        private PointF textLocation(Rectangle rect, SizeF size, string text)
        {
            //Centre the text
            PointF point;
            float x, y;

            x = rect.X + (rect.Width / 2);
            y = rect.Y + (rect.Height / 2);

            x -= size.Width / 2;
            y -= size.Height / 2;

            point = new PointF(x, y);

            return point;
        }

        #endregion

        #region Accessors

        private int SelectIndex
        {
            get;
            set;
        }

        private int DeleteIndex
        {
            get;
            set;
        }

        private int HoverIndex
        {
            get;
            set;
        }

        private bool Deleting
        {
            get;
            set;
        }

        private bool Changing
        {
            get;
            set;
        }

        private int OriginalHeight
        {
            get;
            set;
        }

        private float OriginalLabelSize
        {
            get;
            set;
        }

        private float OriginalListTextSize
        {
            get;
            set;
        }

        #endregion

        #region ContextMenu Events

        /// =========================================
        /// copyToolStripMenuItem_Click()
        /// =========================================
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oCampainTextBox.Copy();
        }

        /// =========================================
        /// pasteToolStripMenuItem_Click()
        /// =========================================
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oCampainTextBox.Paste();
        }

        /// =========================================
        /// cutToolStripMenuItem_Click()
        /// =========================================
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oCampainTextBox.Cut();
        }

        /// =========================================
        /// fontToolStripMenuItem_Click()
        /// =========================================
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;

            oFontDialog.Font = oCampainTextBox.SelectionFont;
            result = oFontDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                oCampainTextBox.SelectionFont = oFontDialog.Font;
            }
        }

        /// =========================================
        /// colourToolStripMenuItem_Click()
        /// =========================================
        private void colourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;

            oColourDialog.Color = oCampainTextBox.SelectionColor;
            result = oColourDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                oCampainTextBox.SelectionColor = oColourDialog.Color;
            }
        }

        /// =========================================
        /// leftToolStripMenuItem_Click()
        /// =========================================
        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oCampainTextBox.SelectionAlignment = HorizontalAlignment.Left;
        }

        /// =========================================
        /// centreToolStripMenuItem_Click()
        /// =========================================
        private void centreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oCampainTextBox.SelectionAlignment = HorizontalAlignment.Center;
        }

        /// =========================================
        /// rightToolStripMenuItem_Click()
        /// =========================================
        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oCampainTextBox.SelectionAlignment = HorizontalAlignment.Right;
        }

        /// =========================================
        /// undoToolStripMenuItem_Click()
        /// =========================================
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oCampainTextBox.Undo();
        }

        /// =========================================
        /// redoToolStripMenuItem_Click()
        /// =========================================
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oCampainTextBox.Redo();
        }

        #endregion

        #region ContextColour Events

        /// =========================================
        /// cutToolStripMenuItem_MouseEnter()
        /// =========================================
        private void cutToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            cutToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// cutToolStripMenuItem_MouseLeave()
        /// =========================================
        private void cutToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            cutToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// copyToolStripMenuItem_MouseEnter()
        /// =========================================
        private void copyToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            copyToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// copyToolStripMenuItem_MouseLeave()
        /// =========================================
        private void copyToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            copyToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// pasteToolStripMenuItem_MouseEnter()
        /// =========================================
        private void pasteToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            pasteToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// pasteToolStripMenuItem_MouseLeave()
        /// =========================================
        private void pasteToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            pasteToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// fontToolStripMenuItem_MouseEnter()
        /// =========================================
        private void fontToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            fontToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// fontToolStripMenuItem_MouseLeave()
        /// =========================================
        private void fontToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            fontToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// colourToolStripMenuItem_MouseEnter()
        /// =========================================
        private void colourToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            colourToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// colourToolStripMenuItem_MouseLeave()
        /// =========================================
        private void colourToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            colourToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// alignmentToolStripMenuItem_MouseEnter()
        /// =========================================
        private void alignmentToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            alignmentToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// alignmentToolStripMenuItem_MouseLeave()
        /// =========================================
        private void alignmentToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            alignmentToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// undoToolStripMenuItem_MouseEnter()
        /// =========================================
        private void undoToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            undoToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// undoToolStripMenuItem_MouseLeave()
        /// =========================================
        private void undoToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            undoToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// redoToolStripMenuItem_MouseEnter()
        /// =========================================
        private void redoToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            redoToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// redoToolStripMenuItem_MouseLeave()
        /// =========================================
        private void redoToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            redoToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// leftToolStripMenuItem_MouseEnter()
        /// =========================================
        private void leftToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            leftToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// leftToolStripMenuItem_MouseLeave()
        /// =========================================
        private void leftToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            leftToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// centreToolStripMenuItem_MouseEnter()
        /// =========================================
        private void centreToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            centreToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// centreToolStripMenuItem_MouseLeave()
        /// =========================================
        private void centreToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            centreToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// rightToolStripMenuItem_MouseEnter()
        /// =========================================
        private void rightToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            rightToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// rightToolStripMenuItem_MouseLeave()
        /// =========================================
        private void rightToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            rightToolStripMenuItem.ForeColor = Color.White;
        }

        #endregion

        #region TextBox Events

        /// =========================================
        /// oCampainTextBox_Enter()
        /// =========================================
        private void oCampainTextBox_Enter(object sender, EventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oCampainTextBox_Leave()
        /// =========================================
        private void oCampainTextBox_Leave(object sender, EventArgs e)
        {
            Program.Typing = false;
        }

        /// =========================================
        /// oCampainTextBox_MouseDown()
        /// =========================================
        private void oCampainTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                oTextBoxContextMenu.Show(oCampainTextBox, e.Location);
            }
        }

        /// =========================================
        /// oCampainTextBox_TextChanged()
        /// =========================================
        private void oCampainTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!Changing)
            {
                Program.Modified = true;
            }
        }

        #endregion

        #region DocumentList Events

        /// =========================================
        /// oDocumentList_SelectedIndexChanged()
        /// =========================================
        private void oDocumentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Document oldDoc, newDoc;

            Changing = true;

            //click on new document
            if (oDocumentList.SelectedIndex != -1)
            {
                newDoc = oDocumentList.Items[oDocumentList.SelectedIndex] as Document;

                if (!Deleting && SelectIndex != -1)
                {
                    oldDoc = oDocumentList.Items[SelectIndex] as Document;
                    oldDoc.Rtf = oCampainTextBox.Rtf;
                }

                SelectIndex = oDocumentList.SelectedIndex;
                oCampainTextBox.Rtf = newDoc.Rtf;

                oDocumentList.Invalidate();
            }

            Changing = false;
        }

        /// =========================================
        /// oDocumentList_DrawItem()
        /// =========================================
        private void oDocumentList_DrawItem(object sender, DrawItemEventArgs e)
        {
            Color fillColour = Constants.ControlGrey, textColour = Color.White, outlineColour = Color.Transparent;
            SizeF size;
            PointF point;
            string listItem;
            Graphics g = e.Graphics;
            ListBox lb = sender as ListBox;

            if (e.Index != -1)
            {
                //Highlight on hover
                if (e.Index == HoverIndex)
                {
                    fillColour = Constants.HighlightBlue;
                    outlineColour = Constants.OutlineBlue;
                    textColour = Color.Black;
                }

                //Highlight selected item
                if (e.Index == SelectIndex && e.Index != HoverIndex)
                {
                    fillColour = Constants.HighlightGrey;
                }

                //Manually draw each item
                using (SolidBrush fill = new SolidBrush(fillColour), text = new SolidBrush(textColour))
                {
                    using (Pen outline = new Pen(outlineColour))
                    {
                        e.DrawBackground();

                        listItem = lb.Items[e.Index].ToString();
                        size = g.MeasureString(listItem, e.Font);
                        point = textLocation(e.Bounds, size, listItem);

                        g.FillRectangle(fill, e.Bounds);
                        g.DrawRectangle(outline, e.Bounds);
                        g.DrawString(listItem, e.Font, text, point);
                    }
                }
            }
        }

        /// =========================================
        /// oDocumentList_MouseMove()
        /// =========================================
        private void oDocumentList_MouseMove(object sender, MouseEventArgs e)
        {
            int index;

            index = oDocumentList.IndexFromPoint(e.Location);

            if (index != -1 && index != HoverIndex)
            {
                HoverIndex = index;
                oDocumentList.Invalidate();
            }
        }

        /// =========================================
        /// oDocumentList_MouseLeave()
        /// =========================================
        private void oDocumentList_MouseLeave(object sender, EventArgs e)
        {
            HoverIndex = -1;
            oDocumentList.Invalidate();
        }

        /// =========================================
        /// oDocumentList_MouseDown()
        /// =========================================
        private void oDocumentList_MouseDown(object sender, MouseEventArgs e)
        {
            //Open context menu
            if (e.Button == MouseButtons.Right)
            {
                DeleteIndex = oDocumentList.IndexFromPoint(e.Location);

                if (DeleteIndex == -1)
                {
                    oNewDocumentContextMenu.Show(oDocumentList, e.Location);
                }
                else
                {
                    oDeleteDocumentContextMenu.Show(oDocumentList, e.Location);
                }
            }
            //Drag and drop list
            else if (e.Button == MouseButtons.Left)
            {
                oDocumentList_SelectedIndexChanged(new object(), new EventArgs());

                if (oDocumentList.SelectedIndex != -1)
                {
                    oDocumentList.DoDragDrop(oDocumentList.SelectedItem, DragDropEffects.Move);
                }
            }
        }

        /// =========================================
        /// oDocumentList_DragOver()
        /// =========================================
        private void oDocumentList_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// =========================================
        /// oDocumentList_DragDrop()
        /// =========================================
        private void oDocumentList_DragDrop(object sender, DragEventArgs e)
        {
            Point point = oDocumentList.PointToClient(new Point(e.X, e.Y));
            int index = oDocumentList.IndexFromPoint(point);

            if (index < 0)
                index = oDocumentList.Items.Count - 1;

            if (index != oDocumentList.SelectedIndex)
                Program.Modified = true;

            object data = e.Data.GetData(typeof(Document));

            Deleting = true;
            oDocumentList.Items.Remove(data);
            oDocumentList.Items.Insert(index, data);
            Deleting = false;

            SelectIndex = index;
            oDocumentList.SelectedIndex = index;
        }

        /// =========================================
        /// deleteToolStripMenuItem_Click()
        /// =========================================
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Deleting = true;

            oDocumentList.Items.RemoveAt(DeleteIndex);

            //Move selection to adjacent item
            if (oDocumentList.Items.Count > 0 && DeleteIndex == SelectIndex)
            {
                if (DeleteIndex == 0)
                    SelectIndex = DeleteIndex;
                else
                    SelectIndex = DeleteIndex - 1;

                oDocumentList.SelectedIndex = SelectIndex;
            }
            //No items are left
            else if (oDocumentList.Items.Count == 0)
            {
                ClearDocumentList();
            }

            Program.Modified = true;
            Deleting = false;
        }

        /// =========================================
        /// renameToolStripMenuItem_Click()
        /// =========================================
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputMessageBox inputMessageBox = new InputMessageBox();
            string name, defaultText = (oDocumentList.Items[DeleteIndex] as Document).Name;

            name = inputMessageBox.ShowMessage("Enter new name.", "Rename Document", defaultText);

            if (name != null)
            {
                oDocumentList.Items[DeleteIndex] = name;

                Program.Modified = true;
            }
        }

        /// =========================================
        /// newToolStripMenuItem_Click()
        /// =========================================
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputMessageBox inputMessageBox = new InputMessageBox();
            Document doc;
            string name;

            name = inputMessageBox.ShowMessage("Enter new document name.", "New Document", "New File");

            if (name != null)
            {
                doc = new Document(Guid.NewGuid().ToString(), name, "");
                oDocumentList.Items.Add(doc);

                oDocumentList.SelectedIndex = oDocumentList.Items.Count - 1;

                Program.Modified = true;
            }
        }

        #endregion

        #region DocumentContext Colour Events

        /// =========================================
        /// renameToolStripMenuItem_MouseEnter()
        /// =========================================
        private void renameToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            renameToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// renameToolStripMenuItem_MouseLeave()
        /// =========================================
        private void renameToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            renameToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// deleteToolStripMenuItem_MouseEnter()
        /// =========================================
        private void deleteToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            deleteToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// deleteToolStripMenuItem_MouseLeave()
        /// =========================================
        private void deleteToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            deleteToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// newToolStripMenuItem_MouseEnter()
        /// =========================================
        private void newToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            newToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// newToolStripMenuItem_MouseLeave()
        /// =========================================
        private void newToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            newToolStripMenuItem.ForeColor = Color.White;
        }

        #endregion

    }
}
