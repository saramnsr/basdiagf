using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO.Compta
{
    public class TaxeValeurAjoutee
    {
        public override string ToString()
        {
            return Libelle;
        }
        public string Code { get; set; }
        public string Libelle { get; set; }
        public double Taux { get; set; }
    }
}
