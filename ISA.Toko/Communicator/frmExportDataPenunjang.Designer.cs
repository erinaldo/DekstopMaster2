﻿namespace ISA.Toko.Communicator
{
    partial class frmExportDataPenunjang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportDataPenunjang));
            this.pbSyncUpload = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.rangeDateBox1 = new ISA.Toko.Controls.RangeDateBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUpload = new System.Windows.Forms.Label();
            this.lblUploadCount = new System.Windows.Forms.Label();
            this.cmdUpload = new ISA.Toko.Controls.CommandButton();
            this.cmdClose = new ISA.Toko.Controls.CommandButton();
            this.SuspendLayout();
            // 
            // pbSyncUpload
            // 
            this.pbSyncUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSyncUpload.Location = new System.Drawing.Point(9, 181);
            this.pbSyncUpload.Name = "pbSyncUpload";
            this.pbSyncUpload.Size = new System.Drawing.Size(691, 23);
            this.pbSyncUpload.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "PERIODE";
            // 
            // rangeDateBox1
            // 
            this.rangeDateBox1.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.rangeDateBox1.FromDate = null;
            this.rangeDateBox1.Location = new System.Drawing.Point(93, 109);
            this.rangeDateBox1.Name = "rangeDateBox1";
            this.rangeDateBox1.Size = new System.Drawing.Size(257, 22);
            this.rangeDateBox1.TabIndex = 7;
            this.rangeDateBox1.ToDate = null;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "Table Upload:";
            // 
            // lblUpload
            // 
            this.lblUpload.AutoSize = true;
            this.lblUpload.Location = new System.Drawing.Point(113, 225);
            this.lblUpload.Name = "lblUpload";
            this.lblUpload.Size = new System.Drawing.Size(22, 14);
            this.lblUpload.TabIndex = 9;
            this.lblUpload.Text = "0/7";
            // 
            // lblUploadCount
            // 
            this.lblUploadCount.AutoSize = true;
            this.lblUploadCount.Location = new System.Drawing.Point(595, 225);
            this.lblUploadCount.Name = "lblUploadCount";
            this.lblUploadCount.Size = new System.Drawing.Size(40, 14);
            this.lblUploadCount.TabIndex = 10;
            this.lblUploadCount.Text = "Count";
            // 
            // cmdUpload
            // 
            this.cmdUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdUpload.CommandType = ISA.Toko.Controls.CommandButton.enCommandType.Upload;
            this.cmdUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cmdUpload.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpload.Image")));
            this.cmdUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdUpload.Location = new System.Drawing.Point(9, 291);
            this.cmdUpload.Name = "cmdUpload";
            this.cmdUpload.Size = new System.Drawing.Size(128, 40);
            this.cmdUpload.TabIndex = 11;
            this.cmdUpload.Text = "UPLOAD";
            this.cmdUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdUpload.UseVisualStyleBackColor = true;
            this.cmdUpload.Click += new System.EventHandler(this.cmdUpload_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.CommandType = ISA.Toko.Controls.CommandButton.enCommandType.Close;
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClose.Location = new System.Drawing.Point(600, 291);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 40);
            this.cmdClose.TabIndex = 12;
            this.cmdClose.Text = "CLOSE";
            this.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmExportDataPenunjang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(710, 341);
            this.Controls.Add(this.cmdUpload);
            this.Controls.Add(this.lblUploadCount);
            this.Controls.Add(this.lblUpload);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rangeDateBox1);
            this.Controls.Add(this.pbSyncUpload);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmExportDataPenunjang";
            this.Text = "EXPORT DATA PENUNJANG";
            this.Title = "EXPORT DATA PENUNJANG";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmExportDataPenunjang_Load);
            this.Controls.SetChildIndex(this.pbSyncUpload, 0);
            this.Controls.SetChildIndex(this.rangeDateBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.lblUpload, 0);
            this.Controls.SetChildIndex(this.lblUploadCount, 0);
            this.Controls.SetChildIndex(this.cmdUpload, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbSyncUpload;
        private System.Windows.Forms.Label label1;
        private ISA.Toko.Controls.RangeDateBox rangeDateBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUpload;
        private System.Windows.Forms.Label lblUploadCount;
        private ISA.Toko.Controls.CommandButton cmdUpload;
        private ISA.Toko.Controls.CommandButton cmdClose;
    }
}