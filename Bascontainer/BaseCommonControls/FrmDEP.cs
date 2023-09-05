using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BL;
using BasCommon_BO;

namespace BaseCommonControls
{
    public partial class FrmDEP : Form
    {



        private ContactAdresse adressepatient = null;
        private ContactAdresse adresseassure = null;
        
        public bool EnableDiag
        {
            get
            {
                return depCtrl1.EnabledDiags;
            }
            set
            {
                depCtrl1.EnabledDiags = value;
            }
        }

        public bool EnableModele
        {
            get
            {
                return depCtrl1.EnableModele;
            }
            set
            {
                depCtrl1.EnableModele = value;
            }
        }
       
       

        private ActePG _actepgassociate;
        public ActePG actepgassociate
        {
            get
            {
                return _actepgassociate;
            }
            set
            {
                _actepgassociate = value;
            }
        }

        private bool _IsDEvisSigned = true;
        public bool IsDEvisSigned
        {
            get
            {
                return _IsDEvisSigned;
            }
            set
            {
                _IsDEvisSigned = value;
            }
        }



        private bool _DEPSaved = false;
        public bool DEPSaved
        {
            get
            {
                return _DEPSaved;
            }
            set
            {
                _DEPSaved = value;
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
            }
        }

        private string _PlanDeTraitement;
        public string PlanDeTraitement
        {
            get
            {
                return _PlanDeTraitement;
            }
            set
            {
                _PlanDeTraitement = value;
            }
        }
       

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

        public FrmDEP(basePatient pat,ActePG acte)
        {
            InitializeComponent();
            CurrentPat = pat;
            actepgassociate = acte;

            if ((actepgassociate!=null) && (actepgassociate.Id_DEP > 0)) 
                _entente = MgmtDemandeEntente.GetEntentePrealable(actepgassociate.Id_DEP,CurrentPat.Id);

            

        }

        public FrmDEP(basePatient pat)
        {
            InitializeComponent();
            CurrentPat = pat;



            actepgassociate = null;

            _entente = null;

        }

        public FrmDEP(EntentePrealable ep)
        {
            InitializeComponent();
            CurrentPat = ep.patient;
            


            actepgassociate = null;

            _entente = ep;

        }

        private void AutoFillComment()
        {
            if (_entente.Commentaires == "")
            {
                if ((_entente.typetraitement == EntentePrealable.TypeDeTraitement.Contention) && (_entente.Semestre == 4))
                    _entente.Commentaires = "denture définitive";
                if ((_entente.typetraitement == EntentePrealable.TypeDeTraitement.Contention) && (_entente.Contention==1))
                    _entente.Commentaires = "Deuxième molaire présente, disclusion canine, disclusion en propulsion, classe I d'angle canine, classe I d'angle molaire, RC = PIM, Plaque de Hawley + 3*3";
                
            }
        }

