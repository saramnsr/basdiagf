using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BASEDiag_BO
{
    public class PlanTraitementObject
    {

        private string _ResourceName;
        public string ResourceName
        {
            get
            {
                return _ResourceName;
            }
            set
            {
                _ResourceName = value;
            }
        }

        private string _CtrlKey;
        public string CtrlKey
        {
            get
            {
                return _CtrlKey;
            }
            set
            {
                _CtrlKey = value;
            }
        }

        private PointF _Point2;
        public PointF Point2
        {
            get
            {
                return _Point2;
            }
            set
            {
                _Point2 = value;
            }
        }

        private PointF _Point1;
        public PointF Point1
        {
            get
            {
                return _Point1;
            }
            set
            {
                _Point1 = value;
            }
        }

        private ResumeClinique _Resumclinique;
        public ResumeClinique Resumclinique
        {
            get
            {
                return _Resumclinique;
            }
            set
            {
                _Resumclinique = value;
            }
        }

        private int _IdResumclinique;
        public int IdResumclinique
        {
            get
            {
                if (Resumclinique != null) _IdResumclinique = Resumclinique.Id;
                return _IdResumclinique;
            }
            set
            {
                _IdResumclinique = value;
            }
        }
    }
}
