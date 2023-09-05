using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using BasCommon_BO.ElementsEnBouche.BO;
using BasCommon_DAL;
using System.Data;

namespace BasCommon_BL
{
    public static class EnBoucheMgmt
    {

        public static void FindDateInstallSupressionTheorique(List<CommClinique> comms, IElementDent elmnt, ref DateTime? dateinstall, ref DateTime? datesuppression)
        {
            CommClinique ccDeReferenceCom = comms[0];

            dateinstall = elmnt.DateInstallation;
            datesuppression = elmnt.Datesuppression;

            /*
            foreach (CommClinique cc in comms)
                if (cc.IsDateDeRef)
                    ccDeReferenceCom = cc;
            */

            CommClinique ccdebut = null;
            CommClinique ccfin = null;

            foreach (CommClinique cc in comms)
            {
                if (cc.Id == elmnt.IdCommDebut) ccdebut = cc;
                if (cc.Id == elmnt.IdCommFin) ccfin = cc;

            }

            if ((ccDeReferenceCom != null) && (ccDeReferenceCom.date != null))
            {
                if ((ccdebut != null) && (elmnt.IdCommDebut > -1))
                {

                    if (ccdebut.date != null)
                        dateinstall = ccdebut.date.Value;
                    else
                        dateinstall = ccDeReferenceCom.date.Value.AddMonths(ccdebut.NbMois - ccDeReferenceCom.NbMois).AddDays(ccdebut.NbJours - ccDeReferenceCom.NbJours);
                }

                if ((ccfin != null) && (elmnt.IdCommFin > -1))
                {
                    if (ccfin.date != null)
                        datesuppression = ccfin.date.Value;
                    else
                        datesuppression = ccDeReferenceCom.date.Value.AddMonths(ccfin.NbMois - ccDeReferenceCom.NbMois).AddDays(ccfin.NbJours - ccDeReferenceCom.NbJours);
                }

            }
        }

        public static void FindDateInstallSupressionTheorique(List<CommClinique> comms, ElementAppareil app, ref DateTime? dateinstall, ref DateTime? datesuppression)
        {
            CommClinique ccDeReferenceCom = null;

            dateinstall = app.DateInstallation;
            datesuppression = app.Datesuppression;

            foreach (CommClinique cc in comms)
                if (cc.IsDateDeRef)
                    ccDeReferenceCom = cc;

            CommClinique ccdebut = null;
            CommClinique ccfin = null;

            foreach (CommClinique cc in comms)
            {
                if (cc.Id == app.IdCommDebut) ccdebut = cc;
                if (cc.Id == app.IdCommFin) ccfin = cc;

            }

            if ((ccDeReferenceCom != null) && (ccDeReferenceCom.date != null))
            {
                if ((ccdebut != null) && (app.IdCommDebut > -1))
                {

                    if (ccdebut.date != null)
                        dateinstall = ccdebut.date.Value;
                    else
                        dateinstall = ccDeReferenceCom.date.Value.AddMonths(ccdebut.NbMois - ccDeReferenceCom.NbMois).AddDays(ccdebut.NbJours - ccDeReferenceCom.NbJours);
                }

                if ((ccfin != null) && (app.IdCommFin > -1))
                {
                    if (ccfin.date != null)
                        datesuppression = ccfin.date.Value;
                    else
                        datesuppression = ccDeReferenceCom.date.Value.AddMonths(ccfin.NbMois - ccDeReferenceCom.NbMois).AddDays(ccfin.NbJours - ccDeReferenceCom.NbJours);
                }

            }
        }

        public static void Save(IElementDent elem)
        {
            if (elem.Id == -1)
            {
                if ((elem.Datesuppression == null) || (((TimeSpan)(elem.Datesuppression - elem.DateInstallation)).TotalDays > 1))
                    DAC.InsertEnbouche(elem);
            }
            else
            {
                if (elem.Datesuppression != null)
                    DAC.RetirerEnbouche(elem);
            }
        }

