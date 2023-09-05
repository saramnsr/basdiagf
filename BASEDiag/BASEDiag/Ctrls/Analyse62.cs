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
    public partial class Analyse62 : ImageCtrlAgg, IAnalyse
    {

        #region Points

        public int Ref = 0;
        public int Po = 1;
        public int Or = 2;
        public int BIS = 3;
        public int BIS2 = 4;
        public int LevreSup = 3;
        public int LevreInf = 4;
        public int Menton = 5;

        #endregion

        #region Resultats

        public string IncisiveSupRes = "Normo";
        public string InclinaisonRes = "Normo";
        public float Inclinaison = 0;
        public string LevreSupRes = "Normo";
        public string LevreInfRes = "Normo";
        public string MentonRes = "Normo";

        #endregion

        public string GetTextResultat()
        {


            string s = "Incisive sup : " + IncisiveSupRes;
            s += "\nLevre sup : " + LevreSupRes;
            s += "\nLevre inf : " + LevreInfRes;
            s += "\nMenton : " + MentonRes;

            return s;
        }

        public void ReinitRotationAuto() {
        
            //if (((PointToTake)sender) == ImgProfilSourire.ListOfPoints[ImgProfilSourire.Or])
            if (ListOfPoints[Po].visible &&
                ListOfPoints[Or].visible)
            {
                float angle = BASEDiag.Ctrls.GeomTools.AngleOfView(ListOfPoints[Po].Pt,
                                                                   ListOfPoints[Or].Pt,
                                                                   new Point(2000, ListOfPoints[Po].Pt.Y));

                AngleDeRotationRadio = -angle;
            }

        }

        public void Recalculate()
        {
            int MARGE = 15;
            int MARGEANGLE = 3;

            PointF[] pts = ConvertListOfPointToArray();

            /*
            Point[] pts = new Point[5]{ Ref.Pt,
                                        Po.Pt,
                                        Or.Pt,
                                        BIS.Pt,
                                        BIS2.Pt};
            */
            pts = TransformPtRadioToScreen(pts);

            double a = GeomTools.AngleOfView(pts[BIS], pts[BIS2]);

            if (a < 179)
                IncisiveSupRes = "Retro";
            else if (a > 181)
                IncisiveSupRes = "Pro";
            else IncisiveSupRes = "Normo";

            Inclinaison = GeomTools.AngleOfView(pts[Or], pts[Po], pts[BIS2], pts[BIS]);
            Inclinaison = Inclinaison > 180 ? 360 - Inclinaison : Inclinaison;

            if ((Inclinaison - 110) > MARGEANGLE)
                InclinaisonRes = "Pro";
            else if ((Inclinaison - 110) < -MARGEANGLE)
                InclinaisonRes = "Retro";
            else InclinaisonRes = "Normo";


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


        public Analyse62()
        {
            InitializeComponent();
            _ListOfPoints.Add(new PointToTake("Front"));
            _ListOfPoints.Add(new PointToTake("Po cutané"));
            _ListOfPoints.Add(new PointToTake("Or cutané"));
            _ListOfPoints.Add(new PointToTake("BIS"));
            _ListOfPoints.Add(new PointToTake("BIS cutané"));
            _ListOfPoints.Add(new PointToTake("Lèvre supérieur"));
            _ListOfPoints.Add(new PointToTake("Lèvre inférieur"));
            _ListOfPoints.Add(new PointToTake("Menton cutané"));

            Ref = 0;
            Po = 1;
            Or = 2;
            BIS2 = 3;
            BIS = 4;
            LevreSup = 5;
            LevreInf = 6;
            Menton = 7;


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

            ////

            Epsitec.Common.Drawing.Font HelpftPrincipal = Epsitec.Common.Drawing.Font.GetFont("Arial", "Regular");

            DrawOutlinedText(gr, "BAS DIAG ANALYSIS ®", HelpftPrincipal, 20, 200, 100);


            ////

            if (currentmode == ImageCtrlAgg.ModeSaisie.None)
            {

                PointF[] pts = ConvertListOfPointToArray();

                /*
                Point[] pts = new Point[5]{ Ref.Pt,
                                            Po.Pt,
                                            Or.Pt,
                                            BIS.Pt,
                                            BIS2.Pt};
                */
                pts = TransformPtRadioToScreen(pts);
                
               
                Epsitec.Common.Drawing.Path pth = new Epsitec.Common.Drawing.Path();


                pth.MoveTo(pts[Ref].X, 0);
                pth.LineTo(pts[Ref].X, 2000);

                pth.MoveTo(0, pts[Ref].Y);
                pth.LineTo(2000, pts[Ref].Y);

                pth.MoveTo(pts[BIS].X, pts[BIS].Y);
                pth.LineTo(pts[BIS2].X, pts[BIS2].Y);

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
