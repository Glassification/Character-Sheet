namespace MyCharacterSheet
{
    partial class EasterEggPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EasterEggPage));
            this.oPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.oPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // oPictureBox
            // 
            this.oPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oPictureBox.Image = global::MyCharacterSheet.Properties.Resources.He_Man;
            this.oPictureBox.Location = new System.Drawing.Point(0, 0);
            this.oPictureBox.Name = "oPictureBox";
            this.oPictureBox.Size = new System.Drawing.Size(480, 321);
            this.oPictureBox.TabIndex = 0;
            this.oPictureBox.TabStop = false;
            // 
            // EasterEggPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 321);
            this.Controls.Add(this.oPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "EasterEggPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Easter Egg";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EasterEggPage_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.oPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox oPictureBox;
    }
}