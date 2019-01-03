﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ISA.DAL;
using ISA.Common;

namespace ISA.Toko.Controls
{
    public partial class LookupBankAsal : UserControl
    {
        public event EventHandler SelectData;
      

        public string NamaBank
        {
            get
            {
                return txtLookup.Text;
            }
            set
            {
                txtLookup.Text = value;
            }
        }

        public string Lokasi
        {
            get
            {
                return lblLookup.Text;
            }
            set
            {
                lblLookup.Text = value;
            }
        }

        public LookupBankAsal()
        {
            InitializeComponent();
        }

        private void ShowDialogForm()
        {
            frmBankAsalLookup ifrmDialog = new frmBankAsalLookup();
            ifrmDialog.ShowDialog();
            if (ifrmDialog.DialogResult == DialogResult.OK)
            {
                GetDialogResult(ifrmDialog);
            }
        }

        private void ShowDialogForm(string searchArg, DataTable dt)
        {
            frmBankAsalLookup ifrmDialog = new frmBankAsalLookup(searchArg, dt);
            ifrmDialog.ShowDialog();
            if (ifrmDialog.DialogResult == DialogResult.OK)
            {
                GetDialogResult(ifrmDialog);
            }
        }

        private void GetDialogResult(frmBankAsalLookup dialogForm)
        {
            txtLookup.Text = dialogForm.NamaBank;
            lblLookup.Text = dialogForm.Lokasi;

            if (this.SelectData != null)
            {
                this.SelectData(this, new EventArgs());
            }
        }

        public void Clear()
        {
            txtLookup.Text = "";
            lblLookup.Text = "[LOKASI]";
            if (this.SelectData != null)
            {
                this.SelectData(this, new EventArgs());
            }
        }

        private void txtLookup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtLookup.Text != "")
                {
                    using (Database db = new Database(GlobalVar.DBFinance))
                    {
                        DataTable dt = new DataTable();

                        db.Commands.Add(db.CreateCommand("usp_BANKKota_LOOKUP"));
                        db.Commands[0].Parameters.Add(new Parameter("@searchArg", SqlDbType.VarChar, txtLookup.Text));
                        dt = db.Commands[0].ExecuteDataTable();
                        if (dt.Rows.Count == 1)
                        {
                            lblLookup.Text = Tools.isNull(dt.Rows[0]["Lokasi"], "").ToString();
                            txtLookup.Text = Tools.isNull(dt.Rows[0]["NamaBank"], "").ToString();
                            if (this.SelectData != null)
                            {
                                this.SelectData(this, new EventArgs());
                            }
                        }
                        else
                        {
                            ShowDialogForm(txtLookup.Text, dt);
                        }
                    }


                }
                else
                {

                    Clear();
                }
            }
        }

    }
}