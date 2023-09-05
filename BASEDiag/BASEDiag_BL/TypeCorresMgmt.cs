using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using BASEDiag_BO;
using BASEDiag_DAL;

namespace BASEDiag_BL
{
    public static class TypeCorresMgmt
    {
        private static List<TypePers> _TypePers;
        public static List<TypePers> TypePers
        {
            get
            {
                if (_TypePers == null)
                    _TypePers = getTypePers();

                return _TypePers;
            }

        }


        public static TypePers getTypeByName(string nom)
        {

            foreach (TypePers tp in TypePers)
            {
                if (tp.Nom.ToUpper() == nom.ToUpper())
                    return tp;

            }
            return null;
        }

        public static TypePers getType(int id)
        {

            foreach (TypePers tp in TypePers)
            {
                if (tp.IdType == id)
                    return tp;

            }
            return null;
        }


        private static List<TypePers> getTypePers()
        {
            DataTable dt = DAC.getAllTypePerss();

            List<TypePers> lst = new List<TypePers>();
            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildTypePers(r));
            }
            return lst;
            ;
        }
    }
}
