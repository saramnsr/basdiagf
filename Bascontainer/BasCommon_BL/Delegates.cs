using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BasCommon_BL
{
    public delegate void ChangeScreenHandler(Form frm, int screenNum);
    public delegate void NextScreenHandler(Form frm);
    public delegate void RHBaseGotoDate(DateTime dte);
    public delegate void NouvelleDemandeInStandBy(int IdPatient);
    public delegate void DelegateRechercher(string recherche);
    
    public static class CommonCalls
    {


        private static Form _FrmContainer = null;
        public static Form FrmContainer
        {
            get
            {
                return _FrmContainer;
            }
            set
            {
                _FrmContainer = value;
            }
        }

        public static ChangeScreenHandler CallChangeScreen = null;
        public static NextScreenHandler NextScreenHandler = null;
        public static RHBaseGotoDate RHBasGotoDate = null;
        public static NouvelleDemandeInStandBy NouvelleDemandeInStandBy = null;
        public static DelegateRechercher RechercherPat = null;
    }

   

}
