using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BASEDiagAdulte.Ctrls
{
    public partial class ExtandablePictureBox : PictureBox
    {
        public ExtandablePictureBox()
        {
            InitializeComponent();
        }

       

        protected override void OnPaint(PaintEventArgs pe)
        {
            // This is the only line needed for anti-aliasing to be turned on.
            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // the next two lines of code (not comments) are needed to get the highest 
            // possible quiality of anti-aliasing. Remove them if you want the image to render faster.
            pe.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            // this line is needed for .net to draw the contents.
            base.OnPaint(pe); 
        }

        protected override void OnClick(EventArgs e)
        {
            FrmFullScreen frm = new FrmFullScreen(this);
            frm.Show();
            base.OnClick(e);
        }

    }

    
}
