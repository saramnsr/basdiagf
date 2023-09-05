using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;

namespace BASEDiag_BL
{
    public static class CategoriesMgmt
    {
        private static List<Category> _Categories;
        public static List<Category> Categories
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
        }
    }
}
