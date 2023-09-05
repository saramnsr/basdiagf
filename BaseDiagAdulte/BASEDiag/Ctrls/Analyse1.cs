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

    public partial class Analyse1 : ImageCtrlAgg, IAnalyse
    {

        #region Points


        /*
          _ListOfPoints.Add(new PointToTake("Visage Sup"));
            _ListOfPoints.Add(new PointToTake("Face Sup"));
            _ListOfPoints.Add(new PointToTake("Sous Nasal"));
            _ListOfPoints.Add(new PointToTake("Contact labial"));
            _ListOfPoints.Add(new PointToTake("Levre inférieure"));
            _ListOfPoints.Add(new PointToTake("Menton"));
            _ListOfPoints.Add(new PointToTake("Externe Droit"));
            _ListOfPoints.Add(new PointToTake("Externe Gauche"));
         */

        public int ExterneGauche = 7;
        public int ExterneDroit = 6;
        public int Menton = 5;
        public int LevreInferieur = 4;
        public int ContactLabial = 3;        
        public int SousNasal = 2;        
        public int FaceSup = 1;                
        public int VisageSup = 0;


        #endregion

        #region Resultats

        public float  EtageSup = 0;
        public float EtageMoy = 0;
        public float EtageInf = 0;
        public float EtageMoy2 = 0;
        public float EtageInf2 = 0;
        public float EtageInfSup = 0;
        public float  EtageInfInf = 0;
        public float  DeviationLevreInf = 0;
        public float  DeviationMenton = 0;


        #endregion

        
       
        public string GetTextResultat()
        {
            string s = "Etage Supérieur = " + string.Format("{0:0}", EtageSup*100) + "%";
            s += "\nEtage Moyen = " + string.Format("{0:0}", EtageMoy * 100) + "% " + string.Format("{0:0}", EtageMoy2 * 100) + "%";
            s += "\nEtage Inférieur = " + string.Format("{0:0}", EtageInf * 100) + "% " + string.Format("{0:0}", EtageInf2 * 100) + "%";
            s += "\n  Etage maxillaire = " + string.Format("{0:0}", EtageInfSup * 100) + "%";
            s += "\n  Etage mandibulaire = " + string.Format("{0:0}", EtageInfInf * 100) + "%";
            
            if(DeviationLevreInf<-.03)
                s += "\nDéviation Levre Inferieur Gauche";
            if (DeviationLevreInf > .03)
                s += "\nDéviation Levre Inferieur Droite";

            if (DeviationMenton < -.03)
                s += "\nDéviation Menton Gauche";
            if (DeviationMenton > .03)
                s += "\nDéviation Menton Droite";

           
            return s;
        }
        

        protected override void DrawTextResult(Epsitec.Common.Drawing.Graphics AggGr)
        {

            int Firstwidth = 150;
            int Smallwidth = 50;

           Epsitec.Common.Drawing.Color neutralcolor = new Epsitec.Common.Drawing.Color(1, 0, 0, 0);
            Epsitec.Common.Drawing.Color badcolor = new Epsitec.Common.Drawing.Color(1, 1, 0, 0);
            Epsitec.Common.Drawing.Color goodcolor = new Epsitec.Common.Drawing.Color(1, 0, 1, 0);
            
            Epsitec.Common.Drawing.Color neutralcolorGray = new Epsitec.Common.Drawing.Color(.5, 0, 0, 0);
            Epsitec.Common.Drawing.Color badcolorGray = new Epsitec.Common.Drawing.Color(.5, 1, 0, 0);
            Epsitec.Common.Drawing.Color goodcolorGray = new Epsitec.Common.Drawing.Color(.5, 0, 1, 0);
            
            Epsitec.Common.Drawing.Color badorgoodcolor = goodcolor;
            Epsitec.Common.Drawing.Font ft = Epsitec.Common.Drawing.Font.GetFont("Arial", "Regular");
            double fontsize = 12;

            int y = Height - 2;


            DrawOutlinedText(AggGr, "norme", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "variation", ft, fontsize, Firstwidth + (1 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "norme", ft, fontsize, Firstwidth + (2 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "variation", ft, fontsize, Firstwidth + (3 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            y -= 15;

            #region Etage Supérieur

            string s = "Etage Supérieur";

            float value = ((EtageSup * 100) - 33);
            if (Math.Round(value) != 0) badorgoodcolor = badcolorGray; else badorgoodcolor = goodcolorGray;
            DrawOutlinedText(AggGr, s, ft, fontsize, 2, y, neutralcolorGray, new Epsitec.Common.Drawing.Color(.5, 1, 1, 1));
            DrawOutlinedText(AggGr, "33%", ft, fontsize, Firstwidth, y, neutralcolorGray, new Epsitec.Common.Drawing.Color(.5, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", value) + "%", ft, fontsize, Firstwidth + (1 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(.5, 1, 1, 1));
           

           
            #endregion
            y -= 15;
            #region Etage Moyen

            s = "Etage Moyen";
            value = ((EtageMoy * 100) - 33);
            if (Math.Round(value) != 0) badorgoodcolor = badcolorGray; else badorgoodcolor = goodcolorGray;
            DrawOutlinedText(AggGr, s, ft, fontsize, 2, y, neutralcolorGray, new Epsitec.Common.Drawing.Color(.5, 1, 1, 1));
            DrawOutlinedText(AggGr, "33%", ft, fontsize, Firstwidth, y, neutralcolorGray, new Epsitec.Common.Drawing.Color(.5, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", value) + "%", ft, fontsize, Firstwidth + (1 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(.5, 1, 1, 1));

            value = ((EtageMoy2 * 100) - 50);
            if (Math.Round(value) != 0) badorgoodcolor = badcolorGray; else badorgoodcolor = goodcolorGray;
            DrawOutlinedText(AggGr, "50%", ft, fontsize, Firstwidth + (2 * Smallwidth), y, neutralcolorGray, new Epsitec.Common.Drawing.Color(.5, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", value) + "%", ft, fontsize, Firstwidth + (3 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(.5, 1, 1, 1));
            
            #endregion
            y -= 15;
            #region Etage Inférieur

            s = "Etage Inférieur";
            value = ((EtageInf * 100) - 33);
            if (Math.Round(value) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, s, ft, fontsize, 2, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "33%", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", value) + "%", ft, fontsize, Firstwidth + (1 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));

            value = ((EtageInf2 * 100) - 50);
            if (Math.Round(value) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, "50%", ft, fontsize, Firstwidth + (2 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", value) + "%", ft, fontsize, Firstwidth + (3 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            

            #endregion
            y -= 15;
            #region Etage maxillaire

            s = "   ->Etage maxillaire";
            value = ((EtageInfSup * 100) - 33);
            if (Math.Round(value) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, s, ft, fontsize, 2, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "33%", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", value) + "%", ft, fontsize, Firstwidth + (1 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));

           
            #endregion
            y -= 15;
            #region Etage mandibulaire


            s = "   ->Etage mandibulaire";
            value = ((EtageInfInf * 100) - 66);
            if (Math.Round(value) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, s, ft, fontsize, 2, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "66%", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", value) + "%", ft, fontsize, Firstwidth + (1 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));

           

         
            #endregion
            y -= 15;

            s = "";
            if (DeviationLevreInf < -.03)
                s = "Déviation Levre Inferieur Gauche";
            if (DeviationLevreInf > .03)
                s = "Déviation Levre Inferieur Droite";
            DrawOutlinedText(AggGr, s, ft, fontsize, 2, y, new Epsitec.Common.Drawing.Color(1, 0, 0, 0), new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            y -= 15;

            s = "";
            if (DeviationMenton < -.03)
                s = "Déviation Menton Gauche";
            if (DeviationMenton > .03)
                s = "Déviation Menton Droite";            
            DrawOutlinedText(AggGr, s, ft, fontsize, 2, y, new Epsitec.Common.Drawing.Color(1, 0, 0, 0), new Epsitec.Common.Drawing.Color(1, 1, 1, 1));

        }
        

        public PointF[] ConvertListOfPointToArray()
        {
            PointF[] ptarray = new PointF[_ListOfPoints.Count];
            for (int i=0;i<_ListOfPoints.Count;i++)
            {
                ptarray[i] = new PointF(_ListOfPoints[i].Pt.X,_ListOfPoints[i].Pt.Y);
            }

            return ptarray;
        }


        public void ReinitRotationAuto() {

            if (ListOfPoints[FaceSup].visible &&
                    ListOfPoints[SousNasal].visible)
            {
                float angle = BASEDiagAdulte.Ctrls.GeomTools.AngleOfView(ListOfPoints[FaceSup].Pt,
                                                                   ListOfPoints[SousNasal].Pt,
                                                                   new Point(ListOfPoints[FaceSup].Pt.X, -2000));

                AngleDeRotationRadio = -angle;
            }
        
        }

        public void Recalculate()
        {
            /*
             _ListOfPoints.Add(VisageSup);
            _ListOfPoints.Add(FaceSup);
            _ListOfPoints.Add(SsNasal);
            _ListOfPoints.Add(ContactLabial);
            _ListOfPoints.Add(LevreInf);
            _ListOfPoints.Add(Menton);
            _ListOfPoints.Add(PointExterneDroit);
            _ListOfPoints.Add(PointExterneGauche);
             */

            PointF[] pts = ConvertListOfPointToArray();

            EtageSup = (float)(pts[FaceSup].Y - pts[VisageSup].Y) / (pts[Menton].Y - pts[VisageSup].Y);
            EtageMoy = (float)(pts[SousNasal].Y - pts[FaceSup].Y) / (pts[Menton].Y - pts[VisageSup].Y);
            EtageInf = (float)(pts[Menton].Y - pts[SousNasal].Y) / (pts[Menton].Y - pts[VisageSup].Y);

            EtageMoy2 = (float)(pts[SousNasal].Y - pts[FaceSup].Y) / (pts[Menton].Y - pts[FaceSup].Y);
            EtageInf2 = (float)(pts[Menton].Y - pts[SousNasal].Y) / (pts[Menton].Y - pts[FaceSup].Y);

            EtageInfInf = (float)(pts[Menton].Y - pts[ContactLabial].Y) / (pts[Menton].Y - pts[SousNasal].Y);
            EtageInfSup = 1f - EtageInfInf;


            Droite dRef = GeomTools.FindDroiteOf(pts[SousNasal], pts[FaceSup]);
            Droite dLevre = GeomTools.FindDroiteOf(pts[LevreInferieur], new PointF(pts[LevreInferieur].X + 10, pts[LevreInferieur].Y));
            Droite dMenton = GeomTools.FindDroiteOf(pts[Menton], new PointF(pts[Menton].X + 10, pts[Menton].Y));


            PointF IntersectLevreRef = dRef.GetIntersectionWith(dLevre);
            PointF IntersectMentonRef = dRef.GetIntersectionWith(dMenton);


            DeviationLevreInf = (IntersectLevreRef.X - _ListOfPoints[LevreInferieur].Pt.X) / (_ListOfPoints[ExterneGauche].Pt.X - _ListOfPoints[ExterneDroit].Pt.X);
            DeviationMenton = (IntersectMentonRef.X - _ListOfPoints[Menton].Pt.X) / (_ListOfPoints[ExterneGauche].Pt.X - _ListOfPoints[ExterneDroit].Pt.X);




        }




        public void DrawExtraForms(Epsitec.Common.Drawing.Graphics gr, bool ForImpression)
        {
            
            if (currentmode == ImageCtrlAgg.ModeSaisie.None)
            {

                PointF[] pts = ConvertListOfPointToArray();

                /*
                Point[] pts = new Point[8]{ PointExterneDroit.Pt,
                                            VisageSup.Pt,
                                            PointExterneGauche.Pt,
                                            Menton.Pt,
                                            LevreInf.Pt,
                                            ContactLabial.Pt,
                                            SsNasal.Pt,
                                            FaceSup.Pt};
                */
                pts = TransformPtRadioToScreen(pts);
                


                Epsitec.Common.Drawing.Path pth = new Epsitec.Common.Drawing.Path();

                //Masque Facial
                pth.MoveTo(pts[ExterneDroit].X, pts[VisageSup].Y);
                pth.LineTo(pts[ExterneDroit].X, pts[Menton].Y);
                pth.LineTo(pts[ExterneGauche].X, pts[Menton].Y);
                pth.LineTo(pts[ExterneGauche].X, pts[VisageSup].Y);
                pth.Close();

                //FaceSup
                pth.MoveTo(pts[ExterneDroit].X, pts[FaceSup].Y);
                pth.LineTo(pts[ExterneGauche].X, pts[FaceSup].Y);

                //SsNasal
                pth.MoveTo(pts[ExterneDroit].X, pts[SousNasal].Y);
                pth.LineTo(pts[ExterneGauche].X, pts[SousNasal].Y);

                //ContactLabial
                pth.MoveTo(pts[ExterneDroit].X, pts[ContactLabial].Y);
                pth.LineTo(pts[ExterneGauche].X, pts[ContactLabial].Y);


                Droite dRef = GeomTools.FindDroiteOf(pts[SousNasal], pts[FaceSup]);


                int ymin = (int)dRef.GetY(0);
                int ymax = (int)dRef.GetY(2000);


                if ((dRef.IsVertical) || (ymin > 25000) || (ymin < -25000) || (ymax > 25000) || (ymax < -25000))
                {
                    pth.MoveTo(pts[FaceSup].X, 0);
                    pth.LineTo(pts[FaceSup].X, 2000);
                }
                else
                {                   

                    pth.MoveTo(0, ymin);
                    pth.LineTo(2000, ymax);

                }

              


                gr.Rasterizer.AddOutline(pth, 4);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
                gr.RenderSolid();

                gr.Rasterizer.AddOutline(pth, 2);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 0, 0.5, 0);
                gr.RenderSolid();


                /*
                
                
                Droite dRef = GeomTools.FindDroiteOf(pts[6], pts[7]);





                int ymin = (int)dRef.GetY(0);
                int ymax = (int)dRef.GetY(2000);

               

                try
                {
                    gr.DrawLine(Pens.White, new Point(0, ymin), new Point(2000, ymax));
                }
                catch (System.OverflowException)
                {
                    gr.DrawLine(Pens.White, new Point(pts[6].X, 0), new Point(pts[6].X, 2000));
                }
                            
                */

            }
        
        }

        public Analyse1()
        {

            _ListOfPoints.Add(new PointToTake("Visage Sup"));
            _ListOfPoints.Add(new PointToTake("Face Sup"));
            _ListOfPoints.Add(new PointToTake("Sous Nasal"));
            _ListOfPoints.Add(new PointToTake("Contact labial"));
            _ListOfPoints.Add(new PointToTake("Levre inférieure"));
            _ListOfPoints.Add(new PointToTake("Menton"));
            _ListOfPoints.Add(new PointToTake("Externe Droit"));
            _ListOfPoints.Add(new PointToTake("Externe Gauche"));

            ExterneGauche = 7;
            ExterneDroit = 6;
            Menton = 5;
            LevreInferieur = 4;
            ContactLabial = 3;
            SousNasal = 2;
            FaceSup = 1;
            VisageSup = 0;

            InitializeComponent();
        }

        private void Analyse1_Load(object sender, EventArgs e)
        {

        }
    }
}
