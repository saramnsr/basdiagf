using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildRecontactLib
    {

        public static RecontactLib Build(DataRow r)
        {
            RecontactLib rec = new RecontactLib();
            rec.Id = SysTools.DataRow_ValueInt(r, "ID");
            rec.Libelle = SysTools.DataRow_ValueString(r, "LIB_RECO");

            return rec;
        }
    }
}
