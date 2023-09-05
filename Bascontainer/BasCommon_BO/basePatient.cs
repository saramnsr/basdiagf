using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BasCommon_BO.ElementsEnBouche.BO;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.Data;
namespace BasCommon_BO
{

    public class basePatient : baseSmallPersonne
    {
        public enum StatusClinique
        {
            NouveauPatient,
            PasPret,
            EnAttente,
            EnAttenteAcceptationDevis,
            EnAttenteDevisSigne,
            EnTraitement,
            EnSurveillance,
            EnContention,
            EnArchive,
            Abandon,
            Transfert,
            Inconnue

        }

        public enum BasePracticeStateEnum
        {
            NonDefini = 0,
            BasePractice = 1,
            Orthalis = 2
        }



       
        private List<InvisalignEnBouche> _aligneurs = null;
         [JsonIgnore]
        public List<InvisalignEnBouche> aligneurs
        {
            get
            {
                return _aligneurs;
            }
            set
            {
                _aligneurs = value;
            }
        }
        


        private InfosInvisalign _infosinvisalign = null;
         [JsonIgnore]
        public InfosInvisalign infosinvisalign
        {
            get
            {
                return _infosinvisalign;
            }
            set
            {
                _infosinvisalign = value;
            }
        }

         private InfoSmilers _infoSmilers = null;
         [JsonIgnore]
         public InfoSmilers infoSmilers
         {
             get
             {
                 return _infoSmilers;
             }
             set
             {
                 _infoSmilers = value;
             }
         }

        private Assurance _assurance;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public Assurance assurance
        {
            get
            {
                return _assurance;
            }
            set
            {
                _assurance = value;
            }
        }

        private List<SuiviSpecialiste> _SuiviSpecialiste = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<SuiviSpecialiste> SuiviSpecialiste
        {
            get
            {
                return _SuiviSpecialiste;
            }
            set
            {
                _SuiviSpecialiste = value;
            }
        }

        private List<CustomStatusClinique> _Customstatus;
       [PropertyCanBeSerialized]
       [JsonIgnore]
         public List<CustomStatusClinique> Customstatus
        {
            get
            {
                return _Customstatus;
            }
            set
            {
                _Customstatus = value;
            }
        }
       

        private BasePracticeStateEnum _BasePracticeState = BasePracticeStateEnum.NonDefini;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public BasePracticeStateEnum BasePracticeState
        {
            get
            {
                return _BasePracticeState;
            }
            set
            {
                _BasePracticeState = value;
            }
        }



        private string _Titulaire;
        [PropertyCanBeSerialized]
        public string Titulaire
        {
            get
            {
                return _Titulaire;
            }
            set
            {
                _Titulaire = value;
            }
        }

        private string _NomBanque;
        [PropertyCanBeSerialized]
        public string NomBanque
        {
            get
            {
                return _NomBanque;
            }
            set
            {
                _NomBanque = value;
            }
        }


        private List<CommentHisto> _CommentsHisto;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<CommentHisto> CommentsHisto
        {
            get
            {
                return _CommentsHisto;
            }
            set
            {
                _CommentsHisto = value;
            }
        }

        private string _CodeBanque;
        [PropertyCanBeSerialized]
        public string CodeBanque
        {
            get
            {
                return _CodeBanque;
            }
            set
            {
                _CodeBanque = value;
            }
        }

        private string _CodeGuichet;
        [PropertyCanBeSerialized]
        public string CodeGuichet
        {
            get
            {
                return _CodeGuichet;
            }
            set
            {
                _CodeGuichet = value;
            }
        }

        private string _NumCompte;
        [PropertyCanBeSerialized]
        public string NumCompte
        {
            get
            {
                return _NumCompte;
            }
            set
            {
                _NumCompte = value;
            }
        }

        private string _CleRIB;
        [PropertyCanBeSerialized]
        public string CleRIB
        {
            get
            {
                return _CleRIB;
            }
            set
            {
                _CleRIB = value;
            }
        }

       public string RIB
        {
            get
            {
                string rib = CodeBanque + CodeGuichet + NumCompte + CleRIB;
                return rib;
            }
            
        }
     
        private bool _publication;
        public bool publication
        {
            get
            {
                return _publication;
            }
            set
            {
                _publication = value;
            }
        }

