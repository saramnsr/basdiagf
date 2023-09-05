using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace BasCommon_BO
{

    [Serializable]
    public class RHAppointment : Appointment
    {



        public RHAppointment()
        {

        }       
        public RHAppointment Clone()
        {
            return (RHAppointment)MemberwiseClone();
        }
        public enum AnnulerPar
        {
            Patient = -1,
            Praticien = 2,
            Personne = -2
        }

        public enum EnumStatus
        {
            Attendu = 0,
            Arrive = 1,
            Absent = 4
        }

        public enum EnumLocalisation
        {
            Aucune = 0,
            EnSalle = 1,
            Fauteuil = 2,
            Secretariat = 4,
            Sortie = 3
        }


        public override string ToString()
        {
            return this.acte.ToString() + " : " + StartDate.ToString() + " " + this.Resource.Name;
        }


        private int _idNextact;
        [PropertyCanBeSerialized]
        public int idNextact
        {
            get
            {
                return _idNextact;
            }
            set
            {
                _idNextact = value;
            }
        }

        

        
        private int _IdCommClinique;
        public int IdCommClinique
        {
            get
            {
                if (CommCl != null) _IdCommClinique = CommCl.Id;
                return _IdCommClinique;
            }
            set
            {
                _IdCommClinique = value;
            }
        }
        

        private bool _AAvancer;
        [PropertyCanBeSerialized]
        public bool AAvancer
        {
            get
            {
                return _AAvancer;
            }
            set
            {
                _AAvancer = value;
            }
        }

        private DateTime? _NextRDV = null;
        [PropertyCanBeSerialized]
        public DateTime? NextRDV
        {
            get
            {
                return _NextRDV;
            }
            set
            {
                _NextRDV = value;
            }
        }


        private DateTime? _DateSortie;
        [PropertyCanBeSerialized]
        public DateTime? DateSortie
        {
            get
            {
                return _DateSortie;
            }
            set
            {
                _DateSortie = value;
            }
        }

        public int _fromweb=1;
        [PropertyCanBeSerialized]
        public int fromweb
        {
            get
            {
                return _fromweb;
            }
            set
            {
                _fromweb = value;
            }
        }

        private Utilisateur _user;
        [PropertyCanBeSerialized]
        public Utilisateur user
        {
            get
            {

                return _user;
            }
            set
            {
                _user = value;
            }
        }




        public int _perIdPersonne ;
        [PropertyCanBeSerialized]
        public int perIdPersonne
        {
            get
            {
                if (user != null) _perIdPersonne = user.Id;
                return _perIdPersonne;
            }
            set
            {
                _perIdPersonne = value;
            }
        }

        private string _userName;
        [PropertyCanBeSerialized]
        public string userName
        {
            get
            {
                if (user != null) _userName = user.Nom + " " + user.Prenom;
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }


        private CommClinique _NextCommClinique = null;
        [PropertyCanBeSerialized]
        public CommClinique NextCommClinique
        {
            get
            {
                return _NextCommClinique;
            }
            set
            {
                _NextCommClinique = value;
            }
        }

        private DateTime? _DateSecretariat;
        [PropertyCanBeSerialized]
        public DateTime? DateSecretariat
        {
            get
            {
                return _DateSecretariat;
            }
            set
            {
                _DateSecretariat = value;
            }
        }

        private Fauteuil _FauteuilReel;
        [PropertyCanBeSerialized]
        public Fauteuil FauteuilReel
        {
            get
            {
                return _FauteuilReel;
            }
            set
            {
                _FauteuilReel = value;
            }
        }

        private DateTime? _DateFauteuil;
        [PropertyCanBeSerialized]
        public DateTime? DateFauteuil
        {
            get
            {
                return _DateFauteuil;
            }
            set
            {
                _DateFauteuil = value;
            }
        }

        private DateTime? _DateArrive;
        [PropertyCanBeSerialized]
        public DateTime? DateArrive
        {
            get
            {
                return _DateArrive;
            }
            set
            {
                _DateArrive = value;
            }
        }

        private EnumLocalisation _Localisation;
        [PropertyCanBeSerialized]
        public EnumLocalisation Localisation
        {
            get
            {
                return _Localisation;
            }
            set
            {
                _Localisation = value;
            }
        }


        private EnumStatus _Status;
        [PropertyCanBeSerialized]
        public EnumStatus Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }

        private int m_Id;
        [PropertyCanBeSerialized]
        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        private string _PatientName;
        [PropertyCanBeSerialized]
        public string PatientName
        {
            get
            {
                if (patient != null) _PatientName = patient.Nom + " " + _patient.Prenom;
                return _PatientName;
            }
            set
            {
                _PatientName = value;
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

        private int _IdPatient;
        [PropertyCanBeSerialized]
        public int IdPatient
        {
            get
            {
                if (patient !=null) _IdPatient = patient.Id;
                return _IdPatient;
            }
            set
            {
                _IdPatient = value;
            }
        }

        private int _IdPraticien;
        [PropertyCanBeSerialized]
        public int IdPraticien
        {
            get
            {
                return _IdPraticien;
            }
            set
            {
                _IdPraticien = value;
            }
        }

        private bool _IsPraticienUnique = false;
        [PropertyCanBeSerialized]
        public bool IsPraticienUnique
        {
            get
            {
                return _IsPraticienUnique;
            }
            set
            {
                _IsPraticienUnique = value;
            }
        }

        private bool _IsAssistanteUnique = false;
        [PropertyCanBeSerialized]
        public bool IsAssistanteUnique
        {
            get
            {
                return _IsAssistanteUnique;
            }
            set
            {
                _IsAssistanteUnique = value;
            }
        }


        private string m_Comment;
        [PropertyCanBeSerialized]
        public string Comment
        {
            get
            {
                return m_Comment;
            }
            set
            {
                m_Comment = value;
            }
        }

        private BasCommon_BO.CommClinique _CommCl = null;
        public BasCommon_BO.CommClinique CommCl
        {
            get
            {
                return _CommCl;
            }
            set
            {
                _CommCl = value;

            }
        }

        private Acte m_acte;
        [PropertyCanBeSerialized]
        public Acte acte
        {
            get
            {
                return m_acte;
            }
            set
            {
                m_acte = value;
                if (m_acte != null)
                    this.Color = System.Drawing.Color.FromArgb(255, m_acte.acte_couleur);

            }
        }
    }

    [Serializable]
    public class Appointment
    {
        public Appointment()
        {
            color = Color.White;
            m_BorderColor = Color.Blue;
        }

             




        private Object _Tag;
        public Object Tag
        {
            get
            {
                return _Tag;
            }
            set
            {
                _Tag = value;
            }
        }
        public double Duree
        {
            get
            {
                return EndDate.Subtract(StartDate).TotalMinutes;
            }
        }

        private DateTime m_StartDate;

        [PropertyCanBeSerialized]
        public DateTime StartDate
        {
            get
            {
                return m_StartDate;
            }
            set
            {
                m_StartDate = value;
                OnStartDateChanged();

            }
        }
        protected virtual void OnStartDateChanged()
        {
        }

        private Resource m_Resource;
        [PropertyCanBeSerialized]
        public Resource Resource
        {
            get
            {
                return m_Resource;
            }
            set
            {
                m_Resource = value;
                OnResourceChanged();
            }
        }




        private DateTime m_EndDate;

        [PropertyCanBeSerialized]
        public DateTime EndDate
        {
            get
            {
                return m_EndDate;
            }
            set
            {
                m_EndDate = value;
                OnEndDateChanged();
            }
        }

        protected virtual void OnEndDateChanged()
        {
        }

        protected virtual void OnResourceChanged()
        {
        }


        private bool m_Locked = false;

        [System.ComponentModel.DefaultValue(false)]
        [PropertyCanBeSerialized]
        public bool Locked
        {
            get { return m_Locked; }
            set
            {
                m_Locked = value;
                OnLockedChanged();
            }
        }

        protected virtual void OnLockedChanged()
        {
        }

        private Color color = Color.White;

        [PropertyCanBeSerialized]
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }


      

        private Color textColor = Color.Black;

        [PropertyCanBeSerialized]
        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }

        private Color m_BorderColor = Color.Blue;

        [PropertyCanBeSerialized]
        public Color BorderColor
        {
            get
            {
                return m_BorderColor;
            }
            set
            {
                m_BorderColor = value;
            }
        }


        private string _title;

        [PropertyCanBeSerialized]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public int m_ConflictCount;
    }

   
}
