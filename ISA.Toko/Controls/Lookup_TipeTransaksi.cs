using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISA.DAL;

namespace ISA.Toko.Controls
{
    public partial class Lookup_TipeTransaksi : UserControl
    {
        public event EventHandler SelectData;

        private string _idTr = string.Empty;
        private string _keterangan = string.Empty;
        private string _kelompok = string.Empty;
        private int _jw = 0;
        private int _js = 0;
        private string _lastKeterangan = string.Empty;


        public string Kode
        {
            get { return Tools.isNull(_idTr, string.Empty).ToString(); }
            set { _idTr = value;
            GetKodeTransaksi(value);
            }
        }

        public string Keterangan
        {
            get { return _keterangan; }
            set { _keterangan = value;}
        }

       

        public int JW
        {
            get { return _jw; }
            set { _jw = value; }
        }

        public int JS
        {
            get { return _js; }
            set { _js = value; }
        }

        public Lookup_TipeTransaksi()
        {
            _lastKeterangan = string.Empty;

            InitializeComponent();
        }

        public void SetNamaTransaksi(string nama)
        {
            txtLookupName.Text = nama;
            _lastKeterangan = nama;
        }

        private void txtLookupName_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void ShowDialogForm()
        {
            frm_TipeTransaksi ifrmDialog = new frm_TipeTransaksi();
            ifrmDialog.ShowDialog();
            if (ifrmDialog.DialogResult == DialogResult.OK)
            {
                GetDialogResult(ifrmDialog);
            }
            else
            {
                txtLookupName.Focus();
            }
        }

        private void ShowDialogForm(string searchArg, DataTable dt)
        {
            frm_TipeTransaksi ifrmDialog = new frm_TipeTransaksi(searchArg, dt);
            ifrmDialog.ShowDialog();
            if (ifrmDialog.DialogResult == DialogResult.OK)
            {
                GetDialogResult(ifrmDialog);
            }
            else
            {
                txtLookupName.Focus();
            }
        }

        private void GetDialogResult(frm_TipeTransaksi dialogForm)
        {
            _idTr = dialogForm.Kode;
            txtLookupName.Text = dialogForm.Keterangan;
            _lastKeterangan = txtLookupName.Text;
            txtLookupCode.Text = dialogForm.Kode;
            _keterangan = dialogForm.Keterangan;
            _jw = dialogForm.JW;
            _js = dialogForm.JS;

            if (this.SelectData != null)
            {
                this.SelectData(this, new EventArgs());
            }
        }

        private void Clear()
        {
            _idTr = string.Empty;
            txtLookupName.Text = "";
            _lastKeterangan = txtLookupName.Text;
            txtLookupCode.Text = "";
            _keterangan = string.Empty;
            _kelompok = string.Empty;
            _jw = 0;
            _js = 0;

            if (this.SelectData != null)
            {
                this.SelectData(this, new EventArgs());
            }
        }

        private void cmdLookup_Click(object sender, EventArgs e)
        {
            ShowDialogForm();
        }

        public void ShowDialogFormValidation()
        {
            if (txtLookupName.Text != "")
            {
                SearchJenisTransaksi();
            }
        
        }