        private int _Idprofile;
        public int Idprofile
        {
            get
            {
                return _Idprofile;
            }
            set
            {
                _Idprofile = value;
            }
        }

        private string _password;
        public string password
        {
            get
            {
                if (_password == "") _password = Nom + Id.ToString();
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        private string _IOlogin;
        public string IOlogin
        {
            get
            {
                if (_IOlogin == "") _IOlogin = Nom;
                return _IOlogin;
            }
            set
            {
                _IOlogin = value;
            }
        }

        private Caisse _caisse;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public Caisse caisse
        {
            get
            {
                return _caisse;
            }
            set
            {
                _caisse = value;
            }
        }

        private Mutuelle _Mutuelle;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public Mutuelle Mutuelle
        {
            get
            {
                return _Mutuelle;
            }
            set
            {
                _Mutuelle = value;
            }
        }


        private List<FeuilleDeSoin> _FeuillesDeSoins = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<FeuilleDeSoin> FeuillesDeSoins
        {
            get
            {
                return _FeuillesDeSoins;
            }
            set
            {
                _FeuillesDeSoins = value;
            }
        }

        private List<EntentePrealable> _ententesPrealable = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<EntentePrealable> ententesPrealable
        {
            get
            {
                return _ententesPrealable;
            }
            set
            {
                _ententesPrealable = value;
            }
        }

        private List<Devis> _devis = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
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

        private List<Devis_TK> _devis_TK = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<Devis_TK> devis_TK
        {
            get
            {
                return _devis_TK;
            }
            set
            {
                _devis_TK = value;
            }
        }


        private BaseLaboratoire _baseLaboratoire = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public BaseLaboratoire baseLaboratoire
        {
            get
            {
                return _baseLaboratoire;
            }
            set
            {
                _baseLaboratoire = value;
            }
        }
        
        string _repertoire = "";
        public string Repertoire
        {
            get
            {
                 /* if ((string.IsNullOrEmpty(_repertoire)) || !Directory.Exists(RepertoireImage + "/" + _repertoire))
                  {
                      _repertoire = Nom.ToUpper() + " " + Prenom + " " + (DateNaissance).ToString("ddMMyyyy");
                      if (!Directory.Exists(Path.Combine(RepertoireImage, _repertoire)))
                      {
                          _repertoire = Nom.ToUpper() + " " + Prenom + " 00000000";
                          if (!Directory.Exists(RepertoireImage + "/" + _repertoire))
                          {
                              _repertoire = Nom.ToUpper() + " " + Prenom;
                              if (!Directory.Exists(RepertoireImage + "/"  + _repertoire))
                              {
                                  _repertoire = Nom.ToUpper() + " " + Prenom + " " + (DateNaissance).ToString("MMddyyyy");
                                  if (!Directory.Exists(RepertoireImage + "/"  + _repertoire))
                                  {
                                      _repertoire = Nom.ToUpper() + " " + Prenom+ " " + DateNaissance.ToString("ddMMyyyy");
                                      if (!Directory.Exists(RepertoireImage + "/" + _repertoire))
                                      {
                                          _repertoire = Nom.ToUpper() + " " + Prenom + " " + DateNaissance.ToString("MMddyyyy");

                                      }
                                  }
                              }
                          }
                      }
                 }*/
                _repertoire = this.Id.ToString();

                
                return _repertoire;
            }
            set
            {
                _repertoire = value;
            }
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
                prefix = Cabinets.Find(c => c.Id == idCabinet).prefix;
            }
        }
        private static List<Cabinet> _lstCabinet;
        public static List<Cabinet> Cabinets
        {
            get
            {
                if (_lstCabinet == null)
                    _lstCabinet = getAllCabinets();
                return _lstCabinet;
            }
            set
            {
                _lstCabinet = value;
            }
        }
        public static List<Cabinet> getAllCabinets()
        {
            List<Cabinet> lst = new List<BasCommon_BO.Cabinet>();
            string FILE_PATH = System.Configuration.ConfigurationManager.AppSettings["cabinets"];
            var xmlString = File.ReadAllText(FILE_PATH);
            var stringReader = new StringReader(xmlString);
            var dsSet = new DataSet();
            dsSet.ReadXml(stringReader);
            DataTable dt = dsSet.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Cabinet cab = new Cabinet();
                cab.Id = dr["Id"] is DBNull ? -1 : Convert.ToInt32(dr["Id"]);
                cab.nomCabinet = dr["nomcabinet"] is DBNull ? "" : Convert.ToString(dr["nomcabinet"]).Trim();
                cab.prefix = dr["prefix"] is DBNull ? "" : Convert.ToString(dr["prefix"]).Trim();
                lst.Add(cab);
            }

            return lst;
        }
        private static string _prefix;
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
        public static string RepertoireImage
        {
            get
            {
             
             return System.Configuration.ConfigurationManager.AppSettings["PHOTO_FOLDER_PATH" + prefix];

            }
        }




