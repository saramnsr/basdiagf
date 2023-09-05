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

    public static class BuildEcriture
    {
        public static Ecriture Build(DataRow r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            Ecriture act = new Ecriture();

            act.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
            act.Libelle = r["LIBELLE"] is DBNull ? "" : Convert.ToString(r["LIBELLE"]).Trim();
            act.Type = r["TYPEREGLEMENT"] is DBNull ? Ecriture.TypeReglement.Inconnus : (Ecriture.TypeReglement)Enum.Parse(typeof(Ecriture.TypeReglement), Convert.ToString(r["TYPEREGLEMENT"]));
            act.codecompta = r["CODECOMPTA"] is DBNull ? null : MgmtCodeComptable.getFromCode(Convert.ToString(r["CODECOMPTA"]));
            act.Idcodecompta = Convert.ToString(r["CODECOMPTA"]);
            act.Credit = r["CREDIT"] is DBNull ? null : (double?)Convert.ToDouble(r["CREDIT"]);
            act.Debit = r["DEBIT"] is DBNull ? null : (double?)Convert.ToDouble(r["DEBIT"]);
            act.IdPieceComptable = r["ID_PIECE_COMPTA"] is DBNull ? -1 : Convert.ToInt32(r["ID_PIECE_COMPTA"]);
            act.taxe = r["ID_TAXEVALEURAJOUTEE"] is DBNull ? null : TaxeValeurAjouteeMgmt.getTaxe(Convert.ToString(r["ID_TAXEVALEURAJOUTEE"]));
            return act;
        }

        public static Ecriture Build(JObject obj)
        {            

                Ecriture e = new Ecriture();

                e.Id = Convert.ToInt32(obj["id"]);
                e.Libelle = String.IsNullOrEmpty(obj["libelle"].ToString()) ? "" : obj["libelle"].ToString().Trim();
                e.Type = String.IsNullOrEmpty(obj["typeReglement"].ToString())  ? Ecriture.TypeReglement.Inconnus : (Ecriture.TypeReglement)Convert.ToInt32(obj["typeReglement"]);

                e.codecompta = String.IsNullOrEmpty(obj["codeCompta"].ToString()) ? null : MgmtCodeComptable.getFromCode(Convert.ToString(obj["codeCompta"])); ;
                e.Idcodecompta = String.IsNullOrEmpty(obj["codeCompta"].ToString()) ? "" : obj["codeCompta"].ToString();
                e.Credit = String.IsNullOrEmpty(obj["credit"].ToString()) ? 0 : (double?)Convert.ToDouble(obj["credit"]);
                e.Debit = String.IsNullOrEmpty(obj["debit"].ToString()) ? 0 :(double?)Convert.ToDouble(obj["debit"]);

               
                PieceComptable p = Builders.BuildPieceComptable.Build((JObject)obj["pieceComptable"]);
                e.IdPieceComptable = p.Id;
                e.piece = p;

                e.taxe = String.IsNullOrEmpty(obj["taxe"].ToString()) ? null : Builders.BuildTaxeValeurAjoutee.Build((JObject)obj["taxe"]);

                return e;
            }
        }

        public static class BuildMdlEcriture
        {
            public static MdlEcriture Build(DataRow r)
            {
                //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
                MdlEcriture act = new MdlEcriture();
                act.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
                act.Libelle = r["LIBELLE"] is DBNull ? "" : Convert.ToString(r["LIBELLE"]).Trim();
                act.Idcodecompta = Convert.ToString(r["CODECOMPTA"]);
                act.codecompta = r["CODECOMPTA"] is DBNull ? null : MgmtCodeComptable.getFromCode(Convert.ToString(r["CODECOMPTA"]));
                act.Credit = r["CREDIT"] is DBNull ? null : (double?)Convert.ToDouble(r["CREDIT"]);
                act.Debit = r["DEBIT"] is DBNull ? null : (double?)Convert.ToDouble(r["DEBIT"]);
                act.IdPieceComptable = r["ID_MDL_PIECE_COMPTA"] is DBNull ? -1 : Convert.ToInt32(r["ID_MDL_PIECE_COMPTA"]);
                act.taxe = r["ID_TAXEVALEURAJOUTEE"] is DBNull ? null : TaxeValeurAjouteeMgmt.getTaxe(Convert.ToString(r["ID_TAXEVALEURAJOUTEE"]));
                return act;
            }
        }
    }

