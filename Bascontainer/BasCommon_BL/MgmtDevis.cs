using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using BasCommon_DAL;
using System.Data;
using Newtonsoft.Json.Linq;
namespace BasCommon_BL
{
    public static class MgmtDevis
    {



        public static void DeleteEcheanceDevisALaCarte(Devis obj)
        {
            if (obj.Id > 0)
                DAC.DeleteEcheanceDevisALaCarte(obj);
        }

        public static void UpdateDevis(Devis obj)
        {
            if (obj.Id > 0)
                DAC.UpdateDevis(obj);
        }
        public static void UpdateDevis_TK(Devis_TK obj)
        {
            if (obj.Id > 0)
                DAC.UpdateDevis_TK(obj);
        }


        //seif impression
        public static DataTable getCommImpression(int idDevis)
        {
            
            return DAC.getCommImpression(idDevis);
        }
        // seif
        public static void update_tempechenaces_tk(TempEcheanceDefinition ted, CommTraitement ct)
        {
            if (ct.Id<=0) return;
            DAC.update_tempechenaces_tk(ted, ct);
        }
        public static void update_tempechenacescc_tk(TempEcheanceDefinition ted, CommClinique cc)
        {
            if (cc.Id <= 0) return;
            DAC.update_tempechenacescc_tk(ted, cc);
        }
        public static void UpdateDevisandPropositions(Devis obj)
        {
            if (obj.Id <= 0) return;

            if (obj.propositions == null)
                obj.propositions = PropositionMgmt.getPropositions(obj);

            int Idpat = -1;
            DateTime? deacceptation = null;

            foreach (Proposition p in obj.propositions)
            {
                if (Idpat == -1)
                {
                    Idpat = p.IdPatient;
                }
                else
                {
                    if (p.IdPatient != Idpat)
                        throw (new System.Exception("Toutes les propositions doivent appartenir au meme patient"));
                }
                if (p.Etat == Proposition.EtatProposition.Accepté)
                    if (deacceptation == null)
                        deacceptation = p.DateAcceptation;
                    else
                        if (deacceptation.Value < p.DateAcceptation) deacceptation = p.DateAcceptation;
            }



            DAC.UpdateDevis(obj);

            DAC.Delete_acte_propositions(obj);
            foreach (ActePGPropose acte in obj.actesHorstraitement)
            {
                acte.devis = obj;
                DAC.Insert_acte_propositions(acte);
            }

            if (obj.echeancestemp != null)
            {
                DAC.DeleteEcheanceDevisALaCarte(obj);
                foreach (EcheanceDevisALaCarte acte in obj.echeancestemp)
                {
                    acte.devis = obj;
                    DAC.AddEcheanceDevisALaCarte(acte);
                }
            }


            foreach (Proposition p in obj.propositions)
            {
                DAC.Delete_acte_propositions(p);
                foreach (ActePGPropose acte in p.matosassociate)
                {
                    acte.proposition = p;
                    DAC.Insert_acte_propositions(acte);
                }

                p.IdDevis = obj.Id;
                p.DateEvenement = DateTime.Now;
                PropositionMgmt.updateProposition(p);

                foreach (Traitement t in p.traitements)
                {
                    t.IdProposition = p.Id;
                    TraitementMgmt.UpdateTraitement(t);
                    foreach (Semestre s in t.semestres)
                    {
                        s.IdTraitement = t.Id;
                        SemestreMgmt.UpdateSemestre(s);
                    }
                }

            }
        }


        public static void SaveAutrePersonne(CommTraitement com)
        {
            DAC.setDevisAutrePersonnes(com);

        }


        public static void AddCommDevis(int id_devis, CommTraitement p_com)
        {
            //DAC.AddActeDevis(id_devis, p_com);
            DAC.InsertDevis_Comment_TK(id_devis, p_com);
        }
        
        public static void DeleteCommDevis(int id_devis, CommTraitement p_com)
        {
            DAC.DeleteDevis_Comment_TK(id_devis, p_com);
        }
      
        public static void UpdateActeDevis_TK(CommTraitement com)
        {

            DAC.UpdateActeDevis_TK(com);
        }

