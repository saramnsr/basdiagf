using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_DAL;
using BASEDiag_BO;
using BasCommon_BL;
using BasCommon_BO;

namespace BASEDiag_BL
{
    public static class MgmtDevis
    {



        public static void DeleteDevis(Devis devis)
        {
            DAC.DeleteDevis(devis);

        }


       

        public static Devis CreateDevis(List<Proposition> propositions, List<ActePGPropose> lstActes)
        {
            int Idpat = -1;
            DateTime? deacceptation = null;

            foreach (Proposition p in propositions)
            {
                if (Idpat == -1)
                {
                    Idpat = p.IdPatient;
                }
                else
                {
                    if (p.IdPatient != Idpat)
                        throw (new System.Exception("Toutes les propositions doivent appartenir au meme patient"));
                }
                if (p.Etat == Proposition.EtatProposition.Accepté)
                    if (deacceptation == null)
                        deacceptation = p.DateAcceptation;
                    else
                        if (deacceptation.Value < p.DateAcceptation) deacceptation = p.DateAcceptation;
            }

            Devis d = new Devis();
            d.DateProposition = DateTime.Now;
            d.DateAcceptation = deacceptation;
            d.DateEcheance = DateTime.Now.AddDays(15);
            d.IdPatient = Idpat;

            DAC.InsertDevis(d);

            d.actesHorstraitement = new List<ActePGPropose>();
            foreach (ActePGPropose acte in lstActes)
            {
                acte.devis = d;
                d.actesHorstraitement.Add(acte);
                DAC.Insert_acte_propositions(acte);
            }


            d.propositions = new List<Proposition>();
            foreach (Proposition p in propositions)
            {
                p.IdDevis = d.Id;
                p.Etat = Proposition.EtatProposition.Soumis;
                p.DateEvenement = DateTime.Now;
                PropositionMgmt.updateProposition(p);
                d.propositions.Add(p);
            }

            return d;

        }



        public static void FillactesHorstraitement(Devis devis)
        {
            DataTable dt = DAC.get_acte_propositions(devis);
            devis.actesHorstraitement = new List<ActePGPropose>();
            foreach (DataRow r in dt.Rows)
            {
                devis.actesHorstraitement.Add(Builders.BuildActesHorstraitement(r));
            }
        }

        public static List<Devis> getDevis(basePatient patient)
        {
            DataTable dt = DAC.getDevis(patient);

            List<Devis> lst = new List<Devis>();
            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildDevis(r));
            }
            return lst;
        }

        public static void AccepterDevis(int IdDevis)
        {
            DAC.AccepterDevis(IdDevis, DateTime.Now);
        }
    }
}
