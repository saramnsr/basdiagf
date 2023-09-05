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
    public partial class FrmChooseCategorie : Form
    {



        private List<Categorie> _value = new List<Categorie>();
        public List<Categorie> value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public FrmChooseCategorie()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        private void Build()
        {
            foreach (object o in lstbxCategories.SelectedItems)
            {
                _value.Add((Categorie)o);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Build();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FrmChooseCategorie_Load(object sender, EventArgs e)
        {
            InitDisplay();
        }

        private void InitDisplay()
        {
            lstbxCategories.Items.Clear();
            foreach (Categorie cc in CategoriesMgmt.Categories)
            {
                lstbxCategories.Items.Add(cc);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            FrmString frm = new FrmString("Nom de la categorie","Categorie","");

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Categorie cc = new Categorie();
                cc.Nom = frm.Value;
                CategoriesMgmt.AddCategorie(cc);
                InitDisplay();
            }
        }
    }
}
