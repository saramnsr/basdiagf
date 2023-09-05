using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Assurance
    {


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
        private int _idPatient;
        [PropertyCanBeSerialized]
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
        private string _libelle;
        [PropertyCanBeSerialized]
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
        private string _libelleclt;
        [PropertyCanBeSerialized]
        public string libelleclt
        {
            get
            {
                return _libelleclt;
            }
            set
            {
                _libelleclt = value;
            }
        }
        private double _montant;
        [PropertyCanBeSerialized]
        public double montant
        {
            get
            {
                return _montant;
            }
            set
            {
                _montant = value;
            }
        }
        private double _Partcentpat;
        [PropertyCanBeSerialized]
        public double Partcentpat
        {
            get
            {
                return _Partcentpat;
            }
            set
            {
                _Partcentpat = value;
            }
        }

        private double _Partmontpat;
        [PropertyCanBeSerialized]
        public double Partmontpat
        {
            get
            {
                return _Partmontpat;
            }
            set
            {
                _Partmontpat = value;
            }
        }

        private double _pourcentage;
        [PropertyCanBeSerialized]
        public double pourcentage
        {
            get
            {
                return _pourcentage;
            }
            set
            {
                _pourcentage = value;
            }
        }
    }
}
