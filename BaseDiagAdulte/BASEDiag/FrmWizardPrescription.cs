using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;
using BASEDiag_BL;
using BaseCommonControls;
using BasCommon_BO;

namespace BASEDiagAdulte
{
    public partial class FrmWizardPrescription : Form
    {

        
        private InvisalignPrescriptionFullObj _CurrentPrescription;
        public InvisalignPrescriptionFullObj CurrentPrescription
        {
            get
            {
                return _CurrentPrescription;
            }
            set
            {
                _CurrentPrescription = value;
            }
        }



        private double[] _espacementdent = null;
        public double[] espacementdent
        {
            get
            {
                return _espacementdent;
            }
            set
            {
                _espacementdent = value;
            }
        }
        
        

        public FrmWizardPrescription(InvisalignPrescriptionFullObj obj)
        {
            CurrentPrescription = obj;
            InitializeComponent();
        }

        private void FrmWizardPrescription_Load(object sender, EventArgs e)
        {
            InitDisplay();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void InitDisplay()
        {

            if (CurrentPrescription == null) return;
            //////////////////////////////Choix Traitement/////////////////////////////////////
           // rbTraitementFull.Checked = CurrentPrescription.tpePrescription == InvisalignPrescriptionFullObj.InvisalignType.Full;
           // rbTraitementLite.Checked = CurrentPrescription.tpePrescription == InvisalignPrescriptionFullObj.InvisalignType.Lite;
           // rbTraitementTeen.Checked=CurrentPrescription.tpePrescription == InvisalignPrescriptionFullObj.InvisalignType.Teen;
           // rbTraitementi7.Checked=CurrentPrescription.tpePrescription == InvisalignPrescriptionFullObj.InvisalignType.I7;


            //////////////////////////////Traitement Arche/////////////////////////////////////

            treatArches_both.Checked=CurrentPrescription.Etape1.TypeTraitement == InvisalignPrescriptionFullObj.treatArches.TypeTraitementArcade.both;
            treatArches_upperOnly.Checked=CurrentPrescription.Etape1.TypeTraitement == InvisalignPrescriptionFullObj.treatArches.TypeTraitementArcade.upperOnly;
            treatArches_lowerOnly.Checked=CurrentPrescription.Etape1.TypeTraitement == InvisalignPrescriptionFullObj.treatArches.TypeTraitementArcade.lowerOnly;


            treatArches_lowerOnly_diagnosticSetup.Checked=CurrentPrescription.Etape1.lowerOnlyDiagnosticSetup;

            treatArches_upperOnly_diagnosticSetup.Checked=CurrentPrescription.Etape1.upperOnlyDiagnosticSetup;

            //////////////////////////////Restriction des mouvements/////////////////////////////////////

            BooleanToChoixDent(CurrentPrescription.Etape2.DoNotMoveTheseTeeth,doNotMoveTeeth_theseTeeth_tooth);

            //////////////////////////////Taquets/////////////////////////////////////
           
            BooleanToChoixDent(CurrentPrescription.Etape3.TeethPermittedForAttachement, teethPermittedForAttachments_doNotPutTheseTeeth_tooth);

            //////////////////////////////4. Rapport A-P/////////////////////////////////////Comprehensive Package

            aPRelation_maintainRight.Checked=CurrentPrescription.Etape4.aPRelationRight == InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.maintain;
            aPRelation_maintainLeft.Checked=CurrentPrescription.Etape4.aPRelationLeft == InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.maintain;

            aPRelation_improveToClass1CanineOnlyRight.Checked=CurrentPrescription.Etape4.aPRelationRight == InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.improveToClass1CanineOnly;
            aPRelation_improveToClass1CanineOnlyLeft.Checked=CurrentPrescription.Etape4.aPRelationLeft == InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.improveToClass1CanineOnly;

            aPRelation_partialClass1CanineOnlyRight.Checked = CurrentPrescription.Etape4.aPRelationRight == InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.partialClass1CanineOnly;
            aPRelation_partialClass1CanineOnlyLeft.Checked = CurrentPrescription.Etape4.aPRelationLeft == InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.partialClass1CanineOnly;

            aPRelation_class1CanineAndMolarRight.Checked = CurrentPrescription.Etape4.aPRelationRight == InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.class1CanineAndMolar;
            aPRelation_class1CanineAndMolarLeft.Checked = CurrentPrescription.Etape4.aPRelationLeft == InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.class1CanineAndMolar;


            APRelation_tooth_movement.Checked = CurrentPrescription.Etape4.options == InvisalignPrescriptionFullObj.aPRelation.Options.ToothMovement;
            APRelation_surgical.Checked = CurrentPrescription.Etape4.options == InvisalignPrescriptionFullObj.aPRelation.Options.Surgical;
            
            APRelation_distalization.Checked = CurrentPrescription.Etape4.distalization;
            APRelation_classIIOrIIICorrectionSimulation.Checked = CurrentPrescription.Etape4.classIIOrIIICorrectionSimulation;
            APRelation_posteriorIPR.Checked = CurrentPrescription.Etape4.PosteriorIPR;

            APRelation_classIIOrIIICorrectionSimulation_alignerCutsYes.Checked = CurrentPrescription.Etape4.classIIOrIIICorrectionSimulationPrecisionCut == InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.Yes;
            APRelation_classIIOrIIICorrectionSimulation_alignerCutsNo.Checked = CurrentPrescription.Etape4.classIIOrIIICorrectionSimulationPrecisionCut == InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.No;

            APRelation_distalization_alignerCutsYes.Checked = CurrentPrescription.Etape4.distalizationPrecisionCut == InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.Yes;
            APRelation_distalization_alignerCutsNo.Checked = CurrentPrescription.Etape4.distalizationPrecisionCut == InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.No;


            //////////////////////////////5. Surplomb/////////////////////////////////////

            overjet_showResultantOverjetAfterAlignment.Checked=CurrentPrescription.Etape5.options == InvisalignPrescriptionFullObj.overJet.overJetOptions.showResultantOverjetAfterAlignment;
            overjet_maintainInitialOverjet.Checked=CurrentPrescription.Etape5.options == InvisalignPrescriptionFullObj.overJet.overJetOptions.maintainInitialOverjet;
            overjet_improveResultingOverjetWithIPR.Checked=CurrentPrescription.Etape5.options == InvisalignPrescriptionFullObj.overJet.overJetOptions.improveResultingOverjetWithIPR;

            //////////////////////////////6. Recouvrement/////////////////////////////////////

            overbite_maintainResultant.Checked=CurrentPrescription.Etape6.options == InvisalignPrescriptionFullObj.overbite.overbiteOptions.maintainResultant;
            overbite_maintainInitial.Checked=CurrentPrescription.Etape6.options == InvisalignPrescriptionFullObj.overbite.overbiteOptions.maintainInitial;
            overbite_correctOpenBiteOptions.Checked=CurrentPrescription.Etape6.options == InvisalignPrescriptionFullObj.overbite.overbiteOptions.correctOpenBite;
            overbite_correctDeepBite.Checked=CurrentPrescription.Etape6.options == InvisalignPrescriptionFullObj.overbite.overbiteOptions.correctDeepBite;

            overbite_correctOpenBiteOptions_extrudeAnteriorOnly.Checked=CurrentPrescription.Etape6.OpenbiteOption == InvisalignPrescriptionFullObj.overbite.OpenbiteOptions.extrudeAnteriorOnly;
            overbite_correctOpenBiteOptions_extrudeAnteriorTeethAndIntrudePosterior.Checked=CurrentPrescription.Etape6.OpenbiteOption == InvisalignPrescriptionFullObj.overbite.OpenbiteOptions.extrudeAnteriorTeethAndIntrudePosterior;
            overbite_correctOpenBiteOptions_other.Checked=CurrentPrescription.Etape6.OpenbiteOption == InvisalignPrescriptionFullObj.overbite.OpenbiteOptions.other;

            overbite_correctOpenBiteOptions_extrudeAnteriorOnlyUpper.Checked = CurrentPrescription.Etape6.extrudeAnteriorOnlyUpperArch;
            overbite_correctOpenBiteOptions_extrudeAnteriorOnlyLower.Checked=CurrentPrescription.Etape6.extrudeAnteriorOnlyLowerArch;
            overbite_correctOpenBiteOptions_extrudeAnteriorTeethAndIntrudePosteriorUpper.Checked=CurrentPrescription.Etape6.extrudeAnteriorTeethAndIntrudePosteriorUpperArch;
            overbite_correctOpenBiteOptions_extrudeAnteriorTeethAndIntrudePosteriorLower.Checked=CurrentPrescription.Etape6.extrudeAnteriorTeethAndIntrudePosteriorLowerArch;


            overbite_correctDeepBiteUpper.Checked=CurrentPrescription.Etape6.correctDeepBiteUpperArch;
            overbite_correctDeepBiteLower.Checked=CurrentPrescription.Etape6.correctDeepBiteLowerArch;

            //////////////////////////////7. Rampes/////////////////////////////////////

            biteRampsUpper_none.Checked = CurrentPrescription.Etape7.biteRampsUpperOpt == InvisalignPrescriptionFullObj.Ramps.biteRampsUpperOptions.None;
            biteRampsUpper_biteRampsUpperOptions.Checked = CurrentPrescription.Etape7.biteRampsUpperOpt == InvisalignPrescriptionFullObj.Ramps.biteRampsUpperOptions.biteRampsUpperOptions;

            biteRampsUpper_biteRampsUpperOptions_incisors.Checked = CurrentPrescription.Etape7.biteRampsUpperOptOn == InvisalignPrescriptionFullObj.Ramps.biteRampsUpperOptionsOn.incisors;
            biteRampsUpper_biteRampsUpperOptions_canines.Checked = CurrentPrescription.Etape7.biteRampsUpperOptOn == InvisalignPrescriptionFullObj.Ramps.biteRampsUpperOptionsOn.canines;

            biteRampsUpper_biteRampsUpperOptions_incisors_centralIncisors.Checked = CurrentPrescription.Etape7.centralIncisors;
            biteRampsUpper_biteRampsUpperOptions_incisors_lateralIncisors.Checked = CurrentPrescription.Etape7.lateralIncisors;


            //////////////////////////////8. Milieux/////////////////////////////////////

            midline_showResultantMidlineAfterAlignment.Checked=CurrentPrescription.Etape8.options == InvisalignPrescriptionFullObj.midline.midlineOptions.showResultantMidlineAfterAlignment;
            midline_maintainInitialMidline.Checked=CurrentPrescription.Etape8.options == InvisalignPrescriptionFullObj.midline.midlineOptions.maintainInitialMidline;
            midline_improveMidlineWithIPR.Checked=CurrentPrescription.Etape8.options == InvisalignPrescriptionFullObj.midline.midlineOptions.improveMidlineWithIPR;

            midline_improveMidlineWithIPRUpper.Checked = CurrentPrescription.Etape8.improveMidlineWithIPRUpperArch;
            midline_improveMidlineWithIPRLower.Checked = CurrentPrescription.Etape8.improveMidlineWithIPRLowerArch;

           midline_improveMidlineWithIPRUpper_patientsRight.Checked=CurrentPrescription.Etape8.improveMidlineWithIPRUpperArchside == InvisalignPrescriptionFullObj.midline.side.Right;
           midline_improveMidlineWithIPRUpper_patientsLeft.Checked=CurrentPrescription.Etape8.improveMidlineWithIPRUpperArchside == InvisalignPrescriptionFullObj.midline.side.Left;

           midline_improveMidlineWithIPRLower_patientsRight.Checked=CurrentPrescription.Etape8.improveMidlineWithIPRLowerArchside == InvisalignPrescriptionFullObj.midline.side.Right;
           midline_improveMidlineWithIPRLower_patientsLeft.Checked=CurrentPrescription.Etape8.improveMidlineWithIPRLowerArchside == InvisalignPrescriptionFullObj.midline.side.Left;

            //////////////////////////////9. Articulé croisé postérieur/////////////////////////////////////
            posteriorCrossbite_correct.Checked=CurrentPrescription.Etape9.options == InvisalignPrescriptionFullObj.CrossBite.CrossBiteOption.Correct;
            posteriorCrossbite_maintain.Checked=CurrentPrescription.Etape9.options == InvisalignPrescriptionFullObj.CrossBite.CrossBiteOption.Maintain;


            espacementdent = CurrentPrescription.Etape10.SpaceArray;

            //////////////////////////////10. Espacement et Encombrement (DDM)/////////////////////////////////////
            spaces_leaveSpaces.Checked = !CurrentPrescription.Etape10.SpaceCloseAll ;
            spaces_closeAll.Checked = CurrentPrescription.Etape10.SpaceCloseAll;

            upperExpandPrimarily.Checked=CurrentPrescription.Etape10.upperExpansion == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            upperExpandIfNeeded.Checked=CurrentPrescription.Etape10.upperExpansion == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            upperExpandNone.Checked=CurrentPrescription.Etape10.upperExpansion == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;


            upperProclinePrimarily.Checked=CurrentPrescription.Etape10.upperVestibuloVersion == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            upperProclineIfNeeded.Checked=CurrentPrescription.Etape10.upperVestibuloVersion == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            upperProclineNone.Checked=CurrentPrescription.Etape10.upperVestibuloVersion == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            upperAnteriorPrimarily.Checked=CurrentPrescription.Etape10.upperRIPAnterieur == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            upperAnteriorIfNeeded.Checked=CurrentPrescription.Etape10.upperRIPAnterieur == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            upperAnteriorNone.Checked=CurrentPrescription.Etape10.upperRIPAnterieur == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            upperPosteriorRightPrimarily.Checked=CurrentPrescription.Etape10.upperRIPPosterieurDroit == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            upperPosteriorRightIfNeeded.Checked=CurrentPrescription.Etape10.upperRIPPosterieurDroit == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            upperPosteriorRightNone.Checked=CurrentPrescription.Etape10.upperRIPPosterieurDroit == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;


            upperPosteriorLeftPrimarly.Checked=CurrentPrescription.Etape10.upperRIPPosterieurGauche == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            upperPosteriorLeftIfNeeded.Checked=CurrentPrescription.Etape10.upperRIPPosterieurGauche == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            upperPosteriorLeftNone.Checked=CurrentPrescription.Etape10.upperRIPPosterieurGauche == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;






            lowerExpandPrimarily.Checked=CurrentPrescription.Etape10.lowerExpansion == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            lowerExpandIfNeeded.Checked=CurrentPrescription.Etape10.lowerExpansion == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            lowerExpandIfNone.Checked=CurrentPrescription.Etape10.lowerExpansion == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;


            lowerProclinePrimarily.Checked=CurrentPrescription.Etape10.lowerVestibuloVersion == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            lowerProclineIfNeeded.Checked=CurrentPrescription.Etape10.lowerVestibuloVersion == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            lowerProclineNone.Checked=CurrentPrescription.Etape10.lowerVestibuloVersion == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            lowerAnteriorPrimarily.Checked=CurrentPrescription.Etape10.lowerRIPAnterieur == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            lowerAnteriorIfNeeded.Checked=CurrentPrescription.Etape10.lowerRIPAnterieur == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            lowerAnteriorNone.Checked=CurrentPrescription.Etape10.lowerRIPAnterieur == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            lowerPosteriorRightPrimarily.Checked=CurrentPrescription.Etape10.lowerRIPPosterieurDroit == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            lowerPosteriorRightIfNeeded.Checked=CurrentPrescription.Etape10.lowerRIPPosterieurDroit == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            lowerPosteriorRightNone.Checked=CurrentPrescription.Etape10.lowerRIPPosterieurDroit == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;


            lowerPosteriorLeftPrimarily.Checked=CurrentPrescription.Etape10.lowerRIPPosterieurGauche == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            lowerPosteriorLeftIfNeeded.Checked=CurrentPrescription.Etape10.lowerRIPPosterieurGauche == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            lowerPosteriorLeftIfNone.Checked=CurrentPrescription.Etape10.lowerRIPPosterieurGauche == InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;


           BooleanToChoixDent(CurrentPrescription.Etape10.Extraction, extraction_theseTeeth_tooth);

            //////////////////////////////11. Instructions speciales/////////////////////////////////////

           SpecInstructions.Text = CurrentPrescription.Etape11_SpecialInstruction;
        }

        private bool Build()
        {


            //////////////////////////////Choix Traitement/////////////////////////////////////
                CurrentPrescription.tpeProduct = InvisalignPrescriptionFullObj.productType.INVISALIGN_CLEAR_ALIGNER;
                if (rbTraitementFull.Checked || rdFirst.Checked)
                    CurrentPrescription.tpePrescription = InvisalignPrescriptionFullObj.InvisalignType.Compréhensive;
                if (rbTraitementLite.Checked)
                    CurrentPrescription.tpePrescription = InvisalignPrescriptionFullObj.InvisalignType.Lite;
                if (rbTraitementTeen.Checked)
                    CurrentPrescription.tpePrescription = InvisalignPrescriptionFullObj.InvisalignType.Teen;
                if (rbTraitementi7.Checked)
                    CurrentPrescription.tpePrescription = InvisalignPrescriptionFullObj.InvisalignType.I7;
            if(rdAdulte.Checked)
                CurrentPrescription.tpePatient = InvisalignPrescriptionFullObj.PatientType.Adulte;
            if(rdTeen.Checked)
                CurrentPrescription.tpePatient = InvisalignPrescriptionFullObj.PatientType.Teen;
            if (rdChild.Checked)
                CurrentPrescription.tpePatient = InvisalignPrescriptionFullObj.PatientType.Child;

            //////////////////////////////Traitement Arche/////////////////////////////////////

            if (treatArches_both.Checked)
                CurrentPrescription.Etape1.TypeTraitement = InvisalignPrescriptionFullObj.treatArches.TypeTraitementArcade.both;
            if (treatArches_upperOnly.Checked)
                CurrentPrescription.Etape1.TypeTraitement = InvisalignPrescriptionFullObj.treatArches.TypeTraitementArcade.upperOnly;
            if (treatArches_lowerOnly.Checked)
                CurrentPrescription.Etape1.TypeTraitement = InvisalignPrescriptionFullObj.treatArches.TypeTraitementArcade.lowerOnly;


            if (treatArches_lowerOnly_diagnosticSetup.Checked)
               CurrentPrescription.Etape1.lowerOnlyDiagnosticSetup = true;
                


            if (treatArches_upperOnly_diagnosticSetup.Checked)
                CurrentPrescription.Etape1.upperOnlyDiagnosticSetup = true;
            
            //////////////////////////////Restriction des mouvements/////////////////////////////////////

            CurrentPrescription.Etape2.DoNotMoveAnyTeeth = String.IsNullOrEmpty(doNotMoveTeeth_theseTeeth_tooth.SelectedDents);
            CurrentPrescription.Etape2.DoNotMoveTheseTeeth = ChoixDentToBoolean(doNotMoveTeeth_theseTeeth_tooth);

            //////////////////////////////Taquets/////////////////////////////////////
            CurrentPrescription.Etape3.TeethPermittedForAttachements = String.IsNullOrEmpty(teethPermittedForAttachments_doNotPutTheseTeeth_tooth.SelectedDents);
            CurrentPrescription.Etape3.TeethPermittedForAttachement = ChoixDentToBoolean(teethPermittedForAttachments_doNotPutTheseTeeth_tooth);

            //////////////////////////////4. Rapport A-P/////////////////////////////////////

            if (aPRelation_maintainRight.Checked)
                CurrentPrescription.Etape4.aPRelationRight = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.maintain;
            if (aPRelation_maintainLeft.Checked)
                CurrentPrescription.Etape4.aPRelationLeft = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.maintain;

            if (aPRelation_improveToClass1CanineOnlyRight.Checked)
                CurrentPrescription.Etape4.aPRelationRight = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.improveToClass1CanineOnly;
            if (aPRelation_improveToClass1CanineOnlyLeft.Checked)
                CurrentPrescription.Etape4.aPRelationLeft = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.improveToClass1CanineOnly;

            if (aPRelation_partialClass1CanineOnlyRight.Checked)
                CurrentPrescription.Etape4.aPRelationRight = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.partialClass1CanineOnly;
            if (aPRelation_partialClass1CanineOnlyLeft.Checked)
                CurrentPrescription.Etape4.aPRelationLeft = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.partialClass1CanineOnly;

            if (aPRelation_class1CanineAndMolarRight.Checked)
                CurrentPrescription.Etape4.aPRelationRight = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.class1CanineAndMolar;
            if (aPRelation_class1CanineAndMolarLeft.Checked)
                CurrentPrescription.Etape4.aPRelationLeft = InvisalignPrescriptionFullObj.aPRelation.TraitmntaPRelation.class1CanineAndMolar;

            if (APRelation_tooth_movement.Checked)
            {
                CurrentPrescription.Etape4.options = InvisalignPrescriptionFullObj.aPRelation.Options.ToothMovement;

                CurrentPrescription.Etape4.PosteriorIPR = APRelation_posteriorIPR.Checked;
                CurrentPrescription.Etape4.classIIOrIIICorrectionSimulation = APRelation_classIIOrIIICorrectionSimulation.Checked;
                CurrentPrescription.Etape4.distalization = APRelation_distalization.Checked;

                CurrentPrescription.Etape4.classIIOrIIICorrectionSimulationPrecisionCut = APRelation_classIIOrIIICorrectionSimulation_alignerCutsYes.Checked ? InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.Yes : InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.No;
                CurrentPrescription.Etape4.distalizationPrecisionCut = APRelation_classIIOrIIICorrectionSimulation_alignerCutsYes.Checked ? InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.Yes : InvisalignPrescriptionFullObj.aPRelation.EnumYeNo.No;
            }


            if (APRelation_surgical.Checked)
                CurrentPrescription.Etape4.options = InvisalignPrescriptionFullObj.aPRelation.Options.Surgical;



            //////////////////////////////5. Surplomb/////////////////////////////////////

            if (overjet_showResultantOverjetAfterAlignment.Checked)
                CurrentPrescription.Etape5.options = InvisalignPrescriptionFullObj.overJet.overJetOptions.showResultantOverjetAfterAlignment;
            if (overjet_maintainInitialOverjet.Checked)
                CurrentPrescription.Etape5.options = InvisalignPrescriptionFullObj.overJet.overJetOptions.maintainInitialOverjet;
            if (overjet_improveResultingOverjetWithIPR.Checked)
                CurrentPrescription.Etape5.options = InvisalignPrescriptionFullObj.overJet.overJetOptions.improveResultingOverjetWithIPR;

            //////////////////////////////6. Recouvrement/////////////////////////////////////

            if (overbite_maintainResultant.Checked)
                CurrentPrescription.Etape6.options = InvisalignPrescriptionFullObj.overbite.overbiteOptions.maintainResultant;
            if (overbite_maintainInitial.Checked)
                CurrentPrescription.Etape6.options = InvisalignPrescriptionFullObj.overbite.overbiteOptions.maintainInitial;
            if (overbite_correctOpenBiteOptions.Checked)
                CurrentPrescription.Etape6.options = InvisalignPrescriptionFullObj.overbite.overbiteOptions.correctOpenBite;
            if (overbite_correctDeepBite.Checked)
                CurrentPrescription.Etape6.options = InvisalignPrescriptionFullObj.overbite.overbiteOptions.correctDeepBite;

            CurrentPrescription.Etape6.OpenbiteOption = InvisalignPrescriptionFullObj.overbite.OpenbiteOptions.None;
            if (overbite_correctOpenBiteOptions_extrudeAnteriorOnly.Checked)
                CurrentPrescription.Etape6.OpenbiteOption = InvisalignPrescriptionFullObj.overbite.OpenbiteOptions.extrudeAnteriorOnly;
            if (overbite_correctOpenBiteOptions_extrudeAnteriorTeethAndIntrudePosterior.Checked)
                CurrentPrescription.Etape6.OpenbiteOption = InvisalignPrescriptionFullObj.overbite.OpenbiteOptions.extrudeAnteriorTeethAndIntrudePosterior;
            if (overbite_correctOpenBiteOptions_other.Checked)
                CurrentPrescription.Etape6.OpenbiteOption = InvisalignPrescriptionFullObj.overbite.OpenbiteOptions.other;

            CurrentPrescription.Etape6.extrudeAnteriorOnlyUpperArch = overbite_correctOpenBiteOptions_extrudeAnteriorOnlyUpper.Checked;
            CurrentPrescription.Etape6.extrudeAnteriorOnlyLowerArch = overbite_correctOpenBiteOptions_extrudeAnteriorOnlyLower.Checked;
            CurrentPrescription.Etape6.extrudeAnteriorTeethAndIntrudePosteriorUpperArch = overbite_correctOpenBiteOptions_extrudeAnteriorTeethAndIntrudePosteriorUpper.Checked;
            CurrentPrescription.Etape6.extrudeAnteriorTeethAndIntrudePosteriorLowerArch = overbite_correctOpenBiteOptions_extrudeAnteriorTeethAndIntrudePosteriorLower.Checked;


            CurrentPrescription.Etape6.correctDeepBiteUpperArch = overbite_correctDeepBiteUpper.Checked;
            CurrentPrescription.Etape6.correctDeepBiteLowerArch = overbite_correctDeepBiteLower.Checked;


            //////////////////////////////7. Rampes/////////////////////////////////////

            if (biteRampsUpper_none.Checked)
                CurrentPrescription.Etape7.biteRampsUpperOpt = InvisalignPrescriptionFullObj.Ramps.biteRampsUpperOptions.None;

            if (biteRampsUpper_biteRampsUpperOptions.Checked)
                CurrentPrescription.Etape7.biteRampsUpperOpt = InvisalignPrescriptionFullObj.Ramps.biteRampsUpperOptions.biteRampsUpperOptions;

            if (biteRampsUpper_biteRampsUpperOptions_incisors.Checked)
                CurrentPrescription.Etape7.biteRampsUpperOptOn = InvisalignPrescriptionFullObj.Ramps.biteRampsUpperOptionsOn.incisors;

            if (biteRampsUpper_biteRampsUpperOptions_canines.Checked)
                CurrentPrescription.Etape7.biteRampsUpperOptOn = InvisalignPrescriptionFullObj.Ramps.biteRampsUpperOptionsOn.canines;


            CurrentPrescription.Etape7.centralIncisors = biteRampsUpper_biteRampsUpperOptions_incisors_centralIncisors.Checked;
            CurrentPrescription.Etape7.lateralIncisors = biteRampsUpper_biteRampsUpperOptions_incisors_lateralIncisors.Checked;



            //////////////////////////////8. Milieux/////////////////////////////////////

            if (midline_showResultantMidlineAfterAlignment.Checked)
                CurrentPrescription.Etape8.options = InvisalignPrescriptionFullObj.midline.midlineOptions.showResultantMidlineAfterAlignment;
            if (midline_maintainInitialMidline.Checked)
                CurrentPrescription.Etape8.options = InvisalignPrescriptionFullObj.midline.midlineOptions.maintainInitialMidline;
            if (midline_improveMidlineWithIPR.Checked)
                CurrentPrescription.Etape8.options = InvisalignPrescriptionFullObj.midline.midlineOptions.improveMidlineWithIPR;

            CurrentPrescription.Etape8.improveMidlineWithIPRUpperArch = midline_improveMidlineWithIPRUpper.Checked;
            CurrentPrescription.Etape8.improveMidlineWithIPRLowerArch = midline_improveMidlineWithIPRLower.Checked;

            if (midline_improveMidlineWithIPRUpper_patientsRight.Checked)
                CurrentPrescription.Etape8.improveMidlineWithIPRUpperArchside = InvisalignPrescriptionFullObj.midline.side.Right;
            if (midline_improveMidlineWithIPRUpper_patientsLeft.Checked)
                CurrentPrescription.Etape8.improveMidlineWithIPRUpperArchside = InvisalignPrescriptionFullObj.midline.side.Left;

            if (midline_improveMidlineWithIPRLower_patientsRight.Checked)
                CurrentPrescription.Etape8.improveMidlineWithIPRLowerArchside = InvisalignPrescriptionFullObj.midline.side.Right;
            if (midline_improveMidlineWithIPRLower_patientsLeft.Checked)
                CurrentPrescription.Etape8.improveMidlineWithIPRLowerArchside = InvisalignPrescriptionFullObj.midline.side.Left;

            //////////////////////////////9. Articulé croisé postérieur/////////////////////////////////////
            if (posteriorCrossbite_correct.Checked)
                CurrentPrescription.Etape9.options = InvisalignPrescriptionFullObj.CrossBite.CrossBiteOption.Maintain;
            if (posteriorCrossbite_maintain.Checked)
                CurrentPrescription.Etape9.options = InvisalignPrescriptionFullObj.CrossBite.CrossBiteOption.Correct;

            //////////////////////////////10. Espacement et Encombrement (DDM)/////////////////////////////////////
            CurrentPrescription.Etape10.SpaceCloseAll = spaces_closeAll.Checked;
            CurrentPrescription.Etape10.SpaceArray = espacementdent;

            

            if (upperExpandPrimarily.Checked)
                CurrentPrescription.Etape10.upperExpansion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (upperExpandIfNeeded.Checked)
                CurrentPrescription.Etape10.upperExpansion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (upperExpandNone.Checked)
                CurrentPrescription.Etape10.upperExpansion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;


            if (upperProclinePrimarily.Checked)
                CurrentPrescription.Etape10.upperVestibuloVersion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (upperProclineIfNeeded.Checked)
                CurrentPrescription.Etape10.upperVestibuloVersion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (upperProclineNone.Checked)
                CurrentPrescription.Etape10.upperVestibuloVersion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            if (upperAnteriorPrimarily.Checked)
                CurrentPrescription.Etape10.upperRIPAnterieur = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (upperAnteriorIfNeeded.Checked)
                CurrentPrescription.Etape10.upperRIPAnterieur = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (upperAnteriorNone.Checked)
                CurrentPrescription.Etape10.upperRIPAnterieur = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            if (upperPosteriorRightPrimarily.Checked)
                CurrentPrescription.Etape10.upperRIPPosterieurDroit = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (upperPosteriorRightIfNeeded.Checked)
                CurrentPrescription.Etape10.upperRIPPosterieurDroit = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (upperPosteriorRightNone.Checked)
                CurrentPrescription.Etape10.upperRIPPosterieurDroit = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;


            if (upperPosteriorLeftPrimarly.Checked)
                CurrentPrescription.Etape10.upperRIPPosterieurGauche = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (upperPosteriorLeftIfNeeded.Checked)
                CurrentPrescription.Etape10.upperRIPPosterieurGauche = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (upperPosteriorLeftNone.Checked)
                CurrentPrescription.Etape10.upperRIPPosterieurGauche = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;






            if (lowerExpandPrimarily.Checked)
                CurrentPrescription.Etape10.lowerExpansion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (lowerExpandIfNeeded.Checked)
                CurrentPrescription.Etape10.lowerExpansion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (lowerExpandIfNone.Checked)
                CurrentPrescription.Etape10.lowerExpansion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;


            if (lowerProclinePrimarily.Checked)
                CurrentPrescription.Etape10.lowerVestibuloVersion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (lowerProclineIfNeeded.Checked)
                CurrentPrescription.Etape10.lowerVestibuloVersion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (lowerProclineNone.Checked)
                CurrentPrescription.Etape10.lowerVestibuloVersion = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            if (lowerAnteriorPrimarily.Checked)
                CurrentPrescription.Etape10.lowerRIPAnterieur = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (lowerAnteriorIfNeeded.Checked)
                CurrentPrescription.Etape10.lowerRIPAnterieur = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (lowerAnteriorNone.Checked)
                CurrentPrescription.Etape10.lowerRIPAnterieur = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;

            if (lowerPosteriorRightPrimarily.Checked)
                CurrentPrescription.Etape10.lowerRIPPosterieurDroit = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (lowerPosteriorRightIfNeeded.Checked)
                CurrentPrescription.Etape10.lowerRIPPosterieurDroit = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (lowerPosteriorRightNone.Checked)
                CurrentPrescription.Etape10.lowerRIPPosterieurDroit = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;


            if (lowerPosteriorLeftPrimarily.Checked)
                CurrentPrescription.Etape10.lowerRIPPosterieurGauche = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.Primarily;
            if (lowerPosteriorLeftIfNeeded.Checked)
                CurrentPrescription.Etape10.lowerRIPPosterieurGauche = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.IfNeeded;
            if (lowerPosteriorLeftIfNone.Checked)
                CurrentPrescription.Etape10.lowerRIPPosterieurGauche = InvisalignPrescriptionFullObj.DDM.EncombrementEnum.None;


            CurrentPrescription.Etape10.NeedExtraction = !string.IsNullOrEmpty(extraction_theseTeeth_tooth.SelectedDents);
            CurrentPrescription.Etape10.Extraction = ChoixDentToBoolean(extraction_theseTeeth_tooth);


            //////////////////////////////11. Instructions speciales/////////////////////////////////////

            CurrentPrescription.Etape11_SpecialInstruction = SpecInstructions.Text;

            string erreur = "";
            if (!CurrentPrescription.Etape1.Validate())
                erreur += ("Erreur etape 1");
            if (!CurrentPrescription.Etape2.Validate())
                erreur += ("Erreur etape 2");
            if (!CurrentPrescription.Etape3.Validate())
                erreur += ("Erreur etape 3");
            if (!CurrentPrescription.Etape4.Validate())
                erreur += ("Erreur etape 4");
            if (!CurrentPrescription.Etape5.Validate())
                erreur += ("Erreur etape 5");
            if (!CurrentPrescription.Etape6.Validate())
                erreur += ("Erreur etape 6");
            if (!CurrentPrescription.Etape8.Validate())
                erreur += ("Erreur etape 7");
            if (!CurrentPrescription.Etape9.Validate())
                erreur += ("Erreur etape 8");
            if (!CurrentPrescription.Etape10.Validate())
                erreur += ("Erreur etape 9");

            if (!string.IsNullOrEmpty(erreur))
            {
                MessageBox.Show(erreur);
                return false;
            }
            else
                return true;
        }



        private void  BooleanToChoixDent(bool[] arr, ChoixDents chxds)
        {
           
            List<string> lst = new List<string>();


             if (arr[0]) lst.Add("18");
             if (arr[1]) lst.Add("17");
             if (arr[2]) lst.Add("16");
             if (arr[3]) lst.Add("15");
             if (arr[4]) lst.Add("14");
             if (arr[5]) lst.Add("13");
             if (arr[6]) lst.Add("12");
             if (arr[7]) lst.Add("11");

             if (arr[8]) lst.Add("21");
             if (arr[9]) lst.Add("22");
             if (arr[10]) lst.Add("23");
             if (arr[11]) lst.Add("24");
             if (arr[12]) lst.Add("25");
             if (arr[13]) lst.Add("26");
             if (arr[14]) lst.Add("27");
             if (arr[15]) lst.Add("28");

             if (arr[16]) lst.Add("38");
             if (arr[17]) lst.Add("37");
             if (arr[18]) lst.Add("36");
             if (arr[19]) lst.Add("35");
             if (arr[20]) lst.Add("34");
             if (arr[21]) lst.Add("33");
             if (arr[22]) lst.Add("32");
             if (arr[23]) lst.Add("31");

             if (arr[24]) lst.Add("41");
             if (arr[25]) lst.Add("42");
             if (arr[26]) lst.Add("43");
             if (arr[27]) lst.Add("44");
             if (arr[28]) lst.Add("45");
             if (arr[29]) lst.Add("46");
             if (arr[30]) lst.Add("47");
             if (arr[31]) lst.Add("48");

             string res = "";

             foreach (string s in lst)
             {
                 if (!string.IsNullOrEmpty(res))
                     res += chxds.separator;
                 res += s;
                 
             }

             chxds.SelectedDents = res;
 
        }


        private bool[] ChoixDentToBoolean(BaseCommonControls.ChoixDents chxds)
        {
            bool[] arrays = new bool[32];
            string[] dents = chxds.SelectedDents.Split(chxds.separator);

            return ChoixDentToBoolean(dents);
        }

        public static bool[] ChoixDentToBoolean(string[] dents)
        {
            bool[] arrays = new bool[32];
           
            arrays[0] = dents.Contains("18");
            arrays[1] = dents.Contains("17");
            arrays[2] = dents.Contains("16");
            arrays[3] = dents.Contains("15") || dents.Contains("55");
            arrays[4] = dents.Contains("14") || dents.Contains("54");
            arrays[5] = dents.Contains("13") || dents.Contains("53");
            arrays[6] = dents.Contains("12") || dents.Contains("52");
            arrays[7] = dents.Contains("11") || dents.Contains("51");

            arrays[8] = dents.Contains("21") || dents.Contains("61");
            arrays[9] = dents.Contains("22") || dents.Contains("62");
            arrays[10] = dents.Contains("23") || dents.Contains("63");
            arrays[11] = dents.Contains("24") || dents.Contains("64");
            arrays[12] = dents.Contains("25") || dents.Contains("65");
            arrays[13] = dents.Contains("26");
            arrays[14] = dents.Contains("27");
            arrays[15] = dents.Contains("28");

            arrays[16] = dents.Contains("38");
            arrays[17] = dents.Contains("37");
            arrays[18] = dents.Contains("36");
            arrays[19] = dents.Contains("35") || dents.Contains("75");
            arrays[20] = dents.Contains("34") || dents.Contains("74");
            arrays[21] = dents.Contains("33") || dents.Contains("73");
            arrays[22] = dents.Contains("32") || dents.Contains("72");
            arrays[23] = dents.Contains("31") || dents.Contains("71");

            arrays[24] = dents.Contains("41") || dents.Contains("81");
            arrays[25] = dents.Contains("42") || dents.Contains("82");
            arrays[26] = dents.Contains("43") || dents.Contains("83");
            arrays[27] = dents.Contains("44") || dents.Contains("84");
            arrays[28] = dents.Contains("45") || dents.Contains("85");
            arrays[29] = dents.Contains("46");
            arrays[30] = dents.Contains("47");
            arrays[31] = dents.Contains("48");

            return arrays;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Build())
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private void treatArches_upperOnly_Click(object sender, EventArgs e)
        {
            treatArches_upperOnly_diagnosticSetup.Enabled = treatArches_upperOnly.Checked;
            treatArches_lowerOnly_diagnosticSetup.Enabled = treatArches_lowerOnly.Checked;

            if (!treatArches_lowerOnly.Enabled) treatArches_lowerOnly.Checked = false;
            if (!treatArches_upperOnly.Enabled) treatArches_upperOnly.Checked = false;

        }

        private void treatArches_both_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void aPRelation_class1CanineAndMolarRight_Click(object sender, EventArgs e)
        {
            panel3.Enabled = (aPRelation_class1CanineAndMolarRight.Checked || aPRelation_class1CanineAndMolarLeft.Checked);
            if (!panel3.Enabled)
            {
                APRelation_tooth_movement.Checked = false;
                APRelation_surgical.Checked = false;
                APRelation_classIIOrIIICorrectionSimulation.Checked = false;
                APRelation_distalization.Checked = false;
                APRelation_posteriorIPR.Checked = false;
            }

            panel4.Enabled = APRelation_classIIOrIIICorrectionSimulation.Checked;
            if (!panel4.Enabled)
            {
                APRelation_classIIOrIIICorrectionSimulation_alignerCutsNo.Checked = false;
                APRelation_classIIOrIIICorrectionSimulation_alignerCutsYes.Checked = false;
            }

            panel5.Enabled = APRelation_distalization.Checked;
            if (!panel5.Enabled)
            {
                APRelation_distalization_alignerCutsNo.Checked = false;
                APRelation_distalization_alignerCutsYes.Checked = false;
            }

            
            
        }

        private void overbite_correctOpenBiteOptions_Click(object sender, EventArgs e)
        {
            panel6.Enabled = overbite_correctOpenBiteOptions.Checked;
            panel9.Enabled = overbite_correctDeepBite.Checked;
        }

        private void overbite_correctOpenBiteOptions_extrudeAnteriorTeethAndIntrudePosterior_Click(object sender, EventArgs e)
        {
            panel7.Enabled = overbite_correctOpenBiteOptions_extrudeAnteriorOnly.Checked;
            panel8.Enabled = overbite_correctOpenBiteOptions_extrudeAnteriorTeethAndIntrudePosterior.Checked;
        }

        private void midline_improveMidlineWithIPR_Click(object sender, EventArgs e)
        {
            panel10.Enabled = midline_improveMidlineWithIPR.Checked;
        }

        private void midline_improveMidlineWithIPRUpper_Click(object sender, EventArgs e)
        {
            panel11.Enabled = midline_improveMidlineWithIPRUpper.Checked;
            panel12.Enabled = midline_improveMidlineWithIPRLower.Checked;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmEspacesDent frm = new FrmEspacesDent();
            frm.values = espacementdent;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                espacementdent = frm.values;
            }
        }

