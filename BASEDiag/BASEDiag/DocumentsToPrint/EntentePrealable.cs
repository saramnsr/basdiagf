using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using BASEDiag_BO;
using BasCommon_BO;

namespace BASEDiag.DocumentsToPrint
{
    public class EntentePrealableDocument : PrintDocument
    {


        private bool _RectoVerso = true;
        public bool RectoVerso
        {
            get
            {
                return _RectoVerso;
            }
            set
            {
                _RectoVerso = value;
            }
        }

        private Rectangle _HardMarginRect;
        public Rectangle HardMarginRect
        {
            get
            {
                return _HardMarginRect;
            }
            set
            {
                _HardMarginRect = value;
            }
        }

        private bool _ShowBackground;
        public bool ShowBackground
        {
            get
            {
                return _ShowBackground;
            }
            set
            {
                _ShowBackground = value;
            }
        }

        private EntentePrealable _CurrentEntente;
        public EntentePrealable CurrentEntente
        {
            get
            {
                return _CurrentEntente;
            }
            set
            {
                _CurrentEntente = value;
            }
        }

        public EntentePrealableDocument(EntentePrealable ep, bool ShowBackground)
        {
            CurrentEntente = ep;
            _ShowBackground = ShowBackground;
        }

        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {

            base.OnQueryPageSettings(e);
            e.PageSettings.Margins = new Margins(0, 0, 0, 0);
            
            
        }

        void DrawTick(Graphics g, PointF pt)
        {
            int WIDTH = 5;
            Pen p = new Pen(Brushes.Black, 2);
            g.DrawLine(p, new PointF(pt.X - WIDTH, pt.Y - WIDTH), new PointF(pt.X + WIDTH, pt.Y + WIDTH));
            g.DrawLine(p, new PointF(pt.X + WIDTH, pt.Y - WIDTH), new PointF(pt.X - WIDTH, pt.Y + WIDTH));
        }

        PointF ConvertmmToPxl(PointF Pt_En_mm, PrinterResolution res)
        {

            //829-1169
            //210-297

            Pt_En_mm = new PointF(Pt_En_mm.X-1,Pt_En_mm.Y-1.5f);


            PointF AdjustementPt = new PointF(HardMarginRect.X, HardMarginRect.Y);

            float x = (829f / 210f) * Pt_En_mm.X;
            float y = (1169f / 297f) * Pt_En_mm.Y;


            /*
            //((value/10) * (96f / 2.54f));
            //96DPI 96pxl=1Inch 96pxl=25,4mm  3,78pxl = 1mm
            //600DPI 600pxl=1Inch 600pxl=25,4mm  23,62pxl = 1mm
            float x = (Pt_En_mm.X * (res.X / 25.4f));
            float y = (Pt_En_mm.Y * (res.Y / 25.4f));
            */
            return new PointF(x + AdjustementPt.X, y + AdjustementPt.Y);

        }

        bool isrecto = false;
        protected override void OnPrintPage(PrintPageEventArgs e)
        {

            PrinterBounds objBounds = new PrinterBounds(e);
            HardMarginRect = objBounds.Bounds;  // Get the REAL Margin Bounds !

            if (RectoVerso)
            {

                if (!isrecto)
                {
                    NormalPrintVerso(e);
                    isrecto = true;
                    e.HasMorePages = true;
                }
                else
                {
                    NormalPrintRecto(e);

                }
            }
            else
            {
                if (!isrecto)
                {
                    NormalPrintVerso(e);                    
                }
                else
                {
                    NormalPrintRecto(e);
                }
            }
        }

        private void TestPrintVerso(PrintPageEventArgs e)
        {
            //PrinterResolution resolution = e.PageSettings.PrinterResolution;
            PrinterResolution resolution = new PrinterResolution();
            resolution.X = 96;
            resolution.Y = 96;


            Font ft = new Font("Arial", 12, FontStyle.Regular);
            Brush br = Brushes.Black;

            e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.DEPVerso, e.PageBounds);


