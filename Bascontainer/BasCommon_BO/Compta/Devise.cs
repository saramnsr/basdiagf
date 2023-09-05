using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO.Compta
{
    public class Devise
    {

        public override string ToString()
        {
            return CodeMonnaie;
        }

        public string CodeMonnaie { get; set; }
        public string LibelleMonnaie { get; set; }

        //Le cours est par rapport à l'EURO
        public double Cours { get; set; }

        public DateTime DateMAJCours { get; set; }
    }
}
