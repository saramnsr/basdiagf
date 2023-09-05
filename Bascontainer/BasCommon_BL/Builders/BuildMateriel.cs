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
    public static class BuildFamillesMateriel
    {
        public static FamillesMateriels Build(DataRow r)
        {
            FamillesMateriels mat = new FamillesMateriels();
            mat.Id = r["id"] is DBNull ? -1 : Convert.ToInt32(r["id"]);
            mat.libelle = r["Nom"] is DBNull ? "" : Convert.ToString(r["Nom"]).Trim();
            mat.ParentFamillesMaterielId = r["Parent"] is DBNull ? -1 : Convert.ToInt32(r["Parent"]);
            mat.couleur = r["couleur"] is DBNull ? System.Drawing.Color.WhiteSmoke : System.Drawing.Color.FromArgb(Convert.ToInt32(r["couleur"]));
            mat.ordre = r["ordre"] is DBNull ? -1 : Convert.ToInt32(r["ordre"]);
            return mat;
        }
        public static FamillesMateriels BuildJ(JObject r)
        {
            FamillesMateriels mat = new FamillesMateriels();
            
                mat.Id = Convert.ToInt32(r["id"]);
                mat.libelle = Convert.ToString(r["nom"]).Trim();
                mat.ParentFamillesMaterielId = Convert.ToInt32(r["parent"]);
                mat.couleur = Convert.ToInt32(r["couleur"]) == 0 ? System.Drawing.Color.WhiteSmoke : System.Drawing.Color.FromArgb(Convert.ToInt32(r["couleur"]));
                mat.ordre = Convert.ToInt32(r["ordre"]);
                return mat;  
        }
    }



     public static class BuildMateriel
    {
         public static Materiel Build(DataRow r)
        {
            Materiel  mat = new Materiel  ();
            mat.id_materiel = r["id_materiel"] is DBNull ? -1 : Convert.ToInt32(r["id_materiel"]);
            mat.materiel_libelle = r["materiel_libelle"] is DBNull ? "" : Convert.ToString(r["materiel_libelle"]).Trim();
            mat.materiel_couleur = r["materiel_couleur"] is DBNull ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["materiel_couleur"]));
            mat.id_famille = r["id_famille_materiel"] is DBNull ? -1 : Convert.ToInt32(r["id_famille_materiel"]);
            mat.prix_materiel  = r["prix_materiel"] is DBNull ? 0 : Convert.ToDouble(r["prix_materiel"]);
            mat.materiel_shortlib = r["SHORTLIB"] is DBNull ? "" : Convert.ToString(r["SHORTLIB"]).Trim();
            mat.coefficient = r["COEFFICIENT"] is DBNull ? 0 : Convert.ToInt32 (r["COEFFICIENT"]);
           mat.materiel_libelle_facture  =  r["MATERIEL_LIBELLE_FACTURE"] is DBNull ? "" : Convert.ToString(r["MATERIEL_LIBELLE_FACTURE"]).Trim();
             mat.materiel_libelle_estimation  =  r["MATERIEL_LIBELLE_ESTIMATION"] is DBNull ? "" : Convert.ToString(r["MATERIEL_LIBELLE_ESTIMATION"]).Trim();
               mat.nomenclature   =  r["NOMENCLATURE"] is DBNull ? "" : Convert.ToString(r["NOMENCLATURE"]).Trim();
               mat.cotation   =  r["COTATION"] is DBNull ? "" : Convert.ToString(r["COTATION"]).Trim();
               mat.ordre = r["ORDRE"] is DBNull ? 0 : Convert.ToInt32(r["ORDRE"]);
               mat.Qte = r["QUANTITE_MATERIEL"] is DBNull ? 1 : Convert.ToInt32(r["QUANTITE_MATERIEL"]);
               mat.BaseRemboursement = r["ACTE_BASE_REMBOURSEMENT"] is DBNull ? 0 : Convert.ToDouble(r["ACTE_BASE_REMBOURSEMENT"]);
               mat.Remboursement = r["ACTE_REMBOURSEMENT"] is DBNull ? 0 : Convert.ToDouble(r["ACTE_REMBOURSEMENT"]);
               mat.Depassement = r["ACTE_DEPASSEMENT"] is DBNull ? 0 : Convert.ToDouble(r["ACTE_DEPASSEMENT"]);
               mat.CodeTransposition = r["ACTE_CODE_TRANSPOSOTION"] is DBNull ? "" : Convert.ToString(r["ACTE_CODE_TRANSPOSOTION"]);
               mat.isFacture = r["ISFACTURE"] is DBNull ? false : Convert.ToBoolean(r["ISFACTURE"]);
            return mat;
        }
         public static Materiel BuildJ(JObject r)
         {
             Materiel mat = new Materiel();
             mat.id_materiel = Convert.ToInt32(r["id_MATERIEL"]);
             mat.materiel_libelle = Convert.ToString(r["materiel_LIBELLE"]).Trim();
             mat.materiel_couleur = Convert.ToInt32(r["materiel_COULEUR"]) == 0 ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["materiel_COULEUR"]));
             mat.id_famille = Convert.ToInt32(r["id_FAMILLE_MATERIEL"]);
             mat.prix_materiel = Convert.ToDouble(r["prix_MATERIEL"]);
             mat.materiel_shortlib = Convert.ToString(r["shortlib"]).Trim();
             mat.coefficient = r["coefficient"].ToString() == "" ? -1 : Convert.ToInt32(r["coefficient"]);
             mat.materiel_libelle_facture = Convert.ToString(r["materiel_LIBELLE_ESTIMATION"]).Trim();
             mat.materiel_libelle_estimation = Convert.ToString(r["materiel_LIBELLE_ESTIMATION"]).Trim();
             mat.nomenclature = Convert.ToString(r["nomenclature"]).Trim();
             mat.cotation = Convert.ToString(r["cotation"]).Trim();
             mat.ordre = r["ordre"].ToString() == "" ? 0 : Convert.ToInt32(r["ordre"]);
             mat.Qte = r["quantite_MATERIEL"].ToString() == "" ? 0 : Convert.ToInt32(r["quantite_MATERIEL"]);
             mat.BaseRemboursement = r["acte_BASE_REMBOURSEMENT"].ToString() == "" ? 0 : Convert.ToDouble(r["acte_BASE_REMBOURSEMENT"]);
             mat.Remboursement = r["acte_REMBOURSEMENT"].ToString() == "" ? 0 : Convert.ToDouble(r["acte_REMBOURSEMENT"]);
             mat.Depassement = r["acte_DEPASSEMENT"].ToString() == "" ? 0 : Convert.ToDouble(r["acte_DEPASSEMENT"]);
             mat.CodeTransposition = Convert.ToString(r["acte_CODE_TRANSPOSOTION"]);
             mat.isFacture = r["isfacture"].ToString() == "" ? false : Convert.ToBoolean(r["isfacture"]);
             mat.codeBarres = String.IsNullOrEmpty(r["codeBarres"].ToString()) ? "" : r["codeBarres"].ToString().Trim();
             return mat;
         }
    }
}
