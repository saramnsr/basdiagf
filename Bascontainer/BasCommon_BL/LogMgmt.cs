using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_DAL;
using BasCommon_BO;
using System.Data;

namespace BasCommon_BL
{
    public static class LogMgmt
    {


        public static BaseLog ReEcheancement(
                                                          List<Echeance> lstEch,
                                                          List<ActePG> actesassocier)
        {
            BaseLog log = new BaseLog();
            log.CodeAction = "REECHANCE";
            log.Category = "Finance Echeance";


            if (actesassocier.Count > 1)
                log.Commentaires = "Réechéancement de l'acte " + actesassocier[0].Libelle + "(" + actesassocier[0].Montant_Honoraire.ToString("C2") + ") du " + actesassocier[0].DateExecution.ToString();

            else
                log.Commentaires = "Réechéancement de " + actesassocier.Count.ToString() + " actes ";

            foreach (Echeance ech in lstEch)
            {
                log.Commentaires += "\n " + ech.Libelle + ":" + ech.Montant.ToString("C2") + ":" + ech.DateEcheance.ToString() + (ech.ParPrelevement ? "A prelever" : "") + (ech.ParVirement ? "par virement" : "");

            }



            log.DteLog = DateTime.Now;
            log.IdPatient = actesassocier[0].IdPatient;
            log.IdUser = UtilisateursMgt.CurrentUtilisateur == null ? -1 : UtilisateursMgt.CurrentUtilisateur.Utilisateur.Id;
            log.NomMachine = System.Environment.MachineName;

            DAC.Insert_Log(log);

            return log;

        }



        public static BaseLog AssocierALencaissement(int Idpat,
                                                          Echeance ech,
                                                          Encaissement ec)
        {
            BaseLog log = new BaseLog();
            log.CodeAction = "ASSOCIERECHANCE";
            log.Category = "Finance Echeance";



            log.Commentaires = "Association manuelle de l'echeance " + ech.Libelle + ":" + ech.Montant.ToString("C2") + ":" + ech.DateEcheance.ToString() + (ech.ParPrelevement ? "A prelever" : "") + (ech.ParVirement ? "par virement" : "");
            log.Commentaires += "\nà l'encaissement du " + ec.paiementreel.DateEncaissement.ToShortDateString() + "(" + ec.paiementreel.Montant.ToString("C2") + ")";

            if (ec.paiementreel.typeencaissement == PaiementReel.TypeEncaissement.Cheque)
                log.Commentaires += "\n cheque : " + ec.paiementreel.NumCheque;
            else
                log.Commentaires += "\n par " + ec.paiementreel.typeencaissement.ToString();






            log.DteLog = DateTime.Now;
            log.IdPatient = Idpat;
            log.IdUser = UtilisateursMgt.CurrentUtilisateur == null ? -1 : UtilisateursMgt.CurrentUtilisateur.Utilisateur.Id;
            log.NomMachine = System.Environment.MachineName;

            DAC.Insert_Log(log);

            return log;

        }

        public static BaseLog Prelever(int Idpat,
                                                          PaiementReel paiement)
        {
            BaseLog log = new BaseLog();
            log.CodeAction = "PRELEVEMENT";
            log.Category = "Finance prelevement";


            log.Commentaires = "prelevement d'un encaissement de " + paiement.Montant.ToString("C2");


            log.Commentaires += "\n Banque : " + paiement.BanqueDeRemise.Libelle.ToString();


            log.DteLog = DateTime.Now;
            log.IdPatient = Idpat;
            log.IdUser = UtilisateursMgt.CurrentUtilisateur == null ? -1 : UtilisateursMgt.CurrentUtilisateur.Utilisateur.Id;
            log.NomMachine = System.Environment.MachineName;

            DAC.Insert_Log(log);

            return log;

        }


        public static List<BaseLog> getLogs(basePatient pat)
        {
            List<BaseLog> lst = new List<BaseLog>();
            DataTable dt = DAC.getLogs(pat);

            foreach (DataRow dr in dt.Rows)
            {
                BaseLog b = Builders.BuildLog.Build(dr);

                lst.Add(b);
            }

            return lst;
        }


