using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using BasCommon_BL;
using BasCommon_DAL;
using System.Data;

namespace BASEPractice_BL
{
    public static class MgmtEcheance
    {

        public enum periodic
        {
            Jour = 0,
            Semaine = 1,
            Mois = 2
        }

        public static bool CheckPrelevementDates(List<BaseTempEcheanceDefinition> lstech)
        {
            string dtepre = System.Configuration.ConfigurationManager.AppSettings["DatesPrelevement"];
            List<int> AuthorizedDays = new List<int>();
            try
            {
                foreach (string s in dtepre.Split(','))
                {
                    int d = Convert.ToInt32(s);
                    AuthorizedDays.Add(d);

                }
            }
            catch (System.Exception)
            {
                AuthorizedDays.Add(10);
                AuthorizedDays.Add(20);
                AuthorizedDays.Add(30);
            }

            foreach (BaseTempEcheanceDefinition ted in lstech)
            {
                int maxday = new DateTime(ted.DAteEcheance.Year, ted.DAteEcheance.Month, 1).AddMonths(1).AddDays(-1).Day;
                bool isAuthorized = false;
                foreach (int d in AuthorizedDays)
                {
                    if ((d == ted.DAteEcheance.Day) ||
                        ((d > maxday) && (maxday == ted.DAteEcheance.Day)))
                    {
                        isAuthorized = true;
                        break;
                    }
                }


                if ((!ted.AlreadyPayed) && (ted.ParPrelevement) && (!isAuthorized)) return false;
            }

            return true;
        }




        public static List<BaseTempEcheanceDefinition> getEcheancesInvisalign(List<BaseTempEcheanceDefinition> mte,
                                                               DateTime FirstEcheance,
                                                              DateTime? FinTraitement,
                                                               BaseTempEcheanceDefinition echOrigine)
        {

            List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();


            BaseTempEcheanceDefinition tmpdef = new BaseTempEcheanceDefinition();
                tmpdef.DAteEcheance = FirstEcheance.AddMonths(1);
                tmpdef.Libelle = "[ClinCheck]";
                tmpdef.Montant = echOrigine.Montant * .5f;
                tmpdef.acte = echOrigine.acte;
                _Montants.Add(tmpdef);

                tmpdef = new BaseTempEcheanceDefinition();
                tmpdef.DAteEcheance = FirstEcheance.AddMonths(4);
                tmpdef.Libelle = echOrigine.acte.Libelle + "[20%]";
                tmpdef.Montant = echOrigine.Montant * .2f;
                tmpdef.acte = echOrigine.acte;
                _Montants.Add(tmpdef);

                tmpdef = new BaseTempEcheanceDefinition();
                tmpdef.DAteEcheance = FirstEcheance.AddMonths(8);
                tmpdef.Libelle = echOrigine.acte.Libelle + "[20%]";
                tmpdef.Montant = echOrigine.Montant * .2f;
                tmpdef.acte = echOrigine.acte;
                _Montants.Add(tmpdef);

                tmpdef = new BaseTempEcheanceDefinition();
                tmpdef.DAteEcheance = FirstEcheance.AddMonths(12);
                tmpdef.Libelle = echOrigine.acte.Libelle + "[10%]";
                tmpdef.Montant = echOrigine.Montant * .1f;
                tmpdef.acte = echOrigine.acte;
                _Montants.Add(tmpdef);
            

            return _Montants;
        }




        public static List<BaseTempEcheanceDefinition> getEcheancesInvisalign(List<BaseTempEcheanceDefinition> mte,
                                                                DateTime FirstEcheance,
                                                               DateTime? FinTraitement,
                                                                List<ActePG> actes,
                                                                double TxMutuelle)
        {

            List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();


            foreach (ActePG acteInvisalign in actes)
            {
                double totalAreecheancer = acteInvisalign.Montant_Honoraire;
                if (TxMutuelle > 0) totalAreecheancer -= (totalAreecheancer * TxMutuelle);
                foreach (BaseTempEcheanceDefinition teda in mte)
                    if ((teda.AlreadyPayed) && (teda.acte.Id == acteInvisalign.Id))
                    {
                        _Montants.Add(teda);
                        totalAreecheancer -= teda.Montant;
                    }

                if ((totalAreecheancer / 10) < 0.05)
                    throw new Exception("Montant par echéance trop petit pour cette échéancier");


                DateTime stdte = acteInvisalign.DateExecution;
                DateTime endte = acteInvisalign.DateExecution;

                if (acteInvisalign.NbMois != null)
                    endte = acteInvisalign.DateExecution.AddMonths(acteInvisalign.NbMois.Value).AddDays(acteInvisalign.NbJours.Value);
                


                

                if (TxMutuelle > 0)
                {
                    BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                    ted.acte = acteInvisalign;
                    ted.AlreadyPayed = false;
                    ted.CanRecalculate = true;
                    ted.DAteEcheance = acteInvisalign.DateExecution.AddDays(15);
                    ted.Libelle = acteInvisalign.Libelle + " Part Mutuelle";
                    ted.Montant = (totalAreecheancer * TxMutuelle);
                    ted.ParPrelevement = false;
                    ted.ParVirement = false;
                    ted.payeur = Echeance.typepayeur.Mutuelle;
                    _Montants.Add(ted);
                }

                BaseTempEcheanceDefinition tmpdef = new BaseTempEcheanceDefinition();
                tmpdef.DAteEcheance = stdte.AddMonths(1);
                tmpdef.Libelle = "[ClinCheck]";
                tmpdef.Montant = totalAreecheancer * .5f;
                tmpdef.acte = acteInvisalign;
                _Montants.Add(tmpdef);

                tmpdef = new BaseTempEcheanceDefinition();
                tmpdef.DAteEcheance = endte;
                tmpdef.Libelle = acteInvisalign.Libelle + "[20%]";
                tmpdef.Montant = totalAreecheancer * .2f;
                tmpdef.acte = acteInvisalign;
                _Montants.Add(tmpdef);

                tmpdef = new BaseTempEcheanceDefinition();
                tmpdef.DAteEcheance = endte;
                tmpdef.Libelle = acteInvisalign.Libelle + "[20%]";
                tmpdef.Montant = totalAreecheancer * .2f;
                tmpdef.acte = acteInvisalign;
                _Montants.Add(tmpdef);

                tmpdef = new BaseTempEcheanceDefinition();
                tmpdef.DAteEcheance = endte;
                tmpdef.Libelle = acteInvisalign.Libelle + "[10%]";
                tmpdef.Montant = totalAreecheancer * .1f;
                tmpdef.acte = acteInvisalign;
                _Montants.Add(tmpdef);
            }
            
            return _Montants;
        }

