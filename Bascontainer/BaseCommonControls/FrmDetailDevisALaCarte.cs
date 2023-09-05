using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BasCommon_BO;
using BasCommon_BL;
using BaseCommonControls;
using System.IO;
using Microsoft.Win32;
namespace BaseCommonControls
{
    public partial class FrmDetailDevisALaCarte : Form
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

        public FrmDetailDevisALaCarte(Devis devis, basePatient currentpatient, bool readOnly)
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
            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETDISJ)) return 1400;
            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETCHIR)) return 1450;
            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEINVCOMPLETDISJCHIR)) return 1500;
            if ((codetraitement == CodesTraitement.ORTHODONTIEADULTEFINITION)) return 1550;
            
            if ((codetraitement == CodesTraitement.CONTENTION1) || (codetraitement == CodesTraitement.CONTENTIONINVISALIGN1)) return 5000;
            if ((codetraitement == CodesTraitement.CONTENTION2) || (codetraitement == CodesTraitement.CONTENTIONINVISALIGN2)) return 5100;

             return 0;

         }

        private int PropositionCompare(Proposition p1, Proposition p2)
        {

            int score1 = getscore(p1.traitements[0].CodeTraitement);
            int score2 = getscore(p2.traitements[0].CodeTraitement);


            return score1 - score2;


            
        }

        private void InitDisplay()
        {

            if (devis == null) return;
                        
            InitDisplayActes();
            
            
        }

        private void InitDisplayEcheances()
        {
            if (devis.echeancestemp == null)
                devis.echeancestemp = MgmtDevis.get_EcheancesDevisALaCarte(devis);
            string SummaryEcheances = EcheancesMgmt.GetSummary(devis.echeancestemp.Cast < BaseTempEcheanceDefinition>().ToList());
            lblEcheances.Text = SummaryEcheances;
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
            double totaldevis = 0;
            List<ActePGPropose> apgs = new List<ActePGPropose>();

            if (devis.actesHorstraitement == null)
                devis.actesHorstraitement = MgmtDevis.getactesHorstraitement(devis);

            foreach (ActePGPropose a in devis.actesHorstraitement)
            {
                apgs.Add(a);
                totaldevis += a.Montant;
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
                btnRistourneGlobal.Visible = false;
            }

            BuildPnlDevis();
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
                    foreach (TempEcheanceDefinition ted in frm.Montants)
                    {
                        if (ted.acte != null)
                        {
                            ted.IdSemestre = ted.acte.Semestre;
                            p.echeancestemp.Add(ted);
                            BasCommon_BL.MgmtDevis.AddTempEcheance(ted);
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

        private void BuildPnlDevis()
        {

            double total = 0;
            double totalavantremise = 0;
            foreach(ActePGPropose acte in devis.actesHorstraitement)
            {
                total += acte.Montant;
                totalavantremise += acte.MontantAvantRemise;

            }

            lblTotal.Text = total.ToString("C2");
            lblTotalAvantRemiseSurActes.Text = totalavantremise.ToString("C2");
            lblTotalAvantRemiseSurActes.Visible = totalavantremise != total;


            if ((devis.MontantAvantRemise != null) && (devis.Montant != null) && (devis.MontantAvantRemise != devis.Montant))
            {
                lblTotalAvantRemise.Text = total.ToString("C2");
                lblTotalAvantRemise.Visible = true;
                lblTotal.Text = devis.Montant.Value.ToString("C2");
            }
            else
                lblTotalAvantRemise.Visible = false;

            InitDisplayEcheances();

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

                DialogResult dr = (File.Exists(CourrierEch))?MessageBox.Show("Souhaitez-vous imprimer les échéanciers avec le devis ?", "Echeanciers", MessageBoxButtons.YesNo, MessageBoxIcon.Question):DialogResult.No;

                if (devis.propositions!=null)
                    foreach (Proposition p in devis.propositions)
                        if ((p.echeancestemp == null) || p.echeancestemp.Count == 0)
                            p.echeancestemp = PropositionMgmt.LoadDefaultTempecheances(p);

                Correspondant praticien = MgmtCorrespondants.getCorrespondant(CurrentPatient.infoscomplementaire.PraticienResponsable.Id);

                BaseCommonControls.CommonActions.AddCourrierAttributsNEwEch(devis.echeancestemp.Cast<BaseTempEcheanceDefinition>().ToList(), praticien, CurrentPatient);
                BaseCommonControls.CommonActions.PrintDevis(devis, CurrentPatient);

                if (dr == System.Windows.Forms.DialogResult.Yes)
                {

                    if ((CourrierEch == null) || (!File.Exists(CourrierEch)))
                    {
                        MessageBox.Show("Aucun courrier d'echeancier parametré !\n cle:Echeancier dans .config");
                        CanCancel = false;
                    }
                    else
                    {
                        BaseCommonControls.CommonActions.AddCourrierAttributsNEwEch(devis.echeancestemp.Cast<BaseTempEcheanceDefinition>().ToList(), praticien, CurrentPatient);
                        OLEAccess.BASLetter.GenerateFrom(CourrierEch.Trim());
                    }
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

                    if ((devis.echeancestemp == null) || (devis.echeancestemp.Count < 2) ||
                   ((devis.echeancestemp.Count > 1) &&
                   (MessageBox.Show("Attention, les échéances vont être supprimé due à la modification du montant du devis\nSouhaitez-vous continuer ?", "Effacer les echeances", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)))
                    {
                        double avantremise = a.MontantAvantRemise;
                        double newval = frm.Value;

                        a.Montant = newval;
                        devis.Montant = null;
                        devis.MontantAvantRemise = null;



                        devis.echeancestemp.Clear();
                        devis.echeancestemp.Add(MgmtDevis.CreateEcheanceDevisALaCarte(devis));
                        InitDisplayActes();
                    }
                }
                
            }

            if (dgvactepropose.Columns[e.ColumnIndex] == ColDel)
            {
                if ((devis.echeancestemp == null) || (devis.echeancestemp.Count < 2) ||
                   ((devis.echeancestemp.Count > 1) &&
                   (MessageBox.Show("Attention, les échéances vont être supprimé due à la modification du montant du devis\nSouhaitez-vous continuer ?", "Effacer les echeances", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)))
                {
                    ActePGPropose a = (ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag;
                    devis.actesHorstraitement.Remove(a);

                    devis.Montant = null;
                    devis.MontantAvantRemise = null;

                    devis.echeancestemp.Clear();
                    devis.echeancestemp.Add(MgmtDevis.CreateEcheanceDevisALaCarte(devis));

                    InitDisplayActes();
                }
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
                double total = 0;
                foreach (ActePGPropose acte in devis.actesHorstraitement)
                    total += acte.Montant;

                devis.MontantAvantRemise = total;
            

            FrmRistourne frm = new FrmRistourne(devis);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((devis.echeancestemp==null)||(devis.echeancestemp.Count<2)||
                    ((devis.echeancestemp.Count>1)&&
                    (MessageBox.Show("Attention, les échéances vont être supprimé due à la modification du montant du devis\nSouhaitez-vous continuer ?","Effacer les echeances",MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.Yes)))
                {
                    double avantremise = devis.MontantAvantRemise.Value;
                    double newval = frm.Value;

                    devis.Montant = newval;

                    devis.echeancestemp.Clear();
                    devis.echeancestemp.Add(MgmtDevis.CreateEcheanceDevisALaCarte(devis));
                    

                    BuildPnlDevis();
                }
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {


            
        }

        private void lblEcheances_Click(object sender, EventArgs e)
        {

            if (devis.echeancestemp == null)
                devis.echeancestemp = MgmtDevis.get_EcheancesDevisALaCarte(devis);



            FrmFinancement frm = new FrmFinancement(devis, CurrentPatient, devis.echeancestemp.Cast<BaseTempEcheanceDefinition>().ToList());
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                devis.echeancestemp.Clear();
                BasCommon_BL.MgmtDevis.DeleteEcheanceDevisALaCarte(devis);
                foreach (BaseTempEcheanceDefinition ted in frm.Montants)
                {
                    if (ted.acte != null)
                    {
                        EcheanceDevisALaCarte edc = EcheanceDevisALaCarte.FromBaseTempEcheanceDefinition(ted);
                        edc.devis = devis;
                        devis.echeancestemp.Add(edc);
                        BasCommon_BL.MgmtDevis.AddEcheanceDevisALaCarte(edc);
                    }
                }

                InitDisplayEcheances();
            }
        }


      
        private void dgvactepropose_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgvactepropose_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvactepropose_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (colQte.Index == e.ColumnIndex)
            {
                int oldqte = ((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).Qte;
                int qte = Convert.ToInt32(dgvactepropose.Rows[e.RowIndex].Cells[colQte.Index].Value);


                ((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).MontantAvantRemise = (((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).MontantAvantRemise / oldqte) * qte;
                ((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).Montant = ((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).MontantAvantRemise;
                ((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).Qte = qte;

                devis.Montant = null;
                devis.MontantAvantRemise = null;

                dgvactepropose.Rows[e.RowIndex].Cells[colTarif.Index].Value = ((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).Montant;

                if ((devis.echeancestemp == null) || (devis.echeancestemp.Count < 2) ||
                   ((devis.echeancestemp.Count > 1) &&
                   (MessageBox.Show("Attention, les échéances vont être supprimé due à la modification du montant du devis\nSouhaitez-vous continuer ?", "Effacer les echeances", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)))
                {

                    devis.echeancestemp.Clear();
                    devis.echeancestemp.Add(MgmtDevis.CreateEcheanceDevisALaCarte(devis));

                }
                BuildPnlDevis();
            }
        }

       

    }
}
