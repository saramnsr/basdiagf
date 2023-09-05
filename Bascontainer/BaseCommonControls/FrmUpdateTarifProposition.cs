using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using BASEPractice_BL;
//using BASEPractice_BO;
//using BASEPractice_BO.PlanGestion;
//using BASEPractice_BL.PlanGestion;
using BasCommon_BO;
using BasCommon_BL;
using System.IO;
using Microsoft.Win32;
namespace BaseCommonControls
{
    public partial class FrmUpdateTarifProposition : Form
    {


        private bool _CanCancel = false;
        public bool CanCancel
        {
            get
            {
                return _CanCancel;
            }
            set
            {
                _CanCancel = value;
                btnCancel.Visible = _CanCancel;
                btnOk.Visible = _CanCancel;
                btnClose.Visible = !_CanCancel;
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

        private Devis _devis;
        public Devis devis
        {
            get
            {
                return _devis;
            }
            set
            {
                _devis = value;
            }
        }

        private bool _ReadOnly;
        public bool ReadOnly
        {
            get
            {
                return _ReadOnly;
            }
            set
            {
                _ReadOnly = value;
            }
        }

        public FrmUpdateTarifProposition(Devis devis, basePatient currentpatient,bool readOnly)
        {
            this.CurrentPatient = currentpatient;
            this.devis = devis;
            InitializeComponent();
            ReadOnly = readOnly;
        }

        /*
        private void InitDisplay()
        {

            int maxY = 0;
            if (devis == null) return;
            int i = 1;
            pnlContainer.Controls.Clear();
            foreach (Proposition prop in devis.propositions)
            {

                


                TableLayoutPanel tlp = new TableLayoutPanel();
                tlp.Dock = DockStyle.Top;

                tlp.ColumnCount = 6;
                tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
                tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
                tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
                tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
                tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
                tlp.Dock = System.Windows.Forms.DockStyle.Top;
                tlp.Name = "tlp_prop_" + i.ToString();
                tlp.RowCount = 1;
                tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                tlp.Size = new Size(100, 25);
                tlp.TabIndex = 0;
                tlp.Tag = prop;

                pnlContainer.Controls.Add(tlp);


                Button btn = new Button();
                btn.Name = "btn_prop_" + i.ToString();
                btn.AutoSize = false;
                btn.Dock = DockStyle.Fill;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Text = "";
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.ImageList = imageList1;
                btn.ImageIndex = 0;

                btn.Click += new EventHandler(btn_Click);
                btn.Tag = prop;

                tlp.Controls.Add(btn, 0, 0);

                Label lbl = new Label();
                lbl.Name = "lbl_prop_" + i.ToString();
                lbl.AutoSize = false;
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                lbl.Text = prop.libelle;
                lbl.BorderStyle = BorderStyle.None;

                tlp.Controls.Add(lbl, 1, 0);

                Label lblTN = new Label();
                lblTN.Name = "lblTN_prop_" + i.ToString();
                lblTN.AutoSize = false;
                lblTN.Dock = DockStyle.Fill;
                lblTN.TextAlign = ContentAlignment.MiddleLeft;
                lblTN.Text = prop.traitements[0].semestres[0].Montant_AvantRemise.ToString("C2");
                lblTN.BorderStyle = BorderStyle.None;
                lblTN.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
                

                tlp.Controls.Add(lblTN, 2, 0);


                TextBox txtbxTarif = new TextBox();
                txtbxTarif.Name = "txtbxTarif_prop_" + i.ToString();
                txtbxTarif.Dock = DockStyle.Fill;
                txtbxTarif.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
                txtbxTarif.Text = prop.traitements[0].semestres[0].Montant_Honoraire.ToString();
                txtbxTarif.BorderStyle = BorderStyle.FixedSingle;
                txtbxTarif.Leave += new EventHandler(txtbxTarif_TextChanged);
                txtbxTarif.Tag = prop;

                tlp.Controls.Add(txtbxTarif, 3, 0);

                NumericUpDown remisepercentage = new NumericUpDown();
                remisepercentage.Name = "txtbxTarif_prop_" + i.ToString();
                remisepercentage.Dock = DockStyle.Fill;
                remisepercentage.Minimum = 0;
                remisepercentage.Maximum = 100;
                remisepercentage.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
                remisepercentage.Value = (decimal)(((prop.traitements[0].semestres[0].Montant_AvantRemise - prop.traitements[0].semestres[0].Montant_Honoraire) / prop.traitements[0].semestres[0].Montant_AvantRemise)*100);
                remisepercentage.BorderStyle = BorderStyle.FixedSingle;
                remisepercentage.BorderStyle = BorderStyle.None;
                remisepercentage.TextAlign = HorizontalAlignment.Right;
                remisepercentage.ValueChanged += new EventHandler(remisepercentage_ValueChanged);
                remisepercentage.Tag = txtbxTarif;

                tlp.Controls.Add(remisepercentage, 4, 0);


                btn = new Button();
                btn.Name = "btn_fi_" + i.ToString();
                btn.AutoSize = false;
                btn.Dock = DockStyle.Fill;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Text = "Financement";
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.ImageList = imageList1;
                btn.ImageIndex = 0;

                btn.Click += new EventHandler(btn_financement);
                btn.Tag = prop;

                tlp.Controls.Add(btn, 5, 0);


                maxY += tlp.Height;

                i++;

            }

            
            if (devis.actesHorstraitement == null)
                devis.actesHorstraitement = MgmtDevis.getactesHorstraitement(devis);

            i = 1;
            foreach (ActePGPropose act in devis.actesHorstraitement)
            {

                if (act.template == null)
                    act.template = TemplateApctePGMgmt.getTemplatesActeGestion(act.IdTemplateActePG);

                TableLayoutPanel tlp = new TableLayoutPanel();
                tlp.Dock = DockStyle.Top;

                tlp.ColumnCount = 5;
                tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
                tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
                tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
                tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
                tlp.Dock = System.Windows.Forms.DockStyle.Top;
                tlp.Name = "tlp_act_" + i.ToString();
                tlp.RowCount = 1;
                tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                tlp.Size = new Size(100, 25);
                tlp.TabIndex = 0;
                tlp.Tag = act;
                pnlContainer.Controls.Add(tlp);

                Button btn = new Button();
                btn.Name = "btn_prop_" + i.ToString();
                btn.AutoSize = false;
                btn.Dock = DockStyle.Fill;
                btn.Tag = act;

                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Text = "";
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.ImageList = imageList1;
                btn.ImageIndex = 0;
                btn.Click += new EventHandler(btn_Click);
                
                tlp.Controls.Add(btn, 0, 0);

                Label lbl = new Label();
                lbl.Name = "lbl_act_" + i.ToString();
                lbl.AutoSize = false;
                lbl.Dock = DockStyle.Fill;
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                lbl.Text = act.Libelle;
                lbl.BorderStyle = BorderStyle.None;
                lbl.ForeColor = Color.Blue;

                tlp.Controls.Add(lbl, 1, 0);

                Label lblTN = new Label();
                lblTN.Name = "lblTN_act_" + i.ToString();
                lblTN.AutoSize = false;
                lblTN.Dock = DockStyle.Fill;
                lblTN.TextAlign = ContentAlignment.MiddleLeft;
                lblTN.Text = act.MontantAvantRemise.ToString("C2");
                lblTN.BorderStyle = BorderStyle.None;
                lblTN.ForeColor = Color.Blue;

                tlp.Controls.Add(lblTN, 2, 0);

                TextBox txtbxTarif = new TextBox();
                txtbxTarif.Name = "txtbxTarif_act_" + i.ToString();
                txtbxTarif.Dock = DockStyle.Fill;
                txtbxTarif.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
                txtbxTarif.Text = act.Montant.ToString();
                txtbxTarif.BorderStyle = BorderStyle.FixedSingle;
                txtbxTarif.Tag = act;
                txtbxTarif.Leave +=new EventHandler(txtbxTarif_TextChanged);
                
                tlp.Controls.Add(txtbxTarif, 3, 0);


                NumericUpDown remisepercentage = new NumericUpDown();
                remisepercentage.Name = "txtbxTarif_prop_" + i.ToString();
                remisepercentage.Dock = DockStyle.Fill;
                remisepercentage.Minimum = 0;
                remisepercentage.Maximum = 100;
                remisepercentage.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
                try
                {
                    remisepercentage.Value = (decimal)(((act.MontantAvantRemise - act.Montant) / act.MontantAvantRemise) * 100);
                }
                catch (System.Exception)
                {
                    remisepercentage.Value = 0;
                }
                remisepercentage.BorderStyle = BorderStyle.FixedSingle;
                remisepercentage.BorderStyle = BorderStyle.None;
                remisepercentage.TextAlign = HorizontalAlignment.Right;
                remisepercentage.ValueChanged += new EventHandler(remisepercentageActe_ValueChanged);
                remisepercentage.Tag = txtbxTarif;

                tlp.Controls.Add(remisepercentage, 4, 0);


                maxY += tlp.Height;
                i++;

            }
            this.Height = (Height - pnlContainer.Height) + maxY;

        }
        */

         private int getscore(string codetraitement)
        {
             if (codetraitement == CodesTraitement.PEDIATRIE) return 1000;
            
            if ((codetraitement == CodesTraitement.ORTHOPEDIE)||
                (codetraitement == CodesTraitement.ORTHOPEDIE)) return 1000;
            
            if ((codetraitement == CodesTraitement.ORTHODONTIEMULTIBAGUEMETAL)||
                (codetraitement == CodesTraitement.ORTHODONTIEMULTIBAGUEMETALHN)) return 1000;
            if ((codetraitement == CodesTraitement.ORTHODONTIEMULTIBAGUECERAMIQUE)||
                (codetraitement == CodesTraitement.ORTHODONTIEMULTIBAGUECERAMIQUEHN)) return 1100;
            if ((codetraitement == CodesTraitement.ORTHODONTIEMULTIBAGUELINGUAL)||
                (codetraitement == CodesTraitement.ORTHODONTIEMULTIBAGUELINGUALHN)) return 1200;
            if ((codetraitement == CodesTraitement.ORTHODONTIEINVISALIGN) ||
                (codetraitement == CodesTraitement.ORTHODONTIEINVISALIGNHN)) return 1300;
             


             if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEMULTIBAGUEMETAL)) return 1000;
            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEMULTIBAGUECERAMIQUE)) return 1050;
            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEMULTIBAGUELINGUAL)) return 1150;

            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEINVARCADE)) return 1200;
            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEINVLIGHT)) return 1250;
            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEINVCOMPLET)) return 1300;
            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETCORR)) return 1350;
            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETCORRTIM)) return 1355;
            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETDISJ)) return 1400;
            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETCHIR)) return 1450;
            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETDISJCHIR)) return 1500;
            if ((codetraitement == CodesTraitement.ISEVEN)) return 1550;
            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEFINITION)) return 1550;
            
            if ((codetraitement == CodesTraitement.CONTENTION1) || (codetraitement == CodesTraitement.CONTENTIONINVISALIGN1)) return 5000;
            if ((codetraitement == CodesTraitement.CONTENTION2) || (codetraitement == CodesTraitement.CONTENTIONINVISALIGN2)) return 5100;

             return 0;

         }

        private int PropositionCompare(Proposition p1, Proposition p2)
        {

           if (CodesTraitement.IsAdulte(p1.traitements[0].CodeTraitement))
           {
               double tarif1 = TraitementMgmt.getTotal(p1.traitements[0]);
               double tarif2 = TraitementMgmt.getTotal(p2.traitements[0]);

               return tarif1.CompareTo(tarif2);
           }

            int score1 = getscore(p1.traitements[0].CodeTraitement);
            int score2 = getscore(p2.traitements[0].CodeTraitement);


            return score1 - score2;


            
        }

        private void InitDisplay()
        {

            int maxY = 0;
            if (devis == null) return;
            pnlContainer.Controls.Clear();


            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;

            tlp.ColumnCount = devis.propositions.Count;

            for (int c = 0; c < devis.propositions.Count; c++)
                tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            
            tlp.Dock = System.Windows.Forms.DockStyle.Left;
            tlp.Name = "tlp";
            tlp.RowCount = 3;
            tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            tlp.Size = new Size(devis.propositions.Count*300, 25);
            tlp.TabIndex = 0;
            tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            pnlContainer.Controls.Add(tlp);

            int i = 0;


            Comparison<Proposition> cmp = new Comparison<Proposition>(PropositionCompare);

            devis.propositions.Sort(cmp);

            foreach (Proposition prop in devis.propositions)
            {
                if (prop.traitements.Count == 0) continue;
                PictureBox pbx = new PictureBox();
                pbx.Dock = DockStyle.Fill;

                string file = Path.GetDirectoryName(Application.ExecutablePath) + "\\DevisImages\\" + prop.traitements[0].semestres[0].traitementSecu.Nom + ".jpg";

                if (File.Exists(file))
                    pbx.Load(file);

                tlp.Controls.Add(pbx, i, 0);




                FlowLayoutPanel flpnl = new FlowLayoutPanel();
                flpnl.Dock = DockStyle.Fill;
                flpnl.BorderStyle = BorderStyle.None;
                

                Label lblTitle = new Label();
                lblTitle.Text = prop.libelle;
                lblTitle.AutoSize = false;
                lblTitle.Font = new System.Drawing.Font("Garamond", 12, FontStyle.Bold);
                lblTitle.Size = new System.Drawing.Size(290, 50);
                lblTitle.Dock = DockStyle.Top;
                lblTitle.TextAlign = ContentAlignment.MiddleCenter;
                flpnl.Controls.Add(lblTitle);

                Label lblsubTitle = new Label();


                if (prop.traitements[0].semestres[0].traitementSecu.TypeDeReglement == ActePG.TypeReglement.Semestriel)
                {
                    lblsubTitle.Text = "Tarif/semestre : " + prop.traitements[0].semestres[0].Montant_AvantRemise.ToString("C2");
                                    }
                else
                {
                    lblsubTitle.Text = "Tarif : " + prop.traitements[0].semestres[0].Montant_AvantRemise.ToString("C2");
                    
                }




                int nbsurv = 0;
                double tarifsurv = 0;
                foreach (Semestre se in prop.traitements[0].semestres)
                {
                    if (se.surveillances.Count > 0)
                    {
                        nbsurv += se.surveillances.Count;
                        tarifsurv = se.surveillances[0].Montant_Honoraire;
                    }
                }

                if (nbsurv>0)
                    lblsubTitle.Text += "\nSurveillance : " + tarifsurv.ToString("C2");
                
                lblsubTitle.AutoSize = false;
                lblsubTitle.Font = new System.Drawing.Font("Garamond", 11, FontStyle.Regular);
                lblsubTitle.TextAlign = ContentAlignment.MiddleRight;
                lblsubTitle.Size = new System.Drawing.Size(290, 30);
                lblsubTitle.Dock = DockStyle.Top;
                lblsubTitle.TextAlign = ContentAlignment.MiddleCenter;
                lblsubTitle.Tag = prop;
                if (!ReadOnly) lblsubTitle.Click += new EventHandler(lblsubTitle_Click);
                flpnl.Controls.Add(lblsubTitle);

              

                if (prop.traitements[0].semestres[0].Montant_Honoraire != prop.traitements[0].semestres[0].Montant_AvantRemise)
                {
                    lblsubTitle.Font = new System.Drawing.Font("Garamond", 11, FontStyle.Strikeout);

                    lblsubTitle = new Label();

                    if (prop.traitements[0].semestres[0].traitementSecu.TypeDeReglement == ActePG.TypeReglement.Semestriel)
                        lblsubTitle.Text = "Tarif appliqué/semestre : " + prop.traitements[0].semestres[0].Montant_Honoraire.ToString("C2");
                    else
                        lblsubTitle.Text = "Tarif appliqué: " + prop.traitements[0].semestres[0].Montant_Honoraire.ToString("C2");


                   
                    lblsubTitle.AutoSize = false;
                    lblsubTitle.Font = new System.Drawing.Font("Garamond", 11, FontStyle.Regular);
                    lblsubTitle.TextAlign = ContentAlignment.MiddleRight;
                    lblsubTitle.Size = new System.Drawing.Size(290, 30);
                    lblsubTitle.Dock = DockStyle.Top;
                    lblsubTitle.TextAlign = ContentAlignment.MiddleCenter;
                    flpnl.Controls.Add(lblsubTitle);
                }


                if ((devis.actesHorstraitement != null) &&
                    (devis.actesHorstraitement.Count == 1) &&
                    (!CodesTraitement.IsContention(prop.traitements[0].semestres[0].CodeSemestre)) &&
                    (!CodesTraitement.IsAdulte(prop.traitements[0].semestres[0].CodeSemestre)))
                {
                    Label lblsubTitleMatos = new Label();
                    lblsubTitleMatos.Text = "Tarif de l'appareil : " + devis.actesHorstraitement[0].Montant.ToString("C2");

                    
                    lblsubTitleMatos.AutoSize = false;
                    lblsubTitleMatos.Font = new System.Drawing.Font("Garamond", 11, FontStyle.Regular);
                    lblsubTitleMatos.TextAlign = ContentAlignment.MiddleRight;
                    lblsubTitleMatos.Size = new System.Drawing.Size(290, 30);
                    lblsubTitleMatos.Dock = DockStyle.Top;
                    lblsubTitleMatos.TextAlign = ContentAlignment.MiddleCenter;
                    lblsubTitleMatos.Tag = prop;
                    if (!ReadOnly) lblsubTitleMatos.Click += new EventHandler(lblsubTitle_Click);
                    flpnl.Controls.Add(lblsubTitleMatos);
                }

                if (CurrentPatient.Mutuelle == null)
                    CurrentPatient.Mutuelle = MutuelleMgmt.getMutuelle(CurrentPatient.mutuelle);


                if ((CurrentPatient.Mutuelle != null) && (CurrentPatient.Mutuelle.MontantPlafond<90000))
                {
                   
                    lblsubTitle = new Label();
                    lblsubTitle.Text = "Plafond Mutuelle : " + CurrentPatient.Mutuelle.MontantPlafond.ToString("C2");
                   
                    lblsubTitle.AutoSize = false;
                    lblsubTitle.Font = new System.Drawing.Font("Garamond", 11, FontStyle.Regular);
                    lblsubTitle.TextAlign = ContentAlignment.MiddleRight;
                    lblsubTitle.Size = new System.Drawing.Size(290, 30);
                    lblsubTitle.Dock = DockStyle.Top;
                    lblsubTitle.TextAlign = ContentAlignment.MiddleCenter;

                    if (CurrentPatient.Mutuelle.MontantPlafond < prop.traitements[0].semestres[0].Montant_Honoraire) 
                        lblsubTitle.ForeColor = Color.Red;
                    flpnl.Controls.Add(lblsubTitle);
                }

                
                lblsubTitle = new Label();

                Dictionary<double, int> nbecheanceparmontant = new Dictionary<double, int>();

                if (prop.echeancestemp == null)
                    prop.echeancestemp = MgmtDevis.get_tempecheances(prop);

                if (prop.echeancestemp.Count == 0)
                {
                    foreach (Traitement t in prop.traitements)
                        foreach (Semestre s in t.semestres)
                        {
                            if (!nbecheanceparmontant.ContainsKey(s.Montant_Honoraire))
                                nbecheanceparmontant.Add(s.Montant_Honoraire, 1);
                            else
                                nbecheanceparmontant[s.Montant_Honoraire]++;

                            foreach (Surveillance su in s.surveillances)
                            {
                                if (!nbecheanceparmontant.ContainsKey(su.Montant_Honoraire))
                                    nbecheanceparmontant.Add(su.Montant_Honoraire, 1);
                                else
                                    nbecheanceparmontant[su.Montant_Honoraire]++;
                            }
                        }
                }
                else
                {
                    foreach (TempEcheanceDefinition t in prop.echeancestemp)
                        if (!nbecheanceparmontant.ContainsKey(t.Montant))
                                nbecheanceparmontant.Add(t.Montant, 1);
                            else
                            nbecheanceparmontant[t.Montant]++;
                }
                foreach (KeyValuePair<double,int> kv in nbecheanceparmontant)
                    lblsubTitle.Text += "\n" + kv.Value.ToString()+" échéance(s) à "+kv.Key.ToString("C2");



                lblsubTitle.AutoSize = false;
                lblsubTitle.TextAlign = ContentAlignment.MiddleCenter;
                lblsubTitle.Font = new System.Drawing.Font("Garamond", 11, FontStyle.Regular);
                lblsubTitle.Size = new System.Drawing.Size(290, 100);
                lblsubTitle.Dock = DockStyle.Top;
                flpnl.Controls.Add(lblsubTitle);


                tlp.Controls.Add(flpnl, i, 1);



                TableLayoutPanel tlpBtn = new TableLayoutPanel();
                tlpBtn.Dock = DockStyle.Fill;

                tlpBtn.ColumnCount = 5;

                tlpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50));
                tlpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
                tlpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
                tlpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
                tlpBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));

                tlpBtn.Dock = System.Windows.Forms.DockStyle.Fill;
                tlpBtn.Name = "tlpbtn_" + i.ToString();
                tlpBtn.RowCount = 1;
                tlpBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
                tlpBtn.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;


                tlp.Controls.Add(tlpBtn, i, 2);

                if (!ReadOnly)
                {
                    Button btnDel = new Button();
                    btnDel.Dock = DockStyle.Fill;
                    btnDel.Tag = prop;
                    btnDel.FlatStyle = FlatStyle.Flat;
                    btnDel.Click += new EventHandler(btnDel_Click);
                    btnDel.ImageList = imageList1;
                    btnDel.ImageIndex = 0;
                    btnDel.Margin = new System.Windows.Forms.Padding(8);
                    btnDel.Text = "Supprimer";
                    btnDel.ImageAlign = ContentAlignment.MiddleCenter;
                    btnDel.TextAlign = ContentAlignment.BottomCenter;
                    btnDel.Font = new System.Drawing.Font("Garamond", 8);

                    tlpBtn.Controls.Add(btnDel, 3, 0);

                    Button btnFi = new Button();
                    btnFi.Dock = DockStyle.Fill;
                    btnFi.Tag = prop;
                    btnFi.Margin = new System.Windows.Forms.Padding(8);
                    btnFi.FlatStyle = FlatStyle.Flat;
                    btnFi.Click += new EventHandler(btn_financement);
                    btnFi.ImageList = imageList1;
                    btnFi.ImageIndex = 1;
                    btnFi.Text = "Echéancier";
                    btnFi.ImageAlign = ContentAlignment.MiddleCenter;
                    btnFi.TextAlign = ContentAlignment.BottomCenter;
                    btnFi.Font = new System.Drawing.Font("Garamond", 7);

                    tlpBtn.Controls.Add(btnFi, 2, 0);

                    Button btnRem = new Button();
                    btnRem.Dock = DockStyle.Fill;
                    btnRem.Tag = prop;
                    btnRem.Click += new EventHandler(btnRem_Click);
                    btnRem.ImageList = imageList1;
                    //btnRem.ImageIndex = 2;
                    btnRem.Margin = new System.Windows.Forms.Padding(8);
                    btnRem.Text = "%";
                    btnRem.FlatAppearance.BorderSize = 0;
                    btnRem.FlatStyle = FlatStyle.Flat;
                    btnRem.ImageAlign = ContentAlignment.MiddleCenter;
                    btnRem.TextAlign = ContentAlignment.MiddleCenter;
                    btnRem.Font = new System.Drawing.Font("Garamond", 8);
                    tlpBtn.Controls.Add(btnRem, 1, 0);

                }

                i++;

            }

            InitDisplayActes();


            
        }

        void lblsubTitle_Click(object sender, EventArgs e)
        {
            Proposition p = (Proposition)((Label)sender).Tag;


            if (p.echeancestemp.Count > 0)
            {
                if (MessageBox.Show("Un échéancier à été fait pour cette proposition et sera supprimé.\nSouhaitez-vous continuer ?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                    return;

            }

            FrmRistourne frm = new FrmRistourne(p.traitements[0].semestres[0]);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                p.echeancestemp.Clear();
                BasCommon_BL.MgmtDevis.DeleteTempEcheance(p);


                double avantremise = frm.Value;


                //double newval = (avantremise - (avantremise * ((double)frm.Value / 100)));
                double newval = frm.Value;

                foreach (Traitement trmnt in p.traitements)
                    foreach (Semestre sem in trmnt.semestres)
                    {
                        sem.Montant_Honoraire = newval;
                        sem.Montant_AvantRemise = newval;
                    }
                InitDisplay();
            }

            
        }

        private void InitDisplayActes()
        {

            List<ActePGPropose> apgs = new List<ActePGPropose>();

            if (devis.actesHorstraitement == null)
                devis.actesHorstraitement = MgmtDevis.getactesHorstraitement(devis);

            foreach (ActePGPropose a in devis.actesHorstraitement)
                apgs.Add(a);


            foreach (Proposition p in devis.propositions)
            {
                if (p.matosassociate == null)
                    p.matosassociate = MgmtDevis.getactesHorstraitement(p);
                foreach (ActePGPropose a in p.matosassociate)
                    apgs.Add(a);
            }
            dgvactepropose.Rows.Clear();
            foreach (ActePGPropose act in apgs)
            {

                if (act.template == null)
                    act.template = TemplateApctePGMgmt.getTemplatesActeGestion(act.IdTemplateActePG);


                object[] obj = new object[]{
                    act.Libelle,
                    act.Qte,
                    act.MontantAvantRemise
                };

                int idx = dgvactepropose.Rows.Add(obj);
                dgvactepropose.Rows[idx].Tag = act;

            }

            if (ReadOnly)
            {
                dgvactepropose.Columns.Remove(colBtn);
                dgvactepropose.Columns.Remove(ColDel);
            }
        }

        void btnRem_Click(object sender, EventArgs e)
        {

            Proposition p = (Proposition)((Button)sender).Tag;

            if (p.echeancestemp == null)
                p.echeancestemp = MgmtDevis.get_tempecheances(p);

            if (p.echeancestemp.Count > 0)
            {
                if (MessageBox.Show("Un échéancier à été fait pour cette proposition et sera supprimé.\nSouhaitez-vous continuer ?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                    return;

                p.echeancestemp.Clear();
                BasCommon_BL.MgmtDevis.DeleteTempEcheance(p);

            }

            FrmRistourne frm = new FrmRistourne(p.traitements[0].semestres[0]);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {



                double avantremise = p.traitements[0].semestres[0].Montant_AvantRemise;



                //double newval = (avantremise - (avantremise * ((double)frm.Value / 100)));
                double newval = frm.Value;

                foreach (Traitement trmnt in p.traitements)
                    foreach (Semestre sem in trmnt.semestres)
                        sem.Montant_Honoraire = newval;

            }

            InitDisplay();

        }

        

        void btn_financement(object sender, EventArgs e)
        {

            if (((Button)sender).Tag is Proposition)
            {
                Proposition p = ((Proposition)((Button)sender).Tag);

                if (p.echeancestemp==null)
                    p.echeancestemp = MgmtDevis.get_tempecheances(p);

                FrmFinancement frm = new FrmFinancement(p, CurrentPatient, p.echeancestemp);
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    p.echeancestemp.Clear();
                    BasCommon_BL.MgmtDevis.DeleteTempEcheance(p);
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
                            p.echeancestemp.Add(tted);
                            BasCommon_BL.MgmtDevis.AddTempEcheance(tted);
                        }
                    }

                    InitDisplay();
                }
            }

            
        }

        void btnDel_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Tag is Proposition)
            {
                Proposition p = ((Proposition)((Button)sender).Tag);
                devis.propositions.Remove(p);
                InitDisplay();
            }

            if (((Button)sender).Tag is ActePGPropose)
            {
                ActePGPropose a = ((ActePGPropose)((Button)sender).Tag);
                devis.actesHorstraitement.Remove(a);
                InitDisplay();
            }
        }

        void txtbxTarif_TextChanged(object sender, EventArgs e)
        {
            RefreshPercentages();
        }

        void remisepercentage_ValueChanged(object sender, EventArgs e)
        {

            double percentage = (double)((NumericUpDown)sender).Value;
            TextBox txtbx = ((TextBox)((NumericUpDown)sender).Tag);
            Proposition p = ((Proposition)txtbx.Tag);

            double trf = p.traitements[0].semestres[0].Montant_AvantRemise;


            //double trf = Convert.ToDouble(((TextBox)tlp.Controls[1]).Text);
            trf = Math.Round(trf - (trf * (percentage / 100.0)), 2);
            txtbx.Text = trf.ToString();

        }

        void remisepercentageActe_ValueChanged(object sender, EventArgs e)
        {

            double percentage = (double)((NumericUpDown)sender).Value;
            TextBox txtbx = ((TextBox)((NumericUpDown)sender).Tag);
            ActePGPropose p = ((ActePGPropose)txtbx.Tag);

            double trf = p.template.Valeur;


            //double trf = Convert.ToDouble(((TextBox)tlp.Controls[1]).Text);
            trf = Math.Round(trf - (trf * (percentage / 100)), 2);
            txtbx.Text = trf.ToString();

        }

        private void FrmUpdateTarifProposition_Load(object sender, EventArgs e)
        {
            InitDisplay();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void ApplyRistourneOnTraitments(double percentage)
        {
            foreach (TableLayoutPanel tlp in pnlContainer.Controls)
            {
                if (tlp.Tag is Proposition)
                {
                    try
                    {
                       // double trf = ((Proposition)tlp.Tag).traitements[0].semestres[0].Montant_AvantRemise;


                        //double trf = Convert.ToDouble(((TextBox)tlp.Controls[1]).Text);
                        //trf = Math.Round(trf - (trf * (percentage / 100)), 2);
                        ((NumericUpDown)tlp.Controls[4]).Value = (decimal)percentage;
                    }
                    catch (System.Exception) { }

                }
            }

            RefreshMontants();
        }


        private void RefreshPercentages()
        {
            foreach (TableLayoutPanel tlp in pnlContainer.Controls)
            {
                foreach (Control c in tlp.Controls)
                {

                    if (c is NumericUpDown)
                    {

                        NumericUpDown nud = (NumericUpDown)c;
                        TextBox txtbx = ((TextBox)nud.Tag);

                        double avantremise = 0;
                        double apresremise = 0;

                        if (txtbx.Tag is Proposition)
                        {
                            Proposition p = ((Proposition)txtbx.Tag);

                            try
                            {
                                avantremise = p.traitements[0].semestres[0].Montant_AvantRemise;
                                apresremise = Convert.ToDouble(txtbx.Text);
                            }
                            catch (System.Exception) { txtbx.Text = avantremise.ToString(); nud.Value = 0; return; }

                        }

                        if (txtbx.Tag is ActePGPropose)
                        {
                            ActePGPropose p = ((ActePGPropose)txtbx.Tag);

                            try
                            {
                             avantremise = p.template.Valeur;
                             apresremise = Convert.ToDouble(txtbx.Text);
                            }
                            catch (System.Exception) { }

                        }

                        try
                        {
                            nud.Value = (decimal)(((avantremise - apresremise) / avantremise) * 100);
                        }
                        catch (System.Exception) { }

                    }
                    
                }


            }
        }


        private void RefreshMontants()
        {
            foreach (TableLayoutPanel tlp in pnlContainer.Controls)
            {
                foreach(Control c in tlp.Controls)
                {
                    if ((c is NumericUpDown) &&(c.Tag is TextBox))
                    {
                         
                         double avantremise = 0;

                         if (((TextBox)c.Tag).Tag is Proposition)
                         {
                             Proposition p = ((Proposition)((TextBox)c.Tag).Tag);
                             avantremise = p.traitements[0].semestres[0].Montant_AvantRemise;



                         }

                         if (((TextBox)c.Tag).Tag is ActePGPropose)
                         {
                             ActePGPropose a = ((ActePGPropose)((TextBox)c.Tag).Tag);
                             avantremise = a.template.Valeur;
                         }

                        double newval = (avantremise - (avantremise * ((double)((NumericUpDown)c).Value / 100)));
                        ((TextBox)c.Tag).Text = newval.ToString();

                        if (((TextBox)c.Tag).Tag is Proposition)
                        {
                            Proposition p = ((Proposition)((TextBox)c.Tag).Tag);

                            foreach (Traitement trmnt in p.traitements)
                                foreach (Semestre sem in trmnt.semestres)
                                    sem.Montant_Honoraire = newval;
                        }


                    }
                }

                
            }
        }

        

        private bool Build()
        {
            
               
                return true;
            
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Build())
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
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
        private void button5_Click(object sender, EventArgs e)
        {

            if (Build())
            {

                string CourrierEch =  templateFolder + System.Configuration.ConfigurationManager.AppSettings["Echeancier"];

                DialogResult dr = (!File.Exists(CourrierEch)) ? MessageBox.Show("Souhaitez-vous imprimer les échéanciers avec le devis ?", "Echeanciers", MessageBoxButtons.YesNo, MessageBoxIcon.Question) : DialogResult.No;


                //DialogResult dr = MessageBox.Show("Souhaitez-vous imprimer les échéanciers avec le devis ?", "Echeanciers", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                foreach (Proposition p in devis.propositions)
                    if ((p.echeancestemp == null) || p.echeancestemp.Count == 0)
                        p.echeancestemp = PropositionMgmt.LoadDefaultTempecheances(p);

                Correspondant praticien = MgmtCorrespondants.getCorrespondant(CurrentPatient.infoscomplementaire.PraticienResponsable.Id);

                if (devis.TypeDevis == Devis.enumtypePropositon.ALaCarte)
                {

                    if (devis.echeancestemp == null)
                        devis.echeancestemp = MgmtDevis.get_EcheancesDevisALaCarte(devis);

                    BaseCommonControls.CommonActions.AddCourrierAttributsNEwEch(devis.echeancestemp.Cast<BaseTempEcheanceDefinition>().ToList(), praticien, CurrentPatient);
                }
                BaseCommonControls.CommonActions.PrintDevis(devis, CurrentPatient);

                if (dr == System.Windows.Forms.DialogResult.Yes)
                    foreach (Proposition p in devis.propositions)
                    {

                       // string CourrierEch = System.Configuration.ConfigurationManager.AppSettings["Echeancier"];

                        if ((CourrierEch == null) || (!File.Exists(CourrierEch)))
                        {
                            MessageBox.Show("Aucun courrier d'echeancier parametré !\n cle:Echeancier dans .config");
                            break;
                        }

                        if (p.echeancestemp == null)
                            p.echeancestemp = PropositionMgmt.LoadDefaultTempecheances(p);

                        if (p.echeancestemp.Count == 0)
                            MessageBox.Show("Aucun échéancier n'a été définit pour cette proposition");


                       
                       
                        //AddCourrierAttributsNEwEch();




                        OLEAccess.BASLetter.GenerateFrom(CourrierEch.Trim());

                    }

                if (CanCancel)
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void dgvactepropose_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvactepropose.Columns[e.ColumnIndex] == colBtn)
            {
                ActePGPropose a = (ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag;

                FrmRistourne frm = new FrmRistourne(a);
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    double avantremise = a.MontantAvantRemise;
                    double newval = frm.Value;

                    a.Montant = newval;
                }
                InitDisplayActes();
            }

            if (dgvactepropose.Columns[e.ColumnIndex] == ColDel)
            {
                ActePGPropose a = (ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag;
                devis.actesHorstraitement.Remove(a);
                InitDisplayActes();
            }
            
        }

        private void dgvactepropose_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != colTarif.Index) return;

            Font ftstriked = new System.Drawing.Font("Garamond", 12, FontStyle.Strikeout);
            Font ft = new System.Drawing.Font("Garamond", 12, FontStyle.Regular);

            ActePGPropose apgp = (ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag;

            e.PaintBackground(e.CellBounds, true);
            if (apgp.Montant != apgp.MontantAvantRemise)
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(apgp.MontantAvantRemise.ToString("C2"), ftstriked, new SolidBrush(e.CellStyle.ForeColor), e.CellBounds, sf);


                sf.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(apgp.Montant.ToString("C2"), ft, new SolidBrush(e.CellStyle.ForeColor), e.CellBounds, sf);
            }
            else
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(apgp.MontantAvantRemise.ToString("C2"), ft, new SolidBrush(e.CellStyle.ForeColor), e.CellBounds, sf);
            }

            e.Handled = true;
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void pnlContainer_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
