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
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace BaseCommonControls
{
    public partial class FrmWizardPropositions : Form
    {

        List<int> lstDuree;


        string _ChoixFamille;

        private Devis _devisALaCarte = null;
        public Devis devisALaCarte
        {
            get
            {
                return _devisALaCarte;
            }
            set
            {
                _devisALaCarte = value;
            }
        }


        DateTime _DateDeFin;
        public DateTime DateDeFin
        {
            get
            {
                return _DateDeFin;
            }
            set
            {
                _DateDeFin = value;
            }
        }

        public DateTime DateDeDebut
        {
            get
            {
                return dtpdebuttrmnt.Value;
            }
            set
            {
                dtpdebuttrmnt.Value = value;
            }
        }

        private string _TitreDevis = "";
        public string TitreDevis
        {
            get
            {
                return _TitreDevis;
            }
            set
            {
                _TitreDevis = value;
            }
        }
        private string _localisationAnatomique = "";
        public string localisationAnatomique
        {
            get
            {
                return _localisationAnatomique;
            }
            set
            {
                _localisationAnatomique = value;
            }
        }
        private int _Duree = 1;
        public int Duree
        {
            get
            {
                return _Duree;
            }
            set
            {
                _Duree = value;
            }
        }

        private ScenarioCommClinique _scenarioSelected;
        public ScenarioCommClinique scenarioSelected
        {
            get
            {
                return _scenarioSelected;
            }
            set
            {
                _scenarioSelected = value;
            }
        }

        private NewTraitement _Value;
        public NewTraitement Value
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

        private List<CommTraitement> _comms;
        public List<CommTraitement> comms
        {
            get
            {
                return _comms;
            }
            set
            {
                _comms = value;
            }
        }



        private int _nbsemEntameHorCabinet = 0;
        public int nbsemEntameHorCabinet
        {
            get
            {
                return _nbsemEntameHorCabinet;
            }
            set
            {
                _nbsemEntameHorCabinet = value;
            }
        }



        private BasCommon_BO.Devis.enumtypePropositon _tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Aucun;
        public BasCommon_BO.Devis.enumtypePropositon tpetrmnt
        {
            get
            {
                return _tpetrmnt;
            }
            set
            {
                _tpetrmnt = value;
            }
        }

        private List<ActePGPropose> _ActesMateriel = new List<ActePGPropose>();
        public List<ActePGPropose> ActesMateriel
        {
            get
            {
                return _ActesMateriel;
            }
            set
            {
                _ActesMateriel = value;
            }
        }



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

        private List<Proposition> _value = new List<Proposition>();
        public List<Proposition> value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        bool show = true;
        public FrmWizardPropositions(int NombreSemestresEntameHorsCabinet, basePatient pat, int MultiChoix = 0, string ChoixFamille = "", bool showPanelTypeTraitement = true)
        {
            _ChoixFamille = ChoixFamille;
            nbsemEntameHorCabinet = NombreSemestresEntameHorsCabinet;
            CurrentPatient = pat;
            InitializeComponent();

            DateDeDebut = DateTime.Now.AddDays(15);

            nbaligneurchange = new System.EventHandler(this.txtbxNbAligneurs_ValueChanged);
            InitDisplay();
            ShowPanel(pnlTypeTraitement);
            //}
            //else
            //{
            //    treeviewTraitements.Visible = false;
            //    this.Height = 300;
            //    this.StartPosition = FormStartPosition.CenterScreen;
            //}

        }


        private void ShowPanel(Panel pnl)
        {

            ShowPanel(pnl, true);
        }

        Stack<Panel> histo = new Stack<Panel>();
        Panel currentpnl = null;

        private void ShowPanel(Panel pnl, bool withhisto)
        {
            if (withhisto)
                if (currentpnl != null) histo.Push(currentpnl);

            currentpnl = pnl;
            foreach (Panel pnlTohide in pnlContainer.Controls)
                pnlTohide.Hide();

            if (pnl.Tag is string)
                Text = (string)pnl.Tag;

            pnl.Show();
        }

        private void BackPanel()
        {
            if (histo.Count == 1) treeviewTraitements.Visible = true;
            if (histo.Count == 0) return;
            Panel pnl = (Panel)histo.Pop();
            ShowPanel(pnl, false);
            //if (!show)
            //{
            //    pnlTypeTraitement.Visible = false;
            //}
            //else
            //{
            //    treeviewTraitements.Visible = false;

            //}

        }

        private void FrmWizardPropositions_Load(object sender, EventArgs e)
        {

            //ShowPanel(pnlChoixProposition);
            //PropositionMgmt.GetAllTypeTraitements();
            //int XX = 10;
            //int YY = 10;
            //foreach (NewTraitement t in PropositionMgmt.GetAllTypeTraitements())
            //{
            //    Button btn = new Button() ;
            //    btn.Text  = t.Traitement_libelle ;
            //    btn.Top = XX;
            //    btn.Left = YY;
            //    btn.BackColor = t.Traitement_couleur;
            //    btn.UseVisualStyleBackColor = false;
            //    btn.Width = 111;
            //    btn.Height = 95;

            //    btn.Font = new Font("Garamond", 12);
            //    btn.TextAlign = ContentAlignment.MiddleCenter;
            //    btn.FlatStyle = FlatStyle.Flat;
            //    panelTraitements.Controls.Add(btn);
            //    YY = YY + 150;
            //}

        }
        private void RefreshFamilies(TreeNodeCollection collec, FamillesTraitements f)
        {


            TreeNode familynode = collec.Add(f.libelle);
            familynode.Tag = f;


            foreach (FamillesTraitements fa in f.ChildFamillesTraitement)
                RefreshFamilies(familynode.Nodes, fa);
        }
        private void AffecteTraitementsToNode(TreeNodeCollection nodecollec)
        {
            foreach (TreeNode node in nodecollec)
            {

                if (node.Tag is FamillesTraitements)
                {
                    foreach (NewTraitement a in TraitementsMgmt.Traitements)
                    {
                        if (a.famille_Traitement != null)
                        {
                            FamillesTraitements TF = new FamillesTraitements();
                            TF = (FamillesTraitements)node.Tag;
                            if (a.famille_Traitement.libelle.Trim() == TF.libelle.Trim() && a.famille_Traitement.ordre == TF.ordre)


                            //if (a.famille_Acte == null) NonAffectedExist = true;
                            //if (a.famille_Traitement == node.Tag)
                            {
                                TreeNode TraitementNode = node.Nodes.Add(a.Traitement_shortlib + "\n" + a.Montant_Scenario );
                                TraitementNode.Tag = a;
                            }
                        }
                    }
                    AffecteTraitementsToNode(node.Nodes);
                }
            }
        }





        private void RefreshTraitements()
        {

            treeviewTraitements.Clear();


            TraitementsMgmt.famillesTraitement = null;
            foreach (FamillesTraitements f in TraitementsMgmt.famillesTraitement)
                if (f.ParentFamillesTraitementId == -1)
                {
                    if (f.typeFamilleTraitement == TypeFamilleTraitement.Senario)
                        RefreshFamilies(treeviewTraitements.Root.Nodes, f);
                }

            AffecteTraitementsToNode(treeviewTraitements.Root.Nodes);

            foreach (NewTraitement a in TraitementsMgmt.Traitements)
            {
                //if (a.famille_Acte == null) NonAffectedExist = true;
                if ((a.famille_Traitement == null) && (a.id_Traitement != -1))
                {
                    TreeNode TraitementNode = treeviewTraitements.Root.Nodes.Add(a.Traitement_libelle);
                    TraitementNode.Tag = a;

                }
            }

            treeviewTraitements.RecalculEmplacementButtons(true);
            if (_ChoixFamille != "")
            {
                foreach (BaseCommonControls.Ctrls.BO.trButton b in treeviewTraitements.ButtonList)
                {
                    if (b.Text.Trim().ToUpper() == _ChoixFamille.Trim().ToUpper())
                    {
                        treeviewTraitements.CurrentNode = ((TreeNode)b.Tag);
                    }
                }
                treeviewTraitements.RecalculEmplacementButtons(true);
            }
        }
        private void InitDisplay()
        {
            RefreshTraitements();

            treeviewTraitements.ButtonPaint += new PaintEventHandler(PaintTraitements);
            treeviewTraitements.HeaderPaint += new PaintEventHandler(PaintHeader);
            //treeviewTraitements.Width = this.Width;
            if (treeviewTraitements.MultiChoiceVisibilite)
            {

                /*  foreach (object comm in comms)
                  {

                      CommClinique test = (CommClinique)comm;
                      NewTraitement _Traitement = new NewTraitement();
                      _Traitement = TraitementsMgmt.GetFullTraitement(test.Id );
                      treeviewTraitements.Alimenter_listBox(_Traitement.Traitement_libelle, test.Id + "$" + _Traitement.Traitement_libelle + "$" + _Traitement.Parent + "$" + _Traitement.Traitement_couleur.A + "$" + _Traitement.Traitement_couleur.R + "$" + _Traitement.Traitement_couleur.G + "$" + _Traitement.Traitement_couleur.B + "$" + _Traitement.Traitement_libelle , false);
                  }*/
            }
        }
        private void PaintHeader(object sender, PaintEventArgs e)
        {

            string Title = "";
            TreeNode node = ((BaseCommonControls.Ctrls.TreeViewIcon)sender).CurrentNode;
            TreeNode parent = node;

            while (parent != null)
            {
                if (Title != "") Title = "/" + Title;
                Title = parent.Text + Title;
                parent = parent.Parent;
            }

            Color cl = Color.WhiteSmoke;
            if (node.Tag is FamillesTraitements)
                cl = ((FamillesTraitements)node.Tag).couleur;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // get the corner path
            GraphicsPath path;
            // get path

            path = BaseCommonControls.Ctrls.BO.GraphicUtils.CreateRoundedRectanglePath(e.ClipRectangle, 5);
            e.Graphics.FillPath(new SolidBrush(cl), path);
            e.Graphics.DrawPath(new Pen(Brushes.Black), path);

            // little transparent
            Color start = Color.FromArgb(255, cl);
            Color end = Color.FromArgb(180, cl);

            if (e.ClipRectangle.Width > 0)
                using (LinearGradientBrush aGB = new LinearGradientBrush(e.ClipRectangle, start, end, LinearGradientMode.Vertical))
                    e.Graphics.FillPath(aGB, path);


            Font ft = new Font("Segoe UI", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            PointF txts = new PointF(e.Graphics.MeasureString(Title, ft).Width, e.Graphics.MeasureString(node.Text, ft).Height);
            e.Graphics.DrawString(Title, ft, Brushes.Black, new PointF(e.ClipRectangle.X + (e.ClipRectangle.Width - txts.X) / 2, e.ClipRectangle.Y + e.ClipRectangle.Height - txts.Y - 2));


        }

        private void PaintTraitements(object sender, PaintEventArgs e)
        {

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.Word;

            TreeNode node = ((TreeNode)sender);
            Color cl = Color.WhiteSmoke;

            bool IsSelected = false;

            IsSelected = (node == treeviewTraitements.SelectedNode);

            if (node.Tag is NewTraitement)
                cl = ((NewTraitement)node.Tag).Traitement_couleur;
            if (node.Tag is FamillesTraitements)
                cl = ((FamillesTraitements)node.Tag).couleur;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // get the corner path
            GraphicsPath path;
            // get path
            if (node.Tag is FamillesTraitements)
            {
                Rectangle r = e.ClipRectangle;
                r.X += 2;
                r.Y += 2;
                path = BaseCommonControls.Ctrls.BO.GraphicUtils.CreateRoundedRectanglePath(r, 5);
                e.Graphics.DrawPath(new Pen(Brushes.Black, 1), path);
            }

            path = BaseCommonControls.Ctrls.BO.GraphicUtils.CreateRoundedRectanglePath(e.ClipRectangle, 5);
            e.Graphics.DrawPath(new Pen(Brushes.Black, 1), path);

            // little transparent
            Color start = Color.FromArgb(255, cl);
            Color end = Color.FromArgb(180, cl);

            if (e.ClipRectangle.Width > 0)
                using (LinearGradientBrush aGB = new LinearGradientBrush(e.ClipRectangle, start, end, LinearGradientMode.Vertical))
                    e.Graphics.FillPath(aGB, path);
            if (IsSelected)
                e.Graphics.DrawPath(new Pen(Brushes.GreenYellow, 5), path);





            float fl = 12;
            Font ft = new Font("Segoe UI", fl, FontStyle.Regular, GraphicsUnit.Pixel);
            SizeF s = e.Graphics.MeasureString(node.Text, ft, e.ClipRectangle.Size, sf);


            while (s.Width > e.ClipRectangle.Width)
            {
                fl--;
                if (fl < 1) { fl = 2; break; }
                ft = new Font("Segoe UI", fl, FontStyle.Regular, GraphicsUnit.Pixel);
                s = e.Graphics.MeasureString(node.Text, ft);

            }

            e.Graphics.DrawString(node.Text, ft, Brushes.Black, e.ClipRectangle, sf);



        }
        private void NewBuild()
        {

            try
            {
                value.Clear();
                ActesMateriel.Clear();


                #region Sucette

                if (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Pediatrie)
                {
                    value.Add(PropositionMgmt.BuildSucette(CurrentPatient, dtpdebuttrmnt.Value, Duree));
                }
                //cas du nouveau Devis
                if (_Value is NewTraitement)
                {
                    Proposition p = new Proposition();
                    NewTraitement _Traitement = new NewTraitement();
                    _Traitement = TraitementsMgmt.GetFullTraitement(_Value.id_Traitement);
                    TraitementsMgmt.GetCommTraitements(ref _Traitement);
                    //if (_Traitement.CommTraitement.Count == 0)
                    //{

                    //    MessageBox.Show("Scénario vide, veuillez choisir un ature!");

                    //    return;
                    //}
                    //p.DateEvenement = _Value .;
                    //p.DateProposition = Convert.ToDateTime(p.DateEvenement);
                    p.patient = CurrentPatient;
                    p.IdPatient = CurrentPatient.Id;
                    p.DateAcceptation = dtpdebuttrmnt.Value;
                    p.DateProposition = dtpdebuttrmnt.Value;
                    p.libelle = "Proposition/Scénario " + _Traitement.Traitement_libelle;
                    foreach (CommTraitement cc in _Traitement.CommTraitement)
                    {

                        Traitement tt;
                        tt = new Traitement();
                        tt.Libelle = cc.Acte.acte_libelle;
                        Semestre s = new Semestre();
                        s.CodeSemestre = "Semestre Ligne " + cc.Acte.acte_libelle; ;
                        s.DateDebut = p.DateProposition;
                        s.DateFin = s.DateDebut.AddMonths(6);
                        s.Montant_Honoraire = cc.prix;
                        s.Montant_AvantRemise = s.Montant_Honoraire;
                        //s.traitementSecu = tmplte;
                        //s.Parent = t;
                        s.NumSemestre = 1;
                        tt.semestres.Add(s);
                        p.traitements.Add(tt);

                    }
                    value.Add(p);


                }
                #endregion

                #region Orthopedie

                if (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Orthopedie)
                {

                    int NbSemVoulu = Duree;
                    int nbsemEnt = PropositionMgmt.NbSemestreEntames(CurrentPatient.propositions);


                    if (nbsemEnt + NbSemVoulu + nbsemEntameHorCabinet > 6)
                        MessageBox.Show("Attention Il y a plus de 6 semestres Remboursés\n Au dela du Sixieme semestre, les semestres ne seront pas remboursés", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                    value.Add(PropositionMgmt.BuildOrthopedie(nbsemEntameHorCabinet, nbsemEnt, CurrentPatient, dtpdebuttrmnt.Value, Duree));
                    value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;

                    TemplateActePG actepg = TemplateApctePGMgmt.getTemplatesActeGestion("MATRCC");
                    if (actepg != null)
                    {

                        ActePGPropose ActeM = new ActePGPropose();
                        ActeM.Qte = 1;
                        ActeM.DateExecution = dtpdebuttrmnt.Value.AddDays(15);
                        ActeM.template = actepg;
                        ActeM.Libelle = ActeM.template.Libelle;

                        ActeM.Montant = ActeM.template.Valeur;
                        ActeM.MontantAvantRemise = ActeM.Montant;

                        ActesMateriel.Add(ActeM);
                    }
                }

                #endregion

                #region Adultes

                if ((tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Adulte) || (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.FinitionAdulte))
                {

                    if (rbAdulteMBC.Checked)
                    {
                        value.Add(PropositionMgmt.BuildMBC_Adulte(CurrentPatient, dtpdebuttrmnt.Value, Duree));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }

                    if (rbAdulteMBL.Checked)
                    {
                        value.Add(PropositionMgmt.BuildMBL_Adulte(CurrentPatient, dtpdebuttrmnt.Value, Duree));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }


                    if (rbAdulteMBM.Checked)
                    {
                        value.Add(PropositionMgmt.BuildMBM_Adulte(CurrentPatient, dtpdebuttrmnt.Value, Duree));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }

                    if (chkbxSetup.Checked)
                    {
                        value.Add(PropositionMgmt.BuildSetup3D_Adulte(CurrentPatient, dtpdebuttrmnt.Value));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }

                    if (chkbxiSeven.Checked)
                    {
                        value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ISEVEN, CurrentPatient, dtpdebuttrmnt.Value, Duree));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }

                    if (rbInvArcade.Checked)
                    {

                        value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVARCADE, CurrentPatient, dtpdebuttrmnt.Value, Duree));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }
                    if (rbLight.Checked)
                    {
                        value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVLIGHT, CurrentPatient, dtpdebuttrmnt.Value, Duree));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }
                    if (rbCompl.Checked)
                    {
                        value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVCOMPLET, CurrentPatient, dtpdebuttrmnt.Value, Duree));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }
                    if (rbComplCorr.Checked)
                    {
                        value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVCOMPLETCORR, CurrentPatient, dtpdebuttrmnt.Value, Duree));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }

                    if (rbComplCorrTIM.Checked)
                    {
                        value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVCOMPLETCORRTIM, CurrentPatient, dtpdebuttrmnt.Value, Duree));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }

                    if (rbComplDisj.Checked)
                    {
                        value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVCOMPLETDISJ, CurrentPatient, dtpdebuttrmnt.Value, Duree));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }
                    if (rbComplChir.Checked)
                    {
                        value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVCOMPLETCHIR, CurrentPatient, dtpdebuttrmnt.Value, Duree));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }
                    if (rbComplDisjChir.Checked)
                    {
                        value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEINVCOMPLETDISJCHIR, CurrentPatient, dtpdebuttrmnt.Value, Duree));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }

                    if (chkbxFinition.Checked)
                    {
                        value.Add(PropositionMgmt.BuildINV_Adulte(CodesTraitement.ORTHODONTIEADULTEFINITION, CurrentPatient, dtpdebuttrmnt.Value, Duree));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                        tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.FinitionAdulte;
                    }

                }
                #endregion

                #region Contention Adultes

                if (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.ContentionAdulte)
                {
                    ActePGPropose mat = null;


                    if (chkbxGBM.Checked)
                    {
                        List<ActePGPropose> matos = new List<ActePGPropose>();

                        Proposition p = PropositionMgmt.BuildContentionAdulteGBM(CurrentPatient, dtpdebuttrmnt.Value, (int)NbGBM.Value);
                        value.Add(p);
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }

                    if (chkbxVIVERA.Checked)
                    {
                        List<ActePGPropose> matos = new List<ActePGPropose>();


                        Proposition p = PropositionMgmt.BuildContentionAdulteVIVERA(CurrentPatient, dtpdebuttrmnt.Value, (int)nbVIVERA.Value);
                        value.Add(p);
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }

                    if (chkbxFil33AcierbasGBM.Checked)
                    {
                        List<ActePGPropose> matos = new List<ActePGPropose>();

                        Proposition p = PropositionMgmt.BuildContentionAdulteFil33AcierGBM(CurrentPatient, dtpdebuttrmnt.Value, (int)nbGBMPourFilAcier.Value);
                        value.Add(p);
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }

                    if (chkbxFil33OrbasGBM.Checked)
                    {
                        List<ActePGPropose> matos = new List<ActePGPropose>();


                        Proposition p = PropositionMgmt.BuildContentionAdulteFil33OrGBM(CurrentPatient, dtpdebuttrmnt.Value, (int)nbGBMPourFilOr.Value);
                        value.Add(p);
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }

                    if (chkbxFil33Acier.Checked)
                    {
                        List<ActePGPropose> matos = new List<ActePGPropose>();


                        Proposition p = PropositionMgmt.BuildContentionAdulteFil33Acier(CurrentPatient, dtpdebuttrmnt.Value);
                        value.Add(p);
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }

                    if (chkbxFil33Or.Checked)
                    {
                        List<ActePGPropose> matos = new List<ActePGPropose>();


                        Proposition p = PropositionMgmt.BuildContentionAdulteFil33Or(CurrentPatient, dtpdebuttrmnt.Value);
                        value.Add(p);
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                    }

                }
                #endregion

                #region Orthodontie

                if (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Orthodontie)
                {
                    int NbSemVoulu = Duree;
                    int nbsemEnt = PropositionMgmt.NbSemestreEntames(CurrentPatient.propositions);


                    if (nbsemEnt + NbSemVoulu + nbsemEntameHorCabinet > 6)
                        MessageBox.Show("Attention Il y a plus de 6 semestres Remboursés\n Au dela du Sixieme semestre, les semestres ne seront pas remboursés", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    bool needRCC = false;
                    bool needInvi = false;

                    if (chkbxMBC.Checked)
                    {
                        value.Add(PropositionMgmt.BuildMBC(nbsemEntameHorCabinet, nbsemEnt + 1, CurrentPatient, dtpdebuttrmnt.Value, NbSemVoulu));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                        needRCC = true;
                    }

                    if (chkbxMBL.Checked)
                    {
                        value.Add(PropositionMgmt.BuildMBL(nbsemEntameHorCabinet, nbsemEnt + 1, CurrentPatient, dtpdebuttrmnt.Value, NbSemVoulu));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                        needRCC = true;
                    }


                    if (chkbxMBM.Checked)
                    {
                        value.Add(PropositionMgmt.BuildMBM(nbsemEntameHorCabinet, nbsemEnt + 1, CurrentPatient, dtpdebuttrmnt.Value, NbSemVoulu));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                        needRCC = true;
                    }


                    if (chkbxINV.Checked)
                    {
                        value.Add(PropositionMgmt.BuildINVTEEN(nbsemEntameHorCabinet, nbsemEnt + 1, CurrentPatient, dtpdebuttrmnt.Value, NbSemVoulu));
                        value[value.Count - 1].IdScenario = scenarioSelected == null ? -1 : scenarioSelected.Id;
                        needInvi = true;
                    }

                    DateTime? dte = PropositionMgmt.GetFinTrrtmntForAll(value);


                    Proposition cont1 = chkbxINV.Checked ? PropositionMgmt.BuildContentionInvisalign1(CurrentPatient, dte.Value) : PropositionMgmt.BuildContention1(CurrentPatient, dte.Value);
                    dte = PropositionMgmt.GetFinTrrtmnt(cont1);
                    value.Add(cont1);


                    Proposition cont2 = chkbxINV.Checked ? PropositionMgmt.BuildContentionInvisalign2(CurrentPatient, dte.Value) : PropositionMgmt.BuildContention2(CurrentPatient, dte.Value);

                    value.Add(cont2);
                    if (needRCC)
                    {
                        TemplateActePG actepg = TemplateApctePGMgmt.getTemplatesActeGestion("MATORTH");
                        if (actepg != null)
                        {

                            ActePGPropose ActeM = new ActePGPropose();
                            ActeM.Qte = 1;
                            ActeM.DateExecution = dtpdebuttrmnt.Value.AddDays(15);
                            ActeM.template = actepg;
                            ActeM.Libelle = ActeM.template.Libelle;

                            ActeM.Montant = ActeM.template.Valeur;
                            ActeM.MontantAvantRemise = ActeM.Montant;

                            ActesMateriel.Add(ActeM);
                        }
                    }

                    if (needInvi)
                    {
                        ActePGPropose ActeM = new ActePGPropose();
                        ActeM.Qte = 1;
                        ActeM.DateExecution = dtpdebuttrmnt.Value.AddDays(15);
                        ActeM.template = TemplateApctePGMgmt.getTemplatesActeGestion("MATINVI");
                        ActeM.Libelle = ActeM.template.Libelle;

                        ActeM.Montant = ActeM.template.Valeur;
                        ActeM.MontantAvantRemise = ActeM.Montant;

                        ActesMateriel.Add(ActeM);
                    }
                }
                #endregion

                ActePGPropose ActeMateriel;


                #region Empreintes
                if ((value.Count > 0) && (CodesTraitement.IsInvisalign((value[value.Count - 1].traitements[0].CodeTraitement))) && (CodesTraitement.IsAdulte((value[value.Count - 1].traitements[0].CodeTraitement))))
                {

                    if (chkbxEmpreinteOptique.Checked)
                    {
                        ActeMateriel = new ActePGPropose();
                        ActeMateriel.DateExecution = dtpdebuttrmnt.Value.AddDays(15);
                        ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("EMPOPTI");

                        ActeMateriel.Libelle = ActeMateriel.template.Libelle;

                        ActeMateriel.Montant = ActeMateriel.template.Valeur;


                        ActeMateriel.MontantAvantRemise = ActeMateriel.Montant;
                        ActeMateriel.Optionnel = chkbxEmpreinteOptique.Checked;
                        ActeMateriel.Qte = 1;

                        ActesMateriel.Add(ActeMateriel);
                    }

                    if (chkbxEmpreinteSilicone.Checked)
                    {
                        ActeMateriel = new ActePGPropose();
                        ActeMateriel.DateExecution = dtpdebuttrmnt.Value.AddDays(15);

                        ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("EMP");


                        ActeMateriel.Libelle = ActeMateriel.template.Libelle;

                        ActeMateriel.Montant = ActeMateriel.template.Valeur;


                        ActeMateriel.MontantAvantRemise = ActeMateriel.Montant;
                        ActeMateriel.Optionnel = chkbxEmpreinteOptique.Checked;
                        ActeMateriel.Qte = 1;

                        ActesMateriel.Add(ActeMateriel);
                    }
                }
                #endregion

                #region options



                if (chkbxRECDECOMP.Checked)
                {
                    ActeMateriel = new ActePGPropose();
                    ActeMateriel.Qte = (int)txtbxQteRECDECOMP.Value;
                    ActeMateriel.DateExecution = dtpdebuttrmnt.Value.AddDays(15);
                    ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("RECDECOMP");
                    ActeMateriel.Libelle = ActeMateriel.template.Libelle;

                    ActeMateriel.Montant = ActeMateriel.template.Valeur;
                    ActeMateriel.MontantAvantRemise = ActeMateriel.Montant;

                    ActesMateriel.Add(ActeMateriel);
                }

                if (chkbxRECDESOUS.Checked)
                {
                    ActeMateriel = new ActePGPropose();
                    ActeMateriel.Qte = (int)txtbxQteRECDESOUS.Value;
                    ActeMateriel.DateExecution = dtpdebuttrmnt.Value.AddDays(15);
                    ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("RECDESOU");
                    ActeMateriel.Libelle = ActeMateriel.template.Libelle;

                    ActeMateriel.Montant = ActeMateriel.template.Valeur;
                    ActeMateriel.MontantAvantRemise = ActeMateriel.Montant;

                    ActesMateriel.Add(ActeMateriel);
                }

                if (chkbxRECDEADD.Checked)
                {
                    ActeMateriel = new ActePGPropose();
                    ActeMateriel.Qte = (int)txtbxQteRECDEADD.Value;
                    ActeMateriel.DateExecution = dtpdebuttrmnt.Value.AddDays(15);
                    ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("RECDEADD");
                    ActeMateriel.Libelle = ActeMateriel.template.Libelle;

                    ActeMateriel.Montant = ActeMateriel.template.Valeur;
                    ActeMateriel.MontantAvantRemise = ActeMateriel.Montant;

                    ActesMateriel.Add(ActeMateriel);
                }



                //On rajoute une empreinte Invisalign pour tous les traitements invisalign


                if (chkbxminivis.Checked)
                {
                    ActeMateriel = new ActePGPropose();
                    ActeMateriel.Qte = 1;
                    ActeMateriel.DateExecution = dtpdebuttrmnt.Value;
                    ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("MINIVIS");
                    ActeMateriel.Libelle = ActeMateriel.template.Libelle;

                    ActeMateriel.Montant = ActeMateriel.template.Valeur;
                    ActeMateriel.MontantAvantRemise = ActeMateriel.Montant;

                    ActesMateriel.Add(ActeMateriel);

                    ActeMateriel = new ActePGPropose();
                    ActeMateriel.Qte = (int)txtbxQteMinivis.Value;
                    ActeMateriel.DateExecution = dtpdebuttrmnt.Value;
                    ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("MATMINIVIS");
                    ActeMateriel.Libelle = ActeMateriel.template.Libelle;

                    ActeMateriel.Montant = ActeMateriel.template.Valeur;
                    ActeMateriel.MontantAvantRemise = ActeMateriel.Montant;

                    ActesMateriel.Add(ActeMateriel);
                }

                if (chkbxEclairssissment.Checked)
                {
                    ActeMateriel = new ActePGPropose();
                    ActeMateriel.Qte = (int)txtbxQteEclair.Value;
                    ActeMateriel.DateExecution = dtpdebuttrmnt.Value;
                    ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("KITBLANCHI");
                    ActeMateriel.Libelle = ActeMateriel.template.Libelle;

                    ActeMateriel.Montant = ActeMateriel.template.Valeur;
                    ActeMateriel.MontantAvantRemise = ActeMateriel.Montant;



                    ActesMateriel.Add(ActeMateriel);
                }




                if (chkbxfacette.Checked)
                {
                    ActeMateriel = new ActePGPropose();
                    ActeMateriel.Qte = 1;
                    ActeMateriel.DateExecution = dtpdebuttrmnt.Value.AddDays(7);
                    ActeMateriel.template = TemplateApctePGMgmt.getTemplatesActeGestion("OPTFACETTE");
                    ActeMateriel.Libelle = ActeMateriel.template.Libelle;


                    ActeMateriel.Montant = ActeMateriel.template.Valeur;
                    ActeMateriel.MontantAvantRemise = ActeMateriel.Montant;



                    ActesMateriel.Add(ActeMateriel);
                }




                #endregion

                if ((value.Count == 0) & (ActesMateriel.Count > 0))
                {
                    Proposition p = new Proposition();
                    p.DateProposition = DateTime.Now;
                    p.patient = CurrentPatient;
                    p.DateEvenement = DateTime.Now;
                    p.DateAcceptation = null;
                    p.Etat = Proposition.EtatProposition.NonImprimé;
                    p.libelle = "Materiels";
                    value.Add(p);
                }



                foreach (Proposition p in value)
                {
                    foreach (Traitement t in p.traitements)
                    {
                        DateTime dte = dtpdebuttrmnt.Value;
                        foreach (Semestre s in t.semestres)
                        {
                            TimeSpan ts = dte - s.DateDebut;
                            s.DateDebut = s.DateDebut.Add(ts);
                            s.DateFin = s.DateFin.Add(ts);
                            dte = s.DateFin;

                        }
                    }
                }


            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void rbMBM_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbOrthopedie_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void pnlTypeTraitement_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Cont2;
            ShowPanel(pnlDebutFinTraitement);

        }







        private void BtnTypeMateriel_Click(object sender, EventArgs e)
        {
            ShowPanel(pnlMateriel);
        }

        private void BtnBlanchimnt_Click(object sender, EventArgs e)
        {
        }

        private void btnGBE_Click(object sender, EventArgs e)
        {



        }

        private void btnGBM_Click(object sender, EventArgs e)
        {


        }

        private void button6_Click(object sender, EventArgs e)
        {


        }

        private void btnGBS_Click(object sender, EventArgs e)
        {


        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void btnOrthopedie_Click(object sender, EventArgs e)
        {
            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Orthopedie;
            NewBuild();
            GoToPanelDuree();

        }

        private void GoToPanelDuree(int TypeDevis = 0)
        {
            if (TypeDevis == 2)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + BasCommon_DAL.DAC.prefix] == "FR" && (Value.TypeScenario == NewTraitement.typeScenario.prothése_santéclair || Value.TypeScenario == NewTraitement.typeScenario.Prothése_CMUC || Value.TypeScenario == NewTraitement.typeScenario.Prothése))
                    ShowPanel(pnlAnatomique);
                else
                    ShowPanel(pnlDebutFinTraitement);
            }
            else
            {
                if (TypeDevis == 1)
                {
                    ShowPanel(pnlDureeSem);
                }
                else
                {
                    TemplateActePG acte = value[0].traitements[0].semestres[0].traitementSecu;

                    if (acte.TypeDeReglement == ActePG.TypeReglement.Semestriel)
                        ShowPanel(pnlDureeSem);
                    else
                    {
                        numericUpDown1.Value = (decimal)acte.NBMois;
                        ShowPanel(pnlDureeAdulte);
                    }
                }
            }

        }

        private void btnOrthodontie_Click(object sender, EventArgs e)
        {
            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Orthodontie;
            ShowPanel(pnlOrthodontie);
        }

        private void btnOkTarif_Click(object sender, EventArgs e)
        {
            if ((tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Orthopedie) || (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Orthodontie))
                GoToPanelDuree();
            else if ((tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Adulte) || ((tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.FinitionAdulte)))
                GoToPanelDuree();
            else if (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Pediatrie)
                GoToPanelDuree();
            else if ((tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Cont1) || (tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Cont2))
                ShowPanel(pnlDebutFinTraitement);
            else return;
        }

        private void btnAdulte_Click(object sender, EventArgs e)
        {
            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Adulte;
            ShowPanel(pnlTypeAdulte);
        }

        private void btnCont1_Click(object sender, EventArgs e)
        {

            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Cont1;
            ShowPanel(pnlDebutFinTraitement);

        }



        private void btnOrthodMBM_Click(object sender, EventArgs e)
        {

        }

        private void btnOrthodMBC_Click(object sender, EventArgs e)
        {

        }

        private void btnOrthodMBL_Click(object sender, EventArgs e)
        {

        }

        private void btnOrthodINV_Click(object sender, EventArgs e)
        {

        }

        private void btnAdulteMBM_Click(object sender, EventArgs e)
        {

        }

        private void btnAdulteMBC_Click(object sender, EventArgs e)
        {

        }

        private void btnAdulteMBL_Click(object sender, EventArgs e)
        {

        }

        private void btnInvArcade_Click(object sender, EventArgs e)
        {

        }

        private void btnLight_Click(object sender, EventArgs e)
        {

        }

        private void btnCompl_Click(object sender, EventArgs e)
        {

        }

        private void btnComplCorr_Click(object sender, EventArgs e)
        {

        }

        private void BtnComplAvecCorrElast_Click(object sender, EventArgs e)
        {

        }

        private void btnComplDisj_Click(object sender, EventArgs e)
        {

        }

        private void btnComplChir_Click(object sender, EventArgs e)
        {

        }

        private void btnComplDisjChir_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            ShowPanel(pnlDebutFinTraitement);
        }



        private void rbMBM_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void rbMBC_CheckedChanged(object sender, EventArgs e)
        {

        }



        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {


        }

        private void rbInvArcade_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbLight_CheckedChanged(object sender, EventArgs e)
        {



        }

        private void rbComplCorr_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void rbComplDisj_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void rbComplChir_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void rbComplDisjChir_CheckedChanged(object sender, EventArgs e)
        {




        }

        private void rbAdulteMBM_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rbAdulteMBC_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rbAdulteMBL_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxMBM_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkbxMBC_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkbxMBL_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkbxINV_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void button5_Click(object sender, EventArgs e)
        {
            NewBuild();
            GoToPanelDuree();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            NewBuild();
            GoToPanelDuree();

        }

        private void txtbxAdultComplDisj_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSucette_Click(object sender, EventArgs e)
        {
            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Pediatrie;
            NewBuild();
            GoToPanelDuree();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            txtbxQteEclair.Visible = chkbxEclairssissment.Checked;
            label1.Visible = chkbxEclairssissment.Checked;
        }

        private void chkbxEmpreinteOptique_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxElastique_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkbxfacette_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            ShowPanel(pnlDebutFinTraitement);
        }





        private void pnlMateriel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNextMatos_Click(object sender, EventArgs e)
        {
            NewBuild();
            DialogResult = DialogResult.OK;
            Close();
        }



        private void lstbxScenarios_Click(object sender, EventArgs e)
        {

        }

        private void lstbxScenarios_OnSelectionChange(object sender, EventArgs e)
        {

        }

        private void pnlDureeSem_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlDureeSem.Visible == true)
            {
                /*
                lstDuree = new List<int>();
                foreach (ScenarioCommClinique scenar in MgmtScenarioCommClinique.scenarios)
                {

                    if ((int)scenar.typettmnt == (int)tpetrmnt)
                        if (!lstDuree.Contains(scenar.NbSemestres))
                            lstDuree.Add(scenar.NbSemestres);
                }

                lstDuree.Sort();
                
                List<FamilyValue> lst = new List<FamilyValue>();

                foreach (int i in lstDuree)
                    lst.Add(new FamilyValue("", i.ToString() + (i == 1 ? " semestre" : " semestres"),i));
                    
                */
                List<FamilyValue> lst = new List<FamilyValue>();

                for (int i = 1; i <= 6; i++)
                    lst.Add(new FamilyValue("", i.ToString() + (i == 1 ? " semestre" : " semestres"), i));


                lstbxDuree.LoadFromFamilyValueList(lst);
            }
        }




        private void pnlScenarios_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void lstbxDuree_OnSelectionChange(object sender, TreeNodeMouseClickEventArgs e)
        {
            Duree = (int)(lstbxDuree.SelectedItems[0].Tag);


            bool containinvisalign = false;
            foreach (Proposition p in value)
                if (CodesTraitement.IsInvisalignAdulte(p.traitements[0].CodeTraitement))
                {
                    containinvisalign = true;
                    break;
                }

            if (containinvisalign)
                ShowPanel(pnlOptionsInvisalign);
            else
                ShowPanel(pnlDebutFinTraitement);

        }

        private void pnlDureeSem_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lstbxScenarios_Load(object sender, EventArgs e)
        {

        }

        private void pnlOrthodontie_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtpdebuttrmnt_ValueChanged(object sender, EventArgs e)
        {



            int dureem = 0;
            int dureed = 0;
            DateTime tempdt = dtpdebuttrmnt.Value;

            if (value.Count > 0)
            {
                int duretheo = 0;
                bool ism = false;
                if (value[0].libelle.Length > 19)
                {
                    if (value[0].libelle.Substring(0, 20) == "Proposition/Scénario")
                    {
                        return;
                    }
                }
                if (!CodesTraitement.IsAdulte(value[0].traitements[0].CodeTraitement))
                {
                    ism = true;
                    foreach (Semestre s in value[0].traitements[0].semestres)
                        duretheo += s.traitementSecu.NBMois != null ? s.traitementSecu.NBMois.Value : 0;
                    dureem = duretheo;
                }
                else
                {
                    if (CodesTraitement.IsInvisalignAdulte(value[0].traitements[0].CodeTraitement))
                    {
                        ism = false;
                        if (CodesTraitement.IsInvisalignFull(value[0].traitements[0].CodeTraitement))
                            dureed = 30 * 14; // 1 aligneur = 14 jours, il faut en moyenne 30 aligneurs pour un traitement complet
                        else
                            dureed = 14 * 14; // 1 aligneur = 14 jours, il faut en moyenne 14 aligneurs pour un traitement Light
                    }
                    else
                    {
                        ism = true;
                        dureem = 12;
                    }
                }

                if (ism)
                    tempdt = tempdt.AddMonths(dureem);
                else
                    tempdt = tempdt.AddDays(dureed);



                DateDeFin = tempdt;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

            NewBuild();
            DialogResult = DialogResult.OK;
            Close();
        }

        System.EventHandler nbaligneurchange;
        private void pnlDebutFinTraitement_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlDebutFinTraitement.Visible)
            {
                if (CurrentPatient.propositions == null)
                    CurrentPatient.propositions = PropositionMgmt.getPropositions(CurrentPatient);

                DateTime maxDate = DateTime.Now;
                foreach (Proposition p in CurrentPatient.propositions)
                    if (p.Etat == Proposition.EtatProposition.Accepté)
                    {
                        foreach (Traitement t in p.traitements)
                        {

                            foreach (Semestre s in t.semestres)
                            {

                                if (s.DateFin != null)
                                {
                                    if (maxDate < s.DateFin)
                                        maxDate = s.DateFin;
                                }
                                else
                                    if (maxDate < DateTime.Now)
                                        maxDate = DateTime.Now;
                            }


                        }
                    }
                NewBuild();

                DateTime dtemin = DateTime.Now;
                if (!(Value is NewTraitement))
                {
                    if (CodesTraitement.IsInvisalignAdulte(value[0].traitements[0].CodeTraitement))
                        dtemin = DateTime.Now.AddMonths(1).AddDays(15);

                }

                dtpdebuttrmnt.Value = dtemin > maxDate ? dtemin : maxDate;

            }
        }

        private void pnlDebutFinTraitement_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtbxNbAligneurs_ValueChanged(object sender, EventArgs e)
        {
        }

        private void pnlOptionsInvisalign_VisibleChanged(object sender, EventArgs e)
        {
            NewBuild();
            chkbxEmpreinteOptique.Visible = false;
            foreach (Proposition p in value)
                foreach (Traitement t in p.traitements)
                    if (CodesTraitement.IsInvisalign(t.CodeTraitement))
                        chkbxEmpreinteOptique.Visible = true;

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            BackPanel();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BackPanel();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BackPanel();
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            BackPanel();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            BackPanel();
        }

        private void chkbxEmpreinteOptique_Click(object sender, EventArgs e)
        {
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.ContentionAdulte;
            ShowPanel(pnlMateriel);
        }

        private void chkbxFil33OrhautGBM_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void chkbxFil33OrhautGBM_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxEmpreinteSilicone_CheckedChanged(object sender, EventArgs e)
        {
        }


        private void chkbxminivis_CheckedChanged(object sender, EventArgs e)
        {
            txtbxQteMinivis.Visible = chkbxminivis.Checked;
            label2.Visible = chkbxminivis.Checked;
        }

        private void button8_Click_2(object sender, EventArgs e)
        {
            Duree = (int)numericUpDown1.Value;
            bool containinvisalign = false;
            foreach (Proposition p in value)
                if (CodesTraitement.IsInvisalignAdulte(p.traitements[0].CodeTraitement))
                {
                    containinvisalign = true;
                    break;
                }

            if (containinvisalign)
                ShowPanel(pnlOptionsInvisalign);
            else
                ShowPanel(pnlDebutFinTraitement);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            BackPanel();
        }

        private void chkbxEmpreinteSilicone_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void chkbxSetup_CheckedChanged(object sender, EventArgs e)
        {

            NewBuild();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            BackPanel();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            NewBuild();

            ShowPanel(pnlOptionsInvisalign);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            NewBuild();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void chkbxRECDEADD_CheckedChanged(object sender, EventArgs e)
        {
            txtbxQteRECDEADD.Visible = chkbxRECDEADD.Checked;
            label5.Visible = chkbxRECDEADD.Checked;
        }

        private void chkbxRECDESOUS_CheckedChanged(object sender, EventArgs e)
        {
            txtbxQteRECDESOUS.Visible = chkbxRECDESOUS.Checked;
            label6.Visible = chkbxRECDESOUS.Checked;
        }

        private void chkbxRECDECOMP_CheckedChanged(object sender, EventArgs e)
        {
            txtbxQteRECDECOMP.Visible = chkbxRECDECOMP.Checked;
            label7.Visible = chkbxRECDECOMP.Checked;
        }

        private void pnlTypeAdulte_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkbxFinition_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkbxiSeven_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            Hide();
            FrmWizardDevisALaCarte frm = new FrmWizardDevisALaCarte(CurrentPatient);
            if (frm.ShowDialog(this.Owner) == System.Windows.Forms.DialogResult.OK)
            {

                tpetrmnt = Devis.enumtypePropositon.ALaCarte;
                devisALaCarte = frm.devis;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                Show();
        }




        private void treeviewTraitements_OnSelected(object sender, EventArgs e)
        {
            if (treeviewTraitements.SelectedNode.Tag is NewTraitement)
            {
                Value = (treeviewTraitements.SelectedNode == null) ? null : ((NewTraitement)treeviewTraitements.SelectedNode.Tag); //((Acte)cbxActes.SelectedItem)
                NewTraitement _Traitement = new NewTraitement();
                _Traitement = TraitementsMgmt.GetFullTraitement(_Value.id_Traitement);
                TraitementsMgmt.GetCommTraitements(ref _Traitement);

                //if (_Traitement.CommTraitement.Count > 0)
                //{
                    GoToPanelDuree(2);
                    treeviewTraitements.Visible = false;
                //}
                //else
                //{
                //    MessageBox.Show("Scénario vide, veuillez choisir un ature!");

                //    return;

                //}

            }
            else
            {
                if (((FamillesTraitements)(treeviewTraitements.SelectedNode.Tag)).ParentFamillesTraitementId == -1)
                    TitreDevis = ((FamillesTraitements)(treeviewTraitements.SelectedNode.Tag)).TitreDevis;

            }
            //if (treeviewTraitements.MultiChoiceVisibilite)
            //{
            //    if (treeviewTraitements.SelectedNode.FirstNode == null)
            //    {
            //        try
            //        {
            //             NewTraitement TmpActe = ((NewTraitement)treeviewTraitements.SelectedNode.Tag);
            //             if (!treeviewTraitements.Alimenter_listBox(treeviewTraitements.SelectedNode.Text, TmpActe.id_Traitement.ToString() + "$" + treeviewTraitements.SelectedNode.ToolTipText + "$" + Parent + "$" + TmpActe.Traitement_couleur.A + "$" + TmpActe.Traitement_couleur.R + "$" + TmpActe.Traitement_couleur.G + "$" + TmpActe.Traitement_couleur.B + "$" + TmpActe.Traitement_libelle , true))
            //            {
            //                MessageBox.Show("Nombre limite des actes supplémentaires , est atteint!");
            //            }

            //        }
            //        catch (System.Exception ex)
            //        {
            //            MessageBox.Show("Famille sans Actes!");
            //        }

            //    }
            //}

        }

        private void treeviewTraitements_Load(object sender, EventArgs e)
        {

        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty)
                MessageBox.Show("Champs obligatoires");
            else
                ShowPanel(pnlDebutFinTraitement);
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            BackPanel();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {

            FrmDents frm = new FrmDents((NewTraitement)treeviewTraitements.SelectedNode.Tag);
            frm.SelectedDent = textBox1.Text;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = frm.SelectedDent;
                localisationAnatomique = textBox1.Text;
            }


        }

        private void btnAncien_Click(object sender, EventArgs e)
        {
            ShowPanel(pnlTypeTraitement);
        }


        //private void btnOk_Click(object sender, EventArgs e)
        //{

        //    //try
        //    //{

        //    //    Value = (treeviewTraitements.SelectedNode == null) ? null : ((NewTraitement)treeviewTraitements.SelectedNode.Tag); //((Acte)cbxActes.SelectedItem)
        //    //}
        //    //catch (System.Exception ex)
        //    //{
        //    //    MessageBox.Show("Il faut choisir un scénario");
        //    //}

        //    //GoToPanelDuree(1);

        //    //DialogResult = DialogResult.OK;
        //    //Close();
        //}


    }
}