        public static List<BaseTempEcheanceDefinition> getEcheances(List<BaseTempEcheanceDefinition> mte,
                                                                DateTime FirstEcheance,
                                                               double MontantParEcheance,
                                                               periodic periode,
                                                               int NbPeriodes,
                                                                List<ActePG> actes,
                                                                double TxMutuelle)
        {

            List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();

            DateTime start = FirstEcheance;
            
            foreach (ActePG acte in actes)
            {

                double totalAreecheancer = acte.Montant_Honoraire;

                foreach (BaseTempEcheanceDefinition teda in mte)
                    if ((teda.AlreadyPayed) && (teda.acte.Id == acte.Id))
                    {
                        _Montants.Add(teda);
                        totalAreecheancer -= teda.Montant;
                    }



                int i = _Montants.Count;



                List<BaseTempEcheanceDefinition> tmplst = getEcheances(start,
                                                                    MontantParEcheance,
                                                                    periode,
                                                                    NbPeriodes,
                                                                    totalAreecheancer,
                                                                    acte.Libelle,
                                                                    TxMutuelle,
                                                                    acte);


                foreach (BaseTempEcheanceDefinition tped in tmplst)
                {
                    tped.acte = acte;
                    tped.Libelle += (tped.payeur==Echeance.typepayeur.Mutuelle)?" Part Mutuelle ": " [Ech " + (i).ToString() + "]";
                    _Montants.Add(tped);
                    i++;
                }

                switch (periode)
                {
                    case periodic.Jour: start = start.AddDays(NbPeriodes * tmplst.Count); break;
                    case periodic.Semaine: start = start.AddDays(NbPeriodes * 7 * tmplst.Count); break;
                    case periodic.Mois: start = start.AddMonths(NbPeriodes * tmplst.Count);
                        break;
                }

            }

            return _Montants;
        }



        public static List<BaseTempEcheanceDefinition> getEcheances(List<BaseTempEcheanceDefinition> mte,
                                                                DateTime FirstEcheance,
                                                               double MontantParEcheance,
                                                               periodic periode,
                                                               int NbPeriodes,
                                                                BaseTempEcheanceDefinition echOrigine)
        {

            List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();

            DateTime start = FirstEcheance;


            double totalAreecheancer = echOrigine.Montant;




            int i = _Montants.Count;



            List<BaseTempEcheanceDefinition> tmplst = getEcheances(start,
                                                                MontantParEcheance,
                                                                periode,
                                                                NbPeriodes,
                                                                totalAreecheancer,
                                                                echOrigine.acte.Libelle,
                                                                echOrigine);


            foreach (BaseTempEcheanceDefinition tped in tmplst)
            {
                tped.acte = echOrigine.acte;
                tped.Libelle += " [Ech " + (i).ToString() + "]";
                _Montants.Add(tped);
                i++;
            }


            return _Montants;
        }




        public static List<BaseTempEcheanceDefinition> getEcheances5050(List<BaseTempEcheanceDefinition> mte,
                                                                DateTime FirstEcheance,
                                                                DateTime? LastEcheance,
                                                                BaseTempEcheanceDefinition echOrigine)
        {


            List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();


            

                double totalAreecheancer = echOrigine.Montant;
                

                
                double montant = totalAreecheancer / 2;

                if (montant < 0.05)
                    throw new Exception("Montant par echéance trop petit pour cette échéancier");

                DateTime stdte = echOrigine.acte.DateExecution;
                DateTime endte = echOrigine.acte.DateExecution;

                if (echOrigine.acte.NbMois != null)
                    endte = echOrigine.acte.DateExecution.AddMonths(echOrigine.acte.NbMois.Value).AddDays(echOrigine.acte.NbJours.Value);




                
                if ((endte != null) && (endte == stdte))
                {
                    BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                    ted.acte = echOrigine.acte;
                    ted.AlreadyPayed = false;
                    ted.CanRecalculate = true;
                    ted.DAteEcheance = stdte;
                    ted.Libelle = echOrigine.Libelle;
                    ted.Montant = echOrigine.Montant;
                    ted.ParPrelevement = false;
                    ted.ParVirement = false;
                    
                    _Montants.Add(ted);
                }
                else
                {


                    BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                    ted.acte = echOrigine.acte;
                    ted.AlreadyPayed = false;
                    ted.CanRecalculate = true;
                    ted.DAteEcheance = stdte;
                    ted.Libelle = echOrigine.Libelle + "[Ech 1 (50%)]";
                    ted.Montant = montant;
                    ted.ParPrelevement = false;
                    ted.ParVirement = false;
                    
                    _Montants.Add(ted);



                    ted = new BaseTempEcheanceDefinition();
                    ted.acte = echOrigine.acte;
                    ted.AlreadyPayed = false;
                    ted.CanRecalculate = true;
                    ted.DAteEcheance = endte;

                    ted.Libelle = echOrigine.Libelle + "[Ech 2 (50%)]";
                    ted.Montant = montant;
                    ted.ParPrelevement = false;
                    ted.ParVirement = false;
                    
                    _Montants.Add(ted);

                }

            

            return _Montants;
        }


