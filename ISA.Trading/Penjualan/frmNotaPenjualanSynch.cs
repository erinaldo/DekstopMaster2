﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISA.Utility;
using ISA.DAL;
using System.Globalization;

namespace ISA.Trading.Penjualan
{
    public partial class frmNotaPenjualanSynch : ISA.Trading.BaseForm
    {
        string InitGudang;
        bool hasSynch = false;

        string cab1 = "", cab2 = "";
        string successMessage = "";

        DataTable dtNota;

        public DialogResult Result
        {
            get { return (hasSynch ? DialogResult.OK : DialogResult.Cancel); }
        }

        public frmNotaPenjualanSynch()
        {
            InitializeComponent();

            using (var db = new Database())
            {
                db.Commands.Add(db.CreateCommand("[usp_Perusahaan_LIST]"));
                DataTable dtbl = db.Commands[0].ExecuteDataTable();

                if (dtbl.Rows.Count > 0)
                {
                    InitGudang = dtbl.Rows[0]["InitGudang"].ToString();
                }
                else MessageBox.Show("Perusahaan tidak di temukan");
            }
        }

        private void frmNotaPenjualanSynch_Load(object sender, EventArgs e)
        {
            GVHeader.AutoGenerateColumns = false;
            if (InitGudang == null || InitGudang == "") this.Close();
            rangeDateBox1.FromDate = rangeDateBox1.ToDate = DateTime.Now;
        }

        private void btn_Clicked(object sender, EventArgs e)
        {
            if (sender.Equals(btnSearch)) GetData();
            else if (sender.Equals(btnClose)) this.Close();
            else if (sender.Equals(btnDownload))
            {
                List<int> idx = new List<int>();
                foreach (DataGridViewRow row in GVHeader.Rows)
                {
                    if (Boolean.Parse(row.Cells["colCheck"].Value.ToString())) idx.Add(int.Parse(row.Cells["colid"].Value.ToString()));
                }

                if (idx.Count > 0) ImportData(idx.ToArray());
                else MessageBox.Show("Tidak ada item untuk di synch");
            }
        }


        JSON mdat;
        DataSet dset;

        InPopup ipProgress;
        FakeProgress fpProgress;

