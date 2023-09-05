using BasCommon_BO;
using BasCommon_BO.Compta;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL.Compta
{
    public static class EcrituresMgmt
    {





        public static string validate(Ecriture ecriture)
        {
            string err = "";
            if ((ecriture.Debit == null) && (ecriture.Credit == null))
                err += "\nAucun montant définit";
            if ((ecriture.Debit != null) && (ecriture.Credit != null))
                err += "\nDebit OU Credit doit être définit";
            if (ecriture.codecompta == null)
                err += "\nAucun code comptable";
            if (ecriture.IdPieceComptable == null)
                err += "\nAucune piece comptable";
                       

            return err;

        }

        public static List<MdlEcriture> GetMdlEcritures(MdlPieceComptable piececompta)
        {
            List<MdlEcriture> lst = new List<MdlEcriture>();

            DataTable dt = BasCommon_DAL.DAC.GetMdlEcritures(piececompta);

            foreach (DataRow dr in dt.Rows)
            {
                MdlEcriture ec = Builders.BuildMdlEcriture.Build(dr);
                lst.Add(ec);
            }

            return lst;
        }

        public static Ecriture GetEcritureNVE(int id) {

            string method = "/getPieceComptableById/" + id;
            JObject obj = BasCommon_DAL.DAC.getMethodeJsonObjet(method);

            //return obj != null ? Builders.BuildEcriture.Build(obj) : null;

            return null;
        }

        public static Ecriture GetEcriture(int id)
        {
            DataRow dr = BasCommon_DAL.DAC.GetPieceComptable(id);

            if (dr == null) return null;

            Ecriture pc = Builders.BuildEcriture.Build(dr);

            return pc;

        }

        public static List<Ecriture> GetEcritures(DateTime dteDebut, DateTime dteFin, bool FromFog) {
            
            List<Ecriture> liste = new List<Ecriture>();
            
            string method = "/getEcritureByDates/";
            method+=dteDebut.ToString("yyyy-MM-dd HH:mm:ss");
            method += "&" + dteFin.ToString("yyyy-MM-dd HH:mm:ss");
            method += "&" + FromFog;

            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);

            foreach (JObject obj in jArray)
            {
                Ecriture e = Builders.BuildEcriture.Build(obj);
                liste.Add(e);
            }
            return liste;              

        }

        public static List<Ecriture> GetEcrituresOld(DateTime dteDebut,DateTime dteFin,bool FromFog)
        {
            DataTable dt = BasCommon_DAL.DAC.GetEcritures(dteDebut, dteFin, FromFog);
            List<Ecriture> lst = new List<Ecriture>();

            foreach (DataRow dr in dt.Rows)
            {
                Ecriture ec = Builders.BuildEcriture.Build(dr);
                lst.Add(ec);
            }

            return lst;

        }

        public static List<Ecriture> GetEcritures(PieceComptable pc)
        {
            DataTable dt = BasCommon_DAL.DAC.GetEcritures(pc);
            List<Ecriture> lst = new List<Ecriture>();

            foreach (DataRow dr in dt.Rows)
            {
                Ecriture ec = Builders.BuildEcriture.Build(dr);
                lst.Add(ec);
            }

            return lst;

        }

        public static void InsertMdlEcriture(MdlEcriture pc)
        {
            if (pc.Id == -1)
                BasCommon_DAL.DAC.AddMdlEcriture(pc);
        }

        public static void UpdateMdlEcriture(MdlEcriture pc)
        {
            if (pc.Id != -1)
                BasCommon_DAL.DAC.UpdateMdlEcriture(pc);
        }

        public static void DeleteMdlPieceComptableMdl(MdlEcriture pc)
        {
            if (pc.Id != -1)
                BasCommon_DAL.DAC.DeleteMdlEcriture(pc);
        }


        public static void InsertEcriture(BordereauFinance bf)
        {


            PieceComptable pc = new PieceComptable();
            pc.Libelle = "Bordereau numero : " + bf.NumBordereau.Trim();
            pc.DateOperation = bf.DateValeur.Value;
            pc.devise = DeviseMgmt.getDevise("EUR");
            pc.journal = bf.BanqueDeRemise.journalComptable;
            pc.NumPiece = bf.NumBordereau.Trim();
            pc.ecritures = new List<Ecriture>();

            pc.NumSaisie = PieceComptableMgmt.GetNextNumSaisie();

            Ecriture ec = new Ecriture();
            ec.codecompta = MgmtCodeComptable.getFromCode(bf.BanqueDeRemise.CompteComptable);
            ec.Credit = null;
            ec.Debit = bf.Montant; 
            ec.Libelle = bf.NumBordereau.Trim();
            ec.piece = pc;

            pc.ecritures.Add(ec);

            

            Ecriture ec2 = new Ecriture();

            switch (bf.paiements[0].typeencaissement)
            {
                case PaiementReel.TypeEncaissement.Especes:
                    ec2.codecompta = MgmtCodeComptable.getFromCode("706000");
                    break;
                case PaiementReel.TypeEncaissement.Cheque:
                    ec2.codecompta = MgmtCodeComptable.getFromCode("706100");
                    break;
                case PaiementReel.TypeEncaissement.CB:
                    ec2.codecompta = MgmtCodeComptable.getFromCode("706200");
                    break;
                case PaiementReel.TypeEncaissement.Pnf:
                case PaiementReel.TypeEncaissement.Optalion:
                    ec2.codecompta = MgmtCodeComptable.getFromCode("706300");
                    break;
                case PaiementReel.TypeEncaissement.Prelevement:
                    ec2.codecompta = MgmtCodeComptable.getFromCode("706400");
                    break;
                case PaiementReel.TypeEncaissement.Virement:
                    ec2.codecompta = MgmtCodeComptable.getFromCode("706500");
                    break;
            }

            
            ec2.Credit =bf.Montant;
            ec2.Debit = null; 
            ec2.Libelle = bf.NumBordereau;
            ec2.piece = pc;

            pc.ecritures.Add(ec2);

            PieceComptableMgmt.InsertPieceComptable(pc);


        }
        
        public static void InsertEcriture(Virement bf)
        {
            if (bf.echeance.patient == null)
                bf.echeance.patient = baseMgmtPatient.GetPatient(bf.echeance.IdPatient);

            PieceComptable pc = new PieceComptable();
            pc.Libelle = "Virement de  " + bf.patient.ToString();
            pc.DateOperation = bf.DateValeurBqe.Value;
            pc.devise = DeviseMgmt.getDevise("EUR");
            pc.journal = bf.comptecabinet.journalComptable;
            pc.NumPiece = "vir" + bf.echeance.Id.ToString();
            pc.ecritures = new List<Ecriture>();



            Ecriture ec = new Ecriture();
            ec.codecompta = MgmtCodeComptable.getFromCode(bf.comptecabinet.CompteComptable);
            ec.Credit = null;
            ec.Debit = bf.Montant; 
            ec.Libelle = "vir" + bf.echeance.Id.ToString();
            ec.piece = pc;

            pc.ecritures.Add(ec);



            Ecriture ec2 = new Ecriture();

            ec2.codecompta = MgmtCodeComptable.getFromCode("706500");


            ec2.Credit = bf.Montant;
            ec2.Debit = null;
            ec2.Libelle = "vir" + bf.echeance.Id.ToString();
            ec2.piece = pc;

            pc.ecritures.Add(ec2);

            PieceComptableMgmt.InsertPieceComptable(pc);


        }
        
        public static void InsertEcriture(PaiementReel bf)
        {
         
            PieceComptable pc = new PieceComptable();
            pc.Libelle = "Reglement de  " + bf.Patients+" par "+bf.typeencaissement.ToString();
            pc.DateOperation = bf.DateValeurBqe.Value;
            pc.devise = DeviseMgmt.getDevise("EUR");
            pc.journal = bf.BanqueDeRemise.journalComptable;
            pc.NumPiece = "Reglmnt_" + bf.Id.ToString();
            pc.ecritures = new List<Ecriture>();



            Ecriture ec = new Ecriture();
            ec.codecompta = MgmtCodeComptable.getFromCode(bf.BanqueDeRemise.CompteComptable);
            ec.Credit =null;
            ec.Debit = bf.Montant;
            ec.Libelle = "Reglmnt_" + bf.Id.ToString();
            ec.piece = pc;

            pc.ecritures.Add(ec);



            Ecriture ec2 = new Ecriture();

            switch (bf.typeencaissement)
            {
                case PaiementReel.TypeEncaissement.Especes:
                    ec2.codecompta = MgmtCodeComptable.getFromCode("706000");
                    break;
                case PaiementReel.TypeEncaissement.Cheque:
                    ec2.codecompta = MgmtCodeComptable.getFromCode("706100");
                    break;
                case PaiementReel.TypeEncaissement.CB:
                    ec2.codecompta = MgmtCodeComptable.getFromCode("706200");
                    break;
                case PaiementReel.TypeEncaissement.Pnf:
                case PaiementReel.TypeEncaissement.Optalion:
                    ec2.codecompta = MgmtCodeComptable.getFromCode("706300");
                    break;
                case PaiementReel.TypeEncaissement.Prelevement:
                    ec2.codecompta = MgmtCodeComptable.getFromCode("706400");
                    break;
                case PaiementReel.TypeEncaissement.Virement:
                    ec2.codecompta = MgmtCodeComptable.getFromCode("706500");
                    break;
            }


            ec2.Credit = bf.Montant;
            ec2.Debit = null; 
            ec2.Libelle = "Reglmnt_" + bf.Id.ToString();
            ec2.piece = pc;

            pc.ecritures.Add(ec2);

            PieceComptableMgmt.InsertPieceComptable(pc);


        }
        
        public static void InsertEcriture(Ecriture pc)
        {
            BasCommon_DAL.DAC.AddEcriture(pc);
        }

        public static void UpdateEcriture(Ecriture pc)
        {
            if (pc.Id != -1)
                BasCommon_DAL.DAC.UpdateEcriture(pc);
        }

        public static void DeleteEcriture(Ecriture ec)
        {
            if (ec.Id != -1)
                BasCommon_DAL.DAC.DeleteEcriture(ec);
        }

        public static void DeleteEcritures(PieceComptable pc)
        {
                BasCommon_DAL.DAC.DeleteEcritures(pc);
        }
    }
}
