using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class ProcessBAS
    {


        private int _ScreenIndex = 0;
        public int ScreenIndex
        {
            get
            {
                return _ScreenIndex;
            }
            set
            {
                _ScreenIndex = value;
            }
        }

        private string _classname;
        public string classname
        {
            get
            {
                return _classname;
            }
            set
            {
                _classname = value;
            }
        }

        private string _ProcessCode;
        public string ProcessCode
        {
            get
            {
                return _ProcessCode;
            }
            set
            {
                _ProcessCode = value;
            }
        }


        private Form _FrmReferenced = null;
        public Form FrmReferenced
        {
            get
            {
                return _FrmReferenced;
            }
            set
            {
                _FrmReferenced = value;
            }
        }


       
        private string _exefile;
        public string exefile
        {
            get
            {
                return _exefile;
            }
            set
            {
                _exefile = value;
            }
        }


        private string _MainForm;
        public string MainForm
        {
            get
            {
                return _MainForm;
            }
            set
            {
                _MainForm = value;
            }
        }

    }
}
