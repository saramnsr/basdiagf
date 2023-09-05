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
    public partial class FrmAddTraitmnt : Form
    {

       
   

        private int _NombreSemestresEntames;
        public int NombreSemestresEntames
        {
            get
            {
                return _NombreSemestresEntames;
            }
            set
            {
                _NombreSemestresEntames = value;
            }
        }

        

        private TemplateActePG.EnumPhase _typeDephase;
        public TemplateActePG.EnumPhase typeDephase
        {
            get
            {
                return _typeDephase;
            }
            set
            {
                _typeDephase = value;
            }
        }

        private Proposition _proposition;
        public Proposition proposition
        {
            get
            {
                return _proposition;
            }
            set
            {
                _proposition = value;
            }
        }

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

        private Traitement _traitement;
        public Traitement traitement
        {
            get
            {
                return _traitement;
            }
            set
            {
                _traitement = value;
            }
        }

        int CurentIdxPnl = 0;

        public FrmAddTraitmnt(Patient patient, Proposition prop, int nombreSemestresEntames,Traitement traitementToEdit,int nombreSemSurveillance)
        {
            proposition = prop;
            CurrentPatient = patient;
            NombreSemestresEntames = nombreSemestresEntames;
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

        private void lstbxAppareil_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NextPage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NextPage();
        }

        private void NextPage()
        {
            if (CurentIdxPnl < pnlContainer.Controls.Count - 1)
                ShowPanel((Panel)pnlContainer.Controls[++CurentIdxPnl]);
            else
            {
                if (BuildTraitement())
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            PreviousPage();
        }

        private void PreviousPage()
        {
            if (CurentIdxPnl > 0)
                ShowPanel((Panel)pnlContainer.Controls[--CurentIdxPnl]);
        }

        private void FrmAddTraitmnt_Load(object sender, EventArgs e)
        {
            pnlContainer.Controls.Clear();
            pnlContainer.Controls.Add(pnlTypeTraitement);
            pnlContainer.Controls.Add(pnlGestion);
            pnlContainer.Controls.Add(pnlChoixApp);
            pnlContainer.Controls.Add(pnlDetailsAppareil);
            pnlContainer.Controls.Add(pnlPlanification);
            ShowPanel((Panel)pnlContainer.Controls[0]);
        }

        private void pnlChoixApp_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlChoixApp.Visible)
            {

                

                lstbxAppareil.Items.Clear();

                if (cbxActePlanGestion.SelectedItem != null)
                    foreach (Appareil app in ((TemplateActePG)cbxActePlanGestion.SelectedItem).SuggestedAppareils)
                        lstbxAppareil.Items.Add(app);

                if (lstbxAppareil.Items.Count>0) 
                    lstbxAppareil.SelectedIndex = 0;

                List<CommonAppareilFromObj> lstSelectedApp = CommonObjectifsMgmt.getCommonAppareilFromObj(CurrentPatient.SelectedObjectifs);

                bool canbreak = false;
                
                    foreach (CommonAppareilFromObj commapp in lstSelectedApp)
                    {
                        foreach (Appareil app in lstbxAppareil.Items)
                        {
                        if (commapp.appareil == app)
                        {
                            bool canselect = true;
                            foreach (PoseAppareil papp in proposition.poseAppareils)
                                if (papp.appareil == app) canselect = false;

                            if (canselect)
                            {
                                lstbxAppareil.SelectedItem = app;
                                canbreak = true;
                                break;
                            }
                        }
                    }
                    if (canbreak) break;
                }


               


                if (lstbxAppareil.Items.Count == 0)
                    NextPage();



                
            }
        }






        private Semestre AddSemestre(int numSemestre, Traitement traitement, TemplateActePG acteGestion, Double MontantDuSemestre)
        {
            Semestre sem = new Semestre();
            sem.NumSemestre = numSemestre;
            sem.NbSurveillance = 0;


            sem.Montant_Honoraire = MontantDuSemestre;

            sem.traitementSecu = acteGestion;
            sem.CodeSemestre = "S" + numSemestre.ToString();

            TemplateActePG surv = TemplateApctePGMgmt.getCodeSecu("SURV");


            sem.MontantSurveillance = surv.Valeur;
            sem.traitementSecuSurveillance = surv;




            sem.DateDebut = null;
            sem.DateFin = null;

            traitement.semestres.Add(sem);

            return sem;
        }

        private bool BuildTraitement()
        {
            if (traitement == null) traitement = new Traitement();

            if (typeDephase== TemplateActePG.EnumPhase.Pédiatrique)
                traitement.Libelle = "Pédiatrie";

            if (typeDephase == TemplateActePG.EnumPhase.Orthodontique)
                traitement.Libelle = "Orthodontie";

            if (typeDephase == TemplateActePG.EnumPhase.Orthopedique)
                traitement.Libelle = "Orthopédie";

            traitement.Phase = typeDephase;

            traitement.Parent = proposition;


           Double tarifSemestre = Convert.ToDouble(txtbxTarifReel.Text);
           TemplateActePG actegestion = (TemplateActePG)cbxActePlanGestion.SelectedItem;


            Semestre S1 = null;
            Semestre S2 = null;
           Semestre S3 = null;
           Semestre S4 = null;
           Semestre S5 = null;
           Semestre S6 = null;
           Semestre S7 = null;
            Semestre S8 = null;

           if (chkbxSem1.Checked)
               S1 = AddSemestre(1, traitement, actegestion, tarifSemestre);
           if (chkbxSem2.Checked)
               S2 = AddSemestre(2, traitement, actegestion, tarifSemestre);
           if (chkbxSem3.Checked)
               S3 = AddSemestre(3, traitement, actegestion, tarifSemestre);
           if (chkbxSem4.Checked)
               S4 = AddSemestre(4, traitement, actegestion, tarifSemestre);
           if (chkbxSem5.Checked)
               S5 = AddSemestre(5, traitement, actegestion, tarifSemestre);
           if (chkbxSem6.Checked)
               S6 = AddSemestre(6, traitement, actegestion, tarifSemestre);
           if (chkbxSem7.Checked)
               S7 = AddSemestre(7, traitement, actegestion, tarifSemestre);
           if (chkbxSem8.Checked)
               S8 = AddSemestre(8, traitement, actegestion, tarifSemestre);

           Appareil app = (Appareil)lstbxAppareil.SelectedItem;
           if (app != null)
           {
               PoseAppareil papp = new PoseAppareil();
               papp.appareil = (Appareil)lstbxAppareil.SelectedItem;
               if (S1 != null) papp.semestres.Add(S1);
               if (S2 != null) papp.semestres.Add(S2);
               if (S3 != null) papp.semestres.Add(S3);
               if (S4 != null) papp.semestres.Add(S4);
               if (S5 != null) papp.semestres.Add(S5);
               if (S6 != null) papp.semestres.Add(S6);
               if (S7 != null) papp.semestres.Add(S7);
               if (S8 != null) papp.semestres.Add(S8);

               proposition.poseAppareils.Add(papp);
           }
            

            double ts = 0;

            if (!Double.TryParse(txtbxTarifReel.Text, out ts))
            {
                MessageBox.Show("Tarif Réel invalide!");
                return false;
            }

            return true;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void pnlTarifs_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlGestion.Visible)
            {




                int maxsem = PropositionMgmt.FindSemestreAffecte(proposition)+1;


                cbxActePlanGestion.Items.Clear();
                foreach (TemplateActePG tpmle in TemplateApctePGMgmt.getPhasesTraitement(typeDephase))
                {
                    cbxActePlanGestion.Items.Add(tpmle);

                }


                if (cbxActePlanGestion.Items.Count > 0)
                    cbxActePlanGestion.SelectedIndex = 0;
            }

        }

        

        private void lblDescriptifGestion_Click(object sender, EventArgs e)
        {

        }

        private void pnlGestion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbxActePlanGestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            TemplateActePG template = (TemplateActePG)cbxActePlanGestion.SelectedItem;
            lblDescriptifGestion.Text = template.Libelle;
            lblDescriptifGestion.Text += "\nValeur : " + template.Valeur.ToString("C2");
            lblDescriptifGestion.Text += "\nCoefficient Secu : " + template.DisplayCodeNVal;

            txtbxTarifReel.Text = template.Valeur.ToString();

        }
                
        private void pnlDetailsAppareil_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlDetailsAppareil.Visible)
            {
                if (lstbxAppareil.SelectedItem == null)
                {
                    NextPage();
                }
            }
        }

        private void pnlPlanification_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlPlanification.Visible)
            {
                

                

                    int maxsem = PropositionMgmt.FindSemestreAffecte(proposition)+1;


                    int nbsemestrepardefaut = 1;

                    if (cbxActePlanGestion.SelectedItem != null)
                        nbsemestrepardefaut = ((TemplateActePG)cbxActePlanGestion.SelectedItem).NBMois / 6;

                if (maxsem>0)
                    for (int i = maxsem; i < maxsem + nbsemestrepardefaut; i++)
                        ((CheckBox)pnlSemestre.Controls[i - 1]).Checked = true;
                
            }
        }

        private void pnlTypeTraitement_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlTypeTraitement.Visible)
            {



                if (CurrentPatient.AgeNbYears <= 4)
                {
                    typeDephase = TemplateActePG.EnumPhase.Pédiatrique;
                    chkBxPediatrie.Checked = true;
                }
                else
                    if (CurrentPatient.AgeNbYears < 16)
                    {
                        bool OrthopedieAlreadyDefined = false;
                        foreach (Traitement t in proposition.traitements)
                            if (t.Phase == TemplateActePG.EnumPhase.Orthopedique)
                                OrthopedieAlreadyDefined = true;

                        if (!OrthopedieAlreadyDefined)
                        {
                            typeDephase = TemplateActePG.EnumPhase.Orthopedique;
                            chkbxOrthopedie.Checked = true;
                        }
                        else
                        {
                            typeDephase = TemplateActePG.EnumPhase.Orthodontique;
                            chkbxOrthodontique.Checked = true;
                        }

                    }
                    else
                    {
                        typeDephase = TemplateActePG.EnumPhase.Orthodontique;
                        chkbxOrthodontique.Checked = true;
                    }

            }
        }

        private void chkBxPediatrie_Click(object sender, EventArgs e)
        {
            
        }

        private void chkbxOrthopedie_Click(object sender, EventArgs e)
        {
            
        }     

        private void chkbxOrthodontique_Click(object sender, EventArgs e)
        {
            
        }

        private void chkbxOrthopedie_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkBxPediatrie_MouseDown(object sender, MouseEventArgs e)
        {
            typeDephase = TemplateActePG.EnumPhase.Pédiatrique;
            NextPage();
        }

        private void chkbxOrthopedie_MouseDown(object sender, MouseEventArgs e)
        {
            typeDephase = TemplateActePG.EnumPhase.Orthopedique;
            NextPage();
        }

        private void chkbxOrthodontique_MouseDown(object sender, MouseEventArgs e)
        {
            typeDephase = TemplateActePG.EnumPhase.Orthodontique;
            NextPage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BASEDiag_BL.OLEAccess.BASELabo.NouvelleDemandeInStandBy(CurrentPatient.Id);

            /*
            FrmBaseLaboPrevision frm;

            if (prevision != null) 
                frm = new FrmBaseLaboPrevision(prevision);
            else
                frm = new FrmBaseLaboPrevision();

            frm.CurrentAppareil = (Appareil)lstbxAppareil.SelectedItem;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                prevision = frm.CurrentDemande;
            }
             * 
             */
        }

        private void lstbxAppareil_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void NombreDeSemestreEnSurveillance_ValueChanged(object sender, EventArgs e)
        {

        }

       
    }
}
