using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BL;
using BasCommon_BO;

namespace BaseCommonControls.BAS
{
    public partial class WizardPraticienAssistantes : SlindingBtn
    {

      


        public Utilisateur SelectedValue
        {
            get
            {
                return (Utilisateur)this.SelectedTag;
            }
            set
            {
                this.SelectedTag = value;
            }
        }


        private bool _IncludeAssistantes = true;
        public bool IncludeAssistantes
        {
            get
            {
                return _IncludeAssistantes;
            }
            set
            {
                _IncludeAssistantes = value;
                InitDatas();
            }
        }

        private bool _IncludePraticien = true;
        public bool IncludePraticien
        {
            get
            {
                return _IncludePraticien;
            }
            set
            {
                _IncludePraticien = value;
                InitDatas();
            }
        }

        public WizardPraticienAssistantes()
        {
            InitializeComponent();

            WrapMode = true;
            Text = "<Aucun>";

            WindowHeight = -1;
            WindowWidth = 400;

            FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Font = new Font("Garamond",11,FontStyle.Regular,GraphicsUnit.Point);
            BackColor = Color.WhiteSmoke;
            
            

                InitDatas();
            
        }

        private void InitDatas()
        {
            if (DesignMode) return;
            try
            {

                bool Includeorga = IncludeAssistantes && IncludePraticien;


                List<BaseCommonControls.FamilyValue> lst = new List<BaseCommonControls.FamilyValue>();

                if (_IncludePraticien)
                {
                    foreach (Utilisateur u in UtilisateursMgt.utilisateurs)
                        if (u.Actif)
                        {
                            if ((u.Fonction == "Praticien") || (u.type == Utilisateur.typeUtilisateur.Praticien))
                                lst.Add(new BaseCommonControls.FamilyValue(Includeorga ? "Praticien" : "", u.LastNameShort, u));

                        }

                }

                if (IncludeAssistantes)
                {

                    foreach (Utilisateur u in UtilisateursMgt.utilisateurs)
                        if (u.Actif)
                        {

                            if ((u.Fonction != "Praticien"))
                            {
                                string orga = (Includeorga ? "Staff" + BaseCommonControls.FamilyValue.famillyseparator : "") + u.Prenom[0].ToString();
                                lst.Add(new BaseCommonControls.FamilyValue(orga, u.NameShort, u));
                            }

                        }

                }

                this.LoadFromFamilyValueList(lst);
            }
            catch (System.Exception) { }
        }
    }
}
