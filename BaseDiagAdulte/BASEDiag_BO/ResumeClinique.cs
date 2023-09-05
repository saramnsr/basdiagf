using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using BasCommon_BO;
using System.IO;

namespace BASEDiag_BO
{
   

    public class ResumeClinique
    {
        
        /*
        #region Enums


        public enum Appareillage
        {
            RCC,
            QuadHelix,
            ArcLingual,
            Disjoncteur,
            GoutieresChirurgical,
            GoutiereBAS,
            PEI,
            ASI,                
            Goutiere_Invisalign,
            undefined
        }

        public enum TypeTraitement
        {
            RCC,
            MultiBague,
            Invisalign,
            Chirurgie,
            undefined
        }



        public enum en_FormeArcade
        {
            U,
            V,
            undefined
        }

        public enum en_OuiNon
        {
            Oui,
            Non,
            undefined
        }

        public enum en_InterpositionLingual
        {
            Normal,
            posterieur,
            anterieur,
            AnterieurEtPosterieur,
            undefined
        }

        public enum en_EtageInf
        {
            undefined,
            Augmentation,
            Normal,
            Diminution,
            Effondrement
        }

        public enum en_Sourire
        {
            undefined,
            Gingival,
            Labial,
            Normal
        }

        public enum en_OccFace
        {
            undefined,
            Supraclusion,
            Infraclusion,
            Normal
        }

        public enum en_SourireDentaire
        {
            undefined,
            Etroit,
            Normal,
            Large
        }

        public enum en_BoiteALangue
        {
            undefined,
            Etroite,
            Convenable
        }

        public enum en_OccInverse
        {
            undefined,
            Aucun,
            Droite,
            Gauche,
            Anterieur,
            Droite_Et_Gauche
        }

        public enum en_Laterodeviation
        {
            undefined,
            Aucun,
            Droite,
            Gauche,
            Droite_Et_Gauche

        }

        public enum en_DiagOsseux
        {
            undefined,
            Endognatie,
            Normognatie,
            Exognatie
        }

        public enum en_DiagAlveolaire
        {
            undefined,
            Endoalveolie,
            Normoalveolie,
            Exoalveolie
        }

        public enum en_TypeTriangleNoirLateral
        {
            undefined,
            Triangle_noir_lateral_1,
            Triangle_noir_lateral_2,
            Triangle_noir_lateral_3,
            Triangle_noir_lateral_4
        }

        public enum en_TriangleNoirLateral
        {
            undefined,
            Droit,
            Gauche,
            Droit_Et_Gauche,
            Aucun
        }

        public enum en_ProRetro
        {
            undefined,
            Pro,
            Retro,
            Normo
        }

        public enum en_Divergence
        {
            undefined,
            Hypodivergent,
            Hyperdivergent,
            Normodivergent
        }

        public enum en_Class
        {
            undefined,
            Class_I,
            Class_II,
            Class_III
        }

        public enum en_Traitement_EI
        {
            undefined,
            Rien,
            Egression,
            Ingression
        }

        public enum en_Traitement_AR
        {
            undefined,
            Rien,
            Avancer,
            Reculer
        }

        public enum en_Traitement_LR
        {
            undefined,
            Rien,
            Lente,
            Rapide
        }

        #endregion
        */
        #region Generalité


        #region Synchro Radio/Photo


        private bool _IsSynchronized = false;
        public bool IsSynchronized
        {
            get
            {
                return _IsSynchronized;
            }
            set
            {
                _IsSynchronized = value;
            }
        }

