﻿namespace ISA.Trading.Penjualan
{
    partial class frmRptlaporanRefilToko
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRptlaporanRefilToko));
            this.label1 = new System.Windows.Forms.Label();
            this.rangeDateBoxPenjualan = new ISA.Trading.Controls.RangeDateBox();
            this.commandButton2 = new ISA.Trading.Controls.CommandButton();
            this.cmdyes = new ISA.Trading.Controls.CommandButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "Tanggal";
            // 
            // rangeDateBoxPenjualan
            // 
            this.rangeDateBoxPenjualan.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.rangeDateBoxPenjualan.FromDate = null;
            this.rangeDateBoxPenjualan.Location = new System.Drawing.Point(116, 92);
            this.rangeDateBoxPenjualan.Name = "rangeDateBoxPenjualan";
            this.rangeDateBoxPenjualan.Size = new System.Drawing.Size(255, 24);
            this.rangeDateBoxPenjualan.TabIndex = 12;
            this.rangeDateBoxPenjualan.ToDate = null;
            // 
            // commandButton2
            // 
            this.commandButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.commandButton2.CommandType = ISA.Trading.Controls.CommandButton.enCommandType.Close;
            this.commandButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.commandButton2.Image = ((System.Drawing.Image)(resources.GetObject("commandButton2.Image")));
            this.commandButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.commandButton2.Location = new System.Drawing.Point(281, 177);
            this.commandButton2.Name = "commandButton2";
            this.commandButton2.ReportName = "";
            this.commandButton2.Size = new System.Drawing.Size(100, 40);
            this.commandButton2.TabIndex = 15;
            this.commandButton2.Text = "CLOSE";
            this.commandButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.commandButton2.UseVisualStyleBackColor = true;
            this.commandButton2.Click += new System.EventHandler(this.commandButton2_Click);
            // 
            // cmdyes
            // 
            this.cmdyes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdyes.CommandType = ISA.Trading.Controls.CommandButton.enCommandType.Print;
            this.cmdyes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cmdyes.Image = ((System.Drawing.Image)(resources.GetObject("cmdyes.Image")));
            this.cmdyes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdyes.Location = new System.Drawing.Point(28, 177);
            this.cmdyes.Name = "cmdyes";
            this.cmdyes.ReportName = "";
            this.cmdyes.Size = new System.Drawing.Size(100, 40);
            this.cmdyes.TabIndex = 14;
            this.cmdyes.Text = "PRINT";
            this.cmdyes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdyes.UseVisualStyleBackColor = true;
            this.cmdyes.Click += new System.EventHandler(this.cmdyes_Click);
            // 
            // frmRptlaporanRefilToko
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 282);
            this.Controls.Add(this.commandButton2);
            this.Controls.Add(this.cmdyes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rangeDateBoxPenjualan);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmRptlaporanRefilToko";
            this.Tag = "";
            this.Text = "LAPORAN REFIL TOKO";
            this.Title = "LAPORAN REFIL TOKO";
            this.Load += new System.EventHandler(this.frmRptlaporanRefilToko_Load);
            this.Controls.SetChildIndex(this.rangeDateBoxPenjualan, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmdyes, 0);
            this.Controls.SetChildIndex(this.commandButton2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ISA.Trading.Controls.RangeDateBox rangeDateBoxPenjualan;
        private ISA.Trading.Controls.CommandButton commandButton2;
        private ISA.Trading.Controls.CommandButton cmdyes;
    }
}