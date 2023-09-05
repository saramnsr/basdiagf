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
    public partial class FrmMoyens : Form
    {

        private TemplateActePG _traitement;
        public TemplateActePG traitement
        {
            get
            {
                return _traitement;
            }
            set
            {
                _traitement = value;
            }
        }


        public FrmMoyens()
        {
            InitializeComponent();
        }

        private void rbmasquedelaire_Paint(object sender, PaintEventArgs e)
        {
            if (((Control)sender).Parent.Enabled == false) return;
           

                if (sender is CheckBox)
                {
                    CheckBox rb = ((CheckBox)sender);

                    switch (rb.CheckState)
                    {
                        case CheckState.Indeterminate:
                            e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.Interogation, new Point(0, 0));
                            //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, 255, 255, 255)), new Rectangle(0, 0, rb.Width, rb.Height));
                            break;
                        case CheckState.Checked: e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.check, new Point(0, 0));
                            break;


                    }
                }

                if (sender is RadioButton)
                {
                    RadioButton rb = ((RadioButton)sender);

                    if (rb.Checked)
                        e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.check, new Point(0, 0));

                }
            
            base.OnPaint(e);
        }

        private void rbMultiBague_CheckedChanged(object sender, EventArgs e)
        {
            pnlMateriauxMB.Enabled = rbMultiBague.Checked;
            pnlGBE.Enabled = rbInvi.Checked;

            
            if (sender == rbInvi)
                traitement = TemplateApctePGMgmt.getCodeSecu(ResumeCliniqueMgmt.INVISALIGN);
        }

        private void rbMetal_Click(object sender, EventArgs e)
        {
            if (sender == rbMetal)
                traitement = TemplateApctePGMgmt.getCodeSecu(ResumeCliniqueMgmt.MULTIBAGUEMETAL);
if (sender == rbCeram)
                traitement = TemplateApctePGMgmt.getCodeSecu(ResumeCliniqueMgmt.MULTIBAGUECERAM);
if (sender == rbLingual)
                traitement = TemplateApctePGMgmt.getCodeSecu(ResumeCliniqueMgmt.MULTIBAGUELINGU);
        }

        private void rbMetal_CheckedChanged(object sender, EventArgs e)
        {
            pnlTIM.Enabled = rbMetal.Checked || rbCeram.Checked || rbLingual.Checked;
        }

        private void pnlMateriauxMB_EnabledChanged(object sender, EventArgs e)
        {
            pnlTIM.Enabled = (rbMetal.Checked || rbCeram.Checked || rbLingual.Checked) && pnlMateriauxMB.Enabled;
        }

        private void pnlTIM_EnabledChanged(object sender, EventArgs e)
        {
            pnlClasses.Enabled = chkbxTIM.CheckState != CheckState.Indeterminate && pnlTIM.Enabled;
        }

        private void chkbxTIM_Click(object sender, EventArgs e)
        {
            pnlClasses.Enabled = chkbxTIM.CheckState != CheckState.Indeterminate;
        }

        private void rbCL_I_CheckedChanged(object sender, EventArgs e)
        {
            pnl2332.Enabled = rbCL_I.Checked || rbCL_II.Checked || rbCL_III.Checked;
        }

        private void panel4_EnabledChanged(object sender, EventArgs e)
        {
            pnl2332.Enabled = (rbCL_I.Checked || rbCL_II.Checked || rbCL_III.Checked) && pnlClasses.Enabled;
        }

        private void rb23_CheckedChanged(object sender, EventArgs e)
        {
            pnlJourNuit.Enabled = (rb23.Checked || rb32.Checked );
        }

        private void panel5_EnabledChanged(object sender, EventArgs e)
        {
            pnlJourNuit.Enabled = (rb23.Checked || rb32.Checked) && pnl2332.Enabled;
        }

        private void pnlGBE_EnabledChanged(object sender, EventArgs e)
        {
            pnlInvType.Enabled = pnlGBE.Enabled && chkbxGBE.CheckState != CheckState.Indeterminate;
        }

        private void chkbxGBE_Click(object sender, EventArgs e)
        {
            pnlInvType.Enabled = chkbxGBE.CheckState != CheckState.Indeterminate;
        }

        private void rbRCC_CheckedChanged(object sender, EventArgs e)
        {
            pnlQH.Visible = rbQuadHelix.Checked;
            pnlRCC.Visible = rbRCC.Checked;
            pnlRCC2.Visible = rbRCC.Checked;

            if (sender == rbmasquedelaire)
                traitement = TemplateApctePGMgmt.getCodeSecu(ResumeCliniqueMgmt.MASQUEDELAIRE);
            if (sender == rbRCC)
                traitement = TemplateApctePGMgmt.getCodeSecu(ResumeCliniqueMgmt.RCC);
            if (sender == rbArcLingual)
                traitement = TemplateApctePGMgmt.getCodeSecu(ResumeCliniqueMgmt.ARCLINGUAL);
            if (sender == rbQuadHelix)
                traitement = TemplateApctePGMgmt.getCodeSecu(ResumeCliniqueMgmt.QUADHELIX);
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
