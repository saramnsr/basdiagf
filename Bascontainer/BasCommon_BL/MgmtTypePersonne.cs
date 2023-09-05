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
    public class MgmtTypePersonne
    {
        private static List<TypePersonne> _TypePersonnes = null;
        public static List<TypePersonne> TypePersonnes
        {
            get
            {
                if (_TypePersonnes == null) _TypePersonnes = getTypePersonne();
                return _TypePersonnes;
            }
        }

        public static bool IsCategorieMedical(TypePersonne tpe)
        {
            return tpe.Categorie == "MED";
        }

            public static bool IsCategorieParaMedical(TypePersonne tpe)
        {
            return tpe.Categorie == "PARAMED";
        }

        public static bool IsCategorieAutre(TypePersonne tpe)
        {
            return (!IsCategorieMedical(tpe)) && (!IsCategorieParaMedical(tpe));
        }


        public static TypePersonne GetTypeFromId(int Id)
        {
            foreach (TypePersonne tpe in TypePersonnes)
            {
                if (tpe.Id == Id) return tpe;
            }
            return null;
        }

        public static TypePersonne GetTypeFromName(string type)
        {
            foreach (TypePersonne tpe in TypePersonnes)
            {
                if (tpe.Nom.ToUpper() == type.ToUpper()) return tpe;
            }
            return null;
        }


        private static List<TypePersonne> getTypePersonne()
        {
            List<TypePersonne> lst = new List<TypePersonne>();
            JArray json = DAC.getMethodeJsonArray("/getTypePersonnes");

            foreach (JObject item in json)
            {
                lst.Add(Builders.BuildTypePersonne.BuildJson(item));
            }
            lst.Sort();
            return lst;
        }

        private static List<TypePersonne> getTypePersonneOld()
        {
            List<TypePersonne> lst = new List<TypePersonne>();
            DataTable dt;

            dt = DAC.getTypePersonnes();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildTypePersonne.Build(r));
            }
            lst.Sort();
            return lst;
        }
    }
}
