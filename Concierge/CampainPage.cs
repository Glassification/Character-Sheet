using System;
using System.Windows.Forms;
using System.Drawing;
using Concierge.Utility;
using Concierge.Lists;
using static Concierge.Utility.Constants;

namespace Concierge
{
    #nullable enable
    public partial class CampainPage : UserControl
    {

        #region Constants

        private const string LINE_SEPARATOR_KEY = "7MOm!GgMo)83>v8+i+qc:lk>";
        private const string LINE_SEPARATOR     = "________________________";

        #endregion

        #region Constructor

        public CampainPage()
        {
            InitializeComponent();

            FormatContextMenus();

            SelectIndex = 0;
            HoverIndex = -1;
            DeleteIndex = -1;
            Deleting = false;
            Changing = false;

            OriginalHeight = oDocumentList.ItemHeight;
            OriginalListTextSize = oDocumentList.Font.Size;
            OriginalLabelSize = oCampainFocus.Font.Size;
            OriginalButtonHeight = btnAddDocument.Font.Size;
        }

        #endregion

        #region Methods

        /// =========================================
        /// FillDocumentList()
        /// =========================================
        public void FillDocumentList()
        {
            Document? document;

            if (Program.Character.oDocuments.Count > 0)
            {
                oDocumentList.Items.AddRange(Program.Character.oDocuments.ToArray());
                document = (oDocumentList.Items[0] as Document);
                if (document != null)
                {
                    oCampainTextBox.Rtf = document.Rtf;
                    oDocumentList.SelectedIndex = 0;
                }
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
        /// WriteCampainNotes()
        /// =========================================
        public void WriteCampainNotes()
        {
            Document? document;

            if (oDocumentList.SelectedIndex != -1)
            {
                document = (oDocumentList.Items[oDocumentList.SelectedIndex] as Document);
                if (document != null)
                    document.Rtf = oCampainTextBox.Rtf;
            }
            foreach (Document doc in oDocumentList.Items)
            {
                if (doc.Name.Equals(LINE_SEPARATOR_KEY))
                    doc.Rtf = "";

                Program.Character.oDocuments.Add(doc);
            }
        }

        /// =========================================
        /// ResizeText()
        /// =========================================
        public void ResizeText()
        {
            oDocumentList.ItemHeight = (int) (OriginalHeight * Program.MainForm.Ratio);
            oDocumentList.Font = new Font(oDocumentList.Font.FontFamily, OriginalListTextSize * Program.MainForm.Ratio, oDocumentList.Font.Style);

            oCampainFocus.Font = new Font(oCampainFocus.Font.FontFamily, OriginalLabelSize * Program.MainForm.Ratio, oCampainFocus.Font.Style);
            label2.Font = new Font(label2.Font.FontFamily, OriginalLabelSize * Program.MainForm.Ratio, label2.Font.Style);

            btnAddDocument.Font = new Font(btnAddDocument.Font.FontFamily, OriginalButtonHeight * Program.MainForm.Ratio, btnAddDocument.Font.Style);
            btnAddSeparator.Font = new Font(btnAddSeparator.Font.FontFamily, OriginalButtonHeight * Program.MainForm.Ratio, btnAddSeparator.Font.Style);
        }

        /// =========================================
        /// DefaultFocus()
        /// =========================================
        public void DefaultFocus()
        {
            oCampainFocus.Focus();
        }

        /// =========================================
        /// FormatContextMenus()
        /// =========================================
        private void FormatContextMenus()
        {
            oTextBoxContextMenu.ForeColor = Color.White;
            oTextBoxContextMenu.BackColor = DarkGrey;

            oDeleteContextMenu.ForeColor = Color.White;
            oDeleteContextMenu.BackColor = DarkGrey;

            oEditDocumentContextMenu.ForeColor = Color.White;
            oEditDocumentContextMenu.BackColor = DarkGrey;

            foreach (ToolStripMenuItem item in alignmentToolStripMenuItem.DropDownItems)
            {
                item.ForeColor = Color.White;
                item.BackColor = DarkGrey;
            }
        }

        /// =========================================
        /// TextLocation()
        /// =========================================
        private PointF TextLocation(Rectangle rect, SizeF size, string text)
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

        /// =========================================
        /// DeleteDocument()
        /// =========================================
        private void DeleteDocument()
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

        private float OriginalButtonHeight
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
            Document? oldDoc, newDoc;

            Changing = true;

            //click on new document
            if (oDocumentList.SelectedIndex != -1)
            {
                newDoc = oDocumentList.Items[oDocumentList.SelectedIndex] as Document;

                if (newDoc != null && !newDoc.Name.Equals(LINE_SEPARATOR_KEY))
                {
                    if (!Deleting && SelectIndex != -1)
                    {
                        oldDoc = oDocumentList.Items[SelectIndex] as Document;
                        if (oldDoc != null)
                            oldDoc.Rtf = oCampainTextBox.Rtf;
                    }

                    SelectIndex = oDocumentList.SelectedIndex;
                    oCampainTextBox.Rtf = newDoc.Rtf;

                    oDocumentList.Invalidate();
                }
            }

            Changing = false;
        }

        /// =========================================
        /// oDocumentList_DrawItem()
        /// =========================================
        private void oDocumentList_DrawItem(object sender, DrawItemEventArgs e)
        {
            Color fillColour = ControlGrey, textColour = Color.White, outlineColour = Color.Transparent;
            SizeF size;
            PointF point;
            string listItem;
            Graphics g = e.Graphics;
            ListBox? lb = sender as ListBox;

            if (e.Index != -1 && lb != null)
            {
                listItem = lb.Items[e.Index].ToString();

                if (!listItem.Equals(LINE_SEPARATOR_KEY))
                {
                    //Highlight on hover
                    if (e.Index == HoverIndex)
                    {
                        fillColour = HighlightBlue;
                        outlineColour = OutlineBlue;
                        textColour = Color.Black;
                    }

                    //Blend selected item
                    if (e.Index == SelectIndex && e.Index != HoverIndex)
                    {
                        fillColour = DarkGrey;
                    }
                }

                //Manually draw each item
                using (SolidBrush fill = new SolidBrush(fillColour), text = new SolidBrush(textColour))
                {
                    using (Pen outline = new Pen(outlineColour))
                    {
                        e.DrawBackground();
                        if (listItem.Equals(LINE_SEPARATOR_KEY))
                            listItem = LINE_SEPARATOR;

                        size = g.MeasureString(listItem, e.Font);
                        point = TextLocation(e.Bounds, size, listItem);

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
            Document? document;

            //Open context menu
            if (e.Button == MouseButtons.Right)
            {
                DeleteIndex = oDocumentList.IndexFromPoint(e.Location);

                if (DeleteIndex != -1)
                {
                    document = oDocumentList.Items[DeleteIndex] as Document;
                    if (document != null && document.Name.Equals(LINE_SEPARATOR_KEY))
                        oDeleteContextMenu.Show(oDocumentList, e.Location);
                    else
                        oEditDocumentContextMenu.Show(oDocumentList, e.Location);
                }
            }
            //Drag and drop list
            else if (e.Button == MouseButtons.Left)
            {
                oDocumentList_SelectedIndexChanged(new object(), EventArgs.Empty);

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
            DeleteDocument();
        }

        /// =========================================
        /// renameToolStripMenuItem_Click()
        /// =========================================
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document? document = oDocumentList.Items[DeleteIndex] as Document;
            InputMessageBox inputMessageBox = new InputMessageBox();
            string? name;
            string defaultText;

            if (document != null)
            {
                defaultText = document.Name;
                name = inputMessageBox.ShowMessage("Enter new name.", "Rename Document", defaultText);

                if (name != null)
                {
                    document.Name = name;

                    oDocumentList.Invalidate();
                    Program.Modified = true;
                }
            }
        }

        /// =========================================
        /// btnAddDocument_Click()
        /// =========================================
        private void btnAddDocument_Click(object sender, EventArgs e)
        {
            InputMessageBox inputMessageBox = new InputMessageBox();
            Document doc;
            string? name;

            name = inputMessageBox.ShowMessage("Enter new document name.", "New Document", "New File");

            if (name != null)
            {
                doc = new Document(Guid.NewGuid(), name, "");
                oDocumentList.Items.Add(doc);

                oDocumentList.SelectedIndex = oDocumentList.Items.Count - 1;

                Program.Modified = true;
            }
        }

        /// =========================================
        /// btnAddSeparator_Click()
        /// =========================================
        private void btnAddSeparator_Click(object sender, EventArgs e)
        {
            oDocumentList.Items.Add(new Document(Guid.NewGuid(), LINE_SEPARATOR_KEY, ""));
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
        /// deleteToolStripMenuItem1_Click()
        /// =========================================
        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DeleteDocument();
        }

        /// =========================================
        /// deleteToolStripMenuItem1_MouseEnter()
        /// =========================================
        private void deleteToolStripMenuItem1_MouseEnter(object sender, EventArgs e)
        {
            deleteToolStripMenuItem1.ForeColor = Color.Black;
        }

        /// =========================================
        /// deleteToolStripMenuItem1_MouseLeave()
        /// =========================================
        private void deleteToolStripMenuItem1_MouseLeave(object sender, EventArgs e)
        {
            deleteToolStripMenuItem1.ForeColor = Color.White;
        }

        #endregion

    }
}
