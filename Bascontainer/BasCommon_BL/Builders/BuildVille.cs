using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using BasCommon_BO;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{
    class BuildVille
    {

        public static Ville Build(DataRow r)
        {
            Ville v = new Ville();
            v.Id = Convert.ToInt32(r["ID_VILLE"]);
            v.NomVille = Convert.ToString(r["nomville"]).Trim();
            v.CodePostal = Convert.ToString(r["codepostal"]).Trim();
            v.Longitude = r["Longitude"] is DBNull ? null : (double?)Convert.ToDouble(r["Longitude"]);
            v.Latitude = r["Latitude"] is DBNull ? null : (double?)Convert.ToDouble(r["Latitude"]);

            if (v.Longitude == 0) v.Longitude = null;
            if (v.Latitude == 0) v.Latitude = null; 


            return v;
        }

        public static Ville BuildJson(JObject r)
        {
            Ville v = new Ville();
            v.Id = r["idVille"].ToString() == "" ? -1 : Convert.ToInt32(r["idVille"]);
            v.NomVille = Convert.ToString(r["nomville"]).Trim();
            v.CodePostal = Convert.ToString(r["codepostal"]).Trim();
            v.Longitude = r["longitude"].ToString()=="" ? null : (double?)Convert.ToDouble(r["longitude"]);
            v.Latitude = r["latitude"].ToString()=="" ? null : (double?)Convert.ToDouble(r["latitude"]);

            if (v.Longitude == 0) v.Longitude = null;
            if (v.Latitude == 0) v.Latitude = null;


            return v;
        }
    }
}
