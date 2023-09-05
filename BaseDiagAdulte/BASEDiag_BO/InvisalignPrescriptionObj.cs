using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
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
            Full,
            Teen,
            Lite,
            I7,
            Vivera
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
        
        private midline _Etape7 = new midline();
        public midline Etape7
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
        
        private CrossBite _Etape8 = new CrossBite();
        public CrossBite Etape8
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
                
        private DDM _Etape9 = new DDM();
        public DDM Etape9
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
        
        private string _Etape10_SpecialInstruction;
        public string Etape10_SpecialInstruction
        {
            get 
            {
                return _Etape10_SpecialInstruction;
            }
            set
            {
                _Etape10_SpecialInstruction = value;
            }
        }
        
        
    }
}
