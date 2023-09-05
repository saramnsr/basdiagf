using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using BasCommon_DAL;
using BasCommon_BO.ElementsEnBouche.BO;
using Newtonsoft.Json.Linq;
using BasCommon_BL.Builders;

namespace BasCommon_BL
{
    public static class MgmtCommentairesFaitAFaire
    {

        public static void updateCommCliniqueIdRDV(CommClinique com)
        {
            DAC.updateCommCliniqueIdRDV(com);
        }

        

        public static DateTime? GetDateTheorique(CommClinique comcl, List<CommClinique> comms)
        {



            if (comcl.date != null)
                return comcl.date.Value;
            else
            {
                if (comms[0].date == null) return null;
                return comms[0].date.Value.AddMonths(comcl.NbMois).AddDays(comcl.NbJours);
            }
        }

        public static CommClinique GetNextComment(CommClinique comcl, List<CommClinique> lstcoms)
        {
            if (comcl == null) return null;
            if (lstcoms == null) return null;


            //Recherche d'un commentaire prevus liée à ce commentaire
            for (int i = 0; i < lstcoms.Count; i++)
            {
                CommClinique cc = lstcoms[i];
                if ((cc.IdParentComment == comcl.Id) || ((comcl.Id == -1) && (cc.ParentComment == comcl)))
                {
                    cc.etat = CommClinique.EtatCommentaire.Afaire;
                    return cc;
                }
            }


            //Sinon recherche du prochain commentaire dans la liste
            for (int i = 0; i < lstcoms.Count - 2; i++)
            {
                CommClinique cc = lstcoms[i];
                if (cc == comcl)
                {
                    lstcoms[i + 1].etat = CommClinique.EtatCommentaire.Afaire;
                    lstcoms[i + 1].ParentComment = comcl;
                    lstcoms[i + 1].IdParentComment = comcl.Id;
                    return lstcoms[i + 1];
                }
            }
            return null;
        }
        public static CommClinique GetCommCliniqueByID(int idComm)
        {
        
            DataRow dr = DAC.GetCommClinique(idComm);

            if (dr == null) return null;


            CommClinique c = Builders.BuildComClinique.BuildCommClinique(dr);
            return c;
        }
        public static CommClinique GetCurrentComment(RHAppointment app, List<CommClinique> lstcoms)
        {
            if (app == null) return null;
            if (lstcoms == null) return null;

            DateTime? StartDte = app.DateArrive;
            DateTime? EndDate = app.DateSortie == DateTime.MinValue ? null : (DateTime?)app.DateSortie;

            if (EndDate == null)
                EndDate = app.DateSecretariat == DateTime.MinValue ? null : (DateTime?)app.DateSecretariat;


            //Verifie si un commentaire n'est pas directement associé au commentaire
            foreach (CommClinique cc in lstcoms)
            {
                if (cc.IdRDV == app.Id)
                    return cc;
            }

            //Verifie si le commentaire à deja été fait
            foreach (CommClinique cc in lstcoms)
            {
                if (StartDte < cc.date)
                    if (
                         ((EndDate != null) && (cc.date < EndDate)) ||
                         ((EndDate == null) &&
                          (cc.IdActe == app.acte.id_acte))
                       )
                        return cc;
            }

            DateTime maxdte = DateTime.MinValue;
            CommClinique LastComm = null;


            //Recupere le dernier commentaire
            foreach (CommClinique cc in lstcoms)
            {
                if ((cc.date != null) && (cc.date.Value > maxdte))
                {
                    maxdte = cc.date.Value;
                    LastComm = cc;
                }
            }

            if (LastComm != null)
            {
                //Verifie si un commentaire liée au dernier commentaire existe
                foreach (CommClinique cc in lstcoms)
                {
                    if (cc.IdParentComment == -1) continue;
                    if ((cc.date == null) && (cc.IdParentComment == LastComm.Id))
                        return cc;
                }
            }

            //Verifie si un future commentaire existe
            foreach (CommClinique cc in lstcoms)
            {
                if ((cc.date == null) && (cc.IdActe == app.acte.id_acte))
                    return cc;
            }
            return null;
        }


