using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
{
    [Serializable]
    public class RHAppointment : Appointment
    {

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

        private Acte _NextActe;
        public Acte NextActe
        {
            get
            {
                return _NextActe;
            }
            set
            {
                _NextActe = value;
            }
        }

        private DateTime? _NextRDV = null;
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


        private DateTime _DateSortie;
        public DateTime DateSortie
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

        private DateTime _DateSecretariat;
        public DateTime DateSecretariat
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

        private DateTime _DateFauteuil;
        public DateTime DateFauteuil
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

        private DateTime _DateArrive;
        public DateTime DateArrive
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
        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        private string _PatientName;
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

        private string m_Comment;
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

        private Acte m_acte;
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
}
