using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;
using BASEDiag_BL;
using System.Text.RegularExpressions;

namespace BASEDiag
{
    public partial class FrmBaseLaboPrevision : Form
    {

        private Appareil _CurrentAppareil;
        public Appareil CurrentAppareil
        {
            
            set
            {
                if (value.Code == "RCC") { ShowPanel(pnlRCC); CurrentDemande.regcc = true; }
                if (value.Code == "ASI")  { ShowPanel(pnlASI); CurrentDemande.ASI = true; }
                if (value.Code == "QH")  { ShowPanel(pnlQH); CurrentDemande.quadhelix = true; }
                if (value.Code == "DISJ") { ShowPanel(pnlDisjoncteur); CurrentDemande.disjoncteur = true; }
                _CurrentAppareil = value;
            }
        }

        private Patient _SelectedPatient;
        public Patient SelectedPatient
        {
            get
            {
                return _SelectedPatient;
            }
            set
            {
                _SelectedPatient = value;
            }
        }

        private BASELaboPrevision _CurrentDemande = new BASELaboPrevision();
        public BASELaboPrevision CurrentDemande
        {
            get
            {
                return _CurrentDemande;
            }
            set
            {
                _CurrentDemande = value;
            }

        }
        
        public FrmBaseLaboPrevision()
        {
            InitializeComponent();
        }

        public FrmBaseLaboPrevision(BASELaboPrevision prvision)
        {
            CurrentDemande = prvision;
            InitializeComponent();
        }

        private void ShowPanel(Panel pnl)
        {
            foreach (Control ctrl in pnlContainer.Controls)
            {
                ctrl.Visible = false;
            }
            pnl.Show();
        }

        private void chkRegCCPRI_CheckedChanged(object sender, EventArgs e)
        {
            pnlPRI.Visible = chkRegCCPRI.Checked;
            
        }


