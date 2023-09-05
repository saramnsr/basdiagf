using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;
using BasCommon_BO;
using BasCommon_BL;

namespace BASEDiag_BL
{
    public static class PropositionMgmt
    {


      

        public static List<ModeleDePropositions> getModeles()
        {
            List<ModeleDePropositions> lst = new List<ModeleDePropositions>();
            DataTable dtmdl = DAC.getModeleDePropositions();
            foreach (DataRow dr in dtmdl.Rows)
                lst.Add(Builders.BuildModeleDeProposition(dr));

            return lst;
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

        /*
        public static void AccepterProposition(List<Proposition> propositions, Proposition proposition, DateTime dteDebut)
        {

            int SemStartedAt = GetNbSemestreAlreadyDone(propositions);

            proposition.Etat = Proposition.EtatProposition.Accepté;
            proposition.DateAcceptation = DateTime.Now;
            MgmtDevis.AccepterDevis(proposition.IdDevis);

            int i = 1;
            DateTime dte = dteDebut;
            foreach(Traitement t in proposition.traitements)
                foreach(Semestre s in t.semestres)
                {
                    TimeSpan ts = s.DateDebut == null ? (s.DateFin.Value - DateTime.MinValue) : (s.DateFin.Value - s.DateDebut.Value);
                    int nbMois = ((int)ts.TotalDays) / 30;
                    s.DateDebut = dte;
                    s.DateFin = s.DateDebut.Value.AddMonths(nbMois);
                    s.NumSemestre = SemStartedAt+i;
                    dte = s.DateFin.Value;
                    SemestreMgmt.UpdateSemestre(s);
                    i++;
                }


            DAC.AccepterProposition(proposition);
        }
        */
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

                    int i = 1;
                    foreach (Traitement t in proposition.traitements)
                        foreach (Semestre s in t.semestres)
                        {
                            if (s.DateFin == null)
                            {
                                s.DateDebut = dte;
                                s.DateFin = null;
                                s.NumSemestre = SemStartedAt + i;
                                SemestreMgmt.UpdateSemestre(s);
                            }
                            else
                            {
                                TimeSpan ts = s.DateDebut == null ? (s.DateFin.Value - DateTime.MinValue) : (s.DateFin.Value - s.DateDebut.Value);
                                int nbMois = ((int)ts.TotalDays) / 30;
                                s.DateDebut = dte;
                                s.DateFin = s.DateDebut.Value.AddMonths(nbMois);
                                s.NumSemestre = SemStartedAt + i;
                                dte = s.DateFin.Value;
                                SemestreMgmt.UpdateSemestre(s);
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
            /*
            foreach (PoseAppareil app in proposition.poseAppareils)
                foreach (string sr in app.appareil.Risques)
                    lst.Add(sr);

            */
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
                        nbjours += (int)((s.DateFin.Value - s.DateDebut.Value).TotalDays);

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
                        SemestreMgmt.AddSemestre(s);
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
                Proposition p = Builders.BuildProposition(dr);
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
                Proposition p = Builders.BuildProposition(dr);
                p.patient = patient;
                p.traitements = TraitementMgmt.getTraitements(p);

                //p.poseAppareils = PoseAppareilMgmt.getPoseAppareils(p);

                lst.Add(p);
            }



            return lst;
        }

        public static List<Proposition> getPropositions(basePatient patient)
        {
            DataTable dt = DAC.getPropositions(patient);

            List<Proposition> lst = new List<Proposition>();

            foreach (DataRow dr in dt.Rows)
            {
                Proposition pr = Builders.BuildProposition(dr);
                pr.patient = patient;

                pr.traitements = TraitementMgmt.getTraitements(pr);

                // pr.poseAppareils = PoseAppareilMgmt.getPoseAppareils(pr);


                lst.Add(pr);

            }

            return lst;
        }


        public static List<Proposition> getPropositions(Devis devis)
        {
            DataTable dt = DAC.getPropositions(devis);

            List<Proposition> lst = new List<Proposition>();

            foreach (DataRow dr in dt.Rows)
            {
                Proposition pr = Builders.BuildProposition(dr);
                pr.patient = devis.patient;

                pr.traitements = TraitementMgmt.getTraitements(pr);

                //pr.poseAppareils = PoseAppareilMgmt.getPoseAppareils(pr);


                lst.Add(pr);

            }

            return lst;
        }




        public static Proposition BuildContention1(basePatient CurrentPatient, DateTime DateFinOrthodontie, double MontantContention, int NbMonth)
        {
            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Contention Année 1";


            Traitement t = new Traitement();
            t.Libelle = "Contention 1";
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Contention;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.CONTENTION1;

            Semestre s = new Semestre();
            s.CodeSemestre = "CONT";
            s.DateDebut = DateFinOrthodontie;
            s.DateFin = s.DateDebut.Value.AddMonths(NbMonth);
            s.Montant_Honoraire = MontantContention;
            s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("CONT1");
            s.Parent = t;
            s.NumSemestre = 1;

            t.semestres.Add(s);

            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildContention2(basePatient CurrentPatient, DateTime DateFinCont1, double MontantContention, int NbMonth)
        {
            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Contention Année 2";


            Traitement t = new Traitement();
            t.Libelle = "Contention 2";
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Contention;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.CONTENTION2;

            Semestre s = new Semestre();
            s.CodeSemestre = "CONT";
            s.DateDebut = DateFinCont1;
            s.DateFin = s.DateDebut.Value.AddMonths(NbMonth);
            s.Montant_Honoraire = MontantContention;
            s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("CONT2");
            s.Parent = t;
            s.NumSemestre = 1;

            t.semestres.Add(s);

            p.traitements.Add(t);

            return p;
        }


        public static Proposition BuildSucette(basePatient CurrentPatient, DateTime DateDEbutTrtmnt, double Montant, int NbSems)
        {
            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Traitement Sucette ";


            Traitement t = new Traitement();
            t.Libelle = "Sucette";
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Pédiatrique;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.SUCETTE;



            DateTime dte = DateDEbutTrtmnt;
            for (int i = 0; i < NbSems; i++)
            {

                Semestre s = new Semestre();
                s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("PEDIATRIE");
                s.CodeSemestre = "PEDIATRIE";
                s.DateDebut = dte;
                s.Montant_Honoraire = Montant;




                s.DateFin = dte.AddMonths(s.traitementSecu.NBMois.Value).AddDays(s.traitementSecu.NBJours.Value);
                s.Parent = t;
                s.NumSemestre = 0;

                t.semestres.Add(s);
                dte = s.DateFin.Value;
            }

            p.traitements.Add(t);

            return p;
        }


        public static Proposition BuildOrthopedie(int NbSemEntameHorCab,  int nbsem, basePatient CurrentPatient, DateTime DateDEbutTrtmnt, double Montant, int NbSems)
        {
            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Traitement Orthopedique ";


            Traitement t = new Traitement();
            t.Libelle = "Orthopedique";
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Orthopedique;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.ORTHOPEDIE;

            int nbsemEnt = PropositionMgmt.NbSemestreEntames(CurrentPatient.propositions);


            DateTime dte = DateDEbutTrtmnt;
            for (int i = 0; i < NbSems; i++)
            {

                Semestre s = new Semestre();
                s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("ORTHP");
                s.CodeSemestre = "ORTHP";
                s.DateDebut = dte;
                s.Montant_Honoraire = Montant;

                if (NbSemEntameHorCab + nbsemEnt + i < 6)
                {
                    s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("ORTHP");
                }
                else
                {
                    s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("ORTHP_HN");
                }

                s.DateFin = dte.AddMonths(s.traitementSecu.NBMois.Value).AddDays(s.traitementSecu.NBJours.Value);
                s.Parent = t;
                s.NumSemestre = nbsem + i;

                t.semestres.Add(s);
                dte = s.DateFin.Value;
            }

            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildMBM_Adulte(basePatient CurrentPatient, DateTime DateFinOrthopedie, double MontantMBM)
        {
            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Traitement Orthodontique Adulte (MBM)";


            Traitement t = new Traitement();
            t.Libelle = "Orthodontie Multibague Metal (HN)";
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Adulte;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.ORTHODONTIEADULTEMULTIBAGUEMETAL;


            DateTime dte = DateFinOrthopedie;

            Semestre s = new Semestre();
            s.CodeSemestre = "MBMETAL_HN";
            s.DateDebut = DateFinOrthopedie;
            s.Montant_Honoraire = MontantMBM;
            s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("MBMETAL_HN");
            s.DateFin = null;

            s.Parent = t;
            s.NumSemestre = -1;

            t.semestres.Add(s);


            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildMBC_Adulte(basePatient CurrentPatient, DateTime DateFinOrthopedie, double MontantMBC)
        {

            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Traitement Orthodontique Adulte (MBC)";


            Traitement t = new Traitement();
            t.Libelle = "Orthodontie Multibague Céramique";
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Orthodontique;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.ORTHODONTIEADULTEMULTIBAGUECERAMIQUE;


            DateTime dte = DateFinOrthopedie;


            Semestre s = new Semestre();
            s.CodeSemestre = "Multibague Céramique";
            s.DateDebut = DateFinOrthopedie;
            s.Montant_Honoraire = MontantMBC;
            s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("MBCERAM_HN");
            s.DateFin = null;
            s.Parent = t;
            s.NumSemestre = -1;

            t.semestres.Add(s);



            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildINV_Adulte(string code, basePatient CurrentPatient, DateTime DateFinOrthopedie, double Montant)
        {
            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Traitement Invisalign";

            if (code == CodesTraitement.ORTHODONTIEADULTEINVARCADE)
                p.libelle = "Traitement Invisalign 'Arcade'";
            if (code == CodesTraitement.ORTHODONTIEADULTEINVCOMPLET)
                p.libelle = "Traitement Invisalign complet sans correstion";
            if (code == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETCHIR)
                p.libelle = "Traitement Invisalign complet avec chirurgie";
            if (code == CodesTraitement.ORTHODONTIEADULTEINVLIGHT)
                p.libelle = "Traitement Invisalign Light";
            if (code == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETDISJ)
                p.libelle = "Traitement Invisalign complet avec Disjoncteur maxilaire";
            if (code == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETCORR)
                p.libelle = "Traitement Invisalign complet avec correction";
            if (code == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETDISJCHIR)
                p.libelle = "Traitement Invisalign complet avec disjoncteur et chirurgie";



            Traitement t = new Traitement();
            t.Libelle = "Orthodontie Invisalign";

            if (code == CodesTraitement.ORTHODONTIEADULTEINVARCADE)
                t.Libelle = "Invisalign 'Arcade'";
            if (code == CodesTraitement.ORTHODONTIEADULTEINVCOMPLET)
                t.Libelle = "Invisalign complet sans correstion";
            if (code == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETCHIR)
                t.Libelle = "Invisalign complet avec chirurgie";
            if (code == CodesTraitement.ORTHODONTIEADULTEINVLIGHT)
                t.Libelle = "Invisalign Light";
            if (code == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETDISJ)
                t.Libelle = "Invisalign complet avec Disjoncteur maxilaire";
            if (code == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETCORR)
                t.Libelle = "Invisalign complet avec correction";
            if (code == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETDISJCHIR)
                t.Libelle = "Invisalign complet avec disjoncteur et chirurgie";

            t.Phase = BasCommon_BO.Traitement.EnumPhase.Adulte;
            t.Parent = p;
            t.CodeTraitement = code;

            DateTime dte = DateFinOrthopedie;


            Semestre s = new Semestre();
            s.CodeSemestre = "HN";
            s.DateDebut = DateFinOrthopedie;
            s.Montant_Honoraire = Montant;
            s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("HN");
            s.DateFin = null;


            s.Parent = t;
            s.NumSemestre = -1;

            t.semestres.Add(s);



            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildMBL_Adulte(basePatient CurrentPatient, DateTime DateFinOrthopedie, double MontantMBC)
        {
            Proposition p = new Proposition();
            p.DateProposition = DateTime.Now;
            p.patient = CurrentPatient;
            p.DateEvenement = DateTime.Now;
            p.DateAcceptation = null;
            p.Etat = Proposition.EtatProposition.NonImprimé;
            p.libelle = "Traitement Orthodontique Adulte (MBL)";


            Traitement t = new Traitement();
            t.Libelle = "Orthodontie Multibague Lingual (HN)";
            t.Phase = BasCommon_BO.Traitement.EnumPhase.Adulte;
            t.Parent = p;
            t.CodeTraitement = CodesTraitement.ORTHODONTIEADULTEMULTIBAGUELINGUAL;

            DateTime dte = DateFinOrthopedie;

            Semestre s = new Semestre();
            s.CodeSemestre = "MBLING_HN";
            s.DateDebut = DateFinOrthopedie;
            s.Montant_Honoraire = MontantMBC;
            s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("MBLING_HN");
            s.DateFin = null;

            s.Parent = t;
            s.NumSemestre = -1;

            t.semestres.Add(s);

            p.traitements.Add(t);

            return p;
        }

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

        public static Proposition BuildMBM(int NbSemEntameHorCab, int nbsem, basePatient CurrentPatient, DateTime DateFinOrthopedie, double MontantMBM, int NbSems)
        {
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

                Semestre s = new Semestre();
                s.CodeSemestre = "MBMETAL";
                s.DateDebut = DateFinOrthopedie;
                s.Montant_Honoraire = MontantMBM;
                s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("MBMETAL");

                s.Parent = t;
                s.NumSemestre = nbsem + i;

                if (NbSemEntameHorCab + nbsemEnt + i < 6)
                {
                    s.CodeSemestre = "MBMETAL";
                    s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("MBMETAL");
                }
                else
                {
                    s.CodeSemestre = "MBMETAL HN";
                    s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("MBMETAL_HN");
                }
                s.DateFin = dte.AddMonths(s.traitementSecu.NBMois.Value).AddDays(s.traitementSecu.NBJours.Value);

                t.semestres.Add(s);
            }
            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildMBC(int NbSemEntameHorCab, int nbsem, basePatient CurrentPatient, DateTime DateFinOrthopedie, double MontantMBC, int NbSems)
        {

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

                Semestre s = new Semestre();
                s.DateDebut = DateFinOrthopedie;
                s.Montant_Honoraire = MontantMBC;
                s.Parent = t;
                s.NumSemestre = nbsem + i;
                if (NbSemEntameHorCab + nbsemEnt + i < 6)
                {
                    s.CodeSemestre = "MBCERAM";
                    s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("MBCERAM");
                }
                else
                {
                    s.CodeSemestre = "MBCERAM HN";
                    s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("MBCERAM_HN");
                }
                s.DateFin = dte.AddMonths(s.traitementSecu.NBMois.Value).AddDays(s.traitementSecu.NBJours.Value);

                t.semestres.Add(s);
            }


            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildMBL(int NbSemEntameHorCab, int nbsem, basePatient CurrentPatient, DateTime DateFinOrthopedie, double MontantMBC, int NbSems)
        {
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
                Semestre s = new Semestre();
                s.DateDebut = DateFinOrthopedie;
                s.Montant_Honoraire = MontantMBC;


                if (NbSemEntameHorCab + nbsemEnt + i < 6)
                {
                    s.CodeSemestre = "MBLING";
                    s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("MBLING");
                }
                else
                {
                    s.CodeSemestre = "MBLING HN";
                    s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("MBLING_HN");
                }
                s.DateFin = dte.AddMonths(s.traitementSecu.NBMois.Value).AddDays(s.traitementSecu.NBJours.Value);

                s.Parent = t;
                s.NumSemestre = nbsem + i;

                t.semestres.Add(s);
            }

            p.traitements.Add(t);

            return p;
        }

        public static Proposition BuildINVTEEN(int NbSemEntameHorCab, int nbsem, basePatient CurrentPatient, DateTime DateFinOrthopedie, double MontantINVTEEN, int NbSems)
        {
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
                Semestre s = new Semestre();
                s.DateDebut = DateFinOrthopedie;
                s.Montant_Honoraire = MontantINVTEEN;
                s.Parent = t;
                s.NumSemestre = nbsem + i;


                if (NbSemEntameHorCab + nbsemEnt + i < 6)
                {
                    s.CodeSemestre = "INVTEEN";
                    s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("INVTEEN");
                }
                else
                {
                    s.CodeSemestre = "INVTEEN HN";
                    s.traitementSecu = TemplateApctePGMgmt.getCodeSecu("INVTEEN_HN");
                }
                s.DateFin = dte.AddMonths(s.traitementSecu.NBMois.Value).AddDays(s.traitementSecu.NBJours.Value);

                t.semestres.Add(s);


            }
            p.traitements.Add(t);

            return p;
        }




    }


    
}
