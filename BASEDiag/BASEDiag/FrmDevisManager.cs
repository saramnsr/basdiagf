using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BL;
using BASEDiag_BO;
using BasCommon_BO;
using BasCommon_BL;
using BaseCommonControls;
using System.IO;
using Microsoft.Win32;
namespace BASEDiag
{
    public partial class FrmDevisManager : Form
    {

        


        private List<Devis> _devis;
        public List<Devis> devis
        {
            get
            {
                return _devis;
            }
            set
            {
                _devis = value;
            }
        }
        private List<Devis_TK> _devisTK;
        public List<Devis_TK> devisTK
        {
            get
            {
                return _devisTK;
            }
            set
            {
                _devisTK = value;
            }
        }
       
        private List<Proposition> _propositions;
        public List<Proposition> propositions
        {
            get
            {
                return _propositions;
            }
            set
            {
                _propositions = value;
            }
        }

        private basePatient _Currentpatient;
        public basePatient Currentpatient
        {
            get
            {
                return _Currentpatient;
            }
            set
            {
                _Currentpatient = value;
            }
        }


        public FrmDevisManager(basePatient pat, InfoPatientComplementaire infocomplementaire, List<Proposition> props, List<Devis> lstdvi,List<Devis_TK> lstdevisTK)
        {
            InitializeComponent();
            Currentpatient = pat;
            propositions = props;

            devis = lstdvi;
            devisTK = lstdevisTK;
            
        }



        private void InitDisplay()
        {

            InitDisplayDevis();



        }

        private void InitDisplayDevis()
        {
            dgvDevis.Rows.Clear();
        
            foreach (Devis d in devis)
            {
                if (d.DateAcceptation != null) continue;
                List<object> lstCell = new List<object>();

                lstCell.Add(d.DateProposition);

                if (d.DateAcceptation == null)
                    lstCell.Add("");
                else
                    lstCell.Add(d.DateAcceptation.Value);

                if (d.DateEcheance == null)
                    lstCell.Add("");
                else
                    lstCell.Add(d.DateEcheance.Value);

                if (d.propositions == null)
                    d.propositions = PropositionMgmt.getPropositions(d);


                lstCell.Add(d.propositions.Count);

                string tr = "";
                double total = 0;
                foreach (Proposition p in d.propositions)
                {
                    if ((d.DateAcceptation == null) || (p.Etat == Proposition.EtatProposition.Accepté))
                    {
                        foreach (Traitement t in p.traitements)
                        {
                            if (tr != "") tr += "+";
                            tr += t.Libelle;
                        }
                        total += PropositionMgmt.GetTotal(p);
                    }
                }



                lstCell.Add(tr);

                if (d.DateAcceptation == null)
                {
                    lstCell.Add("Devis non accepté");
                }
                else
                {
                    lstCell.Add(total);
                }


                int idx = dgvDevis.Rows.Add(lstCell.ToArray());
                dgvDevis.Rows[idx].Tag = d;



            }
            //NADHEM Modification
          
            foreach (Devis_TK d in devisTK)
            {
                if (d.DateAcceptation != null) continue;
                List<object> lstCell = new List<object>();

                lstCell.Add(d.DateProposition);

                if (d.DateAcceptation == null)
                    lstCell.Add("");
                else
                    lstCell.Add(d.DateAcceptation.Value);

                if (d.DateEcheance == null)
                    lstCell.Add("");
                else
                    lstCell.Add(d.DateEcheance.Value);
                lstCell.Add("0");
                if (d.Traitement.Traitement_libelle == "")
                    lstCell.Add("");
                else
                    lstCell.Add(d.Traitement.Traitement_libelle);

                lstCell.Add(d.Montant.Value);



                int idx = dgvDevis.Rows.Add(lstCell.ToArray());
                dgvDevis.Rows[idx].Tag = d;

            }
        }
        
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FrmPropositionsManager_Load(object sender, EventArgs e)
        {                                
            
            InitDisplay();
        }

        
     
       
     
        private void pnlChooseProposition_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void pnlEcheancier_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        

      

        private void pnlEnd_VisibleChanged(object sender, EventArgs e)
        {
                           
        }
                


        

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void rbJusquau_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbMntEch_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void pnlEcheancier_Paint(object sender, PaintEventArgs e)
        {

        }

        

        

