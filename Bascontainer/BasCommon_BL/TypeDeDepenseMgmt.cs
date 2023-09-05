using BasCommon_BO;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL
{
    public static class TypeDeDepenseMgmt
    {

        private static List<TypeDeDepense> _TypeDeDepenses = null;
        public static List<TypeDeDepense> TypeDeDepenses
        {
            get
            {
                if (_TypeDeDepenses == null) _TypeDeDepenses = getTypeDeDepenses();
                return _TypeDeDepenses;
            }
            set
            {
                _TypeDeDepenses = value;
            }
        }

        private static List<TypeDeDepense> getTypeDeDepenses()
        {
            List<TypeDeDepense> lst = new List<TypeDeDepense>();
            JArray array = BasCommon_DAL.DAC.getMethodeJsonArray("/GetTypeDeDepenses");

            foreach (JObject dr in array)
            {
                TypeDeDepense b = Builders.BuildTypeDeDepense.BuildJson(dr);

                lst.Add(b);
            }

            return lst;
        }

        private static List<TypeDeDepense> getTypeDeDepensesOLD()
        {
            List<TypeDeDepense> lst = new List<TypeDeDepense>();
            DataTable dt = DAC.GetTypeDeDepenses();

            foreach (DataRow dr in dt.Rows)
            {
                TypeDeDepense b = Builders.BuildTypeDeDepense.Build(dr);

                lst.Add(b);
            }

            return lst;
        }

        public static void InsertTypeDeDepense(TypeDeDepense tdd)
        {
            if (tdd.Id == -1) DAC.InsertTypeDeDepense(tdd);
            TypeDeDepenses.Add(tdd);
        }

        public static void DeleteTypeDeDepense(TypeDeDepense tdd)
        {
            DAC.DeleteTypeDeDepense(tdd.Id);
            TypeDeDepenses.Remove(tdd);
        }

        
    }
}
