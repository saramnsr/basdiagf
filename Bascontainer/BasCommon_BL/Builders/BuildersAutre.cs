using BasCommon_BO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL.Builders
{
  public class BuildersAutre
    {
      public static Autre BuildAutre(DataRow r)
      {
          //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
          Autre autre = new Autre();
          autre.id = r["id"] is DBNull ? -1 : Convert.ToInt32(r["id"]);
          autre.Libelle = r["Libelle"] is DBNull ? "" : Convert.ToString(r["Libelle"]).Trim();
        
          return autre;
      }
      public static Autre BuildAutreJ(JObject r)
      {
          //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
          Autre autre = new Autre();
          autre.id =  Convert.ToInt32(r["id"]);
          autre.Libelle = Convert.ToString(r["libelle"]).Trim();

          return autre;
      }
    }
}