        private void InitDisplay()
        {

            if (CurrentDemande == null) return;

            if (CurrentDemande.regcc)
            {
                chkRegCCVerinMed.Checked = CurrentDemande.regccverinmed;
                chkRegCCPlanMolaire.Checked = CurrentDemande.regccplanmolaire;
                chkRegCCPRI.Checked = CurrentDemande.regccpri;
                cbxPRIOptions.Text = CurrentDemande.regccprioptions;
                chkRegCCFriels.Checked = CurrentDemande.regccfriels;
                chkRegCCCantilevers.Checked = CurrentDemande.regcccantilevers;
                chkRegCCVerinRecul.Checked = CurrentDemande.regccverinrecul;
                chkRegCCGAP.Checked = CurrentDemande.regccGAP;
                txtbxRegCCPRIHaut.Text = CurrentDemande.regccprihauteur.ToString();
                txtbxRegCCPRILong.Text = CurrentDemande.regccprilongueur.ToString();
                txtBxDentsFriels.Text = CurrentDemande.regccfrielsDent;
                txtbxCantilevers.Text = CurrentDemande.regccCantileversDent;

            }

            if (CurrentDemande.quadhelix)
            {
                chkQuadHelixArc.Checked = CurrentDemande.quadhelixarctriple;
                chkQuadHelixPRI.Checked = CurrentDemande.quadhelixpri;

                txtbxQuadHelixPRILong.Text = CurrentDemande.quadhelixprilongueur.ToString();
                txtbxQuadHelixPRIHaut.Text = CurrentDemande.quadhelixprihauteur.ToString();

            }

            

            if (CurrentDemande.disjoncteur)
            {
                ChkDisjoncteurSurBague.Checked = CurrentDemande.disjoncteursurbague;
                ChkDisjoncteurSurGout.Checked = CurrentDemande.disjoncteursurbagueetgout;
                ChkDisjoncteurArc.Checked = CurrentDemande.disjoncteurarctriple;
            }

           

           

            if (CurrentDemande.goutieresasbh)
            {
                chkGoutieresASBHMain.Checked = CurrentDemande.goutieresasbhmain;
                chkGoutieresASBHTotal.Checked = CurrentDemande.goutieresasbhtotal;
                chkGoutieresASBHDeA.Checked = CurrentDemande.goutieresasbhdea;
                txtbxGoutieresASBHDe.Text = CurrentDemande.goutieresasbhde.ToString();
                txtbxGoutieresASBHA.Text = CurrentDemande.goutieresasbha.ToString();

                chkGoutieresASBHEruption.Checked = CurrentDemande.goutieresasbheruption;
                chkGoutieresASBHEruption3.Checked = CurrentDemande.goutieresasbheruption3;
                chkGoutieresASBHEruption4.Checked = CurrentDemande.goutieresasbheruption4;
                chkGoutieresASBHEruption5.Checked = CurrentDemande.goutieresasbheruption5;

                chkGoutieresASBHBoutonSur.Checked = CurrentDemande.goutieresasbhboutonsur;
                chkGoutieresASBHBoutonSurV.Checked = CurrentDemande.goutieresasbhboutonsurv;
                chkGoutieresASBHBoutonSurL.Checked = CurrentDemande.goutieresasbhboutonsurl;
                txtBxButtonsBASHVL.Text = CurrentDemande.goutieresasbhboutonsurDents;

                chkGoutieresASBHAvecSetup.Checked = CurrentDemande.goutieresasbhavecsetup;
                txtbxASBHSetupDents.Text = CurrentDemande.goutieresasbhavecSetupDents;
                chkGoutieresASBHFacette.Checked = CurrentDemande.goutieresasbhFacette;
                txtbxASBHFacetteDents.Text = CurrentDemande.goutieresasbhFacetteDents;

                chkGoutieresASBHRigide.Checked = CurrentDemande.goutieresasbhrigide;

            }

            if (CurrentDemande.goutieresasbb)
            {
                 chkGoutieresASBBMain.Checked = CurrentDemande.goutieresasbbmain;
                chkGoutieresASBBTotal.Checked = CurrentDemande.goutieresasbbtotal;
                chkGoutieresASBBDeA.Checked = CurrentDemande.goutieresasbbdea;
                txtbxGoutieresASBBDe.Text = CurrentDemande.goutieresasbbde.ToString();
                txtbxGoutieresASBBA.Text = CurrentDemande.goutieresasbba.ToString();

                chkGoutieresASBBEruption.Checked = CurrentDemande.goutieresasbberuption;
                chkGoutieresASBBEruption3.Checked = CurrentDemande.goutieresasbberuption3;
                chkGoutieresASBBEruption4.Checked = CurrentDemande.goutieresasbberuption4;
                chkGoutieresASBBEruption5.Checked = CurrentDemande.goutieresasbberuption5;

                chkGoutieresASBBBoutonSur.Checked = CurrentDemande.goutieresasbbboutonsur;
                chkGoutieresASBBBoutonSurV.Checked = CurrentDemande.goutieresasbbboutonsurv;
                chkGoutieresASBBBoutonSurL.Checked = CurrentDemande.goutieresasbbboutonsurl;
                txtBxButtonsBASBVL.Text = CurrentDemande.goutieresasbbboutonsurDents;

                chkGoutieresASBBAvecSetup.Checked = CurrentDemande.goutieresasbbavecsetup;
                txtbxASBBSetupDents.Text = CurrentDemande.goutieresasbbavecSetupDents;
                chkGoutieresASBBFacette.Checked = CurrentDemande.goutieresasbbFacette;
                txtbxASBBFacetteDents.Text = CurrentDemande.goutieresasbbFacetteDents;

                chkGoutieresASBBRigide.Checked = CurrentDemande.goutieresasbbrigide;

            }

           
            if (CurrentDemande.ASI)
            {
                txtbxASIEruptsur.Text = CurrentDemande.ASItxtbxEruptSur;
                txtbxASISetupSur.Text = CurrentDemande.ASItxtbxSetupSur;

            }

            if (CurrentDemande.Invisalign)
            {
                //RefreshInvisalignList();



            }

            txtbxAutre.Text = CurrentDemande.autre;


        }

