using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BL;
using BASEDiag_BO;

namespace BASEDiag
{
    public partial class FrmResultWizard : Form
    {


        List<Control> lstAppOrthopedie = new List<Control>();

        Screen[] screenlst;
        int CurrentScreenIdx = 0;

        Dictionary<string,string> DefaultCourrier = null;
        private List<Proposition> _propositions;
        public List<Proposition> propositions
        {
            get
            {
                return _propositions;
            }
            set
            {
                _propositions = value;
            }
        }

        private Patient _patient;
        public Patient patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
            }
        }


        public FrmResultWizard(Patient pat)
        {
            _patient = pat;
            InitializeComponent();

            screenlst = Screen.AllScreens;
        }


        #region InitDisplay


        private void InitDisplaypropositions_Editions()
        {
            RefreshPropositions();

        }



        private void InitDisplaypropositions_Tarifs()
        {

            


            dgvTarifs.Rows.Clear();

            List<TemplateActePG> lst = new List<TemplateActePG>();

            foreach (Proposition pr in currentpropositions)
            {
                foreach (Phase p in pr.phases)
                {
                    if (!lst.Contains(p.traitement))
                    {
                        lst.Add(p.traitement);                                              

                        object[] ob = new object[2] { p.Libelle, (p.traitement.Valeur) };

                        dgvTarifs.Rows.Add(ob);

                        dgvTarifs.Rows[dgvTarifs.Rows.Count - 1].Tag = p.traitement;
                    }
                }
            }
        }


        private void createnewpropositionfrom(Proposition proposition)
        {
            currentpropositions.Add(new Proposition());
            currentpropositions[currentpropositions.Count - 1].libelle = "Proposition n°" + (_patient.propositions.Count + currentpropositions.Count).ToString();


            foreach (Phase ph in proposition.phases)
            {
                Phase p = new Phase();
                p.TypeDePhase = ph.TypeDePhase;
                p.traitement = ph.traitement;
                p.Duree = ph.Duree;
                p.TarifSemestre = p.traitement.Valeur;
                currentpropositions[currentpropositions.Count - 1].phases.Add(p);
            }

        }

        private bool SavePropositionsTrtmnt()
        {
            


            currentpropositions.Clear();

            if (rb1PhasePediatrie.Checked)
            {
                Phase ph = new Phase();
                ph.TypeDePhase = Phase.PhaseType.Pediatrie;
                ph.traitement = TemplateApctePGMgmt.getTemplatesActeGestion(ResumeCliniqueMgmt.PEDIATRIE);
                ph.Duree = 6;
                ph.Libelle = "Pédiatrie";
                ph.TarifSemestre = ph.traitement.Valeur;

                Proposition prop = new Proposition();
                prop.phases.Add(ph);
                currentpropositions.Add(prop);
            }
            else
            {

                #region Orthopedie
                Phase PhaseOrthopedie = null;


                if (rb2Phases.Checked)
                {

                    if ((!rbDisjoncteur.Checked) && (!rbASI.Checked) && (!rbmasquedelaire.Checked) && (!rbRCC.Checked) && (!rbArcLingual.Checked) && (!rbQuadHelix.Checked))
                    {
                        MessageBox.Show("Veuillez choisir un traitement Orthopedique");
                        return false;
                    }

                    PhaseOrthopedie = new Phase();
                    PhaseOrthopedie.traitement = TemplateApctePGMgmt.getTemplatesActeGestion(ResumeCliniqueMgmt.ORTHOPEDIE);
                    PhaseOrthopedie.traitement.appareils.Clear();
                    PhaseOrthopedie.Duree = Convert.ToInt32(pnl2Phase1.Tag);
                    PhaseOrthopedie.TarifSemestre = PhaseOrthopedie.traitement.Valeur;

                    foreach (Control ctrl in lstAppOrthopedie)
                    {
                        if (ctrl == rbDisjoncteur)
                            PhaseOrthopedie.traitement.appareils.Add(AppareilMgmt.getAppareil(ResumeCliniqueMgmt.DISJONCTEUR));

                        if (ctrl == rbASI)
                            PhaseOrthopedie.traitement.appareils.Add(AppareilMgmt.getAppareil(ResumeCliniqueMgmt.ASI));

                        if (ctrl == rbmasquedelaire)
                            PhaseOrthopedie.traitement.appareils.Add(AppareilMgmt.getAppareil(ResumeCliniqueMgmt.MASQUEDELAIRE));

                        if (ctrl == rbRCC)
                            PhaseOrthopedie.traitement.appareils.Add(AppareilMgmt.getAppareil(ResumeCliniqueMgmt.RCC));

                        if (ctrl == rbArcLingual)
                            PhaseOrthopedie.traitement.appareils.Add(AppareilMgmt.getAppareil(ResumeCliniqueMgmt.ARCLINGUAL));

                        if (ctrl == rbQuadHelix)
                            PhaseOrthopedie.traitement.appareils.Add(AppareilMgmt.getAppareil(ResumeCliniqueMgmt.QUADHELIX));

                    }
                                       
                    string appStr = "";

                    foreach (Appareil app in PhaseOrthopedie.traitement.appareils)
                    {
                        if (appStr != "") appStr += ",";
                        appStr += app.Libelle;
                    }

                    if (appStr != "")
                        PhaseOrthopedie.Libelle = "Orthopedie (" + appStr + ")";
                    else
                        PhaseOrthopedie.Libelle = "Orthopedie";

                }
                #endregion

                #region Orthodontie

                if ((!chkbxInvi.Checked) && (!chkbxMBmetal.Checked) && (!chkbxMBceram.Checked) && (!chkbxMBLingual.Checked))
                {
                    MessageBox.Show("Veuillez choisir un traitement d'Orthodontie");
                    return false;
                }

                List<Phase> PhaseOrthodontie = new List<Phase>();

                if (chkbxInvi.Checked)
                {
                    Phase ph = new Phase();
                    ph.TypeDePhase = Phase.PhaseType.Orthodontie;
                    ph.traitement = TemplateApctePGMgmt.getTemplatesActeGestion(ResumeCliniqueMgmt.INVISALIGN);
                    ph.Duree = Convert.ToInt32(pnl2Phases2.Tag);
                    ph.Libelle = "Orthodontie " + ph.traitement.Libelle;
                    ph.TarifSemestre = ph.traitement.Valeur;
                    PhaseOrthodontie.Add(ph);
                }
                if (chkbxMBmetal.Checked)
                {
                    Phase ph = new Phase();
                    ph.TypeDePhase = Phase.PhaseType.Orthodontie;
                    ph.traitement = TemplateApctePGMgmt.getTemplatesActeGestion(ResumeCliniqueMgmt.MULTIBAGUEMETAL);
                    ph.Duree = Convert.ToInt32(pnl2Phases2.Tag);
                    ph.Libelle = "Orthodontie " + ph.traitement.Libelle;
                    ph.TarifSemestre = ph.traitement.Valeur;
                    PhaseOrthodontie.Add(ph);
                }
                if (chkbxMBceram.Checked)
                {
                    Phase ph = new Phase();
                    ph.TypeDePhase = Phase.PhaseType.Orthodontie;
                    ph.traitement = TemplateApctePGMgmt.getTemplatesActeGestion(ResumeCliniqueMgmt.MULTIBAGUECERAM);
                    ph.Duree = Convert.ToInt32(pnl2Phases2.Tag);
                    ph.Libelle = "Orthodontie " + ph.traitement.Libelle;
                    ph.TarifSemestre = ph.traitement.Valeur;
                    PhaseOrthodontie.Add(ph);
                }
                if (chkbxMBLingual.Checked)
                {
                    Phase ph = new Phase();
                    ph.TypeDePhase = Phase.PhaseType.Orthodontie;
                    ph.traitement = TemplateApctePGMgmt.getTemplatesActeGestion(ResumeCliniqueMgmt.MULTIBAGUELINGU);
                    ph.Duree = Convert.ToInt32(pnl2Phases2.Tag);
                    ph.Libelle = "Orthodontie " +ph.traitement.Libelle;
                    ph.TarifSemestre = ph.traitement.Valeur;
                    PhaseOrthodontie.Add(ph);
                }

                #endregion


                foreach (Phase ph in PhaseOrthodontie)
                {
                    Proposition prop = new Proposition();
                    if (PhaseOrthopedie != null) prop.phases.Add(PhaseOrthopedie);
                    prop.phases.Add(ph);
                    currentpropositions.Add(prop);

                }


            }

            return true;
        }

        private void InitDisplaypropositions_Risques()
        {

            PatientMgmt.getRisques(_patient);






            foreach (Proposition pr in currentpropositions)
            {
                foreach (string s in _patient.Risques)
                    if (s.Trim() != "")
                        pr.Risques.Add(s);


                foreach (Phase ph in pr.phases)
                    if (ph.traitement != null)

                        foreach (Appareil app in ph.traitement.appareils)
                            foreach (string s in app.Risques)
                                if (!pr.Risques.Contains(s))
                                    if (s.Trim() != "")
                                        pr.Risques.Add(s);
            }

            tvRisques.Root.Nodes.Clear();
            List<string> rsque = new List<string>();
            foreach (Proposition pr in currentpropositions)
            {
                foreach (string s in pr.Risques)
                {
                    if (!rsque.Contains(s.Trim()))
                    {
                        rsque.Add(s.Trim());
                        TreeNode tn = new TreeNode();
                        tn.Text = s.Trim();
                        tn.Tag = s;
                        tvRisques.Root.Nodes.Add(tn);
                    }
                }
            }

            tvRisques.ForceRefresh();
        }

        private void InitDisplaypropositions_Duree()
        {


            currentpropositions.Add(new Proposition());
            currentpropositions[currentpropositions.Count - 1].libelle = "Proposition n°" + (_patient.propositions.Count + currentpropositions.Count).ToString();
        }


        private bool SavePropositionsDuree()
        {

            if ((!rb2Phases.Checked) && (!rb1PhasePediatrie.Checked) && (!rb1Phase.Checked))
            {
                MessageBox.Show("Veuillez choisir le nombre de phase");
                return false;
            }
            

            if (rb1PhasePediatrie.Checked)
            {
                pnlTrmntOrthodontie.Enabled = false;
                pnlTrmntOrthopedie.Enabled = false;
            }

            if (rb2Phases.Checked)
            {
                if ((pnl2Phase1.Tag==null) || (pnl2Phases2.Tag==null))
                {
                    MessageBox.Show("Veuillez choisir la duree");
                    return false;
                }                

                pnlTrmntOrthodontie.Enabled = true;
                pnlTrmntOrthopedie.Enabled = true;
            }

            if (rb1Phase.Checked)
            {
                if ((pnl2Phases2.Tag == null))
                {
                    MessageBox.Show("Veuillez choisir la duree");
                    return false;
                }
                pnlTrmntOrthodontie.Enabled = true;
                pnlTrmntOrthopedie.Enabled = false;
            }

            return true;
        }

        private void InitDisplaypropositions_Trtmnt()
        {
            
            

        }

        private void InitDisplayAmeliorations()
        {
            txtbxAmeliorations.Text = ResumeCliniqueMgmt.GenerateAmeliorations().Replace("\n", "\r\n");

        }

        private bool SaveAmeliorations()
        {
            _patient.infoscomplementaire.Ameliorations = txtbxAmeliorations.Text;

            
            return true;
        }

        private void InitDisplayInfoComplementaires()
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
            cbxSemEntames.Items.Add("???");

            cbxSemEntames.SelectedIndex = 0;


            List<Utilisateur> currentutilisateurs = UtilisateursMgt.getUtilisateurInFauteuil(IamTheFauteuil, DateTime.Now);



            foreach (Utilisateur u in currentutilisateurs)
            {
                if (cbxPratResp.Items.Contains(u)) cbxPratResp.SelectedItem = u;
                if (cbxAssResp.Items.Contains(u)) cbxAssResp.SelectedItem = u;
            }

            dtpDebuTraitement.Value = DateTime.Now;
        }

        private bool SaveInfoComplementaires()
        {

            _patient.infoscomplementaire.PraticienResponsable = (Utilisateur)cbxPratResp.SelectedItem;
            _patient.infoscomplementaire.AssistanteResponsable = (Utilisateur)cbxAssResp.SelectedItem;

            if (_patient.infoscomplementaire.PraticienResponsable == null)
            {
                MessageBox.Show("Veuillez sélectionner un praticien responsable");
                return false;
            }

            if (_patient.infoscomplementaire.AssistanteResponsable == null)
            {
                MessageBox.Show("Veuillez sélectionner une assistante responsable");
                return false;
            }

            
            if (dtpDebuTraitement.Value.Date <DateTime.Now.Date)
            {
                MessageBox.Show("La date de début de traitement ne peut pas être passée");
                return false;
            }

            if (cbxSemEntames.SelectedIndex == -1)
            {
                _patient.infoscomplementaire.NbSemestresEntame = 0;
            }
            else
            {

                if (cbxSemEntames.SelectedIndex == 7)
                    _patient.infoscomplementaire.NbSemestresEntame = -1;
                else
                    _patient.infoscomplementaire.NbSemestresEntame = cbxSemEntames.SelectedIndex;
            }


            _patient.infoscomplementaire.DateDebutTraitement = dtpDebuTraitement.Value;
            return true;
        }

        private void SaveAll()
        {
            PatientMgmt.setinfocomplementaire(_patient);
            PropositionMgmt.setPropositions(_patient);
            CorrespondantMgmt.setPersonnesAContacter(_patient);
        }

        private void SaveSpecialistes()
        {
            if (btnFindCorrespondant15.Tag is Correspondant)
            {
                LienCorrespondant lc = new LienCorrespondant();
                lc.correspondant = (Correspondant)btnFindCorrespondant15.Tag;
                lc.TypeDeLien = "Ac";
                lc.LienLibelle = "";
                _patient.PersonnesAContacter.Add(lc);
            }
            if (btnFindCorrespondant11.Tag is Correspondant)
            {
                LienCorrespondant lc = new LienCorrespondant();
                lc.correspondant = (Correspondant)btnFindCorrespondant11.Tag;
                lc.TypeDeLien = "Ac";
                lc.LienLibelle = "";
                _patient.PersonnesAContacter.Add(lc);
            } 
            
            if (btnFindCorrespondant12.Tag is Correspondant)
            {
                LienCorrespondant lc = new LienCorrespondant();
                lc.correspondant = (Correspondant)btnFindCorrespondant12.Tag;
                lc.TypeDeLien = "Ac";
                lc.LienLibelle = "";
                _patient.PersonnesAContacter.Add(lc);
            } 
            
            if (btnFindCorrespondant13.Tag is Correspondant)
            {
                LienCorrespondant lc = new LienCorrespondant();
                lc.correspondant = (Correspondant)btnFindCorrespondant13.Tag;
                lc.TypeDeLien = "Ac";
                lc.LienLibelle = "";
                _patient.PersonnesAContacter.Add(lc);
            }
        }


        #endregion

        int Step = 0;
        int StepProposition = 0;

        public List<Proposition> currentpropositions = new List<Proposition>();


        private bool SavePropositionsRisques()
        {
            return true;
        }

        private bool SaveTarifsModified()
        {
            foreach (DataGridViewRow row in dgvTarifs.Rows)
            {
                Double val = 0; ;
                bool convertionOk = true;

                if (row.Cells[1].Value is double)
                    val = (double)row.Cells[1].Value;
                else
                    if (row.Cells[1].Value is string)
                        convertionOk = Double.TryParse((string)row.Cells[1].Value, out val);
                    else
                        convertionOk = false;


                if (!convertionOk)
                {
                    MessageBox.Show("Erreur de convertion");
                    return false;
                }
                else
                {

                    foreach (Proposition pr in currentpropositions)
                    {
                        foreach (Phase ph in pr.phases)
                        {
                            if (ph.traitement.Id == ((TemplateActePG)row.Tag).Id)
                            {
                                ph.TarifSemestre = val;
                            }
                        }
                    }
                    
                }
            }

            return true;
            /*
            for (int i = 0; i < currentpropositions[currentpropositions.Count - 1].phases.Count; i++)
            {
                Double val = 0; ;
                bool convertionOk = true;

                if (dgvTarifs.Rows[i].Cells[1].Value is double)
                    val = (double)dgvTarifs.Rows[i].Cells[1].Value;
                else
                    if (dgvTarifs.Rows[i].Cells[1].Value is string)
                        convertionOk = Double.TryParse((string)dgvTarifs.Rows[i].Cells[1].Value, out val);
                    else
                        convertionOk = false;


                if (!convertionOk)
                {
                    MessageBox.Show("Erreur de convertion");
                    return false;
                }
                else
                    currentpropositions[currentpropositions.Count - 1].phases[i].TarifSemestre = val;
            }

            return true;
             * */
        }


        private void selectTab(TabPage tab)
        {
            WizardCtrl.TabPages.Clear();
            WizardCtrl.TabPages.Add(tab);
            WizardCtrl.SelectedTab = tab;
        }

        private void Next()
        {
            

            if (Step == 0)
            {
                InitDisplayAmeliorations();
                selectTab(tabAmelioration);
                Step++;
                return;
            }
            if (Step == 1)
            {
                if (SaveAmeliorations())
                {
                    InitDisplayInfoComplementaires();
                    selectTab(tabInfocomplementaires);
                    Step++;
                }
                return;
            }
            if (Step == 2)
            {
                if (SaveInfoComplementaires())
                {

                    if (StepProposition == 0)
                    {
                        InitDisplaypropositions_Duree();


                        selectTab(tabpropositions_Durée);
                        StepProposition++;
                        return;
                    }

                    if (StepProposition == 1)
                    {
                        if (SavePropositionsDuree())
                        {
                            InitDisplaypropositions_Trtmnt();

                            selectTab(tabproposition_Trtmnt);

                            StepProposition++;
                        }
                            return;
                        
                    }

                    if (StepProposition == 2)
                    {
                        if (SavePropositionsTrtmnt())
                        {
                            InitDisplaypropositions_Risques();

                            selectTab(tabPropositionRisques);

                            StepProposition++;
                        }
                        return;
                    }
                    if (StepProposition == 3)
                    {
                        if (SavePropositionsRisques())
                        {
                            InitDisplaypropositions_Tarifs();

                            selectTab(tabPropositionTarif);

                            StepProposition++;
                        }
                        return;

                    }
                    if (StepProposition == 4)
                    {

                        if (SaveTarifsModified())
                        {

                            foreach(Proposition pr in currentpropositions)
                                _patient.propositions.Add(pr);

                            StepProposition = 0;
                            if (MessageBox.Show("Souhaitez vous ajouter une propositions ?", "Autre proposition", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                Next();
                                return;
                            }
                            else
                            {
                                Step++;
                                Next();
                                return;
                            }
                        }
                    }
                }
            }

            if (Step == 3)
            {
                InitDisplaypropositions_Editions();

                selectTab(tabEditions);

                Step++;
                return;
            }

            if (Step == 4)
            {

                selectTab(tabSpecialistes);

                Step++;
                return;
            }

            if (Step == 5)
            {
                SaveSpecialistes();

                SaveAll();
                Close();
            }
            
        }

        private void StartFlow()
        {
            WizardCtrl.TabPages.Clear();

            Next();

        }


        public Dictionary<string,string> parseCSV(string path)
        {
            Dictionary<string, string> parsedData = new Dictionary<string, string>();

            try
            {
                using (System.IO.StreamReader readFile = new System.IO.StreamReader(path))
                {
                    string line;
                    string[] row;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        row = line.Split('\t');
                        parsedData.Add(row[0],row[1]);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return parsedData;
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


        private void FrmResultWizard_Load(object sender, EventArgs e)
        {
            CurrentScreenIdx++;
            CurrentScreenIdx = CurrentScreenIdx >= screenlst.Length ? 0 : CurrentScreenIdx;
            

            this.Location = new Point(screenlst[CurrentScreenIdx].Bounds.X + ((screenlst[CurrentScreenIdx].Bounds.Width - this.Width) / 2),
                                      screenlst[CurrentScreenIdx].Bounds.Y + ((screenlst[CurrentScreenIdx].Bounds.Height - this.Height) / 2)); 

            
            if (_patient.infoscomplementaire == null) _patient.infoscomplementaire = new InfoPatientComplementaire();
            if (_patient.propositions == null) _patient.propositions = new List<Proposition>();
            
            StartFlow();

            string courierpardefautfile = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\CourriersSpecialiste.config";
            
            if (System.IO.File.Exists(courierpardefautfile))
                DefaultCourrier = parseCSV(courierpardefautfile);


            LoadPPT("Wizard");

        }

        private void rb1Phase_CheckedChanged(object sender, EventArgs e)
        {
            pnl2Phase1.Enabled = rb2Phases.Checked;
            pnl2Phases2.Enabled = rb2Phases.Checked || rb1Phase.Checked;
        }

        private void rb1Phase_Paint(object sender, PaintEventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton rb = ((RadioButton)sender);

                if (rb.Checked)
                    e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.check, new Point(0, 0));

            }

            if (sender is CheckBox)
            {
                CheckBox rb = ((CheckBox)sender);

                if (rb.Checked)
                    e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.check, new Point(0, 0));

            }
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            pnl2Phase1.Tag = ((Control)sender).Tag;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            pnl2Phases2.Tag = ((Control)sender).Tag;
        }

        private void rbDisjoncteur_CheckedChanged(object sender, EventArgs e)
        {
            List<Control> lstFrom = new List<Control>();

            if (rbDisjoncteur.Checked) lstFrom.Add(rbDisjoncteur);
            if (rbASI.Checked) lstFrom.Add(rbASI);
            if (rbmasquedelaire.Checked) lstFrom.Add(rbmasquedelaire);
            if (rbRCC.Checked) lstFrom.Add(rbRCC);
            if (rbArcLingual.Checked) lstFrom.Add(rbArcLingual);
            if (rbQuadHelix.Checked) lstFrom.Add(rbQuadHelix);

            lstAppOrthopedie = new List<Control>();

            ((Control)sender).Tag = int.MaxValue;

            foreach (Control ctrl in lstFrom)
            {
                if (lstAppOrthopedie.Count == 0)
                    lstAppOrthopedie.Add(ctrl);
                else
                {
                    int i = 0;
                    for (i = 0; i < lstAppOrthopedie.Count; i++)
                        if (((int)lstAppOrthopedie[i].Tag) > ((int)ctrl.Tag))
                        {
                            break;
                        }

                    lstAppOrthopedie.Insert(i, ctrl);

                }

            }

            int t = 1;
            foreach (Control ctrl in lstAppOrthopedie)
            {
                ctrl.Tag = t;
                t++;
            }


            rbDisjoncteur.Invalidate();
            rbASI.Invalidate();
            rbmasquedelaire.Invalidate();
            rbRCC.Invalidate();
            rbArcLingual.Invalidate();
            rbQuadHelix.Invalidate();

        }

        

        private void rbInvi_CheckedChanged(object sender, EventArgs e)
        {
            currentpropositions[currentpropositions.Count - 1].phases[currentpropositions[currentpropositions.Count - 1].phases.Count-1].traitement = TemplateApctePGMgmt.getTemplatesActeGestion(ResumeCliniqueMgmt.INVISALIGN);
            
        }

        private void rbMetal_CheckedChanged(object sender, EventArgs e)
        {
            currentpropositions[currentpropositions.Count - 1].phases[currentpropositions[currentpropositions.Count - 1].phases.Count - 1].traitement = TemplateApctePGMgmt.getTemplatesActeGestion(ResumeCliniqueMgmt.MULTIBAGUEMETAL);
            
        }

        private void rbCeram_CheckedChanged(object sender, EventArgs e)
        {
            currentpropositions[currentpropositions.Count - 1].phases[currentpropositions[currentpropositions.Count - 1].phases.Count - 1].traitement = TemplateApctePGMgmt.getTemplatesActeGestion(ResumeCliniqueMgmt.MULTIBAGUECERAM);
           
        }

        private void rbLingual_CheckedChanged(object sender, EventArgs e)
        {
            currentpropositions[currentpropositions.Count - 1].phases[currentpropositions[currentpropositions.Count - 1].phases.Count - 1].traitement = TemplateApctePGMgmt.getTemplatesActeGestion(ResumeCliniqueMgmt.MULTIBAGUELINGU);
          
        }

        

        private void tvRisques_OnSelected(object sender, EventArgs e)
        {
            string folder = System.Configuration.ConfigurationManager.AppSettings["PPTFolder"] + "\\Risques\\";

            if (!System.IO.Directory.Exists(folder))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(folder);
                }
                catch (System.Exception)
                {
                    MessageBox.Show(folder + " n'existe pas!");
                    return;
                }
            }

            string file = folder + ((TreeNode)sender).Text + ".ppt";

            if (System.IO.File.Exists(file))
                System.Diagnostics.Process.Start("POWERPNT.EXE", "/s \"" + file + "\"");
            else
                System.Diagnostics.Process.Start(folder);
        }


        private void RefreshPropositions()
        {
            lvproposition.Items.Clear();

            foreach (Proposition p in _patient.propositions)
            {

                ListViewItem itm = new ListViewItem();
                itm.Text = p.ToString();
                itm.SubItems.Add(p.phases.Count.ToString());
                itm.Tag = p;

                string duree = "";
                foreach (Phase ph in p.phases)
                {
                    duree += duree != "" ? "+" : "";
                    duree += ph.Duree.ToString();
                }
                itm.SubItems.Add(duree);


                int nbSemRemb = _patient.infoscomplementaire.NbSemestresEntame;
                double tarif = 0;
                double rembsecu = 0;
                double tarifReel = 0;

                TimeSpan age = _patient.Age;





                foreach (Phase ph in p.phases)
                {

                    tarif += ph.traitement.Valeur * ph.Duree;


                    tarifReel += ph.TarifSemestre * ph.Duree;

                }


                int years;
                int months;
                int days;
                DateTime startdate = DateTime.Now;

                foreach (Phase ph in p.phases)
                {

                    for (int i = 0; i < ph.Duree; i++)
                    {
                        _patient.AgeToDate(startdate, out years, out months, out days);
                        rembsecu += (years < 16) && (nbSemRemb < 6) ? (ph.traitement.Coeff * ph.traitement.Code.Valeur) : 0;
                        nbSemRemb++;
                        startdate = startdate.AddMonths(ph.traitement.NBMois).AddDays(ph.traitement.NBJours);
                    }

                }


                itm.SubItems.Add(tarif.ToString());
                itm.SubItems.Add(rembsecu.ToString());
                itm.SubItems.Add(tarifReel.ToString());

                lvproposition.Items.Add(itm);
            }
        }

        private void btnPrintDevis_Click(object sender, EventArgs e)
        {

            GenerateNPrintDEvisConsentement(true);

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

            Correspondant c = CorrespondantMgmt.getCorrespondant(_patient.Id);
            Correspondant praticien = CorrespondantMgmt.getCorrespondant(((Utilisateur)cbxPratResp.SelectedItem).Id);
            GenerateAndPrintDevis(devis, praticien, c, printDirect);


            foreach (Proposition p in _patient.propositions)
                p.Etat = Proposition.EtatProposition.Soumis;


            if (!System.IO.File.Exists(consentement))
            {
                FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
                frm.Text = "Choix du consentement";
                if (frm.ShowDialog() != DialogResult.OK) return; else consentement = frm.FileName;
            }

            c = CorrespondantMgmt.getCorrespondant(_patient.Id);
            praticien = CorrespondantMgmt.getCorrespondant(((Utilisateur)cbxPratResp.SelectedItem).Id);
            GenerateAndPrintConsentement(consentement, praticien, c, printDirect);
        }

        private void btnPrintConsentement_Click(object sender, EventArgs e)
        {
            
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

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _patient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _patient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", _patient.AgeNbYears.ToString() + " ans");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _patient.Sexe);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePatient", _patient.Civilite);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _patient.Adresse1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _patient.Adresse2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _patient.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePatient", _patient.Ville);
            if (_patient.Tutoiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _patient.DateNaissance.Date.ToString());
			BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumSecu", _patient.NumSecu.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProchainRDVPatient", _patient.NextRDV.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _patient.Dossier.ToString());
            //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateSoldePatient", _patient.DateSoldePatient.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SoldePatient", _patient.Solde.ToString("0.0"));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateDernierRDVPatient", _patient.LastRDV.ToString());


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


            if (_patient.Dentiste != null)
            {
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitreDentiste", _patient.Dentiste.Titre);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomDentiste", _patient.Dentiste.Nom);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomDentiste", _patient.Dentiste.Prenom);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailDentiste", _patient.Dentiste.Mail);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionDentiste", _patient.Dentiste.Profession);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixeDentiste", _patient.Dentiste.TelFixe);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProDentiste", _patient.Dentiste.TelProfessionnel);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelPortableDentiste", _patient.Dentiste.TelPortable);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxDentiste", _patient.Dentiste.Fax);

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Dentiste", _patient.Dentiste.Adresse1Office);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Dentiste", _patient.Dentiste.Adresse2Office);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalDentiste", _patient.Dentiste.CodePostalOffice);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VilleDentiste", _patient.Dentiste.VilleOffice);
                if (_patient.Dentiste.GenreFeminin)
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreDentiste", "F");
                else
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenreDentiste", "M");

                if (_patient.Dentiste.TuToiement)
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementDentiste", "TU");
                else
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementDentiste", "VOUS");

            }
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
            foreach (Proposition prop in _patient.propositions)
            {

                double total = 0;
                double totalremise = 0;
                devis += "\n\n" + prop.libelle + " : ";

                if (prop.phases.Count > 1)
                {
                    foreach (Phase p in prop.phases)
                    {
                        devis += "\n\t" + p.TypeDePhase.ToString() + " (" + p.traitement.Libelle + ") : " + p.Duree.ToString();
                        devis += p.Duree > 1 ? " semestres" : " semestre";
                        devis += "    :    " + (p.TarifSemestre * p.Duree).ToString("C2");
                        total += p.traitement.Valeur * p.Duree;
                        totalremise += p.TarifSemestre * p.Duree;

                    }
                    devis += "\n\n\tTotal de " + total.ToString("C2");
                    if (total != totalremise)
                    {
                        devis += "\n\tRemise de " + (1 - (totalremise / total)).ToString("P");
                        devis += "\n\tTotal de " + totalremise.ToString("C2");

                    }
                    devis += "\n\tRemboursé par la sécu : " + prop.PartSecu.ToString("C2");

                }
                else
                {
                    devis += "\n\t" + prop.phases[0].TypeDePhase.ToString() + " (" + prop.phases[0].traitement.Libelle + ") : " + prop.phases[0].Duree.ToString();
                    devis += prop.phases[0].Duree > 1 ? " semestres" : " semestre";
                    devis += "    :    " + (prop.phases[0].TarifSemestre * prop.phases[0].Duree).ToString("C2");
                    total += prop.phases[0].traitement.Valeur * prop.phases[0].Duree;
                    totalremise += prop.phases[0].TarifSemestre * prop.phases[0].Duree;

                    devis += "\n\n\tTotal de " + total.ToString("C2");
                    if (total != totalremise)
                    {
                        devis += "\n\tRemise de " + (1 - (totalremise / total)).ToString("P");
                        devis += "\n\tTotal de " + totalremise.ToString("C2");

                    }
                    devis += "\n\tRemboursé par la sécu : " + prop.PartSecu.ToString("C2");

                }


            }


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Devis", devis);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Ameliorations", txtbxAmeliorations.Text);


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _patient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _patient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", _patient.AgeNbYears.ToString() + " ans");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _patient.Sexe);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePatient", _patient.Civilite);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _patient.Adresse1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _patient.Adresse2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _patient.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePatient", _patient.Ville);
            if (_patient.Tutoiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _patient.DateNaissance.Date.ToString());
			BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumSecu", _patient.NumSecu.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _patient.Dossier.ToString());


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
            foreach (Proposition p in _patient.propositions)
            {
                foreach (string r in p.Risques)
                {
                    if (!lstRisques.Contains(r))
                        lstRisques.Add(r);
                }
            }

            string risques = lstRisques.Count == 0 ? "" : lstRisques.Aggregate((i, j) => i + "\n" + j);



            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Risques", risques);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Ameliorations", txtbxAmeliorations.Text);


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _patient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _patient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", _patient.AgeNbYears.ToString() + " ans");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _patient.Sexe);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePatient", _patient.Civilite);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _patient.Adresse1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _patient.Adresse2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _patient.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePatient", _patient.Ville);
            if (_patient.Tutoiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _patient.DateNaissance.Date.ToString());
			BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumSecu", _patient.NumSecu.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _patient.Dossier.ToString());


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

        private void btnFindCorrespondant1_Click(object sender, EventArgs e)
        {
            FrmFindCorrespondant frm = new FrmFindCorrespondant((string)cbxSpecialite1.SelectedItem);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ((Button)sender).Tag = frm.correspondant;
                ((Button)sender).Text = frm.correspondant.ToString();

                if (btnCourrier2.Tag != null)
                    pnlspe2.Enabled = true;
            }
        }

        private void btnCourrier1_Click(object sender, EventArgs e)
        {
            FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(frm.FileName);
                ((Button)sender).Tag = fi;
                string[] ss = System.IO.Path.GetFileNameWithoutExtension(fi.FullName).Split('.');
                ((Button)sender).Text = ss[ss.Length - 1];

            }
            
        }

        private void btnCourrier2_Click(object sender, EventArgs e)
        {
            FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(frm.FileName);

                ((Button)sender).Tag = fi;
                string[] ss = System.IO.Path.GetFileNameWithoutExtension(fi.FullName).Split('.');
                ((Button)sender).Text = ss[ss.Length-1];

                pnlspe2.Enabled = true;
            }
        }

        private void btnCourrier3_Click(object sender, EventArgs e)
        {
            FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(frm.FileName);
                ((Button)sender).Tag = fi;
                string[] ss = System.IO.Path.GetFileNameWithoutExtension(fi.FullName).Split('.');
                ((Button)sender).Text = ss[ss.Length - 1];

                pnlspe3.Enabled = true;
            }
        }

        private void btnCourrier5_Click(object sender, EventArgs e)
        {
            FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(frm.FileName);
                ((Button)sender).Tag = fi;
                string[] ss = System.IO.Path.GetFileNameWithoutExtension(fi.FullName).Split('.');
                ((Button)sender).Text = ss[ss.Length - 1];

                pnlspe5.Enabled = true;

            }
            
        }

        private void btnPrint1_Click(object sender, EventArgs e)
        {
            if ((btnCourrier1.Tag == null) || (btnFindCorrespondant15.Tag == null) || (cbxPratResp.SelectedItem == null)) return;
            System.IO.FileInfo file = (System.IO.FileInfo)btnCourrier1.Tag;
            Utilisateur Praticien = (Utilisateur)cbxPratResp.SelectedItem;
            Correspondant c = ((Correspondant)btnFindCorrespondant15.Tag);
            GenerateCourrier(file.FullName, Praticien, c, false);
        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            if ((btnCourrier2.Tag == null) || (btnFindCorrespondant11.Tag == null) || (cbxPratResp.SelectedItem == null)) return;
            System.IO.FileInfo file = (System.IO.FileInfo)btnCourrier2.Tag;
            Utilisateur Praticien = (Utilisateur)cbxPratResp.SelectedItem;
            Correspondant c = ((Correspondant)btnFindCorrespondant11.Tag);
            GenerateCourrier(file.FullName, Praticien, c, false);
        }

        private void btnPrint3_Click(object sender, EventArgs e)
        {
            if ((btnCourrier3.Tag == null) || (btnFindCorrespondant12.Tag == null) || (cbxPratResp.SelectedItem == null)) return;
            System.IO.FileInfo file = (System.IO.FileInfo)btnCourrier3.Tag;
            Utilisateur Praticien = (Utilisateur)cbxPratResp.SelectedItem;
            Correspondant c = ((Correspondant)btnFindCorrespondant12.Tag);
            GenerateCourrier(file.FullName, Praticien, c, false);
        }

        private void btnPrint4_Click(object sender, EventArgs e)
        {
            if ((btnCourrier4.Tag == null) || (btnFindCorrespondant13.Tag == null) || (cbxPratResp.SelectedItem == null)) return;
            System.IO.FileInfo file = (System.IO.FileInfo)btnCourrier4.Tag;
            Utilisateur Praticien = (Utilisateur)cbxPratResp.SelectedItem;
            Correspondant c = ((Correspondant)btnFindCorrespondant13.Tag);
            GenerateCourrier(file.FullName, Praticien, c, false);
        }

        private void btnPrint5_Click(object sender, EventArgs e)
        {
            if ((btnCourrier5.Tag == null) || (btnFindCorrespondant14.Tag == null) || (cbxPratResp.SelectedItem == null)) return;
            System.IO.FileInfo file = (System.IO.FileInfo)btnCourrier5.Tag;
            Utilisateur Praticien = (Utilisateur)cbxPratResp.SelectedItem;
            Correspondant c = ((Correspondant)btnFindCorrespondant14.Tag);
            GenerateCourrier(file.FullName, Praticien, c, false);
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            btnPrint1_Click(sender, e);
            btnPrint2_Click(sender, e);
            btnPrint3_Click(sender, e);
            btnPrint4_Click(sender, e);
            btnPrint5_Click(sender, e);
        }

        private void GenerateCourrier(string file, Utilisateur Praticien, Correspondant c, bool DirectPrint)
        {

            Correspondant praticien = CorrespondantMgmt.getCorrespondant(Praticien.Id);

            AddCourrierAttributs(praticien, c);

            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionSpecialistes);

            if (!DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);

        }

        private void cbxSemEntames_Load(object sender, EventArgs e)
        {

        }

        private void cbxSemEntames_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dtpDebuTraitement_ValueChanged(object sender, EventArgs e)
        {
            _patient.infoscomplementaire.DateDebutTraitement = dtpDebuTraitement.Value;
        }

        private void lvproposition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxSpecialite_Load(object sender, EventArgs e)
        {
            cbxSpecialite1.Items.Add("Dentiste");
            cbxSpecialite1.Items.Add("Chirurgien");
            cbxSpecialite1.Items.Add("Orthophoniste");
            cbxSpecialite1.Items.Add("Ostéopathe");
            cbxSpecialite1.Items.Add("Posturologue");
            cbxSpecialite1.Items.Add("ORL");
            cbxSpecialite1.Items.Add("Podologue");
            cbxSpecialite1.Items.Add("Généraliste");
            cbxSpecialite1.Items.Add("kiné");
            cbxSpecialite1.Items.Add("Orthoptiste");

            cbxSpecialite2.Items.Add("Dentiste");
            cbxSpecialite2.Items.Add("Chirurgien");
            cbxSpecialite2.Items.Add("Orthophoniste");
            cbxSpecialite2.Items.Add("Ostéopathe");
            cbxSpecialite2.Items.Add("Posturologue");
            cbxSpecialite2.Items.Add("ORL");
            cbxSpecialite2.Items.Add("Podologue");
            cbxSpecialite2.Items.Add("Généraliste");
            cbxSpecialite2.Items.Add("kiné");
            cbxSpecialite2.Items.Add("Orthoptiste");

            cbxSpecialite3.Items.Add("Dentiste");
            cbxSpecialite3.Items.Add("Chirurgien");
            cbxSpecialite3.Items.Add("Orthophoniste");
            cbxSpecialite3.Items.Add("Ostéopathe");
            cbxSpecialite3.Items.Add("Posturologue");
            cbxSpecialite3.Items.Add("ORL");
            cbxSpecialite3.Items.Add("Podologue");
            cbxSpecialite3.Items.Add("Généraliste");
            cbxSpecialite3.Items.Add("kiné");
            cbxSpecialite3.Items.Add("Orthoptiste");

            cbxSpecialite4.Items.Add("Dentiste");
            cbxSpecialite4.Items.Add("Chirurgien");
            cbxSpecialite4.Items.Add("Orthophoniste");
            cbxSpecialite4.Items.Add("Ostéopathe");
            cbxSpecialite4.Items.Add("Posturologue");
            cbxSpecialite4.Items.Add("ORL");
            cbxSpecialite4.Items.Add("Podologue");
            cbxSpecialite4.Items.Add("Généraliste");
            cbxSpecialite4.Items.Add("kiné");
            cbxSpecialite4.Items.Add("Orthoptiste");

            cbxSpecialite5.Items.Add("Dentiste");
            cbxSpecialite5.Items.Add("Chirurgien");
            cbxSpecialite5.Items.Add("Orthophoniste");
            cbxSpecialite5.Items.Add("Ostéopathe");
            cbxSpecialite5.Items.Add("Posturologue");
            cbxSpecialite5.Items.Add("ORL");
            cbxSpecialite5.Items.Add("Podologue");
            cbxSpecialite5.Items.Add("Généraliste");
            cbxSpecialite5.Items.Add("kiné");
            cbxSpecialite5.Items.Add("Orthoptiste");

        }

        private void panel6s_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2s_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1s_Paint(object sender, PaintEventArgs e)
        {

        }

        private void treeViewIconCbx2_Load(object sender, EventArgs e)
        {

        }

        private void tabSpecialistes_Click(object sender, EventArgs e)
        {

        }

        private void cbxSpecialite1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFindCorrespondant11.Tag = null;
            btnFindCorrespondant11.Text = "";
            if (DefaultCourrier.ContainsKey((string)cbxSpecialite1.SelectedItem))
            {
                System.IO.FileInfo nfo = new System.IO.FileInfo(DefaultCourrier[(string)cbxSpecialite1.SelectedItem]);

                string fn = nfo.FullName.Replace(".bvm","");
                btnCourrier2.Tag = nfo;
                string[] ss = fn.Split('.');
                btnCourrier2.Text = ss[ss.Length - 1];
            }

        }

        private void cbxSpecialite2_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFindCorrespondant12.Tag = null;
            btnFindCorrespondant12.Text = "";

            if (DefaultCourrier.ContainsKey((string)cbxSpecialite2.SelectedItem))
            {
                System.IO.FileInfo nfo = new System.IO.FileInfo(DefaultCourrier[(string)cbxSpecialite1.SelectedItem]);

                string fn = nfo.FullName.Replace(".bvm","");
                btnCourrier3.Tag = nfo;
                string[] ss = fn.Split('.');
                btnCourrier3.Text = ss[ss.Length - 1];
            }
        }

        private void cbxSpecialite3_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFindCorrespondant13.Tag = null;
            btnFindCorrespondant13.Text = "";

            if (DefaultCourrier.ContainsKey((string)cbxSpecialite3.SelectedItem))
            {
                System.IO.FileInfo nfo = new System.IO.FileInfo(DefaultCourrier[(string)cbxSpecialite1.SelectedItem]);

                string fn = nfo.FullName.Replace(".bvm","");
                btnCourrier4.Tag = nfo;
                string[] ss = fn.Split('.');
                btnCourrier4.Text = ss[ss.Length - 1];
            }
        }

        private void cbxSpecialite4_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFindCorrespondant14.Tag = null;
            btnFindCorrespondant14.Text = "";

            if (DefaultCourrier.ContainsKey((string)cbxSpecialite4.SelectedItem))
            {
                System.IO.FileInfo nfo = new System.IO.FileInfo(DefaultCourrier[(string)cbxSpecialite1.SelectedItem]);

                string fn = nfo.FullName.Replace(".bvm","");
                btnCourrier5.Tag = nfo;
                string[] ss = fn.Split('.');
                btnCourrier5.Text = ss[ss.Length - 1];
            }
        }

        private void cbxSpecialite5_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFindCorrespondant15.Tag = null;
            btnFindCorrespondant15.Text = "";

            if (DefaultCourrier.ContainsKey((string)cbxSpecialite5.SelectedItem))
            {
                System.IO.FileInfo nfo = new System.IO.FileInfo(DefaultCourrier[(string)cbxSpecialite1.SelectedItem]);

                string fn = nfo.FullName.Replace(".bvm","");
                btnCourrier1.Tag = nfo;
                string[] ss = fn.Split('.');
                btnCourrier1.Text = ss[ss.Length - 1];
            }
        }

        private void btnFindCorrespondant12_Click(object sender, EventArgs e)
        {
            FrmFindCorrespondant frm = new FrmFindCorrespondant((string)cbxSpecialite2.SelectedItem);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ((Button)sender).Tag = frm.correspondant;
                ((Button)sender).Text = frm.correspondant.ToString();

                if (btnCourrier3.Tag != null)
                    pnlspe3.Enabled = true;
            }
        }

        private void btnFindCorrespondant13_Click(object sender, EventArgs e)
        {
            FrmFindCorrespondant frm = new FrmFindCorrespondant((string)cbxSpecialite3.SelectedItem);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ((Button)sender).Tag = frm.correspondant;
                ((Button)sender).Text = frm.correspondant.ToString();

                if (btnCourrier4.Tag != null)
                    pnlspe4.Enabled = true;
            }
        }

        private void btnFindCorrespondant14_Click(object sender, EventArgs e)
        {
            FrmFindCorrespondant frm = new FrmFindCorrespondant((string)cbxSpecialite4.SelectedItem);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ((Button)sender).Tag = frm.correspondant;
                ((Button)sender).Text = frm.correspondant.ToString();

                if (btnCourrier5.Tag != null)
                    pnlspe5.Enabled = true;
            }
        }

        private void btnFindCorrespondant15_Click(object sender, EventArgs e)
        {
            FrmFindCorrespondant frm = new FrmFindCorrespondant((string)cbxSpecialite5.SelectedItem);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ((Button)sender).Tag = frm.correspondant;
                ((Button)sender).Text = frm.correspondant.ToString();
            }
        }

        private void btnCourrier4_Click(object sender, EventArgs e)
        {
            FrmWizardCourrierForSummary frm = new FrmWizardCourrierForSummary();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(frm.FileName);
                ((Button)sender).Tag = fi;
                string[] ss = System.IO.Path.GetFileNameWithoutExtension(fi.FullName).Split('.');
                ((Button)sender).Text = ss[ss.Length - 1];

                pnlspe4.Enabled = true;
            }
        }

        private void btnViewDevis_Click(object sender, EventArgs e)
        {
            GenerateNPrintDEvisConsentement(false);
        }

        private void tabPropositionTarif_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            CurrentScreenIdx++;
            CurrentScreenIdx = CurrentScreenIdx >= screenlst.Length ? 0 : CurrentScreenIdx;



            this.Location = new Point(screenlst[CurrentScreenIdx].Bounds.X + ((screenlst[CurrentScreenIdx].Bounds.Width - this.Width) / 2),
                                      screenlst[CurrentScreenIdx].Bounds.Y + ((screenlst[CurrentScreenIdx].Bounds.Height - this.Height) / 2)); 
                

            
        }

        private void rbDisjoncteur_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Font ft = new Font("Segoe UI",12,FontStyle.Bold);
            CheckBox rb = ((CheckBox)sender);

            if (rb.Checked)
            {
                if (((Control)sender).Tag is Int32)
                {
                    e.Graphics.FillEllipse(new LinearGradientBrush(new Point(3, 18), new Point(3, 3), Color.FromArgb(145, 183, 131), Color.FromArgb(175, 215, 120)), new Rectangle(3, 3, 18, 18));
                    e.Graphics.DrawString(((Control)sender).Tag.ToString(), ft, Brushes.WhiteSmoke, new PointF(5, 1));
                }
            }
        }

        private void lvPPT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start(((string)lvPPT.SelectedItems[0].Tag));
        }

        private void btnAppareil_Click(object sender, EventArgs e)
        {
            BASEDiag_BL.OLEAccess.BASELabo.NouvelleDemandeInStandBy(patient.Id);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FrmWizardClinique frm = new FrmWizardClinique(patient);
            frm.Show();
        }

     


    }

    public class ImpressionSpecialiste
    {
        private Correspondant _correspondant;
        public Correspondant correspondant
        {
            get
            {
                return _correspondant;
            }
            set
            {
                _correspondant = value;
            }
        }

        private string _CourrierParDefault;
        public string CourrierParDefault
        {
            get
            {
                return _CourrierParDefault;
            }
            set
            {
                _CourrierParDefault = value;
            }
        }

        private string _Specialite;
        public string Specialite
        {
            get
            {
                return _Specialite;
            }
            set
            {
                _Specialite = value;
            }
        }
    }

}
