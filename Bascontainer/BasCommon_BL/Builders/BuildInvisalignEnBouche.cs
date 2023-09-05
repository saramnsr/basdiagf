using BasCommon_BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL.Builders
{
    public static class BuildInvisalignEnBouche
    {
        public static InvisalignEnBouche Build(DataRow r)
        {

            InvisalignEnBouche ib = new InvisalignEnBouche();
            ib.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
            ib.DateDebut = r["DATEDEBUT"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEDEBUT"]);
            ib.DateFin = r["DATEFIN"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEFIN"]);
            ib.NumAligneur = Convert.ToInt32(r["NUMALIGNEUR"]);
            ib.IdPatient = Convert.ToInt32(r["ID_PATIENT"]);
            ib.IsHaut = r["HAUT"] is DBNull ? false : Convert.ToChar(r["HAUT"])==1;
            
            return ib;
        }
    }
}
