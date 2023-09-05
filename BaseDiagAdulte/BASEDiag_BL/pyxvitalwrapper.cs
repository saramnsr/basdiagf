using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Xml;
using System.Globalization;

namespace PyxVitalInterface
{
    public static class CarteCPS
    {
        public static Dictionary<string, string> InfoCabinet = null;
        public static List<Dictionary<string, string>> InfoPraticiens = null;
    }

    public static class CarteVital
    {
        public static Dictionary<string, string> Beneficiaire = null;
        public static Dictionary<string, string> Assure = null;
        public static Dictionary<string, string> Medico_administratif = null;
        public static Dictionary<string, string> Remboursement = null;
        public static Dictionary<string, string> AT = null;
        public static List<Dictionary<string, string>> Beneficiaires = null;
    }

    public static class PyxVitalWrapper
    {


        #region Parametrage

        public static bool MotifEDSiDepassement = true;
        public static bool tierspayantObligatoire = false;
        public static bool tierspayantComplementaire = false;
        #endregion


        //0 = Pas de reponse dans les temps
        //4 = Réponse favorable
        //5 = Urgence
        //? = Refus

        public enum CodeAccordPrestation
        {
            Ac0 = 0,
            Ac4 = 1,
            Ac5 = 2
        }

        //valeurs possibles
        // Néant par défaut
        // 11 pour soins en rapport avec une prestation exonérante
        // 21 pour soins en rapport avec K ou KC >=50
        // 13 pour soins particuliers exonérés
        // 15 pour Assuré ou bénéficiaire exonéré, mode dégradé IRIS seulement
        // 9  pour FNS, mode dégradé IRIS seulement
        public enum Exoneration
        {
            ExNéant = 0,
            Ex11 = 1,
            Ex21 = 2,
            Ex13 = 3,
            Ex15 = 4,
            Ex9 = 5,

        }

        // Code Retour compris dans
        //· O
        //· N
        public enum Domicile
        {
            N = 0,
            O = 1
        }

        // Code Retour compris dans
        //· O
        //· N
        public enum DEP
        {
            N = 0,
            O = 1
        }

        // Code Retour compris dans
        //· O
        //· N
        public enum Nuit
        {
            N = 0,
            O = 1
        }

        // Code Retour compris dans
        //· O
        //· N
        public enum DimancheEtJF
        {
            N = 0,
            O = 1
        }

        // Code Retour compris dans
        //· O
        //· N
        public enum ALD
        {
            N = 0,
            O = 1
        }

        // Code Retour compris dans
        //· O
        //· N
        public enum Accident
        {
            N = 0,
            O = 1
        }



        // Code Retour compris dans
        //· Néant
        //· R
        //· HR
        public enum RMO
        {
            Néant = 0,
            R = 1,
            HR = 2,
        }
        // Qualification de depense avec Code Retour compris dans
        //· Néant
        //· D (pour Entente directe)
        //· E (pour Exigence particulière)
        //· F (pour Dépassement pour déplacement non prescrit)
        //· G (pour Gratuit)
        //· N (pour Non remboursable)

        public enum Qualificatif_depense
        {
            Néant = 0,
            D = 1,
            E = 2,
            F = 3,
            G = 4,
            N = 5

        }

        #region section Prestation
        const string Section_Prest = "Prestation";
        const string Cle_Date = "Date";
        const string Cle_Code = "Code";
        const string Cle_Coefficient = "Coefficient";
        const string Cle_Montant_honoraires = "Montant_honoraires";
        const string Cle_Qualificatif_depense = "Qualificatif_depense";
        const string Cle_Domicile = "Domicile";
        const string Cle_Quantite = "Quantité";
        const string Cle_Acte_multiple = "Acte_multiple";
        const string Cle_RMO = "RMO";
        const string Cle_Numero_dent = "Numéro_dent";
        const string Cle_Exoneration = "Exonération";
        const string Cle_Appareil = "Appareil";
        const string Cle_ALD = "ALD";
        const string Cle_Denombrement = "Denombrement";
        const string Cle_Accident = "Accident";
        const string Cle_D_JF = "D_JF";
        const string Cle_NUIT = "NUIT";
        const string Cle_DEP = "DEP";
        const string Cle_DATE_DEP = "DATE_DEP";
        const string Cle_Code_accord_DEP = "Code_accord_DEP";

