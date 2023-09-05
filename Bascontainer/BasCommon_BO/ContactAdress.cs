using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO.Attributes;
using System.ComponentModel;

namespace BasCommon_BO
{
    public class ContactAdresse
    {

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Adr1))
                return Adr2 + "\r\n" + CodePostal + " " + Ville + " " + pays;
            else
                if (string.IsNullOrEmpty(Adr2))
                    return Adr1 + "\r\n" + CodePostal + " " + Ville + " " + pays;
                else
                    return Adr1 + "\r\n" + Adr2 + "\r\n" + CodePostal + " " + Ville + " " + pays;
        }

        private string _Pays;
        [PropertyCanBeSerialized]
        public string Pays
        {
            get
            {
                return _Pays;
            }
            set
            {
                _Pays = value;
            }
        }

        private string _Adr1;
        [PropertyCanBeSerialized]
        public string Adr1
        {
            get
            {
                return _Adr1;
            }
            set
            {
                _Adr1 = value;
            }
        }

        private string _Adr2 = null;
        [PropertyCanBeSerialized]
        public string Adr2
        {
            get
            {
                return _Adr2;
            }
            set
            {
                _Adr2 = value;
            }
        }


        private Ville _objville;
        [PropertyCanBeSerialized]
        public Ville objville
        {
            get
            {
                return _objville;
            }
            set
            {
                _objville = value;
            }
        }


        [PropertyCanBeSerialized]
        public string CodePostal
        {
            get
            {
                return objville.CodePostal;
            }
            set
            {
                if (objville == null) objville = new BasCommon_BO.Ville();
                objville.CodePostal = value;
            }
        }


        [PropertyCanBeSerialized]
        public string Ville
        {
            get
            {
                return objville.NomVille;
            }
            set
            {
                if (objville == null) objville = new BasCommon_BO.Ville();
                objville.NomVille = value;
            }
        }
        private Pays _pays;
        [PropertyCanBeSerialized]
        public Pays pays
        {
            get
            {
                return _pays;
            }
            set
            {

                _pays = value;
            }
        }
    }
}