        public static BaseLog Virement(int Idpat,
                                                         PaiementReel paiement,
                                                         Echeance ech)
        {
            BaseLog log = new BaseLog();
            log.CodeAction = "VIREMENT";
            log.Category = "Finance Viement";


            log.Commentaires = "Encaissement d'un Tiers de " + paiement.Montant.ToString("C2");

            switch (ech.payeur)
            {
                case Echeance.typepayeur.Banque:
                    log.Commentaires += "\n Payeur : Banque";
                    break;
                case Echeance.typepayeur.Mutuelle:
                    log.Commentaires += "\n Payeur : Mutuelle " + (ech.mutuelle==null?"":ech.mutuelle.ToString());
                    break;
                case Echeance.typepayeur.Secu:
                    log.Commentaires += "\n Payeur : Secu ";
                    break;
            }
            log.Commentaires += "\n Banque : " + paiement.BanqueDeRemise.Libelle.ToString();


            log.DteLog = DateTime.Now;
            log.IdPatient = Idpat;
            log.IdUser = UtilisateursMgt.CurrentUtilisateur == null ? -1 : UtilisateursMgt.CurrentUtilisateur.Utilisateur.Id;
            log.NomMachine = System.Environment.MachineName;

            DAC.Insert_Log(log);

            return log;

        }



        public static BaseLog RemiseEnBanque(int Idpat,
                                                          PaiementReel paiement)
        {
            BaseLog log = new BaseLog();
            log.CodeAction = "REMBANQUE";
            log.Category = "Finance Remise en banque";


            log.Commentaires = "Remise en banque d'un encaissement de " + paiement.Montant.ToString("C2");

            if (paiement.typeencaissement == PaiementReel.TypeEncaissement.Cheque)
            {
                log.Commentaires += "\n cheque : " + paiement.NumCheque;
                if (paiement.BanqueEmetrice != null)
                    log.Commentaires += "\n Banque du cheque : " + paiement.BanqueEmetrice.Libelle;

            }
            else
                log.Commentaires += "\n par " + paiement.typeencaissement.ToString();


            log.Commentaires += "\n payeur : " + paiement.payeur.ToString();
            log.Commentaires += "\n Banque : " + paiement.BanqueDeRemise.Libelle.ToString();


            log.DteLog = DateTime.Now;
            log.IdPatient = Idpat;
            log.IdUser = UtilisateursMgt.CurrentUtilisateur == null ? -1 : UtilisateursMgt.CurrentUtilisateur.Utilisateur.Id;
            log.NomMachine = System.Environment.MachineName;

            DAC.Insert_Log(log);

            return log;

        }




        public static BaseLog RefusPaiement(PaiementReel pr, string motif, double frais, int IdPatient)
        {
            BaseLog log = new BaseLog();
            log.CodeAction = "REFUSPAIEMENT";
            log.Category = "Finance Encaissement";



            log.Commentaires = "Refus d'un paiement ";

            if (pr.typeencaissement == PaiementReel.TypeEncaissement.Cheque)
                log.Commentaires += "\n cheque : " + pr.NumCheque + " de " + pr.Montant.ToString("C2");
            else
                log.Commentaires += "\n par " + pr.typeencaissement.ToString() + " de " + pr.Montant.ToString("C2");

            if (pr.EstRemisEnBanque == PaiementReel.RemisEnBanque.Oui)
                log.Commentaires += "\n remis en banque le " + pr.DateRemiseEnBanque.ToString();

            if (pr.payeur != "")
                log.Commentaires += "\n payeur : " + pr.payeur;
            log.Commentaires += "\n frais applique : " + frais.ToString("C2");


            log.DteLog = DateTime.Now;
            log.IdPatient = IdPatient;
            log.IdUser = UtilisateursMgt.CurrentUtilisateur == null ? -1 : UtilisateursMgt.CurrentUtilisateur.Utilisateur.Id;
            
            log.NomMachine = System.Environment.MachineName;

            DAC.Insert_Log(log);

            return log;

        }


