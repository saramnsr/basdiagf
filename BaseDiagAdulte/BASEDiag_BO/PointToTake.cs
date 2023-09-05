using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BASEDiag_BO
{

    [Serializable]
    public class PointToTake
    {


        public PointToTake(string name)
        {
            _PtName = name;
        }


        private bool _visible = false;
        public bool visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }

        private string _PtName = "";
        public string PtName
        {
            get
            {
                return _PtName;
            }
            set
            {
                _PtName = value;
            }
        }

        private Point _Pt;
        public Point Pt
        {
            get
            {
                return _Pt;
            }
            set
            {
                _Pt = value;
                visible = true;
            }
        }

    }
}
