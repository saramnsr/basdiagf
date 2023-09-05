using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_DAL;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Net;
using Newtonsoft.Json.Linq;
using MySql.Data.Types;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace BasCommon_BL
{
    public static class baseMgmtPatient
    {

        private static string _RegistryKey = "Software\\BASE\\BASEPractice";

        private static string _RegistryKeyPref = _RegistryKey + "\\Preferences";
        private static string _CurrentCabRegistryKey = _RegistryKeyPref + "\\CurrentCab";

        public static void GetCurrentCabOnRegistry()
        {


            RegistryKey key = Registry.CurrentUser.OpenSubKey(_CurrentCabRegistryKey);

            // If the return value is null, the key doesn't exist
            if (key == null) return;

            string objValidityDate = (string)key.GetValue("ValidityDate");
            string objValidityUser = (string)key.GetValue("ValidityCab");

            key.Close();

            DateTime ValidityDate;

            if (DateTime.TryParse(objValidityDate, out ValidityDate))
            {
                int idCabinet = Convert.ToInt32(objValidityUser);
                prefix = CabinetMgmt.Cabinet.Find(c => c.Id == idCabinet).prefix;
            }
        }
        private static string _prefix = "";
        public static string prefix
        {
            get
            {
                if (_prefix == null || _prefix == "")
                    GetCurrentCabOnRegistry();
                return _prefix;
            }
            set
            {
                _prefix = "_" + value;
            }


        }
        public static string PathRest
        {
            get
            {

                return System.Configuration.ConfigurationManager.AppSettings["PathRest" + prefix];

            }
        }
        public static string token
        {
            get
            {

                return System.Configuration.ConfigurationManager.AppSettings["token" + prefix];

            }
        }
        public static void UpdateOpeningPatWithBP(int IdPat)
        {
            DAC.UpdateOpeningPatWithBP(IdPat);
        }

        public static void UpdateOpeningPatWithOrth(int IdPat)
        {
            DAC.UpdateOpeningPatWithOrth(IdPat);
        }

        public static void UpdatecoordoneesbancairePatient(basePatient patient)
        {

            DAC.UpdatecoordoneesbancairePatient(patient);

        }

        public static String RemoveDiacritics(String s)
        {
            String normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }
        public static void UpdateReglement(basePatient pat)
        {
            DAC.UpdateReglement(pat);
        }
        public static void UpdatePercentageMutuelle(basePatient pat)
        {
            DAC.UpdatePercentageMutuelle(pat);
        }

        public static void AgeToDate(DateTime d2, DateTime datenaiss, out int years, out int months, out int days)
        {

            DateTime d1 = datenaiss;

            if (d1 < d2)
            {
                DateTime d3 = d2;
                d2 = d1;
                d1 = d3;
            }

            months = 12 * (d1.Year - d2.Year) + (d1.Month - d2.Month);


            if (d1.Day < d2.Day)
            {
                months--;
                days = DateTime.DaysInMonth(d2.Year, d2.Month) - d2.Day + d1.Day;
            }
            else
            {
                days = d1.Day - d2.Day;
            }

            years = months / 12;
            months -= years * 12;
        }



        public static List<baseSmallPersonne> getRestrictedPatientsInAttente()
        {
            JArray json = DAC.getMethodeJsonArray("/PatientsInAttente/" + DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss") + "&" + DateTime.Now.Date.AddHours(24).ToString("yyyy-MM-dd HH:mm:ss"));

            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();

            foreach (JObject r in json)
                lst.Add(BasCommon_BL.Builders.BuildSmallPersonne.BuildJson(r));


            return lst;

        }

        public static List<baseSmallPersonne> getRestrictedPatientsInAttenteOLD()
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            DataTable dt = DAC.getPatientsInAttente();

            foreach (DataRow r in dt.Rows)
                lst.Add(BasCommon_BL.Builders.BuildSmallPersonne.Build(r));


            return lst;

        }

        public static List<baseSmallPersonne> getRestrictedPatientsInAttenteFor(Fauteuil f)
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            DataTable dt = DAC.getPatientsInAttenteFor(f);

            foreach (DataRow r in dt.Rows)
            {
                int idfauteuil = r["ID_FAUTEUIL"] is DBNull ? -1 : Convert.ToInt32(r["ID_FAUTEUIL"]);
                if (idfauteuil == f.Id) lst.Add(BasCommon_BL.Builders.BuildSmallPersonne.Build(r));
            }
            return lst;

        }


        public static void AffectRepertoireToPatient(basePatient pat)
        {
            string rep =  pat.Id.ToString();
            //string rep = DAC.getMethodeJsonString("/getOldRepertoireFromBasPhoto/" + pat.Id);
            pat.Repertoire = rep;
        }


        public static void setPersonnesAContacter(basePatient patient)
        {
            DAC.setPersonnesAContacter(patient);
        }


        public static List<baseSmallPersonne> getRestrictedPatients(string nom, string prenom)
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            foreach (baseSmallPersonne pat in baseMgmtPatient.getAllPatients())
            {

                if ((pat.Nom.ToUpper().StartsWith(nom.ToUpper())) && ((pat.Prenom.ToUpper().StartsWith(prenom.ToUpper())) || (prenom == "")))
                    lst.Add(pat);
            }
            return lst;

        }


        public static List<baseSmallPersonne> getRestrictedPatients(string nom, string prenom, bool usePatientOrthalis, bool IncludeArchived, string email, string Tel, string ville, string cdPostal, string adresse)
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            // Deserialise the response into a GUID
            JArray json = DAC.getMethodeJsonArray("/getAllPatientsV2/" + nom.ToUpper() + "&" + prenom.ToUpper() + "&" + email.ToUpper() + "&" + Tel.ToUpper() + "&" + ville.ToUpper() + "&" + cdPostal.ToUpper() + "&" + adresse.ToUpper() + "&" + IncludeArchived + "&" + usePatientOrthalis);
            foreach (JToken j in json)
            {
                JObject jBp = JObject.Parse(j.ToString());
                baseSmallPersonne p = Builders.BuildSmallPersonne.BuildJson(jBp);
                lst.Add(p);


            }
        
            return lst;

        }


        public static List<baseSmallPersonne> getFamillyMembers(basePatient p)
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            JArray json = DAC.getMethodeJsonArray("/PatientsFamillyMembers/" + p.Id);
            foreach (JObject r in json)
                lst.Add(BasCommon_BL.Builders.BuildSmallPersonne.BuildJson(r));


            return lst;

        }
        public static List<baseSmallPersonne> getFamillyMembersOLD(basePatient p)
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            DataTable dt = DAC.getPatientsFamillyMembers(p);

            foreach (DataRow r in dt.Rows)
                lst.Add(BasCommon_BL.Builders.BuildSmallPersonne.Build(r));


            return lst;

        }
        public static void setinfocomplementaire(InfoPatientComplementaire nfo)
        {

            HistoResponsable h = new HistoResponsable();
            h.DateEvenement = DateTime.Now;
            h.AssistanteResp = nfo.AssistanteResponsable;
            h.IdPatient = nfo.IdPatient;
            h.PraticienUnique = nfo.PraticienUnique;
            h.user = UtilisateursMgt.CurrentUtilisateur == null ? null : UtilisateursMgt.CurrentUtilisateur.Utilisateur;
            h.PaticienResp = nfo.PraticienResponsable;
            MgmtHistoResponsable.InsertHistoResponsable(h);

            DAC.setinfocomplementaire(nfo);

        }

        public static void UpdateAsInvisalign(string id_invisalign, int id_orthalis, string nom, string prenom)
        {
            DAC.UpdateAsInvisalign(id_invisalign, id_orthalis, nom, prenom);
        }





        public static bool SetInfosInvisalign(basePatient pat)
        {

            if (pat.infosinvisalign == null)
                return false;

            DAC.SetInfosInvisalign(pat);
            return true;
        }


        public static bool SetInfossmilers(basePatient pat)
        {

            if (pat.infosinvisalign == null)
                return false;

            DAC.SetInfosInvisalign(pat);
            return true;
        }


        public static InfosInvisalign GetOrCreateInvisalignInfos(basePatient pat, ref bool created)
        {
            //if (pat.infosinvisalign != null)
            //{
            //    created = false;
            //    return pat.infosinvisalign;
            //}
            JObject dr = DAC.getMethodeJsonObjet("/InvisalignInfos/" + pat.Id);
            //DataRow dr = DAC.GetInvisalignInfos(pat);
            InfosInvisalign infosinvisalign = new InfosInvisalign();

            if (dr != null)
            {

                infosinvisalign.IdInvisalign = Convert.ToString(dr["id_INVISALIGN"]);
                infosinvisalign.NomInvisalign = Convert.ToString(dr["nom_INVISALIGN"]);
                infosinvisalign.PrenomInvisalign = Convert.ToString(dr["prenom_INVISALIGN"]);

                infosinvisalign.FreqChangemnt = dr["freqchangemnt"].ToString() == "" ? InvisalignEnBouche.ChangeFrequency.S1 : (InvisalignEnBouche.ChangeFrequency)Convert.ToInt32(dr["freqchangemnt"]);
                infosinvisalign.FreqRDV = dr["freqrdv"].ToString() == "" ? InvisalignEnBouche.RDVFrequency._3Mois : (InvisalignEnBouche.RDVFrequency)Convert.ToInt32(dr["freqrdv"]);
                infosinvisalign.DateFinInvisalign = dr["datefininvisalign"].ToString() == "" ? DateTime.Now.AddYears(1) : (DateTime)(dr["datefininvisalign"]);
                infosinvisalign.TpeTrmnt = dr["tpetrmnt"].ToString() == "" ? InvisalignPrescriptionFullObj.InvisalignType.Compréhensive : (InvisalignPrescriptionFullObj.InvisalignType)Convert.ToInt32(dr["tpetrmnt"]);
                created = false;
            }
            else
            {
                infosinvisalign.NomInvisalign = pat.Nom;
                infosinvisalign.PrenomInvisalign = pat.Prenom;

                created = true;
            }

            return infosinvisalign;
        }
        public static InfosInvisalign GetOrCreateInvisalignInfosOLD(basePatient pat, ref bool created)
        {
            if (pat.infosinvisalign != null)
            {
                created = false;
                return pat.infosinvisalign;
            }
            DataRow dr = DAC.GetInvisalignInfos(pat);
            InfosInvisalign infosinvisalign = new InfosInvisalign();

            if (dr != null)
            {

                infosinvisalign.IdInvisalign = Convert.ToString(dr["id_invisalign"]);
                infosinvisalign.NomInvisalign = Convert.ToString(dr["nom_invisalign"]);
                infosinvisalign.PrenomInvisalign = Convert.ToString(dr["prenom_invisalign"]);

                infosinvisalign.FreqChangemnt = dr["FreqChangemnt"] is DBNull ? InvisalignEnBouche.ChangeFrequency.S1 : (InvisalignEnBouche.ChangeFrequency)Convert.ToInt32(dr["FreqChangemnt"]);
                infosinvisalign.FreqRDV = dr["FreqRDV"] is DBNull ? InvisalignEnBouche.RDVFrequency._3Mois : (InvisalignEnBouche.RDVFrequency)Convert.ToInt32(dr["FreqRDV"]);
                infosinvisalign.DateFinInvisalign = dr["DateFinInvisalign"] is DBNull ? DateTime.Now.AddYears(1) : (DateTime)((MySqlDateTime)(dr["DateFinInvisalign"]));
                infosinvisalign.TpeTrmnt = dr["TpeTrmnt"] is DBNull ? InvisalignPrescriptionFullObj.InvisalignType.Compréhensive : (InvisalignPrescriptionFullObj.InvisalignType)Convert.ToInt32(dr["TpeTrmnt"]);
                created = false;
            }
            else
            {
                infosinvisalign.NomInvisalign = pat.Nom;
                infosinvisalign.PrenomInvisalign = pat.Prenom;

                created = true;
            }

            return infosinvisalign;
        }

        public static bool GetInvisalignInfos(basePatient pat)
        {

            bool created = false;
            InfosInvisalign nfo = GetOrCreateInvisalignInfos(pat, ref created);

            /*select id_orthalis, 
                                       id_invisalign, 
                                       nom_invisalign, 
                                       prenom_invisalign
                                from patient_invisalign;
                                where id_orthalis=@id*/


            pat.infosinvisalign = nfo;
            return created;

        }
        public static List<LienCorrespondant> getPersonnesAContacter(basePatient patient)
        {
            JObject json = DAC.getMethodeJsonObjet("/getPersonnesAContacter/" + patient.Id);
            List<LienCorrespondant> lst = new List<LienCorrespondant>();
            lst.Add(Builders.BuildLienCorrespondant.BuildJson(json));
            return lst;
        }

        public static List<LienCorrespondant> getPersonnesAContacterOld(basePatient patient)
        {
            List<LienCorrespondant> lst = new List<LienCorrespondant>();


            JArray array = BasCommon_DAL.DAC.getMethodeJsonArray("/getPersonnesAContacter/" + patient.Id); ;

            foreach (JObject r in array)
            {
                lst.Add(Builders.BuildLienCorrespondant.BuildJson(r));
            }
            return lst;
        }

        public static List<LienCorrespondant> getPersonnesAContacterold(basePatient patient)
        {
            List<LienCorrespondant> lst = new List<LienCorrespondant>();
            DataTable dt;

            dt = DAC.getPersonnesAContacter(patient);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildLienCorrespondant.Build(r));
            }
            return lst;
        }



        public static void FillInfocomplementaire(basePatient pat)
        {
            pat.infoscomplementaire = getinfocomplementaire(pat.Id);


        }

        public static InfoPatientComplementaire getinfocomplementaireOld(int Idpat)
        {
            DataRow dr = DAC.getinfocomplementaire(Idpat);
            if (dr != null)
                return Builders.BuildInfoPatientComplementaire.Build(dr);
            else
                return null;
        }

        public static InfoPatientComplementaire getinfocomplementaire(int Idpat)
        {
            JObject obj = DAC.getMethodeJsonObjet("/getInfoComplementaire/" + Idpat); ;
            if (obj != null)
                return Builders.BuildInfoPatientComplementaire.BuildJson(obj);
            else
                return null;
        }

        public static void setRisques(basePatient patient)
        {
            DAC.setRisques(patient);

        }

        public static void getRisques(basePatient pat)
        {
            JArray json = DAC.getMethodeJsonArray("/getRisques/" + pat.Id);
            List<String> lst = new List<string>();
            if (json != null)
            {
                foreach (String item in json)
                {
                    lst.Add(item);
                }
            }
            pat.Risques = lst;
        }

        public static void getRisquesOld(basePatient pat)
        {
            pat.Risques = DAC.getRisques(pat);
        }



        public static void ChangerStatusRelance(int Idpatient, Relance.ModeRelance Niveau)
        {
            DAC.ChangeStatusRelance(Idpatient, Niveau);
        }

        public static int GetNextNumDossier()
        {
            return Convert.ToInt32(DAC.getMethodeJsonString("/GetNextNumDossier"));
        }

        public static int GetNextNumDossierold()
        {
            return DAC.GetNextNumDossier();
        }
        public static void setLinkQ1Cs(int id)
        {
             DAC.updateLinkToQ1cs(id);
        }
        public static void deleteLinkQ1Cs(int id)
        {
            DAC.deleteLinkQ1Cs(id);
        }



        public static void SavePatient(basePatient patient)
        {
            if (patient.Id < 1)
            {
                DAC.InsertPatient(patient);
                patient.infoscomplementaire.IdPatient = patient.Id;
                DAC.setinfocomplementaire(patient.infoscomplementaire);
                if (patient.CommentsHisto != null)
                {
                    foreach (CommentHisto cmnt in patient.CommentsHisto)
                    {
                        cmnt.IdPatient = patient.Id;
                        DAC.InsertCommentaires(cmnt);
                    }
                }
            }
            else
            {
                DAC.UpdatePatient(patient);
            }
        }



        public static void FillContacts(basePatient co)
        {
            co.contacts = MgmtContact.getContactsOf(co.Id);


            foreach (Contact c in co.contacts)
            {
                if ((c.TypeContact == Contact.ContactType.Adresse) && (co.MainAdresse == null))
                {
                    co.MainAdresse = c;
                }

                if ((c.TypeContact == Contact.ContactType.Mail) && (co.MainMail == null))
                {
                    co.MainMail = c;
                }

                if ((c.TypeContact == Contact.ContactType.Telephone) && (co.MainTel == null))
                {
                    co.MainTel = c;
                }

                if ((c.TypeContact == Contact.ContactType.Fax) && (co.MainFax == null))
                {
                    co.MainFax = c;
                }
            }
        }


        public static List<baseSmallPersonne> getAllPatients(bool usePatientsOrthalis, bool IncludeArchived)
        {
            DataTable dt = BasCommon_DAL.DAC.getAllPatients(usePatientsOrthalis, IncludeArchived);
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();


            foreach (DataRow dr in dt.Rows)
            {
                baseSmallPersonne p = Builders.BuildSmallPersonne.Build(dr);
                lst.Add(p);
            }

            return lst;
        }
        public static void FillContacts(baseSmallPersonne co)
        {
            co.contacts = MgmtContact.getContactsOf(co.Id);

            foreach (Contact c in co.contacts)
            {
                if ((c.TypeContact == Contact.ContactType.Adresse) && (co.MainAdresse == null))
                {
                    co.MainAdresse = c;
                }

                if ((c.TypeContact == Contact.ContactType.Mail) && (co.MainMail == null))
                {
                    co.MainMail = c;
                }

                if ((c.TypeContact == Contact.ContactType.Telephone) && (co.MainTel == null))
                {
                    co.MainTel = c;
                }

                if ((c.TypeContact == Contact.ContactType.Fax) && (co.MainFax == null))
                {
                    co.MainFax = c;
                }
            }
        }

        public static List<baseSmallPersonne> getAllMateriels()
        {
            JArray array = BasCommon_DAL.DAC.getMethodeJsonArray("/getallmateriels/" + true + "&" + false);

            // DataTable dt = BasCommon_DAL.DAC.getAllMateriels(true, false);

            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();

            foreach (JObject row in array)
            {
                JObject arrayobject = JObject.Parse(row.ToString());
                baseSmallPersonne p = Builders.BuildSmallPersonne.BuildJsonMat(arrayobject);
                lst.Add(p);
            }

            return lst;
        }

        public static List<baseSmallPersonne> getAllMaterielsold()
        {
            DataTable dt = BasCommon_DAL.DAC.getAllMateriels(true, false);
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();

            foreach (DataRow dr in dt.Rows)
            {
                baseSmallPersonne p = Builders.BuildSmallPersonne.BuildMateriels(dr);
                lst.Add(p);
            }

            return lst;
        }

        public static baseMaterielCabinet getMateriel(int id)
        {
            DataRow dr = DAC.getMateriel(id);
            baseMaterielCabinet mat = Builders.BuildBaseMaterielCabinet.BuildMat(dr);
            return mat;
        }
        public static void updateMat(baseMaterielCabinet mat)
        {
            DAC.updateMateriel(mat);
        }
        public static List<baseSmallPersonne> getAllPatients()
        {
            JArray array = DAC.getMethodeJsonArray("/patient/get_all_patient");
            List<baseSmallPersonne> lstPatient = new List<baseSmallPersonne>();
            foreach (JToken item in array)
            {
                JObject arrayPatient = JObject.Parse(item.ToString());
                baseSmallPersonne p = BasCommon_BL.Builders.BuildSmallPersonne.BuildJson(arrayPatient);
                lstPatient.Add(p);
            }
            return lstPatient;
        }

        public static List<baseSmallPersonne> getAllPatientsOld()
        {
            DataTable dt = BasCommon_DAL.DAC.getAllPatients(true, false);
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();

            foreach (DataRow dr in dt.Rows)
            {
                baseSmallPersonne p = Builders.BuildSmallPersonne.Build(dr);
                lst.Add(p);
            }

            return lst;
        }

        public static basePatient GetFullPatient(int id)
        {



            JObject json = BasCommon_DAL.DAC.getMethodeJsonObjet("/patient/" + id);
            // basePatient pat = Builders.buildFullPatient.BuildPatient(json);
            basePatient pat = Builders.buildFullPatient.BuildPatient(json);
            // pat.commentairesClinique = new List<CommClinique>();
            /*  JToken actepg = json.GetValue("actepg");
              pat.ActesPG = new List<ActePG>();
              if (actepg != null)
                  foreach (JToken t in actepg)
                  {
                      JObject j = JObject.Parse(t.ToString());
                      ActePG ac = Builders.BuildActePG.BuildJ(j);
                      pat.ActesPG.Add(ac);
                  }
              */

            /////appointements
            // JToken apps = json.GetValue("apps");
            // pat.appointements = new List<RHAppointment>();
            // if (apps != null)
            //     foreach (JToken t in apps)
            //     {
            //         JObject j = JObject.Parse(t.ToString());
            //         RHAppointment tmpApp = Builders.buildFullPatient.BuildAppJson(j);
            //         tmpApp.PatientName = pat.Nom;
            //         pat.appointements.Add(tmpApp);
            //     }
            //pat.appointements =  pat.appointements.OrderBy(w => w.StartDate).ToList();
            // foreach (RHAppointment rha in pat.appointements)
            // {
            //     RHAppointment tmp = pat.appointements.Find(w => w.StartDate > rha.StartDate);
            //     if(tmp != null)
            //     {
            //         rha.NextRDV = (DateTime?)tmp.StartDate;
            //      //    rha.idNextact =tmp.Id;
            //     }


            // }
            ///////////////commClinique
            /*    JToken commClinique = json.GetValue("comms");
                pat.commentairesClinique = new List<CommClinique>();
                    foreach (JToken t in commClinique)
                   {
                       JObject j = JObject.Parse(t.ToString());
                       CommClinique c = Builders.BuildComClinique.BuildCommCliniqueJson(j);
                       c.patient = pat;
                       pat.commentairesClinique.Add(c);
                      MgmtCommentairesFaitAFaire.ManageDates(DateTime.Now, c);
                   }*/

            //JToken devis = json.GetValue("devis");
            //pat.devis_TK = new List<Devis_TK>();
            //pat.devis = new List<Devis>();
            //if (devis != null)
            //    foreach (JToken t in devis)
            //    {
            //        JObject j = JObject.Parse(t.ToString());
            //        int idTraitement = Convert.ToInt32(j.GetValue("id_TRAITEMENT"));
            //        if(id > 0)
            //        {
            //        Devis_TK d = Builders.buildFullPatient.Build_TK(j);
            //        d.patient = pat;
            //            pat.devis_TK.Add(d);
            //        }
            //        else
            //        {
            //            Devis dev = Builders.buildFullPatient.BuildDevis(j);
            //            pat.devis.Add(dev);
            //            dev.patient = pat;
            //        }

            //    }

            //JToken echeances = json.GetValue("echeances");
            //pat.Echeances = new List<Echeance>();
            //if (echeances != null)
            //    foreach (JToken t in echeances)
            //    {
            //         CommClinique cc =null;
            //        JObject j = JObject.Parse(t.ToString());
            //        ActePG tmpActe = pat.ActesPG.Find(w => w.Id == Convert.ToInt32(j["id_TRAITEMENT"]));
            //        if (tmpActe != null)
            //        {
            //            cc = pat.commentairesClinique.Find(w => w.Id == tmpActe.IdComm);
            //        }

            //        if (cc == null || !cc.desactive)
            //        {
            //            Echeance ech = Builders.buildFullPatient.BuildEcheance(j);
            //            ech.IdPatient = pat.Id;
            //            pat.Echeances.Add(ech);
            //        }


            //    }
            //JToken encaissement = json.GetValue("encaissement");
            //pat.Encaissements = new List<Encaissement>();
            //if (echeances != null)
            //    foreach (JToken t in encaissement)
            //    {
            //        JObject j = JObject.Parse(t.ToString());
            //        if (j.GetValue("paiementReel").ToString() == "")
            //            continue;
            //        JToken paiementReel = j.GetValue("paiementReel");
            //        if ((JObject.Parse(paiementReel.ToString())).GetValue("incn").ToString() == "" || (JObject.Parse(paiementReel.ToString())).GetValue("incn").ToString() == "True")
            //        {                      
            //            Encaissement enc = Builders.buildFullPatient.BuildEncaissement(j);
            //            pat.Encaissements.Add(enc);
            //        }
            //    }
            /*       JToken props = json.GetValue("props");
                   pat.propositions = new List<Proposition>();
                   if (props != null)
                       foreach (JToken t in props)
                       {
                           JObject j = JObject.Parse(t.ToString());
                           Devis tmpDevis = pat.devis.Find(w => w.Id == Convert.ToInt32(j["iddevis"]));
                           if (tmpDevis.DateArchivage != null)
                           {
                               Proposition p = Builders.buildFullPatient.BuildProposition(j); ;
                               pat.propositions.Add(p);
                           }
                       }*/
            //JToken infoComplementaire = json.GetValue("infoComplementaire");
            //JObject info = JObject.Parse(infoComplementaire.ToString());
            //pat.infoscomplementaire = Builders.buildFullPatient.BuildInfoComp(info);  

            /*    JToken patientInvisalign = json.GetValue("patientInvisalign");
                if (patientInvisalign.ToString() != "")
                {
                    JObject infopatientInvisalign = JObject.Parse(patientInvisalign.ToString());
                    pat.infosinvisalign = Builders.buildFullPatient.GetOrCreateInvisalignInfos(infopatientInvisalign);
                    if (pat.infosinvisalign != null)
                    {
                        pat.infosinvisalign.NomInvisalign = pat.Nom;
                        pat.infosinvisalign.PrenomInvisalign = pat.Prenom;

                    }
                }*/
            JToken appareilsEnbouche = json.GetValue("appareilsEnbouche");
            pat.AppareilsEnBouche = new List<BasCommon_BO.ElementsEnBouche.BO.IElementAppareil>();
            pat.ElementsEnBouche = new List<BasCommon_BO.ElementsEnBouche.BO.IElementDent>();
            foreach (JToken t in appareilsEnbouche)
            {

                JObject j = JObject.Parse(t.ToString());
                if (Convert.ToInt32(j.GetValue("id_APPAREIL")) != -1)
                {
                    BasCommon_BO.ElementsEnBouche.BO.IElementAppareil p = Builders.buildFullPatient.BuildElementAppareil(j);
                    pat.AppareilsEnBouche.Add(p);
                }
                else
                {
                    BasCommon_BO.ElementsEnBouche.BO.IElementDent p = Builders.buildFullPatient.BuildElementDent(j);
                    pat.ElementsEnBouche.Add(p);
                }

            }
            /*   JToken laboSuivi = json.GetValue("laboSuivi");
               pat.suivisBaseLabo = new List<ObjSuivi>();
                   foreach (JToken t in laboSuivi)
                   {
                        
                       JObject j = JObject.Parse(t.ToString());
                       if (j.GetValue("demande").ToString() == "[]") continue;
                       JToken demande = j.GetValue("demande");
                       foreach (JToken tj in demande)
                       {
                           JObject jt = JObject.Parse(tj.ToString());
                           if (jt.GetValue("standby").ToString() == "False" && j.GetValue("date_annulation").ToString() == "" && jt.GetValue("date_annulation").ToString() == "")
                           {
                               ObjSuivi ob = Builders.buildFullPatient.BuildObjSuivi(j);
                               pat.suivisBaseLabo.Add(ob);
                               break; ;
                           }
                       }

                   }*/
            JToken suiviSpecialiste = json.GetValue("suiviSpecialiste");
            pat.SuiviSpecialiste = new List<SuiviSpecialiste>();
            if (suiviSpecialiste != null)
                foreach (JToken t in suiviSpecialiste)
                {

                    JObject j = JObject.Parse(t.ToString());
                    SuiviSpecialiste suiv = Builders.buildFullPatient.BuildSuiviSpecialiste(j);
                    pat.SuiviSpecialiste.Add(suiv);


                }
            JToken invisalignEnBouche = json.GetValue("invisalignEnBouche");
            pat.aligneurs = new List<InvisalignEnBouche>();
            foreach (JToken t in invisalignEnBouche)
            {

                JObject j = JObject.Parse(t.ToString());
                InvisalignEnBouche inv = Builders.buildFullPatient.BuildInvisalignEnBouche(j);
                pat.aligneurs.Add(inv);


            }
            /*   JToken histostatus = json.GetValue("histostatus");
               pat.Customstatus = new List<CustomStatusClinique>();
                   foreach (JToken t in histostatus)
                   {

                       JObject j = JObject.Parse(t.ToString());
                       CustomStatusClinique cums = Builders.buildFullPatient.BuildCustomStatusClinique(j);
                       pat.Customstatus.Add(cums);


                   }
       */
            /*  ///fill contacts
                      JToken contacts = json.GetValue("contacts");
                      pat.contacts = new List<Contact>();
                      foreach (JToken t in contacts)
                      {

                          JObject j = JObject.Parse(t.ToString());
                          Contact contact = Builders.buildFullPatient.BuildContact(j);
                          pat.contacts.Add(contact);


                      }
                      foreach (Contact c in pat.contacts)
                      {
                          if ((c.TypeContact == Contact.ContactType.Adresse) && (pat.MainAdresse == null))
                          {
                              pat.MainAdresse = c;
                          }

                          if ((c.TypeContact == Contact.ContactType.Mail) && (pat.MainMail == null))
                          {
                              pat.MainMail = c;
                          }

                          if ((c.TypeContact == Contact.ContactType.Telephone) && (pat.MainTel == null))
                          {
                              pat.MainTel = c;
                          }

                          if ((c.TypeContact == Contact.ContactType.Fax) && (pat.MainFax == null))
                          {
                              pat.MainFax = c;
                          }
                      }*/
            //JToken objet = json.GetValue("objet");
            //AffectImageToPatient(pat, objet);

            return pat;

        }
        private static void AffectImageToPatient(basePatient p_pat, JToken objet)
        {
            AffectImageToPatient(basePatient.RepertoireImage + "/" + p_pat.Repertoire, p_pat, objet);
        }
        private static List<ObjImage> getObjectOf(JToken objet)
        {
            List<ObjImage> lstObjFrmBasPhoto = new List<ObjImage>();
            foreach (JToken t in objet)
            {

                JObject j = JObject.Parse(t.ToString());
                ObjImage ob = Builders.buildFullPatient.BuildObjImage(j);
                JToken lnk_attributs_objets = j.GetValue("lnk_attributs_objets");
                ob.attributs = new List<Attribut>();
                if (lnk_attributs_objets != null)
                    foreach (JToken att in lnk_attributs_objets)
                    {

                        JObject jo = JObject.Parse(att.ToString());
                        Attribut attribut = Builders.buildFullPatient.BuildAttribut(jo);
                        ob.attributs.Add(attribut);
                    }
                lstObjFrmBasPhoto.Add(ob);


            }
            return lstObjFrmBasPhoto;
        }
        private static void AffectImageToPatient(string imagefolder, basePatient p_pat, JToken objet)
        {

            if (!Directory.Exists(p_pat.Repertoire))
                baseMgmtPatient.AffectRepertoireToPatient(p_pat);

            if (p_pat.lstObjFrmBasPhoto == null)
            {
                p_pat.lstObjFrmBasPhoto = new List<ObjImage>();



                List<ObjImage> lst = getObjectOf(objet);

                foreach (ObjImage obj in lst)
                    p_pat.lstObjFrmBasPhoto.Add(obj);

                ImagesMgmt.ReaffectStandardFolders(basePatient.RepertoireImage + "/" + p_pat.Repertoire, p_pat);
            }

        }
        public static basePatient GetPatient(int id)
        {
            JObject json = BasCommon_DAL.DAC.getMethodeJsonObjet("/personne/get_patient/" + id);
            basePatient pat = Builders.BuildPatient.BuildPatientJson(json);
            if (pat == null) return null;
            return pat;
        }

        public static basePatient GetPatientOld(int id)
        {
            DataRow r = BasCommon_DAL.DAC.getPatient(id);
            if (r == null) return null;
            return Builders.BuildPatient.Build(r);
        }


        public static List<basePatient> GetPatientsEnRDVAt(DateTime dte1, DateTime dte2)
        {
            DataTable dt = BasCommon_DAL.DAC.GetPatientsEnRDVAt(dte1, dte2);
            if (dt == null) return null;

            List<basePatient> lst = new List<basePatient>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(Builders.BuildPatient.Build(dr));
            }

            return lst;
        }

        public static baseSmallPersonne GetsmallPatient(int id)
        {
            DataRow r = BasCommon_DAL.DAC.getSmallPersonneFromId(id); ;

            return Builders.BuildSmallPersonne.Build(r);
        }



        public static basePatient GetPatientFromMail(string mail)
        {
            List<Contact> lstmail = MgmtContact.getMails(mail);

            basePatient pat = null;
            foreach (Contact c in lstmail)
            {
                if ((lstmail.Count == 0) || (!c.IdPersonne.HasValue)) continue;

                pat = baseMgmtPatient.GetPatient(c.IdPersonne.Value);

                if (pat != null) break;


            }
            return pat;

        }
        public static List<baseSmallPersonne> GetMateriel()
        {


            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();

            foreach (baseSmallPersonne bp in baseMgmtPatient.getAllMateriels())
            {


                lst.Add(bp);
            }
            return lst;

        }
        public static List<baseSmallPersonne> GetPatientFromName(string name)
        {


            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();

            foreach (baseSmallPersonne bp in baseMgmtPatient.getAllPatients())
            {

                if (bp.Nom.ToUpperInvariant().Contains(name.ToUpperInvariant()))
                    lst.Add(bp);
            }
            return lst;

        }
        public static List<baseSmallPersonne> GetPatientFromName(string nom, string prenom)
        {
            DataTable dt = BasCommon_DAL.DAC.getPatientsFromName(nom, prenom);
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();

            foreach (DataRow dr in dt.Rows)
            {
                baseSmallPersonne p = Builders.BuildSmallPersonne.Build(dr);
                lst.Add(p);
            }

            return lst;



        }


        public static bool IsCMU(basePatient CurrentPatient)
        {

            if ((CurrentPatient.mutuelle > 0) && (CurrentPatient.Mutuelle == null))
                CurrentPatient.Mutuelle = MutuelleMgmt.getMutuelle(CurrentPatient.mutuelle);
            if ((CurrentPatient.IdCaisse > 0) && (CurrentPatient.caisse == null))
                CurrentPatient.caisse = CaissesManager.getCaisse(CurrentPatient.IdCaisse);



            return ((CurrentPatient.Mutuelle != null && CurrentPatient.Mutuelle.IsCMU) || (CurrentPatient.caisse != null && CurrentPatient.caisse.IsCMU));
        }

        public static bool IsTierPayant(basePatient CurrentPatient)
        {

            if ((CurrentPatient.mutuelle > 0) && (CurrentPatient.Mutuelle == null))
                CurrentPatient.Mutuelle = MutuelleMgmt.getMutuelle(CurrentPatient.mutuelle);
            if ((CurrentPatient.IdCaisse > 0) && (CurrentPatient.caisse == null))
                CurrentPatient.caisse = CaissesManager.getCaisse(CurrentPatient.IdCaisse);



            return (CurrentPatient.Mutuelle != null && CurrentPatient.Mutuelle.IsTiersPayant);
        }

        public static void AddPatient(basePatient patient)
        {
            DAC.addPatient(patient);

        }

        public static void UpdateTESTBasePractice(basePatient pat)
        {
            DAC.UpdateTESTBasePractice(pat);
        }

        public static void updateTraitemnt(basePatient pat, StatusClinique sc)
        {
            DAC.updateTraitemnt(pat, sc);
        }

    }
}
