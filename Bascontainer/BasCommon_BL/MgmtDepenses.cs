using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_DAL;

namespace BasCommon_BL
{
    public static class MgmtDepenses
    {

        public static void InsertDepense(Depense p_depense)
        {
            DAC.InsertDepense(p_depense);

        }

        public static void UpdateDepense(Depense p_depense)
        {
            DAC.UpdateDepense(p_depense);
        }


        public static List<Depense> GetDepenses(DateTime dte1,DateTime dte2, EntiteJuridique en)
        {

            List<Depense> lst = new List<Depense>();
            DataTable dt = DAC.GetDepenses(dte1, dte2,  en);

            foreach (DataRow dr in dt.Rows)
            {
                Depense d = Builders.BuildDepense.Build(dr);
                lst.Add(d);

            }

            return lst;
        }


    }
}