        public static List<BaseTempEcheanceDefinition> getEcheances5050(List<BaseTempEcheanceDefinition> mte,
                                                                DateTime FirstEcheance,
                                                                DateTime? LastEcheance,
                                                                List<ActePG> actes,
                                                                double TxMutuelle)
        {


            List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();


            foreach (ActePG acte in actes)
            {

                double totalAreecheancer = acte.Montant_Honoraire;
                 if (TxMutuelle > 0) totalAreecheancer -= (totalAreecheancer * TxMutuelle);


                 foreach (BaseTempEcheanceDefinition teda in mte)
                    if ((teda.AlreadyPayed) && (teda.acte.Id == acte.Id))
                    {
                        _Montants.Add(teda);
                        totalAreecheancer -= teda.Montant;
                    }
                double montant = totalAreecheancer / 2;

                if (montant < 0.05)
                    throw new Exception("Montant par echéance trop petit pour cette échéancier");

                DateTime stdte = acte.DateExecution;
                DateTime endte = acte.DateExecution;

                if (acte.NbMois != null)
                    endte = acte.DateExecution.AddMonths(acte.NbMois.Value).AddDays(acte.NbJours.Value);


               

                if (TxMutuelle > 0)
                {
                    BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                    ted.acte = acte;
                    ted.AlreadyPayed = false;
                    ted.CanRecalculate = true;
                    ted.DAteEcheance = acte.DateExecution.AddMonths(acte.Template.NBMois == null ? 0 : acte.Template.NBMois.Value).AddDays(acte.Template.NBJours == null ? 0 : acte.Template.NBJours.Value);
                    ted.Libelle = acte.Libelle;
                    ted.Montant = (TxMutuelle * totalAreecheancer);
                    ted.payeur = Echeance.typepayeur.Mutuelle;
                    ted.ParPrelevement = false;
                    ted.ParVirement = false;
                    
                    _Montants.Add(ted);
                }
                if (TxMutuelle == 1) continue;

                if ((endte != null) && (endte == stdte))
                {
                    BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                    ted.acte = acte;
                    ted.AlreadyPayed = false;
                    ted.CanRecalculate = true;
                    ted.DAteEcheance = stdte;
                    ted.Libelle = acte.Libelle;
                    ted.Montant = acte.Montant_Honoraire;
                    ted.ParPrelevement = false;
                    ted.ParVirement = false;
                    
                    _Montants.Add(ted);
                }
                else
                {


                    BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                    ted.acte = acte;
                    ted.AlreadyPayed = false;
                    ted.CanRecalculate = true;
                    ted.DAteEcheance = stdte;
                    ted.Libelle = acte.Libelle + "[Ech 1 (50%)]";
                    ted.Montant = montant;
                    ted.ParPrelevement = false;
                    ted.ParVirement = false;
                    
                    _Montants.Add(ted);



                    ted = new BaseTempEcheanceDefinition();
                    ted.acte = acte;
                    ted.AlreadyPayed = false;
                    ted.CanRecalculate = true;
                    ted.DAteEcheance = endte;

                    ted.Libelle = acte.Libelle + "[Ech 2 (50%)]";
                    ted.Montant = montant;
                    ted.ParPrelevement = false;
                    ted.ParVirement = false;
                    
                    _Montants.Add(ted);

                }

            }

            return _Montants;
        }


        public static List<BaseTempEcheanceDefinition> getEcheancesByNb(List<BaseTempEcheanceDefinition> mte,
                                                               DateTime FirstEcheance,
                                                               int NbEch,
                                                               periodic periode,
                                                               int NbPeriodes,
                                                                BaseTempEcheanceDefinition EchOrigine)
        {

            int NbEcheances = NbEch;

            if (NbEcheances <= 0)
                throw new Exception("Nombre d'echeances trop petit");

            DateTime start = FirstEcheance;

            List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();



            double totalAreecheancer = EchOrigine.Montant;



                

                double totalAreecheancerAvantMut = totalAreecheancer;


               


                double montant = totalAreecheancer / NbEcheances;


                int orgcounter = _Montants.Count + 1;





                if (montant < 0.05)
                    throw new Exception("Montant restant trop petit pour cette échéancier");


                for (int i = 0; i < NbEcheances; i++)
                {

                    BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                    ted.acte = EchOrigine.acte;
                    ted.AlreadyPayed = false;
                    ted.CanRecalculate = true;
                    ted.DAteEcheance = start;
                    ted.Libelle = EchOrigine.Libelle + "[Ech " + (i + orgcounter).ToString() + "]";
                    ted.Montant = montant;
                    ted.ParPrelevement = false;
                    ted.ParVirement = false;
                    
                    _Montants.Add(ted);
                    switch (periode)
                    {
                        case periodic.Jour: start = FirstEcheance.AddDays(NbPeriodes * (i + 1)); break; //start = start.AddDays(NbPeriodes); break;
                        case periodic.Semaine: start = FirstEcheance.AddDays(NbPeriodes * 7 * (i + 1)); break; //start = start.AddDays(NbPeriodes * 7); break;
                        case periodic.Mois: start = FirstEcheance.AddMonths(NbPeriodes*(i+1)); //start.AddMonths(NbPeriodes);
                            break;
                    }



                }

            
            return _Montants;
        }

