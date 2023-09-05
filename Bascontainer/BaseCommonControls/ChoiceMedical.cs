using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BO;
using BasCommon_BL;

namespace BaseCommonControls
{
    public partial class ChoiceMedical : UserControl
    {


        public event EventHandler OnSelectionChanged;


        private TypePersonne _Value = null;
        public TypePersonne Value
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
        
       
       
        public ChoiceMedical( )
        {
            InitializeComponent();
        }

        private void ChoiceMedical_Load(object sender, EventArgs e)
        {

            if (!DesignMode)
            {
                List<BaseCommonControls.FamilyValue> lst = new List<BaseCommonControls.FamilyValue>();

                BaseCommonControls.FamilyValue fv;


                foreach (TypePersonne tp in MgmtTypePersonne.TypePersonnes)
                {
                    if (!MgmtTypePersonne.IsCategorieMedical(tp)) continue;
                    fv = new BaseCommonControls.FamilyValue("", tp.Nom, tp);
                    lst.Add(fv);
                }

                lstprofessionMed.LoadFromFamilyValueList(lst);


                lst.Clear();

                foreach (TypePersonne tp in MgmtTypePersonne.TypePersonnes)
                {
                    if (!MgmtTypePersonne.IsCategorieParaMedical(tp)) continue;
                    fv = new BaseCommonControls.FamilyValue("", tp.Nom, tp);
                    lst.Add(fv);
                }

                lstProfessionPara.LoadFromFamilyValueList(lst);

                lst.Clear();

                foreach (TypePersonne tp in MgmtTypePersonne.TypePersonnes)
                {
                    if (!MgmtTypePersonne.IsCategorieAutre(tp)) continue;
                    fv = new BaseCommonControls.FamilyValue("", tp.Nom, tp);
                    lst.Add(fv);
                }

                lstProfessionAutre.LoadFromFamilyValueList(lst);

            }
        }

       


        private void lstprofession_OnSelectionChange(object sender, EventArgs e)
        {

            TypePersonne t = null;

            if (lstprofessionMed.SelectedItems.Count > 0)
                t = (TypePersonne)lstprofessionMed.SelectedItems[0].Tag;
            else
                if (lstProfessionPara.SelectedItems.Count > 0)
                    t = (TypePersonne)lstProfessionPara.SelectedItems[0].Tag;
                else
                    if (lstProfessionAutre.SelectedItems.Count > 0)
                        t = (TypePersonne)lstProfessionAutre.SelectedItems[0].Tag;

            Value = t;

            if (OnSelectionChanged != null) OnSelectionChanged(this, new EventArgs());

        }

        private void lstprofessionMed_Load(object sender, EventArgs e)
        {

        }
    }
}
