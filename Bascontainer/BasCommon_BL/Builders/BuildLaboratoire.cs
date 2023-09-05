using BasCommon_BO;
using BasCommon_BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{
        public static class BuildLaboratoire
        {
            public static Laboratoire Build(DataRow r)
            {
                //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
                Laboratoire lab = new Laboratoire();
                lab.id = r["ID_LABORATOIRE"] is DBNull ? -1 : Convert.ToInt32(r["ID_LABORATOIRE"]);
                lab.nom = r["NOM_LABORATOIRE"] is DBNull ? "" : Convert.ToString(r["NOM_LABORATOIRE"]).Trim();

                return lab;
            }
            public static Laboratoire BuildJ(JObject r)
            {
                //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
                Laboratoire lab = new Laboratoire();
                lab.id = r["idLaboratoire"].ToString() == ""  ? -1 : Convert.ToInt32(r["idLaboratoire"]);
                lab.nom = r["nomLaboratoire"].ToString() == "" ? "" : Convert.ToString(r["nomLaboratoire"]).Trim();

                return lab;
            }
        }

        public static class BuildBaseLaboratoire
        {
            public static BaseLaboratoire Build(DataRow r)
            {
                //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
                BaseLaboratoire blab = new BaseLaboratoire();
                blab.idLaboratoire = Convert.ToInt32(r["ID_LABORATOIRE"]);
                blab.id =  Convert.ToInt32(r["ID"]);
                blab.idpatient =Convert.ToInt32(r["ID_PATIENT"]);
                blab.montant = r["MONTANT"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT"]);
                blab.date = r["DATE"] is DBNull ? DateTime.Now : Convert.ToDateTime(r["DATE"]);
                blab.patient = baseMgmtPatient.GetPatient(blab.idpatient);
                blab.laboratoire = MgmtBaseLaboratoire.lstLaboratoires.Find(w => w.id == blab.idLaboratoire);
            
                return blab;
            }
            public static BaseLaboratoire BuildJ(JObject r)
            {
                //  "id": 1,  "idLaboratoire": 1,  "montant": 1000.0,  
                //  "date": "2019-05-06 23:00:00", "id_patient": 1022386      
          
                BaseLaboratoire blab = new BaseLaboratoire();
                blab.idLaboratoire = Convert.ToInt32(r["idLaboratoire"]);
                blab.id = Convert.ToInt32(r["id"]);
                blab.idpatient = Convert.ToInt32(r["id_patient"]);
                blab.montant = r["montant"].ToString() == "" ? 0 : Convert.ToDouble(r["montant"]);
                blab.date = r["date"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(r["date"]);
                blab.patient = baseMgmtPatient.GetPatient(blab.idpatient);
                blab.laboratoire = MgmtBaseLaboratoire.lstLaboratoires.Find(w => w.id == blab.idLaboratoire);

                return blab;
            }
        }
    
}