        public static void Save(ElementAppareil elem)
        {
            if (elem.Id == -1)
            {
                if ((elem.Datesuppression == null) || (((TimeSpan)(elem.Datesuppression - elem.DateInstallation)).TotalDays > 1))
                    DAC.InsertEnbouche(elem);
            }
            else
            {
                if (elem.Datesuppression != null)
                    DAC.RetirerEnbouche(elem);
            }
        }
        /*
        public static List<IElementDent> GetElementsRemovedEnBouche(CommClinique com)
        {
            List<IElementDent> lst = new List<IElementDent>();
            if (com.Id == -1) return lst;
            DataTable dt = DAC.getEnboucheRemovedAt(com);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildElementDent(r));
            }

            return lst;
        }

        public static List<IElementDent> GetElementsInstalledEnBouche(CommClinique com)
        {
            List<IElementDent> lst = new List<IElementDent>();

            if (com.Id == -1) return lst;
            DataTable dt = DAC.getEnboucheInstalledAt(com);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildElementDent(r));
            }

            return lst;
        }

        public static List<ElementAppareil> GetAppareilsRemovedEnBouche(CommClinique com)
        {
            List<ElementAppareil> lst = new List<ElementAppareil>();

            DataTable dt = DAC.GetAppareilsRemovedEnBouche(com);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildElementAppareil(r));
            }

            return lst;
        }


        public static List<ElementAppareil> GetAppareilsInstalledEnBouche(CommClinique com)
        {
            List<ElementAppareil> lst = new List<ElementAppareil>();

            DataTable dt = DAC.GetAppareilsInstalledEnBouche(com);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildElementAppareil(r));
            }

            return lst;
        }
        */
        public static List<IElementAppareil> GetAllAppareilsEnBouche(basePatient patient)
        {

            List<IElementAppareil> _AppareilsEnBouche = new List<IElementAppareil>();

            DataTable dt = DAC.getAppareilsEnbouche(patient.Id);

            foreach (DataRow r in dt.Rows)
            {
                _AppareilsEnBouche.Add(Builders.BuildEnBouche.BuildElementAppareil(r));
            }
            return _AppareilsEnBouche;
        }

        public static List<IElementAppareil> GetAllAppareilsEnBouche(basePatient patient, DateTime dte)
        {
            List<IElementAppareil> lst = new List<IElementAppareil>();
            foreach (IElementAppareil app in GetAllAppareilsEnBouche(patient))
            {
                DateTime? dteInstall = null;
                DateTime? dteSuppressio = null;


                EnBoucheMgmt.FindDateInstallSupressionTheorique(patient.commentairesClinique, (ElementAppareil)app, ref dteInstall, ref dteSuppressio);
                if (((dteInstall == null) || (dteInstall < dte)) &&
                    ((dteSuppressio == null) || (dteSuppressio > dte)))
                    lst.Add(app);
            }
            return lst;
        }


        public static IElementDent CreateElementFromType(ElementDent.Materials mat)
        {

            IElementDent resultat = null;
            switch (mat)
            {
                case ElementDent.Materials.chainette:
                    resultat = new Chainette();
                    break;
                case ElementDent.Materials.Kobayashi:
                    resultat = new Kobayashi();
                    break;
                case ElementDent.Materials.Ligature:
                    resultat = new Ligature();
                    break;
                case ElementDent.Materials.LigatureM:
                    resultat = new LigatureMetal();
                    break;
                case ElementDent.Materials.TIM:
                    resultat = new TIM();
                    break;

                case ElementDent.Materials.Arc:
                    resultat = new Arc();
                    break;

                case ElementDent.Materials.Ressort:
                    resultat = new Ressort();
                    break;
            }
            return resultat;
        }


        public static List<IElementDent> GetAllElementsEnBouche(basePatient patient)
        {
            List<IElementDent> _ElementsEnBouche = new List<IElementDent>();

            DataTable dt = DAC.getAccessoiresEnbouche(patient.Id);

            foreach (DataRow r in dt.Rows)
            {
                _ElementsEnBouche.Add(Builders.BuildEnBouche.BuildElementDent(r));
            }

            return _ElementsEnBouche;
        }

        /*
        public static List<ElementAppareil> GetAllAppareilsEnBouche(Patient patient,DateTime dte)
        {
            List<ElementAppareil> lst = new List<ElementAppareil>();

            DataTable dt = DAC.getAppareilsEnbouche(patient.Id, dte);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildElementAppareil(r));
            }

            return lst;
        }
        */


    }
}
