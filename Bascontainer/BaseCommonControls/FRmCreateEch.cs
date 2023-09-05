using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BO;
using BASEPractice_BL;
using BasCommon_BL;
using System.Globalization;

namespace BaseCommonControls
{
    public partial class FRmCreateEch : Form
    {

        class tmpDateClass
        {
            public override string ToString()
            {
                return _Date.ToString("dd MMMM yyyy");
            }

            private DateTime _Date;
            public DateTime Date
            {
                get
                {
                    return _Date;
                }
                set
                {
                    _Date = value;
                }
            }
        }

        public string erreur = "";

        private List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();
        public List<BaseTempEcheanceDefinition> Montants
        {
            get
            {
                return _Montants;
            }
            set
            {
                _Montants = value;
            }
        }


        private List<BaseTempEcheanceDefinition> _EchOriginals;
        public List<BaseTempEcheanceDefinition> EchOriginals
        {
            get
            {
                return _EchOriginals;
            }
            set
            {
                _EchOriginals = value;
            }
        }


        public bool Build()
        {
            Montants.Clear();
            erreur = "";
            List<BaseTempEcheanceDefinition> mt = new List<BaseTempEcheanceDefinition>();

            DateTime dt = chkbxPrelevement.Checked ? ((tmpDateClass)cbxPrelevement.SelectedItem).Date : dtpFirstEch.Value;


            DateTime Firstdate =  dt;
            if (Firstdate.Date < DateTime.Now.Date)
            {
                if (MessageBox.Show("La premiere échéance est passé\n Souhaitez-vous continuer?", "Echéance passée", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                    return false;
            }

            foreach (BaseTempEcheanceDefinition EchOriginal in EchOriginals)
                {
                    DateTime maxDate = DateTime.Now.AddYears(2);
                    if (EchOriginal.acte != null)
                    {
                        maxDate = EchOriginal.acte.DateExecution > DateTime.Now ? EchOriginal.acte.DateExecution : DateTime.Now;
                        maxDate = EchOriginal.acte.NbMois == null ? maxDate : maxDate.AddMonths(EchOriginal.acte.NbMois.Value).AddDays(EchOriginal.acte.NbJours.Value);
                        if (EchOriginals.Count > 1) Firstdate = EchOriginal.acte.DateExecution;
   
                    }

                    try
                    {



                        //if (rbEcheancierInvisalign.Checked)
                        //{
                        //    _Montants = MgmtEcheance.getEcheancesInvisalign(
                        //                                            _Montants,
                        //                                            Firstdate,
                        //                                            maxDate,
                        //                                            EchOriginal);




                        //}
                        if (rbMntEch.Checked)
                        {
                            mt = MgmtEcheance.getEcheances(_Montants,
                                                                  Firstdate,
                                                                  Convert.ToDouble(txtbxMntParEch.Text),
                                                                  (MgmtEcheance.periodic)cbxPeriodicite.SelectedIndex,
                                                                  Convert.ToInt32(txtNbPeriodicite.Text),
                                                                    EchOriginal);




                        }
                        if (rbJusquau.Checked)
                        {
                            mt = MgmtEcheance.getEcheances(_Montants,
                                                                  Firstdate,
                                                                  dtpLastEch.Value,
                                                                  (MgmtEcheance.periodic)cbxPeriodicite.SelectedIndex,
                                                                  Convert.ToInt32(txtNbPeriodicite.Text),
                                                                    EchOriginal);
                        }

                        if (rbNbEcheances.Checked)
                        {
                            mt = MgmtEcheance.getEcheancesByNb(_Montants, Firstdate,
                                                                  (int)txtbxNbEcheances.Value,
                                                                  (MgmtEcheance.periodic)cbxPeriodicite.SelectedIndex,
                                                                  Convert.ToInt32(txtNbPeriodicite.Text),
                                                                    EchOriginal);
                        }

                        if (rbInvisalign.Checked)
                        {
                            mt = MgmtEcheance.getEcheancesInvisalign(_Montants,
                                                                    Firstdate,
                                                                    maxDate,
                                                                    EchOriginal);
                        }




                        foreach (BaseTempEcheanceDefinition ted in mt)
                            ted.ParPrelevement = chkbxPrelevement.Checked;

                        foreach (BaseTempEcheanceDefinition ted in mt)
                            ted.ParVirement = chkbxVirement.Checked;

                        foreach (BaseTempEcheanceDefinition ted in mt)
                        {
                            _Montants.Add(ted);
                            if (Firstdate < ted.DAteEcheance) Firstdate = ted.DAteEcheance;
                        }



                        lblErreur.Visible = erreur != "";
                        lblErreur.Text = erreur;


                    }
                    catch (System.Exception ex)
                    {
                        erreur = ex.Message;
                        return false;
                    }

                }
            

            if ((!MgmtEcheance.CheckPrelevementDates(Montants)))
                erreur = "Mauvaises dates pour le(s) prelevement(s)";

            return (erreur == "");
        }


        private void Recalculate()
        {
            Build();
            if (erreur != "")
            {
                MessageBox.Show(erreur, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public FRmCreateEch(bool showAdulteEch)
        {
            InitializeComponent();
            rbInvisalign.Visible = showAdulteEch;
        }

       private void FRmCreateEch_Load(object sender, EventArgs e)
        {

            //var ri = new RegionInfo(System.Threading.Thread.CurrentThread.CurrentCulture.LCID);

            //label3.Text = ri.ISOCurrencySymbol;

            NumberFormatInfo nfi = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat;
            label3.Text = nfi.CurrencySymbol;
           

            RefreshAuthorizedDays(null);

            List<BaseCommonControls.FamilyValue> lst = new List<BaseCommonControls.FamilyValue>();

            foreach (int i in MgmtEncaissement.AuthorizedDays)
                lst.Add(new BaseCommonControls.FamilyValue("","le "+i.ToString(),i));

            lstPre.LoadFromFamilyValueList(lst);


            cbxPeriodicite.Items.Add("Jour(s)");
            cbxPeriodicite.Items.Add("Semaine(s)");
            cbxPeriodicite.Items.Add("Mois");

            cbxPeriodicite.SelectedIndex = 2;

            try
            {
                DateTime mindte = DateTime.Now;
                DateTime maxdte = DateTime.Now;

                foreach (TempEcheanceDefinition ted in EchOriginals)
                    if ((ted.acte != null) && (ted.acte.DateExecution < mindte) && (ted.acte.DateExecution > DateTime.Now))
                        mindte = ted.acte.DateExecution;

                foreach (TempEcheanceDefinition ted in EchOriginals)
                    if ((ted.acte != null) && (ted.acte.DateExecution > maxdte) && (ted.acte.DateExecution > DateTime.Now))
                        maxdte = ted.acte.DateExecution;

                dtpFirstEch.Value = mindte;
                dtpLastEch.Value = maxdte;

            }
            catch (System.Exception)
            {
                dtpFirstEch.Value = DateTime.Now;
            }
        }

        private void RefreshAuthorizedDays(int? filter)
        {
            DateTime dtestart = DateTime.Now.AddDays(7);
            DateTime dteend = DateTime.Now.AddYears(1);
            DateTime dte = dtestart;

            cbxPrelevement.Items.Clear();
            while (dte < dteend)
            {
                foreach (int d in MgmtEncaissement.AuthorizedDays)
                {
                    if ((filter != null) && (filter.Value != d)) continue;
                    tmpDateClass tcd = new tmpDateClass();
                    try
                    {
                        tcd.Date = new DateTime(dte.Year, dte.Month, d);                        
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        tcd.Date = new DateTime(dte.Year, dte.Month, 1).AddMonths(1).AddDays(-1);
                    }

                    if (tcd.Date > dtestart) cbxPrelevement.Items.Add(tcd);
                }
                dte = dte.AddMonths(1);
            }
            cbxPrelevement.SelectedItem = cbxPrelevement.Items[0];

            /*
            DateTime dtestart = DateTime.Now.AddDays(7);
            DateTime dteend = DateTime.Now.AddYears(1);
            DateTime dte = dtestart;
            cbxPrelevement.Items.Clear();

            while (dte < dteend)
            {
                if (AuthorizedDays.Contains(dte.Day))
                {
                    if ((filter == null) || (filter.Value == dte.Day))
                    {
                        tmpDateClass tcd = new tmpDateClass();
                        tcd.Date = dte;
                        cbxPrelevement.Items.Add(tcd);
                    }
                }

                dte = dte.AddDays(1);
            }

            cbxPrelevement.SelectedItem = cbxPrelevement.Items[0];
             */
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!Build())
            {
                lblErreur.Visible = true;
                lblErreur.Text = erreur;
                return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void rbNbEcheances_CheckedChanged(object sender, EventArgs e)
        {
            txtbxNbEcheances.Enabled = rbNbEcheances.Checked;
            dtpLastEch.Enabled = rbJusquau.Checked;
            txtbxMntParEch.Enabled = rbMntEch.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxPrelevement.Checked) chkbxVirement.Checked = false;
        }

        private void chkbxPrelevement_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxVirement.Checked)  chkbxPrelevement.Checked = false;
            cbxPrelevement.Visible = chkbxPrelevement.Checked;
            lstPre.Visible = chkbxPrelevement.Checked;

            if ((chkbxPrelevement.Checked)&&(lstPre.SelectedItems.Count>0))
                RefreshAuthorizedDays(((int)lstPre.SelectedItems[0].Tag));
            else
                RefreshAuthorizedDays(null);


        }

        private void lstPre_OnSelectionChange(object sender, EventArgs e)
        {
            RefreshAuthorizedDays(((int)lstPre.SelectedItems[0].Tag));
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }}
