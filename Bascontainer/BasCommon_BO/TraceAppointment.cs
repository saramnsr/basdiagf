using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class TraceAppointment : RHAppointment
    {


      

        private string _DisplayText = "";
        public string DisplayText
        {
            get
            {
                return _DisplayText;
            }
            set
            {
                _DisplayText = value;
            }
        }


        private Utilisateur _Utilisateur;
        public Utilisateur Utilisateur
        {
            get
            {
                return _Utilisateur;
            }
            set
            {
                _Utilisateur = value;
            }
        }

        private string _TraceComment;
        public string TraceComment
        {
            get
            {
                return _TraceComment;
            }
            set
            {
                _TraceComment = value;
            }
        }
        

        private DateTime _TraceDate;
        public DateTime TraceDate
        {
            get
            {
                return _TraceDate;
            }
            set
            {
                _TraceDate = value;
            }
        }
    }
}
