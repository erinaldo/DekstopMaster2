﻿namespace ISA.Finance.Kasir
{
    partial class frmIndenUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIndenUpdate));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbNoBukti = new ISA.Controls.CommonTextBox();
            this.tbCollector = new ISA.Controls.CommonTextBox();
            this.tbMengetahui = new ISA.Controls.CommonTextBox();
            this.tbKasir = new ISA.Controls.CommonTextBox();
            this.tbTanggal = new ISA.Controls.DateTextBox();
            this.tbTunai = new ISA.Controls.NumericTextBox();
            this.tbTransfer = new ISA.Controls.NumericTextBox();
            this.tbGiro = new ISA.Controls.NumericTextBox();
            this.tbCrd = new ISA.Controls.NumericTextBox();
            this.tbDbt = new ISA.Controls.NumericTextBox();
            this.cmdSave = new ISA.Controls.CommandButton();
            this.cmdClose = new ISA.Controls.CommandButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "No. Bukti";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tanggal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Collector";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "Pembayaran Tunai";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "Pembayaran Transfer";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 14);
            this.label6.TabIndex = 8;
            this.label6.Text = "Pembayaran Giro";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 14);
            this.label7.TabIndex = 9;
            this.label7.Text = "Pembayaran Crd. Card";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 253);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 14);
            this.label8.TabIndex = 10;
            this.label8.Text = "Pembayaran Dbt. Card";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 283);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 14);
            this.label9.TabIndex = 11;
            this.label9.Text = "Kasir";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 316);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 14);
            this.label10.TabIndex = 12;
            this.label10.Text = "Mengetahui";
            // 
            // tbNoBukti
            // 
            this.tbNoBukti.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbNoBukti.Enabled = false;
            this.tbNoBukti.Location = new System.Drawing.Point(171, 25);
            this.tbNoBukti.Name = "tbNoBukti";
            this.tbNoBukti.Size = new System.Drawing.Size(146, 20);
            this.tbNoBukti.TabIndex = 13;
            // 
            // tbCollector
            // 
            this.tbCollector.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbCollector.Location = new System.Drawing.Point(171, 85);
            this.tbCollector.Name = "tbCollector";
            this.tbCollector.Size = new System.Drawing.Size(290, 20);
            this.tbCollector.TabIndex = 1;
            this.tbCollector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCollector_KeyDown);
            // 
            // tbMengetahui
            // 
            this.tbMengetahui.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbMengetahui.Location = new System.Drawing.Point(171, 310);
            this.tbMengetahui.MaxLength = 15;
            this.tbMengetahui.Name = "tbMengetahui";
            this.tbMengetahui.Size = new System.Drawing.Size(290, 20);
            this.tbMengetahui.TabIndex = 3;
            // 
            // tbKasir
            // 
            this.tbKasir.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbKasir.Enabled = false;
            this.tbKasir.Location = new System.Drawing.Point(171, 277);
            this.tbKasir.Name = "tbKasir";
            this.tbKasir.Size = new System.Drawing.Size(146, 20);
            this.tbKasir.TabIndex = 2;
            // 
            // tbTanggal
            // 
            this.tbTanggal.DateValue = null;
            this.tbTanggal.Location = new System.Drawing.Point(171, 54);
            this.tbTanggal.MaxLength = 10;
            this.tbTanggal.Name = "tbTanggal";
            this.tbTanggal.Size = new System.Drawing.Size(100, 20);
            this.tbTanggal.TabIndex = 0;
            this.tbTanggal.Validating += new System.ComponentModel.CancelEventHandler(this.tbTanggal_Validating);
            // 
            // tbTunai
            // 
            this.tbTunai.Enabled = false;
            this.tbTunai.Location = new System.Drawing.Point(171, 115);
            this.tbTunai.Name = "tbTunai";
            this.tbTunai.Size = new System.Drawing.Size(100, 20);
            this.tbTunai.TabIndex = 18;
            this.tbTunai.Text = "0";
            // 
            // tbTransfer
            // 
            this.tbTransfer.Enabled = false;
            this.tbTransfer.Location = new System.Drawing.Point(171, 145);
            this.tbTransfer.Name = "tbTransfer";
            this.tbTransfer.Size = new System.Drawing.Size(100, 20);
            this.tbTransfer.TabIndex = 19;
            this.tbTransfer.Text = "0";
            // 
            // tbGiro
            // 
            this.tbGiro.Enabled = false;
            this.tbGiro.Location = new System.Drawing.Point(171, 176);
            this.tbGiro.Name = "tbGiro";
            this.tbGiro.Size = new System.Drawing.Size(100, 20);
            this.tbGiro.TabIndex = 20;
            this.tbGiro.Text = "0";
            // 
            // tbCrd
            // 
            this.tbCrd.Enabled = false;
            this.tbCrd.Location = new System.Drawing.Point(171, 211);
            this.tbCrd.Name = "tbCrd";
            this.tbCrd.Size = new System.Drawing.Size(100, 20);
            this.tbCrd.TabIndex = 21;
            this.tbCrd.Text = "0";
            // 
            // tbDbt
            // 
            this.tbDbt.Enabled = false;
            this.tbDbt.Location = new System.Drawing.Point(171, 248);
            this.tbDbt.Name = "tbDbt";
            this.tbDbt.Size = new System.Drawing.Size(100, 20);
            this.tbDbt.TabIndex = 22;
            this.tbDbt.Text = "0";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdSave.CommandType = ISA.Controls.CommandButton.enCommandType.Save;
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSave.Location = new System.Drawing.Point(130, 370);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(100, 40);
            this.cmdSave.TabIndex = 4;
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
            this.cmdClose.Location = new System.Drawing.Point(265, 370);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 40);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "CLOSE";
            this.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmIndenUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(517, 422);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.tbDbt);
            this.Controls.Add(this.tbCrd);
            this.Controls.Add(this.tbGiro);
            this.Controls.Add(this.tbTransfer);
            this.Controls.Add(this.tbTunai);
            this.Controls.Add(this.tbTanggal);
            this.Controls.Add(this.tbKasir);
            this.Controls.Add(this.tbMengetahui);
            this.Controls.Add(this.tbCollector);
            this.Controls.Add(this.tbNoBukti);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmIndenUpdate";
            this.Load += new System.EventHandler(this.frmIndenUpdate_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.tbNoBukti, 0);
            this.Controls.SetChildIndex(this.tbCollector, 0);
            this.Controls.SetChildIndex(this.tbMengetahui, 0);
            this.Controls.SetChildIndex(this.tbKasir, 0);
            this.Controls.SetChildIndex(this.tbTanggal, 0);
            this.Controls.SetChildIndex(this.tbTunai, 0);
            this.Controls.SetChildIndex(this.tbTransfer, 0);
            this.Controls.SetChildIndex(this.tbGiro, 0);
            this.Controls.SetChildIndex(this.tbCrd, 0);
            this.Controls.SetChildIndex(this.tbDbt, 0);
            this.Controls.SetChildIndex(this.cmdSave, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private ISA.Controls.CommonTextBox tbNoBukti;
        private ISA.Controls.CommonTextBox tbCollector;
        private ISA.Controls.CommonTextBox tbMengetahui;
        private ISA.Controls.CommonTextBox tbKasir;
        private ISA.Controls.DateTextBox tbTanggal;
        private ISA.Controls.NumericTextBox tbTunai;
        private ISA.Controls.NumericTextBox tbTransfer;
        private ISA.Controls.NumericTextBox tbGiro;
        private ISA.Controls.NumericTextBox tbCrd;
        private ISA.Controls.NumericTextBox tbDbt;
        private ISA.Controls.CommandButton cmdSave;
        private ISA.Controls.CommandButton cmdClose;
    }
}
