using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_DAL;
using BASEDiag_BO;

namespace BASEDiag_BL
{
    public static class PhasesMgmt
    {




        public static void SetPhases(Proposition proposition)
        {
            DAC.SetPhases(proposition);

        }

        public static List<Phase> GetPhases(Proposition proposition)
        {
            DataTable dt = DAC.getPhases(proposition);
            List<Phase> lst = new List<Phase>();

            foreach (DataRow dr in dt.Rows)
            {
                Phase p = Builders.BuildPhase(dr);
                lst.Add(p);

            }

            return lst;

        }
    }
}
