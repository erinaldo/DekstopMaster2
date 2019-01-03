﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ISA.DAL;
using ISA.Bengkel.Library;
using ISA.Bengkel;
using ISA.Bengkel.Helper;

namespace ISA.Bengkel.Master
{
    public partial class frmStandarServiceBrowse : ISA.Bengkel.BaseForm
    {
        public frmStandarServiceBrowse()
        {
            InitializeComponent();
        }

        private void frmStandarServiceBrowse_Load(object sender, EventArgs e)
        {        

            RefreshData();
        }

        public void RefreshData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; 
                using (Database db = new Database())
                {
                    DataTable dt = new DataTable();
                    db.Commands.Add(db.CreateCommand("usp_bkl_hpoint_LIST"));
                    dt = db.Commands[0].ExecuteDataTable();
                    dgMain.DataSource = dt;
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

        private void cmdADD_Click(object sender, EventArgs e)
        {
            frmStandarServiceUpdate ifrmChild = new frmStandarServiceUpdate(this);
            //ifrmChild.MdiParent = Program.MainForm;
            //Program.MainForm.RegisterChild(ifrmChild);
            ifrmChild.ShowDialog();
        }

        private void cmdEDIT_Click(object sender, EventArgs e)
        {
            if (!FormTools.IsRowSelected(dgMain))
            {
                return;
            }

            Guid rowID;

            rowID = (Guid)dgMain.SelectedCells[0].OwningRow.Cells["RowID"].Value;
            frmStandarServiceUpdate ifrmChild = new frmStandarServiceUpdate(this, rowID);
            //ifrmChild.MdiParent = Program.MainForm;
            //Program.MainForm.RegisterChild(ifrmChild);
            ifrmChild.ShowDialog();
        }

        private void cmdDELETE_Click(object sender, EventArgs e)
        {
            if (!FormTools.IsRowSelected(dgMain))
            {
                MessageBox.Show(Messages.Error.RowNotSelected);
                return;
            }


            //if (!FormTools.IsLinkData())
            //{
            //    MessageBox.Show(Messages.Error.RowNotSelected);
            //    return;
            //}

            deleteData();
        }

        private void deleteData()
        {
           
            Guid rowID = (Guid)dgMain.SelectedCells[0].OwningRow.Cells["RowID"].Value;
            if (MessageBox.Show("Hapus StandarService id: " + rowID + " ?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (Database db = new Database())
                    {
                        DataTable dt = new DataTable();
                        db.Commands.Add(db.CreateCommand("usp_bkl_hpoint_DELETE"));
                        db.Commands[0].Parameters.Add(new Parameter("@RowID", SqlDbType.UniqueIdentifier, rowID));
                        dt = db.Commands[0].ExecuteDataTable();
                    }

                    MessageBox.Show("Record telah dihapus");
                    this.RefreshData();
                }
                catch (Exception ex)
                {
                    Error.LogError(ex);
                }
            }           
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void FindRow(string columnName, string value)
        {
            dgMain.FindRow(columnName, value);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                deleteData();
            }
        }
    }
}