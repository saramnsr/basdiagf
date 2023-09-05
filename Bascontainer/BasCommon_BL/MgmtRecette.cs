using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_DAL;

namespace BasCommon_BL
{
    public static class MgmtRecette
    {

        public static List<Recette> GetRecettes(DateTime dte1,DateTime dte2,BanqueDeRemise bqe)
        {
            DataTable dt = DAC.GetRecettes(dte1, dte2, bqe);

            List<Recette> lst = new List<Recette>();

            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(Builders.BuildRecette.Build(dr));
            }

            return lst;
        }

        public static void UpdateRecette(Recette rec)
        {
            DAC.UpdateRecette(rec);
        }

        public static void InsertRecette(Recette rec)
        {
            DAC.InsertRecette(rec);
        }
    }
}
