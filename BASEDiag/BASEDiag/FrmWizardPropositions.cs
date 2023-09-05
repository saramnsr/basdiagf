using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using BASEDiag_BO.PlanGestion;
using BASEDiag_BO;
//using BASEDiag_BL.PlanGestion;
using BASEDiag_BL;
using BasCommon_BO;
using BasCommon_BL;

namespace BASEDiag
{
    public partial class FrmWizardPropositions : Form
    {

        List<int> lstDuree;

       


        private int _Duree;
        public int Duree
        {
            get
            {
                return _Duree;
            }
            set
            {
                _Duree = value;
            }
        }

        private ScenarioCommClinique _scenarioSelected;
        public ScenarioCommClinique scenarioSelected
        {
            get
            {
                return _scenarioSelected;
            }
            set
            {
                _scenarioSelected = value;
            }
        }


        



        private int _nbsemEntameHorCabinet = 0;
        public int nbsemEntameHorCabinet
        {
            get
            {
                return _nbsemEntameHorCabinet;
            }
            set
            {
                _nbsemEntameHorCabinet = value;
            }
        }

        

        private BasCommon_BO.Devis.enumtypePropositon _tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Aucun;
        public BasCommon_BO.Devis.enumtypePropositon tpetrmnt
        {
            get
            {
                return _tpetrmnt;
            }
            set
            {
                _tpetrmnt = value;
            }
        }

        private List<ActePGPropose> _ActesMateriel = new List<ActePGPropose>();
        public List<ActePGPropose> ActesMateriel
        {
            get
            {
                return _ActesMateriel;
            }
            set
            {
                _ActesMateriel = value;
            }
        }

        

        private basePatient _CurrentPatient;
        public basePatient CurrentPatient
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

        private List<Proposition> _value = new List<Proposition>();
        public List<Proposition> value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public FrmWizardPropositions(int NombreSemestresEntameHorsCabinet, basePatient pat)
        {
            nbsemEntameHorCabinet = NombreSemestresEntameHorsCabinet;
            CurrentPatient = pat;
            InitializeComponent();
        }
        
        private void ShowPanel( Panel pnl)
        {
            foreach (Panel pnlTohide in pnlContainer.Controls)
                pnlTohide.Hide();

            pnl.Show();
        }
    
        private void FrmWizardPropositions_Load(object sender, EventArgs e)
        {
            ShowPanel(pnlTypeTraitement);
            //ShowPanel(pnlChoixProposition);

        }

        