        public static List<BaseTempEcheanceDefinition> getEcheancesByNb(List<BaseTempEcheanceDefinition> mte,
                                                                DateTime FirstEcheance,
                                                                int NbEch,
                                                                periodic periode,
                                                                int NbPeriodes,
                                                                List<ActePG> actes,
                                                                double TxMutuelle)
        {

            int NbEcheances = NbEch / actes.Count;

            if (NbEcheances <= 0)
                throw new Exception("Nombre d'echeances trop petit");

            DateTime start = FirstEcheance;

            List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();

            foreach (ActePG acte in actes)
            {

                double totalAreecheancer = acte.Montant_Honoraire;



                foreach (BaseTempEcheanceDefinition teda in mte)
                    if ((teda.AlreadyPayed) && (teda.acte.Id == acte.Id))
                    {
                        _Montants.Add(teda);
                        totalAreecheancer -= teda.Montant;
                    }

                double totalAreecheancerAvantMut = totalAreecheancer;

               
                if (TxMutuelle > 0) 
                    totalAreecheancer -= (totalAreecheancer * TxMutuelle);


                
                double montant = totalAreecheancer / NbEcheances;

               
                int orgcounter = _Montants.Count + 1;


               

                if ((totalAreecheancerAvantMut > 0) && (TxMutuelle > 0)) 
                {
                    BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                    ted.acte = acte;
                    ted.AlreadyPayed = false;
                    ted.CanRecalculate = true;
                    ted.DAteEcheance = start;
                    ted.Libelle = acte.Libelle + " Part Mutuelle";
                    ted.Montant = (totalAreecheancerAvantMut * TxMutuelle);
                    ted.ParPrelevement = false;
                    ted.ParVirement = false;
                    ted.payeur = Echeance.typepayeur.Mutuelle;
                    _Montants.Add(ted);
                }

                if (TxMutuelle==1)  continue;

                if (montant < 0.05)
                    throw new Exception("Montant restant trop petit pour cette échéancier");


                for (int i = 0; i < NbEcheances; i++)
                {

                    BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                    ted.acte = acte;
                    ted.AlreadyPayed = false;
                    ted.CanRecalculate = true;
                    ted.DAteEcheance = start;
                    ted.Libelle = acte.Libelle + "[Ech " + (i + orgcounter).ToString() + "]";
                    ted.Montant = montant;
                    ted.ParPrelevement = false;
                    ted.ParVirement = false;
                    
                    _Montants.Add(ted);


                    switch (periode)
                    {
                        case periodic.Jour: start = FirstEcheance.AddDays(NbPeriodes * (i + 1)); break;
                        case periodic.Semaine: start = FirstEcheance.AddDays(NbPeriodes * 7 * (i + 1)); break;
                        case periodic.Mois: start = FirstEcheance.AddMonths(NbPeriodes * (i + 1));
                            break;
                    }

                    /*
                    switch (periode)
                    {
                        case periodic.Jour: start = start.AddDays(NbPeriodes); break;
                        case periodic.Semaine: start = start.AddDays(NbPeriodes * 7); break;
                        case periodic.Mois: start = start.AddMonths(NbPeriodes);
                            break;
                    }
                    */


                }

            }
            return _Montants;
        }

        public static List<BaseTempEcheanceDefinition> getEcheances(List<BaseTempEcheanceDefinition> mte,
                                                                DateTime FirstEcheance,
                                                                DateTime LastEcheance,
                                                                periodic periode,
                                                                int NbPeriodes,
                                                                List<ActePG> actes,
                                                                double TxMutuelle)
        {
            List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();

            TimeSpan ts = new TimeSpan((LastEcheance - FirstEcheance).Ticks);
            ts = new TimeSpan(ts.Ticks/actes.Count);
            DateTime start = FirstEcheance;

            foreach (ActePG acte in actes)
            {
                start = start.Date;
                double totalAreecheancer = acte.Montant_Honoraire;


                foreach (BaseTempEcheanceDefinition teda in mte)
                    if ((teda.AlreadyPayed) && (teda.acte.Id == acte.Id))
                    {
                        _Montants.Add(teda);
                        totalAreecheancer -= teda.Montant;
                    }



                
                
                int i = 1;


                DateTime endst = start.Add(ts);
                List<BaseTempEcheanceDefinition> tmplst = getEcheances(start,
                                                                    endst,
                                                                    periode,
                                                                    NbPeriodes,
                                                                    totalAreecheancer,
                                                                    acte.Libelle,
                                                                    TxMutuelle,
                                                                    acte);



                foreach (BaseTempEcheanceDefinition tped in tmplst)
                {
                    tped.acte = acte;
                    tped.Libelle = acte.Libelle+( tped.payeur == Echeance.typepayeur.Mutuelle ? " Part Mutuelle" : " [Ech " + (i).ToString() + "]");
                    _Montants.Add(tped);
                    i++;
                }


                switch (periode)
                {
                    case periodic.Jour: start = start.AddDays(NbPeriodes * tmplst.Count); break;
                    case periodic.Semaine: start = start.AddDays(NbPeriodes * 7 * tmplst.Count); break;
                    case periodic.Mois: start = start.AddMonths(NbPeriodes * tmplst.Count);
                        break;
                }



            }

            return _Montants;
        }



        public static List<BaseTempEcheanceDefinition> getEcheances(List<BaseTempEcheanceDefinition> mte,
                                                                DateTime FirstEcheance,
                                                                DateTime LastEcheance,
                                                                periodic periode,
                                                                int NbPeriodes,
                                                                BaseTempEcheanceDefinition EchOrigine)
        {
            List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();

            TimeSpan ts = new TimeSpan((LastEcheance - FirstEcheance).Ticks);
            DateTime start = FirstEcheance;

            
                start = start.Date;
                double totalAreecheancer = EchOrigine.Montant;

                            int i = 1;


                DateTime endst = start.Add(ts);
                List<BaseTempEcheanceDefinition> tmplst = getEcheances(start,
                                                                    endst,
                                                                    periode,
                                                                    NbPeriodes,
                                                                    EchOrigine.Libelle,
                                                                    EchOrigine);



                foreach (BaseTempEcheanceDefinition tped in tmplst)
                {
                    tped.acte = EchOrigine.acte;
                    tped.Libelle = EchOrigine.Libelle + (tped.payeur == Echeance.typepayeur.Mutuelle ? " Part Mutuelle" : " [Ech " + (i).ToString() + "]");
                    _Montants.Add(tped);
                    i++;
                }

                switch (periode)
                {
                    case periodic.Jour: start = start.AddDays(NbPeriodes * tmplst.Count); break;
                    case periodic.Semaine: start = start.AddDays(NbPeriodes * 7 * tmplst.Count); break;
                    case periodic.Mois: start = start.AddMonths(NbPeriodes * tmplst.Count);
                        break;
                }



            

            return _Montants;
        }


