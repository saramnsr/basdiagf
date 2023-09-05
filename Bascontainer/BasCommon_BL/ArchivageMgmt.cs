using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using BasCommon_DAL;
using System.Configuration;
using System.ComponentModel;
using System.IO;
using System.Xml;

namespace BasCommon_BL
{

    public static  class ArchivageMgmt
    {

       

        private static string _patArchiveFile = "";
        public static string patArchiveFile
        {
            get
            {
                return _patArchiveFile;
            }
            set
            {
                _patArchiveFile = value;
            }
        }

        public static bool IsArchiveMode
        {
            get
            {
                return DAC.ArchiveMode;
            }
            set
            {
                DAC.ArchiveMode = value;
            }
            
        }

        public static void ExportPatient(basePatient patient, string filename)
        {
            if (patient.contacts == null)
                baseMgmtPatient.FillContacts(patient);
            if (patient.Correspondants == null)
            {
                patient.Correspondants = MgmtCorrespondants.getCorrespondantsOf(patient);
                foreach (LienCorrespondant lc in patient.Correspondants)
                {
                    lc.correspondant = MgmtCorrespondants.getCorrespondant(lc.IdCorrespondance);
                    MgmtCorrespondants.FillContacts(lc.correspondant);
                    if (lc.IsMutuelle)
                        patient.Mutuelle = MutuelleMgmt.getMutuelle(lc.IdCorrespondance);
                    if (lc.IsCaisse)
                        patient.caisse = CaissesManager.getCaisse(lc.IdCorrespondance);
                }
            }

            if (patient.propositions == null)
                patient.propositions = PropositionMgmt.getPropositions(patient);



            if (patient.CommentsHisto == null)
                patient.CommentsHisto = BasCommon_BL.MgmtCommentairesHisto.GetAllCommentaires(patient);

            if (patient.FeuillesDeSoins == null)
                patient.FeuillesDeSoins = MgmtFeuillesDeSoin.GetFeuillesDeSoin(patient);

            if (patient.ententesPrealable == null)
                patient.ententesPrealable = MgmtDemandeEntente.GetEntentePrealableFromIdPatient(patient);


            if (patient.devis == null)
                patient.devis = MgmtDevis.getDevis(patient);

            if (patient.PaiementReels == null)
                patient.PaiementReels = MgmtEncaissement.GetPaiementReels(patient);

            if (patient.Encaissements == null)
                patient.Encaissements = MgmtEncaissement.GetEncaissements(patient);

            if (patient.Echeances == null)
                patient.Echeances = EcheancesMgmt.GetEcheances(patient);

            if (patient.ActesPG == null)
                patient.ActesPG = ActesPGMgmt.GetActesPG(patient);

            

            if (patient.suivisBaseLabo == null)
                patient.suivisBaseLabo = BaseLaboMgmt.GetAllSuivis(patient.Id);
            if (patient.lstObjFrmBasPhoto == null)
                ImagesMgmt.AffectImageToPatient(patient);

            if (patient.appointements == null)
                patient.appointements = AppointementsMgmt.getAppointments(patient);

            if (patient.commentairesClinique == null)
            {
                DateTime? dtDebut = ActesPGMgmt.getDateDebutTraitement(patient);
                patient.commentairesClinique = MgmtCommentairesFaitAFaire.GetCommCliniqueByIdNDte(patient.Id, dtDebut);
            }



            if (patient.infoscomplementaire == null)
                patient.infoscomplementaire = baseMgmtPatient.getinfocomplementaire(patient.Id);

            if (patient.AppareilsEnBouche == null)
                patient.AppareilsEnBouche = EnBoucheMgmt.GetAllAppareilsEnBouche(patient);

            if (patient.ElementsEnBouche == null)
                patient.ElementsEnBouche = EnBoucheMgmt.GetAllElementsEnBouche(patient);




            foreach (PaiementReel pr in patient.PaiementReels)
            {
                if (pr.lstpatient == null)
                    pr.lstpatient = MgmtEncaissement.GetListPatientsAffectedByPaiement(pr);


            }

            foreach (CommentHisto ch in patient.CommentsHisto)
            {
                if (ch.Ecrivain == null)
                    ch.Ecrivain = UtilisateursMgt.getUtilisateur(ch.Id_Ecrivain);

                ch.patient = patient;
            }



            foreach (Encaissement e in patient.Encaissements)
            {
                foreach (PaiementReel pr in patient.PaiementReels)
                    if (e.IdPaiementReel == pr.Id)
                    {
                        e.paiementreel = pr;
                        break;
                    }
                e.patient = patient;
            }

            foreach (Echeance e in patient.Echeances)
            {
                foreach (Encaissement pr in patient.Encaissements)
                    if (e.ID_Encaissement == pr.Id)
                    {
                        e.encaissement = pr;
                        break;
                    }

                foreach (ActePG pr in patient.ActesPG)
                    if (e.IdActe == pr.Id)
                    {
                        e.acte = pr;
                        break;
                    }
                e.patient = patient;
            }

            foreach (FeuilleDeSoin fs in patient.FeuillesDeSoins)
                foreach (ActePG a in patient.ActesPG)
                {
                    if (a.Id_FS == fs.Id)
                        fs.actes.Add(a);
                }

            foreach (RHAppointment a in patient.appointements)
            {
                a.patient = patient;
            }

            foreach (CommClinique cc in patient.commentairesClinique)
            {
                cc.patient = patient;
                if (cc.Acte == null) cc.Acte = ActesMgmt.getActe(cc.IdActe);
                cc.praticien = UtilisateursMgt.getUtilisateur(cc.IdPraticien);
                cc.Assistante = UtilisateursMgt.getUtilisateur(cc.IdAssistante);
                cc.Secretaire = UtilisateursMgt.getUtilisateur(cc.IdSecretaire);
                if (cc.Radios == null) MgmtCommentairesFaitAFaire.GetActesSupp(cc);
                if (cc.photos == null) MgmtCommentairesFaitAFaire.GetActesSupp(cc);
                if (cc.AutrePersonnes == null) MgmtCommentairesFaitAFaire.GetCommCliniqueAutrePersonne(cc);
                if (cc.DentsAExtraire == null) MgmtCommentairesFaitAFaire.GetCommDentAExtraire(cc);


                foreach (CommClinique childcc in patient.commentairesClinique)
                {
                    if (childcc.Id == cc.IdParentComment)
                        cc.ParentComment = childcc;
                }

            }


            foreach (LienCorrespondant c in patient.Correspondants)
            {
                c.patient = patient;
                if (c.correspondant == null) c.correspondant = MgmtCorrespondants.getCorrespondant(c.IdCorrespondance);
                if (c.correspondant.Echances == null) c.correspondant.Echances = EcheancesMgmt.GetEcheances(c.correspondant);
                MgmtCorrespondants.FillContacts(c.correspondant);
            }

            patient.recontact = MgmtRecontact.GetCurrentRecontact(patient);
            baseMgmtPatient.getRisques(patient);


            foreach (Proposition p in patient.propositions)
            {
                p.echeancestemp = MgmtDevis.get_tempecheances(p);
            }

            foreach (ActePG a in patient.ActesPG)
            {
                foreach (Echeance ec in patient.Echeances)
                    if (ec.IdActe == a.Id)
                    {
                        if (a.lstEcheances == null)
                            a.lstEcheances = new List<Echeance>();
                        a.lstEcheances.Add(ec);
                    }
                a.patient = patient;

                foreach (FeuilleDeSoin fs in patient.FeuillesDeSoins)
                {
                    if (fs.Id == a.Id_FS)
                        a.FeuilleDeSoinAssocier = fs;
                    fs.patient = patient;
                }

                foreach (EntentePrealable ep in patient.ententesPrealable)
                {
                    if (ep.IdModele == a.Id_DEP)
                        a.DEPAssocier = ep;


                }



            }



            StringWriter memoryStream = new StringWriter();

            XmlTextWriter writer = new XmlTextWriter(memoryStream);
            writer.Formatting = Formatting.Indented;

            CustomXMLSerializer.WriteXML(patient, writer, "Main");

            File.WriteAllText(filename, memoryStream.ToString());
        }

        

