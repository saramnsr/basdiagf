using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{

    public class PointageAsRange
    {

        private DateTime _Sortie;
        public DateTime dateSortie
        {
            get
            {
                return _Sortie;
            }
            set
            {
                _Sortie = value;
            }
        }

        private DateTime _Entree;
        public DateTime dateEntree
        {
            get
            {
                return _Entree;
            }
            set
            {
                _Entree = value;
            }
        }

        private Pointage _sortie;
        public Pointage sortie
        {
            get
            {
                return _sortie;
            }
            set
            {
                _sortie = value;
            }
        }

        private Pointage _Entre;
        public Pointage Entre
        {
            get
            {
                return _Entre;
            }
            set
            {
                _Entre = value;
            }
        }
    }

    public class Pointage:IComparable
    {

        public override string ToString()
        {
            return (sens == SensPointage.Entree ? "Entre:" : "Sortie:") + DateTimePointage.ToString();
        }

        public Pointage()
        {
        }

        public Pointage(SensPointage s ,DateTime dte )
        {
            sens = s;
            DateTimePointage = dte;
        }

        public enum SensPointage
        {
            Entree = 0,
            Sortie = 1
        }




        private SensPointage _sens;
        public SensPointage sens
        {
            get
            {
                return _sens;
            }
            set
            {
                _sens = value;
            }
        }

        private int _Id = -1;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        private DateTime _DateTimePointage;
        public DateTime DateTimePointage
        {
            get
            {
                return _DateTimePointage;
            }
            set
            {
                _DateTimePointage = value;
            }
        }
      
        private int _IdUser;
        public int IdUser
        {
            get
            {
                if (user != null) _IdUser = user.Id;
                return _IdUser;
            }
            set
            {
                _IdUser = value;
            }
        }

        private Utilisateur _user;
        public Utilisateur user
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        public int CompareTo(object obj)
        {
            return -((Pointage)obj).DateTimePointage.CompareTo(DateTimePointage);
        }
    }
}
