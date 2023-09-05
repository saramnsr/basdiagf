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

    public static class BuildAccessObject
    {
        public static AccessObject Build(DataRow r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            AccessObject act = new AccessObject();
            act.Password = r["Password"] is DBNull ? "" : Convert.ToString(r["Password"]);
            act.RHBas_AllowToDeleteRDV = r["RHBASE_ALLOWDELETERDV"] is DBNull ? false : Convert.ToString(r["RHBASE_ALLOWDELETERDV"]) == "T";
            act.RHBas_AllowToMoveRDV = r["RHBASE_ALLOWMOVERDV"] is DBNull ? false : Convert.ToString(r["RHBASE_ALLOWMOVERDV"]) == "T";
            act.Bas_Stat_AllowFinances = r["BASE_STAT_ALLOWFINANCE"] is DBNull ? false : Convert.ToString(r["BASE_STAT_ALLOWFINANCE"]) == "T";
            act.BasPract_ListControles = r["BASEPRACT_ALLOWLISTFINANCE"] is DBNull ? false : Convert.ToString(r["BASEPRACT_ALLOWLISTFINANCE"]) == "T";
            act.BasPract_ListFinancieres = r["BASEPRACT_LISTFINANCE"] is DBNull ? false : Convert.ToString(r["BASEPRACT_LISTFINANCE"]) == "T";
            act.BasPract_HistoriqueFinances = r["BASEPRACT_ALLOWHISTOFINANCE"] is DBNull ? false : Convert.ToString(r["BASEPRACT_ALLOWHISTOFINANCE"]) == "T";
            act.Bas_Stat_AllowBPTransfert = r["BPTRANSFERT"] is DBNull ? false : Convert.ToString(r["BPTRANSFERT"]) == "T";
            act.BasPract_Comptabilite = r["BASEPRACT_COMPTABILITE"] is DBNull ? false : Convert.ToString(r["BASEPRACT_COMPTABILITE"]) == "T";
            act.CanDeleteEncaissement = r["BASEPRACT_CanDeleteEncaissement"] is DBNull ? false : Convert.ToString(r["BASEPRACT_CanDeleteEncaissement"]) == "T";
            act.CanDeleteActe = r["BASEPRACT_CanDeleteActe"] is DBNull ? false : Convert.ToString(r["BASEPRACT_CanDeleteActe"]) == "T";
            act.RHBas_AllowAccessRH = r["RHBASE_ACCES_RH"] is DBNull ? false : Convert.ToString(r["RHBASE_ACCES_RH"]) == "T";
            act.RHBas_AllowAccessStatusClinique = r["STATUS_CLINIQUE"] is DBNull ? false : Convert.ToString(r["STATUS_CLINIQUE"]) == "T";
            act.RHBas_AllowToRaccourcirRDV = r["RHBas_AllowToRaccourcirRDV"] is DBNull ? false : Convert.ToString(r["RHBas_AllowToRaccourcirRDV"]) == "T";
            act.Utilisateur = UtilisateursMgt.getUtilisateur(r["IDUtilisateur"] is DBNull ? -1 : Convert.ToInt32(r["IDUtilisateur"]));
            act.SUPER_ADMIN = r["SUPER_USER"] is DBNull ? false : Convert.ToString(r["SUPER_USER"]) == "T";




            return act;
        }
        public static AccessObject BuildJ(JObject r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            AccessObject act = new AccessObject();
            act.Password = Convert.ToString(r["password"]);
            act.RHBas_AllowToDeleteRDV = r["rhbaseAllowdeleterdv"].ToString() == "" ? false : (Convert.ToString(r["rhbaseAllowdeleterdv"]) == "T" || Convert.ToString(r["rhbaseAllowdeleterdv"]) == "1");
            act.RHBas_AllowToMoveRDV = r["rhbaseAllowmoverdv"].ToString() == "" ? false :(Convert.ToString(r["rhbaseAllowmoverdv"]) == "T" || Convert.ToString(r["rhbaseAllowmoverdv"]) == "1");
            act.Bas_Stat_AllowFinances = r["base_statAllowfinance"].ToString() == "" ? false :(Convert.ToString(r["base_statAllowfinance"]) == "T" || Convert.ToString(r["base_statAllowfinance"]) == "1");
            act.BasPract_ListControles = r["basepractAllowlistfinance"].ToString() == "" ? false :(Convert.ToString(r["basepractAllowlistfinance"]) == "T" || Convert.ToString(r["basepractAllowlistfinance"]) == "1");
            act.BasPract_ListFinancieres = r["basepractListfinance"].ToString() == "" ? false : (Convert.ToString(r["basepractListfinance"]) == "T" || Convert.ToString(r["basepractListfinance"]) == "1");
            act.BasPract_HistoriqueFinances = r["basepractAllowhistofinance"].ToString() == "" ? false : (Convert.ToString(r["basepractAllowhistofinance"]) == "T" || Convert.ToString(r["basepractAllowhistofinance"]) == "1");
            act.Bas_Stat_AllowBPTransfert = r["bptransfert"].ToString() == "" ? false : (Convert.ToString(r["bptransfert"]) == "T" || Convert.ToString(r["bptransfert"]) == "1");
            act.BasPract_Comptabilite = r["basepractComptabilite"].ToString() == "" ? false : (Convert.ToString(r["basepractComptabilite"]) == "T" || Convert.ToString(r["basepractComptabilite"]) == "1");
            act.CanDeleteEncaissement = r["basepractCandeleteencaissement"].ToString() == "" ? false : (Convert.ToString(r["basepractCandeleteencaissement"]) == "T" || Convert.ToString(r["basepractCandeleteencaissement"]) == "1");
            act.CanDeleteActe = r["basepractCandeleteacte"].ToString() == "" ? false :(Convert.ToString(r["basepractCandeleteacte"]) == "T" || Convert.ToString(r["basepractCandeleteacte"]) == "1");
            act.RHBas_AllowAccessRH = r["rhbaseAcces_rh"].ToString() == "" ? false : (Convert.ToString(r["rhbaseAcces_rh"]) == "T" || Convert.ToString(r["rhbaseAcces_rh"]) == "1");
            act.RHBas_AllowAccessStatusClinique = r["statusClinique"].ToString() == "" ? false : (Convert.ToString(r["statusClinique"]) == "T" || Convert.ToString(r["statusClinique"]) == "1");
            act.RHBas_AllowToRaccourcirRDV = r["rhbasAllowtoraccourcirrdv"].ToString() == "" ? false : (Convert.ToString(r["rhbasAllowtoraccourcirrdv"]) == "T" || Convert.ToString(r["rhbasAllowtoraccourcirrdv"]) == "1");

            act.RHBas_AllowToDeleteRDV = String.IsNullOrEmpty(r["rhbaseAllowdeleterdv"].ToString()) || r["rhbaseAllowdeleterdv"].ToString() == "0" ? false : true;            
           // act.RHBas_AllowToDeleteRDV = r["rhbaseAllowdeleterdv"].ToString() == "" ? false : Convert.ToString(r["rhbaseAllowdeleterdv"]) == "T";

            act.RHBas_AllowToMoveRDV = String.IsNullOrEmpty(r["rhbaseAllowmoverdv"].ToString()) || r["rhbaseAllowmoverdv"].ToString() == "0" ? false : true;
           // act.RHBas_AllowToMoveRDV = r["rhbaseAllowmoverdv"].ToString() == "" ? false : Convert.ToString(r["rhbaseAllowmoverdv"]) == "T";

            act.Bas_Stat_AllowFinances = String.IsNullOrEmpty(r["base_statAllowfinance"].ToString()) || r["base_statAllowfinance"].ToString() == "0" ? false : true;
           // act.Bas_Stat_AllowFinances = r["base_statAllowfinance"].ToString() == "" ? false : Convert.ToString(r["base_statAllowfinance"]) == "T";


            act.BasPract_ListControles = String.IsNullOrEmpty(r["basepractAllowlistfinance"].ToString()) || r["basepractAllowlistfinance"].ToString() == "0" ? false : true;
           // act.BasPract_ListControles = r["basepractAllowlistfinance"].ToString() == "" ? false : Convert.ToString(r["basepractAllowlistfinance"]) == "T";

            act.BasPract_ListFinancieres = String.IsNullOrEmpty(r["basepractListfinance"].ToString()) || r["basepractListfinance"].ToString() == "0" ? false : true;
            // act.BasPract_ListFinancieres = r["basepractListfinance"].ToString() == "" ? false : Convert.ToString(r["basepractListfinance"]) == "T";

            act.BasPract_HistoriqueFinances = String.IsNullOrEmpty(r["basepractAllowhistofinance"].ToString()) || r["basepractAllowhistofinance"].ToString() == "0" ? false : true;
           // act.BasPract_HistoriqueFinances = r["basepractAllowhistofinance"].ToString() == "" ? false : Convert.ToString(r["basepractAllowhistofinance"]) == "T";

            act.Bas_Stat_AllowBPTransfert = String.IsNullOrEmpty(r["bptransfert"].ToString()) || r["bptransfert"].ToString() == "0" ? false : true;
          //  act.Bas_Stat_AllowBPTransfert = r["bptransfert"].ToString() == "" ? false : Convert.ToString(r["bptransfert"]) == "T";


            act.BasPract_Comptabilite = String.IsNullOrEmpty(r["basepractComptabilite"].ToString()) || r["basepractComptabilite"].ToString() == "0" ? false : true;
          //  act.BasPract_Comptabilite = r["basepractComptabilite"].ToString() == "" ? false : Convert.ToString(r["basepractComptabilite"]) == "T";

            act.CanDeleteEncaissement = String.IsNullOrEmpty(r["basepractCandeleteencaissement"].ToString()) || r["basepractCandeleteencaissement"].ToString() == "0" ? false : true;
          //  act.CanDeleteEncaissement = r["basepractCandeleteencaissement"].ToString() == "" ? false : Convert.ToString(r["basepractCandeleteencaissement"]) == "T";

            act.CanDeleteActe = String.IsNullOrEmpty(r["basepractCandeleteencaissement"].ToString()) || r["basepractCandeleteacte"].ToString() == "0" ? false : true;
           // act.CanDeleteActe = r["basepractCandeleteacte"].ToString() == "" ? false : Convert.ToString(r["basepractCandeleteacte"]) == "T";

            act.RHBas_AllowAccessRH = String.IsNullOrEmpty(r["rhbaseAcces_rh"].ToString()) || r["rhbaseAcces_rh"].ToString() == "0" ? false : true;
           // act.RHBas_AllowAccessRH = r["rhbaseAcces_rh"].ToString() == "" ? false : Convert.ToString(r["rhbaseAcces_rh"]) == "T";

            act.RHBas_AllowAccessStatusClinique = String.IsNullOrEmpty(r["statusClinique"].ToString()) || r["statusClinique"].ToString() == "0" ? false : true;
           // act.RHBas_AllowAccessStatusClinique = r["statusClinique"].ToString() == "" ? false : Convert.ToString(r["statusClinique"]) == "T";


            act.RHBas_AllowToRaccourcirRDV = String.IsNullOrEmpty(r["statusClinique"].ToString()) || r["rhbasAllowtoraccourcirrdv"].ToString() == "0" ? false : true;
           // act.RHBas_AllowToRaccourcirRDV = r["rhbasAllowtoraccourcirrdv"].ToString() == "" ? false : Convert.ToString(r["rhbasAllowtoraccourcirrdv"]) == "T";
           
            act.Utilisateur = UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["idutilisateur"]));
            act.SUPER_ADMIN = r["superUser"].ToString() == "" ? false : Convert.ToString(r["superUser"]) == "T" || Convert.ToString(r["superUser"]) == "1";




            return act;
        }
    }
}
