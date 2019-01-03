﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ISA.DAL;
using System.Data.SqlTypes;

namespace ISA.Trading.Penjualan
{
    public partial class frmPenjualanPotonganUpdateISA : ISA.Trading.BaseForm
    {
        enum enumFormMode { New, Update };
        enumFormMode formMode;
        Guid _rowID, _row;
        string trID, potID;
        string kdtoko, tokoID, nama, alamat, kota;
        DataTable dt = new DataTable();
        double netto;
        bool ajukan=false, acc=false;
        DataRow _historyJualRow;

        public frmPenjualanPotonganUpdateISA(Form caller)
        {
            InitializeComponent();
            formMode = enumFormMode.New;
            this.Caller = caller;
          
        }

        public frmPenjualanPotonganUpdateISA(Form caller, Guid rowID)
        {
            InitializeComponent();
            formMode = enumFormMode.Update;
            this.Caller = caller;
            _rowID = rowID;
        }

        public void RefreshData()
        {
            try
            {
                using (Database db = new Database())
                {
                   
                    DataTable dts = new DataTable();

                    db.Commands.Add(db.CreateCommand("usp_PenjualanPotongan_LIST"));
                    db.Commands[0].Parameters.Add(new Parameter("@rowID", SqlDbType.UniqueIdentifier, _rowID));
                    dt = db.Commands[0].ExecuteDataTable();

                    txtNamaToko.TokoID = Tools.isNull(dt.Rows[0]["KodeToko"], "").ToString();
                   
                    db.Commands.Add(db.CreateCommand("usp_Toko_LIST"));
                    db.Commands[1].Parameters.Add(new Parameter("@kodeToko", SqlDbType.VarChar, dt.Rows[0]["KodeToko"]));
                    dts = db.Commands[1].ExecuteDataTable();

                    _row = (Guid) dt.Rows[0]["NotaPenjualanID"];
                    trID = Tools.isNull(dt.Rows[0]["TrID"], "").ToString();
                    potID = Tools.isNull(dt.Rows[0]["PotID"], "").ToString();
                    
                    txtNamaToko.NamaToko = Tools.isNull(dts.Rows[0]["NamaToko"],"").ToString();
                    txtAlamatKirim.Text = Tools.isNull(dts.Rows[0]["Alamat"], "").ToString();
                    txtKota.Text = Tools.isNull(dts.Rows[0]["Kota"], "").ToString();
                    txtIDWil.Text = Tools.isNull(dts.Rows[0]["WilID"], "").ToString();

                    txtNoPot.Text = Tools.isNull(dt.Rows[0]["NoPot"], "").ToString();
                    txtNoNota.Text = Tools.isNull(dt.Rows[0]["NoNota"], "").ToString();
                    txtTglNota.DateValue = (DateTime?)dt.Rows[0]["TglNota"];
                    txtNilaiNota.Text= Tools.isNull(dt.Rows[0]["RpNet"], "").ToString();
                    txtDisc1.Text = Tools.isNull(dt.Rows[0]["Disc1"], "").ToString();
                    txtDisc2.Text = Tools.isNull(dt.Rows[0]["Disc2"], "").ToString(); 
                    txtDisc3.Text = Tools.isNull(dt.Rows[0]["Disc3"], "").ToString();
                    txtDiscValue.Text = (Convert.ToDouble(dt.Rows[0]["RpNet"]) - Convert.ToDouble(dt.Rows[0]["RpNet"])).ToString();
                    txtRetur.Text = (Convert.ToDouble(dt.Rows[0]["RpNet"]) - Convert.ToDouble(dt.Rows[0]["RpNet"])).ToString();//Thisform.TextRetur.Value = Htransj.Rp_Net3-Hpotj.Rp_Net
                    //Thisform.TextRetur.Value = Htransj.Rp_Net3-Hpotj.Rp_Net
                    txtNetto.Text = Tools.isNull(dt.Rows[0]["RpNet"], "").ToString();
                    //Thisform.TextRp_Net.Value = Hpotj.Rp_Net
                    netto = Convert.ToDouble(dt.Rows[0]["RpNet"]);
                    if (Convert.ToDouble(dt.Rows[0]["Dil"]) > 0)
                    {
                        if (Convert.ToDouble(dt.Rows[0]["RpNet"]) != 0)
                        {
                            txtPotLumPersen.Text = (Convert.ToDouble(dt.Rows[0]["Dil"]) / Convert.ToDouble(dt.Rows[0]["RpNet"]) * 100).ToString();
                        }
                        else
                        {
                            txtPotLumPersen.Text = "0";
                        }
                    }
                    else{
                        txtPotLumPersen.Text = Tools.isNull(dt.Rows[0]["Disc"], "").ToString();
                    }
                    txtPotLumRp.Text=  Tools.isNull(dt.Rows[0]["Dil"], "").ToString();
                    txtCatPot.Text= Tools.isNull(dt.Rows[0]["Catatan"], "").ToString();
                    txtTglPot.DateValue = (DateTime?)dt.Rows[0]["TglPot"];
                    txtACCPersen.Text =Tools.isNull(dt.Rows[0]["DiscACC"], "").ToString();
                    txtACCRp.Text =Tools.isNull(dt.Rows[0]["DilACC"], "").ToString();
                    txtCatACC.Text =Tools.isNull(dt.Rows[0]["CatACC"], "").ToString();
                    
                    DateTime _tglAcc;

                    if (DateTime.TryParse(dt.Rows[0]["TglACC"].ToString(), out _tglAcc))
                    {
                        txtTglACC.DateValue = _tglAcc; // (DateTime?)dt.Rows[0]["TglACC"];
                    }
                    else
                    {
                        txtTglACC.Text = "";
                    }

                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }

        }

        private void frmPenjualanPotonganUpdateISA_Load(object sender, EventArgs e)
        {
            if (formMode == enumFormMode.Update)
            {
                cmdSave.Enabled = true;
                acc = true;
                if (ajukan == true)
                {
                    txtNamaToko.Enabled = true;
                    txtNoNota.Enabled = true;
                    txtPotLumRp.Enabled = true;
                    txtCatPot.Enabled = true;
                    txtTglPot.Enabled = true;
                    RefreshData();
                    txtNamaToko.Focus();
                }
                else if(acc==true)
                {
                    txtACCPersen.Enabled = false;   // true;
                    txtCatACC.Enabled = false;      // true;
                    RefreshData();
                    txtTglACC.DateValue = DateTime.Now;
                    txtACCPersen.Focus();
                }
            }
            else
            {
                //DataTable dtNum = Tools.GetGeneralNumerator(docNoDO);
                //int iNomor = int.Parse(dtNum.Rows[0]["Nomor"].ToString());
                //txtNoPot.Text = iNomor.ToString();
                txtNamaToko.Enabled= true;
                txtNoNota.Enabled=true;
                txtNamaToko.Focus();
                txtPotLumPersen.Enabled = false;    // true;
                txtPotLumRp.Enabled = true;
                txtCatPot.Enabled=true;
                txtTglPot.Enabled=true;
                txtNetto.Text = "0";
                txtDiscValue.Text="0";
                txtTglPot.DateValue = DateTime.Now;
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                try
                {
                    switch (formMode)
                    {
                        case enumFormMode.New:

                            string doc = "NOMOR_POTONGAN";
                            string depan = "";
                            string belakang = "";
                            string NoPot = "";
                            int iNomor;
                            int lebar;

                            depan = Tools.GeneralInitial();
                            DataTable dtNum = Tools.GetGeneralNumerator(doc);
                            lebar = int.Parse(Tools.isNull(dtNum.Rows[0]["Lebar"],"0").ToString());
                            belakang = dtNum.Rows[0]["Belakang"].ToString();
                            if (Tools.isNull(dtNum.Rows[0]["Depan"], "").ToString() != depan)
                            {
                                iNomor = 1;
                            }
                            else
                            {
                                iNomor = int.Parse(dtNum.Rows[0]["Nomor"].ToString());
                                iNomor++;
                            }
                            NoPot = Tools.FormatNumerator(iNomor, lebar, depan, belakang);
                            txtNoPot.Text = Tools.isNull(NoPot,"").ToString();

                            using (Database db = new Database())
                            {
                                DataTable dtCek = new DataTable();
                                db.Commands.Add(db.CreateCommand("usp_PenjualanPotongan_CEK"));
                                db.Commands[0].Parameters.Add(new Parameter("@notaPenjualanID", SqlDbType.UniqueIdentifier, _row));
                                dtCek = db.Commands[0].ExecuteDataTable();
                                if (dtCek.Rows.Count > 0)
                                {
                                    MessageBox.Show("Nota ini sudah ada Potongan.");
                                    return;
                                }
                            }

                            MessageBox.Show(trID.ToString());
                            
                            using (Database db = new Database())
                            {
                                _rowID = Guid.NewGuid();
                                DataTable dt = new DataTable();
                                db.Commands.Add(db.CreateCommand("usp_PenjualanPotongan_INSERT"));
                                db.Commands[0].Parameters.Add(new Parameter("@rowID", SqlDbType.UniqueIdentifier, _rowID));
                                db.Commands[0].Parameters.Add(new Parameter("@notaPenjualanID", SqlDbType.UniqueIdentifier, _row));
                                db.Commands[0].Parameters.Add(new Parameter("@trID", SqlDbType.VarChar, trID));
                                db.Commands[0].Parameters.Add(new Parameter("@potID", SqlDbType.VarChar, Tools.CreateFingerPrint()));
                                db.Commands[0].Parameters.Add(new Parameter("@noPot", SqlDbType.VarChar, txtNoPot.Text));
                                db.Commands[0].Parameters.Add(new Parameter("@tglPot", SqlDbType.DateTime, txtTglPot.DateValue));
                                db.Commands[0].Parameters.Add(new Parameter("@dil", SqlDbType.Money, Convert.ToDouble(txtPotLumRp.Text)));
                                db.Commands[0].Parameters.Add(new Parameter("@disc", SqlDbType.Money, Convert.ToDouble(txtPotLumPersen.Text)));
                                db.Commands[0].Parameters.Add(new Parameter("@rpNet", SqlDbType.Money, Convert.ToDouble(txtNetto.Text)));
                                db.Commands[0].Parameters.Add(new Parameter("@catatan", SqlDbType.VarChar, txtCatPot.Text));
                                db.Commands[0].Parameters.Add(new Parameter("@tglACC", SqlDbType.DateTime, SqlDateTime.Null));
                                db.Commands[0].Parameters.Add(new Parameter("@dilACC", SqlDbType.Money, 0));
                                db.Commands[0].Parameters.Add(new Parameter("@catACC", SqlDbType.VarChar, ""));
                                db.Commands[0].Parameters.Add(new Parameter("@discACC", SqlDbType.Money, 0));
                                db.Commands[0].Parameters.Add(new Parameter("@syncFlag", SqlDbType.Bit, 0));
                                db.Commands[0].Parameters.Add(new Parameter("@idLink", SqlDbType.VarChar, ""));
                                db.Commands[0].Parameters.Add(new Parameter("@statusACC", SqlDbType.Bit, 0));
                                db.Commands[0].Parameters.Add(new Parameter("@lastUpdatedBy", SqlDbType.VarChar, SecurityManager.UserID));
                                db.Commands[0].ExecuteNonQuery();

                                db.Commands.Add(db.CreateCommand("usp_Numerator_UPDATE"));
                                db.Commands[1].Parameters.Add(new Parameter("@doc", SqlDbType.VarChar, doc));
                                db.Commands[1].Parameters.Add(new Parameter("@depan", SqlDbType.VarChar, depan));
                                db.Commands[1].Parameters.Add(new Parameter("@belakang", SqlDbType.VarChar, belakang));
                                db.Commands[1].Parameters.Add(new Parameter("@nomor", SqlDbType.Int, iNomor));
                                db.Commands[1].Parameters.Add(new Parameter("@lebar", SqlDbType.VarChar, lebar));
                                db.Commands[1].Parameters.Add(new Parameter("@lastUpdatedBy", SqlDbType.VarChar, SecurityManager.UserID));
                                db.Commands[1].ExecuteNonQuery();
                            }
                            break;

                        case enumFormMode.Update:
                            if (txtTglACC.DateValue.HasValue)
                            {
                                GlobalVar.LastClosingDate = (DateTime)txtTglACC.DateValue;
                                if ((DateTime)txtTglACC.DateValue <= GlobalVar.LastClosingDate)
                                {
                                    throw new Exception(String.Format(ISA.Trading.Messages.Error.AlreadyClosingPJT, GlobalVar.LastClosingDate));
                                }
                            }
                           
                            using (Database db = new Database())
                            {
                                DataTable dt = new DataTable();
                                db.Commands.Add(db.CreateCommand("usp_PenjualanPotongan_UPDATE"));
                                db.Commands[0].Parameters.Add(new Parameter("@rowID", SqlDbType.UniqueIdentifier, _rowID));
                                db.Commands[0].Parameters.Add(new Parameter("@notaPenjualanID", SqlDbType.UniqueIdentifier, _row));
                                db.Commands[0].Parameters.Add(new Parameter("@trID", SqlDbType.VarChar, trID));
                                db.Commands[0].Parameters.Add(new Parameter("@potID", SqlDbType.VarChar, potID));
                                db.Commands[0].Parameters.Add(new Parameter("@noPot", SqlDbType.VarChar, txtNoPot.Text));
                                db.Commands[0].Parameters.Add(new Parameter("@tglPot", SqlDbType.DateTime, txtTglPot.DateValue));
                                db.Commands[0].Parameters.Add(new Parameter("@dil", SqlDbType.Money, Convert.ToDouble(txtPotLumRp.Text)));
                                //db.Commands[0].Parameters.Add(new Parameter("@disc", SqlDbType.Money, Convert.ToDouble(txtPotLumPersen.Text)));
                                db.Commands[0].Parameters.Add(new Parameter("@rpNet", SqlDbType.Money, Convert.ToDouble(txtNetto.Text)));
                                db.Commands[0].Parameters.Add(new Parameter("@catatan", SqlDbType.VarChar, txtCatPot.Text));
                                if (ajukan == true)
                                    db.Commands[0].Parameters.Add(new Parameter("@statusACC", SqlDbType.Bit, 0));
                                else if (acc == true)
                                {
                                    if (Convert.ToDouble(txtACCRp.Text) > 0)
                                        db.Commands[0].Parameters.Add(new Parameter("@statusACC", SqlDbType.Bit, 1));
                                    else
                                        db.Commands[0].Parameters.Add(new Parameter("@statusACC", SqlDbType.Bit, 0));
                                }
                                db.Commands[0].Parameters.Add(new Parameter("@syncFlag", SqlDbType.Bit, 0));
                                db.Commands[0].Parameters.Add(new Parameter("@lastUpdatedBy", SqlDbType.VarChar, SecurityManager.UserID));
                                db.Commands[0].ExecuteNonQuery();

                                //db.Commands[0].Parameters.Add(new Parameter("@idLink", SqlDbType.VarChar, ""));
                                //db.Commands[0].Parameters.Add(new Parameter("@discACC", SqlDbType.Decimal, Convert.ToDecimal(txtACCPersen.Text)));
                                //db.Commands[0].Parameters.Add(new Parameter("@tglACC", SqlDbType.DateTime, txtTglACC.DateValue));
                                //db.Commands[0].Parameters.Add(new Parameter("@dilACC", SqlDbType.Money, Math.Round(Convert.ToDouble(txtACCRp.Text), 0)));
                                //db.Commands[0].Parameters.Add(new Parameter("@catACC", SqlDbType.VarChar, txtCatACC.Text));
                            }

                            //if (LookupInfo.GetValue("INSTALL_MODULE", "PIUTANG").ToString() == "True")
                            //{
                            //    if (ajukan == false)
                            //    {
                            //        if (acc == true && txtACCRp.GetDoubleValue > 0)
                            //        {
                            //            LinkToPiutang();
                            //        }
                            //    }
                            //}
                            break;
                    }
                    MessageBox.Show("Data telah tersimpan");
                    this.DialogResult = DialogResult.OK;
                    
                }
                catch (Exception ex)
                {
                    Error.LogError(ex);
                }
                finally
                {
                    Penjualan.frmPotonganNotaBrowse frmCaller = (frmPotonganNotaBrowse)this.Caller;
                    frmCaller.RefreshData();
                    frmCaller.FindRow("RowID", _rowID.ToString());
                    this.Close();
                    frmCaller.Show();
                }
            }
        }

        public bool IsValid()
        {
            bool valid = true;
         
            if (txtNoNota.Text == "" || txtNilaiNota.Text  == "")
            {
                errorProvider1.SetError(txtNoNota, Messages.Error.InputRequired);
                txtNoNota.Focus();
                valid = false;
            }
            return valid;
        }

        private void txtNamaToko_SelectData(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                using (Database db = new Database())
                {
                    db.Commands.Add(db.CreateCommand("usp_Toko_SEARCH"));

                    db.Commands[0].Parameters.Add(new Parameter("@searchArg", SqlDbType.VarChar, txtNamaToko.TokoID.ToString()));
                    dt = db.Commands[0].ExecuteDataTable();
                    nama = Tools.isNull(dt.Rows[0]["NamaToko"],"").ToString();
                    alamat = Tools.isNull(dt.Rows[0]["Alamat"], "").ToString();
                    txtAlamatKirim.Text = alamat;
                    kota= Tools.isNull(dt.Rows[0]["Kota"], "").ToString();
                    txtKota.Text = kota; 
                    txtIDWil.Text = Tools.isNull(dt.Rows[0]["WilID"],"").ToString();
                    kdtoko = Tools.isNull(dt.Rows[0]["KodeToko"], "").ToString(); 
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
        }

        private void ShowDialogForm()
        {
            /*frmPotonganVNotaBrowse ifrmDialog = new frmPotonganVNotaBrowse(this, tokoID);
            ifrmDialog.ShowDialog();
            if (ifrmDialog.DialogResult == DialogResult.OK)
            {
                GetDialogResult(ifrmDialog);
            }*/
        }

       /* private void GetDialogResult(frmPotonganVNotaBrowse dialogForm)
        {
            _row = dialogForm.rowId;
            MessageBox.Show(_row.ToString());
            txtNoNota.Text = dialogForm.noNota;
            //txtTglNota.DateValue = dialogForm.tglNota;
            //txtNetto.Text = dialogForm.rpNetto.ToString();
            if (this.SelectData != null)
            {
                this.SelectData(this, new EventArgs());
            }
        }*/

        private void txtPotLumPersen_TextChanged(object sender, EventArgs e)
        {
            if (txtPotLumPersen.Text != "")
            {
                txtPotLumRp.Text = ((Convert.ToDouble(txtPotLumPersen.Text)/100) * netto).ToString();
            }
        }

        private void txtACCPersen_TextChanged(object sender, EventArgs e)
        {
            if (txtACCPersen.Text != "")
            {
                txtACCRp.Text = ((Convert.ToDouble(txtACCPersen.Text) / 100) * netto).ToString();
            }
        }

        private void frmPenjualanPotonganUpdateISA_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }

        private void txtNoNota_Leave(object sender, EventArgs e)
        {
            //if (txtNoNota.Text != string.Empty && txtNamaToko.KodeToko != string.Empty)
            //{
            //    DataTable dt = new DataTable();
            //    try
            //    {
            //        using (Database db = new Database())
            //        {
            //            db.Commands.Add(db.CreateCommand("usp_GetRowIDNota_POT"));
            //            db.Commands[0].Parameters.Add(new Parameter("@nonota", SqlDbType.VarChar, txtNoNota.Text));
            //            db.Commands[0].Parameters.Add(new Parameter("@kdToko", SqlDbType.VarChar, txtNamaToko.KodeToko));
            //            dt = db.Commands[0].ExecuteDataTable();
            //            if (dt.Rows.Count > 0)
            //            {
            //                ShowHistoryPenjualanDialogForm(dt);
            //                if (_historyJualRow != null)
            //                {
            //                    string noNota_ = Tools.isNull(_historyJualRow["NoSuratJalan"], "").ToString();
            //                    txtNoNota.Text = noNota_;
            //                }


            //                //_row = (Guid)dt.Rows[0]["RowID"];
            //                //if (formMode == enumFormMode.New)
            //                //{
            //                //    DataTable dt2 = new DataTable();
            //                //    try
            //                //    {
            //                //        using (Database db2 = new Database())
            //                //        {
            //                //            db2.Commands.Add(db.CreateCommand("usp_NotaPenjualan_LIST_FILTER_RowID"));
            //                //            db2.Commands[0].Parameters.Add(new Parameter("@rowID", SqlDbType.UniqueIdentifier, _row));
            //                //            dt2 = db2.Commands[0].ExecuteDataTable();
            //                //            trID = Tools.isNull(dt2.Rows[0]["RecordID"], "").ToString();
            //                //            txtTglNota.DateValue = (DateTime?)dt2.Rows[0]["TglNota"];
            //                //            txtNilaiNota.Text = Tools.isNull(dt2.Rows[0]["RpJual3"], "").ToString();
            //                //            txtDisc1.Text = Tools.isNull(dt2.Rows[0]["Disc1"], "").ToString();
            //                //            txtDisc2.Text = Tools.isNull(dt2.Rows[0]["Disc2"], "").ToString();
            //                //            txtDisc3.Text = Tools.isNull(dt2.Rows[0]["Disc3"], "").ToString();
            //                //            double discvalue = Convert.ToDouble(dt2.Rows[0]["RpJual3"]) - Convert.ToDouble(dt2.Rows[0]["RpNet3"]);
            //                //            txtDiscValue.Text = discvalue.ToString();
            //                //            txtRetur.Text = discvalue.ToString();
            //                //            netto = Convert.ToDouble(dt2.Rows[0]["RpNet3"]);
            //                //            txtNetto.Text = netto.ToString();
            //                //        }
            //                //    }
            //                //    catch (Exception ex)
            //                //    {
            //                //        Error.LogError(ex);
            //                //    }
            //                //}
            //            }
            //            else
            //            {
            //                MessageBox.Show("Tidak ada nota dengan nomor tersebut.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                txtNoNota.Focus();
            //                return;
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Error.LogError(ex);
            //    }
            //}
        }

        private void ShowHistoryPenjualanDialogForm(DataTable dt)
        {
            frmHistoryPenjualanPotonganBrowser ifrmDialog = new frmHistoryPenjualanPotonganBrowser(dt);
            ifrmDialog.ShowDialog();
            if (ifrmDialog.DialogResult == DialogResult.OK)
            {
                GetHistoryPenjualanDialogResult(ifrmDialog);
                //ChekSisaRetur();
            }
        }

        private void GetHistoryPenjualanDialogResult(frmHistoryPenjualanPotonganBrowser dialogForm)
        {
            _historyJualRow = dialogForm.SelecetedRow;
        }



        private void txtACCPersen_Validated(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtACCRp.Text) > 100.00)
            {
                txtACCRp.Focus();
            }
        }

