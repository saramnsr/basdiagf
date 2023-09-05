using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class AccessObject
    {
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
        private bool _SUPER_ADMIN;
        public bool SUPER_ADMIN
        {
            get
            {
                return _SUPER_ADMIN;
            }
            set
            {
                _SUPER_ADMIN = value;
            }
        }
        private bool _CanDeleteEncaissement;
        public bool CanDeleteEncaissement
        {
            get
            {
                return _CanDeleteEncaissement;
            }
            set
            {
                _CanDeleteEncaissement = value;
            }
        }




        private bool _RHBas_AllowAccessStatusClinique;
        public bool RHBas_AllowAccessStatusClinique
        {
            get
            {
                return _RHBas_AllowAccessStatusClinique;
            }
            set
            {
                _RHBas_AllowAccessStatusClinique = value;
            }
        }

        private bool _RHBas_AllowAccessRH;
        public bool RHBas_AllowAccessRH
        {
            get
            {
                return _RHBas_AllowAccessRH;
            }
            set
            {
                _RHBas_AllowAccessRH = value;
            }
        }

        private bool _CanDeleteActe;
        public bool CanDeleteActe
        {
            get
            {
                return _CanDeleteActe;
            }
            set
            {
                _CanDeleteActe = value;
            }
        }

        private bool _BasPract_Comptabilite;
        public bool BasPract_Comptabilite
        {
            get
            {
                return _BasPract_Comptabilite;
            }
            set
            {
                _BasPract_Comptabilite = value;
            }
        }

        private bool _Bas_Stat_AllowBPTransfert;
        public bool Bas_Stat_AllowBPTransfert
        {
            get
            {
                return _Bas_Stat_AllowBPTransfert;
            }
            set
            {
                _Bas_Stat_AllowBPTransfert = value;
            }
        }
        

        private bool _BasPract_HistoriqueFinances;
        public bool BasPract_HistoriqueFinances
        {
            get
            {
                return _BasPract_HistoriqueFinances;
            }
            set
            {
                _BasPract_HistoriqueFinances = value;
            }
        }
        
        private bool _BasPract_ListControles;
        public bool BasPract_ListControles
        {
            get
            {
                return _BasPract_ListControles;
            }
            set
            {
                _BasPract_ListControles = value;
            }
        }


        private bool _BasPract_ListFinancieres;
        public bool BasPract_ListFinancieres
        {
            get
            {
                return _BasPract_ListFinancieres;
            }
            set
            {
                _BasPract_ListFinancieres = value;
            }
        }
        
        private bool _Bas_Stat_AllowFinances;
        public bool Bas_Stat_AllowFinances
        {
            get
            {
                return _Bas_Stat_AllowFinances;
            }
            set
            {
                _Bas_Stat_AllowFinances = value;
            }
        }

        private bool _RHBas_AllowToMoveRDV;
        public bool RHBas_AllowToMoveRDV
        {
            get
            {
                return _RHBas_AllowToMoveRDV;
            }
            set
            {
                _RHBas_AllowToMoveRDV = value;
            }
        }

        private bool _RHBas_AllowToDeleteRDV;
        public bool RHBas_AllowToDeleteRDV
        {
            get
            {
                return _RHBas_AllowToDeleteRDV;
            }
            set
            {
                _RHBas_AllowToDeleteRDV = value;
            }
        }
        private bool _RHBas_AllowToRaccourcirRDV;
        public bool RHBas_AllowToRaccourcirRDV
        {
            get
            {
                return _RHBas_AllowToRaccourcirRDV;
            }
            set
            {
                _RHBas_AllowToRaccourcirRDV = value;
            }
        }

        private string _Password;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }
    }
}