        private void overbite_correctDeepBite_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void biteRampsUpper_biteRampsUpperOptions_Click(object sender, EventArgs e)
        {
            panel23.Enabled = biteRampsUpper_biteRampsUpperOptions.Checked;

        }

        private void biteRampsUpper_biteRampsUpperOptions_incisors_Click(object sender, EventArgs e)
        {
            panel24.Enabled = biteRampsUpper_biteRampsUpperOptions_incisors.Checked;
        }

        private void rdViveraRetainer_CheckedChanged(object sender, EventArgs e)
        {
            rbTraitementLite.Visible = false;
            rbTraitementFull.Visible = false;
            rbTraitementi7.Visible = false;
            rbTraitementLite.Checked = false;
            rbTraitementFull.Checked = false;
            rbTraitementi7.Checked = false;

        }

        private void rdInvisalignAligneur_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void rdTeen_CheckedChanged(object sender, EventArgs e)
        {
            if (rdTeen.Checked)
            {
                rbTraitementLite.Visible = true;
                rbTraitementFull.Visible = true;
                rbTraitementi7.Visible = true;
                rbTraitementFull.Checked = true;
                rdFirst.Visible = false;
                //  rdFirst.Checked = false;
            }
        }

        private void rdFirst_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdChild_CheckedChanged(object sender, EventArgs e)
        {
            if (rdChild.Checked)
            {
                rbTraitementLite.Visible = false;
                rbTraitementFull.Visible = false;
                rbTraitementi7.Visible = false;
                rdFirst.Visible = true;
                rdFirst.Checked = true;
               
            }
        }

        private void rdAdulte_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAdulte.Checked)
            {
                rbTraitementLite.Visible = true;
                rbTraitementFull.Visible = true;
                rbTraitementi7.Visible = true;
                rbTraitementFull.Checked = true;
                rdFirst.Visible = false;
                //  rdFirst.Checked = false;
            }
        }
    }
}
