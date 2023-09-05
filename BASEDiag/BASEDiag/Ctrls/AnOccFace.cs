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
    public partial class AnOccFace : ImageCtrlAgg, IAnalyse
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


        public int MilieuHaut = 1;
        public int MilieuBas = 2;


        #endregion


        public double DeviationIncisive = 0;

        public string Resultat
        {
            get
            {
                string s = "";
                if (Math.Abs(DeviationIncisive) < 5) s = "Aucune déviation";
                else
                    s = DeviationIncisive > 0 ? "Deviation Droite" : "Deviation gauche";

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

            DeviationIncisive = pts[MilieuHaut].X - pts[MilieuBas].X;

        


        }




        public void DrawExtraForms(Epsitec.Common.Drawing.Graphics gr, bool ForImpression)
        {

            PointF[] pts = ConvertListOfPointToArray();


            pts = TransformPtRadioToScreen(pts);


            Epsitec.Common.Drawing.Path pth = new Epsitec.Common.Drawing.Path();



            pth.MoveTo(pts[MilieuHaut].X, pts[MilieuHaut].Y);
            pth.LineTo(pts[MilieuHaut].X, pts[MilieuHaut].Y + 500);


            gr.Rasterizer.AddOutline(pth, 1);
            gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
            gr.RenderSolid();

            pth.Clear();
            pth.MoveTo(pts[MilieuBas].X, pts[MilieuBas].Y);
            pth.LineTo(pts[MilieuBas].X, pts[MilieuBas].Y - 500);


            gr.Rasterizer.AddOutline(pth, 1);
            gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
            gr.RenderSolid();           
        
        }

        public AnOccFace()
        {

            _ListOfPoints.Add(new PointToTake("Milieu Haut"));
            _ListOfPoints.Add(new PointToTake("Milieu Bas"));


            MilieuHaut = 0;
            MilieuBas = 1;


            InitializeComponent();
        }
    }
}
