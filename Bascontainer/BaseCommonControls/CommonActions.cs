using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using BasCommon_BL;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
namespace BaseCommonControls
{
    
        public static class CommonActions
    {


            public static void AddCourrierAttributs(Correspondant Praticien, basePatient _CurrentPatient)
            {

                if (_CurrentPatient.Correspondants == null)
                    _CurrentPatient.Correspondants = MgmtCorrespondants.getCorrespondantsOf(_CurrentPatient);


                if ((_CurrentPatient.ResponsableFinancier != null) && (_CurrentPatient.ResponsableFinancier.correspondant == null))
                    _CurrentPatient.ResponsableFinancier.correspondant = MgmtCorrespondants.getCorrespondant(_CurrentPatient.ResponsableFinancier.IdCorrespondance);

                if (_CurrentPatient.ResponsableFinancier != null)
                {
                    OLEAccess.BASLetter.AddAttribut("NomRespFi", _CurrentPatient.ResponsableFinancier.correspondant.Nom);
                    OLEAccess.BASLetter.AddAttribut("PrenomRespFi", _CurrentPatient.ResponsableFinancier.correspondant.Prenom);
                }
                else
                {
                    OLEAccess.BASLetter.AddAttribut("NomRespFi", _CurrentPatient.Nom);
                    OLEAccess.BASLetter.AddAttribut("PrenomRespFi", _CurrentPatient.Prenom);
                }

                OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
                OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
                OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
                OLEAccess.BASLetter.AddAttribut("AgePatient", _CurrentPatient.AgeNbYears.ToString() + " ans");
                OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Genre == basePatient.Sexe.Feminin ? "F" : "M");
                OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);

