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
using BASEPractice_BL;
using System.IO;
using Microsoft.Win32;
namespace BaseCommonControls
{
    public partial class FrmFinancement : Form
    {

        private ActePG ap = new ActePG();
        private Proposition _Currentproposition;
        public Proposition Currentproposition
        {
            get
            {
                return _Currentproposition;
            }
            set
            {
                _Currentproposition = value;
            }
        }





        private Boolean  _Visibilite;
         

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
        
        private DataGridViewCellStyle _PayedStyle = new DataGridViewCellStyle();
        public DataGridViewCellStyle PayedStyle
        {
            get
            {
                return _PayedStyle;
            }
            set
            {
                _PayedStyle = value;
            }
        }
        
        private DataGridViewCellStyle _NormalStyle = new DataGridViewCellStyle();
        public DataGridViewCellStyle NormalStyle
        {
            get
            {
                return _NormalStyle;
            }
            set
            {
                _NormalStyle = value;
            }
        }


        private List<BaseTempEcheanceDefinition> _Montants = new List<BaseTempEcheanceDefinition>();
        public List<BaseTempEcheanceDefinition> Montants
        {
            get
            {
                return _Montants;
            }
            set
            {
                _Montants = value;
            }
        }


        string erreur = "";

        private NewTraitement.typeScenario _typeScenario;
        public NewTraitement.typeScenario typeScenario
        {
            get
            {
                return _typeScenario;
            }
            set
            {
                _typeScenario = value;
            }
        }

        public FrmFinancement(Devis devis, basePatient patient, List<BaseTempEcheanceDefinition> lsttmpech)
        {

            InitializeComponent();
            


            if (devis.TypeDevis != Devis.enumtypePropositon.ALaCarte)
                throw new System.Exception("Le devis n'est pas a la carte !");
            

            NormalStyle.ForeColor = Color.Black;
            PayedStyle.ForeColor = Color.Gray;
            PayedStyle.Font = new Font("garamond", 12, FontStyle.Strikeout);


            _CurrentPatient = patient;


            ActePG actesimule = BasCommon_BL.MgmtDevis.CreateActePG(devis,devis.DatePrevisionnelDeDebutTraitement,12);

           

            if ((lsttmpech == null) || (lsttmpech.Count == 0))
            {





                BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                    ted.DAteEcheance = actesimule.DateExecution;
                    ted.Montant = actesimule.Montant_Honoraire;
                    ted.Libelle = actesimule.Libelle;
                    ted.acte = actesimule;
                    ted.AlreadyPayed = false;
                    ted.payeur = Echeance.typepayeur.patient;


                    Montants.Add(ted);
                


            }
            else
            {
                foreach (BaseTempEcheanceDefinition tedi in lsttmpech)
                {
                    if (tedi.acte == null)
                        tedi.acte = actesimule;
                    Montants.Add(tedi);
                }
                
            }








        }


        public FrmFinancement(basePatient pat, List<TempEcheanceDefinition> lsttmpech)
        {
            InitializeComponent();

            CurrentPatient = pat;
            if (lsttmpech.Count == 1)
            {

                button6.Visible = false;
            }

            List<Traitement> TmpListTraitements = new List<Traitement>();



            NormalStyle.ForeColor = Color.Black;
            PayedStyle.ForeColor = Color.Gray;
            PayedStyle.Font = new Font("garamond", 12, FontStyle.Strikeout);





                foreach (TempEcheanceDefinition ted in lsttmpech)
                {

                    TempEcheanceDefinition tmp = new TempEcheanceDefinition();
                    tmp.DAteEcheance = ted.DAteEcheance;
                    tmp.Montant = ted.Montant;
                    tmp.Libelle = ted.Libelle;
                    tmp.acte = ap;
                    tmp.AlreadyPayed = false;
                    tmp.acte.Libelle = ted.acte.Libelle;
                    tmp.payeur =Echeance.typepayeur.patient;

                    Montants.Add(tmp);
                }
    

        }
       
