using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
{
    public class CommonObjectifFromDiag : IComparable<CommonObjectifFromDiag>
    {

        public override string ToString()
        {
            return Descritpion;
        }

        private string _Descritpion;
        public string Descritpion
        {
            get
            {
                return _Descritpion;
            }
            set
            {
                _Descritpion = value;
            }
        }
        private int _NumOption;
        public int NumOption
        {
            get
            {
                return _NumOption;
            }
            set
            {
                _NumOption = value;
            }
        }
        private bool _DiagCanceled;
        public bool DiagCanceled
        {
            get
            {
                return _DiagCanceled;
            }
            set
            {
                _DiagCanceled = value;
            }
        }
        private string _NumDiag;
        public string NumDiag
        {
            get
            {
                return _NumDiag;
            }
            set
            {
                _NumDiag = value;
            }
        }

        private string _SpecialInstruction;
        public string SpecialInstruction
        {
            get
            {
                return _SpecialInstruction;
            }
            set
            {
                _SpecialInstruction = value;
            }
        }
        

        private int _DisplayOrder;
        public int DisplayOrder
        {
            get
            {
                return _DisplayOrder;
            }
            set
            {
                _DisplayOrder = value;
            }
        }
        

        private CommonObjectif _objectif;
        public CommonObjectif objectif
        {
            get
            {
                return _objectif;
            }
            set
            {
                _objectif = value;
            }
        }

        private CommonDiagnostic _diagnostic;
        public CommonDiagnostic diagnostic
        {
            get
            {
                return _diagnostic;
            }
            set
            {
                _diagnostic = value;
            }
        }

        public int CompareTo(CommonObjectifFromDiag other)
        {
            return DisplayOrder.CompareTo(other.DisplayOrder);
        }


        private int _NumDevis;
        public int NumDevis
        {
            get
            {
                return _NumDevis;
            }
            set
            {
                _NumDevis = value;
            }
        }
    }
}
