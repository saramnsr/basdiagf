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
    public static class TraitementMgmt
    {


        public static int GetSemestreMin(Traitement traitment)
        {
            int rez = int.MaxValue;

            foreach (Semestre s in traitment.semestres)
            {
                if (rez > s.NumSemestre)
                    rez = s.NumSemestre;
            }
            return rez;
        }

        public static int GetSemestreMax(Traitement traitment)
        {
            int rez = 0;

            foreach (Semestre s in traitment.semestres)
            {
                if (rez < s.NumSemestre)
                    rez = s.NumSemestre;
            }

            return rez;
        }

        public static double getTotalSansRemise(Traitement t, basePatient pat)
        {
            double _TarifTotal = 0;

            foreach (Semestre s in t.semestres)
            {
                _TarifTotal +=  s.traitementSecu.Valeur;


                foreach (Surveillance su in s.surveillances)
                    _TarifTotal +=  su.traitementSecu.Valeur;

            }

            return _TarifTotal;
        }

        public static double getTotal(Traitement t)
        {
            double _TarifTotal = 0;
            TemplateActePG surv = TemplateApctePGMgmt.getCodeSecu("SURV");


            foreach (Semestre s in t.semestres)
            {
                _TarifTotal += s.Montant_Honoraire;


                foreach (Surveillance su in s.surveillances)
                    _TarifTotal += su.Montant_Honoraire;

            }

            return _TarifTotal;
        }

        public static double GetPartSecu(Traitement t)
        {
            double _TarifTotal = 0;


            foreach (Semestre s in t.semestres)
            {
                _TarifTotal += s.traitementSecu.Coeff * s.traitementSecu.Code.Valeur;


                foreach (Surveillance su in s.surveillances)
                    _TarifTotal += su.traitementSecu.Coeff * su.traitementSecu.Code.Valeur;

            }

            return _TarifTotal;
        }


        public static void AddTraitements(Traitement traitement)
        {
            DAC.AddTraitement(traitement);
        }

        public static List<Traitement> getTraitements(basePatient patient)
        {

            List<Traitement> lst = new List<Traitement>();

            DataTable dtt = DAC.getTraitements(patient);
            foreach (DataRow drt in dtt.Rows)
            {
                Traitement t = Builders.BuildTraitement.Build(drt);
                lst.Add(t);
            }
            return lst;
        }


        public static void UpdateTraitement(Traitement traitement)
        {
            DAC.UpdateTraitement(traitement);
        }

        public static void Delete(Traitement obj)
        {
            DAC.DeleteTraitment(obj.Id);
        }

        public static List<Traitement> getTraitements(Proposition pr)
        {

            List<Traitement> lst = new List<Traitement>();

            JArray json = DAC.getMethodeJsonArray("/PlanTraitements/" + pr.Id);
            foreach (JObject drt in json)
            {
                Traitement t = Builders.BuildTraitement.BuildJ(drt);
                t.Parent = pr;
                t.semestres = SemestreMgmt.getSemestres(t);

                foreach (Semestre s in t.semestres)
                {
                    s.Parent = t;
                    s.surveillances = SurveillanceMgmt.getSurveillances(s);
                    foreach (Surveillance su in s.surveillances)
                        su.Semestre = s;
                }
                lst.Add(t);
            }
            return lst;
        }
        public static List<Traitement> getTraitementsOLD(Proposition pr)
        {

            List<Traitement> lst = new List<Traitement>();

            DataTable dtt = DAC.getTraitements(pr);
            foreach (DataRow drt in dtt.Rows)
            {
                Traitement t = Builders.BuildTraitement.Build(drt);
                t.Parent = pr;
                t.semestres = SemestreMgmt.getSemestres(t);

                foreach (Semestre s in t.semestres)
                {
                    s.Parent = t;
                    s.surveillances = SurveillanceMgmt.getSurveillances(s);
                    foreach (Surveillance su in s.surveillances)
                        su.Semestre = s;
                }
                lst.Add(t);
            }
            return lst;
        }

    }
}
