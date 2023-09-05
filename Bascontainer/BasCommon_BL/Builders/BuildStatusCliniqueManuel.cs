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
    public static class BuildStatusCliniqueManuel
    {
        public static StatusCliniqueManuel Build(DataRow dr)
        {
            StatusCliniqueManuel cs = new StatusCliniqueManuel();
            cs.Id = Convert.ToInt32(dr["ID_STATUT"]);
            cs.Libelle = Convert.ToString(dr["LIBELLE"]).Trim();
            try
            {
                cs.couleur = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(dr["couleur"]));
            }
            catch (System.Exception)
            {
                cs.couleur = System.Drawing.ColorTranslator.FromHtml("#99" + Convert.ToString(dr["couleur"]));
                
            }
            cs.Organisation = Convert.ToString(dr["Organisation"]);
            cs.FamilleStatut = dr["FAMILLE_STATUT"] is DBNull ? StatusCliniqueManuel.FamilyStatut.Autre : (StatusCliniqueManuel.FamilyStatut)Convert.ToInt32(dr["FAMILLE_STATUT"]);

            return cs;
        }
        public static StatusCliniqueManuel BuildJ(JObject dr)
        {
            StatusCliniqueManuel cs = new StatusCliniqueManuel();
            cs.Id = Convert.ToInt32(dr["id_statut"]);
            cs.Libelle = Convert.ToString(dr["libelle"]).Trim();
            try
            {
                cs.couleur = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(dr["couleur"]));
            }
            catch (System.Exception)
            {
                cs.couleur = System.Drawing.ColorTranslator.FromHtml("#99" + Convert.ToString(dr["couleur"]));

            }
            cs.Organisation = Convert.ToString(dr["organisation"]);
            cs.FamilleStatut = dr["famille_statut"].ToString() == "" ? StatusCliniqueManuel.FamilyStatut.Autre : (StatusCliniqueManuel.FamilyStatut)Convert.ToInt32(dr["famille_statut"]);

            return cs;
        }
    }
}