        private void btnPrint_Click(object sender, EventArgs e)
        {

           
        }


        /*
      
        public static void AddCourrierAttributsDevis(Correspondant Praticien, 
                                                        Devis dev,
                                                        basePatient _CurrentPatient)
        {

            if (_CurrentPatient.contacts == null)
                baseMgmtPatient.FillContacts(_CurrentPatient);

            TemplateActePG surv = TemplateApctePGMgmt.getCodeSecu("SURV");
            TemplateActePG survhn = TemplateApctePGMgmt.getCodeSecu("SURV_HN");


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_DEVIS", dev.Id);


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NbProps", dev.propositions.Count);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NbOptions", dev.actesHorstraitement.Count);

            int Optionnum = 1;
            foreach (ActePGPropose acte in dev.actesHorstraitement)
            {

                if ((acte.template == null) && (acte.IdTemplateActePG >= 0))
                    acte.template = TemplateApctePGMgmt.getCodeSecu(acte.IdTemplateActePG);

                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_Montant", acte.Qte * acte.Montant);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_Qte", acte.Qte);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_Libelle", acte.Libelle);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_PartSecu", acte.template.Coeff * acte.template.Code.Valeur);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_PartMutuelle", (acte.Qte * acte.Montant) - (acte.template.Coeff * acte.template.Code.Valeur));
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Option" + Optionnum.ToString() + "_LibSecu", acte.template.DisplayCodeNVal);
                Optionnum++;
            }

            
            int propnum = 1;
            foreach (Proposition prop in dev.propositions)
            {
                if (prop.Etat > Proposition.EtatProposition.Soumis) continue;

                
                int i = 0;
                


                foreach (Traitement t in prop.traitements)
                {
                    double tariftotal = TraitementMgmt.getTotal(t);
                    int nbMois = 0;
                    double RmbMutuelleParSemestre = 0;
                    double RmbSecuParSemestre = 0;
                    string LibSecuParSemestre = "";

                    foreach (Semestre s in t.semestres)
                    {
                        nbMois += s.traitementSecu.NBMois.Value;                        
                    }

                    double secu = t.semestres[0].traitementSecu.Code.Valeur * t.semestres[0].traitementSecu.Coeff;

                    RmbSecuParSemestre += secu;
                    RmbMutuelleParSemestre += t.semestres[0].Montant_Honoraire - secu;
                    if (LibSecuParSemestre != "") LibSecuParSemestre += "+";
                    LibSecuParSemestre += "(" + t.semestres[0].traitementSecu.DisplayCodeNVal + ")";


                    double totalParMois = tariftotal / nbMois;



                    PropositionObjectForLetters ob = new PropositionObjectForLetters();
                    ob.TarifParMois = totalParMois;
                    ob.Honoraires = t.semestres[0].Montant_Honoraire;
                    ob.NbMois = nbMois;
                    ob.PartSecu = RmbSecuParSemestre;
                    ob.PartMutuelle = RmbMutuelleParSemestre;
                    ob.LibSecu = LibSecuParSemestre;
                    ob.CodeTraitement = t.CodeTraitement;

                    //BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Propositions", ob);


                    if (!double.IsInfinity(totalParMois)) BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_TarifParMois", totalParMois.ToString("0.00"));
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_Honoraires", t.semestres[0].Montant_Honoraire.ToString("0.00"));
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_NbMois", nbMois.ToString());
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_PartSecu", RmbSecuParSemestre.ToString("0.00"));
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_PartMutuelle", RmbMutuelleParSemestre.ToString("0.00"));
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_LibSecu", LibSecuParSemestre);
                    BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Prop" + propnum.ToString() + "_CodeTraitement", t.CodeTraitement.Trim());
                    

                                     
                    i++;
                }

                

                propnum++;
            }


           // BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Devis", devis);

            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PATIENT", _CurrentPatient.Id.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPatient", _CurrentPatient.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPatient", _CurrentPatient.Prenom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("AgePatient", y.ToString() + " ans et " + m.ToString() + " mois");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePatient", _CurrentPatient.Genre == basePatient.Sexe.Feminin?"F":"M");
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePatient", _CurrentPatient.Civilite);

            if (_CurrentPatient.MainAdresse != null)
            {
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Patient", _CurrentPatient.MainAdresse.adresse.Adr1);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Patient", _CurrentPatient.MainAdresse.adresse.Adr2);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPatient", _CurrentPatient.MainAdresse.adresse.CodePostal);
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePatient", _CurrentPatient.MainAdresse.adresse.Ville);
            }
            if (_CurrentPatient.Tutoiement)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "TU");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TutoiementPatient", "VOUS");

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("DateNaissancePatient", _CurrentPatient.DateNaissance.Date.ToString());
			   BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumSecu", _CurrentPatient.NumSecu.ToString());
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NumDossierPatient", _CurrentPatient.Dossier.ToString());

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ID_PRATICIEN", Praticien.Id.ToString());

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TitrePraticien", Praticien.Titre);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("NomPraticien", Praticien.Nom);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("PrenomPraticien", Praticien.Prenom);

            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("MailPraticien", Praticien.MainMail == null ? "" : Praticien.MainMail.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("ProfessionPraticien", Praticien.Profession);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelFixePraticien", Praticien.MainTel == null ? "" : Praticien.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("TelProPraticien", Praticien.MainTel == null ? "" : Praticien.MainTel.Value);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("FaxPraticien", Praticien.MainFax == null ? "" : Praticien.MainFax.Value);


            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse1Praticien", Praticien.MainAdresse==null?"":Praticien.MainAdresse.adresse.Adr1);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("Adresse2Praticien", Praticien.MainAdresse==null?"":Praticien.MainAdresse.adresse.Adr2);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("CodePostalPraticien", Praticien.MainAdresse==null?"":Praticien.MainAdresse.adresse.CodePostal);
            BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("VillePraticien", Praticien.MainAdresse==null?"":Praticien.MainAdresse.adresse.Ville);
            if (Praticien.GenreFeminin)
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "F");
            else
                BASEDiag_BL.OLEAccess.BASLetter.AddAttribut("GenrePraticien", "M");




        }


        public static void GenerateAndPrintDevis(string file, Correspondant Praticien, Correspondant c, Devis d, basePatient patient)
        {


            AddCourrierAttributsDevis(Praticien,d, patient);
            BASEDiag_BL.OLEAccess.BASLetter.GenerateFrom(file);


        }

        */
        public static Devis CreateDevisInt( basePatient CurrentPatient)
        {
            FrmWizardPropositions frm = new FrmWizardPropositions(CurrentPatient.infoscomplementaire.NbSemestresEntame, CurrentPatient);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                string folder = "";
                string file = "";

                //Modification nadhem
                  if (frm.Value is NewTraitement)
                {


                    NewTraitement _Traitement = new NewTraitement();
                    _Traitement = TraitementsMgmt.GetFullTraitement(frm.Value.id_Traitement);

                    int pp = 0;
                    ActePG ap = new ActePG();
                    Devis_TK d = new Devis_TK();
                    d.DateProposition = DateTime.Now;
                    d.DateAcceptation = null;
                    d.DateArchivage = null;
                    d.DateEcheance = DateTime.Now.AddDays(15);
                    d.Titre = frm.TitreDevis;
                    d.IdPatient = CurrentPatient.Id;
                    d.Id_Traitement = _Traitement.id_Traitement;
                    d.Traitement = _Traitement;
                    d.DatePrevisionnelDeDebutTraitement = frm.DateDeDebut;
                    d.patient = CurrentPatient;
                    d.DatePrevisionnelDeFinTraitement = frm.DateDeFin;





                    TraitementsMgmt.GetCommTraitements(ref _Traitement);

                    d.actesTraitement = _Traitement.CommTraitement;



                    Double prix_total = 0;


                    double prix_ligne;

                    foreach (CommTraitement trt in d.Traitement.CommTraitement)
                    {

                        prix_ligne = 0;


                        //Gestion des scénarios
                        if (_Traitement.id_Traitement != -1)
                        {
                            trt.ActesSupp = TraitementsMgmt.GetCommActeSupTraitements(trt);
                            trt.Radios = TraitementsMgmt.GetCommActeSupTraitements(trt, "R");
                            trt.photos = TraitementsMgmt.GetCommActeSupTraitements(trt, "P");
                            trt.Materiels = TraitementsMgmt.GetCommMaterielsTraitements(trt);
                            trt.AutrePersonnes = TraitementsMgmt.GetTraitementAutrePersonne(trt);
                            trt.echeancestemp = new List<TempEcheanceDefinition>();

                        }

                        if (_Traitement.id_Traitement == -1)
                        {
                            //if (_id_traitement != -1 && DevisTraitement != null)
                            //{
                            trt.ActesSupp = MgmtDevis.GetCommActeSupDevis(trt);
                            trt.Radios = MgmtDevis.GetCommActeSupDevis(trt, "R");
                            trt.photos = MgmtDevis.GetCommActeSupDevis(trt, "P");
                            trt.Materiels = MgmtDevis.GetCommMaterielsDevis(trt);
                            trt.AutrePersonnes = MgmtDevis.GetDevisAutrePersonne(trt);
                            trt.echeancestemp = MgmtDevis.get_tempecheances_TK(trt);
                            if (trt.echeancestemp.Count == 0)
                            {

                                TempEcheanceDefinition ted = new TempEcheanceDefinition();
                                ted.DAteEcheance = trt.devis.DatePrevisionnelDeDebutTraitement.AddMonths(6);
                                ted.Montant = trt.prix;
                                ted.Libelle = trt.Acte.acte_libelle;
                                ted.acte = ap;
                                ted.acte.Libelle = trt.Acte.acte_libelle;
                                //ted.acte = com.acte;
                                ted.AlreadyPayed = false;
                                ted.payeur = Echeance.typepayeur.patient;
                                trt.echeancestemp.Add(ted);
                            }
                            else
                            {
                                foreach (BaseTempEcheanceDefinition bted in trt.echeancestemp)
                                {
                                    bted.acte = ap;
                                    bted.acte.Libelle = trt.Acte.acte_libelle;
                                }
                            }
                        }
                        else
                        {


                            if (d != null)
                            {
                                if (trt.ActesSupp == null)
                                    trt.ActesSupp = new List<CommActesTraitement>();


                                if (trt.Radios == null)
                                    trt.Radios = new List<CommActesTraitement>();

                                if (trt.photos == null)
                                    trt.photos = new List<CommActesTraitement>();

                                if (trt.Materiels == null)
                                    trt.Materiels = new List<CommMaterielTraitement>();

                                trt.Acte.prix_acte = trt.Acte.prix_traitement;

                                if (trt.ActesSupp != null)
                                {
                                    foreach (CommActesTraitement AC in trt.ActesSupp)
                                    {
                                        AC.prix_acte = AC.prix_traitement;
                                    }
                                }

                                if (trt.Radios != null)
                                {
                                    foreach (CommActesTraitement AC in trt.Radios)
                                    {
                                        AC.prix_acte = AC.prix_traitement;
                                    }
                                }
                                if (trt.photos != null)
                                {
                                    foreach (CommActesTraitement AC in trt.photos)
                                    {
                                        AC.prix_acte = AC.prix_traitement;
                                    }
                                }
                                if (trt.Materiels != null)
                                {
                                    foreach (CommMaterielTraitement AC in trt.Materiels)
                                    {
                                        AC.prix_materiel = AC.prix_traitement;
                                    }
                                }

                            }
                        }






                        if (trt.ActesSupp == null)
                            trt.ActesSupp = new List<CommActesTraitement>();


                        if (trt.Radios == null)
                            trt.Radios = new List<CommActesTraitement>();

                        if (trt.photos == null)
                            trt.photos = new List<CommActesTraitement>();

                        if (trt.Materiels == null)
                            trt.Materiels = new List<CommMaterielTraitement>();

                        if (trt.ActesSupp != null)
                        {
                            foreach (CommActesTraitement cap in trt.ActesSupp)
                            {
                                // cap.Parent = trt;
                                prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                                prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                            }
                        }
                        if (trt.Materiels != null)
                        {

                            foreach (CommMaterielTraitement cap in trt.Materiels)
                            {
                                cap.Famille = MaterielsMgmt.GetFamilleMaterielByIdMateriel(cap.idMateriel);
                                if (cap.Famille != null)
                                    if (cap.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0 || cap.Famille.libelle.ToLower().IndexOf("stérilisation") >= 0)
                                    {
                                        prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                                        prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                                    }
                            }
                        }
                        if (trt.Radios != null)
                        {

                            foreach (CommActesTraitement cap in trt.Radios)
                            {

                                prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                                prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                            }
                        }
                        if (trt.photos != null)
                        {

                            foreach (CommActesTraitement cap in trt.photos)
                            {

                                prix_total = prix_total + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));
                                prix_ligne = prix_ligne + (Convert.ToDouble(cap.prix_traitement) * Convert.ToInt32(cap.Qte));

                            }
                        }
                        prix_total = prix_total + (Convert.ToDouble(trt.Acte.prix_traitement) * Convert.ToInt32(trt.Acte.quantite));
                        prix_ligne = prix_ligne + (Convert.ToDouble(trt.Acte.prix_traitement) * Convert.ToInt32(trt.Acte.quantite));
                        trt.prix = prix_ligne;
                        //if (DevisTraitement != null)
                        //  if (DevisTraitement.actesTraitement == null) ;
                        // _DevisTraitement.MontantScenario = _DevisTraitement.MontantScenario + trt.prix;

                    }