        public static List<BaseTempEcheanceDefinition> getEcheances(DateTime FirstEcheance,
                                                                DateTime LastEcheance,
                                                                periodic periode,
                                                                int NbPeriodes,
                                                                string lib,
                                                                BaseTempEcheanceDefinition echOrigine)
        {


            double MontantOrigine = echOrigine.Montant;




            List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();

            BaseTempEcheanceDefinition reliquat = null;

            DateTime startDte = FirstEcheance;
            double periodicite = NbPeriodes;





            if (startDte.Date < DateTime.Now.Date) throw new System.Exception("La premiere échéance est passé");
            double nbEch = 0;

            DateTime c = startDte.Date;

            int ic = 0;
            while (c < LastEcheance.Date)
            {
                nbEch++;

                switch (periode)
                {
                    case periodic.Jour: c = FirstEcheance.AddDays(NbPeriodes * (ic + 1)); break;
                    case periodic.Semaine: c = FirstEcheance.AddDays(NbPeriodes * 7 * (ic + 1)); break;
                    case periodic.Mois: c = FirstEcheance.AddMonths(NbPeriodes * (ic + 1));
                        break;
                }
                /*
                switch (periode)
                {
                    case periodic.Jour: c = c.AddDays(NbPeriodes); break;
                    case periodic.Semaine: c = c.AddDays(NbPeriodes * 7); break;
                    case periodic.Mois:
                        double nbdays = System.DateTime.DaysInMonth(c.Year, c.Month);
                        nbdays *= NbPeriodes;
                        c = c.AddDays(nbdays);
                        break;
                }
                 * */
                ic++;
            }
            // nbEch++;
            TimeSpan t = (LastEcheance.Date - startDte.Date);

            if (nbEch <= 1) throw new System.Exception("Aucune écheance");
            if (t.Ticks < 0) throw new System.Exception("La derniere échéance ne peut pas être avant la premiere ");


            _Montants.Clear();

            DateTime current = startDte.Date;
            Double MontantParEch = MontantOrigine / nbEch;


            MontantParEch = ((int)MontantParEch);
            Double Reste = MontantOrigine - (MontantParEch * (int)nbEch);




           

            if (MontantParEch < 0.05)
                throw new Exception("Montant par echéance trop petit pour cette échéancier");

            for (int i = 0; i < (int)nbEch; i++)
            {
                BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                ted.DAteEcheance = current;
                ted.Montant = MontantParEch;
                ted.Libelle = lib;
                ted.acte = echOrigine.acte;
                _Montants.Add(ted);


                switch (periode)
                {
                    case periodic.Jour: current = FirstEcheance.AddDays(NbPeriodes * (i + 1)); break;
                    case periodic.Semaine: current = FirstEcheance.AddDays(NbPeriodes * 7 * (i + 1)); break;
                    case periodic.Mois: current = FirstEcheance.AddMonths(NbPeriodes * (i + 1));
                        break;
                }

                /*
                switch (periode)
                {
                    case periodic.Jour: current = current.AddDays(NbPeriodes); break;
                    case periodic.Semaine: current = current.AddDays(NbPeriodes * 7); break;
                    case periodic.Mois:
                        double nbdays = System.DateTime.DaysInMonth(current.Year, current.Month);
                        nbdays *= NbPeriodes;
                        current = current.AddDays(nbdays);
                        break;
                }*/
            }

            /*
            if (Reste < (MontantOrigine / 2))
            {
                _Montants[_Montants.Count - 1].Montant += Reste;
            }
            else
             * */
            if (Reste > 0)
            {
                BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                ted.DAteEcheance = current;
                ted.Montant = Reste;
                ted.Libelle = lib;
                _Montants.Add(ted);
            }

            return _Montants;
        }


