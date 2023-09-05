using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class TypePersonne:IComparable
    {


        private string _Categorie;
        public string Categorie
        {
            get
            {
                return _Categorie;
            }
            set
            {
                _Categorie = value;
            }
        }

            public override string ToString()
            {
                return Nom;
            }

            private int m_Id;
            private int m_DisplayOrder;
            private string m_Nom;

            public int Id
            {
                get { return m_Id; }
                set { m_Id = value; }
            }

            public int DisplayOrder
            {
                get { return m_DisplayOrder; }
                set { m_DisplayOrder = value; }
            }

          
            public string Nom
            {
                get { return m_Nom; }
                set { m_Nom = value; }
            }



            public int CompareTo(object obj)
            {
                if (!(obj is TypePersonne))
                    return 0;
                else
                    return ((TypePersonne)obj).DisplayOrder.CompareTo(this.DisplayOrder);
            }
    }
}
