﻿namespace ISA.Toko.VACCDO_Pos
{
    partial class frmRptDOBlmACCFilterPos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRptDOBlmACCFilterPos));
            this.rgbTglDO = new ISA.Toko.Controls.RangeDateBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtC1 = new ISA.Toko.Controls.CommonTextBox();
            this.cmdYES = new ISA.Toko.Controls.CommandButton();
            this.cmdCLOSE = new ISA.Toko.Controls.CommandButton();
            this.lookupPostArea = new ISA.Toko.Controls.LookupPostArea();
            this.SuspendLayout();
            // 
            // rgbTglDO
            // 
            this.rgbTglDO.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.rgbTglDO.FromDate = null;
            this.rgbTglDO.Location = new System.Drawing.Point(90, 65);
            this.rgbTglDO.Name = "rgbTglDO";
            this.rgbTglDO.Size = new System.Drawing.Size(257, 22);
            this.rgbTglDO.TabIndex = 0;
            this.rgbTglDO.ToDate = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "Tgl. DO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 14);
            this.label2.TabIndex = 9;
            this.label2.Text = "C1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 14);
            this.label3.TabIndex = 10;
            this.label3.Text = "POS";
            // 
            // txtC1
            // 
            this.txtC1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtC1.Location = new System.Drawing.Point(97, 97);
            this.txtC1.Name = "txtC1";
            this.txtC1.Size = new System.Drawing.Size(34, 20);
            this.txtC1.TabIndex = 1;
            // 
            // cmdYES
            // 
            this.cmdYES.CommandType = ISA.Toko.Controls.CommandButton.enCommandType.Yes;
            this.cmdYES.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cmdYES.Image = ((System.Drawing.Image)(resources.GetObject("cmdYES.Image")));
            this.cmdYES.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdYES.Location = new System.Drawing.Point(97, 159);
            this.cmdYES.Name = "cmdYES";
            this.cmdYES.Size = new System.Drawing.Size(100, 40);
            this.cmdYES.TabIndex = 3;
            this.cmdYES.Text = "YES";
            this.cmdYES.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdYES.UseVisualStyleBackColor = true;
            this.cmdYES.Click += new System.EventHandler(this.cmdYES_Click);
            // 
            // cmdCLOSE
            // 
            this.cmdCLOSE.CommandType = ISA.Toko.Controls.CommandButton.enCommandType.Close;
            this.cmdCLOSE.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cmdCLOSE.Image = ((System.Drawing.Image)(resources.GetObject("cmdCLOSE.Image")));
            this.cmdCLOSE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCLOSE.Location = new System.Drawing.Point(220, 159);
            this.cmdCLOSE.Name = "cmdCLOSE";
            this.cmdCLOSE.Size = new System.Drawing.Size(100, 40);
            this.cmdCLOSE.TabIndex = 4;
            this.cmdCLOSE.Text = "CLOSE";
            this.cmdCLOSE.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCLOSE.UseVisualStyleBackColor = true;
            this.cmdCLOSE.Click += new System.EventHandler(this.cmdCLOSE_Click);
            // 
            // lookupPostArea
            // 
            this.lookupPostArea.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.lookupPostArea.Location = new System.Drawing.Point(93, 125);
            this.lookupPostArea.Name = "lookupPostArea";
            this.lookupPostArea.PostID = "";
            this.lookupPostArea.PostName = null;
            this.lookupPostArea.Size = new System.Drawing.Size(233, 28);
            this.lookupPostArea.TabIndex = 2;
            // 
            // frmRptDOBlmACCFilterPos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(372, 229);
            this.Controls.Add(this.lookupPostArea);
            this.Controls.Add(this.cmdCLOSE);
            this.Controls.Add(this.cmdYES);
            this.Controls.Add(this.txtC1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rgbTglDO);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximumSize = new System.Drawing.Size(380, 256);
            this.MinimumSize = new System.Drawing.Size(380, 256);
            this.Name = "frmRptDOBlmACCFilterPos";
            this.Text = "Belum ACC DO Pos";
            this.Title = "Belum ACC DO Pos";
            this.Load += new System.EventHandler(this.frmRptDOBlmACCFilter_Load);
            this.Controls.SetChildIndex(this.rgbTglDO, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtC1, 0);
            this.Controls.SetChildIndex(this.cmdYES, 0);
            this.Controls.SetChildIndex(this.cmdCLOSE, 0);
            this.Controls.SetChildIndex(this.lookupPostArea, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ISA.Toko.Controls.RangeDateBox rgbTglDO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ISA.Toko.Controls.CommonTextBox txtC1;
        private ISA.Toko.Controls.CommandButton cmdYES;
        private ISA.Toko.Controls.CommandButton cmdCLOSE;
        private ISA.Toko.Controls.LookupPostArea lookupPostArea;
    }
}
