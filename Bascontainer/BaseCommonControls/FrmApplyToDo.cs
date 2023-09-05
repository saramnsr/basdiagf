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
    public partial class FrmApplyToDo : Form
    {



        private Proposition _selectedproposition;
        public Proposition selectedproposition
        {
            get
            {
                return _selectedproposition;
            }
            set
            {
                _selectedproposition = value;
            }
        } 
        

        public ScenarioCommClinique value
        {
            get
            {
                if (lstbxScenarios.SelectedItems.Count == 0) return null;
                return ((ScenarioCommClinique)lstbxScenarios.SelectedItems[0].Tag);
            }
            
        }

        public FrmApplyToDo(Proposition selProposition)
        {
            InitializeComponent();


            this.selectedproposition = selProposition;
            
        }

        private void InitDisplayForContention()
        {

            List<FamilyValue> lst = new List<FamilyValue>();

            foreach (ScenarioCommClinique scenar in MgmtScenarioCommClinique.scenarios)
            {
                if (scenar.typettmnt != ScenarioCommClinique.enumtypePropositon.Contention) continue;

                lst.Add(new FamilyValue("",scenar.Libelle,scenar));

            }
            lstbxScenarios.LoadFromFamilyValueList(lst);


        }

        private void InitDisplayForSemestre()
        {
            List<FamilyValue> lst = new List<FamilyValue>();

            foreach (ScenarioCommClinique scenar in MgmtScenarioCommClinique.scenarios)
            {

                
                if (scenar.NbSemestres == selectedproposition.traitements[0].semestres.Count)
                {
                    if ((selectedproposition.traitements[0].Phase == BasCommon_BO.Traitement.EnumPhase.Pédiatrique) &&
                        (scenar.typettmnt == ScenarioCommClinique.enumtypePropositon.Sucette))
                        lst.Add(new FamilyValue("",scenar.Libelle,scenar));

                    if ((selectedproposition.traitements[0].Phase == BasCommon_BO.Traitement.EnumPhase.Orthopedique) &&
                        (scenar.typettmnt == ScenarioCommClinique.enumtypePropositon.Orthopedie))
                        lst.Add(new FamilyValue("", scenar.Libelle, scenar));

                    if ((selectedproposition.traitements[0].Phase == BasCommon_BO.Traitement.EnumPhase.Orthodontique) &&
                        (scenar.typettmnt == ScenarioCommClinique.enumtypePropositon.Orthodontie))
                        lst.Add(new FamilyValue("", scenar.Libelle, scenar));

                    if ((selectedproposition.traitements[0].Phase == BasCommon_BO.Traitement.EnumPhase.Adulte) &&
                        (scenar.typettmnt == ScenarioCommClinique.enumtypePropositon.Adulte))
                        lst.Add(new FamilyValue("", scenar.Libelle, scenar));
                }
            }
            lstbxScenarios.LoadFromFamilyValueList(lst);
            
        }


       


        private void FrmApplyToDo_Load(object sender, EventArgs e)
        {
            if (selectedproposition.traitements[0].semestres[0].CodeSemestre == "CONT")
                InitDisplayForContention();
            else
                InitDisplayForSemestre();
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

        private void lstbxScenarios_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void lstbxScenarios_OnSelectionChange(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
