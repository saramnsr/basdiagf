using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BasCommon_BO.ElementsEnBouche.BO
{
    public class ElementAppareil:IElementAppareil
    {

        private bool _Bas;
        public bool Bas
        {
            get
            {
                return _Bas;
            }
            set
            {
                _Bas = value;
            }
        }

        private bool _Haut;
        public bool Haut
        {
            get
            {
                return _Haut;
            }
            set
            {
                _Haut = value;
            }
        }

        public override string ToString()
        {
            return Appareil.ToString();
        }


        private Appareil _Appareil;
        public Appareil Appareil
        {
            get
            {
                return _Appareil;
            }
            set
            {
                _Appareil = value;
            }
        }

        private int _Id = -1;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }


        public string ShortLib
        {
            get
            {

                return this.Appareil.Code;
            }

        }

        private int _IdCommFin = -1;
        public int IdCommFin
        {
            get
            {
                return _IdCommFin;
            }
            set
            {
                _IdCommFin = value;
            }
        }

        private int _IdCommDebut = -1;
        public int IdCommDebut
        {
            get
            {
                return _IdCommDebut;
            }
            set
            {
                _IdCommDebut = value;
            }
        }

        private basePatient _patient;
        public basePatient patient
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

        private int _IdPatient;
        public int IdPatient
        {
            get
            {
                if (patient != null) _IdPatient = patient.Id;
                return _IdPatient;
            }
            set
            {
                _IdPatient = value;
            }
        }

        private DateTime? _Datesuppression;
        public DateTime? Datesuppression
        {
            get
            {
                return _Datesuppression;
            }
            set
            {
                _Datesuppression = value;
            }
        }

        private DateTime? _DateInstallation = null;
        public DateTime? DateInstallation
        {
            get
            {
                return _DateInstallation;
            }
            set
            {
                _DateInstallation = value;
            }
        }
    }
}
