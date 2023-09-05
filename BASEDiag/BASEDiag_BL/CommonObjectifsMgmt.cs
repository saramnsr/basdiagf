using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using BASEDiag_BO;
using BASEDiag_DAL;
using BasCommon_BO;
using Newtonsoft.Json.Linq;

namespace BASEDiag_BL
{
    public static class CommonObjectifsMgmt
    {

        private static List<CommonObjectif> _CommonObjectifs;
        public static List<CommonObjectif> CommonObjectifs
        {
            get
            {
                if (_CommonObjectifs == null)
                    _CommonObjectifs = getCommonObjectifs();

                return _CommonObjectifs;
            }

        }

        private static List<CommonObjectif> getCommonObjectifs()
        {
            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/getCommonObjectif/");
            List<CommonObjectif> lst = new List<CommonObjectif>();
            foreach (JObject r in json)
            {
                lst.Add(Builders.BuildCommonObjectifJson(r));
            }
            return lst;
        }

        private static List<CommonObjectif> getCommonObjectifsOld()
        {
            DataTable dt = DAC.getCommonObjectifs();

            List<CommonObjectif> lst = new List<CommonObjectif>();
            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildCommonObjectif(r));
            }
            return lst;
        }

        public static CommonObjectif getCommonObjectif(int Id)
        {
            foreach (CommonObjectif f in CommonObjectifs)
                if (f.Id == Id) return f;

            return null;

        }




        public static List<CommonAppareilFromObj> getCommonAppareilFromObj(List<CommonObjectifFromDiag> objs)
        {
            List<CommonObjectif> lsto = new List<CommonObjectif>();

            foreach (CommonObjectifFromDiag cofd in objs) lsto.Add(cofd.objectif);


            DataTable dt = DAC.getAppareilsFromObjectifs(lsto);

            List<CommonAppareilFromObj> lst = new List<CommonAppareilFromObj>();

            if (dt == null) return lst;
            foreach (DataRow r in dt.Rows)
            {
                CommonAppareilFromObj app = Builders.BuildCommonAppareilFromObj(r);
                lst.Add(app);
            }
            return lst;
        }


        public static List<CommonAppareilFromObj> getCommonAppareilFromObj(List<CommonObjectif> objs)
        {


            DataTable dt = DAC.getAppareilsFromObjectifs(objs);

            List<CommonAppareilFromObj> lst = new List<CommonAppareilFromObj>();

            if (dt == null) return lst;
            foreach (DataRow r in dt.Rows)
            {
                CommonAppareilFromObj app = Builders.BuildCommonAppareilFromObj(r);
                lst.Add(app);
            }
            return lst;
        }

    }
}
