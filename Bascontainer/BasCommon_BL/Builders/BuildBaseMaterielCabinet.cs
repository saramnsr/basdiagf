using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{
    public static class BuildBaseMaterielCabinet
    {
        public static baseMaterielCabinet BuildMat(DataRow r)
        {
            
            baseMaterielCabinet mat = new baseMaterielCabinet();
            mat.Id = r["ID_PERSONNE"] is DBNull ? -1 : Convert.ToInt32(r["ID_PERSONNE"]);
            mat.Libelle = r["PER_NOM"] is DBNull ? "" : Convert.ToString(r["PER_NOM"]).Trim();
            mat.Description = r["PER_PRENOM"] is DBNull ? "" : Convert.ToString(r["PER_PRENOM"]).Trim();
            mat.DateAchat = r["PER_DATNAISS"] is DBNull ? DateTime .Now : Convert.ToDateTime(r["PER_DATNAISS"]);
            
            return mat;


        }
    }
}
