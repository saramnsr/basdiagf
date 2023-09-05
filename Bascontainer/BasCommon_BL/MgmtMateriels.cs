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
    public static class MaterielsMgmt
    {
       // public static Boolean Afficher_Familles = false;
        private static List<FamillesMateriels > _famillesmateriel;
        public static List<FamillesMateriels  > famillesmateriel
        {
            get
            {
                if (_famillesmateriel == null)
                    _famillesmateriel = GetFamillesMateriels();
              

                return _famillesmateriel;
            }
            set
            {
                _famillesmateriel = value;
            }
        }

        private static List<Materiel> _Materiels = null;
        public static List<Materiel> Materiels
        {
            get
            {
                if (_Materiels == null) 
                _Materiels = GetMateriels();
                return _Materiels;
            }
            set
            {
                _Materiels = value;
            }
        }


        public static List<Materiel> GetOrdredMaterielByFamilleId(int idFamille) {

            string methode = "/getOrdredListMatByIdFamille/"+idFamille;
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(methode);
            List<Materiel> liste = new List<Materiel>();

            foreach (JObject obj in jArray) {
                liste.Add(Builders.BuildMateriel.BuildJ(obj));
            }
            return liste;
        }
                
        public static List<Materiel> GetMateriels()
        {
            JArray json = DAC.getMethodeJsonArray("/Materiels");
            List<Materiel> lst = new List<Materiel>();

            foreach (JObject r in json)
                lst.Add(Builders.BuildMateriel.BuildJ(r));


            foreach (Materiel a in lst)
                foreach (FamillesMateriels pf in famillesmateriel)
                    if (a.id_famille == pf.Id)
                        a.famille_Materiel = pf;

            return lst;
        }
        public static List<Materiel> GetMaterielsOLD()
        {
            List<Materiel> lst = new List<Materiel>();
            DataTable dt = DAC.getMateriels();

            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildMateriel.Build(r));


            foreach (Materiel a in lst)
                foreach (FamillesMateriels pf in famillesmateriel)
                    if (a.id_famille == pf.Id)
                        a.famille_Materiel = pf;

            return lst;
        }

        public static FamillesMateriels GetFamilleMaterielByIdMateriel(int idMateriel)
        {
            List<FamillesMateriels> lst = new List<FamillesMateriels>();
            JObject json = DAC.getMethodeJsonObjet("/FamillesMaterielsByIdMateriel/" + idMateriel);
            if (json == null) return null;
            FamillesMateriels TmpFamille = new FamillesMateriels();
            TmpFamille = Builders.BuildFamillesMateriel.BuildJ(json);
            return TmpFamille;
        }

        public static FamillesMateriels GetFamilleMaterielByIdMaterielOLD(int idMateriel)
        {
            List<FamillesMateriels> lst = new List<FamillesMateriels>();
            DataTable dt = DAC.getFamillesMaterielsByIdMateriel(idMateriel);

            FamillesMateriels TmpFamille = new FamillesMateriels();
            foreach (DataRow r in dt.Rows)
            {
                //lst.Add(Builders.BuildFamillesMateriel.Build(r));
                TmpFamille = Builders.BuildFamillesMateriel.Build(r);
            }

            return TmpFamille;
        }
        public static FamillesMateriels GetFamilleMateriel(int idFamille)
        {
            List<FamillesMateriels> lst = new List<FamillesMateriels>();
            DataTable dt = DAC.getFamillesMateriels();
            
            FamillesMateriels TmpFamille = new FamillesMateriels();
            foreach (DataRow r in dt.Rows)
            {
                //lst.Add(Builders.BuildFamillesMateriel.Build(r));
                TmpFamille = Builders.BuildFamillesMateriel.Build(r);
            }
            //foreach (FamillesMateriels pf in lst)
            //    if (pf.Id == idFamille )
            //    {
            //        TmpFamille = pf;
            //        break;
            //    }
            return TmpFamille;
        }

        public static List<FamillesMateriels> GetOrdredFamillesMaterielByParentId(int parentId)
        {
            try
            {
                string method = "/ordreFamillesMaterielByParentId/" + parentId;
                JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);
                List<FamillesMateriels> liste = new List<FamillesMateriels>();

                foreach (JObject obj in jArray)
                {
                    liste.Add(Builders.BuildFamillesMateriel.BuildJ(obj));
                }
                return liste;
            }
            catch (Exception e) {
                return new List<FamillesMateriels>();
            }
        }
        
        
        public static List<FamillesMateriels> GetFamillesMateriels()
        {

            JArray json = DAC.getMethodeJsonArray("/familleMateriels");
            List<FamillesMateriels> lst = new List<FamillesMateriels>();
            foreach (JObject r in json)
                lst.Add(Builders.BuildFamillesMateriel.BuildJ(r));

        

            foreach (FamillesMateriels f in lst)
                foreach (FamillesMateriels pf in lst)
                    if (f.ParentFamillesMaterielId == pf.Id)
                    {
                        if (pf.ChildFamillesMateriel == null) pf.ChildFamillesMateriel = new List<FamillesMateriels>();
                        pf.ChildFamillesMateriel.Add(f);
                        f.parent = pf;
                    }

            return lst;
        }
        public static List<FamillesMateriels> GetFamillesMaterielsOLD()
        {

            List<FamillesMateriels> lst = new List<FamillesMateriels>();
            DataTable dt = DAC.getFamillesMateriels();

            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildFamillesMateriel.Build(r));


            foreach (FamillesMateriels  f in lst)
                foreach (FamillesMateriels pf in lst)
                    if (f.ParentFamillesMaterielId  == pf.Id)
                    {
                        if (pf.ChildFamillesMateriel == null) pf.ChildFamillesMateriel = new List<FamillesMateriels>();
                        pf.ChildFamillesMateriel.Add(f);
                        f.parent = pf;
                    }

            return lst;
        }
        public static bool MaterielIsInFamily(Materiel  materiel, string Famille)
        {
            FamillesMateriels  currentfamilleMateriel = null;
            if (materiel == null) return false;

            foreach (FamillesMateriels  f in famillesmateriel )
            {
                if (f.libelle == Famille)
                {
                    currentfamilleMateriel = f;
                    break;
                }
            }

            if (currentfamilleMateriel == null)
                throw new Exception("La famille du matériel '" + Famille + "' n'existe pas");


            FamillesMateriels fa = materiel.famille_Materiel;
            while (fa != null)
            {
                if (fa == currentfamilleMateriel) return true;
                fa = fa.parent;
            }
            return false;
        }
        public static void AddFamille(FamillesMateriels p_famille)
        {
            DAC.AddFamille(p_famille);
        }
        public static void UpdateFamille(FamillesMateriels p_famille)
        {
            DAC.UpdateFamille(p_famille);
        }

        public static void ReorderFamille(FamillesMateriels p_famille, int NewPos)
        {
            DAC.ReorderFamille(p_famille, NewPos);
        }

        public static void DelFamille(FamillesMateriels p_famille)
        {
            DAC.DelFamille(p_famille);
        }

        public static void UpdateFamilyActe(Materiel p_materiel, FamillesMateriels p_familleMateriel)
        {
            DAC.UpdateFamilyMateriel(p_materiel, p_familleMateriel);
        }

        public static void AddMateriel(Materiel m)
        {
            DAC.AddMateriel(m);
        }

        public static void UpdateMateriel(Materiel p_materiel)
        {
            DAC.UpdateMateriel(p_materiel);
        }

        public static void DeleteMateriel(Materiel p_materiel)
        {
            DAC.DeleteMateriel(p_materiel);
        }

        public static Boolean SearchNameMateriel(string s_Materiel)
        {
            return DAC.SearchNameMateriel(s_Materiel);
        }

        //public static Boolean VerifRdv(Materiel p_materiel)
        //{
        //    return DAC.VerifRdv(p_materiel);
        //}
     

    }
     

      
}
