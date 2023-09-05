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
    public static class TraitementsMgmt
    {
      
        private static List<FamillesTraitements> _famillesTraitement;
        public static List<FamillesTraitements> famillesTraitement
        {
            get
            {
                if (_famillesTraitement == null) 
                    _famillesTraitement = GetFamillesTraitements();
            
                return _famillesTraitement;
            }
            set
            {
                _famillesTraitement = value;
            }
        }

        private static List<NewTraitement> _Traitements = null;
        public static List<NewTraitement> Traitements
        {
            get
            {
                if (_Traitements == null)
                    _Traitements = GetTraitements();
                return _Traitements;
            }
            set
            {
                _Traitements = value;
            }
        }
        public static List<NewTraitement> GetTraitementsOld()
        {
            List<NewTraitement> lst = new List<NewTraitement>();
            DataTable dt = DAC.getTraitements();

            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildNewTraitement.Build(r));


            foreach (NewTraitement a in lst)
                foreach (FamillesTraitements pf in famillesTraitement)
                    if (a.id_famille == pf.Id)
                        a.famille_Traitement = pf;

            return lst;
        }

        public static List<NewTraitement> getTraitmentsByFamille(int p)
        {
            List<NewTraitement> lst = new List<NewTraitement>();

            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/ordredTraitemensByFamille/"+p);

            foreach (JObject j in json)
            {
                NewTraitement newTraitement = Builders.BuildNewTraitement.BuildJ(j);
                lst.Add(newTraitement);
            }
            foreach (NewTraitement a in lst)
                foreach (FamillesTraitements pf in famillesTraitement)
                    if (a.id_famille == pf.Id)
                        a.famille_Traitement = pf;

            return lst;
        }
        public static List<NewTraitement> GetTraitements()
        {
            List<NewTraitement> lst = new List<NewTraitement>();

            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/GetTraitements");

            foreach (JObject j in json)
            {
                NewTraitement newTraitement = Builders.BuildNewTraitement.BuildJ(j);
                lst.Add(newTraitement);
            }
            foreach (NewTraitement a in lst)
                foreach (FamillesTraitements pf in famillesTraitement)
                    if (a.id_famille == pf.Id)
                        a.famille_Traitement = pf;

            return lst;
        }
        public static List<FamillesTraitements> GetFamillesTraitements()
        {

            List<FamillesTraitements> lst = new List<FamillesTraitements>();

            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/familleTraitements");

            foreach (JObject j in json)
            {
                FamillesTraitements ac = Builders.BuildFamillesTraitement.BuildJ(j);
                lst.Add(ac);

            }
            foreach (FamillesTraitements f in lst)
                foreach (FamillesTraitements pf in lst)
                    if (f.ParentFamillesTraitementId == pf.Id)
                    {
                        if (pf.ChildFamillesTraitement == null) pf.ChildFamillesTraitement = new List<FamillesTraitements>();
                        pf.ChildFamillesTraitement.Add(f);
                        f.parent = pf;
                    }

            return lst;
        }
        public static List<FamillesTraitements> GetFamillesTraitementsOLD()
        {

            List<FamillesTraitements> lst = new List<FamillesTraitements>();
            DataTable dt = DAC.getFamillesTraitements();

            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildFamillesTraitement.Build(r));


            foreach (FamillesTraitements f in lst)
                foreach (FamillesTraitements pf in lst)
                    if (f.ParentFamillesTraitementId == pf.Id)
                    {
                        if (pf.ChildFamillesTraitement == null) pf.ChildFamillesTraitement = new List<FamillesTraitements>();
                        pf.ChildFamillesTraitement.Add(f);
                        f.parent = pf;
                    }

            return lst;
        }
        public static double GetPrixComAvantRemise(CommTraitement com)
        {
            double PrixCom = 0;
            PrixCom = PrixCom + com.Acte.prix_acte;
            foreach (CommActesTraitement cc in com.ActesSupp)
            {
                PrixCom = PrixCom + cc.prix_acte;
            }
            foreach (CommActesTraitement cc in com.Radios)
            {
                PrixCom = PrixCom + cc.prix_acte;
            }
            foreach (CommActesTraitement cc in com.photos)
            {
                PrixCom = PrixCom + cc.prix_acte;
            }
            foreach (CommMaterielTraitement cc in com.Materiels)
            {
                PrixCom = PrixCom + cc.prix_materiel ;
            }
            return PrixCom;
        }
        public static double GetPrixCom(CommTraitement com)
        {
            double PrixCom = 0;
            PrixCom = PrixCom + (com.Acte.prix_traitement * double.Parse ( com.Acte.quantite))  ;
            foreach (CommActesTraitement cc in com.ActesSupp)
            {
                PrixCom = PrixCom + (cc.prix_traitement *  cc.Qte  );
            }
            foreach (CommActesTraitement cc in com.Radios)
            {
                PrixCom = PrixCom + (cc.prix_traitement * cc.Qte);
            }
            foreach (CommActesTraitement cc in com.photos)
            {
                PrixCom = PrixCom + (cc.prix_traitement * cc.Qte);
            }
            foreach (CommMaterielTraitement cc in com.Materiels)
            {
                if (cc.Famille != null)
                if (cc.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0 || cc.Famille.libelle.ToLower().IndexOf("stérilisation") >= 0 ||  cc.Famille.libelle.ToLower().IndexOf("achats") >= 0) 
                PrixCom = PrixCom + (cc.prix_traitement * cc.Qte);
            }
            return PrixCom;
        }
        private static double getPercent(Double prixActe, Double part,Double prixTraitement)
        {
            double percent=0;
            if(prixActe > 0)
                percent = (((part / prixActe) * 100.0) * prixTraitement) / 100.0;
            return percent;
        }

        public static void getRepartitionToulon(CommClinique ct, ref double partsecu, ref double parPatient)
        {


            if (ct.prix_traitement > 0)
            {
                double t = getPercent(ct.Acte.prix_acte, parPatient, ct.prix_traitement);
                partsecu += getPercent(ct.Acte.prix_acte, ct.Acte.Remboursement, ct.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
              //  partmutuelle += getPercent(ct.Acte.prix_acte, ct.Acte.Remboursement, ct.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
                parPatient += getPercent(ct.Acte.prix_acte, ct.Acte.prix_acte, ct.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
            }
            //foreach (CommActes cm in ct.ActesSupp)
            //{
            //    if (cm.prix_traitement > 0)
            //    {
            //        partsecu += getPercent(cm.prix_acte, cm.Remboursement, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
            //        partmutuelle += getPercent(cm.prix_acte, cm.RembMutuelle, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
            //        parPatient += getPercent(cm.prix_acte, cm.partPatient, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
            //    }

            //    //   partsecu += cm.Remboursement * (double)cm.Qte;
            //    //   partmutuelle += cm.RembMutuelle * (double)cm.Qte;
            //    //  parPatient += cm.partPatient * (double)cm.Qte;
            //}
            //foreach (CommActes cm in ct.photos)
            //{
            //    if (cm.prix_traitement > 0)
            //    {
            //        partsecu += getPercent(cm.prix_acte, cm.Remboursement, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
            //        partmutuelle += getPercent(cm.prix_acte, cm.RembMutuelle, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
            //        parPatient += getPercent(cm.prix_acte, cm.partPatient, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
            //    }
            //    //partsecu += cm.Remboursement * cm.Qte;
            //    //partmutuelle += cm.RembMutuelle * cm.Qte;
            //    //parPatient += cm.partPatient * cm.Qte;
            //}
            //foreach (CommActes cm in ct.Radios)
            //{
            //    if (cm.prix_traitement > 0)
            //    {
            //        partsecu += getPercent(cm.prix_acte, cm.Remboursement, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
            //        partmutuelle += getPercent(cm.prix_acte, cm.RembMutuelle, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
            //        parPatient += getPercent(cm.prix_acte, cm.partPatient, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
            //    }
            //    //partsecu += cm.Remboursement * cm.Qte;
            //    //partmutuelle += cm.RembMutuelle * cm.Qte;
            //    //parPatient += cm.partPatient * cm.Qte;
            //}
            //foreach (CommMateriel cm in ct.Materiels)
            //{
            //    parPatient += cm.prix_materiel_traitement * cm.Qte;
            //}
        }

        public static void getMontantEcheToulon(CommTraitement ct, ref double partsecu,ref double partmutuelle, ref double parPatient )
        {


            if (ct.Acte.prix_traitement > 0)
            {
                double t = getPercent(ct.Acte.prix_acte, parPatient, ct.Acte.prix_traitement);
                partsecu += getPercent(ct.Acte.prix_acte, ct.Acte.Remboursement, ct.Acte.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
                partmutuelle += getPercent(ct.Acte.prix_acte, ct.RembMutuelle, ct.Acte.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
                parPatient += getPercent(ct.Acte.prix_acte, ct.partPatient, ct.Acte.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
            }
            foreach (CommActes cm in ct.ActesSupp)
            {
                if (cm.prix_traitement > 0)
                {
                    partsecu += getPercent(cm.prix_acte, cm.Remboursement, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
                    partmutuelle += getPercent(cm.prix_acte, cm.RembMutuelle, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
                    parPatient += getPercent(cm.prix_acte, cm.partPatient, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
                }

             //   partsecu += cm.Remboursement * (double)cm.Qte;
             //   partmutuelle += cm.RembMutuelle * (double)cm.Qte;
              //  parPatient += cm.partPatient * (double)cm.Qte;
            }
            foreach (CommActes cm in ct.photos)
            {
                if (cm.prix_traitement > 0)
                {
                    partsecu += getPercent(cm.prix_acte, cm.Remboursement, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
                    partmutuelle += getPercent(cm.prix_acte, cm.RembMutuelle, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
                    parPatient += getPercent(cm.prix_acte, cm.partPatient, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
                }
                //partsecu += cm.Remboursement * cm.Qte;
                //partmutuelle += cm.RembMutuelle * cm.Qte;
                //parPatient += cm.partPatient * cm.Qte;
            }
            foreach (CommActes cm in ct.Radios)
            {
                if (cm.prix_traitement > 0)
                {
                    partsecu += getPercent(cm.prix_acte, cm.Remboursement, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
                    partmutuelle += getPercent(cm.prix_acte, cm.RembMutuelle, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
                    parPatient += getPercent(cm.prix_acte, cm.partPatient, cm.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
                }
                //partsecu += cm.Remboursement * cm.Qte;
                //partmutuelle += cm.RembMutuelle * cm.Qte;
                //parPatient += cm.partPatient * cm.Qte;
            }
            foreach (CommMateriel cm in ct.Materiels)
            {
                parPatient += cm.prix_materiel_traitement * cm.Qte;
            }
        }
        public static double getPrixPatient(CommTraitement com)
        {
            double prixCom = 0;
            return prixCom;
        }
        public static double getPrixSecurite(CommTraitement com)
        {
            double prixCom = 0;
            return prixCom;
        }
        public static bool TraitementIsInFamily(NewTraitement Traitement, string Famille)
        {
            FamillesTraitements currentfamilleTraitement = null;
            if (Traitement == null) return false;

            foreach (FamillesTraitements f in famillesTraitement)
            {
                if (f.libelle == Famille)
                {
                    currentfamilleTraitement = f;
                    break;
                }
            }

            if (currentfamilleTraitement == null)
                throw new Exception("La famille du matériel '" + Famille + "' n'existe pas");


            FamillesTraitements fa = Traitement.famille_Traitement;
            while (fa != null)
            {
                if (fa == currentfamilleTraitement) return true;
                fa = fa.parent;
            }
            return false;
        }
        public static void AddFamille(FamillesTraitements p_famille)
        {
            DAC.AddFamille(p_famille);
        }
        public static void UpdateFamille(FamillesTraitements p_famille)
        {
            DAC.UpdateFamille(p_famille);
        }
        public static void UpdateTitreDevis(string  TitreDevis, int idTraitement)
        {
            DAC.UpdateTitreDevis(TitreDevis, idTraitement);
        }

        public static void ReorderFamille(FamillesTraitements p_famille, int NewPos)
        {
            DAC.ReorderFamille(p_famille, NewPos);
        }

        public static void DelFamille(FamillesTraitements p_famille)
        {
            DAC.DelFamille(p_famille);
        }

        //public static void UpdateFamilyTraitement(NewTraitement p_Traitement, FamillesTraitements p_familleTraitement)
        //{
        //    DAC.UpdateFamilyTraitement(p_Traitement, p_familleTraitement);
        //}

        public static void AddTraitement(NewTraitement m)
        {
            DAC.AddTraitement(m);
        }

        public static void UpdateTraitement(NewTraitement p_Traitement)
        {
            DAC.UpdateTraitement(p_Traitement);
        }

        public static void DeleteTraitement(NewTraitement p_Traitement)
        {
            DAC.DeleteTraitement(p_Traitement);
        }

        public static Boolean SearchNameTraitement(string s_Traitement)
        {
            return DAC.SearchNameTraitement(s_Traitement);
        }

        public static void DelActeTraitement(CommTraitement com)
        {
            DAC.DeleteActeTraitement(com);
        }

        //public static Boolean VerifRdv(Traitement p_Traitement)
        //{
        //    return DAC.VerifRdv(p_Traitement);
        //}

        public static void AddCommTraitements(int id_traitement, CommTraitement p_com)
        {
              DAC.AddActeTraitement(id_traitement,p_com);
        }
        public static List<CommMaterielTraitement> GetCommMaterielsTraitements(CommTraitement com)
        {
            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/GetCommMatsTraitements/" + com.Id);

            // DataTable dt = DAC.getActesSupTraitements(comTraitement.Id , TYPE_ACTE_SUPP );

            List<CommMaterielTraitement> lst = new List<CommMaterielTraitement>();

            foreach (JObject j in json)
            {

                CommMaterielTraitement c = Builders.BuildNewTraitement.BuildTraitementMaterielJ(j);
                lst.Add(c);
            }

            //DataTable dt = DAC.GetCommTraitementMateriels(com);
            //List<CommMaterielTraitement> lst = new List<CommMaterielTraitement>();

            //foreach (DataRow r in dt.Rows)
            //{
            //    CommMaterielTraitement c = Builders.BuildNewTraitement.BuildTraitementMateriel(r);
            //    //c.Parent = com;
            //    lst.Add(c);
            //}
            return lst;
        }

        public static void SaveActesSupp(CommTraitement com, string TYPE_ACTE_SUPP = "")
        {
            DAC.SaveActesSuppTraitement(com, TYPE_ACTE_SUPP);

        }

        public static void SavePrixCom(CommTraitement  com)
        {
            DAC.setTraitementPrix(com);

        }
        public static void SaveMateriels(CommTraitement com)
        {
            DAC.setTraitementMateriels(com);

        }

        public static void SaveAutrePersonne(CommTraitement com)
        {
            DAC.setTraitementAutrePersonnes(com);

        }


        public static void UpdateActeTraitement(CommTraitement com)
        {

            DAC.UpdateActeTraitement(com);
        }


        public static List<CommAutrePersonne> GetTraitementAutrePersonne(CommTraitement com)
        {
            DataTable dt = DAC.GetTraitementAutrePersonne(com);
            List<CommAutrePersonne> lst = new List<CommAutrePersonne>();

            foreach (DataRow r in dt.Rows)
            {
                CommAutrePersonne c = Builders.BuildNewTraitement.BuildTraitementAutrePersonne(r);
               // c.Parent = com;
                lst.Add(c);
            }
            return lst;
        }
        public static List<CommActesTraitement> GetCommActeSupTraitements(CommTraitement comTraitement, string TYPE_ACTE_SUPP = "")
        {
            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/GetCommActeSupTraitements/" + comTraitement.Id + "?type=" + TYPE_ACTE_SUPP);

           // DataTable dt = DAC.getActesSupTraitements(comTraitement.Id , TYPE_ACTE_SUPP );

            List<CommActesTraitement> lst = new List<CommActesTraitement>();

            foreach (JObject j in json)
            {

                CommActesTraitement c = Builders.BuildNewTraitement.BuildCommActeSuppTraitementJ(j);
                lst.Add(c);
            }
          
            //foreach (DataRow dr in dt.Rows)
            //{

            //    CommActesTraitement  c = Builders.BuildNewTraitement.BuildCommActeSuppTraitement(dr);
            //    lst.Add(c);
            //  }
          
          
            return lst;
        }

        public static NewTraitement GetFullTraitement(int id)
        {
           JObject json =  DAC.getMethodeJsonObjet("/GetFullTraitement/" + id);
           NewTraitement NT = new NewTraitement();
           // DataTable dt = DAC.getTraitement(id);
           if (json != null && json.ToString() != "")  
            NT = Builders.BuildNewTraitement.BuildJ(json);
           // if (dt.Rows.Count > 0)
            //    NT = Builders.BuildNewTraitement.Build(dt.Rows[0]);
            return NT;

        }
        public static NewTraitement GetFullTraitementOld(int id)
        {


            DataTable dt = DAC.getTraitement(id);
            NewTraitement NT = new NewTraitement();

            if (dt.Rows.Count > 0)
                NT = Builders.BuildNewTraitement.Build(dt.Rows[0]);
            return NT;
          
        }
        public static void GetCommTraitementsOld(ref NewTraitement traitement)
        {
           
            DataTable dt = DAC.getActesTraitements(traitement.id_Traitement);

            List<CommActesTraitement> lst = new List<CommActesTraitement>();

            traitement.CommTraitement = new List<CommTraitement>();
            foreach (DataRow dr in dt.Rows)
            {

                CommTraitement c = Builders.BuildNewTraitement.BuildCommTraitement(dr);

                traitement.CommTraitement.Add(c);
            }


        }
        public static void  GetCommTraitements(ref NewTraitement traitement)
        {
            List<CommActesTraitement> lst = new List<CommActesTraitement>();
            JArray json = DAC.getMethodeJsonArray("/getCommTraitement/" + traitement.id_Traitement);
            traitement.CommTraitement = new List<CommTraitement>();
            foreach (JObject t in json)
            {
             
                traitement.CommTraitement.Add(Builders.BuildNewTraitement.BuildCommTraitementJ(t));
            } 

            //   JToken CommTraitement = r.GetValue("commTraitement");
            //   if (CommTraitement == null)
            //       return trt;
            //   foreach (JToken t in CommTraitement)
            //   {
            //       JObject j = JObject.Parse(t.ToString());

            //       trt.CommTraitement.Add(BuildCommTraitement(j));
            //   }
          //  DataTable dt = DAC.getActesTraitements(traitement.id_Traitement );

         //   List<CommActesTraitement> lst = new List<CommActesTraitement>();

            //traitement.CommTraitement = new List<CommTraitement>();
            //foreach (DataRow dr in dt.Rows)
            //{

            //    CommTraitement c = Builders.BuildNewTraitement.BuildCommTraitement(dr);
                
            //    traitement.CommTraitement .Add (c);
            //}
          
            
        }
        public static FamillesTraitements getParentFamillenewTraitement(FamillesTraitements famille)
        {

            if (famille.ParentFamillesTraitementId == -1)
                return famille;
            else
            {
                famille = famillesTraitement.Find(x => x.ParentFamillesTraitementId == famille.ParentFamillesTraitementId);
               getParentFamillenewTraitement(famille);
               return famille;
                
            }
        }


        public static List<FamillesTraitements> GetFamilleTraitementByParent(int parentId)
        {
            string method = "/ordredFamilleTraitementByIdParent/"+parentId;
            List<FamillesTraitements> liste = new List<FamillesTraitements>();
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);

            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildFamillesTraitement.BuildJ(obj));
            return liste;
            
        }

        
    }
}