                    // frm.value 


                    //d.actesTraitement = _Traitement.CommTraitement;
                    // MgmtDevis.CreateDevis_TK(d);
                    //  CurrentPatient.devis_TK.Add(d);


                    int NombreLabo = 0;
                    int NombreSterilisation = 0;
                    d.Montant = 0;
                    d.MontantAvantRemise = 0;
                    foreach (CommTraitement ct in d.actesTraitement)
                    {
                        d.Montant = d.Montant + ct.Acte.prix_traitement;
                        d.MontantAvantRemise = d.MontantAvantRemise + ct.Acte.prix_acte;
                        foreach (CommActesTraitement c in ct.ActesSupp)
                        {
                            d.Montant = d.Montant + c.prix_traitement;
                            d.MontantAvantRemise = d.MontantAvantRemise + c.prix_acte;
                        }
                        foreach (CommActesTraitement c in ct.Radios)
                        {
                            d.Montant = d.Montant + c.prix_traitement;
                            d.MontantAvantRemise = d.MontantAvantRemise + c.prix_acte;
                        }
                        foreach (CommActesTraitement c in ct.photos)
                        {
                            d.Montant = d.Montant + c.prix_traitement;
                            d.MontantAvantRemise = d.MontantAvantRemise + c.prix_acte;
                        }
                        foreach (CommMaterielTraitement c in ct.Materiels)
                        {
                            d.Montant = d.Montant + c.prix_traitement;
                            d.MontantAvantRemise = d.MontantAvantRemise + c.prix_materiel;
                            if (c.Famille != null)
                            {
                                if (c.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0)
                                    NombreLabo += 1;
                                if (c.Famille.libelle.ToLower().IndexOf("stérilisation") >= 0)
                                    NombreSterilisation += 1;
                            }
                        }
                        if (ct.echeancestemp.Count == 0)
                        {
                            int CtrSemestre = 0;
                            DateTime vDateEcheance = DateTime.Now;
                            if (ct.semestres.Count == 0)
                            {
                                for (int i = 1; i <= frm.Duree; i++)
                                {
                                    Semestre s = new Semestre();
                                    s.CodeSemestre = ct.Acte.acte_libelle;
                                    s.DateDebut = vDateEcheance.AddMonths(6);
                                    s.DateFin = s.DateDebut.AddMonths(6);
                                    vDateEcheance = vDateEcheance.AddMonths(6);
                                    s.Montant_Honoraire = TraitementsMgmt.GetPrixCom(ct);
                                    s.Montant_AvantRemise = TraitementsMgmt.GetPrixComAvantRemise(ct);
                                    //s.traitementSecu = tmplte;
                                    //s.Parent = t;
                                    CtrSemestre++;
                                    s.NumSemestre = CtrSemestre;


                                    ct.semestres.Add(s);
                                }
                            }

                        }
                    }
                    d.MontantScenario = d.Montant;
                    MgmtDevis.CreateDevis_TK(d);

