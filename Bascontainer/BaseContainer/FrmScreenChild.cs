using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FrmScreenChild : Form
    {

        private Screen _dedicatedScreen;
        public Screen dedicatedScreen
        {
            get
            {
                return _dedicatedScreen;
            }
            set
            {
                _dedicatedScreen = value;
            }
        }


        public FrmScreenChild(Screen screen)
        {
            dedicatedScreen = screen;
            InitializeComponent();

            string BackScreenFile = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\FondEcran.png";

            if (System.IO.File.Exists(BackScreenFile))
            {
                this.BackgroundImage = Bitmap.FromFile(BackScreenFile);
                this.BackgroundImageLayout = ImageLayout.Center;
            }
        }

        private void FrmScreenChild_Load(object sender, EventArgs e)
        {

            this.Bounds = dedicatedScreen.WorkingArea;

        }

        private void FrmScreenChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmScreenChild_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