        public static void ArchiverDevis_TK(Devis_TK devis)
        {
            devis.DateArchivage = DateTime.Now;
            DAC.ArchiverDevis_TK(devis);
        }
        public static void ArchiverDevis(Devis devis)
        {
            devis.DateArchivage = DateTime.Now;
            DAC.ArchiverDevis(devis);
        }
        public static void DeleteDevis_TK(Devis_TK devis)
        {
            DAC.DeleteDevis_TK(devis.Id);

        }
        public static void DeleteDevis(Devis devis)
        {
            DAC.DeleteDevis(devis.Id);

        }
        public static void DeleteDevis(int Iddevis)
        {
            DAC.DeleteDevis(Iddevis);

        }
        public static void DeleteDevisOLD(int id)
        {
            DAC.DeleteDevisOLD(id);

        }
        public static EcheanceDevisALaCarte CreateEcheanceDevisALaCarte(Devis devis)
        {
            ActePG actesimule = CreateActePG(devis, devis.DatePrevisionnelDeDebutTraitement, 12);
            EcheanceDevisALaCarte ted = new EcheanceDevisALaCarte();
            ted.DAteEcheance = actesimule.NbMois != null ? actesimule.DateExecution.AddMonths(actesimule.NbMois.Value).AddDays(actesimule.NbJours.Value) : actesimule.DateExecution;
            ted.Montant = actesimule.Montant_Honoraire;
            ted.Libelle = actesimule.Libelle;
            ted.acte = actesimule;
            ted.devis = devis;
            ted.AlreadyPayed = false;
            ted.payeur = Echeance.typepayeur.patient;

            return ted;
        }

        public static ActePG CreateActePG(Devis devis, DateTime dateDebut, int NbMois)
        {
            TemplateActePG tmplteHN = TemplateApctePGMgmt.getTemplatesActeGestion("HN");

            double montant = 0;
            if (devis.Montant == null)
            {
                if (devis.actesHorstraitement == null)
                    devis.actesHorstraitement = MgmtDevis.getactesHorstraitement(devis);

                foreach (ActePGPropose ap in devis.actesHorstraitement)
                    montant += ap.Montant;
            }
            else
                montant = devis.Montant.Value;

            ActePG acte = new ActePG();
            acte.Template = tmplteHN;
            acte.Montant_Honoraire = montant;
            acte.Coeff = tmplteHN.Coeff;
            acte.prestation = tmplteHN.Code;
            acte.Libelle = "Devis du " + DateTime.Now.ToShortDateString();
            acte.Quantite = 1;

            acte.NbJours = 0;
            acte.NbMois = NbMois;

            acte.Id_DEP = -1;



            acte.NeedDEP = tmplteHN.NeedDEP;
            acte.NeedFSE = tmplteHN.NeedFS;
            acte.patient = devis.patient;
            acte.IdPatient = devis.IdPatient;
            acte.IsDecomposed = tmplteHN.IsDecomposed;
            acte.CoeffDecompose = tmplteHN.CoeffDecompose;
            acte.IdSemestrePlanGestionAssocie = -1;
            acte.IdSurvPlanGestionAssocie = -1;
            acte.DevisAssociate = devis;
            acte.DateExecution = dateDebut;
            acte.lstEcheances = new List<Echeance>();

            if ((devis.echeancestemp != null) && (devis.echeancestemp.Count > 0))
            {
                foreach (EcheanceDevisALaCarte edac in devis.echeancestemp)
                {
                    Echeance ec = new Echeance();
                    ec.acte = acte;
                    ec.DateEcheance = edac.DAteEcheance;
                    ec.Libelle = edac.Libelle;
                    ec.Montant = edac.Montant;
                    ec.NomPatient = devis.patient!=null?devis.patient.ToString():"";
                    ec.ParPrelevement = edac.ParPrelevement;
                    ec.ParVirement = edac.ParVirement;
                    ec.payeur = edac.payeur;
                    ec.patient = devis.patient;
                    ec.IdPatient = devis.IdPatient;

                    acte.lstEcheances.Add(ec);
                }
            }

            return acte;
        }


        public static void AddActFromDevisALaCarte(Devis devis,DateTime dateDebut,int NbMois)
        {

            ActePG acte = CreateActePG(devis, dateDebut, NbMois);           

            
            ActesPGMgmt.InsertActePGWithEcheance(acte, false, false, null);




        }
        public static void UpdateEcheancierDocteur(int id_Devis, int Echeancier_Docteur)
        {
            DAC.UpdateEcheancierDocteur(id_Devis ,Echeancier_Docteur );

        }
        public static void DeleteTempEcheance(Proposition p)
        {
            DAC.DelTempEcheances(p);

        }
        public static void DeleteTempEcheance_TK(CommTraitement ct)
        {
            DAC.DelTempEcheances_tk(ct);

        }

        public static void AddTempEcheance(TempEcheanceDefinition ted)
        {
           DAC.AddTempEcheance(ted);

        }

        public static void AddEcheanceDevisALaCarte(EcheanceDevisALaCarte ted)
        {
            DAC.AddEcheanceDevisALaCarte(ted);

        }

        public static List<TempEcheanceDefinition> get_tempecheances_TKOLD(CommTraitement ct)
        {
            List<TempEcheanceDefinition> res = new List<TempEcheanceDefinition>();

            DataTable dt = DAC.get_tempecheances_TK(ct);


            foreach (DataRow dr in dt.Rows)
            {
                TempEcheanceDefinition ted = Builders.BuildTempEcheance.Build(dr);
                res.Add(ted);
            }

            return res;
        }

