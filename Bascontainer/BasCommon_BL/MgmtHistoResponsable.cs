using BasCommon_BO;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL
{
    public static class MgmtHistoResponsable
    {
        public static List<HistoResponsable> GetHistoResponsable(int IdPatient)
        {
            List<HistoResponsable> resultats = new List<HistoResponsable>();
            JArray json = DAC.getMethodeJsonArray("/HistoResponsableByIdPatient/" + IdPatient);
            foreach (JObject dr in json)
            {
                HistoResponsable pr = Builders.BuildHistoResponsable.Build(dr);
                resultats.Add(pr);
            }

            return resultats;
        }
        public static List<HistoResponsable> GetHistoResponsableOLD(int IdPatient)
        {
            List<HistoResponsable> resultats = new List<HistoResponsable>();
            DataTable dt = DAC.getHistoResponsableByIdPatient(IdPatient);

            foreach (DataRow dr in dt.Rows)
            {
                HistoResponsable pr = Builders.BuildHistoResponsable.Build(dr);
                resultats.Add(pr);
            }

            return resultats;
        }

        public static void InsertHistoResponsable(HistoResponsable histo)
        {
            DAC.InsertHistoResponsable(histo);
        }

    }
}
