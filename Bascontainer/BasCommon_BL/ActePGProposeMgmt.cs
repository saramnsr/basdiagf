using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using BasCommon_DAL;

namespace BasCommon_BL
{
    public static class ActePGProposeMgmt
    {

        public static void AddActePGPropose(ActePGPropose a)
        {
            DAC.Insert_acte_propositions(a);

        }

        public static void updateActePGPropose(ActePGPropose a)
        {
            DAC.updateActePGPropose(a);

        }



    }
}
