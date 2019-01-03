﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ISA.DAL;

namespace ISA.Trading.Pembelian
{
    public partial class frmKoreksiReturBeliBrowser : ISA.Trading.BaseForm
    {
        Guid _returBeliDetailID;
        bool _acak = true;
        string _format;

        public frmKoreksiReturBeliBrowser(Guid returBeliDetailID)
        {
            InitializeComponent();
            _returBeliDetailID = returBeliDetailID;
        }

        private void frmKoreksiReturBeliBrowser_Load(object sender, EventArgs e)
        {
            this.Title = "Koreksi Pembelian";
            this.Text = "Pembelian";
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Focus();
            RefreshDataKoreksiBeli();
        }

        public void RefreshDataKoreksiBeli()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (Database db = new Database())
                {
                    DataTable dtKoreksi = new DataTable();
                    db.Commands.Add(db.CreateCommand("usp_KoreksiReturPembelian_LIST"));
                    db.Commands[0].Parameters.Add(new Parameter("@returBeliDetailID", SqlDbType.UniqueIdentifier, _returBeliDetailID));

                    dtKoreksi = db.Commands[0].ExecuteDataTable();
                    dataGridView1.DataSource = dtKoreksi;
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
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount != 0)
                {
                    MessageBox.Show("Sudah ada koreksi," + System.Environment.NewLine
                            + "tidak bisa buat koreksi lagi!");
                    return;
                }
                
                Pembelian.frmKoreksiReturBeliUpdate ifrmChild = new Pembelian.frmKoreksiReturBeliUpdate(this, _returBeliDetailID);
                ifrmChild.MdiParent = Program.MainForm;
                Program.MainForm.RegisterChild(ifrmChild);
                ifrmChild.Show();
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

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show(Messages.Error.RowNotSelected);
                return;
            }

            DateTime tglKoresksi = (DateTime)dataGridView1.SelectedCells[0].OwningRow.Cells["TglKoreksi"].Value;
            if (tglKoresksi.ToShortDateString() != DateTime.Now.ToShortDateString())
            {
                MessageBox.Show("Koreksi hanya boleh dihapus dihari"+System.Environment.NewLine+"yang sama dengan hari pembuatannya!");
                return;
            }
            try
            {
                GlobalVar.LastClosingDate = tglKoresksi;
                if (tglKoresksi <= GlobalVar.LastClosingDate)
                {
                    throw new Exception(string.Format(ISA.Trading.Messages.Error.AlreadyClosingPJT, GlobalVar.LastClosingDate));
                }
                if (MessageBox.Show("Hapus record ini?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Guid rowID = (Guid)dataGridView1.SelectedCells[0].OwningRow.Cells["RowID"].Value;

                    this.Cursor = Cursors.WaitCursor;
                    using (Database db = new Database())
                    {
                        db.Commands.Add(db.CreateCommand("usp_KoreksiReturPembelian_DELETE"));
                        db.Commands[0].Parameters.Add(new Parameter("@rowID", SqlDbType.UniqueIdentifier, rowID));
                        db.Commands[0].ExecuteNonQuery();
                    }

                    MessageBox.Show("Record telah dihapus");
                    this.RefreshDataKoreksiBeli();

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

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {        
            switch (e.KeyCode)
            {
                case Keys.F9:
                    AcakTampilHrg();
                    break;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format currency
            if (!_acak)
                _format = "#,##0.00";
            else
                _format = "XXXXXX";

            dataGridView1.Rows[e.RowIndex].Cells["HrgBeli"].Style.Format = _format;
            dataGridView1.Rows[e.RowIndex].Cells["Potongan"].Style.Format = _format;
            dataGridView1.Rows[e.RowIndex].Cells["HrgBeliKoreksi"].Style.Format = _format;
        }

        private void AcakTampilHrg()
        {
            if (_acak)
            {
                _format = "#,##0.00";
                _acak = false;
            }
            else
            {
                _format = "XXXXXX";
                _acak = true;
            }
            dataGridView1.Columns["HrgBeli"].DefaultCellStyle.Format = _format;
            dataGridView1.Columns["Potongan"].DefaultCellStyle.Format = _format;
            dataGridView1.Columns["HrgBeliKoreksi"].DefaultCellStyle.Format = _format;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void FindHeader(string columnName, string value)
        {
            dataGridView1.FindRow(columnName, value);
        }
    }
}
