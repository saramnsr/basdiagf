using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{
   
        public static class BuildFamillesTraitement
        {
            public static FamillesTraitements Build(DataRow r)
            {
                FamillesTraitements ftrt = new FamillesTraitements();
                ftrt.Id = r["id"] is DBNull ? -1 : Convert.ToInt32(r["id"]);
                ftrt.libelle = r["Nom"] is DBNull ? "" : Convert.ToString(r["Nom"]).Trim();
                ftrt.ParentFamillesTraitementId = r["Parent"] is DBNull ? -1 : Convert.ToInt32(r["Parent"]);
                ftrt.couleur = r["couleur"] is DBNull ? System.Drawing.Color.WhiteSmoke : System.Drawing.Color.FromArgb(Convert.ToInt32(r["couleur"]));
                ftrt.ordre = r["ordre"] is DBNull ? -1 : Convert.ToInt32(r["ordre"]);
                ftrt.TitreDevis = r["TITRE_DEVIS"] is DBNull ? "" : Convert.ToString(r["TITRE_DEVIS"]).Trim();
              //  ftrt.isNotDevis = r["TYPEFAMILLETRAITEMENT"] is DBNull ? false : Convert.ToBoolean(r["TYPEFAMILLETRAITEMENT"]);
                return ftrt;
            }
            public static FamillesTraitements BuildJ(JObject r)
            {
                FamillesTraitements ftrt = new FamillesTraitements();
                ftrt.Id = Convert.ToInt32(r["id"]);
                ftrt.libelle = Convert.ToString(r["nom"]).Trim();
                ftrt.ParentFamillesTraitementId =Convert.ToInt32(r["parent"]);
                ftrt.couleur = r["couleur"].ToString() == ""  ? System.Drawing.Color.WhiteSmoke : System.Drawing.Color.FromArgb(Convert.ToInt32(r["couleur"]));
                ftrt.ordre = r["ordre"].ToString() == "" ? 0 : Convert.ToInt32(r["ordre"]);
                ftrt.TitreDevis =  Convert.ToString(r["titre_DEVIS"]).Trim();
                ftrt.typeFamilleTraitement = r["typefamilletraitement"].ToString() == "" ? TypeFamilleTraitement.Senario : (TypeFamilleTraitement)Convert.ToInt32(r["typefamilletraitement"]);
                return ftrt;
            }
        }



        public static class BuildNewTraitement
        {
            public static CommTraitement BuildCommTraitementJ(JObject r)
            {
                int TmpidActe = -1;
                CommTraitement com = new CommTraitement();
                com.Id = Convert.ToInt32(r["id"]);
                com.IdActe = Convert.ToInt32(r["id_ACTE"]);
                TmpidActe = com.IdActe;
                // a voir aussi 
                com.Acte = new ActeTraitement(BasCommon_BL.ActesMgmt.getActe(com.IdActe));

                if (com.Acte.id_acte == -1)
                {
                    ActesMgmt.Actes = null;
                    com.Acte = new ActeTraitement(BasCommon_BL.ActesMgmt.getActe(TmpidActe));
                }
                

                com.IdAssistante =r["id_ASSISTANTE"].ToString() == "" ? -1 :  Convert.ToInt32(r["id_ASSISTANTE"]);

                com.Assistante = com.IdAssistante == -1 ? null : UtilisateursMgt.getUtilisateur(com.IdAssistante);

                /// a fixer
                com.DatePrevisionnnelle = r["date_COMM"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["date_COMM"]);
                com.IdPraticien = r["id_PRATICIEN"].ToString() == "" ? -1 : Convert.ToInt32(r["id_PRATICIEN"]);
                //  com.active = r["ISACTIVE"] is DBNull ? false : Convert.ToBoolean(r["ISACTIVE"]);

                com.IdSecretaire = r["id_SECRETAIRE"].ToString() == "" ? -1 : Convert.ToInt32(r["id_SECRETAIRE"]);
                com.Secretaire = com.IdSecretaire == -1 ? null : UtilisateursMgt.getUtilisateur(com.IdSecretaire);
                com.praticien = com.IdPraticien == -1 ? null : UtilisateursMgt.getUtilisateur(com.IdPraticien);

                com.Acte.prix_traitement = Convert.ToDouble(r["prix_TRAITEMENT"]);
                com.Acte.prix_acte = Convert.ToDouble(r["prix_ACTE"]);
                com.Acte.quantite = r["qte"].ToString() == "" ? "1" : Convert.ToString(r["qte"]);

                com.ActesSupp = new List<CommActesTraitement>();
                com.Radios = new List<CommActesTraitement>();
                com.photos = new List<CommActesTraitement>();
                com.Materiels = new List<CommMaterielTraitement>();
                com.NbJours = r["nbjours"].ToString() == "" ? 0 : Convert.ToInt32(r["nbjours"]);
                com.echeancestemp = new List<TempEcheanceDefinition>();
                return com;
            }
            public static NewTraitement BuildJ(JObject r)
            {
                NewTraitement trt = new NewTraitement();

                trt.id_Traitement = Convert.ToInt32(r["id_TRAITEMENT"]);
                trt.Traitement_libelle = r["traitement_LIBELLE"].ToString() == "" ? "" : Convert.ToString(r["traitement_LIBELLE"]).Trim();
                trt.Traitement_couleur = r["traitement_COULEUR"].ToString() == "" ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["traitement_COULEUR"]));
                trt.id_famille = Convert.ToInt32(r["id_FAMILLE_TRAITEMENT"]);
                
                // edited by wael
                // code ajouté afin de recuperer la famille traitement d'un NewTraiement               

                trt.famille_Traitement = trt.id_famille > 0 ? BasCommon_BL.TraitementsMgmt.famillesTraitement.Find(x => x.Id == trt.id_famille) : null; ;
                

                trt.Montant_Scenario = r["montantscenario"].ToString() == "" ? 0 : Convert.ToDouble(r["montantscenario"]);
                trt.TypeScenario = r["type"].ToString() == "" ? NewTraitement.typeScenario.Prothése : (NewTraitement.typeScenario)Convert.ToInt32(r["type"]);
                trt.contention = r["contention"].ToString() == "" ? false : Convert.ToBoolean(r["contention"]);
                trt.Traitement_shortlib = r["shortlib"].ToString() == "" ? "" : Convert.ToString(r["shortlib"]).Trim();
                trt.Traitement_commentaire = r["commentaire"].ToString() == "" ? "" : Convert.ToString(r["commentaire"]).Trim();

                trt.ordre = r["ordre"].ToString() == "" ? 0 : Convert.ToInt32(r["ordre"]);

               

                return trt;
            }
            public static NewTraitement  Build(DataRow r)
            {
                NewTraitement trt = new NewTraitement();
                trt.id_Traitement = r["id_Traitement"] is DBNull ? -1 : Convert.ToInt32(r["id_Traitement"]);
                trt.Traitement_libelle = r["Traitement_libelle"] is DBNull ? "" : Convert.ToString(r["Traitement_libelle"]).Trim() ;
                trt.Traitement_couleur = r["Traitement_couleur"] is DBNull ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["Traitement_couleur"]));
                trt.id_famille = r["id_famille_Traitement"] is DBNull ? -1 : Convert.ToInt32(r["id_famille_Traitement"]);

                trt.Montant_Scenario = r["MONTANTSCENARIO"] is DBNull ? 0 : Convert.ToDouble(r["MONTANTSCENARIO"]);
                trt.TypeScenario = r["TYPE"] is DBNull ? NewTraitement.typeScenario.Prothése : (NewTraitement.typeScenario)Convert.ToInt32(r["TYPE"]);
                trt.contention = r["CONTENTION"] is DBNull ? false : Convert.ToBoolean(r["CONTENTION"]);
                    //recherche prix traitement
                //NewTraitement _Traitement = new NewTraitement();
                //_Traitement = TraitementsMgmt.GetFullTraitement(trt.id_Traitement);
                //TraitementsMgmt.GetCommTraitements(ref _Traitement);
                //double prix_total = 0;

                //foreach (CommTraitement ltrt in _Traitement.CommTraitement)
                //{
                //    ltrt.ActesSupp = TraitementsMgmt.GetCommActeSupTraitements(ltrt);
                //    ltrt.Radios = TraitementsMgmt.GetCommActeSupTraitements(ltrt, "R");
                //    ltrt.photos = TraitementsMgmt.GetCommActeSupTraitements(ltrt, "P");
                //    ltrt.Materiels = TraitementsMgmt.GetCommMaterielsTraitements(ltrt);

                //    if (ltrt.ActesSupp != null)
                //    {
                //        foreach (CommActesTraitement cap in ltrt.ActesSupp)
                //        {
                //            // cap.Parent = trt;
                //            prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                //            // prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                //        }
                //    }
                //    if (ltrt.Materiels != null)
                //    {

                //        foreach (CommMaterielTraitement cap in ltrt.Materiels)
                //        {
                //            cap.Famille = MaterielsMgmt.GetFamilleMaterielByIdMateriel(cap.idMateriel);
                //            if (cap.Famille != null)
                //                if (cap.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0 || cap.Famille.libelle.ToLower().IndexOf("stérilisation") >= 0)
                //                {
                //                    prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                //                    //prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                //                }
                //        }
                //    }
                //    if (ltrt.Radios != null)
                //    {

                //        foreach (CommActesTraitement cap in ltrt.Radios)
                //        {

                //            prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                //            //prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                //        }
                //    }
                //    if (ltrt.photos != null)
                //    {

                //        foreach (CommActesTraitement cap in ltrt.photos)
                //        {

                //            prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                //            //  prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));

                //        }
                //    }
                //    prix_total = prix_total + (Convert.ToDouble(ltrt.Acte.prix_traitement) * Convert.ToInt32(ltrt.Acte.quantite));
                //    // prix_ligne = prix_ligne + (Convert.ToDouble(trt.Acte.prix_traitement) * Convert.ToInt32(trt.Acte.quantite));
                //}
                //finc recherche prix
                trt.Traitement_shortlib = r["SHORTLIB"] is DBNull ? "" : Convert.ToString(r["SHORTLIB"]).Trim();
                //+"\n888";

                trt.ordre = r["ORDRE"] is DBNull ? 0 : Convert.ToInt32(r["ORDRE"]);

                return trt;
            }

          
            public static CommTraitement  BuildCommTraitement(DataRow r)
            {
                int TmpidActe = -1;
                CommTraitement com = new CommTraitement();
                 com.Id = Convert.ToInt32(r["ID"]);
                   com.IdActe = r["ID_ACTE"] is DBNull ? -1 : Convert.ToInt32(r["ID_ACTE"]);
                   TmpidActe = com.IdActe;
                 com.Acte = new ActeTraitement(BasCommon_BL.ActesMgmt.getActe(com.IdActe));
                
                 if (com.Acte.id_acte == -1)
                 {
                     ActesMgmt.Actes=null;
                     com.Acte = new ActeTraitement(BasCommon_BL.ActesMgmt.getActe(TmpidActe));
                 }

                 
                 com.IdAssistante = r["ID_ASSISTANTE"] is DBNull ? -1 : Convert.ToInt32(r["ID_ASSISTANTE"]);
                 //com.praticien = UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["ID_PRATICIEN"]));
                 com.Assistante = UtilisateursMgt.getUtilisateur(com.IdAssistante);
                 com.DatePrevisionnnelle = r["DATE_COMM"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATE_COMM"]);
                 com.IdPraticien  = r["ID_PRATICIEN"] is DBNull ? -1 : Convert.ToInt32(r["ID_PRATICIEN"]);
               //  com.active = r["ISACTIVE"] is DBNull ? false : Convert.ToBoolean(r["ISACTIVE"]);
                 com.IdSecretaire = r["ID_SECRETAIRE"] is DBNull ? -1 : Convert.ToInt32(r["ID_SECRETAIRE"]);
                 com.Secretaire = UtilisateursMgt.getUtilisateur(com.IdSecretaire);
                 com.praticien = UtilisateursMgt.getUtilisateur(com.IdPraticien);
                
                     com.Acte.prix_traitement = r["PRIX_TRAITEMENT"] is DBNull ? 0 : Convert.ToDouble(r["PRIX_TRAITEMENT"]);
                     com.Acte.prix_acte = r["PRIX_ACTE"] is DBNull ? 0 : Convert.ToDouble(r["PRIX_ACTE"]);
                     com.Acte.quantite = r["QTE"] is DBNull ? "1" : Convert.ToString(r["QTE"]);
                 
                 com.ActesSupp = new List<CommActesTraitement>();
                 com.Radios = new List<CommActesTraitement>();
                 com.photos = new List<CommActesTraitement>();
                 com.Materiels = new List<CommMaterielTraitement>();
                 com.NbJours = r["NBJOURS"] is DBNull ? 0 : Convert.ToInt32(r["NBJOURS"]);
                 com.echeancestemp = new List<TempEcheanceDefinition>();
                 return com;          
            }
            public static CommMaterielTraitement BuildTraitementMateriel(DataRow r)
            {
                CommMaterielTraitement com = new CommMaterielTraitement();

               // com.IdBaseProduit = r["ID_BASEPRODUIT"] is DBNull ? -1 : Convert.ToInt32(r["ID_BASEPRODUIT"]);
                com.Libelle = Convert.ToString(r["MATERIEL_LIBELLE"]).Trim();
                com.ShortLib = Convert.ToString(r["SHORTLIB"]).Trim();
                com.IdComm = Convert.ToInt32(r["id"]);
               com.idMateriel = Convert.ToInt32(r["id_materiel"]);
                com.prix_materiel = r["PRIX_MATERIEL"] is DBNull ? 0 : Convert.ToDouble(r["PRIX_MATERIEL"]) ;
                com.materiel_couleur = r["materiel_couleur"] is DBNull ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["materiel_couleur"]));
                com.prix_traitement = r["PRIX_TRAITEMENT"] is DBNull ? 0 : Convert.ToDouble(r["PRIX_TRAITEMENT"]) ;
                com.Qte = r["QTE"] is DBNull ? 1 : Convert.ToInt32 (r["QTE"]);
                //com.idFamille = r["ID_FAMILLE_MATERIEL"] is DBNull ? 0 : Convert.ToInt32(r["ID_FAMILLE_MATERIEL"]);
                com.Famille = MaterielsMgmt.GetFamilleMateriel(Convert.ToInt32(r["ID_FAMILLE_MATERIEL"]));
                return com;
            }
            public static CommMaterielTraitement BuildTraitementMaterielJ(JObject r)
            {
                CommMaterielTraitement com = new CommMaterielTraitement();

                // com.IdBaseProduit = r["ID_BASEPRODUIT"] is DBNull ? -1 : Convert.ToInt32(r["ID_BASEPRODUIT"]);
                com.Libelle = Convert.ToString(r.GetValue("materiel")["materiel_LIBELLE"]).Trim();
                com.ShortLib = Convert.ToString(r.GetValue("materiel")["shortlib"]).Trim();
                com.IdComm = Convert.ToInt32(r["id"]);
                com.idMateriel = Convert.ToInt32(r.GetValue("materiel")["id_MATERIEL"]);
                com.prix_materiel = Convert.ToDouble(r.GetValue("materiel")["prix_MATERIEL"]);
                com.materiel_couleur = Convert.ToInt32(r.GetValue("materiel")["materiel_COULEUR"]) == 0 ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r.GetValue("materiel")["materiel_COULEUR"]));
                com.prix_traitement = Convert.ToDouble(r["prix_traitement"]);
                com.Qte = Convert.ToInt32(r["qte"]);
              //c  com.idFamille = r["id_FAMILLE_MATERIEL"].ToString() == "" ? 0 : Convert.ToInt32(r["id_FAMILLE_MATERIEL"]);
                com.Famille = MaterielsMgmt.famillesmateriel.Find(x => x.Id == Convert.ToInt32(r["id_FAMILLE_MATERIEL"]));
                return com;
            }
            public static CommAutrePersonne BuildTraitementAutrePersonne(DataRow r)
        {
            CommAutrePersonne com = new CommAutrePersonne();
            com.IdCorrespondant = Convert.ToInt32(r["ID_CORRESPONDANT"]);
            com.Nom = Convert.ToString(r["per_nom"]).Trim();
            com.Prenom = Convert.ToString(r["per_prenom"]).Trim();
            com.IdComm = Convert.ToInt32(r["id_traitement"]);

            return com;
        }

         
            public static CommActesTraitement BuildCommActeSuppTraitement(DataRow r)
            {
                CommActesTraitement com = new CommActesTraitement();
                com.IdActe = r["ID_ACTE"] is DBNull ? -1 : Convert.ToInt32(r["ID_ACTE"]);
                com.LibActe = r["ACTE_LIBELLE"] is DBNull ? "" : Convert.ToString(r["ACTE_LIBELLE"]).Trim();
                com.acte_couleur = r["ACTE_COULEUR"] is DBNull ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["ACTE_COULEUR"]));
                com.prix_acte = r["prix_acte"] is DBNull ? 0 : Convert.ToDouble(r["prix_acte"]) ;
                com.acte_durestd = r["acte_durestd"] is DBNull ? 0 : Convert.ToInt32(r["acte_durestd"]);
                com.prix_traitement = r["PRIX_TRAITEMENT"] is DBNull ? 0 : Convert.ToDouble(r["PRIX_TRAITEMENT"]) ;
                com.Qte = r["QTE"] is DBNull ? 1 : Convert.ToInt32(r["QTE"]);
                com.Depassement = r["ACTE_DEPASSEMENT"] is DBNull ? 0 : Convert.ToDouble(r["ACTE_DEPASSEMENT"]);
                com.Tarif = r["TARIF"] is DBNull ? 0 : Convert.ToDouble(r["TARIF"]);
                com.Remboursement = r["ACTE_REMBOURSEMENT"] is DBNull ? 0 : Convert.ToDouble(r["ACTE_REMBOURSEMENT"]);
                com.BaseRemboursement = r["ACTE_BASE_REMBOURSEMENT"] is DBNull ? 0 : Convert.ToDouble(r["ACTE_BASE_REMBOURSEMENT"]);
                com.CodeTransposition = r["ACTE_CODE_TRANSPOSOTION"] is DBNull ? "" : Convert.ToString(r["ACTE_CODE_TRANSPOSOTION"]);
                return com;
            }

            public static CommActesTraitement BuildCommActeSuppTraitementJ(JObject r)
            {
                CommActesTraitement com = new CommActesTraitement();
                com.IdActe =  Convert.ToInt32(r["idActe"]);
                com.LibActe =  Convert.ToString(r.GetValue("acte")["acte_LIBELLE"]).Trim();
                com.acte_couleur = Convert.ToInt32(r.GetValue("acte")["acte_COULEUR"]) == 0 ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r.GetValue("acte")["acte_COULEUR"]));
                com.prix_acte = Convert.ToDouble(r.GetValue("acte")["prix_ACTE"]);
                com.acte_durestd = Convert.ToInt32(r.GetValue("acte")["acte_DURESTD"]);
                com.prix_traitement = Convert.ToDouble(r["prix_traitement"]);
                com.Qte =  Convert.ToInt32(r["qte"]);
                com.Depassement =  Convert.ToDouble(r.GetValue("acte")["acte_DEPASSEMENT"]);
                com.Tarif = Convert.ToDouble(r.GetValue("acte")["tarif"]);
                com.Remboursement = Convert.ToDouble(r.GetValue("acte")["acte_REMBOURSEMENT"]);
                com.BaseRemboursement = Convert.ToDouble(r.GetValue("acte")["acte_BASE_REMBOURSEMENT"]);
                com.CodeTransposition = Convert.ToString(r.GetValue("acte")["acte_CODE_TRANSPOSOTION"]);
                return com;
            }
        }

   
}
