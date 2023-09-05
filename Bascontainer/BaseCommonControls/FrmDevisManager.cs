using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BL;
using BasCommon_BO;
using BaseCommonControls;

namespace BaseCommonControls
{
    public partial class FrmDevisManager : Form
    {


        

        private List<ActePG> _ListActePGCree = new List<ActePG>();
        public List<ActePG> ListActePGCree
        {
            get
            {
                return _ListActePGCree;
            }
            
        }



        

        private basePatient _Currentpatient;
        public basePatient Currentpatient
        {
            get
            {
                return _Currentpatient;
            }
            set
            {
                _Currentpatient = value;
            }
        }


        public FrmDevisManager(basePatient pat)
        {
            InitializeComponent();
            Currentpatient = pat;

            
        }



        private void InitDisplay()
        {

            InitDisplayDevis();



        }

        private void InitDisplayDevis()
        {
            dgvDevis.Rows.Clear();

            if (Currentpatient.devis == null)
                Currentpatient.devis = MgmtDevis.getDevis(Currentpatient);

            foreach (Devis d in Currentpatient.devis)
            {
                if ((d.DateAcceptation != null) || (d.DateArchivage != null)) continue;
                List<object> lstCell = new List<object>();

                lstCell.Add(d.DateProposition);

                if (d.DateEcheance == null)
                    lstCell.Add("");
                else
                    lstCell.Add(d.DateEcheance.Value);

                if (d.propositions == null)
                    d.propositions = PropositionMgmt.getPropositions(d);


                lstCell.Add(d.propositions.Count);

                string tr = "";
                double total = 0;
                foreach (Proposition p in d.propositions)
                {
                    if ((d.DateAcceptation == null) || (p.Etat == Proposition.EtatProposition.Accepté))
                    {
                        foreach (Traitement t in p.traitements)
                        {
                            if (tr != "") tr += "+";
                            tr += t.Libelle;
                        }
                        total += PropositionMgmt.GetTotal(p);
                    }
                }



                lstCell.Add(tr);

                


                int idx = dgvDevis.Rows.Add(lstCell.ToArray());
                dgvDevis.Rows[idx].Tag = d;



            }
        }


        
        private void btnOk_Click(object sender, EventArgs e)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FrmPropositionsManager_Load(object sender, EventArgs e)
        {                                
            
            InitDisplay();
        }

        
     
        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvDevis.SelectedRows.Count == 0 )  return;
            FrmAcceptDevis frm = new FrmAcceptDevis((Devis)dgvDevis.SelectedRows[0].Tag);
            if (frm.ShowDialog() == DialogResult.OK)
            {

                AccepteProposition(Currentpatient.propositions, (Devis)dgvDevis.SelectedRows[0].Tag, frm.Value,frm.acteschoisis, null);
                InitDisplay();
            }


            
        }

        private void AccepteProposition(List<Proposition> AllPropositions, Devis d, List<Proposition> propAcceptees,List<ActePGPropose> actesAAjouter, DateTime? datedebut)
        {


            DateTime chooseDate = DateTime.Now;

            DateTime? maxDate = DateTime.MinValue;

            if ((Currentpatient.infoscomplementaire != null) && (Currentpatient.infoscomplementaire.DateDebutTraitement != null))
                maxDate = Currentpatient.infoscomplementaire.DateDebutTraitement.Value;

            // if (dgvpropositions.SelectedRows.Count == 0) return;


            Traitement traitementToClose = null; 
            if (datedebut == null)
            {

                FrmDate frm = new FrmDate("Choix de la date de début de traitement", "Date de début ?");

                foreach (Proposition pr in Currentpatient.propositions)
                    if (pr.Etat == Proposition.EtatProposition.Accepté)
                        foreach (Traitement t in pr.traitements)
                            foreach (Semestre s in t.semestres)
                            {
                                if (s.DateFin == null)
                                {
                                    maxDate = null;
                                    traitementToClose = t;
                                    break;
                                }
                                else
                                    if (maxDate < s.DateFin.Value)
                                        maxDate = s.DateFin.Value;

                                foreach (Surveillance su in s.surveillances)
                                {
                                    if (su.DateFin == null)
                                    {
                                        maxDate = null;
                                        traitementToClose = t;
                                        break;
                                    }
                                    else
                                        if (maxDate < su.DateFin.Value)
                                            maxDate = su.DateFin.Value;
                                }
                            }

                if ((maxDate != null) && (maxDate != DateTime.MinValue) && (maxDate>=DateTime.Now.Date)) 
                    frm.Value = maxDate.Value; 
                else 
                    frm.Value = DateTime.Now;

                frm.Text = "Date de début de traitement";


                if (frm.ShowDialog() == DialogResult.OK)
                    chooseDate = frm.Value;
                else
                    return;

            }
            else
                chooseDate = datedebut.Value;


            //si maxDate est a null, cela signifie que le traitement precedent est un traitement Adulte sans date de fin, 
            // Il faut donc la cloturer
            if ((traitementToClose!=null) &&(maxDate == null))
            {
                PropositionMgmt.CloseTraitement(traitementToClose, chooseDate);
            }

            PropositionMgmt.AccepterPropositions(AllPropositions, propAcceptees, d, chooseDate);

            maxDate = chooseDate;
            foreach (Proposition p in propAcceptees)
                foreach (Traitement t in p.traitements)
                    foreach (Semestre s in t.semestres)
                        if ((s.DateFin!=null) && (s.DateFin > maxDate)) maxDate = s.DateFin;

            foreach (ActePGPropose acteToAdd in actesAAjouter)
            {
                acteToAdd.DateExecution = maxDate;

                PropositionMgmt.AddActFromActePropose(acteToAdd, Currentpatient);
            }

            foreach (Proposition p in propAcceptees)
            {
                List<ActePG> lst = PropositionMgmt.AppliquerLePlanPourBaseDiag(chooseDate, p, Currentpatient);

                foreach (ActePG acte in lst)
                    ListActePGCree.Add(acte);

                //ScenarioCommClinique scenar = MgmtScenarioCommClinique.GetScenario(p.IdScenario);


                FrmApplyToDo frmscenar = new FrmApplyToDo(p);

                if (frmscenar.ShowDialog() == DialogResult.OK)
                {
                    MgmtCommentairesFaitAFaire.AddFromScenario(frmscenar.value, p, Currentpatient);
                    p.IdScenario =frmscenar.value==null?-1: frmscenar.value.Id;
                    PropositionMgmt.updateProposition(p);

                }


            }

            ///////////////////////////////////

            List<EntentePrealable> lstep = MgmtDemandeEntente.GetEntentePrealableFromIdPatient(Currentpatient);

            List<EntentePrealable> lstepnotassociate = new List<EntentePrealable>();
            foreach (EntentePrealable ep in lstep)
            {                
                
                ActePG acteassocie = null;
                if (Currentpatient.ActesPG == null)
                    Currentpatient.ActesPG = BasCommon_BL.ActesPGMgmt.GetActesPG(Currentpatient);



                foreach (ActePG apg in Currentpatient.ActesPG)
                {
                    if (apg.Id_DEP == ep.IdModele)
                        acteassocie = apg;

                }

                if (acteassocie == null) lstepnotassociate.Add(ep);

            }


            foreach (Proposition p in propAcceptees)
                foreach (Traitement t in p.traitements)
                    foreach (Semestre s in t.semestres)
                    {
                        
                        foreach (EntentePrealable ep in lstepnotassociate)
                        {
                            if ((ep.typetraitement == EntentePrealable.TypeDeTraitement.Semestre) || (ep.typetraitement == EntentePrealable.TypeDeTraitement.Debut))
                            {
                                if ((ep.Semestre == s.NumSemestre) || (ep.typetraitement == EntentePrealable.TypeDeTraitement.Debut && s.NumSemestre==1))
                                {
                                    if (MessageBox.Show("Souhaitez-vous associer le semestre " + s.NumSemestre.ToString() + " à la DEP du " + ep.dateProposition.ToShortDateString() + " ?","Associer DEP",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                                    {

                                        foreach (ActePG a in ListActePGCree)
                                            if (a.IdSemestrePlanGestionAssocie == s.Id)
                                            {
                                                ActesPGMgmt.RemoveDEPReference(ep.IdModele);
                                                ActesPGMgmt.AddDEPReference(ep, a);
                                            }
                                        

                                    }
                                }
                            }
                        }
                    }
            

            

            //////////////////////////////////


            DialogResult = DialogResult.OK;
            Close();

        }

        private void pnlChooseProposition_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void pnlEcheancier_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        

      

        private void pnlEnd_VisibleChanged(object sender, EventArgs e)
        {
                           
        }
                


        

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void rbJusquau_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbMntEch_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void pnlEcheancier_Paint(object sender, PaintEventArgs e)
        {

        }

        

        

        private void btnPrint_Click(object sender, EventArgs e)
        {

           
        }



      
        public static void AddCourrierAttributsDevis(Correspondant Praticien, 
                                                        Devis dev,
                                                        basePatient _CurrentPatient)
        {

            if (dev.actesHorstraitement == null)
                dev.actesHorstraitement = MgmtDevis.getactesHorstraitement(dev);
            
            TemplateActePG surv = TemplateApctePGMgmt.getCodeSecu("SURV");
            TemplateActePG survhn = TemplateApctePGMgmt.getCodeSecu("SURV_HN");


            OLEAccess.BASLetter.AddAttribut("ID_DEVIS", dev.Id);
            
            
            OLEAccess.BASLetter.AddAttribut("NbProps", dev.propositions.Count);
            OLEAccess.BASLetter.AddAttribut("NbOptions", dev.actesHorstraitement.Count);

            int Optionnum = 1;
            foreach (ActePGPropose acte in dev.actesHorstraitement)
            {

                if ((acte.template == null) && (acte.IdTemplateActePG >= 0))
                    acte.template = TemplateApctePGMgmt.getCodeSecu(acte.IdTemplateActePG);

                OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_Montant", acte.Qte * acte.Montant);
                OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_MontantAvantRemise", acte.Qte * acte.MontantAvantRemise);
                OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_Qte", acte.Qte);
                OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_Libelle", acte.Libelle);
                OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_PartSecu", acte.template.Coeff*acte.template.Code.Valeur);
                OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_PartMutuelle", (acte.Qte*acte.Montant)-(acte.template.Coeff * acte.template.Code.Valeur));
                OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_LibSecu", acte.template.DisplayCodeNVal);
                Optionnum++;
            }

            
            int propnum = 1;
            foreach (Proposition prop in dev.propositions)
            {
                if (prop.Etat > Proposition.EtatProposition.Soumis) continue;

                
                int i = 0;
                


                foreach (Traitement t in prop.traitements)
                {
                    double tariftotal = TraitementMgmt.getTotal(t);
                    int nbMois = 0;
                    double RmbMutuelleParSemestre = 0;
                    double RmbSecuParSemestre = 0;
                    string LibSecuParSemestre = "";

                    foreach (Semestre s in t.semestres)
                    {
                        nbMois += s.traitementSecu.NBMois.Value;                        
                    }

                    double secu = t.semestres.Count==0?0: t.semestres[0].traitementSecu.Code.Valeur * t.semestres[0].traitementSecu.Coeff;

                    RmbSecuParSemestre += secu;
                    RmbMutuelleParSemestre += t.semestres.Count==0?0: t.semestres[0].Montant_Honoraire - secu;
                    if (LibSecuParSemestre != "") LibSecuParSemestre += "+";
                    LibSecuParSemestre += "(" + (t.semestres.Count == 0 ? "" : t.semestres[0].traitementSecu.DisplayCodeNVal) + ")";


                    double totalParMois = tariftotal / nbMois;



                    PropositionObjectForLetters ob = new PropositionObjectForLetters();
                    ob.TarifParMois = totalParMois;
                    ob.Honoraires = t.semestres[0].Montant_Honoraire;
                    ob.NbMois = nbMois;
                    ob.PartSecu = RmbSecuParSemestre;
                    ob.PartMutuelle = RmbMutuelleParSemestre;
                    ob.LibSecu = LibSecuParSemestre;
                    ob.CodeTraitement = t.CodeTraitement;

                    //OLEAccess.BASLetter.AddAttribut("Propositions", ob);
                    
                    
                    if (!double.IsInfinity(totalParMois))OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_TarifParMois", totalParMois.ToString("0.00"));
                    OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Honoraires", t.semestres[0].Montant_Honoraire.ToString("0.00"));
                    OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_TarifPropose", t.semestres[0].Montant_Honoraire.ToString("0.00"));
                    OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_TarifNormal", t.semestres[0].traitementSecu.Valeur.ToString("0.00"));
                    OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_NbMois", nbMois.ToString());
                    OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_PartSecu", RmbSecuParSemestre.ToString("0.00"));
                    OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_PartMutuelle", RmbMutuelleParSemestre.ToString("0.00"));
                    OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_LibSecu", LibSecuParSemestre);
                    OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_CodeTraitement", t.CodeTraitement.Trim());
                    

                                     
                    i++;
                }

                

                propnum++;
            }


           // OLEAccess.BASLetter.AddAttribut("Devis", devis);

            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id);
            OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et " + m.ToString() + " mois");
            OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Genre == basePatient.Sexe.Feminin ? "F" : "M");
            OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);

            if (_CurrentPatient.MainAdresse != null)
            {
                OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _CurrentPatient.MainAdresse.adresse.Adr1);
                OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _CurrentPatient.MainAdresse.adresse.Adr2);
                OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _CurrentPatient.MainAdresse.adresse.CodePostal);
                OLEAccess.BASLetter.AddAttribut("VillePatient", _CurrentPatient.MainAdresse.adresse.Ville);
                OLEAccess.BASLetter.AddAttribut("PaysPatient", _CurrentPatient.MainAdresse.adresse.Pays);
            }
            
            if (_CurrentPatient.Tutoiement)
                OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _CurrentPatient.DateNaissance.Date.ToString());
			 OLEAccess.BASLetter.AddAttribut("NumSecu", _CurrentPatient.NumSecu.ToString());
            OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());

            OLEAccess.BASLetter.AddAttribut("ID_PRATICIEN", Praticien.Id);

            OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
            OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
            OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
            OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.Mail);
            OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
            OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.Tel);
            OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.Tel);
            OLEAccess.BASLetter.AddAttribut("TelPortablePraticien", Praticien.Tel);
            OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.Fax);

            OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.Adresse1);
            OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.Adresse2);
            OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.CodePostal);
            OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.Ville);
            if (Praticien.GenreFeminin)
                OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
            else
                OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");




        }


        public static void GenerateAndPrintDevis(string file, Correspondant Praticien, Correspondant c, Devis d, basePatient patient)
        {


            AddCourrierAttributsDevis(Praticien,d, patient);
            OLEAccess.BASLetter.GenerateFrom(file);


        }


       
        public static Devis CreateDevisInt(basePatient CurrentPatient)
        {
            FrmWizardPropositions frm = new FrmWizardPropositions(CurrentPatient.infoscomplementaire.NbSemestresEntame, CurrentPatient);

            if (frm.ShowDialog() == DialogResult.OK)
            {

                List<Proposition> lstProp = new List<Proposition>();

                foreach (Proposition p in frm.value)
                {
                    PropositionMgmt.InsertFullProposition(p);
                    lstProp.Add(p);
                }

                Devis d = MgmtDevis.CreateDevis(lstProp, frm.ActesMateriel,frm.tpetrmnt);
                d.TypeDevis = frm.tpetrmnt;

                CurrentPatient.devis.Add(d);
                
                CommonActions.PrintDevis(d,CurrentPatient);

                return d;
            }

            return null;
        }


        private void btnPrintDevis_Click(object sender, EventArgs e)
        {


           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           
        }

        private void dgvpropositions_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            


        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRevoirDevis_Click(object sender, EventArgs e)
        {
            
        }

        private void supprimerLeDevisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvDevis.SelectedRows.Count == 0) return;

            Devis devis = ((Devis)dgvDevis.SelectedRows[0].Tag);

            if (devis.DateAcceptation != null)
            {
                MessageBox.Show("Ce devis à été accepté\nSuppression impossible", "suppression impossible", MessageBoxButtons.OK);
                return;
            }

            if (MessageBox.Show("Souhaitez-vous réellement supprimer ce devis?", "Supression devis", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            MgmtDevis.DeleteDevis(devis);

            Currentpatient.devis.Remove(devis);
            InitDisplayDevis();
        }

        private void revoirLeDevisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoCourrier hc = HistoCourrierMgmt.getHistoriqueDevis(((Devis)dgvDevis.SelectedRows[0].Tag));

            if (hc != null)
            {

                string historic = System.Configuration.ConfigurationManager.AppSettings["HistoriqueFolder"];
                if (hc != null)
                {
                    if (System.IO.File.Exists(historic + "\\" + hc.Fichier.Trim()))
                    {
                        OLEAccess.BASLetter.Open(historic + "\\" + hc.Fichier.Trim());
                    }
                    else
                    {
                        MessageBox.Show("Fichier introuvable:\n" + historic + "\\" + hc.Fichier.Trim());
                    }

                }



                //OLEAccess.BASLetter.Open(hc.Fichier);
            }
            else
                MessageBox.Show("Aucun historique pour ce devis");
        }

        private void modifierLesTarifsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
        }

        private void button2_Click_3(object sender, EventArgs e)
        {
            if (dgvDevis.SelectedRows.Count == 0) return;
            FrmArchivageDevis frm = new FrmArchivageDevis((Devis)dgvDevis.SelectedRows[0].Tag);
            if (frm.ShowDialog() == DialogResult.OK)
            {

                
                frm.CurrentDevis.EmplacementArchivage = frm.emplacement;
                frm.CurrentDevis.ArchivePar = frm.Ecrivain;
                MgmtDevis.ArchiverDevis(frm.CurrentDevis);
                InitDisplayDevis();
            }
        }

        private void btnPrintDevis_Click_1(object sender, EventArgs e)
        {

            if (dgvDevis.SelectedRows.Count < 1) return;
            CommonActions.PrintDevis((Devis)dgvDevis.SelectedRows[0].Tag, Currentpatient);
            InitDisplay();
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Devis dev = ((Devis)dgvDevis.SelectedRows[0].Tag);

            FrmUpdateTarifProposition frm = new FrmUpdateTarifProposition(dev,
                                                                            Currentpatient);

            if (frm.ShowDialog() == DialogResult.OK)
            {

                BasCommon_BL.MgmtDevis.DeleteDevis(dev);

                BasCommon_BL.MgmtDevis.CreateDevis(dev.propositions, dev.actesHorstraitement, dev.TypeDevis);

            }
        }
    }
}