        private void GetData()
        {
            Form thisx = this;

            if (ipProgress == null) ipProgress = new InPopup(this, pnlProgress);
            if (fpProgress == null) fpProgress = new FakeProgress(progbProgress);

            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += (a, b) =>
            {
                fpProgress.Start();

                JSON opt = new JSON(JSONType.Object);
                opt.ObjAdd("gudang", new JSON(InitGudang));
                opt.ObjAdd("fromdate", new JSON(rangeDateBox1.FromDate.Value.ToString("yyyy-MM-dd")));
                opt.ObjAdd("todate", new JSON(rangeDateBox1.ToDate.Value.ToString("yyyy-MM-dd")));

                string host = "http://devwiserdc.sas-autoparts.com:8000";
                using (var db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("[usp_AppSetting_LIST]"));
                    db.Commands[0].Parameters.Add(new Parameter("@Key", SqlDbType.VarChar, "WiserDC_Host"));
                    DataTable dtbl = db.Commands[0].ExecuteDataTable();

                    if (dtbl.Rows.Count > 0) host = dtbl.Rows[0]["Value"].ToString();
                    else throw new Exception("Wiser belum di setting");
                }

                XNet xn = new XNet(host + "/api/notapenjualan/get", XNetMethod.GET);
                //XNet xn = new XNet("https://postman-echo.com/get", XNetMethod.GET);
                XNetThread xnt = xn.Send(opt, c =>
                {
                    if (bgw.CancellationPending) return;
                    ///return;
                    try
                    {
                        if (c.Error != null) throw new Exception("Terjadi error: " + c.Error.Message);
                        else if (c.Output.Length > 0)
                        {

                            JSON jdat = JSON.Parse(c.Output);
                            if (jdat.Type == JSONType.Object)
                            {
                                if (jdat.ObjExists("Result") && jdat["Result"].BoolValue)
                                {
                                    if (jdat.ObjExists("Data"))
                                    {
                                        DataTable dtbl0 = new DataTable();
                                        DataTable dtbl1 = new DataTable();
                                        //return;
                                        dtbl0.Columns.Add("check");
                                        foreach (string k in jdat["Data"].ObjKeys)
                                        {
                                            JSON cur = jdat["Data"][k];
                                            List<object> itm = new List<object>();

                                            itm.Add(true);
                                            foreach (string k2 in cur.ObjKeys)
                                            {
                                                switch (k2)
                                                {
                                                    case "Details":
                                                        // do nothing
                                                        break;
                                                    default:
                                                        if (dtbl0.Rows.Count <= 0) dtbl0.Columns.Add(k2);
                                                        itm.Add(cur[k2].Value);
                                                        break;
                                                }
                                            }

                                            dtbl0.Rows.Add(itm.ToArray());
                                            foreach (string k2 in cur["Details"].ObjKeys)
                                            {
                                                JSON cur2 = cur["Details"][k2];
                                                itm = new List<object>();
                                                foreach (string k3 in cur2.ObjKeys)
                                                {
                                                    switch (k3)
                                                    {
                                                        default:
                                                            if (dtbl1.Rows.Count <= 0) dtbl1.Columns.Add(k3);
                                                            itm.Add(cur2[k3].Value);
                                                            break;
                                                    }
                                                }
                                                dtbl1.Rows.Add(itm.ToArray());
                                            }
                                        }
                                        dset = new DataSet();
                                        mdat = jdat["Data"];
                                        dset.Tables.Add(dtbl0);
                                        dset.Tables.Add(dtbl1);

                                        GVHeader.Invoke(new Action(() => GVHeader.DataSource = dset.Tables[0]));

                                        b.Result = true;
                                        return;
                                    }
                                    throw new Exception("Response server is not expected");
                                }
                                else
                                {
                                    if (jdat.ObjExists("Msg"))
                                    {
                                        //throw new Exception("Server error: " + jdat["Msg"]);
                                        MessageBox.Show("Server error: " + jdat["Msg"]);
                                        return;
                                    }
                                    else
                                    {
                                        //throw new Exception("Server error: " + jdat.ToString());
                                        MessageBox.Show("Server error: " + jdat.ToString()); 
                                        return;
                                    }
                                }
                            }
                            else 
                            { 
                                //throw new Exception("Server error: " + c.Output);
                                MessageBox.Show(c.Output);
                                return;
                            }
                        }
                        else throw new Exception("Tidak ada response dari server");
                    }
                    catch (Exception ex)
                    {
                        b.Result = ex.Message;
                    }
                });

                while (xnt.OnWorking)
                {
                    if (bgw.CancellationPending)
                    {
                        b.Cancel = true;
                        xnt.Cancel();
                        break;
                    }
                };
                if (b.Result != null && !b.Result.Equals(true))
                {
                    MessageBox.Show(b.Result.ToString());
                    return;
                } //throw new Exception(b.Result.ToString());
            };
            bgw.RunWorkerCompleted += (a, b) =>
            {
                bool r = false;
                if (b.Cancelled) MessageBox.Show(thisx, "Operasi di gagalkan");
                else if (b.Error != null) MessageBox.Show(thisx, b.Error.Message);
                else r = true;

                ipProgress.Close(r);
            };

            ipProgress.OpenDialog(this, a =>
            {
            }, () => bgw.CancelAsync());
            bgw.RunWorkerAsync();
        }

