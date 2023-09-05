using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class CustomStatusClinique
    {


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

        public override string ToString()
        {
            if (DateFin != null)
                return status.Libelle + " " + "(jusqu'au : " + DateFin + ")";
            else
                return status.Libelle + " " + "(depuis le : " + DateDebut + ")";
        }


        private StatusCliniqueManuel _status;
        [PropertyCanBeSerialized]
        public StatusCliniqueManuel status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        private int _IdPersonne = -1;
        [PropertyCanBeSerialized]
        public int IdPersonne
        {
            get
            {
                return _IdPersonne;
            }
            set
            {
                _IdPersonne = value;
            }
        }

        private DateTime _DateDebut;
        [PropertyCanBeSerialized]
        public DateTime DateDebut
        {
            get
            {
                return _DateDebut;
            }
            set
            {
                _DateDebut = value;
            }
        }



        private DateTime? _DateFin;
        [PropertyCanBeSerialized]
        public DateTime? DateFin
        {
            get
            {
                return _DateFin;
            }
            set
            {
                _DateFin = value;
            }
        }
    }
}
