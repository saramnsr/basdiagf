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
using BASEDiag.Ctrls;
using System.Configuration;
using BasCommon_BL;
using BasCommon_BO;
using System.IO;
using Microsoft.Win32;

namespace BASEDiag
{
    public partial class FrmAnalyse3 : FormScreen
    {


        private basePatient _CurrentPat;
        public basePatient CurrentPat
        {
            get
            {
                return _CurrentPat;
            }
            set
            {
                _CurrentPat = value;
            }
        }


        public FrmAnalyse3(basePatient pat)
        {
            InitializeComponent();
            CurrentPat = pat;
            
        }

        
        private void InitDisplay()
        {
            MolGClasseI.Checked = ResumeCliniqueMgmt.resumeCl.ClasseMolG == BasCommon_BO.EntentePrealable.en_Class.Class_I;
            MolGClasseII.Checked = ResumeCliniqueMgmt.resumeCl.ClasseMolG == BasCommon_BO.EntentePrealable.en_Class.Class_II;
            MolGClasseIII.Checked = ResumeCliniqueMgmt.resumeCl.ClasseMolG == BasCommon_BO.EntentePrealable.en_Class.Class_III;

            MolDClasseI.Checked = ResumeCliniqueMgmt.resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.Class_I;
            MolDClasseII.Checked = ResumeCliniqueMgmt.resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.Class_II;
            MolDClasseIII.Checked = ResumeCliniqueMgmt.resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.Class_III;

            CanGClasseI.Checked = ResumeCliniqueMgmt.resumeCl.ClasseCanG == BasCommon_BO.EntentePrealable.en_Class.Class_I;
            CanGClasseII.Checked = ResumeCliniqueMgmt.resumeCl.ClasseCanG == BasCommon_BO.EntentePrealable.en_Class.Class_II;
            CanGClasseIII.Checked = ResumeCliniqueMgmt.resumeCl.ClasseCanG == BasCommon_BO.EntentePrealable.en_Class.Class_III;

            CanDClasseI.Checked = ResumeCliniqueMgmt.resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.Class_I;
            CanDClasseII.Checked = ResumeCliniqueMgmt.resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.Class_II;
            CanDClasseIII.Checked = ResumeCliniqueMgmt.resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.Class_III;


            rbSupraclusion.Checked = ResumeCliniqueMgmt.resumeCl.OcclusionFace == BasCommon_BO.EntentePrealable.en_OccFace.Supraclusion;
            rbInfraclusion.Checked = ResumeCliniqueMgmt.resumeCl.OcclusionFace == BasCommon_BO.EntentePrealable.en_OccFace.Infraclusion;
            rbOccNormal.Checked = ResumeCliniqueMgmt.resumeCl.OcclusionFace == BasCommon_BO.EntentePrealable.en_OccFace.Normal;
            

            

            tbOclusion.SelectedIndex = ResumeCliniqueMgmt.resumeCl.OcclusionValue;


            chkBxArtInvDroit.CheckState = (ResumeCliniqueMgmt.resumeCl.OcclusionInverse == BasCommon_BO.EntentePrealable.en_OccInverse.Droite_Et_Gauche) || (ResumeCliniqueMgmt.resumeCl.OcclusionInverse == BasCommon_BO.EntentePrealable.en_OccInverse.Droite) ? CheckState.Checked : CheckState.Unchecked;
            chkBxArtInvGauche.CheckState = (ResumeCliniqueMgmt.resumeCl.OcclusionInverse == BasCommon_BO.EntentePrealable.en_OccInverse.Droite_Et_Gauche) || (ResumeCliniqueMgmt.resumeCl.OcclusionInverse == BasCommon_BO.EntentePrealable.en_OccInverse.Gauche) ? CheckState.Checked : CheckState.Unchecked;

            if (ResumeCliniqueMgmt.resumeCl.SautArticule != BasCommon_BO.EntentePrealable.en_OuiNon.undefined) chkBxArtInvSaut.CheckState = (ResumeCliniqueMgmt.resumeCl.SautArticule == BasCommon_BO.EntentePrealable.en_OuiNon.Oui) ? CheckState.Checked : CheckState.Unchecked;
            if (ResumeCliniqueMgmt.resumeCl.ArticuleInvAnt != BasCommon_BO.EntentePrealable.en_OuiNon.undefined) chkBxArtInvAnt.CheckState = (ResumeCliniqueMgmt.resumeCl.ArticuleInvAnt == BasCommon_BO.EntentePrealable.en_OuiNon.Oui) ? CheckState.Checked : CheckState.Unchecked;
            
           
            rbDiagMaxEndo.Checked = ResumeCliniqueMgmt.resumeCl.DiagMax == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Endoalveolie;
            rbDiagMaxExo.Checked = ResumeCliniqueMgmt.resumeCl.DiagMax == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Exoalveolie;
            rbDiagMandEndo.Checked = ResumeCliniqueMgmt.resumeCl.DiagMand == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Endoalveolie;
            rbDiagMandExo.Checked = ResumeCliniqueMgmt.resumeCl.DiagMand == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Exoalveolie;

            rbSensTrnMandEndo.Checked = ResumeCliniqueMgmt.resumeCl.SensTransvMand == BasCommon_BO.EntentePrealable.en_DiagOsseux.Endognatie;
            rbSensTrnMandExo.Checked = ResumeCliniqueMgmt.resumeCl.SensTransvMand == BasCommon_BO.EntentePrealable.en_DiagOsseux.Exognatie;

            rbSensTrnMaxEndo.Checked = ResumeCliniqueMgmt.resumeCl.SensTransvMax == BasCommon_BO.EntentePrealable.en_DiagOsseux.Endognatie;
            rbSensTrnMaxExo.Checked = ResumeCliniqueMgmt.resumeCl.SensTransvMax == BasCommon_BO.EntentePrealable.en_DiagOsseux.Exognatie;


            chkbxLaterolDeviationDroite.CheckState = (ResumeCliniqueMgmt.resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.Droite) || (ResumeCliniqueMgmt.resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.Droite_Et_Gauche)?CheckState.Checked:CheckState.Unchecked;
            chkbxLaterodeviationGauche.CheckState = (ResumeCliniqueMgmt.resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.Gauche) || (ResumeCliniqueMgmt.resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.Droite_Et_Gauche) ? CheckState.Checked : CheckState.Unchecked;
            if (ResumeCliniqueMgmt.resumeCl.FreinLabial != BasCommon_BO.EntentePrealable.en_OuiNon.undefined) chkbxFreinLabial.CheckState = (ResumeCliniqueMgmt.resumeCl.FreinLabial == BasCommon_BO.EntentePrealable.en_OuiNon.Oui) ? CheckState.Checked : CheckState.Unchecked;
            if (ResumeCliniqueMgmt.resumeCl.FreinLingual != BasCommon_BO.EntentePrealable.en_OuiNon.undefined) chkbxFreinLingual.CheckState = (ResumeCliniqueMgmt.resumeCl.FreinLingual == BasCommon_BO.EntentePrealable.en_OuiNon.Oui) ? CheckState.Checked : CheckState.Unchecked;

            RegenerateDiagObjTraitmnt();
        }