        public FrmFinancement(Proposition proposition, basePatient patient, List<TempEcheanceDefinition> lsttmpech, int IdComm = -1, Boolean visibilite  = true,int EcheancesDevis = 0,NewTraitement.typeScenario type = NewTraitement.typeScenario.Prothése)
        {
            _Visibilite = visibilite;
            InitializeComponent();
            typeScenario = type;
            if (lsttmpech.Count == 1)
            {

                button6.Visible = false;
            }
            if (visibilite)
            {
                button5.Visible = false;
                button6.Visible = false;
                btnAdd.Visible = false;
            }
           
            
            Currentproposition = proposition;

            List<Traitement> TmpListTraitements = new List<Traitement>();

         
            if (IdComm != -1)
            {
                foreach (Traitement t in Currentproposition.traitements)
                {
                    if (t.Id == IdComm)
                    {
                        TmpListTraitements.Add(t);
                    }
                }

            }
            else
            {
                TmpListTraitements = Currentproposition.traitements;
            }


            NormalStyle.ForeColor = Color.Black;
            PayedStyle.ForeColor = Color.Gray;
            PayedStyle.Font = new Font("garamond", 12, FontStyle.Strikeout);


            _CurrentPatient = patient;

            List<ActePG> actesimules = new List<ActePG>();

            if (Currentproposition != null && IdComm == -1 && EcheancesDevis ==0 )
            {
                foreach (Traitement t in Currentproposition.traitements)

                    foreach (Semestre s in t.semestres)
                    {
                        ActePG apg = BasCommon_BL.ActesPGMgmt.CreateActeFromSemestre(s);
                        actesimules.Add(apg);
                        foreach (Surveillance su in s.surveillances)
                        {
                            ActePG apgsu = BasCommon_BL.ActesPGMgmt.CreateActeFromSurveillance(su);
                            actesimules.Add(apgsu);
                        }
                    }
            }

            else
            {



                foreach (TempEcheanceDefinition ted in lsttmpech)
                {

                    TempEcheanceDefinition tmp = new TempEcheanceDefinition();
                    tmp.DAteEcheance = ted.DAteEcheance;
                    tmp.Montant = ted.Montant;
                    tmp.Libelle = ted.Libelle;
                    tmp.acte = ap;
                    tmp.AlreadyPayed = false;
                    tmp.acte.Libelle = ted.acte.Libelle;
                    tmp.acte.DateExecution = proposition.DateEvenement .Value  ;

                    tmp.acte.Montant_Honoraire = ted.Montant;
                 ///modificattion nadheeeeeeeeeeeem
                    tmp.payeur = ted.payeur;

                    Montants.Add(tmp);
                }
            }
            if ((lsttmpech == null) || (lsttmpech.Count == 0))
            {

                if (actesimules.Count != 0)
                {
                    foreach (ActePG a in actesimules)
                    {
                        TempEcheanceDefinition ted = new TempEcheanceDefinition();
                        ted.DAteEcheance = a.NbMois != null ? a.DateExecution.AddMonths(a.NbMois.Value).AddDays(a.NbJours.Value) : a.DateExecution;
                        ted.Montant = a.Montant_Honoraire;
                        ted.Libelle = a.Libelle;
                        ted.acte = a;
                        ted.AlreadyPayed = false;
                        ted.payeur = Echeance.typepayeur.patient;


                        Montants.Add(ted);
                    }

                }
               



















            }
            else
            {


                if (IdComm == -1 && EcheancesDevis == 0)
                {
                    foreach (TempEcheanceDefinition tedi in lsttmpech)
                    {

                        ActePG selecteds = null;
                        foreach (ActePG a in actesimules)
                            if (a.Semestre == tedi.IdSemestre)
                                selecteds = a;

                        tedi.acte = selecteds;


                        Montants.Add(tedi);
                    }
                }
            }







        }
       
