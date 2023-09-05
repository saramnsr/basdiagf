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

namespace BaseCommonControls
{
    public partial class FrmChoixCorrespondant : Form
    {



        private baseSmallPersonne _LinkedPersonne = null;
        public baseSmallPersonne LinkedPersonne
        {
            get
            {
                return _LinkedPersonne;
            }
            set
            {
                _LinkedPersonne = value;
            }
        }
        

        public List<int> _ExcludeFromSearch = new List<int>();
        public List<int> ExcludeFromSearch
        {
            get
            {
                return _ExcludeFromSearch;
            }
            set
            {
                _ExcludeFromSearch = value;
            }
        }

        private basePatient _CurrentPatient;
        public basePatient CurrentPatient
        {
            get
            {
                return _CurrentPatient;
            }
            set
            {
                _CurrentPatient = value;
            }
        }

        

        private TypePersonne _typepersonnefiltre = null;
        public TypePersonne typepersonnefiltre
        {
            get
            {
                return _typepersonnefiltre;
            }
            set
            {
                _typepersonnefiltre = value;
            }
        }


        private Correspondant _Value;
        public Correspondant Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        private string _Filtre;
        public string Filtre
        {
            get
            {
                return _Filtre;
            }
            set
            {
                _Filtre = value;
            }
        }




        public FrmChoixCorrespondant()
        {
            InitializeComponent();
            Search();
        }

        private void Search()
        {
           List<baseSmallPersonne> correspondants = MgmtCorrespondants.getSmallCorrespondants(textBox1.Text, typepersonnefiltre);

            lbxCorres.Items.Clear();

            foreach (baseSmallPersonne c in correspondants)
            {
                if (ExcludeFromSearch.Contains(c.Id)) 
                    continue;
                lbxCorres.Items.Add(c);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Search();
            ((Timer)sender).Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            timerSearch.Enabled = false;
            timerSearch.Enabled = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Build())
            {
                DialogResult = DialogResult.OK;
                Close();
            }

        }

        private bool Build()
        {
            if (lbxCorres.SelectedItem == null) return false; ;
            Correspondant c = MgmtCorrespondants.getCorrespondant(CurrentPatient.Id);
            Value = c;
            return true;
        }

        private void btnCancel_Click()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FrmChoixCorrespondant_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void lbxCorres_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void lbxCorres_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmChoixCorrespondant_Load(object sender, EventArgs e)
        {
            if (typepersonnefiltre!=null)
                this.Text = "Choix du " + typepersonnefiltre.ToString();
        }

        private void lbxCorres_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbxCorres.SelectedItem == null) return;
            Build();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (lbxCorres.SelectedItem == null) return;
            Build();
            Hide();
            frmEditCorrespondant frmc = new frmEditCorrespondant();

            frmc.correspondant = Value;
            frmc.Owner = this;
            frmc.ShowDialog();
            Show();

            
           
                        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                DialogResult = DialogResult.Ignore;
                Close();
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Hide();
            frmEditCorrespondant frmc = new frmEditCorrespondant();
            frmc.txtbxProfession.Text = typepersonnefiltre==null?"":typepersonnefiltre.ToString();
            if (LinkedPersonne!=null)
            {
                if (LinkedPersonne.contacts == null)
                {
                    if (LinkedPersonne is basePatient)
                        baseMgmtPatient.FillContacts((basePatient)LinkedPersonne);

                    if (LinkedPersonne is Correspondant)
                        MgmtCorrespondants.FillContacts((Correspondant)LinkedPersonne);
                }

                foreach (Contact c in LinkedPersonne.contacts)
                {
                    ListViewItem itm = new ListViewItem();
                    itm.Text = c.Libelle == null ? "" : c.Libelle.Libelle;
                    itm.SubItems.Add(c.Value);

                    if (c.TypeContact == Contact.ContactType.Telephone)
                        itm.Group = frmc.lvContact.Groups[0];
                    if (c.TypeContact == Contact.ContactType.Fax)
                        itm.Group = frmc.lvContact.Groups[1];
                    if (c.TypeContact == Contact.ContactType.Mail)
                        itm.Group = frmc.lvContact.Groups[2];
                    if (c.TypeContact == Contact.ContactType.Adresse)
                        itm.Group = frmc.lvContact.Groups[3];

                    itm.Tag = c;

                    frmc.lvContact.Items.Add(itm);

                    frmc.lstContacts.Add(c);
                }
            }
            frmc.correspondant = null;
            frmc.Owner = this;
            frmc.ShowDialog();
            Search();
            Show();
        }

       
    }
}