        private void InitPos()
        {
            int x = 0;
            int y = 0;
            TriImg1.Location = new Point(x, y);
            TriImg1.Size = new Size(tabTriImg.Width / 3, tabTriImg.Height-250);

            x += tabTriImg.Width / 3;
            TriImg2.Location = new Point(x, y);
            TriImg2.Size = new Size(tabTriImg.Width / 3, tabTriImg.Height - 250);

            x += tabTriImg.Width / 3;
            TriImg3.Location = new Point(x, y);
            TriImg3.Size = new Size(tabTriImg.Width / 3, tabTriImg.Height - 250);

           // TriImg1.zoomAuto();
            TriImg1.zoomAutoReverse();
            TriImg1.Center();
            TriImg2.zoomAutoReverse();
            TriImg2.Center();
            TriImg3.zoomAutoReverse();
            TriImg3.Center();
        }

        private void LoadPPT(string subFolder)
        {
            try
            {
                string pptfolder = System.Configuration.ConfigurationManager.AppSettings["PPTFolder"] + "\\" + subFolder;
                lvPPT.Items.Clear();
                foreach (System.String s in Directory.GetFiles(pptfolder))
                {
                    FileInfo nfo = new FileInfo(s);

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

        private void FrmAnalyse3_Load(object sender, EventArgs e)
        {
            InitLoad();
            this.Bounds = screenlst[CurrentScreenIdx].WorkingArea;
        }

        public void InitLoad()
        {

            tbOclusion.Items.Add("0 mm");
            tbOclusion.Items.Add("1 mm");
            tbOclusion.Items.Add("2 mm");
            tbOclusion.Items.Add("3 mm");
            tbOclusion.Items.Add("4 mm");
            tbOclusion.Items.Add("5 mm");
            tbOclusion.Items.Add("6 mm");
            tbOclusion.Items.Add("7 mm");
            tbOclusion.Items.Add("8 mm");
            tbOclusion.Items.Add("9 mm");
            tbOclusion.Items.Add("10 mm");

            this.Bounds = Screen.AllScreens[CurrentScreenIdx].WorkingArea;

            /*

            OccDroite.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];
            OccDroite.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Droit);
            OccDroite.zoomAuto();
            OccDroite.Center();

            OccFace.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];
            OccFace.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Face);
            OccFace.zoomAuto();
            OccFace.Center();

            OccGauche.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];
            OccGauche.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Gauche);
            OccGauche.zoomAuto();
            OccGauche.Center();*/

            RegenerateDiagObjTraitmnt();

            barrePatient1.patient = CurrentPat;

            LoadPPT("Analyse3");

            InitDisplay();

            TriImg1.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Intrabuccale"] + "," + ConfigurationManager.AppSettings["Attr_Droite"];
            TriImg2.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Intrabuccale"] + "," + ConfigurationManager.AppSettings["Attr_Face"];
            TriImg3.TextIfNoImage = ConfigurationManager.AppSettings["Attr_Intrabuccale"] + "," + ConfigurationManager.AppSettings["Attr_Gauche"];
            

            TriImg1.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Droit);
            TriImg2.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Face);
            TriImg3.loadRadio(ResumeCliniqueMgmt.resumeCl.Img_Int_Gauche);




            TriImg1.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];
            TriImg1.StartSaisie();
            TriImg1.OnEndSaisie += new EventHandler(TriImg1_OnEndSaisie);
            TriImg1.resumeclinique = ResumeCliniqueMgmt.resumeCl;


            TriImg3.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];
            TriImg3.StartSaisie();
            TriImg3.OnEndSaisie += new EventHandler(TriImg3_OnEndSaisie);
            TriImg3.resumeclinique = ResumeCliniqueMgmt.resumeCl;


            TriImg2.HelpFolder = System.Configuration.ConfigurationManager.AppSettings["HelpFolder"];
            TriImg2.StartSaisie();
            TriImg2.OnEndSaisie += new EventHandler(TriImg2_OnEndSaisie);
            TriImg2.resumeclinique = ResumeCliniqueMgmt.resumeCl;

