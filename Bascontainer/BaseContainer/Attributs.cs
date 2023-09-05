using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public static class Attributs
    {
        //Key : Attribut (Patient, Praticien, etc.)
        //Value : Valeur (13471, 3...)
        private static Dictionary<string, string> _dictAttributs;
        public static Dictionary<string, string> DictAttributs
        {
            get
            {
                return _dictAttributs;
            }
        }

        static Attributs()
        {
            _dictAttributs = new Dictionary<string, string>();
        }
    }
}
