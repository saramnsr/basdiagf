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
    public partial class Analyse7 : ImageCtrlAgg, IAnalyse
    {

        #region Points

        public int  PtT_S = 0;
        public int  PtT_N = 1;
        public int PtT_A = 2;
        public int PtT_B = 3;    
        public int PtT_Or = 4;
        public int PtT_Po = 5;
        public int PtT_Me = 6;
        public int PtT_Go = 7;
        public int PtT_BIS = 8;
        public int  PtT_AIS = 9;
        public int  PtT_BII = 10;
        public int PtT_AII = 11;
        public int PtT_IF = 12;
        public int PtT_SPP = 13;
        public int PtT_SPA = 14;

        #endregion

        #region Resultats

        public float FMA = 0;
        public float SNA = 0;
        public float SNB = 0;
        public float ANB = 0;
        public float IF = 0;
        public float IM = 0;
        public float I2F = 0;
        public float SPP = 0;
        public float SPA = 0;    

        #endregion
        public Boolean ShowLabel;
        public PointF[] ConvertListOfPointToArray()
        {
            PointF[] ptarray = new PointF[_ListOfPoints.Count];
            for (int i = 0; i < _ListOfPoints.Count; i++)
            {
                ptarray[i] = new PointF(_ListOfPoints[i].Pt.X, _ListOfPoints[i].Pt.Y);
            }

            return ptarray;
        }

        public Analyse7()
        {
            InitializeComponent();

            _ListOfPoints.Add(new PointToTake("S"));
            _ListOfPoints.Add(new PointToTake("N"));
            _ListOfPoints.Add(new PointToTake("A"));
            _ListOfPoints.Add(new PointToTake("B"));            
            _ListOfPoints.Add(new PointToTake("Or osseux"));
            _ListOfPoints.Add(new PointToTake("Po osseux"));
            _ListOfPoints.Add(new PointToTake("Me osseux"));
            _ListOfPoints.Add(new PointToTake("Go osseux"));      
            _ListOfPoints.Add(new PointToTake("BIS dentaire"));
            _ListOfPoints.Add(new PointToTake("AIS"));
            _ListOfPoints.Add(new PointToTake("BII"));
            _ListOfPoints.Add(new PointToTake("AII"));
            _ListOfPoints.Add(new PointToTake("IF"));
            //// ajout nadhem
            _ListOfPoints.Add(new PointToTake("SPP"));
            _ListOfPoints.Add(new PointToTake("SPA"));



            PtT_S = 0;
        PtT_N = 1;
        PtT_A = 2;
        PtT_B = 3;    
        PtT_Or = 4;
        PtT_Po = 5;
        PtT_Me = 6;
        PtT_Go = 7;
        PtT_BIS = 8;
        PtT_AIS = 9;
        PtT_BII = 10;
        PtT_AII = 11;
        PtT_IF = 12;
        PtT_SPP = 13;
        PtT_SPA = 14;
          

        }


        public void ReinitRotationAuto()
        {
            if (ListOfPoints[PtT_Po].visible && ListOfPoints[PtT_Or].visible)
            {
                float angle = BASEDiag.Ctrls.GeomTools.AngleOfView(ListOfPoints[PtT_Po].Pt,
                                                                   ListOfPoints[PtT_Or].Pt,
                                                                   new Point(2000, ListOfPoints[PtT_Po].Pt.Y));

                AngleDeRotationRadio = -angle;
            }
        }
        
        public void Recalculate()
        {


            PointF[] pts = ConvertListOfPointToArray();
            /*
            Point[] pts = new Point[13]{ PtT_S.Pt,      
                                            PtT_N.Pt,
                                            PtT_A.Pt,
                                            PtT_B.Pt,
                                            PtT_Po.Pt,
                                            PtT_Go.Pt,
                                            PtT_Or.Pt,
                                            PtT_Me.Pt,
                                            PtT_AIS.Pt,
                                            PtT_BIS.Pt,
                                            PtT_AII.Pt,
                                            PtT_BII.Pt,
                                            PtT_IF.Pt};
            */
            pts = TransformPtRadioToScreen(pts);

            IF = GeomTools.AngleOfView(pts[PtT_AIS], pts[PtT_BIS], pts[PtT_Or], pts[PtT_Po]);
            IF = IF > 180 ? 360 - IF : IF;

            Droite d = GeomTools.FindDroiteOf(pts[PtT_IF], pts[PtT_BIS]);
            Droite d2 = GeomTools.FindDroiteOf(pts[PtT_Po], pts[PtT_Or]);
            PointF d2d = d.GetIntersectionWith(d2);
            I2F = GeomTools.AngleOfView(d2d, pts[PtT_BIS], pts[PtT_Po]);
            I2F = I2F > 180 ? 360 - I2F : I2F;
            IM = GeomTools.AngleOfView(pts[PtT_BII], pts[PtT_AII], pts[PtT_Go], pts[PtT_Me]);
            IM = IM > 180 ? 360 - IM : IM;
            FMA = GeomTools.AngleOfView(pts[PtT_Po], pts[PtT_Or], pts[PtT_Go], pts[PtT_Me]);
            FMA = FMA > 180 ? 360 - FMA : FMA;
            SNA = GeomTools.AngleOfView(pts[PtT_N], pts[PtT_S], pts[PtT_A]);
            SNA = SNA > 180 ? 360 - SNA : SNA;
            SNB = GeomTools.AngleOfView(pts[PtT_N], pts[PtT_S], pts[PtT_B]);
            SNB = SNB > 180 ? 360 - SNB : SNB;
            ANB = GeomTools.AngleOfView(pts[PtT_N], pts[PtT_A], pts[PtT_B]);
            ANB = ANB > 180 ? 360 - ANB : ANB;
            try
            {
                SPP = GeomTools.AngleOfView(pts[PtT_SPA], pts[PtT_SPP], pts[PtT_Me], pts[PtT_Go]);
                SPP = SPP > 180 ? 360 - SPP : SPP;
            }
            catch (Exception e)
            {
            }
            
            
        }

        public void DrawExtraForms(Epsitec.Common.Drawing.Graphics gr, bool ForImpression)
        {
            if (currentmode == ImageCtrlAgg.ModeSaisie.None)
            {
                PointF[] pts = ConvertListOfPointToArray();
                /*
                Point[] pts = new Point[13]{ PtT_S.Pt,      
                                                PtT_N.Pt,
                                                PtT_A.Pt,
                                                PtT_B.Pt,
                                                PtT_Po.Pt,
                                                PtT_Go.Pt,
                                                PtT_Or.Pt,
                                                PtT_Me.Pt,
                                                PtT_AIS.Pt,
                                                PtT_BIS.Pt,
                                                PtT_AII.Pt,
                                                PtT_BII.Pt,
                                                PtT_IF.Pt};
                */
                pts = TransformPtRadioToScreen(pts);


                Epsitec.Common.Drawing.Path pth = new Epsitec.Common.Drawing.Path();

                //SN
                pth.MoveTo(pts[PtT_S].X, pts[PtT_S].Y);
                pth.LineTo(pts[PtT_N].X, pts[PtT_N].Y);
                //AN
                pth.MoveTo(pts[PtT_A].X, pts[PtT_A].Y);
                pth.LineTo(pts[PtT_N].X, pts[PtT_N].Y);
                //BN
                pth.MoveTo(pts[PtT_N].X, pts[PtT_N].Y);
                pth.LineTo(pts[PtT_B].X, pts[PtT_B].Y);

                //Incisive Sup
                pth.MoveTo(pts[PtT_AIS].X, pts[PtT_AIS].Y);
                pth.LineTo(pts[PtT_BIS].X, pts[PtT_BIS].Y);
                //Incisive Inf
                pth.MoveTo(pts[PtT_AII].X, pts[PtT_AII].Y);
                pth.LineTo(pts[PtT_BII].X, pts[PtT_BII].Y);


                Droite dOrPo = GeomTools.FindDroiteOf(pts[PtT_Or], pts[PtT_Po]);
                Droite dMeGo = GeomTools.FindDroiteOf(pts[PtT_Me], pts[PtT_Go]);

                PointF intersect = dOrPo.GetIntersectionWith(dMeGo);

                //int ymin = (int)dOrPo.GetY(0);

                //dOrPo
                pth.MoveTo(intersect.X, intersect.Y);
                pth.LineTo(pts[PtT_Or].X, pts[PtT_Or].Y);

                //ymin = (int)dMeGo.GetY(0);

                //dMeGo
                pth.MoveTo(intersect.X, intersect.Y);
                pth.LineTo(pts[PtT_Me].X, pts[PtT_Me].Y);

                try
                {
                    ////nadheeeeeeeeem
                    pth.MoveTo(pts[PtT_SPA].X, pts[PtT_SPA].Y);
                    pth.LineTo(pts[PtT_SPP].X, pts[PtT_SPP].Y);

                    Droite dSPP = GeomTools.FindDroiteOf(pts[PtT_SPP], pts[PtT_SPA]);

                    PointF intersect1 = dSPP.GetIntersectionWith(dMeGo);
                    pth.MoveTo(intersect1.X, intersect1.Y);
                    pth.LineTo(pts[PtT_SPA].X, pts[PtT_SPA].Y);
                }
                catch (Exception e)
                { }

                ////////

                gr.Rasterizer.AddOutline(pth, 4);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
                gr.RenderSolid();

                gr.Rasterizer.AddOutline(pth, 2);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 0, 0.5, 0);
                gr.RenderSolid();

                
            }

        }

        protected override void DrawTextResult(Epsitec.Common.Drawing.Graphics AggGr)
        {
            if (ShowLabel)
            {
                Epsitec.Common.Drawing.Font HelpftPrincipal = Epsitec.Common.Drawing.Font.GetFont("Arial", "Regular");

                DrawOutlinedText(AggGr, "BAS DIAG ANALYSIS ®", HelpftPrincipal, 20, 200, 100);

            }
            int Firstwidth = 50;
            int Smallwidth = 50;

            Epsitec.Common.Drawing.Color neutralcolor = new Epsitec.Common.Drawing.Color(1, 0, 0, 0);
            Epsitec.Common.Drawing.Color badcolor = new Epsitec.Common.Drawing.Color(1, 1, 0, 0);
            Epsitec.Common.Drawing.Color goodcolor = new Epsitec.Common.Drawing.Color(1, 0, 1, 0);
            Epsitec.Common.Drawing.Color badorgoodcolor = goodcolor;


            Epsitec.Common.Drawing.Font ft = Epsitec.Common.Drawing.Font.GetFont("Arial", "Regular");
            double fontsize = 12;

            double variation = 0;
            int y = Height - 2;

            #region SNA

            variation = SNA < 80 ? variation = SNA - 80 : SNA > 82 ? SNA - 82 : 0;
                       
            if (Math.Round(variation) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, "SNA", ft, fontsize, 2, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "80-82°", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", SNA) + "°", ft, fontsize, Firstwidth + (1 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", variation) + "°", ft, fontsize, Firstwidth + (2 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            

            #endregion
            y -= 15;
            #region SNB

            variation = SNB < 78 ? variation = SNB - 78 : SNB > 80 ? SNB - 80 : 0;

            if (Math.Round(variation) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, "SNB", ft, fontsize, 2, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "78-80°", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", SNB) + "°", ft, fontsize, Firstwidth + (1 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", variation) + "°", ft, fontsize, Firstwidth + (2 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            

            #endregion
            y -= 15;
            #region ANB

            variation = ANB < 2 ? variation = ANB - 2 : ANB > 4 ? ANB - 4 : 0;

            
            if (Math.Round(variation) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, "ANB", ft, fontsize, 2, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "2-4°", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", ANB) + "°", ft, fontsize, Firstwidth + (1 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", variation) + "°", ft, fontsize, Firstwidth + (2 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            

            #endregion
            y -= 15;
            #region FMA

            variation = FMA < 18 ? variation = FMA - 18 : FMA > 21 ? FMA - 21 : 0;

           
            if (Math.Round(variation) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, "FMA", ft, fontsize, 2, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "18-23°", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", FMA) + "°", ft, fontsize, Firstwidth + (1 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", variation) + "°", ft, fontsize, Firstwidth + (2 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            

            #endregion
            y -= 15;
            #region IF

            variation = IF - 107;
                      

            if (Math.Round(variation) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, "I/F", ft, fontsize, 2, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "107°", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", IF) + "°", ft, fontsize, Firstwidth + (1 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", variation) + "°", ft, fontsize, Firstwidth + (2 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            


            #endregion
            y -= 15;
            #region IM

            variation = IM - 90;

            
            if (Math.Round(variation) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, "I/M", ft, fontsize, 2, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "90°", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", IM) + "°", ft, fontsize, Firstwidth + (1 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", variation) + "°", ft, fontsize, Firstwidth + (2 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
           

            #endregion
            y -= 15;
           #region I2F
            if (Math.Round(variation) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, "I2/F", ft, fontsize, 2, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            //DrawOutlinedText(AggGr, "", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", I2F) + "°", ft, fontsize, Firstwidth + (1 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
           // DrawOutlinedText(AggGr, string.Format("{0:0}", variation) + "°", ft, fontsize, Firstwidth + (2 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            #endregion
            y -= 15;
            #region SPP

            variation = SPP < 25 ? variation = SPP - 25 : SPP > 27 ? SPP - 27 : 0;


            if (Math.Round(variation) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, "SPA-SPP", ft, fontsize, 2, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "25-27°", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", SPP) + "°", ft, fontsize, Firstwidth + (1 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", variation) + "°", ft, fontsize, Firstwidth + (2 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            #endregion
        }
        
        public string GetTextResultat()
        {
           

            return "";
        }

        private void Analyse7_Load(object sender, EventArgs e)
        {
            ShowLabel = false;
        }


       
        
    }
}
