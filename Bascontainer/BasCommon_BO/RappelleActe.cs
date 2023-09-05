using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
   public  class RappelleActe
    {
        private int _id_acte;
        public int idActe
        {
            get
            {
                return _id_acte;
            }
            set
            {
                _id_acte = value;
            }
        }


        private int _idRdv;
        public int idRdv
        {
            get
            {
                return _idRdv;
            }
            set
            {
                _idRdv = value;
            }
        }
        private int _idPatient;
        public int idPatient
        {
            get
            {
                return _idPatient;
            }
            set
            {
                _idPatient = value;
            }
        }
        private string _patient;
        public string patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
            }
        }
        private Acte _acte;
        public Acte acte
        {
            get
            {
                return _acte;
            }
            set
            {
                _acte = value;
            }
        }
        private DateTime _datePrevisionnel;
        public DateTime datePrevisionnel
        {
            get
            {
                return _datePrevisionnel;
            }
            set
            {
                _datePrevisionnel = value;
            }
        }
    }
}
