using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_DAL;
using BASEDiag_BO;

namespace BASEDiag_BL
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


        static List<EntiteJuridique> getEntites()
        {
            List<EntiteJuridique> lst = new List<EntiteJuridique>();
            DataTable dt = DAC.getEntitesJuridique();

            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildEntiteJuridique(r));

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
    }
}
