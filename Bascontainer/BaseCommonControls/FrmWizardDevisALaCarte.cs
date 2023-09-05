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
using System.IO;
namespace BaseCommonControls
{
    public partial class FrmWizardDevisALaCarte : Form
    {



        private Devis _devis = new Devis();
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
        

        private Dictionary<string, List<TemplateActePG>> mydico = new Dictionary<string, List<TemplateActePG>>();
        private List<string> orgalst = new List<string>();


        public FrmWizardDevisALaCarte(basePatient currentpatient)
        {
            InitializeComponent();

            devis.DateProposition = DateTime.Now;
            devis.DateEcheance = DateTime.Now.AddDays(15);
            devis.patient = currentpatient;
            devis.TypeDevis = Devis.enumtypePropositon.ALaCarte;
            devis.propositions = null;
            devis.DatePrevisionnelDeDebutTraitement = DateTime.Now.AddDays(15);
            devis.DatePrevisionnelDeFinTraitement = null;
            devis.actesHorstraitement = new List<ActePGPropose>();

            devis.propositions = new List<Proposition>();

        }

        private void FrmWizardDevisALaCarte_Load(object sender, EventArgs e)
        {
            foreach (TemplateActePG a in TemplateApctePGMgmt.templates)
            {
                if (a.TypeDeReglement != ActePG.TypeReglement.AuDevis) continue;
                if (!mydico.ContainsKey(a.Organisation))
                {
                    mydico.Add(a.Organisation, new List<TemplateActePG>());
                    orgalst.Add(a.Organisation);
                }
                
                mydico[a.Organisation].Add(a);
            }

            NextStep();
        }


        int currentstep =-1;

        private void NextStep()
        {
            currentstep++;
            RefreshStep();
        }

        private void BackStep()
        {
            if (currentstep <= 0) return;
            currentstep--;
            RefreshStep();
        }

        private void RefreshStep()
        {
            if (currentstep >= orgalst.Count)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                BuildDevis();
                Close();
                return;
            }
            Text = orgalst[currentstep];

            dgvDevis.Rows.Clear();



            foreach (TemplateActePG tpg in mydico[orgalst[currentstep]])
            {

                bool isfound = false;

                foreach (ActePGPropose a in devis.actesHorstraitement)
                    if (a.template == tpg)
                        isfound = true;


                string file = Path.GetDirectoryName(Application.ExecutablePath) + "\\DevisImages\\" + tpg.Nom + ".jpg";

                Bitmap bmp = null;

                if (File.Exists(file))
                    bmp = (Bitmap)Bitmap.FromFile(file);

                object[] obj = new object[]{
                    isfound,
                    bmp,
                    tpg.Libelle,
                    1,
                    tpg.Valeur
                };

                int idx = dgvDevis.Rows.Add(obj);
                dgvDevis.Rows[idx].Tag = tpg;

            }
        }

      

        private void BuildDevis()
        {          
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveStep();
            NextStep();
        }

        private void SaveStep()
        {

            foreach (DataGridViewRow dr in dgvDevis.Rows)
            {

                TemplateActePG tp = ((TemplateActePG)dr.Tag);

                if ((!(bool)dr.Cells[colChk.Index].Value))
                {
                    foreach (ActePGPropose a in devis.actesHorstraitement)
                        if (a.template == tp)
                        {
                            devis.actesHorstraitement.Remove(a);
                            break;
                        }
                }
                else
                {
                    ActePGPropose acte = new ActePGPropose();
                    acte.DateExecution = DateTime.Now.AddDays(15);
                    acte.devis = devis;
                    acte.Libelle = tp.Libelle;
                    acte.Montant = (dr.Cells[colTarif.Index].Value == null ? tp.Valeur : (double)dr.Cells[colTarif.Index].Value);
                    acte.MontantAvantRemise = acte.Montant;
                    acte.Optionnel = true;
                    acte.Qte = Convert.ToInt32(dr.Cells[colQte.Index].Value);
                    acte.template = tp;

                    devis.actesHorstraitement.Add(acte);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvDevis.Rows)
                dr.Cells[colChk.Index].Value = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvDevis.Rows)
                dr.Cells[colChk.Index].Value = false;
        }

        private void dgvDevis_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            
        }

        private void dgvDevis_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == colQte.Index)
            {

                int qte = Convert.ToInt32(dgvDevis.Rows[e.RowIndex].Cells[colQte.Index].Value);
                TemplateActePG a = (TemplateActePG) dgvDevis.Rows[e.RowIndex].Tag;
                dgvDevis.Rows[e.RowIndex].Cells[colTarif.Index].Value = a.Valeur * qte;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveStep();
            BackStep();
        }
    }
}
