using System;
using System.Collections.Generic;
using System.Text;

namespace BasCommon_BO
{
    public class Status
    {

        private string _code;
        public string code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }


        public override string ToString()
        {
            return this.Libelle;
        }

        public int Id;
        public string Libelle;
        public bool IsAnAbsence;
    }
}
