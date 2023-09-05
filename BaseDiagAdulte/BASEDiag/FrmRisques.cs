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
using System.IO;
namespace BASEDiagAdulte
{
    public partial class FrmRisques : Form
    {



        private Proposition _proposition;
        public Proposition proposition
        {
            get
            {
                return _proposition;
            }
            set
            {
                _proposition = value;
            }
        }
        
        public FrmRisques(Proposition prop)
        {
            proposition = prop;
            InitializeComponent();

        }

        private void FrmRisquesPerso_Load(object sender, EventArgs e)
        {

            
            foreach (string s in PropositionMgmt.GetRisques(proposition))
            {
                TreeNode tn = new TreeNode();
                tn.Text = s.Trim();
                tn.Tag = s;
                tvRisques.Root.Nodes.Add(tn);
            }

            tvRisques.ForceRefresh();
            

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

        private void FrmRisquesPerso_Shown(object sender, EventArgs e)
        {
        }

        private void tvRisques_OnSelected(object sender, EventArgs e)
        {


            string folder = System.Configuration.ConfigurationManager.AppSettings["PPTFolder"]+"\\Risques\\";

            if (!Directory.Exists(folder))
            {
                try
                {
                    Directory.CreateDirectory(folder);
                }
                catch (System.Exception)
                {
                    MessageBox.Show(folder + " n'existe pas!");
                    return;
                }
            }

            string file = folder + ((TreeNode)sender).Text+".ppt";

            if (File.Exists(file))
                System.Diagnostics.Process.Start("POWERPNT.EXE","/s \"" + file+"\"");
            else
                System.Diagnostics.Process.Start(folder);
            
        }
    }
}
