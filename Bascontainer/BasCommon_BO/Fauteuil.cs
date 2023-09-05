using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    [Serializable]
    public class Fauteuil : Resource
    {
        public override string ToString()
        {
            return this.Name;
        }

        public int Id;
        [PropertyCanBeSerialized]
        public string libelle
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
            }
        }

      



    }
}
