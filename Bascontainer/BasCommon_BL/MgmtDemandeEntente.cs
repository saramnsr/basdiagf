using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_DAL;
using BasCommon_BO;
using System.Data;

namespace BasCommon_BL
{
    public static class MgmtDemandeEntente
    {


        public static void DeleteEntenteWithoutDiag(EntentePrealable ententeprealable)
        {
            DAC.DeleteEntentePrealableWithoutDiag(ententeprealable);


        }

        public static bool FillDiagWithoutEnt(EntentePrealable ententeprealable)
        {
            DataRow dr;

            if (ententeprealable.IdDiag > 0)
                dr = DAC.getDiagEntentePrealableFromId(ententeprealable.IdDiag);
            else
                dr = DAC.getLastDiagEntentePrealable(ententeprealable.idPatient);
            if (dr != null)
            {
                Builders.BuildEntentePrealable.BuildDiagEntente(dr, ref ententeprealable);
                return true;
            }
            else return false;
        }

        public static bool FillDiagEntente(int IdModele, ref EntentePrealable ententeprealable, int IdPatient)
        {
            DataRow dr;
            dr = DAC.getEntentePrealableWithoutDiag(IdModele, IdPatient);
            if (dr == null) return false;
            Builders.BuildEntentePrealable.BuildEntenteWithoutDiag(dr, ref ententeprealable);

            return true;
        }

        public static bool FillDiagWithoutModele(int IdDiag, ref EntentePrealable ententeprealable)
        {
            DataRow dr;

            dr = DAC.getDiagEntentePrealableFromId(IdDiag);
            if (dr != null)
                Builders.BuildEntentePrealable.BuildDiagEntente(dr, ref ententeprealable);
            else
                return false;

            return true;
        }

        public static EntentePrealable GetEntente(int identente, int IdPatient)
        {
            DataRow dr;


            dr = DAC.getEntentePrealableWithoutDiag(identente, IdPatient);
            if (dr == null) return null;

            EntentePrealable ententeprealable = new EntentePrealable();

            Builders.BuildEntentePrealable.BuildEntenteWithoutDiag(dr, ref ententeprealable);


            if (ententeprealable.IdDiag > 0)
            {
                FillDiagWithoutEnt(ententeprealable);
            }


            return ententeprealable;

        }

        public static string getResume(EntentePrealable ep)
        {

            string resultat = "";



            resultat += "\r\nPATIENT : " + ep.patient.Nom + " " + ep.patient.Prenom;
            /*if (ep.patient.Assurepar != null)
                resultat += " assuré par " + ep.patient.Assurepar.Nom + " " + ep.patient.Assurepar.Prenom;
            */


            #region Partie reserve au chirurgien dentiste
            if (ep.ReferenceNationalOpposable == EntentePrealable.RNO.HR)
                resultat += "\r\nReference National Opposable : HR";
            if (ep.ReferenceNationalOpposable == EntentePrealable.RNO.R)
                resultat += "\r\nReference National Opposable : R";

            resultat += "\r\nDate de proposition : " + ep.dateProposition.ToString();
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

        public static List<EntentePrealable> GetEntentePrealableFromIdPatient(basePatient pat)
        {
            List<EntentePrealable> lst = new List<EntentePrealable>();
            System.Data.DataTable dt = DAC.getEntentePrealableWithoutDiag(pat);

            foreach (DataRow r in dt.Rows)
            {
                EntentePrealable ep = new EntentePrealable();
                ep.patient = pat;
                Builders.BuildEntentePrealable.BuildEntenteWithoutDiag(r, ref ep);
                if (ep.IdDiag > 0)
                {
                    DataRow dr = DAC.getDiagEntentePrealableFromId(ep.IdDiag);
                    if (dr == null)
                        ep.IdDiag = -1;
                    else
                        Builders.BuildEntentePrealable.BuildDiagEntente(dr, ref ep);
                }
                lst.Add(ep);
            }


            return lst;
        }

        public static List<EntentePrealable> GetEntentePrealableFromIdPatient(int pat)
        {
            List<EntentePrealable> lst = new List<EntentePrealable>();
            System.Data.DataTable dt = DAC.getEntentePrealableWithoutDiag(pat);

            foreach (DataRow r in dt.Rows)
            {
                EntentePrealable ep = new EntentePrealable();
                ep.idPatient = pat;
                Builders.BuildEntentePrealable.BuildEntenteWithoutDiag(r, ref ep);
                if (ep.IdDiag > 0)
                {
                    DataRow dr = DAC.getDiagEntentePrealableFromId(ep.IdDiag);
                    if (dr == null)
                        ep.IdDiag = -1;
                    else
                        Builders.BuildEntentePrealable.BuildDiagEntente(dr, ref ep);
                }
                lst.Add(ep);
            }


            return lst;
        }


        public static EntentePrealable GetEntentePrealable(int Id, int IdPatient)
        {
            EntentePrealable dep = new EntentePrealable();
            System.Data.DataRow dr = DAC.getEntentePrealableWithoutDiag(Id, IdPatient);
            if (dr == null) return null;
            Builders.BuildEntentePrealable.BuildEntenteWithoutDiag(dr, ref dep);

            System.Data.DataRow DiagRow = DAC.getDiagEntentePrealableFromId(dep.IdDiag);
            if (DiagRow != null)
                Builders.BuildEntentePrealable.BuildDiagEntente(DiagRow, ref dep);

            return dep;
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
    }
}
