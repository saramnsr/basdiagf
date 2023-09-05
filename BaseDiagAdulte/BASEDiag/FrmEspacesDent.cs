using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BASEDiagAdulte
{
    public partial class FrmEspacesDent : Form
    {


        public double[] values
        {
            get
            {
                double[] array = new double[30];
                array[0] = Convert.ToDouble(txtbx1817.Text);
                array[1] = Convert.ToDouble(txtbx1716.Text);
                array[2] = Convert.ToDouble(txtbx1615.Text);
                array[3] = Convert.ToDouble(txtbx1514.Text);
                array[4] = Convert.ToDouble(txtbx1413.Text);
                array[5] = Convert.ToDouble(txtbx1312.Text);
                array[6] = Convert.ToDouble(txtbx1211.Text);
                array[7] = Convert.ToDouble(txtbx1121.Text);
                array[8] = Convert.ToDouble(txtbx2122.Text);
                array[9] = Convert.ToDouble(txtbx2223.Text);
                array[10] = Convert.ToDouble(txtbx2324.Text);
                array[11] = Convert.ToDouble(txtbx2425.Text);
                array[12] = Convert.ToDouble(txtbx2526.Text);
                array[13] = Convert.ToDouble(txtbx2627.Text);
                array[14] = Convert.ToDouble(txtbx2728.Text);


                array[15] = Convert.ToDouble(txtbx3738.Text);
                array[16] = Convert.ToDouble(txtbx3637.Text);
                array[17] = Convert.ToDouble(txtbx3536.Text);
                array[18] = Convert.ToDouble(txtbx3435.Text);
                array[19] = Convert.ToDouble(txtbx3334.Text);
                array[20] = Convert.ToDouble(txtbx3233.Text);
                array[21] = Convert.ToDouble(txtbx3132.Text);
                array[22] = Convert.ToDouble(txtbx3141.Text);
                array[23] = Convert.ToDouble(txtbx4142.Text);
                array[24] = Convert.ToDouble(txtbx4243.Text);
                array[25] = Convert.ToDouble(txtbx4344.Text);
                array[26] = Convert.ToDouble(txtbx4445.Text);
                array[27] = Convert.ToDouble(txtbx4546.Text);
                array[28] = Convert.ToDouble(txtbx4647.Text);
                array[29] = Convert.ToDouble(txtbx4748.Text);

                return array;
            }
            set
            {
                double[] array = value;
                txtbx1817.Text = array[0] .ToString();
                txtbx1716.Text = array[1] .ToString();
                txtbx1615.Text = array[2] .ToString();
                txtbx1514.Text = array[3] .ToString();
                txtbx1413.Text = array[4] .ToString();
                txtbx1312.Text = array[5] .ToString();
                txtbx1211.Text = array[6] .ToString();
                txtbx1121.Text = array[7] .ToString();
                txtbx2122.Text = array[8] .ToString();
                txtbx2223.Text = array[9] .ToString();
                txtbx2324.Text = array[10].ToString();
                txtbx2425.Text = array[11].ToString();
                txtbx2526.Text = array[12].ToString();
                txtbx2627.Text = array[13].ToString();
                txtbx2728.Text = array[14].ToString();


                txtbx3738.Text = array[15].ToString();
                txtbx3637.Text = array[16].ToString(); 
                txtbx3536.Text = array[17].ToString(); 
                txtbx3435.Text = array[18].ToString(); 
                txtbx3334.Text = array[19].ToString(); 
                txtbx3233.Text = array[20].ToString(); 
                txtbx3132.Text = array[21].ToString(); 
                txtbx3141.Text = array[22].ToString(); 
                txtbx4142.Text = array[23].ToString(); 
                txtbx4243.Text = array[24].ToString(); 
                txtbx4344.Text = array[25].ToString(); 
                txtbx4445.Text = array[26].ToString(); 
                txtbx4546.Text = array[27].ToString(); 
                txtbx4647.Text = array[28].ToString(); 
                txtbx4748.Text = array[29].ToString(); 

            }
        }
        


        public FrmEspacesDent()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Build())
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private bool Build()
        {

            return true;
        }

        private void FrmEspacesDent_Load(object sender, EventArgs e)
        {

        }
    }
}
