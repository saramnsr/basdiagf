using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO.ElementsEnBouche.BO
{
    public class LigatureMetal : ElementDent, IElementDent
    {
        #region IElementDent Members


       
        public override string ToString()
        {

            string s = "";
            foreach (int d in Dents)
            {
                if (s != "") s += ",";
                s += d.ToString();
            }
            return "LIG M (" + s + ")";
        }

        public override string ShortLib
        {
            get
            {

                return "LIGM";
            }

        }



        public override string Dents
        {
            get
            {
                string res = "";
                foreach (int d in _dents)
                {
                    if (res != "") res += ",";
                    res += d.ToString();
                }
                return res;
            }
            set
            {
                try
                {
                    _dents.Clear();
                    string[] dts = value.Split(',');
                    foreach (string s in dts)
                        _dents.Add(Convert.ToInt32(s));
                }
                catch (System.Exception) { }
            }
        }


        public override List<int> LstDent
        {
            get
            {
                return _dents;
            }
            
        }

        #endregion

        private List<int> _dents = new List<int>();

        public override ElementDent.Materials typeMaterial
        {
            get
            {
                return ElementDent.Materials.LigatureM;
            }
            
        }
       

        private bool IsCoteACote(int dent1, int dent2)
        {
            if (Math.Abs(dent1 - dent2) == 1) return true;
            if ((dent1 == 11) && (dent2 == 21)) return true;
            if ((dent1 == 41) && (dent2 == 31)) return true;
            if ((dent1 == 21) && (dent2 == 11)) return true;
            if ((dent1 == 31) && (dent2 == 41)) return true;
            return false;
        }

        public int Nbdents()
        {
            return _dents.Count;
        }

        public int? getdent(int idx)
        {
            if (idx >= _dents.Count) return null;
            if (idx < 0) return null;
            return _dents[idx];
        }

        public bool contain(int numdent)
        {
            foreach (int n in _dents)
                if (n == numdent) return true;
            return false;
        }

        public bool Adddent(int numdent)
        {
            foreach (int n in _dents)
                if (n == numdent) return false;

            if (_dents.Count == 0)
            {
                _dents.Add(numdent);
                return true;
            }
            else
            {
                foreach(int n in _dents)
                    if (IsCoteACote(n, numdent))
                    {
                        _dents.Add(numdent);
                        return true;
                    }
            }
            return false;
        }

    }
}
