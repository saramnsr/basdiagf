using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BASEDiag
{
    public partial class FrmDuree : Form
    {

        public bool isPediatrie = false;

        private int _Phase1 = -1;
        public int Phase1
        {
            get
            {
                return _Phase1;
            }
            set
            {
                _Phase1 = value;
            }
        }

        private int _Phase2 = -1;
        public int Phase2
        {
            get
            {
                return _Phase2;
            }
            set
            {
                _Phase2 = value;
            }
        }

        public FrmDuree()
        {
            InitializeComponent();
        }

        private void rb1Phase_CheckedChanged(object sender, EventArgs e)
        {

            isPediatrie = rb1PhasePediatrie.Checked;

          
            pnl1Phase.Visible = false;
            pnl2Phase1.Visible = false;
            pnl2Phases2.Visible = false;

            if (rb1Phase.Checked) pnl1Phase.Visible = true;
            if (rb2Phases.Checked) pnl2Phase1.Visible = true;

            if (rb1PhasePediatrie.Checked)
            {
                Phase1 = 6;
                Phase2 = 0;

                DialogResult = DialogResult.OK;
                Close();
            }




        }

        private void rb5Semestres_CheckedChanged(object sender, EventArgs e)
        {

            Phase1 = Convert.ToInt16(((RadioButton)sender).Tag);
            Phase2 = 0;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Phase1 = Convert.ToInt16(((RadioButton)sender).Tag);
            Phase2 = 0;
            pnl2Phase1.Visible = false;
            pnl2Phases2.Visible = true;

        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            Phase2 = Convert.ToInt16(((RadioButton)sender).Tag);

            DialogResult = DialogResult.OK;
            Close();
        }
       

        private void rb2Semestres_Paint(object sender, PaintEventArgs e)
        {
            if (sender is RadioButton)
            {
                RadioButton rb = ((RadioButton)sender);

                if (rb.Checked)
                    e.Graphics.DrawImage(global::BASEDiag.Properties.Resources.check, new Point(0, 0));

            }
        }
    }
}