    public static void AddCommentaire(CommClinique com,bool verifActeCS=false)
        {
            int idnextacte = 0;
            RHAppointment app = new RHAppointment();
                //ecraser 1 CS 
            if (verifActeCS)
            {
                com.patient.commentairesClinique[0] = GetCommCliniqueByID(com.patient.commentairesClinique[0].Id);
                app = AppointementsMgmt.getAppointment(com.patient.commentairesClinique[0].IdRDV);
                DAC.DeleteCommentaire(com.patient.commentairesClinique[0],true);
              
                com.patient.commentairesClinique.Remove(com.patient.commentairesClinique[0]);
              //  com.patient.commentairesClinique.Add(com);
               

                
            }
    ////////
            if(com.Id == -1)
            DAC.InsertCommentaire(com);

            if (com.ActesSupp != null)
                if (com.ActesSupp.Count > 0)
                {
                    DAC.setComCliniqueActes(com);
                    idnextacte++;
                }
            
            if (com.Radios != null)
                if (com.Radios.Count > 0)
                {
                DAC.setComCliniqueActes(com, "R");
                  idnextacte++;
                }
            if (com.photos != null && com.photos.Count != 0)
            {
                DAC.setComCliniqueActes(com, "P");
                idnextacte++;
            }
            if (com.Materiels != null && com.Materiels.Count !=0)
            {
                DAC.setComCliniqueMateriels(com);
                idnextacte++;
            }
            if (com.AutrePersonnes != null && com.AutrePersonnes.Count != 0)
            {
                DAC.setComCliniqueAutrePersonnes(com);
                idnextacte++;
            }
            if (app != null && verifActeCS)
            {
                app.patient = com.patient;
                app.idNextact = idnextacte;
                AppointementsMgmt.Delete(app, UtilisateursMgt.CurrentUtilisateur.Utilisateur, RHAppointment.AnnulerPar.Praticien);
                app.CommCl = com;
                AppointementsMgmt.AddAppointment(app, UtilisateursMgt.CurrentUtilisateur.Utilisateur);

            }
            if (com.echeancestemp != null)
            {
                foreach (TempEcheanceDefinition ted in com.echeancestemp)
                {
                    if (ted.Id == 0) 
                    MgmtDevis.update_tempechenacescc_tk(ted, com);
                }
            }

        }

        public static void InsertCommentaire(CommClinique com)
        {
          
           
            DAC.InsertCommentaire(com);


        }

        public static void SaveFullCommentaire(CommClinique com)
        {

          
            if (com.Id == -1)
            {
                DAC.InsertCommentaire(com);
            }
            else
                DAC.UpdateCommentaire(com);

            //SaveRadios(com);
            //SavePhotos(com);
            SaveMateriels(com);
            SaveAutrePersonne(com);
            SaveDentsAExtraire(com);


        }

        public static void UpdateCommentaire(CommClinique com)
        {
           
            DAC.UpdateCommentaire(com);
        }

        public static void DelCommClinique(CommClinique com, bool withEcheance)
        {
            DAC.DeleteCommentaire(com, withEcheance);
        }




