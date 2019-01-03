﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ISA.DAL;

namespace ISA.Toko.Master
{
    public partial class frmPemasokBrowse : ISA.Toko.BaseForm
    {
        public frmPemasokBrowse()
        {
            InitializeComponent();
        }

        private void frmPemasokBrowse_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        public void RefreshData()
        {
            try
            {
                using (Database db = new Database())
                {
                    db.Open();

                    DataTable dt = new DataTable();
                    db.Commands.Add(db.CreateCommand("usp_Pemasok_LIST"));

                    dt = db.Commands[0].ExecuteDataTable();
                    dataGridView1.DataSource = dt;

                    db.Close();
                    db.Dispose();
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
        }
              
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            Master.frmPemasokUpdate ifrmChild = new Master.frmPemasokUpdate(this);
            ifrmChild.MdiParent = Program.MainForm;
            Program.MainForm.RegisterChild(ifrmChild);
            ifrmChild.Show();  
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                string rowID = dataGridView1.SelectedCells[0].OwningRow.Cells["PemasokID"].Value.ToString();
                Guid _OrowID = new Guid(dataGridView1.SelectedCells[0].OwningRow.Cells["RowID"].Value.ToString());
                Master.frmPemasokUpdate ifrmChild = new Master.frmPemasokUpdate(this, rowID, _OrowID);
                ifrmChild.MdiParent = Program.MainForm;
                Program.MainForm.RegisterChild(ifrmChild);
                ifrmChild.Show();
            }
            else
            {
                MessageBox.Show(Messages.Error.RowNotSelected);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void DeleteData()
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                string rowID = dataGridView1.SelectedCells[0].OwningRow.Cells["PemasokID"].Value.ToString();
                if (MessageBox.Show("Hapus Pemasok ID: " + rowID + "?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        using (Database db = new Database())
                        {
                            db.Open();

                            DataTable dt = new DataTable();
                            db.Commands.Add(db.CreateCommand("usp_Pemasok_DELETE"));
                            db.Commands[0].Parameters.Add(new Parameter("@PemasokID", SqlDbType.VarChar, rowID));
                            dt = db.Commands[0].ExecuteDataTable();

                            db.Close();
                            db.Dispose();
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
            else
            {
                MessageBox.Show(Messages.Error.RowNotSelected);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void FindRow(string columnName, string value)
        {
            dataGridView1.FindRow(columnName, value);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Keys.Insert:
            //        cmdAdd.PerformClick();
            //        break;
            //    case Keys.Delete:
            //        cmdDelete.PerformClick();
            //        break;
            //    case Keys.Space:
            //        cmdEdit.PerformClick();
            //        break;
            //    case Keys.F5:
            //        RefreshData();
            //        break;
            //}
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        } 
    }
}