        const string Cle_CodeCCAM = "Code_CCAM";
        const string Cle_Modificateurs_CCAM = "Modificateurs_CCAM";
        const string Cle_Code_suppl_ccam = "Code_suppl_ccam";


        #endregion

        #region section Assurance

        const string Section_Assur = "Assurance";
        const string Cle_Date_Accident = "Date";
        #endregion

        #region section Remboursement
        const string Section_Rembo = "Remboursement";
        const string Cle_Tiers_payant_partie_obligatoire = "Tiers_payant_partie_obligatoire";
        const string Cle_Tiers_payant_partie_complementaire = "Tiers_payant_partie_complémentaire";
        const string Cle_Mutuelle = "Mutuelle";
        const string Cle_CMU = "CMU";
        #endregion

        #region section Prescription
        const string Section_Prescript = "Prescription";
        const string Cle_Date_Prescript = "Date";
        #endregion

        public const int WM_USER = 0x400;

        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_MAX = 11;


        #region Répertoires et fichiers PyxVital
        private static string _RepertoirePyxVital = "C:\\pyxdemo148\\";
        public static string RepertoirePyxVital
        {
            get
            {
                return _RepertoirePyxVital;
            }
            set
            {
                _RepertoirePyxVital = value;
            }
        }
        public static string RepertoirePyxVitalInterface
        {
            get
            {
                return _RepertoirePyxVital + "\\INTERF\\";
            }
        }
        public static string FichierPyxVitalStop
        {
            get
            {
                return RepertoirePyxVitalInterface + "Stop";
            }
        }
        public static string FichierPyxVitalActes
        {
            get
            {
                return RepertoirePyxVitalInterface + "Actes.par";
            }
        }
        public static string FichierPyxVitalAMC
        {
            get
            {
                return RepertoirePyxVitalInterface + "AMC.par";
            }
        }
        public static string FichierPyxVitalPatient
        {
            get
            {
                return RepertoirePyxVitalInterface + "Patient.par";
            }
        }
        public static string FichierPyxVitalPraticien
        {
            get
            {
                return RepertoirePyxVitalInterface + "Praticien.par";
            }
        }
        public static string FichierPyxVitalPatientXML
        {
            get
            {
                return RepertoirePyxVitalInterface + "Patient.xml";
            }
        }
        public static string FichierPyxVitalPraticienXML
        {
            get
            {
                return RepertoirePyxVitalInterface + "Praticien.xml";
            }
        }
        public static string FichierPyxVitalExe
        {
            get
            {
                return _RepertoirePyxVital + "Pyxvital.exe";
            }
        }
        public static string FicParam
        {
            get
            {
                return RepertoirePyxVitalInterface + "Param.par";
            }
        }
        public static string FicCV
        {
            get
            {
                return RepertoirePyxVitalInterface + "CV.par";
            }
        }

        #endregion



        #region private Functions



        static Dictionary<string, string> LoadNodeToDico(XmlNode nde)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();

            foreach (XmlNode ndechild in nde.ChildNodes)
            {
                results.Add(ndechild.Name, ndechild.InnerText);
            }

            return results;
        }


        #region interaction PyxVital
        [DllImport("Pyxinterf.dll")]
        public static extern
        int SSV_Command(IntPtr hWnd, int iWM, bool modal, int cmdShow, string szCommand, IntPtr szResult, uint sizeofResult);


