using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseCommonControls
{
    public partial class FrmRepartition : Form
    {

        private double _montantMutuelle=0;
            public double montantMutuelle
            {
               get
            {
                return _montantMutuelle;
            }
            set
            {
                _montantMutuelle = value;
            }
            }
            private double _montantPatient = 0;
            public double montantPatient
            {
                get
                {
                    return _montantPatient;
                }
                set
                {
                    _montantPatient = value;
                }
            }
            private double _total = 0;
            public double total
            {
                get
                {
                    return _total;
                }
                set
                {
                    _total = value;
                }
            }
            bool verifScenario = false;
        public FrmRepartition(double totalMontant,double montantPat,double montantMut,BasCommon_BO.NewTraitement.typeScenario type=BasCommon_BO.NewTraitement.typeScenario.Prothése)
        {
            total = totalMontant;
            montantMutuelle = montantMut;
            montantPatient = montantPat;
            InitializeComponent();
            textBox1.Text = montantMutuelle.ToString();
            textBox2.Text = montantPatient.ToString();

        }

        private void FrmRepartition_Load(object sender, EventArgs e)
        {

            RefreshSummary();
        }
        public void RefreshSummary()
        {
            double partpatient = 0;

            double partmutuelle = 0;

            if(!verifScenario)
            if (!double.TryParse(textBox2.Text, out partpatient))
                return;
           

            if (!double.TryParse(textBox1.Text, out partmutuelle))
                return;
            if (partmutuelle > total)
            {
                lblSummury.Text = "Montant Incorrect";
                lblSummury.ForeColor = Color.Orange;
                textBox2.Text = Convert.ToString(total - partmutuelle);
                return;
            }

            textBox2.Text = Convert.ToString(total  - partmutuelle);
            lblSummury.Text = "";
          //  double totalmontant = 0;
           // double totalrepartis = partpatient  + partmutuelle;


          //  lblSummury.Text = "Reste " + (total - totalrepartis).ToString("C2") + " à répartir sur " + total.ToString("C2");

           // lblSummury.ForeColor = Math.Round(total) == Math.Round(totalrepartis) ? Color.Green : Color.Orange;

        }
        private void button1_Click(object sender, EventArgs e)
        {
         

        }
        private bool IsDouble(string text)
        {
            Double num = 0;
            bool isDouble = false;

            // Check for empty string.
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            isDouble = Double.TryParse(text, out num);

            return isDouble;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            RefreshSummary();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
           
            RefreshSummary();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
           // RefreshSummary();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            RefreshSummary();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {


            if ((textBox1.Text != string.Empty) && !(IsDouble(textBox1.Text.Replace(".", ","))))
                {
                    MessageBox.Show("Format incorrecte");
                    return;
                }
            
         
            if(textBox1.Text != string.Empty)
            montantMutuelle = Convert.ToDouble(textBox1.Text.Replace(".",","));
            if (!verifScenario)
            montantPatient = Convert.ToDouble(textBox2.Text.Replace(".",","));

            if ((montantMutuelle + montantPatient) != Math.Round(total,2))
            {
                MessageBox.Show("Montant incorrecte");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