        private List<PaiementReel> _PaiementReels = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<PaiementReel> PaiementReels
        {
            get
            {
                return _PaiementReels;
            }
            set
            {
                _PaiementReels = value;
            }
        }

        private List<Encaissement> _Encaissements = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<Encaissement> Encaissements
        {
            get
            {
                return _Encaissements;
            }
            set
            {
                _Encaissements = value;
            }
        }


        private List<Echeance> _Echeances;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<Echeance> Echeances
        {
            get
            {
                return _Echeances;
            }
            set
            {
                _Echeances = value;
            }
        }

        private List<ActePG> _ActesPG = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<ActePG> ActesPG
        {
            get
            {
                return _ActesPG;
            }
            set
            {
                _ActesPG = value;
            }
        }

        public string ToShortString()
        {
            return Nom + " " + (Prenom.Length > 0 ? Prenom.Substring(0, 1) + "." : "");
        }

        public override string ToString()
        {
            return Nom + " " + Prenom;
        }

        public enum Sexe
        {
            Masculin,
            Feminin,
            NonDefini
        }


        private List<ObjImage> _lstObjFrmBasPhoto;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<ObjImage> lstObjFrmBasPhoto
        {
            get
            {
                return _lstObjFrmBasPhoto;
            }
            set
            {
                _lstObjFrmBasPhoto = value;
            }
        }

       public Relance.ModeRelance StatusRelancePatient
        {
            get
            {
                if (Echeances==null)
                    return Relance.ModeRelance.Undefined;

                Relance.ModeRelance current = Relance.ModeRelance.Aucun;

                foreach (Echeance ec in Echeances)
                {
                    if (ec.payeur != Echeance.typepayeur.patient) continue;
                    if ((ec.ID_Encaissement <= 0) && (ec.Relances.Contentieux!=null) && (current< Relance.ModeRelance.Contentieux))
                        current = Relance.ModeRelance.Contentieux;

                    if ((ec.ID_Encaissement <= 0) && (ec.Relances.Majoration != null) && (current < Relance.ModeRelance.PreContentieux2))
                        current = Relance.ModeRelance.PreContentieux2;

                    if ((ec.ID_Encaissement <= 0) && (ec.Relances.PreContentieux != null) && (current < Relance.ModeRelance.PreContentieux))
                        current = Relance.ModeRelance.PreContentieux;

                    if ((ec.ID_Encaissement <= 0) && (ec.Relances.Relance != null) && (current < Relance.ModeRelance.Relance1))
                        current = Relance.ModeRelance.Relance1;

                    if ((ec.ID_Encaissement <= 0) && (ec.Relances.ReleveDeCompte != null) && (current < Relance.ModeRelance.Releve))
                        current = Relance.ModeRelance.Releve;
                }
                return current;
               

            }
            
        }

       public Relance.ModeRelance StatusRelanceTier
       {
           get
           {
               if (Echeances == null)
                   return Relance.ModeRelance.Undefined;

               Relance.ModeRelance current = Relance.ModeRelance.Aucun;

               foreach (Echeance ec in Echeances)
               {
                   if ((ec.payeur != Echeance.typepayeur.Mutuelle)&&
                       (ec.payeur != Echeance.typepayeur.Secu)) continue;

                   if ((ec.ID_Encaissement <= 0) && (ec.Relances.Contentieux != null) && (current < Relance.ModeRelance.Contentieux))
                       current = Relance.ModeRelance.Contentieux;

                   if ((ec.ID_Encaissement <= 0) && (ec.Relances.Majoration != null) && (current < Relance.ModeRelance.PreContentieux2))
                       current = Relance.ModeRelance.PreContentieux2;

                   if ((ec.ID_Encaissement <= 0) && (ec.Relances.PreContentieux != null) && (current < Relance.ModeRelance.PreContentieux))
                       current = Relance.ModeRelance.PreContentieux;

                   if ((ec.ID_Encaissement <= 0) && (ec.Relances.Relance != null) && (current < Relance.ModeRelance.Relance1))
                       current = Relance.ModeRelance.Relance1;

                   if ((ec.ID_Encaissement <= 0) && (ec.Relances.ReleveDeCompte != null) && (current < Relance.ModeRelance.Releve))
                       current = Relance.ModeRelance.Releve;
               }
               return current;


           }

       }


