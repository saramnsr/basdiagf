using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BO;

namespace BaseCommonControls
{
    public partial class FrmRistourne : Form
    {

        private object _Tag;
        public object Tag
        {
            get
            {
                return _Tag;
            }
            set
            {
                _Tag = value;

                if (_Tag is Semestre)
                {
                    Semestre s = ((Semestre)_Tag);
                    Text =( s.traitementSecu==null?"semestre":s.traitementSecu.Libelle)+ " à "+s.Montant_AvantRemise.ToString("C2");
                }else
                if (_Tag is ActePGPropose)
                {
                    ActePGPropose s = ((ActePGPropose)_Tag);
                    Text = s.Libelle+ " à " + s.MontantAvantRemise.ToString("C2");
                }else
                    if (_Tag is Devis)
                {
                    Devis s = ((Devis)_Tag);
                    Text = "Devis à " + s.MontantAvantRemise.Value.ToString("C2");
                }
            }
        }

        private double _ValeurDorigine;
        public double ValeurDorigine
        {
            get
            {
                return _ValeurDorigine;
            }
            set
            {
                _ValeurDorigine = value;
            }
        }

        public double Value
        {
            get
            {
                if (rbpercent.Checked)
                {
                    


                    if (slidingList1.SelectedItems.Count > 0)
                    {
                        int v = ((int)slidingList1.SelectedItems[0].Tag);
                        return (ValeurDorigine - (ValeurDorigine * ((double)v / 100)));
                    }
                    else
                        return -1;
                }

                if (rbMontant.Checked)
                {
                    double num;
                    string tarif = mTxtNouveauTarif.Text;
                    bool isNum = double.TryParse(tarif.Trim(), out num);
                    if (isNum)
                        return num;
                    else
                    {
                        string[] str;
                        if (mTxtNouveauTarif.Text.Contains('.'))
                        {
                            str = tarif.Split('.');
                            return Convert.ToDouble(str[0] + "," + str[1]);
                        }
                        else
                            MessageBox.Show("Tarif incorrect. Veuillez vérifier le format du champ", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    
                }
                if (rbMontantRemise.Checked)
                {
                    double num;
                    string montantRemise = mTxtMontantRemise.Text;
                    bool isNum = double.TryParse(montantRemise.Trim(), out num);
                    if (isNum)
                        return (ValeurDorigine - num);
                    else
                    {
                        //string[] str;
                        if (mTxtMontantRemise.Text.Contains('.'))
                        {
                            return (ValeurDorigine - double.Parse  (mTxtMontantRemise.Text.Replace (".",",") ));
                           // str = montantRemise.Split('.');
                           // return Convert.ToDouble(str[0] + "," + str[1]);
                        }
                        else
                            MessageBox.Show("Montant remise incorrect. Veuillez vérifier le format du champ", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
                return -1;
            }
        }

        public FrmRistourne(ActePGPropose originalValue)
        {
            InitializeComponent();

            Tag = originalValue;
            ValeurDorigine = ((ActePGPropose)originalValue).MontantAvantRemise;
            mTxtNouveauTarif.Text = ValeurDorigine.ToString("00.00");
            
            
        }
        public FrmRistourne(CommTraitement comm)
        {

            if (comm.MontantLigneAvantRemise  == null)
                throw new System.Exception("Pas de montant avant remise pour cette ligne");

            InitializeComponent();

            Tag = comm;
            ValeurDorigine = comm.MontantLigneAvantRemise;
            mTxtNouveauTarif.Text = ValeurDorigine.ToString("00.00");

        }
        public FrmRistourne(Devis_TK devis)
        {

            if (devis.Montant == null)
                throw new System.Exception("Pas de montant avant remise pour ce devis");

            InitializeComponent();

            Tag = devis;
           
                ValeurDorigine = devis.MontantScenario.Value ;
         
           
            mTxtNouveauTarif.Text = ValeurDorigine.ToString("00.00");

        }

        public FrmRistourne(Devis devis)
        {

            if (devis.MontantAvantRemise == null)
                throw new System.Exception("Pas de montant avant remise pour ce devis");

            InitializeComponent();

            Tag = devis;
            ValeurDorigine = devis.MontantAvantRemise.Value;
            mTxtNouveauTarif.Text = ValeurDorigine.ToString("00.00");

        }

        public FrmRistourne(Semestre originalValue)
        {
            InitializeComponent();

            Tag = originalValue;
            ValeurDorigine = ((Semestre)originalValue).Montant_AvantRemise;

            mTxtNouveauTarif.Text = ValeurDorigine.ToString("00.00");

        }

        private void FrmRistourne_Load(object sender, EventArgs e)
        {

            List<BaseCommonControls.FamilyValue> lst = new List<BaseCommonControls.FamilyValue>();

            lst.Add(new BaseCommonControls.FamilyValue("", "Offert", 100));
            lst.Add(new BaseCommonControls.FamilyValue("", "5%", 5));
            lst.Add(new BaseCommonControls.FamilyValue("", "10%", 10));
            lst.Add(new BaseCommonControls.FamilyValue("","15%",15));
            lst.Add(new BaseCommonControls.FamilyValue("","20%",20));
            lst.Add(new BaseCommonControls.FamilyValue("","25%",25));
            lst.Add(new BaseCommonControls.FamilyValue("","30%",30));
            lst.Add(new BaseCommonControls.FamilyValue("","35%",35));
            lst.Add(new BaseCommonControls.FamilyValue("","40%",40));
            lst.Add(new BaseCommonControls.FamilyValue("","45%",45));
            lst.Add(new BaseCommonControls.FamilyValue("", "50%", 50));
            lst.Add(new BaseCommonControls.FamilyValue("", "Normal", 0));

            slidingList1.LoadFromFamilyValueList(lst);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void slidingList1_OnSelectionChange(object sender, EventArgs e)
        {
            rbpercent.Checked = true;
            rbMontant.Checked = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = Value < 0 ? System.Windows.Forms.DialogResult.Cancel : System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            rbpercent.Checked = false;
            rbMontant.Checked = true;
            rbMontantRemise.Checked = false;
        }

        private void mTxtMontantRemise_KeyDown(object sender, KeyEventArgs e)
        {
            rbpercent.Checked = false;
            rbMontant.Checked = false;
            rbMontantRemise.Checked = true;
        }

        private void rbMontant_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
