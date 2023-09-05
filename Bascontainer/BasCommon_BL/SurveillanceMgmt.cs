using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using BasCommon_DAL;
using System.Data;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class SurveillanceMgmt
    {


        public static void UpdateSurveillance(Surveillance surv)
        {
            if (surv.Id != -1)
                DAC.UpdateSurveillance(surv);


        }

        public static void DeleteSurveillance(Surveillance surv)
        {
            DAC.DeleteSurveillance(surv);


        }

        public static void AddSurveillance(Surveillance surv)
        {
            if (surv.Id == -1)
                DAC.InsertSurveillance(surv);


        }
        public static List<Surveillance> getSurveillancesOLD(Semestre sem)
        {
            List<Surveillance> lst = new List<Surveillance>();
            DataTable dt = DAC.getSurveillances(sem);

            foreach (DataRow dr in dt.Rows)
            {
                Surveillance s = Builders.BuildSurveillance.Build(dr);
                lst.Add(s);
            }
            return lst;
        }
        public static List<Surveillance> getSurveillances(Semestre sem)
        {
            List<Surveillance> lst = new List<Surveillance>();
            JArray json = DAC.getMethodeJsonArray("/Surveillances/" + sem.Id);

            foreach (JObject dr in json)
            {
                Surveillance s = Builders.BuildSurveillance.BuildJ(dr);
                lst.Add(s);
            }
            return lst;
        }

        public static List<Surveillance> getSurveillances(basePatient pat)
        {
            List<Surveillance> lst = new List<Surveillance>();
            DataTable dt = DAC.getSurveillances(pat);

            foreach (DataRow dr in dt.Rows)
            {
                Surveillance s = Builders.BuildSurveillance.Build(dr);
                lst.Add(s);
            }
            return lst;
        }

        public static void DecalerPlanDeGestion(DateTime APartirDe, basePatient patient, int NbDaysToMove, int NbMonthsToMove)
        {
            DAC.DecalerPlanDeGestion(APartirDe, patient, NbDaysToMove, NbMonthsToMove);
        }

        public static void AjouterUneSurveillance(Surveillance su, DateTime Debut, basePatient pat)
        {
            DateTime DateDeRef = Debut;

            TemplateActePG surv = TemplateApctePGMgmt.getCodeSecu("SURV");


            DateTime dt = DateTime.Now.AddMonths(surv.NBMois.Value).AddDays(surv.NBJours.Value);
            int nbmonth = (int)Math.Round((dt - DateTime.Now).TotalDays / 30.41);
            DecalerPlanDeGestion(Debut.AddDays(-1), pat, 0, nbmonth);



            ActePG a = null;

            a = new ActePG();
            a.Template = surv;
            a.Libelle = surv.Libelle;

            a.Montant_Honoraire = surv.Valeur;

            a.patient = pat;
            a.DateExecution = DateDeRef;
            a.CodePlan = -1;
            a.NbJours = surv.NBJours;
            a.NbMois = surv.NBMois;
            a.NeedDEP = surv.NeedDEP;
            a.NeedFSE = surv.NeedFS;
            a.CoeffDecompose = surv.CoeffDecompose;
            a.IsDecomposed = surv.IsDecomposed;
            a.IdSemestrePlanGestionAssocie = -1;
            a.IdSurvPlanGestionAssocie = su.Id;

            ActesPGMgmt.InsertActePG(a);

            Echeance e = new Echeance();
            e.DateEcheance = DateDeRef.AddDays(a.NbJours.Value).AddMonths(a.NbMois.Value);
            e.patient = pat;
            e.Libelle = surv.Libelle;
            e.Montant = surv.Valeur;

            e.Montant = surv.Valeur;


            e.ParPrelevement = false;
            e.ParVirement = false;
            e.acte = a;

            EcheancesMgmt.InsertEcheance(e);







        }

        public static double getTotal(Surveillance s)
        {

            double _TarifTotal = 0;


            if ((s.Semestre.Parent.Parent.patient == null) || (s.Semestre.Parent.Parent.patient.ActesPG == null)
                )
            {
                _TarifTotal += s.Montant_Honoraire;

            }
            else
            {
                ActePG acteAssocier = null;
                foreach (ActePG a in s.Semestre.Parent.Parent.patient.ActesPG)
                    if (a.IdSurvPlanGestionAssocie == s.Id)
                        acteAssocier = a;

                if (acteAssocier != null) _TarifTotal = acteAssocier.Montant_Honoraire;
            }





            //TemplateActePG surv = TemplateApctePGMgmt.getCodeSecu("SURV");





            return _TarifTotal;
        }


        public static double getSolde(Surveillance s)
        {

            try
            {
                if ((s.Semestre.Parent.Parent.patient.ActesPG == null) ||
                   (s.Semestre.Parent.Parent.patient.Echeances == null)
                   )
                {
                    s.Solde = ActesPGMgmt.GetSolde(s);
                    return s.Solde.Value;
                }

                double solde = 0;
                ActePG acteAssocier = null;
                foreach (ActePG a in s.Semestre.Parent.Parent.patient.ActesPG)
                    if (a.IdSurvPlanGestionAssocie == s.Id)
                        acteAssocier = a;

                if (acteAssocier != null)
                    foreach (Echeance ec in s.Semestre.Parent.Parent.patient.Echeances)
                        if ((ec.IdActe == acteAssocier.Id) && (ec.ID_Encaissement < 0))
                            solde += ec.Montant;

                s.Solde = solde;




                return s.Solde.Value;
            }
            catch (System.Exception)
            {
                return 0;
            }
        }

        public static Surveillance getSurveillance(ActePG acte, List<Proposition> propositions)
        {

            foreach (Proposition p in propositions)
                foreach (Traitement t in p.traitements)
                    foreach (Semestre s in t.semestres)
                        foreach (Surveillance su in s.surveillances)
                            if (su.Id == acte.IdSurvPlanGestionAssocie)
                                return su;
            return null;
        }
    }
}
