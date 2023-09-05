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

    public static class BuildSemestre
    {

        public static Semestre BuildJ(JObject r)
        {
            Semestre s = new Semestre();
            s.Id = r["id"].ToString() =="" ? -1 : Convert.ToInt32(r["id"]);
            s.traitementSecu = r["idActeGestion"].ToString() == "" ? null : TemplateApctePGMgmt.getTemplatesActeGestion(Convert.ToInt32(r["idActeGestion"]));
            s.CodeSemestre = r["codesemestre"].ToString() =="" ? "" : Convert.ToString(r["codesemestre"]);
            s.Montant_Honoraire = r["montantSemestre"].ToString() == "" ? 0 : Convert.ToDouble(r["montantSemestre"]);
            s.Montant_AvantRemise = r["montant_semestre_avantremise"].ToString() == "" ? s.Montant_Honoraire : Convert.ToDouble(r["montant_semestre_avantremise"]);
            s.DateDebut = r["datedebut"].ToString() =="" ? DateTime.Now.AddDays(15) : Convert.ToDateTime(r["datedebut"]);
            s.DateFin = r["datefin"].ToString() =="" ? s.DateDebut.AddMonths(1) : Convert.ToDateTime(r["datefin"]);
            s.NumSemestre = r["numsemestre"].ToString() =="" ? 0 : Convert.ToInt32(r["numsemestre"]);
            s.IdDEPPreAssocier = r["idDep"].ToString() == "" ? -1 : Convert.ToInt32(r["idDep"]);
            s.IdTraitement = r["idTraitement"].ToString() == "" ? -1 : Convert.ToInt32(r["idTraitement"]);

            return s;
        }

        public static Semestre Build(DataRow r)
        {
            Semestre s = new Semestre();
            s.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
            s.traitementSecu = r["ID_ACTE_GESTION"] is DBNull ? null : TemplateApctePGMgmt.getTemplatesActeGestion(Convert.ToInt32(r["ID_ACTE_GESTION"]));
            s.CodeSemestre = r["CODESEMESTRE"] is DBNull ? "" : Convert.ToString(r["CODESEMESTRE"]);
            s.Montant_Honoraire = r["MONTANT_SEMESTRE"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT_SEMESTRE"]);
            s.Montant_AvantRemise = r["MONTANT_SEMESTRE_AvantRemise"] is DBNull ? s.Montant_Honoraire : Convert.ToDouble(r["MONTANT_SEMESTRE_AvantRemise"]);  
            s.DateDebut = r["DATEDEBUT"] is DBNull? DateTime.Now.AddDays(15) : Convert.ToDateTime(r["DATEDEBUT"]);
            s.DateFin = r["DATEFIN"] is DBNull ? s.DateDebut.AddMonths(1) : Convert.ToDateTime(r["DATEFIN"]);
            s.NumSemestre = r["NUMSEMESTRE"] is DBNull ? 0 : Convert.ToInt32(r["NUMSEMESTRE"]);
            s.IdDEPPreAssocier = r["id_dep"] is DBNull ? -1 : Convert.ToInt32(r["id_dep"]);
            s.IdTraitement = r["id_traitement"] is DBNull ? -1 : Convert.ToInt32(r["id_traitement"]);
            
            return s;
        }
    }
}
