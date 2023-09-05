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
    public partial class AnSourire : ImageCtrlAgg, IAnalyse
    {

        Droite LevreSup;
        Droite LevreInf;

        Droite d0 ;
        Droite d1 ;
        Droite d2 ;
        Droite d3 ;
        Droite d4 ;
        Droite d5 ;
        Droite d6 ;
        
        Droite d11;
        Droite d12;
        Droite d13;
        Droite d14;
        Droite d15;
        Droite d16;

        private bool _ShowReel = true;
        public bool ShowReel
        {
            get
            {
                return _ShowReel;
            }
            set
            {
                _ShowReel = value;
            }
        }
        private ResumeClinique _resumeclinique = null;
        public ResumeClinique resumeclinique
        {
            get
            {
                return _resumeclinique;
            }
            set
            {
                _resumeclinique = value;
            }
        }

        #region Points


        public int InterincisiveHaut = 0;
       public int InterincisiveBas = 1;
       public int ComGauche = 2;
       public int ComDroit = 3;
       public int i1 = 4;
       public int i2 = 5;
       public int i3 = 6;
       public int i4 = 7;
       public int i5 = 8;
       public int i6 = 9;

       public int i11 = 10;
       public int i12 = 11;
       public int i13 = 12;
       public int i14 = 13;
       public int i15 = 14;
       public int i16 = 15;



        #endregion



        public string Resultat
        {
            get
            {
                string s = "";

                return s;
            }
           
        }
        
       
        public string GetTextResultat()
        {
            
            return Resultat;
        }
        

        protected override void DrawTextResult(Epsitec.Common.Drawing.Graphics AggGr)
        {
            int Firstwidth = 150;
            int y = Height - 2;
            double fontsize = 12;
            Epsitec.Common.Drawing.Font ft = Epsitec.Common.Drawing.Font.GetFont("Arial", "Regular");
           
            Epsitec.Common.Drawing.Color neutralcolor = new Epsitec.Common.Drawing.Color(1, 0, 0, 0);

            DrawOutlinedText(AggGr, GetTextResultat(), ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            

            
        }
        



        public void ReinitRotationAuto() {
            if (ListOfPoints[ComDroit].visible &&
                                ListOfPoints[ComGauche].visible)
            {
                float angle = BASEDiagAdulte.Ctrls.GeomTools.AngleOfView(ListOfPoints[ComDroit].Pt,
                                                                   ListOfPoints[ComGauche].Pt,
                                                                   new Point(2000, ListOfPoints[ComDroit].Pt.Y));

                AngleDeRotationRadio = -angle-1;
            }
       
        
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


        public void Recalculate()
        {
            
            /*
            PointF[] pts = ConvertListOfPointToArray();

            pts = TransformPtRadioToScreen(pts);

            */
        


        }




        public void DrawExtraForms(Epsitec.Common.Drawing.Graphics gr, bool ForImpression)
        {


            if (currentmode == ImageCtrlAgg.ModeSaisie.None)
            {
                PointF[] pts = ConvertListOfPointToArray();


                pts = TransformPtRadioToScreen(pts);



                LevreSup = GeomTools.FindDroiteOf(pts[ComDroit], pts[ComGauche]);
                LevreInf = LevreSup.FindParalleleOn(pts[InterincisiveBas]);
                d0 = LevreSup.FindPerpendiculaireOn(pts[InterincisiveHaut]);
                d1 = LevreSup.FindPerpendiculaireOn(pts[i1]);
                d2 = LevreSup.FindPerpendiculaireOn(pts[i2]);
                d3 = LevreSup.FindPerpendiculaireOn(pts[i3]);
                d4 = LevreSup.FindPerpendiculaireOn(pts[i4]);
                d5 = LevreSup.FindPerpendiculaireOn(pts[i5]);
                d6 = LevreSup.FindPerpendiculaireOn(pts[i6]);

                d11 = LevreSup.FindPerpendiculaireOn(pts[i11]);
                d12 = LevreSup.FindPerpendiculaireOn(pts[i12]);
                d13 = LevreSup.FindPerpendiculaireOn(pts[i13]);
                d14 = LevreSup.FindPerpendiculaireOn(pts[i14]);
                d15 = LevreSup.FindPerpendiculaireOn(pts[i15]);
                d16 = LevreSup.FindPerpendiculaireOn(pts[i16]);


                PointF intermedDroit = LevreInf.FindProjectionOrthogonalOf(new PointF((pts[InterincisiveBas].X + pts[ComDroit].X) / 2, (pts[InterincisiveBas].Y + pts[ComDroit].Y) / 2));
                PointF intermedGauche = LevreInf.FindProjectionOrthogonalOf(new PointF((pts[InterincisiveBas].X + pts[ComGauche].X) / 2, (pts[InterincisiveBas].Y + pts[ComGauche].Y) / 2));



                PointF x0 = d0.GetIntersectionWith(LevreSup);
                PointF x1 = d1.GetIntersectionWith(LevreSup);
                float refdist = GeomTools.Distance(x0, x1);
                float width = refdist;

                float dist = refdist * 0.75f;
                width += dist;
                for (int i = 0; i < 5; i++)
                {
                    dist *= .75f;
                    width += dist;
                }

                PointF ComGaucheTheo = LevreSup.GetIntersectionWith(x0, width, pts[ComGauche]);

                x0 = d0.GetIntersectionWith(LevreSup);
                x1 = d11.GetIntersectionWith(LevreSup);
                refdist = GeomTools.Distance(x0, x1);
                width = refdist;

                dist = refdist * 0.75f;
                width += dist;
                for (int i = 0; i < 5; i++)
                {
                    dist *= .75f;
                    width += dist;
                }

                PointF ComDroitTheo = LevreSup.GetIntersectionWith(x0, width, pts[ComDroit]);

                PointF intermedGaucheTheo = LevreInf.FindProjectionOrthogonalOf(new PointF((pts[InterincisiveBas].X + ComGaucheTheo.X) / 2, (pts[InterincisiveBas].Y + ComGaucheTheo.Y) / 2));
                PointF intermedDroitTheo = LevreInf.FindProjectionOrthogonalOf(new PointF((pts[InterincisiveBas].X + ComDroitTheo.X) / 2, (pts[InterincisiveBas].Y + ComDroitTheo.Y) / 2));

                Epsitec.Common.Drawing.Color backcolorActive = new Epsitec.Common.Drawing.Color(1, 0, 0, 0);
                Epsitec.Common.Drawing.Color backcolorinactive = new Epsitec.Common.Drawing.Color(0, 0, 0, 0);
                Epsitec.Common.Drawing.Color frontcolorActive = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
                Epsitec.Common.Drawing.Color frontcolorinactive = new Epsitec.Common.Drawing.Color(.2, 1, 1, 1);

                DrawSourire(gr, pts, intermedDroit, intermedGauche, ShowReel ? backcolorActive : backcolorinactive, ShowReel ? frontcolorActive : frontcolorinactive);
                DrawSourireTheoG(gr, pts, intermedDroit, ComGaucheTheo, !ShowReel ? backcolorActive : backcolorinactive, !ShowReel ? frontcolorActive : frontcolorinactive);
                DrawSourireTheoD(gr, pts, intermedGauche, ComDroitTheo, !ShowReel ? backcolorActive : backcolorinactive, !ShowReel ? frontcolorActive : frontcolorinactive);

                DrawRealTooth(gr, pts,intermedDroit, intermedGauche, ShowReel ? backcolorActive : backcolorinactive, ShowReel ? frontcolorActive : frontcolorinactive);
                DrawTheoToothG(gr, pts, intermedGaucheTheo, ComGaucheTheo, !ShowReel ? backcolorActive : backcolorinactive, !ShowReel ? frontcolorActive : frontcolorinactive);
                DrawTheoToothD(gr, pts, intermedDroitTheo, ComDroitTheo, !ShowReel ? backcolorActive : backcolorinactive, !ShowReel ? frontcolorActive : frontcolorinactive);
            }

        }

        private void DrawTheoToothD(Epsitec.Common.Drawing.Graphics gr, PointF[] pts, PointF intermedDroit, PointF ComDroitTheo, Epsitec.Common.Drawing.Color backcolor, Epsitec.Common.Drawing.Color frontcolor)
        {

            Epsitec.Common.Drawing.Path pth = new Epsitec.Common.Drawing.Path();

            PointF x0, x1, x2, x3, x4, x5, x6;

            x0 = d0.GetIntersectionWith(LevreSup);
            x1 = d11.GetIntersectionWith(LevreSup);
            float refdist = GeomTools.Distance(x0, x1);

            float dist = refdist * 0.75f;
            x2 = LevreSup.GetIntersectionWith(x1, dist, ComDroitTheo);
            dist *= 0.75f;
            x3 = LevreSup.GetIntersectionWith(x2, dist, ComDroitTheo);
            dist *= 0.75f;
            x4 = LevreSup.GetIntersectionWith(x3, dist, ComDroitTheo);
            dist *= 0.75f;
            x5 = LevreSup.GetIntersectionWith(x4, dist, ComDroitTheo);
            dist *= 0.75f;
            x6 = LevreSup.GetIntersectionWith(x5, dist, ComDroitTheo);

            Droite d2b = LevreSup.FindPerpendiculaireOn(x2);
            Droite d3b = LevreSup.FindPerpendiculaireOn(x3);
            Droite d4b = LevreSup.FindPerpendiculaireOn(x4);
            Droite d5b = LevreSup.FindPerpendiculaireOn(x5);
            Droite d6b = LevreSup.FindPerpendiculaireOn(x6);

            PointF pt1;
            PointF pt2;
            try
            {
                pt1 = d0.GetIntersectionWith(LevreSup);
                pt2 = d0.GetIntersectionWith(LevreInf);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d11.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetIntersectionWithCurve(pts[InterincisiveBas], intermedDroit, intermedDroit, ComDroitTheo, d11)[0];
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d2b.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetIntersectionWithCurve(pts[InterincisiveBas], intermedDroit, intermedDroit, ComDroitTheo, d2b)[0];
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d3b.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetIntersectionWithCurve(pts[InterincisiveBas], intermedDroit, intermedDroit, ComDroitTheo, d3b)[0];
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d4b.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetIntersectionWithCurve(pts[InterincisiveBas], intermedDroit, intermedDroit, ComDroitTheo, d4b)[0];
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d5b.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetIntersectionWithCurve(pts[InterincisiveBas], intermedDroit, intermedDroit, ComDroitTheo, d5b)[0];
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d6b.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetIntersectionWithCurve(pts[InterincisiveBas], intermedDroit, intermedDroit, ComDroitTheo, d6b)[0];
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }



            gr.Rasterizer.AddOutline(pth, 4);
            gr.SolidRenderer.Color = backcolor;
            gr.RenderSolid();

            gr.Rasterizer.AddOutline(pth, 2);
            gr.SolidRenderer.Color = frontcolor;
            gr.RenderSolid();

        }

        private void DrawTheoToothG(Epsitec.Common.Drawing.Graphics gr, PointF[] pts, PointF intermedGauche, PointF ComGaucheTheo, Epsitec.Common.Drawing.Color backcolor, Epsitec.Common.Drawing.Color frontcolor)
        {

            Epsitec.Common.Drawing.Path pth = new Epsitec.Common.Drawing.Path();

            PointF x0,x1,x2,x3,x4,x5,x6;

            x0 = d0.GetIntersectionWith(LevreSup);
            x1 = d1.GetIntersectionWith(LevreSup);
            float refdist = GeomTools.Distance(x0,x1);
            
           float  dist = refdist * 0.75f;
           x2 = LevreSup.GetIntersectionWith(x1, dist, ComGaucheTheo);
            dist *= 0.75f;
            x3 = LevreSup.GetIntersectionWith(x2, dist, ComGaucheTheo);
            dist *= 0.75f;
            x4 = LevreSup.GetIntersectionWith(x3, dist, ComGaucheTheo);
            dist *= 0.75f;
            x5 = LevreSup.GetIntersectionWith(x4, dist, ComGaucheTheo);
            dist *= 0.75f;
            x6 = LevreSup.GetIntersectionWith(x5, dist, ComGaucheTheo);

            Droite d2b = LevreSup.FindPerpendiculaireOn(x2);
            Droite d3b = LevreSup.FindPerpendiculaireOn(x3);
            Droite d4b = LevreSup.FindPerpendiculaireOn(x4);
            Droite d5b = LevreSup.FindPerpendiculaireOn(x5);
            Droite d6b = LevreSup.FindPerpendiculaireOn(x6);

            PointF pt1;
            PointF pt2;
            try
            {
                pt1 = d0.GetIntersectionWith(LevreSup);
                pt2 = d0.GetIntersectionWith(LevreInf);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d1.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetIntersectionWithCurve(pts[InterincisiveBas], intermedGauche, intermedGauche, ComGaucheTheo, d1)[0];
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d2b.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetIntersectionWithCurve(pts[InterincisiveBas], intermedGauche, intermedGauche, ComGaucheTheo, d2b)[0];
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d3b.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetIntersectionWithCurve(pts[InterincisiveBas], intermedGauche, intermedGauche, ComGaucheTheo, d3b)[0];
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d4b.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetIntersectionWithCurve(pts[InterincisiveBas], intermedGauche, intermedGauche, ComGaucheTheo, d4b)[0];
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d5b.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetIntersectionWithCurve(pts[InterincisiveBas], intermedGauche, intermedGauche, ComGaucheTheo, d5b)[0];
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d6b.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetIntersectionWithCurve(pts[InterincisiveBas], intermedGauche, intermedGauche, ComGaucheTheo, d6b)[0];
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }

           

            gr.Rasterizer.AddOutline(pth,4);
            gr.SolidRenderer.Color = backcolor;
            gr.RenderSolid();

            gr.Rasterizer.AddOutline(pth, 2);
            gr.SolidRenderer.Color = frontcolor;
            gr.RenderSolid();

        }


        private void DrawRealTooth(Epsitec.Common.Drawing.Graphics gr, PointF[] pts,PointF intermedDroite, PointF intermedGauche, Epsitec.Common.Drawing.Color backcolor, Epsitec.Common.Drawing.Color frontcolor)
        {

            Epsitec.Common.Drawing.Path pth = new Epsitec.Common.Drawing.Path();

            PointF pt1;
            PointF pt2;
            try
            {
                pt1 = d0.GetIntersectionWith(LevreSup);
                pt2 = d0.GetIntersectionWith(LevreInf);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d1.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetNearestPointWithCurve(pts[InterincisiveBas], intermedGauche, intermedGauche, pts[ComGauche], d1);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d2.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetNearestPointWithCurve(pts[InterincisiveBas], intermedGauche, intermedGauche, pts[ComGauche], d2);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d3.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetNearestPointWithCurve(pts[InterincisiveBas], intermedGauche, intermedGauche, pts[ComGauche], d3);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d4.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetNearestPointWithCurve(pts[InterincisiveBas], intermedGauche, intermedGauche, pts[ComGauche], d4);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d5.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetNearestPointWithCurve(pts[InterincisiveBas], intermedGauche, intermedGauche, pts[ComGauche], d5);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            try
            {
                pt1 = d6.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetNearestPointWithCurve(pts[InterincisiveBas], intermedGauche, intermedGauche, pts[ComGauche], d6);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }



            try
            {
                pt1 = d11.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetNearestPointWithCurve(pts[InterincisiveBas], intermedDroite, intermedDroite, pts[ComDroit], d11);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }

            try
            {
                pt1 = d12.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetNearestPointWithCurve(pts[InterincisiveBas], intermedDroite, intermedDroite, pts[ComDroit], d12);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }

            try
            {
                pt1 = d13.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetNearestPointWithCurve(pts[InterincisiveBas], intermedDroite, intermedDroite, pts[ComDroit], d13);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }

            try
            {
                pt1 = d14.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetNearestPointWithCurve(pts[InterincisiveBas], intermedDroite, intermedDroite, pts[ComDroit], d14);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }

            try
            {
                pt1 = d15.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetNearestPointWithCurve(pts[InterincisiveBas], intermedDroite, intermedDroite, pts[ComDroit], d15);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }

            try
            {
                pt1 = d16.GetIntersectionWith(LevreSup);
                pt2 = GeomTools.GetNearestPointWithCurve(pts[InterincisiveBas], intermedDroite, intermedDroite, pts[ComDroit], d16);
                pth.MoveTo(pt1.X, pt1.Y);
                pth.LineTo(pt2.X, pt2.Y);
            }
            catch (System.Exception ex) { }
            gr.Rasterizer.AddOutline(pth, 4);
            gr.SolidRenderer.Color = backcolor;
            gr.RenderSolid();
            gr.Rasterizer.AddOutline(pth, 2);
            gr.SolidRenderer.Color = frontcolor;
            gr.RenderSolid();
        }

        private void DrawSourire(Epsitec.Common.Drawing.Graphics gr, PointF[] pts, PointF intermedDroit, PointF intermedGauche, Epsitec.Common.Drawing.Color backcolor, Epsitec.Common.Drawing.Color frontcolor)
        {

            Epsitec.Common.Drawing.Path pth = new Epsitec.Common.Drawing.Path();

            pth.MoveTo(pts[ComGauche].X, pts[ComGauche].Y);
            pth.LineTo(pts[ComDroit].X, pts[ComDroit].Y);


            pth.CurveTo(intermedDroit.X, intermedDroit.Y, intermedDroit.X, intermedDroit.Y, pts[InterincisiveBas].X, pts[InterincisiveBas].Y);
            pth.CurveTo(intermedGauche.X, intermedGauche.Y, intermedGauche.X, intermedGauche.Y, pts[ComGauche].X, pts[ComGauche].Y);



            gr.Rasterizer.AddOutline(pth, 4);
            gr.SolidRenderer.Color = backcolor;
            gr.RenderSolid();

            gr.Rasterizer.AddOutline(pth, 2);
            gr.SolidRenderer.Color = frontcolor;
            gr.RenderSolid();

        }


        private void DrawSourireTheoD(Epsitec.Common.Drawing.Graphics gr, PointF[] pts, PointF intermedDroit, PointF ComDroitTheo, Epsitec.Common.Drawing.Color backcolor, Epsitec.Common.Drawing.Color frontcolor)
        {


            PointF intermedGauche = LevreInf.FindProjectionOrthogonalOf(new PointF((pts[InterincisiveBas].X + ComDroitTheo.X) / 2, (pts[InterincisiveBas].Y + ComDroitTheo.Y) / 2));
            PointF CentreLevreSup = LevreSup.FindProjectionOrthogonalOf(pts[InterincisiveHaut]);


            Epsitec.Common.Drawing.Path pth = new Epsitec.Common.Drawing.Path();

            pth.MoveTo(CentreLevreSup.X, CentreLevreSup.Y);
            pth.LineTo(ComDroitTheo.X, ComDroitTheo.Y);       

            pth.CurveTo(intermedGauche.X, intermedGauche.Y, intermedGauche.X, intermedGauche.Y, pts[InterincisiveBas].X, pts[InterincisiveBas].Y);
         

            gr.Rasterizer.AddOutline(pth, 4);
            gr.SolidRenderer.Color = backcolor;
            gr.RenderSolid();

            gr.Rasterizer.AddOutline(pth, 2);
            gr.SolidRenderer.Color = frontcolor;
            gr.RenderSolid();

        }



        private void DrawSourireTheoG(Epsitec.Common.Drawing.Graphics gr, PointF[] pts, PointF intermedDroit, PointF ComGaucheTheo, Epsitec.Common.Drawing.Color backcolor, Epsitec.Common.Drawing.Color frontcolor)
        {


           PointF intermedGauche = LevreInf.FindProjectionOrthogonalOf(new PointF((pts[InterincisiveBas].X + ComGaucheTheo.X) / 2, (pts[InterincisiveBas].Y + ComGaucheTheo.Y) / 2));
           PointF CentreLevreSup = LevreSup.FindProjectionOrthogonalOf(pts[InterincisiveHaut]);


            Epsitec.Common.Drawing.Path pth = new Epsitec.Common.Drawing.Path();

            pth.MoveTo(CentreLevreSup.X, CentreLevreSup.Y);
            pth.LineTo(ComGaucheTheo.X, ComGaucheTheo.Y);
           // pth.LineTo(pts[ComDroit].X, pts[ComDroit].Y);

            pth.CurveTo(intermedGauche.X, intermedGauche.Y, intermedGauche.X, intermedGauche.Y, pts[InterincisiveBas].X, pts[InterincisiveBas].Y);
          //  pth.CurveTo(intermedGauche.X, intermedGauche.Y, intermedGauche.X, intermedGauche.Y, ComGaucheTheo.X, ComGaucheTheo.Y);
            

            gr.Rasterizer.AddOutline(pth, 4);
            gr.SolidRenderer.Color = backcolor;
            gr.RenderSolid();

            gr.Rasterizer.AddOutline(pth, 2);
            gr.SolidRenderer.Color = frontcolor;
            gr.RenderSolid();

        }


        public AnSourire()
        {

            _ListOfPoints.Add(new PointToTake("Interincisive Haut"));
            _ListOfPoints.Add(new PointToTake("Interincisive Bas"));
            _ListOfPoints.Add(new PointToTake("Com Droit"));
            _ListOfPoints.Add(new PointToTake("Com Gauche"));

            _ListOfPoints.Add(new PointToTake("contact 21/22"));
            _ListOfPoints.Add(new PointToTake("contact 22/23"));
            _ListOfPoints.Add(new PointToTake("contact 23/24"));
            _ListOfPoints.Add(new PointToTake("contact 24/25"));
            _ListOfPoints.Add(new PointToTake("contact 25/26"));
            _ListOfPoints.Add(new PointToTake("contact 26/27"));

            _ListOfPoints.Add(new PointToTake("contact 11/12"));
            _ListOfPoints.Add(new PointToTake("contact 12/13"));
            _ListOfPoints.Add(new PointToTake("contact 13/14"));
            _ListOfPoints.Add(new PointToTake("contact 14/15"));
            _ListOfPoints.Add(new PointToTake("contact 15/16"));
            _ListOfPoints.Add(new PointToTake("contact 16/17"));


            InterincisiveHaut = 0;
            InterincisiveBas = 1;
            ComDroit = 2;
            ComGauche = 3;
            i1 = 4;
            i2 = 5;
            i3 = 6;
            i4 = 7;
            i5 = 8;
            i6 = 9;
            
            i11 = 10;
            i12 = 11;
            i13 = 12;
            i14 = 13;
            i15 = 14;
            i16 = 15;


            InitializeComponent();
        }

        private void AnSourire_Load(object sender, EventArgs e)
        {

        }
    }
}
