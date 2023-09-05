using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    [Serializable]
    public class Caisse
    {
        private int m_Id = -1;
        private string m_Nom;

        private string m_Mail;
        private string m_TelFixe;
        private string m_Fax;

        private string m_Adresse_Num;
        private string m_Adresse_Type_Voie;
        private string m_Adresse_Nom_Voie;

        private string m_Adresse2;
        private string m_Ville;
        private string m_CodePostal;

        public int IdVille = -1;
        public int IdAdresse = -1;


        private bool _NeedOrder;
        [PropertyCanBeSerialized]
        public bool NeedOrder
        {
            get
            {
                return _NeedOrder;
            }
            set
            {
                _NeedOrder = value;
            }
        }


        private bool _IsCMU;
        [PropertyCanBeSerialized]
        public bool IsCMU
        {
            get
            {
                return _IsCMU;
            }
            set
            {
                _IsCMU = value;
            }
        }

        [PropertyCanBeSerialized]
        public string Adresse_Num
        {
            get { return m_Adresse_Num; }
            set { m_Adresse_Num = value; }
        }

        [PropertyCanBeSerialized]
        public string Adresse_Type_Voie
        {
            get { return m_Adresse_Type_Voie; }
            set { m_Adresse_Type_Voie = value; }
        }

        [PropertyCanBeSerialized]
        public string Adresse_Nom_Voie
        {
            get { return m_Adresse_Nom_Voie; }
            set { m_Adresse_Nom_Voie = value; }
        }



        [PropertyCanBeSerialized]
        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        public override string ToString()
        {
            return Nom;
        }

        [PropertyCanBeSerialized]
        public string Adresse2
        {
            get { return m_Adresse2; }
            set { m_Adresse2 = value; }
        }

        [PropertyCanBeSerialized]
        public string Ville
        {
            get { return m_Ville; }
            set { m_Ville = value; }
        }

        [PropertyCanBeSerialized]
        public string CodePostal
        {
            get { return m_CodePostal; }
            set { m_CodePostal = value; }
        }

        [PropertyCanBeSerialized]
        public string Mail
        {
            get { return m_Mail; }
            set { m_Mail = value; }
        }

        [PropertyCanBeSerialized]
        public string TelFixe
        {
            get { return m_TelFixe; }
            set { m_TelFixe = value; }
        }

        [PropertyCanBeSerialized]
        public string Fax
        {
            get { return m_Fax; }
            set { m_Fax = value; }
        }


        [PropertyCanBeSerialized]
        public string Nom
        {
            get { return m_Nom; }
            set { m_Nom = value; }
        }




    }
}
