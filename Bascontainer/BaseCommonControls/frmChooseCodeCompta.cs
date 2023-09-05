using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BO.Compta;
using BasCommon_BL.Compta;

namespace BaseCommonControls
{
    public partial class frmChooseCodeCompta : Form
    {


        public CodeComptable Value { get; set; }


        public frmChooseCodeCompta()
        {
            InitializeComponent();
        }

        private void frmChooseCodeCompta_Shown(object sender, EventArgs e)
        {
            txtbxSearch.Focus();
        }

        private void txtbxSearch_TextChanged(object sender, EventArgs e)
        {

            search();
        }

        private void search()
        {
            var lst = MgmtCodeComptable.codescomptables.Where(cc => cc.Code.StartsWith(txtbxSearch.Text)).ToList();
            lstbxCode.DataSource = lst;
        }

        private void lstbxCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Value = (CodeComptable)lstbxCode.SelectedItem;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void lstbxCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void frmChooseCodeCompta_Load(object sender, EventArgs e)
        {
           lstbxCode.DataSource = MgmtCodeComptable.codescomptables;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAddCodeComptable frm = new FrmAddCodeComptable();

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MgmtCodeComptable.Add(frm.Value);
                search();
            }
        }
    }
}
