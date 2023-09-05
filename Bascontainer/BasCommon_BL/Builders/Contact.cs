using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{
    class BuildContact
    {

        public static Contact Build(DataRow r)
        {
            Contact c = new Contact();

            c.Id = Convert.ToInt32(r["ID"]);
            c.TypeContact = (Contact.ContactType)Convert.ToInt32(r["CONTACTTYPE"]);
            c.Value = Convert.ToString(r["VALUE"]);
            c.id_pays = r["id_pays"] is DBNull ? -1: Convert.ToInt32(r["id_pays"]);
            c.isSms = r["sms"] is DBNull ? false : Convert.ToBoolean(r["sms"]);
            c.Libelle = r["ID_CONTACTLIBELLE"] is DBNull ? null : MgmtContactLib.getLib(Convert.ToInt32(r["ID_CONTACTLIBELLE"]));
            c.prefOrder = r["PREF_ORDER"] is DBNull ? null : (int?)Convert.ToInt32(r["PREF_ORDER"]);
            c.IdPersonne = Convert.ToInt32(r["ID_PERSONNE"]);
            if (c.id_pays != -1)
                c.pays = MgmtPays.getPaysById(c.id_pays);
            if (!(r["ADR1"] is DBNull) || !(r["ADR2"] is DBNull) || !(r["CODEPOSTAL"] is DBNull) || !(r["VILLE"] is DBNull))
            {
                c.adresse = new ContactAdresse();

                c.adresse.Adr1 = (r["ADR1"] is DBNull) ? "" : Convert.ToString(r["ADR1"]);
                c.adresse.Adr2 = (r["ADR2"] is DBNull) ? "" : Convert.ToString(r["ADR2"]);
                c.adresse.CodePostal = (r["CODEPOSTAL"] is DBNull) ? "" : Convert.ToString(r["CODEPOSTAL"]);
                c.adresse.Ville = (r["VILLE"] is DBNull) ? "" : Convert.ToString(r["VILLE"]);
                c.adresse.Pays = (r["PAYS"] is DBNull) ? "" : Convert.ToString(r["PAYS"]);
            }

            return c;

        }

        public static Contact BuildJson(JObject r)
        {
            Contact c = new Contact();

            c.Id = Convert.ToInt32(r["id"]);
            c.TypeContact = (Contact.ContactType)Convert.ToInt32(r["contacttype"]);
            c.Value = Convert.ToString(r["value"]);
            c.id_pays = r["id_PAYS"].ToString() == "" ? -1 : Convert.ToInt32(r["id_PAYS"]);
            c.isSms = r["sms"].ToString() == "" ? false : Convert.ToBoolean(r["sms"]);
            c.Libelle = r["id_CONTACTLIBELLE"].ToString() == "" ? null : MgmtContactLib.getLib(Convert.ToInt32(r["id_CONTACTLIBELLE"]));
            c.prefOrder = r["pref_ORDER"].ToString() == "" ? 0 : Convert.ToInt32(r["pref_ORDER"]);
            c.IdPersonne = Convert.ToInt32(r["id_PERSONNE"]);
            if (c.id_pays != -1)
                c.pays = MgmtPays.getPaysById(c.id_pays);
            if (!(r["adr1"].ToString() == "") || !(r["adr2"].ToString() == "") || !(r["codepostal"].ToString() == "") || !(r["ville"].ToString() == ""))
            {
                c.adresse = new ContactAdresse();
                c.adresse.Adr1 = (r["adr1"].ToString() == "") ? "" : Convert.ToString(r["adr1"]);
                c.adresse.Adr2 = (r["adr2"].ToString() == "") ? "" : Convert.ToString(r["adr2"]);
                c.adresse.CodePostal = (r["codepostal"].ToString() == "") ? "" : Convert.ToString(r["codepostal"]);
                c.adresse.Ville = (r["ville"].ToString() == "") ? "" : Convert.ToString(r["ville"]);
                c.adresse.Pays = (r["pays"].ToString() == "") ? "" : Convert.ToString(r["pays"]);
            }
            return c;

        }

    }
}