        private List<ObjSuivi> _suivisBaseLabo = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<ObjSuivi> suivisBaseLabo
        {
            get
            {
                return _suivisBaseLabo;
            }
            set
            {
                _suivisBaseLabo = value;
            }
        }
        [JsonIgnore]
        private List<IElementDent> _ElementsEnBouche = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<IElementDent> ElementsEnBouche
        {
            get
            {
                return _ElementsEnBouche;
            }
            set
            {
                _ElementsEnBouche = value;
            }
        }

        private List<IElementAppareil> _AppareilsEnBouche = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<IElementAppareil> AppareilsEnBouche
        {
            get
            {
                return _AppareilsEnBouche;
            }
            set
            {
                _AppareilsEnBouche = value;
            }
        }


        private List<BasCommon_BO.RHAppointment> _appointements = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<BasCommon_BO.RHAppointment> appointements
        {
            get
            {
                return _appointements;
            }
            set
            {
                _appointements = value;
            }
        }

        private List<CommClinique> _commentairesClinique = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<CommClinique> commentairesClinique
        {
            get
            {
                return _commentairesClinique;
            }
            set
            {
                _commentairesClinique = value;
            }
        }

        private string _CasierInvisalign;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public string CasierInvisalign
        {
            get
            {
                return _CasierInvisalign;
            }
            set
            {
                _CasierInvisalign = value;
            }
        }

        private string _RefArchive;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public string RefArchive
        {
            get
            {
                return _RefArchive;
            }
            set
            {
                _RefArchive = value;
            }
        }

        
        

        private InfoPatientComplementaire _infoscomplementaire = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public InfoPatientComplementaire infoscomplementaire
        {
            get
            {
                return _infoscomplementaire;
            }
            set
            {
                _infoscomplementaire = value;
            }
        }


        private List<CommonObjectif> _SelectedObjectifs = new List<CommonObjectif>();
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<CommonObjectif> SelectedObjectifs
        {
            get
            {
                return _SelectedObjectifs;
            }
            set
            {
                _SelectedObjectifs = value;
            }
        }

        


        private List<LienCorrespondant> _PersonnesAContacter = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<LienCorrespondant> PersonnesAContacter
        {
            get
            {
                return _PersonnesAContacter;
            }
            set
            {
                _PersonnesAContacter = value;
            }
        }

        private string _ResumeQ1CS = null;
        [PropertyCanBeSerialized]
        public string ResumeQ1CS
        {
            get
            {
                return _ResumeQ1CS;
            }
            set
            {
                _ResumeQ1CS = value;
            }
        }



        private int? _PourcentageMutuelle;
        [PropertyCanBeSerialized]
        public int? PourcentageMutuelle
        {
            get
            {
                return _PourcentageMutuelle;
            }
            set
            {
                _PourcentageMutuelle = value;
            }
        }

        private string _numMoulage;
        [PropertyCanBeSerialized]
        public string numMoulage
        {
            get
            {
                return _numMoulage;
            }
            set
            {
                _numMoulage = value;
            }
        }

