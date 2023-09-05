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

namespace BASEDiag
{
    public partial class FrmOptionDevisInvisalign : Form
    {


        private Devis _devis;
              public Devis devis 
              { 
                get { 
                      return _devis; 
                    } 
                set { 
                      _devis = value; 
                    } 
              }


        public FrmOptionDevisInvisalign()
        {
            InitializeComponent();
        }

        public FrmOptionDevisInvisalign(Devis d)
        {
            InitializeComponent();
            _devis = d;
        }

        private void Build()
        {
            if (devis == null)
                devis = new Devis();

            devis.FacetteEsthetique = chkbxFacette.Checked;
            devis.KitEclaircissement = chkbxEclair.Checked;
            devis.KitTractionSurMiniVis = chkbxTraction.Checked;

            devis.ContentionBAS1Arcade = rbContBAS1Arc.Checked;
            devis.ContentionBAS2Arcades = rbContBAS2Arc.Checked;
            devis.ContentionBASJeu = rbContBASJeu.Checked;
            devis.ContentionVIVERA1Arcade = rbContVIVERA1Arc.Checked;
            devis.ContentionVIVERA2Arcades = rbContVIVERA2Arc.Checked;
            devis.ContentionVIVERAJeu = rbContVIVERAJeu.Checked;

            devis.ContentionFilMetal1Arcade = rbMaintienMetal1ARC.Checked;
            devis.ContentionFilMetal2Arcade = rbMaintienMetal2ARC.Checked;
            devis.ContentionFilOr1Arcade = rbMaintienOr1ARC.Checked;
            devis.ContentionFilOr2Arcades = rbMaintienOr2ARC.Checked;
            devis.ContentionFilFibre1Arcade = rbMaintienFibre1ARC.Checked;
            devis.ContentionFilFibre2Arcades = rbMaintienFibre2ARC.Checked;
            devis.NbMiniVis = (int)txtbxMiniVis.Value;

        }


        private void InitDisplay()
        {
            if (devis == null) return;

            chkbxFacette.Checked = devis.FacetteEsthetique;
            chkbxEclair.Checked = devis.KitEclaircissement;
            chkbxTraction.Checked = devis.KitTractionSurMiniVis;

            rbContBAS1Arc.Checked = devis.ContentionBAS1Arcade;
            rbContBAS2Arc.Checked = devis.ContentionBAS2Arcades;
            rbContBASJeu.Checked = devis.ContentionBASJeu;
            rbContVIVERA1Arc.Checked = devis.ContentionVIVERA1Arcade;
            rbContVIVERA2Arc.Checked = devis.ContentionVIVERA2Arcades;
            rbContVIVERAJeu.Checked = devis.ContentionVIVERAJeu;

            rbMaintienMetal1ARC.Checked = devis.ContentionFilMetal1Arcade;
            rbMaintienMetal2ARC.Checked = devis.ContentionFilMetal2Arcade;
            rbMaintienOr1ARC.Checked = devis.ContentionFilOr1Arcade;
            rbMaintienOr2ARC.Checked = devis.ContentionFilOr2Arcades;
            rbMaintienFibre1ARC.Checked = devis.ContentionFilFibre1Arcade;
            rbMaintienFibre2ARC.Checked = devis.ContentionFilFibre2Arcades;
            txtbxMiniVis.Value = devis.NbMiniVis;

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Build();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FrmOptionDevisInvisalign_Load(object sender, EventArgs e)
        {
            InitDisplay();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            pnlContentionBAS.Visible = checkBox4.Checked;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            pnlContentionVIVERA.Visible = checkBox5.Checked;
        }
    }
}
