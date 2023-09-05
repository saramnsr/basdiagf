using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class ContactLib
    {
        public override string ToString()
        {
            return Libelle;
        }

        public enum AffecteA
        {
            Tous = -1,
            Patient = 1,
            Correspondant = 2,
            Parent = 3

        }

        private AffecteA _AffectedTo = AffecteA.Tous;
        [PropertyCanBeSerialized]
        public AffecteA AffectedTo
        {
            get
            {
                return _AffectedTo;
            }
            set
            {
                _AffectedTo = value;
            }
        }

        private Contact.ContactType _typeCtact;
        [PropertyCanBeSerialized]
        public Contact.ContactType typeCtact
        {
            get
            {
                return _typeCtact;
            }
            set
            {
                _typeCtact = value;
            }
        }

        private String _Libelle;
        [PropertyCanBeSerialized]
        public String Libelle
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

        private int? _Id = null;
        public int? Id
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

        private int _Priorite;
        [PropertyCanBeSerialized]
        public int Priorite
        {
            get
            {
                return _Priorite;
            }
            set
            {
                _Priorite = value;
            }
        }
    }
}