            this.tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;



            TriImg1.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyseOccD;
            TriImg2.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyseOccF;
            TriImg3.ListOfPoints = ResumeCliniqueMgmt.resumeCl.LstPtAnalyseOccG;

            InitPos();
            ReorganiseButtons();
        }

        private void MolGClasseI_CheckedChanged(object sender, EventArgs e)
        {


            if (MolGClasseI.Checked) ResumeCliniqueMgmt.resumeCl.ClasseMolG = BasCommon_BO.EntentePrealable.en_Class.Class_I;
            if (MolGClasseII.Checked) ResumeCliniqueMgmt.resumeCl.ClasseMolG = BasCommon_BO.EntentePrealable.en_Class.Class_II;
            if (MolGClasseIII.Checked) ResumeCliniqueMgmt.resumeCl.ClasseMolG = BasCommon_BO.EntentePrealable.en_Class.Class_III;

            if (MolDClasseI.Checked) ResumeCliniqueMgmt.resumeCl.ClasseMolD = BasCommon_BO.EntentePrealable.en_Class.Class_I;
            if (MolDClasseII.Checked) ResumeCliniqueMgmt.resumeCl.ClasseMolD = BasCommon_BO.EntentePrealable.en_Class.Class_II;
            if (MolDClasseIII.Checked) ResumeCliniqueMgmt.resumeCl.ClasseMolD = BasCommon_BO.EntentePrealable.en_Class.Class_III;

            if (CanGClasseI.Checked) ResumeCliniqueMgmt.resumeCl.ClasseCanG = BasCommon_BO.EntentePrealable.en_Class.Class_I;
            if (CanGClasseII.Checked) ResumeCliniqueMgmt.resumeCl.ClasseCanG = BasCommon_BO.EntentePrealable.en_Class.Class_II;
            if (CanGClasseIII.Checked) ResumeCliniqueMgmt.resumeCl.ClasseCanG = BasCommon_BO.EntentePrealable.en_Class.Class_III;

            if (CanDClasseI.Checked) ResumeCliniqueMgmt.resumeCl.ClasseCanD = BasCommon_BO.EntentePrealable.en_Class.Class_I;
            if (CanDClasseII.Checked) ResumeCliniqueMgmt.resumeCl.ClasseCanD = BasCommon_BO.EntentePrealable.en_Class.Class_II;
            if (CanDClasseIII.Checked) ResumeCliniqueMgmt.resumeCl.ClasseCanD = BasCommon_BO.EntentePrealable.en_Class.Class_III;


            if (rbSupraclusion.Checked) ResumeCliniqueMgmt.resumeCl.OcclusionFace = BasCommon_BO.EntentePrealable.en_OccFace.Supraclusion;
            if (rbInfraclusion.Checked) ResumeCliniqueMgmt.resumeCl.OcclusionFace = BasCommon_BO.EntentePrealable.en_OccFace.Infraclusion;
            if (rbOccNormal.Checked) ResumeCliniqueMgmt.resumeCl.OcclusionFace = BasCommon_BO.EntentePrealable.en_OccFace.Normal;

            if (tbOclusion.SelectedIndex != 0) ResumeCliniqueMgmt.resumeCl.OcclusionValue = tbOclusion.SelectedIndex;


            if ((chkBxArtInvDroit.CheckState == CheckState.Checked) && (chkBxArtInvGauche.CheckState == CheckState.Checked)) 
                ResumeCliniqueMgmt.resumeCl.OcclusionInverse = BasCommon_BO.EntentePrealable.en_OccInverse.Droite_Et_Gauche;
            else
                if ((chkBxArtInvDroit.CheckState == CheckState.Unchecked) && (chkBxArtInvGauche.CheckState == CheckState.Unchecked))
                    ResumeCliniqueMgmt.resumeCl.OcclusionInverse = BasCommon_BO.EntentePrealable.en_OccInverse.Aucun;

                else
                {
                    if (chkBxArtInvGauche.Checked) ResumeCliniqueMgmt.resumeCl.OcclusionInverse = BasCommon_BO.EntentePrealable.en_OccInverse.Gauche;
                    if (chkBxArtInvDroit.Checked) ResumeCliniqueMgmt.resumeCl.OcclusionInverse = BasCommon_BO.EntentePrealable.en_OccInverse.Droite;

                }

            if (chkBxArtInvSaut.CheckState != CheckState.Indeterminate)
                ResumeCliniqueMgmt.resumeCl.SautArticule = chkBxArtInvSaut.CheckState == CheckState.Checked ? BasCommon_BO.EntentePrealable.en_OuiNon.Oui : BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            else
                ResumeCliniqueMgmt.resumeCl.SautArticule = BasCommon_BO.EntentePrealable.en_OuiNon.undefined;
            
            if (chkBxArtInvAnt.CheckState != CheckState.Indeterminate)
                ResumeCliniqueMgmt.resumeCl.ArticuleInvAnt = chkBxArtInvAnt.CheckState == CheckState.Checked ? BasCommon_BO.EntentePrealable.en_OuiNon.Oui : BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            else
                ResumeCliniqueMgmt.resumeCl.ArticuleInvAnt = BasCommon_BO.EntentePrealable.en_OuiNon.undefined;
            

            

            if (rbDiagMaxEndo.Checked) ResumeCliniqueMgmt.resumeCl.DiagMax = BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Endoalveolie;
            if (rbDiagMaxExo.Checked) ResumeCliniqueMgmt.resumeCl.DiagMax = BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Exoalveolie;

            if (rbDiagMandEndo.Checked) ResumeCliniqueMgmt.resumeCl.DiagMand = BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Endoalveolie;
            if (rbDiagMandExo.Checked) ResumeCliniqueMgmt.resumeCl.DiagMand = BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Exoalveolie;
            
            if (rbSensTrnMaxEndo.Checked) ResumeCliniqueMgmt.resumeCl.SensTransvMax = BasCommon_BO.EntentePrealable.en_DiagOsseux.Endognatie;
            if (rbSensTrnMaxExo.Checked) ResumeCliniqueMgmt.resumeCl.SensTransvMax = BasCommon_BO.EntentePrealable.en_DiagOsseux.Exognatie;

            if (rbSensTrnMandEndo.Checked) ResumeCliniqueMgmt.resumeCl.SensTransvMand = BasCommon_BO.EntentePrealable.en_DiagOsseux.Endognatie;
            if (rbSensTrnMandExo.Checked) ResumeCliniqueMgmt.resumeCl.SensTransvMand = BasCommon_BO.EntentePrealable.en_DiagOsseux.Exognatie;


            if ((chkbxLaterolDeviationDroite.CheckState != CheckState.Indeterminate) || (chkbxLaterodeviationGauche.CheckState != CheckState.Indeterminate))
                ResumeCliniqueMgmt.resumeCl.Laterodeviation = BasCommon_BO.EntentePrealable.en_Laterodeviation.Aucun;

            if (chkbxLaterolDeviationDroite.CheckState == CheckState.Checked) 
                ResumeCliniqueMgmt.resumeCl.Laterodeviation = BasCommon_BO.EntentePrealable.en_Laterodeviation.Droite;
            if (chkbxLaterodeviationGauche.CheckState == CheckState.Checked) 
                ResumeCliniqueMgmt.resumeCl.Laterodeviation = BasCommon_BO.EntentePrealable.en_Laterodeviation.Gauche;
            if ((chkbxLaterodeviationGauche.CheckState == CheckState.Checked) && (chkbxLaterolDeviationDroite.CheckState == CheckState.Checked)) 
                ResumeCliniqueMgmt.resumeCl.Laterodeviation = BasCommon_BO.EntentePrealable.en_Laterodeviation.Droite_Et_Gauche;

            if (chkbxFreinLabial.CheckState != CheckState.Indeterminate) ResumeCliniqueMgmt.resumeCl.FreinLabial = chkbxFreinLabial.CheckState == CheckState.Checked ? BasCommon_BO.EntentePrealable.en_OuiNon.Oui : BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            if (chkbxFreinLingual.CheckState != CheckState.Indeterminate) ResumeCliniqueMgmt.resumeCl.FreinLingual = chkbxFreinLingual.CheckState == CheckState.Checked ? BasCommon_BO.EntentePrealable.en_OuiNon.Oui : BasCommon_BO.EntentePrealable.en_OuiNon.Non;

            RegenerateDiagObjTraitmnt();
            Refresh();
        }

