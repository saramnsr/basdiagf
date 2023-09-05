using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;
using BASEDiag_BL;

namespace BASEDiag
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
                return _CurrentScreenIdx.Value;
            }
            set
            {
                _CurrentScreenIdx = value;
                if (_CurrentScreenIdx!=null)
                    RegistryParameters.SetScreenNumberOf(typeof(FormScreen), _CurrentScreenIdx.Value);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormScreen
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "FormScreen";
            this.Load += new System.EventHandler(this.FormScreen_Load);
            this.ResumeLayout(false);

        }

        private void FormScreen_Load(object sender, EventArgs e)
        {

        }

    }
}
