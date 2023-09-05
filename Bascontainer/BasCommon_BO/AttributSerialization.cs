using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CanBeReallySerialized : Attribute
    {

        private bool _CanBeReallySerialized;
        public bool value
        {
            get
            {
                return _CanBeReallySerialized;
            }
            set
            {
                _CanBeReallySerialized = value;
            }
        }


        public CanBeReallySerialized()
        {
            value = true;
        }

        public CanBeReallySerialized(bool ItCan)
        {
            value = ItCan;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyCanBeSerialized : Attribute
    {

       
        public PropertyCanBeSerialized()
        {
        }

        
    }  

  


    public class XMLSerObj 
    {
        public override string ToString()
        {
            return UID.ToString() + "[" + ToSerialize.ToString() + "]";
        }

        private int _UID;
        public int UID
        {
            get
            {
                return _UID;
            }
            set
            {
                _UID = value;
            }
        }

        private object _ToSerialize;
        public object ToSerialize
        {
            get
            {
                return _ToSerialize;
            }
            set
            {
                _ToSerialize = value;
            }
        }

        public XMLSerObj(int id, object obj)
        {
            ToSerialize = obj;
            UID = id;
        }
    }
}
