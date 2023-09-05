using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class CommentHistoComparer : IComparer<CommentHisto>
    {


        #region IComparer Members

        public int Compare(CommentHisto x, CommentHisto y)
        {
            return (int)(((CommentHisto)x).DateCommentaire.CompareTo(((CommentHisto)y).DateCommentaire));
        }

        #endregion
    }

    public class CommentHisto : ICloneable
    {

        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
        public enum CommentHistoImportance
        {
            undefined = -1,
            Comment = 0,
            Warning = 1,
            Urgent = 2
        }

        public enum CommentHistoType
        {
            undefined = 0,
            Objectif = 1,
            Traitement = 2,
            Diagnostique = 3,
            Appareils = 4,
            MotifConsultation = 5,
            Clinique = 6,
            Administratif = 7,
            Financier = 8,
            Orthodontie=9,
            Gencives=10,
        }
        public enum CommentHistosender
        {
            inside = 0,
            tocabinet = 1,
            topatient = 2,
            todentiste = 3,
        }
        /*CREATE TABLE base_commENTS (
    ID INTEGER NOT NULL,
    ID_PATIENT INTEGER,
    TYPE_COMMENT SMALLINT,
    DATE_COMMENT TIMESTAMP,
    ID_WRITER INTEGER);*/

        /*
        ALTER TABLE base_commENTS
ADD CODECOMMENTAIRE CHAR(10);

ALTER TABLE base_commENTS
ADD IMPORTANCE SMALLINT;

ALTER TABLE base_commENTS
ADD PARENT INTEGER;

ALTER TABLE base_commENTS
ADD DATEFIN TIMESTAMP;
        */
        private DateTime? _DateDeDebut;
        [PropertyCanBeSerialized]
        public DateTime? DateDeDebut
        {
            get
            {
                return _DateDeDebut;
            }
            set
            {
                _DateDeDebut = value;
            }
        }
        private bool _isread;
        public bool isread
        {
            get
            {
                return _isread;
            }
            set
            {
                _isread = value;
            }
        }
        private CommentHistosender _sender;
        public CommentHistosender sender
        {
            get
            {
                return _sender;
            }
            set
            {
                _sender = value;
            }
        }
        private DateTime? _DateDeFin;
        [PropertyCanBeSerialized]
        public DateTime? DateDeFin
        {
            get
            {
                return _DateDeFin;
            }
            set
            {
                _DateDeFin = value;
            }
        }

        private int _IdParent = -1;
        [PropertyCanBeSerialized]
        public int IdParent
        {
            get
            {
                return _IdParent;
            }
            set
            {
                _IdParent = value;
            }
        }

        private CommentHistoImportance _Importance;
        [PropertyCanBeSerialized]
        public CommentHistoImportance Importance
        {
            get
            {
                return _Importance;
            }
            set
            {
                _Importance = value;
            }
        }

        private string _Code;
        [PropertyCanBeSerialized]
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }


        public override string ToString()
        {
            if (Ecrivain != null)
                return DateCommentaire.ToString() + " par " + Ecrivain.ToString();
            else
                return DateCommentaire.ToString();
        }

        private Utilisateur _Ecrivain;
        [PropertyCanBeSerialized]
        public Utilisateur Ecrivain
        {
            get
            {

                return _Ecrivain;
            }
            set
            {
                _Ecrivain = value;
            }
        }

        private int _Id_Ecrivain;
        [PropertyCanBeSerialized]
        public int Id_Ecrivain
        {
            get
            {
                if (Ecrivain != null) _Id_Ecrivain = Ecrivain.Id;
                return _Id_Ecrivain;
            }
            set
            {
                _Id_Ecrivain = value;
            }
        }

        private DateTime _DateCommentaire = DateTime.Now;
        [PropertyCanBeSerialized]
        public DateTime DateCommentaire
        {
            get
            {
                return _DateCommentaire;
            }
            set
            {
                _DateCommentaire = value;
            }
        }


        private string _comment;
        [PropertyCanBeSerialized]
        public string comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }

        private CommentHistoType _typecomment;
        [PropertyCanBeSerialized]
        public CommentHistoType typecomment
        {
            get
            {
                return _typecomment;
            }
            set
            {
                _typecomment = value;
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
        private string _strpatient = null;
        [PropertyCanBeSerialized]
        public string strpatient
        {
            get
            {
                return _strpatient;
            }
            set
            {
                _strpatient = value;
            }
        }
        private string _strsender = null;
        [PropertyCanBeSerialized]
        public string strsender
        {
            get
            {
                return _strsender;
            }
            set
            {
                _strsender = value;
            }
        }
        private int _IdPatient = -1;
        [PropertyCanBeSerialized]
        public int IdPatient
        {
            get
            {
                if (_patient != null)
                    _IdPatient = patient.Id;
                return _IdPatient;
            }
            set
            {
                _IdPatient = value;
            }
        }

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


    }
}