                    CurrentPatient.devis_TK.Add(d);

                    

                    ////////////////////////////

                }
                else
                {

                if (frm.tpetrmnt == BasCommon_BO.Devis.enumtypePropositon.Aucun) return null;
                switch (frm.tpetrmnt)
                {
                    case BasCommon_BO.Devis.enumtypePropositon.Aucun: return null;
                    case BasCommon_BO.Devis.enumtypePropositon.Orthopedie: folder = templateFolder +  System.Configuration.ConfigurationManager.AppSettings["Devis_Orthopedie"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Orthodontie: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Orthopedontie"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Adulte: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Adulte"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Pediatrie: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Sucette"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Cont1: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Contention1"]; break;
                    case BasCommon_BO.Devis.enumtypePropositon.Cont2: folder = templateFolder + System.Configuration.ConfigurationManager.AppSettings["Devis_Contention2"]; break;

                }

                bool cancontinue = true;
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
                    MessageBox.Show("Courrier des devis introuvable!\n Le devis sera créé sans impression:\n\n"+ex.Message);
                }


                if (cancontinue)
                {

                    List<Proposition> lstProp = new List<Proposition>();

                    foreach (Proposition p in frm.value)
                    {
                        PropositionMgmt.InsertFullProposition(p);
                        lstProp.Add(p);
                    }

                    Devis d = MgmtDevis.CreateDevis(lstProp, frm.ActesMateriel,frm.tpetrmnt,frm.DateDeDebut,frm.DateDeFin);
                    string devis = file;

                    if (devis != "")
                    {
                        Correspondant c = MgmtCorrespondants.getCorrespondant(CurrentPatient.Id);
                        Correspondant praticien = MgmtCorrespondants.getCorrespondant(CurrentPatient.infoscomplementaire.PraticienResponsable.Id);
                       // BaseCommonControls.CommonActions.GenerateAndPrintDevis(devis, praticien, c,d, CurrentPatient);

                      

                        foreach (Proposition p in d.propositions)
                            if ((p.echeancestemp == null) || p.echeancestemp.Count == 0)
                                p.echeancestemp = PropositionMgmt.LoadDefaultTempecheances(p);

                        BaseCommonControls.CommonActions.PrintDevis(d, CurrentPatient);

                       

                    }

                   



                    return d;

                    
                }
                }
            }

            return null;
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

        private void btnPrintDevis_Click(object sender, EventArgs e)
        {

            Devis d = CreateDevisInt( Currentpatient);
            if (d!=null)
                devis.Add(d);

            InitDisplay();

           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           
        }

        private void dgvpropositions_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            


        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRevoirDevis_Click(object sender, EventArgs e)
        {
            
        }

        private void supprimerLeDevisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvDevis.SelectedRows.Count <= 0) return;



            if (dgvDevis.SelectedRows[0].Tag is Devis_TK)
            {
                Devis_TK devis = ((Devis_TK)dgvDevis.SelectedRows[0].Tag);
                if (devis.DateAcceptation != null)
                {
                    MessageBox.Show("Ce devis à été accepté\nSuppression impossible", "suppression impossible", MessageBoxButtons.OK);
                    return;
                }

                if (MessageBox.Show("Souhaitez-vous réellement supprimer ce devis?", "Supression devis", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                foreach (CommTraitement cmm in devis.actesTraitement)
                {
                    MgmtDevis.DeleteTempEcheance_TK(cmm);
                } 

                MgmtDevis.DeleteDevis_TK(devis);

                this.devisTK.Remove(devis);
            }
            else
            {
                Devis devis = ((Devis)dgvDevis.SelectedRows[0].Tag);
                if (devis.DateAcceptation != null)
                {
                    MessageBox.Show("Ce devis à été accepté\nSuppression impossible", "suppression impossible", MessageBoxButtons.OK);
                    return;
                }

                if (MessageBox.Show("Souhaitez-vous réellement supprimer ce devis?", "Supression devis", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                MgmtDevis.DeleteDevis(devis);

                this.devis.Remove(devis);
            }
            InitDisplayDevis();
        }

        
          }
}
