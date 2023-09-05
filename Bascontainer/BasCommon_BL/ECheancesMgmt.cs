using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_DAL;
using BasCommon_BO;
using System.Data;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public class EcheancesMgmt
    {

        public static double GetSoldePrelevement(basePatient pat)
        {
            double tt = 0;
            if (pat == null) return tt;
            if (pat.Echeances == null) pat.Echeances = BasCommon_BL.EcheancesMgmt.GetEcheances(pat);
            foreach (Echeance ec in pat.Echeances)
                if (
                    (ec.ParPrelevement) &&
                    (ec.payeur == Echeance.typepayeur.patient) &&
                    (ec.ID_Encaissement < 0) &&
                    ((ec.DateEcheance < DateTime.Now))
                    )
                    tt += ec.Montant;

            return tt;
        }

        public static double GetSolde(basePatient pat)
        {
            double tt = 0;
            if (pat == null) return tt;
            if (pat.Echeances == null) pat.Echeances = BasCommon_BL.EcheancesMgmt.GetEcheances(pat);
            foreach (Echeance ec in pat.Echeances)
                if (
                    (!ec.ParPrelevement) &&
                    (ec.payeur == Echeance.typepayeur.patient) &&
                    (ec.ID_Encaissement < 0) &&
                    ((ec.DateEcheance < DateTime.Now))
                    )
                    tt += ec.Montant;

            return tt;
        }

        public static void GetRepartitionPaiements(ref double PartPatient, ref double PartMutuelle, ref double PartBqe, List<Echeance> lstEcheances)
        {

            foreach (Echeance ec in lstEcheances)
            {
                switch (ec.payeur)
                {
                    case Echeance.typepayeur.Banque:
                        PartBqe += ec.Montant;
                        break;
                    case Echeance.typepayeur.Mutuelle:
                    case Echeance.typepayeur.Secu:
                        PartMutuelle += ec.Montant;
                        break;
                    case Echeance.typepayeur.patient:
                        PartPatient += ec.Montant;
                        break;
                }

            }

        }


        public static string GetSummary(List<BaseTempEcheanceDefinition> lstech)
        {
            string resultat = "";

            Dictionary<double, int> DicoMontant = new Dictionary<double, int>();

            foreach (BaseTempEcheanceDefinition echdef in lstech)
            {

                if (echdef.AlreadyPayed) continue;

                if (DicoMontant.ContainsKey(echdef.Montant))
                {
                    DicoMontant[echdef.Montant]++;
                }
                else
                    DicoMontant[echdef.Montant] = 1;
            }

            foreach (KeyValuePair<double, int> kv in DicoMontant)
            {
                if (resultat != "") resultat += "\n+";
                resultat += kv.Value.ToString() + " Echéance(s) à " + kv.Key.ToString("C2");
            }

            return resultat;
        }




        public static List<Echeance> GetEcheanceAOrdonnerParUnTiers()
        {
            DataTable dt = null;            
         
                dt = DAC.GetEcheanceAOrdonnerParUnTiers();

            List<Echeance> lst = new List<Echeance>();

            foreach (DataRow r in dt.Rows)
            {
                Echeance ec = Builders.BuildEcheance.Build(r);
                lst.Add(ec);
            }

            return lst;
        }

        public static List<Echeance> GetEcheanceAEncaisserParUnTiers()
        {
            DataTable dt = DAC.GetEcheanceAEncaisserParUnTiers();

            List<Echeance> lst = new List<Echeance>();

            foreach (DataRow r in dt.Rows)
            {
                Echeance ec = Builders.BuildEcheance.Build(r);
                lst.Add(ec);
            }

            return lst;
        }

        public static List<Echeance> GetEcheanceARelancerParUnTiers()
        {
            DataTable dt = DAC.GetEcheanceARelancerParUnTiers();

            List<Echeance> lst = new List<Echeance>();

            foreach (DataRow r in dt.Rows)
            {
                Echeance ec = Builders.BuildEcheance.Build(r);
                lst.Add(ec);
            }

            return lst;
        }




        public static List<Echeance> GetEcheanceAPrelever(DateTime dte,bool uniquementALaDate,BanqueDeRemise bqe)
        {
            JArray json = DAC.getMethodeJsonArray("/EcheanceAPrelever/" + dte.ToString("yyyy-MM-dd HH:mm:ss") + "&" + bqe.Code);

            List<Echeance> lst = new List<Echeance>();

            foreach (JObject r in json)
            {
                Echeance ec = Builders.BuildEcheance.BuildJ(r);
                lst.Add(ec);
            }

            return lst;
        }
        public static List<Echeance> GetEcheanceAPreleverOLD(DateTime dte, bool uniquementALaDate, BanqueDeRemise bqe)
        {
            DataTable dt = DAC.GetEcheanceAPrelever(dte, uniquementALaDate, bqe);

            List<Echeance> lst = new List<Echeance>();

            foreach (DataRow r in dt.Rows)
            {
                Echeance ec = Builders.BuildEcheance.Build(r);
                lst.Add(ec);
            }

            return lst;
        }
        public static List<Echeance> GetEcheanceEnVirementAPointer(DateTime dte1, DateTime dte2)
        {
            DataTable dt = DAC.GetEcheanceEnVirementAPointer(dte1, dte2);

            List<Echeance> lst = new List<Echeance>();

            foreach (DataRow r in dt.Rows)
            {
                Echeance ec = Builders.BuildEcheance.Build(r);
                ec.patient = baseMgmtPatient.GetPatient(ec.IdPatient);
                lst.Add(ec);
            }

            return lst;
        }
        public static List<TempEcheanceDefinition> get_Echeances(CommClinique cc, string Type_com)
        {
            List<TempEcheanceDefinition> res = new List<TempEcheanceDefinition>();

            DataTable dt = DAC.get_Echeances(cc, Type_com);


            foreach (DataRow dr in dt.Rows)
            {
                TempEcheanceDefinition ted = Builders.BuildTempEcheance.BuildEcheance(dr);
                res.Add(ted);
            }

            return res;
        }

        public static List<Echeance> GetEcheances(Encaissement encaissement)
        {

            DataTable dt = DAC.getEcheances(encaissement);

            List<Echeance> _listEcheances = new List<Echeance>();

            foreach (DataRow r in dt.Rows)
            {
                Echeance ec = Builders.BuildEcheance.Build(r);
                _listEcheances.Add(ec);
            }

            return _listEcheances;
        }

        public static List<Echeance> GetEcheances(PaiementReel pr)
        {

            DataTable dt = DAC.getEcheances(pr);

            List<Echeance> _listEcheances = new List<Echeance>();

            foreach (DataRow r in dt.Rows)
            {
                Echeance ec = Builders.BuildEcheance.Build(r);
                _listEcheances.Add(ec);
            }

            return _listEcheances;
        }



        public static List<Echeance> GetEcheances(ActePG acte)
        {
             if (acte.patient==null)
                  if (acte.patient==null)
                        acte.patient = baseMgmtPatient.GetPatient(acte.IdPatient);
            if (acte.patient.Echeances == null)
                acte.patient.Echeances = GetEcheances(acte.patient);

            List<Echeance> lst = new List<Echeance>();

            foreach (Echeance e in acte.patient.Echeances)
            {
                if (e.IdActe == acte.Id)
                {
                    e.IdPatient = acte.IdPatient;
                    e.patient = acte.patient;
                    lst.Add(e);
                }
            }


            return lst;
        }

        public static List<Echeance> GetEcheances(ActePG acte, bool IncludeCN,bool IncludePerte = false)
        {

            DataTable dt = DAC.GetEcheances(acte,IncludeCN ,IncludePerte);

            List<Echeance> _listEcheances = new List<Echeance>();

            foreach (DataRow r in dt.Rows)
            {
                Echeance ec = Builders.BuildEcheance.Build(r);
               _listEcheances.Add(ec);
            }

            return _listEcheances;
        }

        public static List<Echeance> GetEcheances(basePatient pat)
        {
            List<Echeance> _listEcheances = new List<Echeance>();
            JArray obj = BasCommon_DAL.DAC.getMethodeJsonArray("/getEcheances/" + pat.Id + "&" + Convert.ToInt32(MgmtEncaissement.includeCN));

            foreach (JObject t in obj)
            {

                Echeance ac = Builders.BuildEcheance.BuildJ(t);
                ac.IdPatient = pat.Id;
                _listEcheances.Add(ac);
            }
         //   DataTable dt = DAC.getEcheances(pat,MgmtEncaissement.includeCN);

            //List<Echeance> _listEcheances = new List<Echeance>();

            //foreach (DataRow r in dt.Rows)
            //{
            //    Echeance ec = Builders.BuildEcheance.Build(r);
            //    ec.IdPatient = pat.Id;
            //    _listEcheances.Add(ec);
            //}
                        
            return _listEcheances;
        }

        public static List<Echeance> GetEcheanceAPrelever(int IdActe)
        {

            DataTable dt = DAC.GetEcheanceAPrelever(IdActe);

            List<Echeance> _listEcheances = new List<Echeance>();

            foreach (DataRow r in dt.Rows)
            {
                Echeance ec = Builders.BuildEcheance.Build(r);
                _listEcheances.Add(ec);
            }

            return _listEcheances;
        }

        public static List<Echeance> GetEcheanceAPerte(int IdActe)
        {

            DataTable dt = DAC.GetEcheancePerte(IdActe);

            List<Echeance> _listEcheances = new List<Echeance>();

            foreach (DataRow r in dt.Rows)
            {
                Echeance ec = Builders.BuildEcheance.Build(r);
                _listEcheances.Add(ec);
            }

            return _listEcheances;
        }
        public static List<Echeance> GetEcheances(baseSmallPersonne pat)
        {

            List<Echeance> _listEcheances = new List<Echeance>();
            JArray obj = BasCommon_DAL.DAC.getMethodeJsonArray("/EcheancesbaseSmallPersonne/" + pat.Id + "&" + Convert.ToInt32(MgmtEncaissement.includeCN));

            foreach (JObject t in obj)
            {

                Echeance ac = Builders.BuildEcheance.BuildJ(t);
                ac.IdPatient = pat.Id;
                _listEcheances.Add(ac);
            }
            return _listEcheances;
        }
        public static List<Echeance> GetEcheancesOLD(baseSmallPersonne pat)
        {

            DataTable dt = DAC.getEcheances(pat, MgmtEncaissement.includeCN);

            List<Echeance> _listEcheances = new List<Echeance>();

            foreach (DataRow r in dt.Rows)
            {
                Echeance ec = Builders.BuildEcheance.Build(r);
                ec.IdPatient = pat.Id;
                _listEcheances.Add(ec);
            }

            return _listEcheances;
        }

        public static List<Echeance> GetEcheances(Correspondant ResponsableFi)
        {
            List<Echeance> _listEcheances = new List<Echeance>();
            JArray obj = BasCommon_DAL.DAC.getMethodeJsonArray("/EcheancesCorrespondant/" + ResponsableFi.Id + "&" + Convert.ToInt32(MgmtEncaissement.includeCN));

            foreach (JObject t in obj)
            {

                Echeance ac = Builders.BuildEcheance.BuildJ(t);
                _listEcheances.Add(ac);
            }
            return _listEcheances;
        }

        public static List<Echeance> GetEcheancesOLD(Correspondant ResponsableFi)
        {

            DataTable dt = DAC.getEcheances(ResponsableFi, MgmtEncaissement.includeCN);

            List<Echeance> _listEcheances = new List<Echeance>();

            foreach (DataRow r in dt.Rows)
            {
                Echeance ec = Builders.BuildEcheance.Build(r);
                _listEcheances.Add(ec);
            }

            return _listEcheances;
        }

        public static Double GetRestantDue(basePatient pat)
        {

            if (pat.Echeances == null)
                pat.Echeances = GetEcheances(pat);

           double restantdue = 0;
           foreach (Echeance e in pat.Echeances)
                if (e.ID_Encaissement < 0)
                    restantdue += e.Montant;

            return restantdue;

        }

        public static double GetSoldeARegler(basePatient pat)
        {
            return DAC.GetSoldeAReglerAvantLe(DateTime.Now, pat.Id);
        }


        public static Echeance GetFirstEcheanceARegler(basePatient pat)
        {

            if (pat.Echeances == null)
                pat.Echeances = GetEcheances(pat);

            Echeance ech = null;

            foreach (Echeance e in pat.Echeances)
            {
                if ((e.ID_Encaissement < 0) && (e.DateEcheance != null) && (e.DateEcheance < DateTime.Now))
                {
                    if ((ech == null) || (ech.DateEcheance > e.DateEcheance))
                        ech = e;
                }
            }


            return ech;


        }

        public static List<Echeance> GetEcheancesARegler(basePatient pat)
        {

            if (pat.Echeances == null)
                pat.Echeances = GetEcheances(pat);

            List<Echeance> lst = new List<Echeance>();

            foreach (Echeance e in pat.Echeances)
            {
                if ((e.ID_Encaissement < 0) && (e.DateEcheance != null) && (e.DateEcheance < DateTime.Now))
                {
                    e.patient = pat;
                    lst.Add(e);
                }
            }


            return lst;


        }

        public static Echeance GetNextEcheances(DateTime dte, basePatient pat)
        {
            if (pat.Echeances == null)
                pat.Echeances = GetEcheances(pat);
            Echeance selected = null;
            DateTime dtemin = DateTime.MaxValue;

            foreach (Echeance e in pat.Echeances)
            {
                if ((e.ID_Encaissement < 0) && (e.DateEcheance != null) && (e.DateEcheance > DateTime.Now) && (e.DateEcheance < dtemin))
                {
                    dtemin = e.DateEcheance;
                    selected = e;
                }
            }

            selected.patient = pat;

            return selected;

        }


        public static void InsertEcheance(Echeance echeance)
        {
            DAC.InsertEcheance(echeance);
            
            if (echeance.patient == null) return;
            if (echeance.patient.Echeances!=null)
                echeance.patient.Echeances.Add(echeance);

        }


        public static void RedefinirEcheances(List<ActePG> actes, List<BaseTempEcheanceDefinition> lstEch, bool redefinirALL = false)
        {

            

            List<Echeance> lstechs = new List<Echeance>();
            List<Echeance> tmpPerte = new List<Echeance>();
            foreach (ActePG acte in actes)
            {

                DeleteEcheances(acte);
                acte.lstEcheances = new List<Echeance>();

            }
         
            foreach (BaseTempEcheanceDefinition ted in lstEch)
            {
                if (ted.AlreadyPayed ) continue;
                Echeance NewEc = new Echeance();
                NewEc.acte = ted.acte;
                NewEc.DateEcheance = ted.DAteEcheance;
                NewEc.DateFix = ted.DateFix;
                NewEc.patient = ted.acte.patient;
                NewEc.IdPatient = ted.acte.IdPatient;
                NewEc.Montant = ted.Montant;
                NewEc.Libelle = ted.Libelle;
                NewEc.ParPrelevement = ted.ParPrelevement;
                NewEc.ParVirement = ted.ParVirement;                
                NewEc.payeur = ted.payeur;
                NewEc.TypeActe  = ted.TypeActe;
                InsertEcheance(NewEc);
                //DAC.UpdateCommActeEcheance (
                lstechs.Add(NewEc);
                if (ted.acte.lstEcheances!=null)ted.acte.lstEcheances.Add(NewEc);

            }
            LogMgmt.ReEcheancement( lstechs, actes);
        }


        public static void DeleteEcheances(ActePG acte)
        {
            DAC.DeleteEcheances(acte);
            if (acte.patient == null) return;

            
            acte.lstEcheances = null;
            
        }

        public static void DeleteEcheance(Echeance echeance)
        {
            DAC.DeleteEcheance(echeance);
         
            

        }
        public static void UpdateEcheanceMontant(int Idecheance, double MontantEcheance, double AncienMontant, string LibelleEcheance)
        {
            DAC.UpdateEcheanceMontant(Idecheance, MontantEcheance, AncienMontant, LibelleEcheance);


        }

        public static void UpdateEcheance(Echeance echeance)
        {
            DAC.UpdateEcheance(echeance);


        }
        public static void UpdateEcheanceLibelle(int idTraitement, string Libele)
        {
            DAC.UpdateEcheanceLibelle(idTraitement,Libele);


        }

        public static double getTotalPerte(int id)
        {
           return Convert.ToDouble(DAC.getMethodeJsonString("/echeance/getTotalPerte/" + id).Replace(".",","));
         //   return DAC.getTotalPerte(id);        
        }
        public static Echeance getEcheanceById(int id)
        {
           // Echeance ech = new Echeance();
            DataRow dr = DAC.getEcheances(id);
            if (dr != null)
              return   Builders.BuildEcheance.Build(dr);
            else return null;
        }

        public static List<Perte> getPerteByPatientAndDates(DateTime datedebut, DateTime datefin) {
           
            List<Perte> liste = new List<Perte>();
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray("/getPerteByPatAndDates/" + datedebut.Date.ToString("yyyy-MM-dd HH:mm:ss") + "&" + datefin.Date.AddHours(23).AddMinutes(59).ToString("yyyy-MM-dd HH:mm:ss"));

            foreach (JObject obj in jArray)
            { 
            Perte p = new Perte();

            p.idPatient = Convert.ToInt32(obj["idPatient"]);
            p.nom = obj["nom"].ToString().Trim();            
            p.prenom = obj["prenom"].ToString().Trim();
            p.totalPerte = Convert.ToDouble(obj["totalPerte"]);
            liste.Add(p);
            }

            return liste;
        
        }
        public static DataTable getPerteByPatAndDates(DateTime datedebut,DateTime datefin)
        {

            return DAC.getPatientwithTotalPerteBetwenDates(datedebut, datefin);
           
        }
        
    }
}
