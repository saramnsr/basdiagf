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
    public partial class AnOccGauche : ImageCtrlAgg, IAnalyse
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

        int DeltaDEviation = 5;


        #region Points


        public int Dent23 = 1;
        public int PtContact3334 = 2;

        public int MolSup = 3;
        public int MolInf = 4;


        #endregion


        public double DeviationCanine = 0;
        public double DeviationMolaire = 0;

        public string Resultat
        {
            get
            {
                string s = "";
                if (Math.Abs(DeviationCanine) < DeltaDEviation) s = "Classe I can";
                else
                    s = DeviationCanine > 0 ? "Classe II can" : "Classe III can";

                if (Math.Abs(DeviationMolaire) < DeltaDEviation) s += " Classe I mol";
                else
                    s += DeviationMolaire < 0 ? " Classe III mol" : " Classe II mol";
                 
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

            pts = TransformPtRadioToScreen(pts);


            try
            {
                DeviationCanine = pts[PtContact3334].X - pts[Dent23].X;
            }
            catch (System.Exception)
            {
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
                    resumeclinique.ClasseCanG = BasCommon_BO.EntentePrealable.en_Class.Class_I;
                else
                    if (DeviationCanine < 0)
                        resumeclinique.ClasseCanG = BasCommon_BO.EntentePrealable.en_Class.Class_III;
                    else
                        resumeclinique.ClasseCanG = BasCommon_BO.EntentePrealable.en_Class.Class_II;


                if (Math.Abs(DeviationMolaire) < DeltaDEviation)
                    resumeclinique.ClasseMolG = BasCommon_BO.EntentePrealable.en_Class.Class_I;
                else
                    if (DeviationMolaire > 0)
                        resumeclinique.ClasseMolG = BasCommon_BO.EntentePrealable.en_Class.Class_II;
                    else
                        resumeclinique.ClasseMolG = BasCommon_BO.EntentePrealable.en_Class.Class_III;

            }


        }




        public void DrawExtraForms(Epsitec.Common.Drawing.Graphics gr, bool ForImpression)
        {
            PointF[] pts = ConvertListOfPointToArray();


            pts = TransformPtRadioToScreen(pts);


            Epsitec.Common.Drawing.Path pth = new Epsitec.Common.Drawing.Path();


            try
            {
                pth.MoveTo(pts[Dent23].X, pts[Dent23].Y);
                pth.LineTo(pts[Dent23].X, pts[Dent23].Y + 500);


                gr.Rasterizer.AddOutline(pth, 1);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
                gr.RenderSolid();
            }
            catch (System.Exception)
            { }

            try
            {
                pth.Clear();
                pth.MoveTo(pts[PtContact3334].X, pts[PtContact3334].Y);
                pth.LineTo(pts[PtContact3334].X, pts[PtContact3334].Y - 500);


                gr.Rasterizer.AddOutline(pth, 1);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
                gr.RenderSolid();
            }
            catch (System.Exception)
            { }

            try
            {
                pth.Clear();
                pth.MoveTo(pts[MolInf].X, pts[MolInf].Y);
                pth.LineTo(pts[MolInf].X, pts[MolInf].Y - 500);


                gr.Rasterizer.AddOutline(pth, 1);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
                gr.RenderSolid();
            }
            catch (System.Exception)
            { }



            try
            {
                pth.Clear();
                pth.MoveTo(pts[MolSup].X, pts[MolSup].Y);
                pth.LineTo(pts[MolSup].X, pts[MolSup].Y + 500);


                gr.Rasterizer.AddOutline(pth, 1);
                gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
                gr.RenderSolid();
            }
            catch (System.Exception) { }

        }

        public AnOccGauche()
        {

            _ListOfPoints.Add(new PointToTake("Dent 23"));
            _ListOfPoints.Add(new PointToTake("Point Contact 33/34"));
            _ListOfPoints.Add(new PointToTake("pointe cuspide mesovestibulaire de la 26"));
            _ListOfPoints.Add(new PointToTake("creux entre les 2 cuspides mesovestibulaire de la 36"));


            Dent23 = 0;
            PtContact3334 = 1;
            MolSup = 2;
            MolInf = 3;

            InitializeComponent();
        }

        private void AnOccGauche_Load(object sender, EventArgs e)
        {

        }
    }
}