        public static List<TempEcheanceDefinition> get_tempecheances_TK(CommTraitement ct)
        {
            JArray json = DAC.getMethodeJsonArray("/EcheanceByIdComment/" + ct.Id);
            List<TempEcheanceDefinition> res = new List<TempEcheanceDefinition>();

            foreach (JObject j in json)
            {
                TempEcheanceDefinition ted = Builders.BuildTempEcheance.Build(j);
                res.Add(ted);
            }

            return res;
        }

        
      
        public static List<TempEcheanceDefinition> get_tempecheancescc_TK(CommClinique cc)
        {
            JArray json = DAC.getMethodeJsonArray("/EcheanceByIdCommClinique/" + cc.Id);
            List<TempEcheanceDefinition> res = new List<TempEcheanceDefinition>();

            foreach (JObject j in json)
            {
                TempEcheanceDefinition ted = Builders.BuildTempEcheance.Build(j);
                res.Add(ted);
            }

            return res;
        }

        public static List<TempEcheanceDefinition> get_tempecheancesPAT_TK(List<CommClinique> lst, int idpatient)
        {
            JArray json = DAC.getMethodeJsonArray("/EcheanceByIdCommClinique/" + idpatient);
            List<TempEcheanceDefinition> res = new List<TempEcheanceDefinition>();

            foreach (JObject j in json)
            {
                int idComm =j["id_COMMCLINIQUE"].ToString() == "" ? -1 : Convert.ToInt32( j["id_COMMCLINIQUE"]);
                if (idComm == -1) continue;
                TempEcheanceDefinition ted = Builders.BuildTempEcheance.Build(j);
                if(lst.Find(cm => cm.Id == idComm) != null)
                lst.Find(cm => cm.Id == idComm).echeancestemp.Add(ted);
            }
            return res;
        }
        public static List<TempEcheanceDefinition> get_tempecheancescc_TKOLD(CommClinique cc)
        {
            List<TempEcheanceDefinition> res = new List<TempEcheanceDefinition>();

            DataTable dt = DAC.get_tempecheancescc_TK(cc);


            foreach (DataRow dr in dt.Rows)
            {
                TempEcheanceDefinition ted = Builders.BuildTempEcheance.Build(dr);
                res.Add(ted);
            }

            return res;
        }

        public static List<EcheanceDevisALaCarte> get_EcheancesDevisALaCarte(Devis devis)
        {
            List<EcheanceDevisALaCarte> res = new List<EcheanceDevisALaCarte>();

            if (devis.Id==-1)
            {
                res.Add(CreateEcheanceDevisALaCarte(devis));
            }
            else
            {
                DataTable dt = DAC.get_echeancesDevisALaCarte(devis);


                foreach (DataRow dr in dt.Rows)
                {
                    EcheanceDevisALaCarte ted = Builders.BuildEcheanceDevisALaCarte.Build(dr);
                    res.Add(ted);
                }

                if (res.Count == 0)
                {
                    res.Add(CreateEcheanceDevisALaCarte(devis));
                }
            }
            return res;

        }

        public static List<TempEcheanceDefinition> get_tempecheances(Devis devis)
        {
            List<TempEcheanceDefinition> res = new List<TempEcheanceDefinition>();

            DataTable dt = DAC.get_tempecheances(devis);

            
            foreach (DataRow dr in dt.Rows)
            {
                TempEcheanceDefinition ted = Builders.BuildTempEcheance.Build(dr);
                res.Add(ted);
            }

            return res;

        }

        public static List<TempEcheanceDefinition> get_tempecheances(Proposition p)
        {
            List<TempEcheanceDefinition> res = new List<TempEcheanceDefinition>();

            DataTable dt = DAC.get_tempecheances(p);


            foreach (DataRow dr in dt.Rows)
            {
                TempEcheanceDefinition ted = Builders.BuildTempEcheance.Build(dr);
                res.Add(ted);
            }

            return res;

        }


        public static List<Devis> GetDevisHorsEcheance(List<Devis> lstDEvis)
        {
            List<Devis> res = new List<Devis>();
            
            foreach (Devis d in lstDEvis)
                if ((d.DateAcceptation == null) && (d.DateArchivage == null) && (d.DateEcheance < DateTime.Now))
                    res.Add(d);

            return res;

        }


        public static void GetRepartitionDevis(ref int NbAcceptes, ref int NbEnAttente, ref int NbArchive, List<Devis> lstDEvis, List<Devis_TK> lstDevis_TK)
        {
            if (lstDEvis == null) return;

            NbAcceptes = 0;
            NbEnAttente = 0;
            NbArchive = 0;


            foreach (Devis d in lstDEvis)
            {

                if (d.DateArchivage != null)
                    NbArchive++;
                else
                {
                    if (d.DateAcceptation != null)
                        NbAcceptes++;
                    else
                        NbEnAttente++;
                }

            }
            foreach (Devis_TK d in lstDevis_TK )
            {

                if (d.DateArchivage != null)
                    NbArchive++;
                else
                {
                    if (d.DateAcceptation != null)
                        NbAcceptes++;
                    else
                        NbEnAttente++;
                }

            }


        }


