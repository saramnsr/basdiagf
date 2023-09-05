﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_DAL;
using BasCommon_BO;
using System.Data;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class MgmtEncaissement
    {

        public static bool includeCN = true;

        private static List<int> _AuthorizedDays = null;
        public static List<int> AuthorizedDays
        {
            get
            {
                if (_AuthorizedDays == null)
                {
                    _AuthorizedDays = new List<int>();
                    string dtepre = System.Configuration.ConfigurationManager.AppSettings["DatesPrelevement"];

                    try
                    {
                        foreach (string s in dtepre.Split(','))
                        {
                            int d = Convert.ToInt32(s);
                            _AuthorizedDays.Add(d);

                        }
                    }
                    catch (System.Exception)
                    {
                        _AuthorizedDays.Add(10);
                        _AuthorizedDays.Add(20);
                        _AuthorizedDays.Add(30);
                    }
                }


                return _AuthorizedDays;
            }
            set
            {
                _AuthorizedDays = value;
            }
        }

        public static List<Virement> GetVirementPrevus(BanqueDeRemise bqe, DateTime dt1, DateTime dt2)
        {

            List<Virement> liste = new List<Virement>();

            string method = "/getListVirementPrevus/" + dt1.ToString("yyyy-MM-dd HH:mm:ss");
            method += "&" + dt2.ToString("yyyy-MM-dd HH:mm:ss");

            if (bqe != null)
            {
                method = "/getListVirementPrevusByDateAndBanque/" + dt1.ToString("yyyy-MM-dd HH:mm:ss");
                method += "&" + dt2.ToString("yyyy-MM-dd HH:mm:ss");
                method += "&" + bqe.Code;
            }


            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);

            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildVirement.Build(obj));

            return liste;

        }

        public static List<Virement> GetVirementPrevusOld(BanqueDeRemise bqe, DateTime dt1, DateTime dt2)
        {
            DataTable dt = DAC.GetVirementPrevus(bqe, dt1, dt2);

            List<Virement> lst = new List<Virement>();

            foreach (DataRow r in dt.Rows)
            {
                Virement v = Builders.BuildVirement.Build(r);
                lst.Add(v);

            }

            return lst;
        }


        public static List<PaiementReel> FindEncaissements(string numcheque)
        {
            DataTable dt = DAC.FindCheques(numcheque);

            List<PaiementReel> _listEncaissements = new List<PaiementReel>();

            foreach (DataRow r in dt.Rows)
            {
                PaiementReel ec = Builders.BuildPaiementReel.Build(r);

                bool found = false;
                foreach (PaiementReel pr in _listEncaissements)
                    if (pr.Id == ec.Id)
                    {
                        pr.Patients += "," + ec.Patients;
                        pr.lstpatient.Add(ec.lstpatient[0]);
                        found = true;
                    }


                if (!found) _listEncaissements.Add(ec);

            }

            return _listEncaissements;
        }

        public static List<GroupedPrelevement> GetPrelevementGroupeAPrelever()
        {
            List<GroupedPrelevement> lst = new List<GroupedPrelevement>();
            DataTable dt = DAC.GetPrelevementGroupeAPrelever();
            foreach (DataRow dr in dt.Rows)
            {
                GroupedPrelevement gp = Builders.BuildGroupedPrelevement.Build(dr);
                lst.Add(gp);
            }
            return lst;
        }
        public static List<GroupedPrelevement> GetPrelevementGroupeAPreleverNonControlerOLD()
        {
            List<GroupedPrelevement> lst = new List<GroupedPrelevement>();
            DataTable dt = DAC.GetPrelevementGroupeAPreleverNonControler();
            foreach (DataRow dr in dt.Rows)
            {
                GroupedPrelevement gp = Builders.BuildGroupedPrelevement.Build(dr);
                lst.Add(gp);
            }
            return lst;
        }

        public static List<GroupedPrelevement> GetPrelevementGroupeAPreleverNonControler()
        {
            List<GroupedPrelevement> lst = new List<GroupedPrelevement>();
            JArray json = DAC.getMethodeJsonArray("/PrelevementGroupeAPreleverNonControler");
            foreach (JObject dr in json)
            {
                GroupedPrelevement gp = Builders.BuildGroupedPrelevement.BuildJ(dr);
                lst.Add(gp);
            }
            return lst;
        }


        public static List<int> GetListPatientsAffectedByPaiement(PaiementReel pmt)
        {
            return DAC.GetListPatientsAffectedByPaiement(pmt);
        }


        public static void RejetDePaiement(PaiementReel pr, string motif, double FraisPatient, double FraisBanque, basePatient RejetImputableSur)
        {
            List<Encaissement> lst = MgmtEncaissement.GetEncaissements(pr);

            foreach (Encaissement ec in lst)
            {

                CommentHisto cmnt = new CommentHisto();
                cmnt.comment = "Rejet du paiement " + pr.typeencaissement.ToString() + " de " + pr.Montant + " du " + pr.DateEncaissement.ToShortDateString();
                cmnt.DateCommentaire = DateTime.Now;
                cmnt.Ecrivain = null;
                cmnt.Importance = CommentHisto.CommentHistoImportance.Urgent;
                cmnt.IdPatient = ec.IdPatient;
                if (ec.IdPatient == RejetImputableSur.Id)
                    cmnt.patient = RejetImputableSur;
                cmnt.typecomment = CommentHisto.CommentHistoType.Financier;


                MgmtCommentairesHisto.InsertCommentaire(cmnt);

                LogMgmt.RefusPaiement(pr, motif, FraisPatient, ec.IdPatient);
                MgmtEncaissement.Delete(ec, true);

                /*
                ActePG afrais = new ActePG();
                afrais.Template = TemplateApctePGMgmt.getTemplatesActeGestion("HN");
                afrais.Quantite = 1;
                afrais.IdPatient = ec.IdPatient;
                afrais.NeedDEP = false;
                afrais.NeedFSE = false;
                afrais.NbMois = 0;
                afrais.NbJours = 0;
                afrais.Montant_Honoraire = ec.MontantEncaisse;
                afrais.Libelle = "Rejet paiement du " + pr.DateEncaissement.ToShortDateString() + " de " + pr.Montant.ToString("C2");
                afrais.DateExecution = DateTime.Now;


                ActesPGMgmt.InsertActePGWithEcheance(afrais, false);
                */


            }
            if (FraisPatient > 0)
            {
                ActePG afrais = new ActePG();
                afrais.Template = TemplateApctePGMgmt.getTemplatesActeGestion("HN");
                afrais.Quantite = 1;
                afrais.IdPatient = RejetImputableSur.Id;
                afrais.patient = RejetImputableSur;
                afrais.NeedDEP = false;
                afrais.NeedFSE = false;
                afrais.NbMois = 0;
                afrais.NbJours = 0;
                afrais.Montant_Honoraire = FraisPatient;
                afrais.Libelle = "Rejet de paiement du " + pr.DateEncaissement.ToShortDateString() + " de " + pr.Montant.ToString("C2") + "(FRais de gestion)";
                afrais.DateExecution = DateTime.Now;


                ActesPGMgmt.InsertActePGWithEcheance(afrais, false, false, null);
            }
            if (FraisBanque > 0)
            {
                ActePG afrais = new ActePG();
                afrais.Template = TemplateApctePGMgmt.getTemplatesActeGestion("HN");
                afrais.Quantite = 1;
                afrais.IdPatient = RejetImputableSur.Id;
                afrais.patient = RejetImputableSur;
                afrais.NeedDEP = false;
                afrais.NeedFSE = false;
                afrais.NbMois = 0;
                afrais.NbJours = 0;
                afrais.Montant_Honoraire = FraisBanque;
                afrais.Libelle = "Rejet de paiement du " + pr.DateEncaissement.ToShortDateString() + " de " + pr.Montant.ToString("C2") + "(Frais bancaires)";
                afrais.DateExecution = DateTime.Now;


                ActesPGMgmt.InsertActePGWithEcheance(afrais, false, false, null);
            }



        }

        public static void Delete(Encaissement encaissement, bool ForceIfControled)
        {
             DAC.DeleteEncaissement(encaissement, true, ForceIfControled);
            LogMgmt.SuppressionEncaissement(encaissement.IdPatient, encaissement);

            if ((encaissement.patient != null) && (encaissement.patient.Echeances != null))
            {


                foreach (Echeance e in encaissement.patient.Echeances)
                {
                    if (e.ID_Encaissement == encaissement.Id)
                        e.ID_Encaissement = -1;
                }



                if ((encaissement.patient != null) && (encaissement.patient.PaiementReels != null))
                {
                    List<PaiementReel> lst = encaissement.patient.PaiementReels;
                    for (int i = lst.Count - 1; i >= 0; i--)
                        if (encaissement.IdPaiementReel == lst[i].Id)
                            encaissement.patient.PaiementReels.Remove(lst[i]);


                }

                if ((encaissement.patient != null) && (encaissement.patient.Encaissements != null))
                {
                    encaissement.patient.Encaissements.Remove(encaissement);
                }

            }
        }


        public static void AddPnFCheck(PnFCheck pnfchecker)
        {

            DAC.ADdPnFCheck(pnfchecker);
        }

        public static void EffectuerEncaissements(List<Encaissement> encaissmeents)
        {


            foreach (Encaissement ec in encaissmeents)
            {

                if (ec.paiementreel.BanqueDeRemise == null)
                {
                    if (ec.paiementreel.EntiteJuridique.ComptesBancaire == null)
                        ec.paiementreel.EntiteJuridique.ComptesBancaire = BanqueMgmt.getBanquesDeRemise(ec.paiementreel.EntiteJuridique);

                    ec.paiementreel.BanqueDeRemise = ec.paiementreel.EntiteJuridique.ComptesBancaire.Count == 0 ? null : ec.paiementreel.EntiteJuridique.ComptesBancaire[0];
                }


                if (ec.paiementreel.Id == -1)
                {
                    DAC.InsertPaiementReel(ec.paiementreel);

                    if ((ec.patient != null) && (ec.patient.PaiementReels != null))
                        ec.patient.PaiementReels.Add(ec.paiementreel);
                }

                if (ec.Id == -1)
                {
                    LogMgmt.AjoutEncaissement(ec.IdPatient, ec);
                    DAC.InsertEncaissement(ec);

                    if ((ec.patient != null) && (ec.patient.Encaissements != null))
                        ec.patient.Encaissements.Add(ec);
                }
            }

        }

        public static void EffectuerEncaissements(List<PaiementReel> prs, List<Encaissement> encaissmeents)
        {
            Dictionary<PaiementReel, List<Encaissement>> dico = new Dictionary<PaiementReel, List<Encaissement>>();

            foreach (PaiementReel p in prs)
            {
                dico.Add(p, new List<Encaissement>());

                foreach (Encaissement ec in encaissmeents)
                {
                    if (ec.paiementreel == p)
                        dico[p].Add(ec);
                }

            }

            foreach (KeyValuePair<PaiementReel, List<Encaissement>> kv in dico)
                EffectuerUnEncaissement(kv.Key, kv.Value);



        }

        public static void EffectuerUnEncaissement(PaiementReel pr, List<Encaissement> encaissmeents)
        {


            if (pr.BanqueDeRemise == null)
            {
                //if (pr.EntiteJuridique.ComptesBancaire == null)
                    pr.EntiteJuridique.ComptesBancaire = BanqueMgmt.getBanquesDeRemise(pr.EntiteJuridique);

                pr.BanqueDeRemise = pr.EntiteJuridique.ComptesBancaire.Count == 0 ? null : pr.EntiteJuridique.ComptesBancaire[0];
            }

            if (pr.EspecesMisEncaisse > 0)
                MgmtMvtCaisse.AddMvtCaisse(pr.EspecesMisEncaisse, null);

            if (pr.Id == -1)
            {
                DAC.InsertPaiementReel(pr);

                foreach (Encaissement e in encaissmeents)
                    if ((e.patient != null) && (e.patient.PaiementReels != null))
                        e.patient.PaiementReels.Add(pr);
            }

            foreach (Encaissement ec in encaissmeents)
            {
                if (ec.Id == -1)
                {
                    LogMgmt.AjoutEncaissement(ec.IdPatient, ec);
                    DAC.InsertEncaissement(ec);

                    if ((ec.patient != null) && (ec.patient.Encaissements != null))
                        ec.patient.Encaissements.Add(ec);
                }
            }

        }



        public static void UpdateEncaissement(Encaissement encaissement)
        {
            DAC.UpdateEncaissement(encaissement);
        }


        public static void UpdatePaiementReel(PaiementReel paiementreel)
        {
            DAC.UpdatePaiementReel(paiementreel);
        }

        public static void UpdateEnCaisseNoire(PaiementReel paiementreel, bool IsAdded)
        {
            /*
            if (IsAdded)
            {
                List<ControlFinancier> lst = MgmtControlFinance.GetControlFinancier(paiementreel);
                if (lst.Count > 0)
                    throw new System.Exception(" Ce reglement à été controlé le "+lst[0].DateControl.ToString()+". Sorti impossible !");
            }
            */
            DAC.UpdateEnCaisseNoire(paiementreel, IsAdded);
            paiementreel.IsInBlackCase = IsAdded;

        }


        public static void Prelever(Echeance ech, BanqueDeRemise bqe)
        {

            if (!ech.ParPrelevement) return;

            PaiementReel pr = new PaiementReel();

            pr.MontantRemis = ech.Montant;
            pr.Montant = ech.Montant;
            pr.DateRemiseEnBanque = DateTime.Now;
            pr.DateEcheance = DateTime.Now;
            pr.DateEncaissement = DateTime.Now;
            pr.EstRemisEnBanque = PaiementReel.RemisEnBanque.Oui;
            pr.BanqueDeRemise = bqe;
            pr.typeencaissement = PaiementReel.TypeEncaissement.Prelevement;
            pr.payeur = ech.patient.ToString();


            InfoPatientComplementaire nfo = baseMgmtPatient.getinfocomplementaire(ech.IdPatient);
            if (nfo.PraticienResponsable != null)
                pr.EntiteJuridique = nfo.PraticienResponsable.EntiteJuridique;




            List<Encaissement> lst = new List<Encaissement>();
            Encaissement ec = new Encaissement();
            ec.paiementreel = pr;
            ec.IdPatient = ech.IdPatient;
            ec.MontantEncaisse = ech.Montant;
            lst.Add(ec);



            MgmtEncaissement.EffectuerUnEncaissement(pr, lst);



            ech.encaissement = ec;

            EcheancesMgmt.UpdateEcheance(ech);
            LogMgmt.Prelever(-1, pr);

        }

        public static void Virement(Echeance ech, BanqueDeRemise bqe)
        {
            Virement(ech, bqe, DateTime.Now);
        }

        public static void Virement(Echeance ech, BanqueDeRemise bqe, DateTime DateVirement)
        {


            PaiementReel pr = new PaiementReel();

            pr.MontantRemis = ech.Montant;
            pr.Montant = ech.Montant;
            pr.DateRemiseEnBanque = DateVirement;
            pr.DateEcheance = ech.DateEcheance;
            pr.DateEncaissement = DateVirement;
            pr.EstRemisEnBanque = PaiementReel.RemisEnBanque.Oui;
            pr.BanqueDeRemise = bqe;
            pr.typeencaissement = PaiementReel.TypeEncaissement.Virement;
            pr.Mutuelle = ech.mutuelle;


            InfoPatientComplementaire nfo = baseMgmtPatient.getinfocomplementaire(ech.IdPatient);
            if (nfo.PraticienResponsable != null)
                pr.EntiteJuridique = nfo.PraticienResponsable.EntiteJuridique;



            if (ech.payeur == Echeance.typepayeur.Banque)
                pr.payeur = "Banque";
            if (ech.payeur == Echeance.typepayeur.Mutuelle)
                pr.payeur = ech.mutuelle == null ? "Mutuelle" : ech.mutuelle.ToString();
            if (ech.payeur == Echeance.typepayeur.Secu)
                pr.payeur = "Secu";

            List<Encaissement> lst = new List<Encaissement>();
            Encaissement ec = new Encaissement();
            ec.paiementreel = pr;
            ec.IdPatient = ech.IdPatient;
            ec.MontantEncaisse = ech.Montant;
            lst.Add(ec);



            MgmtEncaissement.EffectuerUnEncaissement(pr, lst);



            ech.encaissement = ec;

            EcheancesMgmt.UpdateEcheance(ech);
            LogMgmt.Virement(-1, pr, ech);

        }


        public static void RemiseEnBanque(PaiementReel paiementreel, BanqueDeRemise bqe)
        {

            paiementreel.DateRemiseEnBanque = DateTime.Now;
            paiementreel.EstRemisEnBanque = PaiementReel.RemisEnBanque.Oui;
            paiementreel.MontantRemis = paiementreel.Montant;
            paiementreel.Status = PaiementReel.statusEncaissement.Remis;
            paiementreel.BanqueDeRemise = bqe;

            LogMgmt.RemiseEnBanque(-1, paiementreel);

            DAC.UpdatePaiementReel(paiementreel);
        }


        public static void ModifierDateEcheance(int IdPatient, PaiementReel paiementreel, DateTime? nouvelleDate)
        {
            DateTime? olddte = paiementreel.DateEcheance;
            paiementreel.DateEcheance = nouvelleDate;
            LogMgmt.ModifierDateEcheance(IdPatient, paiementreel, olddte);

            DAC.UpdatePaiementReel(paiementreel);
        }




        public static List<Encaissement> GetEncaissementsARemettreEnBanque()
        {
            DataTable dt = DAC.GetEncaissementsARemettreEnBanque();

            List<Encaissement> lst = new List<Encaissement>();

            foreach (DataRow r in dt.Rows)
            {
                Encaissement ec = Builders.BuildEncaissement.Build(r);
                lst.Add(ec);

            }

            return lst;
        }




        public static Encaissement GetEncaissement(int Id)
        {
            DataRow dr = DAC.GetEncaissement(Id);

            if (dr == null) return null;
            Encaissement ec = Builders.BuildEncaissement.Build(dr);

            return ec;
        }



        /*
        public static List<PaiementReel> GetPaiementReelsDuJour(PaiementReel.TypeEncaissement type)
        {
            DataTable dt = DAC.GetPaiementReelsDuJour(type);

            Dictionary<int, PaiementReel> dico = new Dictionary<int, PaiementReel>();

            foreach (DataRow r in dt.Rows)
            {
                PaiementReel ec = Builders.BuildPaiementReel.Build(r);


                if (dico.ContainsKey(ec.Id))
                {
                    dico[ec.Id].Patients += " + " + ec.Patients;

                    foreach (int i in ec.lstpatient)
                        dico[ec.Id].lstpatient.Add(i);
                }
                else
                {
                    dico.Add(ec.Id, ec);
                }


            }
            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (KeyValuePair<int, PaiementReel> kv in dico)
                lst.Add(kv.Value);

            return lst;
        }
        */

        public static List<PaiementReel> GetCBOuAMEXDuJour()
        {
            JArray json = DAC.getMethodeJsonArray("/CBDuJour/" + DateTime.Now.Date.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss") + "&" + DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss") + "&CBDUJOUR" + "&" + Convert.ToInt32(PaiementReel.TypeEncaissement.CB));


            Dictionary<int, PaiementReel> dico = new Dictionary<int, PaiementReel>();

            foreach (JObject r in json)
            {
                PaiementReel ec = Builders.BuildPaiementReel.BuildJ(r);


                if (dico.ContainsKey(ec.Id))
                {
                    dico[ec.Id].Patients += " + " + ec.Patients;

                    foreach (int i in ec.lstpatient)
                        dico[ec.Id].lstpatient.Add(i);
                }
                else
                {
                    dico.Add(ec.Id, ec);
                }


            }


            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (KeyValuePair<int, PaiementReel> kv in dico)
                lst.Add(kv.Value);

            return lst;
        }

        public static List<PaiementReel> GetCBOuAMEXDuJourOLD()
        {
            DataTable dt = DAC.GetCBDuJour(PaiementReel.TypeEncaissement.CB, DateTime.Now.Date.AddYears(-1), DateTime.Now.Date, "CBDUJOUR");


            Dictionary<int, PaiementReel> dico = new Dictionary<int, PaiementReel>();

            foreach (DataRow r in dt.Rows)
            {
                PaiementReel ec = Builders.BuildPaiementReel.Build(r);


                if (dico.ContainsKey(ec.Id))
                {
                    dico[ec.Id].Patients += " + " + ec.Patients;

                    foreach (int i in ec.lstpatient)
                        dico[ec.Id].lstpatient.Add(i);
                }
                else
                {
                    dico.Add(ec.Id, ec);
                }


            }


            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (KeyValuePair<int, PaiementReel> kv in dico)
                lst.Add(kv.Value);

            return lst;
        }
        public static List<PaiementReel> GetEspecesDuJour()
        {
            JArray json = DAC.getMethodeJsonArray("/PaiementReelsDuJour/" + new DateTime(2012, 1, 1).ToString("yyyy-MM-dd HH:mm:ss") + "&" + DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss") + "&ESPDUJOUR" + "&" + Convert.ToInt32(PaiementReel.TypeEncaissement.Especes));
            Dictionary<int, PaiementReel> dico = new Dictionary<int, PaiementReel>();

            foreach (JObject r in json)
            {
               
                    PaiementReel ec = Builders.BuildPaiementReel.BuildJ(r);

                    try
                    {
                    if (dico.ContainsKey(ec.Id))
                    {
                        dico[ec.Id].Patients += " + " + ec.Patients;

                        foreach (int i in ec.lstpatient)
                            dico[ec.Id].lstpatient.Add(i);
                    }
                    else
                    {
                        dico.Add(ec.Id, ec);
                    }
                }catch(Exception e)
                {
                }

            }
            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (KeyValuePair<int, PaiementReel> kv in dico)
                lst.Add(kv.Value);

            return lst;
        }

        public static List<PaiementReel> GetEspecesDuJourOLD()
        {
            DataTable dt = DAC.GetPaiementReelsDuJour(PaiementReel.TypeEncaissement.Especes, new DateTime(2012, 1, 1), DateTime.Now.Date, "ESPDUJOUR");

            Dictionary<int, PaiementReel> dico = new Dictionary<int, PaiementReel>();

            foreach (DataRow r in dt.Rows)
            {
                PaiementReel ec = Builders.BuildPaiementReel.Build(r);


                if (dico.ContainsKey(ec.Id))
                {
                    dico[ec.Id].Patients += " + " + ec.Patients;

                    foreach (int i in ec.lstpatient)
                        dico[ec.Id].lstpatient.Add(i);
                }
                else
                {
                    dico.Add(ec.Id, ec);
                }


            }
            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (KeyValuePair<int, PaiementReel> kv in dico)
                lst.Add(kv.Value);

            return lst;
        }

        public static List<PaiementReel> GetEspeces(DateTime dte1, DateTime dte2, Utilisateur praticien)
        {
            List<PaiementReel> liste = new List<PaiementReel>();

            //url: /getPaiementsReel/{debut}&{fin}&{idEntiteJuridique}&{typePaiement}&{inCludeCN}&{idPraticien}

            string method = "/getPaiementsReel/" + dte1.ToString("yyyy-MM-dd HH:mm:ss");
            method += "&" + dte2.ToString("yyyy-MM-dd HH:mm:ss");
            method += "&" + -1;// 
            method += "&" + 1;// type de payement Espece=1
            method += "&" + true;
            method += praticien != null ? "&" + praticien.Id : "&" + -1;

            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);
            Dictionary<int, PaiementReel> dico = new Dictionary<int, PaiementReel>();

            foreach (JObject obj in jArray)
            {
                PaiementReel ec = Builders.BuildPaiementReel.BuildJ(obj);

                if (dico.ContainsKey(ec.Id))
                {
                    dico[ec.Id].Patients += " + " + ec.Patients;

                    foreach (int i in ec.lstpatient)
                        dico[ec.Id].lstpatient.Add(i);
                }
                else
                {
                    dico.Add(ec.Id, ec);
                }

            }
            foreach (KeyValuePair<int, PaiementReel> kv in dico)
                liste.Add(kv.Value);

            return liste;
        }

        public static List<PaiementReel> GetEspecesOld(DateTime dte1, DateTime dte2, Utilisateur praticien)
        {
            DataTable dt = DAC.GetPaiementsReels(dte1, dte2, null, PaiementReel.TypeEncaissement.Especes, true, praticien);

            Dictionary<int, PaiementReel> dico = new Dictionary<int, PaiementReel>();

            foreach (DataRow r in dt.Rows)
            {
                PaiementReel ec = Builders.BuildPaiementReel.Build(r);
                if (dico.ContainsKey(ec.Id))
                {
                    dico[ec.Id].Patients += " + " + ec.Patients;

                    foreach (int i in ec.lstpatient)
                        dico[ec.Id].lstpatient.Add(i);
                }
                else
                {
                    dico.Add(ec.Id, ec);
                }


            }
            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (KeyValuePair<int, PaiementReel> kv in dico)
                lst.Add(kv.Value);

            return lst;
        }



        public static List<PnFCheck> GetPaiementFrac()
        {
            JArray json = DAC.getMethodeJsonArray("/PaiementFrac");

            List<PnFCheck> lst = new List<PnFCheck>();

            foreach (JObject r in json)
            {
                PnFCheck ec = Builders.BuildPnFCheck.BuildJ(r);

                lst.Add(ec);


            }


            return lst;
        }
        public static List<PnFCheck> GetPaiementFracOLD()
        {
            DataTable dt = DAC.GetPaiementFrac("CTRLCREDIT", DateTime.Now.AddYears(-1), DateTime.Now.AddDays(5));

            List<PnFCheck> lst = new List<PnFCheck>();

            foreach (DataRow r in dt.Rows)
            {
                PnFCheck ec = Builders.BuildPnFCheck.Build(r);

                lst.Add(ec);


            }


            return lst;
        }
        public static List<PaiementReel> GetChequeDuJour(DateTime datedujour)
        {
            JArray json = DAC.getMethodeJsonArray("/PaiementReelsDuJour/" + datedujour.Date.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss") + "&" + datedujour.Date.ToString("yyyy-MM-dd HH:mm:ss") + "&CHQDUJOUR" + "&" + Convert.ToInt32(PaiementReel.TypeEncaissement.Cheque));
            Dictionary<int, PaiementReel> dico = new Dictionary<int, PaiementReel>();

            foreach (JObject r in json)
            {
                PaiementReel ec = Builders.BuildPaiementReel.BuildJ(r);


                if (dico.ContainsKey(ec.Id))
                {
                    dico[ec.Id].Patients += " + " + ec.Patients;

                    foreach (int i in ec.lstpatient)
                        dico[ec.Id].lstpatient.Add(i);
                }
                else
                {
                    dico.Add(ec.Id, ec);
                }


            }
            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (KeyValuePair<int, PaiementReel> kv in dico)
                lst.Add(kv.Value);

            return lst;
        }
        public static List<PaiementReel> GetChequeDuJourOLD(DateTime datedujour)
        {
            DataTable dt = DAC.GetPaiementReelsDuJour(PaiementReel.TypeEncaissement.Cheque, datedujour.Date.AddYears(-1), datedujour.Date, "CHQDUJOUR");

            Dictionary<int, PaiementReel> dico = new Dictionary<int, PaiementReel>();

            foreach (DataRow r in dt.Rows)
            {
                PaiementReel ec = Builders.BuildPaiementReel.Build(r);


                if (dico.ContainsKey(ec.Id))
                {
                    dico[ec.Id].Patients += " + " + ec.Patients;

                    foreach (int i in ec.lstpatient)
                        dico[ec.Id].lstpatient.Add(i);
                }
                else
                {
                    dico.Add(ec.Id, ec);
                }


            }
            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (KeyValuePair<int, PaiementReel> kv in dico)
                lst.Add(kv.Value);

            return lst;
        }

        public static List<PaiementReel> GetPaiementReelsARemettreEnBanque(DateTime dte1, DateTime dte2, PaiementReel.TypeEncaissement type, EntiteJuridique entity)
        {
            DataTable dt = DAC.GetPaiementReelsARemettreEnBanque(dte1, dte2, type, entity);

            Dictionary<int, PaiementReel> dico = new Dictionary<int, PaiementReel>();

            foreach (DataRow r in dt.Rows)
            {
                PaiementReel ec = Builders.BuildPaiementReel.Build(r);


                if (dico.ContainsKey(ec.Id))
                {
                    dico[ec.Id].Patients += " + " + ec.Patients;

                    foreach (int i in ec.lstpatient)
                        dico[ec.Id].lstpatient.Add(i);
                }
                else
                {
                    dico.Add(ec.Id, ec);
                }


            }
            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (KeyValuePair<int, PaiementReel> kv in dico)
                lst.Add(kv.Value);

            return lst;
        }

        /*
        public List<PaiementReel> FindCheque(string numcheque)
        {
            DataTable dt = DAC.FindCheques(numcheque);

            List<PaiementReel> _listEncaissements = new List<PaiementReel>();

            foreach (DataRow r in dt.Rows)
            {
                PaiementReel ec = Builders.BuildPaiementReel.Build(r);
                _listEncaissements.Add(ec);

            }

            return _listEncaissements;
        }

        */

        public static List<PaiementReel> GetPaiementReelsRemisEnBanque(DateTime dte1, DateTime dte2, PaiementReel.TypeEncaissement type, EntiteJuridique entity)
        {

            DataTable dt = DAC.GetPaiementReelsRemisEnBanque(dte1, dte2, type, entity);

            Dictionary<int, PaiementReel> dico = new Dictionary<int, PaiementReel>();


            foreach (DataRow r in dt.Rows)
            {
                PaiementReel ec = Builders.BuildPaiementReel.Build(r);


                if (dico.ContainsKey(ec.Id))
                {
                    dico[ec.Id].Patients += " + " + ec.Patients;

                    foreach (int i in ec.lstpatient)
                        dico[ec.Id].lstpatient.Add(i);
                }
                else
                {
                    dico.Add(ec.Id, ec);
                }


            }
            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (KeyValuePair<int, PaiementReel> kv in dico)
                lst.Add(kv.Value);

            return lst;

        }


        public static PaiementReel GetPaiementReel(int Id)
        {
            DataRow dr = DAC.GetPaiementsReel(Id);

            if (dr == null) return null;
            return Builders.BuildPaiementReel.Build(dr);
        }

        public static List<PaiementReel> GetPaiementReels(DateTime dateDebut, DateTime datefin, EntiteJuridique entite, PaiementReel.TypeEncaissement typepaiement, Utilisateur praticien)
        {
            string method = "/findPaiementReelMultiCritere/" + dateDebut.ToString("yyyy-MM-dd HH:mm:ss");
            method += "&" + datefin.ToString("yyyy-MM-dd HH:mm:ss");
            method += "&" + false;
            method += typepaiement == PaiementReel.TypeEncaissement.Tous ? "&-1" : "&" + (int)typepaiement;
            method += entite == null ? "&-1" : "&" + entite.Id;
            method += praticien == null ? "&-1" : "&" + praticien.Id;

            List<PaiementReel> liste = new List<PaiementReel>();
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);
            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildPaiementReel.BuildJdbc(obj));
            return liste;

        }

        public static List<PaiementReel> GetPaiementReelsByCombination(DateTime dateDebut, DateTime datefin, EntiteJuridique entite, PaiementReel.TypeEncaissement typepaiement, Utilisateur praticien)
        {
            string method = "";

            if (typepaiement == PaiementReel.TypeEncaissement.Tous)
            {
                // recherche pour tous mode payement entre 2 dates
                method = "/findByDateTousMode/" + dateDebut.ToString("yyyy-MM-dd HH:mm:ss") + "&" + datefin.ToString("yyyy-MM-dd HH:mm:ss");
                if (entite != null && praticien != null)
                {
                    //recherche par entite et praticien pour tous mode payement
                    method = "/findByDateTousModeAndEntiteAndPraticien/" + dateDebut.ToString("yyyy-MM-dd HH:mm:ss") + "&" + datefin.ToString("yyyy-MM-dd HH:mm:ss");
                    method += "&" + entite.Id + "&" + praticien.Id;
                }
                else
                {
                    // recherche par entite et tous mode payement
                    if (entite != null)
                    {
                        method = "/findByDateTousModeAndEntite/" + dateDebut.ToString("yyyy-MM-dd HH:mm:ss") + "&" + datefin.ToString("yyyy-MM-dd HH:mm:ss");
                        method += "&" + entite.Id;
                    }

                    else
                    //recherche par pratitien et tou mode payement
                    {
                        method = "/findByDateTousModeAndPraticien/" + dateDebut.ToString("yyyy-MM-dd HH:mm:ss") + "&" + datefin.ToString("yyyy-MM-dd HH:mm:ss");
                        method += "&" + praticien.Id;
                    }
                }
            }
            else
            {
                //recherche par defaut 2 date et mode payement indiqué
                int modePayement = (int)typepaiement;
                string parameters = dateDebut.ToString("yyyy-MM-dd HH:mm:ss") + "&"
                    + datefin.ToString("yyyy-MM-dd HH:mm:ss") + "&"
                    + modePayement + "&false";

                if (entite != null && praticien != null)
                {
                    parameters += "&" + entite.Id + "&" + praticien.Id;
                    method += "/findByDefaultsAndEntiteAndPraticien/" + parameters;
                }
                else if (entite != null || praticien != null)
                {
                    if (entite != null)
                    {
                        parameters += "&" + entite.Id;
                        method = "/findByDatesAndModePaiementAndEntite/" + parameters;
                    }
                    else
                    {
                        parameters += "&" + praticien.Id;
                        method = "/findByDatesAndModePaiementAndPraticien/" + parameters;
                    }

                }
                else
                    method = "/findByDatesAndModePaiement/" + parameters;
            }

            List<PaiementReel> liste = new List<PaiementReel>();
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);
            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildPaiementReel.BuildJ(obj));
            return liste;

        }


        public static List<PaiementReel> GetPaiementReelsOld(DateTime dateDebut, DateTime datefin, EntiteJuridique entite, PaiementReel.TypeEncaissement typepaiement, Utilisateur praticien)
        {
            DataTable dt = DAC.GetPaiementsReels(dateDebut, datefin, entite, typepaiement, praticien);

            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (DataRow r in dt.Rows)
            {
                PaiementReel ec = Builders.BuildPaiementReel.Build(r);
                lst.Add(ec);

            }

            return lst;
        }


        public static List<PaiementReel> GetPaiementReels(BordereauFinance bf)
        {
            DataTable dt = DAC.GetPaiementsReels(bf);

            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (DataRow r in dt.Rows)
            {
                PaiementReel ec = Builders.BuildPaiementReel.Build(r);
                lst.Add(ec);

            }

            return lst;
        }


        public static List<PaiementReel> GetPaiementReels(basePatient patient)
        {
            List<PaiementReel> lst = new List<PaiementReel>();
            JArray obj = BasCommon_DAL.DAC.getMethodeJsonArray("/paiementsReelByIdPatient/" + patient.Id + "&" + Convert.ToInt32(MgmtEncaissement.includeCN));

            foreach (JObject j in obj)
            {

                PaiementReel ec = Builders.BuildPaiementReel.BuildJ(j);
                lst.Add(ec);
            }


            return lst;
        }
        public static List<PaiementReel> GetPaiementReelsOLD(basePatient patient)
        {
            DataTable dt = DAC.GetPaiementsReels(patient.Id, includeCN);

            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (DataRow r in dt.Rows)
            {
                PaiementReel ec = Builders.BuildPaiementReel.Build(r);
                lst.Add(ec);

            }

            return lst;
        }
        public static List<PaiementReel> GetPaiementADiffererDuJour(PaiementReel.TypeEncaissement type)
        {
            JArray json = DAC.getMethodeJsonArray("/GetPaiementADiffererDuJour/" + DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss") + "&" + Convert.ToInt32(type));

            Dictionary<int, PaiementReel> dico = new Dictionary<int, PaiementReel>();

            foreach (JObject r in json)
            {
                PaiementReel ec = Builders.BuildPaiementReel.BuildJ(r);


                if (dico.ContainsKey(ec.Id))
                {
                    dico[ec.Id].Patients += " + " + ec.Patients;

                    foreach (int i in ec.lstpatient)
                        dico[ec.Id].lstpatient.Add(i);
                }
                else
                {
                    dico.Add(ec.Id, ec);
                }


            }
            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (KeyValuePair<int, PaiementReel> kv in dico)
                lst.Add(kv.Value);



            //List<PaiementReel> lst = new List<PaiementReel>();

            //foreach (DataRow r in dt.Rows)
            //{
            //    PaiementReel ec = Builders.BuildPaiementReel.Build(r);
            //    lst.Add(ec);

            //}

            return lst;
        }

        public static List<PaiementReel> GetPaiementADiffererDuJourOLD(PaiementReel.TypeEncaissement type)
        {
            DataTable dt = DAC.GetPaiementADiffererDuJour(type);


            Dictionary<int, PaiementReel> dico = new Dictionary<int, PaiementReel>();

            foreach (DataRow r in dt.Rows)
            {
                PaiementReel ec = Builders.BuildPaiementReel.Build(r);


                if (dico.ContainsKey(ec.Id))
                {
                    dico[ec.Id].Patients += " + " + ec.Patients;

                    foreach (int i in ec.lstpatient)
                        dico[ec.Id].lstpatient.Add(i);
                }
                else
                {
                    dico.Add(ec.Id, ec);
                }


            }
            List<PaiementReel> lst = new List<PaiementReel>();

            foreach (KeyValuePair<int, PaiementReel> kv in dico)
                lst.Add(kv.Value);



            //List<PaiementReel> lst = new List<PaiementReel>();

            //foreach (DataRow r in dt.Rows)
            //{
            //    PaiementReel ec = Builders.BuildPaiementReel.Build(r);
            //    lst.Add(ec);

            //}

            return lst;
        }





        public static double GetTotalEncaissements(basePatient patient)
        {
            return DAC.GetTotalEncaissements(patient);


        }
        public static List<Encaissement> GetEncaissements(basePatient patient)
        {

            List<Encaissement> _listEncaissements = new List<Encaissement>();
            JArray obj = BasCommon_DAL.DAC.getMethodeJsonArray("/encaissementByIdPatient/" + patient.Id + "&" + Convert.ToInt32(MgmtEncaissement.includeCN));

            foreach (JObject j in obj)
            {

                Encaissement ec = Builders.BuildEncaissement.BuildJ(j);
                _listEncaissements.Add(ec);
            }

            return _listEncaissements;

        }
        public static List<Encaissement> GetEncaissementsOLD(basePatient patient)
        {

            DataTable dt = DAC.GetEncaissements(patient.Id, includeCN);

            List<Encaissement> _listEncaissements = new List<Encaissement>();

            foreach (DataRow r in dt.Rows)
            {
                Encaissement ec = Builders.BuildEncaissement.Build(r);
                _listEncaissements.Add(ec);

            }

            return _listEncaissements;

        }

        public static List<Encaissement> GetEncaissements(PaiementReel pr)
        {

            DataTable dt = DAC.GetEncaissements(pr);

            List<Encaissement> _listEncaissements = new List<Encaissement>();

            foreach (DataRow r in dt.Rows)
            {
                Encaissement ec = Builders.BuildEncaissement.Build(r);
                _listEncaissements.Add(ec);

            }

            return _listEncaissements;

        }

        public static List<Encaissement> GetEncaissements(bool IncludeAlreadyREmisEnBanque, DateTime dte1, DateTime dte2, PaiementReel.TypeEncaissement tpeEnc, EntiteJuridique entity)
        {
            DataTable dt = DAC.GetEncaissements(IncludeAlreadyREmisEnBanque, dte1, dte2, tpeEnc, entity, includeCN);

            List<Encaissement> _listEncaissements = new List<Encaissement>();

            foreach (DataRow r in dt.Rows)
            {
                Encaissement ec = Builders.BuildEncaissement.Build(r);
                _listEncaissements.Add(ec);

            }

            return _listEncaissements;
        }

        public static DateTime GetNextPrelevementDay()
        {
            MgmtEncaissement.AuthorizedDays.Sort();

            foreach (int i in MgmtEncaissement.AuthorizedDays)
            {
                DateTime dte = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
                if (dte < DateTime.Now) continue;
                return dte;
            }

            return DateTime.Now;
        }
    }
}
