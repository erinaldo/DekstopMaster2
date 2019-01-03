﻿namespace ISA.Finance.DKNForm
{
    partial class frmDKNupdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDKNupdate));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateDKN = new ISA.Controls.DateTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNoDKN = new ISA.Controls.CommonTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.optKredit = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.optDebet = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numJumlah = new ISA.Controls.NumericTextBox();
            this.txtUraian = new ISA.Controls.CommonTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lookupToko1 = new ISA.Controls.LookupToko();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCollectorID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCollector = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmdCLOSE = new ISA.Controls.CommandButton();
            this.cmdSAVE = new ISA.Controls.CommandButton();
            this.txtGudang = new ISA.Controls.CommonTextBox();
            this.lookupBankAsal1 = new ISA.Finance.Controls.LookupBankAsal();
            this.lookupBankTujuan = new ISA.Finance.Controls.LookupBank();
            this.txtPerkiraan = new ISA.Finance.Controls.LookupPerkiraan();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtGudang);
            this.groupBox2.Controls.Add(this.dateDKN);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtNoDKN);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.optKredit);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.optDebet);
            this.groupBox2.Location = new System.Drawing.Point(12, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(504, 101);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Header";
            // 
            // dateDKN
            // 
            this.dateDKN.DateValue = null;
            this.dateDKN.Location = new System.Drawing.Point(140, 18);
            this.dateDKN.MaxLength = 10;
            this.dateDKN.Name = "dateDKN";
            this.dateDKN.Size = new System.Drawing.Size(80, 20);
            this.dateDKN.TabIndex = 0;
            this.dateDKN.Validating += new System.ComponentModel.CancelEventHandler(this.dateDKN_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tanggal DKN";
            // 
            // txtNoDKN
            // 
            this.txtNoDKN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNoDKN.Location = new System.Drawing.Point(362, 18);
            this.txtNoDKN.Name = "txtNoDKN";
            this.txtNoDKN.Size = new System.Drawing.Size(128, 20);
            this.txtNoDKN.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 14);
            this.label4.TabIndex = 11;
            this.label4.Text = "Jenis DKN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(286, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nomor DKN";
            // 
            // optKredit
            // 
            this.optKredit.AutoSize = true;
            this.optKredit.Checked = true;
            this.optKredit.Location = new System.Drawing.Point(235, 72);
            this.optKredit.Name = "optKredit";
            this.optKredit.Size = new System.Drawing.Size(85, 18);
            this.optKredit.TabIndex = 4;
            this.optKredit.TabStop = true;
            this.optKredit.Text = "Kredit nota";
            this.optKredit.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 8;
            this.label3.Text = "Gudang";
            // 
            // optDebet
            // 
            this.optDebet.AutoSize = true;
            this.optDebet.Location = new System.Drawing.Point(140, 72);
            this.optDebet.Name = "optDebet";
            this.optDebet.Size = new System.Drawing.Size(84, 18);
            this.optDebet.TabIndex = 3;
            this.optDebet.Text = "Debet nota";
            this.optDebet.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numJumlah);
            this.groupBox1.Controls.Add(this.txtUraian);
            this.groupBox1.Controls.Add(this.txtPerkiraan);
            this.groupBox1.Location = new System.Drawing.Point(12, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(504, 130);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 14);
            this.label8.TabIndex = 7;
            this.label8.Text = "Jumlah";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "Uraian";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "Kode perkiraan";
            // 
            // numJumlah
            // 
            this.numJumlah.Location = new System.Drawing.Point(140, 98);
            this.numJumlah.Name = "numJumlah";
            this.numJumlah.Size = new System.Drawing.Size(100, 20);
            this.numJumlah.TabIndex = 2;
            this.numJumlah.Text = "0";
            this.numJumlah.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtUraian
            // 
            this.txtUraian.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUraian.Location = new System.Drawing.Point(140, 73);
            this.txtUraian.Name = "txtUraian";
            this.txtUraian.Size = new System.Drawing.Size(351, 20);
            this.txtUraian.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.lookupToko1);
            this.groupBox3.Controls.Add(this.lookupBankAsal1);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.lookupBankTujuan);
            this.groupBox3.Controls.Add(this.txtCollectorID);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtCollector);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(12, 266);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(504, 217);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 162);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 14);
            this.label11.TabIndex = 12;
            this.label11.Text = "Nama Toko";
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
            this.lookupToko1.Location = new System.Drawing.Point(137, 157);
            this.lookupToko1.LookUpType = ISA.Controls.LookupToko.EnumLookUpType.Aktif;
            this.lookupToko1.NamaToko = "";
            this.lookupToko1.Name = "lookupToko1";
            this.lookupToko1.Pasif = false;
            this.lookupToko1.Penanggungjawab = "";
            this.lookupToko1.Plafon = 0;
            this.lookupToko1.RowID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.lookupToko1.Size = new System.Drawing.Size(300, 54);
            this.lookupToko1.TabIndex = 4;
            this.lookupToko1.Telp = "";
            this.lookupToko1.TokoID = null;
            this.lookupToko1.WilID = null;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "Bank Tujuan";
            // 
            // txtCollectorID
            // 
            this.txtCollectorID.Location = new System.Drawing.Point(311, 18);
            this.txtCollectorID.Name = "txtCollectorID";
            this.txtCollectorID.Size = new System.Drawing.Size(163, 20);
            this.txtCollectorID.TabIndex = 1;
            this.txtCollectorID.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 14);
            this.label9.TabIndex = 6;
            this.label9.Text = "Bank Asal";
            // 
            // txtCollector
            // 
            this.txtCollector.Location = new System.Drawing.Point(140, 18);
            this.txtCollector.Name = "txtCollector";
            this.txtCollector.Size = new System.Drawing.Size(163, 20);
            this.txtCollector.TabIndex = 0;
            this.txtCollector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCollector_KeyDown_1);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 14);
            this.label10.TabIndex = 5;
            this.label10.Text = "Collector";
            // 
            // cmdCLOSE
            // 
            this.cmdCLOSE.CommandType = ISA.Controls.CommandButton.enCommandType.Close;
            this.cmdCLOSE.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cmdCLOSE.Image = ((System.Drawing.Image)(resources.GetObject("cmdCLOSE.Image")));
            this.cmdCLOSE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCLOSE.Location = new System.Drawing.Point(417, 492);
            this.cmdCLOSE.Name = "cmdCLOSE";
            this.cmdCLOSE.Size = new System.Drawing.Size(100, 40);
            this.cmdCLOSE.TabIndex = 9;
            this.cmdCLOSE.Text = "CLOSE";
            this.cmdCLOSE.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCLOSE.UseVisualStyleBackColor = true;
            this.cmdCLOSE.Click += new System.EventHandler(this.cmdCLOSE_Click_1);
            // 
            // cmdSAVE
            // 
            this.cmdSAVE.CommandType = ISA.Controls.CommandButton.enCommandType.Save;
            this.cmdSAVE.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cmdSAVE.Image = ((System.Drawing.Image)(resources.GetObject("cmdSAVE.Image")));
            this.cmdSAVE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSAVE.Location = new System.Drawing.Point(11, 492);
            this.cmdSAVE.Name = "cmdSAVE";
            this.cmdSAVE.Size = new System.Drawing.Size(100, 40);
            this.cmdSAVE.TabIndex = 8;
            this.cmdSAVE.Text = "SAVE";
            this.cmdSAVE.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdSAVE.UseVisualStyleBackColor = true;
            this.cmdSAVE.Click += new System.EventHandler(this.cmdSAVE_Click);
            // 
            // txtGudang
            // 
            this.txtGudang.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGudang.Enabled = false;
            this.txtGudang.Location = new System.Drawing.Point(140, 44);
            this.txtGudang.Name = "txtGudang";
            this.txtGudang.ReadOnly = true;
            this.txtGudang.Size = new System.Drawing.Size(80, 20);
            this.txtGudang.TabIndex = 12;
            // 
            // lookupBankAsal1
            // 
            this.lookupBankAsal1.Location = new System.Drawing.Point(140, 104);
            this.lookupBankAsal1.Lokasi = "[LOKASI]";
            this.lookupBankAsal1.NamaBank = "";
            this.lookupBankAsal1.Name = "lookupBankAsal1";
            this.lookupBankAsal1.Size = new System.Drawing.Size(154, 45);
            this.lookupBankAsal1.TabIndex = 3;
            // 
            // lookupBankTujuan
            // 
            this.lookupBankTujuan.BankID = "[CODE]";
            this.lookupBankTujuan.Location = new System.Drawing.Point(140, 49);
            this.lookupBankTujuan.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.lookupBankTujuan.NamaBank = "";
            this.lookupBankTujuan.Name = "lookupBankTujuan";
            this.lookupBankTujuan.RowID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.lookupBankTujuan.Size = new System.Drawing.Size(294, 59);
            this.lookupBankTujuan.TabIndex = 2;
            // 
            // txtPerkiraan
            // 
            this.txtPerkiraan.Location = new System.Drawing.Point(140, 19);
            this.txtPerkiraan.Margin = new System.Windows.Forms.Padding(0);
            this.txtPerkiraan.NamaPerkiraan = "";
            this.txtPerkiraan.Name = "txtPerkiraan";
            this.txtPerkiraan.NoPerkiraan = "[CODE]";
            this.txtPerkiraan.Size = new System.Drawing.Size(234, 47);
            this.txtPerkiraan.TabIndex = 0;
            // 
            // frmDKNupdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(527, 553);
            this.Controls.Add(this.cmdCLOSE);
            this.Controls.Add(this.cmdSAVE);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Location = new System.Drawing.Point(400, 75);
            this.Name = "frmDKNupdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "";
            this.Title = "";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmDKNupdate_Load);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.cmdSAVE, 0);
            this.Controls.SetChildIndex(this.cmdCLOSE, 0);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private ISA.Controls.DateTextBox dateDKN;
        private System.Windows.Forms.Label label1;
        private ISA.Controls.CommonTextBox txtNoDKN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton optKredit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton optDebet;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private ISA.Controls.NumericTextBox numJumlah;
        private ISA.Controls.CommonTextBox txtUraian;
        private ISA.Finance.Controls.LookupPerkiraan txtPerkiraan;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private ISA.Controls.LookupToko lookupToko1;
        private ISA.Finance.Controls.LookupBankAsal lookupBankAsal1;
        private System.Windows.Forms.Label label5;
        private ISA.Finance.Controls.LookupBank lookupBankTujuan;
        private System.Windows.Forms.TextBox txtCollectorID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCollector;
        private System.Windows.Forms.Label label10;
        private ISA.Controls.CommandButton cmdCLOSE;
        private ISA.Controls.CommandButton cmdSAVE;
        private ISA.Controls.CommonTextBox txtGudang;

    }
}