        public static void Insert_com_propositions_TK(ActePGPropose   a)
        {
           // a.
          }

        public static void insertEcheancesDevis_TK(CommTraitement ct, TempEcheanceDefinition ted)
        {
            DAC.insertEcheancesDevis_TK(ct, ted);
        }

        public static void CreateDevis_TK(Devis_TK  devis)
        {
            //int Idpat = -1;
            //DateTime? deacceptation = null;
            //
            //Proposition p = new Proposition(devis);
            //PropositionMgmt.InsertProposition(p);
            //PropositionMgmt.InsertFullProposition(p);
           
            ActePG ap = new ActePG();
            //
            DAC.InsertDevis_TK(devis);
            List<ActePGPropose> apgs = new List<ActePGPropose>();
            foreach (CommTraitement Com in devis.actesTraitement)
            {
                DAC.InsertDevis_Comment_TK(devis.Id , Com);
                DAC.setDevisAutrePersonnes(Com);

                if (Com.echeancestemp.Count != 0)
                {
                    foreach (TempEcheanceDefinition ted in Com.echeancestemp)
                    {
                        DAC.insertEcheancesDevis_TK(Com, ted);
                    }
                }
                else
                {
                    TempEcheanceDefinition ted = new TempEcheanceDefinition();

                    if ((System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + DAC.prefix] == "FR" ) && devis.Traitement.TypeScenario != NewTraitement.typeScenario.Prothése_CMUC)
                    {

                        double partsecu = 0;
                        double partmutuelle = 0;
                        double parPatient = 0;
                        TraitementsMgmt.getMontantEcheToulon(Com, ref partsecu, ref partmutuelle, ref parPatient);

                        if (parPatient > 0)
                        {
                            ted.Montant = parPatient;
                            ted.DAteEcheance = DateTime.Now.AddMonths(6);
                            ted.Libelle = Com.Acte.acte_libelle + "[part patient]";
                            ted.ParPrelevement = false;
                            ted.ParVirement = false;
                            ted.payeur = Echeance.typepayeur.patient;
                            DAC.insertEcheancesDevis_TK(Com, ted);


                        }
                        if (partmutuelle > 0)
                        {
                            ted = new TempEcheanceDefinition();
                            ted.Montant = partmutuelle;
                            ted.DAteEcheance = DateTime.Now.AddMonths(6);
                            ted.Libelle = Com.Acte.acte_libelle + "[part mutuellle]";
                            ted.ParPrelevement = false;
                            ted.ParVirement = false;
                            ted.payeur = Echeance.typepayeur.Mutuelle;
                            DAC.insertEcheancesDevis_TK(Com, ted);
                        }
                        if (partsecu > 0)
                        {
                            ted = new TempEcheanceDefinition();
                            ted.Montant = partsecu;
                            ted.DAteEcheance = DateTime.Now.AddMonths(6);
                            ted.Libelle = Com.Acte.acte_libelle + "[part secu]";
                            ted.ParPrelevement = false;
                            ted.ParVirement = false;
                            ted.payeur = Echeance.typepayeur.Secu;
                            DAC.insertEcheancesDevis_TK(Com, ted);
                        }
                    }
                    else
                    {
                        ted = new TempEcheanceDefinition();
                      //  ted.DAteEcheance = Com.DatePrevisionnnelle.Value;
                      //  ted.DAteEcheance = DateTime.Now.AddMonths(6);
                        ted.DAteEcheance = devis.DatePrevisionnelDeDebutTraitement.AddDays(Com.NbJours);
                        ted.Montant = Com.prix;
                        ted.Libelle = Com.Acte.acte_libelle;
                        ted.acte = ap;
                        ted.AlreadyPayed = false;

                        ted.ParVirement = false;
                        ted.ParPrelevement = false;
                        DAC.insertEcheancesDevis_TK(Com, ted);
                    }

                   



                }

                ActePGPropose Tmpapg;
                //Tmpapg = new ActePGPropose();
            
                //Tmpapg.Libelle = Com.Acte.acte_libelle;
                //Tmpapg.MontantAvantRemise = Com.Acte.prix_acte;
                //Tmpapg.Montant = Com.Acte.prix_traitement;
                //Tmpapg.IdDevis = devis.Id;
                //Tmpapg.Id = Com.Acte.id_acte;
                //Tmpapg.IdProposition = Com.Id;
                //Tmpapg.DateExecution = Com.DatePrevisionnnelle ;
                //Tmpapg.IdTemplateActePG = 1;
                //apgs.Add(Tmpapg);
                
                foreach (CommActesTraitement a in Com.ActesSupp)
                {
                    Tmpapg = new ActePGPropose();
                    Tmpapg.Libelle = a.LibActe;
                    Tmpapg.MontantAvantRemise = a.prix_acte;
                    Tmpapg.Montant = a.prix_traitement;
                    Tmpapg.DateExecution = Com.DatePrevisionnnelle;
                    Tmpapg.IdDevis = devis.Id;
                    Tmpapg.Id = Com.Id;
                    Tmpapg.IdTemplateActePG = a.IdActe;
                    Tmpapg.Type_Acte = "";
                    Tmpapg.Qte = a.Qte;
                    Tmpapg.partPatient = a.partPatient;
                    Tmpapg.RembMutuelle = a.RembMutuelle;
                    Tmpapg.Remboursement = a.Remboursement;
                    Tmpapg.desactive = a.desactive;
                    apgs.Add(Tmpapg);
                }
               
                foreach (CommActesTraitement a in Com.Radios)
                {
                    Tmpapg = new ActePGPropose();
                    Tmpapg.Libelle = a.LibActe;
                    Tmpapg.MontantAvantRemise = a.prix_acte;
                    Tmpapg.Montant = a.prix_traitement;
                    Tmpapg.DateExecution = Com.DatePrevisionnnelle;
                    Tmpapg.IdDevis = devis.Id;
                    Tmpapg.Id  = Com.Id;
                    Tmpapg.IdTemplateActePG = a.IdActe;
                    Tmpapg.Type_Acte = "R";
                    Tmpapg.Qte = a.Qte;
                    Tmpapg.partPatient = a.partPatient;
                    Tmpapg.RembMutuelle = a.RembMutuelle;
                    Tmpapg.desactive = a.desactive;
                    apgs.Add(Tmpapg);
                    
                }
                foreach (CommActesTraitement a in Com.photos)
                {
                    Tmpapg = new ActePGPropose();
                    Tmpapg.Libelle = a.LibActe;
                    Tmpapg.MontantAvantRemise = a.prix_acte;
                    Tmpapg.Montant = a.prix_traitement;
                    Tmpapg.DateExecution = Com.DatePrevisionnnelle;
                    Tmpapg.IdDevis = devis.Id;
                    Tmpapg.Id = Com.Id;
                    Tmpapg.IdTemplateActePG = a.IdActe;
                    Tmpapg.Type_Acte = "P";
                    Tmpapg.Qte = a.Qte;
                    Tmpapg.partPatient = a.partPatient;
                    Tmpapg.RembMutuelle = a.RembMutuelle;
                    Tmpapg.desactive = a.desactive;
                    apgs.Add(Tmpapg);
                }
                if (apgs.Count > 0)
                {
                    DAC.InsertDevis_Actes_TK(apgs);
                    apgs.Clear();
                }

                foreach (CommMaterielTraitement a in Com.Materiels)
                {
                    Tmpapg = new ActePGPropose();
                    Tmpapg.Libelle = a.Libelle;
                    Tmpapg.MontantAvantRemise = a.prix_materiel;
                    Tmpapg.DateExecution = Com.DatePrevisionnnelle;
                    Tmpapg.IdDevis = devis.Id;
                    Tmpapg.Montant = a.prix_traitement;
                    Tmpapg.Id = Com.Id;
                    Tmpapg.Qte = a.Qte;
                    Tmpapg.IdTemplateActePG = a.idMateriel;
                    Tmpapg.Qte = a.Qte;
                    Tmpapg.desactive = a.desactive;
                    apgs.Add(Tmpapg);
                }
                if (Com.Materiels.Count > 0)
                {
                    DAC.Insert_Devis_Materiels_TK(apgs);
                    apgs.Clear();
                }
            }
      
        }
        public static void CreateDevis(Devis devis)
        {
            int Idpat = -1;
            DateTime? deacceptation = null;


            foreach (Proposition p in devis.propositions)
            {
                if (Idpat == -1)
                {
                    Idpat = p.IdPatient;
                }
                else
                {
                    if (p.IdPatient != Idpat)
                        throw (new System.Exception("Toutes les propositions doivent appartenir au meme patient"));
                }
                if (p.Etat == Proposition.EtatProposition.Accepté)
                    if (deacceptation == null)
                        deacceptation = p.DateAcceptation;
                    else
                        if (deacceptation.Value < p.DateAcceptation) deacceptation = p.DateAcceptation;
            }

            

            DAC.InsertDevis(devis);

            foreach (ActePGPropose acte in devis.actesHorstraitement)
            {
                acte.devis = devis;
                DAC.Insert_acte_propositions(acte);
            }

            if (devis.echeancestemp != null)
            {
                DAC.DeleteEcheanceDevisALaCarte(devis);
                foreach (EcheanceDevisALaCarte acte in devis.echeancestemp)
                {
                    acte.devis = devis;
                    DAC.AddEcheanceDevisALaCarte(acte);
                }
            }


            foreach (Proposition p in devis.propositions)
            {

                p.IdDevis = devis.Id;
                p.Etat = Proposition.EtatProposition.Soumis;
                p.DateEvenement = DateTime.Now;
                PropositionMgmt.updateProposition(p);
            }



        }
        public static Double MontantAvantRemise_Com(CommTraitement com)
        {
            double MontantAvantRemise = 0;
            MontantAvantRemise = MontantAvantRemise + com.Acte.prix_acte;

            foreach (CommActesTraitement cat in com.ActesSupp)
            {
                MontantAvantRemise = MontantAvantRemise + cat.prix_acte;
            }

            foreach (CommActesTraitement cat in com.Radios)
            {
                MontantAvantRemise = MontantAvantRemise + cat.prix_acte;
            }

            foreach (CommActesTraitement cat in com.photos)
            {
                 MontantAvantRemise = MontantAvantRemise + cat.prix_acte;
            }


            foreach (CommMaterielTraitement scm in com.Materiels)
            {
                MontantAvantRemise = MontantAvantRemise + scm.prix_materiel;
            }

            return MontantAvantRemise ;
            
        }
        public static Double Montant_Com(CommTraitement com)
        {
            double Montant = 0;
            Montant = Montant + (com.Acte.prix_traitement * int.Parse( com.Acte.quantite)) ;
         
            foreach (CommActesTraitement cat in com.ActesSupp)
            {
                Montant = Montant + (cat.prix_traitement * cat.Qte );
            }

            foreach (CommActesTraitement cat in com.Radios)
            {
                Montant = Montant + (cat.prix_traitement* cat.Qte );
            }

            foreach (CommActesTraitement cat in com.photos)
            {
                Montant = Montant + (cat.prix_traitement* cat.Qte );
            }
           
          
            foreach (CommMaterielTraitement scm in com.Materiels)
            {
                Montant = Montant + (scm.prix_traitement* scm.Qte  );
            }

            return Montant ;
            
        }
       
