using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_DAL;

namespace BasCommon_BL
{
    public static class ActesMgmt
    {


        private static List<FamillesActe> _famillesacte;
        public static List<FamillesActe> famillesacte
        {
            get
            {
                if (_famillesacte == null) _famillesacte = GetFamillesActe();
                return _famillesacte;
            }
        }

        private static List<Acte> _Actes = null;
        public static List<Acte> Actes
        {
            get
            {
                if (_Actes == null) _Actes = GetActes();
                return _Actes;
            }
        }

        public static Acte ActeRadio
        {
            get
            {
                foreach (Acte a in Actes)
                {
                    if (a.code_planing == "radio")
                    {
                        return a;
                    }
                };

                return null;
            }

        }


        public static Acte getActe(int id)
        {
            foreach (Acte a in Actes)
            {
                if (a.id_acte == id)
                {
                    return a;
                }
            }
            return null;
        }

        public static Acte getActe(string name)
        {
            foreach (Acte a in Actes)
            {
                if (a.acte_libelle == name)
                {
                    return a;
                }
            }
            return null;
        }

        public static FamillesActe GetFamille(string famille)
        {

            foreach (FamillesActe fa in famillesacte)
            {
                if (fa.libelle == famille) return fa;
            }
            return null;
        }

        private static List<Acte> GetActes()
        {
            List<Acte> lst = new List<Acte>();
            DataTable dt = DAC.getActes();

            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildActe.Build(r));


            foreach (Acte a in lst)
                foreach (FamillesActe pf in famillesacte)
                    if (a.id_famille == pf.Id)
                        a.famille_Acte = pf;

            return lst;
        }

        private static List<FamillesActe> GetFamillesActe()
        {

            List<FamillesActe> lst = new List<FamillesActe>();
            DataTable dt = DAC.getFamillesActes();

            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildFamillesActe.Build(r));


            foreach (FamillesActe f in lst)
                foreach (FamillesActe pf in lst)
                    if (f.ParentFamillesActeId == pf.Id)
                    {
                        pf.ChildFamillesActe.Add(f);
                        f.parent = pf;
                    }

            return lst;
        }

        public static bool ActeIsInFamily(Acte acte, string Famille)
        {
            FamillesActe currentfamilleActe = null;
            if (acte == null) return false;

            foreach (FamillesActe f in famillesacte)
            {
                if (f.libelle == Famille)
                {
                    currentfamilleActe = f;
                    break;
                }
            }

            if (currentfamilleActe == null)
                throw new Exception("La famille d'acte '" + Famille + "' n'existe pas");


            FamillesActe fa = acte.famille_Acte;
            while (fa != null)
            {
                if (fa == currentfamilleActe) return true;
                fa = fa.parent;
            }
            return false;


        }

        public static void AddFamille(FamillesActe p_famille)
        {
            DAC.AddFamille(p_famille);
        }

        public static void UpdateFamille(FamillesActe p_famille)
        {
            DAC.UpdateFamille(p_famille);
        }

        public static void ReorderFamille(FamillesActe p_famille, int NewPos)
        {
            DAC.ReorderFamille(p_famille, NewPos);
        }

        public static void DelFamille(FamillesActe p_famille)
        {
            DAC.DelFamille(p_famille);
        }

        public static void UpdateFamilyActe(Acte p_acte, FamillesActe p_familleActe)
        {
            DAC.UpdateFamilyActe(p_acte, p_familleActe);
        }

        public static void AddActe(Acte a)
        {
            DAC.AddActe(a);
        }

        public static void UpdateActe(Acte p_acte)
        {
            DAC.UpdateActe(p_acte);
        }

        public static void DeleteActe(Acte p_acte)
        {
            DAC.DeleteActe(p_acte);
        }

        public static Boolean SearchNameActe(string s_Acte)
        {
            return DAC.SearchNameActe(s_Acte);
        }

        public static Boolean VerifRdv(Acte p_acte)
        {
            return DAC.VerifRdv(p_acte);
        }
    }
}
