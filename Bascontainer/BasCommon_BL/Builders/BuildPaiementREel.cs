﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;
using Newtonsoft.Json.Linq;
namespace BasCommon_BL.Builders
{

    public static class BuildPaiementReel
    {




        public static PaiementReel Build(DataRow r)
        {
            PaiementReel cs = new PaiementReel();
            cs.Id = Convert.ToInt32(r["ID"]);
            cs.Montant = Convert.ToDouble(r["MONTANT"]);
            cs.typeencaissement = (PaiementReel.TypeEncaissement)Convert.ToInt32(r["MOYENPAIEMENT"]);
            cs.DateEncaissement = Convert.ToDateTime(r["DATEENCAISSEMENT"]);
            cs.NumCheque = Convert.ToString(r["NUMCHEQUE"]);
            cs.BanqueEmetrice = r["ID_BANQUE_EMETRICE"] is DBNull ? null : BanqueMgmt.getBanque(Convert.ToInt32(r["ID_BANQUE_EMETRICE"]));
            cs.EstRemisEnBanque = r["REMISENBANQUE"] is DBNull ? PaiementReel.RemisEnBanque.NA : (PaiementReel.RemisEnBanque)Convert.ToInt16(r["REMISENBANQUE"]);
            cs.DateRemiseEnBanque = r["DATEREMISEENBANQUE"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEREMISEENBANQUE"]);
            cs.EntiteJuridique = r["ID_ENTITYJURIDIQUE"] is DBNull ? null : EntiteJuridiqueMgmt.getentite(Convert.ToInt32(r["ID_ENTITYJURIDIQUE"]));
            cs.IdPayeur = r["Idpayeur"] is DBNull ? -1 : Convert.ToInt32(r["Idpayeur"]);
            cs.payeur = r["payeur"] is DBNull ? null : Convert.ToString(r["payeur"]);
            cs.MontantEnBanque = r["MONTANT_EN_BANQUE"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT_EN_BANQUE"]);
            cs.BanqueDeRemise = r["ID_BANQUE_REMISE"] is DBNull ? null : BanqueMgmt.getBanqueDeRemise(Convert.ToString(r["ID_BANQUE_REMISE"]));
            cs.DateEcheance = r["DATEECHEANCE"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEECHEANCE"]);
            cs.Mutuelle = r["ID_MUTUELLE"] is DBNull ? null : MutuelleMgmt.getMutuelle(Convert.ToInt32(r["ID_MUTUELLE"]));
            cs.Status = r["STATUS"] == DBNull.Value ? PaiementReel.statusEncaissement.None : (PaiementReel.statusEncaissement)Convert.ToInt32(r["STATUS"]);
            cs.DateValeurBqe = (r["DATEVALEURBANQUE"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["DATEVALEURBANQUE"]));
            cs.IsInBlackCase = r["INCN"] is DBNull ? false : Convert.ToString(r["INCN"]).Trim() == "True";

            cs.EspecesRecu = r["EspecesRecu"] == DBNull.Value ? 0 : Convert.ToDouble(r["EspecesRecu"]);
            cs.EspecesRendus = r["EspecesRendus"] == DBNull.Value ? 0 : Convert.ToDouble(r["EspecesRendus"]);
            cs.EspecesMisEncaisse = r["EspecesMisEncaisse"] == DBNull.Value ? 0 : Convert.ToDouble(r["EspecesMisEncaisse"]);

            cs.IsPnf = r["ISPNF"] is DBNull ? false : Convert.ToString(r["ISPNF"]) == "True";
            cs.MontantRemis = r["MONTANTREMIS"] is DBNull ? 0 : Convert.ToDouble(r["MONTANTREMIS"]);

            if ((r.Table.Columns.Contains("IDPATIENT")) && (!(r["IDPATIENT"] is DBNull)))
            {
                cs.lstpatient = new List<int>();
                cs.lstpatient.Add(Convert.ToInt32(r["IDPATIENT"]));
            }

            if ((r.Table.Columns.Contains("PATIENT")) && (!(r["PATIENT"] is DBNull)))
            {
                cs.Patients += Convert.ToString(r["PATIENT"]);
            }


            if ((r.Table.Columns.Contains("IDPATIENTS")) && (!(r["IDPATIENTS"] is DBNull)))
            {
                cs.lstpatient = new List<int>();
                string ss = Convert.ToString(r["IDPATIENTS"]);
                foreach (string s in ss.Split(','))
                    cs.lstpatient.Add(Convert.ToInt32(s));



            }

            if ((r.Table.Columns.Contains("PATIENTS")) && (!(r["PATIENTS"] is DBNull)))
            {
                string ss = Convert.ToString(r["PATIENTS"]);
                foreach (string s in ss.Split(','))
                {
                    if (!string.IsNullOrEmpty(cs.Patients)) cs.Patients += ',';
                    cs.Patients += s;
                }
            }

            return cs;
        }

        public static PaiementReel BuildJdbc(JObject r)
        {
            PaiementReel cs = new PaiementReel();
            /* "id": 4359,  "montant": 1800.0,  "NUMCHEQUE": "",  "moyenpaiement": 7,  "DATEENCAISSEMENT": "2019-04-30 09:05:50", */
            cs.Id = Convert.ToInt32(r["id"]);

            cs.Montant = Convert.ToDouble(r["montant"]);
            cs.typeencaissement = r["moyenpaiement"] == null ? PaiementReel.TypeEncaissement.Inconnus : (PaiementReel.TypeEncaissement)Convert.ToInt32(r["moyenpaiement"]);
            cs.DateEncaissement = Convert.ToDateTime(r["DATEENCAISSEMENT"]);
            cs.NumCheque = Convert.ToString(r["NUMCHEQUE"]);

            /*"IDPAYEUR": 1023854, "ID_BANQUE_EMETRICE": -1,  "ID_MUTUELLE": -1,  "REMISENBANQUE": -1,"PAYEUR": "BAILLIF Olivier",*/
            cs.IdPayeur = r["IDPAYEUR"] == null ? -1 : Convert.ToInt32(r["IDPAYEUR"]);
            cs.BanqueEmetrice = r["ID_BANQUE_EMETRICE"] == null || r["ID_BANQUE_EMETRICE"].ToString() == "" ? null : BanqueMgmt.getBanque(Convert.ToInt32(r["ID_BANQUE_EMETRICE"]));
            cs.EstRemisEnBanque = Convert.ToInt16(r["REMISENBANQUE"]) == -1 ? PaiementReel.RemisEnBanque.NA : (PaiementReel.RemisEnBanque)Convert.ToInt16(r["REMISENBANQUE"]);
            cs.payeur = Convert.ToString(r["PAYEUR"]);
            cs.Mutuelle = r["ID_MUTUELLE"].ToString() == "" ? null : MutuelleMgmt.getMutuelle(Convert.ToInt32(r["ID_MUTUELLE"]));

            /*"INCN": null, "EspecesRecu": 0.0,  "EspecesRendus": 0.0,"STATUS": -1,*/
            cs.IsInBlackCase = r["INCN"] == null || r["INCN"].ToString() == "" ? false : Convert.ToString(r["INCN"]).Trim() == "True";
            cs.DateValeurBqe = String.IsNullOrEmpty(r["DateValeurBanque"].ToString()) ? null : (DateTime?)Convert.ToDateTime(r["DateValeurBanque"]);
            cs.Status = r["STATUS"].ToString() == "" ? PaiementReel.statusEncaissement.None : (PaiementReel.statusEncaissement)Convert.ToInt32(r["STATUS"]);
            cs.EspecesRecu = Convert.ToDouble(r["EspecesRecu"]);
            cs.EspecesRendus = Convert.ToDouble(r["EspecesRendus"]);

            /*"EspecesMisEncaisse": 0.0,   "ISPNF": "False",  "MONTANT_EN_BANQUE": 0.0,"ID_BANQUE_REMISE": "B5","DATEECHEANCE": "2019-04-30 09:05:43",*/
            cs.EspecesMisEncaisse = Convert.ToDouble(r["EspecesMisEncaisse"]);
            cs.IsPnf = r["ISPNF"].ToString() == "" ? false : Convert.ToString(r["ISPNF"]) == "True";
            cs.MontantEnBanque = Convert.ToDouble(r["MONTANT_EN_BANQUE"]);
            cs.BanqueDeRemise = r["ID_BANQUE_REMISE"].ToString() == "" ? null : BanqueMgmt.getBanqueDeRemise(Convert.ToString(r["ID_BANQUE_REMISE"]));
            cs.DateEcheance = r["DATEECHEANCE"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["DATEECHEANCE"]);

            /*"DATEREMISEENBANQUE": "2019-04-30 09:05:43","MONTANTREMIS": 0.0,  "ID_ENTITYJURIDIQUE": 3,*/
            cs.MontantRemis = Convert.ToDouble(r["MONTANTREMIS"]);
            cs.DateRemiseEnBanque = r["DATEREMISEENBANQUE"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["DATEREMISEENBANQUE"]);
            cs.EntiteJuridique = EntiteJuridiqueMgmt.getentite(Convert.ToInt32(r["ID_ENTITYJURIDIQUE"]));


            /*{  "PATIENTS": "BAILLIF Olivier",  "IDPATIENTS": "1023854" }*/
            if ((r["idPatient"] != null) && (!(r["idPatient"].ToString() == "")) && r["idPatient"].ToString() != "0")
            {
                cs.lstpatient = new List<int>();
                cs.lstpatient.Add(Convert.ToInt32(r["idPatient"]));
            }
            if ((r["patient"] != null) && (!(r["patient"].ToString() == "")))
            {
                cs.Patients += Convert.ToString(r["patient"]);
            }

            if ((r["IDPATIENTS"] != null) && (!(r["IDPATIENTS"].ToString() == "")))
            {
                cs.lstpatient = new List<int>();
                string ss = Convert.ToString(r["IDPATIENTS"]);
                foreach (string s in ss.Split(','))
                    cs.lstpatient.Add(Convert.ToInt32(s));



            }

            if ((r["PATIENTS"] != null) && (!(r["PATIENTS"].ToString() == "")))
            {
                string ss = Convert.ToString(r["PATIENTS"]);
                foreach (string s in ss.Split(','))
                {
                    if (!string.IsNullOrEmpty(cs.Patients)) cs.Patients += ',';
                    cs.Patients += s;
                }
            }

            cs.payeur = r["PAYEUR"].ToString();
            cs.IdPraticien = String.IsNullOrEmpty(r["ID_PRATICIEN"].ToString()) ? -1 : Convert.ToInt32(r["ID_PRATICIEN"]);

            return cs;

        
        }

      /*  public static PaiementReel BuildJ(JObject r)
        {
 
            PaiementReel cs = new PaiementReel();
            
                cs.Id = Convert.ToInt32(r["id"]);
                cs.Montant = Convert.ToDouble(r["montant"]);
                cs.typeencaissement = r["moyenPaiement"] == null ? PaiementReel.TypeEncaissement.Inconnus : (PaiementReel.TypeEncaissement)Convert.ToInt32(r["moyenPaiement"]);
                cs.DateEncaissement = Convert.ToDateTime(r["dateEncaissement"]);
                cs.NumCheque = Convert.ToString(r["numCheque"]);
               
                    cs.IdPayeur = r["idPayeur"] == null ? -1 : Convert.ToInt32(r["idPayeur"]);
                    cs.BanqueEmetrice = r["idBanqueEmetrice"] == null || r["idBanqueEmetrice"].ToString() == "" ? null : BanqueMgmt.getBanque(Convert.ToInt32(r["idBanqueEmetrice"]));
                    cs.EstRemisEnBanque = Convert.ToInt16(r["remiseBanque"]) == -1 ? PaiementReel.RemisEnBanque.NA : (PaiementReel.RemisEnBanque)Convert.ToInt16(r["remiseBanque"]);
                    cs.payeur = Convert.ToString(r["payeur"]);
                    cs.Mutuelle = r["idMutuelle"].ToString() == "" ? null : MutuelleMgmt.getMutuelle(Convert.ToInt32(r["idMutuelle"]));

                    cs.IsInBlackCase = r["incn"] == null || r["incn"].ToString() == "" ? false : Convert.ToString(r["incn"]).Trim() == "True";
                    cs.DateValeurBqe = (r["dateValeurBanque"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateValeurBanque"]));
                    cs.Status = r["status"].ToString() == "" ? PaiementReel.statusEncaissement.None : (PaiementReel.statusEncaissement)Convert.ToInt32(r["status"]);
                    cs.EspecesRecu = Convert.ToDouble(r["especeRecu"]);
                    cs.EspecesRendus = Convert.ToDouble(r["especeRendus"]);

                    cs.EspecesMisEncaisse = Convert.ToDouble(r["speceMisEnCaisse"]);
                    cs.IsPnf = r["isPnf"].ToString() == "" ? false : Convert.ToString(r["isPnf"]) == "True";
                    cs.MontantEnBanque = Convert.ToDouble(r["montantBanque"]);
                    cs.BanqueDeRemise = r["idBanqueRemise"].ToString() == "" ? null : BanqueMgmt.getBanqueDeRemise(Convert.ToString(r["idBanqueRemise"]));
                    cs.DateEcheance = r["dateEcheance"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateEcheance"]);

                    cs.MontantRemis = Convert.ToDouble(r["montantRemise"]);
                    cs.DateRemiseEnBanque = r["dateRemiseBanque"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateRemiseBanque"]);
                    cs.EntiteJuridique = EntiteJuridiqueMgmt.getentite(Convert.ToInt32(r["idEntityJuridique"]));
                


        if ((r["idPatient"] != null) && (!(r["idPatient"].ToString() == "")) && r["idPatient"].ToString()!= "0")
                    
            {
                cs.lstpatient = new List<int>();
                cs.lstpatient.Add(Convert.ToInt32(r["IDPATIENT"]));
            }
            if ((r["PATIENT"] != null) && (!(r["PATIENT"].ToString() == "")))
            {
                cs.Patients += Convert.ToString(r["PATIENT"]);
            }


            if ((r["IDPATIENTS"] != null) && (!(r["IDPATIENTS"].ToString() == "")))
            {
                cs.lstpatient = new List<int>();
                string ss = Convert.ToString(r["IDPATIENTS"]);
                foreach (string s in ss.Split(','))
                    cs.lstpatient.Add(Convert.ToInt32(s));

            }
            if ((r["PATIENTS"] != null) && (!(r["PATIENTS"].ToString() == "")))
            {
                string ss = Convert.ToString(r["PATIENTS"]);
                foreach (string s in ss.Split(','))
                {
                    if (!string.IsNullOrEmpty(cs.Patients)) cs.Patients += ',';
                    cs.Patients += s;
                }
            }
            cs.payeur = r["payeur"].ToString();

            //cs.IdPraticien = Convert.ToInt32(r["id_PRATICIEN"]);

            cs.IdPraticien =String.IsNullOrEmpty(r["idPraticien"].ToString()) ? -1 : Convert.ToInt32(r["idPraticien"]);



        }
       */
       
        public static PaiementReel BuildJOLD(JObject r)
        {
 
            PaiementReel cs = new PaiementReel();
            
                cs.Id = Convert.ToInt32(r["id"]);
                cs.Montant = Convert.ToDouble(r["montant"]);
                cs.typeencaissement = r["moyenPaiement"] == null ? PaiementReel.TypeEncaissement.Inconnus : (PaiementReel.TypeEncaissement)Convert.ToInt32(r["moyenPaiement"]);
                cs.DateEncaissement = Convert.ToDateTime(r["dateEncaissement"]);
                cs.NumCheque = Convert.ToString(r["numCheque"]);
               
                    cs.IdPayeur = r["idPayeur"] == null ? -1 : Convert.ToInt32(r["idPayeur"]);
                    cs.BanqueEmetrice = r["idBanqueEmetrice"] == null || r["idBanqueEmetrice"].ToString() == "" ? null : BanqueMgmt.getBanque(Convert.ToInt32(r["idBanqueEmetrice"]));
                    cs.EstRemisEnBanque = Convert.ToInt16(r["remiseBanque"]) == -1 ? PaiementReel.RemisEnBanque.NA : (PaiementReel.RemisEnBanque)Convert.ToInt16(r["remiseBanque"]);
                    cs.payeur = Convert.ToString(r["payeur"]);
                    cs.Mutuelle = r["idMutuelle"].ToString() == "" ? null : MutuelleMgmt.getMutuelle(Convert.ToInt32(r["idMutuelle"]));

                    cs.IsInBlackCase = r["incn"] == null || r["incn"].ToString() == "" ? false : Convert.ToString(r["incn"]).Trim() == "True";
                    cs.DateValeurBqe = (r["dateValeurBanque"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateValeurBanque"]));
                    cs.Status = r["status"].ToString() == "" ? PaiementReel.statusEncaissement.None : (PaiementReel.statusEncaissement)Convert.ToInt32(r["status"]);
                    cs.EspecesRecu = Convert.ToDouble(r["especeRecu"]);
                    cs.EspecesRendus = Convert.ToDouble(r["especeRendus"]);

                    cs.EspecesMisEncaisse = Convert.ToDouble(r["speceMisEnCaisse"]);
                    cs.IsPnf = r["isPnf"].ToString() == "" ? false : Convert.ToString(r["isPnf"]) == "True";
                    cs.MontantEnBanque = Convert.ToDouble(r["montantBanque"]);
                    cs.BanqueDeRemise = r["idBanqueRemise"].ToString() == "" ? null : BanqueMgmt.getBanqueDeRemise(Convert.ToString(r["idBanqueRemise"]));
                    cs.DateEcheance = r["dateEcheance"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateEcheance"]);

                    cs.MontantRemis = Convert.ToDouble(r["montantRemise"]);
                    cs.DateRemiseEnBanque = r["dateRemiseBanque"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateRemiseBanque"]);
                    cs.EntiteJuridique = EntiteJuridiqueMgmt.getentite(Convert.ToInt32(r["idEntityJuridique"]));




                    if ((r["idPatient"] != null) && (!(r["idPatient"].ToString() == "")) && r["idPatient"].ToString() != "0")
                    {
                        cs.lstpatient = new List<int>();
                        cs.lstpatient.Add(Convert.ToInt32(r["IDPATIENT"]));
                    }
            if ((r["PATIENT"] != null) && (!(r["PATIENT"].ToString() == "")))
            {
                cs.Patients += Convert.ToString(r["PATIENT"]);
            }


            if ((r["IDPATIENTS"] != null) && (!(r["IDPATIENTS"].ToString() == "")))
            {
                cs.lstpatient = new List<int>();
                string ss = Convert.ToString(r["IDPATIENTS"]);
                foreach (string s in ss.Split(','))
                    cs.lstpatient.Add(Convert.ToInt32(s));



            }

            if ((r["PATIENTS"] != null) && (!(r["PATIENTS"].ToString() == "")))
            {
                string ss = Convert.ToString(r["PATIENTS"]);
                foreach (string s in ss.Split(','))
                {
                    if (!string.IsNullOrEmpty(cs.Patients)) cs.Patients += ',';
                    cs.Patients += s;
                }
            }

            cs.payeur = r["payeur"].ToString();
            cs.IdPraticien =String.IsNullOrEmpty(r["idPraticien"].ToString()) ? -1 : Convert.ToInt32(r["idPraticien"]);

            return cs;
        }
 

        public static PaiementReel BuildJ(JObject r)
        {
            PaiementReel cs = new PaiementReel();
            cs.Id = Convert.ToInt32(r["id"]);
            cs.Montant = Convert.ToDouble(r["montant"]);
            cs.typeencaissement = (PaiementReel.TypeEncaissement)Convert.ToInt32(r["moyenPaiement"]);
            cs.DateEncaissement = Convert.ToDateTime(r["dateEncaissement"]);
            cs.NumCheque = Convert.ToString(r["numCheque"]);
            cs.BanqueEmetrice = r["idBanqueEmetrice"].ToString() == "" ? null : BanqueMgmt.getBanque(Convert.ToInt32(r["idBanqueEmetrice"]));
            cs.EstRemisEnBanque = Convert.ToInt16(r["remiseBanque"]) == -1 ? PaiementReel.RemisEnBanque.NA : (PaiementReel.RemisEnBanque)Convert.ToInt16(r["remiseBanque"]);
            cs.DateRemiseEnBanque = r["dateRemiseBanque"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateRemiseBanque"]);
            cs.EntiteJuridique = EntiteJuridiqueMgmt.getentite(Convert.ToInt32(r["idEntityJuridique"]));
            cs.IdPayeur = Convert.ToInt32(r["idPayeur"]);
            cs.payeur = Convert.ToString(r["payeur"]);
            cs.MontantEnBanque = Convert.ToDouble(r["montantBanque"]);
            cs.BanqueDeRemise = r["idBanqueRemise"].ToString() == "" ? null : BanqueMgmt.getBanqueDeRemise(Convert.ToString(r["idBanqueRemise"]));
            cs.DateEcheance = r["dateEcheance"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateEcheance"]);
            cs.Mutuelle = r["idMutuelle"].ToString() == "" ? null : MutuelleMgmt.getMutuelle(Convert.ToInt32(r["idMutuelle"]));
            cs.Status = r["status"].ToString() == "" ? PaiementReel.statusEncaissement.None : (PaiementReel.statusEncaissement)Convert.ToInt32(r["status"]);
            cs.DateValeurBqe = (r["dateValeurBanque"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateValeurBanque"]));
            cs.IsInBlackCase = r["incn"].ToString() == "" ? false : Convert.ToString(r["incn"]).Trim() == "True";

            cs.EspecesRecu = Convert.ToDouble(r["especeRecu"]);
            cs.EspecesRendus = Convert.ToDouble(r["especeRendus"]);
            cs.EspecesMisEncaisse = Convert.ToDouble(r["especeMisEnCaisse"]);

            cs.IsPnf = r["isPnf"].ToString() == "" ? false : Convert.ToString(r["isPnf"]) == "True";
            cs.MontantRemis = Convert.ToDouble(r["montantRemise"]);

            if ((r["idpatient"] != null) && (!(r["idpatient"].ToString() == "")))
            {
                cs.lstpatient = new List<int>();
                cs.lstpatient.Add(Convert.ToInt32(r["idpatient"]));
            }

            if ((r["patient"] != null) && (!(r["patient"].ToString() == "")))
            {
                cs.Patients += Convert.ToString(r["patient"]);
            }


            if ((r["idPatients"] != null) && (!(r["idPatients"].ToString() == "")))
            {
                cs.lstpatient = new List<int>();
                string ss = Convert.ToString(r["idPatients"]);
                foreach (string s in ss.Split(','))
                    cs.lstpatient.Add(Convert.ToInt32(s));



            }
            else
                cs.lstpatient = new List<int>();

            if ((r["patients"] != null) && (!(r["patients"].ToString() == "")))
            {
                string ss = Convert.ToString(r["patients"]);
                foreach (string s in ss.Split(','))
                {
                    if (!string.IsNullOrEmpty(cs.Patients)) cs.Patients += ',';
                    cs.Patients += s;
                }
            }
            cs.IdPraticien = String.IsNullOrEmpty(r["idPraticien"].ToString()) ? -1 : Convert.ToInt32(r["idPraticien"]);
            return cs;
        }
    }
}