        public static basePatient ImportPatient(string filename)
        {
            basePatient mdl = new basePatient();

            XmlTextReader rd = null;

            rd = new XmlTextReader(filename);

            try
            {
                CustomXMLSerializer.ReadXML(mdl, rd);
                patArchiveFile = filename;
                return mdl;
            }
            catch (System.Exception ex)
            {
                return null;
            }
            finally
            {
                rd.Close();
            }
        }


        //public event ProgressChangedEventHandler OnProgress;

        /*
        public void Transfert(int patientId)
        {

           
            
            string connectionString = BasCommon_DAL.DAC.getArchiveConnection();

            string createscript = ConfigurationManager.AppSettings["ScriptCreation"];
            DoInprogress(0, "Création base");


            
            DAC.ExecuteBlockScript(createscript, connectionString, true);

            DoInprogress(25, "Remplissage Patient");


            string QueryListTableFile = ConfigurationManager.AppSettings["QueryList"];
            string[] ss = File.ReadAllLines(QueryListTableFile);

           


            float idx = 0f;
            foreach (string s in ss)
            {
                DoInprogress(25 + ((idx / (ss.Length - 1)) * 25), "Remplissage Patient ");
                DAC.TransfertQuery(string.Format(s,patientId), connectionString);
                idx++;
            }

            string contexteTableFile = ConfigurationManager.AppSettings["contexteTableList"];
            ss = File.ReadAllLines(contexteTableFile);
            idx = 0f;
            foreach (string s in ss)
            {
                DoInprogress(50 + ((idx / (ss.Length - 1)) * 25), "Remplissage contexte + " + s);
                DAC.TransfertTable(s, connectionString);
                idx++;
            }

            DoInprogress(100, "Done");
        }
        */
       
        /*
        private void DoInprogress(double progresspourcentage, string message)
        {

            if (OnProgress != null)
                OnProgress(this, new ProgressChangedEventArgs((int)(progresspourcentage > 100 ? 100 : progresspourcentage), message));
        }

    */


    }
}
