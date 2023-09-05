using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class EntiteJuridiqueMgmt
    {


        private static List<EntiteJuridique> _entites = null;
        public static List<EntiteJuridique> entites
        {
            get
            {
                if (_entites == null) _entites = getEntites();
                return _entites;
            }
            set
            {
                _entites = value;
            }
        }

        public static void AddEntite(EntiteJuridique en)
        {
            DAC.AddEntiteJuridique(en);
            _entites.Add(en);
        }

        public static void UpdateEntite(EntiteJuridique en)
        {
            DAC.UpdateEntiteJuridique(en);
        }

        static List<EntiteJuridique> getEntites()
        {
            List<EntiteJuridique> lst = new List<EntiteJuridique>();
            JArray json = DAC.getMethodeJsonArray("/EntitesJuridique");

            foreach (JObject r in json)
                lst.Add(Builders.BuildEntiteJuridique.BuildJ(r));

            return lst;
        }

        static List<EntiteJuridique> getEntitesOLD()
        {
            List<EntiteJuridique> lst = new List<EntiteJuridique>();
            DataTable dt = DAC.getEntitesJuridique();

            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildEntiteJuridique.Build(r));

            return lst;
        }
        public static EntiteJuridique getentite(int id)
        {
            foreach (EntiteJuridique ej in entites)
            {
                if (ej.Id == id) return ej;
            }
            return null;
        }

        public static EntiteJuridique getentite(basePatient pat)
        {

            int id = DAC.GetEntiteJuridiqueOf(pat.Id);
            return getentite(id);
        }

        public static EntiteJuridique getentiteByIdPatient(int id)
        {

            int ide = DAC.GetEntiteJuridiqueOf(id);
            return getentite(ide);
        }



        public static string GetNbPatientParEntity()
        {

            Dictionary<int, int> dico = new Dictionary<int, int>();

            DataTable dt = DAC.NbPatientsParEntite();

            foreach (DataRow dr in dt.Rows)
            {
                int idEntity = dr["id_entityjuridique"] is DBNull ? -1 : Convert.ToInt32(dr["id_entityjuridique"]);
                int nbPat = Convert.ToInt32(dr["nbPatient"]);
                dico.Add(idEntity, nbPat);
            }

            int total = 0;
            foreach (KeyValuePair<int, int> kv in dico)
                total += kv.Value;

            string s = "";
            foreach (KeyValuePair<int, int> kv in dico)
            {
                if (s != "") s += "\n";

                s += (kv.Value / (float)total).ToString("0.0% : ");

                EntiteJuridique e = getentite(kv.Key);
                if (e == null)
                    s += "Sans Affectation";
                else
                    s += e.Nom;




            }
            return s;

        }

        public static List<EntiteJuridique> getEntitesJuridiques(BanqueDeRemise bdr)
        {
            List<EntiteJuridique> lstent = new List<EntiteJuridique>();

            List<int> lstids = DAC.GetEntitesJuridique(bdr);
            foreach (EntiteJuridique e in entites)
            {
                if (lstids.Contains(e.Id))
                    lstent.Add(e);
            }

            return lstent;
        }

    }
}
