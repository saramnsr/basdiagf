using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildFeuilleDeSoin
    {

        public static FeuilleDeSoin Build(DataRow r)
        {
            FeuilleDeSoin fs = new FeuilleDeSoin();
            fs.Id = Convert.ToInt32(r["ID"]);
            fs.datePaiementHonoraire = Convert.ToDateTime(r["DATE_PAIMENT_HONORAIRE"]);
            //            fs.TotalMontantNonSoumisAEntente = Convert.ToDouble(r["MONTANT_NON_SOUMISEP"]);
            //fs.TotalMontantSoumisAEntente = Convert.ToDouble(r["MONTANT_SOUMISEP"]);
            fs.AgremmentRadiation = Convert.ToString(r["NUMAGREEMENT_RADIONISATION"]);
            fs.AgrementPanoramique = Convert.ToString(r["NUMAGREEMENT_PANORAMIQUE"]);
            fs.AgrementTeleradio = Convert.ToString(r["NUMAGREEMENT_TELERADIO"]);
            fs.dateEdition = Convert.ToDateTime(r["DATE_EDITION"]);
            fs.typedenvois = (FeuilleDeSoin.TypeEnvois)Convert.ToInt32(r["TYPE_ENVOIS"]);
            return fs;
        }

    }
}
