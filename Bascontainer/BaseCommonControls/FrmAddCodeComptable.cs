using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BO.Compta;
using BasCommon_BL;
using BasCommon_BL.Compta;


namespace BaseCommonControls
{
    public partial class FrmAddCodeComptable : Form
    {


        private CodeComptable _Value;
        public CodeComptable Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }
        

        public FrmAddCodeComptable()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (Build())
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private bool Build()
        {


            if (maskedTextBox1.Text.Length < 4)
            {
                MessageBox.Show("Le code comptable est incorrecte!");
                return false;
            }

            if (textBox1.Text == "")
            {
                MessageBox.Show("Le libelle est obligatoire!");
                return false;
            }


            CodeComptable cc = MgmtCodeComptable.getFromCode(maskedTextBox1.Text);
            if (cc!=null)
            {
                MessageBox.Show("Ce code existe deja!");
                return false;
            }

            Value = new CodeComptable();
            Value.Code = maskedTextBox1.Text;
            Value.Libelle = textBox1.Text;
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void FrmAddCodeComptable_Load(object sender, EventArgs e)
        {

        }
    }
}
