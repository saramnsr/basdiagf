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

namespace BASEDiag
{
    public partial class FrmProposition : Form
    {

        private Patient _patient;
        public Patient patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
            }
        }

        private Proposition _proposition;
        public Proposition proposition
        {
            get
            {
                if (_proposition == null) _proposition = new Proposition();
                return _proposition;
            }
            set
            {
                _proposition = value;
            }
        }



        public FrmProposition(Patient patient)
        {
            _patient = patient;
            InitializeComponent();
        }

        private void FrmProposition_Load(object sender, EventArgs e)
        {

        }

        private void pnlDuree_Click(object sender, EventArgs e)
        {

            dgvTarifs.Rows.Clear();

            FrmDuree frm = new FrmDuree();
            if (frm.ShowDialog() == DialogResult.OK)
            {

                proposition.phases.Clear();
                if (frm.isPediatrie)
                {
                    Phase p = new Phase();
                    p.TypeDePhase = Phase.PhaseType.Pediatrie;
                    p.traitement = BASEDiag_BL.TemplateApctePGMgmt.getCodeSecu(ResumeCliniqueMgmt.PEDIATRIE);
                    p.Duree = 6;
                    p.TarifSemestre = p.traitement.Valeur;
                    proposition.phases.Add(p);

                    object[] ob = new object[2] { p.TypeDePhase.ToString() + " " + p.traitement.ToString(), (p.traitement.Valeur) };
                    dgvTarifs.Rows.Add(ob);


                }
                else
                {
                    if (frm.Phase2 != 0)
                    {
                        Phase p = new Phase();
                        p.TypeDePhase = Phase.PhaseType.Orthopedie;
                        p.traitement = null;
                        p.Duree = frm.Phase1;
                        proposition.phases.Add(p);

                        p = new Phase();
                        p.TypeDePhase = Phase.PhaseType.Orthodontie;
                        p.traitement = null;
                        p.Duree = frm.Phase2;
                        proposition.phases.Add(p);

                    }
                    else
                    {
                        Phase p = new Phase();
                        p.TypeDePhase = Phase.PhaseType.Orthodontie;
                        p.traitement = null;
                        p.Duree = frm.Phase1;
                        proposition.phases.Add(p);
                    }
                }

                pnlDuree.Invalidate();
            }
        }

        private void pnlDuree_Paint(object sender, PaintEventArgs e)
        {
            string s = "Duree : ";

            if (proposition.phases.Count > 0)
            {

                s += proposition.phases.Count.ToString() + " phase(s)";


                foreach (Phase p in proposition.phases)
                {
                    s += "\n        " + p.TypeDePhase + " " + p.Duree + " semestres";

                }


            }
            e.Graphics.DrawString(s, pnlDuree.Font, Brushes.Black, new PointF(5, 5));

        }

        private void pnlPlanTraitement_Paint(object sender, PaintEventArgs e)
        {
            string s = "Plan traitement : ";


            foreach (Phase p in proposition.phases)
                if (p.traitement != null)
                    s += "\n    " + p.TypeDePhase.ToString() + " " + p.traitement.ToString();

            e.Graphics.DrawString(s, pnlPlanTraitement.Font, Brushes.Black, new Rectangle(0, 0, pnlPlanTraitementSecu.Width, pnlPlanTraitementSecu.Height));
        }

        private void pnlPlanTraitement_Click(object sender, EventArgs e)
        {
            if (proposition.phases.Count == 0) return;

            if ((proposition.phases.Count == 1) && (proposition.phases[0].TypeDePhase == Phase.PhaseType.Pediatrie))
            {
                MessageBox.Show("1 phase Pédiatrique");
                return;
            }


            FrmMoyens frm = new FrmMoyens();


            foreach (Phase p in proposition.phases)
            {
                if (p.TypeDePhase == Phase.PhaseType.Orthodontie)
                {
                    frm.pnlOrthodontie.Visible = true;
                    frm.pnlOrthopedie.Visible = false;
                }
                if (p.TypeDePhase == Phase.PhaseType.Orthopedie)
                {
                    frm.pnlOrthodontie.Visible = false;
                    frm.pnlOrthopedie.Visible = true;
                }

                if (frm.ShowDialog() == DialogResult.OK)
                {

                    p.traitement = frm.traitement;
                    p.TarifSemestre = p.traitement.Valeur;

                }
                else return;
            }

            dgvTarifs.Rows.Clear();
            proposition.Risques.Clear();


            foreach (Phase p in proposition.phases)
            {

                List<string> lst = new List<string>();

                foreach (Appareil app in p.traitement.appareils)
                    foreach (string s in app.Risques)
                        lst.Add(s);
                
                foreach (string s in _patient.Risques)
                    if (!proposition.Risques.Contains(s))
                        proposition.Risques.Add(s);

                foreach (string s in lst)
                    if (!proposition.Risques.Contains(s))
                        proposition.Risques.Add(s);


                

                object[] ob = new object[2] { p.TypeDePhase.ToString() + " " + p.traitement.ToString(), (p.traitement.Valeur) };
                dgvTarifs.Rows.Add(ob);

            }
            pnlPlanTraitement.Invalidate();
            pnlPlanTraitementSecu.Invalidate();
            pnlRisques.Invalidate();
        }

        private void pnlRisques_Click(object sender, EventArgs e)
        {
            FrmRisques frm = new FrmRisques(proposition);
            frm.ShowDialog();
        }

        private void pnlRisques_Paint(object sender, PaintEventArgs e)
        {
            
            string s = "Risques : ";


            foreach (string ss in proposition.Risques)
            {             
                s += "\n    " + ss;
            }
            e.Graphics.DrawString(s, pnlRisques.Font, Brushes.Black, new Rectangle(0, 0, pnlPlanTraitementSecu.Width, pnlPlanTraitementSecu.Height));
            
        }

        private void pnlPlanTraitementSecu_Paint(object sender, PaintEventArgs e)
        {
            string s = "Plan Secu : ";

            foreach (Phase p in proposition.phases)
            {
                if (p.traitement != null)
                {
                    string pltrmnt  = "";
                    
                    foreach(Appareil app in p.traitement.appareils)
                        pltrmnt += app.InfoDEP + "\n";

                    s += "\n    " + pltrmnt;
                }
            }
            e.Graphics.DrawString(s, pnlPlanTraitementSecu.Font, Brushes.Black, new Rectangle(0, 0, pnlPlanTraitementSecu.Width, pnlPlanTraitementSecu.Height));

        }

        private void dgvTarifs_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < proposition.phases.Count;i++ )
            {
                Double val = 0; ;
                bool convertionOk = true;

                if (dgvTarifs.Rows[i].Cells[1].Value is double)
                    val = (double)dgvTarifs.Rows[i].Cells[1].Value;
                else
                    if (dgvTarifs.Rows[i].Cells[1].Value is string)
                        convertionOk = Double.TryParse((string)dgvTarifs.Rows[i].Cells[1].Value, out val);
                    else
                        convertionOk = false;


                if (!convertionOk)
                {
                    MessageBox.Show("Erreur de convertion");
                    return;
                }
                else
                    proposition.phases[i].TarifSemestre = val;
            }


            proposition.libelle = txtbxLibelle.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
