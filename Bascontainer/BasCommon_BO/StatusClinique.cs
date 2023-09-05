using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class StatusClinique
    {

        public override string ToString()
        {
            return Libelle;
        }

        private string _Libelle;
        public string Libelle
        {
            get
            {
                return _Libelle;
            }
            set
            {
                _Libelle = value;
            }
        }

        private int _IdStatus;
        public int IdStatus
        {
            get
            {
                return _IdStatus;
            }
            set
            {
                _IdStatus = value;
            }
        }
    }
}
