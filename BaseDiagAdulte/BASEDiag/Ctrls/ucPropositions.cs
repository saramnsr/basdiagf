using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BL;
using BasCommon_BO;

namespace BASEDiagAdulte.Ctrls
{
    public partial class ucPropositions : UserControl
    {
        public bool Prop11
        {
            get
            {
                return chkbxProp11.Checked;
            }
           
        }

        public bool Prop12
        {
            get
            {
                return chkbxProp12.Checked;
            }

        }

        public bool Prop21
        {
            get
            {
                return chkbxProp21.Checked;
            }

        }

        public bool Prop22
        {
            get
            {
                return chkbxProp22.Checked;
            }

        }

        public bool Prop31
        {
            get
            {
                return chkbxProp31.Checked;
            }

        }

        public bool Prop32
        {
            get
            {
                return chkbxProp32.Checked;
            }

        }

        public bool Prop33
        {
            get
            {
                return chkbxProp33.Checked;
            }

        }

        private basePatient _CurrentPatient;
        public basePatient CurrentPatient
        {
            get
            {
                return _CurrentPatient;
            }
            set
            {
                _CurrentPatient = value;
                if (_CurrentPatient!=null) 
                    InitButoons();
            }
        }


        public ucPropositions()
        {
            InitializeComponent();
        }


        private void InitButoons()
        {
            chkbxProp11.Text = "1.1\r\n";
            chkbxProp12.Text = "1.2\r\n";
            chkbxProp21.Text = "2.1\r\n";
            chkbxProp22.Text = "2.2\r\n";
            chkbxProp31.Text = "3.1\r\n";
            chkbxProp32.Text = "3.2\r\n";
            chkbxProp33.Text = "3.3\r\n";

            foreach (CommonObjectif cd in CurrentPatient.SelectedObjectifs)
            {
                switch (cd.categorie)
                {
                    case CommonObjectif.CategorieObjectifs.Adulte11: chkbxProp11.Text += "\n" + cd.Libelle; break;
                    case CommonObjectif.CategorieObjectifs.Adulte12: chkbxProp12.Text += "\n" + cd.Libelle; break;
                    case CommonObjectif.CategorieObjectifs.Adulte21: chkbxProp21.Text += "\n" + cd.Libelle; break;
                    case CommonObjectif.CategorieObjectifs.Adulte22: chkbxProp22.Text += "\n" + cd.Libelle; break;
                    case CommonObjectif.CategorieObjectifs.Adulte31: chkbxProp31.Text += "\n" + cd.Libelle; break;
                    case CommonObjectif.CategorieObjectifs.Adulte32: chkbxProp32.Text += "\n" + cd.Libelle; break;
                    case CommonObjectif.CategorieObjectifs.Adulte33: chkbxProp33.Text += "\n" + cd.Libelle; break;
                }

                
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