        private void NewBuild()
        {
            DateTime maxDate = DateTime.MinValue;

            foreach (Proposition p in _CurrentPatient.propositions)
                foreach (Traitement t in p.traitements)
                {
                    foreach (Semestre s in t.semestres)
                    {
                        if (s.DateFin != null)
                            if (maxDate < s.DateFin.Value)
                                maxDate = s.DateFin.Value;
                    }


                }
            



            #region Contentions

            if (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Cont1)
            {
                double TarifCont1 = Convert.ToDouble(txtbxTarif.Text);
                value.Add(PropositionMgmt.BuildContention1(CurrentPatient, maxDate, TarifCont1, 12));
            }

            if (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Cont2)
            {
                double TarifCont2 = Convert.ToDouble(txtbxTarif.Text);
                value.Add(PropositionMgmt.BuildContention2(CurrentPatient, maxDate, TarifCont2, 12));
            }

            #endregion


            #region Sucette

            if (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Sucette)
            {
                double Tarif = Convert.ToDouble(txtbxTarif.Text);
                value.Add(PropositionMgmt.BuildSucette(  CurrentPatient, maxDate, Tarif, 1));
            }

            #endregion

            #region Orthopedie

            if (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Orthopedie)            
            {

                int NbSemVoulu = Duree;
                int nbsemEnt = PropositionMgmt.NbSemestreEntames(_CurrentPatient.propositions);


                if (nbsemEnt + NbSemVoulu + nbsemEntameHorCabinet > 6)
                    MessageBox.Show("Attention Il y a plus de 6 semestres Remboursés\n Au dela du Sixieme semestre, les semestres ne seront pas remboursés", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                double Tarif = Convert.ToDouble(txtbxTarif.Text);
                value.Add(PropositionMgmt.BuildOrthopedie(nbsemEntameHorCabinet, nbsemEnt, CurrentPatient, maxDate, Tarif, Duree));
                value[value.Count - 1].IdScenario = scenarioSelected==null?-1:scenarioSelected.Id;
            }

            #endregion
            
            #region Adultes


            if (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Adulte)
            {

                if (rbAdulteMBC.Checked)
                {
                    double Tarif = Convert.ToDouble(txtbxAdultMBC.Text);
                    value.Add(PropositionMgmt.BuildMBC_Adulte( CurrentPatient, maxDate, Tarif));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                }

                if (rbAdulteMBL.Checked)
                {
                    double Tarif = Convert.ToDouble(txtbxAdultMBL.Text);
                    value.Add(PropositionMgmt.BuildMBL_Adulte(CurrentPatient, maxDate, Tarif));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                }


                if (rbAdulteMBM.Checked)
                {
                    double Tarif = Convert.ToDouble(txtbxAdultMBM.Text);
                    value.Add(PropositionMgmt.BuildMBM_Adulte(CurrentPatient, maxDate, Tarif));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                }


                
                if (rbInvArcade.Checked)
                {

                    double mnt = Convert.ToDouble(txtbxAdultArcade.Text);
                    value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVARCADE, CurrentPatient, maxDate, mnt));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                }
                if (rbLight.Checked)
                {
                    double mnt = Convert.ToDouble(txtbxAdultLight.Text);
                    value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVLIGHT, CurrentPatient, maxDate, mnt));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                }
                if (rbCompl.Checked)
                {
                    double mnt = Convert.ToDouble(txtbxAdultComplSanscorr.Text);
                    value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVCOMPLET, CurrentPatient, maxDate, mnt));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                }
                if (rbComplCorr.Checked)
                {
                    double mnt = Convert.ToDouble(txtbxAdultComplAvecCorr.Text);
                    value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVCOMPLETCORR, CurrentPatient, maxDate, mnt));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                }
               
                if (rbComplDisj.Checked)
                {
                    double mnt = Convert.ToDouble(txtbxAdultComplDisj.Text);
                    value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVCOMPLETDISJ, CurrentPatient, maxDate, mnt));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                }
                if (rbComplChir.Checked)
                {
                    double mnt = Convert.ToDouble(txtbxAdultComplChir.Text);
                    value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVCOMPLETCHIR, CurrentPatient, maxDate, mnt));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                }
                if (rbComplDisjChir.Checked)
                {
                    double mnt = Convert.ToDouble(txtbxAdultComplDisjChir.Text);
                    value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVCOMPLETDISJCHIR, CurrentPatient, maxDate, mnt));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                }


                
            }
            #endregion
            
            #region Orthodontie

            if (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Orthodontie)
            {
                int NbSemVoulu = Duree;
                int nbsemEnt = PropositionMgmt.NbSemestreEntames(CurrentPatient.propositions);


                if (nbsemEnt + NbSemVoulu + nbsemEntameHorCabinet > 6)
                    MessageBox.Show("Attention Il y a plus de 6 semestres Remboursés\n Au dela du Sixieme semestre, les semestres ne seront pas remboursés", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                if (chkbxMBC.Checked)
                {
                    double Tarif = Convert.ToDouble(txtbxTarifMBC.Text);
                    value.Add(PropositionMgmt.BuildMBC(nbsemEntameHorCabinet, nbsemEnt + 1, CurrentPatient, maxDate, Tarif, NbSemVoulu));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                }

                if (chkbxMBL.Checked)
                {
                    double Tarif = Convert.ToDouble(txtbxTarifMBL.Text);
                    value.Add(PropositionMgmt.BuildMBL(nbsemEntameHorCabinet, nbsemEnt + 1, CurrentPatient, maxDate, Tarif, NbSemVoulu));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                }


                if (chkbxMBM.Checked)
                {
                    double Tarif = Convert.ToDouble(txtbxTarifMBM.Text);
                    value.Add(PropositionMgmt.BuildMBM(nbsemEntameHorCabinet, nbsemEnt + 1, CurrentPatient, maxDate, Tarif, NbSemVoulu));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                }


                if (chkbxINV.Checked)
                {
                    double Tarif = Convert.ToDouble(txtbxTarifINV.Text);
                    value.Add(PropositionMgmt.BuildINVTEEN(nbsemEntameHorCabinet, nbsemEnt + 1, CurrentPatient, maxDate, Tarif, NbSemVoulu));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                }

            }
            #endregion

            #region options

            ActePGPropose ActeMateriel;

            //On rajoute une empreinte Invisalign pour tous les traitements invisalign

            if ((rbInvArcade.Checked) ||
                (rbComplCorr.Checked) ||
                (rbCompl.Checked) ||
                (rbComplChir.Checked) ||
                (rbLight.Checked) ||
                (rbComplDisj.Checked) ||
                (rbComplDisjChir.Checked)
                )
            {
                ActeMateriel = new ActePGPropose();
                ActeMateriel.DateExecution = null;
                ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("EMP");
                ActeMateriel.Libelle = ActeMateriel.template.Libelle;
                ActeMateriel.Montant = ActeMateriel.template.Valeur;
                ActeMateriel.Optionnel = false;
                ActeMateriel.Qte = 1;

                ActesMateriel.Add(ActeMateriel);
            }


            if (chkbxEclairssissment.Checked)
            {
                ActeMateriel = new ActePGPropose();
                ActeMateriel.DateExecution = null;
                ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("KITBLANCHI");
                ActeMateriel.Libelle = ActeMateriel.template.Libelle;
                ActeMateriel.Montant = Convert.ToDouble(txtbxEclairssissment.Text);
                ActeMateriel.Qte = (int)txtbxQteEclair.Value;

                ActesMateriel.Add(ActeMateriel);
            }


            if (chkbxElastique.Checked)
            {
                ActeMateriel = new ActePGPropose();
                ActeMateriel.DateExecution = null;
                ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("OPTELASTI");
                ActeMateriel.Libelle = ActeMateriel.template.Libelle;
                ActeMateriel.Montant = Convert.ToDouble(txtbxElastique.Text);
                ActeMateriel.Qte = 1;

                ActesMateriel.Add(ActeMateriel);
            }

            if (chkbxfacette.Checked)
            {
                ActeMateriel = new ActePGPropose();
                ActeMateriel.DateExecution = null;
                ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("OPTFACETTE");
                ActeMateriel.Libelle = ActeMateriel.template.Libelle;
                ActeMateriel.Montant = Convert.ToDouble(txtbxfacette.Text);
                ActeMateriel.Qte = 1;

                ActesMateriel.Add(ActeMateriel);
            }

            if (chkbxEmpreinteOptique.Checked)
            {
                ActeMateriel = new ActePGPropose();
                ActeMateriel.DateExecution = null;
                ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("EMPOPTI");
                ActeMateriel.Libelle = ActeMateriel.template.Libelle;
                ActeMateriel.Montant = Convert.ToDouble(txtbxEmpreinteOptique.Text);
                ActeMateriel.Qte = 1;

                ActesMateriel.Add(ActeMateriel);
            }

            if (chkbx33ACIER.Checked)
            {
                ActeMateriel = new ActePGPropose();
                ActeMateriel.DateExecution = null;
                ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("33ACIER");
                ActeMateriel.Libelle = ActeMateriel.template.Libelle;
                ActeMateriel.Montant = Convert.ToDouble(txtbxMontant33ACIER.Text);
                ActeMateriel.Qte = (int)txtbxQte33ACIER.Value;

                ActesMateriel.Add(ActeMateriel);
            }

            if (chkbx33OR.Checked)
            {
                ActeMateriel = new ActePGPropose();
                ActeMateriel.DateExecution = null;
                ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("33OR");
                ActeMateriel.Libelle = ActeMateriel.template.Libelle;
                ActeMateriel.Montant = Convert.ToDouble(txtbxMontant33OR.Text);
                ActeMateriel.Qte = (int)txtbxQte33OR.Value;

                ActesMateriel.Add(ActeMateriel);
            }

            if (chkbxGBS.Checked)
            {
                ActeMateriel = new ActePGPropose();
                ActeMateriel.DateExecution = null;
                ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("GBS");
                ActeMateriel.Libelle = ActeMateriel.template.Libelle;
                ActeMateriel.Montant = Convert.ToDouble(txtbxMontantGBS.Text);
                ActeMateriel.Qte = (int)txtbxQteGBS.Value;

                ActesMateriel.Add(ActeMateriel);
            }

            if (chkbxGBM.Checked)
            {
                ActeMateriel = new ActePGPropose();
                ActeMateriel.DateExecution = null;
                ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("GBM");
                ActeMateriel.Libelle = ActeMateriel.template.Libelle;
                ActeMateriel.Montant = Convert.ToDouble(txtbxMontantGBM.Text);
                ActeMateriel.Qte = (int)txtbxQteGBM.Value;

                ActesMateriel.Add(ActeMateriel);
            }

            if (chkbxGBE.Checked)
            {
                ActeMateriel = new ActePGPropose();
                ActeMateriel.DateExecution = null;
                ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("GBE");
                ActeMateriel.Libelle = ActeMateriel.template.Libelle;
                ActeMateriel.Montant = Convert.ToDouble(txtbxMontantGBE.Text);
                ActeMateriel.Qte = (int)txtbxQteGBE.Value;

                ActesMateriel.Add(ActeMateriel);
            }

            if (chkbxKitBlan.Checked)
            {
                ActeMateriel = new ActePGPropose();
                ActeMateriel.DateExecution = null;
                ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("KITBLANCHI");
                ActeMateriel.Libelle = ActeMateriel.template.Libelle;
                ActeMateriel.Montant = Convert.ToDouble(txtbxMontantKitBlan.Text);
                ActeMateriel.Qte = (int)txtbxQteKitBlan.Value;

                ActesMateriel.Add(ActeMateriel);
            }

            #endregion

            if ((value.Count == 0) & (ActesMateriel.Count > 0))
            {
                Proposition p = new Proposition();
                p.DateProposition = DateTime.Now;
                p.patient = CurrentPatient;
                p.DateEvenement = DateTime.Now;
                p.DateAcceptation = null;
                p.Etat = Proposition.EtatProposition.NonImprimé;
                p.libelle = "Materiels";
                value.Add(p);
            }

            

            DialogResult = DialogResult.OK;
            Close();
        }
       
        private void btnOk_Click(object sender, EventArgs e)
        {
            
        }

        private void rbMBM_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbOrthopedie_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void pnlTypeTraitement_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Cont2;
            ShowPanel(pnlTarif);
            lblComplementlblTarif.Text = "€ / an";
            txtbxTarif.Text = "250";
            txtbxTarif.Focus(); 
            
        }

        

        

      

        private void BtnTypeMateriel_Click(object sender, EventArgs e)
        {
            ShowPanel(pnlMateriel);
        }

        private void BtnBlanchimnt_Click(object sender, EventArgs e)
        {
        }

        private void btnGBE_Click(object sender, EventArgs e)
        {

           

        }

        private void btnGBM_Click(object sender, EventArgs e)
        {

           
        }

        private void button6_Click(object sender, EventArgs e)
        {

           
        }

        private void btnGBS_Click(object sender, EventArgs e)
        {

            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void btnOrthopedie_Click(object sender, EventArgs e)
        {
            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Orthopedie;
            ShowPanel(pnlTarif);
            lblComplementlblTarif.Text = "€ / Semestre";
            txtbxTarif.Text = "780";
            txtbxTarif.Focus();
            
        }

        private void btnOrthodontie_Click(object sender, EventArgs e)
        {
            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Orthodontie;
            ShowPanel(pnlOrthodontie);
        }

        private void btnOkTarif_Click(object sender, EventArgs e)
        {
            if ((tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Orthopedie) || (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Orthodontie))
                ShowPanel(pnlDureeSem);
            else if (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Adulte)
                NewBuild();
            else if (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Sucette)
                NewBuild();
            else if ((tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Cont1) || (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Cont2))
                NewBuild();
            else return;            
        }

        private void btnAdulte_Click(object sender, EventArgs e)
        {
            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Adulte;
            ShowPanel(pnlTypeAdulte);
        }

        private void btnCont1_Click(object sender, EventArgs e)
        {

            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Cont1;
            ShowPanel(pnlTarif);
            lblComplementlblTarif.Text = "€ / an";
            txtbxTarif.Text = "500";
            txtbxTarif.Focus();

        }

        

        private void btnOrthodMBM_Click(object sender, EventArgs e)
        {
            
        }

        private void btnOrthodMBC_Click(object sender, EventArgs e)
        {
            
        }

        private void btnOrthodMBL_Click(object sender, EventArgs e)
        {
            
        }

        private void btnOrthodINV_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAdulteMBM_Click(object sender, EventArgs e)
        {
           
        }

        private void btnAdulteMBC_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAdulteMBL_Click(object sender, EventArgs e)
        {
            
        }

        private void btnInvArcade_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLight_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCompl_Click(object sender, EventArgs e)
        {
            
        }

        private void btnComplCorr_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnComplAvecCorrElast_Click(object sender, EventArgs e)
        {
            
        }

        private void btnComplDisj_Click(object sender, EventArgs e)
        {
            
        }

        private void btnComplChir_Click(object sender, EventArgs e)
        {
            
        }

        private void btnComplDisjChir_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            NewBuild();
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            ShowPanel(pnlTarif);
            lblComplementlblTarif.Text = "€ / Semestre";
            txtbxTarif.Text = "1200";
            txtbxTarif.Focus();
        }

        private void rbMBM_CheckedChanged_1(object sender, EventArgs e)
        {
            
        }

        private void rbMBC_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void rbMBL_CheckedChanged(object sender, EventArgs e)
        {
            ShowPanel(pnlTarif);
            lblComplementlblTarif.Text = "€ / Semestre";
            txtbxTarif.Text = "1500";
            txtbxTarif.Focus();
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            txtbxAdultComplSanscorr.Visible = rbCompl.Checked;
            txtbxAdultComplSanscorr.Focus(); 
            
           
        }

        private void rbInvArcade_CheckedChanged(object sender, EventArgs e)
        {
            txtbxAdultArcade.Visible = rbInvArcade.Checked;
            txtbxAdultArcade.Focus(); 
            
        }

        private void rbLight_CheckedChanged(object sender, EventArgs e)
        {

            txtbxAdultLight.Visible = rbLight.Checked;
            txtbxAdultLight.Focus();

           
        }

        private void rbComplCorr_CheckedChanged(object sender, EventArgs e)
        {

            txtbxAdultComplAvecCorr.Visible = rbComplCorr.Checked;
            txtbxAdultComplAvecCorr.Focus();
            
        }


        private void rbComplDisj_CheckedChanged(object sender, EventArgs e)
        {

            txtbxAdultComplDisj.Visible = rbComplDisj.Checked;
            txtbxAdultComplDisj.Focus();

            
        }

        private void rbComplChir_CheckedChanged(object sender, EventArgs e)
        {

            txtbxAdultComplChir.Visible = rbComplChir.Checked;
            txtbxAdultComplChir.Focus();

        }

        private void rbComplDisjChir_CheckedChanged(object sender, EventArgs e)
        {

            txtbxAdultComplDisjChir.Visible = rbComplDisjChir.Checked;

            txtbxAdultComplDisjChir.Focus();


           
        }

        private void rbAdulteMBM_CheckedChanged(object sender, EventArgs e)
        {
            txtbxAdultMBM.Visible = rbAdulteMBM.Checked;
            txtbxAdultMBM.Focus();
        }

        private void rbAdulteMBC_CheckedChanged(object sender, EventArgs e)
        {
            txtbxAdultMBC.Visible = rbAdulteMBC.Checked;
            txtbxAdultMBC.Focus(); 
        }

        private void rbAdulteMBL_CheckedChanged(object sender, EventArgs e)
        {
            txtbxAdultMBL.Visible = rbAdulteMBL.Checked;
            txtbxAdultMBL.Focus(); 

            
        }

        private void chkbxMBM_CheckedChanged(object sender, EventArgs e)
        {
            txtbxTarifMBM.Visible = chkbxMBM.Checked;
        }

        private void chkbxMBC_CheckedChanged(object sender, EventArgs e)
        {
            txtbxTarifMBC.Visible = chkbxMBC.Checked;
        }

        private void chkbxMBL_CheckedChanged(object sender, EventArgs e)
        {
            txtbxTarifMBL.Visible = chkbxMBL.Checked;
        }

        private void chkbxINV_CheckedChanged(object sender, EventArgs e)
        {

            txtbxTarifINV.Visible = chkbxINV.Checked;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            ShowPanel(pnlDureeSem);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ShowPanel(pnlOptionsInvisalign);
        }

        private void txtbxAdultComplDisj_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSucette_Click(object sender, EventArgs e)
        {
            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Sucette;
            ShowPanel(pnlTarif);
            lblComplementlblTarif.Text = "€ / Semestre";
            txtbxTarif.Text = "240";
            txtbxTarif.Focus();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            txtbxEclairssissment.Visible = chkbxEclairssissment.Checked;
            txtbxQteEclair.Visible = chkbxEclairssissment.Checked;
            label1.Visible = chkbxEclairssissment.Checked;
        }

        private void chkbxEmpreinteOptique_CheckedChanged(object sender, EventArgs e)
        {
            txtbxEmpreinteOptique.Visible = chkbxEmpreinteOptique.Checked;
        }

        private void chkbxElastique_CheckedChanged(object sender, EventArgs e)
        {
            txtbxElastique.Visible = chkbxElastique.Checked;
        }

        private void chkbxfacette_CheckedChanged(object sender, EventArgs e)
        {
            txtbxfacette.Visible = chkbxfacette.Checked;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            NewBuild();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtbxMontantKitBlan.Visible = chkbxKitBlan.Checked;
            txtbxQteKitBlan.Visible = chkbxKitBlan.Checked;
            label2.Visible = chkbxKitBlan.Checked;
        }

        private void chkbxGBE_CheckedChanged(object sender, EventArgs e)
        {
            txtbxMontantGBE.Visible = chkbxKitBlan.Checked;
            txtbxQteGBE.Visible = chkbxKitBlan.Checked;
            label4.Visible = chkbxKitBlan.Checked;
        }

        private void chkbxGBS_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pnlMateriel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNextMatos_Click(object sender, EventArgs e)
        {
            NewBuild();
        }

      
        
        private void lstbxScenarios_Click(object sender, EventArgs e)
        {
            
        }

        private void lstbxScenarios_OnSelectionChange(object sender, EventArgs e)
        {
           
        }

        private void pnlDureeSem_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlDureeSem.Visible == true)
            {
                lstDuree = new List<int>();
                foreach (ScenarioCommClinique scenar in MgmtScenarioCommClinique.scenarios)
                {

                    if ((int)scenar.typettmnt == (int)tpetrmnt)
                        if (!lstDuree.Contains(scenar.NbSemestres))
                            lstDuree.Add(scenar.NbSemestres);
                }

                lstDuree.Sort();

                foreach (int i in lstDuree)
                    lstbxDuree.Items.Add(i.ToString()+(i==1?" semestre":" semestres"));
                
            }
        }

       


        private void pnlScenarios_VisibleChanged(object sender, EventArgs e)
        {
        
        }

        private void lstbxDuree_OnSelectionChange(object sender, EventArgs e)
        {
            Duree = lstDuree[(lstbxDuree.SelectedIndices[0])];
            NewBuild();
        }

        private void pnlDureeSem_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lstbxScenarios_Load(object sender, EventArgs e)
        {

        }
    }
}