        private void refresh()
        {
            if (CurrentPat.contacts == null)
                baseMgmtPatient.FillContacts(CurrentPat);


            adressepatient = CurrentPat.MainAdresse==null?null:CurrentPat.MainAdresse.adresse;

            if (adresseassure == null)
            {


                if (CurrentPat.Assurepar != null)
                {
                    if (CurrentPat.Assurepar.correspondant == null)
                        CurrentPat.Assurepar.correspondant = MgmtCorrespondants.getCorrespondant(CurrentPat.Assurepar.IdCorrespondance);
                    


                    if (CurrentPat.Assurepar.correspondant.contacts==null)
                        MgmtCorrespondants.FillContacts(CurrentPat.Assurepar.correspondant);


                    adresseassure = CurrentPat.Assurepar.correspondant.MainAdresse == null ? null : CurrentPat.Assurepar.correspondant.MainAdresse.adresse;
                }
                else
                    adresseassure = CurrentPat.MainAdresse == null ? null : CurrentPat.MainAdresse.adresse;
            }





            bool isnew = false;
            if ((entente == null) || (entente.IdModele < 1))
            {
                if (entente == null) entente = new EntentePrealable();
                entente.patient = CurrentPat;
                entente.Profession = CurrentPat.Profession;
                entente.Praticien = CurrentPat.infoscomplementaire.PraticienResponsable;
                isnew = true;

            }
            else
            {
                if (entente.IdDiag < 0)
                {
                    MgmtDemandeEntente.FillDiagWithoutEnt( entente);
                }
            }

                #region assure
            if (CurrentPat.Assurepar != null)
            {
                //entente.AdresseAssure = CurrentPat.Assurepar.Adresse1Home + "\n" + CurrentPat.Assurepar.Adresse2Home + "\n" + CurrentPat.Assurepar.CodePostalHome + " " + CurrentPat.Assurepar.VilleHome;
                entente.AdresseAssure = (adresseassure == null) ? "" : (adresseassure.Adr1 + "\n" + adresseassure.Adr2 + "\n" + adresseassure.CodePostal + " " + adresseassure.Ville);
                entente.AdresseAssure = entente.AdresseAssure.Replace("\n\n", "\n");
                entente.NomPrenomAssure = CurrentPat.Assurepar.correspondant.Nom + " " + CurrentPat.Assurepar.correspondant.Prenom;
                entente.ImmatAssure = CurrentPat.Assurepar.correspondant.numSecu;
                entente.AdressePatient = (adressepatient == null) ? "" : (adressepatient.Adr1 + "\n" + adressepatient.Adr2 + "\n" + adressepatient.CodePostal + " " + adressepatient.Ville);
                //entente.AdressePatient = entente.AdresmessinaseAssure.Replace("\n\n", "\n");
                if ((adressepatient == null) || ((adresseassure!=null) &&(adressepatient.Ville == adresseassure.Ville) &&
                    (adressepatient.CodePostal == adresseassure.CodePostal)&&
                    (adressepatient.Adr1 == adresseassure.Adr1)&&
                    (adressepatient.Adr2 == adresseassure.Adr2)
                    )) entente.AdressePatient = "";
            }
            else
            {
                //entente.AdresseAssure = CurrentPat.Assurepar.Adresse1Home + "\n" + CurrentPat.Assurepar.Adresse2Home + "\n" + CurrentPat.Assurepar.CodePostalHome + " " + CurrentPat.Assurepar.VilleHome;
                entente.AdresseAssure = (adresseassure == null) ? "" : (adresseassure.Adr1 + "\n" + adresseassure.Adr2 + "\n" + adresseassure.CodePostal + " " + adresseassure.Ville);
                entente.AdresseAssure = entente.AdresseAssure.Replace("\n\n", "\n");
                entente.NomPrenomAssure = CurrentPat.Nom + " " + CurrentPat.Prenom;
                entente.ImmatAssure = CurrentPat.NumSecu;
                entente.AdressePatient = "";
            }
                #endregion




            entente.cotationDesActes = actepgassociate==null?"TO90":actepgassociate.Template.DisplayCodeNVal;
            entente.IsDevisSigned = IsDEvisSigned;
            DateTime? dte = PropositionMgmt.GetDebutREprise(CurrentPat.propositions);
            if (dte==null)dte = CurrentPat.infoscomplementaire.DateDebutTraitement;
                entente.DateDebutTraitement = dte==null?DateTime.Now:dte.Value;

            entente.patient = CurrentPat;

            
            
             

            //BASEPractice.documentsToPrint.EntentePrealableDocument doc = new BASEPractice.documentsToPrint.EntentePrealableDocument(entente, false);

            //printPreviewControl1.Document = doc;

           // printPreviewControl1.InvalidatePreview();


            if ((actepgassociate != null)&&(isnew))
            {
               
                if (actepgassociate.NumSemestre > -1)
                {
                    entente.typetraitement = EntentePrealable.TypeDeTraitement.Semestre;
                    entente.Semestre = actepgassociate.NumSemestre + CurrentPat.infoscomplementaire.NbSemestresEntame;
                }
                else
                    if (actepgassociate.NumContention > -1)
                    {
                        entente.typetraitement = EntentePrealable.TypeDeTraitement.Contention;
                        entente.Contention = actepgassociate.NumContention;
                    }
                    else
                        if (actepgassociate.Template.Nom == "SURV")
                        {
                            entente.typetraitement = EntentePrealable.TypeDeTraitement.Surveillance;
                        }
                        else
                            if (CodesTraitement.IsContention1(actepgassociate.Template.Nom))
                            {
                                entente.typetraitement = EntentePrealable.TypeDeTraitement.Contention;
                                entente.Contention = 1;
                            }
                            else
                                if (CodesTraitement.IsContention2(actepgassociate.Template.Nom))
                                {
                                    entente.typetraitement = EntentePrealable.TypeDeTraitement.Contention;
                                    entente.Contention = 2;
                                }
                                else
                                {
                                    entente.typetraitement = EntentePrealable.TypeDeTraitement.Autre;
                                    entente.Autre = actepgassociate.Libelle;
                                }
                 
                IsDEvisSigned = true;
            }

            depCtrl1.entente = entente;

            if ((!isnew) || ((actepgassociate!=null) &&(actepgassociate.DEPAssocier != null)))
            {
                //MessageBox.Show("Une demande d'entente à déja été créé " + "\nLa demande ne sera pas éditable", "DEP existante");
               // depCtrl1.entente = actepgassociate.DEPAssocier;
                depCtrl1.Disabled = true;
            }
            AutoFillComment();
        }