        public static List<BaseTempEcheanceDefinition> getEcheances(DateTime FirstEcheance,
                                                                DateTime LastEcheance,
                                                                periodic periode,
                                                                int NbPeriodes,
                                                                double MontantOrg,
                                                                string lib,
                                                                double TxMutuelle,
                                                                ActePG acte)
        {


            double MontantOrigine = MontantOrg;
            
            
            if (TxMutuelle > 0)
                MontantOrigine -= (MontantOrg * TxMutuelle);


            List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();

            BaseTempEcheanceDefinition reliquat = null;

            DateTime startDte = FirstEcheance;
            double periodicite = NbPeriodes;


          
           

            if (startDte.Date < DateTime.Now.Date) throw new System.Exception("La premiere échéance est passé");
            double nbEch = 0;

            DateTime c = startDte.Date;
            int ic = 0;
            while (c < LastEcheance.Date)
            {
                nbEch++;

                switch (periode)
                {
                    case periodic.Jour: c = FirstEcheance.AddDays(NbPeriodes * (ic + 1)); break; 
                    case periodic.Semaine: c = FirstEcheance.AddDays(NbPeriodes * 7 * (ic + 1)); break; 
                    case periodic.Mois: c = FirstEcheance.AddMonths(NbPeriodes * (ic + 1)); 
                        break;
                }
                ic++;
                /*
                switch (periode)
                {
                    case periodic.Jour: c = c.AddDays(NbPeriodes); break;
                    case periodic.Semaine: c = c.AddDays(NbPeriodes*7); break;
                    case periodic.Mois:
                        double nbdays = System.DateTime.DaysInMonth(c.Year, c.Month);
                        nbdays *= NbPeriodes;
                        c = c.AddDays(nbdays); 
                        break;
                }
                 * */
            }
           // nbEch++;
            TimeSpan t = (LastEcheance.Date - startDte.Date);

                if (nbEch <= 1) throw new System.Exception("Aucune écheance");
                if (t.Ticks < 0) throw new System.Exception("La derniere échéance ne peut pas être avant la premiere ");
            

            _Montants.Clear();

            DateTime current = startDte.Date;
            Double MontantParEch = MontantOrigine / nbEch;

          
            MontantParEch = ((int)MontantParEch);
            Double Reste = MontantOrigine - (MontantParEch * (int)nbEch);


           

            if ((MontantOrg>0) &&(TxMutuelle > 0)) 
            {
                TempEcheanceDefinition ted = new TempEcheanceDefinition();
                ted.DAteEcheance = acte.DateExecution.AddMonths(acte.Template.NBMois == null ? 0 : acte.Template.NBMois.Value).AddDays(acte.Template.NBJours == null ? 0 : acte.Template.NBJours.Value);
                ted.Montant = (MontantOrg * TxMutuelle);
                ted.Libelle = lib + " Part Mutuelle";
                ted.payeur = Echeance.typepayeur.Mutuelle;
                ted.acte = acte;
                _Montants.Add(ted);
            }

            if (TxMutuelle == 1) return _Montants;

            if (MontantParEch < 0.05)
                throw new Exception("Montant par echéance trop petit pour cette échéancier");

            for (int i = 0; i < (int)nbEch; i++)
            {
                TempEcheanceDefinition ted = new TempEcheanceDefinition();
                ted.DAteEcheance = current;
                ted.Montant = MontantParEch;
                ted.Libelle = lib;
                ted.acte = acte;
                _Montants.Add(ted);


                switch (periode)
                {
                    case periodic.Jour: current = FirstEcheance.AddDays(NbPeriodes * (i + 1)); break; //start = start.AddDays(NbPeriodes); break;
                    case periodic.Semaine: current = FirstEcheance.AddDays(NbPeriodes * 7 * (i + 1)); break; //start = start.AddDays(NbPeriodes * 7); break;
                    case periodic.Mois: current = FirstEcheance.AddMonths(NbPeriodes * (i + 1)); //start.AddMonths(NbPeriodes);
                        break;
                }

                /*
                switch (periode)
                {
                    case periodic.Jour: current = current.AddDays(NbPeriodes); break;
                    case periodic.Semaine: current = current.AddDays(NbPeriodes * 7); break;
                    case periodic.Mois:
                        double nbdays = System.DateTime.DaysInMonth(current.Year, current.Month);
                        nbdays *= NbPeriodes;
                        current = current.AddDays(nbdays);
                        break;
                }
                 */
            }

            /*
            if (Reste < (MontantOrigine / 2))
            {
                _Montants[_Montants.Count - 1].Montant += Reste;
            }
            else
             * */
            if (Reste>0)
            {
                TempEcheanceDefinition ted = new TempEcheanceDefinition();
                ted.DAteEcheance = current;
                ted.Montant = Reste;
                ted.Libelle = lib;
                _Montants.Add(ted);
            }

            return _Montants;
        }

        public static List<BaseTempEcheanceDefinition> getEcheances(DateTime FirstEcheance,
                                                                double MontantParEcheance,
                                                                periodic periode,
                                                                int NbPeriodes,
                                                                double MontantOrg,
                                                                string lib,
                                                                double TxMutuelle,
                                                                ActePG acte)
        {

            double MontantOrigine = MontantOrg;

           
            if ((TxMutuelle > 0)) 
                MontantOrigine -= (MontantOrigine * TxMutuelle);

            List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();

            DateTime startDte = FirstEcheance;

            
            if (startDte.Date < DateTime.Now.Date) throw new System.Exception("La premiere échéance est passé");
            double nbEch = 0;
            DateTime current = startDte;


            

            if ((MontantOrg>0) &&(TxMutuelle > 0)) 
            {
                BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                ted.DAteEcheance = acte.DateExecution.AddMonths(acte.Template.NBMois == null ? 0 : acte.Template.NBMois.Value).AddDays(acte.Template.NBJours == null ? 0 : acte.Template.NBJours.Value);
                ted.Montant = (MontantOrg * TxMutuelle);
                ted.Libelle = lib + " Part Mutuelle";
                ted.acte = acte;
                ted.payeur = Echeance.typepayeur.Mutuelle;
                _Montants.Add(ted);
            }


                if ((TxMutuelle == 1) && (acte.Template.Code.Valeur != 0)) return _Montants;
                nbEch = (MontantOrigine / MontantParEcheance);

                if (MontantParEcheance <= 1) throw new System.Exception("Montant par échéance trop petit");
                if (MontantParEcheance > (MontantOrigine / 2)) throw new System.Exception("Montant par échéance trop grand");
            
            

            _Montants.Clear();

            Double MontantParEch = MontantOrigine / nbEch;
            MontantParEch = ((int)MontantParEch);
            Double Reste = MontantOrigine - (MontantParEch * (int)nbEch);

           

            for (int i = 0; i < (int)nbEch; i++)
            {
                BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                ted.DAteEcheance = current;
                ted.Montant = MontantParEch;
                ted.Libelle = lib;
                ted.acte = acte;
                _Montants.Add(ted);


                switch (periode)
                {
                    case periodic.Jour: current = FirstEcheance.AddDays(NbPeriodes * (i + 1)); break; 
                    case periodic.Semaine: current = FirstEcheance.AddDays(NbPeriodes * 7 * (i + 1)); break; 
                    case periodic.Mois: current = FirstEcheance.AddMonths(NbPeriodes * (i + 1)); 
                        break;
                }
                /*
                switch (periode)
                {
                    case periodic.Jour: current = current.AddDays(NbPeriodes); break;
                    case periodic.Semaine: current = current.AddDays(NbPeriodes * 7); break;
                    case periodic.Mois: 
                        double nbdays = System.DateTime.DaysInMonth(current.Year, current.Month);
                        nbdays *= NbPeriodes;
                        current = current.AddDays(nbdays);
                        break;

                }
                */
            }
            /*
            if (Reste < (MontantOrigine / 2))
            {
                _Montants[_Montants.Count - 1].Montant += Reste;
            }
            else
             */
            if (Reste > 0)
            {
                BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                ted.DAteEcheance = current;
                ted.Montant = Reste;
                ted.Libelle = lib;
                _Montants.Add(ted);
            }

            return _Montants;
        }


