using BasCommon_BO.Compta;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL.Compta
{
    public static class TaxeValeurAjouteeMgmt
    {
        static List<TaxeValeurAjoutee> _taxes = null;
        public static List<TaxeValeurAjoutee> taxes
        {
            get
            {
                if (_taxes == null)
                    _taxes = GetTaxeValeurAjoutee();
                return _taxes;
            }
            set { _taxes = value; }
        }


        public static TaxeValeurAjoutee getTaxe(string code)
        {
            foreach (TaxeValeurAjoutee tva in taxes)
                if (tva.Code == code) return tva;

            return null;
        }

        private static List<TaxeValeurAjoutee> GetTaxeValeurAjoutee() {

            string method = "/allTaxesValeurAjoutee";
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);

            List<TaxeValeurAjoutee> liste = new List<TaxeValeurAjoutee>();

            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildTaxeValeurAjoutee.Build(obj));
            
            return liste;        
        }

        private static List<TaxeValeurAjoutee> GetTaxeValeurAjouteeOld()
        {
            DataTable dt = BasCommon_DAL.DAC.getTaxeValeurAjoutee();

            List<TaxeValeurAjoutee> lst = new List<TaxeValeurAjoutee>();

            foreach (DataRow dr in dt.Rows)
            {
                TaxeValeurAjoutee tva = Builders.BuildTaxeValeurAjoutee.Build(dr);

                lst.Add(tva);
            }


            return lst;
        }
    }
}
