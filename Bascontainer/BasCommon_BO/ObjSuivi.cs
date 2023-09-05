using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class ObjSuivi
    {
        public override string ToString()
        {
            return Details + (this.WorkerName != "" ? " par " + this.WorkerName : "") + (this.PoseApp == null ? "" : (" -> " + this.PoseApp.Value.ToShortDateString()));
        }


        private bool _IsToSend;
        [PropertyCanBeSerialized]
        public bool IsToSend
        {
            get
            {
                return _IsToSend;
            }
            set
            {
                _IsToSend = value;
            }
        }

        private int _DemandeID;
        public int DemandeID
        {
            get
            {
                return _DemandeID;
            }
            set
            {
                _DemandeID = value;
            }
        }

        public float tarifTTC
        {
            get
            {
                return tarif * 1.196f;
            }
        }

        private float _tarif;
        [PropertyCanBeSerialized]
        public float tarif
        {
            get
            {
                return _tarif;
            }
            set
            {
                _tarif = value;
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

        private DateTime? _PoseApp;
        [PropertyCanBeSerialized]
        public DateTime? PoseApp
        {
            get
            {
                if (_PoseApp == DateTime.MinValue) return null; else return _PoseApp;
            }
            set
            {
                _PoseApp = value;
            }
        }

        private DateTime? _LastCommentDate;
        [PropertyCanBeSerialized]
        public DateTime? LastCommentDate
        {
            get
            {
                return _LastCommentDate;
            }
            set
            {
                _LastCommentDate = value;
            }
        }

        private DateTime? _ReceptionCab;
        [PropertyCanBeSerialized]
        public DateTime? ReceptionCab
        {
            get
            {
                if (_ReceptionCab == DateTime.MinValue) return null; else return _ReceptionCab;
            }
            set
            {
                _ReceptionCab = value;
            }
        }

        private DateTime? _SortieLabo;
        [PropertyCanBeSerialized]
        public DateTime? SortieLabo
        {
            get
            {
                if (_SortieLabo == DateTime.MinValue) return null; else return _SortieLabo;
            }
            set
            {
                _SortieLabo = value;
            }
        }

        private string _WorkerName;
        [PropertyCanBeSerialized]
        public string WorkerName
        {
            get
            {
                return _WorkerName;
            }
            set
            {
                _WorkerName = value;
            }
        }


        private String _RecupereParName;
        [PropertyCanBeSerialized]
        public String RecupereParName
        {
            get
            {
                return _RecupereParName;
            }
            set
            {
                _RecupereParName = value;
            }
        }


        private String _ValidatorName;
        [PropertyCanBeSerialized]
        public String ValidatorName
        {
            get
            {
                return _ValidatorName;
            }
            set
            {
                _ValidatorName = value;
            }
        }

        private int _ValidatorId;
        public int ValidatorId
        {
            get
            {
                return _ValidatorId;
            }
            set
            {
                _ValidatorId = value;
            }
        }


        private string _RequestorName;
        [PropertyCanBeSerialized]
        public string RequestorName
        {
            get
            {
                return _RequestorName;
            }
            set
            {
                _RequestorName = value;
            }
        }

        private int _RequestorId;
        public int RequestorId
        {
            get
            {
                return _RequestorId;
            }
            set
            {
                _RequestorId = value;
            }
        }

        private int _PatientId;
        public int PatientId
        {
            get
            {
                return _PatientId;
            }
            set
            {
                _PatientId = value;
            }
        }

        private string _PatientName;
        [PropertyCanBeSerialized]
        public string PatientName
        {
            get
            {
                return _PatientName;
            }
            set
            {
                _PatientName = value;
            }
        }

        private DateTime? _Empreinte;
        [PropertyCanBeSerialized]
        public DateTime? Empreinte
        {
            get
            {
                return _Empreinte;
            }
            set
            {
                _Empreinte = value;
            }
        }


        private DateTime? _EntreeLabo;
        [PropertyCanBeSerialized]
        public DateTime? EntreeLabo
        {
            get
            {
                if (_EntreeLabo == DateTime.MinValue)
                    return null;
                else
                    return _EntreeLabo;
            }
            set
            {
                _EntreeLabo = value;
            }
        }

        private DateTime? _SortieCabinet;
        [PropertyCanBeSerialized]
        public DateTime? SortieCabinet
        {
            get
            {
                if (_SortieCabinet == DateTime.MinValue)
                    return null;
                else
                    return _SortieCabinet;
            }
            set
            {
                _SortieCabinet = value;
            }
        }


        private string _EntreeLaboAvec;
        [PropertyCanBeSerialized]
        public string EntreeLaboAvec
        {
            get
            {
                return _EntreeLaboAvec;
            }
            set
            {
                _EntreeLaboAvec = value;
            }
        }

        private string _SortieLaboAvec;
        [PropertyCanBeSerialized]
        public string SortieLaboAvec
        {
            get
            {
                return _SortieLaboAvec;
            }
            set
            {
                _SortieLaboAvec = value;
            }
        }

        private string _SortieCabinetAvec;
        [PropertyCanBeSerialized]
        public string SortieCabinetAvec
        {
            get
            {
                return _SortieCabinetAvec;
            }
            set
            {
                _SortieCabinetAvec = value;
            }
        }

        private string _EntreeCabinetAvec;
        [PropertyCanBeSerialized]
        public string EntreeCabinetAvec
        {
            get
            {
                return _EntreeCabinetAvec;
            }
            set
            {
                _EntreeCabinetAvec = value;
            }
        }

        private string _Details;
        [PropertyCanBeSerialized]
        public string Details
        {
            get
            {
                return _Details;
            }
            set
            {
                _Details = value;
            }
        }

        private string _NatureTravaux;
        [PropertyCanBeSerialized]
        public string NatureTravaux
        {
            get
            {
                return _NatureTravaux;
            }
            set
            {
                _NatureTravaux = value;
            }
        }

        private string _CodeBarre;
        [PropertyCanBeSerialized]
        public string CodeBarre
        {
            get
            {
                return _CodeBarre;
            }
            set
            {
                _CodeBarre = value;
            }
        }

        private DateTime _PaymentEffectueLe;
        [PropertyCanBeSerialized]
        public DateTime PaymentEffectueLe
        {
            get
            {
                return _PaymentEffectueLe;

            }
            set
            {
                _PaymentEffectueLe = value;
            }
        }

        public string PayéLe
        {
            get
            {
                if (_PaymentEffectueLe == DateTime.MinValue) return "Non effectué"; else return _PaymentEffectueLe.ToString("dd/MM/yyyy");

            }

        }

    }
}
