using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;
using BASEDiag_BL;

namespace BASEDiagAdulte
{
    public class FormScreen : Form
    {

        protected Screen[] screenlst = Screen.AllScreens;

        private static int? _CurrentScreenIdx = null;
        public int CurrentScreenIdx
        {
            get
            {
               if (_CurrentScreenIdx==null)
                   _CurrentScreenIdx = RegistryParameters.GetScreenNumberOf(typeof(FormScreen));

               if (_CurrentScreenIdx >= Screen.AllScreens.Length) _CurrentScreenIdx = 0;
                return _CurrentScreenIdx.Value;
            }
            set
            {
                _CurrentScreenIdx = value;
                if (_CurrentScreenIdx!=null)
                    RegistryParameters.SetScreenNumberOf(typeof(FormScreen), _CurrentScreenIdx.Value);
            }
        }

    }
}