        public static BaseLog SuppressionEncaissement(int Idpat,
                                                          Encaissement enc)
        {
            BaseLog log = new BaseLog();
            log.CodeAction = "DELENCAISSEMENT";
            log.Category = "Finance Encaissement";

            if (enc.paiementreel != null)
            {

                log.Commentaires = "Suppression d'un encaissement de " + enc.MontantEncaisse.ToString("C2");

                if (enc.paiementreel.typeencaissement == PaiementReel.TypeEncaissement.Cheque)
                    log.Commentaires += "\n cheque : " + enc.paiementreel.NumCheque + " de " + enc.paiementreel.Montant.ToString("C2");
                else
                    log.Commentaires += "\n par " + enc.paiementreel.typeencaissement.ToString() + " de " + enc.paiementreel.Montant.ToString("C2");

                if (enc.paiementreel.EstRemisEnBanque == PaiementReel.RemisEnBanque.Oui)
                    log.Commentaires += "\n remis en banque le " + enc.paiementreel.DateRemiseEnBanque.ToString();

                if (enc.paiementreel.payeur != "")
                    log.Commentaires += "\n payeur : " + enc.paiementreel.payeur;
            }
            else
            {
                log.Commentaires = enc.MontantEncaisse.ToString("C2");
            }

            log.DteLog = DateTime.Now;
            log.IdPatient = Idpat;
            log.IdUser = UtilisateursMgt.CurrentUtilisateur == null ? -1 : UtilisateursMgt.CurrentUtilisateur.Utilisateur.Id;
            log.NomMachine = System.Environment.MachineName;

            DAC.Insert_Log(log);

            return log;

        }


        public static BaseLog AjoutEncaissement(int Idpat,
                                                          Encaissement enc)
        {
            BaseLog log = new BaseLog();
            log.CodeAction = "ADDENCAISSEMENT";
            log.Category = "Finance Encaissement";
            log.Commentaires = "Ajout d'un encaissement de " + enc.MontantEncaisse.ToString("C2");


            if (enc.paiementreel.typeencaissement == PaiementReel.TypeEncaissement.Cheque)
                log.Commentaires += "\n cheque : " + enc.paiementreel.NumCheque;
            else
                log.Commentaires += "\n par " + enc.paiementreel.typeencaissement.ToString();

            if (enc.paiementreel.payeur != "")
                log.Commentaires += "\n payeur : " + enc.paiementreel.payeur;


            log.DteLog = DateTime.Now;
            log.IdPatient = Idpat;
            log.IdUser = UtilisateursMgt.CurrentUtilisateur == null ? -1 : UtilisateursMgt.CurrentUtilisateur.Utilisateur.Id;
            log.NomMachine = System.Environment.MachineName;

            DAC.Insert_Log(log);

            return log;

        }

        public static BaseLog ModifierDateEcheance(int Idpat,
                                                          PaiementReel enc,
                                                          DateTime? ancienneDate)
        {
            BaseLog log = new BaseLog();
            log.CodeAction = "MODIFIERENCAISSEMENT";
            log.Category = "Finance Encaissement";
            log.Commentaires = "Modification date d'échéance de remise en banque\n";
            log.Commentaires += "\n Ancienne date d'echeance : " + ancienneDate==null?"Aucune":ancienneDate.Value.ToString();
            log.Commentaires += "\n Nouvelle date d'echeance : " + enc.DateEcheance==null?"Aucune":enc.DateEcheance.Value.ToString();

            if (enc.typeencaissement == PaiementReel.TypeEncaissement.Cheque)
                log.Commentaires += "\n cheque : " + enc.NumCheque;
            else
                log.Commentaires += "\n par " + enc.typeencaissement.ToString();

            if (enc.EstRemisEnBanque == PaiementReel.RemisEnBanque.Oui)
                log.Commentaires += "\n remis en banque le " + enc.DateRemiseEnBanque.ToString();

            if (enc.payeur != "")
                log.Commentaires += "\n payeur : " + enc.payeur;




            log.DteLog = DateTime.Now;
            log.IdPatient = Idpat;
            log.IdUser = UtilisateursMgt.CurrentUtilisateur == null ? -1 : UtilisateursMgt.CurrentUtilisateur.Utilisateur.Id;
            log.NomMachine = System.Environment.MachineName;

            DAC.Insert_Log(log);

            return log;

        }



