using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO.ElementsEnBouche.BO
{
    public class Ligature : ElementDent, IElementDent
    {

      
        public override string ToString()
        {
            return "LIG (" + Dent.ToString() + ")";
        }

        #region IElementDent Members




        public override string ShortLib
        {
            get
            {

                return "LIG";
            }

        }




        public override string Dents
        {
            get { return Dent.ToString(); }

            set
            {
                try
                {
                    Dent = Convert.ToInt32(value);
                }
                catch (System.Exception) { }
            }

        }

        public override List<int> LstDent
        {
            get
            {
                List<int> lst = new List<int>();
                lst.Add(Dent);
                return lst;
            }

        }
        #endregion


        public override ElementDent.Materials typeMaterial
        {
            get
            {
                return ElementDent.Materials.Ligature;
            }

        }

        private int _Dent;
        public int Dent
        {
            get
            {
                return _Dent;
            }
            set
            {
                _Dent = value;
            }
        }
    }
}
