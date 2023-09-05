using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildProfilApplique
    {

        public static ProfilApplique Build(DataRow r)
        {
            ProfilApplique pi = new ProfilApplique();

            pi.Id = Convert.ToInt32(r["ID_PROFIL"]);
            pi.Profil = Convert.ToString(r["NOM_PROFIL"]).Trim();

            return pi;

        }

    }
}
