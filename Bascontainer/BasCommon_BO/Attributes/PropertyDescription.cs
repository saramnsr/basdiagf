using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class PropertyDescription : System.Attribute
    {
        public string descr;

        public PropertyDescription(string desc)
        {
            this.descr = desc;
        }
    }
}
