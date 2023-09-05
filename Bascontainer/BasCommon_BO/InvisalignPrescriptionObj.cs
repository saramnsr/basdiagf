using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class InvisalignPrescriptionFullObj
    {

        public class treatArches
        {

            public enum TypeTraitementArcade
            {
                both,
                upperOnly,
                lowerOnly
            }


            private bool _lowerOnlyDiagnosticSetup = false;
            public bool lowerOnlyDiagnosticSetup
            {
                get
                {
                    return _lowerOnlyDiagnosticSetup;
                }
                set
                {
                    _lowerOnlyDiagnosticSetup = value;

                }
            }

            private bool _upperOnlyDiagnosticSetup = false;
            public bool upperOnlyDiagnosticSetup
            {
                get
                {
                    return _upperOnlyDiagnosticSetup;
                }
                set
                {
                    _upperOnlyDiagnosticSetup = value;
                }
            }

            private bool _FullArcade = true;
            public bool FullArcade
            {
                get
                {
                    return _FullArcade;
                }
                set
                {
                    _FullArcade = value;
                }
            }


            private TypeTraitementArcade _TypeTraitement = TypeTraitementArcade.both;
            public TypeTraitementArcade TypeTraitement
            {
                get
                {
                    return _TypeTraitement;
                }
                set
                {
                    _TypeTraitement = value;                   
                    
                }
            }


            public bool Validate()
            {

                bool res = false;

                if (TypeTraitement == TypeTraitementArcade.both)
                {
                    upperOnlyDiagnosticSetup = false;
                    lowerOnlyDiagnosticSetup = false;
                }
                if (TypeTraitement == TypeTraitementArcade.lowerOnly)
                    upperOnlyDiagnosticSetup = false;
                if (TypeTraitement == TypeTraitementArcade.upperOnly)
                    lowerOnlyDiagnosticSetup = false;

                return true;
            }

        }
        

        public class DoNotMoveTeeth
        {

            public DoNotMoveTeeth()
            {
                for (int i = 0; i < DoNotMoveTheseTeeth.Length; i++)
                    DoNotMoveTheseTeeth[i] = false;
            }

            private bool _DoNotMoveAnyTeeth = true;
            public bool DoNotMoveAnyTeeth
            {
                get
                {
                    return _DoNotMoveAnyTeeth;
                }
                set
                {
                    _DoNotMoveAnyTeeth = value;
                }
            }

            private bool[] _DoNotMoveTheseTeeth = new bool[32];
            public bool[] DoNotMoveTheseTeeth
            {
                get
                {
                    return _DoNotMoveTheseTeeth;
                }
                set
                {
                    _DoNotMoveTheseTeeth = value;
                }
            }


            public bool Validate()
            {
                if (DoNotMoveAnyTeeth == false)
                {
                    foreach (bool b in _DoNotMoveTheseTeeth)
                        if (b) return true;
                    return false;
                }
                else
                    return true;

            }
        }

        public class TeenQuestions
        {

            public static  String[] spacecan = new string[] { "Aucun","7 mm", "7.5 mm", "8 mm", "8.5 mm", "9 mm", "autre" };

            public enum SpaceForCanineAnd2NdBiEnum
            {
                sAucun = 0,
                s7mm = 1,
                s75mm = 2,
                s8mm = 3,
                s85mm = 4,
                s9mm = 5,
                sautremm = 6
            };

            public TeenQuestions()
            {
                for (int i = 0; i < spacesForCanineAnd2NdBi.Length; i++)
                    spacesForCanineAnd2NdBi[i] = 0;

                for (int i = 0; i < teenSecondMolarTabs.Length; i++)
                    teenSecondMolarTabs[i] = false;
            }

            private SpaceForCanineAnd2NdBiEnum[] _spacesForCanineAnd2NdBi = new SpaceForCanineAnd2NdBiEnum[32];
            public SpaceForCanineAnd2NdBiEnum[] spacesForCanineAnd2NdBi
            {
                get
                {
                    return _spacesForCanineAnd2NdBi;
                }
                set
                {
                    _spacesForCanineAnd2NdBi = value;
                }
            }

            private bool[] _teenSecondMolarTabs = new bool[32];
            public bool[] teenSecondMolarTabs
            {
                get
                {
                    return _teenSecondMolarTabs;
                }
                set
                {
                    _teenSecondMolarTabs = value;
                }
            }


            private int _teenSecondMolarTabsStartStage = 1;
            public int teenSecondMolarTabsStartStage
            {
                get
                {
                    return _teenSecondMolarTabsStartStage;
                }
                set
                {
                    _teenSecondMolarTabsStartStage = value;
                }
            }


            private bool _IsteenSecondMolar = false;
            public bool IsteenSecondMolar
            {
                get
                {
                    return _IsteenSecondMolar;
                }
                set
                {
                    _IsteenSecondMolar = value;
                }
            }


            public bool Validate()
            {
                bool ok = true;
                for (int i = 0; i < spacesForCanineAnd2NdBi.Length; i++)
                    if ((i != 3 && i != 5 && i != 10 && i != 12) &&
                        (i != 28 && i != 26 && i != 21 && i != 19))
                    {
                        spacesForCanineAnd2NdBi[i] = 0;
                    }

                for (int i = 0; i < teenSecondMolarTabs.Length; i++)
                {
                    if ((i != 0 && i != 1 && i != 14 && i != 15 &&
                        i != 31 && i != 30 && i != 17 && i != 16)||
                        !IsteenSecondMolar)
                        teenSecondMolarTabs[i] = false;
                }

                return ok;
            }
        }

        public class Attachements
        {

            public Attachements()
            {
                for (int i = 0; i < _TeethPermittedForAttachement.Length; i++)
                    _TeethPermittedForAttachement[i] = false;
            }

            private bool _TeethPermittedForAttachements_Any = true;
            public bool TeethPermittedForAttachements
            {
                get
                {
                    return _TeethPermittedForAttachements_Any;
                }
                set
                {
                    _TeethPermittedForAttachements_Any = value;
                }
            }

            private bool[] _TeethPermittedForAttachement = new bool[32];
            public bool[] TeethPermittedForAttachement
            {
                get
                {
                    return _TeethPermittedForAttachement;
                }
                set
                {
                    _TeethPermittedForAttachement = value;
                }
            }

            public bool Validate()
            {
                if (TeethPermittedForAttachements == false)
                {
                    foreach (bool b in _TeethPermittedForAttachement)
                        if (b) return true;
                    return false;
                }
                else
                    return true;

            }
        }
        
        public class aPRelation
        {
            public enum TraitmntaPRelation
            {
                maintain,
                improveToClass1CanineOnly,
                partialClass1CanineOnly,
                class1CanineAndMolar
            }

            public enum Options
            {
                None,
                ToothMovement,
                Surgical
            }


            public enum EnumYeNo
            {
                None,
                Yes,
                No
            }

            private TraitmntaPRelation _aPRelationLeft;
            public TraitmntaPRelation aPRelationLeft
            {
                get
                {
                    return _aPRelationLeft;
                }
                set
                {
                    _aPRelationLeft = value;
                }
            }


            private TraitmntaPRelation _aPRelationRight;
            public TraitmntaPRelation aPRelationRight
            {
                get
                {
                    return _aPRelationRight;
                }
                set
                {
                    _aPRelationRight = value;
                }
            }
            
            private Options _options = Options.None;
            public Options options
            {
                get
                {
                    return _options;
                }
                set
                {
                    _options = value;
                }
            }

            private bool _distalization = false;
            public bool distalization
            {
                get
                {
                    return _distalization;
                }
                set
                {
                    _distalization = value;
                }
            }


            private EnumYeNo _distalizationPrecisionCut = EnumYeNo.None;
            public EnumYeNo distalizationPrecisionCut
            {
                get
                {
                    return _distalizationPrecisionCut;
                }
                set
                {
                    _distalizationPrecisionCut = value;
                }
            }


            private bool _classIIOrIIICorrectionSimulation;
            public bool classIIOrIIICorrectionSimulation
            {
                get
                {
                    return _classIIOrIIICorrectionSimulation;
                }
                set
                {
                    _classIIOrIIICorrectionSimulation = value;
                }
            }


            private EnumYeNo _classIIOrIIICorrectionSimulationPrecisionCut = EnumYeNo.None;
            public EnumYeNo classIIOrIIICorrectionSimulationPrecisionCut
            {
                get
                {
                    return _classIIOrIIICorrectionSimulationPrecisionCut;
                }
                set
                {
                    _classIIOrIIICorrectionSimulationPrecisionCut = value;
                }
            }


            private bool _PosteriorIPR = false;
            public bool PosteriorIPR
            {
                get
                {
                    return _PosteriorIPR;
                }
                set
                {
                    _PosteriorIPR = value;
                }
            }


            public bool Validate()
            {

                if ((aPRelationLeft == TraitmntaPRelation.class1CanineAndMolar) ||
                    (aPRelationRight == TraitmntaPRelation.class1CanineAndMolar))
                {
                }
                else
                    if ((aPRelationLeft == TraitmntaPRelation.partialClass1CanineOnly) ||
                    (aPRelationRight == TraitmntaPRelation.partialClass1CanineOnly))
                    {
                    }
                    else
                        if ((aPRelationLeft == TraitmntaPRelation.improveToClass1CanineOnly) ||
                            (aPRelationRight == TraitmntaPRelation.improveToClass1CanineOnly))
                        {
                            options = Options.ToothMovement;
                            classIIOrIIICorrectionSimulation = false;
                        }
                        else
                            if ((aPRelationLeft == TraitmntaPRelation.maintain) &&
                                (aPRelationRight == TraitmntaPRelation.maintain))
                            {
                                options = Options.None;
                                PosteriorIPR = false;
                                distalization = false;
                                classIIOrIIICorrectionSimulation = false;

                            }

                if (!classIIOrIIICorrectionSimulation)
                    classIIOrIIICorrectionSimulationPrecisionCut = EnumYeNo.None;
                if (!distalization)
                    distalizationPrecisionCut = EnumYeNo.None;


                return true;
            }


        }
        
        public class overJet
        {
            public enum overJetOptions
            {
                showResultantOverjetAfterAlignment,
                maintainInitialOverjet,
                improveResultingOverjetWithIPR
            }


            private overJetOptions _options = overJetOptions.showResultantOverjetAfterAlignment;
            public overJetOptions options
            {
                get
                {
                    return _options;
                }
                set
                {
                    _options = value;
                }
            }


            public bool Validate()
            {

                return true;
            }


        }

        public class Ramps
        {

            public enum biteRampsUpperOptions
            {
                None,
                biteRampsUpperOptions,
            }

            public enum biteRampsUpperOptionsOn
            {
                incisors,
                canines
            }



            public biteRampsUpperOptions biteRampsUpperOpt { get; set; }
            public biteRampsUpperOptionsOn biteRampsUpperOptOn { get; set; }
            public bool centralIncisors { get; set; }
            public bool lateralIncisors { get; set; }

            public bool Validate()
            {

                if (biteRampsUpperOpt != biteRampsUpperOptions.biteRampsUpperOptions)
                {
                    centralIncisors = false;
                    centralIncisors = false;
                }

                if (biteRampsUpperOptOn != biteRampsUpperOptionsOn.canines)
                {
                    centralIncisors = false;
                    centralIncisors = false;
                }

                return true;

            }
        }

        public class overbite
        {
            public enum overbiteOptions
            {
                maintainResultant,
                maintainInitial,
                correctOpenBite,
                correctDeepBite,
            }

            public enum OpenbiteOptions
            {
                None,
                extrudeAnteriorOnly,
                extrudeAnteriorTeethAndIntrudePosterior,
                other
            }


            private overbiteOptions _options;
            public overbiteOptions options
            {
                get
                {
                    return _options;
                }
                set
                {
                    _options = value;
                }
            }



            private OpenbiteOptions _openbiteoption;
            public OpenbiteOptions OpenbiteOption
            {
                get
                {
                    return _openbiteoption;
                }
                set
                {
                    _openbiteoption = value;
                }
            }


            private bool _extrudeAnteriorOnlyLowerArch;
            public bool extrudeAnteriorOnlyLowerArch
            {
                get
                {
                    return _extrudeAnteriorOnlyLowerArch;
                }
                set
                {
                    _extrudeAnteriorOnlyLowerArch = value;
                }
            }


            private bool _extrudeAnteriorOnlyUpperArch;
            public bool extrudeAnteriorOnlyUpperArch
            {
                get
                {
                    return _extrudeAnteriorOnlyUpperArch;
                }
                set
                {
                    _extrudeAnteriorOnlyUpperArch = value;
                }
            }

            private bool _extrudeAnteriorTeethAndIntrudePosteriorLowerArch;
            public bool extrudeAnteriorTeethAndIntrudePosteriorLowerArch
            {
                get
                {
                    return _extrudeAnteriorTeethAndIntrudePosteriorLowerArch;
                }
                set
                {
                    _extrudeAnteriorTeethAndIntrudePosteriorLowerArch = value;
                }
            }


            private bool _extrudeAnteriorTeethAndIntrudePosteriorUpperArch;
            public bool extrudeAnteriorTeethAndIntrudePosteriorUpperArch
            {
                get
                {
                    return _extrudeAnteriorTeethAndIntrudePosteriorUpperArch;
                }
                set
                {
                    _extrudeAnteriorTeethAndIntrudePosteriorUpperArch = value;
                }
            }



            private bool _correctDeepBiteUpperArch;
            public bool correctDeepBiteUpperArch
            {
                get
                {
                    return _correctDeepBiteUpperArch;
                }
                set
                {
                    _correctDeepBiteUpperArch = value;
                }
            }


            private bool _correctDeepBiteLowerArch;
            public bool correctDeepBiteLowerArch
            {
                get
                {
                    return _correctDeepBiteLowerArch;
                }
                set
                {
                    _correctDeepBiteLowerArch = value;
                }
            }


            public bool Validate()
            {

                if (options != overbiteOptions.correctOpenBite)
                {
                    extrudeAnteriorOnlyLowerArch = false;
                    extrudeAnteriorOnlyUpperArch = false;
                    extrudeAnteriorTeethAndIntrudePosteriorLowerArch = false;
                    extrudeAnteriorTeethAndIntrudePosteriorUpperArch = false;
                    OpenbiteOption = OpenbiteOptions.None;
                }
                if (options != overbiteOptions.correctDeepBite)
                {
                    correctDeepBiteUpperArch = false;
                    correctDeepBiteLowerArch = false;

                }

                return true;

            }

        }
        
        public class midline
        {
            public enum midlineOptions
            {
                showResultantMidlineAfterAlignment,
                maintainInitialMidline,
                improveMidlineWithIPR
            }

            public enum side
            {
                None,
                Right,
                Left
            }


            private midlineOptions _options;
            public midlineOptions options
            {
                get
                {
                    return _options;
                }
                set
                {
                    _options = value;
                }
            }


            private bool _improveMidlineWithIPRUpperArch;
            public bool improveMidlineWithIPRUpperArch
            {
                get
                {
                    return _improveMidlineWithIPRUpperArch;
                }
                set
                {
                    _improveMidlineWithIPRUpperArch = value;
                }
            }

            private side _improveMidlineWithIPRUpperArchside;
            public side improveMidlineWithIPRUpperArchside
            {
                get
                {
                    return _improveMidlineWithIPRUpperArchside;
                }
                set
                {
                    _improveMidlineWithIPRUpperArchside = value;
                }
            }

            private bool _improveMidlineWithIPRLowerArch;
            public bool improveMidlineWithIPRLowerArch
            {
                get
                {
                    return _improveMidlineWithIPRLowerArch;
                }
                set
                {
                    _improveMidlineWithIPRLowerArch = value;
                }
            }

            private side _improveMidlineWithIPRLowerArchside;
            public side improveMidlineWithIPRLowerArchside
            {
                get
                {
                    return _improveMidlineWithIPRLowerArchside;
                }
                set
                {
                    _improveMidlineWithIPRLowerArchside = value;
                }
            }


            public bool Validate()
            {

                if (options!= midlineOptions.improveMidlineWithIPR)
                {
                    improveMidlineWithIPRLowerArch = false;
                    improveMidlineWithIPRUpperArch = false;

                }

                if (!improveMidlineWithIPRLowerArch)
                    improveMidlineWithIPRLowerArchside = side.None;

                if (!improveMidlineWithIPRUpperArch)
                    improveMidlineWithIPRUpperArchside = side.None;


                if ((options == midlineOptions.improveMidlineWithIPR)&&
                    (!improveMidlineWithIPRLowerArch)&&
                    (!improveMidlineWithIPRUpperArch)) 
                    return false;


                if ((improveMidlineWithIPRLowerArch) &&
                   (improveMidlineWithIPRLowerArchside == side.None))
                    return false;

                if ((improveMidlineWithIPRUpperArch) &&
                   (improveMidlineWithIPRUpperArchside == side.None))
                    return false;

                return true;

            }
            
        }
        
        public class CrossBite
        {
            public enum CrossBiteOption
            {
                Correct,
                Maintain
            }



            private CrossBite.CrossBiteOption _options = CrossBite.CrossBiteOption.Maintain;
            public CrossBite.CrossBiteOption options
            {
                get
                {
                    return _options;
                }
                set
                {
                    _options = value;
                }
            }

            public bool Validate()
            {
                return true;
            }
        }
        
        public class DDM
        {

            public enum EncombrementEnum
            {
                Primarily,
                IfNeeded,
                None
                
            }



            public DDM()
            {
                for (int i = 0; i < 30; i++)
                    SpaceArray[i] = 0;
            }

            private bool _SpaceCloseAll = true;
            public bool SpaceCloseAll
            {
                get
                {
                    return _SpaceCloseAll;
                }
                set
                {
                    _SpaceCloseAll = value;
                }
            }


            private double[] _SpaceArray = new double[30];
            public double[] SpaceArray
            {
                get
                {
                    return _SpaceArray;
                }
                set
                {
                    _SpaceArray = value;
                }
            }


            private bool _NeedExtraction = false;
            public bool NeedExtraction
            {
                get
                {
                    return _NeedExtraction;
                }
                set
                {
                    _NeedExtraction = value;
                }
            }


            private bool[] _Extraction = new bool[32];
            public bool[] Extraction
            {
                get
                {
                    return _Extraction;
                }
                set
                {
                    _Extraction = value;
                }
            }


            private EncombrementEnum _upperRIPPosterieurGauche;
            public EncombrementEnum upperRIPPosterieurGauche
            {
                get
                {
                    return _upperRIPPosterieurGauche;
                }
                set
                {
                    _upperRIPPosterieurGauche = value;
                }
            }


            private EncombrementEnum _upperRIPPosterieurDroit;
            public EncombrementEnum upperRIPPosterieurDroit
            {
                get
                {
                    return _upperRIPPosterieurDroit;
                }
                set
                {
                    _upperRIPPosterieurDroit = value;
                }
            }


            private EncombrementEnum _upperRIPAnterieur;
            public EncombrementEnum upperRIPAnterieur
            {
                get
                {
                    return _upperRIPAnterieur;
                }
                set
                {
                    _upperRIPAnterieur = value;
                }
            }


            private EncombrementEnum _upperVestibuloVersion;
            public EncombrementEnum upperVestibuloVersion
            {
                get
                {
                    return _upperVestibuloVersion;
                }
                set
                {
                    _upperVestibuloVersion = value;
                }
            }
            

            private EncombrementEnum _upperExpansion;
            public EncombrementEnum upperExpansion
            {
                get
                {
                    return _upperExpansion;
                }
                set
                {
                    _upperExpansion = value;
                }
            }




            private EncombrementEnum _lowerRIPPosterieurGauche;
            public EncombrementEnum lowerRIPPosterieurGauche
            {
                get
                {
                    return _lowerRIPPosterieurGauche;
                }
                set
                {
                    _lowerRIPPosterieurGauche = value;
                }
            }


            private EncombrementEnum _lowerRIPPosterieurDroit;
            public EncombrementEnum lowerRIPPosterieurDroit
            {
                get
                {
                    return _lowerRIPPosterieurDroit;
                }
                set
                {
                    _lowerRIPPosterieurDroit = value;
                }
            }


            private EncombrementEnum _lowerRIPAnterieur;
            public EncombrementEnum lowerRIPAnterieur
            {
                get
                {
                    return _lowerRIPAnterieur;
                }
                set
                {
                    _lowerRIPAnterieur = value;
                }
            }


            private EncombrementEnum _lowerVestibuloVersion;
            public EncombrementEnum lowerVestibuloVersion
            {
                get
                {
                    return _lowerVestibuloVersion;
                }
                set
                {
                    _lowerVestibuloVersion = value;
                }
            }


            private EncombrementEnum _lowerExpansion;
            public EncombrementEnum lowerExpansion
            {
                get
                {
                    return _lowerExpansion;
                }
                set
                {
                    _lowerExpansion = value;
                }
            }

            public bool Validate()
            {
                if (!SpaceCloseAll)
                {
                    double total = 0;
                    foreach (double d in SpaceArray)
                        total+=d;

                    if (total == 0) return false;
                }

                if (NeedExtraction)
                {
                    bool total = false;
                   foreach (bool d in Extraction)
                        total |= d;

                    if (!total) return false;
                }

                return true;
            }
        
        
        }
        
        public enum InvisalignType
        {
            //ancienne valeur est Full,
            Compréhensive,
            Teen,
            Lite,
            I7,
            Finition,
            // ancienne valeur est Middle
            First,
            Vivera
        }
        
        public enum productType
        {
            VIVERA_RETAINERS,
            INVISALIGN_CLEAR_ALIGNER,
            PHASE_1_ALIGNER
        }
        public enum PatientType
        {
            Adulte,
            Teen,
            Child
        }
        private productType _tpeProduct;
        public productType tpeProduct
        {
            get
            {
                return _tpeProduct;
            }
            set
            {
                _tpeProduct = value;
            }
        }
        private PatientType _tpePatient;
        public PatientType tpePatient
        {
            get
            {
                return _tpePatient;
            }
            set
            {
                _tpePatient = value;
            }
        }
        private InvisalignType _tpePrescription;
        public InvisalignType tpePrescription
        {
            get
            {
                return _tpePrescription;
            }
            set
            {
                _tpePrescription = value;
            }
        }
        public InvisalignPrescriptionFullObj(InvisalignType tpe)
        {
            tpePrescription = tpe;
        }

        private treatArches _Etape1 = new treatArches();
        public treatArches Etape1
        {
            get
            {
                return _Etape1;
            }
            set
            {
                _Etape1 = value;
            }
        }

        private DoNotMoveTeeth _Etape2 = new DoNotMoveTeeth();
        public DoNotMoveTeeth Etape2
        {
            get
            {
                return _Etape2;
            }
            set
            {
                _Etape2 = value;
            }
        }

        private Attachements _Etape3 = new Attachements();
        public Attachements Etape3
        {
            get
            {
                return _Etape3;
            }
            set
            {
                _Etape3 = value;
            }
        }

        private TeenQuestions _teen = new TeenQuestions();
        public TeenQuestions teen
        {
            get
            {
                return _teen;
            }
            set
            {
                _teen = value;
            }
        }
        
        private aPRelation _Etape4 = new aPRelation();
        public aPRelation Etape4
        {
            get
            {
                return _Etape4;
            }
            set
            {
                _Etape4 = value;
            }
        }

        private overJet _Etape5 = new overJet();
        public overJet Etape5
        {
            get
            {
                return _Etape5;
            }
            set
            {
                _Etape5 = value;
            }
        }

        private overbite _Etape6 = new overbite();
        public overbite Etape6
        {
            get
            {
                return _Etape6;
            }
            set
            {
                _Etape6 = value;
            }
        }

        private Ramps _Etape7 = new Ramps();
        public Ramps Etape7
        {
            get
            {
                return _Etape7;
            }
            set
            {
                _Etape7 = value;
            }
        }
        
        private midline _Etape8 = new midline();
        public midline Etape8
        {
            get
            {
                return _Etape8;
            }
            set
            {
                _Etape8 = value;
            }
        }
        
        private CrossBite _Etape9 = new CrossBite();
        public CrossBite Etape9
        {
            get
            {
                return _Etape9;
            }
            set
            {
                _Etape9 = value;
            }
        }
                
        private DDM _Etape10 = new DDM();
        public DDM Etape10
        {
            get
            {
                return _Etape10;
            }
            set
            {
                _Etape10 = value;
            }
        }
        
        private string _Etape11_SpecialInstruction;
        public string Etape11_SpecialInstruction
        {
            get 
            {
                return _Etape11_SpecialInstruction;
            }
            set
            {
                _Etape11_SpecialInstruction = value;
            }
        }
        
        
    }
}
