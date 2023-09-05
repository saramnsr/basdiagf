using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;

namespace BASEDiag.Ctrls
{
    public partial class Analyse6 : ImageCtrlAgg, IAnalyse
    {

        #region Points

        public int Ref = 0;
        public int Po = 1;
        public int Or = 2;
        public int LevreSup = 3;
        public int LevreInf = 4;
        public int Menton = 5;

        #endregion

        #region Resultats

        public string LevreSupRes = "Normo";
        public string LevreInfRes = "Normo";
        public string MentonRes = "Normo";


        #endregion

        public string GetTextResultat()
        {


            string s = "Levre sup : " + LevreSupRes;
            s += "\nLevre inf : " + LevreInfRes;
            s += "\nMenton : " + MentonRes;


            return s;
        }


        public Point[] ConvertListOfPointToArray()
        {
            Point[] ptarray = new Point[_ListOfPoints.Count];
            for (int i = 0; i < _ListOfPoints.Count; i++)
            {
                ptarray[i] = new Point(_ListOfPoints[i].Pt.X, _ListOfPoints[i].Pt.Y);
            }

            return ptarray;
        }


        public void Recalculate()
        {
            int MARGE = 25;


            Point[] pts = ConvertListOfPointToArray();

            /*
            Point[] pts = new Point[4]{ Ref.Pt,
                                            LevreSup.Pt,
                                            LevreInf.Pt,
                                            Menton.Pt};
             * 
             */

            TransformPtBmpToScreen(pts);

            if ((pts[Ref].X - pts[LevreSup].X) > MARGE)
                LevreSupRes = "Retro";
            else if ((pts[Ref].X - pts[LevreSup].X) < -MARGE)
                LevreSupRes = "Pro";
            else LevreSupRes = "Normo";

            if ((pts[Ref].X - pts[Menton].X) > MARGE)
                MentonRes = "Retro";
            else if ((pts[Ref].X - pts[Menton].X) < -MARGE)
                MentonRes = "Pro";
            else MentonRes = "Normo";

            if ((pts[Ref].X - pts[LevreInf].X) > MARGE)
                LevreInfRes = "Retro";
            else if ((pts[Ref].X - pts[LevreInf].X) < -MARGE)
                LevreInfRes = "Pro";
            else LevreInfRes = "Normo";


            Invalidate();
        }


        public Analyse6()
        {
            InitializeComponent();



            _ListOfPoints.Add(new PointToTake("Front"));
            _ListOfPoints.Add(new PointToTake("Po"));
            _ListOfPoints.Add(new PointToTake("Or"));
            _ListOfPoints.Add(new PointToTake("Lèvre supérieur"));
            _ListOfPoints.Add(new PointToTake("Lèvre inférieur"));
            _ListOfPoints.Add(new PointToTake("Menton"));

            Ref = 0;
            Po = 1;
            Or = 2;
            LevreSup = 3;
            LevreInf = 4;
            Menton = 5;

        }



        public void DrawExtraForms(Epsitec.Common.Drawing.Graphics gr)
        {

            
            if (!SaisieStarted)
            {

                Point[] pts = ConvertListOfPointToArray();

                /*
                Point[] pts = new Point[4]{ Ref.Pt,
                                                LevreSup.Pt,
                                                LevreInf.Pt,
                                                Menton.Pt};
                 * 
                 */

                TransformPtBmpToScreen(pts);
                
               
                Epsitec.Common.Drawing.Path pth = new Epsitec.Common.Drawing.Path();


                pth.MoveTo(pts[Ref].X, 0);
                pth.LineTo(pts[Ref].X, 2000);


                pth.MoveTo(0, pts[Ref].Y);
                pth.LineTo(2000, pts[Ref].Y);


                pth.MoveTo(pts[Po].X, pts[Po].Y);
                pth.LineTo(pts[Or].X, pts[Or].Y);


                gr.Rasterizer.AddOutline(pth, 4);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
                gr.RenderSolid();

                gr.Rasterizer.AddOutline(pth, 2);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 0, 0.5, 0);
                gr.RenderSolid();


            }
             
        }
    }
}