        private void txtPotLumPersen_Validated(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtPotLumPersen.Text) > 100.00)
            {
                txtPotLumPersen.Focus();
            }
        }

        private void LinkToPiutang()
        {
            if (Tools.isNull(dt.Rows[0]["idLink"], "").ToString() != "")
            {
                MessageBox.Show("Sudah Link ke Piutang", "Perhatian");
                return;
            }

            if (txtACCRp.GetDoubleValue == 0)
            {
                MessageBox.Show("Potongan belum  di ACC", "Perhatian");
                return;
            }

            if (MessageBox.Show("Link ke piutang....?", "Perhatian", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Guid potID = _rowID;
                Guid notaJualID = _row;
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dtLinkPot = new DataTable();
                    using (Database db = new Database())
                    {
                        db.Commands.Add(db.CreateCommand("[psp_Potongan_LinkToPiutang_ISA]"));
                        db.Commands[0].Parameters.Add(new Parameter("@potID", SqlDbType.UniqueIdentifier, potID));
                        db.Commands[0].Parameters.Add(new Parameter("@notaJualID", SqlDbType.UniqueIdentifier, notaJualID));
                        db.Commands[0].Parameters.Add(new Parameter("@lastUpdatedBy", SqlDbType.VarChar, SecurityManager.UserID));

                        dtLinkPot = db.Commands[0].ExecuteDataTable();
                    }
                    if (dtLinkPot.Rows[0]["cekNota"].ToString() == "0")
                        MessageBox.Show("Nota tidak ada", "Perhatian");

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

       
        private void frmPenjualanPotonganUpdateISA_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (this.Caller is frmPenjualanPotonganBrowser)
                {
                    frmPenjualanPotonganBrowserISA frmCaller = (frmPenjualanPotonganBrowserISA)this.Caller;
                    frmCaller.RefreshData();
                    frmCaller.FindRow("RowID", _rowID.ToString());
                }
            }
        }


        private void txtNoNota_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNoNota.Text != string.Empty && txtNamaToko.KodeToko != string.Empty)
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        using (Database db = new Database())
                        {
                            db.Commands.Add(db.CreateCommand("usp_GetRowIDNota_POT"));
                            db.Commands[0].Parameters.Add(new Parameter("@nonota", SqlDbType.VarChar, txtNoNota.Text));
                            db.Commands[0].Parameters.Add(new Parameter("@kdToko", SqlDbType.VarChar, txtNamaToko.KodeToko));
                            dt = db.Commands[0].ExecuteDataTable();
                            if (dt.Rows.Count > 0)
                            {
                                ShowHistoryPenjualanDialogForm(dt);
                                if (_historyJualRow != null)
                                {
                                    txtNoNota.Text = Tools.isNull(_historyJualRow["NoSuratJalan"], "").ToString();
                                    txtTglNota.Text = Tools.isNull(_historyJualRow["TglSuratJalan"], "").ToString();
                                    txtTglNota.DateValue = (DateTime?)Tools.isNull(_historyJualRow["TglSuratJalan"], "");
                                    txtNilaiNota.Text = Tools.isNull(_historyJualRow["RpNet2"], "").ToString();
                                    trID = Tools.isNull(_historyJualRow["TrID"], "").ToString();
                                    _row = new Guid(Tools.isNull(_historyJualRow["RowID"],Guid.Empty).ToString());
                                }
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
        }
    }
}
