using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_DAL;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json.Linq;


namespace BasCommon_BL
{
    public static class ActesMgmt
    {
        
       // public static    Boolean Afficher_Familles = false;
        private static List<FamillesActe> _famillesacte;
        public static List<FamillesActe> famillesacte
        {
            get
            {
              if (_famillesacte == null) 
                    _famillesacte = GetFamillesActe();
               //else if (Afficher_Familles )
               //  _famillesacte = GetFamillesActe();

                return _famillesacte;
            }
            set
            {
                _famillesacte = value;
            }
        }
        private static List<FamillesActe> _famillesactegrp;
        public static List<FamillesActe> famillesactegrp
        {
            get
            {
                if (_famillesactegrp == null)
                    _famillesactegrp = GetFamillesActeGrp();
                //else if (Afficher_Familles )
                //  _famillesacte = GetFamillesActe();

                return _famillesactegrp;
            }
            set
            {
                _famillesactegrp = value;
            }
        }
        private static List<ActeGroupement> _ActesGroupement = null;
        public static List<ActeGroupement> ActesGroupement
        {
            get
            {
                if (_ActesGroupement == null)
                    _ActesGroupement = GetActesGroupement();
                return _ActesGroupement;
            }
            set
            {
                _ActesGroupement = value;
            }
        }
        private static List<Acte> _Actes = null;
        public static List<Acte> Actes
        {
            get
            {
             if (_Actes == null)
                    _Actes = GetActes();
              //else if (Afficher_Familles)
              //    _Actes = GetActes();
                return _Actes;
            }
            set
            {
                _Actes  = value;
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



        public static Acte ActeInvisalignStart
        {
            get
            {
                foreach (Acte a in Actes)
                {
                    if (a.code_planing == "Debutinvi")
                    {
                        return a;
                    }
                };
               

                return null;
            }

        }

        public static Acte ActeInvisalignIntermed
        {
            get
            {
                foreach (Acte a in Actes)
                {
                    if (a.code_planing == "actINV")
                    {
                        return a;
                    }
                };

                return null;
            }

        }

        public static Acte ActeInvisalignEnd
        {
            get
            {
                foreach (Acte a in Actes)
                {
                    if (a.code_planing == "Fininvi")
                    {
                        return a;
                    }
                };

                return null;
            }

        }

      
     
        public static Acte getActe(int id)
        {
            foreach (Acte a in Actes )
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
        public static List<ActeGroupement> getActesGroupemnt()
        {
            List<ActeGroupement> lst = new List<ActeGroupement>();
            ActeGroupement actg = null;
            foreach (Acte ac in Actes)
            {
                actg = new ActeGroupement(ac);
                actg.actes = GetActesGroupementByIdParent(actg.id_acte);
                lst.Add(actg);
            }
            return lst;
        }
        public static List<ActeGroupement> GetActesGroupementByIdParent(int idActe)
        {
            List<ActeGroupement> lst = new List<ActeGroupement>();
            DataTable dt = DAC.getActesGroupementByIdParent(idActe);

            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildActe.BuildActeGroupement(r));

            return lst;
        }


        public static List<ActeGroupement> GetActesGroupement() {

            string method = "/AllActeGrpOld";
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);
            List<ActeGroupement> liste = new List<ActeGroupement>();

            foreach (JObject obj in jArray) 
                liste.Add(Builders.BuildActe.BuildActeGroupement(obj));
            
            foreach (ActeGroupement a in liste)
                foreach (FamillesActe pf in _famillesactegrp)
                    if (a.idParent == pf.Id)
                        a.famille_Acte = pf;
            
            return liste;
        }
        
        public static List<ActeGroupement> GetActesGroupementOld()
        {
            List<ActeGroupement> lst = new List<ActeGroupement>();
            DataTable dt = DAC.getActesGroupement();

            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildActe.BuildActeGroupement(r));
         
            foreach (ActeGroupement a in lst)

                foreach (FamillesActe pf in famillesactegrp)

                    if (a.idParent == pf.Id)
                        a.famille_Acte = pf;
            return lst;
        }
        public static FamillesActe GetFamille(string famille)
        {

            foreach (FamillesActe fa in famillesacte)
            {
                if (fa.libelle == famille) return fa;
            }
            return null;
        }
        public static List<Acte> GetOrdredActesByFamille(int idFamille)
        {
            List<Acte> lst = new List<Acte>();

            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/ordredActesAndByFamille/"+idFamille);

            foreach (JObject j in json)
            {
                Acte ac = Builders.BuildActe.BuildActeJ(j);
                lst.Add(ac);

            }
            foreach (Acte a in lst)
                foreach (FamillesActe pf in famillesacte)

                    if (a.id_famille == pf.Id)
                        a.famille_Acte = pf;
            return lst;
        }

