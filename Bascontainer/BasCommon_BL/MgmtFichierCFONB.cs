using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;
using BasCommon_BO;
using System.IO;

namespace BasCommon_BL
{
    public static class MgmtCFONBFiles
    {

        private static string FillBlanks(string strToFill, int nbToFill, char caractToFill, bool fillToRight)
        {
            string strFilled = "";

            if (strToFill.Length <= nbToFill)
                for (int i = strToFill.Length; i < nbToFill; i++)
                    strFilled += caractToFill;
            else
            {
                for (int i = 0; i < nbToFill; i++)
                    strFilled += strToFill[i].ToString();
                return strFilled;
            }

            if (fillToRight)
                return strToFill += strFilled;
            else
                return strFilled += strToFill;
        }

        private static string GiveSpaces(int nbSpace)
        {
            string str = "";
            for (int i = 0; i < nbSpace; i++)
                str += " ";

            return str;
        }



        public static List<string> GenerateBankFile(List<Prelevement> lst,string RefPrelevement)
        {

              string str = "";
                 string l = "";
                 string s = "";
                 StreamWriter sw;




            try
            {

                Dictionary<BanqueDeRemise, string> dicofiles = new Dictionary<BanqueDeRemise, string>();


                foreach (Prelevement p in lst)
                {
                    if (!dicofiles.ContainsKey(p.comptecabinet))
                    {
                        string tmpfile = Path.GetTempFileName() + "_" + p.comptecabinet.Libelle + ".txt";
                        dicofiles.Add(p.comptecabinet, tmpfile);
                    }
                }




                foreach (KeyValuePair<BanqueDeRemise, string> kv in dicofiles)
                {


                    //Création du dossier qui contiendra les fichiers
                    //de banque


                    /*
                    string path = "BankFiles";

                    try
                    {
                        if (!Directory.Exists(path))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(path);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    string tmpfile = "BankFiles\\" + p.comptecabinet.NumCPT + ".txt";
                     * */
                    sw = new StreamWriter(kv.Value,false,Encoding.ASCII);
                    double somme = 0;

                    #region Première ligne

                    //Infos émetteur (cabinet)
                    str = "";

                    str += "0308";
                    str += GiveSpaces(8); //Espace (8)
                    str += kv.Key.NumNNE; //Numero émetteur (6)
                    str += GiveSpaces(7); //Espace (7)
                    string year = DateTime.Now.Year.ToString();
                    str += new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString("ddMM") + year[3].ToString(); //Date Valeur (5)
                    str += FillBlanks(kv.Key.Titulaire.ToUpper(), 24, ' ', true); //Raison sociale (24)

                    str += FillBlanks(RefPrelevement,11,' ',true); //Ref Virement (11)

                    str += GiveSpaces(15); //Espace (15)
                    str += "E"; //En euros (1)
                    str += GiveSpaces(5); //Espace (5)
                    str += kv.Key.NumGui; //Numero guichet émetteur (5)
                    str += kv.Key.NumCPT; //Numero compte (11)
                    str += GiveSpaces(47); //Espace (47)
                    str += FillBlanks(kv.Key.NumA.Trim(), 5, ' ', true); //Numéro établissement (5)
                    str += GiveSpaces(6); //Espace (6)

                    str += "\r\n";

                    sw.Write(str);

                    #endregion

                    #region Lignes prélèvements

                    foreach (Prelevement pre in lst)
                    {
                        if (pre.comptecabinet == kv.Key)
                        {
                            //Infos destinataire (patient)
                            l = "";

                            l += "0608";
                            l += GiveSpaces(8); //Espace (8)
                            l += kv.Key.NumNNE; //Numero émetteur (6)

                            l += FillBlanks(pre.echeance.Id.ToString() ,12,' ', true ); //Référence ligne virement (12)

                            //TODO Use Titulaire

                            string titulaire = pre.echeance.patient.Titulaire != "" ? pre.echeance.patient.Titulaire.ToUpper() : pre.echeance.patient.Nom.ToUpper() + " " + pre.echeance.patient.Prenom.ToUpper();

                            l += FillBlanks(titulaire, 24, ' ', true); //Nom destinataire (24)
                            l += FillBlanks(pre.echeance.patient.NomBanque.ToUpper(), 20, ' ', true); //Nom banque destinaire (20)
                            l += GiveSpaces(12); //Espace (12)
                            l += pre.echeance.patient.CodeGuichet; //Numero guichet destinataire (5)
                            l += pre.echeance.patient.NumCompte; //Numero compte destinataire (11)

                            l += FillBlanks((pre.Montant * 100).ToString("00"), 16, '0', false); //Montant en centimes (16)
                            somme = somme + (pre.Montant * 100);
                            l += FillBlanks(pre.Libelle, 31, ' ', true); //Libellé (31)
                            l += pre.echeance.patient.CodeBanque; //Numéro établissement dest (5)
                            l += GiveSpaces(6); //Espace (6)

                            l += "\r\n";

                            sw.Write(l);
                        }
                    }

                    #endregion

                    #region Avant dernière ligne

                    //Somme des opérations (pour chaque ligne du fichier)
                    s = "";

                    s += "0808"; //Prélèvement
                    s += GiveSpaces(8); //Espace (8)
                    s += kv.Key.NumNNE; //Numéro émetteur
                    s += GiveSpaces(84); //Espace (84)
                    s += FillBlanks(somme.ToString("00"), 16, '0', false); //Somme des opérations (16)
                    s += GiveSpaces(42); //Espace (42)

                    s += "\r\n";

                    sw.Write(s);

                    #endregion

                    //Dernière ligne (ligne vide)
                    //sw.WriteLine("");
                    sw.Close();

                }

                List<string> files = new List<string>();
                foreach (KeyValuePair<BanqueDeRemise, string> kv in dicofiles)
                    files.Add(kv.Value);
                return files;
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }

        public static List<string> GenerateBankFile(List<Echeance> lst, string refPrelevement)
        {

            List<Prelevement> lstP = new List<Prelevement>();

            foreach (Echeance e in lst)
            {

                if (e.patient == null)
                    e.patient = baseMgmtPatient.GetPatient(e.IdPatient);
                if (e.patient.infoscomplementaire == null)
                    e.patient.infoscomplementaire = baseMgmtPatient.getinfocomplementaire(e.IdPatient);
                if (e.patient.infoscomplementaire.PraticienResponsable.EntiteJuridique.ComptesBancaire == null)
                    e.patient.infoscomplementaire.PraticienResponsable.EntiteJuridique.ComptesBancaire = BanqueMgmt.getBanquesDeRemise(e.patient.infoscomplementaire.PraticienResponsable.EntiteJuridique);

                if (e.DateEcheance == null)
                    continue;


                Prelevement p = new Prelevement();
                p.Libelle = e.Libelle;
                p.echeance = e;

                p.Entite = p.patient.infoscomplementaire.PraticienResponsable.EntiteJuridique;

                p.comptecabinet = p.Entite.ComptesBancaire[0];


                lstP.Add(p);
            }

            return GenerateBankFile(lstP,refPrelevement);
        }

        
    }
}
