using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Encaissement
    {
        public override string ToString()
        {
            if (paiementreel == null)
                return MontantEncaisse.ToString("C2");
            else
                return MontantEncaisse.ToString("C2") + " le " + paiementreel.DateEncaissement.ToShortDateString() + " par " + paiementreel.typeencaissement.ToString();
        }



        

        private PaiementReel _paiementreel = null;
        [PropertyCanBeSerialized]
        public PaiementReel paiementreel
        {
            get
            {
                return _paiementreel;
            }
            set
            {
                _paiementreel = value;
            }
        }

        private int _IdPaiementReel;
        [PropertyCanBeSerialized]
        public int IdPaiementReel
        {
            get
            {
                if (paiementreel != null) _IdPaiementReel = paiementreel.Id;
                return _IdPaiementReel;
            }
            set
            {
                _IdPaiementReel = value;
            }
        }

        private double _MontantEncaisse;
        [PropertyCanBeSerialized]
        public double MontantEncaisse
        {
            get
            {
                return _MontantEncaisse;
            }
            set
            {
                _MontantEncaisse = value;
            }
        }

        private int _IdPatient;
        [PropertyCanBeSerialized]
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

        private basePatient _patient;
        [PropertyCanBeSerialized]
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

        private int _Id = -1;
        [PropertyCanBeSerialized]
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

    }
}
