using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    public class Echeance
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

        private ActePG _acte = null;
        public ActePG acte
        {
            get
            {
                return _acte;
            }
            set
            {
                _acte = value;
                IdActe = value.Id;
            }
        }

        private int _IdActe = -1;
        public int IdActe
        {
            get
            {
                return _IdActe;
            }
            set
            {
                _IdActe = value;
            }
        }

        private int _IdPatient;
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

        private string _Libelle;
        public string Libelle
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

        private DateTime? _DateEcheance;
        public DateTime? DateEcheance
        {
            get
            {
                return _DateEcheance;
            }
            set
            {
                _DateEcheance = value;
            }
        }

        private Patient _patient;
        public Patient patient
        {
            get
            {
                return _patient;
            }
            set
            {
                IdPatient = value.Id;
                _patient = value;
            }
        }
    }
}
