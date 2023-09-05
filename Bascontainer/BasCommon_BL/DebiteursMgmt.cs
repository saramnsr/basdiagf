using BasCommon_BO;
using BasCommon_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL
{
    public static class DebiteursMgmt
    {
        public static List<Debiteurs> Debiteurs()
        {
            DataTable dt = DAC.Debiteurs();
            List<Debiteurs> lst = new List<Debiteurs>();

            foreach (DataRow r in dt.Rows)
            {
              
                    Debiteurs stat = Builders.BuildDebiteurs.BuildDbiteurs(r);
                    lst.Add(stat);
               

            }

            return lst;
        }
    }
}
