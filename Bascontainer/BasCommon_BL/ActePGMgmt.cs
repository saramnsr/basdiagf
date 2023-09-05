using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class ActesPGMgmt
    {




        public static ActePG CreateActeFromSurveillance(Surveillance su)
        {
            ActePG apg = new ActePG();
            apg.NumSemestre = su.Semestre==null?0:su.Semestre.NumSemestre;
            apg.Template = su.traitementSecu;
            apg.IdSurvPlanGestionAssocie = su.Id;

            apg.DateExecution = su.DateDebut.Value;
            apg.prestation = su.traitementSecu.Code;
            apg.patient = su.Semestre.Parent.Parent.patient;
            if ((su.traitementSecu.NBMois == null) || (su.traitementSecu.NBMois.Value < 0))
            {
                apg.NbJours = (int)((su.DateFin.Value - su.DateDebut.Value).TotalDays);
                apg.NbMois = 0;

            }
            else
            {
                apg.NbMois = su.traitementSecu.NBMois;
                apg.NbJours = su.traitementSecu.NBJours;
            }
            apg.Montant_Honoraire = su.Montant_Honoraire;
            apg.Libelle = su.traitementSecu.Libelle;
            return apg;
        }

        public static ActePG CreateActeFromSemestre(Semestre s)
        {
            ActePG apg = new ActePG();
            apg.NumSemestre = s.NumSemestre;
            apg.Template = s.traitementSecu;
            apg.Semestre = s.Id;
            apg.DateExecution = s.DateDebut;
            if (s.traitementSecu != null)
            {
                apg.prestation = s.traitementSecu.Code;
                if ((s.traitementSecu.NBMois == null) || (s.traitementSecu.NBMois.Value < 0))
                {
                    apg.NbJours = (int)((s.DateFin - s.DateDebut).TotalDays);
                    apg.NbMois = 0;

                }
                else
                {
                    apg.NbMois = s.traitementSecu.NBMois;
                    apg.NbJours = s.traitementSecu.NBJours;
                }
                apg.Libelle = s.traitementSecu.Libelle;
            }
            else
            {
                apg.Libelle = s.CodeSemestre;
            }
            if (s.Parent != null)
            apg.patient = s.Parent.Parent.patient;
           
            apg.Montant_Honoraire = s.Montant_Honoraire;
           
            return apg;
        }
 
     
        public static void AffectEchanceToEncaissement(Echeance ech, Encaissement enc)
        {
            DAC.AffectEchanceToEncaissement(ech, enc);
        }

        public static List<ActePG> GetActesPG(Semestre s)
        {

            List<ActePG> _listActe = new List<ActePG>();


            DataTable dt = DAC.getActesPG(s);

            _listActe = new List<ActePG>();

            foreach (DataRow r in dt.Rows)
            {
                ActePG acte = Builders.BuildActePG.Build(r);                
                _listActe.Add(acte);
            }

            return _listActe;

        }


        public static List<ActePG> GetActesPG(basePatient pat)
        {

             List<ActePG> _listActe = new List<ActePG>();

             JArray obj = BasCommon_DAL.DAC.getMethodeJsonArray("/getActesPGByPAtient/" + pat.Id);

             foreach (JObject t in obj)
             {

                 ActePG ac = Builders.BuildActePG.BuildJ(t);
                 _listActe.Add(ac);
             }
                //DataTable dt = DAC.getActesPGByPAtient(pat.Id);

                //_listActe = new List<ActePG>();

                //foreach (DataRow r in dt.Rows)
                //{
                //    ActePG acte = Builders.BuildActePG.Build(r);
                //    /*
                //    if (acte.Id_DEP > 0)
                //    {

                //        EntentePrealable ep = MgmtDemandeEntente.GetEntente(acte.Id_DEP, pat.Id);
                //        if (ep != null)
                //        {
                //            ep.idPatient = pat.Id;
                //            acte.DEPAssocier = ep;
                //        }
                //    }
                //     */
                //    _listActe.Add(acte);
                //}

                return _listActe;

        }
            public static List<ActePG> GetActesPGByIdDevis(int id)
        {

            List<ActePG> _listActe = new List<ActePG>();
             DataTable dt = DAC.getActesPGByIdDevis(id);

             foreach (DataRow r in dt.Rows)
             {
                 ActePG acte = Builders.BuildActePG.Build(r);
                 _listActe.Add(acte);
             }

             return _listActe;

        }
        
        
        /*
        public static List<ActePG> GetActesPG(Semestre s)
        {
            List<ActePG> lst = GetActesPG
            foreach (ActePG actepg in CachesMgmt.GetActesPG(s.Parent.Parent.IdPatient, true))
            {
                if (actepg.IdSemestrePlanGestionAssocie == s.Id)
                    lst.Add(actepg);

            }

            return lst;
        }

        public static List<ActePG> GetActesPG(Surveillance s)
        {
            List<ActePG> lst = new List<ActePG>();
            foreach (ActePG actepg in CachesMgmt.GetActesPG(s.Semestre.Parent.Parent.IdPatient, true))
            {
                if (actepg.IdSurvPlanGestionAssocie == s.Id)
                    lst.Add(actepg);

            }

            return lst;
        }
        */
        

        public static ActePG GetActesPG(int id)
        {
           
            DataRow dr = DAC.getActesPG(id);
            if (dr == null) return null;
            ActePG acte = Builders.BuildActePG.Build(dr);

            return acte;
        }
        public static ActePG GetActesPGByIdMateriel(int idComm,int idMateriel)
        {

            DataRow dr = DAC.getActesPGByCommentMateriel(idComm, idMateriel );
            if (dr == null) return null;
            ActePG acte = Builders.BuildActePG.Build(dr);

            return acte;
        }
        public static ActePG GetActesPGByIdComm(int idComm)
        {

            DataRow dr = DAC.getActesPGByComment (idComm );
            if (dr == null) return null;
            ActePG acte = Builders.BuildActePG.Build(dr);

            return acte;
        }


        public static double GetSolde(Semestre s)
        {


            return DAC.getSolde(s);
        }


        public static double GetSolde(Surveillance s)
        {


            return DAC.getSolde(s);
        }

        public static double GetSolde(ActePG acte)
        {
            return DAC.getSolde(acte);
        }

        public static ActePG GetCurrentActePG(basePatient pat)
        {

            if (pat.ActesPG == null)
                pat.ActesPG = GetActesPG(pat);
            if (pat.ActesPG.Count > 0)
                return pat.ActesPG.Find(x => (x.NbMois == null || x.DateExecution.AddDays(x.NbJours.Value).AddMonths(x.NbMois.Value) > DateTime.Now) && (x.DateExecution < DateTime.Now));
        /*    foreach (ActePG actepg in pat.ActesPG)
            {
                if (((actepg.NbMois == null) ||
               (actepg.DateExecution.AddDays(actepg.NbJours.Value).AddMonths(actepg.NbMois.Value) > DateTime.Now)) &&
               (actepg.DateExecution < DateTime.Now))
                    return actepg;

            }
            */


            return null;


        }

        public static void RemoveDEPReference(int IdDEP)
        {
            DAC.RemoveDEPReference(IdDEP);
        }

        public static void AddDEPReference(EntentePrealable ep, ActePG acte)
        {
            DAC.AddDEPReference(ep.IdModele, acte.Id);
            acte.Id_DEP = ep.IdModele;
            acte.DEPAssocier = ep;
        }


        public static DateTime? getDateFinTraitement(basePatient pat)
        {

            DateTime? dte = null;
            if (pat.ActesPG == null)
                pat.ActesPG = GetActesPG(pat);
            foreach (ActePG actepg in pat.ActesPG)
            {
                DateTime? dteFin = actepg.NbMois == null ? null : (DateTime?)actepg.DateExecution.AddDays(actepg.NbJours.Value).AddMonths(actepg.NbMois.Value);

                if (dteFin == null) return null;

                if ((dte == null) || (dte.Value < dteFin))
                    dte = dteFin;

            }
            return dte;


        }

        public static ActePG GetNextActe(basePatient pat)
        {

            if (pat.ActesPG == null)
                pat.ActesPG = GetActesPG(pat);

            ActePG selected = null;
            DateTime dtemin = DateTime.MaxValue;
            if(pat.ActesPG.Count > 0)
                return pat.ActesPG.OrderBy(a => a.DateExecution > DateTime.Now).First();
          /*  foreach (ActePG e in pat.ActesPG)
            {
                if ((e.DateExecution > DateTime.Now) && (e.DateExecution < dtemin))
                {
                    dtemin = e.DateExecution;
                    selected = e;
                }
            }*/

            return selected;
        }

        public static ActePG GetNextActeWithFS(basePatient pat)
        {
            if (pat.ActesPG == null)
                pat.ActesPG = GetActesPG(pat);

            ActePG selected = null;
            DateTime dtemin = DateTime.MaxValue;
            if (pat.ActesPG.Count > 0)
                return pat.ActesPG.OrderBy(a => a.DateExecution > DateTime.Now && a.Id_FS < 0 && a.NeedFSE).First();
       
          /*  foreach (ActePG e in pat.ActesPG)
            {
                if ((e.Id_FS < 0) && (e.NeedFSE) && (e.DateExecution > DateTime.Now) && (e.DateExecution < dtemin))
                {
                    dtemin = e.DateExecution;
                    selected = e;
                }
            }
            */
            return selected;


        }

        public static ActePG GetNextActeWithDEP(basePatient pat)
        {
            if (pat.ActesPG == null)
                pat.ActesPG = GetActesPG(pat);

            ActePG selected = null;
            DateTime dtemin = DateTime.MaxValue;

            if (pat.ActesPG.Count > 0)
                return pat.ActesPG.OrderBy(a => a.DateExecution > DateTime.Now && a.Id_DEP < 0 && a.NeedDEP).First();
      
/*
            foreach (ActePG e in pat.ActesPG)
            {
                if ((e.Id_DEP < 0) && (e.NeedDEP) && (e.DateExecution > DateTime.Now) && (e.DateExecution < dtemin))
                {
                    dtemin = e.DateExecution;
                    selected = e;
                }
            }*/

            return selected;


        }


        public static ActePG GetCurrentActeWithDEP(basePatient pat)
        {

            if (pat.ActesPG == null)
                pat.ActesPG = GetActesPG(pat);

            if (pat.ActesPG.Count > 0)
                return pat.ActesPG.Find(a => a.DateExecution < DateTime.Now && a.Id_DEP < 0 && a.NeedDEP);
      /*

            foreach (ActePG actepg in pat.ActesPG)
            {

                if ((actepg.NeedDEP) && (actepg.Id_DEP <= 0) && (actepg.DateExecution < DateTime.Now))
                    return actepg;

            }
            */


            return null;


        }

        public static ActePG GetCurrentActeWithFS(basePatient pat)
        {

            if (pat.ActesPG == null)
                pat.ActesPG = GetActesPG(pat);

            if (pat.ActesPG.Count > 0)
                return pat.ActesPG.Find(a => (a.NbMois == null || (a.DateExecution.AddDays(a.NbJours.Value).AddMonths(a.NbMois.Value) > DateTime.Now)) &&
               (a.DateExecution < DateTime.Now) &&
                    (a.NeedFSE) &&
                    (a.Id_FS < 0));

       /*     foreach (ActePG actepg in pat.ActesPG)
            {
                if (((actepg.NbMois == null) ||
               (actepg.DateExecution.AddDays(actepg.NbJours.Value).AddMonths(actepg.NbMois.Value) > DateTime.Now)) &&
               (actepg.DateExecution < DateTime.Now) &&
                    (actepg.NeedFSE) &&
                    (actepg.Id_FS < 0))
                    return actepg;

            }

            */

            return null;


        }


        public static DateTime? getDateDebutTraitement(basePatient pat)
        {
            DateTime? dte = null;
            if (pat.ActesPG == null)
                pat.ActesPG = GetActesPG(pat);
            if (pat.ActesPG.Count > 0)
                return pat.ActesPG.Min(a => a.DateExecution);
            /*    foreach (ActePG actepg in pat.ActesPG)
            {
                if ((dte == null) || (actepg.DateExecution < dte))
                    dte = actepg.DateExecution;

            }
            return dte;*/

            return null;
        }


        public static string GetActesFormatPyxVital(List<ActePG> lst, bool AccidentDroitCommun, bool IsALD)
        {
            string resultat = "[Prestation]\r\n";
            string tmp = "";

            resultat += "Date=";
            foreach (ActePG acte in lst)
            {
                DateTime dte = acte.DateExecution.AddMonths(acte.NbMois.Value).AddDays(acte.NbJours.Value);
                if (tmp != "") tmp += "+";
                tmp += dte.ToString("dd/MM/yy");
            }

                resultat += tmp + "\r\n";


            /*
            resultat += "Prescription=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";
                tmp += acte.DateExecution.ToString("dd/MM/yy");
            }
            resultat += tmp + "\r\n";
            */


            tmp = "";

            resultat += "Quantite=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";
                tmp += acte.Quantite <= 0 ? "1" : acte.Quantite.ToString();
            }
            resultat += tmp + "\r\n";


            //Format NGAP
            tmp = "";
            resultat += "Code=";
            foreach (ActePG acte in lst)
            {

                double depassement = acte.Montant_Honoraire - (acte.Template.Coeff * acte.prestation.Valeur * acte.Quantite);


                if (tmp != "") tmp += "+";
                tmp += acte.prestation.Code;

            }
            resultat += tmp + "\r\n";



            //Format CCAM
            //resultat += "Code=CCAM\r\n";
            //resultat += "Code_CCAM=" +  + "\r\n";
            //resultat += "Code_extdoc_CCAM=" +  + "\r\n";
            //resultat += "Code_compl_CCAM=" +  + "\r\n";
            //resultat += "Modificateurs_CCAM=" +  + "\r\n";
            //resultat += "Code_suppl_CCAM=" +  + "\r\n";


            tmp = "";
            resultat += "Coefficient=";
            foreach (ActePG acte in lst)
            {

                double depassement = acte.Montant_Honoraire - (acte.Template.Coeff * acte.prestation.Valeur * acte.Quantite);


                if (tmp != "") tmp += "+";
                tmp += acte.Template.Coeff.ToString();



            }
            resultat += tmp + "\r\n";


            //resultat += "Acte_multiple" +  + "\r\n";


            tmp = "";
            resultat += "RMO=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";

                switch (acte.rno)
                {
                    case PyxVitalWrapperConst.RNO.R: tmp += "R"; break;
                    case PyxVitalWrapperConst.RNO.HR: tmp += "HR"; break;
                    case PyxVitalWrapperConst.RNO.Néant: tmp += "Néant"; break;
                }

            }
            resultat += tmp + "\r\n";




            tmp = "";
            resultat += "Montant_honoraires=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";
                tmp += acte.Montant_Honoraire.ToString("####.00", System.Globalization.CultureInfo.InvariantCulture);
            }
            resultat += tmp + "\r\n";




            tmp = "";
            resultat += "Qualificatif_depense=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";


                switch (acte.motifdepassement)
                {
                    case PyxVitalWrapperConst.Qualificatif_depense.D: tmp += "D"; break;
                    case PyxVitalWrapperConst.Qualificatif_depense.E: tmp += "E"; break;
                    case PyxVitalWrapperConst.Qualificatif_depense.F: tmp += "F"; break;
                    case PyxVitalWrapperConst.Qualificatif_depense.G: tmp += "G"; break;
                    case PyxVitalWrapperConst.Qualificatif_depense.N: tmp += "N"; break;
                    case PyxVitalWrapperConst.Qualificatif_depense.Néant: tmp += "Néant"; break;
                }


            }
            resultat += tmp + "\r\n";




            tmp = "";
            resultat += "Domicile=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";
                tmp += acte.domicile == PyxVitalWrapperConst.Domicile.O ? "O" : "N";
            }
            resultat += tmp + "\r\n";



            resultat += "Accident=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";
                tmp += (acte.accident == PyxVitalWrapperConst.Accident.O) || AccidentDroitCommun ? "O" : "N";
            }
            resultat += tmp + "\r\n";






            tmp = "";
            resultat += "D_JF=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";
                tmp += acte.DimancheEtJF == PyxVitalWrapperConst.DimancheEtJF.O ? "O" : "N";
            }
            resultat += tmp + "\r\n";

            tmp = "";
            resultat += "Nuit=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";
                tmp += acte.nuit == PyxVitalWrapperConst.Nuit.O ? "O" : "N";
            }
            resultat += tmp + "\r\n";

            tmp = "";
            resultat += "Urgence=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";
                tmp += acte.urgence == PyxVitalWrapperConst.Urgence.O ? "O" : "N";
            }
            resultat += tmp + "\r\n";


            tmp = "";
            resultat += "Numero_dent=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";
                tmp += acte.numdent == "" ? "Néant" : acte.numdent;
            }
            resultat += tmp + "\r\n";



            //resultat += "Denombrement=" +  + "\r\n";
            //resultat += "Code_affine=" +  + "\r\n";


            tmp = "";
            resultat += "Exoneration=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";

                switch (acte.Exoneration)
                {
                    //case PyxVitalWrapperConst.Exoneration.Ex11: tmp += "11"; break;
                    case PyxVitalWrapperConst.Exoneration.Ex13: tmp += "13"; break;
                    case PyxVitalWrapperConst.Exoneration.Ex15: tmp += "15"; break;
                    //case PyxVitalWrapperConst.Exoneration.Ex21: tmp += "21"; break;
                    // case PyxVitalWrapperConst.Exoneration.Ex9: tmp += "9"; break;
                    case PyxVitalWrapperConst.Exoneration.Ex17: tmp += "17"; break;
                    case PyxVitalWrapperConst.Exoneration.Ex19: tmp += "19"; break;
                    case PyxVitalWrapperConst.Exoneration.ExNéant: tmp += "Néant"; break;
                }


            }
            resultat += tmp + "\r\n";


            tmp = "";
            resultat += "ALD=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";
                tmp += (acte.ald == PyxVitalWrapperConst.ALD.O) || IsALD ? "O" : "N";
            }
            resultat += tmp + "\r\n";


            tmp = "";
            resultat += "DEP=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";
                tmp += acte.DEPAssocier != null ? "O" : "N";
            }
            resultat += tmp + "\r\n";


            tmp = "";
            resultat += "Date_DEP=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";
                tmp += acte.DEPAssocier != null ? acte.DEPAssocier.dateProposition.ToString("dd/MM/yy") : DateTime.Now.ToString("dd/MM/yy");
            }
            resultat += tmp + "\r\n";

            //0 = Pas de reponse dans les temps
            //4 = Réponse favorable
            //5 = Urgence
            //? = Refus
            tmp = "";
            resultat += "Code_accord_DEP=";
            foreach (ActePG acte in lst)
            {
                if (tmp != "") tmp += "+";
                if (acte.DEPAssocier != null)
                {
                    switch (acte.DEPAssocier.CodeAccordDEP)
                    {
                        case PyxVitalWrapperConst.CodeAccordDEP.Ac0: resultat += "0"; break;
                        case PyxVitalWrapperConst.CodeAccordDEP.Ac4: resultat += "4"; break;
                        case PyxVitalWrapperConst.CodeAccordDEP.Ac5: resultat += "5"; break;
                        case PyxVitalWrapperConst.CodeAccordDEP.Neant: resultat += "Néant"; break;
                        case PyxVitalWrapperConst.CodeAccordDEP.Refus: resultat += "?"; break;
                    }
                }
                else
                    tmp += "Néant";
            }
            resultat += tmp + "\r\n";



            return resultat;

        }

        public static void InsertActePG(ActePG acte)
        {
            LogMgmt.AjoutActe(acte.IdPatient, acte);
            DAC.InsertActePG(acte);

            if ((acte.patient != null) && (acte.patient != null) && (acte.patient.ActesPG != null))
                acte.patient.ActesPG.Add(acte);

        }


        public static void InsertActePGWithEcheance(ActePG acte, bool APrelever, bool AVirer, DateTime? DateEcheance)
        {


            LogMgmt.AjoutActe(acte.IdPatient, acte);
            DAC.InsertActePG(acte);

            if ((acte.patient != null) && (acte.patient != null) && (acte.patient.ActesPG != null))
                acte.patient.ActesPG.Add(acte);


            if ((acte.lstEcheances == null) || (acte.lstEcheances.Count==0))
            {
                List<Echeance> result = new List<Echeance>();

                Echeance ech = new Echeance();
                ech.acte = acte;

                if (DateEcheance != null)
                {
                    ech.DateEcheance = DateEcheance.Value;
                }
                else
                {
                    if (acte.DateExecution != null)
                    {
                        if (acte.NbMois == null)
                            ech.DateEcheance = acte.DateExecution;
                        else
                            ech.DateEcheance = acte.DateExecution.AddMonths(acte.NbMois.Value).AddDays(acte.NbJours.Value);
                    }
                }
                ech.IdPatient = acte.IdPatient;
                ech.patient = acte.patient;
                ech.Libelle = acte.Libelle;
                ech.Montant = acte.Montant_Honoraire;


                ech.ParVirement = AVirer;


                ech.payeur = Echeance.typepayeur.patient;
                ech.ParPrelevement = APrelever;
                DAC.InsertEcheance(ech);

                if ((acte.patient != null) && (acte.patient != null) && (acte.patient.Echeances != null))
                    acte.patient.Echeances.Add(ech);
                result.Add(ech);

                acte.lstEcheances = result;
            }
            else
            {
                foreach (Echeance ec in acte.lstEcheances)
                {

                    if (ec.DateEcheance == DateTime.MinValue)
                    {
                        if (acte.DateExecution != null)
                        {
                            if (acte.NbMois == null)
                                ec.DateEcheance = acte.DateExecution;
                            else
                                ec.DateEcheance = acte.DateExecution.AddMonths(acte.NbMois.Value).AddDays(acte.NbJours.Value);
                        }
                    }


                    DAC.InsertEcheance(ec);

                    if ((acte.patient != null) && (acte.patient != null) && (acte.patient.Echeances != null))
                        acte.patient.Echeances.Add(ec);
                }
            }

        }
        public static void Supprimer_Achat_Materiel(int idcom,  int vIdActe)
        {
            DAC.DeleteAchatsMateriels(idcom,  vIdActe);
        }
        public static void Supprimer_Achat_Materiel_Comment(int idcom,int Type, int vIdActe, double PrixActe)
        {
            DAC.DeleteAchats(idcom, Type, vIdActe, PrixActe );
        }
     /*   public static void Genere_AchatMateriel(CommClinique com, CommMateriel comMateriel,ref ActePG vActepg, int GetEcheanceDevis = 0)
        {
            //génération achat Correspondant

            //  if (GetEcheanceDevis > 0)
            //{
            //if (com.echeancestemp == null)
            //    com.echeancestemp = MgmtDevis.get_tempecheancescc_TK  (com);
            //}
            //  else
            //  {
            //      com.echeancestemp = MgmtDevis.get_Echeances(com );
            //    //  com.echeancestemp = new List<TempEcheanceDefinition>();
            //  }
            //ActePG apg = new ActePG(com, comMateriel);
            vActepg = new ActePG(com, comMateriel);

            BasCommon_BL.ActesPGMgmt.InsertActePGWithEcheance_Tk(vActepg, false, false, null);
            comMateriel.idecheancestemp = vActepg.lstEcheances[0].Id;
            comMateriel.idencaissement = vActepg.lstEcheances[0].ID_Encaissement ;
           // DAC.UpdateCommMaterielEcheance(comMateriel.idMateriel, comMateriel.idecheancestemp, com.Id);

        }*/
        public static void Genere_AchatActesSupp(CommClinique com, CommActes comRadios, string TypeActe, int GetEcheanceDevis = 0)
        {
            //génération achat Correspondant
             if (GetEcheanceDevis > 0)
            {
            if (com.echeancestemp == null)
                com.echeancestemp = MgmtDevis.get_tempecheancescc_TK(com);
            }
             else
                 com.echeancestemp = new List<TempEcheanceDefinition>();
            //com.echeancestemp = MgmtDevis.get_Echeances(com);


             if (TypeActe == "0")
             {
                 ActePG apg = new ActePG(com, comRadios, TypeActe);
                 BasCommon_BL.ActesPGMgmt.InsertActePGWithEcheance_Tk(apg, false, false, null);
             }
             else
             {
                 ActePG apg = new ActePG();
                 apg = GetActesPGByIdComm(com.Id);

               

                 //Modification de la ligne Achat
                 BasCommon_BL.ActesPGMgmt.UpdateActePGActes_TK(apg, com.Acte, com.prix_traitement );
                
                 if (TypeActe == "M")
                     com.echeancestemp = EcheancesMgmt.get_Echeances(com, TypeActe);
                 else
                     com.echeancestemp = EcheancesMgmt.get_Echeances(com, "0");
                 if (com.echeancestemp.Count > 0)
                 {
                     //Modification de la ligne echéance (Ajout du prix à l'échéance qui le plus peit montant)
               
                    // com.echeancestemp[0].Montant = com.echeancestemp[0].Montant + (comRadios.prix_traitement * comRadios.Qte);
                     EcheancesMgmt.UpdateEcheanceMontant(com.echeancestemp[0].Id, (comRadios.prix_traitement * comRadios.Qte), 0, com.echeancestemp[0].Libelle);
                 }
                 else
                     //création d'une nouvelle echéance
                     BasCommon_BL.ActesPGMgmt.InsertActePGWithEcheance_Tk(apg, false, false, null);

                 /*
                 //Ajout de l'échéance
                 Echeance ech = new Echeance();
                 ech.acte = apg;
                 ech.DateEcheance =Convert.ToDateTime ( com.DatePrevisionnnelle) ;

                 ech.IdPatient = apg.IdPatient;
                 ech.patient = baseMgmtPatient.GetPatient(apg.IdPatient);
                 ech.Libelle = comRadios.LibActe ;
                 ech.Montant = comRadios.prix_traitement * comRadios .Qte  ;
                 ech.IdActe = apg.Id;
                 ech.IdActeTraitement = comRadios.IdActe;
                 ech.ParVirement = false;
                 ech.TypeActe = TypeActe;

                 ech.payeur = Echeance.typepayeur.patient;
                 ech.ParPrelevement = false;
                 DAC.InsertEcheance(ech);
               // DAC.UpdateCommActeEcheance(comRadios, TypeActe, ech.Id, com.Id);
                 comRadios.idecheancestemp = ech.Id;

                 if ((apg.patient != null) && (apg.patient != null) && (apg.patient.Echeances != null))
                     apg.patient.Echeances.Add(ech);
                 // result.Add(ech);

                 //acte.lstEcheances = result;*/
             }

         



        }
       public static void Genere_AchatMateriel(CommClinique com, CommMateriel comMateriel,ref ActePG vActepg)
        {
            vActepg = new ActePG(com, comMateriel);

            DAC.InsertActePG_TK(vActepg);

          

        }

        public static  void Genere_Achat(CommClinique com, int GetEcheanceDevis = 0, int AllComment  = 0, int ActePrincipale = 0, bool LigneVide = false,string prefix = "")
        {
            //génération achat Correspondant
            if (GetEcheanceDevis > 0)
            {
                if (com.echeancestemp == null)
                    com.echeancestemp = MgmtDevis.get_tempecheancescc_TK(com);
            }
            else
            com.echeancestemp = new List<TempEcheanceDefinition>();
            ActePG apg = new ActePG(com, AllComment,LigneVide,prefix);
         
            BasCommon_BL.ActesPGMgmt.InsertActePGWithEcheance_Tk(apg, false, false, null, ActePrincipale );

            //Fin Génération Achat

        }
        public static void InsertActePGWithEcheance_Tk(ActePG acte, bool APrelever, bool AVirer, DateTime? DateEcheance, int ActePrincipale = 0)
        {


            LogMgmt.AjoutActe(acte.IdPatient, acte);
           
                DAC.InsertActePG_TK(acte );

                InsertActePGEcheance_Tk(acte, APrelever, AVirer, DateEcheance, ActePrincipale);
           

        }
        //
        public static void InsertActePGEcheance_Tk(ActePG acte, bool APrelever, bool AVirer, DateTime? DateEcheance, int ActePrincipale = 0)
        {

            foreach (Echeance ech in acte.lstEcheances)
            {
                ech.IdActe = acte.Id;
              

            }
            if ((acte.patient != null) && (acte.patient != null) && (acte.patient.ActesPG != null))
                acte.patient.ActesPG.Add(acte);


            if ((acte.lstEcheances == null) || (acte.lstEcheances.Count == 0))
            {
                List<Echeance> result = new List<Echeance>();

                Echeance ech = new Echeance();
                ech.acte = acte;

                if (DateEcheance != null)
                {
                    ech.DateEcheance = DateEcheance.Value;
                }
                else
                {
                    if (acte.DateExecution != null)
                    {
                        if (acte.NbMois == null)
                            ech.DateEcheance = acte.DateExecution;
                        else
                            ech.DateEcheance = acte.DateExecution.AddMonths(acte.NbMois.Value).AddDays(acte.NbJours.Value);
                    }
                }
                ech.IdPatient = acte.IdPatient;
                ech.patient = baseMgmtPatient.GetPatient(acte.IdPatient);
                ech.Libelle = acte.Libelle;
                ech.Montant = acte.Montant_Honoraire;
                ech.IdActeTraitement = acte.IdActe;

                ech.ParVirement = AVirer;

                if (acte.TypeActe == "M")
                    ech.TypeActe = acte.TypeActe;

                ech.payeur = Echeance.typepayeur.patient;
                ech.ParPrelevement = APrelever;
              
                DAC.InsertEcheance(ech, ActePrincipale);

                if ((acte.patient != null) && (acte.patient != null) && (acte.patient.Echeances != null))
                    acte.patient.Echeances.Add(ech);
                result.Add(ech);

                acte.lstEcheances = result;
            }
            else
            {
                foreach (Echeance ec in acte.lstEcheances)
                {

                    if (ec.DateEcheance == DateTime.MinValue)
                    {
                        if (acte.DateExecution != null)
                        {
                            if (acte.NbMois == null)
                                ec.DateEcheance = acte.DateExecution;
                            else
                                ec.DateEcheance = acte.DateExecution.AddMonths(acte.NbMois.Value).AddDays(acte.NbJours.Value);
                        }
                    }

                    ec.IdComm = acte.IdComm;
                    DAC.InsertEcheance(ec, ActePrincipale);

                    if ((acte.patient != null) && (acte.patient != null) && (acte.patient.Echeances != null))
                        acte.patient.Echeances.Add(ec);
                }
            }
        }
        //

        public static void DeleteActePG(ActePG acte,bool vDelete=false)
        {

            if (acte.PaimentEtat >= ActePG.etatpaiement.Partiel)
                throw new System.Exception("L'acte a été soldé\nSuppression impossible");

            if (acte.DEPAssocier != null)
                throw new System.Exception("Une Demande d'entente à été réalisée\nSuppression impossible");

            if (acte.FeuilleDeSoinAssocier != null)
                throw new System.Exception("Une Feuille de soin à été réalisée\nSuppression impossible");

            if (acte.IdDevisAssociate > 0 && vDelete)
                throw new System.Exception("Cet acte correspond à un Devis\nSuppression impossible");




            if (acte.IdSurvPlanGestionAssocie > 0)
                DAC.DeleteSurveillance(acte.IdSurvPlanGestionAssocie);
            if (acte.IdSemestrePlanGestionAssocie > 0)
                DAC.DeleteSemestre(acte.IdSemestrePlanGestionAssocie);


            if (acte.IdDevisAssociate > 0)
            {
                Devis_TK getDevis_TK = MgmtDevis.getDevis_TK(acte.IdDevisAssociate);
                if (getDevis_TK == null)
                MgmtDevis.DeleteDevis(acte.IdDevisAssociate);
            }

            LogMgmt.SuppressionActe(acte.IdPatient, acte);
            DAC.DeleteActePGAndEcheance(acte);



            if ((acte.patient != null) && (acte.patient != null) && (acte.patient.ActesPG != null))
            {
                for (int i = acte.patient.ActesPG.Count - 1; i >= 0; i--)
                    if (acte.patient.ActesPG[i].Id == acte.Id)
                        acte.patient.ActesPG.Remove(acte.patient.ActesPG[i]);
            }






        }

        public static void DeleteActePGwthEcheance(ActePG acte)
        {
            DAC.DeleteActePGAndEcheance(acte);
        }

        public static void UpdateActePGWithoutLog(ActePG Newacte)
        {

            DAC.UpdateActePG(Newacte);



        }



        public static void UpdateActePG(ActePG Newacte, ActePG Oldacte)
        {

            LogMgmt.ModificationActe(Newacte.IdPatient, Oldacte, Newacte);
            DAC.UpdateActePG(Newacte);



        }

        public static void Modification_Echeances(ActePG TmpActePg, double ancienPrixActe, double NouveauPrix, string LibelleEcheance, Boolean EcraserMontantEcheance = true)
        {
         //   Acte acte = ActesMgmt.Actes.Find(a => a.id_acte == idActe);

            TmpActePg.lstEcheances = EcheancesMgmt.GetEcheances(TmpActePg, false);
            double TmpPrixAncienActe = ancienPrixActe;
            bool isUpdate = false;
            if (TmpActePg.lstEcheances.Count == 0) return;
            Echeance tmpEcheance = TmpActePg.lstEcheances[0];
            TmpActePg.lstEcheances.RemoveAll(w=>w.ID_Encaissement > -1);
           if(TmpActePg.lstEcheances.Count == 0)
            {

                //if ((System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" +  DAC.prefix] == "FR"))
                //{
                //    if (acte.prix_acte - acte.Remboursement > 0)
                //    {
                //        Echeance tmpEch = tmpEcheance;
                //        tmpEch.Montant = NouveauPrix - acte.Remboursement;
                //        tmpEch.ID_Encaissement = -1;
                //        tmpEch.Id = -1;
                //        tmpEch.ID_Facturation = -1;
                //        EcheancesMgmt.InsertEcheance(tmpEch);
                //    }
                //    if (acte.Remboursement > 0)
                //    {
                //        Echeance tmpEch = tmpEcheance;
                //        tmpEch.Montant = acte.Remboursement;
                //        tmpEch.ID_Encaissement = -1;
                //        tmpEch.Id = -1;
                //        tmpEch.ID_Facturation = -1;
                //        EcheancesMgmt.InsertEcheance(tmpEch);
                //    }
                //}
                //else
                //{
                    Echeance tmpEch = tmpEcheance;
                    tmpEch.ID_Encaissement = -1;
                    tmpEch.Id = -1;
                    tmpEch.ID_Facturation = -1;
                    tmpEch.Montant = NouveauPrix;
                    EcheancesMgmt.InsertEcheance(tmpEch);
                //}
            }
           else
            foreach (Echeance ech in TmpActePg.lstEcheances)
            {
                if (Math.Round(ech.Montant, 2) >= Math.Round(TmpPrixAncienActe, 2))
                {
                   
                        EcheancesMgmt.UpdateEcheanceMontant(ech.Id, NouveauPrix, TmpPrixAncienActe, LibelleEcheance);
                   
                    TmpPrixAncienActe = TmpPrixAncienActe - ech.Montant;
                }

                else
                {
                    EcheancesMgmt.DeleteEcheance(ech);
                    TmpPrixAncienActe = TmpPrixAncienActe - ech.Montant;
                }
                if (Math.Round(TmpPrixAncienActe, 2) <= 0)
                    break;

            }
           TmpActePg.lstEcheances = EcheancesMgmt.GetEcheances(TmpActePg, false);

        }

        public static void UpdateActePGActes_TK(ActePG  vActePg,Acte vActe, double vMontant,int TraitemetMateriel = 0)
        {
            DAC.UpdateActePGActes_TK(vActePg, vActe, vMontant, TraitemetMateriel);

         

        }
        public static void UpdateActePG_TK(ActePG Newacte, ActePG Oldacte)
        {

            LogMgmt.ModificationActe(Newacte.IdPatient, Oldacte, Newacte);
            DAC.UpdateActePG_TK(Newacte);
        }
        public static void DeleteActePGMat(ActePG acte)
        {

           DAC.DeleteActePGMat(acte);
        }

        public static List<ActePG> getAchatsByPeriode(DateTime datedebut, DateTime datefin, string ids) {

            List<ActePG> liste = new List<ActePG>();
             

            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray("/getAchatsByPeriode/"+datedebut.Date.ToString("yyyy-MM-dd HH:mm:ss")+"&"+datefin.Date.ToString("yyyy-MM-dd HH:mm:ss")+"&"+ids);
            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildActePG.BuildJ(obj));
            return liste;        
        }
       
        public static List<ActePG> getAchatsByPeriodeOld(DateTime datedebut ,DateTime datefin, string ids)
        {
            List<ActePG> achats = new List<ActePG>();


            DataTable dt = DAC.getAchatsByPeriode(datedebut,datefin,ids);

            achats = new List<ActePG>();

            foreach (DataRow r in dt.Rows)
            {
                ActePG acte = Builders.BuildActePG.Build(r);
                achats.Add(acte);
            }

            return achats;
        }

        public static List<ActePG> getAchatsByPeriodeAndFamilleMateriel(DateTime dateTime1, DateTime dateTime2, FamillesMateriels famillesMateriels)
        {
            List<ActePG> liste = new List<ActePG>();
            string method = "/getAchatsByPeriodAndFamilleMateriel/";
             method += dateTime1.ToString("yyyy-MM-dd HH:mm:ss")+"&"
                + dateTime2.ToString("yyyy-MM-dd HH:mm:ss")+"&"+famillesMateriels.Id;

            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);
            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildActePG.BuildJ(obj));
            return liste;
        }

        public static List<ActePG> getAchatsByPeriodeAndIdMateriel(DateTime dateTime1, DateTime dateTime2, Materiel materiel)
        {
            List<ActePG> liste = new List<ActePG>();
            string method = "/getAchatsByPeriodAndMateriel/";
             method += dateTime1.ToString("yyyy-MM-dd HH:mm:ss") + "&"
                + dateTime2.ToString("yyyy-MM-dd HH:mm:ss") + "&" + materiel.id_materiel;

            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);

            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildActePG.BuildJ(obj));
            return liste;
        }
    }
}
