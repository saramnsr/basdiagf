using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    [Serializable]
    public class CorrespondantType
    {

        

        public override string ToString()
        {
            return Nom;
        }

        private int m_Id;
        private string m_Nom;

        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        public string Nom
        {
            get { return m_Nom; }
            set { m_Nom = value; }
        }
    }

    /// <summary>
    /// Description résumée de Correspondant
    /// </summary>
    [Serializable]
    public class Correspondant : baseSmallPersonne
    {

        public enum EnumPrefCom
        {
            unknown = -1,
            Courrier = 'C',
            Telephone = 'T',
            Fax = 'F',
            Email = 'E'
        }


        private bool _publication;
        public bool publication
        {
            get
            {
                return _publication;
            }
            set
            {
                _publication = value;
            }
        }

        private int _Idprofile;
        public int Idprofile
        {
            get
            {
                return _Idprofile;
            }
            set
            {
                _Idprofile = value;
            }
        }

        private string _password;
        public string password
        {
            get
            {
                if (_password == "") _password = Nom + Id.ToString();
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        private string _IOlogin;
        public string IOlogin
        {
            get
            {
                if (_IOlogin == "") _IOlogin = Nom;
                return _IOlogin;
            }
            set
            {
                _IOlogin = value;
            }
        }

        public string NameShort
        {
            get { return Prenom + " " + Nom.Substring(0, 1) + ". "; }
        }



        public string Initial
        {
            get { return Prenom.Substring(0, 1) + Nom.Substring(0, 1); }
        }

        private int m_Type;
       

        private string m_Profession;
        private string m_AutreProfession;
        

        private string _numSecu;
        [PropertyCanBeSerialized]
        public string numSecu
        {
            get
            {
                return _numSecu;
            }
            set
            {
                _numSecu = value;
            }
        }




        private List<Echeance> _Echances = null;
        [PropertyCanBeSerialized]
        public List<Echeance> Echances
        {
            get
            {
                return _Echances;
            }
            set
            {
                _Echances = value;
            }
        }


         

        private int _LastAffectedNote = -1;
        [PropertyCanBeSerialized]
        public int LastAffectedNote
        {
            get
            {
                return _LastAffectedNote;
            }
            set
            {
                _LastAffectedNote = value;
            }
        }

        private int _Note = 0;
        [PropertyCanBeSerialized]
        public int Note
        {
            get
            {
                return _Note;
            }
            set
            {
                _LastAffectedNote = _Note;
                _Note = value;
            }
        }

        private DateTime? _DateNaissance;
        [PropertyCanBeSerialized]
        public DateTime? DateNaissance
        {
            get
            {
                return _DateNaissance;
            }
            set
            {
                _DateNaissance = value;
            }
        }

        private bool _GenreFeminin;
        [PropertyCanBeSerialized]
        public bool GenreFeminin
        {
            get
            {
                return _GenreFeminin;
            }
            set
            {
                _GenreFeminin = value;
            }
        }




        [PropertyCanBeSerialized]
        public string Profession
        {
            get { return m_Profession; }
            set { m_Profession = value; }
        }

        [PropertyCanBeSerialized]
        public string AutreProfession
        {
            get { return m_AutreProfession; }
            set { m_AutreProfession = value; }
        }

        [PropertyCanBeSerialized]
        public int Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }


       

        private string _Titre;
        [PropertyCanBeSerialized]
        public string Titre
        {
            get
            {
                return _Titre;
            }
            set
            {
                _Titre = value;
            }
        }




        private bool _TuToiement;
        [PropertyCanBeSerialized]
        public bool TuToiement
        {
            get
            {
                return _TuToiement;
            }
            set
            {
                _TuToiement = value;
            }
        }

        private EnumPrefCom _PrefCom = EnumPrefCom.Courrier;
        [PropertyCanBeSerialized]
        public EnumPrefCom PrefCom
        {
            get
            {
                return _PrefCom;
            }
            set
            {
                _PrefCom = value;
            }
        }

        private string _Notes;
        [PropertyCanBeSerialized]
        public string Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                _Notes = value;
            }
        }

        private User _user;
        [PropertyCanBeSerialized]
        public User user
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

        


        public override string ToString()
        {
            return Nom + " " + Prenom;
        }

    }
    public class User
    {
        public int id
        {
            get;
            set;
        }
        public string nom
        {
            get;
            set;
        }
        public string mdp
        {
            get;
            set;
        }
        public string prenom
        {
            get;
            set;
        }
        public string nomUser
        {
            get;
            set;
        }
        public string mail
        {
            get;
            set;
        }
        public string telephone
        {
            get;
            set;
        }
        public string titre
        {
            get;
            set;
        }
        public string sexe
        {
            get;
            set;
        }
        public string naissance
        {
            get;
            set;
        }
    }
}
