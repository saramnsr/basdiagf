using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    
    public static class BuildMvtCaisse
    {

        public static MvtCaisse Build(DataRow r)
        {

            MvtCaisse obj = null;

            obj = new MvtCaisse();
            obj.Date = Convert.ToDateTime(r["DATE_MVT"]);
            obj.Mvt = Convert.ToDouble(r["MONTANT"]);
            

            return obj;
        }
        
    }
}
