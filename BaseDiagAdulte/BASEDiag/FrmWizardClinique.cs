using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BL;
using BASEDiag_BO;

namespace BASEDiag
{
    public partial class FrmWizardClinique : Form
    {

        Proposition currentproposition = null;

        private Patient _CurrentPatient;
        public Patient CurrentPatient
        {
            get
            {
                return _CurrentPatient;
            }
            set
            {
                _CurrentPatient = value;
            }
        }

        int CurentIdxPnl = 0;

        public FrmWizardClinique(Patient patient)
        {
            CurrentPatient = patient;
            InitializeComponent();
            propositionCtrl1.CurrentPatient = CurrentPatient;
            
        }


        private void ShowPanel(Panel pnl)
        {
            foreach (Control ctrl in pnlContainer.Controls)
            {
                ctrl.Visible = false;
            }
            pnl.Show();
        }



        private void LoadPPT(string subFolder)
        {
            try
            {
                string pptfolder = System.Configuration.ConfigurationManager.AppSettings["PPTFolder"] + "\\" + subFolder;
                lvPPT.Items.Clear();
                foreach (System.String s in System.IO.Directory.GetFiles(pptfolder))
                {
                    System.IO.FileInfo nfo = new System.IO.FileInfo(s);

                    ListViewItem itm = new ListViewItem();
                    itm.ImageIndex = 0;
                    itm.Text = nfo.Name;
                    itm.Tag = s;

                    lvPPT.Items.Add(itm);
                }
            }
            catch (System.Exception)
            {

            }


        }


        private void FrmWizardClinique_Load(object sender, EventArgs e)
        {
            cbxSemEntames.SelectedIndex = 0;

            pnlContainer.Controls.Clear();
            pnlContainer.Controls.Add(pnlLstBxDiag);
            pnlContainer.Controls.Add(pnlDefinitionPlanTraitement);

            ShowPanel(pnlLstBxDiag);

            LoadPPT("Wizard");
        }

        private void RegenerateDiagObjTraitmnt()
        {
            ResumeCliniqueMgmt.GenerateCurrentDiags();

            lstBxDiag.Items.Clear();
            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
                lstBxDiag.Items.Add(cd);


            List<CommonObjectifFromDiag> lstobjs = CommonDiagnosticsMgmt.getCommonObjectifs(ResumeCliniqueMgmt.resumeCl.diagnostics);

            lstBxObjectifs.Items.Clear();
            foreach (CommonObjectif cd in CurrentPatient.SelectedObjectifs)
                lstBxObjectifs.Items.Add(cd);

            foreach (CommonObjectifFromDiag cd in lstobjs)
                if (!CurrentPatient.SelectedObjectifs.Contains(cd.objectif))
                    lstBxObjectifs.Items.Add(cd);


          
        }
             

        private void lstBxObjectifs_DrawItem(object sender, DrawItemEventArgs e)
        {

            CommonDiagnostic selecteddiag = (CommonDiagnostic)lstBxDiag.SelectedItem;

            CommonObjectif obj = null;
            if (e.Index == -1) return;

            Brush b = Brushes.Black;

            if (lstBxObjectifs.Items[e.Index] is CommonObjectifFromDiag)
            {
                obj = ((CommonObjectifFromDiag)lstBxObjectifs.Items[e.Index]).objectif;
                if (((CommonObjectifFromDiag)lstBxObjectifs.Items[e.Index]).diagnostic == selecteddiag)
                    b = Brushes.Blue;
            }

            if (lstBxObjectifs.Items[e.Index] is CommonObjectif)
                obj = ((CommonObjectif)lstBxObjectifs.Items[e.Index]);







            if (CurrentPatient.SelectedObjectifs.Contains(obj))
                b = Brushes.Green;

            e.Graphics.DrawString(lstBxObjectifs.Items[e.Index].ToString(), lstBxObjectifs.Font, b, e.Bounds.Location);

        }

        private void lstBxObjectifs_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstBxObjectifs.SelectedItem is CommonObjectif)
            {
                CurrentPatient.SelectedObjectifs.Remove(((CommonObjectif)lstBxObjectifs.SelectedItem));
            }

