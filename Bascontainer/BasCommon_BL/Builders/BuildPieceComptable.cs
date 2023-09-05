using BasCommon_BL.Compta;
using BasCommon_BO.Compta;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL.Builders
{

    public static class BuildPieceComptable
    {
        public static PieceComptable Build(JObject obj)
        {
            PieceComptable pieceComptable = new PieceComptable();
            

            pieceComptable.Id = Convert.ToInt32(obj["id"]);
            pieceComptable.Libelle = String.IsNullOrEmpty(obj["libelle"].ToString()) ? "" : Convert.ToString(obj["libelle"]).Trim();
            pieceComptable.DateOperation = String.IsNullOrEmpty(obj["dateOperation"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(obj["dateOperation"]);

            pieceComptable.DateEcheance = String.IsNullOrEmpty(obj["dateEcheance"].ToString()) ? DateTime.MinValue : (DateTime?)Convert.ToDateTime(obj["dateEcheance"]);
            pieceComptable.devise = DeviseMgmt.getDevise(Convert.ToString(obj["devise"]));
            pieceComptable.journal = JournalMgmt.getJournal(Convert.ToString(obj["numJournal"]));

            pieceComptable.NumPiece = Convert.ToString(obj["numPiece"]);
            pieceComptable.NumSaisie = Convert.ToInt32(obj["numSaisie"]);
            pieceComptable.Fog = Convert.ToString(obj["fog"]) == "T";

            return pieceComptable;

        }

        public static PieceComptable Build(DataRow r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            PieceComptable act = new PieceComptable();
            act.Id = r["id"] is DBNull ? -1 : Convert.ToInt32(r["id"]);
            act.Libelle = r["LIBELLE"] is DBNull ? "" : Convert.ToString(r["LIBELLE"]).Trim();
            act.DateOperation = Convert.ToDateTime(r["DATEOPERATION"]);
            act.DateEcheance = r["DATEECHEANCE"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEECHEANCE"]);
            act.devise = DeviseMgmt.getDevise(Convert.ToString(r["DEVISE"]));
            act.journal = JournalMgmt.getJournal(Convert.ToString(r["NUMJOURNAL"]));
            act.NumPiece = Convert.ToString(r["NUMPIECE"]);
            act.NumSaisie = Convert.ToInt32(r["NUMSAISIE"]);
            act.Fog = Convert.ToString(r["FOG"]) == "T";
            return act;
        }

        public static PieceComptable BuildJson(JObject r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            PieceComptable act = new PieceComptable();
            act.Id = r["id"].ToString() == "" ? -1 : Convert.ToInt32(r["id"]);
            act.Libelle = r["libelle"].ToString() == "" ? "" : Convert.ToString(r["libelle"]).Trim();
            act.DateOperation = Convert.ToDateTime(r["dateoperation"]);
            act.DateEcheance = r["dateecheance"].ToString()=="" ? null : (DateTime?)Convert.ToDateTime(r["dateecheance"]);
            act.devise = DeviseMgmt.getDevise(Convert.ToString(r["devise"]));
            act.journal = JournalMgmt.getJournal(Convert.ToString(r["numjournal"]));
            act.NumPiece = Convert.ToString(r["numpiece"]);
            act.NumSaisie = Convert.ToInt32(r["numsaisie"]);
            act.Fog = Convert.ToString(r["fog"]) == "T";
            return act;
        }

    }

    public static class BuildMdlPieceComptable
    {
        public static MdlPieceComptable Build(DataRow r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            MdlPieceComptable act = new MdlPieceComptable();
            act.Id = r["id"] is DBNull ? -1 : Convert.ToInt32(r["id"]);
            act.Libelle = r["LIBELLE"] is DBNull ? "" : Convert.ToString(r["LIBELLE"]).Trim();
            act.devise = r["DEVISE"] is DBNull ? null : DeviseMgmt.getDevise(Convert.ToString(r["DEVISE"]));
            act.journal = r["NUMJOURNAL"] is DBNull ? null : JournalMgmt.getJournal(Convert.ToString(r["NUMJOURNAL"]));
            act.LibelleMdl = r["LIBELLEMDL"] is DBNull ? "" : Convert.ToString(r["LIBELLEMDL"]);
            act.NumPiece = r["NUMPIECE"] is DBNull ? "" : Convert.ToString(r["NUMPIECE"]);
            act.Organisation = r["Organisation"] is DBNull ? "" : Convert.ToString(r["Organisation"]);

            return act;
        }
    }
    
   
}
