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
    public static class CategoriesMgmt
    {
        private static List<Categorie> _Categories;
        public static List<Categorie> Categories
        {
            get
            {
                if (_Categories == null) _Categories = GetCategories();
                return _Categories;
            }
            set
            {
                _Categories = value;
            }
        }

        private static List<CustomCategorie> _CustomCategories;
        public static List<CustomCategorie> CustomCategories
        {
            get
            {
                if (_CustomCategories == null) _CustomCategories = GetCustomCategories();
                return _CustomCategories;
            }
            set
            {
                _CustomCategories = value;
            }
        }

        private static List<CustomCategorie> _Notes;
        public static List<CustomCategorie> Notes
        {
            get
            {
                if (_Notes == null) _Notes = GetNotes();
                return _Notes;
            }
            set
            {
                _Notes = value;
            }
        }

        /*
        private static List<Category> GetCategories()
        {
            List<Category> lst = new List<Category>();
            DataTable dt;

            dt = DAC.getCategories();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildCategory(r));
            }
            return lst;
        }


        public static Category FromName(string name)
        {
            foreach (Category cat in Categories)
            {
                if (cat.Nom == name) return cat;
            }
            return null;
        }*/

        private static List<Categorie> GetCategories()
        {
            List<Categorie> lst = new List<Categorie>();
            DataTable dt;

            dt = DAC.getCategories();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildCategories.Build(r));
            }
            return lst;
        }



        public static List<CustomCategorie> GetCurrentCategoriesFromIdPersonne(int IdPersonne)
        {
            List<CustomCategorie> lst = new List<CustomCategorie>();
            JArray json = DAC.getMethodeJsonArray("/CurrentCustomCategories/" + IdPersonne);


            foreach (JObject r in json)
            {
                lst.Add(Builders.BuildCustomCategorie.BuildJ(r));
            }
            return lst;
        }
        public static List<CustomCategorie> GetCurrentCategoriesFromIdPersonneOLD(int IdPersonne)
        {
            List<CustomCategorie> lst = new List<CustomCategorie>();
            DataTable dt;

            dt = DAC.getCurrentCustomCategories(IdPersonne);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildCustomCategorie.Build(r));
            }
            return lst;
        }
        private static List<CustomCategorie> GetCustomCategories()
        {
            List<CustomCategorie> lst = new List<CustomCategorie>();
            DataTable dt;

            dt = DAC.getCustomCategories();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildCustomCategorie.Build(r));
            }
            return lst;
        }

        private static List<CustomCategorie> GetNotes()
        {
            List<CustomCategorie> lst = new List<CustomCategorie>();
            DataTable dt;

            dt = DAC.getNotes();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildCustomCategorie.Build(r));
            }
            return lst;
        }

        public static List<CustomCategorie> GetNotesFromIdPersonne(int id)
        {
            List<CustomCategorie> lst = new List<CustomCategorie>();
            DataTable dt;

            dt = DAC.getNotesFromIdPersonne(id);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildCustomCategorie.Build(r));
            }
            return lst;
        }

        public static List<CustomCategorie> GetCategoriesFromIdPersonne(int id)
        {
            List<CustomCategorie> lst = new List<CustomCategorie>();
            DataTable dt;

            dt = DAC.getCategoriesFromIdPersonne(id);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildCustomCategorie.Build(r));
            }
            return lst;
        }

        public static void UpdateCategorieBeToWas(CustomCategorie custo)
        {
            DAC.updateCategorieBeToWas(custo);
        }

        public static void UpdateCategorieWasToBe(CustomCategorie custo)
        {
            DAC.updateCategorieWasToBe(custo);
        }

        public static void InsertCategorieToBe(int Idcorres, Categorie cat)
        {
            DAC.InsertCategorieToBe(Idcorres, cat);
        }

        public static void AddCategorie(Categorie cat)
        {
            DAC.InsertCategorie( cat);
            Categories.Add(cat);
        }

        public static void DeleteCategorie(Categorie cat)
        {
            DAC.DeleteCategorie(cat);
            Categories.Remove(cat);
        }
    }
}
