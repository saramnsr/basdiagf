using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    public class ObjImage
    {

        private int _Id;
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

        public List<Attribut> attributs;


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

    }

}
