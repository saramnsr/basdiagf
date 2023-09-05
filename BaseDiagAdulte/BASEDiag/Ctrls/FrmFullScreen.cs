using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BASEDiagAdulte.Ctrls
{
    public partial class FrmFullScreen : Form
    {

        public PictureBoxSizeMode FullScreenSizeMode
        {
            get
            {
                return pbxFullScreen.SizeMode;
            }
            set
            {
                pbxFullScreen.SizeMode = value;
            }
        }
        
        private ExtandablePictureBox _owner;
        public ExtandablePictureBox owner
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
            }
        }

        public FrmFullScreen(ExtandablePictureBox ownedby)
        {
            owner = ownedby;
            InitializeComponent();

            pbxFullScreen.Image = owner.Image;
            pbxFullScreen.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void pbxFullScreen_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
