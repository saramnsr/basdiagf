using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
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
