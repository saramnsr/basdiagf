using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using BasCommon_BO;

namespace BaseCommonControls.documentsToPrint
{
    public class DocumentFeuilleDeSoin : PrintDocument
    {

        private bool _HideDatePaiement = false;
        public bool HideDatePaiement
        {
            get
            {
                return _HideDatePaiement;
            }
            set
            {
                _HideDatePaiement = value;
            }
        }

        private bool _PrintBothPages = true;
        public bool PrintBothPages
        {
            get
            {
                return _PrintBothPages;
            }
            set
            {
                _PrintBothPages = value;
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

        private bool _IsDuplicata = false;
        public bool IsDuplicata
        {
            get
            {
                return _IsDuplicata;
            }
            set
            {
                _IsDuplicata = value;
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
        private FeuilleDeSoin _CurrentFeuilleDeSoin;
        public FeuilleDeSoin CurrentFeuilleDeSoin
        {
            get
            {
                return _CurrentFeuilleDeSoin;
            }
            set
            {
                _CurrentFeuilleDeSoin = value;
            }
        }


        public DocumentFeuilleDeSoin(FeuilleDeSoin fs, bool ShowBackground)
        {
            CurrentFeuilleDeSoin = fs;
            _ShowBackground = ShowBackground;
        }

        PointF ConvertmmToPxl(PointF Pt_En_mm, PrinterResolution res)
        {

            //829-1169
            //210-297

            Pt_En_mm = new PointF(Pt_En_mm.X - 1, Pt_En_mm.Y - 1.5f);


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


        public bool isrecto = false;
        

        void DrawTick(Graphics g, PointF pt)
        {
            int WIDTH = 5;
            Pen p = new Pen(Brushes.Black, 2);
            g.DrawLine(p, new PointF(pt.X - WIDTH, pt.Y - WIDTH), new PointF(pt.X + WIDTH, pt.Y + WIDTH));
            g.DrawLine(p, new PointF(pt.X + WIDTH, pt.Y - WIDTH), new PointF(pt.X - WIDTH, pt.Y + WIDTH));
        }


        int printed = 0;
        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {

            base.OnQueryPageSettings(e);
            e.PageSettings.Margins = new Margins(0, 0, 0, 0);

        }


        protected override void OnPrintPage(PrintPageEventArgs e)
        {


            PrinterBounds objBounds = new PrinterBounds(e);
            HardMarginRect = objBounds.Bounds;  // Get the REAL Margin Bounds !

            if (PrintBothPages)
            {
                if (printed==0)
                {
                    NormalPrintRecto(e);
                    printed++;
                    e.HasMorePages = true;
                }
                else
                {
                    NormalPrintVerso(e);
                }
            }
            else
            {
                if (isrecto)
                {
                    NormalPrintRecto(e);
                }
                else
                {
                    NormalPrintVerso(e);
                }
            }


            /*
            if (isrecto)
            {
                NormalPrintRecto(e);
                isrecto = false;
                e.HasMorePages = false;
            }
            else
            {
                NormalPrintVerso(e);
                isrecto = true;
                e.HasMorePages = true;
            }
            */
        }

        private void NormalPrintRecto(PrintPageEventArgs e)
        {

            Font ftDuplicata = new Font("Arial", 14, FontStyle.Bold);
            Brush brDuplicata = Brushes.Red;


            //PrinterResolution resolution = e.PageSettings.PrinterResolution;
            PrinterResolution resolution = new PrinterResolution();
            resolution.X = 96;
            resolution.Y = 96;


            Font ft = new Font("Arial", 8, FontStyle.Regular);
            Brush br = Brushes.Black;


            if (ShowBackground)
                e.Graphics.DrawImage(global::BaseCommonControls.Resource.FSRecto, e.PageBounds);

            e.Graphics.DrawString(CurrentFeuilleDeSoin.AgremmentRadiation, ft, br, ConvertmmToPxl(new PointF(31, 67), resolution));
            e.Graphics.DrawString(CurrentFeuilleDeSoin.AgrementPanoramique, ft, br, ConvertmmToPxl(new PointF(92, 67), resolution));
            e.Graphics.DrawString(CurrentFeuilleDeSoin.AgrementTeleradio, ft, br, ConvertmmToPxl(new PointF(163, 67), resolution));

            e.Graphics.DrawString(CurrentFeuilleDeSoin.NomPrenom, ft, br, ConvertmmToPxl(new PointF(89, 76), resolution));

            #region actes Non Soumis à Entente

            if (CurrentFeuilleDeSoin.CodeCoeffTotalNSEP != "") e.Graphics.DrawString(CurrentFeuilleDeSoin.CodeCoeffTotalNSEP, ft, br, ConvertmmToPxl(new PointF(72, 179), resolution));
            if (CurrentFeuilleDeSoin.TotalMontantNonSoumisAEntente > 0) e.Graphics.DrawString(CurrentFeuilleDeSoin.TotalMontantNonSoumisAEntente.ToString("C2"), ft, br, ConvertmmToPxl(new PointF(166, 183), resolution));
            if (CurrentFeuilleDeSoin.TotalMontantNonSoumisAEntente > 0) e.Graphics.DrawString(CurrentFeuilleDeSoin.TotalMontantNonSoumisAEntenteToutelettre, ft, br, ConvertmmToPxl(new PointF(90, 191), resolution));
            #endregion

            #region actes Soumis à Entente

            if (CurrentFeuilleDeSoin.CodeCoeffTotalSEP != "") e.Graphics.DrawString(CurrentFeuilleDeSoin.CodeCoeffTotalSEP, ft, br, ConvertmmToPxl(new PointF(72, 179 + 67), resolution));
            if (CurrentFeuilleDeSoin.TotalMontantSoumisAEntente>0) e.Graphics.DrawString(CurrentFeuilleDeSoin.TotalMontantSoumisAEntente.ToString("C2"), ft, br, ConvertmmToPxl(new PointF(166, 183 + 67), resolution));
            if (CurrentFeuilleDeSoin.TotalMontantSoumisAEntente > 0) e.Graphics.DrawString(CurrentFeuilleDeSoin.TotalMontantSoumisAEntenteToutelettre, ft, br, ConvertmmToPxl(new PointF(90, 192 + 67), resolution));
            #endregion

            float y = 208;
            foreach(ActePG afs in CurrentFeuilleDeSoin.actes)
            {
                DateTime dteActe = afs.DateExecution;

                if (afs.NbMois!=null)
                    dteActe = afs.DateExecution.AddMonths(afs.NbMois.Value).AddDays(afs.NbJours.Value);

                if (afs.NeedDEP)
                {
                    e.Graphics.DrawString(dteActe.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(8, y), resolution));
                    e.Graphics.DrawString(afs.numdent, ft, br, ConvertmmToPxl(new PointF(36, y), resolution));
                    e.Graphics.DrawString(afs.designation, ft, br, ConvertmmToPxl(new PointF(72, y), resolution));
                    if (afs.rno != PyxVitalWrapperConst.RNO.Néant) e.Graphics.DrawString(afs.rno.ToString(), ft, br, ConvertmmToPxl(new PointF(129, y), resolution));
                    e.Graphics.DrawString(afs.accident.ToString(), ft, br, ConvertmmToPxl(new PointF(142, y), resolution));
                    e.Graphics.DrawString(afs.ald.ToString(), ft, br, ConvertmmToPxl(new PointF(159, y), resolution));
                   // e.Graphics.DrawString(afs.Ordonnace.ToString(), ft, br, ConvertmmToPxl(new PointF(175, y), resolution));
                    if (afs.motifdepassement != PyxVitalWrapperConst.Qualificatif_depense.Néant) e.Graphics.DrawString(afs.motifdepassement.ToString(), ft, br, ConvertmmToPxl(new PointF(191, y), resolution));
                    y += 4.3f;
                }
            }

            y = 93.5f;
            foreach (ActePG afs in CurrentFeuilleDeSoin.actes)
            {

                DateTime dteActe = afs.DateExecution;

                if (afs.NbMois != null)
                    dteActe = afs.DateExecution.AddMonths(afs.NbMois.Value).AddDays(afs.NbJours.Value);

                if (!afs.NeedDEP)
                {
                    e.Graphics.DrawString(dteActe.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(8, y), resolution));
                    e.Graphics.DrawString(afs.numdent, ft, br, ConvertmmToPxl(new PointF(36, y), resolution));
                    e.Graphics.DrawString(afs.designation, ft, br, ConvertmmToPxl(new PointF(72, y), resolution));
                    if (afs.rno != PyxVitalWrapperConst.RNO.Néant) e.Graphics.DrawString(afs.rno.ToString(), ft, br, ConvertmmToPxl(new PointF(129, y), resolution));
                    e.Graphics.DrawString(afs.accident.ToString(), ft, br, ConvertmmToPxl(new PointF(142, y), resolution));
                    e.Graphics.DrawString(afs.ald.ToString(), ft, br, ConvertmmToPxl(new PointF(159, y), resolution));
                    e.Graphics.DrawString(afs.ordonnance.ToString(), ft, br, ConvertmmToPxl(new PointF(175, y), resolution));
                    if (afs.motifdepassement != PyxVitalWrapperConst.Qualificatif_depense.Néant) e.Graphics.DrawString(afs.motifdepassement.ToString(), ft, br, ConvertmmToPxl(new PointF(191, y), resolution));
                    y += 4.3f;
                }
            }

            if (!HideDatePaiement)
                e.Graphics.DrawString(CurrentFeuilleDeSoin.datePaiementHonoraire.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(60, 192 + 67 + 8), resolution));


            if (IsDuplicata)
                e.Graphics.DrawString("DUPLICATA", ftDuplicata, brDuplicata, ConvertmmToPxl(new PointF(100, 150), resolution));

        }
        
        private void NormalPrintVerso(PrintPageEventArgs e)
        {
            //PrinterResolution resolution = e.PageSettings.PrinterResolution;
            PrinterResolution resolution = new PrinterResolution();
            resolution.X = 96;
            resolution.Y = 96;

            Font ftDuplicata = new Font("Arial", 26, FontStyle.Bold);
            Brush brDuplicata = Brushes.Red;

            Font ft = new Font("Arial", 8, FontStyle.Regular);
            Brush br = Brushes.Black;

            
            if (ShowBackground)
                e.Graphics.DrawImage(global::BaseCommonControls.Resource.FSVerso, e.PageBounds);

            if ((CurrentFeuilleDeSoin.patient.Assurepar!=null) &&(CurrentFeuilleDeSoin.patient.Assurepar.correspondant == null))
                CurrentFeuilleDeSoin.patient.Assurepar.correspondant = BasCommon_BL.MgmtCorrespondants.getCorrespondant(CurrentFeuilleDeSoin.patient.Assurepar.IdCorrespondance);

            string c = CurrentFeuilleDeSoin.patient.caisse!=null?CurrentFeuilleDeSoin.patient.caisse.Nom:"";

            e.Graphics.DrawString(CurrentFeuilleDeSoin.ImmatAssure+"-"+c, ft, br, ConvertmmToPxl(new PointF(45, 22), resolution));
            if ((CurrentFeuilleDeSoin.DatenaissanceAssure!=null) && (CurrentFeuilleDeSoin.DatenaissanceAssure != DateTime.MinValue))
                e.Graphics.DrawString(CurrentFeuilleDeSoin.DatenaissanceAssure.Value.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(34, 26), resolution));
            e.Graphics.DrawString(CurrentFeuilleDeSoin.NomPrenomAssure.ToString(), ft, br, ConvertmmToPxl(new PointF(26, 31), resolution));
            e.Graphics.DrawString(CurrentFeuilleDeSoin.AdresseAssure, ft, br, ConvertmmToPxl(new PointF(26, 35), resolution));



            if (CurrentFeuilleDeSoin.Salarie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(8, 66), resolution));
            if (CurrentFeuilleDeSoin.SansEmplois)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(8, 71), resolution));
            if (CurrentFeuilleDeSoin.DateDeCessationActivite!=null)
                e.Graphics.DrawString(CurrentFeuilleDeSoin.DateDeCessationActivite.Value.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(67, 68), resolution));

            if (CurrentFeuilleDeSoin.PensionAssure)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(8, 75), resolution));
            if (CurrentFeuilleDeSoin.AutreCas)
            {
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(33, 75), resolution));
                e.Graphics.DrawString(CurrentFeuilleDeSoin.LibAutreCas, ft, br, ConvertmmToPxl(new PointF(67, 73), resolution));

            }
            if (CurrentFeuilleDeSoin.NonSalarie)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(64, 66), resolution));





            e.Graphics.DrawString(CurrentFeuilleDeSoin.Profession, ft, br, ConvertmmToPxl(new PointF(166, 22), resolution));



            if (CurrentFeuilleDeSoin.Accident)
            {
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(138, 33), resolution));
                e.Graphics.DrawString(CurrentFeuilleDeSoin.DateAccident.Value.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(163, 30), resolution));
            }
            else
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(189, 33), resolution));

            if (CurrentFeuilleDeSoin.Pensionne)
                DrawTick(e.Graphics, ConvertmmToPxl(new PointF(189, 42), resolution));




            if (CurrentFeuilleDeSoin.NomPrenomAssure != CurrentFeuilleDeSoin.NomPrenom)
            {
                e.Graphics.DrawString(CurrentFeuilleDeSoin.patient.Nom, ft, br, ConvertmmToPxl(new PointF(122, 51), resolution));
                e.Graphics.DrawString(CurrentFeuilleDeSoin.patient.Prenom, ft, br, ConvertmmToPxl(new PointF(122, 56), resolution));
                e.Graphics.DrawString(CurrentFeuilleDeSoin.patient.DateNaissance.ToShortDateString(), ft, br, ConvertmmToPxl(new PointF(184, 56), resolution));

                if (CurrentFeuilleDeSoin.PensionPatient)
                {
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(159, 66), resolution));
                }
                else
                    DrawTick(e.Graphics, ConvertmmToPxl(new PointF(190, 66), resolution));

                e.Graphics.DrawString(CurrentFeuilleDeSoin.AdressePatient, ft, br, ConvertmmToPxl(new PointF(106, 75), resolution));



            }

           // if (IsDuplicata)
             //   e.Graphics.DrawString("DUPLICATA", ftDuplicata, brDuplicata, ConvertmmToPxl(new PointF(10, 15), resolution));

        }
    }
}
