using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using BASEDiag_BO;
using BASEDiag_DAL;
using BasCommon_BL.Builders;

namespace BASEDiag_BL
{
    public static class PlanTraitementDEPMgmt
    {

        private static List<PlanTraitementDEP> _plantraitementsDEP;
        public static List<PlanTraitementDEP> plantraitementsDEP
        {
            get
            {
                if (_plantraitementsDEP == null) _plantraitementsDEP = getPlanTraitementDEP();
                return _plantraitementsDEP;
            }
            set
            {
                _plantraitementsDEP = value;
            }
        }



        public static List<PlanTraitementDEP> getPlanTraitementDEP()
        {
            DataTable dt = DAC.getPlanTraitementsDEP();

            List<PlanTraitementDEP> lst = new List<PlanTraitementDEP>();
            foreach (DataRow r in dt.Rows)
            {
                lst.Add(BuildPlanTraitementDEP.Build(r));
            }
            return lst;

        }

    }
}
