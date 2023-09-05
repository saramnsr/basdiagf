using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BL;
using BASEDiag_BO;

namespace BASEDiag
{
    public partial class FrmSurveillance : Form
    {


        private Proposition _proposition;
        public Proposition proposition
        {
            get
            {
                return _proposition;
            }
            set
            {
                _proposition = value;
            }
        }

        public FrmSurveillance(Proposition prop)
        {
            proposition = prop;
            InitializeComponent();
        }

        private void FrmSurveillance_Load(object sender, EventArgs e)
        {
            tvS1.Items.Add("0");
            tvS1.Items.Add("1");
            tvS1.Items.Add("2");
            tvS1.Items.Add("3");
            tvS1.Items.Add("4");

            tvS2.Items.Add("0");
            tvS2.Items.Add("1");
            tvS2.Items.Add("2");
            tvS2.Items.Add("3");
            tvS2.Items.Add("4");

            tvS3.Items.Add("0");
            tvS3.Items.Add("1");
            tvS3.Items.Add("2");
            tvS3.Items.Add("3");
            tvS3.Items.Add("4");

            tvS4.Items.Add("0");
            tvS4.Items.Add("1");
            tvS4.Items.Add("2");
            tvS4.Items.Add("3");
            tvS4.Items.Add("4");

            tvS5.Items.Add("0");
            tvS5.Items.Add("1");
            tvS5.Items.Add("2");
            tvS5.Items.Add("3");
            tvS5.Items.Add("4");

            tvS6.Items.Add("0");
            tvS6.Items.Add("1");
            tvS6.Items.Add("2");
            tvS6.Items.Add("3");
            tvS6.Items.Add("4");

            tvS7.Items.Add("0");
            tvS7.Items.Add("1");
            tvS7.Items.Add("2");
            tvS7.Items.Add("3");
            tvS7.Items.Add("4");

            tvS8.Items.Add("0");
            tvS8.Items.Add("1");
            tvS8.Items.Add("2");
            tvS8.Items.Add("3");
            tvS8.Items.Add("4");


            foreach (Traitement t in proposition.traitements)
            {

                if (t.Phase == TemplateActePG.EnumPhase.Orthopedique)
                {
                    foreach (Semestre s in t.semestres)
                    {
                        if (s.NumSemestre == 1)
                        {
                            tvS1.Enabled = true;
                            tvS1.SelectedItem = s.NbSurveillance.ToString();
                        }
                        if (s.NumSemestre == 2)
                        {
                            tvS2.Enabled = true;
                            tvS2.SelectedItem = s.NbSurveillance.ToString();
                        }
                        if (s.NumSemestre == 3)
                        {
                            tvS3.Enabled = true;
                            tvS3.SelectedItem = s.NbSurveillance.ToString();
                        }
                        if (s.NumSemestre == 4)
                        {
                            tvS4.Enabled = true;
                            tvS4.SelectedItem = s.NbSurveillance.ToString();
                        }
                        if (s.NumSemestre == 5)
                        {
                            tvS5.Enabled = true;
                            tvS5.SelectedItem = s.NbSurveillance.ToString();
                        }
                        if (s.NumSemestre == 6)
                        {
                            tvS6.Enabled = true;
                            tvS6.SelectedItem = s.NbSurveillance.ToString();
                        }
                        if (s.NumSemestre == 7)
                        {
                            tvS7.Enabled = true;
                            tvS7.SelectedItem = s.NbSurveillance.ToString();
                        }
                        if (s.NumSemestre == 8)
                        {
                            tvS8.Enabled = true;
                            tvS8.SelectedItem = s.NbSurveillance.ToString();
                        }
                    }
                }
            }


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            

            foreach(Traitement t in proposition.traitements)
                foreach (Semestre s in t.semestres)
                {
                    if (s.NumSemestre == 1)
                        s.NbSurveillance = Convert.ToInt32(tvS1.SelectedItem);
                    if (s.NumSemestre == 2)
                        s.NbSurveillance = Convert.ToInt32(tvS2.SelectedItem);
                    if (s.NumSemestre == 3)
                        s.NbSurveillance = Convert.ToInt32(tvS3.SelectedItem);
                    if (s.NumSemestre == 4)
                        s.NbSurveillance = Convert.ToInt32(tvS4.SelectedItem);
                    if (s.NumSemestre == 5)
                        s.NbSurveillance = Convert.ToInt32(tvS5.SelectedItem);
                    if (s.NumSemestre == 6)
                        s.NbSurveillance = Convert.ToInt32(tvS6.SelectedItem);
                    if (s.NumSemestre == 7)
                        s.NbSurveillance = Convert.ToInt32(tvS7.SelectedItem);
                    if (s.NumSemestre == 8)
                        s.NbSurveillance = Convert.ToInt32(tvS8.SelectedItem);
                }


            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