        private void FrmRedefinirEcheance_Load(object sender, EventArgs e)
        {
            ShowPanel(pnlEcheances);

           

            InitDisplay();


            dgvEcheances.Sort(colDaterem,ListSortDirection.Ascending);

            /*

            double totalMutuelle = 0;
            double totalSecu = 0;
          double totalPatient = 0;
            double total = 0;
            totalSecuforCMU = 0;

            foreach (TempEcheanceDefinition ted in Montants)
            {
                if (ted.payeur == Echeance.typepayeur.Mutuelle)
                    totalMutuelle += ted.Montant;
                if (ted.payeur == Echeance.typepayeur.Secu)
                    totalSecu += ted.Montant;
                if (ted.payeur == Echeance.typepayeur.patient)
                    totalPatient += ted.Montant;
                total += ted.Montant;
            }

           

            lblPercentMutuelle.Value = (decimal)((totalMutuelle / total) * 100f);
            lblPercentMutuelle.Enabled = true;

            if (baseMgmtPatient.IsCMU(CurrentPatient))
            {
                foreach (ActePG a in actespg)
                    totalSecuforCMU += (a.Template.Code.Valeur) * (a.Coeff);
                lblPartSecu.Text = totalSecuforCMU.ToString("C2");
                lblPercentMutuelle.Value = 100;
                lblPercentMutuelle.Enabled = false;
            }else
            if (baseMgmtPatient.IsTierPayant(CurrentPatient))
            {
                lblPercentMutuelle.Enabled = true;
            }else
            {
                lblPercentMutuelle.Value = 0;
                lblPercentMutuelle.Enabled = false;
            }
            


           

            

            rbNbEcheances.Checked = true;

            int nbmois = 1;
            foreach (ActePG a in actespg)
                nbmois += a.NbMois == null ? 1 : a.NbMois.Value;

            txtbxNbEcheances.Value = nbmois > 1 ? nbmois - 1 : 1;
            */
            
        }

