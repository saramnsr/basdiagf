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
using BasCommon_BO;
using BasCommon_BL;

namespace BASEDiagAdulte
{
    public partial class FrmChoixFauteuil : Form
    {
        public Fauteuil Selection
        {
            get
            {
                return (Fauteuil)cbxFauteuil.SelectedItem;
            }
            
        }

        public FrmChoixFauteuil()
        {
            InitializeComponent();
        }

        private void FrmChoixFauteuil_Load(object sender, EventArgs e)
        {
            foreach (Fauteuil f in Fauteuilsmgt.fauteuils)
                cbxFauteuil.Items.Add(f);

            cbxFauteuil.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fauteuilsmgt.SetWhoIam((Fauteuil)cbxFauteuil.SelectedItem);
            Close();
        }
    }
}
