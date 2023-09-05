using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildMaterielCabinet
    {

        public static MaterielCabinet Build(DataRow r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono

            MaterielCabinet matcab = new MaterielCabinet();
            matcab.Id = r["ID_MATERIEL"] is DBNull ? -1 : Convert.ToInt32(r["ID_MATERIEL"]);
            matcab.Libelle = r["MAT_LIBELLE"] is DBNull ? "" : Convert.ToString(r["MAT_LIBELLE"]).Trim();
            matcab.Description = r["MAT_DESCRIPTION"] is DBNull ? "" : Convert.ToString(r["MAT_DESCRIPTION"]).Trim();
            return matcab;


        }

    }
}