        private void rbSupraclusion_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CanGClasseI_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CanGClasseII_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CanGClasseIII_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FrmAnalyse3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                if (ResumeCliniqueMgmt.Analyse3IsValid != "")
                {
                    DialogResult dr = MessageBox.Show("Attention toutes les valeurs n'ont pas été saisies !\n" + ResumeCliniqueMgmt.Analyse1IsValid + "\n\n Souhaitez-vous appliquer les valeurs par defaut ?", "Attention!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    if (dr == DialogResult.Yes)
                    {
                        ResumeCliniqueMgmt.Analyse3AffectDefault();
                    }
                    if (dr == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void imageCtrlAgg2_Load(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MolDClasseI_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void rbSensTrnMandExo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbDiagMaxEndo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        void ReorganiseButtons()
        {
            Rectangle CurrentTabBound = tabControl1.SelectedTab.Bounds;


            gbxCanDroite.Parent = tabControl1.SelectedTab;
            gbxMolDroite.Parent = tabControl1.SelectedTab;
            gbxCanDroite.Location = new Point(2, CurrentTabBound.Height - gbxMolDroite.Height - gbxCanDroite.Height -  2);
            gbxMolDroite.Location = new Point(2, gbxCanDroite.Bottom - 2);
          //  gbxDiagAlveolaire.Location = new Point(2, gbxMolDroite.Bottom - 2);

            gbxOcclusionFace.Parent = tabControl1.SelectedTab;
            gbxDiagDentaire.Parent = tabControl1.SelectedTab;
           // gbxDiagAlveolaire.Parent = tabControl1.SelectedTab;
           // lblSensVertical.Parent = tabControl1.SelectedTab;
            //lblSensTransversal.Parent = tabControl1.SelectedTab;

            gbxDiagDentaire.Location = new Point(gbxCanDroite.Width + 2, CurrentTabBound.Height - gbxDiagDentaire.Height - 2);
            
            //lblSensTransversal.Size = new Size(gbxDiagAlveolaire.Width, 20);
           // lblSensTransversal.Location = new Point(gbxCanDroite.Width + 2, gbxDiagAlveolaire.Top - lblSensTransversal.Height);
            gbxOcclusionFace.Location = new Point(gbxCanDroite.Width + 2, CurrentTabBound.Height - gbxDiagDentaire.Height - gbxOcclusionFace.Height - 2);
          //  lblSensVertical.Size = new Size(gbxOcclusionFace.Width, 20);
          //  lblSensVertical.Location = new Point(gbxCanDroite.Width + 2, gbxOcclusionFace.Top - lblSensVertical.Height);



            gbxCanGauche.Parent = tabControl1.SelectedTab;
            gbxMolGauche.Parent = tabControl1.SelectedTab;
            gbxCanGauche.Location = new Point(gbxOcclusionFace.Right + 2, CurrentTabBound.Height - gbxMolGauche.Height - gbxCanGauche.Height - 2);
            gbxMolGauche.Location = new Point(gbxOcclusionFace.Right + 2, gbxCanGauche.Bottom - 2);
           // gbxDiagBasal.Location = new Point(gbxOcclusionFace.Right + 2, gbxMolGauche.Bottom - 2);
            





        }
        
        /*
        void ReorganiseButtons()
        {
            Rectangle CurrentTabBound = tabControl1.SelectedTab.Bounds;

            switch (tabControl1.SelectedIndex)
            {
                case 0 ://Gauche
                    gbxCanGauche.Parent = tabControl1.SelectedTab;
                    gbxMolGauche.Parent = tabControl1.SelectedTab;
                    gbxCanGauche.Location = new Point(2, CurrentTabBound.Height - gbxCanGauche.Height - 2);
                    gbxMolGauche.Location = new Point(gbxCanGauche.Width + 2, CurrentTabBound.Height - gbxCanGauche.Height - 2);

                    break;
                case 1://Face
                    gbxOcclusionFace.Parent = tabControl1.SelectedTab;
                    gbxOcclusionFace.Location = new Point((CurrentTabBound.Width-gbxOcclusionFace.Width)/2, CurrentTabBound.Height - gbxCanDroite.Height - 2);
                    break;
                case 2://Droite
                    gbxCanDroite.Parent = tabControl1.SelectedTab;
                    gbxMolDroite.Parent = tabControl1.SelectedTab;
                    gbxCanDroite.Location = new Point(2, CurrentTabBound.Height - gbxCanDroite.Height - 2);
                    gbxMolDroite.Location = new Point(gbxCanDroite.Width + 2, CurrentTabBound.Height - gbxCanDroite.Height - 2);
                    
                   break;
                case 3://Toutes
                   gbxCanDroite.Parent = tabControl1.SelectedTab;
                   gbxMolDroite.Parent = tabControl1.SelectedTab;
                   gbxCanDroite.Location = new Point(2, CurrentTabBound.Height - gbxCanDroite.Height - 2);
                   gbxMolDroite.Location = new Point(2, CurrentTabBound.Height - gbxMolDroite.Height - gbxCanDroite.Height - 2);

                   gbxOcclusionFace.Parent = tabControl1.SelectedTab;
                   gbxOcclusionFace.Location = new Point(gbxCanDroite.Width + 2, CurrentTabBound.Height - gbxOcclusionFace.Height - 2);
                   

                   gbxCanGauche.Parent = tabControl1.SelectedTab;
                   gbxMolGauche.Parent = tabControl1.SelectedTab;
                   gbxCanGauche.Location = new Point(gbxOcclusionFace.Right + 2, CurrentTabBound.Height - gbxCanGauche.Height - 2);
                   gbxMolGauche.Location = new Point(gbxOcclusionFace.Right + 2, CurrentTabBound.Height - gbxMolGauche.Height - gbxCanGauche.Height - 2);


                    
                    break;
            }
        }
        */
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReorganiseButtons();
        }

        private void CheckDiags()
        {


            //Pas d'objectifs pour 20 et 23 car classe 1 = normal            
            //41,42,43,45,46,47 = normal
            CommonDiagnostic ClIIMol = null;
            CommonDiagnostic ClIIIMol = null;
            CommonDiagnostic ClIICan = null;
            CommonDiagnostic ClIIICan = null;


            //On ne demande qu'une fois le traitement pour classe II ou III
            //Donc traitement particulier pour les diags : 21, 22, 24, 25

            int[] AcceptedDiags = new int[] { 38, 39, 40, 44 };

            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
            {
                if (cd == null) continue;
                if (cd.Id == 22) ClIIIMol = cd;
                if (cd.Id == 21) ClIIMol = cd;
                if (cd.Id == 25) ClIIICan = cd;
                if (cd.Id == 24) ClIICan = cd;
            }

            if (((ClIIIMol != null) && (ClIIICan != null)))
            {
                FrmLittleWizard frm = new FrmLittleWizard(ClIIIMol,CurrentPat);
                if ((frm.CanBeShown) && (frm.ShowDialog() == DialogResult.OK))
                {
                    foreach (CommonObjectifFromDiag co in frm.values)
                    {
                        if (!CurrentPat.SelectedObjectifs.Contains(co.objectif))
                            CurrentPat.SelectedObjectifs.Add(co.objectif);
                    }
                    ClIIIMol.SelectedObjectif = true;
                    ClIIICan.SelectedObjectif = true;
                }
            }else
                if (((ClIIMol != null) && (ClIICan != null)))
                {
                    FrmLittleWizard frm = new FrmLittleWizard(ClIIMol,CurrentPat);
                    if ((frm.CanBeShown) && (frm.ShowDialog() == DialogResult.OK))
                    {
                        foreach (CommonObjectifFromDiag co in frm.values)
                        {
                            if (!CurrentPat.SelectedObjectifs.Contains(co.objectif))
                                CurrentPat.SelectedObjectifs.Add(co.objectif);
                        }
                        ClIIMol.SelectedObjectif = true;
                        ClIICan.SelectedObjectif = true;
                    }
                }
                else
                {
                    //Les classe molaire et canines sont différentes, donc on demande pour chaque
                    AcceptedDiags = new int[] { 21, 22, 24, 25, 38, 39, 40, 44 };
                }

            


            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
            {
                if (cd == null) continue;
                if (AcceptedDiags.Contains(cd.Id))
                {

                    if (cd.SelectedObjectif) continue;
                    FrmLittleWizard frm = new FrmLittleWizard(cd,CurrentPat);
                    if ((frm.CanBeShown) && (frm.ShowDialog() == DialogResult.OK))
                    {
                        foreach (CommonObjectifFromDiag co in frm.values)
                        {
                            if (!CurrentPat.SelectedObjectifs.Contains(co.objectif))
                                CurrentPat.SelectedObjectifs.Add(co.objectif);
                        }
                        cd.SelectedObjectif = true;
                    }
                }
            }
        }


        private void BTnNext_Click(object sender, EventArgs e)
        {
            CheckDiags();
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void rbSensTrnMaxExo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lvPPT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start(((string)lvPPT.SelectedItems[0].Tag));
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (MdiParent != null)
            {
                MdiParent.Invoke(BasCommon_BL.CommonCalls.NextScreenHandler, new object[] { this });
            }
            else
            {

                this.Visible = false;
                this.WindowState = FormWindowState.Normal;

                CurrentScreenIdx++;
                CurrentScreenIdx = CurrentScreenIdx >= Screen.AllScreens.Length ? 0 : CurrentScreenIdx;



                this.Bounds = Screen.AllScreens[CurrentScreenIdx].WorkingArea;

                //   this.WindowState = FormWindowState.Maximized;
                this.Visible = true;
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            ExportToLetter(false);
        }
        private static string _RegistryKey = "Software\\BASE\\BASEPractice";

        private static string _RegistryKeyPref = _RegistryKey + "\\Preferences";
        private static string _CurrentCabRegistryKey = _RegistryKeyPref + "\\CurrentCab";
        public static void GetCurrentCabOnRegistry()
        {


            RegistryKey key = Registry.CurrentUser.OpenSubKey(_CurrentCabRegistryKey);

            // If the return value is null, the key doesn't exist
            if (key == null) return;

            string objValidityDate = (string)key.GetValue("ValidityDate");
            string objValidityUser = (string)key.GetValue("ValidityCab");

            key.Close();

            DateTime ValidityDate;
            if (DateTime.TryParse(objValidityDate, out ValidityDate))
            {
                int idCabinet = Convert.ToInt32(objValidityUser);
                prefix = CabinetMgmt.Cabinet.Find(c => c.Id == idCabinet).prefix;
            }
        }
        private static string _prefix = "";
        public static string prefix
        {
            get
            {
                if (_prefix == null || _prefix == "")
                    GetCurrentCabOnRegistry();
                return _prefix;
            }
            set
            {
                _prefix = "_" + value;
            }


        }
        private static string _templateFolder = "";
        public static string templateFolder
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["TEMPLATE_FOLDER" + prefix];
            }
            set
            {
                _templateFolder = "_" + value;
            }


        }
        public void ExportToLetter(bool DirectPrint)
        {
            string file = templateFolder + System.Configuration.ConfigurationManager.AppSettings["CourrierAnalyseOcclusal"];
            if (!System.IO.File.Exists(file))
            {
                MessageBox.Show("Le fichier n'existe pas \n" + file);
                return;
            }

            string fOccGauche = GetAnalyseImageOccGauche();

            string fOccFace = GetAnalyseImageOccFace();

            string fOccDroite = GetAnalyseImageOccDroit();


            /*
             public float EspaceDentaireBuccal = 0;
        public float IncisiveMolaireDroit = 0;
        public float IncisiveMolaireGauche = 0;
             */
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseOccGauche", fOccGauche);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseOccFace", fOccFace);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AnalyseOccDroite", fOccDroite);

            BASEDiag_BL.ResumeCliniqueMgmt.AddAttributsToCourrier();

            BASEDiag_BL.OLEAccess.BASLetter.AffectPrintSettings(PrinterSettingsMgmt.ImpressionAnalyse);

            if (!DirectPrint)
                BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);
            else
                BASEDiag_BL.OLEAccess.BASLetter.PrintFrom(file);
        }

