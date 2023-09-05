using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BASEDiagAdulte
{
    public partial class FrmQuickView : Form
    {



        private string _bmpfile;
        public string bmpfile
        {
            get
            {
                return _bmpfile;
            }
            set
            {
                _bmpfile = value;
            }
        }
        

        public FrmQuickView(string file)
        {
            bmpfile = file;
            InitializeComponent();
        }

        private void FrmQuickView_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FrmQuickView_Shown(object sender, EventArgs e)
        {
            var req = System.Net.WebRequest.Create(bmpfile);
            using (Stream stream = req.GetResponse().GetResponseStream())
            {
                pictureBox1.Image = Bitmap.FromStream(stream);
            }
           
        }
    }
}
