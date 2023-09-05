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
    public partial class FrmLnkCorrespondant : Form
    {

        private string _value;
        public string value
        {
            get
            {
                return ((lnk)cbxlink.SelectedTag).Res;
            }

        }

        private class lnk
        {
            public string Res;
            public string Libelle;

            public override string ToString()
            {
                return Libelle;
            }

            public lnk(string res, string libelle)
            {
                Libelle = libelle;
                Res = res;
            }
        }


        public FrmLnkCorrespondant()
        {
            InitializeComponent();

            List<BaseCommonControls.FamilyValue> lst = new List<BaseCommonControls.FamilyValue>();


            foreach (KeyValuePair<string, string> kv in BasCommon_BO.LienCorrespondant.Fonctions)
                lst.Add(new BaseCommonControls.FamilyValue("", kv.Value, new lnk(kv.Key, kv.Value)));

            cbxlink.LoadFromFamilyValueList(lst);

        }

        private void FrmLnkCorrespondant_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public void SelectLink(string key)
        {
            foreach (TreeNode lk in cbxlink.Nodes)
                if (((lnk)lk.Tag).Res == key)
                    cbxlink.SelectedTag = lk.Tag;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