        public void AgeToDate(DateTime d2, out int years, out int months, out int days)
        {

            DateTime d1 = DateNaissance;

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


        private string _Allergie = "";
        [PropertyCanBeSerialized]
        public string Allergie
        {
            get
            {
                return _Allergie;
            }
            set
            {
                _Allergie = value;
            }
        }

       
        private int m_Dossier;
        private string m_Profession;

        private DateTime m_DateNaissance;



        private List<LienCorrespondant> _RecoBy;
        [JsonIgnore]
        public List<LienCorrespondant> RecoBy
        {
            get
            {
                if (m_Correspondants == null) return null;
                List<LienCorrespondant> lst = new List<LienCorrespondant>();
                foreach (LienCorrespondant lc in m_Correspondants)
                {
                    if (lc.TypeDeLien == "Rc")
                    {
                        lst.Add( lc);
                    }
                }
                return lst;
            }
        }

        private List<LienCorrespondant> m_Correspondants = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public List<LienCorrespondant> Correspondants
        {
            get { return m_Correspondants; }
            set { m_Correspondants = value; }
        }


        public LienCorrespondant Assurepar
        {
            get
            {
                foreach (LienCorrespondant lc in Correspondants)
                {
                    if (lc.IsAssure)
                        return lc;
                }
                return null;
            }

        }

        public LienCorrespondant Dentiste
        {
            get
            {
                foreach (LienCorrespondant lc in Correspondants)
                {
                    if (lc.IsDentiste)
                        return lc;
                }
                return null;
            }

        }




        public int mutuelle
        {
            get
            {
                foreach (LienCorrespondant lc in Correspondants)
                {
                    if (lc.IsMutuelle)
                        return lc.IdCorrespondance;
                }
                return -1;
            }

        }

        public LienCorrespondant ResponsableFinancier
        {
            get
            {
                if (Correspondants == null) return null;
                foreach (LienCorrespondant lc in Correspondants)
                {
                    if (lc.IsPayeur)
                        return lc;
                }
                return null;
            }

        }

        public LienCorrespondant RepresentantLegal
        {
            get
            {
                
                foreach (LienCorrespondant lc in Correspondants)
                {
                    if (lc.IsRepresentantLegal)
                        return lc;
                }
                return null;
            }

        }

        

        public LienCorrespondant RecomandePar
        {
            get
            {
                foreach (LienCorrespondant lc in Correspondants)
                {
                    if (lc.IsRecomande)
                        return lc;
                }
                return null;
            }

        }

        public int IdCaisse
        {
            get
            {
                foreach (LienCorrespondant lc in Correspondants)
                {
                    if (lc.IsCaisse)
                        return lc.IdCorrespondance;
                }
                return -1;
            }

        }
        
        private string _Traitement;
        [PropertyCanBeSerialized]
        public string Traitement
        {
            get
            {
                return _Traitement;
            }
            set
            {
                _Traitement = value;
            }
        }

        private string _Objectif;
        [PropertyCanBeSerialized]
        public string Objectif
        {
            get
            {
                return _Objectif;
            }
            set
            {
                _Objectif = value;
            }
        }


        private string _CommentApparreil;
        [PropertyCanBeSerialized]
        public string CommentApparreil
        {
            get
            {
                return _CommentApparreil;
            }
            set
            {
                _CommentApparreil = value;
            }
        }

        private string _Diagnostic;
        [PropertyCanBeSerialized]
        public string Diagnostic
        {
            get
            {
                return _Diagnostic;
            }
            set
            {
                _Diagnostic = value;
            }
        }

        private bool _Tutoiement;
        [PropertyCanBeSerialized]
        public bool Tutoiement
        {
            get
            {
                return _Tutoiement;
            }
            set
            {
                _Tutoiement = value;
            }
        }
        
        Sexe m_Genre;
        [PropertyCanBeSerialized]
        public Sexe Genre
        {
            get { return m_Genre; }
            set { m_Genre = value; }
        }

        string m_Civilite;
        [PropertyCanBeSerialized]
        public string Civilite
        {
            get { return m_Civilite; }
            set { m_Civilite = value; }
        }

        [PropertyCanBeSerialized]
        public string Profession
        {
            get { return m_Profession; }
            set { m_Profession = value; }
        }


        private ActeTraitement _ActeDefaut;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public ActeTraitement ActeDefaut
        {
            get
            {
                return _ActeDefaut;
            }
            set
            {
                _ActeDefaut = value;
            }
        }


        private int _Appareil;
        [PropertyCanBeSerialized]
        public int Appareil
        {
            get
            {
                return _Appareil;
            }
            set
            {
                _Appareil = value;
            }
        }

        private Recontact _recontact = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public Recontact recontact
        {
            get
            {
                return _recontact;
            }
            set
            {
                _recontact = value;
            }
        }

        private StatusCliniqueManuel _statusManuel;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public StatusCliniqueManuel statusManuel
        {
            get
            {
                return _statusManuel;
            }
            set
            {
                _statusManuel = value;
            }
        }

        private DateTime? _DateAbandon;
        [PropertyCanBeSerialized]
        public DateTime? DateAbandon
        {
            get
            {
                return _DateAbandon;
            }
            set
            {
                _DateAbandon = value;
            }
        }
        
        
        private StatusClinique _statusClinique;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public StatusClinique statusClinique
        {
            get
            {
                return _statusClinique;
            }
            set
            {
                _statusClinique = value;
            }
        }

        private string _Moulage;
        [PropertyCanBeSerialized]
        public string Moulage
        {
            get
            {
                return _Moulage;
            }
            set
            {
                _Moulage = value;
            }
        }





        private Correspondant.EnumPrefCom _PrefCom = Correspondant.EnumPrefCom.unknown;
        [PropertyCanBeSerialized]
        public Correspondant.EnumPrefCom PrefCom
        {
            get
            {
                return _PrefCom;
            }
            set
            {
                _PrefCom = value;
            }
        }




        private string _Notes;
        [PropertyCanBeSerialized]
        public string Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                _Notes = value;
            }
        }