                if (_CurrentPatient.MainAdresse != null)
                {
                    OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _CurrentPatient.MainAdresse.adresse.Adr1);
                    OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _CurrentPatient.MainAdresse.adresse.Adr2);
                    OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _CurrentPatient.MainAdresse.adresse.CodePostal);
                    OLEAccess.BASLetter.AddAttribut("VillePatient", _CurrentPatient.MainAdresse.adresse.Ville);
                    OLEAccess.BASLetter.AddAttribut("PaysPatient", _CurrentPatient.MainAdresse.adresse.Pays);
                }

                if (_CurrentPatient.Tutoiement)
                    OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
                else
                    OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

                OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _CurrentPatient.DateNaissance.Date.ToString());
                OLEAccess.BASLetter.AddAttribut("NumSecu", _CurrentPatient.NumSecu.ToString());

                OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());



                OLEAccess.BASLetter.AddAttribut("ID_PRATICIEN", Praticien.Id.ToString());

                OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
                OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
                OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
                OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.Mail);
                OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
                OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.Tel);
                OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.Tel);
                OLEAccess.BASLetter.AddAttribut("TelPortablePraticien", Praticien.Tel);
                OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.Fax);

                OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.Adresse1);
                OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.Adresse2);
                OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.CodePostal);
                OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.Ville);
                if (Praticien.GenreFeminin)
                    OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
                else
                    OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");


            }


            private static void AddCourrierAttributs(Echeance ec)
            {


                if (ec == null)
                {
                    OLEAccess.BASLetter.AddAttribut("DateEcheance", "");
                    OLEAccess.BASLetter.AddAttribut("LibelleEcheance", "");
                    OLEAccess.BASLetter.AddAttribut("MontantEcheance", "");

                }
                else
                {
                    OLEAccess.BASLetter.AddAttribut("DateEcheance", ec.DateEcheance == null ? "" : ec.DateEcheance.ToShortDateString());
                    OLEAccess.BASLetter.AddAttribut("LibelleEcheance", ec.Libelle);
                    OLEAccess.BASLetter.AddAttribut("MontantEcheance", ec.Montant.ToString("C2"));

                }


            }


            public static void AddCourrierAttributsNEwEch(List<BaseTempEcheanceDefinition> Montants, Correspondant praticien, basePatient patient)
            {


                if (patient.contacts == null)
                    baseMgmtPatient.FillContacts(patient);

                 OLEAccess.BASLetter.AddAttribut("MailSubject", InfoCabinetMgmt.informationsCabinet.NomCabinet + " : Devis pour traitement d'orthodontie");


                string body = "";

                switch (patient.Civilite)
                {
                    case "Mr":body="Monsieur,";break;
                    case "Mme": body = "Madame,"; break;
                    case "Mlle":body="Mademoiselle,";break;
                }

                body += "\n\n" + "Veuillez trouver ci joint votre devis en date du " + DateTime.Now.ToString();

                OLEAccess.BASLetter.AddAttribut("MailBody", body);
                OLEAccess.BASLetter.AddAttribut("MAILCORRESPONDANT", patient.MainMail!=null?patient.MainMail.Value:"");

                AddCourrierAttributs(praticien, patient);

                string SummaryEcheances = EcheancesMgmt.GetSummary(Montants);
                string DetailEcheances = "";


                Montants.Sort();
                Object[] dataArray = new object[Montants.Count];


                for (int fi = 0; fi < Montants.Count; fi++)
                {
                    object[] o = new object[4];
                    o[0] = Montants[fi].Libelle;
                    o[1] = Montants[fi].acte == null ? 0 : Montants[fi].acte.NumSemestre;
                    o[2] = Montants[fi].DAteEcheance;
                    o[3] = Montants[fi].Montant;
                    dataArray[fi] = o;
                }


                OLEAccess.BASLetter.AddAttribut("financement", dataArray);

                int i = 1;
                foreach (BaseTempEcheanceDefinition ted in Montants)
                {
                    if (ted.AlreadyPayed) continue;
                    if (DetailEcheances != "") DetailEcheances += "\n";
                    if (ted.DAteEcheance != null)
                        DetailEcheances += ted.Libelle + "\tle " + ted.DAteEcheance.ToShortDateString() + "\t" + ted.Montant.ToString("C2");
                    else
                        DetailEcheances += ted.Libelle + "\t avant la fin de traitement \t" + ted.Montant.ToString("C2");

                    i++;
                }


                OLEAccess.BASLetter.AddAttribut("ResumeEcheances", SummaryEcheances);
                OLEAccess.BASLetter.AddAttribut("DetailsEcheances", DetailEcheances);

            }


            public static void AddCourrierAttributsDevis(Correspondant Praticien,
                                                       Devis dev,
                                                       basePatient _CurrentPatient)
            {

                

                if (dev.actesHorstraitement == null)
                    dev.actesHorstraitement = MgmtDevis.getactesHorstraitement(dev);

                TemplateActePG surv = TemplateApctePGMgmt.getCodeSecu("SURV");
                TemplateActePG survhn = TemplateApctePGMgmt.getCodeSecu("SURV_HN");


                OLEAccess.BASLetter.AddAttribut("ID_DEVIS", dev.Id);


                OLEAccess.BASLetter.AddAttribut("NbProps", dev.propositions==null?0:dev.propositions.Count);
                OLEAccess.BASLetter.AddAttribut("NbOptions", dev.actesHorstraitement.Count);

                double montantavantremise = 0;
                double montant = 0;

                foreach (ActePGPropose app in dev.actesHorstraitement)
                    montantavantremise += app.MontantAvantRemise;
                foreach (ActePGPropose app in dev.actesHorstraitement)
                    montant += app.Montant;



                if (dev.MontantAvantRemise != null)
                    montantavantremise = dev.MontantAvantRemise.Value;
                if (dev.Montant != null)
                    montant = dev.Montant.Value;


                OLEAccess.BASLetter.AddAttribut("MontantAvantRemise", montantavantremise);
                OLEAccess.BASLetter.AddAttribut("Montant", montant);

                int Optionnum = 1;
                
                foreach (ActePGPropose acte in dev.actesHorstraitement)
                {

                    if ((acte.template == null) && (acte.IdTemplateActePG >= 0))
                        acte.template = TemplateApctePGMgmt.getCodeSecu(acte.IdTemplateActePG);

                    OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_Montant", acte.Qte * acte.Montant);
                    OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_MontantAvantRemise", acte.Qte * acte.MontantAvantRemise);
                    OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_Qte", acte.Qte);
                    OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_Code", acte.template.Nom);
                    OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_Libelle", acte.Libelle);
                    OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_PartSecu", acte.template.Coeff * acte.template.Code.Valeur);
                    OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_PartMutuelle", (acte.Qte * acte.Montant) - (acte.template.Coeff * acte.template.Code.Valeur));
                    OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_LibSecu", acte.template.DisplayCodeNVal);
                    OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_Organisation", acte.template.Organisation);
                    Optionnum++;
                }


                int propnum = 1;
                if (dev.propositions!=null)
                    foreach (Proposition prop in dev.propositions)
                    {

                        if ((prop.Etat != Proposition.EtatProposition.Soumis)&&(prop.Etat != Proposition.EtatProposition.Accepté)) continue;


                        int i = 0;

                        if (prop.echeancestemp == null)
                            prop.echeancestemp = PropositionMgmt.LoadDefaultTempecheances(prop);


                        Object[] dataArray = new object[prop.echeancestemp.Count];

                        for (int fi = 0; fi < prop.echeancestemp.Count; fi++)
                        {
                            object[] o = new object[4];
                            o[0] = prop.echeancestemp[fi].Libelle;
                            o[1] = prop.echeancestemp[fi].acte == null ? "" : prop.echeancestemp[fi].acte.NumSemestre.ToString();
                            o[2] = prop.echeancestemp[fi].DAteEcheance.ToShortDateString();
                            o[3] = prop.echeancestemp[fi].Montant.ToString("0.00");
                            dataArray[fi] = o;
                        }


                        OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_financement", dataArray);

                        string SummaryEcheances = EcheancesMgmt.GetSummary(prop.echeancestemp.Cast<BaseTempEcheanceDefinition>().ToList()).Replace("\n", " ");
                        OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_ResumeEcheancier", SummaryEcheances);

                        foreach (Traitement t in prop.traitements)
                        {
                            double tariftotal = TraitementMgmt.getTotal(t);
                            int nbMois = 0;
                            double RmbMutuelleParSemestre = 0;
                            double RmbSecuParSemestre = 0;
                            string LibSecuParSemestre = "";

                            foreach (Semestre s in t.semestres)
                            {

                                int NbAnneesMois = (s.DateFin.Year - s.DateDebut.Year) * 12;
                                int NbMois = s.DateFin.Month - s.DateDebut.Month;

                                if (s.DateFin.Day < s.DateDebut.Day)
                                    nbMois += NbAnneesMois + NbMois - 1;
                                else
                                    nbMois += NbAnneesMois + NbMois;

                            }

                            double secu = t.semestres.Count == 0 ? 0 : t.semestres[0].traitementSecu.Code.Valeur * t.semestres[0].traitementSecu.Coeff;

                            RmbSecuParSemestre += secu;
                            RmbMutuelleParSemestre += t.semestres.Count == 0 ? 0 : t.semestres[0].Montant_Honoraire - secu;
                            if (LibSecuParSemestre != "") LibSecuParSemestre += "+";
                            LibSecuParSemestre += "(" + (t.semestres.Count == 0 ? "" : t.semestres[0].traitementSecu.DisplayCodeNVal) + ")";


                            double totalParMois = tariftotal / nbMois;



                            PropositionObjectForLetters ob = new PropositionObjectForLetters();
                            ob.TarifParMois = totalParMois;
                            ob.Honoraires = t.semestres[0].Montant_Honoraire;
                            ob.NbMois = nbMois;
                            ob.PartSecu = RmbSecuParSemestre;
                            ob.PartMutuelle = RmbMutuelleParSemestre;
                            ob.LibSecu = LibSecuParSemestre;
                            ob.CodeTraitement = t.CodeTraitement;

                            //OLEAccess.BASLetter.AddAttribut("Propositions", ob);


                            if (!double.IsInfinity(totalParMois)) OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_TarifParMois", totalParMois.ToString("0.00"));
                            OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Honoraires", t.semestres[0].Montant_Honoraire.ToString("0.00"));
                            OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_TarifPropose", t.semestres[0].Montant_Honoraire.ToString("0.00"));
                            OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_TarifNormal", t.semestres[0].Montant_AvantRemise.ToString("0.00"));
                            OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_NbMois", nbMois.ToString());
                            OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_PartSecu", RmbSecuParSemestre.ToString("0.00"));
                            OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_PartMutuelle", RmbMutuelleParSemestre.ToString("0.00"));
                            OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_LibSecu", LibSecuParSemestre);
                            OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_CodeTraitement", t.CodeTraitement.Trim());

                            if (prop.matosassociate == null)
                                prop.matosassociate = MgmtDevis.getactesHorstraitement(prop);

                            Optionnum = 1;
                            foreach (ActePGPropose acte in prop.matosassociate)
                            {

                                if ((acte.template == null) && (acte.IdTemplateActePG >= 0))
                                    acte.template = TemplateApctePGMgmt.getCodeSecu(acte.IdTemplateActePG);

                                OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "Option" + Optionnum.ToString() + "_Montant", acte.Qte * acte.Montant);
                                OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "Option" + Optionnum.ToString() + "_MontantAvantRemise", acte.Qte * acte.MontantAvantRemise);
                                OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "Option" + Optionnum.ToString() + "_Qte", acte.Qte);
                                OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "Option" + Optionnum.ToString() + "_Code", acte.template.Nom);
                                OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "Option" + Optionnum.ToString() + "_Libelle", acte.Libelle);
                                OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "Option" + Optionnum.ToString() + "_PartSecu", acte.template.Coeff * acte.template.Code.Valeur);
                                OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "Option" + Optionnum.ToString() + "_PartMutuelle", (acte.Qte * acte.Montant) - (acte.template.Coeff * acte.template.Code.Valeur));
                                OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "Option" + Optionnum.ToString() + "_LibSecu", acte.template.DisplayCodeNVal);
                                OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "Option" + Optionnum.ToString() + "_Organisation", acte.template.Organisation);
                                Optionnum++;
                            }


                            i++;
                        }



                        propnum++;
                    }


                // OLEAccess.BASLetter.AddAttribut("Devis", devis);

                int y;
                int m;
                int d;
                _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

                OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id);
                OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
                OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
                OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et " + m.ToString() + " mois");
                OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Genre == basePatient.Sexe.Feminin ? "F" : "M");
                OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);


                OLEAccess.BASLetter.AddAttribut("MailSubject", InfoCabinetMgmt.informationsCabinet.NomCabinet + " : Devis pour traitement d'orthodontie");


                string body = "";

                switch (_CurrentPatient.Civilite)
                {
                    case "Mr": body = "Monsieur,"; break;
                    case "Mme": body = "Madame,"; break;
                    case "Mlle": body = "Mademoiselle,"; break;
                }

                body += "\n\n" + "Veuillez trouver ci joint votre devis en date du " + DateTime.Now.ToString();

                OLEAccess.BASLetter.AddAttribut("MailBody", body);
                OLEAccess.BASLetter.AddAttribut("MAILCORRESPONDANT", _CurrentPatient.MainMail != null ? _CurrentPatient.MainMail.Value : "");



                if (_CurrentPatient.MainAdresse != null)
                {
                    OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _CurrentPatient.MainAdresse.adresse.Adr1);
                    OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _CurrentPatient.MainAdresse.adresse.Adr2);
                    OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _CurrentPatient.MainAdresse.adresse.CodePostal);
                    OLEAccess.BASLetter.AddAttribut("VillePatient", _CurrentPatient.MainAdresse.adresse.Ville);
                    OLEAccess.BASLetter.AddAttribut("PaysPatient", _CurrentPatient.MainAdresse.adresse.Pays);
                }

                if (_CurrentPatient.Tutoiement)
                    OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
                else
                    OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

                OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _CurrentPatient.DateNaissance.Date.ToString());
                OLEAccess.BASLetter.AddAttribut("NumSecu", _CurrentPatient.NumSecu.ToString());
                OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());

                OLEAccess.BASLetter.AddAttribut("ID_PRATICIEN", Praticien.Id);

                OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
                OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
                OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
                OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.Mail);
                OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
                OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.Tel);
                OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.Tel);
                OLEAccess.BASLetter.AddAttribut("TelPortablePraticien", Praticien.Tel);
                OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.Fax);

                OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.Adresse1);
                OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.Adresse2);
                OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.CodePostal);
                OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.Ville);
                if (Praticien.GenreFeminin)
                    OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
                else
                    OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");




            }

        


       

            public static void GenerateAndPrintDevis(string file, Correspondant Praticien, Correspondant c, Devis d, basePatient patient)
            {


                AddCourrierAttributsDevis(Praticien, d, patient);
                OLEAccess.BASLetter.GenerateFrom(file);


            }
            private static float EspaceDroite(string TotalString, string Libelle)
            {
                float ll = 0;
                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new System.Drawing.Bitmap(1, 1)))
                {
                    System.Drawing.SizeF sizeChar = graphics.MeasureString("A", new System.Drawing.Font("Garamond", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point));
                    System.Drawing.SizeF size0 = graphics.MeasureString(TotalString , new System.Drawing.Font("Garamond", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point));
                    System.Drawing.SizeF size1 = graphics.MeasureString(Libelle, new System.Drawing.Font("Garamond", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point));
                    System.Drawing.SizeF sizeFDiff = size0 - size1;
                    ll = sizeFDiff.Width / 10;
                }
                return ll;
            }
            public static void AddCourrierAttributsDevis_TK(Correspondant Praticien,
                                                    Devis_TK dev,
                                                    basePatient _CurrentPatient)
            {
                
                List<object[]> arrList = new List<object[]>();
                OLEAccess.BASLetter.AddAttribut("Num_Devis", dev.Id);
                double? montantavantremise = 0;
                montantavantremise = dev.MontantAvantRemise;
                double? montant = 0;
                montant = dev.Montant ;
                double MontantHonoraires = 0;
                double MontantHonorairesAvantRemise = 0;
                OLEAccess.BASLetter.AddAttribut("MontantAvantRemise",montantavantremise    );
                OLEAccess.BASLetter.AddAttribut("Montant", montant );

           

                OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id);
                OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
                OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
                
                OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Genre == basePatient.Sexe.Feminin ? "F" : "M");
                OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);
                OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
                OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
                OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
               // string SummaryEcheances = EcheancesMgmt.GetSummary(Montants);
                OLEAccess.BASLetter.AddAttribut("ResumeEcheances",150);
                DateTime TmpDateEcheance  = Convert.ToDateTime ( dev.DateEcheance);


                OLEAccess.BASLetter.AddAttribut("Echeance_Devis", TmpDateEcheance.ToShortDateString ());
                
                List<BaseTempEcheanceDefinition> Montants  = new List<BaseTempEcheanceDefinition>();
                foreach (CommTraitement comm in dev.actesTraitement)
                {
                    foreach (BaseTempEcheanceDefinition ech in comm .echeancestemp )
                    {
                        Montants.Add(ech);
                    }
                    

            }
                //Commtraitements
                Object[] dataArrayCommtraitements = new object[2];

                double NombrePoints = 0;
                Double? MontantFraisLaboratoire = 0;
                Double? MontantAchats = 0;
                Double? MontantSterilisation = 0;
                string  ValeurInitiale = "";
                int CtrActes = 0;
                int CtrEcheances = 0;
                Object[] dataArrayEcheances = new object[50];
                Object[] dataArrayActes = new object[50];
                  
                for (int fi = 0; fi < dev.actesTraitement.Count; fi++)
                {
                    object[] o = new object[5];
                   
                   o[0] = "Traitement " + dev.actesTraitement[fi].Acte.acte_libelle;
                   
                    o[1] = dev.actesTraitement[fi].MontantLigneAvantRemise ;
                    o[2] = dev.actesTraitement[fi].MontantLigne ;
                   
                       for (int fj = 0; fj < dev.actesTraitement[fi].Materiels.Count; fj++)
                   {
                       if (dev.actesTraitement[fi].Materiels[fj].Famille != null)
                       {
                           if (dev.actesTraitement[fi].Materiels[fj].Famille.libelle.ToLower().IndexOf("laboratoire") >= 0)
                               MontantFraisLaboratoire = MontantFraisLaboratoire + dev.actesTraitement[fi].Materiels[fj].prix_traitement;

                           if (dev.actesTraitement[fi].Materiels[fj].Famille.libelle.ToLower().IndexOf("stérilisation") >= 0)
                               MontantSterilisation = MontantSterilisation + dev.actesTraitement[fi].Materiels[fj].prix_traitement;

                           if (dev.actesTraitement[fi].Materiels[fj].Famille.libelle.ToLower().IndexOf("achats") >= 0)
                               MontantAchats = MontantSterilisation + dev.actesTraitement[fi].Materiels[fj].prix_traitement;
                       }

                   }
                      

                       //o[3] = MontantFraisLaboratoire;
                       //o[4] = MontantSterilisation;

                     object[] oActei = new object[8];
                    //oActei[0] = "Acte: " + dev.actesTraitement[fi].Acte.acte_libelle;
                    float lln = EspaceDroite("AAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].Acte.nomenclature);
                    oActei[0] = dev.actesTraitement[fi].Acte.nomenclature.PadRight((int)lln, ' ');
                    oActei[0] = dev.actesTraitement[fi].Acte.nomenclature;

                    lln = EspaceDroite("AAAAAAAAAA", dev.actesTraitement[fi].Acte.quantite);
                     oActei[1] = dev.actesTraitement[fi].Acte.quantite.PadRight((int)lln, ' ');
                     oActei[1] = dev.actesTraitement[fi].Acte.quantite;
                    ////

                    // Set up string.

                     float ll = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].Acte.acte_libelle_estimation);
                  
                  

                        oActei[2] = dev.actesTraitement[fi].Acte.acte_libelle_estimation.PadRight((int)ll,' ');
                        oActei[2] = dev.actesTraitement[fi].Acte.acte_libelle_estimation;
                        float longuerlibelle = 0;
                        using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new System.Drawing.Bitmap(1, 1)))
                        {
                            System.Drawing.SizeF sizeChar = graphics.MeasureString(dev.actesTraitement[fi].Acte.acte_libelle_estimation.PadRight((int)ll, '.'), new System.Drawing.Font("Garamond", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point));
                            longuerlibelle = sizeChar.Width;
                        }


                    //oActei[2] = "tujdfjdkjfkdjfkdjfkjdfjdjfdkjfkdjfkdjfkdjfkjdfkjdkfjdkjfkdjfkdj";

                        lln = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].Acte.cotation);
                    
                        oActei[3] = dev.actesTraitement[fi].Acte.cotation.PadRight((int)lln, ' ');
                        oActei[3] = dev.actesTraitement[fi].Acte.cotation;
                    ValeurInitiale = dev.actesTraitement[fi].Acte.cotation;

                    lln = EspaceDroite("AAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].Acte.nombre_points);
                    oActei[4] = (double.Parse(dev.actesTraitement[fi].Acte.nombre_points) * double.Parse(dev.actesTraitement[fi].Acte.quantite)).ToString().PadRight((int)lln, ' ');
                    oActei[4] = (double.Parse(dev.actesTraitement[fi].Acte.nombre_points) * double.Parse(dev.actesTraitement[fi].Acte.quantite));
                    NombrePoints = NombrePoints + (double.Parse(dev.actesTraitement[fi].Acte.nombre_points) * double.Parse(dev.actesTraitement[fi].Acte.quantite));




                   // oActei[5] = dev.actesTraitement[fi].Acte.prix_acte * int.Parse(dev.actesTraitement[fi].Acte.quantite);

                    lln = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].Acte.prix_acte.ToString());
                    oActei[5] = (dev.actesTraitement[fi].Acte.prix_acte * int.Parse(dev.actesTraitement[fi].Acte.quantite)).ToString("C2").PadRight((int)lln, ' '); ;
                    oActei[5] = (dev.actesTraitement[fi].Acte.prix_acte * int.Parse(dev.actesTraitement[fi].Acte.quantite));

                    oActei[6] = (dev.actesTraitement[fi].Acte.prix_traitement * int.Parse(dev.actesTraitement[fi].Acte.quantite)).ToString ("C2");
                    oActei[6] = (dev.actesTraitement[fi].Acte.prix_traitement * int.Parse(dev.actesTraitement[fi].Acte.quantite));

                    oActei[7] = dev.actesTraitement[fi].Acte.id_acte;

                    MontantHonoraires = MontantHonoraires + (dev.actesTraitement[fi].Acte.prix_traitement * int.Parse ( dev.actesTraitement[fi].Acte.quantite));
                    MontantHonorairesAvantRemise = MontantHonorairesAvantRemise  + (dev.actesTraitement[fi].Acte.prix_acte * int.Parse(dev.actesTraitement[fi].Acte.quantite));

                   

                     bool TrouveActe  = false ;
                    int TmpCtrActes = CtrActes;
                    if (CtrActes == 0)
                    {
                        dataArrayActes[CtrActes] = oActei;
                        CtrActes = CtrActes + 1;
                    }
                    else
                    {
                        for (int ff = 0; ff <= TmpCtrActes; ff++)
                        {
                            if (dataArrayActes[ff] != null)
                            {
                                object[] TmpoActe = new object[8];
                                TmpoActe = (object[])dataArrayActes[ff];
                                if (Convert.ToInt32 ( TmpoActe[7]) ==Convert.ToInt32 ( oActei[7]))
                                {
                                    TmpoActe[1] = Convert.ToInt32(TmpoActe[1]) + Convert.ToInt32(oActei[1]);
                                    TmpoActe[4] = Convert.ToInt32(TmpoActe[4]) + Convert.ToInt32(oActei[4]);
                                    TmpoActe[5] = Convert.ToDouble(TmpoActe[5]) + Convert.ToDouble(oActei[5]);
                                    TmpoActe[6] = Convert.ToDouble(TmpoActe[6]) + Convert.ToDouble(oActei[6]);
                                    dataArrayActes[ff] = TmpoActe;
                                    TrouveActe = true;
                                    break;
                                }

                            }

                        }
                        if (!(TrouveActe))
                        {
                            dataArrayActes[CtrActes] = oActei;
                            CtrActes = CtrActes + 1;
                        }
                    }


                   
                



                   
                    for (int fj = 0; fj < dev.actesTraitement[fi].ActesSupp.Count; fj++)
                    {
                        object[] oActe = new object[8];


                        lln = EspaceDroite("AAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].ActesSupp[fj].nomenclature);
                        oActe[0] = dev.actesTraitement[fi].ActesSupp[fj].nomenclature.PadRight((int)lln, ' ');
                        oActe[0] = dev.actesTraitement[fi].ActesSupp[fj].nomenclature;
                        lln = EspaceDroite("AAAAAAAAAA", dev.actesTraitement[fi].ActesSupp[fj].Qte.ToString());
                        oActe[1] = dev.actesTraitement[fi].ActesSupp[fj].Qte.ToString().PadRight((int)lln, ' ');
                        oActe[1] = dev.actesTraitement[fi].ActesSupp[fj].Qte;
                        float ll2 = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].ActesSupp[fj].LibActe_estimation);
                        oActe[2] = dev.actesTraitement[fi].ActesSupp[fj].LibActe_estimation.PadRight((int)ll2, ' ');
                        oActe[2] = dev.actesTraitement[fi].ActesSupp[fj].LibActe_estimation;
                        using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new System.Drawing.Bitmap(1, 1)))
                        {
                            System.Drawing.SizeF sizeChar = graphics.MeasureString(dev.actesTraitement[fi].ActesSupp[fj].LibActe_estimation.PadRight((int)ll2, '.'), new System.Drawing.Font("Garamond", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point));
                            while (sizeChar.Width > longuerlibelle)
                            {
                                ll2 = ll2 - 1;
                                sizeChar = graphics.MeasureString(dev.actesTraitement[fi].ActesSupp[fj].LibActe_estimation.PadRight((int)ll2, '.'), new System.Drawing.Font("Garamond", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point));

                            }
                        }
                        oActe[2] = dev.actesTraitement[fi].ActesSupp[fj].LibActe_estimation.PadRight((int)ll2+1, ' ');
                        oActe[2] = dev.actesTraitement[fi].ActesSupp[fj].LibActe_estimation;

                        lln = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].ActesSupp[fj].cotation);
                        

                        oActe[3] = dev.actesTraitement[fi].ActesSupp[fj].cotation.PadRight((int)lln, ' ');
                        oActe[3] = dev.actesTraitement[fi].ActesSupp[fj].cotation;
                        lln = EspaceDroite("AAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].ActesSupp[fj].nombre_points);

                        oActe[4] = (double.Parse(dev.actesTraitement[fi].ActesSupp[fj].nombre_points) * dev.actesTraitement[fi].ActesSupp[fj].Qte).ToString().PadRight((int)lln, ' ');
                        oActe[4] = (double.Parse(dev.actesTraitement[fi].ActesSupp[fj].nombre_points) * dev.actesTraitement[fi].ActesSupp[fj].Qte);
                        NombrePoints = NombrePoints + (double.Parse(dev.actesTraitement[fi].ActesSupp[fj].nombre_points) * dev.actesTraitement[fi].ActesSupp[fj].Qte);

                        lln = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].ActesSupp[fj].prix_acte.ToString());
                        oActe[5] = (dev.actesTraitement[fi].ActesSupp[fj].prix_acte * (dev.actesTraitement[fi].ActesSupp[fj].Qte)).ToString("C2").PadRight((int)lln, ' '); ;
                        oActe[5] = (dev.actesTraitement[fi].ActesSupp[fj].prix_acte * (dev.actesTraitement[fi].ActesSupp[fj].Qte));

                        oActe[6] = (dev.actesTraitement[fi].ActesSupp[fj].prix_traitement * (dev.actesTraitement[fi].ActesSupp[fj].Qte)).ToString("C2");
                        oActe[6] = (dev.actesTraitement[fi].ActesSupp[fj].prix_traitement * (dev.actesTraitement[fi].ActesSupp[fj].Qte));
                        oActe[7] = dev.actesTraitement[fi].ActesSupp[fj].IdActe;

                       // oActe[5] = dev.actesTraitement[fi].ActesSupp[fj].prix_acte ;
                       // oActe[6] = dev.actesTraitement[fi].ActesSupp[fj].prix_traitement * dev.actesTraitement[fi].ActesSupp[fj].Qte;
                        MontantHonoraires = MontantHonoraires + (dev.actesTraitement[fi].ActesSupp[fj].prix_traitement  *  dev.actesTraitement[fi].ActesSupp[fj].Qte);

                        MontantHonorairesAvantRemise = MontantHonorairesAvantRemise + (dev.actesTraitement[fi].ActesSupp[fj].prix_acte  * dev.actesTraitement[fi].ActesSupp[fj].Qte);



                         TmpCtrActes = CtrActes;
                         TrouveActe = false;
                         for (int ff = 0; ff <= TmpCtrActes; ff++)
                         {
                             if (dataArrayActes[ff] != null)
                             {
                                 object[] TmpoActe = new object[8];
                                 TmpoActe = (object[])dataArrayActes[ff];
                                 if (Convert.ToInt32(TmpoActe[7]) == Convert.ToInt32(oActe[7]))
                                 {
                                     TmpoActe[1] = Convert.ToInt32(TmpoActe[1]) + Convert.ToInt32(oActe[1]);
                                     TmpoActe[4] = Convert.ToInt32(TmpoActe[4]) + Convert.ToInt32(oActe[4]);
                                     TmpoActe[5] = Convert.ToDouble(TmpoActe[5]) + Convert.ToDouble(oActe[5]);
                                     TmpoActe[6] = Convert.ToDouble(TmpoActe[6]) + Convert.ToDouble(oActe[6]);
                                     dataArrayActes[ff] = TmpoActe;
                                     TrouveActe = true;
                                     break;
                                 }

                             }

                         }
                         if (!(TrouveActe))
                         {
                             dataArrayActes[CtrActes] = oActe;
                             CtrActes = CtrActes + 1;
                         }
                     

                    }
                    for (int fj = 0; fj < dev.actesTraitement[fi].Radios.Count; fj++)
                    {
                        object[] oActe = new object[8];

                        lln = EspaceDroite("AAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].Radios[fj].nomenclature);
                        oActe[0] = dev.actesTraitement[fi].Radios[fj].nomenclature.PadRight((int)lln, ' ');
                        oActe[0] = dev.actesTraitement[fi].Radios[fj].nomenclature;

                        lln = EspaceDroite("AAAAAAAAAA", dev.actesTraitement[fi].Radios[fj].Qte.ToString());
                        oActe[1] = dev.actesTraitement[fi].Radios[fj].Qte.ToString().PadRight((int)lln, ' ');
                        oActe[1] = dev.actesTraitement[fi].Radios[fj].Qte;
                        float ll3 = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].Radios[fj].LibActe_estimation);

                        oActe[2] = dev.actesTraitement[fi].Radios[fj].LibActe_estimation.PadRight((int)ll3, ' ');


                        using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new System.Drawing.Bitmap(1, 1)))
                        {
                            System.Drawing.SizeF sizeChar = graphics.MeasureString(dev.actesTraitement[fi].Radios[fj].LibActe_estimation.PadRight((int)ll3, '.'), new System.Drawing.Font("Garamond", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point));
                            while (sizeChar.Width > longuerlibelle)
                            {
                                ll3 = ll3 - 1;
                                sizeChar = graphics.MeasureString(dev.actesTraitement[fi].Radios[fj].LibActe_estimation.PadRight((int)ll3, '.'), new System.Drawing.Font("Garamond", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point));
                          
                            }
                        }
                        oActe[2] = dev.actesTraitement[fi].Radios[fj].LibActe_estimation.PadRight((int)ll3+1, ' ');
                        oActe[2] = dev.actesTraitement[fi].Radios[fj].LibActe_estimation;

                        //oActe[2] = dev.actesTraitement[fi].Radios[fj].LibActe_estimation.PadRight(120 - dev.actesTraitement[fi].Radios[fj].LibActe_estimation.Length, ' ');

                        lln = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].Radios[fj].cotation);
                        oActe[3] = dev.actesTraitement[fi].Radios[fj].cotation.PadRight((int)lln, ' ');
                        oActe[3] = dev.actesTraitement[fi].Radios[fj].cotation;
                        lln = EspaceDroite("AAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].Radios[fj].nombre_points);

                        oActe[4] = (double.Parse(dev.actesTraitement[fi].Radios[fj].nombre_points) * dev.actesTraitement[fi].Radios[fj].Qte).ToString().PadRight((int)lln, ' ');
                        oActe[4] = (double.Parse(dev.actesTraitement[fi].Radios[fj].nombre_points) * dev.actesTraitement[fi].Radios[fj].Qte);
                        NombrePoints = NombrePoints + (double.Parse(dev.actesTraitement[fi].Radios[fj].nombre_points) * dev.actesTraitement[fi].Radios[fj].Qte);

                        lln = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].Radios[fj].prix_acte.ToString());
                        oActe[5] = (dev.actesTraitement[fi].Radios[fj].prix_acte * (dev.actesTraitement[fi].Radios[fj].Qte)).ToString("C2").PadRight((int)lln, ' '); ;
                        oActe[5] = (dev.actesTraitement[fi].Radios[fj].prix_acte * (dev.actesTraitement[fi].Radios[fj].Qte));
                        oActe[6] = (dev.actesTraitement[fi].Radios[fj].prix_traitement * (dev.actesTraitement[fi].Radios[fj].Qte)).ToString("C2");
                        oActe[6] = (dev.actesTraitement[fi].Radios[fj].prix_traitement * (dev.actesTraitement[fi].Radios[fj].Qte));
                        oActe[7] = dev.actesTraitement[fi].Radios[fj].IdActe;

                        //oActe[5] = dev.actesTraitement[fi].Radios[fj].prix_acte;
                        //oActe[6] = dev.actesTraitement[fi].Radios[fj].prix_traitement * dev.actesTraitement[fi].Radios[fj].Qte;
                        MontantHonoraires = MontantHonoraires + dev.actesTraitement[fi].Radios[fj].prix_traitement * (dev.actesTraitement[fi].Radios[fj].Qte);
                        MontantHonorairesAvantRemise = MontantHonorairesAvantRemise + (dev.actesTraitement[fi].Radios[fj].prix_acte * dev.actesTraitement[fi].Radios[fj].Qte);



                        TmpCtrActes = CtrActes;
                        TrouveActe = false;
                        for (int ff = 0; ff <= TmpCtrActes; ff++)
                        {
                            if (dataArrayActes[ff] != null)
                            {
                                object[] TmpoActe = new object[8];
                                TmpoActe = (object[])dataArrayActes[ff];
                                if (Convert.ToInt32(TmpoActe[7]) == Convert.ToInt32(oActe[7]))
                                {
                                    TmpoActe[1] = Convert.ToInt32(TmpoActe[1]) + Convert.ToInt32(oActe[1]);
                                    TmpoActe[4] = Convert.ToInt32(TmpoActe[4]) + Convert.ToInt32(oActe[4]);
                                    TmpoActe[5] = Convert.ToDouble(TmpoActe[5]) + Convert.ToDouble(oActe[5]);
                                    TmpoActe[6] = Convert.ToDouble(TmpoActe[6]) + Convert.ToDouble(oActe[6]);
                                    dataArrayActes[ff] = TmpoActe;
                                    TrouveActe = true;
                                    break;
                                }

                            }

                        }
                        if (!(TrouveActe))
                        {
                            dataArrayActes[CtrActes] = oActe;
                            CtrActes = CtrActes + 1;
                        }
                     


                        //dataArrayActes[CtrActes] = oActe;
                        //CtrActes = CtrActes + 1;

                    }
                    for (int fj = 0; fj < dev.actesTraitement[fi].photos.Count; fj++)
                    {
                        object[] oActe = new object[8];

                        lln = EspaceDroite("AAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].photos[fj].nomenclature);
                         oActe[0] = dev.actesTraitement[fi].photos[fj].nomenclature.PadRight((int)lln, ' ');
                         oActe[0] = dev.actesTraitement[fi].photos[fj].nomenclature;

                         lln = EspaceDroite("AAAAAAAAAA", dev.actesTraitement[fi].photos[fj].Qte.ToString());
                         oActe[1] = dev.actesTraitement[fi].photos[fj].Qte.ToString().PadRight((int)lln, ' ');
                         oActe[1] = dev.actesTraitement[fi].photos[fj].Qte.ToString();


                         float ll4 = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].photos[fj].LibActe_estimation);

                        oActe[2] = dev.actesTraitement[fi].photos[fj].LibActe_estimation.PadRight((int)ll4, ' ');
                        oActe[2] = dev.actesTraitement[fi].photos[fj].LibActe_estimation;
                        using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new System.Drawing.Bitmap(1, 1)))
                        {
                            System.Drawing.SizeF sizeChar = graphics.MeasureString(dev.actesTraitement[fi].photos[fj].LibActe_estimation.PadRight((int)ll4, '.'), new System.Drawing.Font("Garamond", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point));
                            while (sizeChar.Width > longuerlibelle)
                            {
                                ll4 = ll4 - 1;
                                sizeChar = graphics.MeasureString(dev.actesTraitement[fi].photos[fj].LibActe_estimation.PadRight((int)ll4, '.'), new System.Drawing.Font("Garamond", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point));

                            }
                        }
                        oActe[2] = dev.actesTraitement[fi].photos[fj].LibActe_estimation.PadRight((int)ll4+1, ' ');
                        oActe[2] = dev.actesTraitement[fi].photos[fj].LibActe_estimation;


                      //  oActe[2] = dev.actesTraitement[fi].photos[fj].LibActe_estimation.PadRight(120 - dev.actesTraitement[fi].photos[fj].LibActe_estimation.Length, ' ');

                        lln = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].photos[fj].cotation);
                        oActe[3] = dev.actesTraitement[fi].photos[fj].cotation.PadRight((int)lln, ' ');
                        oActe[3] = dev.actesTraitement[fi].photos[fj].cotation;
                        lln = EspaceDroite("AAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].photos[fj].nombre_points);
                        oActe[4] = (double.Parse(dev.actesTraitement[fi].photos[fj].nombre_points) * dev.actesTraitement[fi].photos[fj].Qte).ToString().PadRight((int)lln, ' ');
                        oActe[4] = (double.Parse(dev.actesTraitement[fi].photos[fj].nombre_points) * dev.actesTraitement[fi].photos[fj].Qte);
                        NombrePoints = NombrePoints + (double.Parse(dev.actesTraitement[fi].photos[fj].nombre_points) * dev.actesTraitement[fi].photos[fj].Qte);
                        lln = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAA", dev.actesTraitement[fi].photos[fj].prix_acte.ToString());
                       
                        oActe[5] = (dev.actesTraitement[fi].photos[fj].prix_acte * (dev.actesTraitement[fi].photos[fj].Qte)).ToString("C2").PadRight((int)lln, ' '); ;
                        oActe[5] = (dev.actesTraitement[fi].photos[fj].prix_acte * (dev.actesTraitement[fi].photos[fj].Qte));
                        oActe[6] = (dev.actesTraitement[fi].photos[fj].prix_traitement * (dev.actesTraitement[fi].photos[fj].Qte)).ToString("C2");
                        oActe[6] = (dev.actesTraitement[fi].photos[fj].prix_traitement * (dev.actesTraitement[fi].photos[fj].Qte));

                        oActe[7] = dev.actesTraitement[fi].photos[fj].IdActe;
                        //oActe[5] = dev.actesTraitement[fi].photos[fj].prix_acte;
                        //oActe[6] = dev.actesTraitement[fi].photos[fj].prix_traitement * dev.actesTraitement[fi].photos[fj].Qte;
                        MontantHonoraires = MontantHonoraires + dev.actesTraitement[fi].photos[fj].prix_traitement * (dev.actesTraitement[fi].photos[fj].Qte); ;

                        MontantHonorairesAvantRemise = MontantHonorairesAvantRemise + (dev.actesTraitement[fi].photos[fj].prix_acte * dev.actesTraitement[fi].photos[fj].Qte);


                         TmpCtrActes = CtrActes;
                         TrouveActe = false;
                         for (int ff = 0; ff <= TmpCtrActes; ff++)
                         {
                             if (dataArrayActes[ff] != null)
                             {
                                 object[] TmpoActe = new object[8];
                                 TmpoActe = (object[])dataArrayActes[ff];
                                 if (Convert.ToInt32(TmpoActe[7]) == Convert.ToInt32(oActe[7]))
                                 {
                                     if (ff==1)
                                     OLEAccess.BASLetter.AddAttribut("Position1" , NombrePoints);
                                     if (ff == 2)
                                         OLEAccess.BASLetter.AddAttribut("Position2", NombrePoints);
                                     
                                     TmpoActe[1] = Convert.ToInt32(TmpoActe[1]) + Convert.ToInt32(oActe[1]);
                                     TmpoActe[4] = Convert.ToInt32(TmpoActe[4]) + Convert.ToInt32(oActe[4]);
                                     TmpoActe[5] = Convert.ToDouble(TmpoActe[5]) + Convert.ToDouble(oActe[5]);
                                     TmpoActe[6] = Convert.ToDouble(TmpoActe[6]) + Convert.ToDouble(oActe[6]);
                                     dataArrayActes[ff] = TmpoActe;
                                     TrouveActe = true;
                                     break;
                                 }

                             }

                         }
                         if (!(TrouveActe))
                         {
                            
                                
                             dataArrayActes[CtrActes] = oActe;
                             CtrActes = CtrActes + 1;
                         }
                     

                        //dataArrayActes[CtrActes] = oActe;
                        //CtrActes = CtrActes + 1;

                    }
                 /*   for (int fj = 0; fj < dev.actesTraitement[fi].Materiels.Count; fj++)
                    {
                        object[] oActe = new object[7];

                        oActe[0] = "0";
                        oActe[1] = dev.actesTraitement[fi].Materiels[fj].Qte.ToString();
                        oActe[2] = dev.actesTraitement[fi].Materiels[fj].Libelle;
                        oActe[3] = "0";
                        oActe[4] = "0";
                        oActe[5] = dev.actesTraitement[fi].Materiels[fj].prix_materiel ;
                        oActe[6] = dev.actesTraitement[fi].Materiels[fj].prix_traitement  ;


                        //oActe[0] = "Matériel: " + dev.actesTraitement[fi].Materiels[fj].Libelle;
                      
                        //oActe[1] = dev.actesTraitement[fi].Materiels[fj].prix_materiel ;
                        //oActe[2] = dev.actesTraitement[fi].Materiels[fj].prix_traitement;
                        dataArrayActes[CtrActes] = oActe;
                        CtrActes = CtrActes + 1;

                    }*/
                   
                  OLEAccess.BASLetter.AddAttribut("NombrePoints", NombrePoints );
                OLEAccess.BASLetter.AddAttribut("ValeurInitiale", ValeurInitiale );

                OLEAccess.BASLetter.AddAttribut("MontantFraisLaboratoire", MontantFraisLaboratoire);
                OLEAccess.BASLetter.AddAttribut("MontantSterilisation", MontantSterilisation);
       


                double b;

               
                bool isBValid = double.TryParse(ValeurInitiale.Replace(".", ","), out b);

                //OLEAccess.BASLetter.AddAttribut("MontantHonoraires", (NombrePoints * b).ToString ("C2"));
            
                    
              
                 //  o[3] = dataArrayActes;
                  
                  
                    for (int fj = 0; fj < dev.actesTraitement[fi].echeancestemp.Count ; fj++)
                    {
                        object[] oEcheance = new object[3];
                        oEcheance[0] = "Echéance: " + dev.actesTraitement[fi].echeancestemp[fj].Libelle;
                        oEcheance[1] =  dev.actesTraitement[fi].echeancestemp[fj].Montant ;
                        oEcheance[2] = dev.actesTraitement[fi].echeancestemp[fj].DAteEcheance;

                        int TmpCtrEcheances = CtrEcheances;
                        bool TrouveEcheance = false;
                        for (int ff = 0; ff < TmpCtrEcheances; ff++)
                        {
                            if (dataArrayEcheances[ff] != null)
                            {
                                object[] TmpoEcheance = new object[3];
                                TmpoEcheance = (object[])dataArrayEcheances[ff];
                                if (Convert.ToDateTime(TmpoEcheance[2]).ToString("dd/MM/yyyy") == Convert.ToDateTime(oEcheance[2]).ToString("dd/MM/yyyy"))
                                {
                                    TmpoEcheance[1] = Convert.ToDouble(TmpoEcheance[1]) + Convert.ToDouble(oEcheance[1]);
                                   
                                    dataArrayEcheances[ff] = TmpoEcheance;
                                    TrouveEcheance = true;
                                    break;
                                }

                            }

                        }
                        if (!(TrouveEcheance))
                        {
                            dataArrayEcheances[CtrEcheances] = oEcheance;
                            CtrEcheances = CtrEcheances + 1;
                        }




                       // dataArrayEcheances[fj] = oEcheance;
                    }
                    o[4] = dataArrayEcheances;
                    //dataArrayCommtraitements[fi] = o;
                   
                }


                dataArrayEcheances  = dataArrayEcheances .Where(c => c != null).ToArray();
                dataArrayActes = dataArrayActes.Where(c => c != null).ToArray();
                float IndexImpression=0;
                Object[] dataArrayLibelle = new object[50];
                Object[] dataArrayValeur = new object[50];
                for (int fj = 0; fj < CtrActes ; fj++)
                {
                    object[] TmpActes = new object[8];
                    TmpActes = (object[])dataArrayActes[fj];
                    //IndexImpression = EspaceDroite("AAAAAAAAAAAAAAAAAA", TmpActes [0].ToString ());
                    IndexImpression = EspaceDroite("AAAAAAAAAAAAA", TmpActes[0].ToString());
                   // TmpActes[0] = TmpActes[0].ToString().PadRight((int)IndexImpression, ' ');
                    TmpActes[0] = TmpActes[0].ToString();
                    string Variable = "Position" + fj.ToString();
                    OLEAccess.BASLetter.AddAttribut(Variable, TmpActes[0]);
                 

                    IndexImpression = EspaceDroite("AAAAAAAAAA", TmpActes[1].ToString());
                    //TmpActes[1] = TmpActes[1].ToString().PadRight((int)IndexImpression, ' ');
                    TmpActes[1] = TmpActes[1].ToString();
                    Variable = "Quantite" + fj.ToString();
                    OLEAccess.BASLetter.AddAttribut(Variable, TmpActes[1]);

                   // IndexImpression = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", TmpActes[2].ToString());
                    IndexImpression = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", TmpActes[2].ToString());
                    //TmpActes[2] = TmpActes[2].ToString().PadRight((int)IndexImpression, ' ');
                    TmpActes[2] = TmpActes[2].ToString();
                    Variable = "Libelle" + fj.ToString();
                    OLEAccess.BASLetter.AddAttribut(Variable, TmpActes[2]);

                    OLEAccess.BASLetter.AddAttribut("TableauValeur", dataArrayValeur);
                    OLEAccess.BASLetter.AddAttribut("TableauValeur", dataArrayValeur);
                   // IndexImpression = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAA", TmpActes[3].ToString());
                    IndexImpression = EspaceDroite("AAAAAAAAAAAAAA", TmpActes[3].ToString());
                    //TmpActes[3] = TmpActes[3].ToString().PadRight((int)IndexImpression, ' ');
                    TmpActes[3] = TmpActes[3].ToString();
                    Variable = "Valeur" + fj.ToString();
                    OLEAccess.BASLetter.AddAttribut(Variable, TmpActes[3]);

                    //IndexImpression = EspaceDroite("AAAAAAAAAAAAAAAAAAAA", TmpActes[4].ToString());
                    IndexImpression = EspaceDroite("AAAAAAAAAAAA", TmpActes[4].ToString());
                    //TmpActes[4] = TmpActes[4].ToString().PadRight((int)IndexImpression, ' ');
                    TmpActes[4] = TmpActes[4].ToString();
                    
                    
                    TmpActes[5] = Convert.ToDouble(TmpActes[5]).ToString("C2");
                    //IndexImpression = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAA", TmpActes[5].ToString());
                    IndexImpression = EspaceDroite("AAAAAAAAAAAAAAAAAAAAAAAAA", TmpActes[5].ToString());
                    //TmpActes[5] = TmpActes[5].ToString().PadRight((int)IndexImpression, ' ');
                    TmpActes[5] = TmpActes[5].ToString();

                    TmpActes[6] = Convert.ToDouble(TmpActes[6]).ToString("C2");
                   // dataArrayActes[0] = dev.actesTraitement[fi].photos[fj].nomenclature.PadRight((int)idx, ' ');
                
                
                }

                dataArrayValeur = dataArrayValeur.Where(c => c != null).ToArray();
                dataArrayLibelle = dataArrayLibelle.Where(c => c != null).ToArray();
                OLEAccess.BASLetter.AddAttribut("TableauLibelle", dataArrayLibelle);
                OLEAccess.BASLetter.AddAttribut("TableauValeur", dataArrayValeur);
                //-------
                object[] arr5 = (object[])dataArrayValeur;
                string sb5 = "";
                object[] arrActes = (object[])((object[])arr5);
                for (int idxActes = 0; idxActes < arrActes.Length; idxActes++)
                {

                    sb5 += (string)((object[])arrActes)[idxActes] + "\n";

                }

               



                //------

                dataArrayCommtraitements[0] = dataArrayActes;
                dataArrayCommtraitements[1] = dataArrayEcheances;

                OLEAccess.BASLetter.AddAttribut("Traitements", dataArrayCommtraitements);

                OLEAccess.BASLetter.AddAttribut("MontantHonoraires", MontantHonoraires);
                OLEAccess.BASLetter.AddAttribut("MontantHonorairesAvantRemise", MontantHonorairesAvantRemise);
                OLEAccess.BASLetter.AddAttribut("MontantTotal", MontantHonoraires + MontantFraisLaboratoire + MontantSterilisation);
                OLEAccess.BASLetter.AddAttribut("MontantTotalAvantRemise", MontantHonorairesAvantRemise + MontantFraisLaboratoire + MontantSterilisation);
                OLEAccess.BASLetter.AddAttribut("TitreDevis", dev.Titre);
                //Fin Comtraitements
              
                


            /*    object[] arr = (object[])dataArrayCommtraitements;

                string lib = "";
                string libActe = "";
                string libEcheance = "";

                string Montant = "";
                string MontantActe = "";
                string PositionActe = "";
                string QuantiteActe = "";
                string ValeurActe = "";
                string PointsActe = "";
                string MontantAvantRemiseActe = "";



                string MontantEcheance = "";

                string DateEcheance = "";
                string sb = "";
                string colonne1 = "Position";
                string colonne2 = "Qté";
                string colonne3 = "Libellé";
                string colonne4 = "Valeur";
                string colonne5 = "Nb Points";
                string colonne6 = "Montant";
                string colonne7 = "Montant proposé";

                colonne1 = colonne1.PadRight(20 - colonne1.Length);
                colonne2 = colonne2.PadRight(35 - colonne2.Length);
                colonne3 = colonne3.PadRight(93 - colonne3.Length);
                colonne4 = colonne4.PadRight(20 - colonne4.Length);
                colonne5 = colonne5.PadRight(35 - colonne5.Length);
                colonne6 = colonne6.PadRight(25 - colonne6.Length);
                sb += colonne1 + colonne2 + colonne3 + colonne4 + colonne5 + colonne6 + colonne7;

                 
                        object[] arrActes = (object[])((object[])arr[0]);
                        //for (int idxActes=0;idxActes<arrActes.Length;idxActes++)
                        for (int idxActes = 0; idxActes < arrActes.Length; idxActes++)
                        {
                            //if (idxActes%3==0&&idxActes>0)sb+="\n";

                            // MontantActe = ((double)((object[])arrActes[idxActes])[6]).ToString("C2");
                            MontantActe = (string)((object[])arrActes[idxActes])[6];

                            PositionActe = (string)((object[])arrActes[idxActes])[0];
                            QuantiteActe = (string)((object[])arrActes[idxActes])[1];
                            libActe = (string)((object[])arrActes[idxActes])[2];
                            ValeurActe = (string)((object[])arrActes[idxActes])[3];
                            PointsActe = (string)((object[])arrActes[idxActes])[4];
                            //MontantAvantRemiseActe = ((double)((object[])arrActes[idxActes])[5]).ToString("C2");
                            MontantAvantRemiseActe = (string)((object[])arrActes[idxActes])[5];

                            sb += "\n";

                            //sb += PositionActe.PadRight(20 - PositionActe.Length) +  QuantiteActe.PadRight(20 - QuantiteActe.Length) + libActe.PadRight(113 - libActe.Length) +  ValeurActe.PadRight(25 - ValeurActe.Length) + PointsActe.PadRight(28 - PointsActe.Length) +  MontantActe;
                            sb += "        " + PositionActe + QuantiteActe + libActe + ValeurActe + PointsActe + MontantAvantRemiseActe + MontantActe;
                        }
                        sb += "\n";*/


                    //impression Echeances

                        object[] arr = (object[])dataArrayCommtraitements;

                        string lib = "";
                        string libActe = "";
                        string libEcheance = "";

                        string Montant = "";
                        string MontantActe = "";
                        string MontantEcheance = "";

                        string dateEcheance = "";
                        string sb = "";



                     //   for (int idx = 0; idx < arr.Length; idx++)
                    //    {
                    //        sb += "        Traitement " + (idx + 1).ToString() + ":\n";
                    //        if (idx < arr.Length)
                          //  {
                                object[] arrEcheances = (object[])((object[])arr[1]);
                                for (int idxEcheances = 0; idxEcheances < arrEcheances.Length; idxEcheances++)
                                {
                                    if (idxEcheances % 3 == 0 && idxEcheances > 0) sb += "\n";
                                    //libEcheance = (string)((object[])arrEcheances[idxEcheances])[0];

                                    libEcheance = "Echéance " + (idxEcheances + 1).ToString();
                                    MontantEcheance = ((double)((object[])arrEcheances[idxEcheances])[1]).ToString("C2");
                                    dateEcheance = ((DateTime)((object[])arrEcheances[idxEcheances])[2]).ToShortDateString();

                                    sb += "        " + libEcheance + " le " + dateEcheance + " de " + MontantEcheance;

                                }
                           // }
                        //    sb += "\n\n";
                   //     }
		

                //Fin Impression Echeances

               

                 
                //Fin test generation
               

                Object[] dataArray = new object[Montants.Count];


                for (int fi = 0; fi < Montants.Count; fi++)
                {
                    object[] o = new object[4];
                    o[0] = Montants[fi].Libelle;
                    o[1] = 1415+fi;
                    o[2] = Montants[fi].DAteEcheance;
                    o[3] =1000*fi;
                    dataArray[fi] = o;
                }


                OLEAccess.BASLetter.AddAttribut("financement", dataArray);
            


            }
          
            public static bool PrintDevis_TK(Devis_TK devis, basePatient CurrentPatient)
            {
                return PrintDevis_TK(devis, CurrentPatient, "");
            }
            public static bool PrintDevis_TK(Devis_TK devis, basePatient CurrentPatient, string file)
            {
                List<Proposition> lstProp = new List<Proposition>();

                /*  if (devis.propositions != null)
                      foreach (Proposition p in devis.propositions)
                          lstProp.Add(p);*/



                if (file != "")
                {
                    Correspondant c = MgmtCorrespondants.getCorrespondant(CurrentPatient.Id);
                    Correspondant praticien = MgmtCorrespondants.getCorrespondant(CurrentPatient.infoscomplementaire.PraticienResponsable.Id);
                    CommonActions.GenerateAndPrintDevis_TK(file, praticien, c, devis, CurrentPatient);
                    return true;
                }
                return false;
            }
            public static void GenerateAndPrintDevis_TK(string file, Correspondant Praticien, Correspondant c, Devis_TK d, basePatient patient)
            {


                AddCourrierAttributsDevis_TK(Praticien, d, patient);
                OLEAccess.BASLetter.GenerateFrom(file);


            }
            public static bool PrintDevis(Devis devis, basePatient CurrentPatient)
            {
                return PrintDevis(devis, CurrentPatient, "");
            }
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
                int IdUser;

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
            private static string _templateFolder = "";
            public static string templateFolder
            {
                get
                {
                    return System.Configuration.ConfigurationManager.AppSettings["TEMPLATE_FOLDER" + prefix];
                }
                set
                {
                    _templateFolder = "_" + value;
                }


            }
            public static bool PrintDevis(Devis devis, basePatient CurrentPatient,string file)
            {
                string folder = "";
                bool cancontinue = true;

                if (!File.Exists(file))
                {

                    if (devis.TypeDevis == BasCommon_BO.Devis.enumtypePropositon.Aucun)
                    {
                        switch (devis.propositions[0].traitements[0].Phase)
                        {
                            case Traitement.EnumPhase.Adulte: devis.TypeDevis = Devis.enumtypePropositon.Adulte; break;
                            case Traitement.EnumPhase.Contention: devis.TypeDevis = Devis.enumtypePropositon.Cont1; break;
                            case Traitement.EnumPhase.Orthodontique: devis.TypeDevis = Devis.enumtypePropositon.Orthodontie; break;
                            case Traitement.EnumPhase.Orthopedique: devis.TypeDevis = Devis.enumtypePropositon.Orthopedie; break;
                            case Traitement.EnumPhase.Pédiatrique: devis.TypeDevis = Devis.enumtypePropositon.Pediatrie; break;
                            case Traitement.EnumPhase.FinitionAdulte: devis.TypeDevis = Devis.enumtypePropositon.FinitionAdulte; break;
                            default: devis.TypeDevis = Devis.enumtypePropositon.Aucun; break;
                        }
                    }




                    switch (devis.TypeDevis)
                    {
                        case BasCommon_BO.Devis.enumtypePropositon.Aucun: return false;
                        case BasCommon_BO.Devis.enumtypePropositon.Orthopedie: folder = templateFolder+ System.Configuration.ConfigurationManager.AppSettings["Devis_Orthopedie"]; break;
                        case BasCommon_BO.Devis.enumtypePropositon.Orthodontie: folder = templateFolder+ System.Configuration.ConfigurationManager.AppSettings["Devis_Orthopedontie"]; break;
                        case BasCommon_BO.Devis.enumtypePropositon.Adulte: folder = templateFolder+ System.Configuration.ConfigurationManager.AppSettings["Devis_Adulte"]; break;
                        case BasCommon_BO.Devis.enumtypePropositon.Pediatrie: folder = templateFolder+ System.Configuration.ConfigurationManager.AppSettings["Devis_Sucette"]; break;
                        case BasCommon_BO.Devis.enumtypePropositon.Cont1: folder = templateFolder+ System.Configuration.ConfigurationManager.AppSettings["Devis_Contention1"]; break;
                        case BasCommon_BO.Devis.enumtypePropositon.Cont2: folder = templateFolder+ System.Configuration.ConfigurationManager.AppSettings["Devis_Contention2"]; break;
                        case BasCommon_BO.Devis.enumtypePropositon.ContentionAdulte: folder = templateFolder+ System.Configuration.ConfigurationManager.AppSettings["Devis_Contention_Adulte"]; break;
                        case BasCommon_BO.Devis.enumtypePropositon.FinitionAdulte: folder = templateFolder+ System.Configuration.ConfigurationManager.AppSettings["Devis_Finition_Adulte"]; break;
                        case BasCommon_BO.Devis.enumtypePropositon.ALaCarte: folder = templateFolder+ System.Configuration.ConfigurationManager.AppSettings["Devis_ALaCarte"]; break;

                    }

                    if (folder == "")
                    {
                        BasCommon_BO.Devis.enumtypePropositon tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Orthodontie;


                        if (CodesTraitement.IsPediatrie(devis.propositions[0].traitements[0].CodeTraitement))
                            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Pediatrie;

                        if (CodesTraitement.IsOrthopedie(devis.propositions[0].traitements[0].CodeTraitement))
                            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Orthopedie;

                        if (CodesTraitement.IsOrthodontieEnfant(devis.propositions[0].traitements[0].CodeTraitement))
                            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Orthodontie;



                        if (CodesTraitement.IsFinitionAdulte(devis.propositions[0].traitements[0].CodeTraitement))
                            tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.FinitionAdulte;
                        else
                        {
                            if (CodesTraitement.IsAdulte(devis.propositions[0].traitements[0].CodeTraitement))
                                tpetrmnt = BasCommon_BO.Devis.enumtypePropositon.Adulte;
                        }



                        //devis.propositions[0].traitements[0].semestres[0];

                        switch (tpetrmnt)
                        {
                            case BasCommon_BO.Devis.enumtypePropositon.Orthopedie: folder =templateFolder +  System.Configuration.ConfigurationManager.AppSettings["Devis_Orthopedie"]; break;
                            case BasCommon_BO.Devis.enumtypePropositon.Orthodontie: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Orthopedontie"]; break;
                            case BasCommon_BO.Devis.enumtypePropositon.Adulte: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Adulte"]; break;
                            case BasCommon_BO.Devis.enumtypePropositon.Pediatrie: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Sucette"]; break;
                            case BasCommon_BO.Devis.enumtypePropositon.Cont1: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Contention1"]; break;
                            case BasCommon_BO.Devis.enumtypePropositon.Cont2: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Contention2"]; break;
                            case BasCommon_BO.Devis.enumtypePropositon.ContentionAdulte: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Contention_Adulte"]; break;
                            case BasCommon_BO.Devis.enumtypePropositon.FinitionAdulte: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Finition_Adulte"]; break;
                                

                        }
                    }

                    
                    try
                    {
                        string[] ss = Directory.GetFiles(folder);



                        if (ss.Length == 1)
                            file = ss[0];
                        else
                        {
                            FrmWizardCourrierForSummary frmletter = new FrmWizardCourrierForSummary(folder);
                            if (frmletter.ShowDialog() == DialogResult.OK)
                            {
                                file = frmletter.FileName;
                            }
                            else
                                cancontinue = false;

                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Courrier des devis introuvable!\n\n" + ex.Message);
                        return false;
                    }

                }

                if (cancontinue)
                {

                    List<Proposition> lstProp = new List<Proposition>();

                    if (devis.propositions!=null)
                         foreach (Proposition p in devis.propositions)
                                lstProp.Add(p); 
                    


                    if (file != "")
                    {
                        Correspondant c = MgmtCorrespondants.getCorrespondant(CurrentPatient.Id);
                        Correspondant praticien = MgmtCorrespondants.getCorrespondant(CurrentPatient.infoscomplementaire.PraticienResponsable.Id);
                        CommonActions.GenerateAndPrintDevis(file, praticien, c, devis, CurrentPatient);
                        return true;
                    }
                }

                return false;
            }



            public static void GenerateAndPrintConsentement(string file, Correspondant Praticien, basePatient _CurrentPatient, Correspondant c, bool DirectPrint)
            {


                AddCourrierAttributsConsentement(Praticien, _CurrentPatient);
                OLEAccess.BASLetter.GenerateFrom(file);

            }


            public static void AddCourrierAttributsConsentement(Correspondant Praticien, basePatient _CurrentPatient)
            {


                if (_CurrentPatient.contacts == null)
                    baseMgmtPatient.FillContacts(_CurrentPatient);


                List<string> lstRisques = new List<string>();
                foreach (Proposition p in _CurrentPatient.propositions)
                {
                    foreach (string r in PropositionMgmt.GetRisques(p))
                    {
                        if (!lstRisques.Contains(r))
                            lstRisques.Add(r);
                    }
                }

                string risques = lstRisques.Count == 0 ? "" : lstRisques.Aggregate((i, j) => i + "\n" + j);

                string objectifs = "";

                foreach (CommonObjectif co in _CurrentPatient.SelectedObjectifs)
                {
                    if (objectifs != "") objectifs += "\n";
                    objectifs += co.ToString();

                }



                OLEAccess.BASLetter.AddAttribut("objectifs", objectifs);



                OLEAccess.BASLetter.AddAttribut("Risques", risques);

                int y;
                int m;
                int d;
                _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

                OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
                OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
                OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
                OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et " + m.ToString() + " mois");
                OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Genre == basePatient.Sexe.Feminin ? "F" : "M");
                OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);
                if (_CurrentPatient.MainAdresse != null)
                {
                    OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _CurrentPatient.MainAdresse.adresse.Adr1);
                    OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _CurrentPatient.MainAdresse.adresse.Adr2);
                    OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _CurrentPatient.MainAdresse.adresse.CodePostal);
                    OLEAccess.BASLetter.AddAttribut("VillePatient", _CurrentPatient.MainAdresse.adresse.Ville);
                }
                if (_CurrentPatient.Tutoiement)
                    OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
                else
                    OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

                OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _CurrentPatient.DateNaissance.Date.ToString());
                OLEAccess.BASLetter.AddAttribut("NumSecu", _CurrentPatient.NumSecu.ToString());
                OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());

                OLEAccess.BASLetter.AddAttribut("ID_Praticien", Praticien.Id.ToString());

                OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
                OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
                OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);
                OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.MainMail == null ? "" : Praticien.MainMail.Value);
                OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
                OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.MainTel == null ? "" : Praticien.MainTel.Value);
                OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.MainTel == null ? "" : Praticien.MainTel.Value);
                OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.MainFax == null ? "" : Praticien.MainFax.Value);

                OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.Adr1);
                OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.Adr2);
                OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.CodePostal);
                OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.MainAdresse == null ? "" : Praticien.MainAdresse.adresse.Ville);
                if (Praticien.GenreFeminin)
                    OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
                else
                    OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");



            }

    }
}
