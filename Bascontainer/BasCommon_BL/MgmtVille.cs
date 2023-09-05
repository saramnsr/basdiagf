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
    public static class MgmtVilles
    {



        private static List<Ville> _villes = null;
        public static List<Ville> villes
        {
            get
            {
                if (_villes == null) _villes = getVilles();
                return _villes;
            }
            set
            {
                _villes = value;
            }
        }

        public static void Preload()
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(Preloadrun));
            th.Start();

        }

        private static void Preloadrun()
        {
            _villes = getVilles();
        }

        public static bool CheckVilleExist(ref string CP, ref string Ville)
        {
            List<Ville> villesMatchingNameOnly = new List<Ville>();
            List<Ville> villesMatchingCPOnly = new List<Ville>();
            foreach (Ville v in villes)
            {
                if (v.NomVille.ToUpper() == Ville.ToUpper())
                    villesMatchingNameOnly.Add(v);
                if (v.CodePostal == CP)
                    villesMatchingCPOnly.Add(v);
            }


            if (villesMatchingNameOnly.Count == 1)
            {
                CP = villesMatchingNameOnly[0].CodePostal;
                Ville = villesMatchingNameOnly[0].NomVille;
                return true;

            }

            if (villesMatchingCPOnly.Count == 1)
            {
                CP = villesMatchingCPOnly[0].CodePostal;
                Ville = villesMatchingCPOnly[0].NomVille;
                return true;

            }

            foreach (Ville v in villes)
            {


                if ((v.CodePostal == CP) && (v.NomVille.ToUpper() == Ville.ToUpper()))
                {
                    CP = v.CodePostal;
                    Ville = v.NomVille;
                    return true;
                }

                if ((v.CodePostal.ToUpper() == CP.ToUpper()) && (v.NomVille.ToUpper().StartsWith(Ville.ToUpper())))
                {
                    CP = v.CodePostal;
                    Ville = v.NomVille;
                    return true;
                }



                if (CP == "")
                {
                    if (v.NomVille.ToUpper() == Ville.ToUpper())
                    {
                        CP = v.CodePostal;
                        Ville = v.NomVille;
                        return true;
                    }

                }



            }

            return false;
        }

        public static bool IsVilleExist(string CP,string Ville)
        {
            foreach (Ville v in villes)
            {


                if ((v.CodePostal == CP) && (v.NomVille.ToUpper() == Ville.ToUpper()))
                    return true;
                

            }

            return false;
        }

        public static List<Ville> getVillesFromParam(string param)
        {
            List<Ville> lst = new List<Ville>();
            DataTable dt = DAC.getVillesFromParam(param);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildVille.Build(r));
            }
            return lst;
        }

        public static void UpdateEloignementVilles(List<Ville> lst)
        {

            foreach (Ville v in lst)
                DAC.UpdateEloignementVille(v);
        }

        private static List<Ville> getVilles()
        {
            List<Ville> lst = new List<Ville>();
            JArray dt = BasCommon_DAL.DAC.getMethodeJsonArray("/villes");

            foreach (JObject r in dt)
            {
                lst.Add(Builders.BuildVille.BuildJson(r));
            }
            return lst;
        }

        private static List<Ville> getVillesold()
        {
            List<Ville> lst = new List<Ville>();
            DataTable dt = DAC.getVilles();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildVille.Build(r));
            }
            return lst;
        }

        public static List<Ville> getVilles(string param)
        {
            List<Ville> lst = new List<Ville>();
            foreach (Ville v in villes)
            {
                if ((v.CodePostal.StartsWith(param.ToUpper())) ||
                    (v.NomVille.ToLower().StartsWith(param.ToLower())))
                    lst.Add(v);

            }
            return lst;
        }

        public static void AddVille(Ville v)
        {
            int i = MgmtVilles.isInDb(v);
            if (i == 0)
            {
                DAC.addVille(v);
                villes.Add(v);
            }
        }

        private static int isInDb(Ville v)
        {
            //DataRow r = DAC.isInDb(v);
            
            String array = BasCommon_DAL.DAC.getMethodeJsonString("/villes/"+v.NomVille+"&"+v.CodePostal);

            return Convert.ToInt32(array).ToString() == "" ? -1 : Convert.ToInt32(array);
        }

        private static int isInDbOLD(Ville v)
        {
            DataRow r = DAC.isInDb(v);

            return Convert.ToInt32(r["Nb"]);
        }
    }
}
