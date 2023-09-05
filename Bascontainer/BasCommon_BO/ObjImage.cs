using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class ObjImage : IComparable<ObjImage>
    {

        private int _Id;
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

        string m_fichier;
        [PropertyCanBeSerialized]
        public string fichier
        {
            get
            {
                return m_fichier;
            }
            set
            {
                m_fichier = value;
            }
        }

        string m_nom;
        [PropertyCanBeSerialized]
        public string nom
        {
            get
            {
                return m_nom;
            }
            set
            {
                m_nom = value;
            }
        }

        private int _Idpatient;
        [PropertyCanBeSerialized]
        public int Idpatient
        {
            get
            {
                return _Idpatient;
            }
            set
            {
                _Idpatient = value;
            }
        }

        private int _IdGabarit;
        [PropertyCanBeSerialized]
        public int IdGabarit
        {
            get
            {
                return _IdGabarit;
            }
            set
            {
                _IdGabarit = value;
            }
        }

        private string _auteur;
        [PropertyCanBeSerialized]
        public string auteur
        {
            get
            {
                return _auteur;
            }
            set
            {
                _auteur = value;
            }
        }

        private DateTime _dateinsertion;
        [PropertyCanBeSerialized]
        public DateTime dateinsertion
        {
            get
            {
                return _dateinsertion;
            }
            set
            {
                _dateinsertion = value;
            }
        }

        private string _syncpath;
        [PropertyCanBeSerialized]
        public string syncpath
        {
            get
            {
                return _syncpath;
            }
            set
            {
                _syncpath = value;
            }
        }

        private string _rep_stockage;
        [PropertyCanBeSerialized]
        public string rep_stockage
        {
            get
            {
                return _rep_stockage;
            }
            set
            {
                _rep_stockage = value;
            }
        }

        private DateTime _last_modif;
        [PropertyCanBeSerialized]
        public DateTime last_modif
        {
            get
            {
                return _last_modif;
            }
            set
            {
                _last_modif = value;
            }
        }

        private double _echelle;
        [PropertyCanBeSerialized]
        public double echelle
        {
            get
            {
                return _echelle;
            }
            set
            {
                _echelle = value;
            }
        }

        private DateTime _datecreation;
        [PropertyCanBeSerialized]
        public DateTime datecreation
        {
            get
            {
                return _datecreation;
            }
            set
            {
                _datecreation = value;
            }
        }

        private int _estidentite;
        [PropertyCanBeSerialized]
        public int estidentite
        {
            get
            {
                return _estidentite;
            }
            set
            {
                _estidentite = value;
            }
        }

        private int _taille;
        [PropertyCanBeSerialized]
        public int taille
        {
            get
            {
                return _taille;
            }
            set
            {
                _taille = value;
            }
        }

        private int _height;
        [PropertyCanBeSerialized]
        public int height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        private int _width;
        [PropertyCanBeSerialized]
        public int width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        private string _extension;
        [PropertyCanBeSerialized]
        public string extension
        {
            get
            {
                return _extension;
            }
            set
            {
                _extension = value;
            }
        }

        [PropertyCanBeSerialized]
        public List<Attribut> attributs { get; set; }


        public override string ToString()
        {
            return ToString(false, false);
        }

        public string ToString(bool p_att)
        {
            return ToString(p_att, false);
        }

        public string ToString(bool p_att, bool p_val)
        {
            if (p_att)
            {
                string attStr = "";
                foreach (Attribut KVP in attributs)
                {
                    if (attStr != "") attStr += ",";
                    attStr += KVP.Nom;

                }
                return fichier + "(" + attStr + ")";
            }
            else return nom;



        }

        public ObjImage()
        {
            attributs = new List<Attribut>();
        }

        public bool HasAttribut(string p_attribut)
        {
            foreach (Attribut s in attributs)
            {
                if (s.Nom == p_attribut) return true;
            }
            return false;
        }

        public bool HasOnlyAttributs(string p_attribut)
        {
            List<string> lst = new List<string>();
            string[] strs = p_attribut.Split(',');

            bool ok = true;

            foreach (Attribut kvp in attributs)
            {
                ok = false;
                foreach (string s in strs)
                {
                    if (kvp.Nom == p_attribut) ok = true;
                }
                if (!ok) continue;
            }

            return ok;
        }




        public int CompareTo(ObjImage other)
        {
            return datecreation.CompareTo(other.datecreation);
        }
    }
}
