using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BO;
using BasCommon_BL;

namespace BaseCommonControls.Ctrls
{

    


    public partial class DEPCtrl : UserControl
    {


        private bool _EnableModele = true;
        public bool EnableModele
        {
            get
            {
                return _EnableModele;
            }
            set
            {
                _EnableModele = value;
                pnlVerso.Enabled = value;
                pnlRecto.Enabled = value;
            }
        }

        private bool _EnabledDiags;
        public bool EnabledDiags
        {
            get
            {
                return _EnabledDiags;
            }
            set
            {
                _EnabledDiags = value;

                chkbxSensSagittalBasalMandPro.Enabled = value;
                chkbxSensSagittalBasalMaxPro.Enabled = value;
                chkbxSensSagittalBasalMandretro.Enabled = value;
                chkbxSensSagittalBasalMaxRetro.Enabled = value;

                chkbxSensTransversalBasalMaxEndo.Enabled = value;
                chkbxSensTransversalBasalMandEndo.Enabled = value;
                chkbxSensTransversalBasalMaxExo.Enabled = value;
                chkbxSensTransversalBasalMandExo.Enabled = value;

                chkbxSensVerticalBasalMaxHyper.Enabled = value;
                chkbxSensVerticalBasalMaxHypo.Enabled = value;


                chkbxSensSagittalAnomalieMandPro.Enabled = value;
                chkbxSensSagittalAnomalieMaxPro.Enabled = value;
                chkbxSensSagittalAnomalieMandRetro.Enabled = value;
                chkbxSensSagittalAnomalieMaxretro.Enabled = value;

                chkbxSensTransversalAnomalieMaxEndo.Enabled = value;
                chkbxSensTransversalAnomalieMandEndo.Enabled = value;
                chkbxSensTransversalAnomalieMaxExo.Enabled = value;
                chkbxSensTransversalAnomalieMandExo.Enabled = value;

                chkbxSensVerticalAnomalieInfra.Enabled = value;
                chkbxSensVerticalAnomalieSupra.Enabled = value;


                chkbxClasseMolaireI.Enabled = value;
                chkbxClasseMolaireII.Enabled = value;
                chkbxClasseMolaireIII.Enabled = value;

                chkbxClasseCanineI.Enabled = value;
                chkbxClasseCanineII.Enabled = value;
                chkbxClasseCanineIII.Enabled = value;


                txtbxClasseCanine.Enabled = value;
                txtbxClasseMolaire.Enabled = value;

                chkbxDDD.Enabled = value;
                chkbxDDM.Enabled = value;


                txtbxAgenesie.Enabled = value;
                txtbxDentSurnum.Enabled = value;
                txtbxMalposition.Enabled = value;


                chkbxOcclusionInverDroit.Enabled = value;
                chkbxOcclusionInverGauche.Enabled = value;
                chkbxOcclusionInverAnterieur.Enabled = value;


                cbxFacteurFonctionnel.Enabled = value;


            }

        }


        private bool _IsDiagChanged = false;
        public bool IsDiagChanged
        {
            get
            {
                return _IsDiagChanged;
            }
            set
            {
                _IsDiagChanged = value;
            }
        }

        private bool _Disabled = false;
        public bool Disabled
        {
            get
            {
                return _Disabled;
            }
            set
            {
                pnlRecto.Enabled = !value;
                pnlVerso.Enabled = !value;
                _Disabled = value;
            }
        }

        public enum CotePage
        {
            Recto,
            Verso
        }

        private CotePage _cotepage;
        public CotePage cotepage
        {
            get
            {
                return _cotepage;
            }
            set
            {
                _cotepage = value;
                pnlRecto.Visible = _cotepage == CotePage.Recto;
                pnlVerso.Visible = _cotepage == CotePage.Verso;
            }
        }

        private EntentePrealable _entente;
        public EntentePrealable entente
        {
            get
            {
                return _entente;
            }
            set
            {
                _entente = value;
                InitDisplay();

            }
        }

        public DEPCtrl()
        {

            

            InitializeComponent();
        }

        private void chkbxAccidentNo_CheckedChanged(object sender, EventArgs e)
        {
            chkbxAccidentYes.Checked = !chkbxAccidentNo.Checked;
        }

        private void chkbxAccidentYes_CheckedChanged(object sender, EventArgs e)
        {
            chkbxAccidentNo.Checked = !chkbxAccidentYes.Checked;
        }

      
        private void chkbxPensionYes_CheckedChanged(object sender, EventArgs e)
        {
            chkbxPensionNo.Checked = !chkbxPensionYes.Checked;
        }

