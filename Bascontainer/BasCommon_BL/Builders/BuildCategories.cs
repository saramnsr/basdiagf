using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{
    public static class BuildCategories
    {
        public static Categorie Build(DataRow r)
        {
            Categorie cat = new Categorie();
            cat.IdCateg = Convert.ToInt32(r["ID_CATEG"]);
            cat.Nom = Convert.ToString(r["CATEG"]);

            return cat;
        }

      

    }

    public static class BuildCustomCategorie
    {
        public static CustomCategorie Build(DataRow r)
        {
            CustomCategorie customcat = new CustomCategorie();
            customcat.Id = Convert.ToInt32(r["ID"]);
            customcat.IdPersonne = Convert.ToInt32(r["ID_PERSONNE"]);
            customcat.DateDebutCat = Convert.ToDateTime(r["DATE_CATEG"]);
            customcat.DateFinCat = (r["DATE_FIN_CATEG"] == DBNull.Value) ? null : (DateTime?)Convert.ToDateTime(r["DATE_FIN_CATEG"]);
            customcat.IdCateg = r["ID_CATEGORIE"] is DBNull ? -1 : Convert.ToInt32(r["ID_CATEGORIE"]);
            customcat.Note = r["NOTE"] is DBNull ? -1 : Convert.ToInt32(r["NOTE"]);
            customcat.Nom = Convert.ToString(r["CATEG"]);

            return customcat;
        }
        public static CustomCategorie BuildJ(JObject r)
        {
            CustomCategorie customcat = new CustomCategorie();
            customcat.Id = Convert.ToInt32(r["id"]);
            customcat.IdPersonne = Convert.ToInt32(r["idPersonne"]);
            customcat.DateDebutCat = Convert.ToDateTime(r["dateCategorie"]);
            customcat.DateFinCat = (r["dateFinCategorie"].ToString() == "") ? null : (DateTime?)Convert.ToDateTime(r["dateFinCategorie"]);
            customcat.IdCateg = r["idCategorie"].ToString() == "" ? -1 : Convert.ToInt32(r["idCategorie"]);
            customcat.Note = r["note"].ToString() == "" ? -1 : Convert.ToInt32(r["note"]);
            customcat.Nom = Convert.ToString(r["categorie"]);

            return customcat;
        }
    }
}
