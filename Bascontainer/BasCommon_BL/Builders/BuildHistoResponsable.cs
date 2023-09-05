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

    public static class BuildHistoResponsable
    {

        public static HistoResponsable Build(DataRow r)
        {

            HistoResponsable obj = new HistoResponsable();
            obj.DateEvenement = Convert.ToDateTime(r["DATEEVENT"]);
            obj.user = r["USEREVENT"] is DBNull?null:UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["USEREVENT"]));
            obj.IdPatient = Convert.ToInt32(r["IDPATIENT"]);
            obj.AssistanteResp = r["ASSISTANTE_RESP"] is DBNull?null:UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["ASSISTANTE_RESP"]));
            obj.PaticienResp = r["PRATICIEN_RESP"] is DBNull ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["PRATICIEN_RESP"]));
            obj.PraticienUnique = r["PRATICIEN_UNIQUE"] is DBNull ? false : Convert.ToBoolean(r["PRATICIEN_UNIQUE"]);
            obj.AssistanteUnique = r["ASSISTANTE_UNIQUE"] is DBNull ? false : Convert.ToBoolean(r["ASSISTANTE_UNIQUE"]);


            return obj;
        }
        public static HistoResponsable Build(JObject r)
        {

            HistoResponsable obj = new HistoResponsable();
            obj.DateEvenement = Convert.ToDateTime(r["dateevent"]);
            obj.user = r["userevent"].ToString() == "" ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["userevent"]));
            obj.IdPatient = Convert.ToInt32(r["idpatient"]);
            obj.AssistanteResp = r["assistanteResp"].ToString() == "" ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["assistanteResp"]));
            obj.PaticienResp = r["praticienResp"].ToString() == "" ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["praticienResp"]));
            obj.PraticienUnique = r["praticienUnique"].ToString() == "" ? false : Convert.ToBoolean(r["praticienUnique"]);
            obj.AssistanteUnique = r["assistanteUnique"].ToString() == "" ? false : Convert.ToBoolean(r["assistanteUnique"]);


            return obj;
        }
    }
}
