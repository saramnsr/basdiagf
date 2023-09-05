using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace FrmContainer_BO
{
    public class PatientARelancer
    {

        private Relance.ModeRelance _CurrentStatus;
        public Relance.ModeRelance CurrentStatus
        {
            get
            {
                return _CurrentStatus;
            }
            set
            {
                _CurrentStatus = value;
            }
        }

        private DateTime _DueDepuis;
        public DateTime DueDepuis
        {
            get
            {
                return _DueDepuis;
            }
            set
            {
                _DueDepuis = value;
            }
        }

        private Double _SommesDue;
        public Double SommesDue
        {
            get
            {
                return _SommesDue;
            }
            set
            {
                _SommesDue = value;
            }
        }

        private int _IdResponsableFi;
        public int IdResponsableFi
        {
            get
            {
                return _IdResponsableFi;
            }
            set
            {
                _IdResponsableFi = value;
            }
        }

        private string _ResponsableFi;
        public string ResponsableFi
        {
            get
            {
                return _ResponsableFi;
            }
            set
            {
                _ResponsableFi = value;
            }
        }

        private string _patient = null;
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

        private int _IdPatient = -1;
        public int IdPatient
        {
            get
            {
                return _IdPatient;
            }
            set
            {
                _IdPatient = value;
            }
        }
    }
}
