using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;

namespace BASEDiag_BL
{
    public static class TypeDevisMgmt
    {

            private static List<TypeDevis> _lstTypeDevis;
            public static List<TypeDevis> lstTypeDevis
            {
                get
                {
                    if (_lstTypeDevis == null)
                        _lstTypeDevis = getTypeDevis();

                    return _lstTypeDevis;
                }

            }

            private static List<TypeDevis> getTypeDevis()
            {
                DataTable dt = DAC.getTypeDevis();

                List<TypeDevis> lst = new List<TypeDevis>();
                foreach (DataRow r in dt.Rows)
                {
                    lst.Add(Builders.BuildTypeDevis(r));
                }
                return lst;
            }

            public static TypeDevis getTypeDevis(int Id)
            {
                foreach (TypeDevis f in lstTypeDevis)
                    if (f.Id == Id) return f;

                return null;

            }

            public static TypeDevis getTypeDevis(string Name)
            {
                foreach (TypeDevis f in lstTypeDevis)
                    if (f.libelle == Name) return f;

                return null;

            }
            public static TypeDevis getTypeDevis(TypeDevis.CategorieDevis categorie)
            {
                foreach (TypeDevis f in lstTypeDevis)
                    if (f.Categorie == categorie) return f;

                return null;

            }


        
    }
}
