using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO.ElementsEnBouche.BO
{
    public class Chainette : ElementDent, IElementDent 
    {
        
        public override string ToString()
        {
            return "Cha (" + DentDepart.ToString() + "-" + DentArrive.ToString() + ")";
        }

        #region IElementDent Members




        public override string ShortLib
        {
            get
            {

                return "CHA";
            }

        }



        public override string Dents
        {
            get
            {
                string res = DentDepart.ToString()+","+ DentArrive.ToString();                
                return res;
            }

            set
            {
                try
                {
                    string[] dts = value.Split(',');
                    DentDepart = Convert.ToInt32(dts[0]);
                    DentArrive = Convert.ToInt32(dts[1]);
                }
                catch (System.Exception) { }
            }
        }

        public override List<int> LstDent
        {
            get
            {
                List<int> lst = new List<int>();
                lst.Add(DentDepart);
                lst.Add(DentArrive);
                return lst;
            }

        }

        #endregion

        public override ElementDent.Materials typeMaterial
        {
            get
            {
                return ElementDent.Materials.chainette;
            }

        }

        private int _DentArrive;
        public int DentArrive
        {
            get
            {
                return _DentArrive;
            }
            set
            {
                _DentArrive = value;
            }
        }

        private int _DentDepart;
        public int DentDepart
        {
            get
            {
                return _DentDepart;
            }
            set
            {
                _DentDepart = value;
            }
        }
    }
}
