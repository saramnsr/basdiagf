using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class IdsCommActesWrapper
    {
        private int[] _ids;
        [PropertyCanBeSerialized]
        public int[] ids
        {
         get
            {
                return _ids;
            }
            set
            {
                _ids = value;
            }
        }
        public IdsCommActesWrapper(){}
    }
}
