using BasCommon_BO.Compta;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL.Builders
{

    public static class BuildTaxeValeurAjoutee
    {
        public static TaxeValeurAjoutee Build(DataRow r)
        {
            TaxeValeurAjoutee act = new TaxeValeurAjoutee();
            act.Code =Convert.ToString(r["code"]);
            act.Libelle =  Convert.ToString(r["Libelle"]).Trim();
            act.Taux = Convert.ToDouble(r["Taux"]);
             return act;
        }

        public  static TaxeValeurAjoutee Build(JObject obj)
        {
            TaxeValeurAjoutee taxe = new TaxeValeurAjoutee();

            taxe.Code = obj["code"].ToString().Trim();
            taxe.Libelle = obj["libelle"].ToString().Trim();
            taxe.Taux = Convert.ToDouble(obj["taux"]);

            return taxe;

        }
    }

   
}
