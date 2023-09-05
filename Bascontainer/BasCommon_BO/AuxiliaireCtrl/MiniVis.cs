using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BasCommon_BO.ElementsEnBouche.BO
{
    public class MiniVis : ElementDent, IElementDent
    {
        
        public override string ToString()
        {
            return "VIS (" + Dent.ToString() + ")";
        }

        private Point _AnchorPoint;
        public Point AnchorPoint
        {
            get
            {
                return _AnchorPoint;
            }
            set
            {
                _AnchorPoint = value;
            }
        }



        public override string ShortLib
        {
            get
            {

                return "MiniVis";
            }

        }


        public override ElementDent.Materials typeMaterial
        {
            get
            {
                return ElementDent.Materials.MiniVis;
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

        #region IElementDent Members


        public override List<int> LstDent
        {
            get
            {
                List<int> lst = new List<int>();
                lst.Add(Dent);
                return lst;
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

        #endregion
    }
}
