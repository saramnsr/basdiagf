using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BL;
using BASEDiag_BO;

namespace BASEDiag
{
    public partial class FrmModelesProposition : Form
    {


        public ModeleDePropositions value
        {
            get
            {
                return (ModeleDePropositions)lstbxModels.SelectedItem;
            }
           
        }

        public FrmModelesProposition()
        {
            InitializeComponent();
        }

        private void FrmModelesProposition_Load(object sender, EventArgs e)
        {
            List<ModeleDePropositions> lst = PropositionMgmt.getModeles();

            foreach (ModeleDePropositions mdl in lst)
                lstbxModels.Items.Add(mdl);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lstbxModels.SelectedItem == null) return;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lstbxModels_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
