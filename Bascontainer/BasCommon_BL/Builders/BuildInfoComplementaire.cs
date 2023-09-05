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

    public static class BuildInfoPatientComplementaire
    {

        public static InfoPatientComplementaire BuildJson(JObject r)
        {

            InfoPatientComplementaire nfocmpl = new InfoPatientComplementaire();
            nfocmpl.IdPatient = Convert.ToInt32(r["idpatient"]);
            nfocmpl.AssistanteResponsable = r["assistante_resp"].ToString() == "" ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["assistante_resp"]));

            nfocmpl.PraticienResponsable = r["praticien_resp"].ToString() == "" ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["praticien_resp"]));
            try
            {
                nfocmpl.PraticienUnique = r["praticien_unique"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToString(r["praticien_unique"]));
            }
            catch (Exception e)
            {
                nfocmpl.PraticienUnique = r["praticien_unique"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToInt32(r["praticien_unique"]));
            }
            try
            {
                nfocmpl.AssistanteUnique = r["assistante_unique"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToInt32(r["assistante_unique"]));
            }
            catch (Exception e)
            {
                nfocmpl.AssistanteUnique = r["assistante_unique"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToString(r["assistante_unique"]));
            }
            nfocmpl.NbSemestresEntame = r["semestresentames"].ToString() == "" ? 0 : Convert.ToInt32(r["semestresentames"]);
            nfocmpl.Ameliorations = r["ameliorations"].ToString() == "" ? "" : Convert.ToString(r["ameliorations"]);
            nfocmpl.DateDebutTraitement = r["debuttraitementenvisage"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["debuttraitementenvisage"]);


            return nfocmpl;
        }

        public static InfoPatientComplementaire Build(DataRow r)
        {

            InfoPatientComplementaire nfocmpl = new InfoPatientComplementaire();
            nfocmpl.IdPatient = Convert.ToInt32(r["IDPATIENT"]);
            nfocmpl.AssistanteResponsable = r["ASSISTANTE_RESP"] is DBNull ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["ASSISTANTE_RESP"]));

            nfocmpl.PraticienResponsable = r["PRATICIEN_RESP"] is DBNull ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["PRATICIEN_RESP"]));
            try
            {
                nfocmpl.PraticienUnique = r["PRATICIEN_UNIQUE"] is DBNull ? false : Convert.ToBoolean(Convert.ToString(r["PRATICIEN_UNIQUE"]));
            }
            catch (Exception e)
            {
                nfocmpl.PraticienUnique = r["PRATICIEN_UNIQUE"] is DBNull ? false : Convert.ToBoolean(Convert.ToInt32(r["PRATICIEN_UNIQUE"]));
            }
            try
            {
                nfocmpl.AssistanteUnique = r["ASSISTANTE_UNIQUE"] is DBNull ? false : Convert.ToBoolean(Convert.ToInt32(r["ASSISTANTE_UNIQUE"]));
            }
            catch (Exception e)
            {
                nfocmpl.AssistanteUnique = r["ASSISTANTE_UNIQUE"] is DBNull ? false : Convert.ToBoolean(Convert.ToString(r["ASSISTANTE_UNIQUE"]));
            }
            nfocmpl.NbSemestresEntame = r["SEMESTRESENTAMES"] is DBNull ? 0 : Convert.ToInt32(r["SEMESTRESENTAMES"]);
            nfocmpl.Ameliorations = r["AMELIORATIONS"] is DBNull ? "" : Convert.ToString(r["AMELIORATIONS"]);
            nfocmpl.DateDebutTraitement = r["DEBUTTRAITEMENTENVISAGE"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DEBUTTRAITEMENTENVISAGE"]);


            return nfocmpl;
        }
    }
}
