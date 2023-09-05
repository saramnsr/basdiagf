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

    public static class BuildDevis
    {
        public static Devis BuildJ(JObject r)
        {

            Devis devis = new Devis();
            devis.Id = Convert.ToInt32(r["id"]);
            devis.DateProposition = Convert.ToDateTime(r["dateproposition"]);
            devis.DatePrevisionnelDeDebutTraitement = r["datedebuttraitement"].ToString() == "" ? DateTime.Now.AddDays(15) : Convert.ToDateTime(r["datedebuttraitement"]);
            devis.DatePrevisionnelDeFinTraitement = r["datefintraitement"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["datefintraitement"]);

            devis.DateAcceptation = r["dateacceptation"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateacceptation"]);
            devis.DateEcheance = r["dateecheance"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateecheance"]);
            devis.DateArchivage = r["datearchivage"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["datearchivage"]);
            devis.ArchivePar = r["archivepar"].ToString() == "" ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["archivepar"]));
            devis.RemarqueArchivage = r["remarquearchivage"].ToString() == "" ? "" : Convert.ToString(r["remarquearchivage"]);
            devis.EmplacementArchivage = r["emplacementarchivage"].ToString() == "" ? "" : Convert.ToString(r["emplacementarchivage"]);
            devis.TypeDevis = r["typedevis"].ToString() == "" ? Devis.enumtypePropositon.Aucun : (Devis.enumtypePropositon)Convert.ToInt32(r["typedevis"]);

            devis.Montant = r["montantpropose"].ToString() == "" ? 0 : (double?)Convert.ToDouble(r["montantpropose"]);
            devis.MontantAvantRemise = r["montantavantproposition"].ToString() == "" ? 0 : (double?)Convert.ToDouble(r["montantavantproposition"]);


            devis.IdPatient = Convert.ToInt32(r["id_PATIENT"]);
            devis.IdObjetBaseView = r["id_OBJET_BASEVIEW"].ToString() == "" ? -1 : Convert.ToInt32(r["id_OBJET_BASEVIEW"]);

            return devis;
        }
        public static Devis Build(DataRow r)
        {

            Devis devis = new Devis();
            devis.Id = Convert.ToInt32(r["ID"]);
            devis.DateProposition = Convert.ToDateTime(r["DATEPROPOSITION"]);
            devis.DatePrevisionnelDeDebutTraitement = r["DATEDEBUTTRAITEMENT"] is DBNull?DateTime.Now.AddDays(15): Convert.ToDateTime(r["DATEDEBUTTRAITEMENT"]);
            devis.DatePrevisionnelDeFinTraitement = r["DATEFINTRAITEMENT"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEFINTRAITEMENT"]);
            
            devis.DateAcceptation = r["DATEACCEPTATION"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEACCEPTATION"]);
            devis.DateEcheance = r["DATEECHEANCE"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEECHEANCE"]);
            devis.DateArchivage = r["DateArchivage"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DateArchivage"]);
            devis.ArchivePar = r["ArchivePar"] is DBNull ? null : UtilisateursMgt.getUtilisateur( Convert.ToInt32(r["ArchivePar"]));
            devis.RemarqueArchivage = r["RemarqueArchivage"] is DBNull ? "" : Convert.ToString(r["RemarqueArchivage"]);
            devis.EmplacementArchivage = r["EmplacementArchivage"] is DBNull ? "" : Convert.ToString(r["EmplacementArchivage"]);
            devis.TypeDevis = r["TypeDevis"] is DBNull ? Devis.enumtypePropositon.Aucun : (Devis.enumtypePropositon)Convert.ToInt32(r["TypeDevis"]);

            devis.Montant = r["MONTANTPROPOSE"] is DBNull ? null : (double?)Convert.ToDouble(r["MONTANTPROPOSE"]);
            devis.MontantAvantRemise = r["MONTANTAVANTPROPOSITION"] is DBNull ? null : (double?)Convert.ToDouble(r["MONTANTAVANTPROPOSITION"]);
            
            
            devis.IdPatient = Convert.ToInt32(r["ID_PATIENT"]);
            devis.IdObjetBaseView = r["ID_OBJET_BASEVIEW"] is DBNull ? -1 : Convert.ToInt32(r["ID_OBJET_BASEVIEW"]);

            return devis;
        }
        public static Devis_TK Build_TKJ(JObject r)
        {

             Devis_TK devis = new Devis_TK();
           devis.Id = Convert.ToInt32(r["id"]);

           devis.DateProposition = Convert.ToDateTime(r["dateproposition"]);
           devis.DatePrevisionnelDeDebutTraitement = r["datedebuttraitement"].ToString() == "" ? DateTime.Now.AddDays(15) : Convert.ToDateTime(r["datedebuttraitement"]);
           devis.DatePrevisionnelDeFinTraitement = r["datefintraitement"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["datefintraitement"]);

           devis.DateAcceptation = r["dateacceptation"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateacceptation"]);
           devis.DateEcheance = r["dateecheance"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateecheance"]);
           devis.DateArchivage = r["datearchivage"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["datearchivage"]);
           devis.ArchivePar = r["archivepar"].ToString() == ""  ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["archivepar"]));
           devis.RemarqueArchivage = r["remarquearchivage"].ToString() == "" ? "" : Convert.ToString(r["remarquearchivage"]);
           devis.EmplacementArchivage = r["emplacementarchivage"].ToString() == "" ? "" : Convert.ToString(r["emplacementarchivage"]);

           devis.Montant = r["montantpropose"].ToString() == "" ? null : (double?)Convert.ToDouble(r["montantpropose"]);
           devis.MontantAvantRemise = r["montantavantproposition"].ToString() == "" ? null : (double?)Convert.ToDouble(r["montantavantproposition"]);


           devis.IdPatient = Convert.ToInt32(r["id_PATIENT"]);
           devis.Id_Traitement =Convert.ToInt32(r["id_TRAITEMENT"]);
           devis.Traitement = TraitementsMgmt.Traitements.Find(w=>w.id_Traitement == devis.Id_Traitement);
           if (devis.Traitement == null)
               devis.Traitement = new NewTraitement();
           devis.actesTraitement = new List<CommTraitement>();
           
           devis.MontantDocteur = r["montantdocteur"].ToString() == "" ? null : (double?)Convert.ToDouble(r["montantdocteur"]);
           devis.Titre = Convert.ToString(r["titre"]);
           devis.EcheancierDocteur = r["echeancier_DOCTEUR"].ToString() == "" ? 0 : Convert.ToInt32(r["echeancier_DOCTEUR"]);
           devis.partPatient = r["partpatient"].ToString() == "" ? 0 : Convert.ToInt32(r["partpatient"]);
           devis.RembMutuelle = r["rembmutuelle"].ToString() == "" ? 0 : Convert.ToInt32(r["rembmutuelle"]);
           devis.localisationAnatomiuque = r["localisation"].ToString() == "" ? "" : Convert.ToString(r["localisation"]);
           return devis;
       }
        public static Devis_TK Build_TK(DataRow r)
        {

            Devis_TK devis = new Devis_TK();
            devis.Id = Convert.ToInt32(r["ID"]);
          
            devis.DateProposition = Convert.ToDateTime(r["DATEPROPOSITION"]);
            devis.DatePrevisionnelDeDebutTraitement = r["DATEDEBUTTRAITEMENT"] is DBNull ? DateTime.Now.AddDays(15) : Convert.ToDateTime(r["DATEDEBUTTRAITEMENT"]);
            devis.DatePrevisionnelDeFinTraitement = r["DATEFINTRAITEMENT"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEFINTRAITEMENT"]);

            devis.DateAcceptation = r["DATEACCEPTATION"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEACCEPTATION"]);
            devis.DateEcheance = r["DATEECHEANCE"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEECHEANCE"]);
            devis.DateArchivage = r["DateArchivage"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DateArchivage"]);
            devis.ArchivePar = r["ArchivePar"] is DBNull ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["ArchivePar"]));
            devis.RemarqueArchivage = r["RemarqueArchivage"] is DBNull ? "" : Convert.ToString(r["RemarqueArchivage"]);
            devis.EmplacementArchivage = r["EmplacementArchivage"] is DBNull ? "" : Convert.ToString(r["EmplacementArchivage"]);
           
            devis.Montant = r["MONTANTPROPOSE"] is DBNull ? null : (double?)Convert.ToDouble(r["MONTANTPROPOSE"]);
            devis.MontantAvantRemise = r["MONTANTAVANTPROPOSITION"] is DBNull ? null : (double?)Convert.ToDouble(r["MONTANTAVANTPROPOSITION"]);


            devis.IdPatient = Convert.ToInt32(r["ID_PATIENT"]);
            devis.Id_Traitement = r["Id_Traitement"] is DBNull ? -1 :Convert.ToInt32(r["Id_Traitement"]);
            NewTraitement TmpTraitement = new NewTraitement();
            TmpTraitement = TraitementsMgmt.GetFullTraitement(devis.Id_Traitement);
            TraitementsMgmt.GetCommTraitements(ref TmpTraitement);
            devis.Traitement = TmpTraitement;
            devis.MontantDocteur = r["MONTANTDOCTEUR"] is DBNull ? 0 : (double?)Convert.ToDouble(r["MONTANTDOCTEUR"]);
            devis.Titre = Convert.ToString(r["TITRE"]);
            devis.EcheancierDocteur = r["echeancier_docteur"] is DBNull ? 0 : Convert.ToInt32(r["echeancier_docteur"]);
            devis.partPatient = r["PARTPATIENT"] is DBNull ? 0 : Convert.ToInt32(r["PARTPATIENT"]);
            devis.RembMutuelle = r["REMBMUTUELLE"] is DBNull ? 0 : Convert.ToInt32(r["REMBMUTUELLE"]);
            devis.localisationAnatomiuque = r["localisation"] is DBNull ? "" : Convert.ToString(r["localisation"]);
            return devis;
        }
        public static CommActesTraitement BuildCommActeSuppDevis(DataRow r)
        {

            CommActesTraitement com = new CommActesTraitement();
            com.IdActe = r["ID_ACTE"] is DBNull ? -1 : Convert.ToInt32(r["ID_ACTE"]);
            com.LibActe = r["ACTE_LIBELLE"] is DBNull ? "" : Convert.ToString(r["ACTE_LIBELLE"]).Trim();
            com.acte_couleur = r["ACTE_COULEUR"] is DBNull ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["ACTE_COULEUR"]));
            com.prix_acte = r["prix_acte"] is DBNull ? 0 : Convert.ToDouble(r["prix_acte"]);
            com.acte_durestd = r["acte_durestd"] is DBNull ? 0 : Convert.ToInt32(r["acte_durestd"]);
            com.prix_traitement = r["MONTANT"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT"]);
            com.Qte = r["QTE"] is DBNull ? 1 : Convert.ToInt32(r["QTE"]);
            com.desactive = r["DESACTIVE"] is DBNull ? false : Convert.ToBoolean(r["DESACTIVE"]);
              com.cotation  =  r["COTATION"] is DBNull ? "" : Convert.ToString(r["COTATION"]).Trim();
                com.coefficient = r["COEFFICIENT"] is DBNull ? -1 : Convert.ToInt32(r["COEFFICIENT"]);
              //  com.nomenclature = r["nomenclature"] is DBNull ? "" : Convert.ToString(r["nomenclature"]).Trim();
          
                com.nombre_points = r["NOMBRE_POINTS"] is DBNull ? "0" : Convert.ToString(r["NOMBRE_POINTS"]);

                com.LibActe_estimation = r["ACTE_LIBELLE_ESTIMATION"] is DBNull ? "" : Convert.ToString(r["ACTE_LIBELLE_ESTIMATION"]).Trim();

                com.LibActe_facture = r["ACTE_LIBELLE_FACTURE"] is DBNull ? "" : Convert.ToString(r["ACTE_LIBELLE_FACTURE"]).Trim();
                com.Depassement = r["ACTE_DEPASSEMENT"] is DBNull ? 0 : Convert.ToDouble(r["ACTE_DEPASSEMENT"]);
                com.CodeTransposition = r["ACTE_CODE_TRANSPOSOTION"] is DBNull ? "" : Convert.ToString(r["ACTE_CODE_TRANSPOSOTION"]);
                com.BaseRemboursement = r["ACTE_BASE_REMBOURSEMENT"] is DBNull ? 0 : Convert.ToDouble(r["ACTE_BASE_REMBOURSEMENT"]);
                com.Tarif = r["TARIF"] is DBNull ? 0 : Convert.ToDouble(r["TARIF"]);
                com.Remboursement = r["ACTE_REMBOURSEMENT"] is DBNull ? 0 : Convert.ToDouble(r["ACTE_REMBOURSEMENT"]);
                com.partPatient = r["PARTPATIENT"] is DBNull ? 0 : Convert.ToDouble(r["PARTPATIENT"]);
                com.RembMutuelle = r["REMBMUTUELLE"] is DBNull ? 0 : Convert.ToDouble(r["REMBMUTUELLE"]);
       

            return com;
        }
        public static CommAutrePersonne BuildDevisAutrePersonne(DataRow r)
        {
            CommAutrePersonne com = new CommAutrePersonne();
            com.IdCorrespondant = Convert.ToInt32(r["ID_CORRESPONDANT"]);
            com.Nom = Convert.ToString(r["per_nom"]).Trim();
            com.Prenom = Convert.ToString(r["per_prenom"]).Trim();
            com.IdComm = Convert.ToInt32(r["ID_COMM_DEVIS"]);

            return com;
        }
        public static CommMaterielTraitement BuildDevisMateriel(DataRow r)
        {
            CommMaterielTraitement com = new CommMaterielTraitement();

            // com.IdBaseProduit = r["ID_BASEPRODUIT"] is DBNull ? -1 : Convert.ToInt32(r["ID_BASEPRODUIT"]);
            com.Libelle = Convert.ToString(r["MATERIEL_LIBELLE"]).Trim();
            com.Qte = Convert.ToInt32(r["QTE"]);
            com.ShortLib = Convert.ToString(r["SHORTLIB"]).Trim();
            com.IdComm = Convert.ToInt32(r["id"]);
            com.idMateriel = Convert.ToInt32(r["id_materiel"]);
            com.prix_materiel = r["MONTANTAVANTREMISE"] is DBNull ? 0 : Convert.ToDouble(r["MONTANTAVANTREMISE"]);
            com.prix_traitement = r["MONTANT"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT"]);
            
            com.materiel_couleur = r["materiel_couleur"] is DBNull ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["materiel_couleur"]));
            com.Famille = MaterielsMgmt.GetFamilleMaterielByIdMateriel(com.idMateriel);
                 return com;
        }
        public static CommTraitement BuildCommTraitement(JObject r)
        {
            int TmpidActe = -1;
            CommTraitement com = new CommTraitement();
            com.Id = Convert.ToInt32(r["id"]);
            com.IdActe = r["id_ACTE"].ToString() == "" ? -1 : Convert.ToInt32(r["id_ACTE"]);
            TmpidActe = com.IdActe;
            // a voir aussi 
            com.Acte = new ActeTraitement(BasCommon_BL.ActesMgmt.getActe(com.IdActe));

            if (com.Acte.id_acte == -1)
            {
                ActesMgmt.Actes = null;
                com.Acte = new ActeTraitement(BasCommon_BL.ActesMgmt.getActe(TmpidActe));
            }


            com.IdAssistante =r["id_ASSISTANTE"].ToString() == "" ? -1  :  Convert.ToInt32(r["id_ASSISTANTE"]);
            //com.praticien = UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["ID_PRATICIEN"]));
            com.Assistante = UtilisateursMgt.getUtilisateur(com.IdAssistante);
            com.dents = r["dents"].ToString() == "" ? "" : Convert.ToString(r["dents"]);
            com.DatePrevisionnnelle = r["date_COMM"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["date_COMM"]);
            com.IdPraticien = r["id_PRATICIEN"].ToString() == "" ? -1 : Convert.ToInt32(r["id_PRATICIEN"]);
            //  com.active = r["ISACTIVE"] is DBNull ? false : Convert.ToBoolean(r["ISACTIVE"]);
            com.IdSecretaire = r["id_SECRETAIRE"].ToString() == "" ? -1 : Convert.ToInt32(r["id_SECRETAIRE"]);
            com.Secretaire = UtilisateursMgt.getUtilisateur(com.IdSecretaire);
            com.praticien = UtilisateursMgt.getUtilisateur(com.IdPraticien);

            com.Acte.prix_traitement = r["montant"].ToString() == "" ? 0 : Convert.ToDouble(r["montant"]);
            com.Acte.prix_acte = r["montantavantproposition"].ToString() == "" ? 0 : Convert.ToDouble(r["montantavantproposition"]);
            com.Acte.quantite = r["qte"].ToString() == "" ? "1" : Convert.ToString(r["qte"]);
            com.MontantAvantRemise = r["montantavantproposition"].ToString() == "" ? 0 : Convert.ToDouble(r["montantavantproposition"]);
            com.NbJours = r["nbjours"].ToString() == "" ? 0 : Convert.ToInt32(r["nbjours"]);
            com.partPatient = r["partpatient"].ToString() == "" ? 0 : Convert.ToDouble(r["partpatient"]);
            com.RembMutuelle = r["rembmutuelle"].ToString() == "" ? 0 : Convert.ToDouble(r["rembmutuelle"]);
            com.ActesSupp = new List<CommActesTraitement>();
            com.Radios = new List<CommActesTraitement>();
            com.photos = new List<CommActesTraitement>();
            com.Materiels = new List<CommMaterielTraitement>();
            com.AutrePersonnes = new List<CommAutrePersonne>();

            com.NbJours = r["nbjours"].ToString() == "" ? 0 : Convert.ToInt32(r["nbjours"]);
            com.echeancestemp = new List<TempEcheanceDefinition>();
            return com;
        }
        public static CommMaterielTraitement BuildDevisMateriel(JObject r)
        {
            CommMaterielTraitement com = new CommMaterielTraitement();

            // com.IdBaseProduit = r["ID_BASEPRODUIT"] is DBNull ? -1 : Convert.ToInt32(r["ID_BASEPRODUIT"]);
            com.Libelle = Convert.ToString(r.GetValue("materiel")["materiel_LIBELLE"]).Trim();
            com.Qte = Convert.ToInt32(r["qte"]);
            com.ShortLib = Convert.ToString(r.GetValue("materiel")["shortlib"]).Trim();
            com.IdComm = Convert.ToInt32(r["id"]);
            com.idMateriel = Convert.ToInt32(r.GetValue("materiel")["id_MATERIEL"]);
            com.prix_materiel = Convert.ToDouble(r["montantavatremise"]);
            com.prix_traitement = Convert.ToDouble(r["montant"]);

            com.materiel_couleur = Convert.ToInt32(r.GetValue("materiel")["materiel_COULEUR"]) == 0 ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r.GetValue("materiel")["materiel_COULEUR"]));

            int tmpidFamille = Convert.ToInt32(r.GetValue("materiel")["id_FAMILLE_MATERIEL"]);
            // a implmeenter 
            com.Famille = MaterielsMgmt.famillesmateriel.Find(x => x.Id == tmpidFamille);
            //     MaterielsMgmt.GetFamilleMaterielByIdMateriel(com.idMateriel);
            return com;
        }
        public static CommMaterielTraitement BuildDevisMaterielJ(JObject r)
        {
            CommMaterielTraitement com = new CommMaterielTraitement();
            com.idMateriel = Convert.ToInt32(r["id_MATERIEL"]);
            Materiel m = MaterielsMgmt.Materiels.Find(ma => ma.id_materiel == com.idMateriel);
            if (m != null)
            {
                com.ShortLib = m.materiel_shortlib;
                com.Libelle = m.materiel_libelle;
                com.materiel_couleur = m.materiel_couleur;
                //com.prix_materiel = m.prix_materiel;
                int tmpidFamille = m.id_famille;
                // a implmeenter 
                com.Famille = MaterielsMgmt.famillesmateriel.Find(x => x.Id == tmpidFamille);
            }
            try
            {
                com.desactive = r["desactive"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToInt32(r["desactive"]));
            }
            catch(Exception e)
            {
                com.desactive = r["desactive"].ToString() == "" ? false : Convert.ToBoolean(r["desactive"]);
            }
            com.Qte = Convert.ToInt32(r["qte"]);
         
            com.IdComm = Convert.ToInt32(r["id"]);

            com.prix_materiel = Convert.ToDouble(r["montantavantremise"]);
            com.prix_traitement = Convert.ToDouble(r["montant"]);

           
            //     MaterielsMgmt.GetFamilleMaterielByIdMateriel(com.idMateriel);
            return com;
        }
        public static CommActesTraitement BuildCommActeSuppDevis(JObject r)
        {
           
            CommActesTraitement com = new CommActesTraitement();
            com.IdActe = Convert.ToInt32(r["id_ACTE"]);
            Acte a = BasCommon_BL.ActesMgmt.Actes.Find(x => x.id_acte == com.IdActe);
            if (a != null)
            {
                com.LibActe = a.acte_libelle;
                com.LibActe_estimation = a.acte_libelle_estimation;
                com.acte_couleur = a.acte_couleur;
                com.acte_durestd = a.acte_durestd;
                com.LibActe_facture = a.acte_libelle_facture;
                com.cotation = a.cotation;
                com.coefficient = a.coefficient;
                com.nomenclature = a.nomenclature;
                com.nombre_points = a.nombre_points;
                com.BaseRemboursement = a.BaseRemboursement;
                com.CodeTransposition = a.CodeTransposition;
                com.Remboursement = a.Remboursement;
                com.Depassement = a.Depassement;
                com.prix_acte = a.prix_acte;
            }


            com.prix_traitement = r["montant"].ToString() == "" ? 0 : Convert.ToDouble(r["montant"]);
            com.Qte = r["qte"].ToString() == "" ? 0 : Convert.ToInt32(r["qte"]);
            try
            {
                com.desactive = r["desactive"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToUInt32(r["desactive"]));
            }
            catch (Exception e)
            {
                com.desactive = r["desactive"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToString(r["desactive"]));
            }

            com.partPatient = r["partpatient"].ToString() == "" ? 0 : Convert.ToDouble(r["partpatient"]);
            com.RembMutuelle = r["rembmutuelle"].ToString() == "" ? 0 : Convert.ToDouble(r["rembmutuelle"]);


            return com;
        }
        public static CommTraitement BuildCommDevis(DataRow r)
        {
            CommTraitement com = new CommTraitement();
            com.Id = Convert.ToInt32(r["ID"]);
            
            com.IdActe = r["ID_ACTE"] is DBNull ? -1 : Convert.ToInt32(r["ID_ACTE"]);
            com.Acte = new ActeTraitement(BasCommon_BL.ActesMgmt.getActe(com.IdActe));
            com.Acte.prix_acte = r["MONTANTAVANTPROPOSITION"] is DBNull ? 0 : Convert.ToDouble(r["MONTANTAVANTPROPOSITION"]);
            com.Acte.prix_traitement = r["MONTANT"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT"]);
            com.Acte.quantite = r["QTE"] is DBNull ? "1" : Convert.ToString(r["QTE"]);
            
            com.IdAssistante = r["ID_ASSISTANTE"] is DBNull ? -1 : Convert.ToInt32(r["ID_ASSISTANTE"]);
            //com.praticien = UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["ID_PRATICIEN"]));
            com.Assistante = UtilisateursMgt.getUtilisateur(com.IdAssistante);
            com.DatePrevisionnnelle = r["DATE_COMM"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATE_COMM"]);
            com.IdPraticien = r["ID_PRATICIEN"] is DBNull ? -1 : Convert.ToInt32(r["ID_PRATICIEN"]);
            com.praticien = UtilisateursMgt.getUtilisateur(com.IdPraticien);
            com.IdSecretaire = r["ID_SECRETAIRE"] is DBNull ? -1 : Convert.ToInt32(r["ID_SECRETAIRE"]);
            com.desactive = r["DESACTIVE"] is DBNull ? false : Convert.ToBoolean(r["DESACTIVE"]);
            com.dents = r["dents"] is DBNull ? "" : Convert.ToString(r["dents"]);
            com.Secretaire = UtilisateursMgt.getUtilisateur(com.IdSecretaire);
            com.devis = MgmtDevis.getDevis_TK(Convert.ToInt32(r["ID_DEVIS"]));
           // com.devis  = MgmtDevis.getDevis_TK(r["ID_DEVIS"]);
            com.Montant  = r["MONTANT"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT"]);
            com.MontantAvantRemise   =  r["MONTANTAVANTPROPOSITION"] is DBNull ? 0 : Convert.ToDouble(r["MONTANTAVANTPROPOSITION"]);
            com.NbJours = r["NBJOURS"] is DBNull ? 0 : Convert.ToInt32(r["NBJOURS"]);
            com.partPatient = r["PARTPATIENT"] is DBNull ? 0 : Convert.ToDouble(r["PARTPATIENT"]);
            com.RembMutuelle = r["REMBMUTUELLE"] is DBNull ? 0 : Convert.ToDouble(r["REMBMUTUELLE"]);
           
            return com;
        }
    }
}
