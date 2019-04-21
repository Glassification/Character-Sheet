namespace Concierge
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
            this.chkTab = new System.Windows.Forms.CheckBox();
            this.chkMute = new System.Windows.Forms.CheckBox();
            this.oAutosaveBox = new System.Windows.Forms.GroupBox();
            this.oAutosaveInterval = new System.Windows.Forms.Label();
            this.trkAutosave = new System.Windows.Forms.TrackBar();
            this.chkAutosave = new System.Windows.Forms.CheckBox();
            this.oFormatBox = new System.Windows.Forms.GroupBox();
            this.chkAnimalCompanion = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.oWeightBox = new System.Windows.Forms.GroupBox();
            this.chkEncumbrance = new System.Windows.Forms.CheckBox();
            this.chkCoinWeight = new System.Windows.Forms.CheckBox();
            this.oFontDialog = new System.Windows.Forms.FontDialog();
            this.oColorDialog = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.oMuteBox.SuspendLayout();
            this.oAutosaveBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkAutosave)).BeginInit();
            this.oFormatBox.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.oWeightBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.oMuteBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.oAutosaveBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oFormatBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.oWeightBox, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.08197F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.79724F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.65899F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.58986F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.447004F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(365, 434);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oMuteBox
            // 
            this.oMuteBox.Controls.Add(this.chkTab);
            this.oMuteBox.Controls.Add(this.chkMute);
            this.oMuteBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oMuteBox.Location = new System.Drawing.Point(4, 4);
            this.oMuteBox.Margin = new System.Windows.Forms.Padding(4);
            this.oMuteBox.Name = "oMuteBox";
            this.oMuteBox.Padding = new System.Windows.Forms.Padding(4);
            this.oMuteBox.Size = new System.Drawing.Size(357, 79);
            this.oMuteBox.TabIndex = 0;
            this.oMuteBox.TabStop = false;
            this.oMuteBox.Text = "Preferences";
            // 
            // chkTab
            // 
            this.chkTab.AutoSize = true;
            this.chkTab.Location = new System.Drawing.Point(12, 52);
            this.chkTab.Margin = new System.Windows.Forms.Padding(4);
            this.chkTab.Name = "chkTab";
            this.chkTab.Size = new System.Drawing.Size(185, 21);
            this.chkTab.TabIndex = 1;
            this.chkTab.Text = "Remember last open tab";
            this.chkTab.UseVisualStyleBackColor = true;
            this.chkTab.CheckedChanged += new System.EventHandler(this.chkTab_CheckedChanged);
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
            // oAutosaveBox
            // 
            this.oAutosaveBox.Controls.Add(this.oAutosaveInterval);
            this.oAutosaveBox.Controls.Add(this.trkAutosave);
            this.oAutosaveBox.Controls.Add(this.chkAutosave);
            this.oAutosaveBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oAutosaveBox.Location = new System.Drawing.Point(4, 91);
            this.oAutosaveBox.Margin = new System.Windows.Forms.Padding(4);
            this.oAutosaveBox.Name = "oAutosaveBox";
            this.oAutosaveBox.Padding = new System.Windows.Forms.Padding(4);
            this.oAutosaveBox.Size = new System.Drawing.Size(357, 130);
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
            this.trkAutosave.Location = new System.Drawing.Point(12, 52);
            this.trkAutosave.Margin = new System.Windows.Forms.Padding(4);
            this.trkAutosave.Maximum = 9;
            this.trkAutosave.Name = "trkAutosave";
            this.trkAutosave.Size = new System.Drawing.Size(176, 56);
            this.trkAutosave.TabIndex = 3;
            this.trkAutosave.ValueChanged += new System.EventHandler(this.trkAutosave_ValueChanged);
            // 
            // chkAutosave
            // 
            this.chkAutosave.AutoSize = true;
            this.chkAutosave.Location = new System.Drawing.Point(12, 23);
            this.chkAutosave.Margin = new System.Windows.Forms.Padding(4);
            this.chkAutosave.Name = "chkAutosave";
            this.chkAutosave.Size = new System.Drawing.Size(136, 21);
            this.chkAutosave.TabIndex = 2;
            this.chkAutosave.Text = "Enable autosave";
            this.chkAutosave.UseVisualStyleBackColor = true;
            this.chkAutosave.CheckedChanged += new System.EventHandler(this.chkAutosave_CheckedChanged);
            // 
            // oFormatBox
            // 
            this.oFormatBox.Controls.Add(this.chkAnimalCompanion);
            this.oFormatBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oFormatBox.Location = new System.Drawing.Point(4, 323);
            this.oFormatBox.Margin = new System.Windows.Forms.Padding(4);
            this.oFormatBox.Name = "oFormatBox";
            this.oFormatBox.Padding = new System.Windows.Forms.Padding(4);
            this.oFormatBox.Size = new System.Drawing.Size(357, 64);
            this.oFormatBox.TabIndex = 3;
            this.oFormatBox.TabStop = false;
            this.oFormatBox.Text = "Animal Companion";
            // 
            // chkAnimalCompanion
            // 
            this.chkAnimalCompanion.AutoSize = true;
            this.chkAnimalCompanion.Location = new System.Drawing.Point(12, 22);
            this.chkAnimalCompanion.Name = "chkAnimalCompanion";
            this.chkAnimalCompanion.Size = new System.Drawing.Size(177, 21);
            this.chkAnimalCompanion.TabIndex = 6;
            this.chkAnimalCompanion.Text = "Hide animal companion";
            this.chkAnimalCompanion.UseVisualStyleBackColor = true;
            this.chkAnimalCompanion.CheckedChanged += new System.EventHandler(this.chkAnimalCompanion_CheckedChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnOK, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(83, 395);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(199, 35);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(4, 4);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(92, 27);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(104, 4);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 27);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // oWeightBox
            // 
            this.oWeightBox.Controls.Add(this.chkEncumbrance);
            this.oWeightBox.Controls.Add(this.chkCoinWeight);
            this.oWeightBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oWeightBox.Location = new System.Drawing.Point(3, 228);
            this.oWeightBox.Name = "oWeightBox";
            this.oWeightBox.Size = new System.Drawing.Size(359, 88);
            this.oWeightBox.TabIndex = 5;
            this.oWeightBox.TabStop = false;
            this.oWeightBox.Text = "Encumbrance";
            // 
            // chkEncumbrance
            // 
            this.chkEncumbrance.AutoSize = true;
            this.chkEncumbrance.Location = new System.Drawing.Point(13, 49);
            this.chkEncumbrance.Name = "chkEncumbrance";
            this.chkEncumbrance.Size = new System.Drawing.Size(145, 21);
            this.chkEncumbrance.TabIndex = 5;
            this.chkEncumbrance.Text = "Use encumbrance";
            this.chkEncumbrance.UseVisualStyleBackColor = true;
            this.chkEncumbrance.CheckedChanged += new System.EventHandler(this.chkEncumbrance_CheckedChanged);
            // 
            // chkCoinWeight
            // 
            this.chkCoinWeight.AutoSize = true;
            this.chkCoinWeight.Location = new System.Drawing.Point(13, 21);
            this.chkCoinWeight.Name = "chkCoinWeight";
            this.chkCoinWeight.Size = new System.Drawing.Size(144, 21);
            this.chkCoinWeight.TabIndex = 4;
            this.chkCoinWeight.Text = "Ignore coin weight";
            this.chkCoinWeight.UseVisualStyleBackColor = true;
            this.chkCoinWeight.CheckedChanged += new System.EventHandler(this.chkCoinWeight_CheckedChanged);
            // 
            // oColorDialog
            // 
            this.oColorDialog.Color = System.Drawing.Color.White;
            // 
            // SettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 434);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SettingsPage_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SettingsPage_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oMuteBox.ResumeLayout(false);
            this.oMuteBox.PerformLayout();
            this.oAutosaveBox.ResumeLayout(false);
            this.oAutosaveBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkAutosave)).EndInit();
            this.oFormatBox.ResumeLayout(false);
            this.oFormatBox.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.oWeightBox.ResumeLayout(false);
            this.oWeightBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox oMuteBox;
        private System.Windows.Forms.CheckBox chkMute;
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
        private System.Windows.Forms.GroupBox oWeightBox;
        private System.Windows.Forms.CheckBox chkEncumbrance;
        private System.Windows.Forms.CheckBox chkCoinWeight;
    }
}