        public static List<CommClinique> AddFromScenarioSemestre(ScenarioCommClinique scenario, Proposition p, basePatient patient)
        {

            List<CommClinique> lst = new List<CommClinique>();
            if (scenario == null) return lst;
            if (scenario.commentaires == null) MgmtScenarioCommClinique.FillCommCliniqueAfaire(scenario);


            List<Semestre> lstsem = new List<Semestre>();

            foreach (Traitement t in p.traitements)
                foreach (Semestre s in t.semestres)
                    lstsem.Add(s);

            

            foreach (CommCliniqueDetailsScenario cca in scenario.commentaires)
            {
                if ((cca.TypeDePeriode == CommCliniqueDetailsScenario.typePeriode.Semestre) &&
                    (cca.Numero <= lstsem.Count) &&
                    (cca.Numero > 0))
                {

                    int nbDaysToAdd = (int)(lstsem[cca.Numero - 1].DateDebut - DateTime.Now).TotalDays;


                    CommClinique cc = new CommClinique();
                    cc.IdActe = cca.IdActe;
                    cc.Commentaire = cca.Commentaire;
                    cc.date = null;
                    cc.patient = patient;
                    cc.NbJours = nbDaysToAdd+cca.NbJours;
                    cc.NbMois = cca.NbMois;
                    cc.IdScenario = cca.IdScenario;
                    cc.IsDateDeRef = cca.IsReferenceDate;
                    cc.modecreation = CommClinique.ModeCreation.Auto;
                    cc.etat = CommClinique.EtatCommentaire.Prevus;
                    Semestre selectedsemestre = lstsem[cca.Numero - 1];


                    cc.IdSemestre = selectedsemestre == null ? -1 : selectedsemestre.Id;
                    cca.RealComm = cc;
                    AddCommentaire(cc);

                    //#region photos
                    //if (cca.photos == null) MgmtScenarioCommClinique.FillCommCliniquePhotos(cca);
                    //cc.photos = new List<CommPhoto>();
                    //foreach (ScenarCommPhoto scp in cca.photos)
                    //{
                    //    ((CommPhoto)scp).Parent = cc;
                    //    cc.photos.Add((CommPhoto)scp);

                    //}
                    //SavePhotos(cc);
                    //#endregion

                    //#region radios
                    //if (cca.Radios == null) MgmtScenarioCommClinique.FillCommCliniqueRadios(cca);
                    //cc.Radios = new List<CommRadio>();
                    //foreach (ScenarCommRadio scp in cca.Radios)
                    //{
                    //    ((CommRadio)scp).Parent = cc;
                    //    cc.Radios.Add((CommRadio)scp);

                    //}
                    //SaveRadios(cc);
                    //#endregion

                    #region materiels
                    if (cca.Materiels == null) MgmtScenarioCommClinique.FillCommCliniqueMateriel(cca);
                    cc.Materiels = new List<CommMateriel>();
                    foreach (ScenarCommMateriel scp in cca.Materiels)
                    {
                        ((CommMateriel)scp).Parent = cc;
                        cc.Materiels.Add((CommMateriel)scp);

                    }
                    SaveMateriels(cc);
                    #endregion






                    lst.Add(cc);
                }
            }

            foreach (CommCliniqueDetailsScenario cca in scenario.commentaires)
            {

                if (cca.TypeDePeriode == CommCliniqueDetailsScenario.typePeriode.Semestre)
                {
                    #region EnBouches
                    if (cca.EnBouches == null) MgmtScenarioCommClinique.FillCommCliniqueEnBouche(cca);

                    foreach (ScenarEnBouche enbouche in cca.EnBouches)
                    {

                        //Evite d'enregistrer 2 fois le meme commentaire du au fait 
                        //que le commentaire de debut et le commentaire de fin on le meme appareil
                        if (enbouche.IdCommDebut != cca.Id) continue;

                        if (enbouche.IdAppareil > -1)
                        {
                            ElementAppareil app = new ElementAppareil();
                            app.Bas = enbouche.Bas;
                            app.Haut = enbouche.Haut;
                            app.IdCommDebut = enbouche.IdCommDebut;
                            app.IdCommFin = enbouche.IdCommFin;

                            AffectEnBoucheIdComms(scenario, app);
                            app.IdPatient = patient.Id;
                            app.Appareil = AppareilMgmt.getAppareil(enbouche.IdAppareil);
                            patient.AppareilsEnBouche.Add(app);
                            EnBoucheMgmt.Save(app);
                        }
                        else
                        {
                            IElementDent elemnt = EnBoucheMgmt.CreateElementFromType(enbouche.type);

                            elemnt.IdCommDebut = enbouche.IdCommDebut;
                            elemnt.IdCommFin = enbouche.IdCommFin;
                            elemnt.IdPatient = patient.Id;
                            elemnt.Dents = enbouche.Dents;
                            patient.ElementsEnBouche.Add(elemnt);
                            EnBoucheMgmt.Save(elemnt);

                        }
                    }

                    #endregion
                }
            }







            return lst;
        }

        private static void AffectEnBoucheIdComms(ScenarioCommClinique scenario, ElementAppareil app)
        {

            foreach (CommCliniqueDetailsScenario detail in scenario.commentaires)
            {
                if (app.IdCommDebut == detail.Id)
                    app.IdCommDebut = detail.RealComm.Id;

                if (app.IdCommFin == detail.Id)
                    app.IdCommFin = detail.RealComm.Id;
            }
        }

        public static List<CommClinique> AddFromScenarioContention(ScenarioCommClinique scenario, Proposition p, basePatient patient)
        {

            bool iscont2 = CodesTraitement.IsContention2( p.traitements[0].semestres[0].CodeSemestre);
            bool iscont1 = CodesTraitement.IsContention1(p.traitements[0].semestres[0].CodeSemestre);

            List<CommClinique> lst = new List<CommClinique>();
            if (scenario == null) return lst;
            if (scenario.commentaires == null) MgmtScenarioCommClinique.FillCommCliniqueAfaire(scenario);


            foreach (CommCliniqueDetailsScenario cca in scenario.commentaires)
            {
                if (cca.TypeDePeriode == CommCliniqueDetailsScenario.typePeriode.Contention)
                {

                    if (iscont2 && cca.Numero != 2) continue;
                    if (iscont1 && cca.Numero != 1) continue;

                    CommClinique cc = new CommClinique();
                    cc.IdActe = cca.IdActe;
                    cc.Commentaire = cca.Commentaire;
                    cc.date = null;
                    cc.patient = patient;
                    cc.NbJours = cca.NbJours;
                    cc.NbMois = cca.NbMois;
                    cc.IdScenario = cca.IdScenario;
                    cc.IsDateDeRef = cca.IsReferenceDate;
                    cc.modecreation = CommClinique.ModeCreation.Auto;
                    cc.etat = CommClinique.EtatCommentaire.Prevus;

                    Semestre selectedsemestre = p.traitements[0].semestres[0];


                    cc.IdSemestre = selectedsemestre == null ? -1 : selectedsemestre.Id;
                    AddCommentaire(cc);
                    lst.Add(cc);
                }

            }

            return lst;
        }


