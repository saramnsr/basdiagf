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
    public static class SemestreMgmt
    {

        public static void AssocierDEP(Semestre sem, EntentePrealable entente)
        {
            DAC.AssocierDEP(sem, entente);
        }

        public static List<Semestre> getSemestres(basePatient pat)
        {
            List<Semestre> lst = new List<Semestre>();
            DataTable dt = DAC.getSemestres(pat);

            foreach (DataRow dr in dt.Rows)
            {
                Semestre s = Builders.BuildSemestre.Build(dr);
                lst.Add(s);
            }
            return lst;
        }

        public static void Delete(Semestre obj)
        {
            DAC.DeleteSemestre(obj.Id);
        }


        public static void AddSemestre(Semestre sem)
        {
            DAC.AddSemestre(sem);

        }

        public static void DelSemestre(Semestre sem)
        {
            DAC.DeleteSemestre(sem);
        }

        public static void UpdateSemestre(Semestre sem)
        {
            DAC.UpdateSemestre(sem);

        }





        public static List<Semestre> getSemestres(Traitement trmnt)
        {
            List<Semestre> lst = new List<Semestre>();
            JArray json = DAC.getMethodeJsonArray("/Semsetres/" + trmnt.Id);

            foreach (JObject dr in json)
            {
                Semestre s = Builders.BuildSemestre.BuildJ(dr);
                lst.Add(s);
            }
            return lst;
        }


        public static List<Semestre> getSemestresOLD(Traitement trmnt)
        {
            List<Semestre> lst = new List<Semestre>();
            DataTable dt = DAC.getSemestres(trmnt);

            foreach (DataRow dr in dt.Rows)
            {
                Semestre s = Builders.BuildSemestre.Build(dr);
                lst.Add(s);
            }
            return lst;
        }

        public static double getTotalSansRemise(Semestre s, basePatient pat)
        {
            double _TarifTotal = 0;




            _TarifTotal += s.traitementSecu.Valeur;


            foreach (Surveillance su in s.surveillances)
                _TarifTotal += su.traitementSecu.Valeur;



            return _TarifTotal;
        }


        public static bool CalquerLesDatesSurLActePG(ref Semestre s)
        {
            List<ActePG> actes = ActesPGMgmt.GetActesPG(s);
            if (actes.Count == 0) return false;
            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;

            foreach (ActePG a in actes)
            {
                DateTime dteFin = a.DateExecution.AddDays(a.NbJours.Value).AddMonths(a.NbMois.Value);
                if (a.DateExecution < minDate) minDate = a.DateExecution;
                if (dteFin > maxDate) maxDate = dteFin;
            }



            if ((s.DateDebut.Date == minDate) &&
                (s.DateFin.Date == maxDate))
                return false;


            s.DateDebut = minDate;
            s.DateFin = maxDate;

            UpdateSemestre(s);
            return false;

        }

        public static bool CalquerLesDatesSurLActePG(Traitement t)
        {
            bool needToSave = false;
            //foreach (Semestre s in t.semestres)
            for (int i = 0; i < t.semestres.Count; i++)
            {
                Semestre s = t.semestres[i];
                if (CalquerLesDatesSurLActePG(ref s))
                    needToSave = true;
            }
            return needToSave;
        }

        public static bool CalquerLesDatesSurLActePG(Proposition p)
        {
            bool needToSave = false;
            //foreach (Semestre s in t.semestres)
            for (int i = 0; i < p.traitements.Count; i++)
            {
                Traitement t = p.traitements[i];
                if (CalquerLesDatesSurLActePG(t))
                    needToSave = true;
            }
            return needToSave;
        }


        public static Surveillance AjouterSurveillances(basePatient pat, Semestre s, List<Proposition> propositions, DateTime dteDebut)
        {
          //  if (s.DateFin == null) return null;





            Surveillance Newsurv = new Surveillance();
            Newsurv.DateDebut = dteDebut;
            Newsurv.traitementSecu = TemplateApctePGMgmt.getCodeSecu("SURV");
            Newsurv.DateFin = Newsurv.DateDebut.Value.AddMonths(Newsurv.traitementSecu.NBMois.Value).AddDays(Newsurv.traitementSecu.NBJours.Value);
            Newsurv.Montant_Honoraire = Newsurv.traitementSecu.Valeur;

            if (pat.Correspondants == null)
                pat.Correspondants = MgmtCorrespondants.getCorrespondantsOf(pat);

            Newsurv.Montant_Honoraire = Newsurv.traitementSecu.Valeur;


            Newsurv.Semestre = s;
            if (s!=null) 
                s.surveillances.Add(Newsurv);

            SurveillanceMgmt.AddSurveillance(Newsurv);



            foreach (Proposition p in propositions)
            {
                CalquerLesDatesSurLActePG(p);
            }
            return Newsurv;

        }

        public static Semestre getSemestre(ActePG acte, List<Proposition> propositions)
        {
            int idSem = DAC.getIdSemestre(acte);

            foreach (Proposition p in propositions)
                foreach (Traitement t in p.traitements)
                    foreach (Semestre s in t.semestres)
                        if (s.Id == idSem)
                            return s;
            return null;
        }

        public static double getSolde(Semestre s)
        {

            if ((s.Parent.Parent.patient == null) || (s.Parent.Parent.patient.ActesPG == null) ||
                (s.Parent.Parent.patient.Echeances == null)
                )
            {
                s.Solde = ActesPGMgmt.GetSolde(s);
                return s.Solde.Value;
            }

            double solde = 0;
            ActePG acteAssocier = null;
            foreach (ActePG a in s.Parent.Parent.patient.ActesPG)
                if (a.IdSemestrePlanGestionAssocie == s.Id)
                    acteAssocier = a;

            if (acteAssocier != null)
            {
                foreach (Echeance ec in s.Parent.Parent.patient.Echeances)
                    if ((ec.IdActe == acteAssocier.Id) && (ec.ID_Encaissement < 0))
                        solde += ec.Montant;
            }
            s.Solde = solde;

            

            return s.Solde.Value;
        }



        public static double getTotal(Semestre s)
        {

            double _TarifTotal = 0;


            if ((s.Parent.Parent.patient == null) || (s.Parent.Parent.patient.ActesPG == null) 
                )
            {
                _TarifTotal += s.Montant_Honoraire;

                foreach (Surveillance su in s.surveillances)
                    _TarifTotal += su.Montant_Honoraire;

            }
            else
            {
                ActePG acteAssocier = null;
                foreach (ActePG a in s.Parent.Parent.patient.ActesPG)
                    if (a.IdSemestrePlanGestionAssocie == s.Id)
                        acteAssocier = a;

                if (acteAssocier != null) _TarifTotal = acteAssocier.Montant_Honoraire;
            }

            



            //TemplateActePG surv = TemplateApctePGMgmt.getCodeSecu("SURV");


           


            return _TarifTotal;
        }

        public static double GetPartSecu(Semestre s)
        {
            double _TarifTotal = 0;


            _TarifTotal += s.traitementSecu.Coeff * s.traitementSecu.Code.Valeur;

            foreach (Surveillance su in s.surveillances)
                _TarifTotal += su.traitementSecu.Coeff * su.traitementSecu.Code.Valeur;




            return _TarifTotal;
        }





    }
}
