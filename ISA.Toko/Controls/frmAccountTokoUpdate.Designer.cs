﻿namespace ISA.Toko.Controls
{
    partial class frmAccountTokoUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccountTokoUpdate));
            this.lookupToko1 = new ISA.Controls.LookupToko();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNoAcc = new ISA.Controls.CommonTextBox();
            this.cmdSave = new ISA.Controls.CommandButton();
            this.cmdClose = new ISA.Controls.CommandButton();
            this.SuspendLayout();
            // 
            // lookupToko1
            // 
            this.lookupToko1.Alamat = null;
            this.lookupToko1.Catatan = "";
            this.lookupToko1.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.lookupToko1.Grade = "";
            this.lookupToko1.HariKirim = 0;
            this.lookupToko1.HariSales = 0;
            this.lookupToko1.KodeToko = "[CODE]";
            this.lookupToko1.Kota = null;
            this.lookupToko1.Location = new System.Drawing.Point(119, 28);
            this.lookupToko1.NamaToko = "";
            this.lookupToko1.Name = "lookupToko1";
            this.lookupToko1.Penanggungjawab = "";
            this.lookupToko1.Plafon = 0;
            this.lookupToko1.RowID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.lookupToko1.Size = new System.Drawing.Size(300, 54);
            this.lookupToko1.TabIndex = 0;
            this.lookupToko1.Telp = "";
            this.lookupToko1.TokoID = null;
            this.lookupToko1.WilID = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nama Toko";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "No Account";
            // 
            // tbNoAcc
            // 
            this.tbNoAcc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbNoAcc.Location = new System.Drawing.Point(122, 91);
            this.tbNoAcc.Name = "tbNoAcc";
            this.tbNoAcc.Size = new System.Drawing.Size(167, 20);
            this.tbNoAcc.TabIndex = 1;
            // 
            // cmdSave
            // 
            this.cmdSave.CommandType = ISA.Controls.CommandButton.enCommandType.Save;
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSave.Location = new System.Drawing.Point(213, 121);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(100, 40);
            this.cmdSave.TabIndex = 2;
            this.cmdSave.Text = "SAVE";
            this.cmdSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.CommandType = ISA.Controls.CommandButton.enCommandType.Close;
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClose.Location = new System.Drawing.Point(319, 121);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 40);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "CLOSE";
            this.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmAccountTokoUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(447, 173);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.tbNoAcc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lookupToko1);
            this.Name = "frmAccountTokoUpdate";
            this.Text = "Tambah Account Toko";
            this.Load += new System.EventHandler(this.frmAccountTokoUpdate_Load);
            this.Controls.SetChildIndex(this.lookupToko1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.tbNoAcc, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ISA.Controls.LookupToko lookupToko1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ISA.Controls.CommonTextBox tbNoAcc;
        private ISA.Controls.CommandButton cmdSave;
        private ISA.Controls.CommandButton cmdClose;
    }
}