        public static List<CommClinique> AddFromScenario(ScenarioCommClinique scenario, Proposition p, basePatient patient)
        {
            if (p.traitements.Count > 0)
            {

                if (CodesTraitement.IsContention( p.traitements[0].semestres[0].CodeSemestre))
                    return AddFromScenarioContention(scenario, p, patient);
                else
                    return AddFromScenarioSemestre(scenario, p, patient);
            }

            return new List<CommClinique>();

        }

        public static void SaveDentsAExtraire(CommClinique com)
        {
            DAC.setComCliniqueDentsAExtraire(com);

        }

        public static void SaveAutrePersonne(CommClinique com)
        {
            DAC.setComCliniqueAutrePersonnes(com);

        }

        public static void SaveMateriels(CommClinique com)
        {
            DAC.setComCliniqueMateriels(com);

        }
        public static void SaveActesSupp(CommClinique com, string TYPE_ACTE_SUPP = "")
        {
            DAC.setComCliniqueActes(com, TYPE_ACTE_SUPP);

        }
        public static void setComCliniqueIsActive(CommClinique com)
        {
            DAC.setComCliniqueIsActive(com);

        }
        //public static void SaveRadios(CommClinique com)
        //{
        //    DAC.setComCliniqueRadio(com);

        //}

        //public static void SavePhotos(CommClinique com)
        //{
        //    DAC.setComCliniquePhotos(com);

        //}

