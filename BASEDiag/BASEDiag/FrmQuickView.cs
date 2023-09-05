using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BASEDiag
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
          //  pictureBox1.Image = Bitmap.FromFile(bmpfile);
        }
    }
}
