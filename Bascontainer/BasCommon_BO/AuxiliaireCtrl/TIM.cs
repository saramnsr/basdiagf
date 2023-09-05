using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO.ElementsEnBouche.BO
{
    public class TIM : ElementDent, IElementDent
    {

        public override string ToString()
        {
            string s = "";
            foreach (int d in Dents)
            {
                if (s != "") s += ",";
                s += d.ToString();
            }
            return "TIM (" + s + ")";
        }

        public override string ShortLib
        {
            get
            {

                return "TIM";
            }

        }



        public override ElementDent.Materials typeMaterial
        {
            get
            {
                return ElementDent.Materials.TIM;
            }

        }

        private List<int> _dents = new List<int>();
        public override List<int> LstDent
        {
            get
            {
                return _dents;
            }
        }

        #region IElementDent Members

        public override string Dents
        {
            get
            {
                string res = "";
                foreach (int d in LstDent)
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

        #endregion
    }
}