        private void InitDisplay()
        {



            

            dgvEcheances.Rows.Clear();
            string lib = "";
            lib = "Echéance " ;
            int echnum = 1;
            foreach (BaseTempEcheanceDefinition ted in Montants)
            {

                object[] obj = new object[]{
                    ted.Libelle,
                    ted.DAteEcheance,
                    ted.Montant,
                    (ted.ParPrelevement?"Oui":"Non"),
                    (ted.ParVirement?"Oui":"Non")
                };

                int idx = dgvEcheances.Rows.Add(obj);
                dgvEcheances.Rows[idx].Tag = ted;
                if (ted.AlreadyPayed)
                    dgvEcheances.Rows[idx].DefaultCellStyle = PayedStyle;


               
                echnum++;
            }



            if (CurrentPatient.Correspondants == null)
                CurrentPatient.Correspondants = MgmtCorrespondants.getCorrespondantsOf(CurrentPatient);

            dgvEcheances.Sort(colDaterem, ListSortDirection.Ascending);

            
            int nbechpat = 0;

            foreach (BaseTempEcheanceDefinition ted in Montants)
                if (ted.payeur == Echeance.typepayeur.patient)
                    nbechpat++;
            //if (_Visibilite ) 
          //  button6.Visible = nbechpat > 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        /*
        private void Recalculate()
        {
            Build();
            InitDisplay();
            if (erreur != "")
            {
                MessageBox.Show(erreur, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        */
             
                

        private void btnOk_Click(object sender, EventArgs e)
        {

            if ((!MgmtEcheance.CheckPrelevementDates(Montants)))
                erreur = "Mauvises dates pour le(s) prelevement(s)";


            if (erreur != "")
            {
                DialogResult = DialogResult.Cancel;

                MessageBox.Show(erreur);
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lvEcheances_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
               

            if (dgvEcheances.SelectedRows.Count == 0) return;
            FrmAddEcheance frm = new FrmAddEcheance();
            frm.SelectedEcheance = ((BaseTempEcheanceDefinition)dgvEcheances.SelectedRows[0].Tag);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BaseTempEcheanceDefinition ted = new BaseTempEcheanceDefinition();
                    ted.DAteEcheance = frm.dateEch;
                    ted.Montant = frm.montant; 
                    ted.CanRecalculate = true;

                    foreach (DataGridViewRow dr in dgvEcheances.SelectedRows)
                    {
                        ((BaseTempEcheanceDefinition)dr.Tag).CanRecalculate = true;
                    }

                    if(dgvEcheances.SelectedRows.Count>0)
                        ted.acte = ((BaseTempEcheanceDefinition)dgvEcheances.SelectedRows[0].Tag).acte;
                    else
                        ted.acte = ((BaseTempEcheanceDefinition)dgvEcheances.Rows[0].Tag).acte;

                    ted.Libelle =  ted.acte.Libelle;


                     MgmtEcheance.AddAndRecalculate(Montants, ted,typeScenario);
                    if (Montants.Count == 1)
                    {
                        button6.Visible = false;
                    }
                    else { button6.Visible = true; }
                    InitDisplay();
                }
                catch (System.Exception) { }
            }
        }

        private void btnPaiementPartiel_Click(object sender, EventArgs e)
        {

            ///Paiement partiel inutile car utilisé directement lors du reglement
            /*
            int idx = 0;
            
            if (dgvEcheances.SelectedRows.Count>0)
                idx = dgvEcheances.SelectedRows[0].Index;

            if (((TempEcheanceDefinition)dgvEcheances.SelectedRows[0].Tag).AlreadyPayed)
            {
                MessageBox.Show("Impossible de faire un paiement partiel sur cette echance");
                return;
            }

            FrmAddEcheance frm = new FrmAddEcheance();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (frm.montant > Montants[idx].Montant) return;
                    TempEcheanceDefinition ted = new TempEcheanceDefinition();
                    ted.DAteEcheance = DateTime.Now;
                    ted.Libelle = Montants[idx].Libelle + " partiel";
                    ted.Montant = frm.montant;
                    ted.CanRecalculate = false;
                    ted.acte = Montants[idx].acte;
                    Montants[idx].Montant = Montants[idx].Montant - frm.montant;
                    _Montants.Insert(idx, ted);
                    erreur = "";
                    InitDisplay();
                }
                catch (System.Exception) { }
            }
             * */
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (Montants.Count == 0)
            {
                MessageBox.Show("Aucun échéancier n'a été définit");
                return;
            }

            Correspondant praticien = MgmtCorrespondants.getCorrespondant(CurrentPatient.infoscomplementaire.PraticienResponsable.Id);

            BaseCommonControls.CommonActions.AddCourrierAttributsNEwEch(Montants.Cast<BaseTempEcheanceDefinition>().ToList(), praticien, CurrentPatient);


            string CourrierRecu =  templateFolder + System.Configuration.ConfigurationManager.AppSettings["Echeancier"];

            if ((CourrierRecu==null) || (!File.Exists(CourrierRecu)))
            {
                MessageBox.Show("Aucun courrier d'echeancier parametré !\n cle:Echeancier dans .config");
                return;
            }

            OLEAccess.BASLetter.GenerateFrom(CourrierRecu.Trim());
        }

        private void dgvEcheances_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {

            MgmtEcheance.RemoveAndRecalculate(Montants, (BaseTempEcheanceDefinition)e.Row.Tag);
                InitDisplay();
            
        }

        

       

        

        private void dgvEcheances_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            BaseTempEcheanceDefinition tedo = (BaseTempEcheanceDefinition)dgvEcheances.SelectedRows[0].Tag;
            double MontantOrigin = tedo.Montant;
            if (tedo.AlreadyPayed) return;

            FrmEditTempEcheanceDef frm = new FrmEditTempEcheanceDef(tedo);
            if (frm.ShowDialog() == DialogResult.OK)
            {

                if (Math.Round(MontantOrigin,2) != tedo.Montant)
                {
                    int nbvar = 0;
                    foreach (BaseTempEcheanceDefinition ted in Montants)
                    {
                        if ((ted.CanRecalculate) && (ted.acte.Equals(tedo.acte))) nbvar++;
                    }
                    if (nbvar == 0)
                    {
                        MessageBox.Show("Impossible de modifier le montant de cette echeance");
                        tedo.CanRecalculate = true;
                        tedo.Montant = MontantOrigin;
                    }
                    else
                    {
                        if (Montants.Count <= 1) return;
                        if (!MgmtEcheance.ModifyAndRecalculate(Montants, MontantOrigin, tedo))
                        {
                            MessageBox.Show("Montant de cette écheance incorrect");
                            tedo.CanRecalculate = true;
                            tedo.Montant = MontantOrigin;
                        }
                    }
                }
                InitDisplay();
            }
        }

       

        private void NbPeriodicite_Load(object sender, EventArgs e)
        {
            
        }

        private void NbPeriodicite_MouseClick(object sender, MouseEventArgs e)
        {

        }

       

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {



        }

       

       
       

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

      
        private void ShowPanel(Panel pnl)
        {
            pnlContainer.Controls.Clear();
            pnlContainer.Controls.Add(pnl);
        }

       
     
        
      


        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {



            if (dgvEcheances.SelectedRows.Count == 0) return;
            if (!(dgvEcheances.SelectedRows[0].Tag is BaseTempEcheanceDefinition)) return;


            List<BaseTempEcheanceDefinition> lst = new List<BaseTempEcheanceDefinition>();

            foreach (DataGridViewRow dr in dgvEcheances.SelectedRows)
            {
                BaseTempEcheanceDefinition selectedted = (BaseTempEcheanceDefinition)dr.Tag;
                if (!selectedted.AlreadyPayed) lst.Add(selectedted);
            }

            if (lst.Count==0) return;

            FRmCreateEch frm;
                
           if (Currentproposition!=null)
                        frm = new FRmCreateEch(CodesTraitement.IsAdulte(Currentproposition.traitements[0].CodeTraitement));
           else
               frm = new FRmCreateEch(true);
           

            frm.EchOriginals = lst;

            if ((frm.ShowDialog() == System.Windows.Forms.DialogResult.OK) && (frm.Montants.Count > 0) && (frm.erreur == ""))
            {
                foreach (BaseTempEcheanceDefinition ted in lst)
                    _Montants.Remove(ted);
                foreach (BaseTempEcheanceDefinition ted in frm.Montants)
                {   

                    Montants.Add(ted);
                }
                
                if (Montants.Count == 1)
                {
                    button6.Visible = false;
                }
                else
                {
                    button6.Visible = true;
                }
                InitDisplay();
            }
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvEcheances_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (dgvEcheances.Rows.Count < 2) e.Cancel = true;
            BaseTempEcheanceDefinition ted = (BaseTempEcheanceDefinition)dgvEcheances.SelectedRows[0].Tag;

            if (ted.AlreadyPayed) e.Cancel = true;

            if (e.Row.Index == -1) e.Cancel = true;

            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dgvEcheances.Rows.Count < 2) return;
            
        MgmtEcheance.RemoveAllAndRecalculate(Montants,typeScenario);
            if (Montants.Count == 1)
            {
                button6.Visible = false;
            }
            else
            {
                button6.Visible = true;
            }
            InitDisplay();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Montants.Count == 0)
            {
                MessageBox.Show("Aucun échéancier n'a été définit");
                return;
            }

            Correspondant praticien = MgmtCorrespondants.getCorrespondant(CurrentPatient.infoscomplementaire.PraticienResponsable.Id);

            BaseCommonControls.CommonActions.AddCourrierAttributsNEwEch(Montants, praticien, CurrentPatient);


            string CourrierRecu = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Echeancier"];

            if (CourrierRecu == null)
            {
                MessageBox.Show("Aucun courrier Echeancier parametrée !\n cle:Echeancier dans .config");
                return;
            }

            OLEAccess.BASLetter.GenerateFrom(CourrierRecu.Trim());
        }

        private void dgvEcheances_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEcheances.SelectedRows.Count == 0) return;
            List<TempEcheanceDefinition> lst = new List<TempEcheanceDefinition>();
            foreach (DataGridViewRow dr in dgvEcheances.SelectedRows)
            {
                if (!(dr.Tag is TempEcheanceDefinition)) continue;
                if (((TempEcheanceDefinition)dr.Tag).payeur == Echeance.typepayeur.Secu || (((TempEcheanceDefinition)dr.Tag).payeur == Echeance.typepayeur.Mutuelle))
                {
                    btnAdd.Visible = false;
                    button5.Visible = false;
                    button6.Visible = false;
                    break;
                }
                else
                {
                    btnAdd.Visible = true;
                    button5.Visible = true;
                    button6.Visible = true;
                }

            }
        }
    }
}
