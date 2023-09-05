using BasCommon_BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL.Builders
{
   public partial class BuildCabinet
    {
        public static Cabinet Build(DataRow dr)
        {
            Cabinet cab = new Cabinet();
            cab.Id = dr["Id"] is DBNull ? -1 : Convert.ToInt32(dr["Id"]);
            cab.nomCabinet = dr["nomcabinet"] is DBNull ? "" : Convert.ToString(dr["nomcabinet"]).Trim();
            cab.prefix = dr["prefix"] is DBNull ? "" : Convert.ToString(dr["prefix"]).Trim();
            return cab;
        }
    }
}
