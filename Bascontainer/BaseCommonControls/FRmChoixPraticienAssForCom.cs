using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEPractice_BL;
//using BASEPractice_BO;
using BasCommon_BL;
using BasCommon_BO;

namespace BaseCommonControls
{
    public partial class FRmChoixPraticienAssForCom : Form
    {

        public enum Mode
        {
            Sec,
            Prat,
            Ass
        }


        private Mode _mode = Mode.Prat;
        public Mode mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
            }
        }

        private CommTraitement  _CurrentCommClinique;
        public CommTraitement CurrentCommClinique
        {
            get
            {
                return _CurrentCommClinique;
            }
            set
            {
                _CurrentCommClinique = value;
            }
        }

        Utilisateur _Responsable;
        public Utilisateur Responsable
        {
            get
            {
                return _Responsable;
            }
            set
            {
                _Responsable = value;
            }
        }

        

        public FRmChoixPraticienAssForCom(CommTraitement com,Mode mde)
        {
            InitializeComponent();
            CurrentCommClinique = com;
            this.mode = mde;
        }

        

      
        private void FRmChoixPraticienAss_Load(object sender, EventArgs e)
        {

            if (mode != Mode.Prat)
            {
                cbxResponsable.WrapMode = true;
                cbxResponsable.WindowHeight = 160;
                Text = "Choix de l'assistante";
                label1.Text = "Assistante recevant le patient ";
            }
            else
            {

                Text = "Choix du praticien";
                label1.Text = "Praticien recevant le patient ";
            }

            List<Utilisateur> UsersInFauteuil = UtilisateursMgt.getUtilisateurInFauteuil(Fauteuilsmgt.GetWhoIam(), DateTime.Now);

            foreach (Utilisateur u in UsersInFauteuil)
            {
                if ((mode == Mode.Prat)&&((u.Fonction == "Praticien")||(u.type==Utilisateur.typeUtilisateur.Praticien)))
                    Responsable = u;
                if ((mode != Mode.Prat) && ((u.Fonction != "Praticien")&&(u.type!=Utilisateur.typeUtilisateur.Praticien)))
                    Responsable = u;
            }



            if (CurrentCommClinique != null)
                if ((CurrentCommClinique.Assistante != null)&&(mode == Mode.Ass))
                    Responsable = CurrentCommClinique.Assistante;

            if (CurrentCommClinique != null)
                if ((CurrentCommClinique.praticien != null)&&(mode == Mode.Prat))
                    Responsable = CurrentCommClinique.praticien;

            if (CurrentCommClinique != null)
                if ((CurrentCommClinique.Secretaire != null)&&(mode == Mode.Sec))
                    Responsable = CurrentCommClinique.Secretaire;
            
            
            /*
            foreach (Utilisateur u in UtilisateursMgt.utilisateurs)
            {
                if (u.Fonction == "Praticien")
                    cbxPratResp.Items.Add(u);
                else
                {
                    cbxAssResp.Items.Add(u);
                    cbxSecResp.Items.Add(u);
                }
                
            }     

            if (Assistante != null)
                cbxAssResp.SelectedItem = Assistante;

            if (Praticien != null)
                cbxPratResp.SelectedItem = Praticien;

            if (Secretaire != null)
                cbxSecResp.SelectedItem = Secretaire;


            */


            List<BaseCommonControls.FamilyValue> lst = new List<BaseCommonControls.FamilyValue>();


            foreach (Utilisateur u in UtilisateursMgt.utilisateurs)
                if (u.Actif)
                {
                    if ((mode == Mode.Prat)&&((u.Fonction == "Praticien")||(u.type == Utilisateur.typeUtilisateur.Praticien)))
                        lst.Add(new BaseCommonControls.FamilyValue("", u.LastNameShort, u));

                    if (((u.Fonction == "Assistante") || (u.type == Utilisateur.typeUtilisateur.Assistante)))
                        lst.Add(new BaseCommonControls.FamilyValue("", u.Prenom + "." + u.Nom[0].ToString(), u));
                    
                    //if ((mode != Mode.Prat) && ((u.Fonction != "Praticien")&&(u.type!=Utilisateur.typeUtilisateur.Praticien)))
                    //   // lst.Add(new BaseCommonControls.FamilyValue("" + u.Prenom.ToString(), u.NameShort, u));
                    //lst.Add(new BaseCommonControls.FamilyValue("", u.Prenom + "." + u.Nom[0].ToString(), u));

                }



            cbxResponsable.LoadFromFamilyValueList(lst);


            if (Responsable!=null) cbxResponsable.SelectedTag = Responsable;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            
            if (cbxResponsable.SelectedNode == null)
            {
                string FonctPersonne="";
                switch (mode)
                {
                    case Mode.Prat :

                        FonctPersonne = " un praticien ";
                        break;

                    case Mode.Ass :
                        FonctPersonne = " une assistante ";
                        break;
                    case Mode.Sec:
                        FonctPersonne = " une secrétaire ";
                        break;

                }
                MessageBox.Show("Veuillez choisir " + FonctPersonne );
               
            }
            else
            {

                _Responsable = (Utilisateur)cbxResponsable.SelectedNode.Tag;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cbxResponsable_Click(object sender, EventArgs e)
        {

        }
    }
}