        public static string Command(IntPtr HandleRetour, string commande)
        {
            IntPtr szResult = System.Runtime.InteropServices.Marshal.AllocHGlobal(256);
            int res = PyxVitalWrapper.SSV_Command(HandleRetour, PyxVitalWrapper.WM_USER, true, SW_RESTORE, commande, szResult, (uint)256);
            string resultat = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(szResult);
            if ((resultat != "") && (resultat[0] == 'E'))
            {
                throw new Exception("Erreur Pyxvital dans la commande : " + commande + "(" + resultat.Substring(1) + ")");
            }
            else
                return resultat;
        }


        #endregion

        #endregion


        #region Actions Vital






        public static string LireCPS(IntPtr Handle)
        {
            CarteCPS.InfoPraticiens = new List<Dictionary<string, string>>();
            string ret = Command(Handle, "P");
            if (ret[0] != 'E')
            {
                XmlTextReader lecturexml = new XmlTextReader(FichierPyxVitalPraticienXML);
                XmlDocument doc = new XmlDocument();
                doc.Load(lecturexml);

                XmlNode noeud = doc.DocumentElement;
                if (noeud.Name != "Praticien.par")
                    throw new Exception("Fichier xml incorrect");
                foreach (XmlNode nd in noeud.ChildNodes)
                {
                    switch (nd.Name)
                    {
                        case "PS":
                            CarteCPS.InfoCabinet = LoadNodeToDico(nd);
                            break;
                        case "PSs":
                            foreach (XmlNode ndechild in nd.ChildNodes)
                            {
                                Dictionary<string, string> tmp = LoadNodeToDico(ndechild);
                                CarteCPS.InfoPraticiens.Add(tmp);
                            }
                            break;
                        default:
                            throw new Exception("Fichier xml incorrect");
                    }
                }




            }

            return ret;
        }

        public static string LireCarteVital(IntPtr Handle)
        {
            string ret = Command(Handle, "V");

            if (ret[0] != 'E')
            {
                XmlTextReader lecturexml = new XmlTextReader(FichierPyxVitalPatientXML);
                XmlDocument doc = new XmlDocument();
                doc.Load(lecturexml);

                XmlNode noeud = doc.DocumentElement;
                if (noeud.Name != "Patient.par")
                    throw new Exception("Fichier xml incorrect");
                foreach (XmlNode nd in noeud.ChildNodes)
                {
                    switch (nd.Name)
                    {
                        case "Bénéficiaire":
                            CarteVital.Beneficiaire = LoadNodeToDico(nd);
                            break;
                        case "Assuré":
                            CarteVital.Assure = LoadNodeToDico(nd);
                            break;
                        case "Médico_administratif":
                            CarteVital.Medico_administratif = LoadNodeToDico(nd);
                            break;
                        case "Remboursement":
                            CarteVital.Remboursement = LoadNodeToDico(nd);
                            break;
                        case "AT":
                            CarteVital.AT = LoadNodeToDico(nd);
                            break;
                        case "Bénéficiaires":
                            CarteVital.Beneficiaires = new List<Dictionary<string, string>>();
                            foreach (XmlNode ndechild in nd.ChildNodes)
                            {
                                Dictionary<string, string> tmp = LoadNodeToDico(ndechild);
                                CarteVital.Beneficiaires.Add(tmp);
                            }
                            break;
                        default:
                            throw new Exception("Fichier xml incorrect");
                    }
                }




            }

            return ret;
        }

        public static string FormatageLot(IntPtr Handle)
        {
            return Command(Handle, "L");
        }

        public static string FormatageFichier(IntPtr Handle)
        {
            return Command(Handle, "F");
        }

        public static string HistoriqueLot(IntPtr Handle)
        {
            return Command(Handle, "I");
        }

        public static string TeleTransmettre(IntPtr Handle)
        {
            return Command(Handle, "2");
        }

        public static string TraduireARL(IntPtr Handle)
        {
            return Command(Handle, "T");
        }

        public static string FermerPyxVital(IntPtr Handle)
        {
            return Command(Handle, "Q");
        }

