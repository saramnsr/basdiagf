using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{

    public static class BuildBanque
    {
        public static Banque Build(DataRow dr)
        {
            Banque b = new Banque();
            b.Libelle = Convert.ToString(dr["NOM"]);
            b.Id = Convert.ToInt32(dr["ID_BANQUENOM"]);

            return b;
        }
        public static Banque BuildJ(JObject dr)
        {
            Banque b = new Banque();
            b.Libelle = Convert.ToString(dr["nom"]);
            b.Id = Convert.ToInt32(dr["idBanquenom"]);

            return b;
        }
        
    }

    public static class BuildBanqueDeRemise
    {
        public static BanqueDeRemise Build(DataRow r)
        {
            /*
            CREATE TABLE BANQUE (
        COD_BAN     CHAR(10) CHARACTER SET NONE NOT NULL,
        LIB_BAN     CHAR(30) CHARACTER SET NONE,
        ADR1_BAN    CHAR(30) CHARACTER SET NONE,
        ADR2_BAN    CHAR(30) CHARACTER SET NONE,
        CP_BAN      CHAR(5) CHARACTER SET NONE,
        VIL_BAN     CHAR(30) CHARACTER SET NONE,
        NUMA_BAN    CHAR(6) CHARACTER SET NONE,
        NUMCPT_BAN  CHAR(11) CHARACTER SET NONE,
        NUMGUI_BAN  CHAR(5) CHARACTER SET NONE
            */

            BanqueDeRemise apg = new BanqueDeRemise();
            apg.Code = r["COD_BAN"] is DBNull ? "" : Convert.ToString(r["COD_BAN"]).Trim();
            apg.Libelle = r["LIB_BAN"] is DBNull ? "" : Convert.ToString(r["LIB_BAN"]).Trim();
            apg.AddrBAN1 = r["ADR1_BAN"] is DBNull ? "" : Convert.ToString(r["ADR1_BAN"]).Trim();
            apg.AddrBAN2 = r["ADR2_BAN"] is DBNull ? "" : Convert.ToString(r["ADR2_BAN"]).Trim();
            apg.CodePostal = r["CP_BAN"] is DBNull ? "" : Convert.ToString(r["CP_BAN"]);
            apg.Ville = r["VIL_BAN"] is DBNull ? "" : Convert.ToString(r["VIL_BAN"]);
            apg.NumA = r["NUMA_BAN"] is DBNull ? "00000" : Convert.ToString(r["NUMA_BAN"]);
            apg.NumCPT = r["NUMCPT_BAN"] is DBNull ? "00000000000" : Convert.ToString(r["NUMCPT_BAN"]);
            apg.NumGui = r["NUMGUI_BAN"] is DBNull ? "00000" : Convert.ToString(r["NUMGUI_BAN"]);
            apg.NumCle = r["NUMCLE_BAN"] is DBNull ? "00" : Convert.ToString(r["NUMCLE_BAN"]);
            apg.CodePays = r["CODEPAYS"] is DBNull ? "00" : Convert.ToString(r["CODEPAYS"]);
            apg.CodeBIC = r["CODEBIC"] is DBNull ? "00" : Convert.ToString(r["CODEBIC"]);
            apg.Titulaire = r["Titulaire"] is DBNull ? "" : Convert.ToString(r["Titulaire"]);
            apg.NumNNE = r["NUM_NNE"] is DBNull ? "000000" : Convert.ToString(r["NUM_NNE"]);
            apg.journalComptable = BasCommon_BL.Compta.JournalMgmt.getJournal(Convert.ToString(r["journalComptable"]));
            apg.CompteComptable = r["CompteComptable"] is DBNull ? "512000" : Convert.ToString(r["CompteComptable"]);
            
            return apg;
        }
        public static BanqueDeRemise BuildJ(JObject r)
        {
            /*
            CREATE TABLE BANQUE (
        COD_BAN     CHAR(10) CHARACTER SET NONE NOT NULL,
        LIB_BAN     CHAR(30) CHARACTER SET NONE,
        ADR1_BAN    CHAR(30) CHARACTER SET NONE,
        ADR2_BAN    CHAR(30) CHARACTER SET NONE,
        CP_BAN      CHAR(5) CHARACTER SET NONE,
        VIL_BAN     CHAR(30) CHARACTER SET NONE,
        NUMA_BAN    CHAR(6) CHARACTER SET NONE,
        NUMCPT_BAN  CHAR(11) CHARACTER SET NONE,
        NUMGUI_BAN  CHAR(5) CHARACTER SET NONE
            */

            BanqueDeRemise apg = new BanqueDeRemise();
            apg.Code = r["codBan"].ToString() == "" ? "" : Convert.ToString(r["codBan"]).Trim();
            apg.Libelle = r["libBan"].ToString() == "" ? "" : Convert.ToString(r["libBan"]).Trim();
            apg.AddrBAN1 = r["adr1Ban"].ToString() == "" ? "" : Convert.ToString(r["adr1Ban"]).Trim();
            apg.AddrBAN2 = r["adr2Ban"].ToString() == "" ? "" : Convert.ToString(r["adr2Ban"]).Trim();
            apg.CodePostal = r["cpBan"].ToString() == "" ? "" : Convert.ToString(r["cpBan"]);
            apg.Ville = r["vilBan"].ToString() == "" ? "" : Convert.ToString(r["vilBan"]);
            apg.NumA = r["numaBan"].ToString() == "" ? "00000" : Convert.ToString(r["numaBan"]);
            apg.NumCPT = r["numcptBan"].ToString() == "" ? "00000000000" : Convert.ToString(r["numcptBan"]);
            apg.NumGui = r["numguiBan"].ToString() == "" ? "00000" : Convert.ToString(r["numguiBan"]);
            apg.NumCle = r["numcleBan"].ToString() == "" ? "00" : Convert.ToString(r["numcleBan"]);
            apg.CodePays = r["codepays"].ToString() == "" ? "00" : Convert.ToString(r["codepays"]);
            apg.CodeBIC = r["codebic"].ToString() == "" ? "00" : Convert.ToString(r["codebic"]);
            apg.Titulaire = r["titulaire"].ToString() == "" ? "" : Convert.ToString(r["titulaire"]);
            apg.NumNNE = r["numNne"].ToString() == "" ? "000000" : Convert.ToString(r["numNne"]);
            apg.journalComptable = BasCommon_BL.Compta.JournalMgmt.getJournal(Convert.ToString(r["journalcomptable"]));
            apg.CompteComptable = r["comptecomptable"].ToString() == "" ? "512000" : Convert.ToString(r["comptecomptable"]);

            return apg;
        }

    }
}
