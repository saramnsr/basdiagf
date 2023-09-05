using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BASEDiag_BO
{
    [Serializable]
    public class Acte
    {

        public Acte()
        {
            id_acte = -1;
        }

        public override string ToString()
        {
            return acte_libelle;
        }
        public int id_acte;
        public string acte_libelle;
        public int acte_durestd;
        public Color acte_couleur;
        public int type_acte;
        public double nb_fautbloc;
        public string code_planing;
        public FamillesActe famille_Acte;
        public int temps_chrono;
        public int id_famille;
        public int id_fauteuil;

        private int _tps_ass;
        public int tps_ass
        {
            get
            {
                return _tps_ass;
            }
            set
            {
                _tps_ass = value;
            }
        }

        private int _tps_prat;
        public int tps_prat
        {
            get
            {
                return _tps_prat;
            }
            set
            {
                _tps_prat = value;
            }
        }


    }
}