        public static string VidageNoemie(IntPtr Handle)
        {
            return Command(Handle, "N");
        }

        public static string ChoixBeneficiaire(IntPtr Handle)
        {
            return Command(Handle, "W");
        }

        public static string FormattageFSE(IntPtr Handle)
        {
            return Command(Handle, "=");
        }

        public static string EmissionTeletransmission(IntPtr Handle)
        {
            return Command(Handle, "S");
        }

        public static string ReceptionTeletransmission(IntPtr Handle)
        {
            return Command(Handle, "R");
        }



        public static string SaisieActe(IntPtr Handle)
        {
            return Command(Handle, "~");
        }

        public static string ChargerActesFromFile(IntPtr Handle)
        {
            return Command(Handle, "A");
        }


        public static void AjoutActeToFic(
            bool ClearFileBeforeAdd,
            string CodeSecu,
            double MontantCodeSecu,
            double Coeff_acte)
        {
            AjoutActeToFic(ClearFileBeforeAdd, false, DateTime.Now, CodeSecu, MontantCodeSecu, Coeff_acte, (MontantCodeSecu * Coeff_acte), PyxVitalWrapper.Qualificatif_depense.Néant, PyxVitalWrapper.RMO.Néant, PyxVitalWrapper.ALD.N, PyxVitalWrapper.Exoneration.ExNéant, PyxVitalWrapper.Accident.N, DateTime.Now, "", PyxVitalWrapper.Domicile.N, PyxVitalWrapper.DimancheEtJF.N, PyxVitalWrapper.Nuit.N, "", PyxVitalWrapper.DEP.N, DateTime.Now, PyxVitalWrapper.CodeAccordPrestation.Ac0);

        }

        public static void AjoutActeToFic(
            bool ClearFileBeforeAdd,
            string CodeSecu,
            double MontantCodeSecu,
            double Coeff_acte,
            double Montant_honoraires)
        {
            AjoutActeToFic(ClearFileBeforeAdd, false, DateTime.Now, CodeSecu, MontantCodeSecu, Coeff_acte, Montant_honoraires, PyxVitalWrapper.Qualificatif_depense.Néant, PyxVitalWrapper.RMO.Néant, PyxVitalWrapper.ALD.N, PyxVitalWrapper.Exoneration.ExNéant, PyxVitalWrapper.Accident.N, DateTime.Now, "", PyxVitalWrapper.Domicile.N, PyxVitalWrapper.DimancheEtJF.N, PyxVitalWrapper.Nuit.N, "", PyxVitalWrapper.DEP.N, DateTime.Now, PyxVitalWrapper.CodeAccordPrestation.Ac0);

        }

