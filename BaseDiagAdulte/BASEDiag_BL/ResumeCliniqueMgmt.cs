using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;
using BasCommon_BO;
using BasCommon_BL;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BASEDiag_BL
{
    public static class ResumeCliniqueMgmt
    {
        
        /// <summary>
        /// Appareils
        /// </summary>
        public const string ARCLINGUAL = "AL";
        public const string DISJONCTEUR = "DISJ";
        public const string QUADHELIX = "QH";
        public const string ASI = "ASI";
        public  const string MASQUEDELAIRE = "MD";
        public const string RCC = "RCC";
         
         
         /// <summary>
         /// Traitements
         /// </summary>
        public const string ORTHOPEDIE = "ORTHP";
        public const string ORTHOPEDIEHN = "ORTHP_HN";
        public const string PEDIATRIE = "PEDIATRIE";
        public const string MULTIBAGUELINGU = "MBLING";
        public const string MULTIBAGUECERAM = "MBCERAM";
        public const string MULTIBAGUEMETAL = "MBMETAL";
        public const string INVISALIGN = "INVTEEN";
        





        public static void Analyse1AffectDefault()
        {
            if (_resumeCl.EtageInf == BasCommon_BO.EntentePrealable.en_EtageInf.undefined)
                _resumeCl.EtageInf = BasCommon_BO.EntentePrealable.en_EtageInf.Normal;
        }

        public static void Analyse2AffectDefault()
        {
            if (_resumeCl.sourireDentaire == BasCommon_BO.EntentePrealable.en_SourireDentaire.undefined)
                _resumeCl.sourireDentaire = BasCommon_BO.EntentePrealable.en_SourireDentaire.Normal;

            if (_resumeCl.DiagAlveolaire == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined)
                _resumeCl.DiagAlveolaire = BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Normoalveolie;

            if (_resumeCl.TNLDroit == BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.undefined)
                _resumeCl.TNLDroit = BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.TNL1;

            if (_resumeCl.TNLGauche == BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.undefined)
                _resumeCl.TNLGauche = BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.TNL1;
        }

        public static void Analyse3AffectDefault()
        {
            if (_resumeCl.FreinLabial == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                _resumeCl.FreinLabial = BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            if (_resumeCl.FreinLingual == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                _resumeCl.FreinLingual = BasCommon_BO.EntentePrealable.en_OuiNon.Non;

            if (_resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.undefined)
                _resumeCl.ClasseMolD = BasCommon_BO.EntentePrealable.en_Class.Class_I;
            if (_resumeCl.ClasseMolG == BasCommon_BO.EntentePrealable.en_Class.undefined)
                _resumeCl.ClasseMolG = BasCommon_BO.EntentePrealable.en_Class.Class_I;

            if (_resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.undefined)
                _resumeCl.ClasseCanD = BasCommon_BO.EntentePrealable.en_Class.Class_I;
            if (_resumeCl.ClasseCanG == BasCommon_BO.EntentePrealable.en_Class.undefined)
                _resumeCl.ClasseCanG = BasCommon_BO.EntentePrealable.en_Class.Class_I;

            if (_resumeCl.OcclusionFace == BasCommon_BO.EntentePrealable.en_OccFace.undefined)
                _resumeCl.OcclusionFace = BasCommon_BO.EntentePrealable.en_OccFace.Normal;
            if (_resumeCl.SautArticule == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                _resumeCl.SautArticule = BasCommon_BO.EntentePrealable.en_OuiNon.Non;

            if (_resumeCl.DiagMax == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined)
                _resumeCl.DiagMax = BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Normoalveolie;
            if (_resumeCl.DiagMand == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined)
                _resumeCl.DiagMand = BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Normoalveolie;

            if (_resumeCl.SensTransvMax == BasCommon_BO.EntentePrealable.en_DiagOsseux.undefined)
                _resumeCl.SensTransvMax = BasCommon_BO.EntentePrealable.en_DiagOsseux.Normognatie;
            if (_resumeCl.SensTransvMand == BasCommon_BO.EntentePrealable.en_DiagOsseux.undefined)
                _resumeCl.SensTransvMand = BasCommon_BO.EntentePrealable.en_DiagOsseux.Normognatie;
        }
        public static void AnalyseFonctionnelAffectDefault()
        {
            //if (_resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined)
            //    _resumeCl.InterpositonLingual = BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined;
            //if (_resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined)
            //    _resumeCl.InterpositonLingual = BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined;
            //if (_resumeCl.InterpositonLingual4 == BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined)
            //    _resumeCl.InterpositonLingual4 = BasCommon_BO.EntentePrealable.en_InterpositionLingual.AnterieurEtPosterieur;

           //if (_resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.undefined)
           //    _resumeCl.Laterodeviation = BasCommon_BO.EntentePrealable.en_Laterodeviation.undefined;
            //if (_resumeCl.Laterodeviation== BasCommon_BO.EntentePrealable.en_Laterodeviation.undefined)
            //    _resumeCl.Laterodeviation = BasCommon_BO.EntentePrealable.en_Laterodeviation.undefined;

            //if (_resumeCl.FormeRespiration4 == BasCommon_BO.EntentePrealable.en_Respiration.undefined)
            //    _resumeCl.FormeRespiration4 = BasCommon_BO.EntentePrealable.en_Respiration.buccale;
            if (_resumeCl.FormeRespiration == BasCommon_BO.EntentePrealable.en_Respiration.undefined)
                _resumeCl.FormeRespiration = BasCommon_BO.EntentePrealable.en_Respiration.undefined;

        }

        public static void Analyse4AffectDefault()
        {
            if (_resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined)
                _resumeCl.InterpositonLingual = BasCommon_BO.EntentePrealable.en_InterpositionLingual.Normal;
            if (_resumeCl.DDD == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                _resumeCl.DDD = BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            if (_resumeCl.Diasteme == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                _resumeCl.Diasteme = BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            if (_resumeCl.DDM == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                _resumeCl.DDM = BasCommon_BO.EntentePrealable.en_OuiNon.Oui;
            if (_resumeCl.LangueBasse == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                _resumeCl.LangueBasse = BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            if (_resumeCl.FreinLabial == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                _resumeCl.FreinLabial = BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            if (_resumeCl.FreinLingual == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                _resumeCl.FreinLingual = BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            if (_resumeCl.FormeArcade == BasCommon_BO.EntentePrealable.en_FormeArcade.undefined)
                _resumeCl.FormeArcade = BasCommon_BO.EntentePrealable.en_FormeArcade.U;

        }
        
        public static void Analyse5AffectDefault()
        {
            if (_resumeCl.SourireGingivalInf == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                _resumeCl.SourireGingivalInf = BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            if (_resumeCl.SourireGingivalSup == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                _resumeCl.SourireGingivalSup = BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            if (_resumeCl.SourireLabial == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                _resumeCl.SourireLabial = BasCommon_BO.EntentePrealable.en_OuiNon.Non;

        }

        public static void Analyse6AffectDefault()
        {
            if (_resumeCl.LevreInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                _resumeCl.LevreInferieur = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            if (_resumeCl.LevreSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                _resumeCl.LevreSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            if (_resumeCl.Menton == BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                _resumeCl.Menton = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;

        }

        public static void Analyse7AffectDefault()
        {
            if (_resumeCl.SensSagittal == BasCommon_BO.EntentePrealable.en_Class.undefined)
                _resumeCl.SensSagittal = BasCommon_BO.EntentePrealable.en_Class.Class_I;
            if (_resumeCl.SensVertical == BasCommon_BO.EntentePrealable.en_Divergence.undefined)
                _resumeCl.SensVertical = BasCommon_BO.EntentePrealable.en_Divergence.Normodivergent;
            if (_resumeCl.SensSagittalMandBasal == BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                _resumeCl.SensSagittalMandBasal = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            if (_resumeCl.SensSagittalMaxBasal == BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                _resumeCl.SensSagittalMaxBasal = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            if (_resumeCl.IncisiveInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                _resumeCl.IncisiveInferieur = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;

        }

        public static void Analyse8AffectDefault()
        {
            if (_resumeCl.EvolGermesDesDents == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                _resumeCl.EvolGermesDesDents = BasCommon_BO.EntentePrealable.en_OuiNon.Non;
            

        }

        public static void AnalysePlanTraitementAffectDefault()
        {
            if (_resumeCl.objectsForPlanTraitement == null)
                _resumeCl.objectsForPlanTraitement = new List<PlanTraitementObject>();
            else
                _resumeCl.objectsForPlanTraitement.Clear();

        }

        public static bool Analyse1IsStarted
        {
            get
            {
                return _resumeCl.EtageInf != BasCommon_BO.EntentePrealable.en_EtageInf.undefined;
            }
        }

        public static bool Analyse2IsStarted
        {
            get
            {
                return ((_resumeCl.sourireDentaire != BasCommon_BO.EntentePrealable.en_SourireDentaire.Normal)
                    || (_resumeCl.DiagAlveolaire != BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined)
                    || (_resumeCl.TNLDroit != BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.undefined)
                    || (_resumeCl.TNLGauche != BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.undefined)
                    );
            }
        }

        public static bool Analyse3IsStarted
        {
            get
            {
                return ((_resumeCl.ClasseMolD != BasCommon_BO.EntentePrealable.en_Class.undefined)
                    || (_resumeCl.ClasseMolG != BasCommon_BO.EntentePrealable.en_Class.undefined)
                    || (_resumeCl.ClasseCanD != BasCommon_BO.EntentePrealable.en_Class.undefined)
                    || (_resumeCl.ClasseCanG != BasCommon_BO.EntentePrealable.en_Class.undefined)
                    || (_resumeCl.OcclusionFace != BasCommon_BO.EntentePrealable.en_OccFace.undefined)

                    || (_resumeCl.DiagMax != BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined)
                    || (_resumeCl.DiagMand != BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined)
                    || (_resumeCl.SensTransvMax != BasCommon_BO.EntentePrealable.en_DiagOsseux.undefined)
                    || (_resumeCl.SensTransvMand != BasCommon_BO.EntentePrealable.en_DiagOsseux.undefined)
                    || (_resumeCl.FreinLabial != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    || (_resumeCl.FreinLingual != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)

                    );
            }
        }
        public static bool AnalyseFonctionnelIsStarted
        {
            get
            {
                return ((_resumeCl.FormeRespiration != BasCommon_BO.EntentePrealable.en_Respiration.undefined)
                    // || (_resumeCl.Laterodeviation != BasCommon_BO.EntentePrealable.en_Laterodeviation.undefined));
                 || (_resumeCl.InterpositonLingual != BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined));

            }
        }

        //public static bool AnalyseFonctionnelIsStarted
        //{
        //    get
        //    {
        //        return ((_resumeCl.FormeRespiration == BasCommon_BO.EntentePrealable.en_Respiration.undefined)

        //           || (_resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.undefined)

        //          || (_resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined)
        //            //|| (_resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined)
        //            );
              
        //    }
        //}
        public static bool Analyse4IsStarted
        {
            get
            {
                
                return ((_resumeCl.InterpositonLingual!= BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined)
                    || (_resumeCl.DDD != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    || (_resumeCl.LangueBasse != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    || (_resumeCl.FreinLabial != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    || (_resumeCl.FreinLingual != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    || (_resumeCl.FormeArcade != BasCommon_BO.EntentePrealable.en_FormeArcade.undefined)
                    );
            }
        }

        public static bool Analyse5IsStarted
        {
            get
            {
                return ((_resumeCl.SourireGingivalInf != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    || (_resumeCl.SourireGingivalSup != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    || (_resumeCl.SourireLabial != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    );
            }
        }

        public static bool Analyse6IsStarted
        {
            get
            {
                return ((_resumeCl.LevreInferieur != BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                    || (_resumeCl.LevreSuperieur != BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                    || (_resumeCl.Menton != BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                  //  || (_resumeCl.IncisiveSuperieur != BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                   );
            }
        }

        public static bool Analyse7IsStarted
        {
            get
            {
                return ((_resumeCl.SensSagittal != BasCommon_BO.EntentePrealable.en_Class.undefined)
                    || (_resumeCl.SensVertical != BasCommon_BO.EntentePrealable.en_Divergence.undefined)
                    || (_resumeCl.SensSagittalMandBasal != BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                    || (_resumeCl.SensSagittalMaxBasal != BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                    || (_resumeCl.IncisiveInferieur != BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                    );
            }
        }

        public static bool Analyse8IsStarted
        {
            get
            {
                return ((_resumeCl.Agenesie != "")
                    || (_resumeCl.DentsIncluses != "")
                    || (_resumeCl.DentsSurnumeraires != "")
                    || (_resumeCl.NoTaquets != "")
                    || (_resumeCl.NoMvts != "")
                    || (_resumeCl.DentsDeSagesse != "")
                    || (_resumeCl.Controle != "")
                    || (_resumeCl.EvolGermesDesDents != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    );
            }
        }
                
        //
        public static string Analyse1IsValid
        {
            get
            {
                if (_resumeCl.EtageInf == BasCommon_BO.EntentePrealable.en_EtageInf.undefined)
                    return "Etage inférieur non renseigné";
                return "";
            }
        }

        public static string AnalysePlanTraitementIsValid
        {
            get
            {
                return "";
            }
        }

        

        public static string Analyse2IsValid
        {
            get
            {
                string str = "";
                if (_resumeCl.sourireDentaire == BasCommon_BO.EntentePrealable.en_SourireDentaire.undefined)
                    str += "\nSourire dentaire non renseigné";
                if (_resumeCl.TNLDroit == BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.undefined)
                    str += "\nTNL droit non renseigné";
                if (_resumeCl.TNLGauche == BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.undefined)
                    str += "\nTNL gauche non renseigné";
                return str;

                
            }
        }
                
        public static string Analyse3IsValid
        {
            get
            {

                string str = "";
                if (_resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.undefined)
                    str += "\nClasse Molaire gauche non renseignée";
                if (_resumeCl.ClasseMolG == BasCommon_BO.EntentePrealable.en_Class.undefined)
                    str += "\nClasse Molaire droite non renseignée";
                if (_resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.undefined)
                    str += "\nClasse Canine droite non renseignée";
                if (_resumeCl.ClasseCanG == BasCommon_BO.EntentePrealable.en_Class.undefined)
                    str += "\nClasse Canine gauche non renseignée";
                if (_resumeCl.OcclusionFace == BasCommon_BO.EntentePrealable.en_OccFace.undefined)
                    str += "\nOcclusion de face non renseignée";
                if (_resumeCl.SautArticule == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    str += "\nSautArticule de face non renseignée";

                
                if (_resumeCl.OcclusionInverse == BasCommon_BO.EntentePrealable.en_OccInverse.undefined)
                    str += "\nArticule inverse non renseigné";
                if (_resumeCl.DiagMax == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined)
                    str += "\nDiagnostique maxilaire non renseigné";
                if (_resumeCl.DiagMand == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined)
                    str += "\nDiagnostique mandibulaire non renseigné";
                if (_resumeCl.SensTransvMax == BasCommon_BO.EntentePrealable.en_DiagOsseux.undefined)
                    str += "\nSens transversal maxilaire non renseigné";
                if (_resumeCl.SensTransvMand == BasCommon_BO.EntentePrealable.en_DiagOsseux.undefined)
                    str += "\nSens transversal mandibulaire non renseigné";
                if (_resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.undefined)
                    str += "\nLateroDéviation non renseigné";

                if (_resumeCl.FreinLabial == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    str += "\nFrein labial non renseigné";
                if (_resumeCl.FreinLingual == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    str += "\nFrein lingual non renseigné";
                return str;


                
            }
        }
        public static string AnalyseFonctionnelIsValid
        {
            get
            {

                string str = "";

                //    if (_resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined)
                //      str += "\nInterpositon Lingual non renseigné";
                // if (_resumeCl.FormeRespiration == BasCommon_BO.EntentePrealable.en_Respiration.undefined)
                //    str += "\nRespiration non renseigné";

                if (_resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.undefined)
                    str += "\nLateroDéviation Fonctionnel non renseigné";

                return str;

            }
        }

        //public static string AnalyseFonctionnelIsValid
        //{
        //    get
        //    {

        //        string str = "";

        //        if (ResumeCliniqueMgmt.Analyse4IsStarted && ResumeCliniqueMgmt.Analyse4IsValid != "")
        //        {
        //            str += "analyse arcade is started";
        //        }
        //    //   if (_resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined)
        //    //str += "\nInterpositon Lingual non renseigné";
        //        //if (_resumeCl.FormeRespiration == BasCommon_BO.EntentePrealable.en_Respiration.buccale)
        //        //    str += "\nRespiration non renseigné";

        //        //if (_resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.undefined)
        //        //    str += "\nLateroDéviation Fonctionnel non renseigné";

        //        return str;

        //    }
        //}

        public static string Analyse4IsValid
        {
            get
            {

                string str = "";
                if (_resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined)
                    str += "\nInterposition lingual non renseignée";
                if (_resumeCl.DDM == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    str += "\nDDM non renseignée";
                if (_resumeCl.Diasteme == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    str += "\nDiasteme non renseignée";
                if (_resumeCl.DDD == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    str += "\nDDD non renseignée";
                if (_resumeCl.LangueBasse == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    str += "\nLangue basse non renseignée";
                if (_resumeCl.FreinLabial == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    str += "\nFrein Labial non renseignée";
                if (_resumeCl.FreinLingual == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    str += "\nFrein Lingual non renseignée";               
                
                if (_resumeCl.FormeArcade == BasCommon_BO.EntentePrealable.en_FormeArcade.undefined)
                    str += "\nForme de l'arcade non renseignée";
                return str;
               
            }
        }

        public static string Analyse5IsValid
        {
            get
            {
                string str = "";
                if (_resumeCl.SourireGingivalInf == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    str += "\nType de sourire non renseignée";
                if (_resumeCl.SourireGingivalSup == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    str += "\nType de sourire non renseignée";
                if (_resumeCl.SourireLabial == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    str += "\nType de sourire non renseignée";

                return str;
                
            }
        }

        public static string Analyse6IsValid
        {
            get
            {

                string str = "";
                if (_resumeCl.LevreInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                    str += "\nLevre inférieure non renseignée";
                if (_resumeCl.LevreSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                    str += "\nLevre supérieure non renseignée";
                if (_resumeCl.Menton == BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                    str += "\nMenton non renseignée";
                
                /*
                if (_resumeCl.IncisiveSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                    str += "\nIncisive Supérieur non renseignée";
                */

                return str;

                
            }
        }

        public static string Analyse7IsValid
        {
            get
            {
                string str = "";
                if (_resumeCl.SensSagittal == BasCommon_BO.EntentePrealable.en_Class.undefined)
                    str += "\nSens Sagittal non renseignée";
                if (_resumeCl.SensVertical == BasCommon_BO.EntentePrealable.en_Divergence.undefined)
                    str += "\nSens vertical non renseignée";
                if (_resumeCl.SensSagittalMandBasal == BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                    str += "\nSens sagittal Mandibulaire non renseignée";
                if (_resumeCl.SensSagittalMaxBasal == BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                    str += "\nSens sagittal Maxilaire non renseignée";
                if (_resumeCl.IncisiveInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.undefined)
                    str += "\nIncisive Inférieur non renseignée";

                return str;

               
            }
        }

        public static string Analyse8IsValid
        {
            get
            {

                string str = "";
                if (_resumeCl.EvolGermesDesDents == BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    str += "\nEvolution des germes des dents non renseignée";
                return str;
               
            }
        }




        public static string AllAnalysesAreValid
        {
            get
            {
                return Analyse1IsValid + Analyse2IsValid + Analyse3IsValid + Analyse4IsValid + Analyse5IsValid + Analyse6IsValid + Analyse7IsValid + Analyse8IsValid;
            }
        }

        public static void AddAttributsToCourrier()
        {


            int y;
            int m;
            int d;
            ResumeCliniqueMgmt.resumeCl.patient.AgeToDate(DateTime.Now, out y, out m, out d);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", ResumeCliniqueMgmt.resumeCl.patient.Id.ToString());
            
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", ResumeCliniqueMgmt.resumeCl.patient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", ResumeCliniqueMgmt.resumeCl.patient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et "+m.ToString()+" mois");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Sexe", ResumeCliniqueMgmt.resumeCl.patient.Genre == basePatient.Sexe.Feminin?"F".ToString():"M".ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ResumeQ1CS", ResumeCliniqueMgmt.resumeCl.patient.ResumeQ1CS);




            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DeviationLevreInf", ResumeCliniqueMgmt.resumeCl.DeviationLevreInf.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DeviationMenton", ResumeCliniqueMgmt.resumeCl.DeviationMenton.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EtageInf", ResumeCliniqueMgmt.resumeCl.EtageInf.ToString().Replace('_', ' '));


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("sourireDentaire", ResumeCliniqueMgmt.resumeCl.sourireDentaire.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DiagAlveolaire", ResumeCliniqueMgmt.resumeCl.DiagAlveolaire.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TNLDroit", ResumeCliniqueMgmt.resumeCl.TNLDroit.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TNLGauche", ResumeCliniqueMgmt.resumeCl.TNLGauche.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DecalageInterIncisiveDG", ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveHaut.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DecalageInterIncisiveHB", ResumeCliniqueMgmt.resumeCl.DecalageInterIncisiveBas.ToString().Replace('_', ' '));


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ClasseCanD", ResumeCliniqueMgmt.resumeCl.ClasseCanD.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ClasseCanG", ResumeCliniqueMgmt.resumeCl.ClasseCanG.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ClasseMolD", ResumeCliniqueMgmt.resumeCl.ClasseMolD.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ClasseMolG", ResumeCliniqueMgmt.resumeCl.ClasseMolG.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SensTransvMand", ResumeCliniqueMgmt.resumeCl.SensTransvMand.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SensTransvMax", ResumeCliniqueMgmt.resumeCl.SensTransvMax.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DiagMand", ResumeCliniqueMgmt.resumeCl.DiagMand.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DiagMax", ResumeCliniqueMgmt.resumeCl.DiagMax.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("OcclusionInverse", ResumeCliniqueMgmt.resumeCl.OcclusionInverse.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SautArticule", ResumeCliniqueMgmt.resumeCl.SautArticule.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("OcclusionValue", ResumeCliniqueMgmt.resumeCl.OcclusionValue.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("OcclusionFace", ResumeCliniqueMgmt.resumeCl.OcclusionFace.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Laterodeviation", ResumeCliniqueMgmt.resumeCl.Laterodeviation.ToString().Replace('_', ' '));


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("InterpositonLingual", ResumeCliniqueMgmt.resumeCl.InterpositonLingual.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FormeArcade", ResumeCliniqueMgmt.resumeCl.FormeArcade.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SurplombValue", ResumeCliniqueMgmt.resumeCl.SurplombValue.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FreinLabial", ResumeCliniqueMgmt.resumeCl.FreinLabial.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FreinLingual", ResumeCliniqueMgmt.resumeCl.FreinLingual.ToString().Replace('_', ' '));

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("LangueBasse", ResumeCliniqueMgmt.resumeCl.LangueBasse.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DDD", ResumeCliniqueMgmt.resumeCl.DDD.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Diasteme", ResumeCliniqueMgmt.resumeCl.Diasteme.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DDM", ResumeCliniqueMgmt.resumeCl.DDM.ToString().Replace('_', ' '));


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SourireGingivalSup", ResumeCliniqueMgmt.resumeCl.SourireGingivalSup.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("LabialValue", ResumeCliniqueMgmt.resumeCl.LabialValue.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GingivalInfValue", ResumeCliniqueMgmt.resumeCl.GingivalInfValue.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GingivalSupValue", ResumeCliniqueMgmt.resumeCl.GingivalSupValue.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SourireGingivalInf", ResumeCliniqueMgmt.resumeCl.SourireGingivalInf.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SourireLabial", ResumeCliniqueMgmt.resumeCl.SourireLabial.ToString().Replace('_', ' '));


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("InclinaisonIncisiveSupValue", ResumeCliniqueMgmt.resumeCl.InclinaisonIncisiveSupValue.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IncisiveSuperieur", ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Menton", ResumeCliniqueMgmt.resumeCl.Menton.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("LevreInferieur", ResumeCliniqueMgmt.resumeCl.LevreInferieur.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("LevreSuperieur", ResumeCliniqueMgmt.resumeCl.LevreSuperieur.ToString().Replace('_', ' '));


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SensSagittalMandBasal", ResumeCliniqueMgmt.resumeCl.SensSagittalMandBasal.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SensSagittalMaxBasal", ResumeCliniqueMgmt.resumeCl.SensSagittalMaxBasal.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("IncisiveInferieur", ResumeCliniqueMgmt.resumeCl.IncisiveInferieur.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SensSagittal", ResumeCliniqueMgmt.resumeCl.SensSagittal.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("SensVertical", ResumeCliniqueMgmt.resumeCl.SensVertical.ToString().Replace('_', ' '));


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EvolGermesDesDentsSur", ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("EvolGermesDesDents", ResumeCliniqueMgmt.resumeCl.EvolGermesDesDents.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DentsDeSagesse", ResumeCliniqueMgmt.resumeCl.DentsDeSagesse);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NoTaquets", ResumeCliniqueMgmt.resumeCl.NoTaquets);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NoMvts", ResumeCliniqueMgmt.resumeCl.NoMvts);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DentsSurnumeraires", ResumeCliniqueMgmt.resumeCl.DentsSurnumeraires);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DentsIncluses", ResumeCliniqueMgmt.resumeCl.DentsIncluses);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Agenesie", ResumeCliniqueMgmt.resumeCl.Agenesie);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Controle", ResumeCliniqueMgmt.resumeCl.Controle);
            

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Rad_Face", ResumeCliniqueMgmt.resumeCl.Img_Rad_Face);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Rad_Pano", ResumeCliniqueMgmt.resumeCl.Img_Rad_Pano);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Rad_Profile", ResumeCliniqueMgmt.resumeCl.Img_Rad_Profile);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Ext_Face", ResumeCliniqueMgmt.resumeCl.Img_Ext_Face);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Ext_Profile", ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Ext_Profile_Sourire", ResumeCliniqueMgmt.resumeCl.Img_Ext_Profile_Sourire);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Ext_Face_Sourire", ResumeCliniqueMgmt.resumeCl.Img_Ext_Face_Sourire);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Ext_Sourire", ResumeCliniqueMgmt.resumeCl.Img_Ext_Sourire);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Int_Droit", ResumeCliniqueMgmt.resumeCl.Img_Int_Droit);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Int_SurPlomb", ResumeCliniqueMgmt.resumeCl.Img_Int_SurPlomb);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Int_Face", ResumeCliniqueMgmt.resumeCl.Img_Int_Face);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Int_Gauche", ResumeCliniqueMgmt.resumeCl.Img_Int_Gauche);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Int_Max", ResumeCliniqueMgmt.resumeCl.Img_Int_Max);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Int_Man", ResumeCliniqueMgmt.resumeCl.Img_Int_Man);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Moul_Droit", ResumeCliniqueMgmt.resumeCl.Img_Moul_Droit);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Moul_Face", ResumeCliniqueMgmt.resumeCl.Img_Moul_Face);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Moul_Gauche", ResumeCliniqueMgmt.resumeCl.Img_Moul_Gauche);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Moul_Max", ResumeCliniqueMgmt.resumeCl.Img_Moul_Max);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Img_Moul_Man", ResumeCliniqueMgmt.resumeCl.Img_Moul_Man);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Laterodeviation", ResumeCliniqueMgmt.resumeCl.Laterodeviation.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("InterpositonLingual", ResumeCliniqueMgmt.resumeCl.InterpositonLingual.ToString().Replace('_', ' '));
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Respiration", ResumeCliniqueMgmt.resumeCl.FormeRespiration.ToString().Replace('_', ' '));
        }


        public static void GenerateCurrentDiags()
        {
            List<CommonDiagnostic> lst = new List<CommonDiagnostic>();

            GetDiagsForAnalyseFace(lst);
            GetDiagsForAnalyseProfil(lst);
            GetDiagsForAnalyseTeleRadio(lst);
            GetDiagsForAnalysePano(lst);
            GetDiagsForAlveolaire(lst);
            GetDiagsForFonctionnel(lst);
            GetDiagsForAnalyseSourie(lst);

            for (int i = lst.Count - 1; i >= 0; i--)
                if (lst[i] == null) lst.Remove(lst[i]);

            resumeCl.diagnostics = lst;

        }

        public static void GetDiagsForAnalyseSourie(List<CommonDiagnostic> lst)
        {
            #region analyse du sourire

            if (_resumeCl.sourireDentaire == BasCommon_BO.EntentePrealable.en_SourireDentaire.Etroit)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(33));

            if ((_resumeCl.TNLDroit == EntentePrealable.en_TriangleNoirLateralType.TNL2) || (_resumeCl.TNLGauche == EntentePrealable.en_TriangleNoirLateralType.TNL2))
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(50));
            if ((_resumeCl.TNLDroit == EntentePrealable.en_TriangleNoirLateralType.TNL3) || (_resumeCl.TNLGauche == EntentePrealable.en_TriangleNoirLateralType.TNL3))
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(51));

                

            if (_resumeCl.SourireGingivalInf == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(35));

            if (_resumeCl.SourireGingivalSup == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(36));

            if (_resumeCl.SourireLabial == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(37));

            #endregion
        }

        public static void GetDiagsForFonctionnel(List<CommonDiagnostic> lst)
        {
            #region fonctionnel
            if (_resumeCl.SurplombValue != 0)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(34));
            
            if (_resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.anterieur)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(28));
            if (_resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.posterieur)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(29));
            if (_resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.AnterieurEtPosterieur)
               lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(30));
           if ((Math.Abs(_resumeCl.LabialValue) > 0) && (_resumeCl.InterpositonLingual != BasCommon_BO.EntentePrealable.en_InterpositionLingual.Normal))
            lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(31));

            if (_resumeCl.FormeRespiration == BasCommon_BO.EntentePrealable.en_Respiration.buccale)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(80));
            if (_resumeCl.FormeRespiration == BasCommon_BO.EntentePrealable.en_Respiration.exclusive)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(81));

            if (_resumeCl.Laterodeviation== BasCommon_BO.EntentePrealable.en_Laterodeviation.Fonctionnel)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(82));
            if (_resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.RCetOIM)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(83));


            if (_resumeCl.LangueBasse == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(32));

            if (_resumeCl.FormeArcade == BasCommon_BO.EntentePrealable.en_FormeArcade.V)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(48));

            if (_resumeCl.Diasteme == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(74));

            #endregion
        }

        public static void GetDiagsForAlveolaire(List<CommonDiagnostic> lst)
        {
            #region DiagAlveolaire

            if ((_resumeCl.OcclusionInverse == BasCommon_BO.EntentePrealable.en_OccInverse.Droite)||
                (_resumeCl.OcclusionInverse == BasCommon_BO.EntentePrealable.en_OccInverse.Droite_Et_Gauche))
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(72));
            if ((_resumeCl.OcclusionInverse == BasCommon_BO.EntentePrealable.en_OccInverse.Gauche)||
                (_resumeCl.OcclusionInverse == BasCommon_BO.EntentePrealable.en_OccInverse.Droite_Et_Gauche))
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(71));

            //Modification Nadheeeeeeeeeeeeem
            if (_resumeCl.SautArticule == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(70));


            if (_resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.Class_I)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(20));
            if (_resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.Class_II)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(21));
            if (_resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.Class_III)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(22));

            if (_resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.Class_I)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(23));
            if (_resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.Class_II)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(24));
            if (_resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.Class_III)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(25));

            if (_resumeCl.ClasseMolG == BasCommon_BO.EntentePrealable.en_Class.Class_I)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(60));
            if (_resumeCl.ClasseMolG == BasCommon_BO.EntentePrealable.en_Class.Class_II)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(61));
            if (_resumeCl.ClasseMolG == BasCommon_BO.EntentePrealable.en_Class.Class_III)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(62));

            if (_resumeCl.ClasseCanG == BasCommon_BO.EntentePrealable.en_Class.Class_I)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(63));
            if (_resumeCl.ClasseCanG == BasCommon_BO.EntentePrealable.en_Class.Class_II)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(64));
            if (_resumeCl.ClasseCanG == BasCommon_BO.EntentePrealable.en_Class.Class_III)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(65));

            
            if (_resumeCl.DDD == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(26));

            if (_resumeCl.Diasteme == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(74));

            if (_resumeCl.DDM == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
            {

                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(27));
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(271));
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(272));
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(273));
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(274));
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(275));
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(276));
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(277));
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(278));
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(279));
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(280));
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(281));
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(282));
            }

            if (_resumeCl.OcclusionFace == BasCommon_BO.EntentePrealable.en_OccFace.Infraclusion)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(38));

            if (_resumeCl.OcclusionFace == BasCommon_BO.EntentePrealable.en_OccFace.Supraclusion)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(39));
                        


            if (_resumeCl.DiagMax == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Endoalveolie)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(40));

            if (_resumeCl.DiagMand == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Endoalveolie)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(41));

            if (_resumeCl.DiagMax == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Exoalveolie)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(42));

            if (_resumeCl.DiagMand == BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Exoalveolie)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(43));


            if (_resumeCl.SensTransvMax == BasCommon_BO.EntentePrealable.en_DiagOsseux.Endognatie)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(44));

            if (_resumeCl.SensTransvMand == BasCommon_BO.EntentePrealable.en_DiagOsseux.Endognatie)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(45));

            if (_resumeCl.SensTransvMax == BasCommon_BO.EntentePrealable.en_DiagOsseux.Exognatie)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(46));

            if (_resumeCl.SensTransvMand == BasCommon_BO.EntentePrealable.en_DiagOsseux.Exognatie)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(47));


          
            #endregion
        }

        public static void GetDiagsForAnalysePano(List<CommonDiagnostic> lst)
        {
            #region Pano
            if (_resumeCl.EvolGermesDesDents == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(19));
            #endregion
        }

        public static void GetDiagsForAnalyseTeleRadio(List<CommonDiagnostic> lst)
        {
            #region Analyse Teleradio
            if (_resumeCl.SensSagittal == BasCommon_BO.EntentePrealable.en_Class.Class_I)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(14));
            if (_resumeCl.SensSagittal == BasCommon_BO.EntentePrealable.en_Class.Class_II)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(15));
            if (_resumeCl.SensSagittal == BasCommon_BO.EntentePrealable.en_Class.Class_III)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(16));

            if (_resumeCl.SensVertical == BasCommon_BO.EntentePrealable.en_Divergence.Hyperdivergent)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(17));
            if (_resumeCl.SensVertical == BasCommon_BO.EntentePrealable.en_Divergence.Hypodivergent)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(18));
            if (_resumeCl.SPP_SPA == BasCommon_BO.EntentePrealable.en_SPP.hyperdivergence)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(283));
            if (_resumeCl.SPP_SPA == BasCommon_BO.EntentePrealable.en_SPP.hypodivergence)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(284));
            if (_resumeCl.SPP_SPA == BasCommon_BO.EntentePrealable.en_SPP.normale)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(285));
            #endregion
        }

        public static void GetDiagsForAnalyseProfil(List<CommonDiagnostic> lst)
        {
            #region analyse Profil
            if (_resumeCl.Menton == BasCommon_BO.EntentePrealable.en_ProRetro.Retro)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(6));

            if (_resumeCl.Menton == BasCommon_BO.EntentePrealable.en_ProRetro.Pro)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(7));

            if (_resumeCl.LevreInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.Retro)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(8));

            if (_resumeCl.LevreInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.Pro)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(9));

            if (_resumeCl.LevreSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.Retro)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(10));

            if (_resumeCl.LevreSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.Pro)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(11));


            if (_resumeCl.IncisiveInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.Retro)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(12));

            if (_resumeCl.IncisiveInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.Pro)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(13));

            #endregion
        }

        public static void GetDiagsForAnalyseFace(List<CommonDiagnostic> lst)
        {
            #region analyse Face
            if (_resumeCl.EtageInf == BasCommon_BO.EntentePrealable.en_EtageInf.Diminution)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(1));

            if (_resumeCl.EtageInf == BasCommon_BO.EntentePrealable.en_EtageInf.Effondrement)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(2));

            if (_resumeCl.EtageInf == BasCommon_BO.EntentePrealable.en_EtageInf.Augmentation)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(3));

            if (Math.Abs(_resumeCl.DeviationMenton) > .03)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(4));

            if (Math.Abs(_resumeCl.DeviationLevreInf) > .03)
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(5));


            if ((Math.Abs(_resumeCl.DecalageInterIncisiveBas) > 0) || (Math.Abs(_resumeCl.DecalageInterIncisiveHaut) > 0))
                lst.Add(CommonDiagnosticsMgmt.getCommonDiagnostics(49));


            #endregion
        }

        public static string Resume
        {
            get
            {

                string tmp = "";
                string _Resume = "";

                //Analyse 1
                if ((_resumeCl.EtageInf != BasCommon_BO.EntentePrealable.en_EtageInf.Normal) && (_resumeCl.EtageInf != BasCommon_BO.EntentePrealable.en_EtageInf.undefined))
                    tmp += "\tEtage Inférieur = " + _resumeCl.EtageInf.ToString() + "\r\r\n";
                if ((Math.Abs(_resumeCl.DeviationMenton) > .03))
                    tmp += "\tDéviation du menton\r\r\n";
                if ((Math.Abs(_resumeCl.DeviationLevreInf) > .03))
                    tmp += "\tDéviation de la levre inférieur\r\r\n";
                if (tmp != "")
                {
                    _Resume += "Masque Facial\r\n";
                    _Resume += tmp;
                }
                tmp = "";

                    //Analyse 2
                    if ((_resumeCl.sourireDentaire != BasCommon_BO.EntentePrealable.en_SourireDentaire.Normal) && (_resumeCl.sourireDentaire != BasCommon_BO.EntentePrealable.en_SourireDentaire.undefined))
                        tmp += "\tSourire dentaire = " + _resumeCl.sourireDentaire.ToString() + "\r\r\n";
                //if ((_resumeCl.DiagAlveolaire != BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Normoalveolie) && (_resumeCl.DiagAlveolaire != BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined))                  
                //    tmp += "\tDiagnostique alvéolaire = " + _resumeCl.DiagAlveolaire.ToString() + "\r\r\n";
                    if ((_resumeCl.TNLDroit != BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.TNL1) && (_resumeCl.TNLDroit != BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.undefined))
                        tmp += "\tTNL Droit = " + _resumeCl.TNLDroit.ToString() + "\r\r\n";
                    if ((_resumeCl.TNLGauche != BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.TNL1) && (_resumeCl.TNLGauche != BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.undefined))
                        tmp += "\tTNL Gauche = " + _resumeCl.TNLGauche.ToString() + "\r\r\n";

                

                if ((_resumeCl.DecalageInterIncisiveHaut < 0))
                        tmp += "\tdecalage inter-incisive superieur à gauche de " + (0-_resumeCl.DecalageInterIncisiveHaut).ToString() + " mm\r\r\n";

                if ((_resumeCl.DecalageInterIncisiveHaut > 0))
                    tmp += "\tdecalage inter-incisive superieur à droite de " + _resumeCl.DecalageInterIncisiveHaut.ToString() + " mm\r\r\n";

                if ((_resumeCl.DecalageInterIncisiveBas < 0))
                    tmp += "\tdecalage inter-incisive inférieur à gauche de " + (0 - _resumeCl.DecalageInterIncisiveBas).ToString() + " mm\r\r\n";

                if ((_resumeCl.DecalageInterIncisiveBas > 0))
                    tmp += "\tdecalage inter-incisive inférieur à droite de " + _resumeCl.DecalageInterIncisiveBas.ToString() + " mm\r\r\n";
                
                if (tmp != "")
                {
                    _Resume += "Sourire face\r\n";
                    _Resume += tmp;
                }
                tmp = "";
               
                
                //Analyse 3
                if (_resumeCl.ClasseMolD != BasCommon_BO.EntentePrealable.en_Class.undefined)
                    tmp += "\tClasse molaire droit = " + _resumeCl.ClasseMolD.ToString() + " \r\r\n";
                if (_resumeCl.ClasseMolG != BasCommon_BO.EntentePrealable.en_Class.undefined)
                    tmp += "\tClasse molaire gauche = " + _resumeCl.ClasseMolG.ToString() + " \r\r\n";
                if (_resumeCl.ClasseCanD != BasCommon_BO.EntentePrealable.en_Class.undefined)
                    tmp += "\tClasse cannine droit = " + _resumeCl.ClasseCanD.ToString() + " \r\r\n";
                if (_resumeCl.ClasseCanG != BasCommon_BO.EntentePrealable.en_Class.undefined)
                    tmp += "\tClasse cannine gauche = " + _resumeCl.ClasseCanG.ToString() + " \r\r\n";

                if (_resumeCl.SautArticule != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    tmp += "\tSaut d'articule = " + _resumeCl.SautArticule.ToString() + " \r\r\n";

                if ((_resumeCl.OcclusionFace != BasCommon_BO.EntentePrealable.en_OccFace.Normal)&&(_resumeCl.OcclusionFace != BasCommon_BO.EntentePrealable.en_OccFace.undefined))
                {
                    tmp += "\tOcclusion facial = " + _resumeCl.OcclusionFace.ToString() + " (" + _resumeCl.OcclusionValue.ToString() + " mm)\r\r\n";
                }

                if ((_resumeCl.OcclusionInverse != BasCommon_BO.EntentePrealable.en_OccInverse.undefined) && (_resumeCl.OcclusionInverse != BasCommon_BO.EntentePrealable.en_OccInverse.Aucun))
                    tmp += "\tArticule inverse = " + _resumeCl.OcclusionInverse.ToString() + " \r\r\n";

                if ((_resumeCl.Laterodeviation != BasCommon_BO.EntentePrealable.en_Laterodeviation.undefined) && (_resumeCl.Laterodeviation != BasCommon_BO.EntentePrealable.en_Laterodeviation.Aucun))
                {
                    if (_resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.Droite)
                        tmp += "\tLaterodeviation = Droite \r\r\n";
                    if (_resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.Gauche)
                        tmp += "\tLaterodeviation = Gauche \r\r\n";
                    if (_resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.Droite_Et_Gauche)
                        tmp += "\tLaterodeviation = Droite et gauche \r\r\n";
                }

                if (tmp != "")
                {
                    _Resume += "Analyse Occlusale\r\n";
                    _Resume += tmp;
                }
                tmp = "";
                // Analyse Fonctionnel
                //if ((ResumeCliniqueMgmt.resumeCl.InterpositonLingual != BasCommon_BO.EntentePrealable.en_InterpositionLingual.posterieur) &&
                //   (ResumeCliniqueMgmt.resumeCl.InterpositonLingual != BasCommon_BO.EntentePrealable.en_InterpositionLingual.anterieur))
                //    tmp += "\tInterposition lingual = " + ResumeCliniqueMgmt.resumeCl.InterpositonLingual.ToString() + " \r\r\n";

                if (ResumeCliniqueMgmt.resumeCl.InterpositonLingual != BasCommon_BO.EntentePrealable.en_InterpositionLingual.posterieur)
                    tmp += "\tInterposition posterieur = " + ResumeCliniqueMgmt.resumeCl.InterpositonLingual.ToString() + " \r\r\n";
                if (ResumeCliniqueMgmt.resumeCl.InterpositonLingual != BasCommon_BO.EntentePrealable.en_InterpositionLingual.anterieur)
                    tmp += "\tInterposition anterieur = " + ResumeCliniqueMgmt.resumeCl.InterpositonLingual.ToString() + " \r\r\n";

                if (ResumeCliniqueMgmt.resumeCl.FormeRespiration != BasCommon_BO.EntentePrealable.en_Respiration.buccale)
                    tmp += "\tForme respiration = " + ResumeCliniqueMgmt.resumeCl.FormeRespiration.ToString() + " \r\r\n";
                if (ResumeCliniqueMgmt.resumeCl.FormeRespiration != BasCommon_BO.EntentePrealable.en_Respiration.exclusive)
                    tmp += "\tForme respiration = " + ResumeCliniqueMgmt.resumeCl.FormeRespiration.ToString() + " \r\r\n";

                if (ResumeCliniqueMgmt.resumeCl.Laterodeviation != BasCommon_BO.EntentePrealable.en_Laterodeviation.Fonctionnel)
                    tmp += "\tForme Laterodeviation = " + ResumeCliniqueMgmt.resumeCl.Laterodeviation.ToString() + " \r\r\n";
                if (ResumeCliniqueMgmt.resumeCl.Laterodeviation != BasCommon_BO.EntentePrealable.en_Laterodeviation.RCetOIM)
                    tmp += "\tForme Laterodeviation = " + ResumeCliniqueMgmt.resumeCl.Laterodeviation.ToString() + " \r\r\n";



                if (tmp != "")
                {
                    _Resume += "Analyse Fonctionnel\r\n";
                    _Resume += tmp;
                }
                tmp = "";

                //Analyse 4
                if ((ResumeCliniqueMgmt.resumeCl.InterpositonLingual != BasCommon_BO.EntentePrealable.en_InterpositionLingual.Normal) &&
                    (ResumeCliniqueMgmt.resumeCl.InterpositonLingual != BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined))
                    tmp += "\tInterposition lingual = " + ResumeCliniqueMgmt.resumeCl.InterpositonLingual.ToString() + " \r\r\n";
                
                if (ResumeCliniqueMgmt.resumeCl.FormeArcade != BasCommon_BO.EntentePrealable.en_FormeArcade.undefined)
                    tmp += "\tForme d'arcade = " + ResumeCliniqueMgmt.resumeCl.FormeArcade.ToString() + " \r\r\n";

                if (ResumeCliniqueMgmt.resumeCl.DDM != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    tmp += "\tDDM = " + ResumeCliniqueMgmt.resumeCl.DDM.ToString() + " \r\r\n";
                if (ResumeCliniqueMgmt.resumeCl.DDD != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    tmp += "\tDDD = " + ResumeCliniqueMgmt.resumeCl.DDD.ToString() + " \r\r\n";
                if (ResumeCliniqueMgmt.resumeCl.Diasteme != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    tmp += "\tDiasteme = " + ResumeCliniqueMgmt.resumeCl.Diasteme.ToString() + " \r\r\n";

                if (ResumeCliniqueMgmt.resumeCl.SurplombValue != 0)
                    tmp += "\tSurplomb = " + ResumeCliniqueMgmt.resumeCl.SurplombValue.ToString() + " mm\r\r\n";
                if (ResumeCliniqueMgmt.resumeCl.LangueBasse != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    tmp += "\tLangue basse = " + ResumeCliniqueMgmt.resumeCl.LangueBasse.ToString() + " \r\r\n";
                if (ResumeCliniqueMgmt.resumeCl.FreinLabial != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    tmp += "\tFrein Labial = " + ResumeCliniqueMgmt.resumeCl.FreinLabial.ToString() + " \r\r\n";
                if (ResumeCliniqueMgmt.resumeCl.FreinLingual != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    tmp += "\tFrein Lingual = " + ResumeCliniqueMgmt.resumeCl.FreinLingual.ToString() + " \r\r\n";

                if (tmp != "")
                {
                    _Resume += "Analyse des arcades\r\n";
                    _Resume += tmp;
                }
                tmp = "";

                //Analyse 5

                if ((ResumeCliniqueMgmt.resumeCl.SourireGingivalSup == BasCommon_BO.EntentePrealable.en_OuiNon.Oui))
                    tmp += "\tSourire Gingival Sup(" + ResumeCliniqueMgmt.resumeCl.GingivalSupValue .ToString()+ "mm)\r\r\n";
                if ((ResumeCliniqueMgmt.resumeCl.SourireGingivalInf == BasCommon_BO.EntentePrealable.en_OuiNon.Oui))
                    tmp += "\tSourire Gingival Inf(" + ResumeCliniqueMgmt.resumeCl.GingivalInfValue.ToString() + "mm)\r\r\n";
                if ((ResumeCliniqueMgmt.resumeCl.SourireLabial == BasCommon_BO.EntentePrealable.en_OuiNon.Oui))
                    tmp += "\tSourire Labial(" + ResumeCliniqueMgmt.resumeCl.LabialValue.ToString() + "mm)\r\r\n";

                if (tmp != "")
                {
                    _Resume += "Analyse du sourire\r\n";
                    _Resume += tmp;
                }
                tmp = "";
                //Analyse 6

                if ((ResumeCliniqueMgmt.resumeCl.LevreSuperieur != BasCommon_BO.EntentePrealable.en_ProRetro.Normo)&&(ResumeCliniqueMgmt.resumeCl.LevreSuperieur != BasCommon_BO.EntentePrealable.en_ProRetro.undefined))
                    tmp += "\tLevre Supérieur : " + ResumeCliniqueMgmt.resumeCl.LevreSuperieur.ToString() + "\r\r\n";
                if ((ResumeCliniqueMgmt.resumeCl.LevreInferieur != BasCommon_BO.EntentePrealable.en_ProRetro.Normo)&&(ResumeCliniqueMgmt.resumeCl.LevreInferieur != BasCommon_BO.EntentePrealable.en_ProRetro.undefined))
                    tmp += "\tLevre Inférieur : " + ResumeCliniqueMgmt.resumeCl.LevreInferieur.ToString() + "\r\r\n";
                if ((ResumeCliniqueMgmt.resumeCl.Menton != BasCommon_BO.EntentePrealable.en_ProRetro.Normo)&&(ResumeCliniqueMgmt.resumeCl.Menton != BasCommon_BO.EntentePrealable.en_ProRetro.undefined))
                    tmp += "\tMenton : " + ResumeCliniqueMgmt.resumeCl.Menton.ToString() + "\r\r\n";


                if ((ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur != BasCommon_BO.EntentePrealable.en_ProRetro.Normo)&&(ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur != BasCommon_BO.EntentePrealable.en_ProRetro.undefined))
                    tmp += "\tIncisive Supérieur : " + ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur.ToString() + "\r\r\n";
                
                if (tmp != "")
                {
                    _Resume += "Analyse du profil\r\n";
                    _Resume += tmp;
                }
                tmp = "";
                //Analyse 7

                if (ResumeCliniqueMgmt.resumeCl.SensSagittal != BasCommon_BO.EntentePrealable.en_Class.undefined)
                    tmp += "\tSens Sagittal : " + ResumeCliniqueMgmt.resumeCl.SensSagittal.ToString() + "\r\r\n";
                if ((ResumeCliniqueMgmt.resumeCl.SensSagittalMandBasal != BasCommon_BO.EntentePrealable.en_ProRetro.Normo) && (ResumeCliniqueMgmt.resumeCl.SensSagittalMandBasal != BasCommon_BO.EntentePrealable.en_ProRetro.undefined))
                    tmp += "\tAnomalie basal Sens Sagittal mandibulaire: " + ResumeCliniqueMgmt.resumeCl.SensSagittalMandBasal.ToString() + "\r\r\n";
                if ((ResumeCliniqueMgmt.resumeCl.SensSagittalMaxBasal != BasCommon_BO.EntentePrealable.en_ProRetro.Normo) && (ResumeCliniqueMgmt.resumeCl.SensSagittalMaxBasal != BasCommon_BO.EntentePrealable.en_ProRetro.undefined))
                    tmp += "\tAnomalie basal Sens Sagittal maxilaire : " + ResumeCliniqueMgmt.resumeCl.SensSagittalMaxBasal.ToString() + "\r\r\n";
                if ((ResumeCliniqueMgmt.resumeCl.SensVertical != BasCommon_BO.EntentePrealable.en_Divergence.Normodivergent) && (ResumeCliniqueMgmt.resumeCl.SensVertical != BasCommon_BO.EntentePrealable.en_Divergence.undefined))
                    tmp += "\tSens Vertical : " + ResumeCliniqueMgmt.resumeCl.SensVertical.ToString() + "\r\r\n";

                //Incisive Superieur déja pris en compte ci dessus
                /*
                if (ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur != BasCommon_BO.EntentePrealable.en_ProRetro.Normo)
                    tmp += "\tIncisive Supérieur : " + ResumeCliniqueMgmt.resumeCl.IncisiveSuperieur.ToString() + "\r\r\n";*/

                if ((ResumeCliniqueMgmt.resumeCl.IncisiveInferieur != BasCommon_BO.EntentePrealable.en_ProRetro.Normo)&&(ResumeCliniqueMgmt.resumeCl.IncisiveInferieur != BasCommon_BO.EntentePrealable.en_ProRetro.undefined))
                    tmp += "\tIncisive Inférieur : " + ResumeCliniqueMgmt.resumeCl.IncisiveInferieur.ToString() + "\r\r\n";

                if (tmp != "")
                {
                    _Resume += "Analyse Radio\r\n";
                    _Resume += tmp;
                }
                tmp = "";


                //Analyse 8

                if (ResumeCliniqueMgmt.resumeCl.EvolGermesDesDents != BasCommon_BO.EntentePrealable.en_OuiNon.undefined)
                    tmp += "\tEvolution des germes des dents : " + ResumeCliniqueMgmt.resumeCl.EvolGermesDesDentsSur + "\r\r\n";
                if (ResumeCliniqueMgmt.resumeCl.Agenesie != "")
                    tmp += "\tAgénésie sur : " + ResumeCliniqueMgmt.resumeCl.Agenesie + "\r\r\n";
                if (ResumeCliniqueMgmt.resumeCl.Controle != "")
                    tmp += "\tControle sur : " + ResumeCliniqueMgmt.resumeCl.Controle + "\r\r\n";
                if (ResumeCliniqueMgmt.resumeCl.DentsIncluses != "")
                    tmp += "\tDents incluses sur : " + ResumeCliniqueMgmt.resumeCl.DentsIncluses + "\r\r\n";
                if (ResumeCliniqueMgmt.resumeCl.DentsDeSagesse != "")
                    tmp += "\tDents à extraires : " + ResumeCliniqueMgmt.resumeCl.DentsDeSagesse + "\r\r\n";

                if (ResumeCliniqueMgmt.resumeCl.NoTaquets != "")
                    tmp += "\tPas de Taquets sur : " + ResumeCliniqueMgmt.resumeCl.NoTaquets + "\r\r\n";

                if (ResumeCliniqueMgmt.resumeCl.NoTaquets != "")
                    tmp += "\tPas de restriction de mvmts sur : " + ResumeCliniqueMgmt.resumeCl.NoMvts + "\r\r\n";
                
                if (tmp != "")
                {
                    _Resume += "Analyse Panoramique\r\n";
                    _Resume += tmp;
                }
                tmp = "";
               
                return _Resume;
            }
        }

        private static ResumeClinique _resumeCl = new ResumeClinique();
        public static ResumeClinique resumeCl
        {
            get
            {
                return _resumeCl;
            }
            set
            {
                _resumeCl = value;
            }
        }


        public static void ConvertFromAnalyse1(float EtageSup, 
            float EtageMoy, 
            float EtageInf, 
            float EtageInfSup,
            float EtageInfInf,
            float DeviationLevreInf,
            float DeviationMenton
            )
        {
            if (Math.Abs(EtageInf - EtageSup) < 0.05) //moins de 5% de marge
                resumeCl.EtageInf = BasCommon_BO.EntentePrealable.en_EtageInf.Normal;
            else
                if (EtageSup > EtageInf)
                {
                    if (EtageInf < 0.2)
                        resumeCl.EtageInf = BasCommon_BO.EntentePrealable.en_EtageInf.Effondrement;
                    else
                        resumeCl.EtageInf = BasCommon_BO.EntentePrealable.en_EtageInf.Diminution;
                }
                else
                    resumeCl.EtageInf = BasCommon_BO.EntentePrealable.en_EtageInf.Augmentation;

            resumeCl.DeviationMenton = DeviationMenton;
            resumeCl.DeviationLevreInf = DeviationLevreInf;
        }


        public static string GenerateObjectifs()
        {
            string res = "";

            foreach (CommonObjectif cd in resumeCl.patient.SelectedObjectifs)
            {

                if (res != "") res += "\n";
                res += cd.Libelle;
                     
            }
            

            return res;

        }

        public static string GenerateCompteRenduClinique()
        {
            string tmp = "";
            string tmpf = "";
            string tmpp = "";
            string res = "";

            // - de face
            tmpf = DiagVisageFace(false);
            // - de profil
            tmpp = DiagVisageProfile(false);

            if ((tmpp != "") || (tmpf != ""))
                res += "Au niveau du visage :";

            if (tmpf != "")
            {
                res += "\n\t de face";
                res += "\n" + tmpf;
            }           
            

            if (tmpp != "")
            {
                res += "\n\t de profil";
                res += "\n" + tmpp;
            }

            //Au niveau alveolodentaire
            tmp = DiagAlveoloDentaire(false);
            if (tmp != "")
            {
                res += "\n- Au niveau alveolodentaire";
                res += tmp;
            }

            //Au niveau fonctionnel
            tmp= DiagFonctionnel(false);
            if (tmp != "")
            {
                res += "\n- Au niveau fonctionnel";
                res += "\n" + tmp;
            }
            //Au niveau teleradiographique
            tmp = DiagTeleradio(false);

            if (tmp != "")
            {
                res += "\n- Au niveau teleradiographique";
                res += "\n" + tmp;
            }

            //Au niveau panoramique
            tmp = DiagPano(false);

            if (tmp != "")
            {
                res += "\n- Au niveau panoramique";
                res += "\n" + tmp;
            }

           


            //Au niveau Esthetique du sourire
            tmp = DiagEsthetique(false);
            if (tmp != "")
            {
                res += "\n- Au niveau de l'esthetique du sourire";
                res += "\n" + tmp;
            }

            return res;

        }


        public static string GeneratePropositionDevis(basePatient patient)
        {
            string res = "";

            bool hasoptions = false;

            SortedDictionary<int, List<CommonObjectifFromDiag>> NbDevis = new SortedDictionary<int, List<CommonObjectifFromDiag>>();
            SortedDictionary<int, List<CommonObjectifFromDiag>> NbOptions = new SortedDictionary<int, List<CommonObjectifFromDiag>>();

            foreach(CommonObjectifFromDiag obj in CommonDiagnosticsMgmt.getCommonObjectifs(resumeCl.diagnostics))
            {

                if (obj.DiagCanceled) 
                    continue;
                if (patient.SelectedObjectifs.Contains(obj.objectif))
                {
                    if (obj.NumDevis > 0)
                    {
                        if (!NbDevis.ContainsKey(obj.NumDevis))
                            NbDevis.Add(obj.NumDevis, new List<CommonObjectifFromDiag>());
                        NbDevis[obj.NumDevis].Add(obj);
                    }

                    if (obj.NumOption > 0)
                    {
                        hasoptions = true;
                        if (!NbOptions.ContainsKey(obj.NumOption))
                            NbOptions.Add(obj.NumOption, new List<CommonObjectifFromDiag>());
                        NbOptions[obj.NumOption].Add(obj);
                    }
                }
            }
            
            
            foreach (KeyValuePair<int, List<CommonObjectifFromDiag>> kv in NbDevis)
            {
                res += "\n\n";

                List<string> lstdiag = new List<string>();
                foreach (CommonObjectifFromDiag cod in kv.Value)
                    if (!lstdiag.Contains(cod.diagnostic.Libelle)) 
                        lstdiag.Add(cod.diagnostic.Libelle);

                string tocoerrect = "Afin de corriger : ";
                foreach (string s in lstdiag)
                    tocoerrect += "\n\t-" + s;

                if (kv.Key == 1)
                    res += "Solution 1 :\n\n" + tocoerrect + @"
Nous proposons un traitement simplifié d'alignement des dents antérieurs, pour améliorer le contexte esthétique et parodontale sans corriger la malocclusion qui n'est possible que dans les solution 3 et 4.";
                if (kv.Key == 2)
                    res += "Solution 2 :\n\n" + tocoerrect + @"
Nous proposons d'aligner toutes les dents avec une legere expansion pour améliorer le sourire dans sa globalité.
Le patient est informé que ce traitement ne corrige pas la malocclusion ou les dismorphoses et améliore l'esthetique et la santé dentaire.";
                if (kv.Key == 3)
                    res += "Solution 3 :\n\n" + tocoerrect + @"
Nous proposons, en plus de l'alignement des dents, la correction de la mauvaise occlusion.";
                if (kv.Key == 4)
                    res += "Solution 4 :\n\n" + tocoerrect + @"
Nous proposons de repositionner les os maxillaires et mandibulaires pour réharmoniser et rééquilibrer l'architecture osseuse du visage.";
                

                


                //switch (kv.Key)
                //{
                //    case 1: res += "\n" + "Nous vous proposons la solution 1 (Alignement des dents antérieurs)"; break;
                //    case 2: res += "\n" + "Nous vous proposons la solution 2 (Alignement de toutes les dents)"; break;
                //    case 3: res += "\n" + "Nous vous proposons la solution 3 (Orthodontie occlusal des dents)"; break;
                //    case 4: res += "\n" + "Nous vous proposons la solution 4 (Orthodontie et chirurgie orthognatique)"; break;
                //}

            }


            if (hasoptions)
            {
                res += "\n\n" + "Nous vous proposons également : ";

                foreach (KeyValuePair<int, List<CommonObjectifFromDiag>> kv in NbOptions)
                {
                    if (!string.IsNullOrEmpty(res)) res += "\n";
                    res += "\tl'option numéro " + kv.Key.ToString();
                    res += " afin de corriger : ";

                    List<string> lstdiag = new List<string>();
                    foreach (CommonObjectifFromDiag cod in kv.Value)
                        if (!lstdiag.Contains(cod.diagnostic.Libelle))
                            lstdiag.Add(cod.diagnostic.Libelle);

                    foreach (string s in lstdiag)
                        res += "\n\t\t-" + s;
                }
            }

            return res;

        }




        public static string DiagEsthetique(bool ameliorer)
        {
            string Esthetique = "";
            if (_resumeCl.sourireDentaire == BasCommon_BO.EntentePrealable.en_SourireDentaire.Etroit)
                Esthetique = ameliorer ? "\nAméliorer le sourire étroit" : "\t\tUn sourire étroit";
            if (_resumeCl.sourireDentaire == BasCommon_BO.EntentePrealable.en_SourireDentaire.Large)
                Esthetique = ameliorer ? "\nAméliorer le sourire large" : "\t\tUn sourire large";

            if (_resumeCl.SourireGingivalInf == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
            {
                if (Esthetique != "") Esthetique = "\n";
                Esthetique += ameliorer ? "\nAméliorer le sourire gingival inférieur" : "\t\tUn sourire gingival inférieur";
            }
            if (_resumeCl.SourireGingivalSup == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
            {
                if (Esthetique != "") Esthetique = "\n";
                Esthetique = ameliorer ? "\nAméliorer le sourire gingival supérieur" : "\t\tUn sourire gingival supérieur";
            }
            if (_resumeCl.SourireLabial == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
            {
                if (Esthetique != "") Esthetique = "\n";
                Esthetique = ameliorer ? "\nAméliorer le sourire labial" : "\t\tUn sourire labial";
            }

            return Esthetique;
        }

        public static string DiagFonctionnel(bool ameliorer)
        {
            string fonctionnel = "";
            if (_resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.anterieur)
                fonctionnel += ameliorer ? "\nAméliorer l'interposition linguale antérieure" : "\t\tUne interposition linguale antérieure" + "\r\r\n";
            if (_resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.posterieur)
                fonctionnel += ameliorer ? "\nAméliorer l'interposition linguale postérieur" : "\t\tUne interposition linguale postérieur" + "\r\r\n";
            if (_resumeCl.InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.AnterieurEtPosterieur)
                fonctionnel += ameliorer ? "\nAméliorer l'interposition linguale antéro-postérieure" : "\t\tUne interposition linguale antéro-postérieure" + "\r\r\n";
            //if ((Math.Abs(_resumeCl.LabialValue) > 0) && (_resumeCl.InterpositonLingual != BasCommon_BO.EntentePrealable.en_InterpositionLingual.Normal))
            //    fonctionnel += ameliorer ? "\nAméliorer l'interposition linguale et labiale" : "\t\tUne interposition linguale et labiale" + "\r\r\n";
            if (_resumeCl.FormeRespiration == BasCommon_BO.EntentePrealable.en_Respiration.buccale)
                fonctionnel += ameliorer ? "\nAméliorer la respiration" : "\t\tUne respiration à tendance buccale" + "\r\r\n";
            if (_resumeCl.FormeRespiration == BasCommon_BO.EntentePrealable.en_Respiration.exclusive)
                fonctionnel += ameliorer ? "\nAméliorer la respiration" : "\t\tUne respiration buccale exclusive" + "\r\r\n";

            if (_resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.Fonctionnel)
                fonctionnel += ameliorer ? "\nAméliorer la  laterodeviation fonctionnet" : "\t\tLaterodeviation Fonctionnel" + "\r\r\n";
            if (_resumeCl.Laterodeviation == BasCommon_BO.EntentePrealable.en_Laterodeviation.RCetOIM)
                fonctionnel += ameliorer ? "\nAméliorer la  laterodeviation fonctionnet" : "\t\tNon correspondance entre realtion centréé(RC) et intercuspidation maximale(OIM)" + "\r\r\n";
         

            if (_resumeCl.LangueBasse == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
                fonctionnel += ameliorer ? "\nAméliorer la langue basse" : "\n\t\tUne langue basse";

            return fonctionnel;
        }

        public static string DiagAlveoloDentaire(bool ameliorer)
        {
            string alveolodentaire = "";



            //  classe canine droite
            if (_resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.Class_I)
            {
                alveolodentaire += ameliorer ? "" : "\n\t\tUne classe dentaire canine I  droite";
            }
            else if (_resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.Class_II)
            {
                alveolodentaire += ameliorer ? "" : "\n\t\tUne classe dentaire canine II droite";

            }
            else if (_resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.Class_III)
            {
                alveolodentaire += ameliorer ? "" : "\n\t\tUne classe dentaire canine III droite";
            }
            // classe canine gauche
            if (_resumeCl.ClasseCanG == BasCommon_BO.EntentePrealable.en_Class.Class_I)
            {
                alveolodentaire += ameliorer ? "" : "\n\t\tUne classe dentaire canine I  gauche";
            }
            else if (_resumeCl.ClasseCanG == BasCommon_BO.EntentePrealable.en_Class.Class_II)
            {
                alveolodentaire += ameliorer ? "" : "\n\t\tUne classe dentaire canine II gauche";

            }
            else if (_resumeCl.ClasseCanG == BasCommon_BO.EntentePrealable.en_Class.Class_III)
            {
                alveolodentaire += ameliorer ? "" : "\n\t\tUne classe dentaire canine III gauche";
            }
            // classe molaire droite

            if (_resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.Class_I)
            {
                alveolodentaire += ameliorer ? "" : "\n\t\tUne classe dentaire molaire I droite";

            }
            else if (_resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.Class_II)
            {
                alveolodentaire += ameliorer ? "" : "\n\t\tUne classe dentaire molaire II droite";

            }
            else if (_resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.Class_III)
            {
                alveolodentaire += ameliorer ? "" : "\n\t\tUne classe dentaire molaire III droite";

            }

            // classe molaire gauche
            if (_resumeCl.ClasseMolG == BasCommon_BO.EntentePrealable.en_Class.Class_I)
            {
                alveolodentaire += ameliorer ? "" : "\n\t\tUne classe dentaire molaire I  gauche";
            }
            else if (_resumeCl.ClasseMolG == BasCommon_BO.EntentePrealable.en_Class.Class_II)
            {
                alveolodentaire += ameliorer ? "" : "\n\t\tUne classe dentaire molaire II gauche";

            }
            else if (_resumeCl.ClasseMolG == BasCommon_BO.EntentePrealable.en_Class.Class_III)
            {
                alveolodentaire += ameliorer ? "" : "\n\t\tUne classe dentaire molaire III gauche";
            }

            //
            //




            if (_resumeCl.SautArticule == EntentePrealable.en_OuiNon.Oui)
            {
                if (alveolodentaire == "") alveolodentaire = "\t\t";
                alveolodentaire += ameliorer ? "\nAméliorer le saut d'articule" : "\n\t\tUn Saut d'articule ";
            }


            if (_resumeCl.OcclusionInverse != EntentePrealable.en_OccInverse.Aucun)
            {
                if (alveolodentaire == "") alveolodentaire = "\t\t";

                if (ameliorer)
                {
                    if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Droite)
                        alveolodentaire += "\nAméliorer l'articule inverse droit";
                    if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Gauche)
                        alveolodentaire += "\nAméliorer l'articule inverse gauche";
                    if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche)
                        alveolodentaire += "\nAméliorer l'articule inverse ";
                    if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Anterieur)
                        alveolodentaire += "\nAméliorer l'articule inverse anterieur";
                    

                }
                else
                {
                    if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Droite)
                        alveolodentaire += "\n\t\tUn inversé d'articulé droit";
                    if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Gauche)
                        alveolodentaire += "\n\t\tUn inversé d'articulé  gauche";
                    if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche)
                        alveolodentaire += "\n\t\tUn inversé d'articulé  droit et gauche";
                    if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Anterieur)
                        alveolodentaire += "\n\t\tUn inversé d'articulé  anterieur";
                }
            }
            if (resumeCl.OcclusionFace != EntentePrealable.en_OccFace.undefined)
            {
                if (_resumeCl.OcclusionFace == EntentePrealable.en_OccFace.Supraclusion)
                    alveolodentaire += "\n\t\tUne supraclusion";
                if (_resumeCl.OcclusionFace == EntentePrealable.en_OccFace.Infraclusion)
                    alveolodentaire += "\n\t\tUne infraclusion";

            }
            if (_resumeCl.SensTransvMax == BasCommon_BO.EntentePrealable.en_DiagOsseux.Endognatie)
            {
                alveolodentaire += "\n\t\tUne endognatie maxillaire\n";
            }

            else if (_resumeCl.SensTransvMand == BasCommon_BO.EntentePrealable.en_DiagOsseux.Endognatie)
            {
                alveolodentaire += "\n\t\tUne endognatie mandibulaire\n";

            }
            if (_resumeCl.DDD == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
            {

                alveolodentaire += ameliorer ? "\n\t\t Améliorer la DDD" : "\n\t\tDDD ";
            }
            if (_resumeCl.DDM == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
            {
                alveolodentaire += ameliorer ? "\n\t\t Améliorer la DDM" : "\n\t\tDDM ";
            }
            


            //if (((_resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.Class_I) && (_resumeCl.ClasseMolG != BasCommon_BO.EntentePrealable.en_Class.Class_I)) ||
            //    ((_resumeCl.ClasseMolD != BasCommon_BO.EntentePrealable.en_Class.Class_I) && (_resumeCl.ClasseMolG == BasCommon_BO.EntentePrealable.en_Class.Class_I)))
            //{
            //    alveolodentaire += ameliorer ? "" : "\n\t\tUne classe dentaire molaire II  (subdivision gauche/dte)";
            //}
            //else
            //{
            //    if (_resumeCl.ClasseMolD == _resumeCl.ClasseMolG)
            //    {
            //        if (_resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.Class_I)
            //            alveolodentaire += ameliorer ? "\nAméliorer la classe I molaire" : "\n\t\tUne classe dentaire molaire I ";
            //        if (_resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.Class_II)
            //            alveolodentaire += ameliorer ? "\nAméliorer la classe II molaire" : "\n\t\tUne classe dentaire molaire II ";
            //        if (_resumeCl.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.Class_III)
            //            alveolodentaire += ameliorer ? "\nAméliorer la classe III molaire" : "\n\t\tUne classe dentaire molaire III ";
            //    }

            //}
            //if ((Math.Abs(_resumeCl.LabialValue) > 0) && (_resumeCl.InterpositonLingual != BasCommon_BO.EntentePrealable.en_InterpositionLingual.Normal))
            //    alveolodentaire += ameliorer ? "\nAméliorer l'interposition linguale et labiale" : "\t\tUne interposition linguale et labiale" + "\r\r\n";
            //if (((_resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.Class_I) && (_resumeCl.ClasseCanG != BasCommon_BO.EntentePrealable.en_Class.Class_I)) ||
            //    ((_resumeCl.ClasseCanD != BasCommon_BO.EntentePrealable.en_Class.Class_I) && (_resumeCl.ClasseCanG == BasCommon_BO.EntentePrealable.en_Class.Class_I)))
            //{
            //    alveolodentaire += ameliorer ? "" : "\n\t\tUne classe dentaire Canine II  (subdivision gauche/dte)";
            //}
            //else
            //{
            //    if (_resumeCl.ClasseCanD == _resumeCl.ClasseCanG)
            //    {
            //        if (_resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.Class_I)
            //            alveolodentaire += ameliorer ? "\nAméliorer la classe I Canine" : "\n\t\tUne classe dentaire Canine I ";
            //        if (_resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.Class_II)
            //            alveolodentaire += ameliorer ? "\nAméliorer la classe II Canine" : "\n\t\tUne classe dentaire Canine II ";
            //        if (_resumeCl.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.Class_III)
            //            alveolodentaire += ameliorer ? "\nAméliorer la classe III Canine" : "\n\t\tUne classe dentaire Canine III ";
            //    }

            //}
            //if (_resumeCl.DDD == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
            //{
            //    if (alveolodentaire == "") alveolodentaire = "\t\t";
            //    alveolodentaire += ameliorer ? "\nAméliorer la DDD" : "\n\t\tUne DDD ";
            //}

            //if (_resumeCl.Diasteme  == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
            //{
            //    if (alveolodentaire == "") alveolodentaire = "\t\t";
            //    alveolodentaire += ameliorer ? "\nRéduire la Diasteme" : "\n\t\tUn Diasteme ";
            //}

           

            //if (_resumeCl.DDM == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
            //{
            //    if (alveolodentaire == "") alveolodentaire = "\t\t";
            //    alveolodentaire += ameliorer ? "\nAméliorer la DDM" : "\n\t\tUne DDM ";

            //}

            //if (_resumeCl.SautArticule == EntentePrealable.en_OuiNon.Oui)
            //{
            //    if (alveolodentaire == "") alveolodentaire = "\t\t";
            //    alveolodentaire += ameliorer ? "\nAméliorer le saut d'articule" : "\n\t\tUn Saut d'articule ";
            //}

            //if (_resumeCl.OcclusionInverse != EntentePrealable.en_OccInverse.Aucun)
            //{
            //    if (alveolodentaire == "") alveolodentaire = "\t\t";

            //    if (ameliorer)
            //    {
            //        if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Droite)
            //            alveolodentaire += "\nAméliorer l'articule inverse droit";
            //        if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Gauche)
            //            alveolodentaire += "\nAméliorer l'articule inverse gauche";
            //        if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche)
            //            alveolodentaire += "\nAméliorer l'articule inverse ";
            //        if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Anterieur)
            //            alveolodentaire += "\nAméliorer l'articule inverse anterieur";

            //    }else
            //    {
            //        if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Droite)
            //            alveolodentaire += "\n\t\tUn articule inverse droit";
            //        if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Gauche)
            //            alveolodentaire += "\n\t\tUn articule inverse gauche";
            //        if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Droite_Et_Gauche)
            //            alveolodentaire += "\n\t\tUn articule inverse droite et gauche";
            //        if (_resumeCl.OcclusionInverse == EntentePrealable.en_OccInverse.Anterieur)
            //            alveolodentaire += "\n\t\tUn articule inverse anterieur";
            //    }
            //}
            //if (resumeCl.OcclusionFace != EntentePrealable.en_OccFace.undefined)
            //{
            //    if (_resumeCl.OcclusionFace == EntentePrealable.en_OccFace.Supraclusion)
            //        alveolodentaire += "\n\t\tSupraclusion";
            //    if (_resumeCl.OcclusionFace == EntentePrealable.en_OccFace.Infraclusion)
            //        alveolodentaire += "\n\t\tInfraclusion";
               
            //}
            return alveolodentaire;
        }

        public static string DiagPano(bool ameliorer)
        {
            string panoramique = "";
            if (_resumeCl.EvolGermesDesDents == BasCommon_BO.EntentePrealable.en_OuiNon.Oui)
            {
                if (ameliorer)
                {
                    panoramique = "\nAméliorer le manque de place pour l'évolution des germes (" + _resumeCl.EvolGermesDesDentsSur + ")";
                }
                else
                {

                    panoramique += "\t\tUn manque de place pour l'évolution des germes ";
                    panoramique += _resumeCl.EvolGermesDesDentsSur == "" ? "" : "(" + _resumeCl.EvolGermesDesDentsSur + ")";
                }
            }
            return panoramique;
        }

        public static string DiagTeleradio(bool ameliorer)
        {
            string Teleradiographie = "";
            if (_resumeCl.SensSagittal == BasCommon_BO.EntentePrealable.en_Class.Class_I)
                Teleradiographie += ameliorer ? "\nAméliorer la classe I" : "\t\tUne classe I ";
            if (_resumeCl.SensSagittal == BasCommon_BO.EntentePrealable.en_Class.Class_II)
                Teleradiographie += ameliorer ? "\nAméliorer la classe II" : "\t\tUne classe II ";
            if (_resumeCl.SensSagittal == BasCommon_BO.EntentePrealable.en_Class.Class_III)
                Teleradiographie += ameliorer ? "\nAméliorer la classe III" : "\t\tUne classe III ";

            if (_resumeCl.SensVertical == BasCommon_BO.EntentePrealable.en_Divergence.Hyperdivergent)
                Teleradiographie += ameliorer ? "\nAméliorer l'hyperdivergence" : "hyperdivergente ";
            if (_resumeCl.SensVertical == BasCommon_BO.EntentePrealable.en_Divergence.Hypodivergent)
                Teleradiographie += ameliorer ? "\nAméliorer l'hypodivergence" : "hypodivergente ";


            if (_resumeCl.SPP_SPA == BasCommon_BO.EntentePrealable.en_SPP.hyperdivergence)
                Teleradiographie += "\n\t\tUne typologie hyperdivergente ";
            if (_resumeCl.SPP_SPA == BasCommon_BO.EntentePrealable.en_SPP.hypodivergence)
                Teleradiographie += "\n\t\tUne typologie hypodivergente ";
            if (_resumeCl.SPP_SPA == BasCommon_BO.EntentePrealable.en_SPP.normale)
                Teleradiographie +=  "\n\t\tUne typologie normodivergente ";


            return Teleradiographie;
        }
        public static string DiagSPP(bool ameliorer)
        {
            string SPP = "";
              if (_resumeCl.SPP_SPA == BasCommon_BO.EntentePrealable.en_SPP.hypodivergence)
                  SPP += ameliorer ? "\n" : "hypodivergence ";
              if (_resumeCl.SPP_SPA == BasCommon_BO.EntentePrealable.en_SPP.hypodivergence)
                SPP += ameliorer ? "\n" : "hypodivergence ";

            return SPP;
        }
        public static string DiagVisageProfile(bool ameliorer)
        {
            string visageProfil = "";
            if (_resumeCl.Menton == BasCommon_BO.EntentePrealable.en_ProRetro.Retro)
            {
                if (ameliorer) visageProfil += "\nAméliorer la rétroposition du menton"; 
                else
                {
                    visageProfil += visageProfil == "" ? "\t\t" : "\n\t\t";
                    visageProfil += "Une rétroposition du menton";
                }
            }
            if (_resumeCl.Menton == BasCommon_BO.EntentePrealable.en_ProRetro.Pro)
            {
                if (ameliorer) visageProfil += "\nAméliorer la proversion du menton";
                else
                {
                    visageProfil += visageProfil == "" ? "\t\t" : "\n\t\t";
                    visageProfil += "Une proversion du menton";
                }
            }

            if (_resumeCl.LevreInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.Retro)
            {
                if (ameliorer) visageProfil += "\nAméliorer la rétroposition de la lèvre inférieure";
                else
                {
                    visageProfil += visageProfil == "" ? "\t\t" : "\n\t\t";
                    visageProfil += "Une Retroposition de la lèvre inférieure";
                }
            }
            if (_resumeCl.LevreInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.Pro)
            {
                if (ameliorer) visageProfil += "\nAméliorer la proversion de la lèvre inférieure";
                else
                {
                    visageProfil += visageProfil == "" ? "\t\t" : "\n\t\t";
                    visageProfil += "Une proversion de la lèvre inférieure";
                }
            }

            if (_resumeCl.LevreSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.Retro)
            {
                if (ameliorer) visageProfil += "\nAméliorer la rétroposition de la lèvre supérieure";
                else
                {
                    visageProfil += visageProfil == "" ? "\t\t" : "\n\t\t";
                    visageProfil += "Une rétroposition de la lèvre supérieure";
                }
            }
            if (_resumeCl.LevreSuperieur == BasCommon_BO.EntentePrealable.en_ProRetro.Pro)
            {
                if (ameliorer) visageProfil += "\nAméliorer la proversion de la lèvre supérieure";
                else
                {
                    visageProfil += visageProfil == "" ? "\t\t" : "\n\t\t";
                    visageProfil += "Une proversion de la lèvre supérieure";
                }
            }


            if (_resumeCl.IncisiveInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.Retro)
            {
                if (ameliorer) visageProfil += "\nAméliorer la rétroposition de l'incisive inférieure";
                else
                {
                    visageProfil += visageProfil == "" ? "\t\t" : "\n\t\t";
                    visageProfil += "Une Retroposition de l'incisive inférieure";
                }
            }
            if (_resumeCl.IncisiveInferieur == BasCommon_BO.EntentePrealable.en_ProRetro.Pro)
            {
                if (ameliorer) visageProfil += "\nAméliorer la proversion de l'incisive inférieure";
                else
                {
                    visageProfil += visageProfil == "" ? "\t\t" : "\n\t\t";
                    visageProfil += "Une proversion de l'incisive inférieure";
                }
            }

            return visageProfil;

        }

        public static string DiagVisageFace(bool ameliorer)
        {
            string visageFace = "";
            if (_resumeCl.EtageInf == BasCommon_BO.EntentePrealable.en_EtageInf.Diminution)
            {
                if (ameliorer) visageFace += "\nAméliorer la diminution de l'étage inférieur";
                else
                {
                    visageFace += visageFace == "" ? "\t\t" : "\n\t\t";
                    visageFace += "Une diminution de l'étage inférieur";
                }
            }
            if (_resumeCl.EtageInf == BasCommon_BO.EntentePrealable.en_EtageInf.Effondrement)
            {
                if (ameliorer) visageFace += "\nAméliorer l'effondrement de l'étage inférieur";
                else
                {
                    visageFace += visageFace == "" ? "\t\t" : "\n\t\t";
                    visageFace += "Un effondrement de l'étage inférieur";
                }
            }
            if (_resumeCl.EtageInf == BasCommon_BO.EntentePrealable.en_EtageInf.Augmentation)
            {
                if (ameliorer) visageFace += "\nAméliorer l'augmentation de l'étage inférieur";
                else
                {
                    visageFace += visageFace == "" ? "\t\t" : "\n\t\t";
                    visageFace += "Une augmentation de l'étage inférieur";
                }
            }
            if (Math.Abs(_resumeCl.DeviationMenton) > .03)
            {
                if (ameliorer) visageFace += "\nAméliorer la déviation du menton";
                else
                {
                    visageFace += visageFace == "" ? "\t\t" : "\n\t\t";
                    visageFace += "Une déviation du menton";
                }
            }
            if (Math.Abs(_resumeCl.DeviationLevreInf) > .03)
            {
                if (ameliorer) visageFace += "\nAméliorer la déviation de la lèvre inférieure";
                else
                {
                    visageFace += visageFace == "" ? "\t\t" : "\n\t\t";
                    visageFace += "Une deviation de la lèvre inférieure";
                }
            }

            return visageFace;
        }



        public static void ConvertFromAnOccDroit(float value)
            {
                resumeCl.ClasseCanD = EntentePrealable.en_Class.Class_I;

        }

        public static void ConvertFromAnalyse2(float EspaceDentaireBuccal,
                                               float IncisiveMolaireDroit,
                                               float IncisiveMolaireGauche,
                                                float AngleIncisiveMolaireDroit,
                                               float AngleIncisiveMolaireGauche)
        {


            if (Math.Abs(EspaceDentaireBuccal - 0.85) > 0.05)//moins de 5% de marge
            {               
                    resumeCl.sourireDentaire = BasCommon_BO.EntentePrealable.en_SourireDentaire.Etroit;                
            }
            else
            {
                resumeCl.sourireDentaire = BasCommon_BO.EntentePrealable.en_SourireDentaire.Normal;
            }
            resumeCl.TNLDroit = BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.TNL1;
            resumeCl.TNLGauche = BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.TNL1;


            if (AngleIncisiveMolaireDroit > 5) // inclinaison Molaire >15°
                resumeCl.TNLDroit = BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.TNL2;
            else
                if (IncisiveMolaireDroit < 0.95)//moins de 5% de marge     
                    resumeCl.TNLDroit = BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.TNL3;


            if (AngleIncisiveMolaireGauche > 5 ) // inclinaison Molaire >15°
                resumeCl.TNLGauche = BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.TNL2;
            else
                if (IncisiveMolaireGauche < 0.95) //moins de 5% de marge            
                    resumeCl.TNLGauche = BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.TNL3;

            
            


        }
        
        public static void ConvertFromAnalyse6(string LevreSupRes, 
            string LevreInfRes,
            string MentonRes,
            string IncisiveSuperieur,
            string InclinaisonIncisiveSup,
            float InclinaisonIncisiveSupValue)
        {
            resumeCl.LevreSuperieur = (BasCommon_BO.EntentePrealable.en_ProRetro)Enum.Parse(typeof(BasCommon_BO.EntentePrealable.en_ProRetro), LevreSupRes);
            resumeCl.LevreInferieur = (BasCommon_BO.EntentePrealable.en_ProRetro)Enum.Parse(typeof(BasCommon_BO.EntentePrealable.en_ProRetro), LevreInfRes);
            resumeCl.Menton = (BasCommon_BO.EntentePrealable.en_ProRetro)Enum.Parse(typeof(BasCommon_BO.EntentePrealable.en_ProRetro), MentonRes);

            resumeCl.IncisiveSuperieur = (BasCommon_BO.EntentePrealable.en_ProRetro)Enum.Parse(typeof(BasCommon_BO.EntentePrealable.en_ProRetro), IncisiveSuperieur);
            resumeCl.InclinaisonIncisiveSupValue = InclinaisonIncisiveSupValue;

        }

        public static void ConvertFromAnalyse7(
            float FMA,
            float ANB,
            float SNA,
            float SNB,
            float IF,
            float IM,
            float SPP
            )
        {
            resumeCl.SensVertical = BasCommon_BO.EntentePrealable.en_Divergence.Normodivergent;
            if (Math.Round(FMA) < 18) resumeCl.SensVertical = BasCommon_BO.EntentePrealable.en_Divergence.Hypodivergent;
            if (Math.Round(FMA) > 25) resumeCl.SensVertical = BasCommon_BO.EntentePrealable.en_Divergence.Hyperdivergent;

            resumeCl.SensSagittal = BasCommon_BO.EntentePrealable.en_Class.Class_I;
            if (Math.Round(ANB) < 2) resumeCl.SensSagittal = BasCommon_BO.EntentePrealable.en_Class.Class_III;
            if (Math.Round(ANB) > 4) resumeCl.SensSagittal = BasCommon_BO.EntentePrealable.en_Class.Class_II;

            resumeCl.IncisiveSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            if (Math.Round(IF) < 107) resumeCl.IncisiveSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            if (Math.Round(IF) > 112) resumeCl.IncisiveSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.Pro;

            resumeCl.IncisiveInferieur = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            if (Math.Round(IM) < 88) resumeCl.IncisiveInferieur = BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            if (Math.Round(IM) > 92) resumeCl.IncisiveInferieur = BasCommon_BO.EntentePrealable.en_ProRetro.Pro;

            resumeCl.SensSagittalMaxBasal = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            if (Math.Round(SNA) < 80) resumeCl.SensSagittalMaxBasal = BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            if (Math.Round(SNA) > 82) resumeCl.SensSagittalMaxBasal = BasCommon_BO.EntentePrealable.en_ProRetro.Pro;

            resumeCl.SensSagittalMandBasal = BasCommon_BO.EntentePrealable.en_ProRetro.Normo;
            if (Math.Round(SNB) < 78) resumeCl.SensSagittalMandBasal = BasCommon_BO.EntentePrealable.en_ProRetro.Retro;
            if (Math.Round(SNB) > 80) resumeCl.SensSagittalMandBasal = BasCommon_BO.EntentePrealable.en_ProRetro.Pro;

            resumeCl.SPP_SPA = BasCommon_BO.EntentePrealable.en_SPP.normale;
            if (Math.Round(SPP) < 25) resumeCl.SPP_SPA = BasCommon_BO.EntentePrealable.en_SPP.hypodivergence;
            if (Math.Round(SPP) > 27) resumeCl.SPP_SPA = BasCommon_BO.EntentePrealable.en_SPP.hyperdivergence;
                
            
        }

        public static void Insert(ResumeClinique resume)
        {
            DAC.InsertResumeClinique(resume);

            if (resume.objectsForPlanTraitement != null)
                SavePlanDeTraitementVisuel(resume);

            #region Diagnostique
            CommentHisto ch = new CommentHisto();

            string s = "";
            foreach (CommonDiagnostic cd in resume.diagnostics)
            {
                if (s != "") s += "\r\n";
                s += cd.Libelle;
            }

            ch.comment = s;
            ch.DateCommentaire = DateTime.Now;
            ch.Ecrivain = resume.patient.infoscomplementaire!=null?resume.patient.infoscomplementaire.PraticienResponsable:null;
            ch.patient = resume.patient;
            ch.typecomment = CommentHisto.CommentHistoType.Diagnostique;
            MgmtCommentairesHisto.InsertCommentaire(ch);
            #endregion

            #region Objectifs

            ch = new CommentHisto();

            s = "";


            foreach (CommonObjectif cd in resume.patient.SelectedObjectifs)
            {
                if (s != "") s += "\r\n";
                s += cd.Libelle;
            }

            ch.comment = s;
            ch.DateCommentaire = DateTime.Now;
            ch.Ecrivain = resume.patient.infoscomplementaire==null?null:resume.patient.infoscomplementaire.PraticienResponsable;
            ch.patient = resume.patient;
            ch.typecomment = CommentHisto.CommentHistoType.Objectif;
            MgmtCommentairesHisto.InsertCommentaire(ch);

            #endregion

           
        }

        private static void SavePlanDeTraitementVisuel(ResumeClinique resume)
        {

            DAC.DeleteFullPlanTraitementVisuel(resume);

            foreach (PlanTraitementObject pto in resume.objectsForPlanTraitement)
                DAC.InsertPlanTraitementVisuel(pto);

        }

        public static void Update(ResumeClinique resume)
        {
            DAC.UpdateResumeClinique(resume);

            if (resume.objectsForPlanTraitement != null)
                SavePlanDeTraitementVisuel(resume);

            #region Diagnostique
            CommentHisto ch = new CommentHisto();

            string s = "";
            foreach (CommonDiagnostic cd in resume.diagnostics)
            {
                if (cd == null) continue;
                if (s != "") s += "\n";
                s += cd.Libelle;
            }

            ch.comment = s;
            ch.DateCommentaire = DateTime.Now;
            ch.Ecrivain = resume.patient.infoscomplementaire == null ? null : resume.patient.infoscomplementaire.PraticienResponsable;
            ch.patient = resume.patient;
            ch.typecomment = CommentHisto.CommentHistoType.Diagnostique;
            MgmtCommentairesHisto.InsertCommentaire(ch);
            #endregion

            #region Objectifs

            ch = new CommentHisto();

            s = "";


            foreach (CommonObjectif cd in resume.patient.SelectedObjectifs)
            {
                if (s != "") s += "\n";
                s += cd.Libelle;
            }

            ch.comment = s;
            ch.DateCommentaire = DateTime.Now;
            ch.Ecrivain = resume.patient.infoscomplementaire == null ? null : resume.patient.infoscomplementaire.PraticienResponsable;
            ch.patient = resume.patient;
            ch.typecomment = CommentHisto.CommentHistoType.Objectif;
            MgmtCommentairesHisto.InsertCommentaire(ch);

            #endregion

        }


        public static List<ResumeClinique> GetResumesClinique(basePatient pat)
        {
            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/getResumesCliniqueByPatient/" + pat.Id);

            List<ResumeClinique> lst = new List<ResumeClinique>();

            foreach (JObject dr in json)
            {
                ResumeClinique rc = Builders.BuildResumeCliniqueJson(dr);
                rc.patient = pat;
                lst.Add(rc);
            }

            return lst;
        }

        public static List<ResumeClinique> GetResumesCliniqueOld(basePatient pat)
        {
            System.Data.DataTable dt = DAC.getResumesCliniqueByPatient(pat.Id);
            
            List<ResumeClinique> lst = new List<ResumeClinique>();

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                ResumeClinique rc = Builders.BuildResumeClinique(dr);
                rc.patient = pat;
                lst.Add(rc);
            }

            return lst;
        }
        public static List<PlanTraitementObject> GetPlanTraitementObjects(int IdREsumeClinique)
        {
            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/getPlanTraitementObject/"+IdREsumeClinique);
            //System.Data.DataTable dt = DAC.GetPlanTraitementObjects(IdREsumeClinique);

            List<PlanTraitementObject> lst = new List<PlanTraitementObject>();

            foreach (JObject r in json)
            {
                PlanTraitementObject su = Builders.BuildPlanTraitementObjectJson(r);
              
                lst.Add(su);
            }

            return lst;
        }

        public static List<PlanTraitementObject> GetPlanTraitementObjectsOLD(int IdREsumeClinique)
        {
            System.Data.DataTable dt = DAC.GetPlanTraitementObjects(IdREsumeClinique);

            List<PlanTraitementObject> lst = new List<PlanTraitementObject>();

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                PlanTraitementObject rc = Builders.BuildPlanTraitementObject(dr);
                lst.Add(rc);
            }

            return lst;
        }
       

        public static void InsertAll(ResumeClinique resume)
        {

            Insert(resume);

            
        
        }

        public static void UpdateAll(ResumeClinique resume)
        {

            Update(resume);


        }

        public static void getObjectifs(ResumeClinique resume)
        {
            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/getSelectedObjectifs/" + resume.Id);
            resume.patient.SelectedObjectifs.Clear();
            foreach (JObject dr in json)
            {
                resume.patient.SelectedObjectifs.Add(CommonObjectifsMgmt.getCommonObjectif(Convert.ToInt32(dr["id"])));
            }
        }


        public static void getObjectifsOLD(ResumeClinique resume)
        {
            DataTable dt = DAC.getSelectedObjectifs(resume);

            resume.patient.SelectedObjectifs.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                resume.patient.SelectedObjectifs.Add(CommonObjectifsMgmt.getCommonObjectif(Convert.ToInt32(dr["Id"])));
            }


        }

        //public static void getAppareils(ResumeClinique resume)
        //{
        //    DataTable dt = DAC.getSelectedAppareils(resume);

        //    resume.patient.SelectedAppareils.Clear();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        resume.patient.SelectedAppareils.Add(AppareilMgmt.getAppareil(Convert.ToInt32(dr["Id"])));
        //    }

        //}

        public static void setObjectifs(ResumeClinique resume)
        {
            DAC.setSelectedObjectifs(resume);

        }

        //public static void setAppareils(ResumeClinique resume)
        //{
        //    DAC.setSelectedAppareils(resume);

        //}

        public static void SaveAll(ResumeClinique resume)
        {
            if (resume.Id == -1) InsertAll(resume);
            else UpdateAll(resume);

            setObjectifs(resume);
            //setAppareils(resume);
                
        }

     
        
    }
}
