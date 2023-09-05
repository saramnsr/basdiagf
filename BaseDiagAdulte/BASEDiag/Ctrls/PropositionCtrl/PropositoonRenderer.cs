using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using BASEDiag_BL;
using BASEDiag_BO;

namespace BASEDiag.Ctrls
{
    public class PropositoonRenderer : AbstractPropositoonRenderer
    {

        public override Size MeasureProposition(Graphics p_graphics)
        {
            return new Size(200,25);
        }

        public override void DrawProposition(Graphics p_graphics, PropositionCell pc)
        {

            Font ft = new Font("Calibri", 8, FontStyle.Regular);


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            StringFormat sftarif = new StringFormat();
            sftarif.Alignment = StringAlignment.Far;
            sftarif.LineAlignment = StringAlignment.Center;


            if (pc.State == PropositionCell.Status.Selected)
                p_graphics.FillRectangle(Brushes.LightGray, pc.Bound);
            else
                p_graphics.FillRectangle(Brushes.White, pc.Bound);

            p_graphics.DrawRectangle(Pens.Black, pc.Bound);
            p_graphics.DrawString(pc.proposition.libelle, ft, Brushes.Black, pc.Bound);


            Rectangle rectTarif = new Rectangle(pc.Bound.X, pc.Bound.Y, pc.Bound.Width, pc.Bound.Height);
            string tarif = pc.proposition.TarifTotal.ToString("C2") + "(" + pc.proposition.PartSecu.ToString("C2") + ")";
            p_graphics.DrawString(tarif, ft, Brushes.Black, rectTarif, sftarif);
        }

        public override Size MeasureTraitement(Graphics p_graphics)
        {
            return new Size(550, 25);
        }

        public override void DrawTraitement(Graphics p_graphics, TraitementCell tc)
        {

            Font ft = new Font("Calibri", 8, FontStyle.Regular);


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            StringFormat sftarif = new StringFormat();
            sftarif.Alignment = StringAlignment.Far;

            sftarif.LineAlignment = StringAlignment.Center;
            if (tc.State == TraitementCell.Status.Selected)
                p_graphics.FillRectangle(Brushes.LightGray, tc.Bound);
            else
                p_graphics.FillRectangle(Brushes.White, tc.Bound);

            p_graphics.DrawRectangle(Pens.Black, tc.Bound);

            string txt = tc.traitement.traitementSecu.Libelle;
            if (tc.traitement.appareil != null) txt += "(" + tc.traitement.appareil.Libelle + ")";
            p_graphics.DrawString(txt, ft, Brushes.Black, tc.Bound);

            Rectangle rectTarif = new Rectangle(tc.Bound.X, tc.Bound.Y, 300, tc.Bound.Height);
            string tarif = tc.traitement.TarifTotal.ToString("C2") + "(" + tc.traitement.PartSecu.ToString("C2") + ")";
            p_graphics.DrawString(tarif, ft, Brushes.Black, rectTarif, sftarif);


            //Semestre

            for (int i = 1; i < 10; i++)
            {

                Rectangle SemRect = new Rectangle(300 + tc.Bound.X + (i * tc.Bound.Height), tc.Bound.Y, tc.Bound.Height, tc.Bound.Height);
                SemRect.Inflate(-3, -3);

                if (i <= tc.traitement.patient.infoscomplementaire.NbSemestresEntame)
                    p_graphics.FillRectangle(Brushes.Gray, SemRect);


                if ((i > tc.traitement.NumSemestres.Max()) && (tc.traitement.NumSemestres.Max() + tc.traitement.NbSurveillance >= i))
                    p_graphics.FillRectangle(Brushes.LightGreen, SemRect);

                if ((tc.traitement.traitementSecu.phase == TemplateActePG.EnumPhase.Pédiatrique) &&
                    (tc.traitement.NumSemestres.Contains(i)))
                    p_graphics.FillRectangle(Brushes.LightCoral, SemRect);

                if ((tc.traitement.traitementSecu.phase == TemplateActePG.EnumPhase.Orthopedique) &&
                    (tc.traitement.NumSemestres.Contains(i)))
                    p_graphics.FillRectangle(Brushes.LightBlue, SemRect);

                if ((tc.traitement.traitementSecu.phase == TemplateActePG.EnumPhase.Orthodontique) &&
                                            (tc.traitement.NumSemestres.Contains(i)))
                    p_graphics.FillRectangle(Brushes.LightBlue, SemRect);

                if ((tc.traitement.traitementSecu.phase == TemplateActePG.EnumPhase.Contention) &&
                                            (tc.traitement.NumSemestres.Contains(i)))
                    p_graphics.FillRectangle(Brushes.LightSalmon, SemRect);

                p_graphics.DrawRectangle(Pens.Black, SemRect);
                p_graphics.DrawString("S" + i.ToString(), ft, Brushes.Black, SemRect, sf);
            }

        }
        
    }
}
