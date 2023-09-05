using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
{

    

    public class InfoPatientComplementaire
    {
                
        private DateTime? _DateDebutTraitement;
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


        
               
                
        private Utilisateur _PraticienResponsable;
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
                

        private int _IdPatient = -1;
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
