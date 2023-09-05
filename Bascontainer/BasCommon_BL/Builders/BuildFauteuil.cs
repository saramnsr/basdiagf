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

    public static class BuildFauteuil
    {
        public static Fauteuil Build(DataRow r)
        {
            Fauteuil faut = new Fauteuil();
            faut.Id = Convert.ToInt32(r["id_fauteuil"]);
            faut.libelle = Convert.ToString(r["faut_libelle"]).Trim();
            faut.Name = faut.libelle;
            faut.Visible = true;
            return faut;
        }
        public static Fauteuil BuildJ(JObject r)
        {
            Fauteuil faut = new Fauteuil();
            faut.Id = Convert.ToInt32(r["id_fauteuil"]);
            faut.libelle = Convert.ToString(r["fautLibelle"]).Trim();
            faut.Name = faut.libelle;
            faut.Visible = true;
            return faut;
        }
    }
}