        public static List<BaseTempEcheanceDefinition> getEcheances(DateTime FirstEcheance,
                                                                  double MontantParEcheance,
                                                                  periodic periode,
                                                                  int NbPeriodes,
                                                                  double MontantOrg,
                                                                  string lib,
                                                                  BaseTempEcheanceDefinition echOrigine)
        {

            double MontantOrigine = MontantOrg;



            List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();

            DateTime startDte = FirstEcheance;


            if (startDte.Date < DateTime.Now.Date) throw new System.Exception("La premiere échéance est passé");
            double nbEch = 0;
            DateTime current = startDte;


            nbEch = (MontantOrigine / MontantParEcheance);

            if (MontantParEcheance <= 1) throw new System.Exception("Montant par échéance trop petit");
            if (MontantParEcheance > (MontantOrigine / 2)) throw new System.Exception("Montant par échéance trop grand");



            _Montants.Clear();

            Double MontantParEch = MontantOrigine / nbEch;
            MontantParEch = ((int)MontantParEch);
            Double Reste = MontantOrigine - (MontantParEch * (int)nbEch);



            for (int i = 0; i < (int)nbEch; i++)
            {
                BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                ted.DAteEcheance = current;
                ted.Montant = MontantParEch;
                ted.Libelle = lib;
                ted.acte = echOrigine.acte;
                _Montants.Add(ted);



                switch (periode)
                {
                    case periodic.Jour: current = FirstEcheance.AddDays(NbPeriodes * (i + 1)); break;
                    case periodic.Semaine: current = FirstEcheance.AddDays(NbPeriodes * 7 * (i + 1)); break;
                    case periodic.Mois: current = FirstEcheance.AddMonths(NbPeriodes * (i + 1));
                        break;
                }
                /*
                switch (periode)
                {
                    case periodic.Jour: current = current.AddDays(NbPeriodes); break;
                    case periodic.Semaine: current = current.AddDays(NbPeriodes * 7); break;
                    case periodic.Mois:
                        double nbdays = System.DateTime.DaysInMonth(current.Year, current.Month);
                        nbdays *= NbPeriodes;
                        current = current.AddDays(nbdays);
                        break;

                }
                */
            }
            /*
            if (Reste < (MontantOrigine / 2))
            {
                _Montants[_Montants.Count - 1].Montant += Reste;
            }
            else
             */
            if (Reste > 0)
            {
                BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                ted.DAteEcheance = current;
                ted.Montant = Reste;
                ted.Libelle = lib;
                _Montants.Add(ted);
            }

            return _Montants;
        }



        public static bool RemoveAndRecalculate(List<BaseTempEcheanceDefinition> lst, BaseTempEcheanceDefinition tedToDelete)
    {

        List<BaseTempEcheanceDefinition> internallst = new List<BaseTempEcheanceDefinition>();
        foreach (BaseTempEcheanceDefinition ted in lst)
        {
            if ((((ted.acte.Id > 0) && (ted.acte.Id.Equals(tedToDelete.acte.Id))) || (tedToDelete.acte == ted.acte)) && (ted.CanRecalculate) && (ted != tedToDelete))
                    internallst.Add(ted);
        }



        if (internallst.Count < 1) return false;

        List<double> lstpercentage = new List<double>();
        double Total = 0;

        foreach (BaseTempEcheanceDefinition ted in internallst)
            Total += ted.Montant;


        for (int i = 0; i < internallst.Count; i++)
                lstpercentage.Add((internallst[i].Montant) / Total);


        for (int i = 0; i < internallst.Count; i++)
            internallst[i].Montant += (tedToDelete.Montant * lstpercentage[i]);


        lst.Remove(tedToDelete);

        
        return true;
    }

        public static void RemoveAllAndRecalculate(List<BaseTempEcheanceDefinition> lst,NewTraitement.typeScenario type = NewTraitement.typeScenario.Prothése)
    {     
        List<BaseTempEcheanceDefinition> internallst = new List<BaseTempEcheanceDefinition>();
        Dictionary<int, double> dico = new Dictionary<int, double>();
        Dictionary<int, ActePG> dicoactes = new Dictionary<int, ActePG>();
        DateTime TmpDate = new DateTime();
        TmpDate = lst.Min(a => a.DAteEcheance);
        foreach (BaseTempEcheanceDefinition ted in lst)
        {

            if ((System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + DAC.prefix] == "FR") && type != NewTraitement.typeScenario.Prothése_CMUC)
            {
                if (ted.AlreadyPayed || ted.payeur == Echeance.typepayeur.Mutuelle || ted.payeur == Echeance.typepayeur.Secu)
                {
                    internallst.Add(ted);
                }
                else
                {

                    int hscde = ted.acte.Id != -1 ? ted.acte.Id : ted.acte.GetHashCode();
                    if (!dico.ContainsKey(hscde))
                        dico.Add(hscde, ted.Montant);
                    else
                        dico[hscde] += ted.Montant;

                    if (!dicoactes.ContainsKey(hscde))
                        dicoactes.Add(hscde, ted.acte);
                }
            }
            else
            {
                if (ted.AlreadyPayed)
                {

                    internallst.Add(ted);

                }
                else
                {


                    int hscde = ted.acte.Id != -1 ? ted.acte.Id : ted.acte.GetHashCode();
                    if (!dico.ContainsKey(hscde))
                        dico.Add(hscde, ted.Montant);
                    else
                        dico[hscde] += ted.Montant;

                    if (!dicoactes.ContainsKey(hscde))
                        dicoactes.Add(hscde, ted.acte);

                }
            }
        }


