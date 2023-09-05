using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;
using BASEDiag_BL;
using BasCommon_BL;
using BasCommon_BO;

namespace BASEDiag
{
    public partial class FrmChoixPatient : Form
    {

        int NbPatFortheFauteuil = 0;


        Dictionary<int, baseSmallPersonne> listpat = new Dictionary<int, baseSmallPersonne>();

        public baseSmallPersonne SelectedPatient
        {
            get
            {
                return (baseSmallPersonne)LstBxChoixPatient.SelectedItem;
            }
        }

        public FrmChoixPatient()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FrmChoixPatient_Load(object sender, EventArgs e)
        {
            Fauteuil IamTheFauteuil = Fauteuilsmgt.GetWhoIam();

            if (IamTheFauteuil == null)
            {
                FrmChoixFauteuil frm = new FrmChoixFauteuil();
                frm.ShowDialog();
                IamTheFauteuil = frm.Selection;
            }

            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            if (IamTheFauteuil!=null)
                lst = baseMgmtPatient.getRestrictedPatientsInAttenteFor(IamTheFauteuil);
            foreach (baseSmallPersonne rp in lst)
            {
                listpat.Add(rp.Id,rp);
            }

            NbPatFortheFauteuil = lst.Count;

            lst = baseMgmtPatient.getRestrictedPatientsInAttente();
            foreach(baseSmallPersonne rp in lst)
            {
                if (!listpat.ContainsKey(rp.Id))
                    listpat.Add(rp.Id,rp);
            }
            List<baseSmallPersonne> list = listpat.Values.ToList<baseSmallPersonne>();
            LstBxChoixPatient.DataSource = list;

        }

        private void txtbxSearch_TextChanged(object sender, EventArgs e)
        {
            LstBxChoixPatient.DataSource = baseMgmtPatient.getRestrictedPatients(txtbxSearch.Text);
            NbPatFortheFauteuil = -1;

        }

        private void LstBxChoixPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void LstBxChoixPatient_MouseDown(object sender, MouseEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void LstBxChoixPatient_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            

        }

        private void LstBxChoixPatient_DrawItem(object sender, DrawItemEventArgs e)
        {

            if (e.Index < 0) return;
            Font ft1 = new Font(LstBxChoixPatient.Font.Name, LstBxChoixPatient.Font.Size, FontStyle.Bold);
            Font ft2 = new Font(LstBxChoixPatient.Font.Name, LstBxChoixPatient.Font.Size, FontStyle.Regular);

            baseSmallPersonne pat = ((baseSmallPersonne)LstBxChoixPatient.Items[e.Index]);

            e.DrawBackground();

            if (e.Index < NbPatFortheFauteuil)
            {
                e.Graphics.DrawString(pat.ToString(), ft1, Brushes.Black, new PointF(e.Bounds.X, e.Bounds.Y));
            }
            else
            {
                e.Graphics.DrawString(pat.ToString(), ft2, Brushes.Black, new PointF(e.Bounds.X, e.Bounds.Y));

            }
        }
    }
}