        public static List<CommClinique> GetCommCliniqueByIdNDte(int Idpat, DateTime? DteDebutTrtmnt)
        {
            DataTable dt = DAC.GetCommCliniquesByIdPatient(Idpat);
            List<CommClinique> lst = new List<CommClinique>();

            foreach (DataRow r in dt.Rows)
            {
                CommClinique c = Builders.BuildComClinique.BuildCommClinique(r);
                c.IdPatient = Idpat;

                ManageDates(DteDebutTrtmnt, c);



                lst.Add(c);

            }
            return lst;
        }
        public static List<RappelleActe> GetActePoseRCC()
        {
            List<RappelleActe> lst = new List<RappelleActe>();

            string ids = System.Configuration.ConfigurationManager.AppSettings["RappelActeRCC" + basePatient.prefix];
            JArray json = DAC.getMethodeJsonArray("/CommClinique/RappelActe?str=" + ids);
            foreach (JObject j in json)
            {

                RappelleActe c = Builders.BuildComClinique.BuildRappelActe(j);
                lst.Add(c);
            }
            return lst;
        }
        public static List<RappelleActe> GetActePoseAligneurs()
        {
            List<RappelleActe> lst = new List<RappelleActe>();

            string ids = System.Configuration.ConfigurationManager.AppSettings["RappelActePoseAligneurs" + basePatient.prefix];
            JArray json = DAC.getMethodeJsonArray("/CommClinique/RappelActe?str=" + ids);
            foreach (JObject j in json)
            {

                RappelleActe c = Builders.BuildComClinique.BuildRappelActe(j);
                lst.Add(c);
            }
            return lst;

        }
        public static List<CommClinique> GetFullCommClinique(basePatient pat, DateTime? DteDebutTrtmnt)
        {
            List<CommClinique> lst = new List<CommClinique>();

            
            JArray jsonCommActes = DAC.getMethodeJsonArray("/CommActeByPatient/" + pat.Id);
            JArray jsonCommMats = DAC.getMethodeJsonArray("/GetMatsTK/" + pat.Id);
            JArray json = DAC.getMethodeJsonArray("/CommClinique/GetCommCliniquesByIdPatient?id=" + pat.Id);
            foreach (JObject j in json)
            {

                CommClinique c = Builders.BuildComClinique.BuildCommCliniqueJson(j);
                c.patient = pat;
                 ManageDates(DteDebutTrtmnt, c);
                lst.Add(c);


            }

          
            List<CommActes> lstComm = new List<CommActes>();
            foreach (JObject j in jsonCommActes)
              {
                string typeActe = j["typeActe"].ToString() ;
                  CommActes ca = Builders.BuildComClinique.BuildCommActes(j);
                  switch (typeActe)
                  {
                      case "": 
                          if( lst.Find(cm => cm.Id == ca.IdComm) != null)
                          lst.Find(cm => cm.Id == ca.IdComm).ActesSupp.Add(ca); 
                          break;
                      case "R":
                          if (lst.Find(cm => cm.Id == ca.IdComm) != null)
                          lst.Find(cm => cm.Id == ca.IdComm).Radios.Add(ca); break;
                      case "P":
                          if (lst.Find(cm => cm.Id == ca.IdComm) != null)
                          lst.Find(cm => cm.Id == ca.IdComm).photos.Add(ca); break;

                  }
                
              }
            foreach (JObject j in jsonCommMats)
            {
                CommMateriel ca = Builders.BuildComClinique.BuildCommMaterielJson(j);
                {
                   if( lst.Find(cm => cm.Id == ca.IdComm) != null)
                   lst.Find(cm => cm.Id == ca.IdComm).Materiels.Add(ca);

                }

            }
            MgmtDevis.get_tempecheancesPAT_TK(lst,pat.Id);
            
            // foreach (BaseTempEcheanceDefinition bted in com.echeancestemp)
            // {
            //     prix = prix + bted.Montant;
            // }
      /*    var elapsedMs = watch.Elapsed;
            watch = System.Diagnostics.Stopwatch.StartNew();
            DataTable dt = DAC.GetCommClinique(pat);
           

            int i = 0;

            foreach (DataRow r in dt.Rows)
            {
                CommClinique c = Builders.BuildComClinique.BuildCommClinique(r);
                c.patient = pat;

                ManageDates(DteDebutTrtmnt, c);

                
                lst.Add(c);

            }
            var elapsedMs1 = watch.Elapsed;

         /*   dt = DAC.GetCommCliniqueAutrePersonne(pat);
            List<CommAutrePersonne> lstCommAutrePersonne = new List<CommAutrePersonne>();

            foreach (DataRow r in dt.Rows)
            {
                CommAutrePersonne c = Builders.BuildComClinique.BuildCommAutrePersonne(r);
                lstCommAutrePersonne.Add(c);
            }

            
            dt = DAC.GetCommCliniqueDentAExtraire(pat);
            List<CommDentAExtraire> lsCommDentAExtrairet = new List<CommDentAExtraire>();

            foreach (DataRow r in dt.Rows)
            {
                CommDentAExtraire c = Builders.BuildComClinique.BuildCommDentAExtraire(r);
                lsCommDentAExtrairet.Add(c);
            }
            dt = DAC.GetCommCliniqueActes(pat);
            List<CommActes> lsCommCommActes = new List<CommActes>();

            foreach (DataRow r in dt.Rows)
            {
                CommActes c = Builders.BuildComClinique.BuildCommActes(r);
                lsCommCommActes.Add(c);
            }

            dt = DAC.GetCommCliniqueActes(pat, "P");

            List<CommActes> lsCommCommPhoto = new List<CommActes>();

            foreach (DataRow r in dt.Rows)
            {
                CommActes c = Builders.BuildComClinique.BuildCommActes(r);
                lsCommCommPhoto.Add(c);
            }

            dt = DAC.GetCommCliniqueActes(pat, "R");
            List<CommActes> lsCommCommRadio = new List<CommActes>();

            foreach (DataRow r in dt.Rows)
            {
                CommActes c = Builders.BuildComClinique.BuildCommActes(r);
                lsCommCommRadio.Add(c);
            }

            dt = DAC.GetCommCliniqueMateriels(pat);
            List<CommMateriel> lsCommCommMat = new List<CommMateriel>();

            foreach (DataRow r in dt.Rows)
            {
                CommMateriel c = Builders.BuildComClinique.BuildCommMateriel(r);
                lsCommCommMat.Add(c);
            }

            
            foreach (CommClinique cc in lst)
            {
                 cc.AutrePersonnes = new List<CommAutrePersonne>();
                 cc.DentsAExtraire = new List<CommDentAExtraire>();
                 cc.photos = new List<CommActes >();
                cc.Radios = new List<CommActes >();
                 cc.Materiels = new List<CommMateriel>();
                 cc.ActesSupp = new List<CommActes>();
                
                foreach (CommAutrePersonne cap in lstCommAutrePersonne)
                    if (cap.IdComm == cc.Id)
                    {
                        cc.AutrePersonnes.Add(cap);
                        cap.Parent = cc;
                    }

                foreach (CommDentAExtraire cap in lsCommDentAExtrairet)
                    if (cap.IdComm == cc.Id)
                    {
                        cc.DentsAExtraire.Add(cap);
                        cap.Parent = cc;
                    }

                foreach (CommActes cap in lsCommCommPhoto)
                    if (cap.IdComm == cc.Id)
                    {
                        cc.photos.Add(cap);
                        cap.Parent = cc;
                    }
                foreach (CommActes cap in lsCommCommActes)
                    if (cap.IdComm == cc.Id)
                    {
                        cc.ActesSupp.Add(cap);
                        cap.Parent = cc;
                    }
                foreach (CommActes cap in lsCommCommRadio)
                    if (cap.IdComm == cc.Id)
                    {
                        cc.Radios.Add(cap);
                        cap.Parent = cc;
                    }

                foreach (CommMateriel cap in lsCommCommMat)
                    if (cap.IdComm == cc.Id)
                    {
                        cc.Materiels.Add(cap);
                        cap.Parent = cc;
                    }

            }
           
            */
            return lst;
        }

