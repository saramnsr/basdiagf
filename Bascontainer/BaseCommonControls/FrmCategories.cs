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
    public partial class FrmCategories : Form
    {

        private List<CustomCategorie> _ccOrigin = new List<CustomCategorie>();

        private List<CustomCategorie> _ccToRemove = new List<CustomCategorie>();
        public List<CustomCategorie> ccToRemove
        {
            get
            {
                return _ccToRemove;
            }
            set
            {
                _ccToRemove = value;
            }
        }


        private List<CustomCategorie> _ccToAdd = new List<CustomCategorie>();
        public List<CustomCategorie> ccToAdd
        {
            get
            {
                return _ccToAdd;
            }
            set
            {
                _ccToAdd = value;
            }
        }

        private object _CurrentPersonne;
        public object CurrentPersonne
        {
            get
            {
                return _CurrentPersonne;
            }
            set
            {
                if (!(value is Correspondant) && !(value is basePatient))
                    throw new System.Exception("CurrentPersonne doit etre un patient ou un correspondant");
                _CurrentPersonne = value;
            }
        }

        public FrmCategories(basePatient patient)
        {
            CurrentPersonne = patient;
            InitializeComponent();
        }

        public FrmCategories(Correspondant correspondant)
        {
            CurrentPersonne = correspondant;
            InitializeComponent();
        }

        private void Build()
        {
            bool DontCare = false;

            foreach (CustomCategorie cc in _ccOrigin)
            {
                DontCare = false;
                foreach (Categorie c in lstBxCategActuelles.Items)
                {

                    //
                    if (cc.IdCateg == c.IdCateg)
                    {
                        //_ccToRemove.Add(cc);
                        DontCare = true;
                        break;
                    }

                }
                if (!DontCare) _ccToRemove.Add(cc);

            }

             foreach (Categorie c in lstBxCategActuelles.Items)
            {
                DontCare = false;
                foreach (CustomCategorie cc in _ccOrigin)
                {
                    if (cc.IdCateg == c.IdCateg)
                    {
                        DontCare = true;
                        break;
                    }
                }
                if (!DontCare)
                {
                    CustomCategorie newcc = new CustomCategorie();
                    newcc.DateDebutCat = DateTime.Now;
                    newcc.DateFinCat = null;
                    newcc.IdCateg = c.IdCateg;
                    newcc.IdPersonne = CurrentPersonne is basePatient ? ((basePatient)CurrentPersonne).Id : ((Correspondant)CurrentPersonne).Id;
                    newcc.Nom = c.Nom;
                    _ccToAdd.Add(newcc);
                }
            }


        }

        private void InitDisplay()
        {
            List<CustomCategorie> lst = CategoriesMgmt.GetCategoriesFromIdPersonne((CurrentPersonne is basePatient ? ((basePatient)CurrentPersonne).Id : ((Correspondant)CurrentPersonne).Id));

            lvHistorique.Items.Clear();
            lstBxCategActuelles.Items.Clear();

            foreach (CustomCategorie cc in lst)
            {
                if (cc.DateFinCat != null)
                {
                    ListViewItem itm = new ListViewItem();
                    itm.Text = cc.Nom;
                    itm.SubItems.Add(cc.DateDebutCat.ToString("dd/MM/yyyy"));
                    itm.SubItems.Add(cc.DateFinCat.Value.ToString("dd/MM/yyyy"));

                    lvHistorique.Items.Add(itm);
                }
                else
                {
                    Categorie c = new Categorie();
                    c.IdCateg = cc.IdCateg;
                    c.Nom = cc.Nom;
                    lstBxCategActuelles.Items.Add(c);
                    _ccOrigin.Add(cc);
                }

            }




        }

        private void FrmCategories_Load(object sender, EventArgs e)
        {
            InitDisplay();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Build();
            DialogResult = DialogResult.OK;
            Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmChooseCategorie frm = new FrmChooseCategorie();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                foreach (Categorie cc in lstBxCategActuelles.Items)
                    foreach (Categorie ctoinsert in frm.value)
                        if (cc.IdCateg == ctoinsert.IdCateg)
                            return;
                
                foreach (Categorie c in frm.value)
                    lstBxCategActuelles.Items.Add(c);
            }
        }

        private void lstBxCategActuelles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                for (int i=lstBxCategActuelles.Items.Count-1;i>=0;i--)
                {
                    if (lstBxCategActuelles.SelectedIndices.Contains(i))
                    {
                        lstBxCategActuelles.Items.RemoveAt(i);
                    }
                }

            }
        }
    }
}
