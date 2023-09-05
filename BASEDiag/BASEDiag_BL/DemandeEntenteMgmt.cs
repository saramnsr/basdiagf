using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;
using BasCommon_BO;

namespace BASEDiag_BL
{
    public static class DemandeEntenteMgmt
    {

        public static void UpdateIdModeleEntente(int Idresume, int IdEntente)
        {
            DAC.UpdateIdModeleEntente(Idresume, IdEntente);
        }
        
        public static EntentePrealable CreateEntenteFromResume(ResumeClinique resume)
        {
            EntentePrealable ep = new EntentePrealable();

            #region Patient
            ep.patient = resume.patient;
            ep.Profession = resume.patient.Profession;
            #endregion


            #region Partie reserve au chirurgien dentiste
            ep.ReferenceNationalOpposable = EntentePrealable.RNO.None;
            ep.dateProposition = DateTime.Now;
            ep.DateDebutTraitement = DateTime.Now;
            ep.cotationDesActes = "";
            ep.IsDevisSigned = true;

            #endregion

            #region assure
            if (resume.patient.Assurepar != null)
            {
                if (resume.patient.Assurepar.correspondant == null)
                    resume.patient.Assurepar.correspondant = BasCommon_BL.MgmtCorrespondants.getCorrespondant(resume.patient.Assurepar.IdCorrespondance);

                if (resume.patient.Assurepar.correspondant.MainAdresse != null)
                {
                    ep.AdresseAssure = resume.patient.Assurepar.correspondant.MainAdresse.adresse.Adr1 + "\n" + resume.patient.Assurepar.correspondant.MainAdresse.adresse.Adr2 + "\n" + resume.patient.Assurepar.correspondant.MainAdresse.adresse.CodePostal + " " + resume.patient.Assurepar.correspondant.MainAdresse.adresse.Ville;
                    ep.AdresseAssure = ep.AdresseAssure.Replace("\n\n", "\n");
                }
                ep.NomPrenomAssure = resume.patient.Assurepar.correspondant.Nom + " " + resume.patient.Assurepar.correspondant.Prenom;
                ep.ImmatAssure = resume.patient.Assurepar.correspondant.numSecu;

                if (resume.patient.MainAdresse != null)
                {
                    ep.AdressePatient = resume.patient.MainAdresse.adresse.Adr1 + "\n" + resume.patient.MainAdresse.adresse.Adr2 + "\n" + resume.patient.MainAdresse.adresse.CodePostal + " " + resume.patient.MainAdresse.adresse.Ville;
                    ep.AdressePatient = ep.AdresseAssure.Replace("\n\n", "\n");
                }
                if (ep.AdressePatient == ep.AdresseAssure) ep.AdressePatient = "";
            }
            #endregion

            #region Renseignements medicaux
            ep.typetraitement = EntentePrealable.TypeDeTraitement.Debut;

            #region Diagnostic




            ep.SensSagittalBasalMax = resume.SensSagittalMaxBasal;
            ep.SensSagittalBasalMand = resume.SensSagittalMandBasal;
            ep.SensSagittalAlveolaireMax = resume.IncisiveSuperieur;
            ep.SensSagittalAlveolaireMand = resume.IncisiveInferieur;

            ep.SensTransversalBasalMax = resume.SensTransvMax;
            ep.SensTransversalBasalMand = resume.SensTransvMand;
            ep.SensTransversalAlveolaireMax = resume.DiagMax;
            ep.SensTransversalAlveolaireMand = resume.DiagMand;

            ep.SensVerticalBasal = resume.SensVertical;
            ep.SensVerticalAlveolaire = resume.OcclusionFace;

            if (((resume.ClasseMolD == BasCommon_BO.EntentePrealable.en_Class.Class_I) && (resume.ClasseMolG != BasCommon_BO.EntentePrealable.en_Class.Class_I)) ||
                ((resume.ClasseMolD != BasCommon_BO.EntentePrealable.en_Class.Class_I) && (resume.ClasseMolG == BasCommon_BO.EntentePrealable.en_Class.Class_I)))
            {
                ep.ClasseDentaireMolaire = BasCommon_BO.EntentePrealable.en_Class.Class_II;
                ep.ClasseDentaireMolaireTxt = "subdivision gauche/dte";
            }
            else
            {
                if (resume.ClasseMolD == resume.ClasseMolG)
                {
                    ep.ClasseDentaireMolaire = resume.ClasseMolD;
                    ep.ClasseDentaireMolaireTxt = "";
                }

            }


            if (((resume.ClasseCanD == BasCommon_BO.EntentePrealable.en_Class.Class_I) && (resume.ClasseCanG != BasCommon_BO.EntentePrealable.en_Class.Class_I)) ||
                ((resume.ClasseCanD != BasCommon_BO.EntentePrealable.en_Class.Class_I) && (resume.ClasseCanG == BasCommon_BO.EntentePrealable.en_Class.Class_I)))
            {
                ep.ClasseDentaireCanine = BasCommon_BO.EntentePrealable.en_Class.Class_II;
                ep.ClasseDentaireCanineTxt = "subdivision gauche/dte";
            }
            else
            {
                if (resume.ClasseCanD == resume.ClasseCanG)
                {
                    ep.ClasseDentaireCanine = resume.ClasseCanD;
                    ep.ClasseDentaireCanineTxt = "";
                }

            }



            ep.DDD = resume.DDD == BasCommon_BO.EntentePrealable.en_OuiNon.Oui;
            ep.DDM = resume.DDM == BasCommon_BO.EntentePrealable.en_OuiNon.Oui;

            ep.DentsIncluseSurnum = "";
            ep.Agenesie = resume.Agenesie;
            if (resume.DentsIncluses != "")
                ep.DentsIncluseSurnum += " inc : " + resume.DentsIncluses;
            if (resume.DentsSurnumeraires != "")
                ep.DentsIncluseSurnum += " surnum : " + resume.DentsSurnumeraires;

            ep.Malposition = "Multiples";

            ep.occInverse = resume.OcclusionInverse;
            //ep.SautArticule = resume.SautArticule;

            

            ep.FacteurFonctionnel = resume.FacteurFonctionnel;

            /*
            switch (resume.TypeDeTraitement)
            {
                case BasCommon_BO.EntentePrealable.TypeTraitement.Chirurgie:
                    ep.PlanDeTraitement = "Traitement  ADULTE  ortho-chirurgical";
                    break;
                 case BasCommon_BO.EntentePrealable.TypeTraitement.Invisalign:
                    ep.PlanDeTraitement = "technique  de  déplacement  par  gouttières  thermoformées  amovibles \n avec  éventuelle  finition  multibague";
                    break;
                 case BasCommon_BO.EntentePrealable.TypeTraitement.MultiBague:
                    ep.PlanDeTraitement = "Multibagues haut et bas , nivellement , coordination";
                    break;
                 case BasCommon_BO.EntentePrealable.TypeTraitement.RCC:
                    ep.PlanDeTraitement = "1 appareillage  maxillaire  type  plaque  palatine  de  schwartz  modifié";
                    break;

            }
            */

            ep.Commentaires = "";

            #endregion
            #endregion


            return ep;
        }


    }
}