        public void SearchJenisTransaksi()
        {
            try
            {
                using (Database db = new Database())
                {
                    DataTable dt = new DataTable();

                    db.Commands.Add(db.CreateCommand("usp_TransactionType_LIST"));
                    db.Commands[0].Parameters.Add(new Parameter("@Aktif", SqlDbType.Bit, true));
                    db.Commands[0].Parameters.Add(new Parameter("@SearchArg", SqlDbType.VarChar, txtLookupName.Text));
                    dt = db.Commands[0].ExecuteDataTable();
                    if (dt.Rows.Count == 1)
                    {
                        txtLookupName.Text = Tools.isNull(dt.Rows[0]["keterangan"], string.Empty).ToString();
                        txtLookupCode.Text = Tools.isNull(dt.Rows[0]["Kode"], string.Empty).ToString();
                        _lastKeterangan = txtLookupCode.Text;

                        _idTr = Tools.isNull(dt.Rows[0]["Kode"], string.Empty).ToString();
                        _keterangan = Tools.isNull(dt.Rows[0]["keterangan"], string.Empty).ToString();
                        _jw = int.Parse(Tools.isNull(dt.Rows[0]["jw"], "0").ToString());
                        _js = int.Parse(Tools.isNull(dt.Rows[0]["js"], "0").ToString());

                        if (this.SelectData != null)
                        {
                            this.SelectData(this, new EventArgs());
                        }
                    }
                    else
                    {
                        ShowDialogForm(txtLookupName.Text, dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }
 
        }

        public void GetKodeTransaksi(String __Kode)
        {
            try
            {
                if (__Kode == "") return;
                using (Database db = new Database())
                {
                    DataTable dt = new DataTable();

                    db.Commands.Add(db.CreateCommand("usp_TransactionType_LIST"));
                    db.Commands[0].Parameters.Add(new Parameter("@Aktif", SqlDbType.Bit, true));
                    db.Commands[0].Parameters.Add(new Parameter("@kode", SqlDbType.VarChar, __Kode));
                    dt = db.Commands[0].ExecuteDataTable();
                    if (dt.Rows.Count == 1)
                    {
                        txtLookupName.Text = Tools.isNull(dt.Rows[0]["keterangan"], string.Empty).ToString();
                        txtLookupCode.Text = Tools.isNull(dt.Rows[0]["Kode"], string.Empty).ToString();
                        _lastKeterangan = txtLookupCode.Text;

                        _idTr = Tools.isNull(dt.Rows[0]["Kode"], string.Empty).ToString();
                        _keterangan = Tools.isNull(dt.Rows[0]["keterangan"], string.Empty).ToString();
                        _jw = int.Parse(Tools.isNull(dt.Rows[0]["jw"], "0").ToString());
                        _js = int.Parse(Tools.isNull(dt.Rows[0]["js"], "0").ToString());

                        if (this.SelectData != null)
                        {
                            this.SelectData(this, new EventArgs());
                        }
                    }
                    else
                    {
                        //ShowDialogForm(txtLookupName.Text, dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
            }

        }


        private void txtLookupName_Validating(object sender, CancelEventArgs e)
        {
            if (_lastKeterangan.Trim() != txtLookupName.Text.Trim())
            {
                if (txtLookupName.Text != "")
                {
                    SearchJenisTransaksi();
                }
                else
                {
                    Clear();
                }
            }
        }

        private void Lookup_TipeTransaksi_Load(object sender, EventArgs e)
        {
            if (GlobalVar.Gudang == "2803")
            {
                try
                {
                    using (Database db = new Database())
                    {
                        DataTable dt = new DataTable();

                        db.Commands.Add(db.CreateCommand("usp_JenisTransaksi_DO"));
                        db.Commands[0].Parameters.Add(new Parameter("@KodeTr", SqlDbType.VarChar, "KG"));
                        dt = db.Commands[0].ExecuteDataTable();
                        if (dt.Rows.Count == 1)
                        {
                            txtLookupName.Text = Tools.isNull(dt.Rows[0]["keterangan"], string.Empty).ToString();
                            txtLookupCode.Text = Tools.isNull(dt.Rows[0]["id_tr"], string.Empty).ToString();
                            _lastKeterangan = txtLookupCode.Text;

                            _idTr = Tools.isNull(dt.Rows[0]["id_tr"], string.Empty).ToString();
                            _keterangan = Tools.isNull(dt.Rows[0]["keterangan"], string.Empty).ToString();
                            _kelompok = Tools.isNull(dt.Rows[0]["kelompok"], string.Empty).ToString();
                            _jw = int.Parse(Tools.isNull(dt.Rows[0]["jw"], "0").ToString());
                            _js = int.Parse(Tools.isNull(dt.Rows[0]["js"], "0").ToString());

                            if (this.SelectData != null)
                            {
                                this.SelectData(this, new EventArgs());
                            }
                        }
                        else
                        {
                            ShowDialogForm(txtLookupName.Text, dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.LogError(ex);
                }
            }
        }
    }
}