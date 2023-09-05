using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace FrmContainer_BO
{
    public class PatientEnRecontact
    {


        private Utilisateur _UserTentative = null;
        public Utilisateur UserTentative
        {
            get
            {
                return _UserTentative;
            }
            set
            {
                _UserTentative = value;
            }
        }


        private bool _IsPatientOrthalis;
        public bool IsPatientOrthalis
        {
            get
            {
                return _IsPatientOrthalis;
            }
            set
            {
                _IsPatientOrthalis = value;
            }
        }


        private int _IdUserTentative;
        public int IdUserTentative
        {
            get
            {
                if (UserTentative != null) _IdUserTentative = UserTentative.Id;
                return _IdUserTentative;
            }
            set
            {
                _IdUserTentative = value;
            }
        }

        private int _NumTentative;
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

        private DateTime _DerniereTentative;
        public DateTime DerniereTentative
        {
            get
            {
                return _DerniereTentative;
            }
            set
            {
                _DerniereTentative = value;
            }
        }

        private DateTime _ProchaineTentative;
        public DateTime ProchaineTentative
        {
            get
            {
                return _ProchaineTentative;
            }
            set
            {
                _ProchaineTentative = value;
            }
        }

        private DateTime _DepuisLe;
        public DateTime DepuisLe
        {
            get
            {
                return _DepuisLe;
            }
            set
            {
                _DepuisLe = value;
            }
        }

        private string _Motif;
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

        private DateTime? _DateDernierRDV;
        public DateTime? DateDernierRDV
        {
            get
            {
                return _DateDernierRDV;
            }
            set
            {
                _DateDernierRDV = value;
            }
        }

        private string _Prenom;
        public string Prenom
        {
            get
            {
                return _Prenom;
            }
            set
            {
                _Prenom = value;
            }
        }

        private string _Nom;
        public string Nom
        {
            get
            {
                return _Nom;
            }
            set
            {
                _Nom = value;
            }
        }

        private int _Id;
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

    public class PatientARecontacter
    {


        private DateTime? _DateDernierRDV;
        public DateTime? DateDernierRDV
        {
            get
            {
                return _DateDernierRDV;
            }
            set
            {
                _DateDernierRDV = value;
            }
        }

        private string _Prenom;
        public string Prenom
        {
            get
            {
                return _Prenom;
            }
            set
            {
                _Prenom = value;
            }
        }

        private string _Nom;
        public string Nom
        {
            get
            {
                return _Nom;
            }
            set
            {
                _Nom = value;
            }
        }

        private int _Id;
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
