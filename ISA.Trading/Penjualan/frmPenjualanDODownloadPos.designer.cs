﻿namespace ISA.Trading.Penjualan
{
    partial class frmPenjualanDODownloadPos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPenjualanDODownloadPos));
            this.gvDownload1 = new ISA.Trading.Controls.CustomGridView();
            this.lblInfo1 = new System.Windows.Forms.Label();
            this.cmdDownload = new ISA.Trading.Controls.CommandButton();
            this.cmdClose = new ISA.Trading.Controls.CommandButton();
            this.lblDownloadCount1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pbDownload1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.gvDownload1)).BeginInit();
            this.SuspendLayout();
            // 
            // gvDownload1
            // 
            this.gvDownload1.AllowUserToAddRows = false;
            this.gvDownload1.AllowUserToDeleteRows = false;
            this.gvDownload1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvDownload1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDownload1.Location = new System.Drawing.Point(9, 100);
            this.gvDownload1.MultiSelect = false;
            this.gvDownload1.Name = "gvDownload1";
            this.gvDownload1.ReadOnly = true;
            this.gvDownload1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gvDownload1.Size = new System.Drawing.Size(902, 411);
            this.gvDownload1.StandardTab = true;
            this.gvDownload1.TabIndex = 7;
            // 
            // lblInfo1
            // 
            this.lblInfo1.AutoSize = true;
            this.lblInfo1.Location = new System.Drawing.Point(6, 83);
            this.lblInfo1.Name = "lblInfo1";
            this.lblInfo1.Size = new System.Drawing.Size(56, 14);
            this.lblInfo1.TabIndex = 8;
            this.lblInfo1.Text = "Htjtmp ";
            // 
            // cmdDownload
            // 
            this.cmdDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDownload.CommandType = ISA.Trading.Controls.CommandButton.enCommandType.Download;
            this.cmdDownload.Enabled = false;
            this.cmdDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cmdDownload.Image = ((System.Drawing.Image)(resources.GetObject("cmdDownload.Image")));
            this.cmdDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDownload.Location = new System.Drawing.Point(666, 573);
            this.cmdDownload.Name = "cmdDownload";
            this.cmdDownload.Size = new System.Drawing.Size(128, 40);
            this.cmdDownload.TabIndex = 2;
            this.cmdDownload.Text = "DOWNLOAD";
            this.cmdDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdDownload.UseVisualStyleBackColor = true;
            this.cmdDownload.Click += new System.EventHandler(this.cmdDonwload_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.CommandType = ISA.Trading.Controls.CommandButton.enCommandType.Close;
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClose.Location = new System.Drawing.Point(815, 573);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 40);
            this.cmdClose.TabIndex = 14;
            this.cmdClose.Text = "CLOSE";
            this.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lblDownloadCount1
            // 
            this.lblDownloadCount1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDownloadCount1.AutoSize = true;
            this.lblDownloadCount1.Location = new System.Drawing.Point(144, 553);
            this.lblDownloadCount1.Name = "lblDownloadCount1";
            this.lblDownloadCount1.Size = new System.Drawing.Size(28, 14);
            this.lblDownloadCount1.TabIndex = 17;
            this.lblDownloadCount1.Text = "0/0";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 553);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 14);
            this.label4.TabIndex = 16;
            this.label4.Text = "Htjtmp  Dowloaded:";
            // 
            // pbDownload1
            // 
            this.pbDownload1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDownload1.Location = new System.Drawing.Point(9, 523);
            this.pbDownload1.Name = "pbDownload1";
            this.pbDownload1.Size = new System.Drawing.Size(902, 23);
            this.pbDownload1.TabIndex = 13;
            // 
            // frmPenjualanDODownloadPos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(925, 622);
            this.Controls.Add(this.lblDownloadCount1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblInfo1);
            this.Controls.Add(this.pbDownload1);
            this.Controls.Add(this.gvDownload1);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdDownload);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmPenjualanDODownloadPos";
            this.Text = "Download Transaksi Dari Pos";
            this.Title = "Download Transaksi Dari Pos";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmPenjualanDODownloadPos_Load);
            this.Controls.SetChildIndex(this.cmdDownload, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.gvDownload1, 0);
            this.Controls.SetChildIndex(this.pbDownload1, 0);
            this.Controls.SetChildIndex(this.lblInfo1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.lblDownloadCount1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gvDownload1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ISA.Trading.Controls.CustomGridView gvDownload1;
        private System.Windows.Forms.Label lblInfo1;
        private ISA.Trading.Controls.CommandButton cmdDownload;
        private ISA.Trading.Controls.CommandButton cmdClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar pbDownload1;
        private System.Windows.Forms.Label lblDownloadCount1;
    }
}
