﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ISA.Trading.Class;
using ISA.DAL;
using System.IO;

namespace ISA.Trading.Communicator
{
    public partial class frmPOSSUpload : ISA.Trading.BaseForm
    {
        List<string> files = new List<string>();
        int jumlahTable = 32;
        int uploadTable = 0;
        DateTime dt1;
        DateTime dt2;

        private void DownloadCount()
        {
            uploadTable++;
            lblUpload.Text = uploadTable.ToString() + "/" + jumlahTable.ToString();
        }

        public frmPOSSUpload()
        {
            InitializeComponent();
        }

        private void frmPOSSUpload_Load(object sender, EventArgs e)
        {
            InitializeComponent();
            // override 
            if (SecurityManager.IsManager() || SecurityManager.IsAdministrator())
                cmdStartUpload.Enabled = true;
            else
                cmdStartUpload.Enabled = false;
            rgbTanggal.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            rgbTanggal.ToDate = DateTime.Now;
            DateTime dt1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dt2 =DateTime.Now;

            chkTokoToSales.Checked = false;     // true;
            chkClosingStock.Checked = false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdStartUpload_Click(object sender, EventArgs e)
        {
           
            if (rgbTanggal.FromDate.ToString() == "" || rgbTanggal.ToDate.ToString() == "")
            {
                MessageBox.Show("Range tanggal belum diisi!", "Perhatian");
                return;
            }

            dt1 = rgbTanggal.FromDate.Value;
            dt2 = rgbTanggal.ToDate.Value;

            if (chkTokoToSales.Checked)
            {
                Toko2Sales(); DownloadCount();
            }
            if (chkClosingStock.Checked)
            {
                UploadClosingStok(); DownloadCount();
            }

            UploadAntarGudang(); DownloadCount();
            UploadAntarGudangDetail(); DownloadCount();
            UploadOrderPenjualan(); DownloadCount();
            UploadOrderPenjualanDetail(); DownloadCount();
            UploadNotaPenjualan(); DownloadCount();
            UploadNotaPenjualanDetail(); DownloadCount();
            UploadReturPenjualan(); DownloadCount();
            UploadReturPenjualanDetail(); DownloadCount();
            UploadKoreksi(); DownloadCount();
            UploadMutasi(); DownloadCount();
            UploadMutasiDetail(); DownloadCount();
            UploadOpname(); DownloadCount();
            UploadPenjualanPotongan(); DownloadCount();
            UploadToko(); DownloadCount();
            UploadStatusToko(); DownloadCount();
            UploadSales(); DownloadCount();
            UploadStok(); DownloadCount();
            UploadExpedisi(); DownloadCount();
            UploadRekapKoli(); DownloadCount();
            UploadRekapKoliDetail(); DownloadCount();
            UploadRekapKoliSubDetail(); DownloadCount();
            UploadPeminjaman(); DownloadCount();
            UploadPeminjamanDetail(); DownloadCount();
            UploadPengembalian(); DownloadCount();
            UploadPengembalianDetail(); DownloadCount();
            UploadPemasok(); DownloadCount();
            UploadNotaPembelian(); DownloadCount();
            UploadNotaPembelianDetail(); DownloadCount();
            UploadOrderPembelian(); DownloadCount();
            UploadOrderPembelianDetail(); DownloadCount();
            UploadReturPembelian(); DownloadCount();
            UploadReturPembelianDetail(); DownloadCount();

            ZipFile(files);
            MessageBox.Show("POS Upload Selesai. Lokasi file: " + GlobalVar.DbfUpload + "\\data" + GlobalVar.Gudang + ".zip");
        }

        #region closingStok
        private void UploadClosingStok()
        {
            SqlDataReader dr;
            string FileName = "Clstmp" + GlobalVar.CabangID;

            string TableName = "Closing Stok";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Closing Stok' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("idrec", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("tmt", "tmt", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("kd_gdg", "kd_gdg", Foxpro.enFoxproTypes.Char, 4));
                fields.Add(new Foxpro.DataStruct("id_brg", "id_brg", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("keterangan", "keterangan", Foxpro.enFoxproTypes.Char, 50));
                fields.Add(new Foxpro.DataStruct("qty", "qty", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("userid", "userid", Foxpro.enFoxproTypes.Char, 50));
                fields.Add(new Foxpro.DataStruct("updatetime", "updatetime", Foxpro.enFoxproTypes.DateTime, 8));
                
                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idhkrmagud", "IDHKRMAGUD"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("[usp_ClosingStok_uploadDBF]"));
                    db.Commands[0].Parameters.Add(new Parameter("@Date", SqlDbType.DateTime, dt1));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }

                //using (Database db = new Database())
                //{
                //    db.Commands.Add(db.CreateCommand("usp_ClosingStokSyncflag_UPDATE"));
                //    db.Commands[0].Parameters.Add(new Parameter("@Date", SqlDbType.DateTime, dt1));
                //    db.Commands[0].ExecuteNonQuery();
                //}
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion




        #region Toko to sales
        private void Toko2Sales()
        {
            SqlDataReader dr;
            string FileName = "T2stmp" + GlobalVar.CabangID;

            string TableName = "Toko to sales";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Toko to sales' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("idrec", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("kd_toko", "kd_toko", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("kd_sales", "kd_sales", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("piutang_b", "piutang_b", Foxpro.enFoxproTypes.Numeric, 13));
                fields.Add(new Foxpro.DataStruct("piutang_j", "piutang_j", Foxpro.enFoxproTypes.Numeric, 13));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idhkrmagud", "IDHKRMAGUD"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("usp_TokoToSales_uploadDBF"));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }

            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion


        #region Antar Gudang
        private void UploadAntarGudang()
        {
            SqlDataReader dr;
            string FileName = "HAGTmp" + GlobalVar.CabangID;

            string TableName = "Antar Gudang";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Antar Gudang' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("idhkrmagud", "idhkrmagud", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("dr_gud", "dr_gud", Foxpro.enFoxproTypes.Char, 4));
                fields.Add(new Foxpro.DataStruct("ke_gud", "ke_gud", Foxpro.enFoxproTypes.Char, 4));
                fields.Add(new Foxpro.DataStruct("tgl_krm", "tgl_krm", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("tgl_trm", "tgl_trm", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("no_ag", "no_ag", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("pengirim", "pengirim", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("penerima", "penerima", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("drcheck1", "drcheck1", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("drcheck2", "drcheck2", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("kecheck1", "kecheck1", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("kecheck2", "kecheck2", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("catatan", "catatan", Foxpro.enFoxproTypes.Char, 43));
                fields.Add(new Foxpro.DataStruct("exp", "exp", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("no_kend", "no_kend", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("nm_sopir", "nm_sopir", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("id_krmtrm", "id_krmtrm", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("no_bodo", "no_dobo", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("lbo", "lbo", Foxpro.enFoxproTypes.Logical, 1));
                fields.Add(new Foxpro.DataStruct("nprint", "nprint", Foxpro.enFoxproTypes.Numeric, 1));
                fields.Add(new Foxpro.DataStruct("no_rq", "no_rq", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tgl_rq", "tgl_rq", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("no_do", "no_do", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tgl_do", "tgl_do", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("tgl_sj", "tgl_sj", Foxpro.enFoxproTypes.DateTime, 8));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));


                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idhkrmagud", "IDHKRMAGUD"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_AntarGudang"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }

            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UploadAntarGudangDetail()
        {
            SqlDataReader dr;
            string FileName = "DAGTmp" + GlobalVar.CabangID;

            string TableName = "Antar Gudang Detail";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Antar Gudang Detail' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("iddkrmagud", "iddkrmagud", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("idhkrmagud", "idhkrmagud", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("id_brg", "id_brg", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("nama_stok", "nama_stok", Foxpro.enFoxproTypes.Char, 73));
                fields.Add(new Foxpro.DataStruct("qty_krm", "qty_krm", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("qty_trm", "qty_trm", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("catatan", "catatan", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("hpp", "hpp", Foxpro.enFoxproTypes.Numeric, 10));
                fields.Add(new Foxpro.DataStruct("ongkos", "ongkos", Foxpro.enFoxproTypes.Numeric, 10));
                fields.Add(new Foxpro.DataStruct("drgud", "drgud", Foxpro.enFoxproTypes.Char, 4));
                fields.Add(new Foxpro.DataStruct("kegud", "kegud", Foxpro.enFoxproTypes.Char, 4));
                fields.Add(new Foxpro.DataStruct("tgl_krm", "tgl_krm", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("tgl_trm", "tgl_trm", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("qty_do", "qty_do", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("no_bodo", "no_bodo", Foxpro.enFoxproTypes.Char, 7));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("iddkrmagud", "IDDKRMAGUD"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_AntarGudangDetail"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Expedisi
        private void UploadExpedisi()
        {
            SqlDataReader dr;
            string FileName = "ExpTmp" + GlobalVar.CabangID;

            string TableName = "Expedisi";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Expedisi' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();
                fields.Add(new Foxpro.DataStruct("No", "No", Foxpro.enFoxproTypes.Char, 4));
                fields.Add(new Foxpro.DataStruct("KodeExpedisi", "kode", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("NamaExpedisi", "nm_exp", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("Alamat", "alamat", Foxpro.enFoxproTypes.Char, 60));
                fields.Add(new Foxpro.DataStruct("Telp", "telepon", Foxpro.enFoxproTypes.Char, 32));
                fields.Add(new Foxpro.DataStruct("KotaTujuan", "kota_tj", Foxpro.enFoxproTypes.Char, 80));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("kode", "KODE"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_Expedisi"));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Kompensasi
        private void UploadKompensasi()
        {
            SqlDataReader dr;
            string FileName = "KmpTmp" + GlobalVar.CabangID;

            string TableName = "Kompensasi";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Kompensasi' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("RecordID", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("DiscKompensasi", "disc_komp", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idrec", "IDREC"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_Kompensasi"));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Koreksi
        private void UploadKoreksi()
        {
            SqlDataReader dr;
            string FileName = "KorTmp" + GlobalVar.CabangID;

            string TableName = "Koreksi";
            label1.Text = TableName;


            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Koreksi' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("RecordID", "id_koreksi", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("IdTr", "idtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("DetailID", "id_detail", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("TglKoreksi", "tglkoreksi", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("NoKoreksi", "no_koreksi", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("NamaStok", "nama_stok", Foxpro.enFoxproTypes.Char, 73));
                fields.Add(new Foxpro.DataStruct("KelompokBarang", "klp", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("QtyNotaBaru", "j_nota", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("Satuan", "satuan", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("Catatan", "catatan", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("HrgBeliBaru", "h_jual", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("HrgPokok", "h_pokok", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("Disc1", "disc_1", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("Disc2", "disc_2", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("Disc2", "disc_3", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("IDDisc", "id_disc", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("Potongan", "pot_rp", Foxpro.enFoxproTypes.Numeric, 12));
                fields.Add(new Foxpro.DataStruct("Pemasok", "kd_toko", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("KodeSales", "kd_sales", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("Sumber", "sumber", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("BarangID", "id_brg", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("HrgBeliKoreksi", "h_koreksi", Foxpro.enFoxproTypes.Numeric, 12));
                fields.Add(new Foxpro.DataStruct("LinkID", "dt_link", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("KodeGudang", "kd_gdg", Foxpro.enFoxproTypes.Char, 4));
                fields.Add(new Foxpro.DataStruct("QtyNotaKoreksi", "n_koreksi", Foxpro.enFoxproTypes.Numeric, 6));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("id_koreksi", "ID_KOREKSI"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_Koreksi"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Toko
        private void UploadToko()
        {
            SqlDataReader dr;
            string FileName = "TokTmp" + GlobalVar.CabangID;

            string TableName = "Toko";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Toko' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("TokoID", "idtoko", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("NamaToko", "namatoko", Foxpro.enFoxproTypes.Char, 31));
                fields.Add(new Foxpro.DataStruct("Alamat", "alamat", Foxpro.enFoxproTypes.Char, 60));
                fields.Add(new Foxpro.DataStruct("Kota", "kota", Foxpro.enFoxproTypes.Char, 20));
                fields.Add(new Foxpro.DataStruct("Telp", "notelp", Foxpro.enFoxproTypes.Char, 20));
                fields.Add(new Foxpro.DataStruct("WilID", "idwil", Foxpro.enFoxproTypes.Char, 8));
                fields.Add(new Foxpro.DataStruct("PenanggungJawab", "pngjwb", Foxpro.enFoxproTypes.Char, 20));
                fields.Add(new Foxpro.DataStruct("KodeToko", "kd_toko", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("PiutangB", "piutang_b", Foxpro.enFoxproTypes.Numeric, 13));
                fields.Add(new Foxpro.DataStruct("PiutangJ", "piutang_j", Foxpro.enFoxproTypes.Numeric, 13));
                fields.Add(new Foxpro.DataStruct("Plafon", "plafon", Foxpro.enFoxproTypes.Numeric, 13));
                fields.Add(new Foxpro.DataStruct("ToJual", "to_jual", Foxpro.enFoxproTypes.Numeric, 13));
                fields.Add(new Foxpro.DataStruct("ToRetPot", "to_retpot", Foxpro.enFoxproTypes.Numeric, 13));
                fields.Add(new Foxpro.DataStruct("JangkaWaktuKredit", "jkw_kredit", Foxpro.enFoxproTypes.Numeric, 3));
                fields.Add(new Foxpro.DataStruct("Cabang2", "cab2", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("Tgl1st", "tgl1st", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("Exist", "exist", Foxpro.enFoxproTypes.Logical, 1));
                fields.Add(new Foxpro.DataStruct("ClassID", "idclass", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("Catatan", "catatan", Foxpro.enFoxproTypes.Char, 73));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("HariKirim", "hari_krm", Foxpro.enFoxproTypes.Numeric, 2));
                fields.Add(new Foxpro.DataStruct("KodePos", "kd_pos", Foxpro.enFoxproTypes.Char, 5));
                fields.Add(new Foxpro.DataStruct("Grade", "grade", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("Plafon1st", "plafon_1st", Foxpro.enFoxproTypes.Numeric, 10));
                fields.Add(new Foxpro.DataStruct("Flag", "flag", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("Bentrok", "bentrok", Foxpro.enFoxproTypes.Char, 20));
                fields.Add(new Foxpro.DataStruct("StatusAktif", "lpasif", Foxpro.enFoxproTypes.Logical, 1));
                fields.Add(new Foxpro.DataStruct("HariSales", "hari_sls", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("Daerah", "daerah", Foxpro.enFoxproTypes.Char, 25));
                fields.Add(new Foxpro.DataStruct("Propinsi", "propinsi", Foxpro.enFoxproTypes.Char, 30));
                fields.Add(new Foxpro.DataStruct("AlamatRumah", "alm_rumah", Foxpro.enFoxproTypes.Char, 60));
                fields.Add(new Foxpro.DataStruct("Pengelola", "pengelola", Foxpro.enFoxproTypes.Char, 20));
                fields.Add(new Foxpro.DataStruct("TglLahir", "tgl_lahir", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("HP", "hp", Foxpro.enFoxproTypes.Char, 30));
                fields.Add(new Foxpro.DataStruct("Status", "status", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("ThnBerdiri", "th_berdiri", Foxpro.enFoxproTypes.Char, 4));
                fields.Add(new Foxpro.DataStruct("StatusRuko", "lruko", Foxpro.enFoxproTypes.Logical, 1));
                fields.Add(new Foxpro.DataStruct("JmlCabang", "jml_cabang", Foxpro.enFoxproTypes.Numeric, 2));
                fields.Add(new Foxpro.DataStruct("JmlSales", "jml_sales", Foxpro.enFoxproTypes.Numeric, 2));
                fields.Add(new Foxpro.DataStruct("Kinerja", "kinerja", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("BidangUsaha", "bdg_usaha", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("RefSales", "reff_sls", Foxpro.enFoxproTypes.Char, 35));
                fields.Add(new Foxpro.DataStruct("RefCollector", "reff_col", Foxpro.enFoxproTypes.Char, 35));
                fields.Add(new Foxpro.DataStruct("RefSupervisor", "reff_spv", Foxpro.enFoxproTypes.Char, 35));
                fields.Add(new Foxpro.DataStruct("PlafonSurvey", "plf_survey", Foxpro.enFoxproTypes.Numeric, 13));
                fields.Add(new Foxpro.DataStruct("no_toko", "no_toko", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("exp_norm", "exp_norm", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("cab", "cab", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("cab1", "cab1", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("l_edit", "l_edit", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("idrec_post", "idrec_post", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("tgl_edit", "tgl_edit", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("no_urut", "no_urut", Foxpro.enFoxproTypes.Char, 8));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("kd_toko", "KODE_TOKO"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_Toko"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromdate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@todate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Sales
        private void UploadSales()
        {
            SqlDataReader dr;
            string FileName = "SlsTmp" + GlobalVar.CabangID;

            string TableName = "Sales";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Sales' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("SalesID", "kd_sales", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("NamaSales", "nm_sales", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("RecID", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("TglLahir", "tgl_lahir", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("Alamat", "alamat", Foxpro.enFoxproTypes.Char, 30));
                fields.Add(new Foxpro.DataStruct("Target", "target", Foxpro.enFoxproTypes.Numeric, 16));
                fields.Add(new Foxpro.DataStruct("BatasOD", "batas_od", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("TglMasuk", "tgl_masuk", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("TglKeluar", "tgl_keluar", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("SalesID", "kd_toko", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("NamaSales", "namatoko", Foxpro.enFoxproTypes.Char, 31));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 31));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("kd_sales", "KODE_SALES"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_Sales"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromdate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@todate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Stok
        private void UploadStok()
        {
            SqlDataReader dr;
            string FileName = "StkTmp" + GlobalVar.CabangID;

            string TableName = "Stok";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Stok' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("BarangID", "id_brg", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("RecordID", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("Bundle", "bundel", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("NamaStok", "nama_stok", Foxpro.enFoxproTypes.Char, 73));
                fields.Add(new Foxpro.DataStruct("KodeSolo", "kodesolo", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("HrgJual", "hjual", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("Hpp", "hpp", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("Kendaraan", "kendaraan", Foxpro.enFoxproTypes.Char, 43));
                fields.Add(new Foxpro.DataStruct("NamaTertera", "nm_tertera", Foxpro.enFoxproTypes.Char, 43));
                fields.Add(new Foxpro.DataStruct("PartNo", "partno", Foxpro.enFoxproTypes.Char, 21));
                fields.Add(new Foxpro.DataStruct("Merek", "merek", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("Dibungkus", "dibungkus", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("SumberDr", "sumber_dr", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("ProsesID", "idproses", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("SatSolo", "sat_solo", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("Material", "material", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("SatJual", "sat_jual", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("HPPSolo", "hpp_solo", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("HPPSas", "hpp_sas", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("HPPSolo", "hppsolo", Foxpro.enFoxproTypes.Numeric, 8));
                fields.Add(new Foxpro.DataStruct("KodeRak", "kd_rak", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("KodeRak1", "kd_rak1", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("KodeRak2", "kd_rak2", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("JB", "jb", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("StatusPasif", "lpasif", Foxpro.enFoxproTypes.Logical, 1));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("Flag1", "flag1", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("TglOpname", "tgl_opnm", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("TglAwal", "tgl_awal", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("QAwal", "q_awal", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("HariRataRata", "q_opnm", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("RppAwal", "rpp_awal", Foxpro.enFoxproTypes.Numeric, 12));
                fields.Add(new Foxpro.DataStruct("QJual", "q_jual", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("QBeli", "q_beli", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("QReturJual", "q_retj", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("QReturBeli", "q_retb", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("QOrderJual", "q_ordj", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("PrediksiLamaKirim", "q_ordb", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("QMutasi", "q_mutasi", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("QKrsi", "q_krsi", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("QAngd", "q_angd", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("QSelisih", "q_slsh", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("QAkhir", "q_akhir", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("StokMin", "stokmin", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("StokMax", "stokmax", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("IsiKoli", "isi_koli", Foxpro.enFoxproTypes.Numeric, 4));
                fields.Add(new Foxpro.DataStruct("konversi", "konversi", Foxpro.enFoxproTypes.Numeric, 10));
                fields.Add(new Foxpro.DataStruct("jenis", "jenis", Foxpro.enFoxproTypes.Char, 20));
                fields.Add(new Foxpro.DataStruct("katpj", "katpj", Foxpro.enFoxproTypes.Numeric, 1));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idrec", "ID_REC"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_Stok"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromdate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@todate", SqlDbType.DateTime, dt2));

                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Pemasok
        private void UploadPemasok()
        {
            SqlDataReader dr;
            string FileName = "PmsTmp" + GlobalVar.CabangID;
            string TableName = "Pemasok";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Pemasok' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("PemasokID", "idp", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("Nama", "nama", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("Lengkap", "lengkap", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("Alamat", "alamat", Foxpro.enFoxproTypes.Char, 50));
                fields.Add(new Foxpro.DataStruct("Kota", "kota", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("Telp", "telp", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("Fax", "fax", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("Kontak", "kontak", Foxpro.enFoxproTypes.Char, 21));
                fields.Add(new Foxpro.DataStruct("Keterangan", "keterangan", Foxpro.enFoxproTypes.Char, 43));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("lpasif", "lpasif", Foxpro.enFoxproTypes.Logical, 1));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idp", "ID_PEMASOK"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_Pemasok"));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Opname
        private void UploadOpname()
        {
            SqlDataReader dr;
            string FileName = "SopTmp" + GlobalVar.CabangID;
            string TableName = "Opname";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Opname' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("nama_stok", "nama_stok", Foxpro.enFoxproTypes.Char, 73));
                fields.Add(new Foxpro.DataStruct("tgl_opnm", "tgl_opnm", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("qty_opnm", "qty_opnm", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("sat", "sat", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("ket_opnm", "ket_opnm", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("idrec", "idrec", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("id_brg", "id_brg", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("kd_gdg", "kd_gdg", Foxpro.enFoxproTypes.Char, 4));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idrec", "ID_REC"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_OpnameHistory"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromdate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@todate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region OrderPembelian
        private void UploadOrderPembelian()
        {
            SqlDataReader dr;
            string FileName = "HShTmp" + GlobalVar.CabangID;

            string TableName = "Order Pembelian";
            label1.Text = TableName;


            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'OrderPembelian' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("RecordID", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("NoRequest", "no_rq", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("TglRequest", "tgl_rq", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("Pemasok", "pemasok", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("Cabang1", "c1", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("Cabang2", "c2", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("EstHrgJual", "est_rpjual", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("EstHPP", "est_rphpp", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("NItem", "nitem", Foxpro.enFoxproTypes.Numeric, 4));
                fields.Add(new Foxpro.DataStruct("EstRptm", "est_rptrm", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("NoACC", "no_acc", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("Catatan", "catatan", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("id_upl", "id_upl", Foxpro.enFoxproTypes.Char, 23));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idrec", "ID_REC"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_OrderPembelian"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void UploadOrderPembelianDetail()
        {
            SqlDataReader dr;
            string FileName = "DShTmp" + GlobalVar.CabangID;

            string TableName = "Order Pembelian Detail";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'OrderPembelianDetail' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("idrec", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("idheader", "idheader", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("nama_stok", "nama_stok", Foxpro.enFoxproTypes.Char, 73));
                fields.Add(new Foxpro.DataStruct("j_bo", "j_bo", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("j_plus", "j_plus", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("j_rq", "j_rq", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("j_do", "j_do", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("j_trm", "j_trm", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("satuan", "satuan", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("id_brg", "id_brg", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("idtr", "idtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("h_jual", "h_jual", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("hpp_solo", "hpp_solo", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("catatan", "catatan", Foxpro.enFoxproTypes.Char, 90));
                fields.Add(new Foxpro.DataStruct("tgl_rq", "tgl_rq", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("q_jual", "q_jual", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("q_akhir", "q_akhir", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("ket", "ket", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("kd_gdg", "kd_gdg", Foxpro.enFoxproTypes.Char, 4));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idrec", "ID_REC"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_OrderPembelianDetail"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Order Penjualan
        private void UploadOrderPenjualan()
        {
            SqlDataReader dr;
            string FileName = "HHjTmp" + GlobalVar.CabangID;
            string TableName = "Order Penjualan";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'OrderPenjualan' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                string Fpt = GlobalVar.DbfUpload + "\\" + FileName + ".fpt";
                //files.Add(Indexing);
                files.Add(Physical);
                files.Add(Fpt);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("idhtr", "idhtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("cab1", "cab1", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("cab2", "cab2", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("cab3", "cab3", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("no_rq", "no_rq", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tgl_rq", "tgl_rq", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("no_do", "no_do", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tgl_do", "tgl_do", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("no_nota", "no_nota", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tgl_nota", "tgl_nota", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("no_sj", "no_sj", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tgl_sj", "tgl_sj", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("tgl_trm", "tgl_trm", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("hr_krdt", "hr_krdt", Foxpro.enFoxproTypes.Numeric, 3));
                fields.Add(new Foxpro.DataStruct("kd_toko", "kd_toko", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("kd_sales", "kd_sales", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("nm_toko", "nm_toko", Foxpro.enFoxproTypes.Char, 31));
                fields.Add(new Foxpro.DataStruct("al_kirim", "al_kirim", Foxpro.enFoxproTypes.Char, 60));
                fields.Add(new Foxpro.DataStruct("kota", "kota", Foxpro.enFoxproTypes.Char, 20));
                fields.Add(new Foxpro.DataStruct("rp_jual", "rp_jual", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("rp_jual2", "rp_jual2", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("rp_jual3", "rp_jual3", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("rp_net", "rp_net", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("rp_net2", "rp_net2", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("rp_net3", "rp_net3", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("disc_1", "disc_1", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("disc_2", "disc_2", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("disc_3", "disc_3", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("pot_rp", "pot_rp", Foxpro.enFoxproTypes.Numeric, 12));
                fields.Add(new Foxpro.DataStruct("pot_rp2", "pot_rp2", Foxpro.enFoxproTypes.Numeric, 12));
                fields.Add(new Foxpro.DataStruct("pot_rp3", "pot_rp3", Foxpro.enFoxproTypes.Numeric, 12));
                fields.Add(new Foxpro.DataStruct("rp_fee1", "rp_fee1", Foxpro.enFoxproTypes.Numeric, 11));
                fields.Add(new Foxpro.DataStruct("rp_fee2", "rp_fee2", Foxpro.enFoxproTypes.Numeric, 11));
                fields.Add(new Foxpro.DataStruct("expedisi", "expedisi", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("laudit", "laudit", Foxpro.enFoxproTypes.Logical, 1));
                fields.Add(new Foxpro.DataStruct("id_disc", "id_disc", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("catatan", "catatan", Foxpro.enFoxproTypes.Memo, 4));
                fields.Add(new Foxpro.DataStruct("catatan1", "catatan1", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("catatan2", "catatan2", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("catatan3", "catatan3", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("catatan4", "catatan4", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("catatan5", "catatan5", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("no_dobo", "no_dobo", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tgl_reord", "tgl_reord", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("lbo", "lbo", Foxpro.enFoxproTypes.Logical, 1));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("id_link", "id_link", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("id_tr", "id_tr", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("hari_krm", "hari_krm", Foxpro.enFoxproTypes.Numeric, 3));
                fields.Add(new Foxpro.DataStruct("hari_sls", "hari_sls", Foxpro.enFoxproTypes.Numeric, 3));
                fields.Add(new Foxpro.DataStruct("nprint", "nprint", Foxpro.enFoxproTypes.Numeric, 2));
                fields.Add(new Foxpro.DataStruct("no_acc", "no_acc", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("shift", "shift", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("checker_1", "checker_1", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("checker_2", "checker_2", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("cicil", "cicil", Foxpro.enFoxproTypes.Numeric, 3));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 11));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));
                
                //fields.Add(new Foxpro.DataStruct("Cicil", "Cicil", Foxpro.enFoxproTypes.Numeric, 2));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idhtr", "ID_HTR"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_OrderPenjualan"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void UploadOrderPenjualanDetail()
        {
            SqlDataReader dr;
            string FileName = "DHjTmp" + GlobalVar.CabangID;
            string TableName = "Order Penjualan Detail";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'OrderPenjualanDetail' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("idrec", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("idhtr", "idhtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("id_brg", "id_brg", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("nama_stok", "nama_stok", Foxpro.enFoxproTypes.Char, 73));
                fields.Add(new Foxpro.DataStruct("klp", "klp", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("j_rq", "j_rq", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("j_do", "j_do", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("j_sj", "j_sj", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("j_nota", "j_nota", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("j_retur", "j_retur", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("j_koli", "j_koli", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("koli_awal", "koli_awal", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("koli_akhir", "koli_akhir", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("no_koli", "no_koli", Foxpro.enFoxproTypes.Char, 15));
                fields.Add(new Foxpro.DataStruct("satuan", "satuan", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("catatan", "catatan", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("tgl_sj", "tgl_sj", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("h_jual", "h_jual", Foxpro.enFoxproTypes.Char, 14));
                //fields.Add(new Foxpro.DataStruct("h_jual", "h_jual", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("h_pokok", "h_pokok", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("hpp_solo", "hpp_solo", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("disc_1", "disc_1", Foxpro.enFoxproTypes.Char, 5));
                //fields.Add(new Foxpro.DataStruct("disc_1", "disc_1", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("disc_2", "disc_2", Foxpro.enFoxproTypes.Char, 5));
                //fields.Add(new Foxpro.DataStruct("disc_2", "disc_2", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("disc_3", "disc_3", Foxpro.enFoxproTypes.Char, 5));
                //fields.Add(new Foxpro.DataStruct("disc_3", "disc_3", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("pot_rp", "pot_rp", Foxpro.enFoxproTypes.Numeric, 12));
                fields.Add(new Foxpro.DataStruct("id_disc", "id_disc", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("id_koreksi", "id_koreksi", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("kd_toko", "kd_toko", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("no_bodo", "no_bodo", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("nprint", "nprint", Foxpro.enFoxproTypes.Numeric, 1));
                fields.Add(new Foxpro.DataStruct("no_acc", "no_acc", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("ket_koli", "ket_koli", Foxpro.enFoxproTypes.Char, 20));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idrec", "ID_REC"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_OrderPenjualanDetail"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region RekapKoli
        private void UploadRekapKoli()
        {
            SqlDataReader dr;
            string FileName = "HxpTmp" + GlobalVar.CabangID;
            string TableName = "Rekap Koli";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'RekapKoli' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("idtr", "idtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("tgl_sj", "tgl_sj", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("no_sj", "no_sj", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("idwil", "idwil", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("kd_toko", "kd_toko", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("nm_toko", "nm_toko", Foxpro.enFoxproTypes.Char, 31));
                fields.Add(new Foxpro.DataStruct("alamat", "alamat", Foxpro.enFoxproTypes.Char, 60));
                fields.Add(new Foxpro.DataStruct("kota", "kota", Foxpro.enFoxproTypes.Char, 20));
                fields.Add(new Foxpro.DataStruct("tgl_klr", "tgl_klr", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("kd_exp", "kd_exp", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("shift", "shift", Foxpro.enFoxproTypes.Numeric, 1));
                fields.Add(new Foxpro.DataStruct("jumlah", "jumlah", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("by_exp", "by_exp", Foxpro.enFoxproTypes.Numeric, 10));
                fields.Add(new Foxpro.DataStruct("kd_exp2", "kd_exp2", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("kd_exp3", "kd_exp3", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("by_exp2", "by_exp2", Foxpro.enFoxproTypes.Numeric, 10));
                fields.Add(new Foxpro.DataStruct("by_exp3", "by_exp3", Foxpro.enFoxproTypes.Numeric, 10));
                fields.Add(new Foxpro.DataStruct("nprint", "nprint", Foxpro.enFoxproTypes.Numeric, 1));
                fields.Add(new Foxpro.DataStruct("kp", "kp", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idtr", "ID_TR"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_RekapKoli"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void UploadRekapKoliSubDetail()
        {
            SqlDataReader dr;
            string FileName = "CxpTmp" + GlobalVar.CabangID;
            string TableName = "Rekap Koli Sub Detail";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'RekapKoliSubDetail' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("idtr", "idtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("no_nota", "no_nota", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("idrec", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("uraian", "uraian", Foxpro.enFoxproTypes.Char, 12));
                fields.Add(new Foxpro.DataStruct("jumlah", "jumlah", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("satuan", "satuan", Foxpro.enFoxproTypes.Char, 5));
                fields.Add(new Foxpro.DataStruct("ket", "ket", Foxpro.enFoxproTypes.Char, 30));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("flag", "flag", Foxpro.enFoxproTypes.Char, 1));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idrec", "ID_REC"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_RekapKoliSubDetail"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UploadRekapKoliDetail()
        {
            SqlDataReader dr;
            string FileName = "DxpTmp" + GlobalVar.CabangID;
            string TableName = "Rekap Koli Detail";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'RekapKoliDetail' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("idtr", "idtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("idrec", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("idhtr", "idhtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("no_do", "no_do", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("no_nota", "no_nota", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tk", "tk", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("nominal", "nominal", Foxpro.enFoxproTypes.Numeric, 10));
                fields.Add(new Foxpro.DataStruct("uraian", "uraian", Foxpro.enFoxproTypes.Char, 12));
                fields.Add(new Foxpro.DataStruct("jumlah", "jumlah", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("satuan", "satuan", Foxpro.enFoxproTypes.Char, 5));
                fields.Add(new Foxpro.DataStruct("sales", "sales", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("ket", "ket", Foxpro.enFoxproTypes.Char, 30));
                fields.Add(new Foxpro.DataStruct("no_resi", "no_resi", Foxpro.enFoxproTypes.Char, 15));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idrec", "ID_REC"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_RekapKoliDetail"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Mutasi
        private void UploadMutasi()
        {
            SqlDataReader dr;
            string FileName = "HmtTmp" + GlobalVar.CabangID;
            string TableName = "Mutasi";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Mutasi' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("MutasiID", "id_mts", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("TglMutasi", "tgl_mts", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("NomorMutasi", "no_mts", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("KeteranganMutasi", "ket_mts", Foxpro.enFoxproTypes.Char, 63));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("LAudit", "laudit", Foxpro.enFoxproTypes.Logical, 1));
                fields.Add(new Foxpro.DataStruct("TipeMutasi", "type", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("MutasiPlus", "mts_plus", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("MutasiMinus", "mts_minus", Foxpro.enFoxproTypes.Numeric, 9));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("id_mts", "ID_MUTASI"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_Mutasi"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UploadMutasiDetail()
        {
            SqlDataReader dr;
            string FileName = "DmtTmp" + GlobalVar.CabangID;
            string TableName = "Mutasi Detail";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'MutasiDetail' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("MutasiID", "id_mts", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("RecordID", "idrec", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("QtyMutasi", "j_mts", Foxpro.enFoxproTypes.Numeric, 6));
                fields.Add(new Foxpro.DataStruct("NamaStok", "nama_stok", Foxpro.enFoxproTypes.Char, 73));
                fields.Add(new Foxpro.DataStruct("SatJual", "sat", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("Kelompok", "klp", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("HrgPokok", "h_pokok", Foxpro.enFoxproTypes.Numeric, 9));
                fields.Add(new Foxpro.DataStruct("HppSolo", "hpp_solo", Foxpro.enFoxproTypes.Numeric, 8));
                fields.Add(new Foxpro.DataStruct("Keterangan", "catatan", Foxpro.enFoxproTypes.Char, 43));
                fields.Add(new Foxpro.DataStruct("TglMutasi", "tgl_mts", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("Syncflag", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("KodeBarang", "id_brg", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("TipeMutasi", "type", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("Gudang", "kd_gdg", Foxpro.enFoxproTypes.Char, 4));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idrec", "ID_REC"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_MutasiDetail"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Peminjaman
        private void UploadPeminjaman()
        {
            SqlDataReader dr;
            string FileName = "HpjTmp" + GlobalVar.CabangID;
            string TableName = "Peminjaman";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Peminjaman' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("RecordID", "idtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("NoBukti", "nobukti", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("TglKeluar", "tgl_kelpj", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("TglBatas", "tgl_btspj", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("StaffPenjualan", "penjstaff", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("Catatan", "catatan", Foxpro.enFoxproTypes.Char, 25));
                fields.Add(new Foxpro.DataStruct("NPrint", "print", Foxpro.enFoxproTypes.Numeric, 1));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("KodeSales", "kd_sales", Foxpro.enFoxproTypes.Char, 11));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idtr", "ID_TR"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_Peminjaman"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UploadPeminjamanDetail()
        {
            SqlDataReader dr;
            string FileName = "DpjTmp" + GlobalVar.CabangID;
            string TableName = "Peminjaman Detail";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'PeminjamanDetail' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("TransactionID", "idtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("RecordID", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("KodeBarang", "id_brg", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("KodeSales", "kd_sales", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("NamaStok", "nama_stok", Foxpro.enFoxproTypes.Char, 73));
                fields.Add(new Foxpro.DataStruct("Satuan", "satuan", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("QtyMemo", "qty_kelpj", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("QtyKeluarGudang", "qty_kelgd", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("QtyKembali", "qty_kmb", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("catatan", "catatan", Foxpro.enFoxproTypes.Char, 25));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idrec", "ID_REC"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_PeminjamanDetail"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Pengembalian
        private void UploadPengembalian()
        {
            SqlDataReader dr;
            string FileName = "HkbTmp" + GlobalVar.CabangID;
            string TableName = "Pengembalian";
            label1.Text = TableName;


            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'Pengembalian' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("RecordID", "idtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("NoBukti", "nobukti", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("TglKembaliPJ", "tgl_kmbpj", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("TglKembaliGdg", "tgl_kmbgd", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("Catatan", "catatan", Foxpro.enFoxproTypes.Char, 25));
                fields.Add(new Foxpro.DataStruct("NPrint", "print", Foxpro.enFoxproTypes.Numeric, 1));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("KodeSales", "kd_sales", Foxpro.enFoxproTypes.Char, 11));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idtr", "ID_TR"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_Pengembalian"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UploadPengembalianDetail()
        {
            SqlDataReader dr;
            string FileName = "DkbTmp" + GlobalVar.CabangID;
            string TableName = "Pengembalian Detail";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                //if (dtPengembalianDetail.Rows.Count == 0)
                //{
                //    return; 
                //}

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'PengembalianDetail' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("TransactionID", "idtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("RecordID", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("IDPinjam", "iddpinjam", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("BarangID", "id_brg", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("NamaStok", "nama_stok", Foxpro.enFoxproTypes.Char, 73));
                fields.Add(new Foxpro.DataStruct("Satuan", "satuan", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("NoPinjam", "nopjm", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("QtyKembali", "qty_kmb", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("Catatan", "catatan", Foxpro.enFoxproTypes.Char, 25));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idtr", "ID_TR"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_PengembalianDetail"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Retur Pembelian
        private void UploadReturPembelian()
        {
            SqlDataReader dr;
            string FileName = "HrbTmp" + GlobalVar.CabangID;
            string TableName = "Retur Pembelian";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'ReturPembelian' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("idretur", "idretur", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("no_retur", "no_retur", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tgl_retur", "tgl_retur", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("pemasok", "pemasok", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("penerima", "penerima", Foxpro.enFoxproTypes.Char, 17));
                fields.Add(new Foxpro.DataStruct("tgl_keluar", "tgl_keluar", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("rp_nilai", "rp_nilai", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("pengirim", "pengirim", Foxpro.enFoxproTypes.Char, 17));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("laudit", "laudit", Foxpro.enFoxproTypes.Logical, 1));
                fields.Add(new Foxpro.DataStruct("nprint", "nprint", Foxpro.enFoxproTypes.Numeric, 1));
                fields.Add(new Foxpro.DataStruct("no_mpr", "no_mpr", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tgl_kirim", "tgl_kirim", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idretur", "ID_RETUR"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_ReturPembelian"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UploadReturPembelianDetail()
        {
            SqlDataReader dr;
            string FileName = "DrbTmp" + GlobalVar.CabangID;
            string TableName = "Retur Pembelian Detail";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'ReturPembelianDetail' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("idrec", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("idretur", "idretur", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("idhtr", "idhtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("iddtr", "iddtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("kdretur", "kdretur", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("nama_stok", "nama_stok", Foxpro.enFoxproTypes.Char, 73));
                fields.Add(new Foxpro.DataStruct("pemasok", "pemasok", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("q_gudang", "q_gudang", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("q_terima", "q_terima", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("satuan", "satuan", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("klp", "klp", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("h_beli", "h_beli", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("h_net", "h_net", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("h_pokok", "h_pokok", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("hpp_solo", "hpp_solo", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("pot_rp", "pot_rp", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("id_disc", "id_disc", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("disc_1", "disc_1", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("disc_2", "disc_2", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("disc_3", "disc_3", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("id_koreksi", "id_koreksi", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("catatan", "catatan", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("tgl_keluar", "tgl_keluar", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("tgl_beli", "tgl_beli", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("id_brg", "id_brg", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("kd_gdg", "kd_gdg", Foxpro.enFoxproTypes.Char, 4));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idrec", "ID_REC"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_ReturPembelianDetail"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Retur Penjualan
        private void UploadReturPenjualan()
        {
            SqlDataReader dr;
            string FileName = "HrjTmp" + GlobalVar.CabangID;
            string TableName = "Retur Penjualan";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'ReturPenjualan' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("cab1", "cab1", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("cab2", "cab2", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("idretur", "idretur", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("no_memo", "no_memo", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("no_ret", "no_ret", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("no_tolak", "no_tolak", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tgl_memo", "tgl_memo", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("tgl_ret", "tgl_ret", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("tgl_tolak", "tgl_tolak", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("kd_toko", "kd_toko", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("nm_toko", "nm_toko", Foxpro.enFoxproTypes.Char, 31));
                fields.Add(new Foxpro.DataStruct("al_kirim", "al_kirim", Foxpro.enFoxproTypes.Char, 60));
                fields.Add(new Foxpro.DataStruct("kota", "kota", Foxpro.enFoxproTypes.Char, 20));
                fields.Add(new Foxpro.DataStruct("pngmbln", "pngmbln", Foxpro.enFoxproTypes.Char, 17));
                fields.Add(new Foxpro.DataStruct("tgl_pngmb", "tgl_pngmb", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("tgl_gudang", "tgl_gudang", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("rp_nilai1", "rp_nilai1", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("rp_nilai2", "rp_nilai2", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("rp_nilai3", "rp_nilai3", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("rp_nilai", "rp_nilai", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("bag_penj", "bag_penj", Foxpro.enFoxproTypes.Char, 17));
                fields.Add(new Foxpro.DataStruct("penerima", "penerima", Foxpro.enFoxproTypes.Char, 17));
                fields.Add(new Foxpro.DataStruct("dt_link", "dt_link", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("laudit", "laudit", Foxpro.enFoxproTypes.Logical, 1));
                fields.Add(new Foxpro.DataStruct("nprint", "nprint", Foxpro.enFoxproTypes.Numeric, 1));
                fields.Add(new Foxpro.DataStruct("tgl_rqret", "tgl_rqret", Foxpro.enFoxproTypes.DateTime, 8));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idretur", "IDRETUR"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_ReturPenjualan"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UploadReturPenjualanDetail()
        {
            SqlDataReader dr;
            string FileName = "DrjTmp" + GlobalVar.CabangID;
            string TableName = "Retur Penjualan Detail";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'ReturPenjualanDetail' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("idrec", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("idretur", "idretur", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("idhtr", "idhtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("iddtr", "iddtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("kdretur", "kdretur", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("nama_stok", "nama_stok", Foxpro.enFoxproTypes.Char, 73));
                fields.Add(new Foxpro.DataStruct("kd_sales", "kd_sales", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("kd_toko", "kd_toko", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("asalnota", "asalnota", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("klp", "klp", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("q_memo", "q_memo", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("q_tarik", "q_tarik", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("q_terima", "q_terima", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("q_gudang", "q_gudang", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("q_tolak", "q_tolak", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("satuan", "satuan", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("h_jual", "h_jual", Foxpro.enFoxproTypes.Numeric, 8));
                fields.Add(new Foxpro.DataStruct("h_net1", "h_net1", Foxpro.enFoxproTypes.Numeric, 8));
                fields.Add(new Foxpro.DataStruct("h_net2", "h_net2", Foxpro.enFoxproTypes.Numeric, 8));
                fields.Add(new Foxpro.DataStruct("h_net3", "h_net3", Foxpro.enFoxproTypes.Numeric, 8));
                fields.Add(new Foxpro.DataStruct("h_net", "h_net", Foxpro.enFoxproTypes.Numeric, 8));
                fields.Add(new Foxpro.DataStruct("h_pokok", "h_pokok", Foxpro.enFoxproTypes.Numeric, 8));
                fields.Add(new Foxpro.DataStruct("hpp_solo", "hpp_solo", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("pot_rp", "pot_rp", Foxpro.enFoxproTypes.Numeric, 8));
                fields.Add(new Foxpro.DataStruct("id_disc", "id_disc", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("disc_1", "disc_1", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("disc_2", "disc_2", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("disc_3", "disc_3", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("id_koreksi", "id_koreksi", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("catatan", "catatan", Foxpro.enFoxproTypes.Char, 30));
                fields.Add(new Foxpro.DataStruct("catatan1", "catatan1", Foxpro.enFoxproTypes.Char, 30));
                fields.Add(new Foxpro.DataStruct("tgl_gudang", "tgl_gudang", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("id_brg", "id_brg", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("kategori", "kategori", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("kd_gdg", "kd_gdg", Foxpro.enFoxproTypes.Char, 4));
                fields.Add(new Foxpro.DataStruct("no_acc", "no_acc", Foxpro.enFoxproTypes.Char, 6));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idrec", "ID_REC"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_ReturPenjualanDetail"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Potongan
        private void UploadPenjualanPotongan()
        {
            SqlDataReader dr;
            string FileName = "HptTmp" + GlobalVar.CabangID;
            string TableName = "Penjualan Potongan";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'PenjualanPotongan' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("TrID", "idtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("PotID", "idpot", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("NoPot", "Nopot", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("TglPot", "Tgl_pot", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("Dil", "Dil", Foxpro.enFoxproTypes.Numeric, 8));
                fields.Add(new Foxpro.DataStruct("Disc", "Disc", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("RpNet", "Rp_net", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("Catatan", "Catatan", Foxpro.enFoxproTypes.Char, 17));
                fields.Add(new Foxpro.DataStruct("TglACC", "Tgl_acc", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("DilACC", "Dil_acc", Foxpro.enFoxproTypes.Numeric, 11));
                fields.Add(new Foxpro.DataStruct("CatACC", "Cat_acc", Foxpro.enFoxproTypes.Char, 17));
                fields.Add(new Foxpro.DataStruct("DiscACC", "Disc_acc", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("Dib", "Dib", Foxpro.enFoxproTypes.Numeric, 8));
                fields.Add(new Foxpro.DataStruct("DibAcc", "Dib_acc", Foxpro.enFoxproTypes.Numeric, 10));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "Id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("IdLink", "Id_link", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("KodeToko", "Kd_toko", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("StatusACC", "acc", Foxpro.enFoxproTypes.Char, 1));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idpot", "ID_POT"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_PenjualanPotongan"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Nota Pembelian
        private void UploadNotaPembelian()
        {
            SqlDataReader dr;
            string FileName = "HtbTmp" + GlobalVar.CabangID;
            string TableName = "Nota Pembelian";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'NotaPembelian' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                string Fpt = GlobalVar.DbfUpload + "\\" + FileName + ".fpt";
                files.Add(Physical);
                files.Add(Fpt);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("RecordID", "idtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("NoRequest", "no_rq", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("TglRequest", "tgl_rq", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("NoDO", "no_do", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("TglTransaksi", "tgl_trans", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("NoNota", "no_nota", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("TglNota", "tgl_nota", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("NoSuratJalan", "no_sj", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("TglSuratJalan", "tgl_sj", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("TglTerima", "tgl_trm", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("HariKredit", "hr_krdt", Foxpro.enFoxproTypes.Numeric, 3));
                fields.Add(new Foxpro.DataStruct("Pemasok", "pemasok", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("HargaBeli", "rp_beli", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("HargaNet", "rp_net", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("Disc1", "disc_1", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("Disc2", "disc_2", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("Disc3", "disc_3", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("Expedisi", "Expedisi", Foxpro.enFoxproTypes.Char, 9));
                fields.Add(new Foxpro.DataStruct("IsClosed", "Laudit", Foxpro.enFoxproTypes.Logical, 1));
                fields.Add(new Foxpro.DataStruct("DiscFormula", "id_disc", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("PPN", "ppn", Foxpro.enFoxproTypes.Numeric, 3));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("Catatan", "catatan", Foxpro.enFoxproTypes.Memo, 4));
                fields.Add(new Foxpro.DataStruct("pot_rt", "pot_rt", Foxpro.enFoxproTypes.Numeric, 10));
                fields.Add(new Foxpro.DataStruct("oab", "oab", Foxpro.enFoxproTypes.Numeric, 10));
                fields.Add(new Foxpro.DataStruct("oab_rt", "oab_rt", Foxpro.enFoxproTypes.Numeric, 10));
                fields.Add(new Foxpro.DataStruct("pot_rp", "pot_rp", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("catatan1", "catatan1", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("asuransi", "asuransi", Foxpro.enFoxproTypes.Numeric, 7));


                

                fields.Add(new Foxpro.DataStruct("Cabang", "cab", Foxpro.enFoxproTypes.Char, 2));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idtr", "ID_TR"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_NotaPembelian"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void UploadNotaPembelianDetail()
        {
            SqlDataReader dr;
            string FileName = "DtbTmp" + GlobalVar.CabangID;
            string TableName = "Nota Pembelian Detail";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'NotaPembelianDetail' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("RecordID", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("HeaderRecID", "idtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("NamaStok", "nama_stok", Foxpro.enFoxproTypes.Char, 73));
                fields.Add(new Foxpro.DataStruct("Kelompok", "klp", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("QtyRequest", "j_rq", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("QtyDO", "j_do", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("QtySuratJalan", "j_sj", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("QtyNota", "j_nota", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("QtyRetur", "j_retur", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("Satuan", "satuan", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("Catatan", "catatan", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("TglTerima", "tgl_trm", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("HrgBeli", "h_beli", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("HrgPokok", "h_pokok", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("HPPSolo", "hpp_solo", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("Disc1", "disc_1", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("Disc2", "disc_2", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("Disc3", "disc_3", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("DiscFormula", "id_disc", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("Pot", "pot_rp", Foxpro.enFoxproTypes.Numeric, 12));
                fields.Add(new Foxpro.DataStruct("KoreksiID", "id_koreksi", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("PPN", "Ppn", Foxpro.enFoxproTypes.Numeric, 3));
                fields.Add(new Foxpro.DataStruct("Pemasok", "Pemasok", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("SyncFlag", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("BarangID", "id_brg", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("KodeGudang", "kd_gdg", Foxpro.enFoxproTypes.Char, 4));
                fields.Add(new Foxpro.DataStruct("catatan1", "catatan1", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("h_faktur", "h_faktur", Foxpro.enFoxproTypes.Numeric, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idrec", "ID_REC"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_NotaPembelianDetail"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Nota Penjualan
        private void UploadNotaPenjualan()
        {
            SqlDataReader dr;
            string FileName = "HtjTmp" + GlobalVar.CabangID;
            string TableName = "Nota Penjualan";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'NotaPenjualan' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                string Fpt = GlobalVar.DbfUpload + "\\" + FileName + ".fpt";
                files.Add(Physical);
                files.Add(Fpt);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("idhtr", "idhtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("idtr", "idtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("cab1", "cab1", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("cab2", "cab2", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("cab3", "cab3", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("no_rq", "no_rq", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tgl_rq", "tgl_rq", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("no_do", "no_do", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tgl_do", "tgl_do", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("no_nota", "no_nota", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tgl_nota", "tgl_nota", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("no_sj", "no_sj", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("tgl_sj", "tgl_sj", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("tgl_trm", "tgl_trm", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("hr_krdt", "hr_krdt", Foxpro.enFoxproTypes.Numeric, 3));
                fields.Add(new Foxpro.DataStruct("kd_toko", "kd_toko", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("kd_sales", "kd_sales", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("nm_toko", "nm_toko", Foxpro.enFoxproTypes.Char, 31));
                fields.Add(new Foxpro.DataStruct("al_kirim", "al_kirim", Foxpro.enFoxproTypes.Char, 60));
                fields.Add(new Foxpro.DataStruct("kota", "kota", Foxpro.enFoxproTypes.Char, 20));
                fields.Add(new Foxpro.DataStruct("rp_jual", "rp_jual", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("rp_jual2", "rp_jual2", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("rp_jual3", "rp_jual3", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("rp_net", "rp_net", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("rp_net2", "rp_net2", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("rp_net3", "rp_net3", Foxpro.enFoxproTypes.Numeric, 14));
                fields.Add(new Foxpro.DataStruct("disc_1", "disc_1", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("disc_2", "disc_2", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("disc_3", "disc_3", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("pot_rp", "pot_rp", Foxpro.enFoxproTypes.Numeric, 12));
                fields.Add(new Foxpro.DataStruct("pot_rp2", "pot_rp2", Foxpro.enFoxproTypes.Numeric, 12));
                fields.Add(new Foxpro.DataStruct("pot_rp3", "pot_rp3", Foxpro.enFoxproTypes.Numeric, 12));
                fields.Add(new Foxpro.DataStruct("rp_fee1", "rp_fee1", Foxpro.enFoxproTypes.Numeric, 11));
                fields.Add(new Foxpro.DataStruct("rp_fee2", "rp_fee2", Foxpro.enFoxproTypes.Numeric, 11));
                fields.Add(new Foxpro.DataStruct("Expedisi", "Expedisi", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("Laudit", "Laudit", Foxpro.enFoxproTypes.Logical, 1));
                fields.Add(new Foxpro.DataStruct("id_disc", "id_disc", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("catatan1", "catatan1", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("catatan2", "catatan2", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("catatan3", "catatan3", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("catatan4", "catatan4", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("catatan5", "catatan5", Foxpro.enFoxproTypes.Char, 40));
                fields.Add(new Foxpro.DataStruct("tgl_strm", "tgl_strm", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("tgl_reord", "tgl_reord", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("lbo", "lbo", Foxpro.enFoxproTypes.Logical, 1));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("id_link", "id_link", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("id_tr", "id_tr", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("hari_krm", "hari_krm", Foxpro.enFoxproTypes.Numeric, 3));
                fields.Add(new Foxpro.DataStruct("hari_sls", "hari_sls", Foxpro.enFoxproTypes.Numeric, 3));
                fields.Add(new Foxpro.DataStruct("nprint", "nprint", Foxpro.enFoxproTypes.Numeric, 2));
                fields.Add(new Foxpro.DataStruct("no_acc", "no_acc", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("shift", "shift", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("checker_1", "checker_1", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("checker_2", "checker_2", Foxpro.enFoxproTypes.Char, 11));
                fields.Add(new Foxpro.DataStruct("cab0", "cab0", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("catatan", "catatan", Foxpro.enFoxproTypes.Memo, 4));
                fields.Add(new Foxpro.DataStruct("isi_koli", "isi_koli", Foxpro.enFoxproTypes.Numeric, 7));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idhtr", "ID_TR"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_NotaPenjualan"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void UploadNotaPenjualanDetail()
        {
            SqlDataReader dr;
            string FileName = "DtjTmp" + GlobalVar.CabangID;
            string TableName = "Nota Penjualan Detail";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'NotaPenjualanDetail' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("idrec", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("idtr", "idtr", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("nama_stok", "nama_stok", Foxpro.enFoxproTypes.Char, 73));
                fields.Add(new Foxpro.DataStruct("klp", "Klp", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("j_rq", "j_rq", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("j_do", "j_do", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("j_sj", "j_sj", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("j_nota", "j_nota", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("j_koli", "j_koli", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("j_retur", "j_retur", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("koli_awal", "koli_awal", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("koli_akhir", "koli_akhir", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("no_koli", "no_koli", Foxpro.enFoxproTypes.Char, 15));
                fields.Add(new Foxpro.DataStruct("satuan", "satuan", Foxpro.enFoxproTypes.Char, 3));
                fields.Add(new Foxpro.DataStruct("catatan", "catatan", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("tgl_sj", "tgl_sj", Foxpro.enFoxproTypes.DateTime, 8));
                //fields.Add(new Foxpro.DataStruct("h_jual", "h_jual", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("h_jual", "h_jual", Foxpro.enFoxproTypes.Char, 14));
                fields.Add(new Foxpro.DataStruct("h_pokok", "h_pokok", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("hpp_solo", "hpp_solo", Foxpro.enFoxproTypes.Numeric, 7));
                fields.Add(new Foxpro.DataStruct("disc_1", "disc_1", Foxpro.enFoxproTypes.Char, 5));
                //fields.Add(new Foxpro.DataStruct("disc_1", "disc_1", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("disc_2", "disc_2", Foxpro.enFoxproTypes.Char, 5));
                //fields.Add(new Foxpro.DataStruct("disc_2", "disc_2", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("disc_3", "disc_3", Foxpro.enFoxproTypes.Char, 5));
                //fields.Add(new Foxpro.DataStruct("disc_3", "disc_3", Foxpro.enFoxproTypes.Numeric, 5));
                fields.Add(new Foxpro.DataStruct("pot_rp", "pot_rp", Foxpro.enFoxproTypes.Numeric, 12));
                fields.Add(new Foxpro.DataStruct("id_disc", "id_disc", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("id_koreksi", "id_koreksi", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("kd_toko", "kd_toko", Foxpro.enFoxproTypes.Char, 19));
                fields.Add(new Foxpro.DataStruct("no_dobo", "no_dobo", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("no_bodo", "no_bodo", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("NPrint", "nprint", Foxpro.enFoxproTypes.Numeric, 1));
                fields.Add(new Foxpro.DataStruct("no_acc", "no_acc", Foxpro.enFoxproTypes.Char, 7));
                fields.Add(new Foxpro.DataStruct("ket_koli", "ket_koli", Foxpro.enFoxproTypes.Char, 20));
                fields.Add(new Foxpro.DataStruct("id_brg", "id_brg", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("kd_gdg", "kd_gdg", Foxpro.enFoxproTypes.Char, 4));
                fields.Add(new Foxpro.DataStruct("isi_koli", "isi_koli", Foxpro.enFoxproTypes.Numeric, 7));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 250));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("idrec", "ID_REC"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_POS_UPLOAD_NotaPenjualanDetail"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region "StatusTOko"
        private void UploadStatusToko()
        {
            SqlDataReader dr;
            string FileName = "Stotmp" + GlobalVar.CabangID;
            string TableName = "StatusToko";
            label1.Text = TableName;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                pbSyncUpload.Value = 0;
                lblProgress.Text = "Data 'StatusToko' is Uploading...";
                refreshForm();
                string Physical = GlobalVar.DbfUpload + "\\" + FileName + ".dbf";
                //string Indexing = GlobalVar.DbfUpload + "\\" + FileName + ".cdx";
                //files.Add(Indexing);
                files.Add(Physical);

                if (File.Exists(Physical))
                {
                    File.Delete(Physical);
                }

                List<Foxpro.DataStruct> fields = new List<Foxpro.DataStruct>();

                fields.Add(new Foxpro.DataStruct("c1", "c1", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("KodeToko", "kd_toko", Foxpro.enFoxproTypes.Char, 10));
                fields.Add(new Foxpro.DataStruct("tmt", "tmt", Foxpro.enFoxproTypes.DateTime, 8));
                fields.Add(new Foxpro.DataStruct("sts", "sts", Foxpro.enFoxproTypes.Char, 2));
                fields.Add(new Foxpro.DataStruct("idrec", "idrec", Foxpro.enFoxproTypes.Char, 23));
                fields.Add(new Foxpro.DataStruct("ket", "ket", Foxpro.enFoxproTypes.Char, 30));
                fields.Add(new Foxpro.DataStruct("id_match", "id_match", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("ksts", "ksts", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("rd", "rd", Foxpro.enFoxproTypes.Char, 1));
                fields.Add(new Foxpro.DataStruct("idwil", "idwil", Foxpro.enFoxproTypes.Char, 8));
                fields.Add(new Foxpro.DataStruct("tmt_pasif", "tmt_pasif", Foxpro.enFoxproTypes.DateTime, 8));
                //fields.Add(new Foxpro.DataStruct("luby", "luby", Foxpro.enFoxproTypes.Char, 1));
                //fields.Add(new Foxpro.DataStruct("lutime", "lutime", Foxpro.enFoxproTypes.DateTime, 8));

                List<Foxpro.IndexStruct> index = new List<Foxpro.IndexStruct>();
                //index.Add(new Foxpro.IndexStruct("id_mts", "ID_MUTASI"));

                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("[psp_POS_UPLOAD_StatusToko]"));
                    db.Commands[0].Parameters.Add(new Parameter("@fromDate", SqlDbType.DateTime, dt1));
                    db.Commands[0].Parameters.Add(new Parameter("@toDate", SqlDbType.DateTime, dt2));
                    db.Open();
                    dr = db.Commands[0].ExecuteReader();
                    Foxpro.WriteReaderToFile(GlobalVar.DbfUpload + "\\", FileName, fields, dr, index, this, pbSyncUpload, lblUploadCount);
                    db.Close();
                    lblProgress.Text = "";
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        private void refreshForm()
        {
            this.Refresh();
            this.Invalidate();
            Application.DoEvents();
        }

        private void ZipFile(List<string> files)
        {
            string fileZipName = GlobalVar.DbfUpload + "\\data" + GlobalVar.Gudang + ".zip";

            if (File.Exists(fileZipName))
            {
                File.Delete(fileZipName);
            }

            Zip.ZipFiles(files, fileZipName);

            foreach (string str in files)
            {
                if (File.Exists(str))
                {
                    File.Delete(str);
                }
            }
        } 

    }
}
