using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BASEDiagAdulte
{
    public partial class FrmObjTrmnt : Form
    {


        public string Resultat
        {
            get
            {
                return txtbxObjTrmnt.Text;
            }
           
        }

        public FrmObjTrmnt()
        {
            InitializeComponent();
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


        private void InitDisplay()
        {
           List<BASEDiag_BO.ObjectifSuggests> lst =  BASEDiag_BL.DiagObjTratmntSuggestedMgmt.getAllDiagnostiques();

           foreach (BASEDiag_BO.ObjectifSuggests os in lst)
           {
               ListViewItem itm = new ListViewItem();
               itm.Text = os.Libelle;
               itm.Tag = os;
               lvDescription.Items.Add(itm);
           }

        }

        private void FrmObjTrmnt_Load(object sender, EventArgs e)
        {
            InitDisplay();
            
        }

        private void txtbxSearch_TextChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem itm in lvDescription.Items)
            {
                if (itm.Text.ToUpper().Contains(txtbxSearch.Text.ToUpper()))
                {
                    itm.Selected = true;
                    itm.EnsureVisible();
                }
                else
                    itm.Selected = false;

            }
        }

        private void FrmObjTrmnt_Shown(object sender, EventArgs e)
        {
            txtbxSearch.Focus();
        }

        private void lvDescription_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                if ((Math.Abs(DownAt.X - e.X) > 5) || (Math.Abs(DownAt.Y - e.Y) > 5))
                {
                    List<ListViewItem> lstitm = new List<ListViewItem>();

                    foreach (ListViewItem itm in lvDescription.Items)
                        if (itm.Selected) lstitm.Add(itm);

                    DataObject dobj = new DataObject();
                    dobj.SetData("List", lstitm);
                    lvDescription.DoDragDrop(dobj, DragDropEffects.Move);
                }
        }

        Point DownAt;
        private void lvDescription_MouseDown(object sender, MouseEventArgs e)
        {
            DownAt = new Point(e.X, e.Y);
        }

        private void txtbxObjTrmnt_DragDrop(object sender, DragEventArgs e)
        {
            List<ListViewItem> lstitm = (List<ListViewItem>)e.Data.GetData("List");
            foreach (ListViewItem itm in lstitm)
            {
                txtbxObjTrmnt.Text += txtbxObjTrmnt.Text == "" ? "" : "\r\n";
                txtbxObjTrmnt.Text +=  itm.Text;
            }
        }

        private void txtbxObjTrmnt_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("List"))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lvDescription_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem itm in lvDescription.SelectedItems)
            {
                txtbxObjTrmnt.Text += txtbxObjTrmnt.Text == "" ? "" : "\r\n";
                txtbxObjTrmnt.Text += itm.Text;
            }
        }
    }
}
