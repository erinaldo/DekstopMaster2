﻿namespace ISA.Controls
{
    partial class LookupBarcode
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
            this.tbBarcode = new System.Windows.Forms.TextBox();
            this.lKodebarcode = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbBarcode
            // 
            this.tbBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBarcode.Location = new System.Drawing.Point(8, 3);
            this.tbBarcode.Name = "tbBarcode";
            this.tbBarcode.Size = new System.Drawing.Size(249, 20);
            this.tbBarcode.TabIndex = 0;
            // 
            // lKodebarcode
            // 
            this.lKodebarcode.AutoSize = true;
            this.lKodebarcode.Location = new System.Drawing.Point(3, 26);
            this.lKodebarcode.Name = "lKodebarcode";
            this.lKodebarcode.Size = new System.Drawing.Size(98, 13);
            this.lKodebarcode.TabIndex = 2;
            this.lKodebarcode.Text = "[KODE BARCODE]";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackgroundImage = global::ISA.Controls.Properties.Resources.Search16;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Location = new System.Drawing.Point(262, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 23);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LookupBarcode
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lKodebarcode);
            this.Controls.Add(this.tbBarcode);
            this.Name = "LookupBarcode";
            this.Size = new System.Drawing.Size(314, 45);
            this.Load += new System.EventHandler(this.LookupBarcode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public  System.Windows.Forms.TextBox tbBarcode;
        private System.Windows.Forms.Label lKodebarcode;
        private System.Windows.Forms.Button button1;
    }
}
