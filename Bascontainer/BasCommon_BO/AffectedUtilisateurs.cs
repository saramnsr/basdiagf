using System;
using System.Collections.Generic;
using System.Text;

namespace BasCommon_BO
{
    public class AffectedUtilisateurs : Utilisateur 
    {

        public override string ToString()
        {
            return base.ToString()+"("+fauteuil.ToString()+")";
        }
        public AffectedUtilisateurs(Utilisateur p_user)
            : base(p_user)
        {
            isAffected = true;
        }

        public AffectedUtilisateurs()
            : base()
        {
            isAffected = true;
        }


        private bool _isAffected;
        public bool isAffected
        {
            get
            {
                return _isAffected;
            }
            set
            {
                _isAffected = value;
            }
        }

        public Fauteuil fauteuil;
        public DateTime date;
    }
}
