using BasCommon_BO;
using BasCommon_BO.ElementsEnBouche.BO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BL.Builders
{
   public static class buildFullPatient
    {
       public static FamillesActe BuildFamilleActe(JObject r)
       {
           //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
           FamillesActe act = new FamillesActe();
           act.Id = Convert.ToInt32(r["id"]);
           act.libelle = r["nom"].ToString() == "" ? "" : Convert.ToString(r["nom"]).Trim();
           act.ParentFamillesActeId = Convert.ToInt32(r["parent"]);
           act.couleur = Convert.ToInt32(r["couleur"]) == 0 ? System.Drawing.Color.WhiteSmoke : System.Drawing.Color.FromArgb(Convert.ToInt32(r["couleur"]));
           act.ordre = Convert.ToInt32(r["ordre"]);
           return act;
       }
       public static FamillesMateriels BuildFamilleMateriel(JObject r)
       {
           FamillesMateriels mat = new FamillesMateriels();
           mat.Id = Convert.ToInt32(r["id"]);
           mat.libelle = r["nom"].ToString() == "" ? "" : Convert.ToString(r["nom"]).Trim();
           mat.ParentFamillesMaterielId =  Convert.ToInt32(r["parent"]);
           mat.couleur = Convert.ToInt32(r["couleur"]) == 0 ? System.Drawing.Color.WhiteSmoke : System.Drawing.Color.FromArgb(Convert.ToInt32(r["couleur"]));
           mat.ordre =Convert.ToInt32(r["ordre"]);
           return mat;
       }
       public static Acte BuildActe(JObject r)
       {
           //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
           Acte act = new Acte();
           act.id_acte = Convert.ToInt32(r["id_ACTE"]);
           act.acte_libelle =Convert.ToString(r["acte_LIBELLE"]).Trim();
           act.acte_durestd = Convert.ToInt32(r["acte_DURESTD"]);
           act.acte_couleur = Convert.ToInt32(r["acte_COULEUR"]) == 0 ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["acte_COULEUR"]));
           act.type_acte =Convert.ToInt32(r["type_ACTE"]);
           act.nb_fautbloc =  Convert.ToDouble(r["nb_FAUTBLOC"]);
           act.code_planing =  Convert.ToString(r["code_PLANNING"]).Trim();
           act.temps_chrono = Convert.ToInt32(r["temps_CHRONO"]);
           act.id_famille =Convert.ToInt32(r["id_famille_ACTE"]);
           act.tps_ass =  Convert.ToInt32(r["tps_ASS"]);
           act.tps_prat = Convert.ToInt32(r["tps_PRAT"]);


           act.prix_acte =  Convert.ToDouble(r["prix_ACTE"]);
           act.MailConfirmationAttachments =  Convert.ToString(r["mailattachement"]);
           act.MailConfirmationRDVBody = r["mailbody"].ToString() == "" ? "" : Convert.ToString(r["mailbody"]);
           act.MailConfirmationSubject = r["mailsubject"].ToString() == "" ? "" : Convert.ToString(r["mailsubject"]);
           act.acte_libelle_estimation = r["acte_LIBELLE_ESTIMATION"].ToString() == "" ? "" : Convert.ToString(r["acte_LIBELLE_ESTIMATION"]).Trim();
           act.acte_libelle_facture = r["acte_LIBELLE_FACTURE"].ToString() == "" ? "" : Convert.ToString(r["acte_LIBELLE_FACTURE"]).Trim();
           act.cotation = r["cotation"].ToString() == "" ? "0" : Convert.ToString(r["cotation"]).Trim();
           act.coefficient = Convert.ToInt32(r["coefficient"]);
           act.nomenclature = r["nomenclature"].ToString() == "" ? "" : Convert.ToString(r["nomenclature"]).Trim();
           act.nom_raccourci = r["shortlib"].ToString() == "" ? "" : Convert.ToString(r["shortlib"]).Trim();
           act.emplacement = r["id_FAUTEUIL"].ToString() == "" ? "" : Convert.ToString(r["id_FAUTEUIL"]);
           act.praticien = r["praticien"].ToString() == "" ? "" : Convert.ToString(r["praticien"]);
           act.jour = r["jours"].ToString() == "" ? "" : Convert.ToString(r["jours"]);

           act.ordre = Convert.ToInt32(r["ORDRE"]);
           act.nombre_points = r["nombre_POINTS"].ToString() == "" ? "0" : Convert.ToString(Convert.ToDouble((r["nombre_POINTS"])));
           act.quantite = r["quantite"].ToString() == "" ? "1" : Convert.ToString(r["quantite"]);
           act.heure_debut = r["heure_DEBUT"].ToString() == "" ? "00:00" : Convert.ToString(r["heure_DEBUT"]);
           act.heure_fin = r["heure_FIN"].ToString() == "" ? "00:00" : Convert.ToString(r["heure_FIN"]);
           act.acte_libelle_estimation = r["acte_LIBELLE_ESTIMATION"].ToString() == "" ? "" : Convert.ToString(r["acte_LIBELLE_ESTIMATION"]).Trim();
           act.acte_libelle_facture = r["acte_LIBELLE_FACTURE"].ToString() == "" ? "" : Convert.ToString(r["acte_LIBELLE_FACTURE"]).Trim();
           act.BaseRemboursement =  Convert.ToDouble(r["acte_BASE_REMBOURSEMENT"]);
           act.Remboursement =  Convert.ToDouble(r["acte_REMBOURSEMENT"]);
           act.Depassement = Convert.ToDouble(r["acte_DEPASSEMENT"]);
           act.CodeTransposition = r["acte_CODE_TRANSPOSOTION"].ToString() == "" ? "" : Convert.ToString(r["acte_CODE_TRANSPOSOTION"]);
           act.Tarif =Convert.ToDouble(r["TARIF"]);
           return act;
       }
       public static ActePG BuildActePG(JObject r)
       {


           ActePG apg = new ActePG();

           apg.Id =  Convert.ToInt32(r["id"]);

           apg.IdPatient = Convert.ToInt32(r["id_PATIENT"]);
           apg.IdActe =  Convert.ToInt32(r["id_ACTE"]);

           apg.DateExecution = Convert.ToDateTime(r["date_DEBUT"]);


           apg.NbJours = Convert.ToInt32(r["nb_JOURS"]) == -1 ? null : (int?)Convert.ToInt32(r["nb_JOURS"]);
           apg.NbMois = Convert.ToInt32(r["nb_MOIS"]) == -1 ? null : (int?)Convert.ToInt32(r["nb_MOIS"]);

           apg.IdPlan =  Convert.ToInt32(r["id_PLAN"]);
           apg.Template =  Convert.ToInt32(r["id_ACTEGESTION"]) == -1 ? null : TemplateApctePGMgmt.getCodeSecu(Convert.ToInt32(r["id_ACTEGESTION"]));
           apg.Libelle = r["libelle"].ToString() == "" ? "" : Convert.ToString(r["libelle"]).Trim();
           apg.prestation = Convert.ToInt32(r["id_PRESTATION"]) == -1 ? null : TemplateApctePGMgmt.getCodePrestation(Convert.ToString(r["id_PRESTATION"]).Trim());
           apg.Coeff =  Convert.ToInt32(r["coef"]);
           apg.Montant_Honoraire =Convert.ToDouble(r["montant"]);
           apg.NeedFSE =Convert.ToBoolean(Convert.ToInt32(r["need_FSE"]));
           apg.NeedDEP = Convert.ToBoolean(Convert.ToInt32(r["need_DEP"]));
           try
           {
               apg.IsDecomposed =  Convert.ToBoolean(Convert.ToInt32(r["isdecomposed"]));
           }
           catch (Exception e)
           {
               apg.IsDecomposed = Convert.ToBoolean(Convert.ToString(r["isdecomposed"]));
           }
           apg.CoeffDecompose = r["decomposed"].ToString() == "" ? "" : Convert.ToString(r["decomposed"]);

           apg.NumSemestre = Convert.ToInt16(r["num_SEMESTRE"]);
           apg.NumContention = Convert.ToInt16(r["num_Contention"]);



           apg.motifdepassement = (PyxVitalWrapperConst.Qualificatif_depense)Convert.ToInt32(r["motifdepassement"]);
           apg.domicile =  ((PyxVitalWrapperConst.Domicile)Convert.ToInt32(r["lieuexecution"]));
           apg.Quantite =  Convert.ToInt32(r["nbkilometre"]);
           //apg.rembExceptionel = r[""] is DBNull ? (PyxVitalWrapperConst.RembExceptionel.N) : Convert.ToInt32(r["nbkilometre"]);
           //apg.suplCharge = r[""] is DBNull ? (PyxVitalWrapperConst.SuplCharge.N) : Convert.ToInt32(r["nbkilometre"]);

           apg.rno = (PyxVitalWrapperConst.RNO)Convert.ToInt32(r["rno"]);
           apg.nuit = (PyxVitalWrapperConst.Nuit)Convert.ToInt32(r["nuit"]);
           //apg.urgence = r[""] is DBNull ? (PyxVitalWrapperConst.Urgence.N) : Convert.ToInt32(r[""]);
           apg.DimancheEtJF = (PyxVitalWrapperConst.DimancheEtJF)Convert.ToInt32(r["dimancheetjf"]);
           apg.ald = (PyxVitalWrapperConst.ALD)Convert.ToInt32(r["ald"]);
           apg.Exoneration =  (PyxVitalWrapperConst.Exoneration)Convert.ToInt32(r["exoneration"]);

           //apg.ExonerationLibOuTx = r[""] is DBNull ? "" : Convert.ToInt32(r[""]);

           //apg.DateDEP = r[""] is DBNull ? null : Convert.ToInt32(r[""]);
           apg.Id_DEP = Convert.ToInt32(r["id_DEP"]);
           apg.Id_FS =  Convert.ToInt32(r["id_FS"]);

           //apg.CodeAccordDEP = r["codeaccentente"] is DBNull ? (PyxVitalWrapperConst.CodeAccordDEP.Ac0) : (PyxVitalWrapperConst.CodeAccordDEP)Convert.ToInt32(r["codeaccentente"]);

           apg.accident =  (PyxVitalWrapperConst.Accident)Convert.ToInt32(r["accident"]);
           apg.DateAccident = r["dateaccident"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateaccident"]);

           apg.NumMutuelle = r["nummutuelle"].ToString() == "" ? "" : Convert.ToString(r["nummutuelle"]);
           apg.ActeCMU = (PyxVitalWrapperConst.CMU)Convert.ToInt32(r["actecmu"]);

           apg.numdent = r["numdent"].ToString() == "" ? "" : Convert.ToString(r["numdent"]);
           apg.IdSemestrePlanGestionAssocie = Convert.ToInt32(r["IDSEM_PTA"]);
           apg.IdSurvPlanGestionAssocie =  Convert.ToInt32(r["IDSURV_PTA"]);
           apg.IdDevisAssociate =  Convert.ToInt32(r["IDDEVIS_PTA"]);
           apg.IdComm =Convert.ToInt32(r["ID_COMM"]);
           apg.Facturee = Convert.ToInt32(r["FACTUREE"]);
           apg.Id_facture =  Convert.ToInt32(r["ID_FACTURE"]);
           apg.TypeActe = r["type_COMMENT"].ToString() == "" ? "0" : Convert.ToString(r["type_COMMENT"]);
           apg.rabais = Convert.ToDouble(r["RABAIS"]);
           return apg;
       }

       public static TempEcheanceDefinition BuildTmpEcheance(JObject r)
       {
           TempEcheanceDefinition ted = new TempEcheanceDefinition();

           ted.Id = Convert.ToInt32(r["id"]);
           ted.AlreadyPayed = false;
           ted.CanRecalculate = true;
           ted.DAteEcheance = r["dteecheance"].ToString() == "" ? DateTime.Now.AddYears(1) : Convert.ToDateTime(r["dteecheance"]);
           ted.Libelle = Convert.ToString(r["libelle"]);
           ted.ParPrelevement = Convert.ToString(r["parprelevement"]) == "True";
           ted.ParVirement = Convert.ToString(r["parvirement"]) == "True";

           ted.payeur = (Echeance.typepayeur)Convert.ToInt32(r["typepayeur"]);
           ted.Montant = Convert.ToDouble(r["montant"]);
           ted.IdSemestre = Convert.ToInt32(r["id_SEM_PROPOSE"]);

           return ted;
       }
       public static CommAutrePersonne BuildDevisAutrePersonne(JObject r)
       {
           CommAutrePersonne com = new CommAutrePersonne();
           com.IdCorrespondant = Convert.ToInt32(r["id_CORRESPONDANT"]);
           com.Nom = Convert.ToString(r["per_NOM"]).Trim();
           com.Prenom = Convert.ToString(r["per_PRENOM"]).Trim();
           com.IdComm = Convert.ToInt32(r["id_COMM_DEVIS"]);

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
       public static CommTraitement BuildCommTraitement(JObject r)
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


           com.IdAssistante = Convert.ToInt32(r["id_ASSISTANTE"]);
           //com.praticien = UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["ID_PRATICIEN"]));
           com.Assistante = UtilisateursMgt.getUtilisateur(com.IdAssistante);

           /// a fixer
           com.DatePrevisionnnelle = r["date_COMM"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["date_COMM"]);
           com.IdPraticien = Convert.ToInt32(r["id_PRATICIEN"]);
           //  com.active = r["ISACTIVE"] is DBNull ? false : Convert.ToBoolean(r["ISACTIVE"]);
           com.IdSecretaire =  Convert.ToInt32(r["id_SECRETAIRE"]);
           com.Secretaire = UtilisateursMgt.getUtilisateur(com.IdSecretaire);
           com.praticien = UtilisateursMgt.getUtilisateur(com.IdPraticien);

           com.Acte.prix_traitement = Convert.ToDouble(r["prix_TRAITEMENT"]);
           com.Acte.prix_acte = Convert.ToDouble(r["prix_ACTE"]);
           com.Acte.quantite = r["qte"].ToString() == "" ? "1" : Convert.ToString(r["qte"]);
           
              com.ActesSupp = new List<CommActesTraitement>();
           com.Radios = new List<CommActesTraitement>();
           com.photos = new List<CommActesTraitement>();
           com.Materiels = new List<CommMaterielTraitement>();
        /*   JToken actesupp = r.GetValue("commActesTraitement");
           foreach (JToken j in actesupp)
           {
               JObject obActS = JObject.Parse(j.ToString());
               CommActesTraitement ac = BuildCommActeSuppDevis(obActS);
               switch(obActS.GetValue("type_ACTE").ToString())
               {
                   case "" : com.ActesSupp.Add(ac); break;
                   case "R": com.Radios.Add(ac); break;
                   case "P": com.photos.Add(ac); break;
               }
           }
           JToken mats = r.GetValue("commMatsTraitements");
           foreach (JToken j in mats)
           {
               JObject obActS = JObject.Parse(j.ToString());
               CommMaterielTraitement ac = BuildDevisMateriel(obActS);

           }  */       
           com.NbJours = Convert.ToInt32(r["nbjours"]);
           com.echeancestemp = new List<TempEcheanceDefinition>();
           return com;
       }
           
       public static CommActesTraitement BuildCommActeSuppDevis(JObject r)
       {

           CommActesTraitement com = new CommActesTraitement();
           com.IdActe =  Convert.ToInt32(r["id_ACTE"]);
           com.LibActe = Convert.ToString(r.GetValue("acte")["acte_LIBELLE"]).Trim();
           com.acte_couleur = Convert.ToInt32(r.GetValue("acte")["acte_COULEUR"]) == 0 ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r.GetValue("acte")["acte_COULEUR"]));
           com.prix_acte = Convert.ToDouble(r.GetValue("acte")["prix_ACTE"]);
           com.acte_durestd = Convert.ToInt32(r.GetValue("acte")["acte_DURESTD"]);
           com.prix_traitement =  Convert.ToDouble(r["montant"]);
           com.Qte =  Convert.ToInt32(r["qte"]);
           com.desactive = r["desactive"].ToString() == ""  ? false : Convert.ToBoolean(r["desactive"]);
           com.cotation = Convert.ToString(r.GetValue("acte")["cotation"]).Trim();
           com.coefficient = Convert.ToInt32(r.GetValue("acte")["coefficient"]);
           com.nomenclature = Convert.ToString(r.GetValue("acte")["nomenclature"]).Trim();

           com.nombre_points = r.GetValue("acte")["nombre_POINTS"].ToString() == "" ? "0" : Convert.ToString(r.GetValue("acte")["nombre_POINTS"]);

           com.LibActe_estimation = Convert.ToString(r.GetValue("acte")["acte_LIBELLE_ESTIMATION"]).Trim();

           com.LibActe_facture = Convert.ToString(r.GetValue("acte")["acte_LIBELLE_FACTURE"]).Trim();
           com.Depassement = Convert.ToDouble(r.GetValue("acte")["ACTE_DEPASSEMENT"]);
           com.CodeTransposition = Convert.ToString(r.GetValue("acte")["acte_CODE_TRANSPOSOTION"]);
           com.BaseRemboursement = Convert.ToDouble(r.GetValue("acte")["acte_BASE_REMBOURSEMENT"]);
           com.Tarif = Convert.ToDouble(r.GetValue("acte")["tarif"]);
           com.Remboursement = Convert.ToDouble(r.GetValue("acte")["acte_REMBOURSEMENT"]);
           com.partPatient = Convert.ToDouble(r.GetValue("acte")["parpatient"]);
           com.RembMutuelle = Convert.ToDouble(r.GetValue("acte")["REMBMUTUELLE"]);


           return com;
       }
       public static Devis BuildDevis(JObject r)
       {

           Devis devis = new Devis();
           devis.Id = Convert.ToInt32(r["id"]);
           devis.DateProposition = Convert.ToDateTime(r["dateproposition"]);
           devis.DatePrevisionnelDeDebutTraitement = r["datedebuttraitement"].ToString() == ""  ? DateTime.Now.AddDays(15) : Convert.ToDateTime(r["datedebuttraitement"]);
           devis.DatePrevisionnelDeFinTraitement = r["datefintraitement"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["datefintraitement"]);

           devis.DateAcceptation = r["dateacceptation"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateacceptation"]);
           devis.DateEcheance = r["dateecheance"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateecheance"]);
           devis.DateArchivage = r["datearchivage"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["datearchivage"]);
           devis.ArchivePar = Convert.ToInt32(r["archivepar"]) == 0 ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["archivepar"]));
           devis.RemarqueArchivage = r["remarquearchivage"].ToString() == "" ? "" : Convert.ToString(r["remarquearchivage"]);
           devis.EmplacementArchivage = r["emplacementarchivage"].ToString() == "" ? "" : Convert.ToString(r["emplacementarchivage"]);
           devis.TypeDevis = Convert.ToInt32(r["typeDevis"]) == 0 ? Devis.enumtypePropositon.Aucun : (Devis.enumtypePropositon)Convert.ToInt32(r["typeDevis"]);

           devis.Montant =  (double?)Convert.ToDouble(r["montantpropose"]);
           devis.MontantAvantRemise = (double?)Convert.ToDouble(r["montantavantproposition"]);


           devis.IdPatient = Convert.ToInt32(r["id_PATIENT"]);
           devis.IdObjetBaseView = Convert.ToInt32(r["id_OBJET_BASEVIEW"]);

           return devis;
       }
       public static Devis_TK Build_TK(JObject r)
       {

           Devis_TK devis = new Devis_TK();
           devis.Id = Convert.ToInt32(r["id"]);

           devis.DateProposition = Convert.ToDateTime(r["dateproposition"]);
           devis.DatePrevisionnelDeDebutTraitement = r["datedebuttraitement"].ToString() == "" ? DateTime.Now.AddDays(15) : Convert.ToDateTime(r["datedebuttraitement"]);
           devis.DatePrevisionnelDeFinTraitement = r["datefintraitement"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["datefintraitement"]);

           devis.DateAcceptation = r["dateacceptation"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateacceptation"]);
           devis.DateEcheance = r["dateecheance"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateecheance"]);
           devis.DateArchivage = r["datearchivage"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["datearchivage"]);
           devis.ArchivePar = Convert.ToInt32(r["archivepar"]) == 0  ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["archivepar"]));
           devis.RemarqueArchivage = r["remarquearchivage"].ToString() == "" ? "" : Convert.ToString(r["remarquearchivage"]);
           devis.EmplacementArchivage = r["emplacementarchivage"].ToString() == "" ? "" : Convert.ToString(r["emplacementarchivage"]);

           devis.Montant =  (double?)Convert.ToDouble(r["montantpropose"]);
           devis.MontantAvantRemise = (double?)Convert.ToDouble(r["montantavantproposition"]);


           devis.IdPatient = Convert.ToInt32(r["id_PATIENT"]);
           devis.Id_Traitement =Convert.ToInt32(r["id_TRAITEMENT"]);
           devis.Traitement = TraitementsMgmt.Traitements.Find(w=>w.id_Traitement == devis.Id_Traitement);
         /*  NewTraitement TmpTraitement = new NewTraitement();
           if (r.GetValue("traitement").ToString() != "")
           {
               TmpTraitement = BuildNewTraitement(JObject.Parse(r.GetValue("traitement").ToString()));            
               devis.Traitement = TmpTraitement;
           }*/
           JToken CommTraitement = r.GetValue("commTraitements");
           devis.actesTraitement = new List<CommTraitement>();
           foreach (JToken t in CommTraitement)
           {
               JObject j = JObject.Parse(t.ToString());
               CommTraitement tmpCom = BuildCommTraitement(j);
               tmpCom.devis = devis;
               devis.actesTraitement.Add(tmpCom);
           }
           devis.MontantDocteur = (double?)Convert.ToDouble(r["montantdocteur"]);
           devis.Titre = Convert.ToString(r["titre"]);
           devis.EcheancierDocteur = Convert.ToInt32(r["echeancier_DOCTEUR"]);
           devis.partPatient =  Convert.ToInt32(r["parpatient"]);
           devis.RembMutuelle = Convert.ToInt32(r["rembmutuelle"]);
           devis.localisationAnatomiuque = r["localisation"].ToString() == "" ? "" : Convert.ToString(r["localisation"]);
           return devis;
       }
       public static NewTraitement BuildNewTraitement(JObject r)
       {
           NewTraitement trt = new NewTraitement();
           trt.id_Traitement = Convert.ToInt32(r["id_TRAITEMENT"]);
           trt.Traitement_libelle = r["traitement_LIBELLE"].ToString() == "" ? "" : Convert.ToString(r["traitement_LIBELLE"]).Trim();
           trt.Traitement_couleur = Convert.ToInt32(r["traitement_COULEUR"]) == 0 ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["traitement_COULEUR"]));
           trt.id_famille =  Convert.ToInt32(r["id_FAMILLE_TRAITEMENT"]);

           trt.Montant_Scenario = Convert.ToDouble(r["MONTANTSCENARIO"]);
           trt.TypeScenario = Convert.ToInt32(r["TYPE"]) == -1 ? NewTraitement.typeScenario.Prothése :  (NewTraitement.typeScenario)Convert.ToInt32(r["TYPE"]);
           trt.contention = r["contention"].ToString() == "" ? false : Convert.ToBoolean(r["contention"]);
           trt.Traitement_shortlib = r["shortlib"].ToString() == "" ? "" : Convert.ToString(r["shortlib"]).Trim();

           trt.ordre = Convert.ToInt32(r["ordre"]);     
           return trt;
       }

       public static CommClinique BuildCommCliniqueJson(JObject r)
       {
           double prix = 0;
           CommClinique com = new CommClinique();


           com.Id = Convert.ToInt32(r.GetValue("id"));
           com.IdPraticien = Convert.ToInt32(r.GetValue("id_PRATICIEN"));
           com.IdAssistante = Convert.ToInt32(r.GetValue("id_ASSISTANTE"));
           com.IdSecretaire = Convert.ToInt32(r.GetValue("id_SECRETAIRE"));
           com.IdActe = Convert.ToInt32(r.GetValue("id_ACTE"));
           com.Hygiene = Convert.ToInt32(r.GetValue("hygiene"));
           com.IdRDV = Convert.ToInt32(r.GetValue("id_RDV"));
           com.IdPatient = Convert.ToInt32(r.GetValue("id_PATIENT"));
            com.date = r.GetValue("date_COMM").ToString() == String.Empty ? null : (DateTime?)Convert.ToDateTime(r.GetValue("date_COMM"));
           com.Commentaire = Convert.ToString(r.GetValue("commentaires"));
           com.CommentaireAFaire = Convert.ToString(r.GetValue("commentairesafairr"));
           com.NbJours = Convert.ToInt32(r.GetValue("nbjours"));
           com.NbMois = Convert.ToInt32(r.GetValue("nbmois"));
           com.IdScenario = Convert.ToInt32(r.GetValue("id_SCENARIO"));
           com.etat = r.GetValue("etat") == null ? (com.date == null ? CommClinique.EtatCommentaire.Prevus : CommClinique.EtatCommentaire.Termine) : (CommClinique.EtatCommentaire)Convert.ToInt32(r.GetValue("etat"));
           com.DatePrevisionnnelle = r.GetValue("dateprevisionnelle") == null ? null : (DateTime?)Convert.ToDateTime(r.GetValue("dateprevisionnelle"));
           try
           {
               com.desactive = r["desactive"] == null ? false : Convert.ToBoolean(Convert.ToInt32(r["desactive"]));
           }
           catch (Exception e)
           {
               com.desactive = r["DESACTIVE"] == null ? false : Convert.ToBoolean(Convert.ToString(r["DESACTIVE"]));
           }
           com.IdSemestre = Convert.ToInt32(r.GetValue("id_SEMESTRE"));
           com.IdParentComment = Convert.ToInt32(r.GetValue("id_PARENTCOMMENT"));
           com.IsDateDeRef = Convert.ToString(r.GetValue("isdatederef")) == "T" || Convert.ToString(r.GetValue("isdatederef")) == "Y";
           com.modecreation = r.GetValue("modecreation") == null ? CommClinique.ModeCreation.Auto : (CommClinique.ModeCreation)Convert.ToInt32(r.GetValue("modecreation"));
           com.rabais = Convert.ToInt32(r.GetValue("rabais"));

           ///a fixer aussi 
           ///

           JToken tmpEcheance = r.GetValue("tmpEcheance");
           com.echeancestemp = new List<TempEcheanceDefinition>();
           if(tmpEcheance != null)
           foreach (JToken t in tmpEcheance)
           {
               JObject j = JObject.Parse(t.ToString());
               com.echeancestemp.Add(BuildTmpEcheance(j));
           }
          // com.echeancestemp =    MgmtDevis.get_tempecheancescc_TK(com);

           foreach (BaseTempEcheanceDefinition bted in com.echeancestemp)
           {
               prix = prix + bted.Montant;
           }
           com.prix = prix;
           com.praticien = UtilisateursMgt.getUtilisateur(com.IdPraticien);
           com.Assistante = UtilisateursMgt.getUtilisateur(com.IdAssistante);
           //com.Acte = (Acte)BasCommon_BL.ActesMgmt.getActe(com.IdActe).Clone();
           JToken Acte = r.GetValue("acte");
           com.Acte = BuildActe(JObject.Parse(Acte.ToString()));
           //com.Acte.prix_acte = r["MONTANTAVANTREMISE"] is DBNull ? 0 : Convert.ToDouble(r["MONTANTAVANTREMISE"]);
           //       com.ActesSupp = MgmtCommentairesFaitAFaire.GetActesSupp_tk(com.Id,com.IdPatient, "");
           // com.Radios = MgmtCommentairesFaitAFaire.GetActesSupp_tk(com.Id, com.IdPatient, "R");
           JToken tmpCommActe = r.GetValue("commActes");
           if (tmpCommActe == null)
           {
               com.photos = new List<CommActes>();
               com.Radios = new List<CommActes>();
               com.ActesSupp = new List<CommActes>();
           }
           else
               foreach (JToken t in tmpCommActe)
               {
                   JObject j = JObject.Parse(t.ToString());
                   switch (j.GetValue("type_ACTE_SUPP").ToString())
                   {
                       case "P":
                           com.photos.Add(BuildCommActesJson(j));
                           break;
                       case "R": com.Radios.Add(BuildCommActesJson(j));
                           break;
                       case "": com.ActesSupp.Add(BuildCommActesJson(j));
                           break;
                   }
               }

           /* MgmtCommentairesFaitAFaire.GetActesSupp(com, "P");
            MgmtCommentairesFaitAFaire.GetActesSupp(com, "R");
            MgmtCommentairesFaitAFaire.GetActesSupp(com);*/

           /////ne pas utiliser a verifier aprés 
           /*
           MgmtCommentairesFaitAFaire.GetCommCliniqueAutrePersonne(com);
           MgmtCommentairesFaitAFaire.GetCommDentAExtraire(com);*/
           com.prix_traitement = Convert.ToDouble(r.GetValue("montant"));
           com.Acte.quantite = Convert.ToString(r.GetValue("qte_ACTE"));
           com.echeance =r.GetValue("echeance").ToString() == "" ? 0 : Convert.ToInt32(r.GetValue("echeance"));
           com.idTour = Convert.ToInt32(r.GetValue("id_tour"));
          // JToken tour = r.GetValue("tour");
           //if(tour.ToString() != "")
           //com.tour = BuildAutre(JObject.Parse(tour.ToString()));
           com.idTim = Convert.ToInt32(r.GetValue("id_tim"));
           com.tim = AutreMgmt.tim.Find(w => w.id == com.idTim);
           //JToken timCL = r.GetValue("timCL");
           //if (timCL.ToString() != "")
           //    com.tim = BuildAutre(JObject.Parse(timCL.ToString()));
           com.idChgt = Convert.ToInt32(r.GetValue("id_chgt"));
           com.chgt = AutreMgmt.chght.Find(w => w.id == com.idChgt);
           //JToken chgt = r.GetValue("chght");
           //if (chgt.ToString() != "")
           //    com.chgt = BuildAutre(JObject.Parse(chgt.ToString()));
           com.idNumLogiciel = Convert.ToInt32(r.GetValue("idnumlogiciel"));
           com.numLogiciel = AutreMgmt.numLogiciel.Find(w => w.id == com.idNumLogiciel);
          // JToken numLogiciel = r.GetValue("numLogiciel");
           //if (numLogiciel.ToString() != "")
           //    com.numLogiciel = BuildAutre(JObject.Parse(numLogiciel.ToString()));
           com.idDroit = Convert.ToInt32(r.GetValue("id_droit"));
           com.droit = AutreMgmt.droit.Find(w => w.id == com.idDroit);
           //JToken timDroit = r.GetValue("timDroit");
           //if (timDroit.ToString() != "")
           //    com.droit = BuildAutre(JObject.Parse(timDroit.ToString()));
           com.idGauche = Convert.ToInt32(r.GetValue("id_gauche"));
           com.gauche = AutreMgmt.gauche.Find(w => w.id == com.idGauche);
           //JToken timGauche = r.GetValue("timGauche");
           //if (timGauche.ToString() != "")
           //    com.gauche = BuildAutre(JObject.Parse(timGauche.ToString()));
           com.idBlanchiment = Convert.ToInt32(r.GetValue("id_blanchiment"));
           com.idDiaporama = Convert.ToInt32(r.GetValue("id_diaporama"));
           com.diaporama = AutreMgmt.diaporama.Find(w => w.id == com.idDiaporama);
           //JToken diaporama = r.GetValue("diaporama");
           //if (diaporama.ToString() != "")
           //    com.diaporama = BuildAutre(JObject.Parse(diaporama.ToString()));
           com.idAccelerateur = Convert.ToInt32(r.GetValue("id_accelerateur"));
           com.Accelerateur = AutreMgmt.accelerateur.Find(w => w.id == com.idAccelerateur);
           //JToken accelerateur = r.GetValue("accelerateur");
           //if (accelerateur.ToString() != "")
           //    com.Accelerateur = BuildAutre(JObject.Parse(accelerateur.ToString()));
           com.Vu = Convert.ToString(r.GetValue("vu")).Trim();
           com.Donne = Convert.ToString(r.GetValue("donne")).Trim();
           JToken tmpCommMats = r.GetValue("commMateriels");
           com.Materiels = new List<CommMateriel>();
           if (tmpCommMats != null)
               foreach (JToken t in tmpCommMats)
               {
                   JObject j = JObject.Parse(t.ToString());
                   com.Materiels.Add(BuildCommMaterielJson(j));
               }
           com.DentsAExtraire = new List<CommDentAExtraire>();
           com.AutrePersonnes = new List<CommAutrePersonne>();
           //MgmtCommentairesFaitAFaire.GetMateriels(com);
           return com;
       }
       public static CommMateriel BuildCommMaterielJson(JObject r)
       {
           CommMateriel com = new CommMateriel();

           com.IdBaseProduit = Convert.ToInt32(r["id_BASEPRODUIT"]);
           com.Libelle = Convert.ToString(r.GetValue("materiel")["materiel_LIBELLE"]).Trim();
           com.Qte = Convert.ToInt32(r["qte"]);
           com.ShortLib = Convert.ToString(r.GetValue("materiel")["shortlib"]).Trim();
           com.IdComm = Convert.ToInt32(r["id_COMM"]);
           com.idMateriel = Convert.ToInt32(r["id_MATERIEL"]);
           com.materiel_couleur = Convert.ToInt32(r.GetValue("materiel")["materiel_couleur"]) == 0 ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r.GetValue("materiel")["materiel_couleur"]));
           com.prix_materiel = Convert.ToDouble(r["montantavantremise"]);
           com.prix_materiel_traitement = Convert.ToDouble(r["montant"]);
           com.echeancestemp = Convert.ToInt32(r["echance"]);
           try
           {
               com.desactive = r["desactive"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToString(r["desactive"]));
           }
           catch (Exception e)
           {
               com.desactive = r["desactive"].ToString() == ""? false : Convert.ToBoolean(Convert.ToInt32(r["desactive"]));
           }
           com.idencaissement = Convert.ToInt32(r["id_ENCAISSEMENT"]);
           com.rabais = Convert.ToInt32(r["rabais"]);
           return com;
       }
       public static CommActes BuildCommActesJson(JObject r)
       {
           CommActes com = new CommActes();

           com.IdActe = Convert.ToInt32(r["id_acte"]);


           com.IdComm = Convert.ToInt32(r.GetValue("id_COMM"));
           com.ShortLib = Convert.ToString(r.GetValue("acte")["shortlib"]);
           com.LibActe = Convert.ToString(r.GetValue("acte")["acte_LIBELLE"]);
           com.acte_couleur = r.GetValue("acte")["acte_COULEUR"] == null ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r.GetValue("acte")["acte_COULEUR"]));
           com.acte_durestd = Convert.ToInt32(r.GetValue("acte")["acte_DURESTD"]);
           com.Qte = Convert.ToInt32(r["qte"]);
           com.prix_acte = Convert.ToDouble(r["montantavantremise"]);
           com.prix_traitement = Convert.ToDouble(r["montant"]);
           com.LibActe_facture = Convert.ToString(r.GetValue("acte")["acte_LIBELLE_FACTURE"]);
           com.LibActe_estimation = Convert.ToString(r.GetValue("acte")["acte_LIBELLE_ESTIMATION"]);
           try
           {
               com.desactive = r["desactive"].ToString() == String.Empty ? false : Convert.ToBoolean(Convert.ToInt32(r["desactive"]));
           }catch(Exception e)
           {
                 com.desactive = r["desactive"].ToString() == String.Empty ? false : Convert.ToBoolean(Convert.ToString(r["desactive"]));
           }
           com.cotation = Convert.ToString(r.GetValue("acte")["cotation"]).Trim();
           com.coefficient = Convert.ToInt32(r.GetValue("acte")["coefficient"]);
           com.nomenclature = Convert.ToString(r.GetValue("acte")["nomenclature"]).Trim();
           com.rabais = Convert.ToInt32(r["rabais"]);
           com.nombre_points = Convert.ToString(r.GetValue("acte")["nombre_POINTS"]);
           com.echeancestemp = Convert.ToInt32(r["echeance"]);
           // com.idencaissement = r["ID_ENCAISSEMENT"] is DBNull ? 0 : Convert.ToInt32(r["ID_ENCAISSEMENT"]);

           return com;
       }
       public static Proposition BuildProposition(JObject r)
       {

           Proposition proposition = new Proposition();
           proposition.Id = Convert.ToInt32(r["id"]);
           proposition.Etat = (Proposition.EtatProposition)Convert.ToInt32(r["etat"]);
           proposition.DateEvenement = r["dateevent"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateevent"]);
           proposition.libelle = Convert.ToString(r["libelle"]);
           proposition.DateAcceptation = r["date_ACCEPTATION"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["date_ACCEPTATION"]);
           proposition.IdModel = Convert.ToInt32(r["id_MODELE"]);
           proposition.DateProposition = r["date_PROPOSITION"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(r["date_PROPOSITION"]);
           proposition.IdScenario = Convert.ToInt32(r["idscenario"]);
           proposition.IdDevis = Convert.ToInt32(r["iddevis"]);

           return proposition;
       }
       public static basePatient BuildPatient(JObject r)
       {
           if (r == null) return null;
           basePatient Pat = new basePatient();
           Pat.Id = Convert.ToInt32(r.GetValue("personne")["id_personne"]);
           Pat.Civilite = Convert.ToString(r.GetValue("personne")["pers_titre"]);
           Pat.Genre = (Convert.ToString(r.GetValue("personne")["per_genre"]) == "M") ? basePatient.Sexe.Masculin : basePatient.Sexe.Feminin;
           Pat.Nom = Convert.ToString(r.GetValue("personne")["per_nom"]).Trim();
           Pat.NomJF = Convert.ToString(r.GetValue("personne")["per_nomjf"]).Trim();

           Pat.IOlogin = r.GetValue("personne")["oi_login"].ToString() == "" ? "" : Convert.ToString(r.GetValue("personne")["oi_login"]).Trim();
           Pat.password = r.GetValue("personne")["oi_mdp"].ToString() == "" ? "" : Convert.ToString(r.GetValue("personne")["oi_mdp"]).Trim();
           Pat.publication = Convert.ToBoolean(r.GetValue("personne")["oi_autorisation"]);
           Pat.Idprofile = Convert.ToInt32(r.GetValue("personne")["oi_profil"]);
           Pat.BasePracticeState = (basePatient.BasePracticeStateEnum)Convert.ToInt32(r.GetValue("personne")["test_pb"]);


           Pat.Prenom = Convert.ToString(r.GetValue("personne")["per_prenom"]).Trim();
           Pat.DateNaissance = r.GetValue("personne")["per_datnaiss"].ToString() == "" ?  DateTime.MinValue : Convert.ToDateTime(r.GetValue("personne")["per_datnaiss"]) ;
           Pat.Dossier = Convert.ToInt32(r["pat_numdossier"]);
           Pat.Moulage = Convert.ToString(r["num_moulage"]);
           Pat.NumSecu = r.GetValue("personne")["per_secu"].ToString() == "" ? "" : Convert.ToString(r.GetValue("personne")["per_secu"]);


           try
           {
               Pat.CodeBanque = r["code_banque"].ToString() == "" ? "00000" : Convert.ToString(r["code_banque"]).Trim();
               Pat.CodeGuichet = r["code_guichet"].ToString() == "" ? "00000" : Convert.ToString(r["code_guichet"]).Trim();
               Pat.NumCompte = r["num_compte"].ToString() == "" ? "00000000000" : Convert.ToString(r["num_compte"]).Trim();
               Pat.CleRIB = r["cle_rib"].ToString() == "" ? "00" : Convert.ToString(r["cle_rib"]).Trim();
               Pat.NomBanque = r["nom_banque"].ToString() == "" ? "" : Convert.ToString(r["nom_banque"]).Trim();
               Pat.Titulaire = r["titulaire"].ToString() == "" ? "" : Convert.ToString(r["titulaire"]).Trim();

           }
           catch (System.Exception) { }

           Pat.Profession = Convert.ToString(r.GetValue("personne")["profession"]).Trim();
           Pat.Tutoiement = r.GetValue("personne")["tuvous"].ToString() == "" ? true : Convert.ToBoolean(r.GetValue("personne")["tuvous"]);

           if (!(r["allergie"].ToString() == ""))
           {
               System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
               Pat.Allergie =r["allergie"].ToString() == ""  ? null :enc.GetString((byte[])r["allergie"]);
           }
           Pat.CasierInvisalign = Convert.ToString(r["pat_refdossier"]).Trim();
           Pat.RefArchive = Convert.ToString(r["refarchive"]).Trim();
           Pat.numMoulage = Convert.ToString(r["num_moulage"]).Trim();

           Pat.Diagnostic = Convert.ToString(r["pat_diag"]);
           Pat.Traitement = Convert.ToString(r["pat_plan"]);
           Pat.Objectif = Convert.ToString(r["pat_objectif_trait"]);
           Pat.CommentApparreil = Convert.ToString(r["pat_appareil"]);
           Pat.PourcentageMutuelle =r["percentagemutuelle"].ToString() == "" ? 0 : (int?)Convert.ToInt32(r["percentagemutuelle"]);


           Pat.Notes = Convert.ToString(r.GetValue("personne")["per_notes"]);
           Pat.PrefCom = r.GetValue("personne")["pref_com"].ToString() == "" ? Correspondant.EnumPrefCom.Courrier : (Correspondant.EnumPrefCom)Convert.ToString(r.GetValue("personne")["pref_com"])[0];
           Pat.statusClinique = r["statusclinique"].ToString() == "" ? basePatient.StatusClinique.Inconnue : (basePatient.StatusClinique)Convert.ToInt32(r["statusclinique"]);
           Pat.DateAbandon = r["dateabandon"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateabandon"]);


           Pat.statusManuel = StatusCliniqueManuelMgmt.GetStatus(Convert.ToInt32(r["id_STATUT"]));

           return Pat;
       }
       public static InfoPatientComplementaire BuildInfoComp(JObject r)
       {

           InfoPatientComplementaire nfocmpl = new InfoPatientComplementaire();
           nfocmpl.IdPatient = Convert.ToInt32(r["idpatient"]);
           nfocmpl.AssistanteResponsable =r["assistante_resp"].ToString() == "" ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["assistante_resp"]));

           nfocmpl.PraticienResponsable = r["praticien_resp"].ToString() == "" ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["praticien_resp"]));
           try
           {
               nfocmpl.PraticienUnique = r["praticien_unique"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToString(r["praticien_unique"]));
           }
           catch (Exception e)
           {
               nfocmpl.PraticienUnique = r["praticien_unique"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToInt32(r["praticien_unique"]));
           }
           try
           {
               nfocmpl.AssistanteUnique = r["assistante_unique"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToInt32(r["assistante_unique"]));
           }
           catch (Exception e)
           {
               nfocmpl.AssistanteUnique = r["assistante_unique"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToString(r["assistante_unique"]));
           }
           nfocmpl.NbSemestresEntame = r["semestresentames"].ToString() == "" ? 0 : Convert.ToInt32(r["semestresentames"]);
           nfocmpl.Ameliorations = r["ameliorations"].ToString() == "" ? "" : Convert.ToString(r["ameliorations"]);
           nfocmpl.DateDebutTraitement = r["debuttraitementenvisage"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["debuttraitementenvisage"]);


           return nfocmpl;
       }
       public static RHAppointment BuildAppJson(JObject r)
       {
           RHAppointment App = new RHAppointment();
           App.Id = Convert.ToInt32(r["id_rdv"]);




           App.StartDate = Convert.ToDateTime(r["rdvDate"]);

           App.DateArrive = r["rdvArrivee"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["rdvArrivee"]);
           App.DateFauteuil = r["heureFauteuil"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["heureFauteuil"]);
           App.DateSecretariat = r["heureSecretariat"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["heureSecretariat"]);
           App.DateSortie = r["heureSorti"] .ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["heureSorti"]);
           App.Localisation = r["localisation"].ToString() == "" ? RHAppointment.EnumLocalisation.Aucune : (RHAppointment.EnumLocalisation)Convert.ToInt16(r["localisation"]);
           App.Status =  r["rdvStatut"].ToString() == "" ? RHAppointment.EnumStatus.Attendu : (RHAppointment.EnumStatus)Convert.ToInt16(r["rdvStatut"]);
           // a fixer
           //if (r["NextRDV"] == DBNull.Value)
           //    App.NextRDV = null;
           //else
           //    App.NextRDV = (DateTime?)Convert.ToDateTime(r["NextRDV"]);


           App.IdCommClinique =  Convert.ToInt32(r["idCommClinique"]);
           App.idNextact =r["idNextActe"].ToString() == "" ? -1 : Convert.ToInt32(r["idNextActe"]);
           App.EndDate = App.StartDate.AddMinutes(Convert.ToInt32(r["rdv_duree"]));


           App.acte = ActesMgmt.Actes.Find(a => a.id_acte == Convert.ToInt32(r["id_acte"]));
               //ActesMgmt.getActe(Convert.ToInt32(r["id_acte"]));

           App.Resource = Fauteuilsmgt.getfauteuil(Convert.ToInt32(r["id_fauteuil"]));
           App.FauteuilReel =  r["fautUtilise"].ToString() == "" ? null : Fauteuilsmgt.getfauteuil(Convert.ToInt32(r["fautUtilise"]));


           App.IdPatient = Convert.ToInt32(r["id_personne"]);
   
           App.Comment = r["rdvComm"].ToString() == "" ? "" : Convert.ToString(r["rdvComm"]);

           return App;
       }
       public static Echeance BuildEcheance(JObject r)
       {

           /*
            string selectQuery = "select base_echeance.ID, ";
               selectQuery += "        base_echeance.ID_TRAITEMENT, ";
               selectQuery += "        base_echeance.ID_TRAITEMENT, ";
               selectQuery += "        base_echeance.MONTANT, ";
               selectQuery += "        base_echeance.DTEECHEANCE, ";
               selectQuery += "        base_echeance.LIBELLE ";   
               selectQuery += " from base_echeance";
               selectQuery += " inner join base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT and base_traitement.ID_PATIENT = @id_patient";
               selectQuery += " order by DTEECHEANCE";
            */
           Echeance apg = new Echeance();
           apg.Id =  Convert.ToInt32(r["id"]);
           apg.IdActe = Convert.ToInt32(r["id_TRAITEMENT"]);

           apg.ID_Facturation =Convert.ToInt32(r["id_FACTURATION"]);
           //La date d'echeance ne peut plus être à null
           //Donc si on tombe sur une date d'echeance à null, elle est remise à hier pour etre réglé le plus vite possible

           apg.DateEcheance = r["dteecheance"].ToString() == "" ? DateTime.Now.AddDays(-1) : Convert.ToDateTime(r["dteecheance"]);
           apg.Libelle = r["libelle"].ToString() == "" ? "" : Convert.ToString(r["libelle"]).Trim();
           apg.Montant =  Convert.ToDouble(r["montant"]);

           try
           {
               apg.ParPrelevement = r["parprelevement"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToString(r["parprelevement"]));
           }
           catch (Exception e)
           {
               apg.ParPrelevement = r["parprelevement"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToInt32(r["parprelevement"]));
           }
           try
           {
               apg.ParVirement = r["parvirement"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToString(r["parvirement"]));
           }
           catch (Exception ex)
           {
               apg.ParVirement = r["parvirement"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToInt32(r["parvirement"]));
           }


           apg.IdPatient = Convert.ToInt32(r["id_PATIENT"]);
           apg.ID_Encaissement =  Convert.ToInt32(r["id_ENCAISSEMENT"]);
           apg.mutuelle =  MutuelleMgmt.getMutuelle(Convert.ToInt32(r["id_MUTUELLE"]));
           apg.payeur = r["typepayeur"].ToString() == "" ? Echeance.typepayeur.patient : (Echeance.typepayeur)Convert.ToInt32(r["typepayeur"]);

           apg.Relances.ReleveDeCompte = r["relevedecompte"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["relevedecompte"]);
           apg.Relances.Relance = r["relance"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["relance"]);
           apg.Relances.PreContentieux = r["precontentieux"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["precontentieux"]);
           apg.Relances.Majoration = r["majoration"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["majoration"]);
           apg.Relances.Contentieux = r["contentieux"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["contentieux"]);
           apg.IdComm =Convert.ToInt32(r["id_COMM"]);
           //si quoi ça 
         //  if (r.Table.Columns.Contains("NomPatient"))
          //     apg.NomPatient = r["NomPatient"] is DBNull ? null : Convert.ToString(r["NomPatient"]);
           apg.TypeActe = r["typeacte"].ToString() == "" ? "" : Convert.ToString(r["typeacte"]);
           return apg;
       }
       public static SuiviSpecialiste BuildSuiviSpecialiste(JObject r)
       {
           //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
           SuiviSpecialiste act = new SuiviSpecialiste();
           act.Id = Convert.ToInt32(r["id"]);
           act.IdPatient =Convert.ToInt32(r["id_PATIENT"]);
           act.DateEnvois = r["date_ENVOIS"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(r["DATE_ENVOIS"]);
           act.IdCorrespondant =Convert.ToInt32(r["id_CORRESPONDANT"]);
           act.NomCorrespondant = r["nomcorrespondant"].ToString() == "" ? "" : Convert.ToString(r["nomcorrespondant"]);
           act.Commentaire = r["commentaire"].ToString() == "" ? "" : Convert.ToString(r["commentaire"]);
           return act;
       }
       public static InfosInvisalign GetOrCreateInvisalignInfos(JObject dr)
       {
           InfosInvisalign infosinvisalign = new InfosInvisalign();


               infosinvisalign.IdInvisalign = Convert.ToString(dr["id_INVISALIGN"]);
               infosinvisalign.NomInvisalign = Convert.ToString(dr["nom_INVISALIGN"]);
               infosinvisalign.PrenomInvisalign = Convert.ToString(dr["prenom_INVISALIGN"]);

               infosinvisalign.FreqChangemnt = dr["freqchangemnt"].ToString() == "" ?  InvisalignEnBouche.ChangeFrequency.S1 : (InvisalignEnBouche.ChangeFrequency)Convert.ToInt32(dr["freqchangemnt"]);
               infosinvisalign.FreqRDV = dr["freqrdv"].ToString() == "" ? InvisalignEnBouche.RDVFrequency._3Mois : (InvisalignEnBouche.RDVFrequency)Convert.ToInt32(dr["freqrdv"]);
               infosinvisalign.DateFinInvisalign = dr["datefininvisalign"].ToString() == "" ? DateTime.Now.AddYears(1) :Convert.ToDateTime(dr["DateFinInvisalign"]);
               infosinvisalign.TpeTrmnt = dr["tpetrmnt"].ToString() == "" ? InvisalignPrescriptionFullObj.InvisalignType.Compréhensive : (InvisalignPrescriptionFullObj.InvisalignType)Convert.ToInt32(dr["tpetrmnt"]);


           return infosinvisalign;
       }
       public static IElementDent BuildElementDent(JObject r)
       {
           IElementDent resultat = null;
           ElementDent.Materials mat = (ElementDent.Materials)Convert.ToInt32(r["typematerial"]);
           resultat = EnBoucheMgmt.CreateElementFromType(mat);

           resultat.Id = Convert.ToInt32(r["id"]);
           resultat.DateInstallation = r["datedebut"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["datedebut"]);
           resultat.Datesuppression = r["datefin"].ToString() == ""  ? null : (DateTime?)Convert.ToDateTime(r["datefin"]);
           resultat.Dents = Convert.ToString(r["dents"]);
           resultat.IdPatient = Convert.ToInt32(r["id_PATIENT"]);
           resultat.IdCommFin = Convert.ToInt32(r["id_COMM_FIN"]);
           resultat.IdCommDebut = Convert.ToInt32(r["id_COMM_DEBUT"]);

           return resultat;
       }
       public static ElementAppareil BuildElementAppareil(JObject r)
       {
           ElementAppareil resultat = new ElementAppareil();



           resultat.Id = Convert.ToInt32(r["id"]);
           resultat.DateInstallation = r["datedebut"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["datedebut"]);
           resultat.Datesuppression = r["datefin"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["datefin"]);
           resultat.IdPatient = Convert.ToInt32(r["id_PATIENT"]);
           resultat.IdCommFin =Convert.ToInt32(r["id_COMM_FIN"]);
           resultat.IdCommDebut = Convert.ToInt32(r["id_COMM_DEBUT"]);
           resultat.Appareil = AppareilMgmt.getAppareil(Convert.ToInt32(r["id_APPAREIL"]));
           resultat.Haut =  Convert.ToBoolean(r["haut"]);
           resultat.Bas =  Convert.ToBoolean(r["bas"]);

           return resultat;
       }
       public static ObjSuivi BuildObjSuivi(JObject r)
       {



           ObjSuivi suivi = new ObjSuivi();
           suivi.Id =Convert.ToInt32(r["id"]);
           suivi.CodeBarre = Convert.ToString(r["codebarre"]);
           suivi.NatureTravaux = Convert.ToString(r["nature"]);
           suivi.Details = Convert.ToString(r["Detail"]);
           suivi.SortieCabinet = r["sortie_cab"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["sortie_cab"]);

           suivi.SortieCabinetAvec = Convert.ToString(r["sortie_cab_with"]);  
           suivi.EntreeCabinetAvec = Convert.ToString(r["entree_cab_with"]); 
           suivi.SortieLaboAvec = Convert.ToString(r["detailsortie_labo_with"]);
           suivi.EntreeLaboAvec = Convert.ToString(r["entree_labo_with"]);



           suivi.EntreeLabo = r["entre_labo"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["entre_labo"]);
           suivi.PatientName = Convert.ToString(r["patientname"]);
           suivi.PatientId = Convert.ToInt32(r["patientid"]);
           suivi.RequestorName = Convert.ToString(r["requestorname"]);
           suivi.RequestorId = Convert.ToInt32(r["requestorid"]);
           suivi.ValidatorName = Convert.ToString(r["validatorname"]);
           suivi.ValidatorId = Convert.ToInt32(r["validatorid"]);
           suivi.RecupereParName = Convert.ToString(r["recupereparname"]);
           suivi.WorkerName = Convert.ToString(r["workername"]);
           suivi.SortieLabo = r["sortie_labo"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["sortie_labo"]);
           suivi.ReceptionCab = r["reception_cab"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["reception_cab"]);
           suivi.PoseApp = r["pose_app"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["pose_app"]);
           suivi.tarif = (float)Convert.ToDouble(r["tarif"]);
           suivi.PaymentEffectueLe = r["paymenteffectuele"].ToString() == "" ? suivi.PaymentEffectueLe : Convert.ToDateTime(r["paymenteffectuele"]);
         
             suivi.Empreinte = r["dateempreinte"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateempreinte"]);
           suivi.LastCommentDate = r["lastCommentDate"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["lastCommentDate"]);
           suivi.DemandeID = Convert.ToInt32(r["demande_id"]);
           suivi.IsToSend = Convert.ToBoolean(r["aenvoye"]);

           return suivi;
       }
       public static InvisalignEnBouche BuildInvisalignEnBouche(JObject r)
       {

           InvisalignEnBouche ib = new InvisalignEnBouche();
           ib.Id = Convert.ToInt32(r["id"]);
           ib.DateDebut = r["datedebut"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["datedebut"]);
           ib.DateFin = r["datefin"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["datefin"]);
           ib.NumAligneur = Convert.ToInt32(r["numaligneur"]);
           ib.IdPatient = Convert.ToInt32(r["id_PATIENT"]);
           ib.IsHaut = Convert.ToChar(r["haut"]) == 1;

           return ib;
       }
       
        public static CustomStatusClinique BuildCustomStatusClinique(JObject r)
        {
            CustomStatusClinique customcat = new CustomStatusClinique();
            customcat.Id = Convert.ToInt32(r["id"]);
            customcat.status = StatusCliniqueManuelMgmt.GetStatus(Convert.ToInt32(r["id_STATUS"]));
            customcat.IdPersonne = Convert.ToInt32(r["id_PATIENT"]);
            customcat.DateDebut = Convert.ToDateTime(r["datedebut"]);
            customcat.DateFin = r["datefin"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["datefin"]);

            return customcat;
        }
        public static ObjImage BuildObjImage(JObject r)
        {

            ObjImage obj = null;

            obj = new ObjImage();
            obj.Id = Convert.ToInt32(r["pkObjet"]);
            obj.fichier = r["fichier"].ToString() == "" ? "" : Convert.ToString(r["fichier"]);
            obj.nom = r["nom"].ToString() == "" ? "" : Convert.ToString(r["nom"]);

            obj.extension = r["extension"].ToString() == "" ? "" : Convert.ToString(r["extension"]);
            //obj.datas = Convert.ToString(r["datas"]);
            //obj.vignette = Convert.ToString(r["vignette"]);
            obj.width =  Convert.ToInt32(r["width"]);
            obj.height = Convert.ToInt32(r["height"]);
            obj.taille =  Convert.ToInt32(r["taille"]);
            obj.estidentite =  Convert.ToInt32(r["estidentite"]);
            obj.datecreation = r["dateCreation"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(r["dateCreation"]);
            obj.echelle = Convert.ToDouble(r["echelle"]);
           // a fixer
            // obj.last_modif = r["lastModif"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(r["lastModif"]);
            obj.rep_stockage = r["repStockage"].ToString() == "" ? "" : Convert.ToString(r["repStockage"]);
            obj.syncpath = r["syncPath"].ToString() == "" ? "" : Convert.ToString(r["syncPath"]);

            // a fixer
           // obj.dateinsertion = r["dateinsertion"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(r["dateinsertion"]);
            obj.auteur = r["auteur"].ToString() == "" ? "" : Convert.ToString(r["auteur"]);
            obj.IdGabarit = Convert.ToInt32(r["idGabarit"]);
            obj.Idpatient = Convert.ToInt32(r["id_patient_orthalis"]);


            return obj;
        }
        public static Attribut BuildAttribut(JObject r)
        {

            Attribut obj = null;

            obj = new Attribut();
            obj.Id = Convert.ToInt32(r.GetValue("attributs")["pk_attribut"]);
            obj.Nom = Convert.ToString(r.GetValue("attributs")["nom"]);
            obj.Valeur = Convert.ToString(r["valeur"]);
            return obj;
        }
        public static Encaissement BuildEncaissement(JObject r)
        {
            Encaissement cs = new Encaissement();
            cs.Id = Convert.ToInt32(r["id"]);
            cs.IdPatient = Convert.ToInt32(r["id_PATIENT"]);
            cs.MontantEncaisse = Convert.ToDouble(r["montant_ENCAISSE"]);
            cs.IdPaiementReel = Convert.ToInt32(r["id_PAIEMENT_REEL"]);

            return cs;
        }

        public static Contact BuildContact(JObject r)
        {
            Contact c = new Contact();

            c.Id = Convert.ToInt32(r["id"]);
            c.TypeContact = (Contact.ContactType)Convert.ToInt32(r["contacttype"]);
            c.Value = Convert.ToString(r["value"]);
            c.id_pays =  r["id_PAYS"].ToString() == "" ? -1 : Convert.ToInt32(r["id_PAYS"]);
            c.isSms = r["sms"].ToString() == "" ? false : Convert.ToBoolean(r["sms"]);
            c.Libelle = MgmtContactLib.getLib(Convert.ToInt32(r["id_CONTACTLIBELLE"]));
            c.prefOrder = (int?)Convert.ToInt32(r["PREF_ORDER"]);
            c.IdPersonne = Convert.ToInt32(r["id_PERSONNE"]);
            if (c.id_pays != -1)
                c.pays = MgmtPays.getPaysById(c.id_pays);
            if (!(r["adr1"].ToString() == "") || !(r["adr2"].ToString() == "") || !(r["codepostal"].ToString() == "") || !(r["ville"].ToString() == ""))
            {
                c.adresse = new ContactAdresse();

                c.adresse.Adr1 = (r["adr1"].ToString() == "") ? "" : Convert.ToString(r["adr1"]);
                c.adresse.Adr2 = (r["adr1"].ToString() == "") ? "" : Convert.ToString(r["adr1"]);
                c.adresse.CodePostal = (r["codepostal"].ToString() == "") ? "" : Convert.ToString(r["codepostal"]);
                c.adresse.Ville = (r["ville"].ToString() == "") ? "" : Convert.ToString(r["ville"]);
                c.adresse.Pays = (r["pays"].ToString() == "") ? "" : Convert.ToString(r["pays"]);
            }

            return c;

        }


        public static AffectationFauteuil BuildAffectationFauteuil(JObject r)
        {
            AffectationFauteuil Ut = new AffectationFauteuil();           
            Ut.Id = Convert.ToInt32(r["ID"]);
            Ut.Iduser = Convert.ToInt32(r["id_user"]);
            Ut.fauteuil = BasCommon_BL.Fauteuilsmgt.getfauteuil(Convert.ToInt32(r["id_fauteuil"]));
            Ut.DteFrom = (Convert.ToDateTime(r["affecte_from"]));
            Ut.DteTo = (Convert.ToDateTime(r["affecte_to"]));
            Ut.Remarque = (Convert.ToString(r["remarques"]));
            Ut.user = UtilisateursMgt.getUtilisateur(Ut.Iduser);            
            return Ut;
        }
        public static Autre BuildAutre(JObject r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            Autre autre = new Autre();
            autre.id = Convert.ToInt32(r["id"]);
            autre.Libelle = r["libelle"].ToString() == "" ? "" : Convert.ToString(r["libelle"]).Trim();

            return autre;
        }
    }
}