        private void chkbxPensionNo_CheckedChanged(object sender, EventArgs e)
        {
            chkbxPensionYes.Checked = !chkbxPensionNo.Checked;
            
        }

        private void chkbxRNO_R_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void ImageVerso_Click(object sender, EventArgs e)
        {

        }

        private void chkbxDevisToPatientYes_CheckedChanged(object sender, EventArgs e)
        {
            chkbxDevisToPatientNo.Checked = !chkbxDevisToPatientYes.Checked;
            
        }

        private void chkbxDevisToPatientNo_CheckedChanged(object sender, EventArgs e)
        {
            chkbxDevisToPatientYes.Checked = !chkbxDevisToPatientNo.Checked;
            
        }

        private void chkbxRNO_HR_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void chkbxAutre_CheckedChanged(object sender, EventArgs e)
        {
            

        }


        private void InitDisplay()
        {

            if (_entente == null) return;

            cbxPraticien.SelectedItem = entente.Praticien;

            chkbxRNO_R.Checked = _entente.ReferenceNationalOpposable == EntentePrealable.RNO.R;
            chkbxRNO_HR.Checked = _entente.ReferenceNationalOpposable == EntentePrealable.RNO.HR;

            dtpDateProposition.Value = _entente.dateProposition == DateTime.MinValue ? DateTime.Now : _entente.dateProposition;

            try
            {
                dtpDebutTraitement.Value = _entente.DateDebutTraitement == DateTime.MinValue ? DateTime.Now : _entente.DateDebutTraitement;
            }
            catch (System.Exception)
            {
                dtpDebutTraitement.Value = DateTime.Now;
            }
            txtbxCotationActe.Text = _entente.cotationDesActes;
            chkbxDevisToPatientYes.Checked = _entente.IsDevisSigned;



            txtbxNomPrenomPatient.Text = _entente.NomPrenom ;
            try { dtpDateNaissance.Value = _entente.patient.DateNaissance; }
            catch (System.Exception) { };
            txtbxImmatassure.Text = _entente.ImmatAssure.Trim();



            chkbxDebutTraitement.Checked = _entente.typetraitement == EntentePrealable.TypeDeTraitement.Debut;

            chkbxSuite.Checked = _entente.typetraitement == EntentePrealable.TypeDeTraitement.Semestre;
            if (_entente.Semestre > 1)
                txtbxNumSemestre.Text = _entente.Semestre.ToString();

            chkbxAutre.Checked = _entente.typetraitement == EntentePrealable.TypeDeTraitement.Autre;
            txtbxAutre.Text = _entente.Autre;

            chkbxSurv.Checked = _entente.typetraitement == EntentePrealable.TypeDeTraitement.Surveillance;
            chkbxContention.Checked = _entente.typetraitement == EntentePrealable.TypeDeTraitement.Contention;

            txtbxNumAnnee.Text = _entente.Contention.ToString();


            chkbxSensSagittalBasalMaxPro.Checked = _entente.SensSagittalBasalMax == EntentePrealable.en_ProRetro.Pro;
            chkbxSensSagittalBasalMaxRetro.Checked = _entente.SensSagittalBasalMax == EntentePrealable.en_ProRetro.Retro;
            chkbxSensSagittalBasalMandPro.Checked = _entente.SensSagittalBasalMand == EntentePrealable.en_ProRetro.Pro;
            chkbxSensSagittalBasalMandretro.Checked = _entente.SensSagittalBasalMand == EntentePrealable.en_ProRetro.Retro;

            chkbxSensTransversalBasalMaxEndo.Checked = _entente.SensTransversalBasalMax == EntentePrealable.en_DiagOsseux.Endognatie;
            chkbxSensTransversalBasalMaxExo.Checked = _entente.SensTransversalBasalMax == EntentePrealable.en_DiagOsseux.Exognatie;
            chkbxSensTransversalBasalMandEndo.Checked = _entente.SensTransversalBasalMand == EntentePrealable.en_DiagOsseux.Endognatie;
            chkbxSensTransversalBasalMandExo.Checked = _entente.SensTransversalBasalMand == EntentePrealable.en_DiagOsseux.Exognatie;

            chkbxSensVerticalBasalMaxHyper.Checked = _entente.SensVerticalBasal == EntentePrealable.en_Divergence.Hyperdivergent;
            chkbxSensVerticalBasalMaxHypo.Checked = _entente.SensVerticalBasal == EntentePrealable.en_Divergence.Hypodivergent;


            chkbxSensSagittalAnomalieMaxPro.Checked = _entente.SensSagittalAlveolaireMax == EntentePrealable.en_ProRetro.Pro;
            chkbxSensSagittalAnomalieMaxretro.Checked = _entente.SensSagittalAlveolaireMax == EntentePrealable.en_ProRetro.Retro;
            chkbxSensSagittalAnomalieMandPro.Checked = _entente.SensSagittalAlveolaireMand == EntentePrealable.en_ProRetro.Pro;
            chkbxSensSagittalAnomalieMandRetro.Checked = _entente.SensSagittalAlveolaireMand == EntentePrealable.en_ProRetro.Retro;

            chkbxSensTransversalAnomalieMaxEndo.Checked = _entente.SensTransversalAlveolaireMax == EntentePrealable.en_DiagAlveolaire.Endoalveolie;
            chkbxSensTransversalAnomalieMaxExo.Checked = _entente.SensTransversalAlveolaireMax == EntentePrealable.en_DiagAlveolaire.Exoalveolie;
            chkbxSensTransversalAnomalieMandEndo.Checked = _entente.SensTransversalAlveolaireMand == EntentePrealable.en_DiagAlveolaire.Endoalveolie;
            chkbxSensTransversalAnomalieMandExo.Checked = _entente.SensTransversalAlveolaireMand == EntentePrealable.en_DiagAlveolaire.Exoalveolie;

            chkbxSensVerticalBasalMaxHyper.Checked = _entente.SensVerticalBasal == EntentePrealable.en_Divergence.Hyperdivergent;
            chkbxSensVerticalBasalMaxHypo.Checked = _entente.SensVerticalBasal == EntentePrealable.en_Divergence.Hypodivergent;

            chkbxSensVerticalAnomalieInfra.Checked = _entente.SensVerticalAlveolaire == EntentePrealable.en_OccFace.Infraclusion;
            chkbxSensVerticalAnomalieSupra.Checked = _entente.SensVerticalAlveolaire == EntentePrealable.en_OccFace.Supraclusion;



            txtbxClasseMolaire.Text = _entente.ClasseDentaireMolaireTxt;
            txtbxClasseCanine.Text = _entente.ClasseDentaireCanineTxt;

            chkbxClasseMolaireI.Checked = _entente.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_I;
            chkbxClasseMolaireII.Checked = _entente.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_II;
            chkbxClasseMolaireIII.Checked = _entente.ClasseDentaireMolaire == EntentePrealable.en_Class.Class_III;

            chkbxClasseCanineI.Checked = _entente.ClasseDentaireCanine == EntentePrealable.en_Class.Class_I;
            chkbxClasseCanineII.Checked = _entente.ClasseDentaireCanine == EntentePrealable.en_Class.Class_II;
            chkbxClasseCanineIII.Checked = _entente.ClasseDentaireCanine == EntentePrealable.en_Class.Class_III;

            chkbxDDD.Checked = _entente.DDD;
            chkbxDDM.Checked = _entente.DDM;
            txtbxAgenesie.Text = _entente.Agenesie;
            txtbxMalposition.Text = _entente.Malposition;
            txtbxDentSurnum.Text = _entente.DentsIncluseSurnum;

            if (_entente.occInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche)
            {
                chkbxOcclusionInverDroit.Checked = true;
                chkbxOcclusionInverGauche.Checked = true;
            }
            else
            {
                chkbxOcclusionInverDroit.Checked = _entente.occInverse == EntentePrealable.en_OccInverse.Droite;
                chkbxOcclusionInverGauche.Checked = _entente.occInverse == EntentePrealable.en_OccInverse.Gauche;

            }

            chkbxOcclusionInverAnterieur.Checked = _entente.occInverse == EntentePrealable.en_OccInverse.Anterieur;


            cbxFacteurFonctionnel.Text = _entente.FacteurFonctionnel;

            txtbxPlanTraitement.Text = _entente.PlanDeTraitement;
            txtbxComment.Text = _entente.Commentaires;

            txtbxImmatassure.Text = _entente.ImmatAssure;
            txtbxImmat.Text = _entente.ImmatAssure;
            if (_entente.DatenaissanceAssure != DateTime.MinValue)
               txtbxdatenaissanceAssure.Text = _entente.DatenaissanceAssure.ToShortDateString();
           txtbxAdresseAssure.Text = _entente.AdresseAssure.Replace("\n", "\r\n");
           txtbxNomPrenomAssure.Text = _entente.NomPrenomAssure;

           txtbxNomPatientSiNonassure.Text = _entente.patient.Nom;
           txtbxPrenomPatientSiNonassure.Text = _entente.patient.Prenom;
           txtbxDAteNaissPatientSiNonAssure.Text = _entente.patient.DateNaissance.ToShortDateString();
           txtbxAdressePatientSiNonAssure.Text = _entente.AdressePatient;
           txtbxProfessionPatient.Text = _entente.Profession;

            chkbxSalarie.Checked = _entente.Salarie;
            chkbxSansEmplois.Checked = _entente.SansEmplois;
            chkbxNonSalarie.Checked = _entente.NonSalarie;
            chkbxPensionne.Checked = _entente.PensionAssure;
            chkBxAutreCas.Checked = _entente.AutreCas;
            txtbxAutreCas.Text = _entente.LibAutreCas;

            txtbxDatecessationAct.Text = _entente.DateDeCessationActivite != DateTime.MinValue ? _entente.DateDeCessationActivite.ToShortDateString() : "";
            txtbxDateAccident.Text = _entente.DateAccident != DateTime.MinValue ? _entente.DateAccident.ToShortDateString() : "";
            txtbxProfessionPatient.Text = _entente.Profession;
            chkbxAccidentYes.Checked = _entente.Accident;
            chkbxPensionAffect.Checked = _entente.Pensionne;
            chkbxPensionYes.Checked = _entente.PensionPatient;


            IsDiagChanged = false;

        }

