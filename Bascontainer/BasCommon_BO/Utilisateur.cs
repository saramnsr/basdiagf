using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Address
    {




        private string _Adress1;
        [PropertyCanBeSerialized]
        public string Adress1
        {
            get
            {
                return _Adress1;
            }
            set
            {
                _Adress1 = value;
            }
        }


        private string _Adress2;
        [PropertyCanBeSerialized]
        public string Adress2
        {
            get
            {
                return _Adress2;
            }
            set
            {
                _Adress2 = value;
            }
        }


        private string _Ville;
        [PropertyCanBeSerialized]
        public string Ville
        {
            get
            {
                return _Ville;
            }
            set
            {
                _Ville = value;
            }
        }

        private string _CP;
        [PropertyCanBeSerialized]
        public string CP
        {
            get
            {
                return _CP;
            }
            set
            {
                _CP = value;
            }
        }


    }

    public class Utilisateur : IComparable
    {
        public enum typeUtilisateur
        {
            Assistante = 2,
            Secretaire = 5,
            Staff = 6,
            Praticien = 4
        }

        public Utilisateur(Utilisateur p_user)
        {
            _type = p_user.type;

            m_Id = p_user.Id;
            m_Nom = p_user.Nom;
            m_Prenom = p_user.Prenom;
            m_Civilite = p_user.Civilite;
            m_Profession = p_user.Profession;
            m_Mail = p_user.Mail;
            m_Fonction = p_user.Fonction;
            m_Tel = p_user.Tel;
            m_Adresse = p_user.Adresse;
            _password = p_user.password;


        }


        public Utilisateur()
        {
            m_Adresse = new Address();
        }

        public override string ToString()
        {
            return this.Prenom + " " + this.Nom;
        }




        private string _Google_Password;
        [PropertyCanBeSerialized]
        public string Google_Password
        {
            get
            {
                return _Google_Password;
            }
            set
            {
                _Google_Password = value;
            }
        }


        private string _Google_Login;
        [PropertyCanBeSerialized]
        public string Google_Login
        {
            get
            {
                return _Google_Login;
            }
            set
            {
                _Google_Login = value;
            }
        }
        


        private int m_Id;
        private string m_Nom;
        private string m_Prenom;
        private string m_Civilite;
        private string m_Profession;
        private string m_Mail;
        private string m_Fonction;
        private string m_Tel;
        private Address m_Adresse;
        private bool m_Actif;


        private int _NbJoursDeCongés = -1;
        [PropertyCanBeSerialized]
        public int NbJoursDeCongés
        {
            get
            {
                return _NbJoursDeCongés;
            }
            set
            {
                _NbJoursDeCongés = value;
            }
        }

        private string _password;
        public string password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        private EntiteJuridique _EntiteJuridique;
        [PropertyCanBeSerialized]
        public EntiteJuridique EntiteJuridique
        {
            get
            {
                return _EntiteJuridique;
            }
            set
            {
                _EntiteJuridique = value;
            }
        }

        private typeUtilisateur _type;
        [PropertyCanBeSerialized]
        public typeUtilisateur type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }


        [PropertyCanBeSerialized]
        public Address Adresse
        {
            get { return m_Adresse; }
            set { m_Adresse = value; }
        }


            private string _AssMaladie;
            [PropertyCanBeSerialized]
            public string AssMaladie 
              { 
                get { 
                      return _AssMaladie; 
                    } 
                set { 
                      _AssMaladie = value; 
                    } 
              }

            private string _NumSecu;
            [PropertyCanBeSerialized]
            public string NumSecu 
              { 
                get { 
                      return _NumSecu; 
                    } 
                set { 
                      _NumSecu = value; 
                    } 
              }


            private DateTime? _DateNaiss;
            [PropertyCanBeSerialized]
            public DateTime? DateNaiss
            {
                get
                {
                    return _DateNaiss;
                }
                set
                {
                    _DateNaiss = value;
                }
            }


            private string _NomJeuneFille;
            [PropertyCanBeSerialized]
            public string NomJeuneFille
            {
                get
                {
                    return _NomJeuneFille;
                }
                set
                {
                    _NomJeuneFille = value;
                }
            }


            private string _NumOrdre;
            [PropertyCanBeSerialized]
            public string NumOrdre 
              { 
                get { 
                      return _NumOrdre; 
                    } 
                set { 
                      _NumOrdre = value; 
                    } 
              }



            private string _DiplomeNational;
            [PropertyCanBeSerialized]
            public string DiplomeNational
            {
                get
                {
                    return _DiplomeNational;
                }
                set
                {
                    _DiplomeNational = value;
                }
            }

            private string _DiplomeUniversitaire;
            [PropertyCanBeSerialized]
            public string DiplomeUniversitaire 
              { 
                get { 
                      return _DiplomeUniversitaire; 
                    } 
                set { 
                      _DiplomeUniversitaire = value; 
                    } 
              }
        
            private string _DiplomeOptionNational;
            [PropertyCanBeSerialized]
            public string DiplomeOptionNational 
              { 
                get { 
                      return _DiplomeOptionNational; 
                    } 
                set { 
                      _DiplomeOptionNational = value; 
                    } 
              }
        

        [PropertyCanBeSerialized]
        public string Tel
        {
            get { return m_Tel; }
            set { m_Tel = value; }
        }

        [PropertyCanBeSerialized]
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


        [PropertyCanBeSerialized]
        public string Mail
        {
            get { return m_Mail; }
            set { m_Mail = value; }
        }

        [PropertyCanBeSerialized]
        public bool Actif
        {
            get { return m_Actif; }
            set { m_Actif = value; }
        }


        private DateTime? _DateFinContrat;
        [PropertyCanBeSerialized]
        public DateTime? DateFinContrat
        {
            get
            {
                return _DateFinContrat;
            }
            set
            {
                _DateFinContrat = value;
            }
        }

        private DateTime? _DateEmbauche;
        [PropertyCanBeSerialized]
        public DateTime? DateEmbauche
        {
            get
            {
                return _DateEmbauche;
            }
            set
            {
                _DateEmbauche = value;
            }
        }

        [PropertyCanBeSerialized]
        public string Nom
        {
            get { return m_Nom; }
            set { m_Nom = value.ToUpper(); }
        }

        [PropertyCanBeSerialized]
        public string Prenom
        {
            get { return m_Prenom; }
            set
            {
                try
                {
                    if (value.Length > 2)
                    {
                        m_Prenom = value.ToLower();
                        m_Prenom = m_Prenom.ToUpper()[0] + m_Prenom.ToLower().Substring(1);
                    }
                    else m_Prenom = value;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string NameShort
        {
            get { return m_Prenom + " "+ m_Nom.Substring(0, 1) + ". "; }
        }

        public string LastNameShort
        {
            get { return m_Prenom == "" ?  "" : m_Prenom.Substring(0, 1) + ". " + m_Nom; }
        }

        public string Initial
        {
            get { return m_Prenom.Substring(0, 1) + m_Nom.Substring(0, 1); }
        }

        [PropertyCanBeSerialized]
        public string Civilite
        {
            get { return m_Civilite; }
            set { m_Civilite = value; }
        }

        [PropertyCanBeSerialized]
        public string Fonction
        {
            get { return m_Fonction; }
            set { m_Fonction = value; }
        }

        private bool _SynchroPointeuse = false;
        [PropertyCanBeSerialized]
        public bool SynchroPointeuse
        {
            get
            {
                return _SynchroPointeuse;
            }
            set
            {
                _SynchroPointeuse = value;
            }
        }





        private List<Pointage> _pointages = null;
        public List<Pointage> pointages
        {
            get
            {
                return _pointages;
            }
            set
            {
                _pointages = value;
            }
        }

        DateTime CachTime;

        private List<Pointage> _pointageDuJour = null;
        public List<Pointage> pointageDuJour
        {
            get
            {
                //En cache seulement pendant 10 minutes
                if ((DateTime.Now - CachTime) > new TimeSpan(0, 0, 10, 0, 0)) _pointageDuJour = null;
                return _pointageDuJour;
            }
            set
            {
                CachTime = DateTime.Now;
                _pointageDuJour = value;
            }
        }


        private List<HoraireReel> _horairesDePointeuse = new List<HoraireReel>();
        public List<HoraireReel> horairesDePointeuse
        {
            get
            {
                return _horairesDePointeuse;
            }
            set
            {
                _horairesDePointeuse = value;
            }
        }

        private List<HoraireReel> _horairesreels = null;
        public List<HoraireReel> horairesreels
        {
            get
            {
                return _horairesreels;
            }
            set
            {
                _horairesreels = value;
            }
        }

        private List<HorairesDeTravail> m_horairesDeTravail = null;
        public List<HorairesDeTravail> horairesDeTravail
        {
            get { return m_horairesDeTravail; }
            set
            {
                m_horairesDeTravail = value;
            }
        }

        private List<Holiday> _Holidays = null;
        public List<Holiday> Holidays
        {
            get
            {
                if (_Holidays != null) _Holidays.Sort();
                return _Holidays;
            }
            set
            {
                _Holidays = value;
            }
        }

        private List<UserStatus> _status = null;
        [PropertyCanBeSerialized]
        public List<UserStatus> status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        private List<Fauteuil> m_fauteuils = new List<Fauteuil>();
        [PropertyCanBeSerialized]
        public List<Fauteuil> Fauteuils
        {
            get { return m_fauteuils; }
            set { m_fauteuils = value; }
        }


        public int CompareTo(object obj)
        {
            Utilisateur TmpUser = new Utilisateur();
            TmpUser = (Utilisateur)obj;
            return this.Nom.CompareTo(TmpUser .m_Nom );
           
        }
    }
}
