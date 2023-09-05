using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using BasCommon_BO;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class MgmtBordereauFinance
    {


        

        public static void suppressionPaiementfrombordereau(BordereauFinance bf,PaiementReel pr)
        {

            bool cancontinue = false;
            foreach (PaiementReel bfpr in bf.paiements)
            {
                if (bfpr.typeencaissement == PaiementReel.TypeEncaissement.Especes) return;
                if (pr.Id == bfpr.Id)
                {
                    cancontinue = true;
                }
            }
            if (!cancontinue) return;
            double total = 0;
            int nbchque = 0;
            int nbcb = 0;
            int nbvirements = 0;
            int nbprelevements = 0;
            foreach (PaiementReel bfpr in bf.paiements)
            {
                if (bfpr.Id == pr.Id) continue;
                total += bfpr.Montant;
                nbchque += bfpr.typeencaissement == PaiementReel.TypeEncaissement.Cheque ? 1 : 0;
                nbcb += ((bfpr.typeencaissement == PaiementReel.TypeEncaissement.CB || bfpr.typeencaissement == PaiementReel.TypeEncaissement.AMEX) ? 1 : 0);
                nbvirements += ((bfpr.typeencaissement == PaiementReel.TypeEncaissement.Virement) ? 1 : 0);
                nbprelevements += ((bfpr.typeencaissement == PaiementReel.TypeEncaissement.Prelevement) ? 1 : 0);

            }
            bf.Montant = total;
            bf.NbCheques = nbchque;
            bf.NbCBs = nbcb;
            bf.NbVirements = nbvirements;
            bf.NbPrelevements = nbprelevements;

            DAC.suppressionPaiementfrombordereau(bf,pr);
        }

        public static void Validate(BordereauFinance bf)
        {
            DAC.validateBordereau(bf);
            BasCommon_BL.Compta.EcrituresMgmt.InsertEcriture(bf);
        }

        public static List<PaiementReel> GetPaiementsToCheck(BanqueDeRemise bqe, DateTime dte1, DateTime dte2) {

            List<PaiementReel> liste = new List<PaiementReel>();
            string method = "/getPaiementReelToCheck/" + dte1.ToString("yyyy-MM-dd HH:mm:ss") + "&" + dte2.ToString("yyyy-MM-dd HH:mm:ss")+"&";
            if (bqe == null)
                method += -1;
            else
                method += bqe.Code;
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);

            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildPaiementReel.BuildJ(obj));
            return liste;
        }
        public static List<PaiementReel> GetPaiementsToCheckOld(BanqueDeRemise bqe, DateTime dte1, DateTime dte2)
        {
            List<PaiementReel> resultats = new List<PaiementReel>();
            DataTable dt = DAC.GetPaiementsToCheck(bqe,dte1,dte2);

            foreach (DataRow dr in dt.Rows)
            {
                PaiementReel pr = Builders.BuildPaiementReel.Build(dr);
                resultats.Add(pr);
            }

            return resultats;
        }

        public static List<BordereauFinance> GetBordereauToCheck(BanqueDeRemise bqe, DateTime dte1, DateTime dte2)
        {
            List<BordereauFinance> resultats = new List<BordereauFinance>();
            if (bqe != null)
            {
                //bqe.Code = Convert.ToString(bqe.Code).ToString()== null ? "" : Convert.ToString(bqe.Code);

                
                JArray array = BasCommon_DAL.DAC.getMethodeJsonArray("/GetBordereauToCheck/" + bqe.Code + "&" + dte1.ToString("yyyy-MM-dd HH:mm:ss") + "&" + dte2.ToString("yyyy-MM-dd HH:mm:ss"));

                foreach (JObject dr in array)
                {
                    BordereauFinance bf = Builders.BuildBordereauFinance.BuildJson(dr);
                    resultats.Add(bf);
                }


                return resultats;
            }
            return resultats;
            
           
            
        }

        public static List<BordereauFinance> GetBordereauToCheckold(BanqueDeRemise bqe,DateTime dte1,DateTime dte2)
        {
            List<BordereauFinance> resultats = new List<BordereauFinance>();
            DataTable dt = DAC.GetBordereauToCheck(bqe,dte1,dte2);

            foreach (DataRow dr in dt.Rows)
            {
                BordereauFinance bf = Builders.BuildBordereauFinance.Build(dr);
                resultats.Add(bf);
            }

            return resultats;
        }

        public static List<BordereauFinance> CreateBordereaux(List<PaiementReel> paiements, 
                                                                ControlFinancier ctrlfinance)
        {

            List<BordereauFinance> resultats = new List<BordereauFinance>();
            Dictionary<BanqueDeRemise, List<PaiementReel>> dico = new Dictionary<BanqueDeRemise, List<PaiementReel>>();


            foreach (PaiementReel pr in paiements)
            {
                if (pr.BanqueDeRemise == null)
                    throw new System.Exception("Le paiement de " + pr.payeur + " n'a pas de banque de remise");
                if (!dico.ContainsKey(pr.BanqueDeRemise))
                    dico.Add(pr.BanqueDeRemise, new List<PaiementReel>());

                dico[pr.BanqueDeRemise].Add(pr);
            }

            int i = 1;
            foreach (KeyValuePair<BanqueDeRemise, List<PaiementReel>> kv in dico)
            {
                BordereauFinance bf = new BordereauFinance();
                bf.BanqueDeRemise = kv.Key;

                int nbchq = 0;
                int nbcb = 0;
                int nbprelevement = 0;
                int nbvirements = 0;
                double total = 0;
                bf.paiements = new List<PaiementReel>();
                foreach (PaiementReel pr in kv.Value)
                {
                    if (pr.typeencaissement==PaiementReel.TypeEncaissement.Cheque)
                        nbchq++;
                    if (pr.typeencaissement == PaiementReel.TypeEncaissement.CB ||
                        pr.typeencaissement == PaiementReel.TypeEncaissement.AMEX)
                        nbcb++;
                    if (pr.typeencaissement == PaiementReel.TypeEncaissement.Prelevement)
                        nbprelevement++;
                    if (pr.typeencaissement == PaiementReel.TypeEncaissement.Virement)
                        nbvirements++;

                    total+=pr.Montant;
                    bf.paiements.Add(pr);
                }
                bf.Montant = total;
                bf.NbCheques = nbchq;
                bf.NbCBs = nbcb;
                bf.NbPrelevements = nbprelevement;
                bf.NbVirements = nbvirements;
                bf.NumBordereau = DateTime.Now.ToString("ddMMyyyyHHmmss") + i.ToString();
                bf.NumBordereauBancaire = "";
                bf.DateRemise = DateTime.Now;
                bf.Controlfinance = ctrlfinance;
                resultats.Add(bf);
                i++;
                
            }

            return resultats;
        }

        public static void Insert(BordereauFinance bf)
        {
           DAC.InsertBordereaufinance(bf);

            
        }

       

    }
}