        [PropertyCanBeSerialized]
        public int Dossier
        {
            get { return m_Dossier; }
            set { m_Dossier = value; }
        }

        private string _NomJF;
        [PropertyCanBeSerialized]
        public string NomJF
        {
            get
            {
                return _NomJF;
            }
            set
            {
                _NomJF = value;
            }
        }

       

        [PropertyCanBeSerialized]
        public DateTime DateNaissance
        {
            get { return m_DateNaissance; }
            set { m_DateNaissance = value; }
        }

        private string m_NumSecu;
        [PropertyCanBeSerialized]
        public string NumSecu
        {
            get { return m_NumSecu; }
            set { m_NumSecu = value; }
        }
        
        public string Age()
        {
            return AgeAt(DateTime.Now);
        }

        public string AgeAt(DateTime dte)
        {
            int years, months, days;
            DateDiff(dte, this.DateNaissance, out years, out months, out days);

            string r = years.ToString() + " ans";
            if (months > 0) r += " et " + months.ToString() + " mois";
            return r;
        }


        private static int GetDaysInMonth(int year, int month)
        {
            // this is also available from Calendar class,
            // but just as easy to do ourselves

            if (month < 1 || month > 12)
            {
                throw new ArgumentException("month value must be from 1-12");
            }

            // 1 2 3 4 5 6 7 8 9 10 11 12
            int[] days = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            if (((year / 400 * 400) == year) ||
            (((year / 4 * 4) == year) && (year % 100 != 0)))
            {
                days[2] = 29;
            }

            return days[month];
        }


        private static void DateDiff(DateTime d1, DateTime d2,
        out int years, out int months, out int days)
        {
            // compute & return the difference of two dates,
            // returning years, months & days
            // d1 should be the larger (newest) of the two dates
            //
            //
            // y m d
            // 3/10/2005 <-- 3/10/2005 0 0 0
            // 3/10/2005 <-- 3/09/2005 0 0 1
            // 3/10/2005 <-- 3/01/2005 0 0 9
            // 3/10/2005 <-- 2/28/2005 0 0 10
            // 3/10/2005 <-- 2/11/2005 0 0 27
            // 3/10/2005 <-- 2/10/2005 0 1 0
            // 3/10/2005 <-- 2/09/2005 0 1 1
            // 3/10/2005 <-- 7/20/1969 35 7 21

            // we want d1 to be the larger (newest) date
            // flip if we need to

            if (d1 < d2)
            {
                DateTime d3 = d2;
                d2 = d1;
                d1 = d3;
            }

            // compute difference in total months
            months = 12 * (d1.Year - d2.Year) + (d1.Month - d2.Month);

            // based upon the 'days',
            // adjust months & compute actual days difference
            if (d1.Day < d2.Day)
            {
                months--;
                days = GetDaysInMonth(d2.Year, d2.Month) - d2.Day + d1.Day;
            }
            else
            {
                days = d1.Day - d2.Day;
            }

            // compute years & actual months
            years = months / 12;
            months -= years * 12;

            //Debug.WriteLine(string.Format("{0} <-- {1} {2,2} {3,2} {4,2}",d1.ToShortDateString(),d2.ToShortDateString(),year s,months,days));
        }

        public static bool IsLeapYear(int Year)
        {
            return (((Year & 3) == 0) && ((Year % 100 != 0) || (Year % 400 == 0)));
        }  

