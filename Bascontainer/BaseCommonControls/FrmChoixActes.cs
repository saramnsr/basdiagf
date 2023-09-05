using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BO;
using BasCommon_BL;
using BaseCommonControls;
using BasCommon_BO.ElementsEnBouche.BO;
using Microsoft.Office.Interop.Word;



namespace BaseCommonControls
{
    public delegate void delegateCommClinique();
    public partial class FrmChoixActes : Form
    {

        public TypeFamilleTraitement _typeFmTrt { get; set; }

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

        private Devis_TK _DevisTraitement;
        public Devis_TK DevisTraitement
        {
            get
            {
                return _DevisTraitement;
            }
            set
            {
                _DevisTraitement = value;
            }
        }

        private List<CommTraitement> _Value;
        public List<CommTraitement> Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        private List<TempEcheanceDefinition> _tmpTED = new List<TempEcheanceDefinition>();
        public List<TempEcheanceDefinition> tmpTED
        {
            get
            {
                return _tmpTED;
            }
            set
            {
                _tmpTED = value;
            }
        }
        private bool _isFullTime;
        public bool isFullTime
        {
            get
            {
                return _isFullTime;
            }
            set
            {
                _isFullTime = value;
            }
        }

        double prix_total = 0;
        private int _id_traitement;
        public NewTraitement _Traitement;
        private Devis_TK _Devis;
        CommTraitement com = new CommTraitement();
        Proposition p = new Proposition();
        public delegateCommClinique CommCliniquerefresh;
        private Boolean _visualisation = false;
        int _DureeSemestres;
        private ActePG ap = new ActePG();
        private Boolean _isNotDevis = false;
        Boolean _ChanngeItem = false;
        bool isTraitement = false;
        public bool isfulltime;


        public FrmChoixActes(
            int id_traitement, string LibTraitement, ref Devis_TK Devis_Traitement,
            Boolean visualisation = false, int DureeSemestres = 0,
            bool showRepart = false, bool isNotDevis = false,
            basePatient pat = null)
        {
            InitializeComponent();
            _DevisTraitement = Devis_Traitement;
            this.Text = "Scénario: " + LibTraitement;
            _id_traitement = id_traitement;
            _visualisation = visualisation;

            _isNotDevis = isNotDevis;
            isTraitement = DevisTraitement == null ? true : false;
            if (!isTraitement)
                colDent.Visible = true;
            if (visualisation)
            {

                BTN_ADD.Visible = false;
                BTN_DELETE.Visible = false;
                btnOk.Visible = false;
                btnRistourneGlobal.Visible = false;
            }

            if ((System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + BasCommon_DAL.DAC.prefix] == "FR") && _DevisTraitement != null && _DevisTraitement.Traitement.TypeScenario != NewTraitement.typeScenario.Prothése_CMUC)
            {
                colRepartition.Visible = true;
                //  btnOk.Visible = true;
                //  button1.Visible = true;
            }
            _DureeSemestres = DureeSemestres;
            CurrentPatient = pat;

        }

        private void verifierChangement(Boolean ChangeItems, CommTraitement com)
        {
            _ChanngeItem = ChangeItems;
            if (ChangeItems == true)
                com.echeancestemp.Clear();

        }


        private void FrmChoixActes_KeyDown(object sender, KeyEventArgs e)
        {

        }




        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;


            OnCellSelectedEventArgs args = new OnCellSelectedEventArgs(((DataGridView)sender).Columns[e.ColumnIndex], ((CommTraitement)((DataGridView)sender).Rows[e.RowIndex].Tag), e);
            com = args.comment;

            if (e.ColumnIndex != 1 && com.desactive == true)
            {
                return;
            }
            Boolean ColTouche = false;

