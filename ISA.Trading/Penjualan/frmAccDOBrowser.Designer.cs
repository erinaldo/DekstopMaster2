﻿namespace ISA.Trading.Penjualan
{
    partial class frmAccDOBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccDOBrowser));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdClose = new ISA.Controls.CommandButton();
            this.cmdYes = new ISA.Controls.CommandButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.dataGridDO = new ISA.Trading.Controls.CustomGridView();
            this.RowID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoAccPusat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoAccPiutang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsOverdueBE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSalesBL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ovdBE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hrbe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoDo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TglDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodeSales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NamaToko = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Alamat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plf_fb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.piutang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GIT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Giro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiroTolak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SumRpNet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sisaPlf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ovdsbl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KodeToko = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdSearch = new ISA.Trading.Controls.CommandButton();
            this.rdbTglDO = new ISA.Trading.Controls.RangeDateBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDO)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 14);
            this.label5.TabIndex = 42;
            this.label5.Text = "Range tanggal DO :";
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.CommandType = ISA.Controls.CommandButton.enCommandType.Close;
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClose.Location = new System.Drawing.Point(672, 510);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 40);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "CLOSE";
            this.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdYes
            // 
            this.cmdYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdYes.CommandType = ISA.Controls.CommandButton.enCommandType.Yes;
            this.cmdYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.cmdYes.Image = ((System.Drawing.Image)(resources.GetObject("cmdYes.Image")));
            this.cmdYes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdYes.Location = new System.Drawing.Point(568, 510);
            this.cmdYes.Name = "cmdYes";
            this.cmdYes.Size = new System.Drawing.Size(100, 40);
            this.cmdYes.TabIndex = 3;
            this.cmdYes.Text = "YES";
            this.cmdYes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdYes.UseVisualStyleBackColor = true;
            this.cmdYes.Click += new System.EventHandler(this.cmdYes_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 481);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 14);
            this.label1.TabIndex = 43;
            this.label1.Text = "Plafon Acc Overdue";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 504);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 14);
            this.label2.TabIndex = 44;
            this.label2.Text = "Sudah digunakan";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 530);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 45;
            this.label3.Text = "Sisa Plafon";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(132, 481);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 47;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(132, 503);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 48;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(132, 530);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 49;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dataGridDO
            // 
            this.dataGridDO.AllowUserToAddRows = false;
            this.dataGridDO.AllowUserToDeleteRows = false;
            this.dataGridDO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridDO.BackgroundColor = System.Drawing.Color.White;
            this.dataGridDO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridDO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RowID,
            this.NoAccPusat,
            this.NoAccPiutang,
            this.IsOverdueBE,
            this.IsSalesBL,
            this.ovdBE,
            this.hrbe,
            this.NoDo,
            this.TglDO,
            this.KodeSales,
            this.NamaToko,
            this.Alamat,
            this.Kota,
            this.plf_fb,
            this.piutang,
            this.GIT,
            this.Giro,
            this.GiroTolak,
            this.SumRpNet,
            this.sisaPlf,
            this.ovdsbl,
            this.KodeToko});
            this.dataGridDO.Location = new System.Drawing.Point(12, 89);
            this.dataGridDO.MultiSelect = false;
            this.dataGridDO.Name = "dataGridDO";
            this.dataGridDO.ReadOnly = true;
            this.dataGridDO.RowHeadersVisible = false;
            this.dataGridDO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridDO.Size = new System.Drawing.Size(760, 384);
            this.dataGridDO.StandardTab = true;
            this.dataGridDO.TabIndex = 2;
            // 
            // RowID
            // 
            this.RowID.DataPropertyName = "RowID";
            this.RowID.HeaderText = "RowID";
            this.RowID.Name = "RowID";
            this.RowID.ReadOnly = true;
            this.RowID.Visible = false;
            // 
            // NoAccPusat
            // 
            this.NoAccPusat.DataPropertyName = "NoAccPusat";
            this.NoAccPusat.HeaderText = "No Acc Pusat";
            this.NoAccPusat.Name = "NoAccPusat";
            this.NoAccPusat.ReadOnly = true;
            this.NoAccPusat.Visible = false;
            // 
            // NoAccPiutang
            // 
            this.NoAccPiutang.DataPropertyName = "NoAccPiutang";
            this.NoAccPiutang.HeaderText = "No Acc Piutang";
            this.NoAccPiutang.Name = "NoAccPiutang";
            this.NoAccPiutang.ReadOnly = true;
            this.NoAccPiutang.Visible = false;
            // 
            // IsOverdueBE
            // 
            this.IsOverdueBE.DataPropertyName = "IsOverdueBE";
            this.IsOverdueBE.HeaderText = "Overdue?";
            this.IsOverdueBE.Name = "IsOverdueBE";
            this.IsOverdueBE.ReadOnly = true;
            this.IsOverdueBE.Visible = false;
            this.IsOverdueBE.Width = 70;
            // 
            // IsSalesBL
            // 
            this.IsSalesBL.DataPropertyName = "IsSalesBL";
            this.IsSalesBL.HeaderText = "Sales?";
            this.IsSalesBL.Name = "IsSalesBL";
            this.IsSalesBL.ReadOnly = true;
            this.IsSalesBL.Visible = false;
            this.IsSalesBL.Width = 60;
            // 
            // ovdBE
            // 
            this.ovdBE.DataPropertyName = "ovdBE";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            this.ovdBE.DefaultCellStyle = dataGridViewCellStyle1;
            this.ovdBE.HeaderText = "Overdue BE";
            this.ovdBE.Name = "ovdBE";
            this.ovdBE.ReadOnly = true;
            // 
            // hrbe
            // 
            this.hrbe.DataPropertyName = "hrbe";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            this.hrbe.DefaultCellStyle = dataGridViewCellStyle2;
            this.hrbe.HeaderText = "Lama Ovd BE (Hari)";
            this.hrbe.Name = "hrbe";
            this.hrbe.ReadOnly = true;
            // 
            // NoDo
            // 
            this.NoDo.DataPropertyName = "NoDo";
            this.NoDo.HeaderText = "No. DO";
            this.NoDo.Name = "NoDo";
            this.NoDo.ReadOnly = true;
            // 
            // TglDO
            // 
            this.TglDO.DataPropertyName = "TglDO";
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            this.TglDO.DefaultCellStyle = dataGridViewCellStyle3;
            this.TglDO.HeaderText = "Tgl. DO";
            this.TglDO.Name = "TglDO";
            this.TglDO.ReadOnly = true;
            // 
            // KodeSales
            // 
            this.KodeSales.DataPropertyName = "KodeSales";
            this.KodeSales.HeaderText = "Kode Sales";
            this.KodeSales.Name = "KodeSales";
            this.KodeSales.ReadOnly = true;
            // 
            // NamaToko
            // 
            this.NamaToko.DataPropertyName = "NamaToko";
            this.NamaToko.HeaderText = "Nama Toko";
            this.NamaToko.Name = "NamaToko";
            this.NamaToko.ReadOnly = true;
            this.NamaToko.Width = 400;
            // 
            // Alamat
            // 
            this.Alamat.DataPropertyName = "Alamat";
            this.Alamat.HeaderText = "Alamat";
            this.Alamat.Name = "Alamat";
            this.Alamat.ReadOnly = true;
            this.Alamat.Width = 400;
            // 
            // Kota
            // 
            this.Kota.DataPropertyName = "Kota";
            this.Kota.HeaderText = "Kota";
            this.Kota.Name = "Kota";
            this.Kota.ReadOnly = true;
            this.Kota.Width = 150;
            // 
            // plf_fb
            // 
            this.plf_fb.DataPropertyName = "plf_fb";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            this.plf_fb.DefaultCellStyle = dataGridViewCellStyle4;
            this.plf_fb.HeaderText = "Plafon";
            this.plf_fb.Name = "plf_fb";
            this.plf_fb.ReadOnly = true;
            // 
            // piutang
            // 
            this.piutang.DataPropertyName = "piutang";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            this.piutang.DefaultCellStyle = dataGridViewCellStyle5;
            this.piutang.HeaderText = "Piutang";
            this.piutang.Name = "piutang";
            this.piutang.ReadOnly = true;
            // 
            // GIT
            // 
            this.GIT.DataPropertyName = "GIT";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            this.GIT.DefaultCellStyle = dataGridViewCellStyle6;
            this.GIT.HeaderText = "GIT";
            this.GIT.Name = "GIT";
            this.GIT.ReadOnly = true;
            // 
            // Giro
            // 
            this.Giro.DataPropertyName = "Giro";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N0";
            this.Giro.DefaultCellStyle = dataGridViewCellStyle7;
            this.Giro.HeaderText = "Giro";
            this.Giro.Name = "Giro";
            this.Giro.ReadOnly = true;
            // 
            // GiroTolak
            // 
            this.GiroTolak.DataPropertyName = "GiroTolak";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N0";
            this.GiroTolak.DefaultCellStyle = dataGridViewCellStyle8;
            this.GiroTolak.HeaderText = "Giro Tolak";
            this.GiroTolak.Name = "GiroTolak";
            this.GiroTolak.ReadOnly = true;
            // 
            // SumRpNet
            // 
            this.SumRpNet.DataPropertyName = "SumRpNet";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N0";
            this.SumRpNet.DefaultCellStyle = dataGridViewCellStyle9;
            this.SumRpNet.HeaderText = "Rp Net DO";
            this.SumRpNet.Name = "SumRpNet";
            this.SumRpNet.ReadOnly = true;
            // 
            // sisaPlf
            // 
            this.sisaPlf.DataPropertyName = "sisaPlf";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N0";
            this.sisaPlf.DefaultCellStyle = dataGridViewCellStyle10;
            this.sisaPlf.HeaderText = "Sisa Plafon";
            this.sisaPlf.Name = "sisaPlf";
            this.sisaPlf.ReadOnly = true;
            // 
            // ovdsbl
            // 
            this.ovdsbl.DataPropertyName = "ovdsbl";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N0";
            dataGridViewCellStyle11.NullValue = null;
            this.ovdsbl.DefaultCellStyle = dataGridViewCellStyle11;
            this.ovdsbl.HeaderText = "Overdue SBL";
            this.ovdsbl.Name = "ovdsbl";
            this.ovdsbl.ReadOnly = true;
            this.ovdsbl.Visible = false;
            this.ovdsbl.Width = 120;
            // 
            // KodeToko
            // 
            this.KodeToko.DataPropertyName = "KodeToko";
            this.KodeToko.HeaderText = "Kode Toko";
            this.KodeToko.Name = "KodeToko";
            this.KodeToko.ReadOnly = true;
            // 
            // cmdSearch
            // 
            this.cmdSearch.CommandType = ISA.Trading.Controls.CommandButton.enCommandType.SearchS;
            this.cmdSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdSearch.Location = new System.Drawing.Point(399, 60);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 23);
            this.cmdSearch.TabIndex = 1;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // rdbTglDO
            // 
            this.rdbTglDO.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.rdbTglDO.FromDate = null;
            this.rdbTglDO.Location = new System.Drawing.Point(146, 60);
            this.rdbTglDO.Name = "rdbTglDO";
            this.rdbTglDO.Size = new System.Drawing.Size(257, 22);
            this.rdbTglDO.TabIndex = 0;
            this.rdbTglDO.ToDate = null;
            this.rdbTglDO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdbTglDO_KeyDown);
            // 
            // frmAccDOBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdYes);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.dataGridDO);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.rdbTglDO);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmAccDOBrowser";
            this.Text = "Acc DO";
            this.Title = "Acc DO";
            this.Load += new System.EventHandler(this.frmAccDOBrowser_Load);
            this.Controls.SetChildIndex(this.rdbTglDO, 0);
            this.Controls.SetChildIndex(this.cmdSearch, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.dataGridDO, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdYes, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.textBox2, 0);
            this.Controls.SetChildIndex(this.textBox3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDO)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private ISA.Trading.Controls.CommandButton cmdSearch;
        private ISA.Trading.Controls.RangeDateBox rdbTglDO;
        private ISA.Trading.Controls.CustomGridView dataGridDO;
        private ISA.Controls.CommandButton cmdClose;
        private ISA.Controls.CommandButton cmdYes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowID;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoAccPusat;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoAccPiutang;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsOverdueBE;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsSalesBL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ovdBE;
        private System.Windows.Forms.DataGridViewTextBoxColumn hrbe;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoDo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TglDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeSales;
        private System.Windows.Forms.DataGridViewTextBoxColumn NamaToko;
        private System.Windows.Forms.DataGridViewTextBoxColumn Alamat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kota;
        private System.Windows.Forms.DataGridViewTextBoxColumn plf_fb;
        private System.Windows.Forms.DataGridViewTextBoxColumn piutang;
        private System.Windows.Forms.DataGridViewTextBoxColumn GIT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Giro;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiroTolak;
        private System.Windows.Forms.DataGridViewTextBoxColumn SumRpNet;
        private System.Windows.Forms.DataGridViewTextBoxColumn sisaPlf;
        private System.Windows.Forms.DataGridViewTextBoxColumn ovdsbl;
        private System.Windows.Forms.DataGridViewTextBoxColumn KodeToko;
    }
}