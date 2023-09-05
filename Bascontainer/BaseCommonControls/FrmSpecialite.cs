using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseCommonControls
{
    public partial class FrmSpecialite : Form
    {


        public BasCommon_BO.TypePersonne Value
        {
            get
            {
                return choixProfession.Value;
            }
           
        }
        
        public FrmSpecialite()
        {
            InitializeComponent();
        }

        private void FrmSpecialite_Load(object sender, EventArgs e)
        {
            choixProfession.OnSelectionChanged += choixProfession_OnSelectionChanged;
        }

        void choixProfession_OnSelectionChanged(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close(); 
        }
    }
}