        public static void SavePrixCom(CommTraitement com)
        {
            DAC.setDevisPrix(com);

        }

        public static void setDevisPrixTotal(Devis_TK dev)
        {
            DAC.setDevisPrixTotal(dev);

        }


        public static Devis CreateDevis(List<Proposition> propositions, List<ActePGPropose> lstActes, Devis.enumtypePropositon tpe,DateTime dteDebut,DateTime? dteFin)
        {
            int Idpat = -1;
            DateTime? deacceptation = null;

            foreach (Proposition p in propositions)
            {
                if (Idpat == -1)
                {
                    Idpat = p.IdPatient;
                }
                else
                {
                    if (p.IdPatient != Idpat)
                        throw (new System.Exception("Toutes les propositions doivent appartenir au meme patient"));
                }
                if (p.Etat == Proposition.EtatProposition.Accepté)
                    if (deacceptation == null)
                        deacceptation = p.DateAcceptation;
                    else
                        if (deacceptation.Value < p.DateAcceptation) deacceptation = p.DateAcceptation;
            }

            Devis d = new Devis();
            d.DateProposition = DateTime.Now;
            d.DateAcceptation = deacceptation;
            d.DateArchivage = null;
            d.DateEcheance = DateTime.Now.AddDays(15);
            d.IdPatient = Idpat;
            d.TypeDevis = tpe;
            d.DatePrevisionnelDeDebutTraitement = dteDebut;
            d.DatePrevisionnelDeFinTraitement = dteFin;

            DAC.InsertDevis(d);

            d.actesHorstraitement = new List<ActePGPropose>();
            foreach (ActePGPropose acte in lstActes)
            {
                acte.devis = d;
                d.actesHorstraitement.Add(acte);
                DAC.Insert_acte_propositions(acte);
            }

            if (d.echeancestemp != null)
            {
                DAC.DeleteEcheanceDevisALaCarte(d);
                foreach (EcheanceDevisALaCarte acte in d.echeancestemp)
                {
                    acte.devis = d;
                    DAC.AddEcheanceDevisALaCarte(acte);
                }
            }

            if (d.echeancestemp != null)
            {
                DAC.DeleteEcheanceDevisALaCarte(d);
                foreach (EcheanceDevisALaCarte acte in d.echeancestemp)
                {
                    acte.devis = d;
                    DAC.AddEcheanceDevisALaCarte(acte);
                }
            }

            d.propositions = new List<Proposition>();
            foreach (Proposition p in propositions)
            {
                
                p.IdDevis = d.Id;
                p.Etat = Proposition.EtatProposition.Soumis;
                p.DateEvenement = DateTime.Now;
                PropositionMgmt.updateProposition(p);
                d.propositions.Add(p);
            }

            return d;

        }

