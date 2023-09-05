using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BasCommon_BO
{
    public class AffectationFauteuil
    {

        public override string ToString()
        {
            return user.ToString() + " (" + fauteuil.Name + ")";
            //return user.ToString() + " sur " + fauteuil.Name + " le " + DteFrom.Date.ToString("dd/MM/yyyy") + " de " + DteFrom.ToString("HH:mm") + " au " + DteTo.ToString("HH:mm");
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

        private string _Remarque;
        public string Remarque
        {
            get
            {
                return _Remarque;
            }
            set
            {
                _Remarque = value;
            }
        }

        private BasCommon_BO.Fauteuil _fauteuil;
        public BasCommon_BO.Fauteuil fauteuil
        {
            get
            {
                return _fauteuil;
            }
            set
            {
                _fauteuil = value;
            }
        }

        private int _Iduser;
        public int Iduser
        {
            get
            {
                if (user != null) _Iduser = user.Id;
                return _Iduser;
            }
            set
            {
                _Iduser = value;
            }
        }

        private DateTime _DteTo;
        public DateTime DteTo
        {
            get
            {
                return _DteTo;
            }
            set
            {
                _DteTo = value;
            }
        }

        private DateTime _DteFrom;
        public DateTime DteFrom
        {
            get
            {
                return _DteFrom;
            }
            set
            {
                _DteFrom = value;
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
    }
}
