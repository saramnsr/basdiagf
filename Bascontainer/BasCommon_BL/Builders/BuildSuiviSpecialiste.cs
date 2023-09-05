using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildSuiviSpecialiste
    {
        public static SuiviSpecialiste Build(DataRow r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            SuiviSpecialiste act = new SuiviSpecialiste();
            act.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
            act.IdPatient = r["ID_PATIENT"] is DBNull ? -1 : Convert.ToInt32(r["ID_PATIENT"]);
            act.DateEnvois = r["DATE_ENVOIS"] is DBNull ? DateTime.Now : Convert.ToDateTime(r["DATE_ENVOIS"]);
            act.IdCorrespondant = r["ID_CORRESPONDANT"] is DBNull ? -1 : Convert.ToInt32(r["ID_CORRESPONDANT"]);
            act.NomCorrespondant = r["NomCorrespondant"] is DBNull ? "" : Convert.ToString(r["NomCorrespondant"]);
            act.Commentaire = r["COMMENTAIRES"] is DBNull ? "" : Convert.ToString(r["COMMENTAIRES"]);
            return act;
        }
    }
}