        public static void ManageDates(CommClinique c)
        {
            DateTime? dtDebut = ActesPGMgmt.getDateDebutTraitement(c.patient);
            ManageDates(dtDebut, c);
        }

        public static void ManageDates(DateTime? DteDebutTrtmnt, CommClinique c)
        {
            
    
            if ((c.IdRDV > 0) && (c.Appointement == null))
            {
                List<RHAppointment> appointements = null;

                if (c.patient != null)
                {
                    if (c.patient.appointements == null)
                        c.patient.appointements = AppointementsMgmt.getAppointments(c.patient);
                    appointements = c.patient.appointements;
                }
                else
                    appointements = AppointementsMgmt.getAppointments(c.IdPatient);

                foreach (RHAppointment app in appointements)
                    if (app.Id == c.IdRDV)
                        c.Appointement = app;
                if (c.Appointement == null)
                    c.IdRDV = -1;
                else
                    if (c.Appointement.StartDate > DateTime.Now)
                        c.DatePrevisionnnelle = c.Appointement.StartDate;
                    else
                        c.date = c.Appointement.StartDate;
            }

            if (c.DatePrevisionnnelle == null)
                if (c.date != null)
                    c.DatePrevisionnnelle = c.date;
                else
                    if (DteDebutTrtmnt != null)
                        c.DatePrevisionnnelle = DteDebutTrtmnt.Value.AddMonths(c.NbMois).AddDays(c.NbJours);
        }
        public static void ManageDates(DateTime? DteDebutTrtmnt, CommClinique c, DateTime Dte)
        {    
            if ((c.IdRDV > 0) && (c.Appointement == null))
            {
                List<RHAppointment> appointements = null;
                if (c.patient != null)
                {
                    if (c.patient.appointements == null)
                        c.patient.appointements = AppointementsMgmt.getAppointmentsByDate(c.patient, Dte);
                    appointements = c.patient.appointements;
                }
                else
                    appointements = AppointementsMgmt.getAppointmentsByDate(c.IdPatient, Dte);
                foreach (RHAppointment app in appointements)
                    if (app.Id == c.IdRDV)
                        c.Appointement = app;
                if (c.Appointement == null)
                    c.IdRDV = -1;
                else
                    if (c.Appointement.StartDate > DateTime.Now)
                        c.DatePrevisionnnelle = c.Appointement.StartDate;
                    else
                        c.date = c.Appointement.StartDate;
            }
            if (c.DatePrevisionnnelle == null)
                if (c.date != null)
                    c.DatePrevisionnnelle = c.date;
                else
                    if (DteDebutTrtmnt != null)
                        c.DatePrevisionnnelle = DteDebutTrtmnt.Value.AddMonths(c.NbMois).AddDays(c.NbJours);
        }

        public static void ManageDates(DateTime? DteDebutTrtmnt, CommClinique c, DateTime Dte, int idRDV)
        {
            if ((c.IdRDV > 0) && (c.Appointement == null))
            {
                List<RHAppointment> appointements = null;
                if (c.patient != null)
                {
                    if (c.patient.appointements == null)
                        c.patient.appointements = AppointementsMgmt.getAppointmentsByDate(c.patient, Dte, c.IdRDV);
                    appointements = c.patient.appointements;
                }
                else
                    appointements = AppointementsMgmt.getAppointmentsByDate(c.IdPatient, Dte, c.IdRDV);
                foreach (RHAppointment app in appointements)
                    if (app.Id == c.IdRDV)
                        c.Appointement = app;
                if (c.Appointement == null)
                    c.IdRDV = -1;
                else
                    if (c.Appointement.StartDate > DateTime.Now)
                    c.DatePrevisionnnelle = c.Appointement.StartDate;
                else
                    c.date = c.Appointement.StartDate;
            }
            if (c.DatePrevisionnnelle == null)
                if (c.date != null)
                    c.DatePrevisionnnelle = c.date;
                else
                    if (DteDebutTrtmnt != null)
                    c.DatePrevisionnnelle = DteDebutTrtmnt.Value.AddMonths(c.NbMois).AddDays(c.NbJours);
        }

