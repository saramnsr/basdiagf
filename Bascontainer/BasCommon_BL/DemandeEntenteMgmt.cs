using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_DAL;
using BasCommon_BO;
using System.Data;

namespace BasCommon_BL
{
    public static class DemandeEntenteMgmt
    {


       
        /*

        public static string getResume(EntentePrealable ep)
        {

            string resultat = "";



            resultat += "\r\nPATIENT : " + ep.patient.Nom + " " + ep.patient.Prenom;
            if (ep.patient.Assurepar != null)
                resultat += " assuré par " + ep.patient.Assurepar.Nom + " " + ep.patient.Assurepar.Prenom;



            #region Partie reserve au chirurgien dentiste
            if (ep.ReferenceNationalOpposable == EntentePrealable.RNO.HR)
                resultat += "\r\nReference National Opposable : HR";
            if (ep.ReferenceNationalOpposable == EntentePrealable.RNO.R)
                resultat += "\r\nReference National Opposable : R";

            resultat += "\r\nDate de proposition : " + ep.dateProposition.ToString();
            resultat += "\r\nDate d'impression : " + ep.DateImpression.ToString();
            if (ep.DateAccord != null) resultat += "\r\nDate d'accord : " + ep.DateAccord.Value.ToString();
            resultat += "\r\nDate de début de traitement : " + ep.DateDebutTraitement.ToString();
            resultat += "\r\nCotation des actes : " + ep.cotationDesActes.ToString();

            if (ep.IsDevisSigned)
                resultat += "\r\nUn devis a été signé";
            else
                resultat += "\r\nAucun devis n'a été signé";


            #endregion


            #region Renseignements medicaux
            resultat += "\r\nPeriode du traitement : " + ep.typetraitement.ToString();
            #region Diagnostic


            resultat += "\r\nAnomalie(s) Basal(s)";
            resultat += "\r\n\tSens Sagittal Maxilaire : " + ep.SensSagittalBasalMax.ToString();
            resultat += "\r\n\tSens Sagittal Mandibulaire : " + ep.SensSagittalBasalMand.ToString();
            resultat += "\r\n\tSens Transversal Maxilaire : " + ep.SensTransversalBasalMax.ToString();
            resultat += "\r\n\tSens Transversal Mandibulaire : " + ep.SensTransversalBasalMand.ToString();
            resultat += "\r\n\tSens Vertical : " + ep.SensVerticalBasal.ToString();

            resultat += "\r\nAnomalie(s) Alvéolaire(s)";
            resultat += "\r\n\tSens Sagittal Maxilaire : " + ep.SensSagittalAlveolaireMax.ToString();
            resultat += "\r\n\tSens Sagittal Mandibulaire : " + ep.SensSagittalAlveolaireMand.ToString();
            resultat += "\r\n\tSens Transversal Maxilaire : " + ep.SensTransversalAlveolaireMax.ToString();
            resultat += "\r\n\tSens Transversal Mandibulaire : " + ep.SensTransversalAlveolaireMand.ToString();
            resultat += "\r\n\tSens Vertical : " + ep.SensVerticalAlveolaire.ToString();

            resultat += "\r\nClasse dentaire molaire : " + ep.ClasseDentaireMolaire.ToString() + " " + ep.ClasseDentaireMolaireTxt;
            resultat += "\r\nClasse dentaire canine : " + ep.ClasseDentaireCanine.ToString() + " " + ep.ClasseDentaireCanineTxt;

            resultat += ep.DDD ? "DDD : Oui" : "DDD : Non";
            resultat += ep.DDM ? "DDM : Oui" : "DDM : Non";
            resultat += "\r\nAgenesie : " + ep.Agenesie;
            resultat += "\r\nDent(s) incl. ou surnum. : " + ep.DentsIncluseSurnum;
            resultat += "\r\nMalposition(s) : " + ep.Malposition;
            resultat += "\r\nOcclusion inversée : " + ep.occInverse.ToString();
            resultat += "\r\nFacteurs fonctionnel(s) : " + ep.FacteurFonctionnel;
            resultat += "\r\ncommentaires : " + ep.Commentaires;

            #endregion
            #endregion


            return resultat;
        }


        


        


        public static bool FillFullEntente(EntentePrealable ententeprealable)
        {
            DataRow dr;


            dr = DAC.getEntentePrealableWithoutDiag(ententeprealable.patient.Id, EntentePrealable.TypeDeTraitement.Debut);
            if (dr == null) return false;
            Builders.BuildEntentePrealable.BuildEntenteWithoutDiag(dr, ref ententeprealable);

            if (ententeprealable.typetraitement == EntentePrealable.TypeDeTraitement.Debut)
            {
                FillDiagWithoutEnt(ref ententeprealable);
            }
            return true;

        }

        


        public static void UpdateDiagEntente(EntentePrealable ententeprealable)
        {
            DAC.UpdateDiagEntentePrealable(ententeprealable);

        }

        public static void InsertDiagEntente(EntentePrealable ententeprealable)
        {
            DAC.InsertDiagEntentePrealable(ententeprealable);


        }

        public static void UpdateEntenteWithoutDiag(EntentePrealable ententeprealable)
        {
            DAC.UpdateEntentePrealableWithoutDiag(ententeprealable);

        }

        public static void InsertEntenteWithoutDiag(EntentePrealable ententeprealable)
        {
            DAC.InsertEntentePrealableWithoutDiag(ententeprealable);


        }
         * 
         * 
         */
    }
}