        public static void AjoutActeToFic(
            bool ClearFileBeforeAdd,
            bool IsCMU,
            DateTime dateActe,
            string CodeSecu,
            double MontantCodeSecu,
            double Coeff_acte,
            double Montant_honoraires,
            Qualificatif_depense QualifDepensePrestation,
            RMO RmoPrestation,
            ALD AldPrestation,
            Exoneration exoneration,
            Accident accident,
            DateTime DateAccident,
            string mutuelle,
            Domicile domicile,
            DimancheEtJF jourferie,
            Nuit nuit,
            string dent,
            DEP dep,
            DateTime DateDEP,
            CodeAccordPrestation accord)
        {

            NumberFormatInfo n = CultureInfo.GetCultureInfo("en-US").NumberFormat;

            IniFile parfile = new IniFile(FichierPyxVitalActes);

            String TotalDate = "";
            String TotalCodeSecu = "";
            String TotalCoeff_acte = "";
            String TotalMontantHonoraire = "";
            String TotalQte = "";
            string TotalQualificatif_depense = "";
            string TotalCodeCCAM = "";
            string TotalModificateurCCAM = "";
            string TotalCodeSupplCCAM = "";
            string TotalActe_multiple = "";

            if (!ClearFileBeforeAdd)
            {
                TotalDate = parfile.GetValue(Section_Prest, Cle_Date);
                TotalCodeSecu = parfile.GetValue(Section_Prest, Cle_Code);
                TotalCoeff_acte = parfile.GetValue(Section_Prest, Cle_Coefficient);
                TotalMontantHonoraire = parfile.GetValue(Section_Prest, Cle_Montant_honoraires);

                TotalQte = parfile.GetValue(Section_Prest, Cle_Quantite);
                TotalQualificatif_depense = parfile.GetValue(Section_Prest, Cle_Qualificatif_depense);
                TotalCodeCCAM = parfile.GetValue(Section_Prest, Cle_CodeCCAM);
                TotalModificateurCCAM = parfile.GetValue(Section_Prest, Cle_Modificateurs_CCAM);
                TotalCodeSupplCCAM = parfile.GetValue(Section_Prest, Cle_Code_suppl_ccam);
                TotalActe_multiple = parfile.GetValue(Section_Prest, Cle_Acte_multiple);

            }

            TotalActe_multiple += TotalActe_multiple == "" ? "" : "+";
            TotalCodeSecu += TotalCodeSecu == "" ? "" : "+";
            TotalCoeff_acte += TotalCoeff_acte == "" ? "" : "+";
            TotalMontantHonoraire += TotalMontantHonoraire == "" ? "" : "+";
            TotalQte += TotalQte == "" ? "" : "+";
            TotalQualificatif_depense += TotalQualificatif_depense == "" ? "" : "+";
            TotalCodeCCAM += TotalCodeCCAM == "" ? "" : "+";
            TotalModificateurCCAM += TotalModificateurCCAM == "" ? "" : "+";
            TotalCodeSupplCCAM += TotalCodeSupplCCAM == "" ? "" : "+";

            //Date de l'acte
            TotalDate += TotalDate == "" ? "" : "+";
            TotalDate += dateActe.ToString("dd/MM/yy");
            TotalActe_multiple += "0";

            double depassement = Montant_honoraires - (Coeff_acte * MontantCodeSecu);


            if ((depassement > 0.01f) && (CodeSecu.ToUpper() == "TO"))
            {

                //Acte TO avec Depassement
                TotalCodeSecu += CodeSecu + "+FDO";
                TotalCoeff_acte += Coeff_acte + "+1";
                TotalMontantHonoraire += MontantCodeSecu.ToString("F2", n) + "+" + depassement.ToString("F2", n);
            }
            else
            {
                //Acte hors TO "avec Depassement"
                TotalCodeSecu += CodeSecu;
                TotalCoeff_acte += Coeff_acte;
                TotalMontantHonoraire += (MontantCodeSecu * Coeff_acte).ToString("F2", n);

            }
            TotalQte = TotalQte += "1";


            parfile.SetValue(Section_Prest, Cle_Date, TotalDate);
            parfile.SetValue(Section_Prest, Cle_Code, TotalCodeSecu);
            parfile.SetValue(Section_Prest, Cle_Coefficient, TotalCoeff_acte);

            TotalCodeCCAM += "Néant";
            TotalModificateurCCAM += "Néant";
            TotalCodeSupplCCAM += "Néant";

            parfile.SetValue(Section_Prest, Cle_CodeCCAM, TotalCodeCCAM);
            parfile.SetValue(Section_Prest, Cle_Modificateurs_CCAM, TotalModificateurCCAM);
            parfile.SetValue(Section_Prest, Cle_Code_suppl_ccam, TotalCodeSupplCCAM);



            parfile.SetValue(Section_Prest, Cle_Montant_honoraires, TotalMontantHonoraire);

            parfile.SetValue(Section_Prest, Cle_Quantite, TotalQte);

            parfile.SetValue(Section_Prest, Cle_Acte_multiple, TotalActe_multiple);


            if (IsCMU)
            {
                if (CodeSecu.ToUpper() == "TO")
                {
                    TotalQualificatif_depense += Qualificatif_depense.Néant.ToString() + "+" + Qualificatif_depense.N.ToString();
                }
                else
                {
                    TotalQualificatif_depense += QualifDepensePrestation.ToString();
                }
            }
            else
            {
                Qualificatif_depense value = QualifDepensePrestation;

                if ((CodeSecu.ToUpper().Contains("Z")) && ((CodeSecu.ToUpper().Contains("TO")) || (CodeSecu.ToUpper().Contains("CS")))) value = Qualificatif_depense.E;
                if ((TotalCoeff_acte != "") && (CodeSecu.ToUpper() == "TO") && (Coeff_acte == 45)) value = Qualificatif_depense.D;

                if ((MotifEDSiDepassement) && (depassement > 0.01f))
                    if (CodeSecu.ToUpper() == "CS") value = Qualificatif_depense.E; else value = Qualificatif_depense.D;

                TotalQualificatif_depense += value;

            }

            parfile.SetValue(Section_Prest, Cle_Qualificatif_depense, TotalQualificatif_depense);


            parfile.SetValue(Section_Prest, Cle_RMO, RmoPrestation.ToString());
            parfile.SetValue(Section_Prest, Cle_ALD, AldPrestation.ToString());
            parfile.SetValue(Section_Prest, Cle_Exoneration, exoneration.ToString().Substring(2));
            parfile.SetValue(Section_Prest, Cle_Accident, accident.ToString());

            if (accident == Accident.O)
                parfile.SetValue(Section_Assur, Cle_Date_Accident, DateAccident.ToString("dd/MM/yy"));

            parfile.SetValue(Section_Prest, Cle_Domicile, domicile.ToString());
            parfile.SetValue(Section_Prest, Cle_D_JF, jourferie.ToString());
            parfile.SetValue(Section_Prest, Cle_NUIT, nuit.ToString());
            parfile.SetValue(Section_Prest, Cle_Numero_dent, dent.ToString());
            parfile.SetValue(Section_Prest, Cle_Denombrement, "0");
            parfile.SetValue(Section_Prest, Cle_DEP, dep.ToString());
            parfile.SetValue(Section_Prest, Cle_DATE_DEP, DateDEP.ToString("dd/MM/yy"));
            parfile.SetValue(Section_Prest, Cle_Code_accord_DEP, accord.ToString().Substring(2));
            parfile.SetValue(Section_Prest, Cle_Appareil, "EN COURS");


            if (IsCMU)
            {
                parfile.SetValue(Section_Rembo, Cle_Tiers_payant_partie_obligatoire, "O");
                parfile.SetValue(Section_Rembo, Cle_Tiers_payant_partie_complementaire, "O");
                parfile.SetValue(Section_Rembo, Cle_CMU, "O");

            }
            else
            {
                string tpO = tierspayantObligatoire ? "O" : "N";
                string tpC = tierspayantComplementaire ? "O" : "N";
                parfile.SetValue(Section_Rembo, Cle_Tiers_payant_partie_obligatoire, tpO);
                parfile.SetValue(Section_Rembo, Cle_Tiers_payant_partie_complementaire, tpC);
                parfile.SetValue(Section_Rembo, Cle_CMU, "N");
            }

            if (IsCMU)
            {
                if (mutuelle == "")
                    parfile.SetValue(Section_Rembo, Cle_Mutuelle, "99999997");
                else
                    parfile.SetValue(Section_Rembo, Cle_Mutuelle, mutuelle);
            }

            parfile.Save();
        }




        public static string HistoriqueFSE(IntPtr Handle)
        {
            return Command(Handle, "H");
        }

        public static void DemarragePyxVital()
        {
            Process notePad = new Process();

            notePad.StartInfo.FileName = FichierPyxVitalExe;

            notePad.Start();
        }

        #endregion
    }
}
