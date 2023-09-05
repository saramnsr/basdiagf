using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Debiteurs
    {

        public enum RangeCalendar
        {
            De0a6Mois = 1,
            De6Moisa1an = 2,
            De1ana3ans = 3,
            PlusDe3ans = 4
        }

        private RangeCalendar _rangecalendar;
        public RangeCalendar rangecalendar
        {
            get
            {
                return _rangecalendar;
            }
            set
            {
                _rangecalendar = value;
            }
        }

        private Echeance.typepayeur _typepayeur;
        public Echeance.typepayeur typepayeur
        {
            get
            {
                return _typepayeur;
            }
            set
            {
                _typepayeur = value;
            }
        }
        
            private string _libelle;
        public string libelle
        {
            get
            {
                return _libelle;
            }
            set
            {
                _libelle = value;
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
        private int _idPersonne;
        public int idPersonne
        {
            get
            {
                return _idPersonne;
            }
            set
            {
                _idPersonne = value;
            }
        }
        private double _Montant;
        public double Montant
        {
            get
            {
                return _Montant;
            }
            set
            {
                _Montant = value;
            }
        }
        private DateTime _dateEcheance;
        public DateTime dateEcheance
        {
            get
            {
                return _dateEcheance;
            }
            set
            {
                _dateEcheance = value;
            }
        }
    }
}
