using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class BaseLaboMgmt
    {
        public static List<ObjSuivi> GetAllSuivis(DateTime dt1, DateTime dt2)
        {

            List<ObjSuivi> objs = new List<ObjSuivi>();

            DataTable dt = DAC.GetAllObjSuivi(dt1, dt2);


            foreach (DataRow r in dt.Rows)
            {
                ObjSuivi obj = Builders.BuildObjSuivi.Build(r);
                objs.Add(obj);
            }


            return objs;

        }

        public static List<ObjSuivi> GetAllSuivis(int IdPatient)
        {
            JArray json = DAC.getMethodeJsonArray("/AllObjSuivis/"+IdPatient);
            List<ObjSuivi> objs = new List<ObjSuivi>();




            foreach (JObject r in json)
            {
                ObjSuivi obj = Builders.BuildObjSuivi.BuildJ(r);
                objs.Add(obj);
            }


            return objs;

        }
        public static List<ObjSuivi> GetAllSuivisOLD(int IdPatient)
        {

            List<ObjSuivi> objs = new List<ObjSuivi>();

            DataTable dt = DAC.GetAllObjSuivis(IdPatient);


            foreach (DataRow r in dt.Rows)
            {
                ObjSuivi obj = Builders.BuildObjSuivi.Build(r);
                objs.Add(obj);
            }


            return objs;

        }

    }
}
