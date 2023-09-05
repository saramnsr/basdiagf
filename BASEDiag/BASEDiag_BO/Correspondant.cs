using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;


namespace BASEDiag_BO
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
    public class Correspondant
    {

        public enum EnumPrefCom
        {
            Courrier = 'C',
            Fax = 'F',
            Email = 'E'
        }

        private int m_Id = -1;
        private int m_Type;
        private string m_Nom;
        private string m_Prenom;
        private string m_numSecu;

       
        private string m_Profession;




        private List<Contact> _contacts = null;
        public List<Contact> contacts
        {
            get
            {
                return _contacts;
            }
            set
            {
                _contacts = value;
            }
        }

        private Contact _MainAdresse;
        public Contact MainAdresse
        {
            get
            {
                return _MainAdresse;
            }
            set
            {
                _MainAdresse = value;
            }
        }

        private Contact _MainMail;
        public Contact MainMail
        {
            get
            {
                return _MainMail;
            }
            set
            {
                _MainMail = value;
            }
        }

        private Contact _MainFax;
        public Contact MainFax
        {
            get
            {
                return _MainFax;
            }
            set
            {
                _MainFax = value;
            }
        }

        private Contact _MainTel;
        public Contact MainTel
        {
            get
            {
                return _MainTel;
            }
            set
            {
                _MainTel = value;
            }
        }

        private bool _GenreFeminin;
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


        public string numSecu
        {
            get { return m_numSecu; }
            set { m_numSecu = value; }
        }


        private List<Category> _Categories = new List<Category>();
        public List<Category> Categories
        {
            get
            {
                return _Categories;
            }
            set
            {
                _Categories = value;
            }
        }

        

        

        public string Profession
        {
            get { return m_Profession; }
            set { m_Profession = value; }
        }

        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        public int Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        

        private string _Titre;
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

        private string _mdp;
        public string mdp
        {
            get
            {
                return _mdp;
            }
            set
            {
                _mdp = value;
            }
        }

        private string _Login;
        public string Login
        {
            get
            {
                return _Login;
            }
            set
            {
                _Login = value;
            }
        }

        private EnumPrefCom _PrefCom = EnumPrefCom.Courrier;
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

       

        public string Nom
        {
            get { return m_Nom; }
            set { m_Nom = value; }
        }

        public string Prenom
        {
            get { return m_Prenom; }
            set { m_Prenom = value; }
        }


        public override string ToString()
        {
            return Nom + " " + Prenom;
        }

    }
}