            switch (dataGridView1.Columns[e.ColumnIndex].Name)
            {
                case "colPrat":
                    if (!(_visualisation))
                    {

                        FRmChoixPraticienAssForCom frmChoixPraticien = new FRmChoixPraticienAssForCom(com, FRmChoixPraticienAssForCom.Mode.Prat);

                        if (frmChoixPraticien.ShowDialog() == DialogResult.OK)
                        {

                            com.praticien = frmChoixPraticien.Responsable;
                            com.IdPraticien = frmChoixPraticien.Responsable.Id;

                            if (DevisTraitement == null)
                            {
                                TraitementsMgmt.UpdateActeTraitement(com);
                            }


                        }
                        ColTouche = true;
                    }
                    break;
                case "colDent":
                    if (!(isTraitement))
                    {

                        frmAddDents frm = new frmAddDents(com.dents);
                        if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            com.dents = frm.dents;
                            ((CommTraitement)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Tag).dents = com.dents;
                            ColTouche = true;
                        }
                    }
                    break;
                case "colActive":
                    if (!timer1.Enabled && _DevisTraitement != null)
                    {
                        _DevisTraitement.echeancestemp = null;

                        double? MontantAvRedDocteur = 0;
                        MontantAvRedDocteur = _DevisTraitement.MontantAvantRemise;
                        //  DataGridViewCheckBoxColumn parent = (DataGridViewCheckBoxColumn)dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[colActive.Name].OwningColumn;

                        //  com.desactive = parent.Selected;
                        if (com.desactive)
                        {
                            //   dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[colActive.Name].Value = false;
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
                            if (_DevisTraitement.MontantDocteur > 0)
                                _DevisTraitement.MontantDocteur += com.prix;
                            MontantAvRedDocteur = MontantAvRedDocteur + com.prix;
                            _DevisTraitement.Montant = _DevisTraitement.Montant + com.prix;
                            _DevisTraitement.MontantAvantRemise = MontantAvRedDocteur;
                            com.desactive = false;

                        }
                        else
                        {
                            //  dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[colActive.Name].Value = true;
                            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(175, 175, 175);
                            com.desactive = true;
                            if (_DevisTraitement.MontantDocteur > 0)
                                _DevisTraitement.MontantDocteur -= com.prix;
                            MontantAvRedDocteur = MontantAvRedDocteur - com.prix;
                            _DevisTraitement.MontantAvantRemise = MontantAvRedDocteur;
                            _DevisTraitement.Montant = _DevisTraitement.Montant + com.prix;
                        }
                        _DevisTraitement.EcheancierDocteur = 0;
                        _DevisTraitement.MontantScenario = MontantAvRedDocteur;

                        TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", MontantAvRedDocteur);
                        TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.Montant);
                        // TxtPrixTotal_AvantRemise.Text = string.Format("{0:f2}", _Traitement.MontantAvantRemise);
                        timer1.Enabled = false;
                        timer1.Enabled = true;
                        ColTouche = true;
                    }


                    break;

                case "colAss":
                    if (!(_visualisation))
                    {
                        FRmChoixPraticienAssForCom frmchoixAssistant = new FRmChoixPraticienAssForCom(com, FRmChoixPraticienAssForCom.Mode.Ass);

                        if (frmchoixAssistant.ShowDialog() == DialogResult.OK)
                        {

                            com.Assistante = frmchoixAssistant.Responsable;
                            if (DevisTraitement == null)
                            {
                                TraitementsMgmt.UpdateActeTraitement(com);
                            }

                        }
                        ColTouche = true;
                    }
                    break;
                case "colSec":
                    if (!(_visualisation))
                    {
                        FRmChoixPraticienAssForCom frmchoixSecretaire = new FRmChoixPraticienAssForCom(com, FRmChoixPraticienAssForCom.Mode.Sec);

                        if (frmchoixSecretaire.ShowDialog() == DialogResult.OK)
                        {

                            com.Secretaire = frmchoixSecretaire.Responsable;
                            if (DevisTraitement == null)
                            {
                                TraitementsMgmt.UpdateActeTraitement(com);
                            }

                        }
                        ColTouche = true;
                    }
                    break;
                case "Autre":
                    if (!(_visualisation))
                    {
                        if (com.AutrePersonnes == null)
                            com.AutrePersonnes = TraitementsMgmt.GetTraitementAutrePersonne(com);
                        FrmChoixAutrePersonne frmchoixAutrePersonne = new FrmChoixAutrePersonne(com);

                        if (frmchoixAutrePersonne.ShowDialog() == DialogResult.OK)
                        {

                            com.AutrePersonnes = frmchoixAutrePersonne.values;
                            if (DevisTraitement == null)
                            {
                                TraitementsMgmt.SaveAutrePersonne(com);
                            }

                        }
                        ColTouche = true;
                    }
                    break;
                case "Date":
                    FrmChoisxDate frmCD = new FrmChoisxDate();
                    if (frmCD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (frmCD.checkDate)
                        {
                            if (frmCD.date < DateTime.Now)
                                MessageBox.Show("La date ne peut pas être passée");
                            else
                            {
                                // condition à rajouter içi pour le click de modif date
                                ((CommTraitement)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Tag).DatePrevisionnnelle = frmCD.date;

                                ((CommTraitement)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Tag).NbJours = (int)(frmCD.date - DateTime.Now.Date).TotalDays;
                            }
                        }
                        else
                        {
                            // ça c'est autre chose ca ne pourra plus arriver içi dans notre cas
                            ((CommTraitement)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Tag).DatePrevisionnnelle = DevisTraitement.DatePrevisionnelDeDebutTraitement.AddDays(frmCD.nbJours);
                            ((CommTraitement)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Tag).NbJours = frmCD.nbJours;
                        }
                    }
                    dataGridView1.Invalidate();
                    // Initdgv(DevisTraitement.actesTraitement);
                    break;
                case "Acte":
                    if (!(_visualisation))
                    {
                        bool isCreated = false;
                        if (_DevisTraitement != null)
                            isCreated = true;
                        FrmActeTraitement frm = new FrmActeTraitement(com, 0, "", false, isCreated);
                        // FrmNewActesTraitement frm = new FrmNewActesTraitement(com, "", 0, isTraitement);
                        if (frm.ShowDialog() == DialogResult.OK)
                        {



                            if (com.etat == CommTraitement.EtatCommentaire.EnCours)
                                com.etat = CommTraitement.EtatCommentaire.Termine;
                            if (frm.Value != null)
                            {
                                com.Acte = frm.Value;
                                verifierChangement(frm.ChangeItems, com);
                                if (DevisTraitement == null)
                                {
                                    TraitementsMgmt.UpdateActeTraitement(com);
                                    TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _Traitement.MontantAvantRemise);
                                    TxtPrixTotal.Text = string.Format("{0:f2}", _Traitement.Montant);
                                }
                                else
                                {
                                    MgmtDevis.SavePrixCom(com);
                                    TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _DevisTraitement.MontantAvantRemise);
                                    TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.Montant);
                                }

                            }

                        }
                        ColTouche = true;
                    }
                    break;
                case "ActeSupp":
                    if (!(_visualisation))
                    {
                        if (com.ActesSupp == null)
                            com.ActesSupp = TraitementsMgmt.GetCommActeSupTraitements(com);
                        bool isCreated = false;
                        if (_DevisTraitement != null)
                            isCreated = true;
                        FrmActeTraitement frmActeSupp = new FrmActeTraitement(com, 1, "", false, isCreated);
                        if (frmActeSupp.ShowDialog() == DialogResult.OK)
                        {
                            if (com.etat == CommTraitement.EtatCommentaire.EnCours)
                                com.etat = CommTraitement.EtatCommentaire.Termine;


                            com.ActesSupp = frmActeSupp.comms;
                            verifierChangement(frmActeSupp.ChangeItems, com);
                            if (DevisTraitement == null)
                            {
                                TraitementsMgmt.SaveActesSupp(com);
                                TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _Traitement.MontantAvantRemise);
                                TxtPrixTotal.Text = string.Format("{0:f2}", _Traitement.Montant);
                            }
                            if (_DevisTraitement != null)
                            {
                                TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _DevisTraitement.MontantAvantRemise);
                                TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.Montant);
                            }

                        }
                        ColTouche = true;
                    }
                    break;
                //case "Actesupp1":
                //    //if (!(_visualisation))
                //    //{
                //    //    if (com.ActesSupp1 == null)
                //    //      //  com.Radios = TraitementsMgmt.GetCommActeSupTraitements(com, "R");
                //    //        com.ActesSupp1 = TraitementsMgmt.GetCommActeSupTraitements(com, "S");
                //    //    bool isCreated = false;
                //    //    if (_DevisTraitement != null)
                //    //        isCreated = true;
                //    //    FrmActeTraitement frmActeSupp1 = new FrmActeTraitement(com, 1, "ActesSupp1", false, isCreated);
                //    //    if (frmActeSupp1.ShowDialog() == DialogResult.OK)
                //    //    {
                //    //        if (com.etat == CommTraitement.EtatCommentaire.EnCours)
                //    //            com.etat = CommTraitement.EtatCommentaire.Termine;


                //    //        com.ActesSupp1 = frmActeSupp1.comms;
                //    //        verifierChangement(frmActeSupp1.ChangeItems, com);
                //    //        if (DevisTraitement == null)
                //    //        {
                //    //            TraitementsMgmt.SaveActesSupp(com, "S");
                //    //            TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _Traitement.MontantAvantRemise);
                //    //            TxtPrixTotal.Text = string.Format("{0:f2}", _Traitement.Montant);
                //    //        }
                //    //        if (_DevisTraitement != null)
                //    //        {
                //    //            TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _DevisTraitement.MontantAvantRemise);
                //    //            TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.Montant);
                //    //        }

                //    //    }
                //    //    ColTouche = true;
                //    //}
                //   break;
                case "Radios":
                    if (!(_visualisation))
                    {
                        if (com.Radios == null)
                            com.Radios = TraitementsMgmt.GetCommActeSupTraitements(com, "R");
                        bool isCreated = false;
                        if (DevisTraitement != null)
                            isCreated = true;
                        FrmActeTraitement frmRadio = new FrmActeTraitement(com, 1, "RADIO", false, isCreated);
                        //FrmNewActesTraitement frmRadio = new FrmNewActesTraitement(com, "radio", 2, isTraitement);
                        if (frmRadio.ShowDialog() == DialogResult.OK)
                        {
                            com.Radios = frmRadio.comms;
                            verifierChangement(frmRadio.ChangeItems, com);
                            if (DevisTraitement == null)
                            {
                                TraitementsMgmt.SaveActesSupp(com, "R");
                                TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _Traitement.MontantAvantRemise);
                                TxtPrixTotal.Text = string.Format("{0:f2}", _Traitement.Montant);
                            }
                            else
                            {
                                TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _DevisTraitement.MontantAvantRemise);
                                TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.Montant);
                            }

                        }

                        ColTouche = true;
                    }
                    break;
                case "Photos":
                    if (!(_visualisation))
                    {

                        if (com.photos == null)
                            com.photos = TraitementsMgmt.GetCommActeSupTraitements(com, "P");

                        bool isCreated = false;
                        if (DevisTraitement != null)
                            isCreated = true;

                        FrmActeTraitement frmPhotos = new FrmActeTraitement(com, 1, "Photos", false, isCreated);
                        // FrmNewActesTraitement frmPhotos = new FrmNewActesTraitement(com, "Photos", 3, isTraitement);
                        if (frmPhotos.ShowDialog() == DialogResult.OK)
                        {
                            verifierChangement(frmPhotos.ChangeItems, com);
                            com.photos = frmPhotos.comms;
                            if (DevisTraitement == null)
                            {
                                TraitementsMgmt.SaveActesSupp(com, "P");
                                TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _Traitement.MontantAvantRemise);
                                TxtPrixTotal.Text = string.Format("{0:f2}", _Traitement.Montant);
                            }
                            else
                            {
                                TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _DevisTraitement.MontantAvantRemise);
                                TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.Montant);
                            }

                        }

                        ColTouche = true;
                    }
                    break;
                case "Mat":
                    if (!(_visualisation))
                    {
                        if (com.Materiels == null)
                            com.Materiels = TraitementsMgmt.GetCommMaterielsTraitements(com);

                        bool isCreated = false;
                        if (DevisTraitement != null)
                            isCreated = true;
                        //FrmNewActesTraitement frmMateriel = new FrmNewActesTraitement(com, "", 4, isTraitement);
                        FrmChoiceMateriel frmMateriel = new FrmChoiceMateriel(com, 1, isCreated);
                        if (frmMateriel.ShowDialog() == DialogResult.OK)
                        {
                            verifierChangement(frmMateriel.ChangeItems, com);
                            com.Materiels = frmMateriel.comms;
                            foreach (CommMateriel cm in com.Materiels)
                                cm.Famille = MaterielsMgmt.GetFamilleMaterielByIdMateriel(cm.idMateriel);
                            if (DevisTraitement == null)
                            {
                                TraitementsMgmt.SaveMateriels(com);
                                TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _Traitement.MontantAvantRemise);
                                TxtPrixTotal.Text = string.Format("{0:f2}", _Traitement.Montant);
                            }
                            else
                            {
                                TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _DevisTraitement.MontantAvantRemise);
                                TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.Montant);
                            }
                        }
                        ColTouche = true;
                    }
                    break;
                //case 17 - 13 - 14 - 15 - 16:
                //    //   FrmSchemaDentaire frmSchemaDentaire = new FrmSchemaDentaire( com.patient ,com.patient.AppareilsEnBouche ,com.patient.ElementsEnBouche );

                //    /*   if (com.etat == CommClinique.EtatCommentaire.EnCours)
                //           com.etat = CommClinique.EtatCommentaire.Termine;
                //       if (com.Id == -1)
                //           MgmtCommentairesFaitAFaire.AddCommentaire(com);

                //       frm.CurrentCom = com;

                //       if (frm.ShowDialog() == DialogResult.OK)
                //       {
                //           foreach (IElementDent elem in frm.Value)
                //           {

                //               ((ElementDent)elem).patient = CurrentPatient;
                //               EnBoucheMgmt.Save(elem);

                //           }

                //           foreach (ElementAppareil elem in frm.Appareils)
                //           {

                //               elem.patient = CurrentPatient;
                //               EnBoucheMgmt.Save(elem);

                //           }
                //           NetWorkThreadClass.SendMessage(new NetWorkMsg() { ActionCODE = "ENBOUCHE", Parametres = "", IdPatient = CurrentPatient.Id });

                //           //ReinitDisplay(RefreshOnlyOne.TheBigControl);
                //           theBigCtrl1.RefreshRow(com, args.eventarg.RowIndex);*/
                //    break;

                case "Réduction":

                    FrmDetailLigneTraitement frmristourne;
                    if (_DevisTraitement == null)
                    {
                        frmristourne = new FrmDetailLigneTraitement(_Traitement.CommTraitement, com, false, false);
                    }
                    else
                    {
                        com.MontantLigne = com.MontantLigne;
                        com.MontantLigneAvantRemise = com.MontantLigneAvantRemise;

                        frmristourne = new FrmDetailLigneTraitement(_DevisTraitement.actesTraitement, com, _visualisation, false, Convert.ToDouble(_DevisTraitement.MontantDocteur), Convert.ToDouble(_DevisTraitement.Montant), Convert.ToDouble(_DevisTraitement.MontantAvantRemise));
                    }

                    if (frmristourne.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        verifierChangement(frmristourne.ChangeItems, com);
                        if (_DevisTraitement == null)
                        {
                            TraitementsMgmt.SavePrixCom(com);

                            TxtPrixTotal.Text = string.Format("{0:f2}", _Traitement.Montant);
                            TxtPrixTotal_AvantRemise.Text = string.Format("{0:f2}", _Traitement.MontantAvantRemise);
                            //TxtPrixTotal_AvantRemise.Visible = TxtPrixTotal.Text != TxtPrixTotal_AvantRemise.Text;

                        }
                        else
                        {
                            if (_id_traitement == -1)
                            {
                                MgmtDevis.SavePrixCom(com);


                                TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.Montant);
                                TxtPrixTotal_AvantRemise.Text = string.Format("{0:f2}", _DevisTraitement.MontantAvantRemise);
                                //TxtPrixTotal_AvantRemise.Visible = TxtPrixTotal.Text != TxtPrixTotal_AvantRemise.Text;


                            }
                        }


                        ColTouche = true;
                    }

                    break;

                case "colRepartition":
                    if (_DevisTraitement.Traitement.TypeScenario != NewTraitement.typeScenario.Prothése_CMUC)
                    {
                        FrmDetailLigneTraitement frm = new FrmDetailLigneTraitement(_DevisTraitement.actesTraitement, com, _visualisation, false, Convert.ToDouble(_DevisTraitement.MontantDocteur), Convert.ToDouble(_DevisTraitement.Montant), Convert.ToDouble(_DevisTraitement.MontantAvantRemise), _DevisTraitement.Traitement.TypeScenario, true);


                        if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            com.echeancestemp.Clear();

                            double partsecu = 0;
                            double partmutuelle = 0;
                            double parPatient = 0;
                            TraitementsMgmt.getMontantEcheToulon(com, ref partsecu, ref partmutuelle, ref parPatient);

                            CreateEcheanceToulon(_DevisTraitement.DatePrevisionnelDeDebutTraitement.AddDays(com.NbJours).AddMonths(6), partsecu, partmutuelle, parPatient, ap, com.Acte.acte_libelle, com.Acte.acte_libelle);
                            com.echeancestemp.AddRange(tmpTED);


                        }

                    }
                    break;

                case "Echéance":
                    //if (!(_visualisation))
                    // {

                    int CtrSemestre = 0;
                    DateTime vDateEcheance = DateTime.Now;
                    if (com.semestres.Count == 0)
                    {
                        for (int i = 1; i <= _DureeSemestres; i++)
                        {
                            Semestre s = new Semestre();
                            s.CodeSemestre = com.Acte.acte_libelle;
                            s.DateDebut = vDateEcheance.AddMonths(6);
                            s.DateFin = s.DateDebut.AddMonths(6);
                            vDateEcheance = vDateEcheance.AddMonths(6);
                            s.Montant_Honoraire = TraitementsMgmt.GetPrixCom(com);
                            s.Montant_AvantRemise = TraitementsMgmt.GetPrixComAvantRemise(com);
                            //s.traitementSecu = tmplte;
                            //s.Parent = t;
                            CtrSemestre++;
                            s.NumSemestre = CtrSemestre;


                            com.semestres.Add(s);
                        }
                    }

                    Proposition p = new Proposition(_DevisTraitement);


                    foreach (CommTraitement ctrt in _DevisTraitement.actesTraitement)
                    {
                        if (ctrt.desactive) continue;
                        if (ctrt.echeancestemp.Count == 0)
                        {
                            ap = new ActePG();
                            TempEcheanceDefinition ted = new TempEcheanceDefinition();
                            if ((System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + BasCommon_DAL.DAC.prefix] == "FR") && _DevisTraitement.Traitement.TypeScenario != NewTraitement.typeScenario.Prothése_CMUC)
                            {
                                double partsecu = 0;
                                double partmutuelle = 0;
                                double parPatient = 0;


                                TraitementsMgmt.getMontantEcheToulon(ctrt, ref partsecu, ref partmutuelle, ref parPatient);
                                CreateEcheanceToulon(_DevisTraitement.DatePrevisionnelDeDebutTraitement.AddDays(ctrt.NbJours).AddMonths(6), partsecu, partmutuelle, parPatient, ap, ctrt.Acte.acte_libelle, ctrt.Acte.acte_libelle);
                                ctrt.echeancestemp.AddRange(tmpTED);


                            }
                            else
                            {
                                ted.DAteEcheance = _DevisTraitement.DatePrevisionnelDeDebutTraitement.AddDays(ctrt.NbJours);
                                //      ted.DAteEcheance = ctrt.DatePrevisionnnelle.Value;
                                ted.Montant = TraitementsMgmt.GetPrixCom(ctrt);
                                ted.Libelle = ctrt.Acte.acte_libelle;
                                ted.acte = ap;
                                ted.acte.Libelle = ctrt.Acte.acte_libelle;
                                //ted.acte = com.acte;
                                ted.AlreadyPayed = false;
                                ted.payeur = Echeance.typepayeur.patient;

                                ctrt.echeancestemp.Add(ted);
                            }
                        }
                    }

                    FrmFinancement frmP = new FrmFinancement(p, p.patient, com.echeancestemp, com.Id, _visualisation, 0, _DevisTraitement.Traitement.TypeScenario);

                    if (frmP.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        com.echeancestemp.Clear();
                        BasCommon_BL.MgmtDevis.DeleteTempEcheance(p);
                        if (frmP.Montants.Count > 1)
                        {

                            TxtPrixTotal_AvantRemise.Text = string.Format("{0:f2}", _DevisTraitement.MontantAvantRemise);
                            TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.Montant);
                        }
                        //TxtPrixTotal_AvantRemise.Visible = _DevisTraitement.Montant != _DevisTraitement.MontantAvantRemise;

                        foreach (BaseTempEcheanceDefinition ted in frmP.Montants)
                        {
                            if (ted.acte != null)
                            {
                                TempEcheanceDefinition tted = new TempEcheanceDefinition()
                                {
                                    acte = ted.acte,
                                    AlreadyPayed = ted.AlreadyPayed,
                                    CanRecalculate = ted.CanRecalculate,
                                    DAteEcheance = ted.DAteEcheance,
                                    Id = ted.Id,
                                    IdSemestre = ted.acte.Semestre,
                                    Libelle = ted.Libelle,
                                    Montant = ted.Montant,
                                    ParPrelevement = ted.ParPrelevement,
                                    ParVirement = ted.ParVirement,
                                    payeur = ted.payeur,


                                };
                                com.echeancestemp.Add(tted);
                            }
                        }
                        DevisTraitement.EcheancierDocteur = 1;

                    }



                    //}
                    break;
            }
            if (ColTouche)
                if (_DevisTraitement == null)
                {
                    Initdgv(_Traitement.CommTraitement, false);

                }
                else
                {
                    _DevisTraitement.MontantScenario = _DevisTraitement.Montant;
                    BuildPnlDevis();
                    Initdgv(_DevisTraitement.actesTraitement, false);
                }
            // RefreshRow(com, e.RowIndex);



        }

        private void RefreshRow(CommTraitement c, int row)
        {
            List<IElementAppareil> AppH = new List<IElementAppareil>();
            List<IElementDent> AccH = new List<IElementDent>();
            List<IElementAppareil> AppB = new List<IElementAppareil>();
            List<IElementDent> AccB = new List<IElementDent>();
            if (c.Acte == null)
                c.Acte = new ActeTraitement();
            // il faut espionner c pour verfier si c.ActeSuup est != de null

            /*     var Acte= c.Acte;
                 if (this._Traitement.id_famille != -1)
                     this._Traitement.famille_Traitement = TraitementsMgmt.getFamilleTraitementById(_Traitement.id_famille);

                 if (_Traitement.famille_Traitement != null && _Traitement.famille_Traitement.typeFamilleTraitement == TypeFamilleTraitement.GroupementActe2 && row == 0) 
                 {
                     c.ActesSupp = null;
                     c.ActesSupp = TraitementsMgmt.GetCommActeSupTraitements(c);
                     Acte = null;
                 }*/


            object[] obj = new object[] 
            {
                c.modecreation == CommTraitement.ModeCreation.Manuel? imgsStatus.Images[1]:imgsStatus.Images[0],
                  c.desactive,
                c.praticien,
                c.Assistante,
                c.Secretaire,
                c.AutrePersonnes,                    
                c.DatePrevisionnnelle,
               c.dents,
                c.Acte.id_acte == -1 ? null : c.Acte,
                    c.Acte.id_acte == -1 ? null : c.Acte,
                // (c.Appointement==null?com.Acte:com.Appointement.acte),
                // (c.Appointement==null?com.Acte:com.Appointement.acte),
                // (com.Appointement==null?com.Acte:com.Appointement.acte),
                c.ActesSupp,
                c.Radios,
                c.photos,
                c.Materiels,
                 c.prix.ToString ("00.00"),
                 "%",
                 "Répartir",
                 "Echéancier"






            };
            if (dataGridView1 != null)
            {
                if (c.desactive)
                    dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(175, 175, 175);
                else
                    dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.Empty;
                dataGridView1.Rows[row].SetValues(obj);
                dataGridView1.Rows[row].Tag = c;
            }
            //dataGridView1.Rows[row].SetValues(obj);
        }


        public void Initdgv(List<CommTraitement> com, Boolean GetColumns = true)
        {

            dataGridView1.Rows.Clear();

            dataGridView1.Refresh();


            prix_total = 0;


            double prix_ligne;

            foreach (CommTraitement trt in com)
            {
                prix_ligne = 0;
                if (GetColumns)
                {
                    //Gestion des scénarios
                    if (_id_traitement != -1)
                    {
                        // verfier le resultat du GetCommActeSupTraitements by Id
                        trt.ActesSupp = TraitementsMgmt.GetCommActeSupTraitements(trt);
                        trt.Radios = TraitementsMgmt.GetCommActeSupTraitements(trt, "R");
                        trt.photos = TraitementsMgmt.GetCommActeSupTraitements(trt, "P");
                        trt.Materiels = TraitementsMgmt.GetCommMaterielsTraitements(trt);
                        trt.AutrePersonnes = TraitementsMgmt.GetTraitementAutrePersonne(trt);
                        trt.echeancestemp = new List<TempEcheanceDefinition>();

                    }

                    if (_id_traitement == -1)
                    {
                        //if (_id_traitement != -1 && DevisTraitement != null)
                        //{
                        trt.ActesSupp = MgmtDevis.GetCommActeSupDevis(trt);
                        trt.Radios = MgmtDevis.GetCommActeSupDevis(trt, "R");
                        trt.photos = MgmtDevis.GetCommActeSupDevis(trt, "P");
                        trt.Materiels = MgmtDevis.GetCommMaterielsDevis(trt);
                        trt.AutrePersonnes = MgmtDevis.GetDevisAutrePersonne(trt);
                        trt.echeancestemp = MgmtDevis.get_tempecheances_TK(trt);
                        if (trt.echeancestemp.Count == 0)
                        {

                            TempEcheanceDefinition ted = new TempEcheanceDefinition();
                            ted.DAteEcheance = trt.devis.DatePrevisionnelDeDebutTraitement.AddMonths(6);
                            ted.Montant = trt.prix;
                            ted.Libelle = trt.Acte.acte_libelle;
                            ted.acte = ap;
                            ted.acte.Libelle = trt.Acte.acte_libelle;
                            //ted.acte = com.acte;
                            ted.AlreadyPayed = false;
                            ted.payeur = Echeance.typepayeur.patient;
                            trt.echeancestemp.Add(ted);
                        }
                        else
                        {
                            foreach (BaseTempEcheanceDefinition bted in trt.echeancestemp)
                            {
                                bted.acte = ap;
                                bted.acte.Libelle = trt.Acte.acte_libelle;
                            }
                        }
                    }
                    else
                    {


                        if (DevisTraitement != null)
                        {
                            if (trt.ActesSupp == null)
                                trt.ActesSupp = new List<CommActesTraitement>();


                            if (trt.Radios == null)
                                trt.Radios = new List<CommActesTraitement>();

                            if (trt.photos == null)
                                trt.photos = new List<CommActesTraitement>();

                            if (trt.Materiels == null)
                                trt.Materiels = new List<CommMaterielTraitement>();

                            trt.Acte.prix_acte = trt.Acte.prix_traitement;
                            //modification nadheeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeem
                            trt.partPatient = trt.Acte.prix_traitement - trt.Acte.Remboursement;

                            if (trt.ActesSupp != null)
                            {
                                foreach (CommActesTraitement AC in trt.ActesSupp)
                                {
                                    AC.prix_acte = AC.prix_traitement;
                                    //modification nadheeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeem
                                    AC.partPatient = AC.prix_traitement - AC.Remboursement;
                                }
                            }

                            if (trt.Radios != null)
                            {
                                foreach (CommActesTraitement AC in trt.Radios)
                                {
                                    AC.prix_acte = AC.prix_traitement;
                                    //modification nadheeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeem
                                    AC.partPatient = AC.prix_traitement - AC.Remboursement;

                                }
                            }
                            if (trt.photos != null)
                            {
                                foreach (CommActesTraitement AC in trt.photos)
                                {
                                    AC.prix_acte = AC.prix_traitement;
                                    //modification nadheeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeem
                                    AC.partPatient = AC.prix_traitement - AC.Remboursement;

                                }
                            }
                            if (trt.Materiels != null)
                            {
                                foreach (CommMaterielTraitement AC in trt.Materiels)
                                {
                                    AC.prix_materiel = AC.prix_traitement;
                                }
                            }

                        }
                    }


                }



                if (trt.ActesSupp == null)
                    trt.ActesSupp = new List<CommActesTraitement>();


                if (trt.Radios == null)
                    trt.Radios = new List<CommActesTraitement>();

                if (trt.photos == null)
                    trt.photos = new List<CommActesTraitement>();

                if (trt.Materiels == null)
                    trt.Materiels = new List<CommMaterielTraitement>();


                if (trt.ActesSupp != null)
                {
                    foreach (CommActesTraitement cap in trt.ActesSupp)
                    {
                        // cap.Parent = trt;
                        if (trt.desactive || cap.desactive) continue;
                        prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                        prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                    }
                }
                if (trt.Materiels != null)
                {

                    foreach (CommMaterielTraitement cap in trt.Materiels)
                    {
                        cap.Famille = MaterielsMgmt.GetFamilleMaterielByIdMateriel(cap.idMateriel);
                        if (cap.Famille != null)
                            if (cap.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0 || cap.Famille.libelle.ToLower().IndexOf("stérilisation") >= 0 || cap.Famille.libelle.ToLower().IndexOf("achats") >= 0)
                            {
                                if (trt.desactive || cap.desactive) continue;
                                prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                                prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                            }
                    }
                }
                if (trt.Radios != null)
                {

                    foreach (CommActesTraitement cap in trt.Radios)
                    {
                        if (trt.desactive || cap.desactive) continue;
                        prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                        prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                    }
                }
                if (trt.photos != null)
                {

                    foreach (CommActesTraitement cap in trt.photos)
                    {
                        if (trt.desactive || cap.desactive) continue;
                        prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                        prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));

                    }
                }
                if (trt.desactive) continue;
                if (trt.Acte != null)
                {
                    prix_total = prix_total + (Convert.ToDouble(trt.Acte.prix_traitement) * Convert.ToInt32(trt.Acte.quantite));
                    prix_ligne = prix_ligne + (Convert.ToDouble(trt.Acte.prix_traitement) * Convert.ToInt32(trt.Acte.quantite));
                }
                trt.prix = prix_ligne;

                //if (DevisTraitement != null)
                //  if (DevisTraitement.actesTraitement == null) ;
                // _DevisTraitement.MontantScenario = _DevisTraitement.MontantScenario + trt.prix;

            }
            if (DevisTraitement != null)
            {

                if (_DevisTraitement.MontantDocteur != null && _DevisTraitement.MontantDocteur > 0)
                {
                    TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.MontantDocteur);
                    TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _DevisTraitement.MontantAvantRemise);
                }
                else if (DevisTraitement.Montant > 0)
                {
                    TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.Montant);
                    if (_DevisTraitement.MontantDocteur == 0)
                        _DevisTraitement.MontantScenario = _DevisTraitement.Montant;
                    if (_DevisTraitement.MontantScenario == 0)
                    {
                        TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _DevisTraitement.Montant);
                        _DevisTraitement.MontantScenario = _DevisTraitement.Montant;
                    }
                    else
                        TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _DevisTraitement.MontantScenario);
                }
                else
                {
                    TxtPrixTotal.Text = prix_total.ToString("00.00");
                    TxtPrixAvR_Docteur.Text = prix_total.ToString("00.00");
                    _DevisTraitement.MontantScenario = prix_total;
                }


                if (DevisTraitement.MontantAvantRemise > 0)
                {
                    TxtPrixTotal_AvantRemise.Text = string.Format("{0:f2}", _DevisTraitement.MontantAvantRemise);
                    //TxtPrixTotal_AvantRemise.Visible = true;
                    //label2.Visible = false;
                    //label1.Visible = true;
                    //TxtPrixTotal_AvantRemise.Visible = true;
                    //TxtPrixAvR_Docteur.Visible = false;

                }

                //if (_DevisTraitement.MontantDocteur != null && _DevisTraitement.MontantDocteur > 0)
                //    //TxtPrixTotal_AvantRemise.Visible = true ;
                //else
                ////TxtPrixTotal_AvantRemise.Visible = TxtPrixTotal.Text != TxtPrixTotal_AvantRemise.Text;
            }
            else
            {


                TxtPrixTotal.Text = string.Format("{0:f2}", _Traitement.Montant);
                TxtPrixTotal_AvantRemise.Text = string.Format("{0:f2}", _Traitement.MontantAvantRemise);
                //TxtPrixTotal_AvantRemise.Visible = TxtPrixTotal.Text != TxtPrixTotal_AvantRemise.Text;

                btnRistourneGlobal.Visible = false;

                Echeancier.Visible = false;
                label2.Visible = false;
                label1.Visible = true;
                TxtPrixTotal_AvantRemise.Visible = _Traitement.Montant != _Traitement.MontantAvantRemise;
                label1.Visible = _Traitement.Montant != _Traitement.MontantAvantRemise;
                if (_isNotDevis)
                {
                    btnRistourneGlobal.Visible = false;
                    btnOk.Visible = true;
                    Echeancier.Visible = true;
                    label2.Visible = false;
                    label1.Visible = true;
                    BTN_ADD.Visible = true;
                    BTN_DELETE.Visible = true;
                    btnAddRdv.Visible = false;
                }
            }
            foreach (CommTraitement c in com)
            {
                int rowidx = dataGridView1.Rows.Add();
                RefreshRow(c, rowidx);

            }
            if (_DevisTraitement != null)
            {
                if ((_DevisTraitement.MontantDocteur != _DevisTraitement.MontantScenario && _DevisTraitement.MontantDocteur > 0) || (_visualisation))
                {
                    BTN_ADD.Visible = false;
                    BTN_DELETE.Visible = false;
                }
                else
                {
                    BTN_ADD.Visible = true;
                    BTN_DELETE.Visible = true;
                }
            }
            else
            {
                foreach (FamillesTraitements pf in TraitementsMgmt.famillesTraitement)
                    if (_Traitement.id_famille == pf.Id)
                    {
                        _Traitement.famille_Traitement = pf;
                        break;
                    }
                _Traitement.Montant_Scenario = _Traitement.Montant;
                BasCommon_BL.TraitementsMgmt.UpdateTraitement(_Traitement);
            }

        }
        private void FrmChoixActes_Load(object sender, EventArgs e)
        {

            if (_id_traitement == -1)
            {
                // Echeancier.Visible = false;

                Initdgv(DevisTraitement.actesTraitement);
                dataGridView1.Columns["Autre"].Visible = true;
            }
            else
            {
                // Echeancier.Visible = false;
                if (DevisTraitement == null)
                {
                    btnAddRdv.Visible = false;
                    dataGridView1.Columns["Echéance"].Visible = false;
                    colActive.Visible = false;
                }
                else
                {
                    dataGridView1.Columns["Echéance"].Visible = true;
                }

                _Traitement = new NewTraitement();
                _Traitement = TraitementsMgmt.GetFullTraitement(_id_traitement);
                TraitementsMgmt.GetCommTraitements(ref _Traitement);


                Initdgv(_Traitement.CommTraitement);


            }

            if (DevisTraitement != null && _id_traitement != -1)
            {
                dataGridView1.Columns["Autre"].Visible = true;
                DevisTraitement.actesTraitement = _Traitement.CommTraitement;
                //  Echeancier.Visible = false;
                p = new Proposition(DevisTraitement);
                //MgmtDevis.CreateDevis_TK(DevisTraitement);

            }


        }

        private void BTN_ADD_Click(object sender, EventArgs e)
        {
            // verfi if treeGrpAct2 && the dataGridView is empty
            if (this._typeFmTrt == TypeFamilleTraitement.GroupementActe2 && this.dataGridView1.RowCount == 0)
            {

                CommTraitement com = new CommTraitement();
                com.ActesSupp = new List<CommActesTraitement>();

                // com.date = DateTime.Now;
                com.NbJours = 0;
                // com.date = DateTime.Now.AddHours(22);
                com.DatePrevisionnnelle = DateTime.Now.AddDays(com.NbJours);
                FrmActeTraitement frm = new FrmActeTraitement(com, 1, "");

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (_DevisTraitement == null)
                    {

                        com.echeancestemp = new List<TempEcheanceDefinition>();
                        com.modecreation = CommTraitement.ModeCreation.Manuel;
                        TraitementsMgmt.AddCommTraitements(_id_traitement, com);

                        _Traitement.CommTraitement.Add(com);
                        TraitementsMgmt.SaveActesSupp(com);

                        //Initdgv(_Traitement.CommTraitement,false);
                        Initdgv(_Traitement.CommTraitement, false);
                    }

                }
            }

            else
            {
                FRmWizardNewComment frm = new FRmWizardNewComment();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CommTraitement com = new CommTraitement();
                    // com.patient = CurrentPatient;
                    if (frm.OtherDate.HasValue)
                    {
                        com.NbJours = (int)(frm.OtherDate.Value.Date - DateTime.Now.Date).TotalDays;
                        com.date = frm.OtherDate.Value;
                        com.etat = CommTraitement.EtatCommentaire.Prevus;
                    }
                    else
                    {
                        if ((frm.NbMoisFromNow == -1) || (frm.NbJoursFromNow == -1))
                        {
                            com.date = DateTime.Now.Date.AddHours(22);
                            com.etat = CommTraitement.EtatCommentaire.Prevus;

                        }
                        else
                        {
                            com.NbJours = frm.NbJoursFromNow;
                            com.NbMois = frm.NbMoisFromNow;

                            com.date = DateTime.Now.AddMonths(frm.NbMoisFromNow).AddDays(frm.NbJoursFromNow);
                            com.etat = CommTraitement.EtatCommentaire.Prevus;

                        }
                    }

                    com.praticien = frm.PraticienREsp;
                    com.Acte = new ActeTraitement(frm.acteselected);
                    com.Acte.prix_traitement = frm.newprix;
                    com.Acte.quantite = frm.vquantite;
                    com.echeancestemp = new List<TempEcheanceDefinition>();
                    com.modecreation = CommTraitement.ModeCreation.Manuel;
                    if (_DevisTraitement == null)
                    {
                        TraitementsMgmt.AddCommTraitements(_id_traitement, com);
                        _Traitement.CommTraitement.Add(com);
                        // code ajouté pour enregistrer les acte supp avant l'initialisation du dgv
                        //TraitementsMgmt.SaveActesSupp(com);
                        //Initdgv(_Traitement.CommTraitement, true);
                        Initdgv(_Traitement.CommTraitement, false);
                    }
                    else
                    {
                        if (com.NbMois > 0) com.NbJours = com.NbMois * 30;
                        MgmtDevis.AddCommDevis(_DevisTraitement.Id, com);
                        MgmtDevis.SaveAutrePersonne(com);
                        _DevisTraitement.actesTraitement.Add(com);
                        // MessageBox.Show(Convert.ToString (  _DevisTraitement.Montant));
                        Initdgv(_DevisTraitement.actesTraitement, false);
                        com.devis = _DevisTraitement;

                    }
                    if (_DevisTraitement != null)
                        _DevisTraitement.EcheancierDocteur = 0;

                    //  prix_total = prix_total + Convert.ToDouble(com.Acte.prix_acte);
                }
                if (_DevisTraitement != null)
                {
                    DevisTraitement.actesTraitement = DevisTraitement.actesTraitement.OrderBy(w => w.DatePrevisionnnelle).ToList();
                    Initdgv(DevisTraitement.actesTraitement);
                }
                #region Old Commented code
                /*FrmActeTraitement frm = new FrmActeTraitement(com);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //if (com.etat == CommClinique.EtatCommentaire.EnCours)
                    //    com.etat = CommClinique.EtatCommentaire.Termine;
                    com.Acte = frm.Value;
               
                    // NetWorkThreadClass.SendMessage(new NetWorkMsg() { ActionCODE = "COMMCLINIQUE", Parametres = com.Id.ToString(), IdPatient = CurrentPatient.Id });

                    //ReinitDisplay(RefreshOnlyOne.TheBigControl);
                   // int rowidx = dataGridView1.Rows.Add();
                    TraitementsMgmt.AddCommTraitements(_id_traitement, com, com.Acte);
                    prix_total = prix_total + Convert.ToDouble(com.Acte .prix_acte );
                    _Traitement.CommTraitement.Add(com);
                    Initdgv(_Traitement);
                    //RefreshRow(com, dataGridView1.Rows.Count-2);

                }*/
                #endregion
            }
        }



        public class OnCellSelectedEventArgs : EventArgs
        {

            private CommTraitement _comment;
            public CommTraitement comment
            {
                get
                {
                    return _comment;
                }
                set
                {
                    _comment = value;
                }
            }

            private DataGridViewColumn _column;
            public DataGridViewColumn column
            {
                get
                {
                    return _column;
                }
                set
                {
                    _column = value;
                }
            }

            private DataGridViewCellEventArgs _eventarg;
            public DataGridViewCellEventArgs eventarg
            {
                get
                {
                    return _eventarg;
                }
                set
                {
                    _eventarg = value;
                }
            }

            public OnCellSelectedEventArgs(DataGridViewColumn colonne, CommTraitement commentaire, DataGridViewCellEventArgs e)
            {
                eventarg = e;
                comment = commentaire;
                column = colonne;
            }




        }

        private void BTN_DELETE_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.Rows.Count > 1 && dataGridView1.SelectedCells[0].RowIndex == 0)
            //{
            //    MessageBox.Show("Suppression impossible!");
            //    return;
            //}
            com = ((CommTraitement)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Tag);
            if (com == null) return;
            if (MessageBox.Show("Souhaitez-vous réellement supprimer cet acte ?", "Supression", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (_DevisTraitement == null)
                {
                    TraitementsMgmt.DelActeTraitement(com);
                    _Traitement.CommTraitement.RemoveAt(dataGridView1.SelectedCells[0].RowIndex);
                    // dataGridView1.Rows.RemoveAt(dataGridView1.SelectedCells[0].RowIndex);
                    Initdgv(_Traitement.CommTraitement, false);

                }
                else
                {
                    MgmtDevis.DeleteCommDevis(_DevisTraitement.Id, com);
                    //MgmtDevis.SaveAutrePersonne(com);
                    _DevisTraitement.actesTraitement.Remove(com);
                    Initdgv(_DevisTraitement.actesTraitement, false);
                }
                if (_DevisTraitement != null)
                    _DevisTraitement.EcheancierDocteur = 0;


            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            if (_DevisTraitement == null)
            {
                Value = _Traitement.CommTraitement;
            }
            else
            {
                Value = _DevisTraitement.actesTraitement;
            }


            DialogResult = DialogResult.OK;
            Close();
            if (_DevisTraitement != null)
            {
                if (_DevisTraitement.DateAcceptation != null)
                {
                    DateTime chooseDate = _DevisTraitement.DatePrevisionnelDeDebutTraitement;
                    FrmDate frmdate = new FrmDate("Choix de la date de début de traitement", "Date de début ?");
                    frmdate.Value = _DevisTraitement.DatePrevisionnelDeDebutTraitement;
                    frmdate.Text = "Date de début de traitement";

                    if (frmdate.ShowDialog() == DialogResult.OK)
                    {
                        chooseDate = frmdate.Value;
                        if (_DevisTraitement.DatePrevisionnelDeDebutTraitement.Date != chooseDate.Date && (System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + BasCommon_DAL.DAC.prefix] == "CH"))
                        {
                            _DevisTraitement.DatePrevisionnelDeDebutTraitement = chooseDate;
                            foreach (CommTraitement cm in _DevisTraitement.actesTraitement)
                            {
                                foreach (TempEcheanceDefinition ted in cm.echeancestemp)
                                    ted.DAteEcheance = _DevisTraitement.DatePrevisionnelDeDebutTraitement;
                            }
                        }


                        _DevisTraitement.DatePrevisionnelDeDebutTraitement = chooseDate;

                    }
                    else
                        return;
                }
            }




        }
        private void CreateEcheanceToulon(DateTime date, double partSecu, double partMutuelle, double partPatient, ActePG ap, string libelleTrai, string libelleActe, bool alreadyPayed = false, bool canRecalculate = false, int idSemestre = 0, bool parPrelevelemnt = false, bool parVirement = false, Echeance.typepayeur payeur = Echeance.typepayeur.patient, bool verif = false)
        {

            TempEcheanceDefinition ted = new TempEcheanceDefinition();
            ap = new ActePG();
            tmpTED = new List<TempEcheanceDefinition>();
            int idx1 = libelleTrai.IndexOf("[P");
            if (idx1 > -1)
            {
                int idx2 = libelleTrai.IndexOf(']', idx1);
                libelleTrai = libelleTrai.Remove(idx1, (idx2 - idx1) + 1);
            }
            if (verif)
            {


                if (partPatient > 0 && payeur == Echeance.typepayeur.patient)
                {
                    ted = new TempEcheanceDefinition();
                    ted.DAteEcheance = date;
                    ted.Montant = partPatient;
                    ted.Libelle = libelleTrai + "[Part Patient]";
                    ted.acte = ap;
                    ted.acte.Libelle = libelleActe;
                    ted.ParVirement = parVirement;
                    ted.ParPrelevement = parPrelevelemnt;
                    ted.IdSemestre = idSemestre;

                    ted.AlreadyPayed = alreadyPayed;
                    ted.payeur = Echeance.typepayeur.patient;

                    tmpTED.Add(ted);

                }
                else
                    if (partMutuelle > 0 && payeur == Echeance.typepayeur.Mutuelle)
                    {
                        ted = new TempEcheanceDefinition();
                        ted.DAteEcheance = date;
                        ted.Montant = partMutuelle;
                        ted.Libelle = libelleTrai + "[Part Mutuelle]";
                        ted.acte = ap;
                        ted.acte.Libelle = libelleActe;
                        ted.ParVirement = parVirement;
                        ted.ParPrelevement = parPrelevelemnt;
                        ted.IdSemestre = idSemestre;

                        ted.AlreadyPayed = alreadyPayed;
                        ted.payeur = Echeance.typepayeur.Mutuelle;

                        tmpTED.Add(ted);

                    }

                    else

                        if (partSecu > 0 && payeur == Echeance.typepayeur.Secu)
                        {
                            ted = new TempEcheanceDefinition();
                            ted.DAteEcheance = date;
                            ted.Montant = partSecu;
                            ted.Libelle = libelleTrai + "[Part Secu]";
                            ted.acte = ap;
                            ted.acte.Libelle = libelleActe;
                            ted.ParVirement = parVirement;
                            ted.ParPrelevement = parPrelevelemnt;
                            ted.IdSemestre = idSemestre;

                            ted.AlreadyPayed = alreadyPayed; ;
                            ted.payeur = Echeance.typepayeur.Secu;

                            tmpTED.Add(ted);

                        }
            }
            else
            {
                if (partPatient > 0)
                {
                    ted = new TempEcheanceDefinition();
                    ted.DAteEcheance = date;
                    ted.Montant = partPatient;
                    ted.Libelle = libelleTrai + "[Part Patient]";
                    ted.acte = ap;
                    ted.acte.Libelle = libelleActe;
                    ted.ParVirement = parVirement;
                    ted.ParPrelevement = parPrelevelemnt;
                    ted.IdSemestre = idSemestre;

                    ted.AlreadyPayed = alreadyPayed;
                    ted.payeur = Echeance.typepayeur.patient;

                    tmpTED.Add(ted);

                }

                if (partMutuelle > 0)
                {
                    ted = new TempEcheanceDefinition();
                    ted.DAteEcheance = date;
                    ted.Montant = partMutuelle;
                    ted.Libelle = libelleTrai + "[Part Mutuelle]";
                    ted.acte = ap;
                    ted.acte.Libelle = libelleActe;
                    ted.ParVirement = parVirement;
                    ted.ParPrelevement = parPrelevelemnt;
                    ted.IdSemestre = idSemestre;

                    ted.AlreadyPayed = alreadyPayed;
                    ted.payeur = Echeance.typepayeur.Mutuelle;

                    tmpTED.Add(ted);

                }



                if (partSecu > 0)
                {
                    ted = new TempEcheanceDefinition();
                    ted.DAteEcheance = date;
                    ted.Montant = partSecu;
                    ted.Libelle = libelleTrai + "[Part Secu]";
                    ted.acte = ap;
                    ted.acte.Libelle = libelleActe;
                    ted.ParVirement = parVirement;
                    ted.ParPrelevement = parPrelevelemnt;
                    ted.IdSemestre = idSemestre;

                    ted.AlreadyPayed = alreadyPayed; ;
                    ted.payeur = Echeance.typepayeur.Secu;

                    tmpTED.Add(ted);

                }
            }
        }

        private void Echeancier_Click(object sender, EventArgs e)
        {

            List<TempEcheanceDefinition> lst = new List<TempEcheanceDefinition>();
            #region AjouterScenario
            if (_isNotDevis)
            {
                int countEch = 0;
                foreach (CommTraitement ct in _Traitement.CommTraitement)
                {

                    countEch += ct.echeancestemp.Count;
                }


                if (countEch == 0 || _ChanngeItem)
                    foreach (CommTraitement ct in _Traitement.CommTraitement)
                    {
                        ct.echeancestemp.Clear();
                        TempEcheanceDefinition ted = new TempEcheanceDefinition();
                        ted.DAteEcheance = ct.DatePrevisionnnelle.Value.AddMonths(6);
                        ted.Montant = TraitementsMgmt.GetPrixCom(ct);
                        ted.Libelle = ct.Acte.acte_libelle;
                        ted.acte = ap;
                        ted.acte.Libelle = ct.Acte.acte_libelle;
                        //ted.acte = com.acte;
                        ted.AlreadyPayed = false;
                        ted.payeur = Echeance.typepayeur.patient;

                        ct.echeancestemp.Add(ted);
                        lst.Add(ted);


                    }
                else
                    if (countEch > 1)
                        foreach (CommTraitement ct in _Traitement.CommTraitement)
                            lst.AddRange(ct.echeancestemp);
                    else
                        lst = _Traitement.CommTraitement[0].echeancestemp;
                DateTime tmpDate = new DateTime();
                for (int i = lst.Count - 1; i >= 0; i--)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (lst[i].DAteEcheance == lst[j].DAteEcheance)
                        {
                            lst[i].Montant += lst[j].Montant;
                            lst.Remove(lst[j]);
                        }
                    }


                }
                FrmFinancement frm = new FrmFinancement(CurrentPatient, lst);
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    List<TempEcheanceDefinition> TmpEcheancesScenario = new List<TempEcheanceDefinition>();
                    List<CommTraitement> TmpActeTraitements = new List<CommTraitement>();

                    List<TempEcheanceDefinition> ResultatEcheances = new List<TempEcheanceDefinition>();




                    foreach (BaseTempEcheanceDefinition tmp in frm.Montants)
                    {
                        TempEcheanceDefinition ted = new TempEcheanceDefinition();
                        ted.DAteEcheance = tmp.DAteEcheance;
                        ted.Montant = tmp.Montant;
                        ted.Libelle = tmp.Libelle;
                        ted.acte = ap;
                        ted.acte.Libelle = tmp.acte.Libelle;
                        //ted.acte = com.acte;
                        ted.AlreadyPayed = false;
                        ted.payeur = Echeance.typepayeur.patient;
                        TmpEcheancesScenario.Add(ted);

                    }

                    if (TmpEcheancesScenario.Count == 1)
                    {
                        foreach (CommTraitement ct in _Traitement.CommTraitement)
                        {
                            ct.echeancestemp.Clear();
                        }
                        _Traitement.CommTraitement[0].echeancestemp = TmpEcheancesScenario;
                    }
                    else
                    {
                        TmpActeTraitements = _Traitement.CommTraitement;
                        List<double> MontantsEcheancesLignes = new List<double>();
                        List<double> MontantsEcheancesScenario = new List<double>();
                        foreach (TempEcheanceDefinition de in TmpEcheancesScenario)
                        {
                            MontantsEcheancesScenario.Add(de.Montant);
                        }
                        foreach (CommTraitement ct in TmpActeTraitements)
                        {
                            MontantsEcheancesLignes.Add(ct.MontantLigne);
                        }
                        for (int t = 0; t <= MontantsEcheancesScenario.Count - 1; t++)
                        {
                            for (int i = 0; i <= MontantsEcheancesLignes.Count - 1; i++)
                            {
                                if (MontantsEcheancesLignes[i] > 0)
                                {
                                    double MontantEcheancesLigne = 0;
                                    Boolean BreakFor = false;
                                    if (MontantsEcheancesScenario[t] <= MontantsEcheancesLignes[i])
                                    {
                                        TempEcheanceDefinition tted = new TempEcheanceDefinition()
                                        {
                                            acte = TmpEcheancesScenario[t].acte,
                                            AlreadyPayed = TmpEcheancesScenario[t].AlreadyPayed,
                                            CanRecalculate = TmpEcheancesScenario[t].CanRecalculate,
                                            DAteEcheance = TmpEcheancesScenario[t].DAteEcheance,
                                            Id = TmpEcheancesScenario[t].Id,
                                            IdSemestre = TmpEcheancesScenario[t].acte.Semestre,
                                            Libelle = TmpEcheancesScenario[t].Libelle,
                                            Montant = MontantsEcheancesScenario[t],
                                            ParPrelevement = TmpEcheancesScenario[t].ParPrelevement,
                                            ParVirement = TmpEcheancesScenario[t].ParVirement,
                                            payeur = TmpEcheancesScenario[t].payeur,


                                        };
                                        MontantsEcheancesScenario[t] = MontantsEcheancesScenario[t] - tted.Montant;
                                        MontantsEcheancesLignes[i] = MontantsEcheancesLignes[i] - tted.Montant;
                                        MontantEcheancesLigne = MontantEcheancesLigne + tted.Montant;

                                        ResultatEcheances.Add(tted);
                                        // ct.echeancestemp.Add(tted);
                                        break;
                                    }

                                    else
                                    {
                                        TempEcheanceDefinition tted = new TempEcheanceDefinition()
                                        {
                                            acte = TmpEcheancesScenario[t].acte,
                                            AlreadyPayed = TmpEcheancesScenario[t].AlreadyPayed,
                                            CanRecalculate = TmpEcheancesScenario[t].CanRecalculate,
                                            DAteEcheance = TmpEcheancesScenario[t].DAteEcheance,
                                            Id = TmpEcheancesScenario[t].Id,
                                            IdSemestre = TmpEcheancesScenario[t].acte.Semestre,
                                            Libelle = TmpEcheancesScenario[t].Libelle,
                                            Montant = MontantsEcheancesLignes[i],
                                            ParPrelevement = TmpEcheancesScenario[t].ParPrelevement,
                                            ParVirement = TmpEcheancesScenario[t].ParVirement,
                                            payeur = TmpEcheancesScenario[t].payeur,


                                        };
                                        MontantsEcheancesScenario[t] = MontantsEcheancesScenario[t] - tted.Montant;
                                        MontantsEcheancesLignes[i] = MontantsEcheancesLignes[i] - tted.Montant;
                                        ResultatEcheances.Add(tted);
                                    }
                                }

                            }


                        }
                        foreach (CommTraitement tact in _Traitement.CommTraitement)
                        {
                            tact.echeancestemp.Clear();
                            double TotEcheancesLignes = 0;
                            for (int i = 0; i <= ResultatEcheances.Count - 1; i++)
                            {
                                if (tact.MontantLigne > TotEcheancesLignes)
                                {
                                    if (ResultatEcheances[i].IdSemestre == 0)
                                    {
                                        tact.echeancestemp.Add(ResultatEcheances[i]);
                                        TotEcheancesLignes = TotEcheancesLignes + ResultatEcheances[i].Montant;
                                        // ResultatEcheances[i].Montant = 0;
                                        ResultatEcheances[i].IdSemestre = 1;
                                    }
                                }
                                else
                                    break;


                            }
                        }
                    }


                }

            }
            #endregion
            else
            {

                if (_DevisTraitement.echeancestemp == null)
                    _DevisTraitement.echeancestemp = new List<TempEcheanceDefinition>();
                if ((System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + BasCommon_DAL.DAC.prefix] == "FR") && _DevisTraitement.Traitement.TypeScenario != NewTraitement.typeScenario.Prothése_CMUC)
                {
                    #region TOULON



                    if (_DevisTraitement.EcheancierDocteur == 1)
                    {

                        DateTime TmpDate = DateTime.Parse("01/01/1950");
                        TempEcheanceDefinition techd = new TempEcheanceDefinition();
                        double MontantTmpEcheance = 0;

                        _DevisTraitement.echeancestemp.Clear();
                        if (_DevisTraitement.actesTraitement[0].echeancestemp.Count == 0)
                            foreach (CommTraitement tt in _DevisTraitement.actesTraitement)
                            {

                                double partsecu = 0;
                                double partmutuelle = 0;
                                double parPatient = 0;

                                TraitementsMgmt.getMontantEcheToulon(tt, ref partsecu, ref partmutuelle, ref parPatient);
                                tt.echeancestemp = new List<TempEcheanceDefinition>();
                                CreateEcheanceToulon(_DevisTraitement.DatePrevisionnelDeDebutTraitement.AddMonths(6), partsecu, partmutuelle, parPatient, ap, _DevisTraitement.Traitement.Traitement_libelle, tt.Acte.acte_libelle);
                                tt.echeancestemp.AddRange(tmpTED);

                            }




                        foreach (CommTraitement cct in _DevisTraitement.actesTraitement)
                        {
                            foreach (TempEcheanceDefinition te in cct.echeancestemp)
                            {


                                TempEcheanceDefinition tes = _DevisTraitement.echeancestemp.Find(x => (x.DAteEcheance.ToString("dd/M/yyyy") == te.DAteEcheance.ToString("dd/M/yyyy")) && (x.payeur == te.payeur));
                                if (tes != null)
                                {


                                    if (te.Montant > 0)
                                    {

                                        tes.Montant += te.Montant;
                                        te.Libelle = tes.Libelle;

                                    }
                                }

                                else
                                {
                                    double partsecu = 0;
                                    double partmutuelle = 0;
                                    double parPatient = 0;

                                    TraitementsMgmt.getMontantEcheToulon(cct, ref partsecu, ref partmutuelle, ref parPatient);
                                    if (te.payeur == Echeance.typepayeur.patient)
                                        parPatient = te.Montant;
                                    CreateEcheanceToulon(te.DAteEcheance, partsecu, partmutuelle, parPatient, te.acte, te.Libelle, te.acte.Libelle, te.AlreadyPayed, te.CanRecalculate, te.IdSemestre, te.ParPrelevement, te.ParVirement, te.payeur, true);
                                    _DevisTraitement.echeancestemp.AddRange(tmpTED);

                                }




                                // TmpDate = te.DAteEcheance;
                            }


                        }

                    }

                    else
                    {

                        double partsecu = 0;
                        double partmutuelle = 0;
                        double parPatient = 0;
                        double SommePartsecu = 0;
                        double SommePartmutuelle = 0;
                        double SommeParPatient = 0;

                        foreach (CommTraitement ct in _DevisTraitement.actesTraitement)
                        {
                            TraitementsMgmt.getMontantEcheToulon(ct, ref partsecu, ref partmutuelle, ref parPatient);
                            SommeParPatient += parPatient;
                            SommePartmutuelle += partmutuelle;
                            SommePartsecu += partsecu;


                        }
                        _DevisTraitement.echeancestemp.Clear();
                        TempEcheanceDefinition ted = new TempEcheanceDefinition();
                        ap = new ActePG();
                        CreateEcheanceToulon(_DevisTraitement.DatePrevisionnelDeDebutTraitement.AddMonths(6), SommePartsecu, SommePartmutuelle, SommeParPatient, ap, _DevisTraitement.Traitement.Traitement_libelle, _DevisTraitement.Traitement.Traitement_libelle);
                        _DevisTraitement.echeancestemp.AddRange(tmpTED);

                        _DevisTraitement.EcheancierDocteur = 1;


                    }


                    Proposition p = new Proposition(_DevisTraitement);
                    p.echeancestemp.Sort((x, y) => x.DAteEcheance.CompareTo(y.DAteEcheance));
                    if (_DevisTraitement.MontantDocteur > 0 && p.echeancestemp.Count == 1)
                        p.echeancestemp[0].Montant = Convert.ToDouble(_DevisTraitement.MontantDocteur);
                    FrmFinancement frm = new FrmFinancement(p, p.patient, p.echeancestemp, -1, _visualisation, 1, _DevisTraitement.Traitement.TypeScenario);
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        _DevisTraitement.EcheancierDocteur = 1;
                        _DevisTraitement.echeancestemp.Clear();
                        BasCommon_BL.MgmtDevis.DeleteTempEcheance(p);
                        BasCommon_BL.MgmtDevis.UpdateEcheancierDocteur(_DevisTraitement.Id, _DevisTraitement.EcheancierDocteur);
                        if (frm.Montants.Count > 1)
                        {
                            TxtPrixTotal_AvantRemise.Text = string.Format("{0:f2}", _DevisTraitement.MontantAvantRemise);
                            TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.Montant);
                        }
                        //TxtPrixTotal_AvantRemise.Visible = _DevisTraitement.Montant != _DevisTraitement.MontantAvantRemise;

                        foreach (BaseTempEcheanceDefinition ted in frm.Montants)
                        {
                            if (ted.acte != null)
                            {
                                TempEcheanceDefinition tted = new TempEcheanceDefinition()
                                {
                                    acte = ted.acte,
                                    AlreadyPayed = ted.AlreadyPayed,
                                    CanRecalculate = ted.CanRecalculate,
                                    DAteEcheance = ted.DAteEcheance,
                                    Id = ted.Id,
                                    IdSemestre = ted.acte.Semestre,
                                    Libelle = ted.Libelle,
                                    Montant = ted.Montant,
                                    ParPrelevement = ted.ParPrelevement,
                                    ParVirement = ted.ParVirement,
                                    payeur = ted.payeur,


                                };
                                _DevisTraitement.echeancestemp.Add(tted);
                            }
                        }
                        List<TempEcheanceDefinition> TmpEcheancesDevis = new List<TempEcheanceDefinition>();
                        List<TempEcheanceDefinition> TmpEcheancesDevisTOULON = new List<TempEcheanceDefinition>();

                        List<CommTraitement> TmpActeTraitements = new List<CommTraitement>();

                        List<TempEcheanceDefinition> ResultatEcheances = new List<TempEcheanceDefinition>();


                        TmpEcheancesDevis = _DevisTraitement.echeancestemp;
                        TmpActeTraitements = _DevisTraitement.actesTraitement;

                        List<double> MontantsEcheancesLignesTOULON = new List<double>();
                        List<double> MontantsEcheancesDevisTOULON = new List<double>();

                        foreach (TempEcheanceDefinition de in _DevisTraitement.echeancestemp)
                        {
                            if (de.payeur == Echeance.typepayeur.patient)
                                TmpEcheancesDevisTOULON.Add(de);
                        }
                        foreach (TempEcheanceDefinition de in TmpEcheancesDevis)
                        {
                            if (de.payeur == Echeance.typepayeur.patient)
                                MontantsEcheancesDevisTOULON.Add(de.Montant);
                        }
                        foreach (CommTraitement ct in TmpActeTraitements)
                        {
                            double mon = 0;
                            double parsecu = 0;
                            double partmut = 0;
                            TraitementsMgmt.getMontantEcheToulon(ct, ref parsecu, ref partmut, ref mon);
                            MontantsEcheancesLignesTOULON.Add(mon);

                        }
                        for (int t = 0; t <= MontantsEcheancesDevisTOULON.Count - 1; t++)
                        {
                            for (int i = 0; i <= MontantsEcheancesLignesTOULON.Count - 1; i++)
                            {
                                if (MontantsEcheancesLignesTOULON[i] > 0)
                                {
                                    double MontantEcheancesLigne = 0;
                                    Boolean BreakFor = false;


                                    if (MontantsEcheancesDevisTOULON[t] <= MontantsEcheancesLignesTOULON[i])
                                    {
                                        TempEcheanceDefinition tted = new TempEcheanceDefinition()
                                        {
                                            acte = TmpEcheancesDevisTOULON[t].acte,
                                            AlreadyPayed = TmpEcheancesDevisTOULON[t].AlreadyPayed,
                                            CanRecalculate = TmpEcheancesDevisTOULON[t].CanRecalculate,
                                            DAteEcheance = TmpEcheancesDevisTOULON[t].DAteEcheance,
                                            Id = TmpEcheancesDevisTOULON[t].Id,
                                            IdSemestre = TmpEcheancesDevisTOULON[t].acte.Semestre,
                                            Libelle = TmpEcheancesDevisTOULON[t].Libelle,
                                            Montant = MontantsEcheancesDevisTOULON[t],
                                            ParPrelevement = TmpEcheancesDevisTOULON[t].ParPrelevement,
                                            ParVirement = TmpEcheancesDevisTOULON[t].ParVirement,
                                            payeur = TmpEcheancesDevisTOULON[t].payeur,


                                        };
                                        MontantsEcheancesDevisTOULON[t] = MontantsEcheancesDevisTOULON[t] - tted.Montant;
                                        MontantsEcheancesLignesTOULON[i] = MontantsEcheancesLignesTOULON[i] - tted.Montant;
                                        MontantEcheancesLigne = MontantEcheancesLigne + tted.Montant;

                                        ResultatEcheances.Add(tted);
                                        // ct.echeancestemp.Add(tted);
                                        break;
                                    }

                                    else
                                    {
                                        TempEcheanceDefinition tted = new TempEcheanceDefinition()
                                        {
                                            acte = TmpEcheancesDevisTOULON[t].acte,
                                            AlreadyPayed = TmpEcheancesDevisTOULON[t].AlreadyPayed,
                                            CanRecalculate = TmpEcheancesDevisTOULON[t].CanRecalculate,
                                            DAteEcheance = TmpEcheancesDevisTOULON[t].DAteEcheance,
                                            Id = TmpEcheancesDevisTOULON[t].Id,
                                            IdSemestre = TmpEcheancesDevisTOULON[t].acte.Semestre,
                                            Libelle = TmpEcheancesDevisTOULON[t].Libelle,
                                            Montant = MontantsEcheancesLignesTOULON[i],
                                            ParPrelevement = TmpEcheancesDevisTOULON[t].ParPrelevement,
                                            ParVirement = TmpEcheancesDevisTOULON[t].ParVirement,
                                            payeur = TmpEcheancesDevisTOULON[t].payeur,


                                        };
                                        MontantsEcheancesDevisTOULON[t] = MontantsEcheancesDevisTOULON[t] - tted.Montant;
                                        MontantsEcheancesLignesTOULON[i] = MontantsEcheancesLignesTOULON[i] - tted.Montant;
                                        ResultatEcheances.Add(tted);
                                    }


                                }


                            }
                        }
                        foreach (CommTraitement tact in _DevisTraitement.actesTraitement)
                        {
                            double mon = 0;
                            double parsecu = 0;
                            double partmut = 0;
                            TraitementsMgmt.getMontantEcheToulon(tact, ref parsecu, ref partmut, ref mon);
                            if (tact.echeancestemp.Count == 0)
                            {
                                CreateEcheanceToulon(_DevisTraitement.DatePrevisionnelDeDebutTraitement.AddMonths(6), parsecu, partmut, mon, ap, tact.Acte.acte_libelle, tact.Acte.acte_libelle);
                                tact.echeancestemp.AddRange(tmpTED);
                            }
                            for (int j = tact.echeancestemp.Count - 1; j >= 0; j--)
                            {
                                if (tact.echeancestemp[j].payeur == Echeance.typepayeur.patient)
                                    tact.echeancestemp.Remove(tact.echeancestemp[j]);
                            }
                            double TotEcheancesLignes = 0;
                            for (int i = 0; i <= ResultatEcheances.Count - 1; i++)
                            {

                                if (Math.Round(mon, 2) > Math.Round(TotEcheancesLignes, 2))
                                {
                                    if (ResultatEcheances[i].IdSemestre == 0)
                                    {
                                        tact.echeancestemp.Add(ResultatEcheances[i]);
                                        TotEcheancesLignes = TotEcheancesLignes + ResultatEcheances[i].Montant;
                                        // ResultatEcheances[i].Montant = 0;
                                        ResultatEcheances[i].IdSemestre = 1;
                                    }
                                }
                                else
                                    break;


                            }
                        }
                        foreach (CommTraitement ca in _DevisTraitement.actesTraitement)
                        {
                            foreach (TempEcheanceDefinition ec in ca.echeancestemp)
                            {
                                //if (_DevisTraitement.MontantDocteur > 0)
                                //    ec.Montant = (double)(ec.Montant * (_DevisTraitement.MontantDocteur / _DevisTraitement.Montant));


                                MgmtDevis.update_tempechenaces_tk(ec, ca);
                            }

                        }





                        _DevisTraitement.EcheancierDocteur = 1;
                        /* else
                         {
                             _DevisTraitement.echeancestemp = LastEcheancesDevis;
                         }*/

                    }
                    #endregion
                }

                else
                {
                    #region AUTRESS
                    if (_DevisTraitement.EcheancierDocteur == 1)
                    {

                        DateTime TmpDate = DateTime.Parse("01/01/1950");
                        TempEcheanceDefinition techd = new TempEcheanceDefinition();
                        double MontantTmpEcheance = 0;

                        _DevisTraitement.echeancestemp.Clear();
                        bool verifEcheancetemp = false;
                        foreach (CommTraitement tt in _DevisTraitement.actesTraitement)
                        {
                            if (tt.echeancestemp.Count > 0)
                            {
                                verifEcheancetemp = true;
                                break;
                            }
                        }
                        if (!verifEcheancetemp)
                            foreach (CommTraitement tt in _DevisTraitement.actesTraitement)
                            {
                                if (tt.desactive) continue;
                                tt.echeancestemp = new List<TempEcheanceDefinition>();
                                TempEcheanceDefinition ted = new TempEcheanceDefinition();
                                ted.DAteEcheance = _DevisTraitement.DatePrevisionnelDeDebutTraitement;
                                ted.Montant = TraitementsMgmt.GetPrixCom(tt);
                                ted.Libelle = _DevisTraitement.Traitement.Traitement_libelle;
                                ted.acte = ap;
                                ted.acte.Libelle = tt.Acte.acte_libelle;
                                //ted.acte = com.acte;
                                ted.AlreadyPayed = false;
                                ted.payeur = Echeance.typepayeur.patient;

                                tt.echeancestemp.Add(ted);

                            }




                        foreach (CommTraitement cct in _DevisTraitement.actesTraitement)
                        {
                            if (cct.desactive)
                            {
                                cct.echeancestemp.Clear(); continue;
                            }
                            foreach (TempEcheanceDefinition te in cct.echeancestemp)
                            {

                                ////AUTRES
                                TempEcheanceDefinition tes = _DevisTraitement.echeancestemp.Find(x => x.DAteEcheance.ToString("dd/M/yyyy") == te.DAteEcheance.ToString("dd/M/yyyy"));

                                if (tes != null)
                                {


                                    if (te.Montant > 0)
                                    {
                                        tes.Montant += te.Montant;
                                        te.Libelle = tes.Libelle;

                                    }
                                }


                                else
                                {


                                    techd = new TempEcheanceDefinition();
                                    techd.acte = te.acte;
                                    techd.AlreadyPayed = te.AlreadyPayed;
                                    techd.CanRecalculate = te.CanRecalculate;
                                    techd.DAteEcheance = te.DAteEcheance;
                                    techd.Id = te.Id;
                                    techd.IdSemestre = te.acte.Semestre;
                                    techd.Libelle = te.Libelle;
                                    techd.Montant = te.Montant;
                                    techd.ParPrelevement = te.ParPrelevement;
                                    techd.ParVirement = te.ParVirement;
                                    techd.payeur = te.payeur;
                                    MontantTmpEcheance = te.Montant;
                                    _DevisTraitement.echeancestemp.Add(techd);
                                }

                                // TmpDate = te.DAteEcheance;
                            }


                        }


                        if (_DevisTraitement.echeancestemp.Count == 0 && MontantTmpEcheance > 0)
                        {
                            techd.Montant = MontantTmpEcheance;
                            _DevisTraitement.echeancestemp.Add(techd);
                        }
                        else
                        {

                            if (_DevisTraitement.echeancestemp[_DevisTraitement.echeancestemp.Count - 1].DAteEcheance == techd.DAteEcheance)
                                _DevisTraitement.echeancestemp[_DevisTraitement.echeancestemp.Count - 1].Montant = techd.Montant;
                            else
                            {
                                techd.Montant = MontantTmpEcheance;
                                _DevisTraitement.echeancestemp.Add(techd);
                            }
                        }


                    }

                    else
                    {

                        _DevisTraitement.echeancestemp.Clear();
                        TempEcheanceDefinition ted = new TempEcheanceDefinition();
                        ted.DAteEcheance = _DevisTraitement.DatePrevisionnelDeDebutTraitement;
                        ted.Montant = Convert.ToDouble(_DevisTraitement.Montant);
                        ted.Libelle = _DevisTraitement.Traitement.Traitement_libelle;
                        ted.acte = ap;
                        ted.acte.Libelle = _DevisTraitement.Traitement.Traitement_libelle;
                        //ted.acte = com.acte;
                        ted.AlreadyPayed = false;
                        ted.payeur = Echeance.typepayeur.patient;
                        _DevisTraitement.EcheancierDocteur = 1;
                        _DevisTraitement.echeancestemp.Add(ted);

                    }

                    Proposition p = new Proposition(_DevisTraitement);
                    p.echeancestemp.Sort((x, y) => x.DAteEcheance.CompareTo(y.DAteEcheance));
                    if (_DevisTraitement.MontantDocteur > 0 && p.echeancestemp.Count == 1)
                        p.echeancestemp[0].Montant = Convert.ToDouble(_DevisTraitement.MontantDocteur);
                    FrmFinancement frm = new FrmFinancement(p, p.patient, p.echeancestemp, -1, _visualisation, 1, _DevisTraitement.Traitement.TypeScenario);
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        _DevisTraitement.EcheancierDocteur = 1;
                        _DevisTraitement.echeancestemp.Clear();
                        BasCommon_BL.MgmtDevis.DeleteTempEcheance(p);
                        BasCommon_BL.MgmtDevis.UpdateEcheancierDocteur(_DevisTraitement.Id, _DevisTraitement.EcheancierDocteur);
                        if (frm.Montants.Count > 1)
                        {
                            TxtPrixTotal_AvantRemise.Text = string.Format("{0:f2}", _DevisTraitement.MontantAvantRemise);
                            TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.Montant);
                        }
                        //TxtPrixTotal_AvantRemise.Visible = _DevisTraitement.Montant != _DevisTraitement.MontantAvantRemise;

                        foreach (BaseTempEcheanceDefinition ted in frm.Montants)
                        {
                            if (ted.acte != null)
                            {
                                TempEcheanceDefinition tted = new TempEcheanceDefinition()
                                {
                                    acte = ted.acte,
                                    AlreadyPayed = ted.AlreadyPayed,
                                    CanRecalculate = ted.CanRecalculate,
                                    DAteEcheance = ted.DAteEcheance,
                                    Id = ted.Id,
                                    desactive = ted.desactive,
                                    IdSemestre = ted.acte.Semestre,
                                    Libelle = ted.Libelle,
                                    Montant = ted.Montant,
                                    ParPrelevement = ted.ParPrelevement,
                                    ParVirement = ted.ParVirement,
                                    payeur = ted.payeur,


                                };
                                _DevisTraitement.echeancestemp.Add(tted);
                            }
                        }
                        List<TempEcheanceDefinition> TmpEcheancesDevis = new List<TempEcheanceDefinition>();
                        List<TempEcheanceDefinition> TmpEcheancesDevisTOULON = new List<TempEcheanceDefinition>();

                        List<CommTraitement> TmpActeTraitements = new List<CommTraitement>();

                        List<TempEcheanceDefinition> ResultatEcheances = new List<TempEcheanceDefinition>();


                        TmpEcheancesDevis = _DevisTraitement.echeancestemp;
                        TmpActeTraitements = _DevisTraitement.actesTraitement;
                        List<double> MontantsEcheancesLignes = new List<double>();
                        List<double> MontantsEcheancesDevis = new List<double>();

                        foreach (TempEcheanceDefinition de in TmpEcheancesDevis)
                        {
                            MontantsEcheancesDevis.Add(de.Montant);
                        }
                        foreach (CommTraitement ct in TmpActeTraitements)
                        {
                            if (ct.desactive) continue;
                            MontantsEcheancesLignes.Add(ct.MontantLigne);

                        }



                        for (int t = 0; t <= MontantsEcheancesDevis.Count - 1; t++)
                        {
                            for (int i = 0; i <= MontantsEcheancesLignes.Count - 1; i++)
                            {
                                if (MontantsEcheancesLignes[i] > 0)
                                {
                                    double MontantEcheancesLigne = 0;
                                    Boolean BreakFor = false;


                                    if (MontantsEcheancesDevis[t] <= MontantsEcheancesLignes[i])
                                    {
                                        TempEcheanceDefinition tted = new TempEcheanceDefinition()
                                        {
                                            acte = TmpEcheancesDevis[t].acte,
                                            AlreadyPayed = TmpEcheancesDevis[t].AlreadyPayed,
                                            CanRecalculate = TmpEcheancesDevis[t].CanRecalculate,
                                            DAteEcheance = TmpEcheancesDevis[t].DAteEcheance,
                                            Id = TmpEcheancesDevis[t].Id,
                                            IdSemestre = TmpEcheancesDevis[t].acte.Semestre,
                                            Libelle = TmpEcheancesDevis[t].Libelle,
                                            Montant = MontantsEcheancesDevis[t],
                                            ParPrelevement = TmpEcheancesDevis[t].ParPrelevement,
                                            ParVirement = TmpEcheancesDevis[t].ParVirement,
                                            payeur = TmpEcheancesDevis[t].payeur,


                                        };
                                        MontantsEcheancesDevis[t] = MontantsEcheancesDevis[t] - tted.Montant;
                                        MontantsEcheancesLignes[i] = MontantsEcheancesLignes[i] - tted.Montant;
                                        MontantEcheancesLigne = MontantEcheancesLigne + tted.Montant;

                                        ResultatEcheances.Add(tted);
                                        // ct.echeancestemp.Add(tted);
                                        break;
                                    }

                                    else
                                    {
                                        TempEcheanceDefinition tted = new TempEcheanceDefinition()
                                        {
                                            acte = TmpEcheancesDevis[t].acte,
                                            AlreadyPayed = TmpEcheancesDevis[t].AlreadyPayed,
                                            CanRecalculate = TmpEcheancesDevis[t].CanRecalculate,
                                            DAteEcheance = TmpEcheancesDevis[t].DAteEcheance,
                                            Id = TmpEcheancesDevis[t].Id,
                                            IdSemestre = TmpEcheancesDevis[t].acte.Semestre,
                                            Libelle = TmpEcheancesDevis[t].Libelle,
                                            Montant = MontantsEcheancesLignes[i],
                                            ParPrelevement = TmpEcheancesDevis[t].ParPrelevement,
                                            ParVirement = TmpEcheancesDevis[t].ParVirement,
                                            payeur = TmpEcheancesDevis[t].payeur,


                                        };
                                        MontantsEcheancesDevis[t] = MontantsEcheancesDevis[t] - tted.Montant;
                                        MontantsEcheancesLignes[i] = MontantsEcheancesLignes[i] - tted.Montant;
                                        ResultatEcheances.Add(tted);
                                    }


                                }


                            }
                        }
                        double tot = 0;
                        foreach (CommTraitement tact in _DevisTraitement.actesTraitement)
                        {
                            if (tact.desactive) continue;
                            tact.echeancestemp.Clear();
                            double TotEcheancesLignes = 0;
                            for (int i = 0; i <= ResultatEcheances.Count - 1; i++)
                            {
                                if (Math.Round(tact.MontantLigne, 2) > Math.Round(TotEcheancesLignes, 2))
                                {
                                    if (ResultatEcheances[i].IdSemestre == 0)
                                    {
                                        tact.echeancestemp.Add(ResultatEcheances[i]);
                                        TotEcheancesLignes = TotEcheancesLignes + ResultatEcheances[i].Montant;
                                        // ResultatEcheances[i].Montant = 0;
                                        ResultatEcheances[i].IdSemestre = 1;
                                    }
                                }
                                else
                                    break;


                            }
                        }
                        foreach (CommTraitement ca in _DevisTraitement.actesTraitement)
                        {
                            foreach (TempEcheanceDefinition ec in ca.echeancestemp)
                            {
                                //if (_DevisTraitement.MontantDocteur > 0)
                                //    ec.Montant = (double)(ec.Montant * (_DevisTraitement.MontantDocteur / _DevisTraitement.Montant));


                                MgmtDevis.update_tempechenaces_tk(ec, ca);
                            }

                        }


                        _DevisTraitement.EcheancierDocteur = 1;
                        /* else
                         {
                             _DevisTraitement.echeancestemp = LastEcheancesDevis;
                         }*/

                    }

                    #endregion
                }

            }
        }


        private void btnRistourneGlobal_Click(object sender, EventArgs e)
        {


            double total = 0;
            double MontantLabo = 0;
            List<TempEcheanceDefinition> EcheancesDevis = new List<TempEcheanceDefinition>();

            foreach (CommTraitement ac in _DevisTraitement.actesTraitement)
            {
                if (ac.desactive) continue;
                foreach (CommMaterielTraitement mat in ac.Materiels)
                {
                    if (mat.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0 && mat.ShortLib != "STE" && !mat.desactive && mat.ShortLib != "achats")
                    {
                        MontantLabo = MontantLabo + (mat.prix_traitement * mat.Qte);
                    }
                }

                foreach (TempEcheanceDefinition ech in ac.echeancestemp)
                {
                    EcheancesDevis.Add(ech);
                }
            }



            FrmRistourne frm = new FrmRistourne(_DevisTraitement);
            double? TmpMontantDevis = _DevisTraitement.Montant;
            double? MontantAvRedDocteur = Convert.ToDouble(TxtPrixAvR_Docteur.Text);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //if (_DevisTraitement.Montant - frm.Value > MontantLabo)
                //{
                //    MessageBox.Show("MontantTrop grand, Vous ne pouvez déduire que " + MontantLabo.ToString("C2"));
                //    return;
                //}
                double avantremise = _DevisTraitement.MontantAvantRemise.Value;
                double newval = frm.Value;
                _DevisTraitement.MontantDocteur = newval;
                TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", MontantAvRedDocteur);
                _DevisTraitement.MontantScenario = MontantAvRedDocteur;
                //   _DevisTraitement.echeancestemp.Clear();

                //if (_DevisTraitement.echeancestemp == null)
                //  _DevisTraitement.echeancestemp = new List<TempEcheanceDefinition>();
                foreach (CommTraitement cm in _DevisTraitement.actesTraitement)
                {
                    cm.echeancestemp.Clear();
                    TempEcheanceDefinition tted = new TempEcheanceDefinition();
                    tted.DAteEcheance = _DevisTraitement.DatePrevisionnelDeDebutTraitement.AddMonths(6);
                    tted.Montant = TraitementsMgmt.GetPrixCom(cm);
                    tted.Libelle = cm.Acte.acte_libelle;
                    tted.acte = ap;
                    tted.acte.Libelle = cm.Acte.acte_libelle;
                    //ted.acte = com.acte;
                    tted.AlreadyPayed = false;
                    tted.payeur = Echeance.typepayeur.patient;

                    cm.echeancestemp.Add(tted);
                }
                if (_DevisTraitement.echeancestemp == null)
                    _DevisTraitement.echeancestemp = new List<TempEcheanceDefinition>();
                _DevisTraitement.echeancestemp.Clear();
                TempEcheanceDefinition ted = new TempEcheanceDefinition();
                ted.DAteEcheance = _DevisTraitement.DatePrevisionnelDeDebutTraitement.AddMonths(6);
                ted.Montant = (newval);
                ted.Libelle = _DevisTraitement.Traitement.Traitement_shortlib;
                ted.acte = ap;
                ted.acte.Libelle = _DevisTraitement.Traitement.Traitement_shortlib;
                //ted.acte = com.acte;
                ted.AlreadyPayed = false;
                ted.payeur = Echeance.typepayeur.patient;

                _DevisTraitement.echeancestemp.Add(ted);










                foreach (CommTraitement cm in _DevisTraitement.actesTraitement)
                {
                    foreach (CommMaterielTraitement mat in cm.Materiels)
                    {
                        if (mat.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0 && mat.ShortLib != "STE" && mat.ShortLib != "achats")
                            mat.prix_traitement = mat.prix_traitement - ((mat.prix_traitement / MontantLabo) * (double)(TmpMontantDevis - _DevisTraitement.MontantDocteur));
                        //*(_DevisTraitement .Montant -_DevisTraitement .MontantDocteur  )) ;

                    }
                    MgmtDevis.SavePrixCom(cm);
                }


                if (string.Format("{0:f2}", _DevisTraitement.MontantScenario) == string.Format("{0:f2}", newval))
                    _DevisTraitement.MontantDocteur = 0;


                //affectation sur les lignes
                List<TempEcheanceDefinition> TmpEcheancesDevis = new List<TempEcheanceDefinition>();
                List<CommTraitement> TmpActeTraitements = new List<CommTraitement>();

                List<TempEcheanceDefinition> ResultatEcheances = new List<TempEcheanceDefinition>();


                TmpEcheancesDevis = _DevisTraitement.echeancestemp;
                TmpActeTraitements = _DevisTraitement.actesTraitement;
                List<double> MontantsEcheancesLignes = new List<double>();
                List<double> MontantsEcheancesDevis = new List<double>();
                foreach (TempEcheanceDefinition de in TmpEcheancesDevis)
                {
                    MontantsEcheancesDevis.Add(de.Montant);
                }
                foreach (CommTraitement ct in TmpActeTraitements)
                {
                    MontantsEcheancesLignes.Add(ct.MontantLigne);
                }

                for (int t = 0; t <= MontantsEcheancesDevis.Count - 1; t++)
                {
                    for (int i = 0; i <= MontantsEcheancesLignes.Count - 1; i++)
                    {
                        if (MontantsEcheancesLignes[i] > 0)
                        {
                            double MontantEcheancesLigne = 0;
                            Boolean BreakFor = false;
                            if (MontantsEcheancesDevis[t] <= MontantsEcheancesLignes[i])
                            {
                                TempEcheanceDefinition tted = new TempEcheanceDefinition()
                                {
                                    acte = TmpEcheancesDevis[t].acte,
                                    AlreadyPayed = TmpEcheancesDevis[t].AlreadyPayed,
                                    CanRecalculate = TmpEcheancesDevis[t].CanRecalculate,
                                    DAteEcheance = TmpEcheancesDevis[t].DAteEcheance,
                                    Id = TmpEcheancesDevis[t].Id,
                                    IdSemestre = TmpEcheancesDevis[t].acte.Semestre,
                                    Libelle = TmpEcheancesDevis[t].Libelle,
                                    Montant = MontantsEcheancesDevis[t],
                                    ParPrelevement = TmpEcheancesDevis[t].ParPrelevement,
                                    ParVirement = TmpEcheancesDevis[t].ParVirement,
                                    payeur = TmpEcheancesDevis[t].payeur,


                                };
                                MontantsEcheancesDevis[t] = MontantsEcheancesDevis[t] - tted.Montant;
                                MontantsEcheancesLignes[i] = MontantsEcheancesLignes[i] - tted.Montant;
                                MontantEcheancesLigne = MontantEcheancesLigne + tted.Montant;

                                ResultatEcheances.Add(tted);
                                // ct.echeancestemp.Add(tted);
                                break;
                            }

                            else
                            {
                                TempEcheanceDefinition tted = new TempEcheanceDefinition()
                                {
                                    acte = TmpEcheancesDevis[t].acte,
                                    AlreadyPayed = TmpEcheancesDevis[t].AlreadyPayed,
                                    CanRecalculate = TmpEcheancesDevis[t].CanRecalculate,
                                    DAteEcheance = TmpEcheancesDevis[t].DAteEcheance,
                                    Id = TmpEcheancesDevis[t].Id,
                                    IdSemestre = TmpEcheancesDevis[t].acte.Semestre,
                                    Libelle = TmpEcheancesDevis[t].Libelle,
                                    Montant = MontantsEcheancesLignes[i],
                                    ParPrelevement = TmpEcheancesDevis[t].ParPrelevement,
                                    ParVirement = TmpEcheancesDevis[t].ParVirement,
                                    payeur = TmpEcheancesDevis[t].payeur,


                                };
                                MontantsEcheancesDevis[t] = MontantsEcheancesDevis[t] - tted.Montant;
                                MontantsEcheancesLignes[i] = MontantsEcheancesLignes[i] - tted.Montant;
                                ResultatEcheances.Add(tted);
                            }
                        }

                    }


                }
                double tot = 0;
                foreach (CommTraitement tact in _DevisTraitement.actesTraitement)
                {
                    tact.echeancestemp.Clear();
                    double TotEcheancesLignes = 0;
                    for (int i = 0; i <= ResultatEcheances.Count - 1; i++)
                    {
                        if (tact.MontantLigne > TotEcheancesLignes)
                        {
                            if (ResultatEcheances[i].IdSemestre == 0)
                            {
                                tact.echeancestemp.Add(ResultatEcheances[i]);
                                TotEcheancesLignes = TotEcheancesLignes + ResultatEcheances[i].Montant;
                                // ResultatEcheances[i].Montant = 0;
                                ResultatEcheances[i].IdSemestre = 1;
                            }
                        }
                        else
                            break;


                    }
                }
                foreach (CommTraitement ca in _DevisTraitement.actesTraitement)
                {
                    foreach (TempEcheanceDefinition ec in ca.echeancestemp)
                    {
                        //if (_DevisTraitement.MontantDocteur > 0)
                        //    ec.Montant = (double)(ec.Montant * (_DevisTraitement.MontantDocteur / _DevisTraitement.Montant));


                        MgmtDevis.update_tempechenaces_tk(ec, ca);
                    }

                }
                //fon affectation sur les lignes

                BuildPnlDevis();
                Initdgv(_DevisTraitement.actesTraitement, false);
                //   }
            }

        }
        private void BuildPnlDevis()
        {

            //double total = 0;
            //double totalavantremise = 0;
            //foreach (ActePGPropose acte in devis.actesHorstraitement)
            //{
            //    total += acte.Montant;
            //    totalavantremise += acte.MontantAvantRemise;

            //}

            TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.Montant);
            TxtPrixTotal_AvantRemise.Text = string.Format("{0:f2}", _DevisTraitement.MontantAvantRemise);

            //TxtPrixTotal_AvantRemise.Visible = true;
            TxtPrixAvR_Docteur.Visible = true;
            if (_DevisTraitement.MontantDocteur == 0)
            {
                TxtPrixAvR_Docteur.Text = string.Format("{0:f2}", _DevisTraitement.Montant);

            }
            MgmtDevis.UpdateDevis_TK(_DevisTraitement);
            //else
            //if (TxtPrixTotal.Text != string.Format("{0:f2}", _DevisTraitement.MontantDocteur) && (_DevisTraitement .MontantDocteur > 0))
            //{
            //    //TxtPrixTotal_AvantRemise.Visible = true;
            //    TxtPrixTotal_AvantRemise.Text = string.Format("{0:f2}", TxtPrixTotal.Text );
            //    TxtPrixTotal.Text = string.Format("{0:f2}", _DevisTraitement.MontantDocteur); 
            //}



            //if ((devis.MontantAvantRemise != null) && (devis.Montant != null) && (devis.MontantAvantRemise != devis.Montant))
            //{
            //    lblTotalAvantRemise.Text = total.ToString("C2");
            //    lblTotalAvantRemise.Visible = true;
            //    lblTotal.Text = devis.Montant.Value.ToString("C2");
            //}
            //else
            //    lblTotalAvantRemise.Visible = false;

            //InitDisplayEcheances();            

        }

        private void Imprimer_Devis_Click(object sender, EventArgs e)
        {






            Devis_TK TmpDevis_TK = new Devis_TK();
            int id = Convert.ToInt32(_DevisTraitement.Id);
            //  TmpDevis_TK = (Devis_TK)dgvDevis.SelectedRows[0].Tag;

            MgmtDevis.GetCommDevis(ref TmpDevis_TK);
            Double vMontantDevis = 0;
            Double vMontantDevisAvantRemise = 0;
            /*
            foreach (CommTraitement ct in TmpDevis_TK.actesTraitement)
            {

                ct.ActesSupp = MgmtDevis.GetCommActeSupDevis(ct);
                ct.Radios = MgmtDevis.GetCommActeSupDevis(ct, "R");
                ct.photos = MgmtDevis.GetCommActeSupDevis(ct, "P");
                ct.Materiels = MgmtDevis.GetCommMaterielsDevis(ct);
                ct.AutrePersonnes = MgmtDevis.GetDevisAutrePersonne(ct);
                ct.echeancestemp = MgmtDevis.get_tempecheances_TK(ct);
                vMontantDevis = vMontantDevis + ct.MontantLigne;
                vMontantDevisAvantRemise = vMontantDevisAvantRemise + ct.MontantLigneAvantRemise;
                //echecances
                ct.echeancestemp = MgmtDevis.get_tempecheances_TK(ct);
                if (ct.echeancestemp.Count == 0)
                {

                    TempEcheanceDefinition ted = new TempEcheanceDefinition();
                    ted.DAteEcheance = ct.devis.DatePrevisionnelDeDebutTraitement.AddMonths(6);
                    ted.Montant = ct.prix;
                    ted.Libelle = "Ligne : " + ct.Acte.acte_libelle;
                    ted.acte = ap;
                    ted.acte.Libelle = ct.Acte.acte_libelle;
                    //ted.acte = com.acte;
                    ted.AlreadyPayed = false;
                    ted.payeur = Echeance.typepayeur.patient;
                    ct.echeancestemp.Add(ted);
                }

            }
            TmpDevis_TK.MontantAvantRemise = vMontantDevisAvantRemise;
            TmpDevis_TK.Montant = vMontantDevis;
           
            //foreach (CommTraitement ct in TmpDevis_TK.actesTraitement)
            //{
            //    if (ct.echeancestemp == null || ct.echeancestemp.Count == 0)
            //    {
            //        ct.echeancestemp = MgmtDevis.get_tempecheances_TK(ct);
            //    }
            //}
            FrmImpression frmImp = new FrmImpression(TmpDevis_TK);
            frmImp.ShowDialog();
        }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {


        }

        private void dataGridView1_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.EndEdit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int l = ((Timer)sender).Interval * 10;
            for (int i = 1; i <= l; i++)
                Console.WriteLine("just wait");
            timer1.Enabled = false;
        }

        private void FrmChoixActes_FormClosed(object sender, FormClosedEventArgs e)
        {


        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (DevisTraitement != null)
            {
                if (CurrentPatient.infoscomplementaire == null)
                    CurrentPatient.infoscomplementaire = baseMgmtPatient.getinfocomplementaire(CurrentPatient.Id);
            }
            FRmWizardNewRDV frm = new FRmWizardNewRDV(CurrentPatient, true);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                NewTraitement _Traitement = new NewTraitement();
                _Traitement = TraitementsMgmt.GetFullTraitement(frm.traitementselected.id_Traitement);

                TraitementsMgmt.GetCommTraitements(ref _Traitement);

                int pp = 0;
                ActePG ap = new ActePG();
                Devis_TK d = null;
                DateTime TmpDate = new DateTime();
                if (frm.OtherDate.HasValue)
                {
                    TmpDate = frm.OtherDate.Value;

                }
                else
                {
                    if ((frm.NbMoisFromNow == -1) || (frm.NbJoursFromNow == -1))
                    {
                        TmpDate = DateTime.Now.Date.AddHours(22);

                    }
                    else
                    {
                        TmpDate = DateTime.Now.AddMonths(frm.NbMoisFromNow).AddDays(frm.NbJoursFromNow);


                    }
                }
                FrmChoixActes frmActes = new FrmChoixActes(_Traitement.id_Traitement, _Traitement.Traitement_libelle, ref d, true, 1, false, true, CurrentPatient);
                if (frmActes.ShowDialog() == DialogResult.OK)
                {
                    bool verifEcheance = false;

                    if (frmActes.Value[0].echeancestemp.Count == 0)
                        verifEcheance = true;


                    foreach (CommTraitement ct in frmActes.Value)
                    {
                        if (DevisTraitement != null)
                            DevisTraitement.actesTraitement.Add(ct);
                        else
                        {
                            TraitementsMgmt.AddCommTraitements(_Traitement.id_Traitement, ct);
                            _Traitement.CommTraitement.Add(ct);
                        }
                    }
                    if (_DevisTraitement != null)
                    {
                        DevisTraitement.actesTraitement = DevisTraitement.actesTraitement.OrderBy(w => w.DatePrevisionnnelle).ToList();
                        Initdgv(DevisTraitement.actesTraitement, false);
                    }
                    else
                        Initdgv(_Traitement.CommTraitement, false);
                }
            }
        }

        private void btnGrpActes_Ok_Click(object sender, EventArgs e)
        {
            List<ActeGroupement> ac = new List<ActeGroupement>();
            FrmActeTraitement frm = new FrmActeTraitement();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //if (dataGridView1.Rows.Count < 1)
                //{
                //    return;
                //}
                com = ((CommTraitement)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Tag);
                //if (com == null) return;
                foreach (ActeGroupement acg in frm.acteg)
                {
                  //  com.Acte.acte_libelle=
                   com.ActesSupp.Add(new CommActesTraitement(acg));
                }
                if (DevisTraitement == null)
                {
                    TraitementsMgmt.SaveActesSupp(com, "");
                    Initdgv(_Traitement.CommTraitement, false);
                }
                else
                    Initdgv(DevisTraitement.actesTraitement, false);

            }
        }

       



    }

}