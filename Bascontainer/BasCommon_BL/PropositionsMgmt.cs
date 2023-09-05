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
    public static class PropositionMgmt
    {


        public static void Delete(Proposition obj)
        {
            DAC.DeleteProposition(obj.Id);
        }

        public static void UpdateProposition(Proposition proposition)
        {
            if (proposition.Id != -1)
            {
                DAC.UpdateProposition(proposition);


            }


        }


        public static void InsertProposition(Proposition proposition)
        {
            if (proposition.Id == -1)
            {
                DAC.InsertPropositions(proposition);


            }


        }

        public static void AddTraitement(Traitement traitement)
        {
            DAC.AddTraitement(traitement);
        }

        public static List<TempEcheanceDefinition> LoadDefaultTempecheances(Proposition prop)
        {

            List<TempEcheanceDefinition> lst = new List<TempEcheanceDefinition>();
            if ((prop.echeancestemp==null) ||(prop.echeancestemp.Count == 0))
            {
                foreach (Traitement t in prop.traitements)
                    foreach (Semestre s in t.semestres)
                    {
                        TempEcheanceDefinition ted = new TempEcheanceDefinition();
                        ted.DAteEcheance = s.DateFin;
                        ted.Montant = s.Montant_Honoraire;
                        ted.ParPrelevement = false;
                        ted.ParVirement = false;
                        ted.Libelle = s.traitementSecu.Libelle;
                        ted.acte = BasCommon_BL.ActesPGMgmt.CreateActeFromSemestre(s);
                        lst.Add(ted);
                    }
            }
            else
                return prop.echeancestemp;

            lst.Sort();

            return lst;
        }


        public static DateTime? GetDebutREprise(List<Proposition> propositions)
        {
            DateTime? mindte = null;
            DateTime? maxdte = null;


            Semestre currents = GetCurrentSemestre(propositions);

            if (currents == null) return null;

            Proposition p = currents.Parent.Parent;

            
                    foreach (Traitement t in p.traitements)
                    {
                        foreach (Semestre s in t.semestres)
                        {
                            foreach (Surveillance su in s.surveillances)
                            {
                                if ((su.DateFin != null) && ((maxdte == null) || (su.DateFin > maxdte)))
                                    maxdte = su.DateFin.Value;
                            }
                            if ((maxdte == null) || (s.DateFin > maxdte))
                                maxdte = s.DateFin;

                            if ((mindte == null) || (s.DateDebut < mindte))
                                mindte = s.DateDebut;
                        }
                    }
                
            

            return mindte;
        }

        public static DateTime? GetDebutTrrtmnt(List<Proposition> propositions)
        {

            DateTime? mindte = null;
            DateTime? maxdte = null;

            foreach (Proposition p in propositions)
            {

                foreach (Traitement t in p.traitements)
                {
                    foreach (Semestre s in t.semestres)
                    {
                        foreach (Surveillance su in s.surveillances)
                        {
                            if ((su.DateFin != null) && ((maxdte == null) || (su.DateFin > maxdte)))
                                maxdte = su.DateFin.Value;
                        }
                        if ((maxdte == null) || (s.DateFin > maxdte))
                            maxdte = s.DateFin;

                        if ((mindte == null) || (s.DateDebut < mindte))
                            mindte = s.DateDebut;
                    }

                }
            }

            return mindte;
        }

        public static DateTime? GetFinTrrtmntForAll(List<Proposition> propositions)
        {

            DateTime? maxdte = null;

            foreach (Proposition p in propositions)
            {

                
                    DateTime? dte = GetFinTrrtmnt(p);

                    if ((maxdte == null) || ((dte != null) && (maxdte.Value < dte.Value)))
                        maxdte = dte;
                
            }

            return maxdte;
        }

        public static DateTime? GetFinTrrtmnt(List<Proposition> propositions)
        {

            DateTime? maxdte = null;

            foreach (Proposition p in propositions)
            {

                if (p.Etat == Proposition.EtatProposition.Accepté)
                {
                    DateTime? dte = GetFinTrrtmnt(p);

                    if ((maxdte == null) || ((dte != null) && (maxdte.Value < dte.Value)))
                        maxdte = dte;
                }
            }

            return maxdte;
        }


        public static DateTime? GetFinTrrtmnt(Proposition p)
        {

            DateTime? mindte = null;
            DateTime? maxdte = null;

            
                
                    foreach (Traitement t in p.traitements)
                    {
                        foreach (Semestre s in t.semestres)
                        {
                            foreach (Surveillance su in s.surveillances)
                            {
                                if ((su.DateFin != null) && ((maxdte == null) || (su.DateFin > maxdte)))
                                    maxdte = su.DateFin.Value;
                            }
                            if ((maxdte == null) || (s.DateFin > maxdte))
                                maxdte = s.DateFin;

                            if ((mindte == null) || (s.DateDebut < mindte))
                                mindte = s.DateDebut;
                        }
                    }
                
            

            return maxdte;
        }


        public static void CloseTraitement(Traitement traitementToClose, DateTime chooseDate)
        {
            foreach (Semestre s in traitementToClose.semestres)
            {
                if (s.DateFin == null)
                {
                    List<ActePG> actes = ActesPGMgmt.GetActesPG(s);

                    foreach (ActePG a in actes)
                    {
                        ActePG oldacte = (ActePG)a.Clone();

                        a.NbMois = 0;
                        a.NbJours = (int)(chooseDate - a.DateExecution).TotalDays;

                        ActesPGMgmt.UpdateActePG(a, oldacte);
                        List<Echeance> lst = EcheancesMgmt.GetEcheances(a);

                        foreach (Echeance ech in lst)
                        {
                            if ((ech.DateEcheance == null) && (ech.ID_Encaissement < 0))
                            {
                                ech.DateEcheance = chooseDate;
                                EcheancesMgmt.UpdateEcheance(ech);
                            }
                        }

                    }

                    s.DateFin = chooseDate;
                    SemestreMgmt.UpdateSemestre(s);
                }
            }
        }


        public static List<ModeleDePropositions> getModeles()
        {
            List<ModeleDePropositions> lst = new List<ModeleDePropositions>();
            DataTable dtmdl = DAC.getModeleDePropositions();
            foreach (DataRow dr in dtmdl.Rows)
                lst.Add(Builders.BuildModeleDeProposition.Build(dr));

            return lst;
        }


        public static ModeleDePropositions getModele(int Idmdl)
        {

            DataRow drmdl = DAC.getModeleDeProposition(Idmdl);
            ModeleDePropositions mdl = Builders.BuildModeleDeProposition.Build(drmdl);


            DataTable dt = DAC.getPropositions(mdl);

            foreach (DataRow dr in dt.Rows)
            {
                Proposition pr = Builders.BuildProposition.Build(dr);
                pr.IdModel = mdl.Id;

                pr.traitements = TraitementMgmt.getTraitements(pr);

                //pr.poseAppareils = PoseAppareilMgmt.getPoseAppareils(pr);


                mdl.propositions.Add(pr);
            }

            return mdl;
        }


        private static int GetNbSemestreAlreadyDone(List<Proposition> propositions)
        {
            int nbSemAlreadyDone = 0;
            foreach (Proposition pr in propositions)
            {
                if (pr.Etat != Proposition.EtatProposition.Accepté) continue;
                foreach (Traitement t in pr.traitements)
                    foreach (Semestre s in t.semestres)
                        if (s.traitementSecu.Code.Code == "TO")
                            nbSemAlreadyDone++;
            }

            return nbSemAlreadyDone;
        }


        public static int GetNbSemestreSecuAlreadyDone(List<Proposition> propositions)
        {
            int nbSemAlreadyDone = 0;
            foreach (Proposition pr in propositions)
            {
                if (pr.Etat != Proposition.EtatProposition.Accepté) continue;
                foreach (Traitement t in pr.traitements)
                    foreach (Semestre s in t.semestres)
                        if (s.traitementSecu.Code.Code == "TO")
                            nbSemAlreadyDone++;
            }

            return nbSemAlreadyDone;
        }

        
        public static void AccepterPropositions(List<Proposition> Allpropositions, List<Proposition> propAcceptees, Devis d, DateTime dteDebut)
        {

            int SemStartedAt = GetNbSemestreAlreadyDone(Allpropositions);

            if (d.propositions == null) d.propositions = PropositionMgmt.getPropositions(d);

            MgmtDevis.AccepterDevis(d.Id);
            DateTime dte = dteDebut;

            foreach (Proposition proposition in d.propositions)
            {
                if (!propAcceptees.Contains(proposition))
                {
                    proposition.Etat = Proposition.EtatProposition.Refusé;
                    proposition.DateEvenement = DateTime.Now;
                }
                else
                {

                    proposition.Etat = Proposition.EtatProposition.Accepté;
                    proposition.DateAcceptation = DateTime.Now;
                    proposition.IdDevis = d.Id;

                    if (proposition.matosassociate == null)
                        proposition.matosassociate = MgmtDevis.getactesHorstraitement(proposition);

                    foreach (ActePGPropose a in proposition.matosassociate)
                        PropositionMgmt.AddActFromActePropose(a, proposition.IdPatient);


                    int i = 1;
                    foreach (Traitement t in proposition.traitements)
                        foreach (Semestre s in t.semestres)
                        {

                            TimeSpan ts = s.DateDebut == null ? (s.DateFin - DateTime.MinValue) : (s.DateFin - s.DateDebut);
                            int nbMois = ((int)ts.TotalDays) / 30;
                            s.DateDebut = dte;
                            s.DateFin = s.DateDebut.AddMonths(nbMois);
                            if (! CodesTraitement.IsContention(s.CodeSemestre))
                                s.NumSemestre = SemStartedAt + i;
                            else
                            {
                                if (CodesTraitement.IsContention1(s.CodeSemestre))
                                    s.NumSemestre = 1;

                                if (CodesTraitement.IsContention2(s.CodeSemestre))
                                    s.NumSemestre = 2;
                            }

                            dte = s.DateFin;
                            SemestreMgmt.UpdateSemestre(s);
                            foreach (Surveillance su in s.surveillances)
                            {
                                ts = su.DateDebut == null ? (su.DateFin.Value - DateTime.MinValue) : (su.DateFin.Value - su.DateDebut.Value);
                                nbMois = ((int)ts.TotalDays) / 30;

                                su.DateDebut = dte;
                                su.DateFin = su.DateDebut.Value.AddMonths(nbMois);
                                dte = su.DateFin.Value;
                                SurveillanceMgmt.UpdateSurveillance(su);
                            }
                            i++;
                        }
                }
                updateProposition(proposition);
            }
        }


        public static Semestre GetCurrentSemestre(List<Proposition> propositions)
        {
            foreach (Proposition p in propositions)
                if (p.Etat == Proposition.EtatProposition.Accepté)
                    foreach (Traitement t in p.traitements)
                        foreach (Semestre s in t.semestres)
                            if ((s.DateDebut < DateTime.Now) && (s.DateFin > DateTime.Now))
                                return s;

            return null;

        }

        public static Semestre GetLastSemestre(List<Proposition> propositions)
        {

            DateTime mindte = DateTime.MinValue;
            Semestre se = null;

            foreach (Proposition p in propositions)
                if (p.Etat == Proposition.EtatProposition.Accepté)
                    foreach (Traitement t in p.traitements)
                        foreach (Semestre s in t.semestres)
                        {
                            if ((s.DateDebut > mindte)&&(!CodesTraitement.IsContention(s.CodeSemestre)))
                            {
                                mindte = s.DateDebut;
                                se = s;
                            }
                        }

            return se;

        }
        public static List<NewTraitement > GetAllTypeTraitements()
        {

            DataTable dt = DAC.getAllTraitements();

            List<NewTraitement> lst = new List<NewTraitement>();

            foreach (DataRow dr in dt.Rows)
            {
                NewTraitement p = Builders.BuildProposition.BuildTraitementDevis(dr);
                lst.Add(p);
            }



            return lst;

        }
        


        public static int FindSemestreAffecte(Proposition proposition)
        {
            int maxSem = 0;
            foreach (Traitement t in proposition.traitements)
                foreach (Semestre s in t.semestres)
                    if (s.NumSemestre > maxSem) maxSem = s.NumSemestre;

            return maxSem;

        }

        public static string CheckValiditeRemboursement(Proposition proposition, DateTime DateDebutTraitement, int NbSemestreEntame)
        {
            string Errors = "";

            Semestre[] S = new Semestre[10];
            int[] numS = new int[10];
            for (int i = 0; i < S.Length; i++)
                S[i] = null;

            foreach (Traitement t in proposition.traitements)
                if ((t.Phase == BasCommon_BO.Traitement.EnumPhase.Orthodontique) || (t.Phase == BasCommon_BO.Traitement.EnumPhase.Orthopedique))
                    foreach (Semestre s in t.semestres)
                    {
                        if (s.NumSemestre == 1) S[1] = s;
                        if (s.NumSemestre == 2) S[2] = s;
                        if (s.NumSemestre == 3) S[3] = s;
                        if (s.NumSemestre == 4) S[4] = s;
                        if (s.NumSemestre == 5) S[5] = s;
                        if (s.NumSemestre == 6) S[6] = s;
                        if (s.NumSemestre == 7) S[7] = s;
                        if (s.NumSemestre == 8) S[8] = s;

                    }
            foreach (Traitement t in proposition.traitements)
                foreach (Semestre s in t.semestres)
                {
                    if (s.NumSemestre == 1) numS[1]++;
                    if (s.NumSemestre == 2) numS[2]++;
                    if (s.NumSemestre == 3) numS[3]++;
                    if (s.NumSemestre == 4) numS[4]++;
                    if (s.NumSemestre == 5) numS[5]++;
                    if (s.NumSemestre == 6) numS[6]++;
                    if (s.NumSemestre == 7) numS[7]++;
                    if (s.NumSemestre == 8) numS[8]++;

                }
            foreach (int nb in numS)
                if (nb > 1) Errors = "Des semestres se superposent";

            #region CheckNbSemestre
            int y;
            int m;
            int d;
            proposition.patient.AgeToDate(DateDebutTraitement, out y, out m, out d);

            bool as16ans = y < 16;

            int NbSemRemb = 0;

            foreach (Semestre s in S)
                if ((s != null) && (s.traitementSecu.Code.Code != "HN"))
                    NbSemRemb++;

            if (as16ans && ((NbSemestreEntame + NbSemRemb) > 6))
            {
                if (Errors != "") Errors += "\n";
                Errors += "Il y a plus de 6 semestres remboursés !";
            }

            if ((!as16ans) && (NbSemRemb > 1))
            {
                if (Errors != "") Errors += "\n";
                Errors += "Le patient à plus de 16 ans au début du traitement\nIl ne peut pas avoir de semestre remboursé !";
            }


            foreach (Semestre s in S)
                if ((s != null) && (s.traitementSecu.Code.Code != "HN") && (s.surveillances.Count > 1))
                {
                    if (Errors != "") Errors += "\n";
                    Errors += "Un seul semestre de surveillance peut être remboursé (2 seances en TO5)";
                }

            #endregion

            return Errors;
        }

        public static List<string> GetRisques(Proposition proposition)
        {
            List<string> lst = new List<string>();
            

            return lst;

        }

        public static Double GetTotal(Proposition proposition)
        {
            Double resultat = 0;

            foreach (Traitement t in proposition.traitements)
                resultat += TraitementMgmt.getTotal(t);

            return resultat;

        }

        public static int GetDuree(Proposition proposition)
        {
            int nbjours = 0;

            foreach (Traitement t in proposition.traitements)
                foreach (Semestre s in t.semestres)
                    if (s.DateFin != null && s.DateDebut != null)
                        nbjours += (int)((s.DateFin - s.DateDebut).TotalDays);

            return nbjours;

        }

        public static string GetSmoothedTarif(Proposition proposition)
        {
            string Strresultat = "";

            //foreach (Traitement t in proposition.traitements)
            for (int i = 0; i < proposition.traitements.Count; i++)
            {
                Traitement t = proposition.traitements[i];
                string resT = "";
                if ((t.Phase == BasCommon_BO.Traitement.EnumPhase.Orthodontique) || (t.Phase == BasCommon_BO.Traitement.EnumPhase.Orthopedique))
                {
                    Double Tresultat = TraitementMgmt.getTotal(t);
                    resT = (Tresultat / t.semestres.Count).ToString("C2") + " pendant " + t.semestres.Count.ToString() + " semestre(s)";

                    if (Strresultat != "")
                    {
                        if (i == proposition.traitements.Count - 1)
                            Strresultat += " puis ";
                        else
                            Strresultat += ",";
                    }
                }
                if (t.Phase == BasCommon_BO.Traitement.EnumPhase.Contention)
                {
                    Double Tresultat = TraitementMgmt.getTotal(t);
                    resT = t.semestres.Count + " an(s) de contention à " + Tresultat.ToString("C2");

                    if (Strresultat != "")
                    {
                        if (i == proposition.traitements.Count - 1)
                            Strresultat += " puis ";
                        else
                            Strresultat += ",";
                    }
                }
                Strresultat += resT;
            }

            return Strresultat;

        }

        public static string GetAllSemestres(Proposition proposition)
        {
            string resultat = "";

            foreach (Traitement t in proposition.traitements)
                foreach (Semestre s in t.semestres)
                {
                    if (resultat != "") resultat += ",";
                    resultat += s.CodeSemestre;
                }

            return resultat;

        }



        public static Double GetPartSecu(Proposition proposition)
        {
            Double resultat = 0;

            foreach (Traitement t in proposition.traitements)
                resultat += TraitementMgmt.GetPartSecu(t);

            return resultat;

        }


        public static void updateProposition(Proposition proposition)
        {
            DAC.UpdatePropositions(proposition);
        }

        public static void SaveProposition(Proposition proposition)
        {
            if (proposition.Id == -1)
            {
                DAC.InsertPropositions(proposition);
            }
            else
            {
                DAC.UpdatePropositions(proposition);
            }

        }

        public static void InsertFullProposition(Proposition proposition)
        {
            if (proposition.Id == -1)
            {
                DAC.InsertPropositions(proposition);


                foreach (Traitement t in proposition.traitements)
                {
                    TraitementMgmt.AddTraitements(t);
                    foreach (Semestre s in t.semestres)
                    {
                        SemestreMgmt.AddSemestre(s);
                        foreach (Surveillance su in s.surveillances)
                        {
                            su.Semestre = s;
                            if (su.Id <= 0)
                                SurveillanceMgmt.AddSurveillance(su);
                            else
                                SurveillanceMgmt.UpdateSurveillance(su);
                        }
                    }
                }
                if (proposition.matosassociate == null)
                    proposition.matosassociate = new List<ActePGPropose>();
                    foreach (ActePGPropose m in proposition.matosassociate)
                    {
                        m.IdProposition = proposition.Id;
                        m.proposition = proposition;
                        ActePGProposeMgmt.AddActePGPropose(m);
                    }
                /*
                foreach (PoseAppareil pa in proposition.poseAppareils)
                    PoseAppareilMgmt.AddPoseAppareil(pa);
                 * */
            }


        }


        public static List<Proposition> getSignedPropositions(basePatient patient)
        {
            
            DataTable dt = DAC.getSignedPropositions(patient);

            List<Proposition> lst = new List<Proposition>();

            foreach (DataRow dr in dt.Rows)
            {
                Proposition p = Builders.BuildProposition.Build(dr);
                p.patient = patient;


                lst.Add(p);
            }



            return lst;
        }

        public static List<Proposition> getSignedFullPropositions(basePatient patient)
        {
            DataTable dt = DAC.getSignedPropositions(patient);

            List<Proposition> lst = new List<Proposition>();

            foreach (DataRow dr in dt.Rows)
            {
                Proposition p = Builders.BuildProposition.Build(dr);
                p.patient = patient;
                p.traitements = TraitementMgmt.getTraitements(p);

                //p.poseAppareils = PoseAppareilMgmt.getPoseAppareils(p);

                lst.Add(p);
            }



            return lst;
        }
        public static List<Proposition> getPropositions(basePatient patient)
        {
            JArray json = DAC.getMethodeJsonArray("/Propositions/" + patient.Id);

            List<Proposition> lst = new List<Proposition>();

            foreach (JObject dr in json)
            {
                Proposition pr = Builders.BuildProposition.BuildJ(dr);
                pr.patient = patient;

                pr.traitements = TraitementMgmt.getTraitements(pr);

                // pr.poseAppareils = PoseAppareilMgmt.getPoseAppareils(pr);


                lst.Add(pr);

            }

            return lst;
        }
        public static List<Proposition> getPropositionsOLD(basePatient patient)
        {
            DataTable dt = DAC.getPropositions(patient);

            List<Proposition> lst = new List<Proposition>();

            foreach (DataRow dr in dt.Rows)
            {
                Proposition pr = Builders.BuildProposition.Build(dr);
                pr.patient = patient;

                pr.traitements = TraitementMgmt.getTraitements(pr);

                // pr.poseAppareils = PoseAppareilMgmt.getPoseAppareils(pr);


                lst.Add(pr);

            }

            return lst;
        }


        public static List<Proposition> getFullPropositions(basePatient patient)
        {
            DataTable dt = DAC.getPropositions(patient);
            List<Proposition> lstprop = new List<Proposition>();

            foreach (DataRow dr in dt.Rows)
            {
                Proposition pr = Builders.BuildProposition.Build(dr);
                pr.patient = patient;
                lstprop.Add(pr);

            }

            dt = DAC.getTraitements(patient);
            List<Traitement> lsttrmnt = new List<Traitement>();

            foreach (DataRow dr in dt.Rows)
            {
                Traitement pr = Builders.BuildTraitement.Build(dr);
                lsttrmnt.Add(pr);
            }

            dt = DAC.getSemestres(patient);
            List<Semestre> lstsems = new List<Semestre>();

            foreach (DataRow dr in dt.Rows)
            {
                Semestre pr = Builders.BuildSemestre.Build(dr);
                lstsems.Add(pr);
            }

            dt = DAC.getSurveillances(patient);
            List<Surveillance> lstsurvs = new List<Surveillance>();

            foreach (DataRow dr in dt.Rows)
            {
                Surveillance pr = Builders.BuildSurveillance.Build(dr);
                lstsurvs.Add(pr);
            }

            foreach (Proposition p in lstprop)                
                foreach(Traitement t in lsttrmnt)
                    if (t.IdProposition == p.Id)
                    {
                        p.traitements.Add(t);
                        t.Parent = p;
                    }
                    
            

            foreach (Traitement t in lsttrmnt)
                foreach (Semestre s in lstsems)
                    if (s.IdTraitement == t.Id)
                    {
                        t.semestres.Add(s);
                        s.Parent = t;
                    }

                    
                


            foreach (Semestre s in lstsems)
                foreach (Surveillance su in lstsurvs)
                    if (su.IdSemestre == s.Id)
                    {
                        su.Semestre = s;
                        s.surveillances.Add(su);
                    }
                    
                

            return lstprop;
        }


        public static List<Proposition> getPropositions(Devis devis)
        {
            DataTable dt = DAC.getPropositions(devis);

            List<Proposition> lst = new List<Proposition>();

            foreach (DataRow dr in dt.Rows)
            {
                Proposition pr = Builders.BuildProposition.Build(dr);
                pr.patient = devis.patient;

                pr.traitements = TraitementMgmt.getTraitements(pr);

                //pr.poseAppareils = PoseAppareilMgmt.getPoseAppareils(pr);


                lst.Add(pr);

            }

            return lst;
        }

        public static void AddActFromActePropose(ActePGPropose actepropose, basePatient pat)
        {

            if ((actepropose.IdTemplateActePG >= 0) && (actepropose.template == null))
                actepropose.template = TemplateApctePGMgmt.getCodeSecu(actepropose.IdTemplateActePG);

            ActePG acte = new ActePG();
            acte.Template = actepropose.template;
            acte.Montant_Honoraire = actepropose.Qte * actepropose.Montant;
            acte.Coeff = actepropose.template.Coeff;
            acte.prestation = actepropose.template.Code;
            acte.Libelle = actepropose.Libelle;
            acte.Quantite = actepropose.Qte;

            acte.NbJours = actepropose.template.NBJours;
            acte.NbMois = actepropose.template.NBMois;

            acte.Id_DEP = -1;



            acte.NeedDEP = actepropose.template.NeedDEP;
            acte.NeedFSE = actepropose.template.NeedFS;
            acte.patient = pat;
            acte.IsDecomposed = actepropose.template.IsDecomposed;
            acte.CoeffDecompose = actepropose.template.CoeffDecompose;
            acte.IdSemestrePlanGestionAssocie = -1;
            acte.IdSurvPlanGestionAssocie = -1;
            acte.DateExecution = actepropose.DateExecution.Value;
            //modification nadhemmmm
          //  acte.IdDevisAssociate = actepropose.IdDevis;
            ActesPGMgmt.InsertActePGWithEcheance(acte, false, false, null);

        }

        public static void AddActFromActePropose(ActePGPropose actepropose, int Idpat)
        {

            if ((actepropose.IdTemplateActePG >= 0) && (actepropose.template == null))
                actepropose.template = TemplateApctePGMgmt.getCodeSecu(actepropose.IdTemplateActePG);

            ActePG acte = new ActePG();
            acte.Template = actepropose.template;
            acte.Montant_Honoraire = actepropose.Qte * actepropose.Montant;
            acte.Coeff = actepropose.template.Coeff;
            acte.prestation = actepropose.template.Code;
            acte.Libelle = actepropose.Libelle;
            acte.Quantite = actepropose.Qte;

            acte.NbJours = actepropose.template.NBJours;
            acte.NbMois = actepropose.template.NBMois;

            acte.Id_DEP = -1;



            acte.NeedDEP = actepropose.template.NeedDEP;
            acte.NeedFSE = actepropose.template.NeedFS;
            acte.IdPatient = Idpat;
            acte.IsDecomposed = actepropose.template.IsDecomposed;
            acte.CoeffDecompose = actepropose.template.CoeffDecompose;
            acte.IdSemestrePlanGestionAssocie = -1;
            acte.IdSurvPlanGestionAssocie = -1;
            acte.DateExecution = actepropose.DateExecution.Value;
            //modification nadhemmm
        //    acte.IdDevisAssociate = actepropose.IdDevis;
            
            ActesPGMgmt.InsertActePGWithEcheance(acte, false, false, null);

        }


        public static List<ActePG> AppliquerLePlanPourBaseDiag(DateTime debutTraitement, Proposition proposition, basePatient pat,ref DateTime finTrmnt)
        {
            List<ActePG> resultat = new List<ActePG>();

            DateTime currentDte = debutTraitement.Date;

            foreach (Traitement t in proposition.traitements)
            {
                foreach (Semestre s in t.semestres)
                {
                    if (s.Montant_Honoraire == 0) continue;
                    ActePG acte = new ActePG();
                    acte.Template = s.traitementSecu;
                    acte.Montant_Honoraire = s.Montant_Honoraire;
                    acte.Coeff = s.traitementSecu.Coeff;
                    acte.prestation = s.traitementSecu.Code;



                    if (s.DateFin == null)
                    {
                        acte.NbJours = null;
                        acte.NbMois = null;
                    }
                    else
                    {
                        acte.NbJours = s.traitementSecu.NBJours;
                        acte.NbMois = s.traitementSecu.NBMois;
                    }
                    acte.Id_DEP = s.IdDEPPreAssocier;

                    if ((s.DateDebut != s.DateFin) && (acte.NbJours == 0) && (acte.NbMois == 0) && (s.DateDebut != null) && (s.DateFin != null))
                    {
                        double nbdays = (s.DateFin - s.DateDebut).TotalDays;
                        acte.NbJours = (int)nbdays;
                    }

                    acte.NeedDEP = s.traitementSecu.NeedDEP;
                    acte.NeedFSE = s.traitementSecu.NeedFS;
                    acte.patient = pat;
                    acte.IdPatient = pat.Id;
                    acte.IsDecomposed = s.traitementSecu.IsDecomposed;
                    acte.CoeffDecompose = s.traitementSecu.CoeffDecompose;
                    acte.IdSemestrePlanGestionAssocie = s.Id;
                    acte.IdSurvPlanGestionAssocie = -1;

                    if ((t.Phase == BasCommon_BO.Traitement.EnumPhase.Contention))
                    {
                        acte.DateExecution = currentDte;
                        if (s.CodeSemestre == "A1") acte.NumContention = 1;
                        if (s.CodeSemestre == "A2") acte.NumContention = 2;
                        acte.Libelle = t.Phase.ToString() + " " + s.CodeSemestre;
                        resultat.Add(acte);
                        currentDte = currentDte.AddMonths(acte.NbMois.Value).AddDays(acte.NbJours.Value);
                    }
                    if ((t.Phase == BasCommon_BO.Traitement.EnumPhase.Pédiatrique))
                    {
                        acte.DateExecution = currentDte;
                        acte.NumSemestre = s.NumSemestre;
                        acte.Libelle = t.Libelle;
                        resultat.Add(acte);
                        currentDte = currentDte.AddMonths(acte.NbMois.Value).AddDays(acte.NbJours.Value);
                    }
                    if ((t.Phase == BasCommon_BO.Traitement.EnumPhase.Orthodontique) || (t.Phase == BasCommon_BO.Traitement.EnumPhase.Orthopedique))
                    {
                        acte.DateExecution = currentDte;
                        acte.NumSemestre = s.NumSemestre;
                        acte.Libelle = t.Libelle + " S" + s.NumSemestre.ToString();

                        resultat.Add(acte);
                        currentDte = currentDte.AddMonths(acte.NbMois.Value).AddDays(acte.NbJours.Value);


                    }
                    if ((t.Phase == BasCommon_BO.Traitement.EnumPhase.Adulte) || (t.Phase == BasCommon_BO.Traitement.EnumPhase.FinitionAdulte))
                    {
                        acte.DateExecution = currentDte;
                        acte.NumSemestre = s.NumSemestre;
                        acte.Libelle = t.Libelle;
                        resultat.Add(acte);
                        currentDte = currentDte.AddMonths(acte.NbMois.Value).AddDays(acte.NbJours.Value);

                    }

                    int i = 1;
                    foreach (Surveillance su in s.surveillances)
                    {
                        ActePG a = new ActePG();


                        a.Template = su.traitementSecu;
                        a.Libelle = " Surveillance " + " S" + s.NumSemestre.ToString();
                        a.Montant_Honoraire = su.Montant_Honoraire;
                        acte.NumSemestre = s.NumSemestre;
                        a.IdSurvPlanGestionAssocie = su.Id;

                        a.patient = pat;
                        a.IdPatient = pat.Id;

                        a.DateExecution = currentDte;

                        a.CodePlan = -1;
                        a.NbJours = a.Template.NBJours;
                        a.NbMois = a.Template.NBMois;
                        a.NeedDEP = a.Template.NeedDEP;
                        a.NeedFSE = a.Template.NeedFS;
                        a.CoeffDecompose = a.Template.CoeffDecompose;
                        a.IsDecomposed = a.Template.IsDecomposed;


                        resultat.Add(a);
                        currentDte = currentDte.AddMonths(a.NbMois.Value).AddDays(a.NbJours.Value);
                        i++;
                    }
                }
            }

            if (proposition.echeancestemp==null)
                proposition.echeancestemp = MgmtDevis.get_tempecheances(proposition);
            foreach (ActePG act in resultat)
            {
                foreach (TempEcheanceDefinition ted in proposition.echeancestemp)
                {
                    if (ted.IdSemestre==act.IdSemestrePlanGestionAssocie)
                    {
                        if (act.lstEcheances==null)
                            act.lstEcheances = new List<Echeance>();


                        Echeance ec = new Echeance();
                        ec.acte = act;
                        ec.DateEcheance = ted.DAteEcheance;
                        ec.IdPatient = pat.Id;
                        ec.patient = pat;
                        ec.payeur = ted.payeur;
                        ec.Montant = ted.Montant;
                        ec.Libelle = ted.Libelle;
                        ec.ParPrelevement = ted.ParPrelevement;
                        ec.ParVirement = ted.ParVirement;

                        act.lstEcheances.Add(ec);
                    }
                }

                //modification nadhem
            //    act.IdDevisAssociate = proposition.IdDevis;

                ActesPGMgmt.InsertActePGWithEcheance(act, false,false,null);
            }
            finTrmnt = currentDte;
            return resultat;
        }



        public static Proposition BuildContentionInvisalign1(basePatient CurrentPatient, DateTime DateFinOrthodontie)
        {


            bool isCMU = baseMgmtPatient.IsCMU(CurrentPatient);


            TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(CodesTraitement.CONTENTIONINVISALIGN1);
            if (tmplte == null)
                throw new System.NotSupportedException("L'acte : " + CodesTraitement.CONTENTIONINVISALIGN1 + " n'existe pas");


            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = tmplte.Libelle;


            Traitement t = new Traitement();
            t.Libelle = tmplte.Libelle;
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Contention;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.CONTENTIONINVISALIGN1;

            Semestre s = new Semestre();
            s.CodeSemestre = CodesTraitement.CONTENTIONINVISALIGN1;
            s.DateDebut = DateFinOrthodontie;
            s.DateFin = tmplte.NBMois != null ? s.DateDebut.AddMonths(tmplte.NBMois.Value) : s.DateDebut.AddMonths(12);
            s.Montant_Honoraire = tmplte.Valeur;
            s.Montant_AvantRemise = s.Montant_Honoraire;
            s.traitementSecu = tmplte;
            s.Parent = t;
            s.NumSemestre = 1;

            t.semestres.Add(s);

            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildContentionInvisalign2(basePatient CurrentPatient, DateTime DateFinCont1)
        {
            bool isCMU = baseMgmtPatient.IsCMU(CurrentPatient);

            TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(CodesTraitement.CONTENTIONINVISALIGN2);
            if (tmplte == null)
                throw new System.NotSupportedException("L'acte : " + CodesTraitement.CONTENTIONINVISALIGN2 + " n'existe pas");

            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = tmplte.Libelle;


            Traitement t = new Traitement();
            t.Libelle = tmplte.Libelle;
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Contention;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.CONTENTIONINVISALIGN2;

            Semestre s = new Semestre();
            s.CodeSemestre = CodesTraitement.CONTENTIONINVISALIGN2;
            s.DateDebut = DateFinCont1;
            s.DateFin = (tmplte.NBMois != null) ? s.DateDebut.AddMonths(tmplte.NBMois.Value) : s.DateDebut.AddMonths(12);
            s.Montant_Honoraire = tmplte.Valeur;
            s.Montant_AvantRemise = s.Montant_Honoraire;
            s.traitementSecu = tmplte;
            s.Parent = t;
            s.NumSemestre = 2;

            t.semestres.Add(s);

            p.traitements.Add(t);

            return p;
        }



        

        public static Proposition BuildContention1(basePatient CurrentPatient, DateTime DateFinOrthodontie)
        {


            bool isCMU = baseMgmtPatient.IsCMU(CurrentPatient);
            

            TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(CodesTraitement.CONTENTION1);
            if (tmplte == null)
                throw new System.NotSupportedException("L'acte : " + CodesTraitement.CONTENTION1 + " n'existe pas");


            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = tmplte.Libelle;


            Traitement t = new Traitement();
            t.Libelle = tmplte.Libelle;
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Contention;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.CONTENTION1;

            Semestre s = new Semestre();
            s.CodeSemestre = CodesTraitement.CONTENTION1;
            s.DateDebut = DateFinOrthodontie;
            s.DateFin = tmplte.NBMois!=null?s.DateDebut.AddMonths(tmplte.NBMois.Value): s.DateDebut.AddMonths(12);
            s.Montant_Honoraire = tmplte.Valeur;
            s.Montant_AvantRemise = s.Montant_Honoraire;
            s.traitementSecu = tmplte;
            s.Parent = t;
            s.NumSemestre = 1;

            t.semestres.Add(s);

            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildContention2(basePatient CurrentPatient, DateTime DateFinCont1)
        {
            bool isCMU = baseMgmtPatient.IsCMU(CurrentPatient);
            
            TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(CodesTraitement.CONTENTION2);
            if (tmplte == null)
                throw new System.NotSupportedException("L'acte : " + CodesTraitement.CONTENTION2 + " n'existe pas");

            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = tmplte.Libelle;


            Traitement t = new Traitement();
            t.Libelle = tmplte.Libelle;
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Contention;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.CONTENTION2;

            Semestre s = new Semestre();
            s.CodeSemestre = CodesTraitement.CONTENTION2;
            s.DateDebut = DateFinCont1;
            s.DateFin = (tmplte.NBMois!=null)? s.DateDebut.AddMonths(tmplte.NBMois.Value):s.DateDebut.AddMonths(12);
            s.Montant_Honoraire = tmplte.Valeur;
            s.Montant_AvantRemise = s.Montant_Honoraire;
            s.traitementSecu = tmplte;
            s.Parent = t;
            s.NumSemestre = 2;

            t.semestres.Add(s);

            p.traitements.Add(t);

            return p;
        }
        
        public static Proposition BuildSucette(basePatient CurrentPatient, DateTime DateDEbutTrtmnt, int NbSems)
        {
            TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(CodesTraitement.PEDIATRIE);

            if (tmplte == null)
                throw new System.NotSupportedException("L'acte : " + CodesTraitement.PEDIATRIE + " n'existe pas");



            bool isCMU = baseMgmtPatient.IsCMU(CurrentPatient);
            
            
            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = tmplte.Libelle;


            Traitement t = new Traitement();
            t.Libelle = tmplte.Libelle;
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Pédiatrique;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.PEDIATRIE;



            DateTime dte = DateDEbutTrtmnt;
            for (int i = 0; i < NbSems; i++)
            {

                Semestre s = new Semestre();
                s.traitementSecu = tmplte;
                s.CodeSemestre = CodesTraitement.PEDIATRIE;
                s.DateDebut = dte;
                s.Montant_Honoraire = tmplte.Valeur;
                s.Montant_AvantRemise = s.Montant_Honoraire;




                s.DateFin = dte.AddMonths(s.traitementSecu.NBMois.Value).AddDays(s.traitementSecu.NBJours.Value);
                s.Parent = t;
                s.NumSemestre = 0;

                t.semestres.Add(s);
                dte = s.DateFin;
            }

            p.traitements.Add(t);

            return p;
        }
        
        public static Proposition BuildOrthopedie(int NbSemEntameHorCab,  int nbsem, basePatient CurrentPatient, DateTime DateDEbutTrtmnt, int NbSems)
        {

           
            
            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Orthopedie";


            Traitement t = new Traitement();
            t.Libelle = "Orthopedie";
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Orthopedique;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.ORTHOPEDIE;

            int nbsemEnt = PropositionMgmt.NbSemestreEntames(CurrentPatient.propositions);


            DateTime dte = DateDEbutTrtmnt;
            Semestre s=null;

            for (int i = 0; i < NbSems; i++)
            {

                TemplateActePG tmplte;

                string code = "";
                if (NbSemEntameHorCab + nbsemEnt + i < 6)
                {
                    code = (CodesTraitement.ORTHOPEDIE);
                }
                else
                {
                    code = (CodesTraitement.ORTHOPEDIEHN);
                }

                tmplte = TemplateApctePGMgmt.getCodeSecu(code);

                if (tmplte == null)
                    throw new System.NotSupportedException("L'acte : " + code + " n'existe pas");

                s = new Semestre();
                s.traitementSecu = tmplte;
                s.CodeSemestre = code;
                s.DateDebut = dte;
                s.Montant_Honoraire = tmplte.Valeur;
                s.Montant_AvantRemise = s.Montant_Honoraire;

               

                s.DateFin = dte.AddMonths(s.traitementSecu.NBMois.Value).AddDays(s.traitementSecu.NBJours.Value);
                s.Parent = t;
                s.NumSemestre = nbsem + i;

                t.semestres.Add(s);
                dte = s.DateFin;
            }

            Surveillance su = new Surveillance();
            su.DateDebut = s.DateFin;
            su.DateFin = su.DateDebut.Value.AddMonths(6);
            su.traitementSecu = TemplateApctePGMgmt.getCodeSecu("SURV");
            su.Montant_Honoraire = su.traitementSecu.Valeur;
            

            su.Semestre = s;
            s.surveillances.Add(su);


            p.traitements.Add(t);

            return p;
        }


        /*
        public static Proposition BuildiSeven_Adulte(basePatient CurrentPatient, DateTime DateDebut)
        {
           
            TemplateActePG tmplteEmpOptic = TemplateApctePGMgmt.getCodeSecu("EMPOPTI");
            if (tmplteEmpOptic == null)
                throw new System.NotSupportedException("L'acte : EMPOPTI n'existe pas");

            TemplateActePG tmplteMATINVI = TemplateApctePGMgmt.getCodeSecu("MATINVI");
            if (tmplteMATINVI == null)
                throw new System.NotSupportedException("L'acte : MATINVI n'existe pas");

            TemplateActePG tmplteISEVEN = TemplateApctePGMgmt.getCodeSecu("ISEVEN");
            if (tmplteISEVEN == null)
                throw new System.NotSupportedException("L'acte : ISEVEN n'existe pas");


            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = tmplteISEVEN.Libelle;


            Traitement t = new Traitement();
            t.Libelle = tmplteISEVEN.Libelle;
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Adulte;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.ISEVEN;


            DateTime dte = DateDebut;

            Semestre s = new Semestre();
            s.CodeSemestre = CodesTraitement.ISEVEN;
            s.DateDebut = dte;
            s.DateFin = DateDebut.AddMonths(tmplteISEVEN.NBMois.Value).AddDays(tmplteISEVEN.NBJours.Value);
            s.Montant_Honoraire = tmplteISEVEN.Valeur;
            s.Montant_AvantRemise = s.Montant_Honoraire;
            s.traitementSecu = tmplteISEVEN;


            s.Parent = t;
            s.NumSemestre = -1;

          
                TempEcheanceDefinition ted = new TempEcheanceDefinition();
                ted.acte = null;
                ted.AlreadyPayed = false;
                ted.CanRecalculate = true;
                ted.DAteEcheance = DateDebut.AddMonths(tmplteISEVEN.NBMois.Value).AddDays(tmplteISEVEN.NBJours.Value);
                ted.semestre = s;
                ted.Libelle = tmplteISEVEN.Libelle;
                ted.Montant = tmplteISEVEN.Valeur;
                ted.ParPrelevement = false;
                ted.ParVirement = false;
                ted.payeur = Echeance.typepayeur.patient;

                if (p.echeancestemp == null) p.echeancestemp = new List<TempEcheanceDefinition>();
                p.echeancestemp.Add(ted);
            

            t.semestres.Add(s);


            p.traitements.Add(t);

            return p;
        }

        */

        public static Proposition BuildSetup3D_Adulte(basePatient CurrentPatient, DateTime DateDebut)
        {
           
            TemplateActePG tmplteEmpOptic = TemplateApctePGMgmt.getCodeSecu("EMPOPTI");
            if (tmplteEmpOptic == null)
                throw new System.NotSupportedException("L'acte : EMPOPTI n'existe pas");

            TemplateActePG tmplteMATINVI = TemplateApctePGMgmt.getCodeSecu("MATINVI");
            if (tmplteMATINVI == null)
                throw new System.NotSupportedException("L'acte : MATINVI n'existe pas");

            TemplateActePG tmplteSETUP3D = TemplateApctePGMgmt.getCodeSecu("SETUP3D");
            if (tmplteSETUP3D == null)
                throw new System.NotSupportedException("L'acte : SETUP3D n'existe pas");


            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = tmplteSETUP3D.Libelle;


            Traitement t = new Traitement();
            t.Libelle = tmplteSETUP3D.Libelle;
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Adulte;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.SETUP3D;


            DateTime dte = DateDebut;

            Semestre s = new Semestre();
            s.CodeSemestre = CodesTraitement.SETUP3D;
            s.DateDebut = dte;
            s.DateFin = DateDebut.AddMonths(tmplteSETUP3D.NBMois.Value).AddDays(tmplteSETUP3D.NBJours.Value);
            s.Montant_Honoraire = tmplteEmpOptic.Valeur + tmplteMATINVI.Valeur;
            s.Montant_AvantRemise = s.Montant_Honoraire;
            s.traitementSecu = tmplteSETUP3D;


            s.Parent = t;
            s.NumSemestre = -1;

          
                TempEcheanceDefinition ted = new TempEcheanceDefinition();
                ted.acte = null;
                ted.AlreadyPayed = false;
                ted.CanRecalculate = true;
                ted.DAteEcheance = DateDebut.AddMonths(tmplteSETUP3D.NBMois.Value).AddDays(tmplteSETUP3D.NBJours.Value);
                ted.semestre = s;
                ted.Libelle = tmplteSETUP3D.Libelle;
                ted.Montant = tmplteEmpOptic.Valeur + tmplteMATINVI.Valeur;
                ted.ParPrelevement = false;
                ted.ParVirement = false;
                ted.payeur = Echeance.typepayeur.patient;

                if (p.echeancestemp == null) p.echeancestemp = new List<TempEcheanceDefinition>();
                p.echeancestemp.Add(ted);
            

            t.semestres.Add(s);


            p.traitements.Add(t);

            return p;
        }




        public static Proposition BuildINV_Adulte(string code, basePatient CurrentPatient, DateTime DateDebut, int Duree)
        {

            Proposition p = BuildPropositionAdulte(CurrentPatient, DateDebut, Duree, code);
            //ForGeneve ?
            //AddMatosToProposition(p, "MATINVI");
            return p;
        }

        public static Proposition BuildMBL_Adulte(basePatient CurrentPatient, DateTime DateDebut, int Duree)
        {
            string code = CodesTraitement.ORTHODONTIEADULTEMULTIBAGUELINGUAL;
            Proposition p = BuildPropositionAdulte(CurrentPatient, DateDebut, Duree, code);
            AddMatosToProposition(p, "MATMBL"); 
            return p;
        }

        public static Proposition BuildMBM_Adulte(basePatient CurrentPatient, DateTime DateDebut, int Duree)
        {
            string code = CodesTraitement.ORTHODONTIEADULTEMULTIBAGUEMETAL;
            Proposition p = BuildPropositionAdulte(CurrentPatient, DateDebut, Duree, code);
            AddMatosToProposition(p, "MATMBM");
            return p;
        }

        public static Proposition BuildMBC_Adulte(basePatient CurrentPatient, DateTime DateDebut,int Duree)
        {
            string code = CodesTraitement.ORTHODONTIEADULTEMULTIBAGUECERAMIQUE;
            Proposition p = BuildPropositionAdulte(CurrentPatient, DateDebut, Duree, code);
            AddMatosToProposition(p, "MATMBC");
            return p;
        }

        private static bool AddMatosToProposition(Proposition p, string code)
        {
            TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(code);
            if (tmplte == null)
                return false;

            p.matosassociate = new List<ActePGPropose>();

            ActePGPropose a = new ActePGPropose();
            a.DateExecution = p.traitements[0].semestres[0].DateDebut;
            a.Libelle = tmplte.Libelle;
            a.Montant = tmplte.Valeur;
            a.MontantAvantRemise = tmplte.Valeur;
            a.Optionnel = false;
            a.proposition = p;
            a.Qte = 1;
            a.template = tmplte;


            p.matosassociate.Add(a);

            return true;
        }

        

        private static Proposition BuildPropositionAdulte(basePatient CurrentPatient, DateTime DateDebut, int Duree, string code)
        {
            TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(code);
            if (tmplte == null)
                throw new System.NotSupportedException("L'acte : " + code + " n'existe pas");

            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = tmplte.Libelle;


            Traitement t = new Traitement();
            t.Libelle = tmplte.Libelle;
            t.Phase = CodesTraitement.IsFinitionAdulte(code)?BasCommon_BO.Traitement.EnumPhase.FinitionAdulte: BasCommon_BO.Traitement.EnumPhase.Adulte;
            t.Parent = p;
            t.CodeTraitement = tmplte.Nom;


            DateTime dte = DateDebut;


            if (tmplte.TypeDeReglement == ActePG.TypeReglement.Semestriel)
            {

                for (int i = 1; i <= Duree; i++)
                {

                    Semestre s = new Semestre();
                    s.CodeSemestre = tmplte.Nom;
                    s.DateDebut = dte;
                    s.DateFin = dte.AddMonths(6);
                    s.Montant_Honoraire = tmplte.Valeur;
                    s.Montant_AvantRemise = s.Montant_Honoraire;
                    s.traitementSecu = tmplte;
                    s.NumSemestre = i;

                    TempEcheanceDefinition ted = new TempEcheanceDefinition();
                    ted.acte = null;
                    ted.AlreadyPayed = false;
                    ted.CanRecalculate = true;
                    ted.DAteEcheance = dte.AddMonths(6);
                    ted.semestre = s;
                    ted.Libelle = tmplte.Libelle + " S" + i.ToString();
                    ted.Montant = tmplte.Valeur;
                    ted.ParPrelevement = false;
                    ted.ParVirement = false;
                    ted.payeur = Echeance.typepayeur.patient;

                    if (p.echeancestemp == null) p.echeancestemp = new List<TempEcheanceDefinition>();
                    p.echeancestemp.Add(ted);

                    s.Parent = t;
                    s.NumSemestre = -1;

                    t.semestres.Add(s);
                    dte = dte.AddMonths(6);
                }
            }
            else
                if (tmplte.TypeDeReglement == ActePG.TypeReglement.Forfaitaire)
                {
                    Semestre s = new Semestre();
                    s.CodeSemestre = tmplte.Nom;
                    s.DateDebut = DateDebut;
                    s.DateFin = DateDebut.AddMonths(Duree);
                    s.Montant_Honoraire = tmplte.Valeur;
                    s.Montant_AvantRemise = s.Montant_Honoraire;
                    s.traitementSecu = tmplte;



                    TempEcheanceDefinition ted = new TempEcheanceDefinition();
                    ted.acte = null;
                    ted.AlreadyPayed = false;
                    ted.CanRecalculate = true;
                    ted.DAteEcheance = s.DateFin;
                    ted.semestre = s;
                    ted.Libelle = tmplte.Libelle;
                    ted.Montant = tmplte.Valeur;
                    ted.ParPrelevement = false;
                    ted.ParVirement = false;
                    ted.payeur = Echeance.typepayeur.patient;

                    if (p.echeancestemp == null) p.echeancestemp = new List<TempEcheanceDefinition>();
                    p.echeancestemp.Add(ted);



                    s.Parent = t;
                    s.NumSemestre = -1;

                    t.semestres.Add(s);


                }





            p.traitements.Add(t);
            return p;
        }




        #region ContentionAdulte
        public static Proposition BuildContentionAdulteFil33AcierGBM(basePatient CurrentPatient, DateTime DateDebut, int nbGBM)
        {

            TemplateActePG tmplteGBM = TemplateApctePGMgmt.getCodeSecu("GBM");
            if (tmplteGBM == null)
                throw new System.NotSupportedException("L'acte : GBM n'existe pas");

            TemplateActePG tmplteFIL = TemplateApctePGMgmt.getCodeSecu("33ACIER");
            if (tmplteFIL == null)
                throw new System.NotSupportedException("L'acte : 33ACIER n'existe pas");

           
            TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(CodesTraitement.CONTENTIONADULTE);

            if (tmplte == null)
                throw new System.NotSupportedException("L'acte : " + CodesTraitement.CONTENTIONADULTE + " n'existe pas");

            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Contention Adulte ( Fil 3-3 Acier + " + nbGBM.ToString() + " GBM)";
            p.matosassociate = new List<ActePGPropose>();

            Traitement t = new Traitement();
            t.Libelle = "Contention Adulte ( Fil 3-3 Acier + " + nbGBM.ToString() + " GBM)";



            t.Phase = BasCommon_BO.Traitement.EnumPhase.Adulte;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.CONTENTIONADULTE;

            DateTime dte = DateDebut;


            Semestre s = new Semestre();
            s.CodeSemestre = CodesTraitement.CONTENTIONADULTE;
            s.DateDebut = dte;
            s.Montant_Honoraire = tmplteFIL.Valeur +  (tmplteGBM.Valeur * nbGBM);
            s.Montant_AvantRemise = s.Montant_Honoraire;
            s.traitementSecu = tmplte;
            s.DateFin = dte.AddMonths(tmplte.NBMois == null ? 0 : tmplte.NBMois.Value).AddDays(tmplte.NBJours == 0 ? 0 : tmplte.NBJours.Value);


            s.Parent = t;
            s.NumSemestre = -1;

            t.semestres.Add(s);



            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildContentionAdulteFil33OrGBM(basePatient CurrentPatient, DateTime DateDebut, int nbGBM)
        {

            TemplateActePG tmplteGBM = TemplateApctePGMgmt.getCodeSecu("GBM");
            if (tmplteGBM == null)
                throw new System.NotSupportedException("L'acte : GBM n'existe pas");

            TemplateActePG tmplteFIL = TemplateApctePGMgmt.getCodeSecu("33OR");
            if (tmplteFIL == null)
                throw new System.NotSupportedException("L'acte : 33OR n'existe pas");

            TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(CodesTraitement.CONTENTIONADULTE);
            if (tmplte == null)
                throw new System.NotSupportedException("L'acte : " + CodesTraitement.CONTENTIONADULTE + " n'existe pas");

            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Contention Adulte ( Fil 3-3 Or + " + nbGBM.ToString() + " GBM)";
            p.matosassociate = new List<ActePGPropose>();

            Traitement t = new Traitement();
            t.Libelle = "Contention Adulte ( Fil 3-3 Or + " + nbGBM.ToString() + " GBM)";



            t.Phase = BasCommon_BO.Traitement.EnumPhase.Adulte;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.CONTENTIONADULTE;

            DateTime dte = DateDebut;


            Semestre s = new Semestre();
            s.CodeSemestre = CodesTraitement.CONTENTIONADULTE;
            s.DateDebut = dte;
            s.Montant_Honoraire = tmplteFIL.Valeur + (tmplteGBM.Valeur * nbGBM);
            s.Montant_AvantRemise = s.Montant_Honoraire;
            s.traitementSecu = tmplte;
            s.DateFin = dte.AddMonths(tmplte.NBMois == null ? 0 : tmplte.NBMois.Value).AddDays(tmplte.NBJours == 0 ? 0 : tmplte.NBJours.Value);


            s.Parent = t;
            s.NumSemestre = -1;

            t.semestres.Add(s);



            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildContentionAdulteFil33Acier(basePatient CurrentPatient, DateTime DateDebut)
        {

           
            TemplateActePG tmplteFIL = TemplateApctePGMgmt.getCodeSecu("33ACIER");
            if (tmplteFIL == null)
                throw new System.NotSupportedException("L'acte : 33ACIER n'existe pas");


            TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(CodesTraitement.CONTENTIONADULTE);

            if (tmplte == null)
                throw new System.NotSupportedException("L'acte : " + CodesTraitement.CONTENTIONADULTE + " n'existe pas");

            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Contention Adulte (Fil 3-3 Acier)";
            p.matosassociate = new List<ActePGPropose>();

            Traitement t = new Traitement();
            t.Libelle = "Contention Adulte ( Fil 3-3 Acier)";



            t.Phase = BasCommon_BO.Traitement.EnumPhase.Adulte;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.CONTENTIONADULTE;

            DateTime dte = DateDebut;


            Semestre s = new Semestre();
            s.CodeSemestre = CodesTraitement.CONTENTIONADULTE;
            s.DateDebut = dte;
            s.Montant_Honoraire = tmplteFIL.Valeur ;
            s.Montant_AvantRemise = s.Montant_Honoraire;
            s.traitementSecu = tmplte;
            s.DateFin = dte.AddMonths(tmplte.NBMois == null ? 0 : tmplte.NBMois.Value).AddDays(tmplte.NBJours == 0 ? 0 : tmplte.NBJours.Value);


            s.Parent = t;
            s.NumSemestre = -1;

            t.semestres.Add(s);



            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildContentionAdulteFil33Or(basePatient CurrentPatient, DateTime DateDebut)
        {

          
            TemplateActePG tmplteFIL = TemplateApctePGMgmt.getCodeSecu("33OR");
            if (tmplteFIL == null)
                throw new System.NotSupportedException("L'acte : 33OR n'existe pas");

            TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(CodesTraitement.CONTENTIONADULTE);
            if (tmplte == null)
                throw new System.NotSupportedException("L'acte : " + CodesTraitement.CONTENTIONADULTE + " n'existe pas");

            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Contention Adulte ( Fil 3-3 Or)";
            p.matosassociate = new List<ActePGPropose>();

            Traitement t = new Traitement();
            t.Libelle = "Contention Adulte ( Fil 3-3 Or)";



            t.Phase = BasCommon_BO.Traitement.EnumPhase.Adulte;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.CONTENTIONADULTE;

            DateTime dte = DateDebut;


            Semestre s = new Semestre();
            s.CodeSemestre = CodesTraitement.CONTENTIONADULTE;
            s.DateDebut = dte;
            s.Montant_Honoraire = tmplteFIL.Valeur;
            s.Montant_AvantRemise = s.Montant_Honoraire;
            s.traitementSecu = tmplte;
            s.DateFin = dte.AddMonths(tmplte.NBMois == null ? 0 : tmplte.NBMois.Value).AddDays(tmplte.NBJours == 0 ? 0 : tmplte.NBJours.Value);


            s.Parent = t;
            s.NumSemestre = -1;

            t.semestres.Add(s);



            p.traitements.Add(t);

            return p;
        }
       
        public static Proposition BuildContentionAdulteVIVERA(basePatient CurrentPatient, DateTime DateDebut, int nbVivera)
        {

            TemplateActePG tmplteGBM = TemplateApctePGMgmt.getCodeSecu("VIVERA");
            if (tmplteGBM == null)
                throw new System.NotSupportedException("L'acte : VIVERA n'existe pas");

            TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(CodesTraitement.CONTENTIONADULTE);

            if (tmplte == null)
                throw new System.NotSupportedException("L'acte : " + CodesTraitement.CONTENTIONADULTE + " n'existe pas");

            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Contention Adulte (" + nbVivera.ToString() + " VIVERA)";
            p.matosassociate = new List<ActePGPropose>();

            Traitement t = new Traitement();
            t.Libelle = "Contention Adulte (" + nbVivera.ToString() + " VIVERA)";



            t.Phase = BasCommon_BO.Traitement.EnumPhase.Adulte;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.CONTENTIONADULTE;

            DateTime dte = DateDebut;


            Semestre s = new Semestre();
            s.CodeSemestre = CodesTraitement.CONTENTIONADULTE;
            s.DateDebut = dte;
            s.Montant_Honoraire = tmplteGBM.Valeur * nbVivera;
            s.Montant_AvantRemise = s.Montant_Honoraire;
            s.traitementSecu = tmplte;
            s.DateFin = dte.AddMonths(tmplte.NBMois == null ? 0 : tmplte.NBMois.Value).AddDays(tmplte.NBJours == 0 ? 0 : tmplte.NBJours.Value);


            s.Parent = t;
            s.NumSemestre = -1;

            t.semestres.Add(s);



            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildContentionAdulteGBM(basePatient CurrentPatient, DateTime DateDebut,int nbGBM)
        {

            TemplateActePG tmplteGBM = TemplateApctePGMgmt.getCodeSecu("GBM");
            if (tmplteGBM == null)
                throw new System.NotSupportedException("L'acte : GBM n'existe pas");

            TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(CodesTraitement.CONTENTIONADULTE);

            if (tmplte == null)
                throw new System.NotSupportedException("L'acte : " + CodesTraitement.CONTENTIONADULTE + " n'existe pas");

            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Contention Adulte ("+nbGBM.ToString()+" GBM)" ;
            p.matosassociate = new List<ActePGPropose>();
            
            Traitement t = new Traitement();
            t.Libelle = "Contention Adulte (" + nbGBM.ToString() + " GBM)";

            

            t.Phase = BasCommon_BO.Traitement.EnumPhase.Adulte;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.CONTENTIONADULTE;

            DateTime dte = DateDebut;


            Semestre s = new Semestre();
            s.CodeSemestre = CodesTraitement.CONTENTIONADULTE;
            s.DateDebut = dte;
            s.Montant_Honoraire = tmplteGBM.Valeur*nbGBM;
            s.Montant_AvantRemise = s.Montant_Honoraire;
            s.traitementSecu = tmplte;
            s.DateFin = dte.AddMonths(tmplte.NBMois==null?0:tmplte.NBMois.Value).AddDays(tmplte.NBJours==0?0:tmplte.NBJours.Value);
            

            s.Parent = t;
            s.NumSemestre = -1;

            t.semestres.Add(s);



            p.traitements.Add(t);

            return p;
        }
        #endregion

        public static int NbSemestreEntames(List<Proposition> props)
        {
            int nbSemestreEntames = 0;
            foreach (Proposition p in props)
            {
                if (p.Etat != Proposition.EtatProposition.Accepté) continue;


                foreach (Traitement t in p.traitements)
                    foreach (Semestre s in t.semestres)
                        if (s.traitementSecu.Code.Code == "TO")
                            nbSemestreEntames++;


            }

            return nbSemestreEntames;
        }

        public static Proposition BuildMBM(int NbSemEntameHorCab, int nbsem, basePatient CurrentPatient, DateTime DateFinOrthopedie,  int NbSems)
        {

            bool isCMU = baseMgmtPatient.IsCMU(CurrentPatient);
            

            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Traitement Orthodontique (MBM)";


            Traitement t = new Traitement();
            t.Libelle = "Orthodontie Multibague Metal";
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Orthodontique;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.ORTHODONTIEMULTIBAGUEMETAL;

            int nbsemEnt = PropositionMgmt.NbSemestreEntames(CurrentPatient.propositions);

            DateTime dte = DateFinOrthopedie;
            for (int i = 0; i < NbSems; i++)
            {
                string code = "";

                if (NbSemEntameHorCab + nbsemEnt + i < 6)
                    code = CodesTraitement.ORTHODONTIEMULTIBAGUEMETAL;
                else
                    code = CodesTraitement.ORTHODONTIEMULTIBAGUEMETALHN;



                TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(code);
                if (tmplte == null)
                    throw new System.NotSupportedException("L'acte : " + code + " n'existe pas");

           
                Semestre s = new Semestre();
                s.CodeSemestre = code;
                s.DateDebut = dte;
                s.Montant_Honoraire = tmplte.Valeur;
                s.Montant_AvantRemise = s.Montant_Honoraire;
                s.traitementSecu = tmplte;

                s.Parent = t;
                s.NumSemestre = nbsem + i;

                s.DateFin = dte.AddMonths(s.traitementSecu.NBMois.Value).AddDays(s.traitementSecu.NBJours.Value);

                t.semestres.Add(s);

                dte = s.DateFin;
            }
            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildMBC(int NbSemEntameHorCab, int nbsem, basePatient CurrentPatient, DateTime DateFinOrthopedie,  int NbSems)
        {
            bool isCMU = baseMgmtPatient.IsCMU(CurrentPatient);
            
            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Traitement Orthodontique (MBC)";


            Traitement t = new Traitement();
            t.Libelle = "Orthodontie Multibague Céramique";
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Orthodontique;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.ORTHODONTIEMULTIBAGUECERAMIQUE;


            DateTime dte = DateFinOrthopedie;
            int nbsemEnt = PropositionMgmt.NbSemestreEntames(CurrentPatient.propositions);

            for (int i = 0; i < NbSems; i++)
            {


                string code = "";

                if (NbSemEntameHorCab + nbsemEnt + i < 6)
                    code = CodesTraitement.ORTHODONTIEMULTIBAGUECERAMIQUE;
                else
                    code = CodesTraitement.ORTHODONTIEMULTIBAGUECERAMIQUEHN;

                TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(code);
                if (tmplte == null)
                    throw new System.NotSupportedException("L'acte : " + code + " n'existe pas");

                Semestre s = new Semestre();
                s.DateDebut = dte;
                s.Montant_Honoraire = tmplte.Valeur;
                s.Montant_AvantRemise = s.Montant_Honoraire;
                s.Parent = t;
                s.NumSemestre = nbsem + i;
                
                    s.CodeSemestre = code;
                    s.traitementSecu = tmplte;
                
                s.DateFin = dte.AddMonths(s.traitementSecu.NBMois.Value).AddDays(s.traitementSecu.NBJours.Value);

                t.semestres.Add(s);

                dte = s.DateFin;
            }


            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildMBL(int NbSemEntameHorCab, int nbsem, basePatient CurrentPatient, DateTime DateFinOrthopedie,  int NbSems)
        {

            bool isCMU = baseMgmtPatient.IsCMU(CurrentPatient);
            

            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Traitement Orthodontique (MBL)";


            Traitement t = new Traitement();
            t.Libelle = "Orthodontie Multibague Lingual";
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Orthodontique;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.ORTHODONTIEMULTIBAGUELINGUAL;


            DateTime dte = DateFinOrthopedie;
            int nbsemEnt = PropositionMgmt.NbSemestreEntames(CurrentPatient.propositions);

            for (int i = 0; i < NbSems; i++)
            {

                string code = "";

                if (NbSemEntameHorCab + nbsemEnt + i < 6)
                    code = CodesTraitement.ORTHODONTIEMULTIBAGUELINGUAL;
                else
                    code = CodesTraitement.ORTHODONTIEMULTIBAGUELINGUALHN;

                TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(code);

                if (tmplte == null)
                    throw new System.NotSupportedException("L'acte : " + code + " n'existe pas");


                Semestre s = new Semestre();
                s.DateDebut = dte;
                s.Montant_Honoraire = tmplte.Valeur;
                s.Montant_AvantRemise = s.Montant_Honoraire;


              
                    s.CodeSemestre = code;
                    s.traitementSecu = tmplte;
                

                s.DateFin = dte.AddMonths(s.traitementSecu.NBMois.Value).AddDays(s.traitementSecu.NBJours.Value);

                s.Parent = t;
                s.NumSemestre = nbsem + i;

                t.semestres.Add(s);

                dte = s.DateFin;
            }

            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildINVTEEN(int NbSemEntameHorCab, int nbsem, basePatient CurrentPatient, DateTime DateFinOrthopedie,  int NbSems)
        {

            bool isCMU = baseMgmtPatient.IsCMU(CurrentPatient);
            

            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Traitement Orthodontique (INVTEEN)";


            Traitement t = new Traitement();
            t.Libelle = "Orthodontie Invisalign Teen";
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Orthodontique;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.ORTHODONTIEINVISALIGN;

            int nbsemEnt = PropositionMgmt.NbSemestreEntames(CurrentPatient.propositions);

            DateTime dte = DateFinOrthopedie;

            for (int i = 0; i < NbSems; i++)
            {

                string code = "";

                if (NbSemEntameHorCab + nbsemEnt + i < 6)
                    code = CodesTraitement.ORTHODONTIEINVISALIGN;
                else
                    code = CodesTraitement.ORTHODONTIEINVISALIGNHN;

                TemplateActePG tmplte = TemplateApctePGMgmt.getCodeSecu(code);
                if (tmplte == null)
                    throw new System.NotSupportedException("L'acte : " + code + " n'existe pas");



                Semestre s = new Semestre();
                s.DateDebut = dte;
                s.Montant_Honoraire = tmplte.Valeur;
                s.Montant_AvantRemise = s.Montant_Honoraire;
                s.Parent = t;
                s.NumSemestre = nbsem + i;


              
                    s.CodeSemestre = code;
                    s.traitementSecu = tmplte;
                
                s.DateFin = dte.AddMonths(s.traitementSecu.NBMois.Value).AddDays(s.traitementSecu.NBJours.Value);

                t.semestres.Add(s);

                dte = s.DateFin;

            }
            p.traitements.Add(t);

            return p;
        }




    }
}