        public int AgeNbYears
        {
            get
            {
                // Age théorique
                int age = DateTime.Now.Year - m_DateNaissance.Year;

                DateTime DateAnniv;
                if (IsLeapYear(m_DateNaissance.Year) && m_DateNaissance.Month == 2 && m_DateNaissance.Day==29)
                    DateAnniv = new DateTime(DateTime.Now.Year, 2, 28);
                else
                    DateAnniv = new DateTime(DateTime.Now.Year, m_DateNaissance.Month, m_DateNaissance.Day);


                // Si pas encore passé, retirer 1 an
                if (DateAnniv > DateTime.Now)
                    age--;

                return age;

            }
        }

        public basePatient()
        { }

        private List<string> _Risques = null;
        [PropertyCanBeSerialized]
        public List<string> Risques
        {
            get
            {
                return _Risques;
            }
            set
            {
                _Risques = value;
            }
        }


        #region Images


        private bool _ImagesHasBeenLoaded;
        public bool ImagesHasBeenLoaded
        {
            get
            {
                return _ImagesHasBeenLoaded;
            }
            
        }

        private string _Img_Rad_Face;
        [PropertyCanBeSerialized]
        public string Img_Rad_Face
        {
            get
            {
                return _Img_Rad_Face;
            }
            set
            {
                _Img_Rad_Face = value;
                _ImagesHasBeenLoaded = true;
            }
        }