        private void BuildDemande()
        {
            Regex regex = new Regex(@"[^0-9]");




                       


            _CurrentDemande.regccverinmed = chkRegCCVerinMed.Checked;
            _CurrentDemande.regccGAP = chkRegCCGAP.Checked;


            _CurrentDemande.regccplanmolaire = chkRegCCPlanMolaire.Checked;
            _CurrentDemande.regccpri = chkRegCCPRI.Checked;
            _CurrentDemande.regccprioptions = cbxPRIOptions.Text;

            try
            {
                _CurrentDemande.regccprilongueur = Convert.ToInt32(txtbxRegCCPRILong.Text);
            }
            catch (System.Exception)
            {
                _CurrentDemande.regccprilongueur = 0;
            }

            try
            {
                _CurrentDemande.regccprihauteur = Convert.ToInt32(txtbxRegCCPRIHaut.Text);
            }
            catch (System.Exception)
            {
                _CurrentDemande.regccprihauteur = 0;
            }

            _CurrentDemande.regccfriels = chkRegCCFriels.Checked;
            _CurrentDemande.regccfrielsDent = regex.Replace(txtBxDentsFriels.Text, ",");
            _CurrentDemande.regcccantilevers = chkRegCCCantilevers.Checked;
            _CurrentDemande.regccCantileversDent = regex.Replace(txtbxCantilevers.Text, ",");
            _CurrentDemande.regccverinrecul = chkRegCCVerinRecul.Checked;
            _CurrentDemande.quadhelixarctriple = chkQuadHelixArc.Checked;
            _CurrentDemande.quadhelixpri = chkQuadHelixPRI.Checked;


            try
            {
                _CurrentDemande.quadhelixprilongueur = Convert.ToInt32(txtbxQuadHelixPRILong.Text);
            }
            catch (System.Exception)
            {
                _CurrentDemande.quadhelixprilongueur = 0;
            }
            try
            {
                _CurrentDemande.quadhelixprihauteur = Convert.ToInt32(txtbxQuadHelixPRIHaut.Text);
            }
            catch (System.Exception)
            {
                _CurrentDemande.quadhelixprihauteur = 0;
            }


           
            _CurrentDemande.disjoncteursurbague = ChkDisjoncteurSurBague.Checked;
            _CurrentDemande.disjoncteurarctriple = ChkDisjoncteurArc.Checked;
            _CurrentDemande.disjoncteursurbagueetgout = ChkDisjoncteurSurGout.Checked;



            _CurrentDemande.goutieresasbhmain = chkGoutieresASBHMain.Checked;
            _CurrentDemande.goutieresasbhtotal = chkGoutieresASBHTotal.Checked;
            _CurrentDemande.goutieresasbhdea = chkGoutieresASBHDeA.Checked;


            try
            {
                _CurrentDemande.goutieresasbhde = Convert.ToInt32(txtbxGoutieresASBHDe.Text);
            }
            catch (System.Exception)
            {
                _CurrentDemande.goutieresasbhde = 0;
            }

            try
            {
                _CurrentDemande.goutieresasbha = Convert.ToInt32(txtbxGoutieresASBHA.Text);
            }
            catch (System.Exception)
            {
                _CurrentDemande.goutieresasbha = 0;
            }





            _CurrentDemande.goutieresasbhboutonsur = chkGoutieresASBHBoutonSur.Checked;
            _CurrentDemande.goutieresasbhboutonsurv = chkGoutieresASBHBoutonSurV.Checked;
            _CurrentDemande.goutieresasbhboutonsurl = chkGoutieresASBHBoutonSurL.Checked;
            _CurrentDemande.goutieresasbhboutonsurDents = regex.Replace(txtBxButtonsBASHVL.Text, ",");
            _CurrentDemande.goutieresasbheruption = chkGoutieresASBHEruption.Checked;
            _CurrentDemande.goutieresasbheruption3 = chkGoutieresASBHEruption3.Checked;
            _CurrentDemande.goutieresasbheruption4 = chkGoutieresASBHEruption4.Checked;
            _CurrentDemande.goutieresasbheruption5 = chkGoutieresASBHEruption5.Checked;


            _CurrentDemande.goutieresasbhavecsetup = chkGoutieresASBHAvecSetup.Checked;
            _CurrentDemande.goutieresasbhavecSetupDents = regex.Replace(txtbxASBHSetupDents.Text, ",");

            _CurrentDemande.goutieresasbhFacette = chkGoutieresASBHFacette.Checked;
            _CurrentDemande.goutieresasbhFacetteDents = regex.Replace(txtbxASBHFacetteDents.Text, ",");


            _CurrentDemande.goutieresasbhrigide = chkGoutieresASBHRigide.Checked;




            _CurrentDemande.goutieresasbbmain = chkGoutieresASBBMain.Checked;
            _CurrentDemande.goutieresasbbtotal = chkGoutieresASBBTotal.Checked;
            _CurrentDemande.goutieresasbbdea = chkGoutieresASBBDeA.Checked;


            try
            {
                _CurrentDemande.goutieresasbbde = Convert.ToInt32(txtbxGoutieresASBBDe.Text);
            }
            catch (System.Exception)
            {
                _CurrentDemande.goutieresasbbde = 0;
            }

            try
            {
                _CurrentDemande.goutieresasbba = Convert.ToInt32(txtbxGoutieresASBBA.Text);
            }
            catch (System.Exception)
            {
                _CurrentDemande.goutieresasbba = 0;
            }





            _CurrentDemande.goutieresasbbboutonsur = chkGoutieresASBBBoutonSur.Checked;
            _CurrentDemande.goutieresasbbboutonsurv = chkGoutieresASBBBoutonSurV.Checked;
            _CurrentDemande.goutieresasbbboutonsurl = chkGoutieresASBBBoutonSurL.Checked;
            _CurrentDemande.goutieresasbbboutonsurDents = regex.Replace(txtBxButtonsBASBVL.Text, ",");
            _CurrentDemande.goutieresasbberuption = chkGoutieresASBBEruption.Checked;
            _CurrentDemande.goutieresasbberuption3 = chkGoutieresASBBEruption3.Checked;
            _CurrentDemande.goutieresasbberuption4 = chkGoutieresASBBEruption4.Checked;
            _CurrentDemande.goutieresasbberuption5 = chkGoutieresASBBEruption5.Checked;


            _CurrentDemande.goutieresasbbavecsetup = chkGoutieresASBBAvecSetup.Checked;
            _CurrentDemande.goutieresasbbavecSetupDents = regex.Replace(txtbxASBBSetupDents.Text, ",");

            _CurrentDemande.goutieresasbbFacette = chkGoutieresASBBFacette.Checked;
            _CurrentDemande.goutieresasbbFacetteDents = regex.Replace(txtbxASBBFacetteDents.Text, ",");


            _CurrentDemande.goutieresasbbrigide = chkGoutieresASBBRigide.Checked;


            _CurrentDemande.ASItxtbxSetupSur = regex.Replace(txtbxASISetupSur.Text, ",");
            _CurrentDemande.ASItxtbxEruptSur = regex.Replace(txtbxASIEruptsur.Text, ",");

            
            _CurrentDemande.autre = txtbxAutre.Text;

          

            _CurrentDemande.patient = SelectedPatient;
           

        }

