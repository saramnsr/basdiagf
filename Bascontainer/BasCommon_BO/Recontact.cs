using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Recontact
    {

        public enum Moyen
        {
            Telephone=0,
            Fax=1,
            Mail=2,
            Courrier=3
        }

        private Moyen _moyenProchaineTentative = Moyen.Telephone;
        [PropertyCanBeSerialized]
        public Moyen moyenProchaineTentative
        {
            get
            {
                return _moyenProchaineTentative;
            }
            set
            {
                _moyenProchaineTentative = value;
            }
        }

        private Moyen _moyen = Moyen.Telephone;
        [PropertyCanBeSerialized]
        public Moyen moyen
        {
            get
            {
                return _moyen;
            }
            set
            {
                _moyen = value;
            }
        }

       

        private string _Creator;
        [PropertyCanBeSerialized]
        public string Creator
        {
            get
            {
                return _Creator;
            }
            set
            {
                _Creator = value;
            }
        }

        private DateTime? _DateProchaineTentative;
        [PropertyCanBeSerialized]
        public DateTime? DateProchaineTentative
        {
            get
            {
                return _DateProchaineTentative;
            }
            set
            {
                _DateProchaineTentative = value;
            }
        }

        private DateTime? _DateTentative;
        [PropertyCanBeSerialized]
        public DateTime? DateTentative
        {
            get
            {
                return _DateTentative;
            }
            set
            {
                _DateTentative = value;
            }
        }

        private Utilisateur _usertentative = null;
        [PropertyCanBeSerialized]
        public Utilisateur usertentative
        {
            get
            {
                return _usertentative;
            }
            set
            {
                _usertentative = value;
            }
        }

        private int _IdUserTentative;
        [PropertyCanBeSerialized]
        public int IdUserTentative
        {
            get
            {
                if (usertentative != null) _IdUserTentative = usertentative.Id;
                return _IdUserTentative;
            }
            set
            {
                _IdUserTentative = value;
            }
        }

        private int _NumTentative;
        [PropertyCanBeSerialized]
        public int NumTentative
        {
            get
            {
                return _NumTentative;
            }
            set
            {
                _NumTentative = value;
            }
        }

        private DateTime _ARecontacterDepuisLe;
        [PropertyCanBeSerialized]
        public DateTime ARecontacterDepuisLe
        {
            get
            {
                return _ARecontacterDepuisLe;
            }
            set
            {
                _ARecontacterDepuisLe = value;
            }
        }

        private string _Motif;
        [PropertyCanBeSerialized]
        public string Motif
        {
            get
            {
                return _Motif;
            }
            set
            {
                _Motif = value;
            }
        }

        private basePatient _patient = null;
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

        private int _IdPatient = -1;
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
    }
}