        public string GetAnalyseImageOccDroit()
        {
            Bitmap bmp = new Bitmap(TriImg3.Width, TriImg3.Height);
            Graphics g = Graphics.FromImage(bmp);
            TriImg3.PaintOn(g, new Rectangle(0, 0, TriImg3.Width, TriImg3.Height));
            string fOccDroite = Path.GetTempFileName();
            bmp.Save(fOccDroite);
            return fOccDroite;
        }

        public string GetAnalyseImageOccFace()
        {
            Bitmap bmp = new Bitmap(TriImg2.Width, TriImg2.Height);
            Graphics g = Graphics.FromImage(bmp);
            TriImg2.PaintOn(g, new Rectangle(0, 0, TriImg2.Width, TriImg2.Height));

          //  TriImg2.PaintOn(g, new Rectangle(0, 0, TriImg2.Width, TriImg2.Height));
            string fOccFace = Path.GetTempFileName();
            bmp.Save(fOccFace);
            return fOccFace;
        }

        public string GetAnalyseImageOccGauche()
        {
            Bitmap bmp = new Bitmap(TriImg1.Width, TriImg1.Height);
            Graphics g = Graphics.FromImage(bmp);
            TriImg1.PaintOn(g, new Rectangle(0, 0, TriImg1.Width, TriImg1.Height));
            string fOccGauche = Path.GetTempFileName();
            bmp.Save(fOccGauche);
            return fOccGauche;
        }