        private void chkQuadHelixPRI_CheckedChanged(object sender, EventArgs e)
        {
            pnlQuadPRI.Visible = chkQuadHelixPRI.Checked;
        }

        private void chkGoutieresASBHEruption4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            BuildDemande();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void chkRegCCCantilevers_CheckedChanged(object sender, EventArgs e)
        {
            pnlCantilevers.Visible = chkRegCCCantilevers.Checked;
        }

        private void chkRegCCFriels_CheckedChanged(object sender, EventArgs e)
        {
            pnlFriels.Visible = chkRegCCFriels.Checked;
            
        }

        private void chkGoutieresASBHMain_CheckedChanged(object sender, EventArgs e)
        {
            pnlMaintH.Visible = chkGoutieresASBHMain.Checked;
        }

        private void chkGoutieresASBHBoutonSur_CheckedChanged(object sender, EventArgs e)
        {
            pnlButtonHSur.Visible = chkGoutieresASBHBoutonSur.Checked;
        }

        private void chkGoutieresASBHEruption_CheckedChanged(object sender, EventArgs e)
        {
            pnlEruptionH.Visible = chkGoutieresASBHEruption.Checked;
        }

        private void chkGoutieresASBHAvecSetup_CheckedChanged(object sender, EventArgs e)
        {
            txtbxASBHSetupDents.Visible = chkGoutieresASBHAvecSetup.Checked;
        }

        private void chkGoutieresASBHFacette_CheckedChanged(object sender, EventArgs e)
        {
            txtbxASBHFacetteDents.Visible = chkGoutieresASBHFacette.Checked;
        }

        private void FrmBaseLaboPrevision_Load(object sender, EventArgs e)
        {
            if (CurrentDemande != null)
                InitDisplay();
        }
    }
}
