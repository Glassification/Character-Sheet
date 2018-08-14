namespace MyCharacterSheet
{
    partial class CampainPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.oCampainTextBox = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oCampainFocus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.oDocumentList = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.oTextBoxContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oFontDialog = new System.Windows.Forms.FontDialog();
            this.oColourDialog = new System.Windows.Forms.ColorDialog();
            this.oDeleteDocumentContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oNewDocumentContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.oTextBoxContextMenu.SuspendLayout();
            this.oDeleteDocumentContextMenu.SuspendLayout();
            this.oNewDocumentContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // oCampainTextBox
            // 
            this.oCampainTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.oCampainTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.oCampainTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oCampainTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oCampainTextBox.ForeColor = System.Drawing.Color.White;
            this.oCampainTextBox.Location = new System.Drawing.Point(27, 16);
            this.oCampainTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.oCampainTextBox.Name = "oCampainTextBox";
            this.oCampainTextBox.Size = new System.Drawing.Size(1047, 636);
            this.oCampainTextBox.TabIndex = 0;
            this.oCampainTextBox.Text = "";
            this.oCampainTextBox.TextChanged += new System.EventHandler(this.oCampainTextBox_TextChanged);
            this.oCampainTextBox.Enter += new System.EventHandler(this.oCampainTextBox_Enter);
            this.oCampainTextBox.Leave += new System.EventHandler(this.oCampainTextBox_Leave);
            this.oCampainTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.oCampainTextBox_MouseDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.Controls.Add(this.oCampainFocus, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.oDocumentList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1297, 720);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // oCampainFocus
            // 
            this.oCampainFocus.AutoSize = true;
            this.oCampainFocus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(54)))), ((int)(((byte)(92)))));
            this.oCampainFocus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oCampainFocus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oCampainFocus.ForeColor = System.Drawing.Color.White;
            this.oCampainFocus.Location = new System.Drawing.Point(0, 0);
            this.oCampainFocus.Margin = new System.Windows.Forms.Padding(0);
            this.oCampainFocus.Name = "oCampainFocus";
            this.oCampainFocus.Size = new System.Drawing.Size(194, 50);
            this.oCampainFocus.TabIndex = 2;
            this.oCampainFocus.Text = "Documents";
            this.oCampainFocus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(54)))), ((int)(((byte)(92)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(194, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1103, 50);
            this.label2.TabIndex = 3;
            this.label2.Text = "Campain Notes";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // oDocumentList
            // 
            this.oDocumentList.AllowDrop = true;
            this.oDocumentList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.oDocumentList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.oDocumentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oDocumentList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.oDocumentList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oDocumentList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.oDocumentList.FormattingEnabled = true;
            this.oDocumentList.ItemHeight = 24;
            this.oDocumentList.Location = new System.Drawing.Point(0, 50);
            this.oDocumentList.Margin = new System.Windows.Forms.Padding(0);
            this.oDocumentList.Name = "oDocumentList";
            this.oDocumentList.Size = new System.Drawing.Size(194, 670);
            this.oDocumentList.TabIndex = 4;
            this.oDocumentList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.oDocumentList_DrawItem);
            this.oDocumentList.SelectedIndexChanged += new System.EventHandler(this.oDocumentList_SelectedIndexChanged);
            this.oDocumentList.DragDrop += new System.Windows.Forms.DragEventHandler(this.oDocumentList_DragDrop);
            this.oDocumentList.DragOver += new System.Windows.Forms.DragEventHandler(this.oDocumentList_DragOver);
            this.oDocumentList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.oDocumentList_MouseDown);
            this.oDocumentList.MouseLeave += new System.EventHandler(this.oDocumentList_MouseLeave);
            this.oDocumentList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.oDocumentList_MouseMove);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.5F));
            this.tableLayoutPanel2.Controls.Add(this.oCampainTextBox, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(194, 50);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.5F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1103, 670);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // oTextBoxContextMenu
            // 
            this.oTextBoxContextMenu.BackColor = System.Drawing.SystemColors.Control;
            this.oTextBoxContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.oTextBoxContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator1,
            this.fontToolStripMenuItem,
            this.colourToolStripMenuItem,
            this.alignmentToolStripMenuItem,
            this.toolStripSeparator2,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.oTextBoxContextMenu.Name = "oTextBoxContextMenu";
            this.oTextBoxContextMenu.ShowImageMargin = false;
            this.oTextBoxContextMenu.Size = new System.Drawing.Size(123, 208);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            this.cutToolStripMenuItem.MouseEnter += new System.EventHandler(this.cutToolStripMenuItem_MouseEnter);
            this.cutToolStripMenuItem.MouseLeave += new System.EventHandler(this.cutToolStripMenuItem_MouseLeave);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            this.copyToolStripMenuItem.MouseEnter += new System.EventHandler(this.copyToolStripMenuItem_MouseEnter);
            this.copyToolStripMenuItem.MouseLeave += new System.EventHandler(this.copyToolStripMenuItem_MouseLeave);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            this.pasteToolStripMenuItem.MouseEnter += new System.EventHandler(this.pasteToolStripMenuItem_MouseEnter);
            this.pasteToolStripMenuItem.MouseLeave += new System.EventHandler(this.pasteToolStripMenuItem_MouseLeave);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(119, 6);
            // 
            // fontToolStripMenuItem
            // 
            this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            this.fontToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.fontToolStripMenuItem.Text = "Font";
            this.fontToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem_Click);
            this.fontToolStripMenuItem.MouseEnter += new System.EventHandler(this.fontToolStripMenuItem_MouseEnter);
            this.fontToolStripMenuItem.MouseLeave += new System.EventHandler(this.fontToolStripMenuItem_MouseLeave);
            // 
            // colourToolStripMenuItem
            // 
            this.colourToolStripMenuItem.Name = "colourToolStripMenuItem";
            this.colourToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.colourToolStripMenuItem.Text = "Colour";
            this.colourToolStripMenuItem.Click += new System.EventHandler(this.colourToolStripMenuItem_Click);
            this.colourToolStripMenuItem.MouseEnter += new System.EventHandler(this.colourToolStripMenuItem_MouseEnter);
            this.colourToolStripMenuItem.MouseLeave += new System.EventHandler(this.colourToolStripMenuItem_MouseLeave);
            // 
            // alignmentToolStripMenuItem
            // 
            this.alignmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leftToolStripMenuItem,
            this.centreToolStripMenuItem,
            this.rightToolStripMenuItem});
            this.alignmentToolStripMenuItem.Name = "alignmentToolStripMenuItem";
            this.alignmentToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.alignmentToolStripMenuItem.Text = "Alignment";
            this.alignmentToolStripMenuItem.MouseEnter += new System.EventHandler(this.alignmentToolStripMenuItem_MouseEnter);
            this.alignmentToolStripMenuItem.MouseLeave += new System.EventHandler(this.alignmentToolStripMenuItem_MouseLeave);
            // 
            // leftToolStripMenuItem
            // 
            this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            this.leftToolStripMenuItem.Size = new System.Drawing.Size(127, 26);
            this.leftToolStripMenuItem.Text = "Left";
            this.leftToolStripMenuItem.Click += new System.EventHandler(this.leftToolStripMenuItem_Click);
            this.leftToolStripMenuItem.MouseEnter += new System.EventHandler(this.leftToolStripMenuItem_MouseEnter);
            this.leftToolStripMenuItem.MouseLeave += new System.EventHandler(this.leftToolStripMenuItem_MouseLeave);
            // 
            // centreToolStripMenuItem
            // 
            this.centreToolStripMenuItem.Name = "centreToolStripMenuItem";
            this.centreToolStripMenuItem.Size = new System.Drawing.Size(127, 26);
            this.centreToolStripMenuItem.Text = "Centre";
            this.centreToolStripMenuItem.Click += new System.EventHandler(this.centreToolStripMenuItem_Click);
            this.centreToolStripMenuItem.MouseEnter += new System.EventHandler(this.centreToolStripMenuItem_MouseEnter);
            this.centreToolStripMenuItem.MouseLeave += new System.EventHandler(this.centreToolStripMenuItem_MouseLeave);
            // 
            // rightToolStripMenuItem
            // 
            this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            this.rightToolStripMenuItem.Size = new System.Drawing.Size(127, 26);
            this.rightToolStripMenuItem.Text = "Right";
            this.rightToolStripMenuItem.Click += new System.EventHandler(this.rightToolStripMenuItem_Click);
            this.rightToolStripMenuItem.MouseEnter += new System.EventHandler(this.rightToolStripMenuItem_MouseEnter);
            this.rightToolStripMenuItem.MouseLeave += new System.EventHandler(this.rightToolStripMenuItem_MouseLeave);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(119, 6);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            this.undoToolStripMenuItem.MouseEnter += new System.EventHandler(this.undoToolStripMenuItem_MouseEnter);
            this.undoToolStripMenuItem.MouseLeave += new System.EventHandler(this.undoToolStripMenuItem_MouseLeave);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            this.redoToolStripMenuItem.MouseEnter += new System.EventHandler(this.redoToolStripMenuItem_MouseEnter);
            this.redoToolStripMenuItem.MouseLeave += new System.EventHandler(this.redoToolStripMenuItem_MouseLeave);
            // 
            // oDeleteDocumentContextMenu
            // 
            this.oDeleteDocumentContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.oDeleteDocumentContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.oDeleteDocumentContextMenu.Name = "oDeleteDocumentContextMenu";
            this.oDeleteDocumentContextMenu.ShowImageMargin = false;
            this.oDeleteDocumentContextMenu.Size = new System.Drawing.Size(108, 52);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(107, 24);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            this.renameToolStripMenuItem.MouseEnter += new System.EventHandler(this.renameToolStripMenuItem_MouseEnter);
            this.renameToolStripMenuItem.MouseLeave += new System.EventHandler(this.renameToolStripMenuItem_MouseLeave);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            this.deleteToolStripMenuItem.MouseEnter += new System.EventHandler(this.deleteToolStripMenuItem_MouseEnter);
            this.deleteToolStripMenuItem.MouseLeave += new System.EventHandler(this.deleteToolStripMenuItem_MouseLeave);
            // 
            // oNewDocumentContextMenu
            // 
            this.oNewDocumentContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.oNewDocumentContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem});
            this.oNewDocumentContextMenu.Name = "oNewDocumentContextMenu";
            this.oNewDocumentContextMenu.ShowImageMargin = false;
            this.oNewDocumentContextMenu.Size = new System.Drawing.Size(84, 28);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            this.newToolStripMenuItem.MouseEnter += new System.EventHandler(this.newToolStripMenuItem_MouseEnter);
            this.newToolStripMenuItem.MouseLeave += new System.EventHandler(this.newToolStripMenuItem_MouseLeave);
            // 
            // CampainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CampainPage";
            this.Size = new System.Drawing.Size(1297, 720);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.oTextBoxContextMenu.ResumeLayout(false);
            this.oDeleteDocumentContextMenu.ResumeLayout(false);
            this.oNewDocumentContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox oCampainTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label oCampainFocus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip oTextBoxContextMenu;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colourToolStripMenuItem;
        private System.Windows.Forms.FontDialog oFontDialog;
        private System.Windows.Forms.ColorDialog oColourDialog;
        private System.Windows.Forms.ToolStripMenuItem alignmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ListBox oDocumentList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ContextMenuStrip oDeleteDocumentContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip oNewDocumentContextMenu;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
    }
}
