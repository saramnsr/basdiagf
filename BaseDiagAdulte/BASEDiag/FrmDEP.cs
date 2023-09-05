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
using BasCommon_BL;
using BasCommon_BO;

namespace BASEDiag
{
    public partial class FrmDEP : Form
    {

        

        private bool _IsPrinted = false;
        public bool IsPrinted
        {
            get
            {
                return _IsPrinted;
            }
            set
            {
                _IsPrinted = value;
            }
        }

        private string _CotationDesActes = "TO90";
        public string CotationDesActes
        {
            get
            {
                return _CotationDesActes;
            }
            set
            {
                _CotationDesActes = value;
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

        private BasCommon_BO.EntentePrealable _ententeprealable;
        public BasCommon_BO.EntentePrealable ententeprealable
        {
            get
            {
                return _ententeprealable;
            }
            set
            {
                _ententeprealable = value;
            }
        }

        private bool _IsDuplicata;
        public bool IsDuplicata
        {
            get
            {
                return _IsDuplicata;
            }
            set
            {
                _IsDuplicata = value;
            }
        }
       

        public FrmDEP(basePatient pat)
        {
            
            InitializeComponent();
            CurrentPat = pat;
        }

        private void refresh()
        {
            if (_ententeprealable == null)
            {
                _ententeprealable = new EntentePrealable();
                _ententeprealable.patient = _CurrentPat;                
            }

            if (_ententeprealable.IdModele > 0)
            {
                depCtrl1.EnableModele = false;
            }else
                if (_ententeprealable.IdDiag > 0)
                {
                    depCtrl1.EnabledDiags = false;
                }
                else
                {
                    depCtrl1.EnabledDiags = true;
                    _ententeprealable.IdDiag = -1;
                    _ententeprealable = BASEDiag_BL.DemandeEntenteMgmt.CreateEntenteFromResume(BASEDiag_BL.ResumeCliniqueMgmt.resumeCl);


                    int nbsemEnt = PropositionMgmt.NbSemestreEntames(_CurrentPat.propositions);

                    if (_CurrentPat.infoscomplementaire.NbSemestresEntame + nbsemEnt > 0)
                    {
                        _ententeprealable.typetraitement = EntentePrealable.TypeDeTraitement.Semestre;
                        _ententeprealable.Semestre = _CurrentPat.infoscomplementaire.NbSemestresEntame + nbsemEnt + 1;
                    }
                    else
                    {
                        _ententeprealable.typetraitement = EntentePrealable.TypeDeTraitement.Debut;

                    }

                    _ententeprealable.cotationDesActes = CotationDesActes;
                    _ententeprealable.ReferenceNationalOpposable = EntentePrealable.RNO.None;
                    _ententeprealable.IsDevisSigned = true;
                    _ententeprealable.PlanDeTraitement = PlanDeTraitement;
                    _ententeprealable.Praticien = CurrentPat.infoscomplementaire.PraticienResponsable;



                    #region assure
                    if (CurrentPat.Assurepar != null)
                    {
                        _ententeprealable.AdresseAssure = CurrentPat.Assurepar.MainAdresse == null ? "" : CurrentPat.Assurepar.MainAdresse.adresse.Adr1 + "\n" + CurrentPat.Assurepar.MainAdresse.adresse.Adr2 + "\n" + CurrentPat.Assurepar.MainAdresse.adresse.CodePostal + " " + CurrentPat.Assurepar.MainAdresse.adresse.Ville;
                        _ententeprealable.AdresseAssure = _ententeprealable.AdresseAssure.Replace("\n\n", "\n");
                        _ententeprealable.NomPrenomAssure = CurrentPat.Assurepar.Nom + " " + CurrentPat.Assurepar.Prenom;
                        _ententeprealable.ImmatAssure = CurrentPat.Assurepar.numSecu;
                        if (CurrentPat.MainAdresse != null)
                        {
                            _ententeprealable.AdressePatient = CurrentPat.MainAdresse.adresse.Adr1 + "\n" + CurrentPat.MainAdresse.adresse.Adr2 + "\n" + CurrentPat.MainAdresse.adresse.CodePostal + " " + CurrentPat.MainAdresse.adresse.Ville;
                            _ententeprealable.AdressePatient = _ententeprealable.AdresseAssure.Replace("\n\n", "\n");
                        }

                        if (_ententeprealable.AdressePatient == _ententeprealable.AdresseAssure) _ententeprealable.AdressePatient = "";
                    }
                }
            #endregion




            BASEDiag.DocumentsToPrint.EntentePrealableDocument doc = new BASEDiag.DocumentsToPrint.EntentePrealableDocument(_ententeprealable, false);

            depCtrl1.entente = _ententeprealable;

        }

        private void FrmResultat_Load(object sender, EventArgs e)
        {

           
            refresh( );
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            depCtrl1.cotepage = BASEDiag.Ctrls.DEPCtrl.CotePage.Recto;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            depCtrl1.cotepage = BASEDiag.Ctrls.DEPCtrl.CotePage.Verso;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FrmProperties frm = new FrmProperties();
            frm.ShowDialog();
            refresh( );
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            
            BasePrinterSetting bps = BASEDiag_BL.PrinterSettingsMgmt.getPrintSettingsByName("ImpressionDEP");


            try
            {
                depCtrl1.BuildEntente();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            BASEDiag.DocumentsToPrint.EntentePrealableDocument doc = new BASEDiag.DocumentsToPrint.EntentePrealableDocument(depCtrl1.entente, false);
            if (bps != null)
                doc.PrinterSettings = bps.settings;
            else
            {
                PrintDialog pd = new PrintDialog();
                if (pd.ShowDialog() == DialogResult.OK)
                {
                    doc.PrinterSettings = pd.PrinterSettings;
                    IsPrinted = true;
                }
                else
                    return;
            }

            doc.Print();

            _ententeprealable.DateImpression = DateTime.Now;
                    
            
            if (_ententeprealable.IdDiag == -1)
                MgmtDemandeEntente.InsertDiagEntente(_ententeprealable);

            if (_ententeprealable.IdModele == -1)
                MgmtDemandeEntente.InsertEntenteWithoutDiag(_ententeprealable);

            ResumeCliniqueMgmt.resumeCl.IdModelEntente = _ententeprealable.IdModele;
            /*
            if (((_ententeprealable.IdModele == -1) && (_ententeprealable.IdDiag == -1))||
                (_ententeprealable.IdDiag == 0))
            {
                try
                {
                    _ententeprealable.DateImpression = DateTime.Now;
                    DemandeEntenteMgmt.InsertFirstEntenteWithoutDiag(_ententeprealable);
                    DemandeEntenteMgmt.InsertDiagEntente(_ententeprealable);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message,"Erreur",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
             */

            DialogResult = DialogResult.OK;
            Close();
            
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
    }
}
