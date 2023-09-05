using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using BASEDiag_BO;
using BASEDiag_BL;

namespace BASEDiag.Ctrls.PropositionCtrlV2
{
    public class StandardPropCtrlV2Renderer:AbstractPropCtrlV2Renderer
    {

        Color[] Traitementcolorarray = new Color[10];

        public StandardPropCtrlV2Renderer()
        {
            Traitementcolorarray[0] = Color.FromArgb(30,Color.Red);
            Traitementcolorarray[1] = Color.FromArgb(30,Color.Bisque);
            Traitementcolorarray[2] = Color.FromArgb(30,Color.Blue);
            Traitementcolorarray[3] = Color.FromArgb(30,Color.BlueViolet);
            Traitementcolorarray[4] = Color.FromArgb(30,Color.Green);
            Traitementcolorarray[5] = Color.FromArgb(30,Color.Gray);
            Traitementcolorarray[6] = Color.FromArgb(30,Color.Goldenrod);
            Traitementcolorarray[7] = Color.FromArgb(30,Color.GhostWhite);
            Traitementcolorarray[8] = Color.FromArgb(30,Color.GreenYellow);
            Traitementcolorarray[8] = Color.FromArgb(30, Color.Gold);

        }

        public override void DrawProposition(Graphics g, PropositionRow row)
        {
            Font ft = new Font("Segoe UI", 8, FontStyle.Regular);
            Rectangle rectTarif = new Rectangle(row.Bounds.X, row.Bounds.Y, row.Bounds.Width, row.Bounds.Height);
            
            StringFormat sftarif = new StringFormat();
            sftarif.Alignment = StringAlignment.Center;
            sftarif.LineAlignment = StringAlignment.Near;
            Brush b = Brushes.Black;

            //string txt = PropositionMgmt.GetTotal(row.proposition).ToString("C2") + "(" + PropositionMgmt.GetPartSecu(row.proposition).ToString("C2") + ")";
            string txt = PropositionMgmt.GetSmoothedTarif(row.proposition) + "\nPart Secu : " + PropositionMgmt.GetPartSecu(row.proposition).ToString("C2");

            if (row.proposition.Etat == Proposition.EtatProposition.Refusé)
            {

                if (row.State == PropositionRow.Status.Selected)
                {
                    g.FillRectangle(SystemBrushes.Highlight, row.Bounds);
                    b = new SolidBrush(Color.White);
                }
                else
                {
                    g.FillRectangle(Brushes.LightGray, row.Bounds);
                    b = new SolidBrush(Color.Gray);
                }

                g.DrawRectangle(Pens.Gray, row.Bounds);
                txt += " [Refusé]";
                                
            }else
                if (row.proposition.Etat == Proposition.EtatProposition.Accepté)
            {

                if (row.State == PropositionRow.Status.Selected)
                {
                    g.FillRectangle(SystemBrushes.Highlight, row.Bounds);
                    b = new SolidBrush(Color.White);
                }
                else
                {
                    g.FillRectangle(Brushes.LightGreen, row.Bounds);
                    b = new SolidBrush(Color.Green);
                }

                g.DrawRectangle(Pens.Black, row.Bounds);
                txt += " [Accepté]";
              
            }else
            {

                if (row.State == PropositionRow.Status.Selected)
                {
                    g.FillRectangle(SystemBrushes.Highlight, row.Bounds);
                    b = new SolidBrush(Color.White);
                }
                else
                {
                    g.FillRectangle(Brushes.WhiteSmoke, row.Bounds);
                    b = new SolidBrush(Color.Black);
                }

                g.DrawRectangle(Pens.Black, row.Bounds);

               
            }

            g.DrawString(txt, ft, b, rectTarif, sftarif);

        }
        
        public override void DrawSemestreCell(Graphics g, SemestreCell cell, PropositionRow row)
        {

            string appareillages = "";
            string ActeGestion = "";
            int NbSurveillance = 0;

            Brush b = Brushes.Black;
            Brush fillb = Brushes.WhiteSmoke;
            Pen contourp = Pens.Black;

            foreach (PoseAppareil papp in row.proposition.poseAppareils)
            {
                foreach (Semestre s in papp.semestres)
                {
                    if (s.NumSemestre == cell.Index)
                    {
                        if (appareillages != "")
                            appareillages += "\n";
                        appareillages += papp.appareil.Libelle;
                    }
                }
            }


            foreach (Traitement t in row.proposition.traitements)
            {
                

                foreach (Semestre s in t.semestres)
                {
                    if (s.NumSemestre == cell.Index)
                    {


                        if (ActeGestion != "")
                            ActeGestion += "\n";

                        ActeGestion += SemestreMgmt.getTotal(s).ToString("C2") + " (" + s.traitementSecu.DisplayCodeNVal + ")";

                        NbSurveillance += s.NbSurveillance;

                    }

                }

            }


            Font ft = new Font("Segoe UI", 8, FontStyle.Regular);



            if (row.proposition.Etat == Proposition.EtatProposition.Accepté)
            {
                b = Brushes.Black;
                fillb = Brushes.WhiteSmoke;
                contourp = Pens.Black;
            }

            if (row.proposition.Etat == Proposition.EtatProposition.Refusé)
            {
                b = Brushes.Gray;
                fillb = Brushes.LightGray;
                contourp = Pens.Gray;
            }
            
            if (cell.State == SemestreCell.Status.Selected)
            {
                b = Brushes.White;
                fillb = SystemBrushes.Highlight;
                contourp = Pens.Black;
            }




            g.FillRectangle(fillb, cell.Bounds);

            g.DrawRectangle(contourp, cell.Bounds);

            string txt = "S" + cell.Index.ToString();
            if (NbSurveillance > 0)
                txt += "\n+" + NbSurveillance.ToString() + " Survs";
            SizeF sz = g.MeasureString(txt, ft);
            Point position = new Point(cell.Bounds.Right - (int)sz.Width, cell.Bounds.Y);
            g.DrawString(txt, ft, b, position);


            txt = ActeGestion;
            sz = g.MeasureString(txt, ft);
            position = new Point(cell.Bounds.Right - (int)sz.Width, cell.Bounds.Bottom - (int)sz.Height);
            g.DrawString(txt, ft, b, position);

            g.DrawString(appareillages, ft, b, cell.Bounds.Location);

        }
        public override void DrawTraitement(Graphics g, TraitementCell cell, PropositionRow row)
        {

            Brush b = Brushes.Black;
            Pen contourp = Pens.Black;


            if (row.proposition.Etat == Proposition.EtatProposition.Accepté)
            {
                b = Brushes.Black;
                contourp = Pens.Black;
            }

            if (row.proposition.Etat == Proposition.EtatProposition.Refusé)
            {
                b = Brushes.Gray;
                contourp = Pens.Gray;
            }

           

            Font ft = new Font("Segoe UI", 8, FontStyle.Regular);

            g.FillRectangle(Brushes.White, cell.Bounds);
            g.FillRectangle(new SolidBrush(Traitementcolorarray[cell.Index]), cell.Bounds);
            g.DrawRectangle(contourp, cell.Bounds);
            g.DrawString(cell.traitement.Libelle, ft, b, cell.Bounds.Location);
        }

    }
}
