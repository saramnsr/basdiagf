using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BO;

namespace BaseCommonControls
{
    public partial class FrmPointage : Form
    {



        

        Utilisateur currentuser = null;
        public FrmPointage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentuser==null) return;
            //if (currentuser.pointageDuJour.Count > 0)
            //{

            //    if (currentuser.pointageDuJour[currentuser.pointageDuJour.Count - 1].sens == Pointage.SensPointage.Entree)
            //    {
            //        MessageBox.Show("Entrée déjà pointée");
            //        return;
            //    }
            //}
            BasCommon_BL.UtilisateursMgt.AddPointage(currentuser, Pointage.SensPointage.Entree);
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentuser == null) return;
            //if (currentuser.pointageDuJour[currentuser.pointageDuJour.Count - 1].sens == Pointage.SensPointage.Sortie)
            //{
            //    MessageBox.Show("Sortie déjà pointée");
            //    return;
            //}
            BasCommon_BL.UtilisateursMgt.AddPointage(currentuser, Pointage.SensPointage.Sortie);
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void txtbxPassWord_TextChanged(object sender, EventArgs e)
        {
            CheckUser();

            timer1.Enabled = currentuser != null;
            RefreshSummary();
        }

        private void CheckUser()
        {

            AccessObject ao = BasCommon_BL.AccessMgmt.GetAccessObj(txtbxPassWord.Text);
            if (ao == null)
                currentuser = null;
            else
                currentuser = ao.Utilisateur;           
        }

        private void FrmPointage_Shown(object sender, EventArgs e)
        {
            txtbxPassWord.Focus();
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshSummary();
        }

        private void RefreshSummary()
        {
            if (currentuser != null)
            {
                if (currentuser .pointageDuJour != null)
                {
                if (currentuser.pointageDuJour.Count == 0 )
                {
                    btnIn.Visible = true;
                    btnOut.Visible = false;

                }else if (currentuser.pointageDuJour[currentuser.pointageDuJour.Count - 1].sens == Pointage.SensPointage.Entree)
                {
                    btnIn.Visible = false;
                    btnOut.Visible = true;

                }
                else
                {
                    btnIn.Visible =true;
                    btnOut.Visible = false;

                }
            }
                    lblSummary.Text = currentuser.Nom;
                if (currentuser.pointageDuJour == null)
                    currentuser.pointageDuJour = BasCommon_BL.UtilisateursMgt.getPointagesDuJour(currentuser,DateTime.Now);

                TimeSpan ts = BasCommon_BL.UtilisateursMgt.GetCumulHeuresPointage(currentuser.pointageDuJour);
                lblSummary.Text += "\n" + ts.TotalHours.ToString("00") + ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");

                switch (BasCommon_BL.UtilisateursMgt.GetStatusPointage(currentuser, DateTime.Now))
                {
                    case BasCommon_BL.UtilisateursMgt.StatusPointage.AbsenceNormal: lblSummary.ForeColor = Color.Blue; break;
                    case BasCommon_BL.UtilisateursMgt.StatusPointage.AbsenceANormal: lblSummary.ForeColor = Color.Red; break;
                    case BasCommon_BL.UtilisateursMgt.StatusPointage.PresenceNormal: lblSummary.ForeColor = Color.Green; break;
                    case BasCommon_BL.UtilisateursMgt.StatusPointage.PresenceHeureSupp: lblSummary.ForeColor = Color.Orange; break;

                }
            }
            else
                lblSummary.Text = "utilisateur non trouvé";
        }
    }
}
