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
    public partial class FrmAddPoseAppareillage : Form
    {



        Semestre S1 = null;
        Semestre S2 = null;
        Semestre S3 = null;
        Semestre S4 = null;
        Semestre S5 = null;
        Semestre S6 = null;
        Semestre S7 = null;
        Semestre S8 = null;

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

        private PoseAppareil _poseappareillage;
        public PoseAppareil poseappareillage
        {
            get
            {
                return _poseappareillage;
            }
            set
            {
                _poseappareillage = value;
            }
        }
     
        int CurentIdxPnl = 0;

        public FrmAddPoseAppareillage(Patient patient, Proposition propo)
        {
            proposition = propo;
            CurrentPatient = patient;
            InitializeComponent();


            foreach(Traitement t in proposition.traitements)
                foreach (Semestre s in t.semestres)
                {
                    if (s.NumSemestre == 1) S1 = s;
                    if (s.NumSemestre == 2) S2 = s;
                    if (s.NumSemestre == 3) S3 = s;
                    if (s.NumSemestre == 4) S4 = s;
                    if (s.NumSemestre == 5) S5 = s;
                    if (s.NumSemestre == 6) S6 = s;
                    if (s.NumSemestre == 7) S7 = s;
                    if (s.NumSemestre == 8) S8 = s;

                }
            
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
                if (Build())
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

                
                foreach(Appareil a in AppareilMgmt.appareils)
                {
                    lstbxAppareil.Items.Add(a);
                }

                
            }
        }






        private Semestre AddSemestre(int numSemestre, Traitement traitement, TemplateActePG acteGestion, Double MontantDuSemestre)
        {
            Semestre sem = new Semestre();
            sem.NumSemestre = numSemestre;
            sem.NbSurveillance = 0;
            

            sem.traitementSecu = acteGestion;

            sem.DateDebut = null;
            sem.DateFin = null;

            traitement.semestres.Add(sem);

            return sem;
        }

        private bool Build()
        {

           poseappareillage = new PoseAppareil();
           poseappareillage.appareil = (Appareil)lstbxAppareil.SelectedItem;
           if ((S1 != null) && (chkbxSem1.Checked)) poseappareillage.semestres.Add(S1);
           if ((S2 != null) && (chkbxSem2.Checked)) poseappareillage.semestres.Add(S2);
           if ((S3 != null) && (chkbxSem3.Checked)) poseappareillage.semestres.Add(S3);
           if ((S4 != null) && (chkbxSem4.Checked)) poseappareillage.semestres.Add(S4);
           if ((S5 != null) && (chkbxSem5.Checked)) poseappareillage.semestres.Add(S5);
           if ((S6 != null) && (chkbxSem6.Checked)) poseappareillage.semestres.Add(S6);
           if ((S7 != null) && (chkbxSem7.Checked)) poseappareillage.semestres.Add(S7);
           if ((S8 != null) && (chkbxSem8.Checked)) poseappareillage.semestres.Add(S8);

            

           
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

       

       
    }
}
