﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ISA.DAL;
using ISA.Finance.Class;
using Microsoft.Reporting.WinForms;
using System.Data.SqlTypes;
using ISA.Common;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Diagnostics;

namespace ISA.Finance.PJ3
{
    public partial class frmKoreksiPenjualanBrowse : ISA.Controls.BaseForm
    {
        Guid _notaJualDetailID;
        DataTable dtNotaDetail;
        bool _acak;
        string _format, _recID;

        public frmKoreksiPenjualanBrowse(Form caller, Guid notaJualDetailID, string recID)
        {
            InitializeComponent();
            _notaJualDetailID = notaJualDetailID;
            _recID = recID;
            this.Caller = caller;
        }

        private void frmKoreksiPenjualanBrowse_Load(object sender, EventArgs e)
        {
            _acak = true;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Focus();
            RefreshDataKoreksiJual();
        }

        public void RefreshDataKoreksiJual()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dtKoreksi = new DataTable();
                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("usp_KoreksiPenjualan_LIST")); //cek heri
                    db.Commands[0].Parameters.Add(new Parameter("@notaJualDetailID", SqlDbType.UniqueIdentifier, _notaJualDetailID));
                    dtKoreksi = db.Commands[0].ExecuteDataTable();
                }
                dataGridView1.DataSource = dtKoreksi;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                return;
            }
            if (_recID.Substring(0, 3) != GlobalVar.PerusahaanID)
            {
                MessageBox.Show("Maaf, hapus koreksi hanya dapat dilakukan di cabang/posko " + GlobalVar.PerusahaanID + " saja");
                return;
            }
            if (dataGridView1.SelectedCells[0].OwningRow.Cells["LinkID"].Value.ToString() != "")
            {
                MessageBox.Show("DATA SUDAH LINK !, TIDAK BISA DIHAPUS...");
                return;
            }
            if (MessageBox.Show("Hapus record ini?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Guid rowID = (Guid)dataGridView1.SelectedCells[0].OwningRow.Cells["RowID"].Value;
                try
                {
                    GlobalVar.LastClosingDate = (DateTime)dataGridView1.SelectedCells[0].OwningRow.Cells["TglKoreksi"].Value;
                    if ((DateTime)dataGridView1.SelectedCells[0].OwningRow.Cells["TglKoreksi"].Value <= GlobalVar.LastClosingDate)
                    {
                        MessageBox.Show("Sudah Closing, tidak bisa di Delete.");
                        return;
                    }
                    this.Cursor = Cursors.WaitCursor;
                    using (Database db = new Database())
                    {
                        db.Commands.Add(db.CreateCommand("usp_KoreksiPenjualan_DELETE"));
                        db.Commands[0].Parameters.Add(new Parameter("@rowID", SqlDbType.UniqueIdentifier, rowID));
                        db.Commands[0].ExecuteNonQuery();
                    }
                    MessageBox.Show("Record telah dihapus");
                    this.RefreshDataKoreksiJual();
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
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount != 0)
            {
                MessageBox.Show("Sudah ada koreksi," + System.Environment.NewLine
                        + "tidak bisa buat koreksi lagi!");
                return;
            }
            if (ChekKoreksi())
            {
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dtNotaDetail = new DataTable();
                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("usp_NotaPenjualanDetail_LIST_FILTER_RowID")); //cek heri 12032013
                    db.Commands[0].Parameters.Add(new Parameter("@rowID", SqlDbType.UniqueIdentifier, _notaJualDetailID));
                    dtNotaDetail = db.Commands[0].ExecuteDataTable();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            Finance.PJ3.frmKoreksiPenjualanUpdate ifrmChild = new Finance.PJ3.frmKoreksiPenjualanUpdate(this, dtNotaDetail);
            ifrmChild.ShowDialog();
            if (ifrmChild.DialogResult == DialogResult.OK)
            {
                this.Close();
            }
        }

        private bool ChekKoreksi()
        {

            Finance.PJ3.frmPJ3Browse frmCaller = (Finance.PJ3.frmPJ3Browse)this.Caller;

            //if (frmCaller.CekSudahPernahKoreksi())
            //{
            //    MessageBox.Show("Record ini sudah pernah di Koreksi, tidak boleh dikoreksi");
            //    return true;
            //}

            if (frmCaller.CekSudahPernahRetur())
            {
                MessageBox.Show("Record ini sudah pernah diretur, tidak boleh dikoreksi");
                return true;
            }
            if (frmCaller.CekSudahAdaPotongan())
            {
                MessageBox.Show("Nota ini sudah pernah terjadi potongan (DIL), tidak boleh dikoreksi");
                return true;
            }

            return false;
        }

        public void FindRow(string columnName, string value)
        {
            dataGridView1.FindRow(columnName, value);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F9:
                    AcakTampilHrg();
                    break;
            }
        }

        private void AcakTampilHrg()
        {
            _acak = !_acak;

            dataGridView1.Columns["HrgJual"].Visible = !_acak;
            dataGridView1.Columns["HrgJualKoreksi"].Visible = !_acak;

            dataGridView1.Columns["HrgJualAck"].Visible = _acak;
            dataGridView1.Columns["HrgJualKoreksiAck"].Visible = _acak;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            double hrgJualBaru = double.Parse(Tools.isNull(dataGridView1.Rows[e.RowIndex].Cells["HrgJual"].Value, 0).ToString());
            double hrgJualKor =  double.Parse(Tools.isNull(dataGridView1.Rows[e.RowIndex].Cells["HrgJualKoreksi"].Value, 0).ToString());

            dataGridView1.Rows[e.RowIndex].Cells["HrgJual"].Style.Format = "#,##0";
            dataGridView1.Rows[e.RowIndex].Cells["HrgJualKoreksi"].Style.Format = "#,##0";

            dataGridView1.Rows[e.RowIndex].Cells["HrgJualAck"].Value = Tools.GetAntiNumeric(hrgJualBaru.ToString("#,##0"));
            if (hrgJualKor > 0)
                dataGridView1.Rows[e.RowIndex].Cells["HrgJualKoreksiAck"].Value = Tools.GetAntiNumeric(hrgJualKor.ToString("#,##0"));
            else
                dataGridView1.Rows[e.RowIndex].Cells["HrgJualKoreksiAck"].Value = "-"+Tools.GetAntiNumeric(Math.Abs(hrgJualKor).ToString("#,##0")).ToString();
            dataGridView1.Rows[e.RowIndex].Cells["HrgJualAck"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Rows[e.RowIndex].Cells["HrgJualKoreksiAck"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

    }
}
