using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
{
    public class CommentHisto
    {


        public enum CommentHistoType
        {
            undefined = 0,
            Objectif = 1,
            Traitement = 2,
            Diagnostique = 3,
            Appareils = 4,
            MotifConsultation = 5
        }

        /*CREATE TABLE BASE_COMMENTS (
    ID INTEGER NOT NULL,
    ID_PATIENT INTEGER,
    TYPE_COMMENT SMALLINT,
    DATE_COMMENT TIMESTAMP,
    ID_WRITER INTEGER);*/

        public override string ToString()
        {
            if (Ecrivain != null)
                return DateCommentaire.ToString() + " par " + Ecrivain.ToString();
            else
                return DateCommentaire.ToString();
        }

        private Utilisateur _Ecrivain;
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

        private DateTime _DateCommentaire;
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
