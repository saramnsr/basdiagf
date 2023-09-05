using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseCommonControls
{
    public partial class CtrlEspacement : UserControl
    {


        			
				
			

        private Dictionary<string,double>  _Espacement = new Dictionary<string,double>() ;
        public Dictionary<string,double>   Espacement
        {
            get
            {
                return _Espacement;
            }
            set
            {
                _Espacement = value;
            }
        }
        
        public void InitDisplay()
        {

            txtbx1112.Text = Espacement["1112"].ToString();
            txtbx1213.Text = Espacement["1213"].ToString();
            txtbx1314.Text = Espacement["1314"].ToString();
            txtbx1415.Text = Espacement["1415"].ToString();
            txtbx1516.Text = Espacement["1516"].ToString();
            txtbx1617.Text = Espacement["1617"].ToString();
            txtbx1718.Text = Espacement["1718"].ToString();

            txtbx2122.Text = Espacement["2122"].ToString();
            txtbx2223.Text = Espacement["2223"].ToString();
            txtbx2324.Text = Espacement["2324"].ToString();
            txtbx2425.Text = Espacement["2425"].ToString();
            txtbx2526.Text = Espacement["2526"].ToString();
            txtbx2627.Text = Espacement["2627"].ToString();
            txtbx2728.Text = Espacement["2728"].ToString();

            txtbx3132.Text = Espacement["3132"].ToString();
            txtbx3233.Text = Espacement["3233"].ToString();
            txtbx3334.Text = Espacement["3334"].ToString();
            txtbx3435.Text = Espacement["3435"].ToString();
            txtbx3536.Text = Espacement["3536"].ToString();
            txtbx3637.Text = Espacement["3637"].ToString();
            txtbx3738.Text = Espacement["3738"].ToString();

            txtbx4142.Text = Espacement["4142"].ToString();
            txtbx4243.Text = Espacement["4243"].ToString();
            txtbx4344.Text = Espacement["4344"].ToString();
            txtbx4445.Text = Espacement["4445"].ToString();
            txtbx4546.Text = Espacement["4546"].ToString();
            txtbx4647.Text = Espacement["4647"].ToString();
            txtbx4748.Text = Espacement["4748"].ToString();

        }

        public void Build()
        {

            Espacement["1112"] = Convert.ToDouble(txtbx1112.Text);
            Espacement["1213"] = Convert.ToDouble(txtbx1213.Text);
            Espacement["1314"] = Convert.ToDouble(txtbx1314.Text);
            Espacement["1415"] = Convert.ToDouble(txtbx1415.Text);
            Espacement["1516"] = Convert.ToDouble(txtbx1516.Text);
            Espacement["1617"] = Convert.ToDouble(txtbx1617.Text);
            Espacement["1718"] = Convert.ToDouble(txtbx1718.Text);

            Espacement["2122"] = Convert.ToDouble(txtbx2122.Text);
            Espacement["2223"] = Convert.ToDouble(txtbx2223.Text);
            Espacement["2324"] = Convert.ToDouble(txtbx2324.Text);
            Espacement["2425"] = Convert.ToDouble(txtbx2425.Text);
            Espacement["2526"] = Convert.ToDouble(txtbx2526.Text);
            Espacement["2627"] = Convert.ToDouble(txtbx2627.Text);
            Espacement["2728"] = Convert.ToDouble(txtbx2728.Text);

            Espacement["3132"] = Convert.ToDouble(txtbx3132.Text);
            Espacement["3233"] = Convert.ToDouble(txtbx3233.Text);
            Espacement["3334"] = Convert.ToDouble(txtbx3334.Text);
            Espacement["3435"] = Convert.ToDouble(txtbx3435.Text);
            Espacement["3536"] = Convert.ToDouble(txtbx3536.Text);
            Espacement["3637"] = Convert.ToDouble(txtbx3637.Text);
            Espacement["3738"] = Convert.ToDouble(txtbx3738.Text);

            Espacement["4142"] = Convert.ToDouble(txtbx4142.Text);
            Espacement["4243"] = Convert.ToDouble(txtbx4243.Text);
            Espacement["4344"] = Convert.ToDouble(txtbx4344.Text);
            Espacement["4445"] = Convert.ToDouble(txtbx4445.Text);
            Espacement["4546"] = Convert.ToDouble(txtbx4546.Text);
            Espacement["4647"] = Convert.ToDouble(txtbx4647.Text);
            Espacement["4748"] = Convert.ToDouble(txtbx4748.Text);
        }

        public CtrlEspacement()
        {
            InitializeComponent();

            Espacement.Add("1112",0);
            Espacement.Add("1213",0);
            Espacement.Add("1314",0);
            Espacement.Add("1415",0);
            Espacement.Add("1516",0);
            Espacement.Add("1617",0);
            Espacement.Add("1718",0);

            Espacement.Add("2122",0);
            Espacement.Add("2223",0);
            Espacement.Add("2324",0);
            Espacement.Add("2425",0);
            Espacement.Add("2526",0);
            Espacement.Add("2627",0);
            Espacement.Add("2728",0);

            Espacement.Add("3132",0);
            Espacement.Add("3233",0);
            Espacement.Add("3334",0);
            Espacement.Add("3435",0);
            Espacement.Add("3536",0);
            Espacement.Add("3637",0);
            Espacement.Add("3738",0);

            Espacement.Add("4142",0);
            Espacement.Add("4243",0);
            Espacement.Add("4344",0);
            Espacement.Add("4445",0);
            Espacement.Add("4546",0);
            Espacement.Add("4647",0);
            Espacement.Add("4748",0);

        }


       

        private void CtrlEspacement_Load(object sender, EventArgs e)
        {

        }
    }
}
