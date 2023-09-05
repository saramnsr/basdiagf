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
    public static class PieceComptableMgmt
    {

        private static List<MdlPieceComptable> _modeles;
        public static List<MdlPieceComptable> modeles
        {
            get
            {
                if (_modeles == null)
                    _modeles = getModeles();

                return _modeles;
            }

            set
            {
                _modeles = value;
            }
        }





        public static void AddDepense(string Libelle, Devise devise, double Montant, TypeDeDepense tdd, Ecriture.TypeReglement typereglement, DateTime date, BanqueDeRemise bqe, string numeropiece)
        {
            PieceComptable piececomptable = new PieceComptable();
            piececomptable.DateEcheance = null;
            piececomptable.DateOperation = date;
            piececomptable.devise = devise;
            piececomptable.Fog = true;
            piececomptable.journal = JournalMgmt.getJournal("60") == null ? JournalMgmt.getJournal("HA") : JournalMgmt.getJournal("60");
            piececomptable.Libelle = string.IsNullOrEmpty(Libelle) ? ((tdd == null) ? "Achat" : tdd.Libelle) : Libelle;
            piececomptable.NumPiece = numeropiece;
            piececomptable.NumSaisie = PieceComptableMgmt.GetNextNumSaisie();
           
            piececomptable.ecritures = new List<Ecriture>();

            Ecriture ec = new Ecriture();
            ec.codecompta = (tdd == null) ? MgmtCodeComptable.getFromCode("600000") : tdd.CodeComptable;
            ec.Credit = Montant;
            ec.Debit = null;
            ec.piece = piececomptable;
            ec.taxe = TaxeValeurAjouteeMgmt.getTaxe("TVA");
            ec.Libelle = piececomptable.Libelle;
            ec.Type = typereglement;

            piececomptable.ecritures.Add(ec);

            ec = new Ecriture();
            ec.codecompta = MgmtCodeComptable.getFromCode(bqe.CompteComptable);
            ec.Credit = null;
            ec.Debit = Montant;
            ec.piece = piececomptable;
            ec.taxe = TaxeValeurAjouteeMgmt.getTaxe("TVA");
            ec.Libelle = piececomptable.Libelle;

            piececomptable.ecritures.Add(ec);

            PieceComptableMgmt.InsertPieceComptable(piececomptable);


        }

        private static List<MdlPieceComptable> getModeles()
        {
            DataTable dt = BasCommon_DAL.DAC.GetMdlPieceComptable();

            List<MdlPieceComptable> lst = new List<MdlPieceComptable>();

            foreach (DataRow dr in dt.Rows)
            {
                MdlPieceComptable mdl = Builders.BuildMdlPieceComptable.Build(dr);
                lst.Add(mdl);
            }


            return lst;
        }




        public static void SortirDuBrouillard(PieceComptable pc)
        {
            pc.Fog = false;
            BasCommon_DAL.DAC.SortirDuBrouillard(pc);

        }

        public static int GetNextNumSaisie()
        {
           // return BasCommon_DAL.DAC.GetNextNumSaisie();
            return Convert.ToInt32(BasCommon_DAL.DAC.getMethodeJsonString("/GetNextNumSaisie"));

        }

        public static string validate(PieceComptable piece)
        {
            string err = "";
            double totalcredit = 0;
            double totaldebit = 0;

            foreach (Ecriture e in piece.ecritures)
            {
                if (e.Credit.HasValue) totalcredit += e.Credit.Value;
                if (e.Debit.HasValue)totaldebit += e.Debit.Value;
            }

            if (totaldebit == 0 && totalcredit == 0)
                err += "\n La saisie d'une piece comptable dont la valeur est nul n'est pas autorisé";

            if (piece.ecritures.Count == 0)
                err += "\n Aucune ecriture pour cette piece";

            if (totaldebit != totalcredit)
                err += "\n Balance non équilibrée pour cette piece";


            if (piece.journal==null)
                err += "\n Cette piece comptable doit être attribuée à un journal";

            if (string.IsNullOrEmpty(piece.NumPiece) || piece.NumPiece=="?")
                err += "\n Un numero de piece est obligatoire";

            if (!BasCommon_DAL.DAC.CheckPieceIsOk(piece))
                err += "\n Numero de piece comptable déja attribué";


            return err;

        }
       

        public static List<PieceComptable> GetPieceComptablesInTheFog(string text, DateTime dteStart, DateTime dteEnd) {
            
            //  Path :/PieceComptablesInTheFog/{text}&{startDate}@{endDate}

            List<PieceComptable> liste = new List<PieceComptable>();
            
            string method = "/PieceComptablesInTheFog/";

            method += String.IsNullOrEmpty(text) ? "" : text;
            
            if (dteStart == DateTime.MinValue || dteStart == null)
                method += "&&";
            else
                method += "&" + dteStart.ToString("yyyy-MM-dd HH:mm:ss") + "&" + dteEnd.ToString("yyyy-MM-dd HH:mm:ss");
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);

            if (jArray == null) return liste;
            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildPieceComptable.Build(obj));
            return liste;
        
        }
        public static List<PieceComptable> GetPieceComptablesInTheFogOld(string text, DateTime dteStart, DateTime dteEnd)
        {
            DataTable dt = BasCommon_DAL.DAC.GetPieceComptablesInTheFog(text, dteStart, dteEnd);

            List<PieceComptable> lst = new List<PieceComptable>();
            foreach (DataRow dr in dt.Rows)
            {
                PieceComptable pc = Builders.BuildPieceComptable.Build(dr);
                lst.Add(pc);
            }


            return lst;

        }
        public static List<PieceComptable> GetPieceComptables(DateTime dteStart, DateTime dteEnd) {

            List<PieceComptable> liste = new List<PieceComptable>();
            string method = "/getPieceComptableByDates/" + dteStart.ToString("yyyy-MM-dd HH:mm:ss");
            method += "&" + dteEnd.ToString("yyyy-MM-dd HH:mm:ss");

            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);
            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildPieceComptable.Build(obj));
            return liste;
        
        }

        public static List<PieceComptable> GetPieceComptablesOld(DateTime dteStart, DateTime dteEnd)
        {
            DataTable dt = BasCommon_DAL.DAC.GetPieceComptables(dteStart, dteEnd);

            List<PieceComptable> lst = new List<PieceComptable>();
            foreach (DataRow dr in dt.Rows)
            {
                PieceComptable pc = Builders.BuildPieceComptable.Build(dr);
                lst.Add(pc);
            }


            return lst;

        }
        public static List<PieceComptable> GetPieceComptables(DateTime dteStart, DateTime dteEnd, Journal jrnl)
        {
            JArray array = BasCommon_DAL.DAC.getMethodeJsonArray("/getAllPieceComptables/" + dteStart.ToString("yyyy-MM-dd HH:mm:ss") + "&" + dteEnd.ToString("yyyy-MM-dd HH:mm:ss") + "&" + jrnl.NumJournal);

            List<PieceComptable> lst = new List<PieceComptable>();
            foreach (JObject dr in array)
            {
                PieceComptable pc = Builders.BuildPieceComptable.BuildJson(dr);
                lst.Add(pc);
            }


            return lst;

        }

        public static List<PieceComptable> GetPieceComptablesOld(DateTime dteStart, DateTime dteEnd,Journal jrnl)
        {
            DataTable dt = BasCommon_DAL.DAC.GetAllPieceComptables(dteStart, dteEnd, jrnl);

            List<PieceComptable> lst = new List<PieceComptable>();
            foreach (DataRow dr in dt.Rows)
            {
                PieceComptable pc = Builders.BuildPieceComptable.Build(dr);
                lst.Add(pc);
            }


            return lst;

        }

        public static List<PieceComptable> GetPieceComptablesDepenses(DateTime dteStart, DateTime dteEnd)
        {
            return GetPieceComptables(dteStart, dteEnd, JournalMgmt.getJournal("60"));           
        }


        public static PieceComptable GetPieceComptable(int id) {

            string method = "/getPieceComptableById/"+id;

            JObject obj = BasCommon_DAL.DAC.getMethodeJsonObjet(method);
            return obj != null ? Builders.BuildPieceComptable.Build(obj) : null;
 
        }

        public static PieceComptable GetPieceComptableOld(int id)
        {
            DataRow dr = BasCommon_DAL.DAC.GetPieceComptable(id);

            if (dr == null) return null;

            PieceComptable pc = Builders.BuildPieceComptable.Build(dr);

            return pc;

        }


        public static PieceComptable GetPieceComptableFromModele(MdlPieceComptable pc)
        {
            PieceComptable mdl = new PieceComptable();

            mdl.devise = pc.devise;
            mdl.journal = pc.journal;
            mdl.Libelle = pc.Libelle;            
            mdl.NumPiece = pc.NumPiece;

            mdl.ecritures = new List<Ecriture>();
            foreach (MdlEcriture ec in pc.ecritures)
            {
                Ecriture mdlec = new Ecriture();
                mdlec.codecompta = ec.codecompta;
                mdlec.Credit = ec.Credit;
                mdlec.Debit = ec.Debit;
                mdlec.Libelle = ec.Libelle;
                mdlec.taxe = ec.taxe;
                mdlec.Idcodecompta = ec.Idcodecompta;

                mdl.ecritures.Add(mdlec);
            }
            return mdl;



        }


        public static MdlPieceComptable GetMdlPieceComptable(PieceComptable pc)
        {
            MdlPieceComptable mdl = new MdlPieceComptable();

            mdl.devise = pc.devise;
            mdl.journal = pc.journal;
            mdl.Libelle = pc.Libelle;
            mdl.LibelleMdl = pc.Libelle;
            mdl.NumPiece = pc.NumPiece;

            mdl.ecritures = new List<MdlEcriture>();
            foreach (Ecriture ec in pc.ecritures)
            {
                MdlEcriture mdlec = new MdlEcriture();
                mdlec.codecompta = ec.codecompta;
                mdlec.Idcodecompta = ec.Idcodecompta;
                mdlec.Credit = ec.Credit;
                mdlec.Debit = ec.Debit;
                mdlec.Libelle = ec.Libelle;
                mdlec.taxe = ec.taxe;

                mdl.ecritures.Add(mdlec);
            }


            return mdl;


           
        }

        public static void DeleteMdlPieceComptable(MdlPieceComptable pc)
        {
            if (pc.Id != -1)
            {
                BasCommon_DAL.DAC.DeleteMdlPieceComptable(pc);
                modeles.Remove(pc);
            }
        }

        public static void InsertMdlPieceComptable(MdlPieceComptable pc)
        {
            if (pc.Id == -1)
            {
                BasCommon_DAL.DAC.AddMdlPieceComptable(pc);

                foreach (MdlEcriture ec in pc.ecritures)
                {
                    ec.piece = pc;
                    EcrituresMgmt.InsertMdlEcriture(ec);
                }

                modeles.Add(pc);
            }
        }


        public static void InsertPieceComptable(PieceComptable pc)
        {
            if (pc.Id == -1)
            {
                BasCommon_DAL.DAC.AddPieceComptable(pc);

                foreach (Ecriture ec in pc.ecritures)
                {
                    ec.piece = pc;
                    EcrituresMgmt.InsertEcriture(ec);
                }
            }
        }


        

        public static void UpdatePieceComptable(PieceComptable pc)
        {
            if (pc.Id != -1)
            {
                BasCommon_DAL.DAC.UpdatePieceComptable(pc);
            }
        }

        public static void DeletePieceComptable(PieceComptable pc)
        {
            if (pc.Id != -1)
            {
                BasCommon_DAL.DAC.DeletePieceComptable(pc);
            }
        }

    }
}
