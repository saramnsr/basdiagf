using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using BasCommon_BO;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{
    public static class BuildContactLib
    {
        public static ContactLib Build(DataRow r)
        {
            ContactLib c = new ContactLib();

            c.Id = Convert.ToInt32(r["ID"]);
            c.typeCtact = (Contact.ContactType)Convert.ToInt32(r["TYPECONTACT"]);
            c.AffectedTo = (ContactLib.AffecteA)Convert.ToInt32(r["TYPEAFFECTATION"]);
            c.Libelle = Convert.ToString(r["LIBELLE"]);
            c.Priorite = (r["PRIORITE"] is DBNull) ? 0 : Convert.ToInt32(r["PRIORITE"]);

            return c;

        }
        public static ContactLib BuildJ(JObject r)
        {
            ContactLib c = new ContactLib();

            c.Id = Convert.ToInt32(r["id"]);
            c.typeCtact = (Contact.ContactType)Convert.ToInt32(r["typecontact"]);
            c.AffectedTo = (ContactLib.AffecteA)Convert.ToInt32(r["typeaffectation"]);
            c.Libelle = Convert.ToString(r["libelle"]);
            c.Priorite = (r["priorite"].ToString() == "") ? 0 : Convert.ToInt32(r["priorite"]);

            return c;

        }
    }
}