        private void rbArticuleInvG_CheckedChanged(object sender, EventArgs e)
        {
            chkbxLaterodeviationGauche.Checked = (chkBxArtInvGauche.Checked);
            chkbxLaterolDeviationDroite.Checked = (chkBxArtInvDroit.Checked);

        }

        private void TriImg3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (((ImageCtrlAgg)sender).Parent == this.pnlFullScreen)
            {
                pnlFullScreen.Visible = false;
                this.pnlFullScreen.Controls.Remove((ImageCtrlAgg)sender);
                this.tabTriImg.Controls.Add((ImageCtrlAgg)sender);
                ((ImageCtrlAgg)sender).Parent = this.tabTriImg;
                ((ImageCtrlAgg)sender).Dock = DockStyle.None;
                InitPos();
            }
            else
            {

                this.tabTriImg.Controls.Remove((ImageCtrlAgg)sender);
                this.pnlFullScreen.Controls.Add((ImageCtrlAgg)sender);
                ((ImageCtrlAgg)sender).Parent = this.pnlFullScreen;
                ((ImageCtrlAgg)sender).Dock = DockStyle.Fill;
                pnlFullScreen.Visible = true;
                ((ImageCtrlAgg)sender).zoomAuto();
                ((ImageCtrlAgg)sender).Center();
            }
        }

        private void TriImg3_Click(object sender, EventArgs e)
        {
            
        }

        private void TriImg3_OnRadioChanged(object sender, EventArgs e)
        {
            
            ResumeCliniqueMgmt.resumeCl.Img_Int_Gauche = TriImg3.file;
        }

        private void TriImg1_OnRadioChanged(object sender, EventArgs e)
        {
            ResumeCliniqueMgmt.resumeCl.Img_Int_Droit = TriImg1.file;
        }

        private void TriImg2_OnRadioChanged(object sender, EventArgs e)
        {

            ResumeCliniqueMgmt.resumeCl.Img_Int_Face = TriImg2.file;
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void FrmAnalyse3_Resize(object sender, EventArgs e)
        {
            InitPos();
            ReorganiseButtons();
        }

        private void rbOccNormal_Paint(object sender, PaintEventArgs e)
        {

            if (ResumeCliniqueMgmt.resumeCl.OcclusionFace == BasCommon_BO.EntentePrealable.en_OccFace.undefined)
            {
                if (sender == rbOccNormal)
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}
                if (sender == rbInfraclusion)
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}
                if (sender == rbSupraclusion)
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}

            }

            if (ResumeCliniqueMgmt.resumeCl.DiagMand == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined)
            {
                if (sender == rbDiagMandEndo)
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}
                if (sender == rbDiagMandExo)
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}
                                       
            }
            if (ResumeCliniqueMgmt.resumeCl.DiagMax == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined)
            {
                if (sender == rbDiagMaxEndo)
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}
                if (sender == rbDiagMaxExo)
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}

            }
            if (ResumeCliniqueMgmt.resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.undefined)
            {
                if ((sender == CanDClasseI) || (sender == CanDClasseII) || (sender == CanDClasseIII))
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}
               
            }

            if (ResumeCliniqueMgmt.resumeCl.ClasseCanG == BasCommon_BO.EntentePrealable.en_Class.undefined)
            {
                if ((sender == CanGClasseI) || (sender == CanGClasseII) || (sender == CanGClasseIII))
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}

            }
            if (ResumeCliniqueMgmt.resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.undefined)
            {
                if ((sender == MolDClasseI) || (sender == MolDClasseII) || (sender == MolDClasseIII))
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}

            }

            if (ResumeCliniqueMgmt.resumeCl.ClasseMolG == BasCommon_BO.EntentePrealable.en_Class.undefined)
            {
                if ((sender == MolGClasseI) || (sender == MolGClasseII) || (sender == MolGClasseIII))
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}

            }

            if (ResumeCliniqueMgmt.resumeCl.SensTransvMax == BasCommon_BO.EntentePrealable.en_DiagOsseux.undefined)
            {
                if ((sender == rbSensTrnMaxEndo) || (sender == rbSensTrnMaxExo))
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}

            }

            if (ResumeCliniqueMgmt.resumeCl.SensTransvMand == BasCommon_BO.EntentePrealable.en_DiagOsseux.undefined)
            {
                if ((sender == rbSensTrnMandEndo) || (sender == rbSensTrnMandExo))
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}

            }

            if (ResumeCliniqueMgmt.resumeCl.SautArticule == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                if (sender == chkBxArtInvSaut)
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}

           

            if (ResumeCliniqueMgmt.resumeCl.OcclusionInverse == BasCommon_BO.EntentePrealable.en_OccInverse.undefined)
            {
                if ((sender == chkBxArtInvGauche) || (sender == chkBxArtInvDroit))
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}

            }
            if (ResumeCliniqueMgmt.resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.undefined)
            {
                if ((sender == chkbxLaterolDeviationDroite) || (sender == chkbxLaterodeviationGauche))
                    {e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));return;}

            }


            if (sender is CheckBox)
            {
                CheckBox rb = ((CheckBox)sender);

                switch (rb.CheckState)
                {
                    case CheckState.Indeterminate:
                        e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));
                        //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, 255, 255, 255)), new Rectangle(0, 0, rb.Width, rb.Height));
                        break;
                    case CheckState.Checked: e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.check,new Point(0,0));
                        break;

                   
                }                
            }

            if (sender is RadioButton)
            {
                RadioButton rb = ((RadioButton)sender);

                if (rb.Checked)
                    e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.check, new Point(0, 0));
                                                
            }
            
            base.OnPaint(e);
        }

        private void tabTriImg_Click(object sender, EventArgs e)
        {
           

        }

        private void btnRisque_Click(object sender, EventArgs e)
        {
            FrmAddRisquesPerso frm = new FrmAddRisquesPerso(_CurrentPat);
            frm.ShowDialog();
        }

        private void chkbxFreinLingual_MouseDown(object sender, MouseEventArgs e)
        {
            if (((CheckBox)sender).CheckState == CheckState.Indeterminate)
                ((CheckBox)sender).CheckState = CheckState.Unchecked;
        }

        private void chkbxFreinLingual_MouseUp(object sender, MouseEventArgs e)
        {
            MolGClasseI_CheckedChanged(sender, new EventArgs());
        }

        private void RegenerateDiagObjTraitmnt()
        {
            ResumeCliniqueMgmt.GenerateCurrentDiags();

            lstBxDiag.Items.Clear();
            foreach (CommonDiagnostic cd in ResumeCliniqueMgmt.resumeCl.diagnostics)
                if (cd!=null)
                    lstBxDiag.Items.Add(cd);


            List<CommonObjectifFromDiag> lstobjs = CommonDiagnosticsMgmt.getCommonObjectifsEnfant(ResumeCliniqueMgmt.resumeCl.diagnostics);

            lstBxObjectifs.Items.Clear();
            foreach (CommonObjectif cd in CurrentPat.SelectedObjectifs)
                lstBxObjectifs.Items.Add(cd);
            /*
            foreach (CommonObjectifFromDiag cd in lstobjs)
                if (!CurrentPat.SelectedObjectifs.Contains(cd.objectif))
                    lstBxObjectifs.Items.Add(cd);
            */



        }

      
        private void lstBxObjectifs_DrawItem(object sender, DrawItemEventArgs e)
        {
            CommonObjectif obj = null;
            if (e.Index == -1) return;


            if (lstBxObjectifs.Items[e.Index] is CommonObjectifFromDiag)
                obj = ((CommonObjectifFromDiag)lstBxObjectifs.Items[e.Index]).objectif;

            if (lstBxObjectifs.Items[e.Index] is CommonObjectif)
                obj = ((CommonObjectif)lstBxObjectifs.Items[e.Index]);


            Brush b = Brushes.Black;

            if (CurrentPat.SelectedObjectifs.Contains(obj))
                b = Brushes.Green;

            CommonDiagnostic selecteddiag = (CommonDiagnostic)lstBxDiag.SelectedItem;
            if (lstBxObjectifs.Items[e.Index] is CommonObjectifFromDiag)
            {
                obj = ((CommonObjectifFromDiag)lstBxObjectifs.Items[e.Index]).objectif;
                if (((CommonObjectifFromDiag)lstBxObjectifs.Items[e.Index]).diagnostic == selecteddiag)
                    b = Brushes.Blue;
            }

            e.Graphics.DrawString(lstBxObjectifs.Items[e.Index].ToString(), lstBxObjectifs.Font, b, e.Bounds.Location);

        }

        private void lstBxObjectifs_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void lstBxDiag_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBxObjectifs.Refresh();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        void TriImg1_OnEndSaisie(object sender, EventArgs e)
        {

            ResumeCliniqueMgmt.resumeCl.LstPtAnalyseOccD = TriImg1.ListOfPoints;

            InitDisplay();
            Refresh();
        }

        void TriImg3_OnEndSaisie(object sender, EventArgs e)
        {

            ResumeCliniqueMgmt.resumeCl.LstPtAnalyseOccG = TriImg3.ListOfPoints;

            InitDisplay();
            Refresh();
        }

        void TriImg2_OnEndSaisie(object sender, EventArgs e)
        {

            ResumeCliniqueMgmt.resumeCl.LstPtAnalyseOccF = TriImg2.ListOfPoints;

            InitDisplay();
            Refresh();
        }

        private void barrePatient1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tbDecInterIncisive_Scroll(object sender, EventArgs e)
        {

        }

        private void gbxCanDroite_Enter(object sender, EventArgs e)
        {

        }

        private void CanDClasseII_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CanDClasseI_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CanDClasseIII_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void gbxMolGauche_Enter(object sender, EventArgs e)
        {

        }

        private void MolGClasseII_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MolGClasseI_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void MolGClasseIII_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void gbxDiagDentaire_Enter(object sender, EventArgs e)
        {

        }

        private void chkBxArtInvAnt_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkBxArtInvGauche_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkBxArtInvDroit_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkBxArtInvSaut_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void gbxOcclusionFace_Enter(object sender, EventArgs e)
        {

        }

        private void tbOclusion_Load(object sender, EventArgs e)
        {

        }

        private void rbOccNormal_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbSupraclusion_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void rbInfraclusion_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void gbxCanGauche_Enter(object sender, EventArgs e)
        {

        }

        private void CanGClasseII_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void CanGClasseI_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void CanGClasseIII_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void gbxMolDroite_Enter(object sender, EventArgs e)
        {

        }

        private void MolDClasseII_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void MolDClasseI_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void MolDClasseIII_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TriImg3_Load(object sender, EventArgs e)
        {

        }

        private void TriImg2_Load(object sender, EventArgs e)
        {

        }

        private void TriImg1_Load(object sender, EventArgs e)
        {

        }

        private void gbxDiagAlveolaire_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rbSensTrnMandExo_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lvPPT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void chkbxFreinLingual_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxFreinLabial_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxLaterodeviationGauche_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxLaterolDeviationDroite_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pnlFullScreen_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTitre_Click(object sender, EventArgs e)
        {

        }

        private void lstBxObjectifs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void imageCtrl1_Load(object sender, EventArgs e)
        {

        }
    }
}