        private string _Img_Rad_Pano;
        [PropertyCanBeSerialized]
        public string Img_Rad_Pano
        {
            get
            {
                return _Img_Rad_Pano;
            }
            set
            {
                _Img_Rad_Pano = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Rad_Profile;
        [PropertyCanBeSerialized]
        public string Img_Rad_Profile
        {
            get
            {
                return _Img_Rad_Profile;
            }
            set
            {
                _Img_Rad_Profile = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Ext_Face;
        [PropertyCanBeSerialized]
        public string Img_Ext_Face
        {
            get
            {
                return _Img_Ext_Face;
            }
            set
            {
                _Img_Ext_Face = value;
                _ImagesHasBeenLoaded = true;
            }
        }
        private string _Img_Innoclusion;
        [PropertyCanBeSerialized]
        public string Img_Innoclusion
        {
            get
            {
                return _Img_Innoclusion;
            }
            set
            {
                _Img_Innoclusion = value;
                _ImagesHasBeenLoaded = true;
            }
        }
        private string _ConsentementSigne;
        [PropertyCanBeSerialized]
        public string ConsentementSigne
        {
            get
            {
                return _ConsentementSigne;
            }
            set
            {
                _ConsentementSigne = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _DevisSigne;
        [PropertyCanBeSerialized]
        public string DevisSigne
        {
            get
            {
                return _DevisSigne;
            }
            set
            {
                _DevisSigne = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _QuestionnaireMedical;
        [PropertyCanBeSerialized]
        public string QuestionnaireMedical
        {
            get
            {
                return _QuestionnaireMedical;
            }
            set
            {
                _QuestionnaireMedical = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Ext_Profile;
        [PropertyCanBeSerialized]
        public string Img_Ext_Profile
        {
            get
            {
                return _Img_Ext_Profile;
            }
            set
            {
                _Img_Ext_Profile = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Ext_Profile_Sourire;
        [PropertyCanBeSerialized]
        public string Img_Ext_Profile_Sourire
        {
            get
            {
                return _Img_Ext_Profile_Sourire;
            }
            set
            {
                _Img_Ext_Profile_Sourire = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Ext_Face_Sourire;
        [PropertyCanBeSerialized]
        public string Img_Ext_Face_Sourire
        {
            get
            {
                return _Img_Ext_Face_Sourire;
            }
            set
            {
                _Img_Ext_Face_Sourire = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Ext_Sourire;
        [PropertyCanBeSerialized]
        public string Img_Ext_Sourire
        {
            get
            {
                return _Img_Ext_Sourire;
            }
            set
            {
                _Img_Ext_Sourire = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Int_Droit;
        [PropertyCanBeSerialized]
        public string Img_Int_Droit
        {
            get
            {
                return _Img_Int_Droit;
            }
            set
            {
                _Img_Int_Droit = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Int_SurPlomb;
        [PropertyCanBeSerialized]
        public string Img_Int_SurPlomb
        {
            get
            {
                return _Img_Int_SurPlomb;
            }
            set
            {
                _Img_Int_SurPlomb = value;
                _ImagesHasBeenLoaded = true;
            }
        }



        private string _Img_Int_Gauche;
        [PropertyCanBeSerialized]
        public string Img_Int_Gauche
        {
            get
            {
                return _Img_Int_Gauche;
            }
            set
            {
                _Img_Int_Gauche = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Int_Face;
        [PropertyCanBeSerialized]
        public string Img_Int_Face
        {
            get
            {
                return _Img_Int_Face;
            }
            set
            {
                _Img_Int_Face = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _MImg_Int_Gauche;
        [PropertyCanBeSerialized]
        public string MImg_Int_Gauche
        {
            get
            {
                return _MImg_Int_Gauche;
            }
            set
            {
                _MImg_Int_Gauche = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Int_Max;
        [PropertyCanBeSerialized]
        public string Img_Int_Max
        {
            get
            {
                return _Img_Int_Max;
            }
            set
            {
                _Img_Int_Max = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Int_Man;
        [PropertyCanBeSerialized]
        public string Img_Int_Man
        {
            get
            {
                return _Img_Int_Man;
            }
            set
            {
                _Img_Int_Man = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Moul_Droit;
        [PropertyCanBeSerialized]
        public string Img_Moul_Droit
        {
            get
            {
                return _Img_Moul_Droit;
            }
            set
            {
                _Img_Moul_Droit = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Moul_Face;
        [PropertyCanBeSerialized]
        public string Img_Moul_Face
        {
            get
            {
                return _Img_Moul_Face;
            }
            set
            {
                _Img_Moul_Face = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Moul_Gauche;
        [PropertyCanBeSerialized]
        public string Img_Moul_Gauche
        {
            get
            {
                return _Img_Moul_Gauche;
            }
            set
            {
                _Img_Moul_Gauche = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Moul_Max;
        [PropertyCanBeSerialized]
        public string Img_Moul_Max
        {
            get
            {
                return _Img_Moul_Max;
            }
            set
            {
                _Img_Moul_Max = value;
                _ImagesHasBeenLoaded = true;
            }
        }

        private string _Img_Moul_Man;
        [PropertyCanBeSerialized]
        public string Img_Moul_Man
        {
            get
            {
                return _Img_Moul_Man;
            }
            set
            {
                _Img_Moul_Man = value;
                _ImagesHasBeenLoaded = true;
            }
        }
        private string _Img_Rad_Supp;
        [PropertyCanBeSerialized]
        public string Img_Rad_Supp
        {
            get
            {
                return _Img_Rad_Supp;
            }
            set
            {
                _Img_Rad_Supp = value;
                _ImagesHasBeenLoaded = true;
            }
        }
        //Images

         
        //
        #endregion

         

          

        private RHAppointment _NextRDV;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public RHAppointment NextRDV
        {
            get
            {

                if (appointements != null)
                {
                    RHAppointment NextApp = null;
                    foreach (RHAppointment app in appointements)
                    {
                        if ((app.StartDate > DateTime.Now) && ((NextApp == null) || (app.StartDate < NextApp.StartDate)))
                            NextApp = app;
                    }
                    return NextApp;

                }
                else
                    return _NextRDV;

                
            }
            set
            {
                _NextRDV = value;
            }
        }

     

        private RHAppointment _LastRDV = null;
        [PropertyCanBeSerialized]
        [JsonIgnore]
        public RHAppointment LastRDV
        {
            get
            {
                
                if (appointements != null)
                {
                    RHAppointment LastApp = null;
                    foreach (RHAppointment app in appointements)
                    {
                        if ((app.StartDate < DateTime.Now) && ((LastApp==null)||(app.StartDate > LastApp.StartDate)))
                            LastApp = app;
                    }
                    return LastApp;

                }else
                    return _LastRDV;
            }
            set
            {
                _LastRDV = value;
            }
        }


        private List<Proposition> _propositions;
        [PropertyCanBeSerialized]
        [JsonIgnore]
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
        public enum Enumregelement
        {
            Aucun =0,
            Acte=1,
            Forfait=2,
            Semestre=3,
            Mensuel = 4
        }

        private Enumregelement _regelement;
        [PropertyCanBeSerialized]
        public Enumregelement regelement
        {
            get
            {
                return _regelement;
            }
            set
            {
                _regelement = value;
            }
        }
    }
}
