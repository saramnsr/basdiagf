using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;

namespace BASEDiagAdulte.Ctrls
{


    public partial class Synchro : ImageCtrlAgg, IAnalyse
    {
        public int Point2 = 1;
        public int Point1 = 0;

        public Synchro()
        {
            InitializeComponent();

            _ListOfPoints.Add(new PointToTake("Point1"));
            _ListOfPoints.Add(new PointToTake("Point2"));
        }

        public PointF[] ConvertListOfPointToArray()
        {
            PointF[] ptarray = new PointF[_ListOfPoints.Count];
            for (int i = 0; i < _ListOfPoints.Count; i++)
            {
                ptarray[i] = new PointF(_ListOfPoints[i].Pt.X, _ListOfPoints[i].Pt.Y);
            }

            return ptarray;
        }

        public void DrawExtraForms(Epsitec.Common.Drawing.Graphics gr, bool ForImpression)
        {

            if (currentmode == ImageCtrlAgg.ModeSaisie.None)
            {

                PointF[] pts = ConvertListOfPointToArray();


                pts = TransformPtRadioToScreen(pts);



                Epsitec.Common.Drawing.Path pth = new Epsitec.Common.Drawing.Path();

                //Masque Facial
                pth.MoveTo(pts[Point1].X, pts[Point1].Y);
                pth.LineTo(pts[Point2].X, pts[Point2].Y);
                pth.Close();





                gr.Rasterizer.AddOutline(pth, 4);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
                gr.RenderSolid();

                gr.Rasterizer.AddOutline(pth, 2);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 0, 0.5, 0);
                gr.RenderSolid();




            }

        }




        public string GetTextResultat()
        {
            return "";
        }

        public void Recalculate()
        { 
        }

        public void ReinitRotationAuto()
        { 
        }
    }


    

}
