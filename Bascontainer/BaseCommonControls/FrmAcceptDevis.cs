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

namespace BaseCommonControls
{
    public partial class FrmAcceptDevis : Form
    {


        private List<Proposition> _Value = new List<Proposition>();
        public List<Proposition> Value
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

        private List<ActePGPropose> _acteschoisis = new List<ActePGPropose>();
        public List<ActePGPropose> acteschoisis
        {
            get
            {
                return _acteschoisis;
            }
            set
            {
                _acteschoisis = value;
            }
        }


        private Devis _CurrentDevis;
        public Devis CurrentDevis
        {
            get
            {
                return _CurrentDevis;
            }
            set
            {
                _CurrentDevis = value;
            }
        }

        public FrmAcceptDevis(Devis devis)
        {
            CurrentDevis = devis;
            InitializeComponent();
        }

        private void FrmAcceptDevis_Load(object sender, EventArgs e)
        {
            if (CurrentDevis.propositions == null)
                CurrentDevis.propositions = PropositionMgmt.getPropositions(CurrentDevis);

            foreach (Proposition p in CurrentDevis.propositions)
            {
                lstBxPropositions.Items.Add(p);
            }

            if (_CurrentDevis.propositions.Count == 1)
                lstBxPropositions.SetItemChecked(0, true);

            if (CurrentDevis.actesHorstraitement == null)
                CurrentDevis.actesHorstraitement =  MgmtDevis.getactesHorstraitement(CurrentDevis);

            foreach (ActePGPropose p in CurrentDevis.actesHorstraitement)
            {
                if (p.Optionnel)
                    lstbxOptions.SetItemChecked(lstbxOptions.Items.Add(p), true);
                else
                    acteschoisis.Add(p);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            if (lstBxPropositions.CheckedItems.Count == 0)
            {
                MessageBox.Show("Aucune selection !");
                return;
            }
            foreach (Proposition p in lstBxPropositions.CheckedItems)
            {
                Value.Add(p);
            }

            foreach (ActePGPropose p in lstbxOptions.CheckedItems)
            {
                acteschoisis.Add(p);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