        private void FrmResultat_Load(object sender, EventArgs e)
        {   
            refresh( );
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            depCtrl1.cotepage = Ctrls.DEPCtrl.CotePage.Verso;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            depCtrl1.cotepage = Ctrls.DEPCtrl.CotePage.Recto;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*
            FrmProperties frm = new FrmProperties();
            frm.ShowDialog();
            refresh( );
             * */
        }



        private void btnPrint_Click(object sender, EventArgs e)
        {

            

            depCtrl1.BuildEntente();
            entente = depCtrl1.entente;

            if ((entente.Praticien!=null) && (CurrentPat.infoscomplementaire.PraticienResponsable!=null) && (entente.Praticien.Id != CurrentPat.infoscomplementaire.PraticienResponsable.Id))
            {
                if (MessageBox.Show("Attention cette entente n'est pas au nom du praticien responsable\nSouhaitez-vous continuer ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            PrintDialog pd = new PrintDialog();
            pd.Document = new documentsToPrint.EntentePrealableDocument(entente, false);

            ((documentsToPrint.EntentePrealableDocument)pd.Document).PrintBothPages = chkbxprintboth.Checked;
            ((documentsToPrint.EntentePrealableDocument)pd.Document).isrecto = (depCtrl1.cotepage == Ctrls.DEPCtrl.CotePage.Verso);
            
            
            if (pd.ShowDialog() == DialogResult.OK)
            {
                pd.Document.PrinterSettings = pd.PrinterSettings;
                pd.Document.Print();

                SaveNClose();
            }
            
        }


        void btnSave_Click(object sender, System.EventArgs e)
        {

            depCtrl1.BuildEntente();
            entente = depCtrl1.entente;

            if ((entente.Praticien != null) && (CurrentPat.infoscomplementaire.PraticienResponsable != null) && (entente.Praticien.Id != CurrentPat.infoscomplementaire.PraticienResponsable.Id))
            {
                if (MessageBox.Show("Attention cette entente n'est pas au nom du praticien responsable\nSouhaitez-vous continuer ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            SaveNClose();
        }

        private void SaveNClose()
        {
            DEPSaved = true;
            SaveDEP();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void SaveDEP()
        {
            entente.DateImpression = DateTime.Now;

            if ((depCtrl1.IsDiagChanged) || (entente.IdDiag == -1))
            {
                entente.IdDiag = -1;
                MgmtDemandeEntente.InsertDiagEntente(entente);
            }

            if (entente.IdModele == -1)
                MgmtDemandeEntente.InsertEntenteWithoutDiag(entente);
            else
                MgmtDemandeEntente.UpdateEntenteWithoutDiag(entente);

            if (actepgassociate != null)
            {
                ActesPGMgmt.RemoveDEPReference(entente.IdModele);
                ActesPGMgmt.AddDEPReference(entente, actepgassociate);
                actepgassociate.Id_DEP = entente.IdModele;
            }
        }

        private void dtpProposition_ValueChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void dtpDebutTraitmnt_ValueChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void txtbxFacteurFonctionnel_Leave(object sender, EventArgs e)
        {
            refresh();
        }

        private void cbxTypeDeTraitement_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            depCtrl1.BuildEntente();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {


        }

        private void depCtrl1_Load(object sender, EventArgs e)
        {

        }
    }
}
