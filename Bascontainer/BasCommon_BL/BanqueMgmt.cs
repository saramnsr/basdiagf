using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using BasCommon_DAL;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class BanqueMgmt
    {

        private static List<Banque> _Banques = null;
        public static List<Banque> Banques
        {
            get
            {
                if (_Banques == null) _Banques = getBanques();
                return _Banques;
            }
            set
            {
                _Banques = value;
            }
        }

        private static List<BanqueDeRemise> _BanquesDeRemise = null;
        public static List<BanqueDeRemise> BanquesDeRemise
        {
            get
            {
                if (_BanquesDeRemise == null) _BanquesDeRemise = getBanquesDeRemise();
                return _BanquesDeRemise;
            }
            set
            {
                _BanquesDeRemise = value;
            }
        }


        private static List<BanqueDeRemise> getBanquesDeRemise()
        {
            List<BanqueDeRemise> lst = new List<BanqueDeRemise>();
            JArray json = DAC.getMethodeJsonArray("/BanquesRemise");
            foreach (JObject dr in json)
            {
                BanqueDeRemise b = Builders.BuildBanqueDeRemise.BuildJ(dr);

                lst.Add(b);
            }

            return lst;
        }
        private static List<BanqueDeRemise> getBanquesDeRemiseOLD()
        {
            List<BanqueDeRemise> lst = new List<BanqueDeRemise>();
            DataTable dt = DAC.GetBanquesDeRemise();

            foreach (DataRow dr in dt.Rows)
            {
                BanqueDeRemise b = Builders.BuildBanqueDeRemise.Build(dr);

                lst.Add(b);
            }

            return lst;
        }



        public static void AddBanqueEmetrices(Banque bnk)
        {
            if (bnk.Id == -1)
            {
                DAC.AddBanqueEmetrices(bnk);
                _Banques.Add(bnk);
            }
        }

        private static List<Banque> getBanques()
        {
            List<Banque> lst = new List<Banque>();
            JArray json = DAC.getMethodeJsonArray("/BanquesEmetrices");

            foreach (JObject dr in json)
            {
                Banque b = Builders.BuildBanque.BuildJ(dr);

                lst.Add(b);
            }

            return lst;
        }
        private static List<Banque> getBanquesOLD()
        {
            List<Banque> lst = new List<Banque>();
            DataTable dt = DAC.GetBanquesEmetrices();

            foreach (DataRow dr in dt.Rows)
            {
                Banque b = Builders.BuildBanque.Build(dr);

                lst.Add(b);
            }

            return lst;
        }
        public static void AddBanqueDeRemise(EntiteJuridique en, BanqueDeRemise bdr)
        {
            string codban = DAC.AddBanqueDeRemise(en, bdr);
            bdr.Code = codban;
            
            _BanquesDeRemise.Add(bdr);

            if (en.ComptesBancaire!=null) 
                en.ComptesBancaire.Add(bdr);

        }

        public static void DeleteBanqueDeRemise(BanqueDeRemise bdr)
        {
            DAC.DeleteBanqueDeRemise( bdr);
            
            _BanquesDeRemise.Remove(bdr);

        }


        

        public static void UpdateBanqueDeRemise( BanqueDeRemise bdr)
        {
            DAC.UpdateBanqueDeRemise( bdr);
        }




        public static char RibLetterToDigit(char letter)
        {
            if (letter >= 'A' && letter <= 'I')
            {
                return (char)(letter - 'A' + '1');
            }
            else if (letter >= 'J' && letter <= 'R')
            {
                return (char)(letter - 'J' + '1');
            }
            else if (letter >= 'S' && letter <= 'Z')
            {
                return (char)(letter - 'S' + '2');
            }
            else
                throw new ArgumentOutOfRangeException("Le caractère à convertir doit être une lettre majuscule dans la plage A-Z");
        }


        public static bool ValidateIBAN(string ibanValue)
        {
            string iban = ibanValue.Substring(4, ibanValue.Length - 4)
                  + ibanValue.Substring(0, 4);
            StringBuilder sb = new StringBuilder();
            foreach (char c in iban)
            {
                int v;
                if (Char.IsLetter(c))
                {
                    v = c - 'A' + 10;
                }
                else
                {
                    v = c - '0';
                }
                sb.Append(v);
            }
            string checkSumString = sb.ToString();
            int checksum = int.Parse(checkSumString.Substring(0, 1));
            for (int i = 1; i < checkSumString.Length; i++)
            {
                int v = int.Parse(checkSumString.Substring(i, 1));
                checksum *= 10;
                checksum += v;
                checksum %= 97;
            }
            return checksum == 1;
        }

        private static Regex regex_rib;
        public static bool IsValidRib(string rib)
        {
            // Suppression des espaces et tirets
            string tmp = rib.Replace(" ", "").Replace("-", "");

            // Vérification du format BBBBBGGGGGCCCCCCCCCCCKK
            // B : banque
            // G : guichet
            // C : numéro de compte
            // K : clé RIB
            if (regex_rib == null)
            {
                regex_rib = new Regex(@"(?<B>\d{5})(?<G>\d{5})(?<C>\w{11})(?<K>\d{2})", RegexOptions.Compiled);
            }
            Match m = regex_rib.Match(tmp);
            if (!m.Success)
                return false;

            // Extraction des composants
            string b_s = m.Groups["B"].Value;
            string g_s = m.Groups["G"].Value;
            string c_s = m.Groups["C"].Value;
            string k_s = m.Groups["K"].Value;

            // Remplacement des lettres par des chiffres dans le numéro de compte
            StringBuilder sb = new StringBuilder();
            foreach (char ch in c_s.ToUpper())
            {
                if (char.IsDigit(ch))
                    sb.Append(ch);
                else
                    sb.Append(RibLetterToDigit(ch));
            }
            c_s = sb.ToString();

            // Séparation du numéro de compte pour tenir sur 32bits
            

            // Calcul de la clé RIB
            // Algo ici : http://fr.wikipedia.org/wiki/Clé_RIB#Algorithme_de_calcul_qui_fonctionne_avec_des_entiers_32_bits

            int k;

            k = int.Parse(k_s);

            int calculatedKey = CalculateKeyRIB(b_s, g_s, c_s);

            return (k == calculatedKey);
        }

        private static int CalculateKeyRIB(string b_s, string g_s, string c_s)
        {

            string d_s = c_s.Substring(0, 6);
            string c_ss = c_s.Substring(6, 5);

            int b = int.Parse(b_s);
            int g = int.Parse(g_s);
            int d = int.Parse(d_s);
            int c = int.Parse(c_ss);


            return 97 - ((89 * b + 15 * g + 76 * d + 3 * c) % 97);
        }

        public static int GetCleBanqueRemise(BanqueDeRemise Compte)
        {

            return CalculateKeyRIB(Compte.NumA,Compte.NumGui,Compte.NumCPT);

          
        }


        public static List<BanqueDeRemise> getBanquesDeRemise(EntiteJuridique ej)
        {
            List<BanqueDeRemise> lstbqe = new List<BanqueDeRemise>();
            JArray json = DAC.getMethodeJsonArray("/GetBanquesDeRemise/" + ej.Id);
            List<string> lstcode = json.ToObject<List<string>>();
            foreach (BanqueDeRemise b in BanquesDeRemise)
            {
                if (lstcode.Contains(b.Code))
                    lstbqe.Add(b);
            }
            return lstbqe;
        }

        public static List<BanqueDeRemise> getBanquesDeRemiseOLD(EntiteJuridique ej)
        {
            List<BanqueDeRemise> lstbqe = new List<BanqueDeRemise>();
      
            List<string> lstcode = DAC.GetBanquesDeRemise(ej);
            foreach (BanqueDeRemise b in BanquesDeRemise)
            {
                if (lstcode.Contains(b.Code))
                    lstbqe.Add(b);
            }
            return lstbqe;
        }


        public static BanqueDeRemise getBanqueDeRemise(string code)
        {

            foreach (BanqueDeRemise b in BanquesDeRemise)
            {
                if (b.Code.Trim() == code.Trim()) return b;
            }
            return null;
        }

        public static Banque getBanque(string lib)
        {

            foreach (Banque b in Banques)
            {
                if (b.Libelle == lib) return b;
            }
            return null;
        }

        public static Banque getBanque(int id)
        {

            foreach (Banque b in Banques)
            {
                if (b.Id == id) return b;
            }
            return null;
        }
    }
}
