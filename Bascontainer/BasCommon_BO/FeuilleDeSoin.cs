﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class FeuilleDeSoin
    {

        public enum TypeEnvois
        {
            Papier = 0,
            Electronic = 1
        }

        public double TotalMontantNonSoumisAEntente
        {
            get
            {
                double Total = 0;
                foreach (ActePG afs in actes)
                {


                    if (!afs.NeedDEP)
                    {
                        Total += afs.Montant_Honoraire;


                    }


                }
                return Total;
            }

        }

        public double TotalMontantSoumisAEntente
        {
            get
            {
                double Total = 0;
                foreach (ActePG afs in actes)
                {


                    if (afs.NeedDEP)
                    {
                        Total += afs.Montant_Honoraire;


                    }


                }
                return Total;
            }

        }


        public string TotalMontantSoumisAEntenteToutelettre
        {
            get
            {
                string monnaie = System.Configuration.ConfigurationManager.AppSettings["monnaie"];
                string centime = System.Configuration.ConfigurationManager.AppSettings["centime"];
                return converti(TotalMontantSoumisAEntente, string.IsNullOrEmpty(monnaie) ? "euros" : monnaie, string.IsNullOrEmpty(centime) ? "centimes" : centime);
            }

        }

        public string TotalMontantNonSoumisAEntenteToutelettre
        {
            get
            {
                string monnaie = System.Configuration.ConfigurationManager.AppSettings["monnaie"];
                string centime = System.Configuration.ConfigurationManager.AppSettings["centime"];
                return converti(this.TotalMontantNonSoumisAEntente, string.IsNullOrEmpty(monnaie) ? "euros" : monnaie, string.IsNullOrEmpty(centime) ? "centimes" : centime);
            }

        }

        public string CodeCoeffTotalSEP
        {
            get
            {

                Dictionary<string, double> dico = new Dictionary<string, double>();
                foreach (ActePG afs in actes)
                {


                    if (afs.NeedDEP)
                    {

                        if (dico.ContainsKey(afs.prestation.Code))
                            dico[afs.prestation.Code] = dico[afs.prestation.Code] + afs.Template.Coeff;
                        else
                            dico.Add(afs.prestation.Code, afs.Template.Coeff);

                    }
                }

                string totalCoeff = "";
                foreach (KeyValuePair<string, double> kv in dico)
                {
                    totalCoeff += totalCoeff == "" ? "" : " ";
                    totalCoeff += kv.Key + ((int)kv.Value).ToString();
                }
                return totalCoeff;
            }
        }

        public string CodeCoeffTotalNSEP
        {
            get
            {

                Dictionary<string, double> dico = new Dictionary<string, double>();
                foreach (ActePG afs in actes)
                {


                    if (!afs.NeedDEP)
                    {

                        if (dico.ContainsKey(afs.prestation.Code))
                            dico[afs.prestation.Code] = dico[afs.prestation.Code] + afs.Template.Coeff;
                        else
                            dico.Add(afs.prestation.Code, afs.Template.Coeff);

                    }


                }

                string totalCoeff = "";
                foreach (KeyValuePair<string, double> kv in dico)
                {
                    totalCoeff += totalCoeff == "" ? "" : " ";
                    totalCoeff += kv.Key + ((int)kv.Value).ToString();
                }
                return totalCoeff;
            }

        }


        private List<ActePG> _actes = new List<ActePG>();
        [PropertyCanBeSerialized]
        public List<ActePG> actes
        {
            get
            {
                return _actes;
            }
            set
            {
                _actes = value;
            }
        }

        private DateTime _dateEdition;
        [PropertyCanBeSerialized]
        public DateTime dateEdition
        {
            get
            {
                return _dateEdition;
            }
            set
            {
                _dateEdition = value;
            }
        }

        private TypeEnvois _typedenvois = TypeEnvois.Papier;
        [PropertyCanBeSerialized]
        public TypeEnvois typedenvois
        {
            get
            {
                return _typedenvois;
            }
            set
            {
                _typedenvois = value;
            }
        }


        private DateTime _datePaiementHonoraire;
        [PropertyCanBeSerialized]
        public DateTime datePaiementHonoraire
        {
            get
            {
                return _datePaiementHonoraire;
            }
            set
            {
                _datePaiementHonoraire = value;
            }
        }


        private basePatient _patient;
        [PropertyCanBeSerialized]
        public basePatient patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;

            }
        }




        public double Montant
        {
            get
            {
                return TotalMontantNonSoumisAEntente + TotalMontantSoumisAEntente;
            }

        }

        private int _Id = -1;
        [PropertyCanBeSerialized]
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }


        #region Feuille De Soin Papier

        #region Recto
        private string _AgremmentRadiation = "";
        [PropertyCanBeSerialized]
        public string AgremmentRadiation
        {
            get
            {
                return _AgremmentRadiation;
            }
            set
            {
                _AgremmentRadiation = value;
            }
        }

        private string _AgrementTeleradio = "";
        [PropertyCanBeSerialized]
        public string AgrementTeleradio
        {
            get
            {
                return _AgrementTeleradio;
            }
            set
            {
                _AgrementTeleradio = value;
            }
        }

        private string _AgrementPanoramique = "";
        [PropertyCanBeSerialized]
        public string AgrementPanoramique
        {
            get
            {
                return _AgrementPanoramique;
            }
            set
            {
                _AgrementPanoramique = value;
            }
        }
        #endregion


        #region Verso
        #region Assure

        public string NomPrenom
        {
            get
            {
                return patient.Nom + " " + patient.Prenom;
            }

        }

        #region Administratif de l'assure

        public string AdresseAssure
        {
            get
            {
                if (patient.Assurepar != null)
                {
                    return patient.Assurepar.correspondant.Adresse1 + "\n" + patient.Assurepar.correspondant.Adresse2 + "\n" + patient.Assurepar.correspondant.CodePostal + " " + patient.Assurepar.correspondant.Ville;
                }
                else
                    return patient.Adresse1 + "\n" + patient.Adresse2 + "\n" + patient.CodePostal + " " + patient.Ville;
            }
        }


        public string NomPrenomAssure
        {
            get
            {
                if (patient.Assurepar != null)
                    return patient.Assurepar.correspondant.Nom + " " + patient.Assurepar.correspondant.Prenom;
                else
                    return patient.Nom + " " + patient.Prenom;
            }
        }

        public DateTime? DatenaissanceAssure
        {
            get
            {
                if (patient.Assurepar != null)
                    return patient.Assurepar.correspondant.DateNaissance;
                else
                    return patient.DateNaissance;
            }

        }

        public string ImmatAssure
        {
            get
            {
                if ((patient.Assurepar != null)&&(patient.Assurepar.correspondant!=null))
                {
                    return patient.Assurepar.correspondant.numSecu;
                }
                else
                    return patient.NumSecu;
            }

        }

        #endregion

        #region Situation de l'assure

        private string _LibAutreCas;
        [PropertyCanBeSerialized]
        public string LibAutreCas
        {
            get
            {
                return _LibAutreCas;
            }
            set
            {
                _LibAutreCas = value;
            }
        }

        private bool _AutreCas;
        [PropertyCanBeSerialized]
        public bool AutreCas
        {
            get
            {
                return _AutreCas;
            }
            set
            {
                _AutreCas = value;
            }
        }

        private bool _PensionAssure;
        [PropertyCanBeSerialized]
        public bool PensionAssure
        {
            get
            {
                return _PensionAssure;
            }
            set
            {
                _PensionAssure = value;
            }
        }

        private bool _PensionPatient;
        [PropertyCanBeSerialized]
        public bool PensionPatient
        {
            get
            {
                return _PensionPatient;
            }
            set
            {
                _PensionPatient = value;
            }
        }

        private DateTime? _DateDeCessationActivite;
        [PropertyCanBeSerialized]
        public DateTime? DateDeCessationActivite
        {
            get
            {
                return _DateDeCessationActivite;
            }
            set
            {
                _DateDeCessationActivite = value;
            }
        }

        private bool _SansEmplois;
        [PropertyCanBeSerialized]
        public bool SansEmplois
        {
            get
            {
                return _SansEmplois;
            }
            set
            {
                _SansEmplois = value;
            }
        }

        private bool _NonSalarie;
        [PropertyCanBeSerialized]
        public bool NonSalarie
        {
            get
            {
                return _NonSalarie;
            }
            set
            {
                _NonSalarie = value;
            }
        }

        private bool _Salarie;
        [PropertyCanBeSerialized]
        public bool Salarie
        {
            get
            {
                return _Salarie;
            }
            set
            {
                _Salarie = value;
            }
        }
        #endregion

        #endregion

        #region Patient

        #region Patient
        private string _Profession;
        [PropertyCanBeSerialized]
        public string Profession
        {
            get
            {
                return _Profession;
            }
            set
            {
                _Profession = value;
            }
        }

        private DateTime? _DateAccident;
        [PropertyCanBeSerialized]
        public DateTime? DateAccident
        {
            get
            {
                return _DateAccident;
            }
            set
            {
                _DateAccident = value;
            }
        }

        private bool _Accident;
        [PropertyCanBeSerialized]
        public bool Accident
        {
            get
            {
                return _Accident;
            }
            set
            {
                _Accident = value;
            }
        }

        private bool _SoinPourPensionne;
        [PropertyCanBeSerialized]
        public bool SoinPourPensionne
        {
            get
            {
                return _SoinPourPensionne;
            }
            set
            {
                _SoinPourPensionne = value;
            }
        }

        #endregion

        #region Si Patient <> Assure
        public string NomPatient
        {
            get
            {
                return patient.Nom;
            }
            set
            {
                patient.Nom = value;
            }
        }

        public string PrenomPatient
        {
            get
            {
                return patient.Prenom;
            }
            set
            {
                patient.Prenom = value;
            }
        }

        public DateTime DateNaissancePatient
        {
            get
            {
                return patient.DateNaissance;
            }
            set
            {
                patient.DateNaissance = value;
            }
        }

        private bool _Pensionne;
        [PropertyCanBeSerialized]
        public bool Pensionne
        {
            get
            {
                return _Pensionne;
            }
            set
            {
                _Pensionne = value;
            }
        }

        private string _AdressePatient;
        [PropertyCanBeSerialized]
        public string AdressePatient
        {
            get
            {
                return _AdressePatient;
            }
            set
            {
                _AdressePatient = value;
            }
        }
        #endregion

        #endregion

        #endregion


        private string converti(double chiffre, string symbolmonnaie, string symbolmonnaiecent)
        {
            int centaine = 0;
            int dizaine = 0;
            int unite = 0;
            int reste = 0;
            int y = 0;
            bool dix = false;
            bool soixanteDix = false;
            string lettre = "";
            reste = (int)Math.Floor(chiffre) / 1;
            int i = 1000000000;
            while (i >= 1)
            {
                y = reste / i;
                if (!(y == 0))
                {
                    centaine = y / 100;
                    dizaine = (y - centaine * 100) / 10;
                    unite = y - (centaine * 100) - (dizaine * 10);
                    switch (centaine)
                    {
                        case 0:
                            break;
                        // break
                        case 1:
                            lettre += "cent ";
                            break;
                        // break
                        case 2:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "deux cents ";
                            }
                            else
                            {
                                lettre += "deux cent ";
                            }
                            break;
                        // break
                        case 3:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "trois cents ";
                            }
                            else
                            {
                                lettre += "trois cent ";
                            }
                            break;
                        // break
                        case 4:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "quatre cents ";
                            }
                            else
                            {
                                lettre += "quatre cent ";
                            }
                            break;
                        // break
                        case 5:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "cinq cents ";
                            }
                            else
                            {
                                lettre += "cinq cent ";
                            }
                            break;
                        // break
                        case 6:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "six cents ";
                            }
                            else
                            {
                                lettre += "six cent ";
                            }
                            break;
                        // break
                        case 7:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "sept cents ";
                            }
                            else
                            {
                                lettre += "sept cent ";
                            }
                            break;
                        // break
                        case 8:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "huit cents ";
                            }
                            else
                            {
                                lettre += "huit cent ";
                            }
                            break;
                        // break
                        case 9:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "neuf cents ";
                            }
                            else
                            {
                                lettre += "neuf cent ";
                            }
                            break;
                        // break
                    }
                    switch (dizaine)
                    {
                        case 0:
                            break;
                        // break
                        case 1:
                            dix = true;
                            break;
                        // break
                        case 2:
                            lettre += "vingt ";
                            break;
                        // break
                        case 3:
                            lettre += "trente ";
                            break;
                        // break
                        case 4:
                            lettre += "quarante ";
                            break;
                        // break
                        case 5:
                            lettre += "cinquante ";
                            break;
                        // break
                        case 6:
                            lettre += "soixante ";
                            break;
                        // break
                        case 7:
                            dix = true;
                            soixanteDix = true;
                            lettre += "soixante ";
                            break;
                        // break
                        case 8:
                            lettre += "quatre-vingt ";
                            break;
                        // break
                        case 9:
                            dix = true;
                            lettre += "quatre-vingt ";
                            break;
                        // break
                    }
                    switch (unite)
                    {
                        case 0:
                            if (dix)
                            {
                                lettre += "dix ";
                            }
                            break;
                        // break
                        case 1:
                            if (soixanteDix)
                            {
                                lettre += "et onze ";
                            }
                            else
                            {
                                if (dix)
                                {
                                    lettre += "onze ";
                                }
                                else
                                {
                                    if ((!(dizaine == 1) && !(dizaine == 0)))
                                    {
                                        lettre += "et un ";
                                    }
                                    else
                                    {
                                        lettre += "un ";
                                    }
                                }
                            }
                            break;
                        // break
                        case 2:
                            if (dix)
                            {
                                lettre += "douze ";
                            }
                            else
                            {
                                lettre += "deux ";
                            }
                            break;
                        // break
                        case 3:
                            if (dix)
                            {
                                lettre += "treize ";
                            }
                            else
                            {
                                lettre += "trois ";
                            }
                            break;
                        // break
                        case 4:
                            if (dix)
                            {
                                lettre += "quatorze ";
                            }
                            else
                            {
                                lettre += "quatre ";
                            }
                            break;
                        // break
                        case 5:
                            if (dix)
                            {
                                lettre += "quinze ";
                            }
                            else
                            {
                                lettre += "cinq ";
                            }
                            break;
                        // break
                        case 6:
                            if (dix)
                            {
                                lettre += "seize ";
                            }
                            else
                            {
                                lettre += "six ";
                            }
                            break;
                        // break
                        case 7:
                            if (dix)
                            {
                                lettre += "dix-sept ";
                            }
                            else
                            {
                                lettre += "sept ";
                            }
                            break;
                        // break
                        case 8:
                            if (dix)
                            {
                                lettre += "dix-huit ";
                            }
                            else
                            {
                                lettre += "huit ";
                            }
                            break;
                        // break
                        case 9:
                            if (dix)
                            {
                                lettre += "dix-neuf ";
                            }
                            else
                            {
                                lettre += "neuf ";
                            }
                            break;
                        // break
                    }
                    switch (i)
                    {
                        case 1000000000:
                            if (y > 1)
                            {
                                lettre += "milliards ";
                            }
                            else
                            {
                                lettre += "milliard ";
                            }
                            break;
                        // break
                        case 1000000:
                            if (y > 1)
                            {
                                lettre += "millions ";
                            }
                            else
                            {
                                lettre += "million ";
                            }
                            break;
                        // break
                        case 1000:
                            lettre += "mille ";
                            break;
                        // break
                    }
                }
                reste -= y * i;
                dix = false;
                soixanteDix = false;
                i /= 1000;
            }
            if (lettre.Length == 0)
            {
                lettre += "zero ";
            }
            decimal chiffre3 = default(decimal);
            chiffre3 = Convert.ToDecimal((chiffre * 100)) % 100;
            Console.WriteLine(chiffre3);
            dizaine = Convert.ToInt32((chiffre3)) / 10;
            unite = Convert.ToInt32(chiffre3) - (dizaine * 10);
            string lettre2 = "";
            switch (dizaine)
            {
                case 0:
                    break;
                // break
                case 1:
                    dix = true;
                    break;
                // break
                case 2:
                    lettre2 += "vingt ";
                    break;
                // break
                case 3:
                    lettre2 += "trente ";
                    break;
                // break
                case 4:
                    lettre2 += "quarante ";
                    break;
                // break
                case 5:
                    lettre2 += "cinquante ";
                    break;
                // break
                case 6:
                    lettre2 += "soixante ";
                    break;
                // break
                case 7:
                    dix = true;
                    soixanteDix = true;
                    lettre2 += "soixante ";
                    break;
                // break
                case 8:
                    lettre2 += "quatre-vingt ";
                    break;
                // break
                case 9:
                    dix = true;
                    lettre2 += "quatre-vingt ";
                    break;
                // break
            }
            switch (unite)
            {
                case 0:
                    if (dix)
                    {
                        lettre2 += "dix ";
                    }
                    break;
                // break
                case 1:
                    if (soixanteDix)
                    {
                        lettre2 += "et onze ";
                    }
                    else
                    {
                        if (dix)
                        {
                            lettre2 += "onze ";
                        }
                        else
                        {
                            if ((!(dizaine == 1) && !(dizaine == 0)))
                            {
                                lettre2 += "et un ";
                            }
                            else
                            {
                                lettre2 += "un ";
                            }
                        }
                    }
                    break;
                // break
                case 2:
                    if (dix)
                    {
                        lettre2 += "douze ";
                    }
                    else
                    {
                        lettre2 += "deux ";
                    }
                    break;
                // break
                case 3:
                    if (dix)
                    {
                        lettre2 += "treize ";
                    }
                    else
                    {
                        lettre2 += "trois ";
                    }
                    break;
                // break
                case 4:
                    if (dix)
                    {
                        lettre2 += "quatorze ";
                    }
                    else
                    {
                        lettre2 += "quatre ";
                    }
                    break;
                // break
                case 5:
                    if (dix)
                    {
                        lettre2 += "quinze ";
                    }
                    else
                    {
                        lettre2 += "cinq ";
                    }
                    break;
                // break
                case 6:
                    if (dix)
                    {
                        lettre2 += "seize ";
                    }
                    else
                    {
                        lettre2 += "six ";
                    }
                    break;
                // break
                case 7:
                    if (dix)
                    {
                        lettre2 += "dix-sept ";
                    }
                    else
                    {
                        lettre2 += "sept ";
                    }
                    break;
                // break
                case 8:
                    if (dix)
                    {
                        lettre2 += "dix-huit ";
                    }
                    else
                    {
                        lettre2 += "huit ";
                    }
                    break;
                // break
                case 9:
                    if (dix)
                    {
                        lettre2 += "dix-neuf ";
                    }
                    else
                    {
                        lettre2 += "neuf ";
                    }
                    break;
                // break
            }
            if (lettre.StartsWith("un mille"))
            {
                lettre = lettre.Remove(0, 3);
            }
            if (lettre2.Equals(""))
            {
                return lettre + symbolmonnaie;
            }
            else
            {
                if (dizaine.Equals(0) && unite.Equals(1))
                {
                    return lettre + symbolmonnaie + " et " + lettre2 + symbolmonnaiecent;
                }
                else
                {
                    return lettre + symbolmonnaie + " et " + lettre2 + symbolmonnaiecent;
                }
            }
        }

        #endregion
    }
}
