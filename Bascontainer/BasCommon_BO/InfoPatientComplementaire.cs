using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class InfoPatientComplementaire
    {

        private DateTime? _DateDebutTraitement;
        [PropertyCanBeSerialized]
        public DateTime? DateDebutTraitement
        {
            get
            {
                return _DateDebutTraitement;
            }
            set
            {
                _DateDebutTraitement = value;
            }
        }




        private string _Ameliorations;
        [PropertyCanBeSerialized]
        public string Ameliorations
        {
            get
            {
                return _Ameliorations;
            }
            set
            {
                _Ameliorations = value;
            }
        }

        private int _NbSemestresEntame = 0;
        [PropertyCanBeSerialized]
        public int NbSemestresEntame
        {
            get
            {
                return _NbSemestresEntame;
            }
            set
            {
                _NbSemestresEntame = value;
            }
        }



        private bool _PraticienUnique;
        [PropertyCanBeSerialized]
        public bool PraticienUnique
        {
            get
            {
                return _PraticienUnique;
            }
            set
            {
                _PraticienUnique = value;
            }
        }
        private bool _AssistanteUnique;
        [PropertyCanBeSerialized]
        public bool AssistanteUnique
        {
            get
            {
                return _AssistanteUnique;
            }
            set
            {
                _AssistanteUnique = value;
            }
        }

        private Utilisateur _PraticienResponsable;
        [PropertyCanBeSerialized]
        public Utilisateur PraticienResponsable
        {
            get
            {
                return _PraticienResponsable;
            }
            set
            {
                _PraticienResponsable = value;
            }
        }

        private Utilisateur _AssistanteResponsable;
        [PropertyCanBeSerialized]
        public Utilisateur AssistanteResponsable
        {
            get
            {
                return _AssistanteResponsable;
            }
            set
            {
                _AssistanteResponsable = value;
            }
        }


        private int _IdPatient;
        public int IdPatient
        {
            get
            {
                return _IdPatient;
            }
            set
            {
                _IdPatient = value;
            }
        }
    }
}