        public static CommClinique GetCommClinique(int Id, DateTime? DteDebutTrtmnt)
        {


            JObject dr = DAC.getMethodeJsonObjet("/CommClinique/"+ Id);

            if (dr == null) return null;
            CommClinique c = Builders.BuildComClinique.BuildCommCliniqueJson(dr);
            JArray jarray = DAC.getMethodeJsonArray("/CommActeByIdComm/" + Id);
             List<CommActes> lstCommActes = new List<CommActes>();
            foreach (JObject obj in jarray)
            {
                CommActes commActe = Builders.BuildComClinique.BuildCommActesJson(obj);
                switch (obj["type_ACTE_SUPP"].ToString())
                {
                    case  "" : c.ActesSupp.Add(commActe);break;
                    case "P": c.photos.Add(commActe);break;
                    case  "R" :c.Radios.Add(commActe);break;
                }
            }
            ManageDates(DteDebutTrtmnt, c);
            return c;
        }
        public static CommClinique GetCommClinique(int Id, DateTime? DteDebutTrtmnt, DateTime Dte)
        {
            JObject dr = DAC.getMethodeJsonObjet("/CommClinique/"+ Id);
            if (dr == null) return null;
            CommClinique c = Builders.BuildComClinique.BuildCommCliniqueJson(dr);
            JArray jarray = DAC.getMethodeJsonArray("/CommActeByIdComm/" + Id);
             List<CommActes> lstCommActes = new List<CommActes>();
            foreach (JObject obj in jarray)
            {
                CommActes commActe = Builders.BuildComClinique.BuildCommActesJson(obj);
                switch (obj["type_ACTE_SUPP"].ToString())
                {
                    case  "" : c.ActesSupp.Add(commActe);break;
                    case "P": c.photos.Add(commActe);break;
                    case  "R" :c.Radios.Add(commActe);break;
                }
            }
            //ManageDates(DteDebutTrtmnt, c, Dte);
            //ManageDates(DteDebutTrtmnt, c, Dte, c.IdRDV);
            return c;
        }
        public static CommClinique GetCommCliniqueOLD(int Id, DateTime? DteDebutTrtmnt)
        {


            DataRow dr = DAC.GetCommClinique(Id);

            if (dr == null) return null;


            CommClinique c = Builders.BuildComClinique.BuildCommClinique(dr);

            ManageDates(DteDebutTrtmnt, c);

            return c;
        }

        public static List<CommClinique> GetCommCliniques(basePatient patient, DateTime? DteDebutTrtmnt)
        {
            DataTable dt = DAC.GetCommClinique(patient);

            List<CommClinique> lst = new List<CommClinique>();


            foreach (DataRow dr in dt.Rows)
            {

                CommClinique c = Builders.BuildComClinique.BuildCommClinique(dr);

                ManageDates(DteDebutTrtmnt, c);

                lst.Add(c);
            }

            return lst;
        }

     

      

        public static List<CommClinique> GetCommCliniques(basePatient patient)
        {
            DataTable dt = DAC.GetCommCliniquesByIdPatient(patient.Id);

            List<CommClinique> lst = new List<CommClinique>();


            foreach (DataRow dr in dt.Rows)
            {

                CommClinique c = Builders.BuildComClinique.BuildCommClinique(dr);
                c.patient = patient;
                ManageDates( c);

                lst.Add(c);
            }

            return lst;
        }

        public static List<CommClinique> GetCommCliniquesByIdPatient(int IdPatient)
        {
            
            DataTable dt = DAC.GetCommCliniquesByIdPatient(IdPatient);
            List<CommClinique> lst = new List<CommClinique>();

            foreach (DataRow r in dt.Rows)
            {
                CommClinique c = Builders.BuildComClinique.BuildCommClinique(r);

                lst.Add(c);

            }
            return lst;
        }

        
        
        public static List<CommentaireOrthalis> GetCommOrthalis(basePatient pat)
        {
            DataTable dt = DAC.getCommentairesOrthalis(pat);
            List<CommentaireOrthalis> lst = new List<CommentaireOrthalis>();

            foreach (DataRow r in dt.Rows)
            {
                CommentaireOrthalis c = Builders.BuildCommentaireOrthalis.Build(r);

                System.Windows.Forms.RichTextBox rtf = new System.Windows.Forms.RichTextBox();
                try
                {
                    rtf.Rtf = c.Fait;
                    c.Fait = rtf.Text;
                }
                catch (System.Exception)
                {
                }
                try
                {
                    rtf.Rtf = c.Afaire;
                    c.Afaire = rtf.Text;
                }
                catch (System.Exception)
                {
                }

                c.Praticien = c.Praticien.Trim();


                lst.Add(c);

            }
            return lst;
        }

        


