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
    public partial class Analyse2 : ImageCtrlAgg, IAnalyse
    {

        #region Points

        public int FaceSup = 0;
        public int sousnasal = 1;

        public int OeilDroit = 2;
        public int OeilGauche = 3;
        public int CommisureExtDroite = 4;
        public int CommisureExtGauche = 5;
        public int MolaireDroitInf = 6;
        public int MolaireDroitSup = 7;
        public int MolaireGaucheInf = 8;
        public int MolaireGaucheSup = 9;
        public int InterIncisive = 10;

        #endregion

        #region Resultats

       public float EspaceDentaireBuccal = 0;
        public float IncisiveMolaireDroit = 0;
        public float IncisiveMolaireGauche = 0;
        public float AngleIncisiveMolaireDroit = 0;
        public float AngleIncisiveMolaireGauche = 0;

        #endregion

        

        public string GetTextResultat()
        {
            string s = "Espace dentaire/Espace buccal = " + string.Format("{0:0}", EspaceDentaireBuccal * 100) + "%";
            s += "\nInterIncisive/Molaire Droit = " + string.Format("{0:0}", IncisiveMolaireDroit * 100) + "%";
            s += "\nInterIncisive/Molaire Gauche = " + string.Format("{0:0}", IncisiveMolaireGauche * 100) + "%";
            

            return s;
        }

        protected override void DrawTextResult(Epsitec.Common.Drawing.Graphics AggGr)
        {

            int Firstwidth = 200;
            int Smallwidth = 50;

            Epsitec.Common.Drawing.Color neutralcolor = new Epsitec.Common.Drawing.Color(1, 0, 0, 0);
            Epsitec.Common.Drawing.Color badcolor = new Epsitec.Common.Drawing.Color(1, 1, 0, 0);
            Epsitec.Common.Drawing.Color goodcolor = new Epsitec.Common.Drawing.Color(1, 0, 1, 0);
            Epsitec.Common.Drawing.Color badorgoodcolor = goodcolor;
            Epsitec.Common.Drawing.Font ft = Epsitec.Common.Drawing.Font.GetFont("Arial", "Regular");
            double fontsize = 12;

            int y = Height - 2;


            DrawOutlinedText(AggGr, "norme", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "données", ft, fontsize, Firstwidth + (1 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "variation", ft, fontsize, Firstwidth + (2 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            y -= 15;

            
            float value = ((EspaceDentaireBuccal * 100) - 90);
            if (Math.Round(value) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, "Espace dentaire/Espace buccal", ft, fontsize, 2, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "90%", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", EspaceDentaireBuccal * 100) + "%", ft, fontsize, Firstwidth + (1 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", value) + "%", ft, fontsize, Firstwidth + (2 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            y -= 15;

            value = ((IncisiveMolaireDroit * 100) - 90);
            if (Math.Round(value) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, "InterIncisive/Molaire Droit", ft, fontsize, 2, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "90%", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", IncisiveMolaireDroit*100) + "%", ft, fontsize, Firstwidth + (1 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", value) + "%", ft, fontsize, Firstwidth + (2 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            y -= 15;

            value = ((IncisiveMolaireGauche * 100) - 90);
            if (Math.Round(value) != 0) badorgoodcolor = badcolor; else badorgoodcolor = goodcolor;
            DrawOutlinedText(AggGr, "InterIncisive/Molaire Gauche", ft, fontsize, 2, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, "90%", ft, fontsize, Firstwidth, y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", IncisiveMolaireGauche * 100) + "%", ft, fontsize, Firstwidth + (1 * Smallwidth), y, neutralcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            DrawOutlinedText(AggGr, string.Format("{0:0}", value)+"%", ft, fontsize, Firstwidth + (2 * Smallwidth), y, badorgoodcolor, new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
            y -= 15;



           
            
           
        }

        public void ReinitRotationAuto() {

            if (ListOfPoints[FaceSup].visible &&
                    ListOfPoints[sousnasal].visible)
            {
                float angle = BASEDiag.Ctrls.GeomTools.AngleOfView(ListOfPoints[FaceSup].Pt,
                                                                   ListOfPoints[sousnasal].Pt,
                                                                   new Point(ListOfPoints[FaceSup].Pt.X, -2000));

                AngleDeRotationRadio = -angle;
            }
        
        }

        public void Recalculate()
        {

            PointF[] pts = ConvertListOfPointToArray();
            /*
            Point[] pts = new Point[7]{ OeilDroit.Pt,
                                            OeilGauche.Pt,
                                            CommisureExtDroite.Pt,
                                            CommisureExtGauche.Pt,
                                            MolaireDroit.Pt,
                                            MolaireGauche.Pt,
                                            InterIncisive.Pt};
            */
            pts = TransformPtRadioToScreen(pts);

            EspaceDentaireBuccal = (float)(pts[MolaireGaucheSup].X - pts[MolaireDroitSup].X) / (pts[CommisureExtGauche].X - pts[CommisureExtDroite].X);

            IncisiveMolaireDroit = (float)(pts[InterIncisive].X - pts[MolaireDroitSup].X) / (pts[InterIncisive].X - pts[CommisureExtDroite].X);
            IncisiveMolaireGauche = (float)(pts[InterIncisive].X - pts[MolaireGaucheSup].X) / (pts[InterIncisive].X - pts[CommisureExtGauche].X);



            AngleIncisiveMolaireDroit = GeomTools.AngleOfView(pts[MolaireDroitInf], pts[MolaireDroitSup]);
            AngleIncisiveMolaireGauche = 360 - GeomTools.AngleOfView(pts[MolaireGaucheInf], pts[MolaireGaucheSup]);

            if (AngleIncisiveMolaireDroit > 180)
                AngleIncisiveMolaireDroit = AngleIncisiveMolaireDroit - 360;

            if (AngleIncisiveMolaireGauche > 180)
                AngleIncisiveMolaireGauche = AngleIncisiveMolaireGauche - 360;

            /*
            TNLDroit = (float)(pts[InterIncisive].X - pts[MolaireDroit].X) / (pts[InterIncisive].X - pts[CommisureExtDroite].X);
            TNLGauche = (float)(pts[InterIncisive].X - pts[MolaireGauche].X) / (pts[InterIncisive].X - pts[CommisureExtGauche].X);
            */
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

                pth.MoveTo(pts[OeilGauche].X, 0);
                pth.LineTo(pts[OeilGauche].X, 2000);


                pth.MoveTo(pts[OeilDroit].X, 0);
                pth.LineTo(pts[OeilDroit].X, 2000);


                pth.MoveTo(pts[sousnasal].X, 0);
                pth.LineTo(pts[sousnasal].X, 2000);

                gr.Rasterizer.AddOutline(pth, 3);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
                gr.RenderSolid();

                gr.Rasterizer.AddOutline(pth, 1.5);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 0, 0.5, 0);
                gr.RenderSolid();

                Droite d = GeomTools.FindDroiteOf(pts[MolaireDroitInf], pts[MolaireDroitSup]);
                PointF pt = new PointF(d.GetX(pts[MolaireDroitSup].Y + 50), pts[MolaireDroitSup].Y + 50);

                pth.Clear();
                pth.MoveTo(pt.X, pt.Y);
                pth.LineTo(pts[MolaireDroitInf].X, pts[MolaireDroitInf].Y);

                gr.Rasterizer.AddOutline(pth, 1);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 0);
                gr.RenderSolid();

                d = GeomTools.FindDroiteOf(pts[MolaireGaucheInf], pts[MolaireGaucheSup]);
                pt = new PointF(d.GetX(pts[MolaireGaucheSup].Y + 50), pts[MolaireGaucheSup].Y + 50);

                pth.Clear();
                pth.MoveTo(pt.X, pt.Y);
                pth.LineTo(pts[MolaireGaucheInf].X, pts[MolaireGaucheInf].Y);

                gr.Rasterizer.AddOutline(pth, 1);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 0);
                gr.RenderSolid();

            }

        }



        public Analyse2()
        {



            _ListOfPoints.Add(new PointToTake("Face Sup"));
            _ListOfPoints.Add(new PointToTake("Sous Nasal"));
            _ListOfPoints.Add(new PointToTake("Oeil Droit"));
            _ListOfPoints.Add(new PointToTake("Oeil Gauche"));
            _ListOfPoints.Add(new PointToTake("Commissure externe droite"));
            _ListOfPoints.Add(new PointToTake("Commissure externe gauche"));
            _ListOfPoints.Add(new PointToTake("Cuspide PreMolaireSup Droit"));
            _ListOfPoints.Add(new PointToTake("JEC de PreMolaire Sup Droit"));
            _ListOfPoints.Add(new PointToTake("Cuspide PreMolaireSup Gauche"));
            _ListOfPoints.Add(new PointToTake("JEC de PreMolaire Sup Gauche"));
            _ListOfPoints.Add(new PointToTake("InterIncisive"));


            FaceSup = 0;
            sousnasal = 1;

            OeilDroit = 2;
            OeilGauche = 3;
            CommisureExtDroite = 4;
            CommisureExtGauche = 5;
            MolaireDroitInf = 6;
            MolaireDroitSup = 7;
            MolaireGaucheInf = 8;
            MolaireGaucheSup = 9;
            InterIncisive = 10;

            InitializeComponent();
        }
    }
}
