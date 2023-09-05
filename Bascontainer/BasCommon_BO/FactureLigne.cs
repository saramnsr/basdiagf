using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class FactureLigne
    {
        private int _id_Facture;
        [PropertyCanBeSerialized]
        public int id_Facture
        {
            get
            {
                return _id_Facture;
            }
            set
            {
                _id_Facture = value;
            }
        }
        private int _id_Ligne;
        [PropertyCanBeSerialized]
        public int id_Ligne
        {
            get
            {
                return _id_Ligne;
            }
            set
            {
                _id_Ligne = value;
            }
        }
        private int _id_Echeance;
        [PropertyCanBeSerialized]
        public int id_Echeance
        {
            get
            {
                return _id_Echeance;
            }
            set
            {
                _id_Echeance = value;
            }
        }
        private DateTime _date_Execution;
        [PropertyCanBeSerialized]
        public DateTime date_Execution
        {
            get
            {
                return _date_Execution;
            }
            set
            {
                _date_Execution = value;
            }
        }
        private double? _montantLigneFacture;
        public double? montantLigneFacture
        {
            get
            {
                return _montantLigneFacture;
            }
            set
            {
                _montantLigneFacture = value;
            }
        }
        private double _partPatient = 0;
        public double partPatient
        {
            get
            {
                return _partPatient;
            }
            set
            {
                _partPatient = value;
            }
        }
        private double _montantSS = 0;
        public double montantSS
        {
            get
            {
                return _montantSS;
            }
            set
            {
                _montantSS = value;
            }
        }
        private double _montantMutuelle  = 0;
        public double montantMutuelle
        {
            get
            {
                return _montantMutuelle;
            }
            set
            {
                _montantMutuelle = value;
            }
        }

        private string _NombrePtsActe;
        public string NombrePtsActe
        {
            get
            {
                return _NombrePtsActe;
            }
            set
            {
                _NombrePtsActe = value;

            }
        }

        private string _NomenclatureActe;
        public string NomenclatureActe
        {
            get
            {
                return _NomenclatureActe;
            }
            set
            {
                _NomenclatureActe = value;

            }
        }

        private double _rabais;
        public double rabais
        {
            get
            {
                return _rabais;
            }
            set
            {
                _rabais = value;

            }
        }
        private string _LibelleActe;
        public string LibelleActe
        {
            get
            {
                return _LibelleActe;
            }
            set
            {
                _LibelleActe = value;

            }
        }

        private double _QuantiteActe;
        public double QuantiteActe
        {
            get
            {
                return _QuantiteActe;
            }
            set
            {
                _QuantiteActe = value;

            }
        }
        
        private string _CotationActe;
        public string CotationActe
        {
            get
            {
                return _CotationActe;
            }
            set
            {
                _CotationActe = value;
                
            }
        }

        private int _idComm;
        [PropertyCanBeSerialized]
        public int idComm
        {
            get
            {
                return _idComm;
            }
            set
            {
                _idComm = value;
            }
        }

        private int _idTraitement;
        [PropertyCanBeSerialized]
        public int idTraitement
        {
            get
            {
                return _idTraitement;
            }
            set
            {
                _idTraitement = value;
            }
        }
        
    

    }
}
