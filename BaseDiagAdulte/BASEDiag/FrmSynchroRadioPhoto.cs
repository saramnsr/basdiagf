using BASEDiagAdulte.Ctrls;
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
    public partial class FrmSynchroRadioPhoto : Form
    {

        public FrmSynchroRadioPhoto(Epsitec.Common.Drawing.Bitmap bitmap1, Epsitec.Common.Drawing.Bitmap bitmap2)
        {
            InitializeComponent();

            var bmp = (Epsitec.Common.Drawing.Bitmap)Epsitec.Common.Drawing.Bitmap.FromNativeBitmap((Bitmap)(bitmap1.NativeBitmap.Clone()));
            synchro1.loadRadio(bmp);
            bmp = (Epsitec.Common.Drawing.Bitmap)Epsitec.Common.Drawing.Bitmap.FromNativeBitmap((Bitmap)(bitmap2.NativeBitmap.Clone()));
            synchro2.loadRadio(bmp);

            synchro1.Center();
            synchro1.zoomAuto();
            synchro2.Center();
            synchro2.zoomAuto();
        }



        public PointF OffSet { get; set; }

        public float AngleRot { get; set; }

        public float Zoom { get; set; }

        private void synchro1_Load(object sender, EventArgs e)
        {
            
        }

        private void FrmSynchroRadioPhoto_Load(object sender, EventArgs e)
        {
            synchro1.StartSaisie();
            synchro2.StartSaisie();
        }

        private void FrmSynchroRadioPhoto_Resize(object sender, EventArgs e)
        {
            synchro1.Center();
            synchro1.zoomAuto();
            synchro2.Center();
            synchro2.zoomAuto();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            if ((synchro1.currentmode != ImageCtrlAgg.ModeSaisie.None) ||
                (synchro2.currentmode != ImageCtrlAgg.ModeSaisie.None)) return;

            PointF RotationPointInPhoto = new PointF((float)(synchro2.RadioImage.Width / 2f), (float)(synchro2.RadioImage.Height / 2f));


            double d1 = GeomTools.Distance(synchro1.ListOfPoints[synchro1.Point1].Pt, synchro1.ListOfPoints[synchro1.Point2].Pt);
            double d2 = GeomTools.Distance(synchro2.ListOfPoints[synchro2.Point1].Pt, synchro2.ListOfPoints[synchro2.Point2].Pt);

            //zoomPhoto = 1;
            Zoom = (float)(d1 / d2);

            Epsitec.Common.Drawing.Transform t = new Epsitec.Common.Drawing.Transform();

            float a = GeomTools.AngleOfView(synchro1.ListOfPoints[synchro1.Point1].Pt, synchro1.ListOfPoints[synchro1.Point2].Pt,
                                            synchro2.ListOfPoints[synchro2.Point1].Pt, synchro2.ListOfPoints[synchro2.Point2].Pt);
            AngleRot = a;


            t.Translate(-RotationPointInPhoto.X, -RotationPointInPhoto.Y);
            t.Scale(Zoom);
            t.RotateDeg(AngleRot);
            t.Translate(RotationPointInPhoto.X, RotationPointInPhoto.Y);

            double x = synchro2.ListOfPoints[synchro2.Point1].Pt.X;
            double y = synchro2.ListOfPoints[synchro2.Point1].Pt.Y;


            t.TransformDirect(ref x, ref y);


            OffSet = new PointF((synchro1.ListOfPoints[synchro1.Point1].Pt.X - (float)x), ((float)y - synchro1.ListOfPoints[synchro1.Point1].Pt.Y));

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}