        public static BaseLog ModifierEcheance(Echeance Old,
                                                          Echeance enc)
        {
            BaseLog log = new BaseLog();
            log.CodeAction = "MODIFIERECHEANCE";
            log.Category = "Finance Echéance";
            log.Commentaires = "Modification de l'échéance " + enc.acte.Libelle + "\n";
            log.Commentaires += "\n Ancienne écheance : " + Old.Libelle + ":" + Old.DateEcheance.ToString();
            log.Commentaires += "\n Nouvelle écheance : " + enc.Libelle + ":" + enc.DateEcheance.ToString();



            log.DteLog = DateTime.Now;
            log.IdPatient = enc.IdPatient;
            log.IdUser = UtilisateursMgt.CurrentUtilisateur == null ? -1 : UtilisateursMgt.CurrentUtilisateur.Utilisateur.Id;
            log.NomMachine = System.Environment.MachineName;

            DAC.Insert_Log(log);

            return log;

        }


        public static BaseLog AjoutActe(int Idpat,ActePG acte, CommClinique comm = null)
        {
            BaseLog log = new BaseLog();
            log.CodeAction = "ADDPRODUIT";
            log.Category = "Finance Acte/Produit";
            log.Commentaires = "Ajout d'un Acte/Produit";
            log.Commentaires += "\n" + acte.Libelle;

            log.Commentaires += "\nMontant : " + acte.Montant_Honoraire.ToString("C2");
            log.Commentaires += "\ndesignation : " + acte.designation;


            log.DteLog = DateTime.Now;
            log.IdPatient = Idpat;
            log.IdUser = UtilisateursMgt.CurrentUtilisateur==null?-1:UtilisateursMgt.CurrentUtilisateur.Utilisateur.Id;
            log.NomMachine = System.Environment.MachineName;

            DAC.Insert_Log(log);

            return log;

        }


        public static BaseLog SuppressionActe(int Idpat,ActePG acte)
        {
            BaseLog log = new BaseLog();
            log.CodeAction = "DELPRODUIT";
            log.Category = "Finance Acte/Produit";
            log.Commentaires = "Suppression d'un achat";
            log.Commentaires += "\n" + acte.Libelle;
            log.Commentaires += "\nMontant : " + acte.Montant_Honoraire.ToString("C2");
            log.Commentaires += "\ndesignation : " + acte.designation;


            log.DteLog = DateTime.Now;
            log.IdPatient = Idpat;
            log.IdUser = UtilisateursMgt.CurrentUtilisateur == null ? -1 : UtilisateursMgt.CurrentUtilisateur.Utilisateur.Id;
            log.NomMachine = System.Environment.MachineName;

            DAC.Insert_Log(log);

            return log;

        }

        public static BaseLog ModificationActe(int Idpat,ActePG Oldacte,
                                                           ActePG Newacte
            )
        {
            BaseLog log = new BaseLog();
            log.CodeAction = "DELPRODUIT";
            log.Category = "Finance Acte/Produit";
            log.Commentaires += "Modification d'un Acte/Produit";
            log.Commentaires += "\n" + Newacte.Libelle;

            System.Reflection.PropertyInfo[] array = typeof(ActePG).GetProperties();

            foreach (System.Reflection.PropertyInfo nfo in array)
            {
                try
                {
                    object Oldobject = nfo.GetValue(Oldacte, new object[] { });
                    object Newobject = nfo.GetValue(Newacte, new object[] { });
                    if (((Oldobject != null) && (Newobject != null)) && (!Oldobject.Equals(Newobject)))
                    {
                        log.Commentaires += "\n" + nfo.Name + " De : " + Oldobject.ToString() + " vers : " + Newobject.ToString();

                    }
                }
                catch (System.Exception) { }

            }



            log.DteLog = DateTime.Now;
            log.IdPatient = Idpat;
            log.IdUser = UtilisateursMgt.CurrentUtilisateur == null ? -1 : UtilisateursMgt.CurrentUtilisateur.Utilisateur.Id;
            log.NomMachine = System.Environment.MachineName;

            DAC.Insert_Log(log);

            return log;

        }


        public static void InsertLog(BaseLog log)
        {
            DAC.Insert_Log(log);

        }
    }
}
