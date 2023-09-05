using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrmContainer_BO
{
    public class PatientSoldeNegatifCeJour
    {

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

        private DateTime _DateDeDerniereEcheance;
        public DateTime DateDeDerniereEcheance
        {
            get
            {
                return _DateDeDerniereEcheance;
            }
            set
            {
                _DateDeDerniereEcheance = value;
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
