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

    public static class BuildControlFinancier
    {

        public static ControlFinancier Build(DataRow dr)
        {
            ControlFinancier ctrl = new ControlFinancier();
            ctrl.Id = Convert.ToInt32(dr["ID"]);
            ctrl.Etat = (ControlFinancier.EtatControl)Convert.ToInt32(dr["ETAT"]);
            ctrl.Remarques = Convert.ToString(dr["REMARQUE"]);
            ctrl.CodeControl = Convert.ToString(dr["CODECONTROL"]);
            ctrl.Libelle = Convert.ToString(dr["LIBELLE"]);
            ctrl.ControlerPar = UtilisateursMgt.getUtilisateur(Convert.ToInt32(dr["ID_CONTROLEUR"]));
            ctrl.DateControl = Convert.ToDateTime(dr["DATE"]);
            ctrl.IdControleur = Convert.ToInt32(dr["ID_CONTROLEUR"]);
            
            return ctrl;
        }
        public static ControlFinancier Build(JObject dr)
        {
            ControlFinancier ctrl = new ControlFinancier();
            ctrl.Id = Convert.ToInt32(dr["id"]);
            ctrl.Etat = (ControlFinancier.EtatControl)Convert.ToInt32(dr["etat"]);
            ctrl.Remarques = Convert.ToString(dr["remarque"]);
            ctrl.CodeControl = Convert.ToString(dr["codecontrol"]);
            ctrl.Libelle = Convert.ToString(dr["libelle"]);
            ctrl.ControlerPar = UtilisateursMgt.getUtilisateur(Convert.ToInt32(dr["idControleur"]));
            ctrl.DateControl = Convert.ToDateTime(dr["date"]);
            ctrl.IdControleur = Convert.ToInt32(dr["id_controleur"]);

            return ctrl;
        }
    }

}