        public void BuildEntente()
        {
            //_entente.ImmatAssure = txtbxImmatassure.Text;
            //_entente.DatenaissanceAssure = txtbxDateNaissAssuree.Text;
            //_entente.NomPrenomAssure = txtbxNomPrenomAssure.Text;
            //_entente.AdresseAssure = txtbxAdresseAssure.Text.Replace("\n","\r\n");

            _entente.Praticien = (Utilisateur)cbxPraticien.SelectedItem;

            _entente.Salarie = chkbxSalarie.Checked;
            _entente.SansEmplois = chkbxSansEmplois.Checked;
            _entente.NonSalarie = chkbxNonSalarie.Checked;
            _entente.PensionAssure = chkbxPensionne.Checked;
            _entente.AutreCas = chkBxAutreCas.Checked;
            _entente.LibAutreCas = txtbxAutreCas.Text;
            try
            {
                if (txtbxDatecessationAct.Text!="")_entente.DateDeCessationActivite = Convert.ToDateTime(txtbxDatecessationAct.Text);
            }
            catch (System.Exception)
            {
                throw new System.Exception("Date de cessation d'activité invalide");
            }

            _entente.Profession = txtbxProfessionPatient.Text;
            _entente.Accident = chkbxAccidentYes.Checked;
            try
            {
                if (txtbxDateAccident.Text != "") _entente.DateAccident = Convert.ToDateTime(txtbxDateAccident.Text);
            }
            catch (System.Exception)
            {
                throw new System.Exception("Date de l'accident non valide");
            }
            _entente.Pensionne = chkbxPensionne.Checked;

            _entente.PensionPatient = chkbxPensionYes.Checked;







            if (chkbxRNO_R.Checked)
                _entente.ReferenceNationalOpposable = EntentePrealable.RNO.R;
            else
                if (chkbxRNO_HR.Checked)
                    _entente.ReferenceNationalOpposable = EntentePrealable.RNO.HR;
                else
                    _entente.ReferenceNationalOpposable = EntentePrealable.RNO.None;

            _entente.dateProposition = dtpDateProposition.Value;
            _entente.DateDebutTraitement = dtpDebutTraitement.Value;
            _entente.cotationDesActes = txtbxCotationActe.Text;
            _entente.IsDevisSigned = chkbxDevisToPatientYes.Checked;



            //_entente.NomPrenom = txtbxNomPrenomPatient.Text;
            _entente.DateNaissancePatient = dtpDateNaissance.Value;
            _entente.ImmatAssure = txtbxImmatassure.Text;
            
            if (chkbxDebutTraitement.Checked) _entente.typetraitement = EntentePrealable.TypeDeTraitement.Debut;
            if (chkbxSuite.Checked) _entente.typetraitement = EntentePrealable.TypeDeTraitement.Semestre;
            if (chkbxSurv.Checked) _entente.typetraitement = EntentePrealable.TypeDeTraitement.Surveillance;
            if (chkbxContention.Checked) _entente.typetraitement = EntentePrealable.TypeDeTraitement.Contention;
            if (chkbxAutre.Checked) _entente.typetraitement = EntentePrealable.TypeDeTraitement.Autre;
            int res = 0;
            Int32.TryParse(txtbxNumSemestre.Text, out res);
            _entente.Semestre = res;
            res = 0;
            Int32.TryParse(txtbxNumAnnee.Text, out res);
            _entente.Contention = res;
            _entente.Autre = txtbxAutre.Text;



            if (chkbxSensSagittalBasalMaxPro.Checked)
                _entente.SensSagittalBasalMax = EntentePrealable.en_ProRetro.Pro;
            if (chkbxSensSagittalBasalMaxRetro.Checked)
                _entente.SensSagittalBasalMax = EntentePrealable.en_ProRetro.Retro;
            if (chkbxSensSagittalBasalMandPro.Checked)
                _entente.SensSagittalBasalMand = EntentePrealable.en_ProRetro.Pro;
            if (chkbxSensSagittalBasalMandretro.Checked)
                _entente.SensSagittalBasalMand = EntentePrealable.en_ProRetro.Retro;

            if (chkbxSensTransversalBasalMaxEndo.Checked)
                _entente.SensTransversalBasalMax = EntentePrealable.en_DiagOsseux.Endognatie;
            if (chkbxSensTransversalBasalMaxExo.Checked)
                _entente.SensTransversalBasalMax = EntentePrealable.en_DiagOsseux.Exognatie;
            if (chkbxSensTransversalBasalMandEndo.Checked)
                _entente.SensTransversalBasalMand = EntentePrealable.en_DiagOsseux.Endognatie;
            if (chkbxSensTransversalBasalMandExo.Checked)
                _entente.SensTransversalBasalMand = EntentePrealable.en_DiagOsseux.Exognatie;

            if (chkbxSensVerticalBasalMaxHyper.Checked)
                _entente.SensVerticalBasal = EntentePrealable.en_Divergence.Hyperdivergent;
            if (chkbxSensVerticalBasalMaxHypo.Checked)
                _entente.SensVerticalBasal = EntentePrealable.en_Divergence.Hypodivergent;


            if (chkbxSensSagittalAnomalieMaxPro.Checked)
                _entente.SensSagittalAlveolaireMax = EntentePrealable.en_ProRetro.Pro;
            if (chkbxSensSagittalAnomalieMaxretro.Checked)
                _entente.SensSagittalAlveolaireMax = EntentePrealable.en_ProRetro.Retro;
            if (chkbxSensSagittalAnomalieMandPro.Checked)
                _entente.SensSagittalAlveolaireMand = EntentePrealable.en_ProRetro.Pro;
            if (chkbxSensSagittalAnomalieMandRetro.Checked)
                _entente.SensSagittalAlveolaireMand = EntentePrealable.en_ProRetro.Retro;

            if (chkbxSensTransversalAnomalieMaxEndo.Checked)
                _entente.SensTransversalAlveolaireMax = EntentePrealable.en_DiagAlveolaire.Endoalveolie;
            if (chkbxSensTransversalAnomalieMaxExo.Checked)
                _entente.SensTransversalAlveolaireMax = EntentePrealable.en_DiagAlveolaire.Exoalveolie;
            if (chkbxSensTransversalAnomalieMandEndo.Checked)
                _entente.SensTransversalAlveolaireMand = EntentePrealable.en_DiagAlveolaire.Endoalveolie;
            if (chkbxSensTransversalAnomalieMandExo.Checked)
                _entente.SensTransversalAlveolaireMand = EntentePrealable.en_DiagAlveolaire.Exoalveolie;

            if (chkbxSensVerticalBasalMaxHyper.Checked)
                _entente.SensVerticalBasal = EntentePrealable.en_Divergence.Hyperdivergent;
            if (chkbxSensVerticalBasalMaxHypo.Checked)
                _entente.SensVerticalBasal = EntentePrealable.en_Divergence.Hypodivergent;
            
            if (chkbxSensVerticalAnomalieInfra.Checked)
                _entente.SensVerticalAlveolaire = EntentePrealable.en_OccFace.Infraclusion;
            if (chkbxSensVerticalAnomalieSupra.Checked)
                _entente.SensVerticalAlveolaire = EntentePrealable.en_OccFace.Supraclusion;
            
          

            _entente.ClasseDentaireMolaireTxt = txtbxClasseMolaire.Text;
            _entente.ClasseDentaireCanineTxt = txtbxClasseCanine.Text;

            if (chkbxClasseMolaireI.Checked)
                _entente.ClasseDentaireMolaire = EntentePrealable.en_Class.Class_I;
            else if (chkbxClasseMolaireII.Checked)
                _entente.ClasseDentaireMolaire = EntentePrealable.en_Class.Class_II;
            else if (chkbxClasseMolaireIII.Checked)
                _entente.ClasseDentaireMolaire = EntentePrealable.en_Class.Class_III;

            if (chkbxClasseCanineI.Checked)
                _entente.ClasseDentaireCanine = EntentePrealable.en_Class.Class_I;
            else if (chkbxClasseCanineII.Checked)
                _entente.ClasseDentaireCanine = EntentePrealable.en_Class.Class_II;
            else if (chkbxClasseCanineIII.Checked)
                _entente.ClasseDentaireCanine = EntentePrealable.en_Class.Class_III;

            _entente.DDD = chkbxDDD.Checked;
            _entente.DDM = chkbxDDM.Checked;
            _entente.Agenesie = txtbxAgenesie.Text;
            _entente.Malposition = txtbxMalposition.Text;
            _entente.DentsIncluseSurnum = txtbxDentSurnum.Text;


            if (chkbxOcclusionInverDroit.Checked && (chkbxOcclusionInverGauche.Checked))
                _entente.occInverse = EntentePrealable.en_OccInverse.Droite_Et_Gauche;
            else if (chkbxOcclusionInverDroit.Checked)
                _entente.occInverse = EntentePrealable.en_OccInverse.Droite;
            else if (chkbxOcclusionInverGauche.Checked)
                _entente.occInverse = EntentePrealable.en_OccInverse.Gauche;
            if(chkbxOcclusionInverAnterieur.Checked)
                _entente.occInverse = EntentePrealable.en_OccInverse.Anterieur;


            _entente.FacteurFonctionnel = cbxFacteurFonctionnel.Text;

            _entente.PlanDeTraitement = txtbxPlanTraitement.Text;
            _entente.Commentaires = txtbxComment.Text;


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxSensTransversalBasalMaxEndo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxOcclusionInverDroit_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxDebutTraitement_MouseClick(object sender, MouseEventArgs e)
        {
            chkbxDebutTraitement.Checked = false;
            chkbxSuite.Checked = false;
            chkbxSurv.Checked = false;
            chkbxContention.Checked = false;
            chkbxAutre.Checked = false;

            ((CheckBox)sender).Checked = true;
        }

        private void chkbxOcclusionInverAnterieur_Click(object sender, EventArgs e)
        {
            if (sender == chkbxOcclusionInverAnterieur)
            {
                if (chkbxOcclusionInverAnterieur.Checked)
                {
                    chkbxOcclusionInverDroit.Checked = false;
                    chkbxOcclusionInverGauche.Checked = false;
                }
            }
            else
            {
                chkbxOcclusionInverAnterieur.Checked = false;
            }
        }

        private void chkbxOcclusionInverAnterieur_AutoSizeChanged(object sender, EventArgs e)
        {

        }

        private void txtbxPlanTraitement_Click(object sender, EventArgs e)
        {
            
        }

        private void txtbxPlanTraitement_MouseClick(object sender, MouseEventArgs e)
        {
            frmPlanTraitementDEP frm = new frmPlanTraitementDEP(txtbxPlanTraitement.Text);
            if (frm.ShowDialog() == DialogResult.OK)
                txtbxPlanTraitement.Text = frm.PlanDeTraitement;
        }

        private void DEPCtrl_Load(object sender, EventArgs e)
        {
            foreach (Utilisateur usr in UtilisateursMgt.Praticiens)
                    cbxPraticien.Items.Add(usr);
        }

        private void chkbxOcclusionInverAnterieur_CheckedChanged(object sender, EventArgs e)
        {
            IsDiagChanged = true;
        }

        private void txtbxClasseMolaire_TextChanged(object sender, EventArgs e)
        {
            IsDiagChanged = true;
        }

        
    }
}
