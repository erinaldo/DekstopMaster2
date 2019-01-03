﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ISA.DAL;
using ISA.Trading;
using System.Data.SqlTypes;

namespace ISA.Trading.PO
{
    public partial class frmPOUpdate : ISA.Trading.BaseForm
    {

        enum enumFormMode { Update, New };
        enumFormMode formMode;
        DataTable dtPO;
        Guid _rowID;
        bool Closing;
        string strNoPO;
        string CekOrder = "0";


        public frmPOUpdate(Form caller)
        {
            InitializeComponent();
            formMode = enumFormMode.New;
            this.Caller = caller;
        }
        public frmPOUpdate(Form caller, Guid rowID, bool CLosing_)
        {
            InitializeComponent();
            formMode = enumFormMode.Update;
            _rowID = rowID;
            this.Caller = caller;
            Closing = CLosing_;
        }

        private void frmPOUpdate_Load(object sender, EventArgs e)
        {
            
            this.Title = "Update PO Header";
            try
            {
                //this.Cursor = Cursors.WaitCursor;
                dtPO = new DataTable();
                DataTable dt1 = new DataTable();
                using (Database db = new Database())
                {
                    if (formMode == enumFormMode.Update)
                    {
                        db.Commands.Add(db.CreateCommand("usp_POH_List"));
                        db.Commands[0].Parameters.Add(new Parameter("@row", SqlDbType.UniqueIdentifier, _rowID));
                        dtPO = db.Commands[0].ExecuteDataTable();
                    }

                }
                
                if (formMode == enumFormMode.Update)
                {
                    textBox1.Text = dtPO.Rows[0]["no_po"].ToString();
                    tbAdmin.Text = dtPO.Rows[0]["admin"].ToString();
                    tbGudang.Text = dtPO.Rows[0]["gudang"].ToString();
                    tbCatatan.Text = dtPO.Rows[0]["keterangan"].ToString().Replace(Environment.NewLine, string.Empty).TrimEnd();
                    DateTime _tanggal = (DateTime)dtPO.Rows[0]["tgl_po"];
                    DateTime _tanggalawal = (DateTime)dtPO.Rows[0]["tanggal1"];
                    DateTime _tanggalAkhir = (DateTime)dtPO.Rows[0]["tanggal2"];
                    dtpTanggal.Value = _tanggal;
                    dtpAwal.Value = _tanggalawal;
                    dtpAkhir.Value = _tanggalAkhir;
                    textBox1.ReadOnly = true;
                    tbAdmin.ReadOnly = true;
                    if (tbCatatan.Text.ToString().Trim() == "PO ke 00")
                        cbOrder.CheckState = CheckState.Checked;
                    else
                        cbOrder.CheckState = CheckState.Unchecked;
                

                    }
                    else
                    {
                    }

            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbSave_Click(object sender, EventArgs e)
        {
            UpdatePO(strNoPO);
            frmPO frmCaller = (frmPO)this.Caller;
            frmCaller.RefreshHeader();
            this.Close();
        }

        private void UpdatePO(string NoPO)
        {
            //MessageBox.Show("masuk ke insert");
            using (Database db = new Database())
            {
                DataTable dt = new DataTable();
                try
                {

                    db.Commands.Add(db.CreateCommand("usp_POHeader_UPDATE"));
                    db.Commands[0].Parameters.Add(new Parameter("@rowID", SqlDbType.UniqueIdentifier, _rowID));
                    db.Commands[0].Parameters.Add(new Parameter("@idtr", SqlDbType.VarChar, dtPO.Rows[0]["idtr"]));
                    db.Commands[0].Parameters.Add(new Parameter("@no_po", SqlDbType.VarChar, textBox1.Text));
                    db.Commands[0].Parameters.Add(new Parameter("@admin", SqlDbType.VarChar, tbAdmin.Text));
                    db.Commands[0].Parameters.Add(new Parameter("@gudang", SqlDbType.VarChar, tbGudang.Text));
                    db.Commands[0].Parameters.Add(new Parameter("@keterangan", SqlDbType.VarChar, tbCatatan.Text.Replace(Environment.NewLine, string.Empty).TrimEnd()));
                                         
                    db.Commands[0].ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Di simpan");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    db.RollbackTransaction();
                    MessageBox.Show("Gagal Menyimpan Data");
                }
            }
        }

        private void cbOrder_CheckedChanged(object sender, EventArgs e)
        {
            string cCatatan = Tools.isNull(this.tbCatatan.Text, "").ToString().Trim();
            if (cCatatan == "POKE00")
                cCatatan = "";

            if (cbOrder.CheckState.ToString() == "Checked")
            {
                CekOrder = "1";
                this.tbCatatan.Enabled = false;
                tbCatatan.Text = "POKE00";
            }
            else
            {
                CekOrder = "0";
                this.tbCatatan.Enabled = true;
                tbCatatan.Text = cCatatan;
            }
        }
    }
}