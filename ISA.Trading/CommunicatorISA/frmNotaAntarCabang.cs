﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ISA.DAL;
using ISA.FTP;

namespace ISA.Trading.CommunicatorISA
{
    public partial class frmNotaAntarCabang : ISA.Trading.BaseForm
    {

        DataSet dsResult = new DataSet();        
        string Gudang;
        int counter1 = 0;
        int counter2 = 0;

        public frmNotaAntarCabang()
        {
            InitializeComponent();
        }

        private void frmNotaAntarCabang_Load(object sender, EventArgs e)
        {
            rangeDateBox1.FromDate = DateTime.Now.Date;
            rangeDateBox1.ToDate = DateTime.Now.Date;
            lookupGudang1.GudangID = string.Empty;
            gridViewNotaPenjualan.AutoGenerateColumns = true;
            gridViewNotaPenjualanDetail.AutoGenerateColumns = true;            
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lookupGudang1.GudangID))
            {
                Gudang = string.Empty;
            }
            else
            {
                Gudang = lookupGudang1.GudangID;
            }


            RefreshData();
        }


        private void RefreshData()
        {
            pbUpload1.Value = 0;
            pbUpload2.Value = 0;

            try
            {

                this.Cursor = Cursors.WaitCursor;

                using (Database db = new Database())
                {


                    db.Commands.Add(db.CreateCommand("usp_NotaAntarCabang_Upload"));
                    db.Commands[0].Parameters.Add(new Parameter("@FromDate", SqlDbType.DateTime, rangeDateBox1.FromDate));
                    db.Commands[0].Parameters.Add(new Parameter("@ToDate", SqlDbType.DateTime, rangeDateBox1.ToDate));
                    db.Commands[0].Parameters.Add(new Parameter("@GudangID", SqlDbType.VarChar, Gudang));
                    dsResult = db.Commands[0].ExecuteDataSet();

                    if (dsResult.Tables.Count > 0)
                    {
                        gridViewNotaPenjualan.DataSource = dsResult.Tables[0];
                        gridViewNotaPenjualanDetail.DataSource = dsResult.Tables[1];
                    }
                    else
                    {
                        MessageBox.Show("Data Tidak Ada");
                    }


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


        private void refreshForm()
        {
            this.Refresh();
            this.Invalidate();
            Application.DoEvents();
        }


        private DataSet GetSyncData()
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            using (Database db = new Database())
            {


                db.Commands.Clear();
                db.Commands.Add(db.CreateCommand("psp_UPLOAD_NotaPenjualan_ISA"));
                db.Commands[0].Parameters.Add(new Parameter("@FromDate", SqlDbType.DateTime, rangeDateBox1.FromDate));
                db.Commands[0].Parameters.Add(new Parameter("@ToDate", SqlDbType.DateTime, rangeDateBox1.ToDate));
                db.Commands[0].Parameters.Add(new Parameter("@GudangID", SqlDbType.VarChar, Gudang));
                dt = db.Commands[0].ExecuteDataTable();
                dt.TableName = "NotaPenjualan";
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }
                foreach (DataRow dr1 in ds.Tables[0].Rows)
                {
                    counter1++;
                    pbUpload1.Minimum = 0;
                    pbUpload1.Maximum = ds.Tables[0].Rows.Count;


                    pbUpload1.Increment(1);
                }


                db.Commands.Clear();
                db.Commands.Add(db.CreateCommand("psp_UPLOAD_NotaPenjualanDetail_ISA"));
                db.Commands[0].Parameters.Add(new Parameter("@FromDate", SqlDbType.DateTime, rangeDateBox1.FromDate));
                db.Commands[0].Parameters.Add(new Parameter("@ToDate", SqlDbType.DateTime, rangeDateBox1.ToDate));
                db.Commands[0].Parameters.Add(new Parameter("@GudangID", SqlDbType.VarChar, Gudang));
                dt = db.Commands[0].ExecuteDataTable();
                dt.TableName = "NotaPenjualanDetail";
                if (dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                }
                foreach (DataRow dr2 in ds.Tables[1].Rows)
                {
                    counter2++;
                    pbUpload2.Minimum = 0;
                    pbUpload2.Maximum = ds.Tables[1].Rows.Count;


                    pbUpload2.Increment(1);
                }


            }
            return ds;
        }


        private void cmdUpload_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataSet ds = GetSyncData();

                if (ds.Tables.Count > 0)
                {
                    string Target = lookupGudang1.GudangID; 
                    string fileOuput = FtpEngine.UploadDirectory + "\\" +  "NPJ-" + DateTime.Now.ToString("yyyyMMdd hhmmss") + " " + Guid.NewGuid().ToString() + ".xml";
                    ds.WriteXml(fileOuput);

                    if (FTP.FtpEngine.Upload(Target, fileOuput))
                    {

                        MessageBox.Show(Messages.Confirm.UploadSuccessful + "\n" + fileOuput);
                    }
                    else
                    {
                        MessageBox.Show(Messages.Confirm.UploadFailed);
                    }
                }
                else
                {
                    MessageBox.Show(Messages.Confirm.NoDataAvailable);
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

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
