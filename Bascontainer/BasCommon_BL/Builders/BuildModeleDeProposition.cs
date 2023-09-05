using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildModeleDeProposition
    {

        public static ModeleDePropositions Build(DataRow r)
        {
            ModeleDePropositions mdl = new ModeleDePropositions();

            mdl.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
            mdl.Nom = r["LIBELLE"] is DBNull ? "" : Convert.ToString(r["LIBELLE"]);

            return mdl;
        }
    }
}