            if (lstBxObjectifs.SelectedItem is CommonObjectifFromDiag)
            {
                CommonObjectif app = ((CommonObjectifFromDiag)lstBxObjectifs.SelectedItem).objectif;
                CurrentPatient.SelectedObjectifs.Add(app);
            }
            RegenerateDiagObjTraitmnt();
        }

        private void lstBxDiag_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBxObjectifs.Invalidate();
        }

        private void pnlDefinitionPlanTraitement_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlDefinitionPlanTraitement.Visible == true)
            {
                lstBxObjectifsDefinitifs.Items.Clear();
                foreach (CommonObjectif cd in CurrentPatient.SelectedObjectifs)
                    lstBxObjectifsDefinitifs.Items.Add(cd);

                lstBxAppareillages.Items.Clear();
                foreach (Appareil app in AppareilMgmt.appareils)
                    lstBxAppareillages.Items.Add(app);


            }
        }

        private void pnlLstBxDiag_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlLstBxDiag.Visible == true)
                RegenerateDiagObjTraitmnt();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            string chk = "";
            foreach (Proposition p in CurrentPatient.propositions)
            {
                if (chk != "") chk += "\n";

                string tmpchk = PropositionMgmt.CheckValiditeRemboursement(p, CurrentPatient.infoscomplementaire.DateDebutTraitement.Value, CurrentPatient.infoscomplementaire.NbSemestresEntame);
                chk += tmpchk != "" ? p.libelle + " : " + tmpchk : "";
            }

            if (chk != "")
            {
                if (MessageBox.Show(chk + "\n Souhaitez-vous continuer ?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
            }



            if (CurentIdxPnl < pnlContainer.Controls.Count - 1)
                ShowPanel((Panel)pnlContainer.Controls[++CurentIdxPnl]);
            else
            {
                SaveInfoComplementaires();
                PatientMgmt.setinfocomplementaire(CurrentPatient);
                PropositionMgmt.setPropositions(CurrentPatient, CurrentPatient.infoscomplementaire.DateDebutTraitement.Value);
                Close();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CurentIdxPnl > 0)
                ShowPanel((Panel)pnlContainer.Controls[--CurentIdxPnl]);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            AjoutTraitement(currentproposition);

            
            
        }

        private bool AjoutTraitement(Proposition prop)
        {
            
            bool iscanceled = false;
             
             
            FrmAddTraitmnt frm = new FrmAddTraitmnt(CurrentPatient, prop, cbxSemEntames.SelectedIndex, null, 0);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                
                prop.traitements.Add(frm.traitement);


                iscanceled = false;


                propositionCtrl1.BuildNInvalidate();

            }else
                iscanceled = true;

            if (!iscanceled)
                if (MessageBox.Show("Souhaitez-vous ajoutez une autre phase de traitement ?", "Autre traitement ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    AjoutTraitement(prop);
            
            return !iscanceled;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!SaveInfoComplementaires()) return;

            Proposition prop = new Proposition();

            prop.libelle = "Proposition " + (CurrentPatient.propositions.Count + 1).ToString();
            prop.patient = CurrentPatient;
            prop.DateEvenement = DateTime.Now;

            btnAddTraitement.Enabled = true;
            propositionCtrl1.Invalidate();
            currentproposition = prop;
            propositionCtrl1.AddProposition(prop);
            CurrentPatient.propositions.Add(prop);

            if (!AjoutTraitement(prop))
                propositionCtrl1.RemoveProposition(prop);
            
        }

        private void cbxNbSemestresEntamé_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }




        private void btnAddContention_Click(object sender, EventArgs e)
        {

            foreach (Traitement t in currentproposition.traitements)
                foreach (Semestre s in t.semestres)
                    if (s.CodeSemestre == "CONT2")
                        return;


            int sem = PropositionMgmt.FindSemestreAffecte(currentproposition);
            Traitement phasedeContention = null;

            foreach (Traitement t in currentproposition.traitements)
                if (t.Phase == TemplateActePG.EnumPhase.Contention)
                    phasedeContention = t;

            if (phasedeContention == null)
            {
                phasedeContention = new Traitement();
                phasedeContention.Libelle = "Contention";
                phasedeContention.Parent = currentproposition;
                phasedeContention.Phase = TemplateActePG.EnumPhase.Contention;


                TemplateActePG template = TemplateApctePGMgmt.getCodeSecu("CONT1");

                

                Semestre s = new Semestre();
                s.NumSemestre = sem + 1;
                s.traitementSecu = template;
                s.Montant_Honoraire = template.Valeur;
                s.CodeSemestre = "A1";
                s.NbSurveillance = 0;
                phasedeContention.semestres.Add(s);

                currentproposition.traitements.Add(phasedeContention);

            }
            else
                if (phasedeContention != null)
                {

                    TemplateActePG template = TemplateApctePGMgmt.getCodeSecu("CONT2");

                    
                    Semestre s = new Semestre();
                    s.NumSemestre = sem + 1;
                    s.traitementSecu = template;
                    s.NbSurveillance = 0;
                    s.CodeSemestre = "A2";
                    s.Montant_Honoraire = template.Valeur;
                    
                    phasedeContention.semestres.Add(s);
                }

            propositionCtrl1.BuildNInvalidate();
        }

        private void pnlPlanTraitement_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlPlanTraitement.Visible)
            {
                Fauteuil IamTheFauteuil = Fauteuilsmgt.GetWhoIam();

                if (IamTheFauteuil == null)
                {
                    FrmChoixFauteuil frm = new FrmChoixFauteuil();
                    frm.ShowDialog();
                    IamTheFauteuil = frm.Selection;
                }


                foreach (Utilisateur u in UtilisateursMgt.utilisateurs)
                {
                    if (u.Actif)
                    {
                        if (u.Fonction == "Praticien")
                            cbxPratResp.Items.Add(u);
                        else
                            cbxAssResp.Items.Add(u);
                    }
                }


                cbxSemEntames.Items.Add("Aucun");
                cbxSemEntames.Items.Add("1");
                cbxSemEntames.Items.Add("2");
                cbxSemEntames.Items.Add("3");
                cbxSemEntames.Items.Add("4");
                cbxSemEntames.Items.Add("5");
                cbxSemEntames.Items.Add("6");

                cbxSemEntames.SelectedIndex = 0;


                List<Utilisateur> currentutilisateurs = UtilisateursMgt.getUtilisateurInFauteuil(IamTheFauteuil, DateTime.Now);



                foreach (Utilisateur u in currentutilisateurs)
                {
                    if (cbxPratResp.Items.Contains(u)) cbxPratResp.SelectedItem = u;
                    if (cbxAssResp.Items.Contains(u)) cbxAssResp.SelectedItem = u;
                }

                dtpDebuTraitement.Value = DateTime.Now;
            }
        }

        private void cbxSemEntames_Load(object sender, EventArgs e)
        {
            
        }

        private void cbxSemEntames_SelectedIndexChanged(object sender, EventArgs e)
        {
            int delta = cbxSemEntames.SelectedIndex - CurrentPatient.infoscomplementaire.NbSemestresEntame;
            CurrentPatient.infoscomplementaire.NbSemestresEntame = cbxSemEntames.SelectedIndex;
            
        }

        private bool SaveInfoComplementaires()
        {
            if (CurrentPatient.infoscomplementaire == null)
                CurrentPatient.infoscomplementaire = new InfoPatientComplementaire();
            CurrentPatient.infoscomplementaire.PraticienResponsable = (Utilisateur)cbxPratResp.SelectedItem;
            CurrentPatient.infoscomplementaire.AssistanteResponsable = (Utilisateur)cbxAssResp.SelectedItem;

            if (CurrentPatient.infoscomplementaire.PraticienResponsable == null)
            {
                MessageBox.Show("Veuillez sélectionner un praticien responsable");
                return false;
            }

            if (CurrentPatient.infoscomplementaire.AssistanteResponsable == null)
            {
                MessageBox.Show("Veuillez sélectionner une assistante responsable");
                return false;
            }


            if (dtpDebuTraitement.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("La date de début de traitement ne peut pas être passée");
                return false;
            }

            if (cbxSemEntames.SelectedIndex == -1)
            {
                CurrentPatient.infoscomplementaire.NbSemestresEntame = 0;
            }
            else
            {

                if (cbxSemEntames.SelectedIndex == 7)
                    CurrentPatient.infoscomplementaire.NbSemestresEntame = -1;
                else
                    CurrentPatient.infoscomplementaire.NbSemestresEntame = cbxSemEntames.SelectedIndex;
            }


            CurrentPatient.infoscomplementaire.DateDebutTraitement = dtpDebuTraitement.Value;
            return true;
        }

        private void cbxPratResp_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPatient.infoscomplementaire.PraticienResponsable = (Utilisateur)cbxPratResp.SelectedItem;
        }

        private void cbxAssResp_SelectedIndexChanged(object sender, EventArgs e)
        {

            CurrentPatient.infoscomplementaire.AssistanteResponsable = (Utilisateur)cbxAssResp.SelectedItem;
        }

        private void dtpDebuTraitement_ValueChanged(object sender, EventArgs e)
        {
            if (CurrentPatient.infoscomplementaire!=null)
                CurrentPatient.infoscomplementaire.DateDebutTraitement = dtpDebuTraitement.Value;
        }

        private void cbxPratResp_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
                   
        }

        private void propositionCtrl1_OnSelectionChange(object sender, EventArgs e)
        {
            currentproposition = propositionCtrl1.SelectedProposition.proposition;
            btnAddContention.Enabled = currentproposition.traitements.Count > 0;
            BtnShowRisques.Enabled = PropositionMgmt.GetRisques(currentproposition).Count > 0;
            btnDEP.Enabled = currentproposition!=null;
            
        }

        private void propositionCtrl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                propositionCtrl1.RemoveSelection();
                
            }

            propositionCtrl1.Invalidate();
        }

        private void propositionCtrl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*
            if (propositionCtrl1.SelectedTraitement == null)
            {
                AjoutTraitement(currentproposition);            
            }
            else
            {
                 int idx = propositionCtrl1.SelectedProposition.traitements.IndexOf(propositionCtrl1.SelectedTraitement);
                    

                int NbSurv = 0;
                if ((idx + 1 < propositionCtrl1.SelectedProposition.traitements.Count) &&
                        (propositionCtrl1.SelectedProposition.traitements[idx + 1].traitementSecu.Nom == "SURV"))
                {
                    NbSurv = propositionCtrl1.SelectedProposition.traitements[idx + 1].NumSemestres.Count();
                }

                FrmAddTraitmnt frm = new FrmAddTraitmnt(CurrentPatient, currentproposition, cbxSemEntames.SelectedIndex, propositionCtrl1.SelectedTraitement, NbSurv);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    int delta = propositionCtrl1.SelectedTraitement.NumSemestres.Max() - frm.traitement.NumSemestres.Max();
               
                    
                    if ((idx+1<propositionCtrl1.SelectedProposition.traitements.Count)&&
                        (propositionCtrl1.SelectedProposition.traitements[idx+1].traitementSecu.Nom == "SURV"))
                    {
                        propositionCtrl1.SelectedProposition.traitements.RemoveAt(idx+1);                    
                    }

                    propositionCtrl1.SelectedProposition.traitements.Remove(propositionCtrl1.SelectedTraitement);
                    propositionCtrl1.SelectedProposition.traitements.Insert(idx, frm.traitement);

                    
                    propositionCtrl1.Rebuild();
                    propositionCtrl1.Invalidate();

                }

            }
             * */
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (currentproposition == null) return;

            FrmRisques frm = new FrmRisques(currentproposition);
            frm.Show();
        }

        private void btnDEP_Click(object sender, EventArgs e)
        {
            Proposition propositionselected = null;
            foreach (Proposition p in CurrentPatient.propositions)
            {
                if (p.Etat == Proposition.EtatProposition.Accepté)
                {
                    propositionselected = p;
                    break;
                }
            }

            if (propositionselected == null)
                propositionselected = currentproposition;


            if (propositionselected == null)
            {
                MessageBox.Show("Aucune proposition sélectionnée !");
                return;
            }

            FrmDEP frm = new FrmDEP(CurrentPatient);


            foreach (PoseAppareil pa in propositionselected.poseAppareils)
                if (pa.appareil != null)
                    frm.PlanDeTraitement += " " + pa.appareil.InfoDEP + "\n";


            string cotation = "TO90";
            foreach (Traitement t in propositionselected.traitements)
                foreach (Semestre s in t.semestres)
                    if (s.NumSemestre == 1)
                        cotation = s.traitementSecu.Code.Code + s.traitementSecu.Coeff.ToString();



            frm.CotationDesActes = cotation;

            frm.ShowDialog();
        }

        private void pnlPlanTraitement_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSigne_Click(object sender, EventArgs e)
        {
            if (currentproposition.Etat != Proposition.EtatProposition.Soumis)
            {
                if (currentproposition.Etat == Proposition.EtatProposition.Refusé)
                {
                    MessageBox.Show("La proposition à été refusé");
                    return;
                }
                if (currentproposition.Etat == Proposition.EtatProposition.NonImprimé)
                    if (MessageBox.Show("La proposition n'a pas été imprimé.\nSouhaitez Vous quand même l'accepter ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                if (currentproposition.Etat == Proposition.EtatProposition.Accepté)
                {
                    MessageBox.Show("La proposition est déja accepté");
                    return;
                }
            }

            foreach (Proposition p in CurrentPatient.propositions)
            {
                p.DateEvenement = DateTime.Now;

                if (currentproposition == p)
                {
                    p.Etat = Proposition.EtatProposition.Accepté;
                    p.DateAcceptation = DateTime.Now;
                    currentproposition = p;
                }
                else
                    p.Etat = Proposition.EtatProposition.Refusé;

                PropositionMgmt.updateProposition(p);
            }


            if (MessageBox.Show("Souhaitez-vous créer un plan de gestion pour BasePractice ?", "plan de gestion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {




                List<ActePG> retour = PropositionMgmt.AppliquerLePlanPourBaseDiag(CurrentPatient.infoscomplementaire.DateDebutTraitement.Value, currentproposition, CurrentPatient);

                bool cancontinue = true;
                if (BASEDiag_BL.ActesPGMgmt.IsTraitementEnCours(CurrentPatient))
                {
                    if (MessageBox.Show("Un traitement est actuellement en cours, souhaitez vous le remplacer ?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        BASEDiag_BL.ActesPGMgmt.DeleteActePGFor(CurrentPatient);
                    }
                    else cancontinue = false;
                }
                if (!cancontinue) return;

                foreach (ActePG apg in retour)
                {
                    BASEDiag_BL.ActesPGMgmt.InsertActePGWithEcheance(apg);

                }

                MessageBox.Show("Le plan de gestion à été créer!");

            }

            if (MessageBox.Show("Souhaitez-vous créer un plan de gestion pour Orthalis ?", "plan de gestion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

               // if (MessageBox.Show("ATTENTION Cette action va supprimer toutes les entrées du plan de gestion\n et toutes les DEPs initialement créées dans Orthalis pour ce patient\nSouhaitez-vous continuer ?", "Nettoyage Orthalis", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
               // {

                  //  BASEDiag_BL.ActesPGMgmt.CleanPGOrthalis(CurrentPatient);


                    List<ActePG> retour = PropositionMgmt.AppliquerLePlanPourOrthalis(CurrentPatient.infoscomplementaire.DateDebutTraitement.Value, currentproposition, CurrentPatient,cbxSemEntames.SelectedIndex);

                    foreach (ActePG apg in retour)
                    {
                        BASEDiag_BL.ActesPGMgmt.InsertActePGWithEcheanceForOrthalis(apg);

                    }

                    MessageBox.Show("Le plan de gestion à été créer!");
             //   }

            }
        }

        private void lstBxPersonneAContacter_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            FrmSpecialistes frm = new FrmSpecialistes(_CurrentPatient);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CorrespondantMgmt.setPersonnesAContacter(_CurrentPatient);

                RefreshListePersonneAContacter();

            }
            
        }

        private void RefreshListePersonneAContacter()
        {
            lstBxPersonneAContacter.Items.Clear();
            foreach (LienCorrespondant lc in _CurrentPatient.PersonnesAContacter)
            {
                lstBxPersonneAContacter.Items.Add(lc);
            }
        }

        private void button1_Click_3(object sender, EventArgs e)
        {

            
        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            if (SaveInfoComplementaires())
            {
                string chk = "";
                foreach (Proposition p in CurrentPatient.propositions)
                {
                    if (chk != "") chk += "\n";

                    string tmpchk = PropositionMgmt.CheckValiditeRemboursement(p, CurrentPatient.infoscomplementaire.DateDebutTraitement.Value, CurrentPatient.infoscomplementaire.NbSemestresEntame);
                    chk += tmpchk != "" ? p.libelle + " : " + tmpchk : "";
                }
             
                if (chk != "")
                {
                    if (MessageBox.Show(chk+"\n Souhaitez-vous continuer ?","Attention",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
                    {
                        GenerateNPrintDEvisConsentement(true);
                    }
                }else
                    GenerateNPrintDEvisConsentement(true);

            }
        }


        private void GenerateNPrintDEvisConsentement(bool printDirect)
        {
            string devis = System.Configuration.ConfigurationManager.AppSettings["CourrierDevis"];
            string consentement = System.Configuration.ConfigurationManager.AppSettings["CourrierConsentement"];

            if (!System.IO.File.Exists(devis))
            {
                FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
                frm.Text = "Choix du devis";
                if (frm.ShowDialog() != DialogResult.OK) return; else devis = frm.FileName;
            }

            Correspondant c = CorrespondantMgmt.getCorrespondant(_CurrentPatient.Id);
            Correspondant praticien = CorrespondantMgmt.getCorrespondant(((Utilisateur)cbxPratResp.SelectedItem).Id);
            GenerateAndPrintDevis(devis, praticien, c, printDirect);


            foreach (Proposition p in _CurrentPatient.propositions)
                p.Etat = Proposition.EtatProposition.Soumis;


            if (!System.IO.File.Exists(consentement))
            {
                FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
                frm.Text = "Choix du consentement";
                if (frm.ShowDialog() != DialogResult.OK) return; else consentement = frm.FileName;
            }

            c = CorrespondantMgmt.getCorrespondant(_CurrentPatient.Id);
            praticien = CorrespondantMgmt.getCorrespondant(((Utilisateur)cbxPratResp.SelectedItem).Id);
            GenerateAndPrintConsentement(consentement, praticien, c, printDirect);
        }


        private void GenerateAndPrintDevis(string file, Correspondant Praticien, Correspondant c, bool DirectPrint)
        {


            AddCourrierAttributsDevis(Praticien);

            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionDevis);

            if (!DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);
        }

        private void GenerateAndPrintConsentement(string file, Correspondant Praticien, Correspondant c, bool DirectPrint)
        {


            AddCourrierAttributsConsentement(Praticien);

            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionConsentement);


            if (!DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);

        }

        private void AddCourrierAttributs(Correspondant Praticien, Correspondant c)
        {

            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et "+m.ToString()+" mois");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Sexe);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _CurrentPatient.Adresse1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _CurrentPatient.Adresse2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _CurrentPatient.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePatient", _CurrentPatient.Ville);
            if (_CurrentPatient.Tutoiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _CurrentPatient.DateNaissance.Date.ToString());
			BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumSecu", _CurrentPatient.NumSecu.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProchainRDVPatient", _CurrentPatient.NextRDV.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());
            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateSoldePatient", _CurrentPatient.DateSoldePatient.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SoldePatient", _CurrentPatient.Solde.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateDernierRDVPatient", _CurrentPatient.LastRDV.ToString());

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_CORRESPONDANT", c.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitreCorrespondant", c.Titre);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomCorrespondant", c.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomCorrespondant", c.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailCorrespondant", c.Mail);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionCorrespondant", c.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixeCorrespondant", c.TelFixe);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProCorrespondant", c.TelProfessionnel);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelPortableCorrespondant", c.TelPortable);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxCorrespondant", c.Fax);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Correspondant", c.Adresse1Office);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Correspondant", c.Adresse2Office);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalCorrespondant", c.CodePostalOffice);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VilleCorrespondant", c.VilleOffice);
            if (c.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreCorrespondant", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreCorrespondant", "M");

            if (c.TuToiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementCorrespondant", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementCorrespondant", "VOUS");


            if (_CurrentPatient.Dentiste != null)
            {

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_DENTISTE", _CurrentPatient.Id.ToString());
            
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitreDentiste", _CurrentPatient.Dentiste.Titre);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomDentiste", _CurrentPatient.Dentiste.Nom);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomDentiste", _CurrentPatient.Dentiste.Prenom);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailDentiste", _CurrentPatient.Dentiste.Mail);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionDentiste", _CurrentPatient.Dentiste.Profession);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixeDentiste", _CurrentPatient.Dentiste.TelFixe);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProDentiste", _CurrentPatient.Dentiste.TelProfessionnel);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelPortableDentiste", _CurrentPatient.Dentiste.TelPortable);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxDentiste", _CurrentPatient.Dentiste.Fax);

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Dentiste", _CurrentPatient.Dentiste.Adresse1Office);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Dentiste", _CurrentPatient.Dentiste.Adresse2Office);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalDentiste", _CurrentPatient.Dentiste.CodePostalOffice);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VilleDentiste", _CurrentPatient.Dentiste.VilleOffice);
                if (_CurrentPatient.Dentiste.GenreFeminin)
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreDentiste", "F");
                else
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreDentiste", "M");

                if (_CurrentPatient.Dentiste.TuToiement)
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementDentiste", "TU");
                else
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementDentiste", "VOUS");

            }

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_Praticien", Praticien.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.Mail);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.TelFixe);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.TelProfessionnel);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelPortablePraticien", Praticien.TelPortable);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.Fax);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.Adresse1Office);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.Adresse2Office);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.CodePostalOffice);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.VilleOffice);
            if (c.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");




            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoExterneFace", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Face);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoExterneFaceSourire", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Face_Sourire);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoExterneProfil", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoExterneProfilSourire", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile_Sourire);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoExterneSourire", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Ext_Sourire);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalDroit", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Droit);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalFace", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Face);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalGauche", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Gauche);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalMandibulaire", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Man);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalMaxilaire", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_Max);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoIntrabuccalSurplomb", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Int_SurPlomb);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoMoulageDroit", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Moul_Droit);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoMoulageFace", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Moul_Face);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoMoulageGauche", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Moul_Gauche);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoMoulageMaxilaire", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Moul_Max);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoMoulageMandibulaire", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Moul_Man);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoRadioFace", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Rad_Face);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoRadioProfil", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Rad_Profile);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PhotoRadioPanoramique", BASEDiag_BL.ResumeCliniqueMgmt.resumeCl.Img_Rad_Pano);


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", ResumeCliniqueMgmt.GenerateCompteRenduClinique());


        }

        private void AddCourrierAttributsDevis(Correspondant Praticien)
        {
            string devis = "";

            TemplateActePG surv = TemplateApctePGMgmt.getCodeSecu("SURV");
            TemplateActePG survhn = TemplateApctePGMgmt.getCodeSecu("SURV_HN");

            int propnum = 1;
            foreach (Proposition prop in _CurrentPatient.propositions)
            {

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_NbPhases", prop.traitements.Count.ToString());
                
                int i = 0;

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase1", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase1_TarifParMois", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase1_NbMois", "");


                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase2", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase2_TarifParMois", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase2_NbMois", "");

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Contention_TarifParMois", "");
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Contention_NbMois", "");


                foreach (Traitement t in prop.traitements)
                {
                        double tariftotal = TraitementMgmt.getTotal(t);
                        int nbMois = 0;
                        foreach (Semestre s in t.semestres)
                            nbMois += s.traitementSecu.NBMois;


                        double totalParMois = tariftotal / nbMois;

                    if ((t.Phase == TemplateActePG.EnumPhase.Orthopedique) || (t.Phase == TemplateActePG.EnumPhase.Pédiatrique))
                    {

                        int idxapp = 1;
                        foreach (PoseAppareil pa in prop.poseAppareils)
                        {
                            if (((pa.semestres[0].NumSemestre <= t.semestres[0].NumSemestre) &&
                                (pa.semestres[pa.semestres.Count - 1].NumSemestre >= t.semestres[t.semestres.Count - 1].NumSemestre)) ||
                                ((pa.semestres[0].NumSemestre >= t.semestres[0].NumSemestre) &&
                                (pa.semestres[0].NumSemestre < t.semestres[t.semestres.Count - 1].NumSemestre)) ||
                                ((pa.semestres[pa.semestres.Count - 1].NumSemestre >= t.semestres[0].NumSemestre) &&
                                (pa.semestres[pa.semestres.Count - 1].NumSemestre < t.semestres[t.semestres.Count - 1].NumSemestre))
                                )
                            {
                                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase1_Appareil" + idxapp.ToString(), pa.appareil.Libelle);
                                idxapp++;
                            }
                        }
                        BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase1", t.Phase.ToString());
                        BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase1_TarifParMois", totalParMois.ToString("C2"));
                        BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase1_NbMois", nbMois.ToString());

                    }
                    if (t.Phase == TemplateActePG.EnumPhase.Orthodontique)
                    {
                        int idxapp = 1;
                        foreach (PoseAppareil pa in prop.poseAppareils)
                        {
                            if (((pa.semestres[0].NumSemestre <= t.semestres[0].NumSemestre) &&
                                (pa.semestres[pa.semestres.Count - 1].NumSemestre >= t.semestres[t.semestres.Count - 1].NumSemestre))||
                                ((pa.semestres[0].NumSemestre >= t.semestres[0].NumSemestre) &&
                                (pa.semestres[0].NumSemestre < t.semestres[t.semestres.Count - 1].NumSemestre)) ||
                                ((pa.semestres[pa.semestres.Count - 1].NumSemestre >= t.semestres[0].NumSemestre) &&
                                (pa.semestres[pa.semestres.Count - 1].NumSemestre < t.semestres[t.semestres.Count - 1].NumSemestre))
                                )
                            {
                                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase2_Appareil" + idxapp.ToString(), pa.appareil.Libelle);
                                idxapp++;
                            }
                        }

                        BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase2", t.Phase.ToString());
                        BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase2_TarifParMois", totalParMois.ToString("C2"));
                        BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Phase2_NbMois", nbMois.ToString());

                    }
                    i++;
                }


                double tariftotalContention = 0;
                int nbMoisContention = 0;

                foreach (Traitement t in prop.traitements)
                {

                    if (t.Phase == TemplateActePG.EnumPhase.Contention)
                    {
                        tariftotalContention += TraitementMgmt.getTotal(t);
                        foreach (Semestre s in t.semestres)
                            nbMoisContention += s.traitementSecu.NBMois;

                       
                    }
                }
                if (tariftotalContention > 0)
                {
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Contention_TarifParMois", (tariftotalContention / nbMoisContention).ToString("C2"));
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Contention_NbMois", nbMoisContention.ToString());
                }

                propnum++;







                if (devis != "") devis += "\n\n";
                devis += prop.libelle + " : " + PropositionMgmt.GetTotal(prop).ToString("C2");

                foreach (Traitement t in prop.traitements)
                {
                    if ((t.Phase == TemplateActePG.EnumPhase.Orthopedique) || (t.Phase == TemplateActePG.EnumPhase.Orthodontique))
                    {
                        double totalsansremise = TraitementMgmt.getTotalSansRemise(t);
                        double totalremise = TraitementMgmt.getTotal(t);
                        double partSecu = TraitementMgmt.GetPartSecu(t);

                        devis += "\n\t" + t.Libelle;
                        devis += " sur " + t.semestres.Count.ToString() + " semestre(s)";
                        devis += "\n\t\tTarif : " + (totalsansremise).ToString("C2");
                        if (totalsansremise != totalremise) devis += "\n\t\tTarif remisé: " + (totalremise).ToString("C2");
                        if (partSecu > 0) devis += "\n\t\tRemboursement Secu: " + partSecu.ToString("C2");
                    }
                    if (t.Phase == TemplateActePG.EnumPhase.Contention)
                    {
                        double totalsansremise = TraitementMgmt.getTotalSansRemise(t);
                        double totalremise = TraitementMgmt.getTotal(t);
                        double partSecu = TraitementMgmt.GetPartSecu(t);

                        int nbmois = 0;
                        foreach(Semestre s in t.semestres)
                            nbmois += s.traitementSecu.NBMois;

                        devis += "\n\t" + t.Libelle;
                        devis += " pendant " + nbmois.ToString() + " mois";
                        devis += "\n\t\tTarif : " + (totalsansremise).ToString("C2");
                        if (totalsansremise != totalremise) devis += "\n\t\tTarif remisé: " + (totalremise).ToString("C2");
                        if (partSecu > 0) devis += "\n\t\tRemboursement Secu: " + partSecu.ToString("C2");
                    }

                }
            }

            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Devis", devis);

            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et "+m.ToString()+" mois");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Sexe);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _CurrentPatient.Adresse1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _CurrentPatient.Adresse2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _CurrentPatient.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePatient", _CurrentPatient.Ville);
            if (_CurrentPatient.Tutoiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _CurrentPatient.DateNaissance.Date.ToString());
			BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumSecu", _CurrentPatient.NumSecu.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_Praticien", Praticien.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.Mail);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.TelFixe);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.TelProfessionnel);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelPortablePraticien", Praticien.TelPortable);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.Fax);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.Adresse1Office);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.Adresse2Office);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.CodePostalOffice);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.VilleOffice);
            if (Praticien.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", ResumeCliniqueMgmt.GenerateCompteRenduClinique());


        }

        private void AddCourrierAttributsConsentement(Correspondant Praticien)
        {

            List<string> lstRisques = new List<string>();
            foreach (Proposition p in _CurrentPatient.propositions)
            {
                foreach (string r in PropositionMgmt.GetRisques(p))
                {
                    if (!lstRisques.Contains(r))
                        lstRisques.Add(r);
                }
            }

            string risques = lstRisques.Count == 0 ? "" : lstRisques.Aggregate((i, j) => i + "\n" + j);

            string objectifs = "";

            foreach (CommonObjectif co in CurrentPatient.SelectedObjectifs)
            {
                if (objectifs != "") objectifs += "\n";
                objectifs += co.ToString();
 
            }



            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("objectifs", objectifs);



            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Risques", risques);

            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et "+m.ToString()+" mois");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Sexe);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _CurrentPatient.Adresse1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _CurrentPatient.Adresse2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _CurrentPatient.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePatient", _CurrentPatient.Ville);
            if (_CurrentPatient.Tutoiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _CurrentPatient.DateNaissance.Date.ToString());
			BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumSecu", _CurrentPatient.NumSecu.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_Praticien", Praticien.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.Mail);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.TelFixe);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.TelProfessionnel);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelPortablePraticien", Praticien.TelPortable);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.Fax);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.Adresse1Office);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.Adresse2Office);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.CodePostalOffice);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.VilleOffice);
            if (Praticien.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diagnostique", ResumeCliniqueMgmt.GenerateCompteRenduClinique());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmSurveillance frm = new FrmSurveillance(currentproposition);
            frm.ShowDialog();

            propositionCtrl1.BuildNInvalidate();
        }

        private void btnAddApp_Click(object sender, EventArgs e)
        {
            FrmAddPoseAppareillage frm = new FrmAddPoseAppareillage(CurrentPatient,currentproposition);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                currentproposition.poseAppareils.Add(frm.poseappareillage);
                propositionCtrl1.BuildNInvalidate();
            }
        }

        private void trToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ajouterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddPoseAppareillage frm = new FrmAddPoseAppareillage(CurrentPatient, currentproposition);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                currentproposition.poseAppareils.Add(frm.poseappareillage);
                propositionCtrl1.BuildNInvalidate();
            }
        }

        private void supprimerToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            supprimerToolStripMenuItem.DropDownItems.Clear();
            foreach (Semestre s in propositionCtrl1.SelectedSemestre.Semestres)
            {
                foreach (PoseAppareil pa in currentproposition.poseAppareils)
                {
                    if (pa.semestres.Contains(s))
                    {
                        ToolStripItem itm = new ToolStripMenuItem();
                        itm.Text = pa.appareil.Libelle;
                        itm.Tag = pa;
                        itm.Click += new EventHandler(SuppAppareilFromMenu_Click);
                        supprimerToolStripMenuItem.DropDownItems.Add(itm);
                    }
                }
            }

            

            
        }

        void SuppAppareilFromMenu_Click(object sender, EventArgs e)
        {
            currentproposition.poseAppareils.Remove(((PoseAppareil)((ToolStripItem)sender).Tag));
            propositionCtrl1.BuildNInvalidate();
        }

        private void surveillanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSurveillance frm = new FrmSurveillance(currentproposition);
            frm.ShowDialog();

            propositionCtrl1.BuildNInvalidate();
        }

        private void modifierLeTarifToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FrmTarifModification frm = new FrmTarifModification();
            frm.TarifSecu = propositionCtrl1.SelectedSemestre.Semestres[0].traitementSecu;
            frm.TarifApplique = propositionCtrl1.SelectedSemestre.Semestres[0].Montant_Honoraire;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                foreach (Semestre s in propositionCtrl1.SelectedSemestre.Semestres)
                {
                    s.traitementSecu = frm.TarifSecu;
                    s.Montant_Honoraire = frm.TarifApplique;

                    s.traitementSecu = frm.TarifSecu;
                }
                propositionCtrl1.BuildNInvalidate();
            }
        }

        private void modifierLeTarifDuSemestreToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (propositionCtrl1.SelectedSemestre.Semestres.Count==0)return;

            FrmTarifModification frm = new FrmTarifModification();
            frm.TarifSecu = propositionCtrl1.SelectedSemestre.Semestres[0].traitementSecu;
            frm.TarifApplique = propositionCtrl1.SelectedSemestre.Semestres[0].Montant_Honoraire;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                int numsem = propositionCtrl1.SelectedSemestre.Semestres[0].NumSemestre;

                foreach (Traitement t in currentproposition.traitements)
                {
                    int smin = TraitementMgmt.GetSemestreMin(t);
                    int smax = TraitementMgmt.GetSemestreMax(t);
                    if ((smin <= numsem) && (numsem <= smax))
                    {
                        foreach (Semestre s in t.semestres)
                        {
                            s.traitementSecu = frm.TarifSecu;
                        }
                    }
                   
                }
                
                propositionCtrl1.BuildNInvalidate();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmModelesParametre frm = new FrmModelesParametre();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                ModeleDePropositions mdl = new ModeleDePropositions();
                mdl.Nom = frm.txtbxModeleName.Text;

                foreach (Proposition p in CurrentPatient.propositions)
                    mdl.propositions.Add(p);

                PropositionMgmt.AddModele(mdl);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            FrmModelesProposition frm = new FrmModelesProposition();
            if (frm.ShowDialog() == DialogResult.OK)
            {

                ModeleDePropositions mdl = PropositionMgmt.getModele(frm.value.Id);

                foreach (Proposition p in mdl.propositions)
                {
                    p.patient = CurrentPatient;
                    CurrentPatient.propositions.Add(p);
                    propositionCtrl1.AddProposition(p);
                }
                propositionCtrl1.BuildNInvalidate();
            }
        }

        private void lvPPT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start(((string)lvPPT.SelectedItems[0].Tag));
        }

       

        Point DownAt;   
        private void lstBxAppareillages_MouseDown(object sender, MouseEventArgs e)
        {
            DownAt = new Point(e.X, e.Y);
        }

        private void lstBxAppareillages_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                if ((Math.Abs(DownAt.X - e.X) > 5) || (Math.Abs(DownAt.Y - e.Y) > 5))
                {

                    Appareil cafo = (Appareil)lstBxAppareillages.SelectedItem;
                                    

                    DataObject dobj = new DataObject();
                    dobj.SetData("CommonAppareil", cafo);
                    lstBxAppareillages.DoDragDrop(dobj, DragDropEffects.Move);
                }
        }

        private void propositionCtrl1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("CommonAppareil"))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void propositionCtrl1_DragDrop(object sender, DragEventArgs e)
        {
            Point p = propositionCtrl1.PointToClient(new Point(e.X, e.Y));

            propositionCtrl1.AffectSelection(p);
            if (propositionCtrl1.SelectedProposition == null) return;
            if (propositionCtrl1.SelectedSemestre == null) return;

            if (e.Data.GetDataPresent("CommonAppareil"))
            {

                FrmNbSemPoseApp frm = new FrmNbSemPoseApp();
                if (frm.ShowDialog() == DialogResult.OK)
                {

                    Appareil cafo = (Appareil)e.Data.GetData("CommonAppareil");

                    PoseAppareil pa = new PoseAppareil();
                    pa.appareil = cafo;
                    pa.Parent = propositionCtrl1.SelectedProposition.proposition;

                    bool canadd = false;
                    int i = 0;
                    foreach (BASEDiag.Ctrls.PropositionCtrlV2.SemestreCell sc in propositionCtrl1.SelectedProposition.Semestres)
                    {
                        if ((canadd) && (i < frm.value))
                        {
                            pa.semestres.Add(sc.Semestres[0]);
                        }

                        if (sc == propositionCtrl1.SelectedSemestre)
                        {
                            canadd = true;
                            i = 0;
                            pa.semestres.Add(sc.Semestres[0]);
                        }
                        if (canadd) i++;
                        
                    }

                    


                    propositionCtrl1.SelectedProposition.proposition.poseAppareils.Add(pa);
                    propositionCtrl1.BuildNInvalidate();
                }

            }
        }

        private void propositionCtrl1_DragOver(object sender, DragEventArgs e)
        {
           
        }

      

        
    }
}
