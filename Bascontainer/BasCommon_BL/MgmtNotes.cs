using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_DAL;

namespace BasCommon_BL
{
    public static class NoteMgmt
    {
        public static List<CustomCategorie> GetNotesFromIdPersonne(int id)
        {
            List<CustomCategorie> lst = new List<CustomCategorie>();
            DataTable dt;

            dt = DAC.getNotesFromIdPersonne(id);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildCustomCategorie.Build(r));
            }
            return lst;
        }

        public static int GetCurrentNotesFromIdPersonne(int id)
        {
            return DAC.GetCurrentNotesFromIdPersonne(id);
        }

        public static void AffectNote(int idpersonne, int Note)
        {
            DAC.AffectNote(idpersonne, Note);
        }
    }
}
