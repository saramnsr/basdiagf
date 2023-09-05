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
    class BuildDiag
    {
        public static Diagnostic Build(JObject obj) {
            // valide
            Diagnostic d = new Diagnostic();

            d.id_diagnostic = Convert.ToInt32(obj["id"]);
            d.libelle = String.IsNullOrEmpty(obj["libelle"].ToString()) ? "" : Convert.ToString(obj["libelle"]).Trim();
            d.categorie = String.IsNullOrEmpty(obj["categorie"].ToString()) ? "" : Convert.ToString(obj["categorie"]).Trim();
            d.question = String.IsNullOrEmpty(obj["question"].ToString()) ? "" : Convert.ToString(obj["question"]).Trim();
            d.photo = String.IsNullOrEmpty(obj["photos"].ToString()) ? "" : Convert.ToString(obj["photos"]).Trim();
            
            return d;
        }

        public static Diagnostic Build(DataRow r)
        {
            Diagnostic diag = new Diagnostic();
            diag.id_diagnostic = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
            diag.libelle = r["LIBELLE"] is DBNull ? "" : Convert.ToString(r["LIBELLE"]).Trim();
            diag.categorie = r["CATEGORIE"] is DBNull ? "" : Convert.ToString(r["CATEGORIE"]).Trim();
            diag.photo = r["QUESTION"] is DBNull ? "" : Convert.ToString(r["QUESTION"]).Trim();
            diag.question = r["PHOTOS"] is DBNull ? "" : Convert.ToString(r["PHOTOS"]).Trim();
            return diag;
        }

        public static Objectif BuildObjectif(JObject r)
        {
            // valide
            Objectif objct = new Objectif();

            objct.id_objectif =  Convert.ToInt32(r["idObjectif"]);
            objct.libelle = String.IsNullOrEmpty(r["libelle"].ToString()) ? "" : Convert.ToString(r["libelle"]).Trim();
            objct.categorie = r["categorie"]==null ? -1 : Convert.ToInt32(r["categorie"]);
            objct.description = String.IsNullOrEmpty(r["description"].ToString()) ? "" : Convert.ToString(r["description"]).Trim();
            
            return objct;
        }

        public static Objectif BuildObjectif(DataRow r)
        {
            Objectif objct = new Objectif();
            objct.id_objectif = r["ID_OBJ"] is DBNull ? -1 : Convert.ToInt32(r["ID_OBJ"]);
            objct.libelle = r["LIBELLE"] is DBNull ? "" : Convert.ToString(r["LIBELLE"]).Trim();
            objct.categorie = r["CATEGORIE"] is DBNull ? -1 : Convert.ToInt32(r["CATEGORIE"]);
            objct.description = r["DESCRIPTION"] is DBNull ? "" : Convert.ToString(r["DESCRIPTION"]).Trim();
            return objct;
        }
        public static ObjectifDetail BuildOD(DataRow r)
        {
            ObjectifDetail od = new ObjectifDetail();
            od.id_objectif = r["ID_OBJ"] is DBNull ? -1 : Convert.ToInt32(r["ID_OBJ"]);
            od.id_diagnostic = r["ID_DIAG"] is DBNull ? -1 : Convert.ToInt32(r["ID_DIAG"]);
            od.description = r["DESCRIPTION"] is DBNull ? "" : Convert.ToString(r["DESCRIPTION"]).Trim();
            od.instSpecial = r["SPECIALINSTRUCTIONS"] is DBNull ? "" : Convert.ToString(r["SPECIALINSTRUCTIONS"]).Trim();
            od.solution = r["NUM_DEVIS"] is DBNull ? -1 : Convert.ToInt32(r["NUM_DEVIS"]);
            od.txtinvisalign = r["INVISALIGNTXT"] is DBNull ? "" : Convert.ToString(r["INVISALIGNTXT"]).Trim();
            return od;
        }

    }
}
