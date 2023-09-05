using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    public class Address
    {


        public string Adress1;
        public string Adress2;
        public string Ville;
        public string CP;

    }

    public class Utilisateur
    {
        public Utilisateur(Utilisateur p_user)
        {
            m_Id = p_user.Id;
            m_Nom = p_user.Nom;
            m_Prenom = p_user.Prenom;
            m_Civilite = p_user.Civilite;
            m_Profession = p_user.Profession;
            m_Mail = p_user.Mail;
            m_Fonction = p_user.Fonction;
            m_Tel = p_user.Tel;
            m_Adresse = p_user.Adresse;



        }


        public Utilisateur()
        {
            m_Adresse = new Address();
        }

        public override string ToString()
        {
            return this.Nom + " " + this.Prenom;
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






        public Address Adresse
        {
            get { return m_Adresse; }
            set { m_Adresse = value; }
        }


        public string Tel
        {
            get { return m_Tel; }
            set { m_Tel = value; }
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


        public string Mail
        {
            get { return m_Mail; }
            set { m_Mail = value; }
        }

        public bool Actif
        {
            get { return m_Actif; }
            set { m_Actif = value; }
        }


        private DateTime? _DateFinContrat;
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

        public string Nom
        {
            get { return m_Nom; }
            set { m_Nom = value.ToUpper(); }
        }

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
            get { return m_Nom.Substring(0, 1) + ". " + m_Prenom; }
        }

        public string LastNameShort
        {
            get { return m_Nom + " " + m_Prenom.Substring(0, 1) + "."; }
        }

        public string Initial
        {
            get { return m_Nom.Substring(0, 1) + m_Prenom.Substring(0, 1); }
        }

        public string Civilite
        {
            get { return m_Civilite; }
            set { m_Civilite = value; }
        }

        public string Fonction
        {
            get { return m_Fonction; }
            set { m_Fonction = value; }
        }


    }
}
