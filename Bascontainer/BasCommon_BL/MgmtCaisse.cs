using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_DAL;
using System.Data;
using BasCommon_BO;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public class CaissesManager
    {

        private static List<Caisse> _caisses;
        public static List<Caisse> caisses
        {
            get
            {
                if (_caisses == null) _caisses = getAllCaisses();
                return _caisses;
            }
            set
            {
                _caisses = value;
            }
        }

        static List<Caisse> getAllCaisses()
        {
            List<Caisse> lst = new List<Caisse>();
            JArray json = DAC.getMethodeJsonArray("/Caisses");
            foreach (JObject r in json)
            {
                lst.Add(Builders.BuildCaisse.BuildJ(r));
            }
            return lst;
        }

        static List<Caisse> getAllCaissesOLD()
        {
            List<Caisse> lst = new List<Caisse>();
            DataTable dt;

            dt = DAC.getCaisses();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildCaisse.Build(r));
            }
            return lst;
        }

        public static Caisse getCaisseByIdPatient(int Idpat)
        {
            DataRow dr = DAC.getCaisseByIdPatient(Idpat);

            if (dr == null) return null;

            int id = Convert.ToInt32(dr["ID_CAISSE"]);

            List<Caisse> lst = new List<Caisse>();

            foreach (Caisse c in caisses)
                if (id == c.Id)
                    return c;

            return null;
        }

        
        public static Caisse getCaisse(basePatient pat)
        {

            return getCaisseByIdPatient(pat.Id);
        }

        public static Caisse getCaisse(int p_ID)
        {


            foreach (Caisse c in caisses)
                if (c.Id == p_ID)
                    return c;

            return null;
        }

        public static List<object> getCaisses()
        {
            List<object> lst = new List<object>();

            foreach (Caisse c in caisses)
                lst.Add(c);

            return lst;

        }

        public static List<Caisse> getCaisses(string parametres)
        {
            List<Caisse> lst = new List<Caisse>();

            foreach (Caisse c in caisses)
                if (c.Nom.ToUpper().Contains(parametres.ToUpper()))
                    lst.Add(c);

            return lst;

        }

        public static List<Caisse> getCaissesByNom(string m_Nom)
        {
            List<Caisse> lst = new List<Caisse>();

            foreach (Caisse c in caisses)
                if (c.Nom.Contains(m_Nom))
                    lst.Add(c);

            return lst;
        }

        public static int AddCaisse(Caisse p_caisse)
        {
            caisses.Add(p_caisse);
            return DAC.InsertCaisse(p_caisse);

        }

        public static void UpdateCaisse(Caisse p_caisse)
        {
            DAC.UpdateCaisse(p_caisse);
        }

        public static void Save(Caisse p_caisse)
        {
            if (p_caisse.Id < 0)
            {
                AddCaisse(p_caisse);
               // caisses.Add(p_caisse);
            }
            else
            {
                UpdateCaisse(p_caisse);
            }
        }


    }
}