        private void ImportData(int[] ids)
        {
            Form thisx = this;

            if (ipProgress == null) ipProgress = new InPopup(this, pnlProgress);
            if (fpProgress == null) fpProgress = new FakeProgress(progbProgress);

            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += (a, b) =>
            {
                fpProgress.Start();

                using (var db = new Database())
                {
                    if (bgw.CancellationPending) return;

                    db.Commands.Add(db.CreateCommand("[usp_AppSetting_LIST]"));
                    db.Commands[0].Parameters.Add(new Parameter("@Key", SqlDbType.VarChar, "WiserDC_Host"));
                    DataTable dtbl2 = db.Commands[0].ExecuteDataTable();

                    string host = "http://devwiserdc.sas-autoparts.com:8000";
                    if (dtbl2.Rows.Count > 0) host = dtbl2.Rows[0]["Value"].ToString();
                    else
                    {
                        MessageBox.Show("Download gagal, Wiser belum di setting");
                        return;
                    }

                    Guid _headerRowID, _detailID;
                    List<int> scs = new List<int>();
                    foreach (int i in ids)
                    {
                        JSON cur = mdat[i.ToString()];
                        List<Parameter> cmdl = new List<Parameter>();
                        db.Commands.Clear();

                        //DateTime tTerima = DateTime.ParseExact(cur["TglTerima"].StringValue, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        //MessageBox.Show(cur["NotaRowID"].GuidValue(DBNull.Value).ToString());
                        //return;
                        if (cur["NotaRowID"].GuidValue(DBNull.Value) == DBNull.Value)
                        {
                            _headerRowID = Guid.NewGuid();
                        }
                        else 
                        {
                            _headerRowID = (Guid)cur["NotaRowID"].GuidValue(DBNull.Value);
                        }
                        cmdl.Add(new Parameter("@RowID", SqlDbType.UniqueIdentifier, _headerRowID));
                        cmdl.Add(new Parameter("@HtrID", SqlDbType.VarChar, FingerPrintWiser(cur["wiserid"].StringValue)));
                        cmdl.Add(new Parameter("@RecordID", SqlDbType.VarChar, FingerPrintWiser(cur["wiserid"].StringValue)));
                        cmdl.Add(new Parameter("@DOID", SqlDbType.UniqueIdentifier, cur["DOID"].GuidValue(DBNull.Value)));
                        cmdl.Add(new Parameter("@Cabang1", SqlDbType.VarChar, cur["Cabang1"].StringValue));
                        cmdl.Add(new Parameter("@Cabang2", SqlDbType.VarChar, cur["Cabang2"].StringValue));
                        cmdl.Add(new Parameter("@Cabang3", SqlDbType.VarChar, cur["Cabang3"].StringValue));
                        cmdl.Add(new Parameter("@NoNota", SqlDbType.VarChar, cur["NoNota"].StringValue));
                        cmdl.Add(new Parameter("@TglNota", SqlDbType.DateTime, cur["TglNota"].DateTimeValue(DBNull.Value)));
                        cmdl.Add(new Parameter("@NoSuratJalan", SqlDbType.VarChar, cur["NoSuratJalan"].StringValue));
                        cmdl.Add(new Parameter("@TglSuratJalan", SqlDbType.DateTime, cur["TglSuratJalan"].DateTimeValue(DBNull.Value)));
                        cmdl.Add(new Parameter("@KodeToko", SqlDbType.VarChar, cur["KodeToko"].StringValue));
                        cmdl.Add(new Parameter("@KodeSales", SqlDbType.VarChar, cur["KodeSales"].StringValue));
                        cmdl.Add(new Parameter("@TglTerima", SqlDbType.DateTime, cur["TglTerima"].DateTimeValue(DBNull.Value)));
                        cmdl.Add(new Parameter("@TglSerahTerimaChecker", SqlDbType.DateTime, cur["TglSerahTerimaChecker"].DateTimeValue(DBNull.Value)));
                        cmdl.Add(new Parameter("@TglExpedisi", SqlDbType.DateTime, cur["TglExpedisi"].DateTimeValue(DBNull.Value)));
                        cmdl.Add(new Parameter("@AlamatKirim", SqlDbType.VarChar, cur["AlamatKirim"].StringValue));
                        cmdl.Add(new Parameter("@Kota", SqlDbType.VarChar, cur["Kota"].StringValue));
                        cmdl.Add(new Parameter("@isClosed", SqlDbType.Bit, 0));
                        cmdl.Add(new Parameter("@Catatan1", SqlDbType.VarChar, cur["Catatan1"].StringValue));
                        cmdl.Add(new Parameter("@Catatan2", SqlDbType.VarChar, cur["Catatan2"].StringValue));
                        cmdl.Add(new Parameter("@Catatan3", SqlDbType.VarChar, cur["Catatan3"].StringValue));
                        cmdl.Add(new Parameter("@Catatan4", SqlDbType.VarChar, cur["Catatan4"].StringValue));
                        cmdl.Add(new Parameter("@Catatan5", SqlDbType.VarChar, cur["Catatan5"].StringValue));
                        cmdl.Add(new Parameter("@SyncFlag", SqlDbType.Bit, 0));
                        //cmdl.Add(new Parameter("@LinkID", SqlDbType.VarChar, ""));
                        cmdl.Add(new Parameter("@NPrint", SqlDbType.Int, cur["NPrint"].NumberValue));
                        cmdl.Add(new Parameter("@TransactionType", SqlDbType.VarChar, cur["TransactionType"].StringValue));
                        cmdl.Add(new Parameter("@HariKredit", SqlDbType.Int, cur["HariKredit"].NumberValue));
                        cmdl.Add(new Parameter("@HariKirim", SqlDbType.Int, cur["HariKirim"].NumberValue));
                        cmdl.Add(new Parameter("@HariSales", SqlDbType.Int, cur["HariSales"].NumberValue));
                        cmdl.Add(new Parameter("@Checker1", SqlDbType.VarChar, cur["Checker1"].StringValue));
                        cmdl.Add(new Parameter("@Checker2", SqlDbType.VarChar, cur["Checker2"].StringValue));
                        cmdl.Add(new Parameter("@LastUpdatedBy", SqlDbType.VarChar, cur["LastUpdatedBy"].StringValue));
                        
                        DateTime TglSuratJalan = new DateTime();
                        DateTime.TryParse(cur["TglNota"].DateTimeValue(DBNull.Value).ToString(), out TglSuratJalan);
                        string barcode = cur["NoNota"].StringValue + TglSuratJalan.ToString("yyyy").Substring(1, 3) + TglSuratJalan.ToString("MM");

                        cmdl.Add(new Parameter("@Barcode", SqlDbType.VarChar, barcode));
                        cmdl.Add(new Parameter("@WiserTag", SqlDbType.VarChar, "WISERDC"));
                        cmdl.Add(new Parameter("@wiserid", SqlDbType.Int, cur["wiserid"].NumberValue));

                        try
                        {
                            db.Commands.Clear();
                            db.Commands.Add(db.CreateCommand("usp_NotaPenjualan_WISERDC_SET"));
                            db.Commands[0].Parameters = cmdl;
                            DataTable dtbl = db.Commands[0].ExecuteDataTable();
                            
                            if (dtbl.Rows.Count > 0)
                            {
                                if (dtbl.Rows[0]["Result"].ToString() == "1")
                                {
                                    Guid id = (Guid)dtbl.Rows[0]["RowID"];

                                    int cid = 0;
                                    bool itOk = true;
                                    bool itTrasaction = false;

                                    try
                                    {
                                        db.Commands.Clear();
                                        itTrasaction = true;
                                        db.BeginTransaction();

                                        foreach (string ck in cur["Details"].ObjKeys)
                                        {
                                            _detailID = Guid.NewGuid();
                                            JSON curd = cur["Details"][ck];

                                            cmdl = new List<Parameter>();
                                            cmdl.Add(new Parameter("@RowID", SqlDbType.UniqueIdentifier, _detailID));
                                            cmdl.Add(new Parameter("@HeaderID", SqlDbType.UniqueIdentifier, id));
                                            cmdl.Add(new Parameter("@DOID", SqlDbType.UniqueIdentifier, curd["DOID"].GuidValue(DBNull.Value)));
                                            cmdl.Add(new Parameter("@DODetailID", SqlDbType.UniqueIdentifier, curd["DODetailID"].GuidValue(DBNull.Value)));
                                            cmdl.Add(new Parameter("@RecordID", SqlDbType.VarChar, FingerPrintWiser(curd["wiserid"].StringValue)));
                                            cmdl.Add(new Parameter("@HtrID", SqlDbType.VarChar, FingerPrintWiser(curd["wiserid"].StringValue)));
                                            cmdl.Add(new Parameter("@KodeGudang", SqlDbType.VarChar, curd["KodeGudang"].StringValue));
                                            cmdl.Add(new Parameter("@BarangID", SqlDbType.VarChar, curd["BarangID"].StringValue));
                                            cmdl.Add(new Parameter("@QtySuratJalan", SqlDbType.Int, curd["QtySuratJalan"].NumberValue));
                                            cmdl.Add(new Parameter("@QtyNota", SqlDbType.Int, curd["QtyNota"].NumberValue));
                                            cmdl.Add(new Parameter("@HrgJual", SqlDbType.Money, curd["HrgJual"].NumberValue));
                                            cmdl.Add(new Parameter("@Disc1", SqlDbType.Decimal, curd["Disc1"].NumberValue));
                                            cmdl.Add(new Parameter("@Disc2", SqlDbType.Decimal, curd["Disc2"].NumberValue));
                                            cmdl.Add(new Parameter("@Disc3", SqlDbType.Decimal, curd["Disc3"].NumberValue));
                                            cmdl.Add(new Parameter("@Pot", SqlDbType.Money, 0));
                                            cmdl.Add(new Parameter("@DiscFormula", SqlDbType.VarChar, ""));
                                            cmdl.Add(new Parameter("@QtyKoli", SqlDbType.Int, curd["QtyKoli"].NumberValue));
                                            cmdl.Add(new Parameter("@KoliAwal", SqlDbType.Int, curd["KoliAwal"].NumberValue));
                                            cmdl.Add(new Parameter("@KoliAkhir", SqlDbType.Int, curd["KoliAkhir"].NumberValue));
                                            cmdl.Add(new Parameter("@NoKoli", SqlDbType.VarChar, curd["NoKoli"].StringValue));
                                            cmdl.Add(new Parameter("@Catatan", SqlDbType.VarChar, curd["Catatan"].StringValue));
                                            cmdl.Add(new Parameter("@SyncFlag", SqlDbType.Bit, 0));
                                            cmdl.Add(new Parameter("@KetKoli", SqlDbType.VarChar, curd["KetKoli"].StringValue));
                                            cmdl.Add(new Parameter("@NPackingListPrint", SqlDbType.Int, curd["NPackingListPrint"].NumberValue));
                                            cmdl.Add(new Parameter("@LastUpdatedBy", SqlDbType.VarChar, curd["LastUpdatedBy"].StringValue));
                                            cmdl.Add(new Parameter("@CreatedBy", SqlDbType.VarChar, curd["LastUpdatedBy"].StringValue));
                                            cmdl.Add(new Parameter("@QtyStock", SqlDbType.Int, curd["QtyStock"].NumberValue));
                                            cmdl.Add(new Parameter("@WiserID", SqlDbType.Int, curd["wiserid"].NumberValue));
                                            cmdl.Add(new Parameter("@WiserHeaderID", SqlDbType.Int, cur["wiserid"].NumberValue));
                                            cmdl.Add(new Parameter("@WiserTag", SqlDbType.VarChar, "WISERDC"));

                                            db.Commands.Add(db.CreateCommand("[usp_NotaPenjualanDetail_WISERDC_SET]"));
                                            db.Commands[cid].Parameters = cmdl;
                                            cid += 1;
                                        }

                                        foreach (Command cmd in db.Commands)
                                        {
                                            try { cmd.ExecuteNonQuery(); }
                                            catch (Exception ex) { Error.LogError(ex); }                                            
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        if (itTrasaction) db.RollbackTransaction();
                                        itOk = false;
                                    }
                                    if (itOk) 
                                    {
                                        if (itTrasaction) db.CommitTransaction();
                                        try
                                        {
                                            //Link Ke Piutang
                                            GetDataNota(id);                                                 
                                            if ((new JSON(cur["TglTerima"].DateTimeValue(DBNull.Value))).DateTimeValue(null) != null)
                                            {
                                                IsiTglTerima((DateTime)cur["TglTerima"].DateTimeValue(DBNull.Value), (string)cur["TransactionType"].StringValue, (int)cur["HariKredit"].NumberValue, (int)cur["HariKirim"].NumberValue, (int)cur["HariSales"].NumberValue, (string)cur["LastUpdatedBy"].StringValue);
                                                GetDataNota(id); 
                                                LinkKePiutang();
                                                successMessage = "DATA BERHASIL DITAMBAHKAN DAN SUDAH DILINK KE PIUTANG";
                                            }
                                            else
                                            {
                                                successMessage = "DATA BERHASIL DITAMBAHKAN TAPI BELUM DILINK KE PIUTANG KARENA TGL TERIMA MASIH KOSONG";
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            itOk = false;
                                        }
                                    }
                                    if (itOk)
                                    {
                                        //if (itTrasaction) db.CommitTransaction();
                                        scs.Add((int)cur["wiserid"].NumberValue);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            db.Commands.Clear();
                                            db.Commands.Add(db.CreateCommand("[usp_NotaPenjualan_WISERDC_DELETE]"));
                                            db.Commands[0].Parameters.Add(new Parameter("@RowID", SqlDbType.UniqueIdentifier, id));
                                            db.Commands[0].Parameters.Add(new Parameter("@WiserID", SqlDbType.Int, cur["wiserid"].NumberValue));

                                            db.Commands[0].ExecuteNonQuery();
                                        }
                                        catch (Exception) { }
                                    }
                                }
                                else if (dtbl.Rows[0]["Result"].ToString() == "2")
                                {
                                    MessageBox.Show("Order Penjualan atas Nota " + cur["NoNota"].StringValue + " tidak ditemukan");
                                    //return;
                                }
                                else
                                {
                                    if (successMessage != "")
                                    {
                                        if (!successMessage.Contains("SURAT JALAN"))
                                        {
                                            successMessage = successMessage + " ADA NOTA YANG BELUM DIBUAT SURAT JALAN.";
                                        }
                                    }
                                    else 
                                    {
                                        if(!successMessage.Contains("SURAT JALAN") && !successMessage.Contains("PIUTANG"))
                                        {
                                            successMessage = "ADA NOTA YANG BELUM DIBUAT SURAT JALAN.";
                                        }
                                    }                  
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex);
                            return;
                        }
                    }

                    if (scs.Count > 0)
                    {
                        MessageBox.Show(successMessage);
                    }
                    else MessageBox.Show("Synch gagal");

                    if (scs.Count > 0)
                    {
                        if (!hasSynch) hasSynch = true;
                        MarkAsSuccess(scs.ToArray());

                        JSON opt = new JSON(JSONType.Object);
                        JSON lst = new JSON(JSONType.Array);
                        foreach (int ix in scs) lst.ArrAdd(new JSON(ix));
                        opt.ObjAdd("mark", new JSON(true));
                        opt.ObjAdd("ids", lst);

                        string errm = "";

                        XNet xn = new XNet(host + "/api/notapenjualan/synch", XNetMethod.GET);
                        XNetThread xnt = xn.Send(opt, r =>
                        {
                            if (r.Error != null) errm = r.Error.Message;
                            else if (r.Output.Length > 0)
                            {
                                JSON jres = JSON.Parse(r.Output);
                                if (jres.Type == JSONType.Object)
                                {
                                    if (jres.ObjExists("Result") && jres["Result"].BoolValue) errm = "";
                                    else if (jres.ObjExists("Msg")) errm = jres["Msg"].StringValue;
                                    else errm = "Marking to server failed";
                                    return;
                                }
                            }
                            errm = "Marking to server failed";
                        });

                        if (errm.Length > 0) MessageBox.Show("Server message:\n" + errm);
                    }
                    else MessageBox.Show("Synch gagal");

                    b.Result = true;
                    return;
                }
                if (b.Result != null && !b.Result.Equals(true))
                {
                    MessageBox.Show(b.Result.ToString());
                    return;
                }
            };
            bgw.RunWorkerCompleted += (a, b) =>
            {
                bool r = false;
                if (b.Cancelled) MessageBox.Show(thisx, "Operasi di gagalkan");
                else if (b.Error != null) MessageBox.Show(thisx, b.Error.Message);
                else r = true;

                ipProgress.Close(r);
                //if (scs.Count == mdat.Count) this.Close();
            };

            ipProgress.OpenDialog(this, a =>
            {
            }, () => bgw.CancelAsync());
            bgw.RunWorkerAsync();
        }

        private string FingerPrintWiser(string id)
        {
            string ids = id.PadLeft(10, '0');
            string FingerPrint = "SAS" + DateTime.Now.ToString("yyMMdd") + ids + "SYNC";
            return FingerPrint;
        }

        private void MarkAsSuccess(int[] ids)
        {
            List<int> idx = new List<int>(ids);
            foreach (DataGridViewRow row in GVHeader.Rows)
            {
                if (idx.IndexOf(int.Parse(row.Cells["colid"].Value.ToString())) >= 0)
                {
                    foreach (DataGridViewColumn cl in row.DataGridView.Columns)
                    {
                        if (cl.Name == "colCheck")
                        {
                            row.Cells[cl.Name].Value = false;
                            row.Cells[cl.Name].Tag = true;
                        }
                        row.Cells[cl.Name].Style.BackColor = Color.FromArgb(221, 255, 181);
                    }
                }
            }
        }

        private void GVHeader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                DataGridViewCell cur = GVHeader.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cur.Tag != null && cur.Tag.Equals(true)) return;
                cur.Value = !Boolean.Parse(cur.Value.ToString());
            }
        }

        private void GetDataNota(Guid NotaRowID)
        {
            try
            {
                //this.Cursor = Cursors.WaitCursor;
                dtNota = new DataTable();
                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("usp_NotaPenjualan_LIST_FILTER_RowID"));
                    db.Commands[0].Parameters.Add(new Parameter("@rowID", SqlDbType.UniqueIdentifier, NotaRowID));
                    dtNota = db.Commands[0].ExecuteDataTable();
                }

                // Display data
                cab1 = Tools.isNull(dtNota.Rows[0]["Cabang1"], "").ToString();
                cab2 = Tools.isNull(dtNota.Rows[0]["Cabang2"], "").ToString();
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
        }

        private void IsiTglTerima(DateTime tglTerima, string jnsTrans, int hariKredit, int hariKirim, int hariSales, string updatedBy)
        {
            try
            {
                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("usp_NotaPenjualan_UPDATE"));
                    db.Commands[0].Parameters.Add(new Parameter("@rowID", SqlDbType.UniqueIdentifier, dtNota.Rows[0]["RowID"]));
                    db.Commands[0].Parameters.Add(new Parameter("@htrID", SqlDbType.VarChar, dtNota.Rows[0]["HtrID"]));
                    db.Commands[0].Parameters.Add(new Parameter("@recID", SqlDbType.VarChar, dtNota.Rows[0]["RecordID"]));
                    db.Commands[0].Parameters.Add(new Parameter("@DOID", SqlDbType.UniqueIdentifier, dtNota.Rows[0]["DOID"]));
                    db.Commands[0].Parameters.Add(new Parameter("@noNota", SqlDbType.VarChar, dtNota.Rows[0]["NoSuratJalan"]));
                    db.Commands[0].Parameters.Add(new Parameter("@tglNota", SqlDbType.DateTime, dtNota.Rows[0]["TglSuratJalan"]));
                    db.Commands[0].Parameters.Add(new Parameter("@noSJ", SqlDbType.VarChar, dtNota.Rows[0]["NoSuratJalan"]));
                    db.Commands[0].Parameters.Add(new Parameter("@tglSJ", SqlDbType.DateTime, dtNota.Rows[0]["TglSuratJalan"]));
                    db.Commands[0].Parameters.Add(new Parameter("@tglTerima", SqlDbType.DateTime, tglTerima));
                    db.Commands[0].Parameters.Add(new Parameter("@tglSerahTerimaChecker", SqlDbType.DateTime, dtNota.Rows[0]["TglSerahTerimaChecker"]));
                    db.Commands[0].Parameters.Add(new Parameter("@cabang1", SqlDbType.VarChar, dtNota.Rows[0]["Cabang1"]));
                    db.Commands[0].Parameters.Add(new Parameter("@cabang2", SqlDbType.VarChar, dtNota.Rows[0]["Cabang2"]));
                    db.Commands[0].Parameters.Add(new Parameter("@cabang3", SqlDbType.VarChar, dtNota.Rows[0]["Cabang3"]));
                    db.Commands[0].Parameters.Add(new Parameter("@kodeSales", SqlDbType.VarChar, dtNota.Rows[0]["KodeSales"]));
                    db.Commands[0].Parameters.Add(new Parameter("@kodeToko", SqlDbType.VarChar, dtNota.Rows[0]["KodeToko"]));
                    db.Commands[0].Parameters.Add(new Parameter("@alamatKirim", SqlDbType.VarChar, dtNota.Rows[0]["alamatKirim"]));
                    db.Commands[0].Parameters.Add(new Parameter("@kota", SqlDbType.VarChar, dtNota.Rows[0]["Kota"]));
                    db.Commands[0].Parameters.Add(new Parameter("@isClosed", SqlDbType.Bit, dtNota.Rows[0]["isClosed"]));
                    db.Commands[0].Parameters.Add(new Parameter("@catatan1", SqlDbType.VarChar, dtNota.Rows[0]["Cat1"]));
                    db.Commands[0].Parameters.Add(new Parameter("@catatan2", SqlDbType.VarChar, dtNota.Rows[0]["Cat2"]));
                    db.Commands[0].Parameters.Add(new Parameter("@catatan3", SqlDbType.VarChar, dtNota.Rows[0]["Cat3"]));
                    db.Commands[0].Parameters.Add(new Parameter("@catatan4", SqlDbType.VarChar, dtNota.Rows[0]["Cat4"]));
                    db.Commands[0].Parameters.Add(new Parameter("@catatan5", SqlDbType.VarChar, dtNota.Rows[0]["Cat5"]));
                    db.Commands[0].Parameters.Add(new Parameter("@syncFlag", SqlDbType.Bit, 0));
                    db.Commands[0].Parameters.Add(new Parameter("@linkID", SqlDbType.VarChar, dtNota.Rows[0]["LinkID"]));
                    db.Commands[0].Parameters.Add(new Parameter("@nPrint", SqlDbType.Int, dtNota.Rows[0]["NPrint"]));
                    db.Commands[0].Parameters.Add(new Parameter("@checker1", SqlDbType.VarChar, dtNota.Rows[0]["Checker1"]));
                    db.Commands[0].Parameters.Add(new Parameter("@checker2", SqlDbType.VarChar, dtNota.Rows[0]["Checker2"]));
                    db.Commands[0].Parameters.Add(new Parameter("@transactionType", SqlDbType.VarChar, jnsTrans));
                    db.Commands[0].Parameters.Add(new Parameter("@hariKredit", SqlDbType.Int, hariKredit));
                    db.Commands[0].Parameters.Add(new Parameter("@hariKirim", SqlDbType.Int, hariKirim));
                    db.Commands[0].Parameters.Add(new Parameter("@hariSales", SqlDbType.Int, hariSales));
                    db.Commands[0].Parameters.Add(new Parameter("@lastUpdatedBy", SqlDbType.VarChar, updatedBy));
                    db.Commands[0].ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
        }

        private void LinkKePiutang()
        {
            /* validasi untuk link ke piutang */
            if (cab1 != GlobalVar.Gudang)
            {
                return;
            }
            // NOTA BARANG BONUS TIDAK PERLU DI LINK KE API 
            string rpNet2 = dtNota.Rows[0]["RpNet2"].ToString();
            string rpJual2 = dtNota.Rows[0]["RpJual2"].ToString();
            string rpPot2 = dtNota.Rows[0]["RpPot2"].ToString();

            if (double.Parse(rpNet2) == 0
                    && (double.Parse(rpJual2) - double.Parse(rpPot2)) == 0)
            {
                MessageBox.Show("INI NOTA BARANG BONUS, TIDAK PERLU LINK KE API");
                //this.Close();
                return;
            }

            // TIDAK BOLEH LINK KE API JIKA RP_NET3 MASIH 0 
            string rpNet3 = dtNota.Rows[0]["RpNet3"].ToString();

            if (double.Parse(rpNet3) == 0)
            {
                MessageBox.Show("NILAI NOTA MASIH 0...!, HARAP DIULANG AGAR LINK KE API", "PERINGATAN");
                //this.Close();
                return;
            }

            /* proses untuk link ke piutang (panggil psp) */
            try
            {
                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("psp_PJ3_LinkToPiutang_ISA"));
                    db.Commands[0].Parameters.Add(new Parameter("@notaID", SqlDbType.UniqueIdentifier, (Guid)dtNota.Rows[0]["RowID"]));
                    db.Commands[0].Parameters.Add(new Parameter("@recordID", SqlDbType.VarChar, dtNota.Rows[0]["RecordID"]));
                    db.Commands[0].Parameters.Add(new Parameter("@tglJatuhTempo", SqlDbType.DateTime, HitungTglJatuhTempo()));
                    db.Commands[0].Parameters.Add(new Parameter("@tipeLink", SqlDbType.VarChar, "1")); // TipeLink 1 untuk POS
                    db.Commands[0].Parameters.Add(new Parameter("@lastUpdatedBy", SqlDbType.VarChar, SecurityManager.UserID));
                    db.Commands[0].ExecuteNonQuery();
                }

                //isi kode disini
                InsertBarcode((Guid)dtNota.Rows[0]["RowID"]);
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
        }


        private DateTime HitungTglJatuhTempo()
        {
            object tglJatuhTempo = null;
            try
            {
                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("usp_GetTglJatuhTempo"));
                    db.Commands[0].Parameters.Add(new Parameter("@transactionType", SqlDbType.VarChar, Tools.isNull(dtNota.Rows[0]["TransactionType"], "").ToString()));
                    db.Commands[0].Parameters.Add(new Parameter("@tglTerima", SqlDbType.DateTime, (DateTime)dtNota.Rows[0]["TglTerima"]));
                    //db.Commands[0].Parameters.Add(new Parameter("@tglTerima", SqlDbType.DateTime, txtTglSJ.DateValue));
                    db.Commands[0].Parameters.Add(new Parameter("@hariKredit", SqlDbType.Int, Tools.isNull(dtNota.Rows[0]["HariKredit"], "0").ToString()));
                    db.Commands[0].Parameters.Add(new Parameter("@hariKirim", SqlDbType.Int, Tools.isNull(dtNota.Rows[0]["HariKirim"], "0").ToString()));
                    db.Commands[0].Parameters.Add(new Parameter("@HariSales", SqlDbType.Int, Tools.isNull(dtNota.Rows[0]["HariSales"], "0").ToString()));

                    tglJatuhTempo = db.Commands[0].ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
            return (DateTime)tglJatuhTempo;
        }

        private void InsertBarcode(Guid NotaRowID)
        {
            DateTime TglSuratJalan = new DateTime();
            DateTime.TryParse(dtNota.Rows[0]["TglSuratJalan"].ToString(), out TglSuratJalan);
            string barcode = dtNota.Rows[0]["NoSuratJalan"].ToString() + TglSuratJalan.ToString("yyyy").Substring(1, 3) + TglSuratJalan.ToString("MM");

            try
            {
                DataTable dt = new DataTable();
                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("usp_Barcode_Insert"));
                    db.Commands[0].Parameters.Add(new Parameter("@RowID", SqlDbType.UniqueIdentifier, NotaRowID));
                    db.Commands[0].Parameters.Add(new Parameter("@Barcode", SqlDbType.VarChar, barcode));
                    db.Commands[0].Parameters.Add(new Parameter("@Createdby", SqlDbType.VarChar, SecurityManager.UserName));
                    db.Commands[0].ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