        public static void ReCreateDevis(Devis dev)
        {
            int Idpat = -1;
           
            foreach (Proposition p in dev.propositions)
            {
                if (Idpat == -1)
                {
                    Idpat = p.IdPatient;
                }
                else
                {
                    if (p.IdPatient != Idpat)
                        throw (new System.Exception("Toutes les propositions doivent appartenir au meme patient"));
                }
                
            }



            DAC.InsertDevis(dev);

            foreach (ActePGPropose acte in dev.actesHorstraitement)
                DAC.Insert_acte_propositions(acte);



            foreach (Proposition p in dev.propositions)
            {
                p.IdDevis = dev.Id;
                p.Etat = Proposition.EtatProposition.Soumis;
                p.DateEvenement = DateTime.Now;
                PropositionMgmt.SaveProposition(p);
            }


        }

        public static List<ActePGPropose> getactesHorstraitement(Proposition p)
        {
            DataTable dt = DAC.get_acte_propositions(p);
            List<ActePGPropose> lst = new List<ActePGPropose>();
            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildActesHorstraitement.Build(r));
            }

            return lst;
        }
       

        public static List<ActePGPropose> getactesHorstraitement(Devis devis)
        {
            DataTable dt = DAC.get_acte_propositions(devis);
            List<ActePGPropose> lst = new List<ActePGPropose>();
            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildActesHorstraitement.Build(r));
            }

            return lst;
        }


        public static Devis_TK getDevis_TK(int id)
        {
            DataRow r = DAC.getDevis_TK(id);
            if (r == null) return null;
            return Builders.BuildDevis.Build_TK(r);
        }

        public static List<Devis_TK> getDevis_TK(basePatient patient)
        {
            JArray json = DAC.getMethodeJsonArray("/Devis/DevisTK/" + patient.Id);
            JArray jsonCommActeSup = new JArray() ;
            JArray jsonCommMat = new JArray();
            JArray jsonComments = new JArray();
            List<Devis_TK> lst = new List<Devis_TK>();
            List<CommTraitement> lstC = new List<CommTraitement>();
            if (json.Count > 0)
            {
                 jsonComments = DAC.getMethodeJsonArray("/CommTraitementByIPatient/" + patient.Id);
           
                if (jsonComments.Count > 0)
                {
                     jsonCommActeSup = DAC.getMethodeJsonArray("/GetCommActeSupTraitementsDevisPat/" + patient.Id);
                     jsonCommMat = DAC.getMethodeJsonArray("/CommDevisMatsTraitementsDevisPAT/" + patient.Id);
                }
            }
            
           
            foreach (JObject r in json)
            {
                Devis_TK d = Builders.BuildDevis.Build_TKJ(r);
                d.patient = patient;
               // GetCommDevis(ref d);
                d.actesTraitement = new List<CommTraitement>();
                foreach (JObject j in jsonComments)
                {
                    if(Convert.ToInt32(j["id_DEVIS"]) == d.Id)
                    {
                    CommTraitement c = Builders.BuildDevis.BuildCommTraitement(j);
                    c.devis = d;
                    d.actesTraitement.Add(c);
                    }
                }
                foreach (CommTraitement cc in d.actesTraitement)
                {
                    foreach (JToken cas in jsonCommActeSup)
                    {
                        if (cas.ToString() == "") continue;
                        JObject casj = JObject.Parse(cas.ToString());
                        if (Convert.ToInt32(cas["id"]) == cc.Id)
                        {
                            CommActesTraitement cma = Builders.BuildDevis.BuildCommActeSuppDevis(casj);
                            string type = casj["type_ACTE"].ToString();
                            switch (type)
                            {
                                case "": cc.ActesSupp.Add(cma); break;
                                case "R": cc.photos.Add(cma); break;
                                case "P": cc.Radios.Add(cma); break;
                            }
                        }
                    }

                    foreach (JObject casM in jsonCommMat)
                    {
                        if (Convert.ToInt32(casM["id"]) == cc.Id)
                        {
                            CommMaterielTraitement cma = Builders.BuildDevis.BuildDevisMaterielJ(casM);
                            cc.Materiels.Add(cma);
                        }
                    }
                    cc.AutrePersonnes = new List<CommAutrePersonne>();

                }


                lst.Add(d);
            }
            return lst;
        }
        public static List<Devis_TK> getDevis_TKOLD(basePatient patient)
        {
            DataTable dt = DAC.getDevis_TK(patient);
            List<Devis_TK> lst = new List<Devis_TK>();
            foreach (DataRow r in dt.Rows)
            {
                Devis_TK d = Builders.BuildDevis.Build_TK(r);
                d.patient = patient;
                GetCommDevis(ref d);
                foreach (CommTraitement cc in d.actesTraitement)
                {
                  
                   
                    cc.ActesSupp = MgmtDevis.GetCommActeSupDevis(cc);
                    cc.Radios = MgmtDevis.GetCommActeSupDevis(cc, "R");
                    cc.photos = MgmtDevis.GetCommActeSupDevis(cc, "P");
                    cc.Materiels = MgmtDevis.GetCommMaterielsDevis(cc);
                    cc.AutrePersonnes = MgmtDevis.GetDevisAutrePersonne(cc);

                }
                
                
                lst.Add(d);
            }
            return lst;
        }
        public static void GetCommDevis(ref Devis_TK devis)
        {
            JArray json = DAC.getMethodeJsonArray("/CommTraitementDevisByIdDevis/" +devis.Id);

            List<CommActesTraitement> lst = new List<CommActesTraitement>();
            devis.actesTraitement = new List<CommTraitement>();

            foreach (JObject j in json)
            {

                CommTraitement c = Builders.BuildDevis.BuildCommTraitement(j);
                c.devis = devis;
                devis.actesTraitement.Add(c);
            }


        }
        public static void GetCommDevisOLD(ref Devis_TK  devis)
        {
            DataTable dt = DAC.getCommDevis(devis.Id);

            List<CommActesTraitement> lst = new List<CommActesTraitement>();

            devis.actesTraitement = new List<CommTraitement>();

            foreach (DataRow dr in dt.Rows)
            {

                CommTraitement c = Builders.BuildDevis.BuildCommDevis(dr);
                devis.actesTraitement.Add (c);
            }


        }
        public static List<Devis> getDevis(basePatient patient)
        {
            JArray json = DAC.getMethodeJsonArray("/Devis/Devis/" + patient.Id);
                       List<Devis> lst = new List<Devis>();

            foreach (JObject r in json)
            {
                Devis d = Builders.BuildDevis.BuildJ(r);
                d.patient = patient;                
                lst.Add(d);
            }        

                
            return lst;
        }
        public static List<Devis> getDevisOLD(basePatient patient)
        {
            DataTable dt = DAC.getDevis(patient);

            List<Devis> lst = new List<Devis>();
            foreach (DataRow r in dt.Rows)
            {
                Devis d = Builders.BuildDevis.Build(r);
                d.patient = patient;
                lst.Add(d);
            }
            return lst;
        }
        public static void AccepterDevis(int IdDevis)
        {
            DAC.AccepterDevis(IdDevis, DateTime.Now);
        }

        public static List<CommActesTraitement> GetCommActeSupDevis(CommTraitement comTraitement, string TYPE_ACTE_SUPP = "")
        {
            JArray json = DAC.getMethodeJsonArray("/GetCommActeSupTraitementsDevis/" + comTraitement.Id + "?type=" + TYPE_ACTE_SUPP);
            List<CommActesTraitement> lst = new List<CommActesTraitement>();


            foreach (JObject dr in json)
            {

                CommActesTraitement c = Builders.BuildDevis.BuildCommActeSuppDevis(dr);
                lst.Add(c);
            }


            return lst;
        }
        public static List<CommActesTraitement> GetCommActeSupDevisOLD(CommTraitement comTraitement, string TYPE_ACTE_SUPP = "")
        {
            DataTable dt = DAC.getActesSupDevis(comTraitement.Id, TYPE_ACTE_SUPP);

            List<CommActesTraitement> lst = new List<CommActesTraitement>();


            foreach (DataRow dr in dt.Rows)
            {

                CommActesTraitement c = Builders.BuildDevis.BuildCommActeSuppDevis(dr);
                lst.Add(c);
            }


            return lst;
        }

        public static List<CommMaterielTraitement> GetCommMaterielsDevis(CommTraitement com)
        {
            JArray json = DAC.getMethodeJsonArray("/CommDevisMatsTraitementsDevis/" + com.Id);
            List<CommMaterielTraitement> lst = new List<CommMaterielTraitement>();

            foreach (JObject r in json)
            {
                CommMaterielTraitement c = Builders.BuildDevis.BuildDevisMaterielJ(r);
                //c.Parent = com;
                lst.Add(c);
            }
            return lst;
        }

        public static List<CommMaterielTraitement> GetCommMaterielsDevisOLD(CommTraitement com)
        {
            DataTable dt = DAC.GetCommDevisMateriels(com);
            List<CommMaterielTraitement> lst = new List<CommMaterielTraitement>();

            foreach (DataRow r in dt.Rows)
            {
                CommMaterielTraitement c = Builders.BuildDevis.BuildDevisMateriel(r);
                //c.Parent = com;
                lst.Add(c);
            }
            return lst;
        }
        public static void SaveActesSupp(CommTraitement com, string TYPE_ACTE_SUPP = "")
        {
            DAC.SaveActesSuppDevis(com, TYPE_ACTE_SUPP);

        }
        public static void SaveMateriels(CommTraitement com)
        {
            DAC.setDevisMateriels(com);

        }
        public static List<CommAutrePersonne> GetDevisAutrePersonne(CommTraitement com)
        {
            //DataTable dt = DAC.GetDevisAutrePersonne(com);
            List<CommAutrePersonne> lst = new List<CommAutrePersonne>();

            //foreach (DataRow r in dt.Rows)
            //{
            //    CommAutrePersonne c = Builders.BuildDevis.BuildDevisAutrePersonne(r);
            //    // c.Parent = com;
            //    lst.Add(c);
            //}
            return lst;
        }
      
    }
}
