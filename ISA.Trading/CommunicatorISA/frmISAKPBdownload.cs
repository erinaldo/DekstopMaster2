﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ISA.DAL;

namespace ISA.Trading.CommunicatorISA
{
    public partial class frmISAKPBdownload : ISA.Trading.BaseForm
    {
        int counter = 0;
        DataTable fileList;
        bool loaded = false;


        string initDir =FTP.FtpEngine.InboxDirectory;

        private DataSet ReadData(string fullFilePath)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(fullFilePath);
            return ds;
        }

        public frmISAKPBdownload()
        {
            InitializeComponent();
        }

        private void frmISAKPBdownload_Load(object sender, EventArgs e)
        {
            LoadPemasok();
        }

        private void cmdDownload_Click(object sender, EventArgs e)
        {
            if (customGridView1.SelectedCells.Count > 0)
            {
                try
                {
                    bool success = false;
                    string fileName = customGridView1.SelectedCells[0].OwningRow.Cells["FileName"].Value.ToString();
                    DataRow rowItem = fileList.Rows.Find(fileName);
                    if (rowItem != null)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        lblStatus.Text = "Download file from FTP ...";
                        refreshForm();

                        double downloadSize = FTP.FtpEngine.Download(initDir, fileName);
                        if (downloadSize == (double)rowItem["FileSize"] && downloadSize != 0)
                        {
                            success = FTP.FtpEngine.Delete(initDir, fileName);
                            if (success)
                            {
                                rowItem.Delete();

                                DataSet ds = ReadData(FTP.FtpEngine.DownloadDirectory + "\\" + fileName);

                                lblStatus.Text = "Upload data to ISA ....";
                                refreshForm();
                                ProcessTable(ds);

                                MessageBox.Show(Messages.Confirm.DownloadSuccess);
                            }
                        }
                        else
                        {
                            MessageBox.Show(Messages.Error.FailDownload);
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
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refreshForm()
        {
            this.Refresh();
            this.Invalidate();
            Application.DoEvents();
        }

        private void ReadFileList()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //FTP.FtpEngine.InboxDirectory
                fileList = FTP.FtpEngine.GetFileList(initDir);
               fileList.DefaultView.Sort = "FileName";
               fileList.DefaultView.RowFilter = "FileName LIKE 'KoreksiPenjualan11%' OR FileName LIKE 'NotaPenjualan11%'";
                customGridView1.DataSource = fileList.DefaultView.ToTable();
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

        private void frmISAKPBdownload_Shown(object sender, EventArgs e)
        {
            if (!loaded)
            {
                lblStatus.Text = "Get FTP Info ...";
                refreshForm();
                ReadFileList();
                loaded = true;
            }
        }

        private void ProcessTable(DataSet ds)
        {
            counter = 0;
            pbSyncUpload.Value = 0;
            foreach (DataTable dt in ds.Tables)
            {
                counter++;
                lblStatus.Text = "Download " + dt.TableName;
                refreshForm();
                ImportData(dt);
                pbSyncUpload.Value = counter;
                lblUpload.Text = counter.ToString("#,##0") + "/" + ds.Tables.Count.ToString("#,##0");
                refreshForm();
            }
            pbSyncUpload.Value = pbSyncUpload.Maximum;
        }


        private void ImportData(DataTable dt)
        {
            using (Database db = new Database())
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dt.Columns.Contains("XmlData"))
                    {
                        if (dr["XmlData"] != null && dr["XmlData"].ToString() != "")
                        {
                            db.Commands.Clear();
                            db.Commands.Add(db.CreateCommand("psp_SYNC_DOWNLOAD_DIRECTOR"));
                            db.Commands[0].Parameters.Add(new Parameter("@tableName", SqlDbType.VarChar, dr["TableName"].ToString()));
                            db.Commands[0].Parameters.Add(new Parameter("@method", SqlDbType.VarChar, dr["Method"].ToString()));
                            db.Commands[0].Parameters.Add(new Parameter("@xmlData", SqlDbType.VarChar, dr["XmlData"].ToString()));
                            if (comboBox1.SelectedIndex == 1)
                                db.Commands[0].Parameters.Add(new Parameter("@Pemasok", SqlDbType.VarChar, cboPemasok.Text));
                            db.Commands[0].ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                fileList.DefaultView.RowFilter = "FileName LIKE 'KoreksiPenjualan11%'";
                label2.Visible = false ;
                cboPemasok.Visible = false;
            }
            else
            {
                fileList.DefaultView.RowFilter = "FileName LIKE 'NotaPenjualan11%'";
                label2.Visible = true;
                cboPemasok.Visible = true;
            }
            customGridView1.DataSource = fileList.DefaultView.ToTable();
        }

        private void LoadPemasok()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (Database db = new Database())
                {
                    DataTable dt = new DataTable();
                    db.Commands.Add(db.CreateCommand("usp_Pemasok_LIST"));
                    dt = db.Commands[0].ExecuteDataTable();

                    cboPemasok.ValueMember = "PemasokID";
                    cboPemasok.DisplayMember = "Nama";
                    cboPemasok.DataSource = dt;
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
    }
}