using BasCommon_BO;
using BasCommon_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL
{
    public static class MgmtInvisalignEnBouche
    {




        public static InvisalignEnBouche GetActualInvisalignEnBouche(basePatient patient,bool Haut)
        {

            if (patient.aligneurs == null)
                patient.aligneurs = MgmtInvisalignEnBouche.GetInvisalignEnBouche(patient);
            if(patient.aligneurs.Count > 0)
                return patient.aligneurs.Find(x=>x.DateDebut < DateTime.Now && x.DateFin > DateTime.Now && x.IsHaut == Haut);
         /**   foreach (InvisalignEnBouche ib in patient.aligneurs)
                if ((ib.DateDebut < DateTime.Now) && (ib.DateFin > DateTime.Now) && (ib.IsHaut == Haut))
                    return ib;
            */

            return null;
        }

        public static int GetNbTotalAligneurs(basePatient patient, bool Haut)
        {

            if (patient.aligneurs == null)
                patient.aligneurs = MgmtInvisalignEnBouche.GetInvisalignEnBouche(patient);


            int total = 0;
            foreach (InvisalignEnBouche ib in patient.aligneurs)
                if (ib.IsHaut == Haut)
                    total++;


            return total;
        }

        public static List<InvisalignEnBouche> GetInvisalignEnBouche(basePatient patient)
        {
            List<InvisalignEnBouche> lst = new List<InvisalignEnBouche>();

            DataTable dt = DAC.getInvisalignEnbouche(patient);
            foreach (DataRow dr in dt.Rows)
            {
                InvisalignEnBouche ib = Builders.BuildInvisalignEnBouche.Build(dr);
                lst.Add(ib);
            }
            return lst;
        }

        public static void InsertInvisalignEnBouche(InvisalignEnBouche invisalign)
        {
            if (invisalign.Id == -1) DAC.AddInvisalignEnBouche(invisalign);

        }

        public static void UpdateInvisalignEnBouche(InvisalignEnBouche invisalign)
        {
            if (invisalign.Id != -1) DAC.UpdateInvisalignEnBouche(invisalign);

        }

        public static void DeleteInvisalignEnBouche(InvisalignEnBouche invisalign)
        {
            if (invisalign.Id != -1) DAC.DeleteInvisalignEnBouche(invisalign);

        }

        public static void SaveAligneurs(List<InvisalignEnBouche> aligneurs)
        {

            foreach (InvisalignEnBouche ib in aligneurs)
            {
                if (ib.Id == -1)
                    InsertInvisalignEnBouche(ib);
                else
                    UpdateInvisalignEnBouche(ib);
            }
        }

        public static List<InvisalignEnBouche> GetPlanningAligneurs(basePatient pat,
            int NbAligneursBas,
            int NbAligneursHAut,
            InvisalignEnBouche.ChangeFrequency ChgeFreq,
            DateTime DteDebut)
        {
            List<InvisalignEnBouche> lst = new List<InvisalignEnBouche>();

            DateTime DteH = DteDebut;
            DateTime DteB = DteDebut;


            #region Aligneur Haut
            for (int i = 1; i <= NbAligneursHAut; i++)
            {
                InvisalignEnBouche ib = new InvisalignEnBouche();
                ib.Patient = pat;
                ib.NumAligneur = i;
                ib.IsHaut = true;
                ib.DateDebut = DteH;

                switch (ChgeFreq)
                {
                    case InvisalignEnBouche.ChangeFrequency.S1:
                        DteH = DteH.AddDays(14);
                        break;
                    case InvisalignEnBouche.ChangeFrequency.S2:
                        DteH = DteH.AddMonths(1);
                        break;
                    case InvisalignEnBouche.ChangeFrequency.S4:
                        DteH = DteH.AddDays(3 * 7);
                        break;
                    //case InvisalignEnBouche.ChangeFrequency._4Mois:
                    //    DteH = DteH.AddMonths(4);
                    //    break;
                    case InvisalignEnBouche.ChangeFrequency.S3:
                        DteH = DteH.AddDays(7);
                        break;
                }

                ib.DateFin = DteB > DteH ? DteB : DteH;
                lst.Add(ib);

            }
            #endregion

            #region Aligneur Bas
            for (int i = 1; i <= NbAligneursBas; i++)
            {
                InvisalignEnBouche ib = new InvisalignEnBouche();
                ib.Patient = pat;
                ib.NumAligneur = i;
                ib.IsHaut = false;
                ib.DateDebut = DteB;

                switch (ChgeFreq)
                {
                    case InvisalignEnBouche.ChangeFrequency.S1:
                        DteB = DteB.AddDays(14);
                        break;
                    case InvisalignEnBouche.ChangeFrequency.S3:
                        DteB = DteB.AddDays(14);
                        break;
                    case InvisalignEnBouche.ChangeFrequency.S2:
                        DteB = DteB.AddMonths(1);
                        break;
                    case InvisalignEnBouche.ChangeFrequency.S4:
                        DteB = DteB.AddDays(3 * 7);
                        break;
                    //case InvisalignEnBouche.ChangeFrequency._4Mois:
                    //    DteB = DteB.AddMonths(4);
                    //    break;
                }
                ib.DateFin = DteB;
                lst.Add(ib);
            }
            #endregion



            return lst;
        }

        public static bool CheckSuperpos(List<InvisalignEnBouche> list)
        {

            foreach (InvisalignEnBouche ib in list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if ((ib.DateDebut > list[i].DateDebut) && (ib.DateDebut < list[i].DateFin))
                        return false;
                    if ((ib.DateFin > list[i].DateDebut) && (ib.DateFin < list[i].DateFin))
                        return false;
                }
            }
            return true;
        }


        public static void MoveAligneurs(DateTime dte, TimeSpan ts,List<InvisalignEnBouche> list,InvisalignEnBouche[] excepts)
        {
            foreach (InvisalignEnBouche ieb in list)
            {
                if (excepts.Contains(ieb)) continue;
                if (ieb.DateDebut.HasValue && (ieb.DateDebut.Value.Date >= dte.Date))
                {
                    if (ieb.DateDebut.HasValue) ieb.DateDebut = ieb.DateDebut.Value.Add(ts);
                    if (ieb.DateFin.HasValue) ieb.DateFin = ieb.DateFin.Value.Add(ts);
                }
            }
        }


        //public static bool CheckSuperpos(InvisalignEnBouche ElementToCheck,List<InvisalignEnBouche> list)
        //{
        //    InvisalignEnBouche nextaligneur = null;
        //    foreach (InvisalignEnBouche ib in list)
        //    {
        //        if ((ib.NumAligneur == ElementToCheck.NumAligneur + 1))
        //        {
        //            nextaligneur = ib;
        //            break;
        //        }                
        //    }

        //    if (nextaligneur != null)
        //    {
        //        if (ElementToCheck.DateFin > nextaligneur.DateDebut)
        //        {
        //            TimeSpan ts = nextaligneur.DateFin.Value - nextaligneur.DateDebut.Value;
        //            nextaligneur.DateDebut = ElementToCheck.DateFin;
        //            nextaligneur.DateFin = nextaligneur.DateDebut.Value.Add(ts);
        //            CheckSuperpos(nextaligneur, list);
        //        }
        //    }

        //    return true;
        //}

    }
}
