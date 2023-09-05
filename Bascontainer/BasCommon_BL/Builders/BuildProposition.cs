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

    public static class BuildProposition
    {
        
        public static Proposition Build(DataRow r)
        {

            Proposition proposition = new Proposition();
            proposition.Id = Convert.ToInt32(r["ID"]);
            proposition.Etat = (Proposition.EtatProposition)Convert.ToInt32(r["ETAT"]);
            proposition.DateEvenement = r["DATEEVENT"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEEVENT"]);
            proposition.libelle = Convert.ToString(r["LIBELLE"]);
            proposition.DateAcceptation = r["DATE_ACCEPTATION"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATE_ACCEPTATION"]);
            proposition.IdModel = r["ID_MODELE"] is DBNull ? -1 : Convert.ToInt32(r["ID_MODELE"]);
            proposition.DateProposition = r["DATE_PROPOSITION"] is DBNull ? DateTime.Now : Convert.ToDateTime(r["DATE_PROPOSITION"]);
            proposition.IdScenario = r["IDSCENARIO"] is DBNull ? -1 : Convert.ToInt32(r["IDSCENARIO"]);

            return proposition;
        }
        public static Proposition BuildJ(JObject r)
        {

            Proposition proposition = new Proposition();
            proposition.Id = Convert.ToInt32(r["id"]);
            proposition.Etat = (Proposition.EtatProposition)Convert.ToInt32(r["etat"]);
            proposition.DateEvenement = r["dateevent"].ToString() =="" ? null : (DateTime?)Convert.ToDateTime(r["dateevent"]);
            proposition.libelle = Convert.ToString(r["libelle"]);
            proposition.DateAcceptation = r["date_ACCEPTATION"].ToString() =="" ? null : (DateTime?)Convert.ToDateTime(r["date_ACCEPTATION"]);
            proposition.IdModel = r["id_MODELE"].ToString() =="" ? -1 : Convert.ToInt32(r["id_MODELE"]);
            proposition.DateProposition = r["date_PROPOSITION"].ToString() =="" ? DateTime.Now : Convert.ToDateTime(r["date_PROPOSITION"]);
            proposition.IdScenario = r["idscenario"].ToString() =="" ? -1 : Convert.ToInt32(r["idscenario"]);

            return proposition;
        }
        public static NewTraitement BuildTraitementDevis(DataRow r)
        {
            NewTraitement trt = new NewTraitement();
            trt.id_Traitement = Convert.ToInt32(r["id_Traitement"]);
           
            trt.Traitement_libelle =  Convert.ToString(r["TRAITEMENT_LIBELLE"]);
            trt.Traitement_couleur =  r["TRAITEMENT_COULEUR"] is DBNull ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["TRAITEMENT_COULEUR"]));
            trt.id_famille  =  r["ID_FAMILLE_TRAITEMENT"] is DBNull ? -1 : Convert.ToInt32(r["ID_FAMILLE_TRAITEMENT"]);
            trt.Traitement_shortlib  = Convert.ToString(r["SHORTLIB"]);
            trt.Montant_Scenario = r["MONTANTSCENARIO"] is DBNull ? 0 : Convert.ToDouble(r["MONTANTSCENARIO"]);

            return trt;

        }

    }
}
