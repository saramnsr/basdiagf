using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using BasCommon_DAL;
using System.Data;
using BasCommon_BO.Compta;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Compta
{
    public static class MgmtCodeComptable
    {

            public const string CLASSE1 = "1 Capital";
            public const string CLASSE2 = "2 Immobilisations";
            public const string CLASSE3 = "3 Stock";
            public const string CLASSE4 = "4 Fournisseurs";
            public const string CLASSE5 = "5 Valeurs mobilieres";
            public const string CLASSE6 = "6 Achats";
            public const string CLASSE7 = "7 Ventes";
            public const string CLASSE8 = "8 Comptes spéciaux";



        private static List<CodeComptable> _codescomptables = null;
        public static List<CodeComptable> codescomptables
        {
            get
            {

                if (_codescomptables == null) _codescomptables = getCodesComptable();
                return _codescomptables;

            }
        }





        private static List<CodeComptable> getCodesComptable()
        {
            List<CodeComptable> lst = new List<CodeComptable>();
            JArray array = BasCommon_DAL.DAC.getMethodeJsonArray("/getCodesComptables");

            foreach (JObject dr in array)
            {
                CodeComptable cc = Builders.BuildCodeComptable.BuildJson(dr);
                lst.Add(cc);
            }
            return lst;
        }


        private static List<CodeComptable> getCodesComptableold()
        {
            List<CodeComptable> lst = new List<CodeComptable>();
            DataTable dt = DAC.getCodesComptables();

            foreach (DataRow dr in dt.Rows)
            {
                CodeComptable cc = Builders.BuildCodeComptable.Build(dr);
                lst.Add(cc);
            }
            return lst;
        }


        public static void Add(CodeComptable cc)
        {
            DAC.AddCodeComptable(cc);
            codescomptables.Add(cc);
        }


        public static void Delete(CodeComptable cc)
        {
            DAC.DeleteCodeComptable(cc);
            codescomptables.Remove(cc);
        }


        public static CodeComptable getFromCode(string cde)
        {

            string fullcde = cde;
            foreach (var cc in codescomptables)
            {
                if (cc.Code == fullcde)
                    return cc;
                
            }
            return null;
        }

        public static List<CodeComptable> getSousClasses()
        {
            List<CodeComptable> lst = new List<CodeComptable>();
            foreach (var cc in codescomptables)
            {
                if (cc.Code.Length == 2)
                    lst.Add(cc);
                
            }

            return lst;
        }

        public static List<CodeComptable> getSousSousClasses()
        {
            List<CodeComptable> lst = new List<CodeComptable>();
            foreach (var cc in codescomptables)
            {
                if (cc.Code.Length == 3)
                    lst.Add(cc);

            }

            return lst;
        }

    }


}
