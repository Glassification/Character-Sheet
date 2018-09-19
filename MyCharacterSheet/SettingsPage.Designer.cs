namespace MyCharacterSheet
{
    partial class SettingsPage
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsPage));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oMuteBox = new System.Windows.Forms.GroupBox();
            this.chkMute = new System.Windows.Forms.CheckBox();
            this.oTabBox = new System.Windows.Forms.GroupBox();
            this.chkTab = new System.Windows.Forms.CheckBox();
            this.oAutosaveBox = new System.Windows.Forms.GroupBox();
            this.oAutosaveInterval = new System.Windows.Forms.Label();
            this.trkAutosave = new System.Windows.Forms.TrackBar();
            this.chkAutosave = new System.Windows.Forms.CheckBox();
            this.oFormatBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.oFontDialog = new System.Windows.Forms.FontDialog();
            this.oColorDialog = new System.Windows.Forms.ColorDialog();
            this.chkAnimalCompanion = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.oMuteBox.SuspendLayout();
            this.oTabBox.SuspendLayout();
            this.oAutosaveBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkAutosave)).BeginInit();
            this.oFormatBox.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.oMuteBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.oTabBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oAutosaveBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.oFormatBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.01831F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.70481F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.63844F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.96796F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.21281F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(365, 437);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oMuteBox
            // 
            this.oMuteBox.Controls.Add(this.chkMute);
            this.oMuteBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oMuteBox.Location = new System.Drawing.Point(4, 4);
            this.oMuteBox.Margin = new System.Windows.Forms.Padding(4);
            this.oMuteBox.Name = "oMuteBox";
            this.oMuteBox.Padding = new System.Windows.Forms.Padding(4);
            this.oMuteBox.Size = new System.Drawing.Size(357, 62);
            this.oMuteBox.TabIndex = 0;
            this.oMuteBox.TabStop = false;
            this.oMuteBox.Text = "Mute";
            // 
            // chkMute
            // 
            this.chkMute.AutoSize = true;
            this.chkMute.Location = new System.Drawing.Point(12, 23);
            this.chkMute.Margin = new System.Windows.Forms.Padding(4);
            this.chkMute.Name = "chkMute";
            this.chkMute.Size = new System.Drawing.Size(169, 21);
            this.chkMute.TabIndex = 0;
            this.chkMute.Text = "Remember mute state";
            this.chkMute.UseVisualStyleBackColor = true;
            this.chkMute.CheckedChanged += new System.EventHandler(this.chkMute_CheckedChanged);
            // 
            // oTabBox
            // 
            this.oTabBox.Controls.Add(this.chkTab);
            this.oTabBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oTabBox.Location = new System.Drawing.Point(4, 74);
            this.oTabBox.Margin = new System.Windows.Forms.Padding(4);
            this.oTabBox.Name = "oTabBox";
            this.oTabBox.Padding = new System.Windows.Forms.Padding(4);
            this.oTabBox.Size = new System.Drawing.Size(357, 65);
            this.oTabBox.TabIndex = 1;
            this.oTabBox.TabStop = false;
            this.oTabBox.Text = "Tabs";
            // 
            // chkTab
            // 
            this.chkTab.AutoSize = true;
            this.chkTab.Location = new System.Drawing.Point(12, 36);
            this.chkTab.Margin = new System.Windows.Forms.Padding(4);
            this.chkTab.Name = "chkTab";
            this.chkTab.Size = new System.Drawing.Size(185, 21);
            this.chkTab.TabIndex = 0;
            this.chkTab.Text = "Remember last open tab";
            this.chkTab.UseVisualStyleBackColor = true;
            this.chkTab.CheckedChanged += new System.EventHandler(this.chkTab_CheckedChanged);
            // 
            // oAutosaveBox
            // 
            this.oAutosaveBox.Controls.Add(this.oAutosaveInterval);
            this.oAutosaveBox.Controls.Add(this.trkAutosave);
            this.oAutosaveBox.Controls.Add(this.chkAutosave);
            this.oAutosaveBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oAutosaveBox.Location = new System.Drawing.Point(4, 147);
            this.oAutosaveBox.Margin = new System.Windows.Forms.Padding(4);
            this.oAutosaveBox.Name = "oAutosaveBox";
            this.oAutosaveBox.Padding = new System.Windows.Forms.Padding(4);
            this.oAutosaveBox.Size = new System.Drawing.Size(357, 139);
            this.oAutosaveBox.TabIndex = 2;
            this.oAutosaveBox.TabStop = false;
            this.oAutosaveBox.Text = "Autosave";
            // 
            // oAutosaveInterval
            // 
            this.oAutosaveInterval.AutoSize = true;
            this.oAutosaveInterval.Location = new System.Drawing.Point(25, 106);
            this.oAutosaveInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.oAutosaveInterval.Name = "oAutosaveInterval";
            this.oAutosaveInterval.Size = new System.Drawing.Size(175, 17);
            this.oAutosaveInterval.TabIndex = 2;
            this.oAutosaveInterval.Text = "Autosave every % minutes";
            // 
            // trkAutosave
            // 
            this.trkAutosave.Location = new System.Drawing.Point(12, 66);
            this.trkAutosave.Margin = new System.Windows.Forms.Padding(4);
            this.trkAutosave.Maximum = 9;
            this.trkAutosave.Name = "trkAutosave";
            this.trkAutosave.Size = new System.Drawing.Size(176, 56);
            this.trkAutosave.TabIndex = 1;
            this.trkAutosave.ValueChanged += new System.EventHandler(this.trkAutosave_ValueChanged);
            // 
            // chkAutosave
            // 
            this.chkAutosave.AutoSize = true;
            this.chkAutosave.Location = new System.Drawing.Point(12, 38);
            this.chkAutosave.Margin = new System.Windows.Forms.Padding(4);
            this.chkAutosave.Name = "chkAutosave";
            this.chkAutosave.Size = new System.Drawing.Size(136, 21);
            this.chkAutosave.TabIndex = 0;
            this.chkAutosave.Text = "Enable autosave";
            this.chkAutosave.UseVisualStyleBackColor = true;
            this.chkAutosave.CheckedChanged += new System.EventHandler(this.chkAutosave_CheckedChanged);
            // 
            // oFormatBox
            // 
            this.oFormatBox.Controls.Add(this.chkAnimalCompanion);
            this.oFormatBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oFormatBox.Location = new System.Drawing.Point(4, 294);
            this.oFormatBox.Margin = new System.Windows.Forms.Padding(4);
            this.oFormatBox.Name = "oFormatBox";
            this.oFormatBox.Padding = new System.Windows.Forms.Padding(4);
            this.oFormatBox.Size = new System.Drawing.Size(357, 88);
            this.oFormatBox.TabIndex = 3;
            this.oFormatBox.TabStop = false;
            this.oFormatBox.Text = "Animal Companion";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.Controls.Add(this.btnOK, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 390);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(357, 43);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(4, 4);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(152, 35);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(199, 4);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(154, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // oColorDialog
            // 
            this.oColorDialog.Color = System.Drawing.Color.White;
            // 
            // chkAnimalCompanion
            // 
            this.chkAnimalCompanion.AutoSize = true;
            this.chkAnimalCompanion.Location = new System.Drawing.Point(12, 42);
            this.chkAnimalCompanion.Name = "chkAnimalCompanion";
            this.chkAnimalCompanion.Size = new System.Drawing.Size(177, 21);
            this.chkAnimalCompanion.TabIndex = 0;
            this.chkAnimalCompanion.Text = "Hide animal companion";
            this.chkAnimalCompanion.UseVisualStyleBackColor = true;
            this.chkAnimalCompanion.CheckedChanged += new System.EventHandler(this.chkAnimalCompanion_CheckedChanged);
            // 
            // SettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 437);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "SettingsPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SettingsPage_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SettingsPage_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oMuteBox.ResumeLayout(false);
            this.oMuteBox.PerformLayout();
            this.oTabBox.ResumeLayout(false);
            this.oTabBox.PerformLayout();
            this.oAutosaveBox.ResumeLayout(false);
            this.oAutosaveBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkAutosave)).EndInit();
            this.oFormatBox.ResumeLayout(false);
            this.oFormatBox.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox oMuteBox;
        private System.Windows.Forms.CheckBox chkMute;
        private System.Windows.Forms.GroupBox oTabBox;
        private System.Windows.Forms.CheckBox chkTab;
        private System.Windows.Forms.GroupBox oAutosaveBox;
        private System.Windows.Forms.Label oAutosaveInterval;
        private System.Windows.Forms.TrackBar trkAutosave;
        private System.Windows.Forms.CheckBox chkAutosave;
        private System.Windows.Forms.GroupBox oFormatBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FontDialog oFontDialog;
        private System.Windows.Forms.ColorDialog oColorDialog;
        private System.Windows.Forms.CheckBox chkAnimalCompanion;
    }
}