        private PointF _offset;
        public PointF synchrooffset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
            }
        }

        private float _angle;
        public float synchroangle
        {
            get
            {
                return _angle;
            }
            set
            {
                _angle = value;
            }
        }

        private float _zoom;
        public float synchrozoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                _zoom = value;
            }
        }

        #endregion

        private int _IdPatient;
        public int IdPatient
        {
            get
            {
                if (patient != null) _IdPatient = patient.Id;
                return _IdPatient;
            }
            set
            {
                _IdPatient = value;
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
        

        private DateTime _dateResume;
        public DateTime dateResume
        {
            get
            {
                return _dateResume;
            }
            set
            {
                _dateResume = value;
            }
        }



        private static bool exist(string path)
        {
            try
            {
                var req = System.Net.WebRequest.Create(path);
                using (Stream stream = req.GetResponse().GetResponseStream())
                {

                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;

        }
        private basePatient _patient;
        public basePatient patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;

                if (!exist(Img_Rad_Face))
                        Img_Rad_Face = _patient.Img_Rad_Face;
                if (!exist(Img_Rad_Pano))
                    Img_Rad_Pano = _patient.Img_Rad_Pano;
                if (!exist(Img_Rad_Profile))
                    Img_Rad_Profile = _patient.Img_Rad_Profile;

                if (!exist(Img_Ext_Face))
                    Img_Ext_Face = _patient.Img_Ext_Face;
                if (!exist(Img_Ext_Profile))
                    Img_Ext_Profile = _patient.Img_Ext_Profile;

                if (!exist(Img_Ext_Profile_Sourire))
                    Img_Ext_Profile_Sourire = _patient.Img_Ext_Profile_Sourire;
                if (!exist(Img_Ext_Face_Sourire))
                    Img_Ext_Face_Sourire = _patient.Img_Ext_Face_Sourire;

                if (!exist(Img_Ext_Sourire))
                    Img_Ext_Sourire = _patient.Img_Ext_Sourire;

                if (!exist(Img_Int_Droit))
                    Img_Int_Droit = _patient.Img_Int_Droit;
                if (!exist(Img_Int_SurPlomb))
                    Img_Int_SurPlomb = _patient.Img_Int_SurPlomb;
                if (!exist(Img_Int_Face))
                    Img_Int_Face = _patient.Img_Int_Face;
                if (!exist(Img_Int_Gauche))
                    Img_Int_Gauche = _patient.Img_Int_Gauche;
                if (!exist(Img_Int_Max))
                    Img_Int_Max = _patient.Img_Int_Max;
                if (!exist(Img_Int_Man))
                    Img_Int_Man = _patient.Img_Int_Man;

                if (!exist(Img_Moul_Droit))
                    Img_Moul_Droit = _patient.Img_Moul_Droit;
                if (!exist(Img_Moul_Face))
                    Img_Moul_Face = _patient.Img_Moul_Face;
                if (!exist(Img_Moul_Gauche))
                    Img_Moul_Gauche = _patient.Img_Moul_Gauche;
                if (!exist(Img_Moul_Max))
                    Img_Moul_Max = _patient.Img_Moul_Max;
                if (!exist(Img_Moul_Man))
                    Img_Moul_Man = _patient.Img_Moul_Man;
                if (!exist(Img_Rad_Supp))
                    Img_Moul_Man = _patient.Img_Moul_Man;

            }
        }
        #endregion


        private List<PlanTraitementObject> _objectsForPlanTraitement = null;
        public List<PlanTraitementObject> objectsForPlanTraitement
        {
            get
            {
                return _objectsForPlanTraitement;
            }
            set
            {
                _objectsForPlanTraitement = value;
            }
        }
        

        private List<CommonDiagnostic> _diagnostics = new List<CommonDiagnostic>();
        public List<CommonDiagnostic> diagnostics
        {
            get
            {
                return _diagnostics;
            }
            set
            {
                _diagnostics = value;
            }
        }
        
        #region Analyse 1 (Masque facial)


        private float _DeviationLevreInf;
        public float DeviationLevreInf
        {
            get
            {
                return _DeviationLevreInf;
            }
            set
            {
                _DeviationLevreInf = value;
            }
        }

        private float _DeviationMenton;
        public float DeviationMenton
        {
            get
            {
                return _DeviationMenton;
            }
            set
            {
                _DeviationMenton = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_EtageInf _EtageInf = BasCommon_BO.EntentePrealable.en_EtageInf.undefined;
        public BasCommon_BO.EntentePrealable.en_EtageInf EtageInf
        {
            get
            {
                return _EtageInf;
            }
            set
            {
                _EtageInf = value;
            }
        }

        #endregion

        #region Analyse 2 (Sourire Facial)

        private BasCommon_BO.EntentePrealable.en_SourireDentaire _sourireDentaire = BasCommon_BO.EntentePrealable.en_SourireDentaire.Normal;
        public BasCommon_BO.EntentePrealable.en_SourireDentaire sourireDentaire
        {
            get
            {
                return _sourireDentaire;
            }
            set
            {
                _sourireDentaire = value;
                _DiagAlveolaire = _sourireDentaire == BasCommon_BO.EntentePrealable.en_SourireDentaire.Etroit ? BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Endoalveolie : BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Normoalveolie;
                _DiagAlveolaire = _sourireDentaire == BasCommon_BO.EntentePrealable.en_SourireDentaire.Large ? BasCommon_BO.EntentePrealable.en_DiagAlveolaire.Exoalveolie : _DiagAlveolaire;
            }
        }

        private BasCommon_BO.EntentePrealable.en_DiagAlveolaire _DiagAlveolaire = BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined;
        public BasCommon_BO.EntentePrealable.en_DiagAlveolaire DiagAlveolaire
        {
            get
            {
                return _DiagAlveolaire;
            }
            set
            {
                _DiagAlveolaire = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType _TNLDroit = BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.undefined;
        public BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType TNLDroit
        {
            get
            {
                return _TNLDroit;
            }
            set
            {
                _TNLDroit = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType _TNLGauche = BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType.undefined;
        public BasCommon_BO.EntentePrealable.en_TriangleNoirLateralType TNLGauche
        {
            get
            {
                return _TNLGauche;
            }
            set
            {
                _TNLGauche = value;
            }
        }
        

        /// <summary>
        /// en mm
        /// </summary>
        private int _DecalageInterIncisiveHaut = 0;
        public int DecalageInterIncisiveHaut
        {
            get
            {
                return _DecalageInterIncisiveHaut;
            }
            set
            {
                _DecalageInterIncisiveHaut = value;
            }
        }

        /// <summary>
        /// en mm
        /// </summary>
        private int _DecalageInterIncisiveBas = 0;
        public int DecalageInterIncisiveBas
        {
            get
            {
                return _DecalageInterIncisiveBas;
            }
            set
            {
                _DecalageInterIncisiveBas = value;
            }
        }



        #endregion

        #region Analyse 3 (Occlusal)

        private BasCommon_BO.EntentePrealable.en_Class _ClasseCanD = BasCommon_BO.EntentePrealable.en_Class.undefined;
        public BasCommon_BO.EntentePrealable.en_Class ClasseCanD
        {
            get
            {
                return _ClasseCanD;
            }
            set
            {
                _ClasseCanD = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_Class _ClasseCanG = BasCommon_BO.EntentePrealable.en_Class.undefined;
        public BasCommon_BO.EntentePrealable.en_Class ClasseCanG
        {
            get
            {
                return _ClasseCanG;
            }
            set
            {
                _ClasseCanG = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_Class _ClasseMolD = BasCommon_BO.EntentePrealable.en_Class.undefined;
        public BasCommon_BO.EntentePrealable.en_Class ClasseMolD
        {
            get
            {
                return _ClasseMolD;
            }
            set
            {
                _ClasseMolD = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_Class _ClasseMolG = BasCommon_BO.EntentePrealable.en_Class.undefined;
        public BasCommon_BO.EntentePrealable.en_Class ClasseMolG
        {
            get
            {
                return _ClasseMolG;
            }
            set
            {
                _ClasseMolG = value;
            }
        }


        private BasCommon_BO.EntentePrealable.en_DiagOsseux _SensTransvMand = BasCommon_BO.EntentePrealable.en_DiagOsseux.undefined;
        public BasCommon_BO.EntentePrealable.en_DiagOsseux SensTransvMand
        {
            get
            {
                return _SensTransvMand;
            }
            set
            {
                _SensTransvMand = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_DiagOsseux _SensTransvMax = BasCommon_BO.EntentePrealable.en_DiagOsseux.undefined;
        public BasCommon_BO.EntentePrealable.en_DiagOsseux SensTransvMax
        {
            get
            {
                return _SensTransvMax;
            }
            set
            {
                _SensTransvMax = value;
            }
        }


        private BasCommon_BO.EntentePrealable.en_DiagAlveolaire _DiagMand = BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined;
        public BasCommon_BO.EntentePrealable.en_DiagAlveolaire DiagMand
        {
            get
            {
                return _DiagMand;
            }
            set
            {
                _DiagMand = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_DiagAlveolaire _DiagMax = BasCommon_BO.EntentePrealable.en_DiagAlveolaire.undefined;
        public BasCommon_BO.EntentePrealable.en_DiagAlveolaire DiagMax
        {
            get
            {
                return _DiagMax;
            }
            set
            {
                _DiagMax = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_Laterodeviation _Laterodeviation = BasCommon_BO.EntentePrealable.en_Laterodeviation.Aucun;
        public BasCommon_BO.EntentePrealable.en_Laterodeviation Laterodeviation
        {
            get
            {
                return _Laterodeviation;
            }
            set
            {
                _Laterodeviation = value;
            }
        }


        private BasCommon_BO.EntentePrealable.en_OuiNon _SautArticule = BasCommon_BO.EntentePrealable.en_OuiNon.Non;
        public BasCommon_BO.EntentePrealable.en_OuiNon SautArticule
        {
            get
            {
                return _SautArticule;
            }
            set
            {
                _SautArticule = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_OccInverse _OcclusionInverse = BasCommon_BO.EntentePrealable.en_OccInverse.Aucun;
        public BasCommon_BO.EntentePrealable.en_OccInverse OcclusionInverse
        {
            get
            {
                return _OcclusionInverse;
            }
            set
            {
                _OcclusionInverse = value;
            }
        }

        public string FacteurFonctionnel
        {
            get
            {
                /*
                 * Dysfonctions  linguale, labial   et  occlusale
                 * Incompétence labiale
                 * Interposition  labiale              
                 * Interposition  linguale  antérieure
                 * Interposition  linguale  antérieure langue basse
                 * Interposition  linguale  antérieure langue basse+ respiration buccale
                 * Interposition  linguale  antérieure langue basse,  latérodéviation
                 * Interposition  linguale  antérieure langue basse, frein labial, latérodéviation
                 * Interposition  linguale  antérieure langue basse, latérodéviation
                 * Interposition linguale postérieure
                 * Interposition linguale postérieure et frein labial bas
                 * Langue  Basse
                 * Langue  Basse, frein labial bas
                 * Laterodeviation mandibulaire à dgauche
                 * Laterodeviation mandibulaire à droite
                 * Laterodeviation mandibulaire à gauche
                 * Latérodéviation  fonctionnelle
                 * Latérodéviation  fonctionnelle et langue basse
                 * Latérodéviation  fonctionnelle, frein labial bas
                 * Promandibulie fonctionnelle
                 * Respiration buccale
                 * Rétroposition fonctionnelle
                 * Succion  pouce
                 * interposition linguale postérieure
                 */
                List<string> res = new List<string>();
                if (LangueBasse == BasCommon_BO.EntentePrealable.en_OuiNon.Oui) res.Add("Langue basse");
                if (InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.anterieur) res.Add("Interposition lingual anterieur");
                if (InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.AnterieurEtPosterieur) res.Add("Interposition lingual anterieur et postérieur");
                if (InterpositonLingual == BasCommon_BO.EntentePrealable.en_InterpositionLingual.posterieur) res.Add("Interposition lingual posterieur");
                if (FreinLabial == BasCommon_BO.EntentePrealable.en_OuiNon.Oui) res.Add("Frein labial");
                if (FreinLingual == BasCommon_BO.EntentePrealable.en_OuiNon.Oui) res.Add("Frein lingual");

                string result = "";
                foreach (string s in res)
                {
                    result += result == "" ? "" : ",";
                    result += s;
                }
                return result;

            }
            
        }

        private int _OcclusionValue = 0;
        public int OcclusionValue
        {
            get
            {
                return _OcclusionValue;
            }
            set
            {
                _OcclusionValue = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_OccFace _OcclusionFace = BasCommon_BO.EntentePrealable.en_OccFace.undefined;
        public BasCommon_BO.EntentePrealable.en_OccFace OcclusionFace
        {
            get
            {
                return _OcclusionFace;
            }
            set
            {
                _OcclusionFace = value;
            }
        }

        
        
        #endregion
        #region Analyse Fonctionnel 

     
        //private BasCommon_BO.EntentePrealable.en_Respiration _FormeRespiration = BasCommon_BO.EntentePrealable.en_Respiration.undefined;
        //public BasCommon_BO.EntentePrealable.en_Respiration FormeRespiration
        //{
        //    get
        //    {
        //        return _FormeRespiration;
        //    }
        //    set
        //    {
        //        _FormeRespiration = value;
        //    }
        //}
     


        #endregion

        #region Analyse 4 (Arcade)
        private BasCommon_BO.EntentePrealable.en_InterpositionLingual _InterpositonLingual = BasCommon_BO.EntentePrealable.en_InterpositionLingual.undefined;
        public BasCommon_BO.EntentePrealable.en_InterpositionLingual InterpositonLingual
        {
            get
            {
                return _InterpositonLingual;
            }
            set
            {
                _InterpositonLingual = value;
            }
        }
     

        private BasCommon_BO.EntentePrealable.en_FormeArcade _FormeArcade = BasCommon_BO.EntentePrealable.en_FormeArcade.undefined;
        public BasCommon_BO.EntentePrealable.en_FormeArcade FormeArcade
        {
            get
            {
                return _FormeArcade;
            }
            set
            {
                _FormeArcade = value;
            }
        }
        private BasCommon_BO.EntentePrealable.en_Respiration _FormeRespiration = BasCommon_BO.EntentePrealable.en_Respiration.undefined;
        public BasCommon_BO.EntentePrealable.en_Respiration FormeRespiration
        {
            get
            {
                return _FormeRespiration;
            }
            set
            {
                _FormeRespiration = value;
            }
        }


        private int _SurplombValue;
        public int SurplombValue
        {
            get
            {
                return _SurplombValue;
            }
            set
            {
                _SurplombValue = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_OuiNon _FreinLabial = BasCommon_BO.EntentePrealable.en_OuiNon.undefined;
        public BasCommon_BO.EntentePrealable.en_OuiNon FreinLabial
        {
            get
            {
                return _FreinLabial;
            }
            set
            {
                _FreinLabial = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_OuiNon _FreinLingual = BasCommon_BO.EntentePrealable.en_OuiNon.undefined;
        public BasCommon_BO.EntentePrealable.en_OuiNon FreinLingual
        {
            get
            {
                return _FreinLingual;
            }
            set
            {
                _FreinLingual = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_OuiNon _LangueBasse = BasCommon_BO.EntentePrealable.en_OuiNon.undefined;
        public BasCommon_BO.EntentePrealable.en_OuiNon LangueBasse
        {
            get
            {
                return _LangueBasse;
            }
            set
            {
                _LangueBasse = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_OuiNon _DDD = BasCommon_BO.EntentePrealable.en_OuiNon.undefined;
        public BasCommon_BO.EntentePrealable.en_OuiNon DDD
        {
            get
            {
                return _DDD;
            }
            set
            {
                _DDD = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_OuiNon _Diasteme = BasCommon_BO.EntentePrealable.en_OuiNon.Non;
        public BasCommon_BO.EntentePrealable.en_OuiNon Diasteme
        {
            get
            {
                return _Diasteme;
            }
            set
            {
                _Diasteme = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_OuiNon _DDM = BasCommon_BO.EntentePrealable.en_OuiNon.undefined;
        public BasCommon_BO.EntentePrealable.en_OuiNon DDM
        {
            get
            {
                return _DDM;
            }
            set
            {
                _DDM = value;
            }
        }
#endregion

        #region analyse 5

        private BasCommon_BO.EntentePrealable.en_OuiNon _SourireGingivalSup = BasCommon_BO.EntentePrealable.en_OuiNon.undefined;
        public BasCommon_BO.EntentePrealable.en_OuiNon SourireGingivalSup
        {
            get
            {
                return _SourireGingivalSup;
            }
            set
            {
                _SourireGingivalSup = value;
            }
        }

        private int _LabialValue;
        public int LabialValue
        {
            get
            {
                return _LabialValue;
            }
            set
            {
                _LabialValue = value;
            }
        }

        private int _GingivalInfValue;
        public int GingivalInfValue
        {
            get
            {
                return _GingivalInfValue;
            }
            set
            {
                _GingivalInfValue = value;
            }
        }

        private int _GingivalSupValue;
        public int GingivalSupValue
        {
            get
            {
                return _GingivalSupValue;
            }
            set
            {
                _GingivalSupValue = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_OuiNon _SourireGingivalInf = BasCommon_BO.EntentePrealable.en_OuiNon.undefined;
        public BasCommon_BO.EntentePrealable.en_OuiNon SourireGingivalInf
        {
            get
            {
                return _SourireGingivalInf;
            }
            set
            {
                _SourireGingivalInf = value;
            }
        }


        private BasCommon_BO.EntentePrealable.en_OuiNon _SourireLabial = BasCommon_BO.EntentePrealable.en_OuiNon.undefined;
        public BasCommon_BO.EntentePrealable.en_OuiNon SourireLabial
        {
            get
            {
                return _SourireLabial;
            }
            set
            {
                _SourireLabial = value;
            }
        }

        #endregion

        #region Analyse 6
        private float _InclinaisonIncisiveSupValue = 110;
        public float InclinaisonIncisiveSupValue
        {
            get
            {
                return _InclinaisonIncisiveSupValue;
            }
            set
            {
                _InclinaisonIncisiveSupValue = value;
            }
        }



        private BasCommon_BO.EntentePrealable.en_ProRetro _IncisiveSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.undefined;
        public BasCommon_BO.EntentePrealable.en_ProRetro IncisiveSuperieur
        {
            get
            {
                return _IncisiveSuperieur;
            }
            set
            {
                _IncisiveSuperieur = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_ProRetro _Menton = BasCommon_BO.EntentePrealable.en_ProRetro.undefined;
        public BasCommon_BO.EntentePrealable.en_ProRetro Menton
        {
            get
            {
                return _Menton;
            }
            set
            {
                _Menton = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_ProRetro _LevreInferieur = BasCommon_BO.EntentePrealable.en_ProRetro.undefined;
        public BasCommon_BO.EntentePrealable.en_ProRetro LevreInferieur
        {
            get
            {
                return _LevreInferieur;
            }
            set
            {
                _LevreInferieur = value;
            }
        }



        private int _IdModeleEntente = -1;
        public int IdModelEntente
        {
            get
            {
                return _IdModeleEntente;
            }
            set
            {
                _IdModeleEntente = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_ProRetro _LevreSuperieur = BasCommon_BO.EntentePrealable.en_ProRetro.undefined;
        public BasCommon_BO.EntentePrealable.en_ProRetro LevreSuperieur
        {
            get
            {
                return _LevreSuperieur;
            }
            set
            {
                _LevreSuperieur = value;
            }
        }

        #endregion

        #region Analyse 7

        private BasCommon_BO.EntentePrealable.en_ProRetro _SensSagittalMandBasal = BasCommon_BO.EntentePrealable.en_ProRetro.undefined;
        public BasCommon_BO.EntentePrealable.en_ProRetro SensSagittalMandBasal
        {
            get
            {
                return _SensSagittalMandBasal;
            }
            set
            {
                _SensSagittalMandBasal = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_ProRetro _SensSagittalMaxBasal = BasCommon_BO.EntentePrealable.en_ProRetro.undefined;
        public BasCommon_BO.EntentePrealable.en_ProRetro SensSagittalMaxBasal
        {
            get
            {
                return _SensSagittalMaxBasal;
            }
            set
            {
                _SensSagittalMaxBasal = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_ProRetro _IncisiveInferieur = BasCommon_BO.EntentePrealable.en_ProRetro.undefined;
        public BasCommon_BO.EntentePrealable.en_ProRetro IncisiveInferieur
        {
            get
            {
                return _IncisiveInferieur;
            }
            set
            {
                _IncisiveInferieur = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_Class _SensSagittal = BasCommon_BO.EntentePrealable.en_Class.undefined;
        public BasCommon_BO.EntentePrealable.en_Class SensSagittal
        {
            get
            {
                return _SensSagittal;
            }
            set
            {
                _SensSagittal = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_Divergence _SensVertical = BasCommon_BO.EntentePrealable.en_Divergence.undefined;
        public BasCommon_BO.EntentePrealable.en_Divergence SensVertical
        {
            get
            {
                return _SensVertical;
            }
            set
            {
                _SensVertical = value;
            }
        }
        private BasCommon_BO.EntentePrealable.en_SPP _SPP_SPA = BasCommon_BO.EntentePrealable.en_SPP.normale;
        public BasCommon_BO.EntentePrealable.en_SPP SPP_SPA
        {
            get
            {
                return _SPP_SPA;
            }
            set
            {
                _SPP_SPA = value;
            }
        }
        #endregion

        #region Analyse 8 (Pano)
        private string _EvolGermesDesDentsSur = "";
        public string EvolGermesDesDentsSur
        {
            get
            {
                return _EvolGermesDesDentsSur;
            }
            set
            {
                _EvolGermesDesDentsSur = value;
            }
        }

        private BasCommon_BO.EntentePrealable.en_OuiNon _EvolGermesDesDents = BasCommon_BO.EntentePrealable.en_OuiNon.undefined;
        public BasCommon_BO.EntentePrealable.en_OuiNon EvolGermesDesDents
        {
            get
            {
                return _EvolGermesDesDents;
            }
            set
            {
                _EvolGermesDesDents = value;
            }
        }

        private string _DentsDeSagesse = "";
        public string DentsDeSagesse
        {
            get
            {
                return _DentsDeSagesse;
            }
            set
            {
                _DentsDeSagesse = value;
            }
        }

        private string _NoTaquets = "";
        public string NoTaquets
        {
            get
            {
                return _NoTaquets;
            }
            set
            {
                _NoTaquets = value;
            }
        }

        private string _NoMvts = "";
        public string NoMvts
        {
            get
            {
                return _NoMvts;
            }
            set
            {
                _NoMvts = value;
            }
        }

        private string _DentsSurnumeraires = "";
        public string DentsSurnumeraires
        {
            get
            {
                return _DentsSurnumeraires;
            }
            set
            {
                _DentsSurnumeraires = value;
            }
        }

        private string _DentsIncluses = "";
        public string DentsIncluses
        {
            get
            {
                return _DentsIncluses;
            }
            set
            {
                _DentsIncluses = value;
            }
        }



        private string _Controle = "";
        public string Controle
        {
            get
            {
                return _Controle;
            }
            set
            {
                _Controle = value;
            }
        }

        private string _Agenesie = "";
        public string Agenesie
        {
            get
            {
                return _Agenesie;
            }
            set
            {
                _Agenesie = value;
            }
        }
        #endregion
            
        #region Images
        //Images
        public string Img_Rad_Face = "";
        public string Img_Rad_Pano = "";
        public string Img_Rad_Profile = "";

        public string Img_Ext_Face = "";
        public string Img_Ext_Profile = "";

        public string Img_Ext_Profile_Sourire = "";
        public string Img_Ext_Face_Sourire = "";
        public string Img_Ext_Sourire = "";

        public string Img_Int_Droit = "";
        public string Img_Int_SurPlomb = "";
        public string Img_Int_Face = "";
        public string Img_Int_Gauche = "";
        public string Img_Int_Max = "";
        public string Img_Int_Man = "";

        public string Img_Moul_Droit = "";
        public string Img_Moul_Face = "";
        public string Img_Moul_Gauche = "";
        public string Img_Moul_Max = "";
        public string Img_Moul_Man = "";
        public string Img_Rad_Supp = "";
        //


       
       
               
        #endregion

        
        
        private List<PointToTake> _LstPtAnalyse1 = new List<PointToTake>();
        public List<PointToTake> LstPtAnalyse1
        {
            get
            {
                return _LstPtAnalyse1;
            }
            set
            {
                _LstPtAnalyse1 = value;
            }
        }

        private List<PointToTake> _LstPtAnalyseSourire = new List<PointToTake>();
        public List<PointToTake> LstPtAnalyseSourire
        {
            get
            {
                return _LstPtAnalyseSourire;
            }
            set
            {
                _LstPtAnalyseSourire = value;
            }
        }
              
        private List<PointToTake> _LstPtAnalyse2 = new List<PointToTake>();
        public List<PointToTake> LstPtAnalyse2
        {
            get
            {
                return _LstPtAnalyse2;
            }
            set
            {
                _LstPtAnalyse2 = value;
            }
        }

        private List<PointToTake> _LstPtAnalyse62 = new List<PointToTake>();
        public List<PointToTake> LstPtAnalyse62
        {
            get
            {
                return _LstPtAnalyse62;
            }
            set
            {
                _LstPtAnalyse62 = value;
            }
        }
        
        private List<PointToTake> _LstPtAnalyse7 = new List<PointToTake>();
        public List<PointToTake> LstPtAnalyse7
        {
            get
            {
                return _LstPtAnalyse7;
            }
            set
            {
                _LstPtAnalyse7 = value;
            }
        }

        private List<PointToTake> _LstPtAnalyseOccD = new List<PointToTake>();
        public List<PointToTake> LstPtAnalyseOccD
        {
            get
            {
                return _LstPtAnalyseOccD;
            }
            set
            {
                _LstPtAnalyseOccD = value;
            }
        }

        private List<PointToTake> _LstPtAnalyseOccF = new List<PointToTake>();
        public List<PointToTake> LstPtAnalyseOccF
        {
            get
            {
                return _LstPtAnalyseOccF;
            }
            set
            {
                _LstPtAnalyseOccF = value;
            }
        }

        private List<PointToTake> _LstPtAnalyseOccG = new List<PointToTake>();
        public List<PointToTake> LstPtAnalyseOccG
        {
            get
            {
                return _LstPtAnalyseOccG;
            }
            set
            {
                _LstPtAnalyseOccG = value;
            }
        }

       

        #region Post analyse


        private BasCommon_BO.EntentePrealable.Appareillage _appareillage = BasCommon_BO.EntentePrealable.Appareillage.undefined;
        public BasCommon_BO.EntentePrealable.Appareillage appareillage
        {
            get
            {
                return _appareillage;
            }
            set
            {
                _appareillage = value;
            }
        }

        private BasCommon_BO.EntentePrealable.TypeTraitement _TypeDeTraitement = BasCommon_BO.EntentePrealable.TypeTraitement.undefined;
        public BasCommon_BO.EntentePrealable.TypeTraitement TypeDeTraitement
        {
            get
            {
                return _TypeDeTraitement;
            }
            set
            {
                _TypeDeTraitement = value;
            }
        }

        #endregion


        public BasCommon_BO.EntentePrealable.Appareillage GetAppareillage()
        {
            //TODO definir automatiquement l'appareillage et le type de traitement
            return BasCommon_BO.EntentePrealable.Appareillage.RCC;
        }

        public BasCommon_BO.EntentePrealable.TypeTraitement GetTypeTraitement()
        {
            //TODO definir automatiquement l'appareillage et le type de traitement
            BasCommon_BO.EntentePrealable.Appareillage app = GetAppareillage();
            switch (app)
            {
                case BasCommon_BO.EntentePrealable.Appareillage.RCC:
                    return BasCommon_BO.EntentePrealable.TypeTraitement.RCC;
                case BasCommon_BO.EntentePrealable.Appareillage.QuadHelix:
                    return BasCommon_BO.EntentePrealable.TypeTraitement.MultiBague;
                    //case Appareillage.PEI:
                    //case Appareillage.Disjoncteur
                    //case Appareillage.ASI
                    //case Appareillage.ArcLingual
                case BasCommon_BO.EntentePrealable.Appareillage.GoutieresChirurgical:
                    return BasCommon_BO.EntentePrealable.TypeTraitement.Chirurgie;
                case BasCommon_BO.EntentePrealable.Appareillage.Goutiere_Invisalign:
                    return BasCommon_BO.EntentePrealable.TypeTraitement.Invisalign;
                default :
                    return BasCommon_BO.EntentePrealable.TypeTraitement.undefined;
                    
            }
            
        }



    }
}
