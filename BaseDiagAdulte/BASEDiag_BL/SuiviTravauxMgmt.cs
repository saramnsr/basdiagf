using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_DAL;
using BASEDiag_BO;
using BasCommon_BO;

namespace BASEDiag_BL
{
    public static class SuiviTravauxBaseLaboMgmt
    {

        public static List<ObjSuivi> GetAllAppareils(basePatient pat)
        {

            List<ObjSuivi> objs = new List<ObjSuivi>();

            DataTable dt = DAC_BaseProduct.GetAllObjSuivi(pat);


            foreach (DataRow r in dt.Rows)
            {
                ObjSuivi obj = Builders.BuildObjSuivi(r);
                objs.Add(obj);
            }


            return objs;

        }
    }
}