            e.Graphics.DrawString("ABBCCDDEEEFFFGG", ft, br, ConvertmmToPxl(new PointF(45, 18), resolution));
            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(34, 22), resolution));
            e.Graphics.DrawString("TESTMEGALONG Arthur", ft, br, ConvertmmToPxl(new PointF(26, 27), resolution));
            e.Graphics.DrawString("5 rue de l'ormeteau\n1°Etage\n67000 STRASBOURG", ft, br, ConvertmmToPxl(new PointF(26, 31), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(8, 58), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(8, 63), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(8, 67), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(33, 67), resolution));
                e.Graphics.DrawString("Autre cas", ft, br, ConvertmmToPxl(new PointF(67, 64), resolution));

           DrawTick(e.Graphics, ConvertmmToPxl(new PointF(64, 58), resolution));


           e.Graphics.DrawString("01/01/01", ft, br, ConvertmmToPxl(new PointF(62, 61), resolution));


            e.Graphics.DrawString("Informaticien", ft, br, ConvertmmToPxl(new PointF(166, 18), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(138, 29), resolution));
            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(163, 26), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(189, 29), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(189, 37), resolution));


            
            e.Graphics.DrawString("TESTMEGALONG", ft, br, ConvertmmToPxl(new PointF(122, 43), resolution));
                e.Graphics.DrawString("Arthur", ft, br, ConvertmmToPxl(new PointF(122, 48), resolution));
                e.Graphics.DrawString(DateTime.Now.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(184, 48), resolution));

               
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(159, 58), resolution));
                
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(190, 58), resolution));

                e.Graphics.DrawString("5 rue de l'ormeteau\n1°Etage 67000 STRASBOURG", ft, br, ConvertmmToPxl(new PointF(106, 65), resolution));



            
        }
        
        private void NormalPrintVerso(PrintPageEventArgs e)
        {
            //PrinterResolution resolution = e.PageSettings.PrinterResolution;
            PrinterResolution resolution = new PrinterResolution();
            resolution.X = 96;
            resolution.Y = 96;


            Font ft = new Font("Arial", 8, FontStyle.Regular);
            Brush br = Brushes.Black;

            if (ShowBackground)
                e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.DEPVerso, e.PageBounds);


            e.Graphics.DrawString(CurrentEntente.ImmatAssure, ft, br, ConvertmmToPxl(new PointF(45, 18), resolution));
            if (CurrentEntente.DatenaissanceAssure!= DateTime.MinValue)
                e.Graphics.DrawString(CurrentEntente.DatenaissanceAssure.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(34, 22), resolution));
            e.Graphics.DrawString(CurrentEntente.NomPrenomAssure.ToString(), ft, br, ConvertmmToPxl(new PointF(26, 27), resolution));
            e.Graphics.DrawString(CurrentEntente.AdresseAssure, ft, br, ConvertmmToPxl(new PointF(26, 31), resolution));



            if (CurrentEntente.Salarie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(8, 58), resolution));
            if (CurrentEntente.SansEmplois)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(8, 63), resolution));
            if (CurrentEntente.PensionAssure)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(8, 67), resolution));
            if (CurrentEntente.AutreCas)
            {
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(33, 67), resolution));
                e.Graphics.DrawString(CurrentEntente.LibAutreCas, ft, br, ConvertmmToPxl(new PointF(67, 64), resolution));

            }
            if (CurrentEntente.NonSalarie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(64, 58), resolution));



            e.Graphics.DrawString(CurrentEntente.DateDeCessationActivite.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(62, 61), resolution));


            e.Graphics.DrawString(CurrentEntente.Profession, ft, br, ConvertmmToPxl(new PointF(166, 18), resolution));


            
            if (CurrentEntente.Accident)
            {
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(138, 29), resolution));
                e.Graphics.DrawString(CurrentEntente.DateAccident.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(163, 26), resolution));
            }
            else
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(189, 29), resolution));

            if (CurrentEntente.Pensionne)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(189, 34), resolution));


            

            if (CurrentEntente.NomPrenomAssure != CurrentEntente.NomPrenom)
            {
                e.Graphics.DrawString(CurrentEntente.patient.Nom, ft, br, ConvertmmToPxl(new PointF(122, 43), resolution));
                e.Graphics.DrawString(CurrentEntente.patient.Prenom, ft, br, ConvertmmToPxl(new PointF(122, 48), resolution));
                e.Graphics.DrawString(CurrentEntente.patient.DateNaissance.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(184, 48), resolution));

                if (CurrentEntente.PensionPatient)
                {
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(159, 58), resolution));
                }
                else
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(190, 58), resolution));

                e.Graphics.DrawString(CurrentEntente.AdressePatient, ft, br, ConvertmmToPxl(new PointF(106, 65), resolution));



            }
        }


        private void TestPrintRecto(PrintPageEventArgs e)
        {
            //PrinterResolution resolution = e.PageSettings.PrinterResolution;
            PrinterResolution resolution = new PrinterResolution();
            resolution.X = 96;
            resolution.Y = 96;

            Font ft = new Font("Arial", 12, FontStyle.Regular);
            Brush br = Brushes.Black;

            e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.DEPRecto, e.PageBounds);

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(140, 72), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(178, 72), resolution));


            //PointF ptf = ConvertmmToPxl(new PointF(210,297),resolution);
            //e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, (int)ptf.X, (int)ptf.Y));

            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(73, 77), resolution));
            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(73, 86), resolution));
            e.Graphics.DrawString("TO90", ft, br, ConvertmmToPxl(new PointF(73, 94), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(106, 105), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(156, 105), resolution));


            e.Graphics.DrawString("TESTMEGALONG", ft, br, ConvertmmToPxl(new PointF(73, 158), resolution));
            e.Graphics.DrawString("09/09/1980", ft, br, ConvertmmToPxl(new PointF(52, 162), resolution));
            e.Graphics.DrawString("ABBCCDDEEEFFFGG", ft, br, ConvertmmToPxl(new PointF(126, 162), resolution));








            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(11, 181), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(79, 181), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(153, 181), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(11, 186), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(79, 186), resolution));



            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(46, 202), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(66, 202), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(84, 202), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(107, 202), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(132, 202), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(153, 202), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 202), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(193, 202), resolution));


            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(46, 206), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(66, 206), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(84, 206), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(107, 206), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(132, 206), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(153, 206), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 206), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(193, 206), resolution));


            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(46, 210), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(84, 210), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(132, 210), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 210), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(43, 219), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(56, 219), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(69, 219), resolution));

            e.Graphics.DrawString("Partiel", ft, br, ConvertmmToPxl(new PointF(80, 219), resolution));


            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(145, 219), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(158, 219), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 219), resolution));

            e.Graphics.DrawString("Partiel", ft, br, ConvertmmToPxl(new PointF(182, 219), resolution));


            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(10, 223), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(125, 223), resolution));

            e.Graphics.DrawString("CurrentEntente.Agenesie", ft, br, ConvertmmToPxl(new PointF(28, 225), resolution));
            e.Graphics.DrawString("CurrentEntente.DentsIncluseSurnum", ft, br, ConvertmmToPxl(new PointF(104, 225), resolution));
            e.Graphics.DrawString("CurrentEntente.Malposition", ft, br, ConvertmmToPxl(new PointF(168, 225), resolution));


            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(62, 232), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(85, 232), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(110, 232), resolution));

            e.Graphics.DrawString("Petit test pour le champ \"Facteur fonctionnel\"", ft, br, ConvertmmToPxl(new PointF(58, 238), resolution));

            e.Graphics.DrawString("Ligne 1", ft, br, ConvertmmToPxl(new PointF(10, 250), resolution));
            e.Graphics.DrawString("Ligne 2", ft, br, ConvertmmToPxl(new PointF(10, 255), resolution));
            e.Graphics.DrawString("Ligne 3", ft, br, ConvertmmToPxl(new PointF(10, 260), resolution));

            e.Graphics.DrawString("Ligne 11", ft, br, ConvertmmToPxl(new PointF(10, 272), resolution));
            e.Graphics.DrawString("Ligne 12", ft, br, ConvertmmToPxl(new PointF(10, 277), resolution));


            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(150, 285), resolution));
        }

        private void NormalPrintRecto(PrintPageEventArgs e)
        {
            //PrinterResolution resolution = e.PageSettings.PrinterResolution;
            PrinterResolution resolution = new PrinterResolution();
            resolution.X = 96;
            resolution.Y = 96;

            Font ft = new Font("Arial", 8, FontStyle.Regular);
            Brush br = Brushes.Black;

            if (_ShowBackground)
                e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.DEPRecto, e.PageBounds);

            if (CurrentEntente.ReferenceNationalOpposable == EntentePrealable.RNO.R)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(140, 72), resolution));
            if (CurrentEntente.ReferenceNationalOpposable == EntentePrealable.RNO.HR)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(178, 72), resolution));


            //PointF ptf = ConvertmmToPxl(new PointF(210,297),resolution);
            //e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, (int)ptf.X, (int)ptf.Y));

            e.Graphics.DrawString(CurrentEntente.dateProposition.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(73, 77), resolution));
            e.Graphics.DrawString(CurrentEntente.DateDebutTraitement.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(73, 86), resolution));
            e.Graphics.DrawString(CurrentEntente.cotationDesActes, ft, br, ConvertmmToPxl(new PointF(73, 94), resolution));

            if (CurrentEntente.IsDevisSigned)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(106, 105), resolution));
            else
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(156, 105), resolution));


            e.Graphics.DrawString(CurrentEntente.NomPrenom, ft, br, ConvertmmToPxl(new PointF(73, 158), resolution));
            e.Graphics.DrawString(CurrentEntente.patient.DateNaissance.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(52, 162), resolution));
            e.Graphics.DrawString(CurrentEntente.ImmatAssure, ft, br, ConvertmmToPxl(new PointF(126, 162), resolution));








            switch (CurrentEntente.typetraitement)
            {
                case EntentePrealable.TypeDeTraitement.Debut:
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(11, 181), resolution));
                    break;
                case EntentePrealable.TypeDeTraitement.Semestre:
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(79, 181), resolution));
                    e.Graphics.DrawString(CurrentEntente.Semestre.ToString(), ft, br, ConvertmmToPxl(new PointF(124, 181), resolution));
                    break;
                case EntentePrealable.TypeDeTraitement.Autre:
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(153, 181), resolution));
                    e.Graphics.DrawString(CurrentEntente.Autre.ToString(), ft, br, ConvertmmToPxl(new PointF(124, 181), resolution));
                    break;
                case EntentePrealable.TypeDeTraitement.Surveillance:
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(11, 186), resolution));
                    break;
                case EntentePrealable.TypeDeTraitement.Contention:
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(79, 186), resolution));
                    e.Graphics.DrawString(CurrentEntente.Contention.ToString(), ft, br, ConvertmmToPxl(new PointF(124, 181), resolution));
                    break;

            }

            if (CurrentEntente.SensSagittalBasalMax == EntentePrealable.en_ProRetro.Pro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(46, 202), resolution));
            if (CurrentEntente.SensSagittalBasalMax == EntentePrealable.en_ProRetro.Retro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(66, 202), resolution));
            if (CurrentEntente.SensSagittalBasalMand == EntentePrealable.en_ProRetro.Pro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(84, 202), resolution));
            if (CurrentEntente.SensSagittalBasalMand == EntentePrealable.en_ProRetro.Retro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(107, 202), resolution));

            if (CurrentEntente.SensSagittalAlveolaireMax == EntentePrealable.en_ProRetro.Pro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(132, 202), resolution));
            if (CurrentEntente.SensSagittalAlveolaireMax == EntentePrealable.en_ProRetro.Retro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(153, 202), resolution));
            if (CurrentEntente.SensSagittalAlveolaireMand == EntentePrealable.en_ProRetro.Pro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 202), resolution));
            if (CurrentEntente.SensSagittalAlveolaireMand == EntentePrealable.en_ProRetro.Retro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(193, 202), resolution));


            if (CurrentEntente.SensTransversalBasalMax == EntentePrealable.en_DiagOsseux.Endognatie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(46, 206), resolution));
            if (CurrentEntente.SensTransversalBasalMax == EntentePrealable.en_DiagOsseux.Exognatie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(66, 206), resolution));
            if (CurrentEntente.SensTransversalBasalMand == EntentePrealable.en_DiagOsseux.Endognatie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(84, 206), resolution));
            if (CurrentEntente.SensTransversalBasalMand == EntentePrealable.en_DiagOsseux.Exognatie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(107, 206), resolution));

            if (CurrentEntente.SensTransversalAlveolaireMax == EntentePrealable.en_DiagAlveolaire.Endoalveolie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(132, 206), resolution));
            if (CurrentEntente.SensTransversalAlveolaireMax == EntentePrealable.en_DiagAlveolaire.Exoalveolie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(153, 206), resolution));
            if (CurrentEntente.SensTransversalAlveolaireMand == EntentePrealable.en_DiagAlveolaire.Endoalveolie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 206), resolution));
            if (CurrentEntente.SensTransversalAlveolaireMand == EntentePrealable.en_DiagAlveolaire.Exoalveolie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(193, 206), resolution));


            if (CurrentEntente.SensVerticalBasal == EntentePrealable.en_Divergence.Hypodivergent)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(46, 210), resolution));
            if (CurrentEntente.SensVerticalBasal == EntentePrealable.en_Divergence.Hyperdivergent)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(84, 210), resolution));
            if (CurrentEntente.SensVerticalAlveolaire == EntentePrealable.en_OccFace.Supraclusion)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(132, 210), resolution));
            if (CurrentEntente.SensVerticalAlveolaire == EntentePrealable.en_OccFace.Infraclusion)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 210), resolution));

            if (CurrentEntente.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_I)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(43, 219), resolution));
            if (CurrentEntente.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_II)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(56, 219), resolution));
            if (CurrentEntente.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_III)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(69, 219), resolution));

            e.Graphics.DrawString(CurrentEntente.ClasseDentaireMolaireTxt.ToString(), ft, br, ConvertmmToPxl(new PointF(80, 219), resolution));


            if (CurrentEntente.ClasseDentaireCanine == EntentePrealable.en_Class.Class_I)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(145, 219), resolution));
            if (CurrentEntente.ClasseDentaireCanine == EntentePrealable.en_Class.Class_II)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(158, 219), resolution));
            if (CurrentEntente.ClasseDentaireCanine == EntentePrealable.en_Class.Class_III)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 219), resolution));

            e.Graphics.DrawString(CurrentEntente.ClasseDentaireCanineTxt.ToString(), ft, br, ConvertmmToPxl(new PointF(182, 219), resolution));


            if (CurrentEntente.DDM)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(10, 223), resolution));
            if (CurrentEntente.DDD)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(125, 223), resolution));

            e.Graphics.DrawString(CurrentEntente.Agenesie, ft, br, ConvertmmToPxl(new PointF(28, 225), resolution));
            e.Graphics.DrawString(CurrentEntente.DentsIncluseSurnum, ft, br, ConvertmmToPxl(new PointF(104, 225), resolution));
            e.Graphics.DrawString(CurrentEntente.Malposition, ft, br, ConvertmmToPxl(new PointF(168, 225), resolution));


            if ((CurrentEntente.occInverse == EntentePrealable.en_OccInverse.Droite) || (CurrentEntente.occInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche))
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(62, 232), resolution));

            if ((CurrentEntente.occInverse == EntentePrealable.en_OccInverse.Gauche) || (CurrentEntente.occInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche))
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(85, 232), resolution));

            if (CurrentEntente.occInverse == EntentePrealable.en_OccInverse.Anterieur)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(110, 232), resolution));

            e.Graphics.DrawString(CurrentEntente.FacteurFonctionnel, ft, br, ConvertmmToPxl(new PointF(58, 238), resolution));

            PointF Startpt = ConvertmmToPxl(new PointF(10, 250), resolution);
            PointF Endpt = ConvertmmToPxl(new PointF(200, 265), resolution);
            RectangleF r = new RectangleF(Startpt.X, Startpt.Y,Endpt.X-Startpt.X,Endpt.Y-Startpt.Y);

            e.Graphics.DrawString(CurrentEntente.PlanDeTraitement, ft, br, r);


            Startpt = ConvertmmToPxl(new PointF(10, 273), resolution);
            Endpt = ConvertmmToPxl(new PointF(200, 287), resolution);
            r = new RectangleF(Startpt.X, Startpt.Y, Endpt.X - Startpt.X, Endpt.Y - Startpt.Y);


            e.Graphics.DrawString(CurrentEntente.Commentaires, ft, br, r);
            e.Graphics.DrawString(CurrentEntente.Commentaires, ft, br, r);


            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(150, 285), resolution));
        }
    }
    /*
    public class EntentePrealableDocumentRecto : PrintDocument
    {

        private EntentePrealable _CurrentEntente;
        public EntentePrealable CurrentEntente
        {
            get
            {
                return _CurrentEntente;
            }
            set
            {
                _CurrentEntente = value;
            }
        }


        private bool _ShowBackGround;
        public bool ShowBackGround
        {
            get
            {
                return _ShowBackGround;
            }
            set
            {
                _ShowBackGround = value;
            }
        }

        public EntentePrealableDocumentRecto(EntentePrealable ep, bool ShowBackGround)
        {
            CurrentEntente = ep;
            _ShowBackGround = ShowBackGround;
        }

        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {
            
            base.OnQueryPageSettings(e); 
            e.PageSettings.Margins = new Margins(0, 0, 0, 0);
        }

        void DrawTick(Graphics g,PointF pt)
        {
            int WIDTH = 5;
            Pen p = new Pen(Brushes.Black,2);
            g.DrawLine(p, new PointF(pt.X - WIDTH, pt.Y - WIDTH), new PointF(pt.X + WIDTH, pt.Y + WIDTH));
            g.DrawLine(p, new PointF(pt.X + WIDTH, pt.Y - WIDTH), new PointF(pt.X - WIDTH, pt.Y + WIDTH));
        }

        PointF ConvertmmToPxl(PointF Pt_En_mm, PrinterResolution res)
        {

            //829-1169
            //210-297


            PointF AdjustementPt = new PointF(-5, -5);

            float x = (829f / 210f) * Pt_En_mm.X;
            float y = (1169f / 297f) * Pt_En_mm.Y;


            
            //((value/10) * (96f / 2.54f));
            //96DPI 96pxl=1Inch 96pxl=25,4mm  3,78pxl = 1mm
            //600DPI 600pxl=1Inch 600pxl=25,4mm  23,62pxl = 1mm
            //float x = (Pt_En_mm.X * (res.X / 25.4f));
            //float y = (Pt_En_mm.Y * (res.Y / 25.4f));
            
            return new PointF(x + AdjustementPt.X, y + AdjustementPt.Y);

        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {

            NormalPrint(e);

        }

        private void TestPrint(PrintPageEventArgs e)
        {
            //PrinterResolution resolution = e.PageSettings.PrinterResolution;
            PrinterResolution resolution = new PrinterResolution();
            resolution.X = 96;
            resolution.Y = 96;

            Font ft = new Font("Arial", 12, FontStyle.Regular);
            Brush br = Brushes.Black;

            e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.DEPRecto, e.PageBounds);

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(140, 72), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(178, 72), resolution));


            //PointF ptf = ConvertmmToPxl(new PointF(210,297),resolution);
            //e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, (int)ptf.X, (int)ptf.Y));

            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(73, 77), resolution));
            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(73, 86), resolution));
            e.Graphics.DrawString("TO90", ft, br, ConvertmmToPxl(new PointF(73, 94), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(106, 105), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(156, 105), resolution));


            e.Graphics.DrawString("TESTMEGALONG", ft, br, ConvertmmToPxl(new PointF(73, 158), resolution));
            e.Graphics.DrawString("09/09/1980", ft, br, ConvertmmToPxl(new PointF(52, 162), resolution));
            e.Graphics.DrawString("ABBCCDDEEEFFFGG", ft, br, ConvertmmToPxl(new PointF(126, 162), resolution));








            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(11, 181), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(79, 181), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(153, 181), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(11, 186), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(79, 186), resolution));

            

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(46, 202), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(66, 202), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(84, 202), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(107, 202), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(132, 202), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(153, 202), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 202), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(193, 202), resolution));


            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(46, 206), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(66, 206), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(84, 206), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(107, 206), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(132, 206), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(153, 206), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 206), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(193, 206), resolution));


            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(46, 210), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(84, 210), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(132, 210), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 210), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(43, 219), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(56, 219), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(69, 219), resolution));

            e.Graphics.DrawString("Partiel", ft, br, ConvertmmToPxl(new PointF(80, 219), resolution));


            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(145, 219), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(158, 219), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 219), resolution));

            e.Graphics.DrawString("Partiel", ft, br, ConvertmmToPxl(new PointF(182, 219), resolution));


            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(10, 223), resolution));
            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(125, 223), resolution));

            e.Graphics.DrawString("CurrentEntente.Agenesie", ft, br, ConvertmmToPxl(new PointF(28, 225), resolution));
            e.Graphics.DrawString("CurrentEntente.DentsIncluseSurnum", ft, br, ConvertmmToPxl(new PointF(104, 225), resolution));
            e.Graphics.DrawString("CurrentEntente.Malposition", ft, br, ConvertmmToPxl(new PointF(168, 225), resolution));


            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(62, 232), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(85, 232), resolution));

            DrawTick(e.Graphics, ConvertmmToPxl(new PointF(110, 232), resolution));

            e.Graphics.DrawString("Petit test pour le champ \"Facteur fonctionnel\"", ft, br, ConvertmmToPxl(new PointF(58, 238), resolution));

            e.Graphics.DrawString("Ligne 1", ft, br, ConvertmmToPxl(new PointF(10, 250), resolution));
            e.Graphics.DrawString("Ligne 2", ft, br, ConvertmmToPxl(new PointF(10, 255), resolution));
            e.Graphics.DrawString("Ligne 3", ft, br, ConvertmmToPxl(new PointF(10, 260), resolution));

            e.Graphics.DrawString("Ligne 11", ft, br, ConvertmmToPxl(new PointF(10, 272), resolution));
            e.Graphics.DrawString("Ligne 12", ft, br, ConvertmmToPxl(new PointF(10, 277), resolution));


            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(150, 285), resolution));
        }

        private void NormalPrint(PrintPageEventArgs e)
        {
            //PrinterResolution resolution = e.PageSettings.PrinterResolution;
            PrinterResolution resolution = new PrinterResolution();
            resolution.X = 96;
            resolution.Y = 96;

            Font ft = new Font("Arial", 12, FontStyle.Regular);
            Brush br = Brushes.Black;

            if (_ShowBackGround)
                e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.DEPRecto, e.PageBounds);

            if (CurrentEntente.ReferenceNationalOpposable == EntentePrealable.RNO.R)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(140, 72), resolution));
            if (CurrentEntente.ReferenceNationalOpposable == EntentePrealable.RNO.HR)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(178, 72), resolution));


            //PointF ptf = ConvertmmToPxl(new PointF(210,297),resolution);
            //e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, (int)ptf.X, (int)ptf.Y));

            e.Graphics.DrawString(CurrentEntente.dateProposition.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(73, 77), resolution));
            e.Graphics.DrawString(CurrentEntente.DateDebutTraitement.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(73, 86), resolution));
            e.Graphics.DrawString(CurrentEntente.cotationDesActes, ft, br, ConvertmmToPxl(new PointF(73, 94), resolution));

            if (CurrentEntente.IsDevisSigned)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(106, 105), resolution));
            else
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(156, 105), resolution));


            e.Graphics.DrawString(CurrentEntente.NomPrenom, ft, br, ConvertmmToPxl(new PointF(73, 158), resolution));
            e.Graphics.DrawString(CurrentEntente.DateNaissance, ft, br, ConvertmmToPxl(new PointF(52, 162), resolution));
            e.Graphics.DrawString(CurrentEntente.ImmatAssure, ft, br, ConvertmmToPxl(new PointF(126, 162), resolution));








            switch (CurrentEntente.typetraitement)
            {
                case EntentePrealable.TypeDeTraitement.Debut:
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(11, 181), resolution));
                    break;
                case EntentePrealable.TypeDeTraitement.Semestre:
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(79, 181), resolution));
                    e.Graphics.DrawString(CurrentEntente.Semestre.ToString(), ft, br, ConvertmmToPxl(new PointF(124, 181), resolution));
                    break;
                case EntentePrealable.TypeDeTraitement.Autre:
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(153, 181), resolution));
                    e.Graphics.DrawString(CurrentEntente.Autre.ToString(), ft, br, ConvertmmToPxl(new PointF(124, 181), resolution));
                    break;
                case EntentePrealable.TypeDeTraitement.Surveillance:
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(11, 186), resolution));
                    break;
                case EntentePrealable.TypeDeTraitement.Contention:
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(79, 186), resolution));
                    e.Graphics.DrawString(CurrentEntente.Contention.ToString(), ft, br, ConvertmmToPxl(new PointF(124, 181), resolution));
                    break;

            }

            if (CurrentEntente.SensSagittalBasalMax == BasCommon_BO.EntentePrealable.en_ProRetro.Pro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(46, 202), resolution));
            if (CurrentEntente.SensSagittalBasalMax == BasCommon_BO.EntentePrealable.en_ProRetro.Retro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(66, 202), resolution));
            if (CurrentEntente.SensSagittalBasalMand == BasCommon_BO.EntentePrealable.en_ProRetro.Pro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(84, 202), resolution));
            if (CurrentEntente.SensSagittalBasalMand == BasCommon_BO.EntentePrealable.en_ProRetro.Retro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(107, 202), resolution));

            if (CurrentEntente.SensSagittalAlveolaireMax == BasCommon_BO.EntentePrealable.en_ProRetro.Pro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(132, 202), resolution));
            if (CurrentEntente.SensSagittalAlveolaireMax == BasCommon_BO.EntentePrealable.en_ProRetro.Retro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(153, 202), resolution));
            if (CurrentEntente.SensSagittalAlveolaireMand == BasCommon_BO.EntentePrealable.en_ProRetro.Pro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 202), resolution));
            if (CurrentEntente.SensSagittalAlveolaireMand == BasCommon_BO.EntentePrealable.en_ProRetro.Retro)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(193, 202), resolution));


            if (CurrentEntente.SensTransversalBasalMax == BasCommon_BO.EntentePrealable.en_DiagOsseux.Endognatie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(46, 206), resolution));
            if (CurrentEntente.SensTransversalBasalMax == BasCommon_BO.EntentePrealable.en_DiagOsseux.Exognatie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(66, 206), resolution));
            if (CurrentEntente.SensTransversalBasalMand == BasCommon_BO.EntentePrealable.en_DiagOsseux.Endognatie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(84, 206), resolution));
            if (CurrentEntente.SensTransversalBasalMand == BasCommon_BO.EntentePrealable.en_DiagOsseux.Exognatie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(107, 206), resolution));

            if (CurrentEntente.SensTransversalAlveolaireMax == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Endoalveolie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(132, 206), resolution));
            if (CurrentEntente.SensTransversalAlveolaireMax == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Exoalveolie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(153, 206), resolution));
            if (CurrentEntente.SensTransversalAlveolaireMand == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Endoalveolie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 206), resolution));
            if (CurrentEntente.SensTransversalAlveolaireMand == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Exoalveolie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(193, 206), resolution));


            if (CurrentEntente.SensVerticalBasal == BasCommon_BO.EntentePrealable.en_Divergence.Hypodivergent)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(46, 210), resolution));
            if (CurrentEntente.SensVerticalBasal == BasCommon_BO.EntentePrealable.en_Divergence.Hyperdivergent)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(84, 210), resolution));
            if (CurrentEntente.SensVerticalAlveolaire == BasCommon_BO.EntentePrealable.en_OccFace.Supraclusion)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(132, 210), resolution));
            if (CurrentEntente.SensVerticalAlveolaire == BasCommon_BO.EntentePrealable.en_OccFace.Infraclusion)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 210), resolution));

            if (CurrentEntente.ClasseDentaireMolaire == BasCommon_BO.EntentePrealable.en_Class.Class_I)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(43, 219), resolution));
            if (CurrentEntente.ClasseDentaireMolaire == BasCommon_BO.EntentePrealable.en_Class.Class_II)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(56, 219), resolution));
            if (CurrentEntente.ClasseDentaireMolaire == BasCommon_BO.EntentePrealable.en_Class.Class_III)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(69, 219), resolution));

            e.Graphics.DrawString(CurrentEntente.ClasseDentaireMolaireTxt.ToString(), ft, br, ConvertmmToPxl(new PointF(80, 219), resolution));


            if (CurrentEntente.ClasseDentaireCanine == BasCommon_BO.EntentePrealable.en_Class.Class_I)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(145, 219), resolution));
            if (CurrentEntente.ClasseDentaireCanine == BasCommon_BO.EntentePrealable.en_Class.Class_II)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(158, 219), resolution));
            if (CurrentEntente.ClasseDentaireCanine == BasCommon_BO.EntentePrealable.en_Class.Class_III)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(171, 219), resolution));

            e.Graphics.DrawString(CurrentEntente.ClasseDentaireCanineTxt.ToString(), ft, br, ConvertmmToPxl(new PointF(182, 219), resolution));


            if (CurrentEntente.DDM)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(10, 223), resolution));
            if (CurrentEntente.DDD)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(125, 223), resolution));

            e.Graphics.DrawString(CurrentEntente.Agenesie, ft, br, ConvertmmToPxl(new PointF(28, 225), resolution));
            e.Graphics.DrawString(CurrentEntente.DentsIncluseSurnum, ft, br, ConvertmmToPxl(new PointF(104, 225), resolution));
            e.Graphics.DrawString(CurrentEntente.Malposition, ft, br, ConvertmmToPxl(new PointF(168, 225), resolution));


            if ((CurrentEntente.occInverse == BasCommon_BO.EntentePrealable.en_OccInverse.Droite) || (CurrentEntente.occInverse == BasCommon_BO.EntentePrealable.en_OccInverse.Droite_Et_Gauche))
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(62, 232), resolution));

            if ((CurrentEntente.occInverse == BasCommon_BO.EntentePrealable.en_OccInverse.Gauche) || (CurrentEntente.occInverse == BasCommon_BO.EntentePrealable.en_OccInverse.Droite_Et_Gauche))
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(85, 232), resolution));

            if (CurrentEntente.occInverse == BasCommon_BO.EntentePrealable.en_OccInverse.Anterieur)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(110, 232), resolution));

            e.Graphics.DrawString(CurrentEntente.FacteurFonctionnel, ft, br, ConvertmmToPxl(new PointF(58, 238), resolution));

            string[] ss = CurrentEntente.PlanDeTraitement.Split('\n');
            if (ss.Length > 0) e.Graphics.DrawString(ss[0], ft, br, ConvertmmToPxl(new PointF(10, 250), resolution));
            if (ss.Length > 1) e.Graphics.DrawString(ss[1], ft, br, ConvertmmToPxl(new PointF(10, 255), resolution));
            if (ss.Length > 2) e.Graphics.DrawString(ss[2], ft, br, ConvertmmToPxl(new PointF(10, 260), resolution));

            ss = CurrentEntente.Commentaires.Split('\n');
            if (ss.Length > 0) e.Graphics.DrawString(ss[0], ft, br, ConvertmmToPxl(new PointF(10, 272), resolution));
            if (ss.Length > 1) e.Graphics.DrawString(ss[1], ft, br, ConvertmmToPxl(new PointF(10, 277), resolution));


            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(150, 285), resolution));
        }
    }
    */
}
