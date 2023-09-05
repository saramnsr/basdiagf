using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{

    public static class BuildGroupedPrelevement
    {
        public static GroupedPrelevement BuildJ(JObject r)
        {
            GroupedPrelevement cs = new GroupedPrelevement();
            cs.IdPatient = Convert.ToInt32(r["id_personne"]);
            cs.Patient = Convert.ToString(r["nompatient"]);
            cs.DatePremierPrelevement = Convert.ToDateTime(r["datepremierprelevement"]);
            cs.traitement = Convert.ToString(r["traitement"]);
            cs.idtraitement = Convert.ToInt32(r["idTraitement"]);


            cs.Montants = new List<double>();
            string[] ss = Convert.ToString(r["montants"]).Split(',');
            foreach (string s in ss)
            {
                try
                {
                    float d = 0;

                    if (Single.TryParse(s, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out d))
                        cs.Montants.Add(d);



                }
                catch (System.Exception) { }
            }

            ss = Convert.ToString(r["days"]).Split(',');
            foreach (string s in ss)
            {
                try
                {
                    int d = Convert.ToInt32(s);
                    cs.EcheanceDays.Add(d);
                }
                catch (System.Exception) { }
            }

            ss = Convert.ToString(r["ids"]).Split(',');
            foreach (string s in ss)
            {
                try
                {
                    int d = Convert.ToInt32(s);
                    cs.EcheanceIds.Add(d);
                }
                catch (System.Exception) { }
            }

            return cs;
        }
        public static GroupedPrelevement Build(DataRow r)
        {
            GroupedPrelevement cs = new GroupedPrelevement();
            cs.IdPatient = Convert.ToInt32(r["id_personne"]);
            cs.Patient = Convert.ToString(r["NomPatient"]);
            cs.DatePremierPrelevement = Convert.ToDateTime(r["DatePremierPrelevement"]);
            cs.traitement = Convert.ToString(r["traitement"]);
            cs.idtraitement = Convert.ToInt32(r["idtraitement"]);
            

           cs.Montants = new List<double>();
           string[] ss = Convert.ToString(r["montants"]).Split(',');
           foreach (string s in ss)
           {
               try
               {
                   float d = 0;

                   if (Single.TryParse(s, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out d))                   
                        cs.Montants.Add(d);
                   
                   
                   
               }
               catch (System.Exception) { }
           }

           ss = Convert.ToString(r["days"]).Split(',');
           foreach (string s in ss)
           {
               try
               {
                   int d = Convert.ToInt32(s);
                   cs.EcheanceDays.Add(d);
               }
               catch (System.Exception) { }
           }

           ss = Convert.ToString(r["ids"]).Split(',');
           foreach (string s in ss)
           {
               try
               {
                   int d = Convert.ToInt32(s);
                   cs.EcheanceIds.Add(d);
               }
               catch (System.Exception) { }
           }

            return cs;
        }

    }
}
