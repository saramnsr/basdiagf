using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildPlanTraitementDEP
    {
        public static PlanTraitementDEP Build(DataRow dr)
        {
            PlanTraitementDEP co = new PlanTraitementDEP();
            co.Id = Convert.ToInt32(dr["ID_KEY"]);
            co.Libelle = Convert.ToString(dr["LIBELLE"]);
            return co;
        }

    }
}
