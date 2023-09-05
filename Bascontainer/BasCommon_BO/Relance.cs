using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Relance
    {
        public enum ModeRelance
        {
            /*
                Relevé de compte, (30 jours apres echeance)
			  Relance 1, (70 jours apres echeance)
			  PreContentieux, (180 jours apres echeance) (Ajout de frais de lettre recommandé + interet 10%)
			  PreContentieux 2 (210 jours) ( pas de courrier mais +10%)
			  Contentieux (1 an) 
                */
            Undefined = -1,
            Aucun = 0,
            Releve = 10,
            Relance1 = 20,
            PreContentieux = 30,
            PreContentieux2 = 40,
            Contentieux = 50

        };

        private DateTime _DateRelance;
        public DateTime DateRelance
        {
            get
            {
                return _DateRelance;
            }
            set
            {
                _DateRelance = value;
            }
        }

        private ModeRelance _NiveauDeRelance;
        public ModeRelance NiveauDeRelance
        {
            get
            {
                return _NiveauDeRelance;
            }
            set
            {
                _NiveauDeRelance = value;
            }
        }



    }
}
