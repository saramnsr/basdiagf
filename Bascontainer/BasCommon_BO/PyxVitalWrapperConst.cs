using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public static class PyxVitalWrapperConst
    {

        #region Parametrage

        public static bool tierspayantObligatoire = false;
        public static bool tierspayantComplementaire = false;
        #endregion


        //0 = Pas de reponse dans les temps
        //4 = Réponse favorable
        //5 = Urgence
        //? = Refus

        public enum CodeAccordDEP
        {
            Neant = -1,
            Ac0 = 0,
            Ac4 = 1,
            Ac5 = 2,
            Refus = 3
        }

        //valeurs possibles
        // Néant par défaut
        // 11 Soins en rapport avec une prest. exonérante
        // 21 Soins en rapport avec K ou KC >= 50


        // 13 Soins particuliers exonérés
        // 15 Assuré ou bénéficiaire exonéré
        // 17 Exonération dans le cadre d'un dispositif de prévention
        // 19 Fonds de Solidarité Vieillesse [FSV]

        // 9  pour FNS, mode dégradé IRIS seulement
        public enum Exoneration
        {
            ExNéant = 0,
            //  Ex11 = 1,
            //  Ex21 = 2,
            Ex13 = 3,
            Ex15 = 4,
            //  Ex9 = 5,
            Ex17 = 6,
            Ex19 = 7,

        }

        // Code Retour compris dans
        //· O
        //· N
        public enum Domicile
        {
            N = 0,
            O = 1
        }

        // Code Retour compris dans
        //· O
        //· N
        public enum RembExceptionel
        {
            N = 0,
            O = 1
        }

        // Code Retour compris dans
        //· O
        //· N
        public enum SuplCharge
        {
            N = 0,
            O = 1
        }

        // Code Retour compris dans
        //· O
        //· N
        public enum Urgence
        {
            N = 0,
            O = 1
        }



        // Code Retour compris dans
        //· O
        //· N
        public enum DEP
        {
            N = 0,
            O = 1
        }

        // Code Retour compris dans
        //· O
        //· N
        public enum Nuit
        {
            N = 0,
            O = 1
        }

        // Code Retour compris dans
        //· O
        //· N
        public enum DimancheEtJF
        {
            N = 0,
            O = 1
        }

        // Code Retour compris dans
        //· O
        //· N
        public enum ALD
        {
            N = 0,
            O = 1
        }

        // Code Retour compris dans
        //· O
        //· N
        public enum Accident
        {
            N = 0,
            O = 1
        }

        // Code Retour compris dans
        //· O
        //· N
        public enum CMU
        {
            N = 0,
            O = 1
        }

        // Code Retour compris dans
        //· Néant
        //· R
        //· HR
        public enum RNO
        {
            Néant = 0,
            R = 1,
            HR = 2,
        }


        public enum Ordonnance
        {
            O = 0,
            N = 1
        }

        // Qualification de depense avec Code Retour compris dans
        //· Néant
        //· D (pour Entente directe)
        //· E (pour Exigence particulière)
        //· F (pour Dépassement pour déplacement non prescrit)
        //· G (pour Gratuit)
        //· N (pour Non remboursable)

        public enum Qualificatif_depense
        {
            Néant = 0,
            D = 1,
            E = 2,
            F = 3,
            G = 4,
            N = 5

        }

        #region section Prestation
        public const string Section_Prest = "Prestation";
        public const string Cle_Date = "Date";
        public const string Cle_Code = "Code";
        public const string Cle_Coefficient = "Coefficient";
        public const string Cle_Montant_honoraires = "Montant_honoraires";
        public const string Cle_Qualificatif_depense = "Qualificatif_depense";
        public const string Cle_Domicile = "Domicile";
        public const string Cle_Quantite = "Quantité";
        public const string Cle_Acte_multiple = "Acte_multiple";
        public const string Cle_RMO = "RMO";
        public const string Cle_Numero_dent = "Numéro_dent";
        public const string Cle_Exoneration = "Exonération";
        public const string Cle_Appareil = "Appareil";
        public const string Cle_ALD = "ALD";
        public const string Cle_Denombrement = "Denombrement";
        public const string Cle_Accident = "Accident";
        public const string Cle_D_JF = "D_JF";
        public const string Cle_NUIT = "NUIT";
        public const string Cle_DEP = "DEP";
        public const string Cle_DATE_DEP = "DATE_DEP";
        public const string Cle_Code_accord_DEP = "Code_accord_DEP";

        public const string Cle_CodeCCAM = "Code_CCAM";
        public const string Cle_Modificateurs_CCAM = "Modificateurs_CCAM";
        public const string Cle_Code_suppl_ccam = "Code_suppl_ccam";


        #endregion

        #region section Assurance

        public const string Section_Assur = "Assurance";
        public const string Cle_Date_Accident = "Date";
        #endregion

        #region section Remboursement
        public const string Section_Rembo = "Remboursement";
        public const string Cle_Tiers_payant_partie_obligatoire = "Tiers_payant_partie_obligatoire";
        public const string Cle_Tiers_payant_partie_complementaire = "Tiers_payant_partie_complémentaire";
        public const string Cle_Mutuelle = "Mutuelle";
        public const string Cle_CMU = "CMU";
        #endregion

        #region section Prescription
        const string Section_Prescript = "Prescription";
        const string Cle_Date_Prescript = "Date";
        #endregion

        public const int WM_USER = 0x400;

        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_MAX = 11;


        #region Répertoires et fichiers PyxVital
        private static string _RepertoirePyxVital = "C:\\pyxdemo148\\";
        public static string RepertoirePyxVital
        {
            get
            {
                return _RepertoirePyxVital;
            }
            set
            {
                _RepertoirePyxVital = value;
            }
        }
        public static string RepertoirePyxVitalInterface
        {
            get
            {
                return _RepertoirePyxVital + "\\INTERF\\";
            }
        }
        public static string FichierPyxVitalStop
        {
            get
            {
                return RepertoirePyxVitalInterface + "Stop";
            }
        }
        public static string FichierPyxVitalActes
        {
            get
            {
                return RepertoirePyxVitalInterface + "Actes.par";
            }
        }
        public static string FichierPyxVitalAMC
        {
            get
            {
                return RepertoirePyxVitalInterface + "AMC.par";
            }
        }
        public static string FichierPyxVitalPatient
        {
            get
            {
                return RepertoirePyxVitalInterface + "Patient.par";
            }
        }
        public static string FichierPyxVitalPraticien
        {
            get
            {
                return RepertoirePyxVitalInterface + "Praticien.par";
            }
        }
        public static string FichierPyxVitalPatientXML
        {
            get
            {
                return RepertoirePyxVitalInterface + "Patient.xml";
            }
        }
        public static string FichierPyxVitalPraticienXML
        {
            get
            {
                return RepertoirePyxVitalInterface + "Praticien.xml";
            }
        }
        public static string FichierPyxVitalExe
        {
            get
            {
                return _RepertoirePyxVital + "Pyxvital.exe";
            }
        }
        public static string FicParam
        {
            get
            {
                return RepertoirePyxVitalInterface + "Param.par";
            }
        }
        public static string FicCV
        {
            get
            {
                return RepertoirePyxVitalInterface + "CV.par";
            }
        }

        #endregion

    }
}