        public static void GetCommDentAExtraire(CommClinique com)
        {
            DataTable dt = DAC.GetCommCliniqueDentAExtraire(com);
            List<CommDentAExtraire> lst = new List<CommDentAExtraire>();

            foreach (DataRow r in dt.Rows)
            {
                CommDentAExtraire c = Builders.BuildComClinique.BuildCommDentAExtraire(r);
                c.Parent = com;
                lst.Add(c);
            }
            com.DentsAExtraire = lst;
        }

        public static void GetCommCliniqueAutrePersonne(CommClinique com)
        {
            DataTable dt = DAC.GetCommCliniqueAutrePersonne(com);
            List<CommAutrePersonne> lst = new List<CommAutrePersonne>();

            foreach (DataRow r in dt.Rows)
            {
                CommAutrePersonne c = Builders.BuildComClinique.BuildCommAutrePersonne(r);
                c.Parent = com;
                lst.Add(c);
            }
            com.AutrePersonnes = lst;
        }
        public static void GetMateriels(CommClinique com)
        {
            JArray jarray = DAC.getMethodeJsonArray("/CommMats/GetCommMatsByIdComm?id=" +com.Id);
            List<CommMateriel> lst = new List<CommMateriel>();

            foreach (JObject r in jarray)
            {
                CommMateriel c = Builders.BuildComClinique.BuildCommMaterielJson(r);
                c.Parent = com;
                lst.Add(c);
            }
            com.Materiels = lst;
        }
        public static void GetMaterielsOLD(CommClinique com)
        {
            DataTable dt = DAC.GetCommCliniqueMateriels(com);
            List<CommMateriel> lst = new List<CommMateriel>();

            foreach (DataRow r in dt.Rows)
            {
                CommMateriel c = Builders.BuildComClinique.BuildCommMateriel(r);
                c.Parent = com;
                lst.Add(c);
            }
            com.Materiels = lst;
        }
        public static void GetActesSupp(CommClinique com, string TypeActeSupp  = "")
        {
            DataTable dt = DAC.GetCommCliniqueActes(com,TypeActeSupp);
            List<CommActes> lst = new List<CommActes>();


            foreach (DataRow r in dt.Rows)
            {
                CommActes c = Builders.BuildComClinique.BuildCommActes(r);
                c.Parent = com;

                lst.Add(c);
            }
            if (TypeActeSupp == "R")
            {
                com.Radios = lst;
            }
            else if (TypeActeSupp == "P" )
            {
                com.photos = lst;
            }
            else
            {
            com.ActesSupp = lst;
            }
        }
        // wael 
        public static List<CommActes> GetActesSupp_tk(int Idcc, int IdPatient, string TypeActeSupp = "") 
        {
            
            List<CommActes> liste = new List<CommActes>();
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray("/GetActesSuppTK/"+Idcc+"&"+IdPatient+"&"+ TypeActeSupp);

            foreach (JObject obj in jArray)             
            {
                CommActes ca = Builders.BuildComClinique.BuildCommActes(obj);
                liste.Add(ca);
            }

            return liste;
        }
        //seif
        public static List<CommActes> GetActesSupp_tkOld(int Idcc, int IdPatient, string TypeActeSupp = "")
        {
            DataTable dt = DAC.GetCommCliniqueActesSupp( Idcc, IdPatient, TypeActeSupp);
            List<CommActes> lst = new List<CommActes>();

            foreach (DataRow r in dt.Rows)
            {
                CommActes c = Builders.BuildComClinique.BuildCommActes(r);
                lst.Add(c);
            }
           
            return lst;
        }

        //public static void GetPhotos(CommClinique com)
        //{
        //    DataTable dt = DAC.GetCommCliniquePhotos(com);
        //    List<CommPhoto> lst = new List<CommPhoto>();

        //    foreach (DataRow r in dt.Rows)
        //    {
        //        CommPhoto c = Builders.BuildComClinique.BuildCommPhotos(r);
        //        c.Parent = com;
        //        lst.Add(c);
        //    }
        //    com.photos = lst;
        //}

        //public static void GetRadios(CommClinique com)
        //{
        //    DataTable dt = DAC.GetCommCliniqueRadios(com);
        //    List<CommRadio> lst = new List<CommRadio>();

        //    foreach (DataRow r in dt.Rows)
        //    {
        //        CommRadio c = Builders.BuildComClinique.BuildCommRadio(r);
        //        c.Parent = com;
        //        lst.Add(c);
        //    }
        //    com.Radios = lst;
        //}





        
           
                          
    }
}