        public static List<Acte> GetActes()
        {

             JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/Actes");            List<Acte> lst = new List<Acte>();


             foreach (JObject j in json)
             {
                 Acte ac = Builders.BuildActe.BuildActeJ(j);
                 lst.Add(ac);
                
             }
             foreach (Acte a in lst)
                 foreach (FamillesActe pf in famillesacte)

                     if (a.id_famille == pf.Id)
                         a.famille_Acte = pf;
            return lst;
        }


        public static List<Acte> GetActesOld()
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
        public static List<FamillesActe> GetOrdredFamillesActeByparentId(int parent) {

            string method = "/ordredfamilleActeAndByParent/"+parent;
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);
            List<FamillesActe> liste = new List<FamillesActe>();

            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildFamillesActe.BuildFamilleActeJ(obj));
            return liste;
        }

        public static List<FamillesActe> GetFamillesActe()
        {

            List<FamillesActe> lst = new List<FamillesActe>();

            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/familleActes");

            foreach (JObject j in json)
            {
                FamillesActe ac = Builders.BuildFamillesActe.BuildFamilleActeJ(j);
                lst.Add(ac);

            }


            foreach (FamillesActe f in lst)
                foreach (FamillesActe pf in lst)
                    if (f.ParentFamillesActeId == pf.Id)
                    {
                        if (pf.ChildFamillesActe == null) pf.ChildFamillesActe = new List<FamillesActe>();
                        pf.ChildFamillesActe.Add(f);
                        f.parent = pf;
                    }

            return lst;
        }
        public static List<FamillesActe> GetFamillesActeOld()
        {

            List<FamillesActe> lst = new List<FamillesActe>();
            DataTable dt = DAC.getFamillesActes();

            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildFamillesActe.Build(r));


            foreach (FamillesActe f in lst)
                foreach (FamillesActe pf in lst)
                    if (f.ParentFamillesActeId == pf.Id)
                    {
                        if (pf.ChildFamillesActe == null) pf.ChildFamillesActe = new List<FamillesActe>();
                        pf.ChildFamillesActe.Add(f);
                        f.parent = pf;
                    }

            return lst;
        }
        public static List<FamillesActe> GetFamilleActeGrpByParent(int parentId) 
        {
            List<FamillesActe> liste = new List<FamillesActe>();
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray("/familleActGrpByParentId/" + parentId);

            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildFamillesActe.Build(obj));

            foreach (FamillesActe f in liste)
                foreach (FamillesActe pf in liste)
                    if (f.ParentFamillesActeId == pf.Id)
                    {
                        if (pf.ChildFamillesActe == null) pf.ChildFamillesActe = new List<FamillesActe>();
                        pf.ChildFamillesActe.Add(f);
                        f.parent = pf;
                    }

            return liste;
        }

        public static List<FamillesActe> GetFamillesActeGrp()
        {
            List<FamillesActe> liste = new List<FamillesActe>();
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray("/allFamillesActeGrp");

            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildFamillesActe.Build(obj));

            foreach (FamillesActe f in liste)
                foreach (FamillesActe pf in liste)
                    if (f.ParentFamillesActeId == pf.Id)
                    {
                        if (pf.ChildFamillesActe == null) pf.ChildFamillesActe = new List<FamillesActe>();
                        pf.ChildFamillesActe.Add(f);
                        f.parent = pf;
                    }

            return liste;
            
        
        }

        public static List<FamillesActe> GetFamillesActeGrpOld()
        {            
            // à faire en Java c pas dure 
            List<FamillesActe> lst = new List<FamillesActe>();
            DataTable dt = DAC.getFamillesActesGrp();

            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildFamillesActe.Build(r));


            foreach (FamillesActe f in lst)
                foreach (FamillesActe pf in lst)
                    if (f.ParentFamillesActeId == pf.Id)
                    {
                        if (pf.ChildFamillesActe == null) pf.ChildFamillesActe = new List<FamillesActe>();
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
        public static void UpdateFamilleGrp(FamillesActe p_famille)
        {
            DAC.UpdateFamilleGrp(p_famille);
        }
        public static void ReorderFamille(FamillesActe p_famille, int NewPos)
        {
            DAC.ReorderFamille(p_famille, NewPos);
        }

        public static void DelFamille(FamillesActe p_famille)
        {
            DAC.DelFamille(p_famille);
        }
        public static void DelFamilleGroupement(FamillesActe p_famille)
        {
            DAC.DelFamilleGroupement(p_famille);
        }
        public static void UpdateFamilyActe(Acte p_acte, FamillesActe p_familleActe)
        {
            DAC.UpdateFamilyActe(p_acte, p_familleActe);
        }

        public static void AddActe(Acte a)
        {
            DAC.AddActe(a);
        }
        public static void AddActeGroupement(List<ActeGroupement> lst)
        {
            DAC.AddActeGroupement(lst);
        }
        public static void UpdateActe(Acte p_acte)
        {
            DAC.UpdateActe(p_acte);
        }
        public static void DeleteActeGroupement(int id_acte)
        {
            DAC.DeleteActeGroupement(id_acte);
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
