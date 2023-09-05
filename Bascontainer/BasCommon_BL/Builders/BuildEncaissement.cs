using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{

    public static class BuildEncaissement
    {
        public static Encaissement Build(DataRow r)
        {
            Encaissement cs = new Encaissement();
            cs.Id = Convert.ToInt32(r["ID"]);
            cs.IdPatient = Convert.ToInt32(r["ID_PATIENT"]);
            cs.MontantEncaisse = Convert.ToDouble(r["MONTANT_ENCAISSE"]);
            cs.IdPaiementReel = Convert.ToInt32(r["ID_PAIEMENT_REEL"]);

            return cs;
        }
        public static Encaissement BuildJ(JObject r)
        {
            Encaissement cs = new Encaissement();
            cs.Id = Convert.ToInt32(r["id"]);
            cs.IdPatient = Convert.ToInt32(r["id_PATIENT"]);
            cs.MontantEncaisse = Convert.ToDouble(r["montant_ENCAISSE"]);
            cs.IdPaiementReel = Convert.ToInt32(r["id_PAIEMENT_REEL"]);

            return cs;
        }
    }
}
