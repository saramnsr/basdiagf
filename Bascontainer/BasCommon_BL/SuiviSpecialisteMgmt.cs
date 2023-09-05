using BasCommon_BO;
using BasCommon_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL
{
    public static class SuiviSpecialisteMgmt
    {

        public static void Save(SuiviSpecialiste ss)
        {
            if (ss.Id == -1)
                InsertSuiviSpecialiste(ss);
            if (ss.Id > 0)
                UpdateSuiviSpecialiste(ss);
        }

        public static void InsertSuiviSpecialiste(SuiviSpecialiste ss)
        {
            if (ss.Id == -1)
                DAC.AddSuiviSpecialistes(ss);
        }

        public static void UpdateSuiviSpecialiste(SuiviSpecialiste ss)
        {
            if (ss.Id > 0)
                DAC.UpdateSuiviSpecialistes(ss);
        }

        public static void DeleteSuiviSpecialiste(SuiviSpecialiste ss)
        {
            if (ss.Id > 0)
                DAC.DeleteSuiviSpecialistes(ss);
        }

        public static List<SuiviSpecialiste> getSuiviSpecialiste(int IdPatient)
        {
            List<SuiviSpecialiste> lst = new List<SuiviSpecialiste>();
            DataTable dt = DAC.getSuiviSpecialistes(IdPatient);

            foreach (DataRow dr in dt.Rows)
            {
                SuiviSpecialiste b = Builders.BuildSuiviSpecialiste.Build(dr);

                lst.Add(b);
            }

            return lst;
        }
    }
}
