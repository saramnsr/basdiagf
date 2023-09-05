using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;

namespace BASEDiag_BL
{
    public static class DiagObjTratmntSuggestedMgmt
    {

        public static List<ObjectifSuggests> getAllObjectifs()
        {
            DataTable dt = DAC.getAllObjectifsSuggested();
            List<ObjectifSuggests> lst = new List<ObjectifSuggests>();

            foreach (DataRow dr in dt.Rows)
            {
                ObjectifSuggests o = Builders.BuildObjectifSuggests(dr);
                lst.Add(o);
            }

            return lst;

            
        }

        public static List<ObjectifSuggests> getAllDiagnostiques()
        {
            DataTable dt = DAC.getAllDiagnostiqueSuggested();
            List<ObjectifSuggests> lst = new List<ObjectifSuggests>();

            foreach (DataRow dr in dt.Rows)
            {
                ObjectifSuggests o = Builders.BuildObjectifSuggests(dr);
                lst.Add(o);
            }

            return lst;

            
        }

        
    }
}