        foreach (KeyValuePair<int, double> kv in dico)
        {
            BaseTempEcheanceDefinition td = new BaseTempEcheanceDefinition();
            td.Libelle = dicoactes[kv.Key].Libelle;
            td.Montant = kv.Value;

            td.acte = dicoactes[kv.Key];
            
            if (dicoactes[kv.Key].Semestre == 0)
            {
                td.DAteEcheance = TmpDate;
            }
            else
            {
                td.DAteEcheance = (dicoactes[kv.Key].NbMois == null || dicoactes[kv.Key].NbMois < 0) ? dicoactes[kv.Key].DateExecution : dicoactes[kv.Key].DateExecution.AddMonths(dicoactes[kv.Key].NbMois.Value).AddDays(dicoactes[kv.Key].NbJours.Value);
            }
            internallst.Add(td);
        }


        lst.Clear();

        foreach (BaseTempEcheanceDefinition ted in internallst)
            lst.Add(ted);
        
    }


    public static void AffectEncaissementToEcheance(Echeance echeance)
    {
        LogMgmt.AssocierALencaissement(echeance.IdPatient, echeance, echeance.encaissement);
        DAC.AffectEncaissementToEcheance(echeance);
    }

       

    public static void AddAndRecalculate(List<BaseTempEcheanceDefinition> lst, BaseTempEcheanceDefinition tedToAdd,NewTraitement.typeScenario type = NewTraitement.typeScenario.Prothése)
    {



        //            - si ajoute un troisième voir une quatrième, voir plus d'échéances manuellement la somme se déduit automatiquement sur la base de la première échéance.

        //- La somme de base ne peut pas être négative mais elle peut être égale à 0.
        List<BaseTempEcheanceDefinition> internallst = new List<BaseTempEcheanceDefinition>();

        List<double> lstpercentage = new List<double>();
        double Total = 0;
        foreach (BaseTempEcheanceDefinition ted in lst)
        {

            if ((ted.CanRecalculate) && (ted.acte.Id == tedToAdd.acte.Id) &&(System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + DAC.prefix] == "FR") && type != NewTraitement.typeScenario.Prothése_CMUC)             
            {
                if (ted.payeur != Echeance.typepayeur.Mutuelle || ted.payeur == Echeance.typepayeur.Secu)
                {

                    Total += ted.Montant;
                    internallst.Add(ted);
                }
            }
            else

            if ((ted.CanRecalculate) && (ted.acte.Id==tedToAdd.acte.Id))
            {
                Total += ted.Montant;
                internallst.Add(ted);
            }
        }

        if (tedToAdd.Montant <= 0) return;
        if (Total <= 0) return;


        for (int i = 0; i < internallst.Count; i++)
            lstpercentage.Add((internallst[i].Montant) / Total);

        double tmpMontant = tedToAdd.Montant;
        for (int i = 0; i < internallst.Count; i++)
        {
            if (tmpMontant <= 0) break;

            if (internallst[i].Montant <= tmpMontant)
            {
                tmpMontant -= internallst[i].Montant;
                internallst[i].Montant = 0;
            }
            else
            {
                internallst[i].Montant -= tmpMontant;
                tmpMontant -= tedToAdd.Montant;
            }
        //    internallst[i].Montant -= (tedToAdd.Montant * lstpercentage[i]);
            if (internallst[i].Montant <= 0)
            {
        
                lst.Remove(internallst[i]);
                
            }
        }

        double total = 0;
        foreach (BaseTempEcheanceDefinition ted in internallst)
            total += ted.Montant;

        if (total < 0) 
            tedToAdd.Montant += total;

        lst.Add(tedToAdd);

        lst = lst.OrderBy(e => e.DAteEcheance).ThenBy(e => e.DAteEcheance.TimeOfDay).ToList();

        lst.Sort();

 
    }



    public static bool ModifyAndRecalculate(List<BaseTempEcheanceDefinition> lst, double montantorigin, BaseTempEcheanceDefinition tedToModify)
    {

        List<BaseTempEcheanceDefinition> internallst = new List<BaseTempEcheanceDefinition>();

        List<double> lstpercentage = new List<double>();
        double Total = 0;

        foreach (BaseTempEcheanceDefinition ted in lst)
        {
            if ((ted.acte.Equals(tedToModify.acte)) && (ted.CanRecalculate) && (ted != tedToModify))
            {
                Total += ted.Montant;
                internallst.Add(ted);
            }
        }

        if (tedToModify.Montant >= (Total+montantorigin)) return false;
        if (tedToModify.Montant <= 0) return false;

        double montantToSplit = montantorigin - tedToModify.Montant;


        for (int i = 0; i < internallst.Count; i++)
            lstpercentage.Add((internallst[i].Montant) / Total);


        for (int i = 0; i < internallst.Count; i++)
            internallst[i].Montant += (montantToSplit * lstpercentage[i]);

        lst.Sort();
        return true;
        /*
        int echnum = 1;
        foreach (TempEcheanceDefinition ted in internallst)
        {
            string rootlib = ted.Libelle.IndexOf("[")==-1?"":ted.Libelle.Substring(0, ted.Libelle.IndexOf("["));
            ted.Libelle = rootlib + " [Ech " + echnum.ToString() + " ]";
            echnum++;
        }
        */
    }


    public static void UpdateEcheanceF(Echeance ech)
    {
        DAC.UpdateEcheanceF(ech);
    }

    }
}
