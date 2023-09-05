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
    public partial class AnOccDroit : ImageCtrlAgg, IAnalyse
    {


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



        public int Dent13 = 1;
        public int PtContact4344 = 2;
        public int MolSup = 3;
        public int MolInf = 4;
        
            

        #endregion

        int DeltaDEviation = 5;
        public double DeviationCanine = 0;
        public double DeviationMolaire = 0;

        public string Resultat
        {
            get
            {
                string s = "";
                if (Math.Abs(DeviationCanine) < DeltaDEviation) s = "Classe I can";
                else
                    s = DeviationCanine > 0 ? "Classe III can" : "Classe II can";


                if (Math.Abs(DeviationMolaire) < DeltaDEviation) 
                    s += " Classe I mol";
                else
                    s += DeviationMolaire > 0 ? " Classe III mol" : " Classe II mol";
                                
                return s;
            }
           // set
           // {
           //     Resultat = "";
//}
           
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


            try
            {
                DeviationCanine = pts[PtContact4344].X - pts[Dent13].X;
            }
            catch (System.Exception) {
                DeviationCanine = 0;
            
            }

            try
            {
                DeviationMolaire = pts[MolInf].X - pts[MolSup].X;
            }
            catch (System.Exception)
            {
                DeviationMolaire = 0;

            }

            if (resumeclinique != null)
            {
                if (Math.Abs(DeviationCanine) < DeltaDEviation)
                    resumeclinique.ClasseCanD = BasCommon_BO.EntentePrealable.en_Class.Class_I;
                else
                    if (DeviationCanine > 0)
                        resumeclinique.ClasseCanD = BasCommon_BO.EntentePrealable.en_Class.Class_III;
                    else
                        resumeclinique.ClasseCanD = BasCommon_BO.EntentePrealable.en_Class.Class_II;

                if (Math.Abs(DeviationMolaire) < DeltaDEviation)
                    resumeclinique.ClasseMolD = BasCommon_BO.EntentePrealable.en_Class.Class_I;
                else
                    if (DeviationMolaire > 0)
                        resumeclinique.ClasseMolD = BasCommon_BO.EntentePrealable.en_Class.Class_III;
                    else
                        resumeclinique.ClasseMolD = BasCommon_BO.EntentePrealable.en_Class.Class_II;

            }

        }




        public void DrawExtraForms(Epsitec.Common.Drawing.Graphics gr, bool ForImpression)
        {

            PointF[] pts = ConvertListOfPointToArray();

            
            pts = TransformPtRadioToScreen(pts);


            Epsitec.Common.Drawing.Path pth = new Epsitec.Common.Drawing.Path();


            try
            {
                pth.MoveTo(pts[Dent13].X, pts[Dent13].Y);
                pth.LineTo(pts[Dent13].X, pts[Dent13].Y + 500);


                gr.Rasterizer.AddOutline(pth, 1);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
                gr.RenderSolid();
            }
            catch (System.Exception) { }

            try
            {
                pth.Clear();
                pth.MoveTo(pts[PtContact4344].X, pts[PtContact4344].Y);
                pth.LineTo(pts[PtContact4344].X, pts[PtContact4344].Y - 500);


                gr.Rasterizer.AddOutline(pth, 1);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
                gr.RenderSolid();
            }
            catch (System.Exception) { }

            try
            {
                pth.Clear();
                pth.MoveTo(pts[MolInf].X, pts[MolInf].Y);
                pth.LineTo(pts[MolInf].X, pts[MolInf].Y - 500);


                gr.Rasterizer.AddOutline(pth, 1);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
                gr.RenderSolid();
            }
            catch (System.Exception) { }


            try
            {
                pth.Clear();
                pth.MoveTo(pts[MolSup].X, pts[MolSup].Y);
                pth.LineTo(pts[MolSup].X, pts[MolSup].Y + 500);


                gr.Rasterizer.AddOutline(pth, 1);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
                gr.RenderSolid();
            }
            catch (System.Exception)
            { }
        }

        public AnOccDroit()
        {


            _ListOfPoints.Add(new PointToTake("Dent 13"));
            _ListOfPoints.Add(new PointToTake("Point Contact 43/44"));
            _ListOfPoints.Add(new PointToTake("pointe cuspide mesovestibulaire de la 16"));
            _ListOfPoints.Add(new PointToTake("creux entre les 2 cuspides mesovestibulaire de la 46"));

               Dent13 = 0;
               PtContact4344 = 1;
               MolSup = 2;
               MolInf = 3;
            

            InitializeComponent();
        }

        internal void PaintOn(Graphics g, Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        private void AnOccDroit_Load(object sender, EventArgs e)
        {

        }
    }
}
