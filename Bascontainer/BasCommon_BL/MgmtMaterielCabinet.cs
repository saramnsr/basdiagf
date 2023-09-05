using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;

namespace BasCommon_BL
{
    public static class MgmtMaterielCabinet
    {
        public static void Add(baseMaterielCabinet matcab)
        {
            BasCommon_DAL.DAC.AddMaterielCabinet(matcab);


        }
        public static void Delete(int id)
        {
            BasCommon_DAL.DAC.DeleteMatCab(id);
        }
    }
}
