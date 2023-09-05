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

namespace BASEDiagAdulte
{
    public partial class frmChoixASsPrat : Form
    {

        public Utilisateur praticien
        {
            get
            {
                return cbxPratResp.SelectedValue;
            }
           
        }

        public Utilisateur assistante
        {
            get
            {
                return cbxAssResp.SelectedValue;
            }
           
        }
              

        public DateTime DateDebutTraitement
        {
            get
            {
                return dtpdebuttrmnt.Value;
            }
            
        }

        public frmChoixASsPrat()
        {
            InitializeComponent();

            ShowPanel(pnlAssPrat);
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

        private void ShowPanel(Panel pnl)
        {
            ShowPanel(pnl, true);
        }

        private void BackPanel()
        {
            if (histo.Count == 0) return;
            Panel pnl = (Panel)histo.Pop();
            ShowPanel(pnl, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowNextPnl();
        }

        private void ShowNextPnl()
        {
            if (currentpnl == pnlAssPrat)
            {
                ShowPanel(pnlDates);
                return;
            }
            if (currentpnl == pnlDates)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BackPanel();
        }

        private void frmWizardCreationDevis_Load(object sender, EventArgs e)
        {

        }

    
    }
}
