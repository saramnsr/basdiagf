using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
{

    public class Patient
    {


        private List<Contact> _contacts = null;
        public List<Contact> contacts
        {
            get
            {
                return _contacts;
            }
            set
            {
                _contacts = value;
            }
        }

              

        private Contact _MainAdresse = null;
        public Contact MainAdresse
        {
            get
            {
                return _MainAdresse;
            }
            set
            {
                _MainAdresse = value;
            }
        }

        private Contact _MainMail = null;
        public Contact MainMail
        {
            get
            {
                return _MainMail;
            }
            set
            {
                _MainMail = value;
            }
        }
        

        public string ToShortString()
        {
            return Nom + " " + Prenom.Substring(0, 1) + ".";
        }

        private List<LienCorrespondant> _PersonnesAContacter = new List<LienCorrespondant>();
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


        private List<Proposition> _propositions = null;
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


        private List<string> _Risques = new List<string>();
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

        private List<Appareil> _SelectedAppareils = new List<Appareil>();
        public List<Appareil> SelectedAppareils
        {
            get
            {
                return _SelectedAppareils;
            }
            set
            {
                _SelectedAppareils = value;
            }
        }

        private List<CommonObjectif> _SelectedObjectifs = new List<CommonObjectif>();
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

        private InfoPatientComplementaire _infoscomplementaire = new InfoPatientComplementaire();
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

        public Correspondant Assurepar
        {
            get
            {
                foreach (LienCorrespondant lc in Correspondants)
                {
                    if (lc.TypeDeLien == "As") 
                        return lc.correspondant;
                }
                return null;
            }

        }

        public Correspondant Dentiste
        {
            get
            {
                foreach (LienCorrespondant lc in Correspondants)
                {
                    if (lc.TypeDeLien == "De")
                        return lc.correspondant;
                }
                return null;
            }

        }

        public List<ObjImage> lstObjFrmKitView = new List<ObjImage>();

        public override string ToString()
        {
            return this.Nom + " " + this.Prenom;
        }

        #region Images
        //Images
        public string Img_Rad_Face = "";
        public string Img_Rad_Pano = "";
        public string Img_Rad_Profile = "";

        public string Img_Ext_Face = "";
        public string Img_Ext_Profile = "";

        public string Img_Ext_Profile_Sourire = "";
        public string Img_Ext_Face_Sourire = "";
        public string Img_Ext_Sourire = "";

        public string Img_Int_Droit = "";
        public string Img_Int_SurPlomb = "";
        public string Img_Int_Face = "";
        public string Img_Int_Gauche = "";
        public string Img_Int_Max = "";
        public string Img_Int_Man = "";

        public string Img_Moul_Droit = "";
        public string Img_Moul_Face = "";
        public string Img_Moul_Gauche = "";
        public string Img_Moul_Max = "";
        public string Img_Moul_Man = "";
        //
        #endregion


        private string _Sexe;
        public string Sexe
        {
            get
            {
                return _Sexe;
            }
            set
            {
                _Sexe = value;
            }
        }

        private double  _Solde;
        public double  Solde
        {
            get
            {
                return _Solde;
            }
            set
            {
                _Solde = value;
            }
        }

        private DateTime? _NextRDV;
        public DateTime? NextRDV
        {
            get
            {
                return _NextRDV;
            }
            set
            {
                _NextRDV = value;
            }
        }

        private DateTime? _LastRDV;
        public DateTime? LastRDV
        {
            get
            {
                return _LastRDV;
            }
            set
            {
                _LastRDV = value;
            }
        }

        private bool _TuToiement;
        public bool TuToiement
        {
            get
            {
                return _TuToiement;
            }
            set
            {
                _TuToiement = value;
            }
        }


        private int m_Id;
        private string m_Nom;
        private string m_NomJF;
        private string m_Prenom;
        private int m_Dossier;
        private string m_Civilite;

        
        private string m_Profession;

        private DateTime m_DateNaissance;


        public static string RepertoireImage
        {
            get
            {
                return  System.Configuration.ConfigurationManager.AppSettings["PHOTO_FOLDER_PATH"];
            }
        }

        string _repertoire = "";
        public string Repertoire
        {
            get
            {
                if (!System.IO.Directory.Exists(RepertoireImage + "\\" + _repertoire))
                {
                    _repertoire = Nom.ToUpper() + " " + Prenom.ToUpper() + " " + (DateNaissance).ToString("ddMMyyyy");
                    if (!System.IO.Directory.Exists(RepertoireImage + "\\" + _repertoire))
                    {
                        _repertoire = Nom.ToUpper() + " " + Prenom.ToUpper() + " 00000000";
                        if (!System.IO.Directory.Exists(RepertoireImage + "\\" + _repertoire))
                        {
                            _repertoire = Nom.ToUpper() + " " + Prenom.ToUpper();
                            if (!System.IO.Directory.Exists(RepertoireImage + "\\" + _repertoire))
                            {
                                _repertoire = Nom.ToUpper() + " " + Prenom.ToUpper() + " " + (DateNaissance).ToString("MMddyyyy");
                                if (!System.IO.Directory.Exists(RepertoireImage + "\\" + _repertoire))
                                {
                                    _repertoire = Nom.ToUpper() + " " + Prenom.ToUpper() + " " + DateNaissance.ToString("ddMMyyyy");
                                    if (!System.IO.Directory.Exists(RepertoireImage + "\\" + _repertoire))
                                    {
                                        _repertoire = Nom.ToUpper() + " " + Prenom.ToUpper() + " " + DateNaissance.ToString("MMddyyyy");

                                    }
                                }
                            }
                        }
                    }
                }
                return _repertoire;
            }
            set
            {
                _repertoire = value;
            }
        }


        private List<LienCorrespondant> m_Correspondants = new List<LienCorrespondant>();

        private List<LienCorrespondant> m_RecoBy = new List<LienCorrespondant>();

        public List<LienCorrespondant> Correspondants
        {
            get { return m_Correspondants; }
            set { m_Correspondants = value; }
        }

        public List<LienCorrespondant> RecoBy
        {
            get { return m_RecoBy; }
            set { m_RecoBy = value; }
        }


        private string _ResumeQ1CS;
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


        public string Profession
        {
            get { return m_Profession; }
            set { m_Profession = value; }
        }



        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

       

        private bool _Tutoiement;
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

       
     
        
        private string _numSecu;
        public string numSecu
        {
            get
            {
                return _numSecu;
            }
            set
            {
                _numSecu = value;
            }
        }

       
        public int Dossier
        {
            get { return m_Dossier; }
            set { m_Dossier = value; }
        }

        public string Nom
        {
            get { return m_Nom; }
            set { m_Nom = value; }
        }

        public string NomJF
        {
            get { return m_NomJF; }
            set { m_NomJF = value; }
        }

        public string Prenom
        {
            get { return m_Prenom; }
            set { m_Prenom = value; }
        }

        public string Civilite
        {
            get { return m_Civilite; }
            set { m_Civilite = value; }
        }



        public DateTime DateNaissance
        {
            get { return m_DateNaissance; }
            set { m_DateNaissance = value; }
        }

        public TimeSpan Age
        {
            get
            {
                TimeSpan tp = new TimeSpan();
                tp = DateTime.Now.Subtract(m_DateNaissance);
                return tp;
            }
        }

        public int AgeNbYears
        {
            get
            {
                // Age théorique
                int age = DateTime.Now.Year - m_DateNaissance.Year;


                // Date de l'anniversaire de cette année
                DateTime DateAnniv =
                      new DateTime(DateTime.Now.Year, m_DateNaissance.Month, m_DateNaissance.Day);


                // Si pas encore passé, retirer 1 an
                if (DateAnniv > DateTime.Now)
                    age--;

                return age;

            }
        }


        public void AgeToDate( DateTime d2, out int years, out int months, out int days)
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
        

        public Patient()
        { }





    }

}
