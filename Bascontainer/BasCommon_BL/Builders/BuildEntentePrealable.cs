using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;

namespace BasCommon_BL.Builders
{
    public static class BuildEntentePrealable
    {

        public static void BuildDiagEntente(DataRow r, ref EntentePrealable ep)
        {

            if (Convert.ToBoolean(r["me_alvemaxpro"])) ep.SensSagittalAlveolaireMax = EntentePrealable.en_ProRetro.Pro;
            if (Convert.ToBoolean(r["me_alvemaxpro"])) ep.SensSagittalAlveolaireMax = EntentePrealable.en_ProRetro.Pro;
            if (Convert.ToBoolean(r["me_alvemaxendo"])) ep.SensTransversalAlveolaireMax = EntentePrealable.en_DiagAlveolaire.Endoalveolie;
            if (Convert.ToBoolean(r["me_alvemaxsupra"])) ep.SensVerticalAlveolaire = EntentePrealable.en_OccFace.Supraclusion;
            if (Convert.ToBoolean(r["me_alvemaxretro"])) ep.SensSagittalAlveolaireMax = EntentePrealable.en_ProRetro.Retro;
            if (Convert.ToBoolean(r["me_alvemaxexo"])) ep.SensTransversalAlveolaireMax = EntentePrealable.en_DiagAlveolaire.Exoalveolie;
            if (Convert.ToBoolean(r["me_alvemandpro"])) ep.SensSagittalAlveolaireMand = EntentePrealable.en_ProRetro.Pro;
            if (Convert.ToBoolean(r["me_alvemandendo"])) ep.SensTransversalAlveolaireMand = EntentePrealable.en_DiagAlveolaire.Endoalveolie;
            if (Convert.ToBoolean(r["me_alvemandinfra"])) ep.SensVerticalAlveolaire = EntentePrealable.en_OccFace.Infraclusion;
            if (Convert.ToBoolean(r["me_alvemandretro"])) ep.SensSagittalAlveolaireMand = EntentePrealable.en_ProRetro.Retro;
            if (Convert.ToBoolean(r["me_alvemandexo"])) ep.SensTransversalAlveolaireMand = EntentePrealable.en_DiagAlveolaire.Exoalveolie;
            if (Convert.ToBoolean(r["me_basmaxpro"])) ep.SensSagittalBasalMax = EntentePrealable.en_ProRetro.Pro;
            if (Convert.ToBoolean(r["me_basmaxendo"])) ep.SensTransversalBasalMax = EntentePrealable.en_DiagOsseux.Endognatie;
            if (Convert.ToBoolean(r["me_basmaxhypo"])) ep.SensVerticalBasal = EntentePrealable.en_Divergence.Hypodivergent;
            if (Convert.ToBoolean(r["me_basmaxretro"])) ep.SensSagittalBasalMax = EntentePrealable.en_ProRetro.Retro;
            if (Convert.ToBoolean(r["me_basmaxexo"])) ep.SensTransversalBasalMax = EntentePrealable.en_DiagOsseux.Exognatie;
            if (Convert.ToBoolean(r["me_basmandpro"])) ep.SensSagittalBasalMand = EntentePrealable.en_ProRetro.Pro;
            if (Convert.ToBoolean(r["me_basmandendo"])) ep.SensTransversalBasalMand = EntentePrealable.en_DiagOsseux.Endognatie;
            if (Convert.ToBoolean(r["me_basmandhyper"])) ep.SensVerticalBasal = EntentePrealable.en_Divergence.Hyperdivergent;
            if (Convert.ToBoolean(r["me_basmandretro"])) ep.SensSagittalBasalMand = EntentePrealable.en_ProRetro.Retro;
            if (Convert.ToBoolean(r["me_basmandexo"])) ep.SensTransversalBasalMand = EntentePrealable.en_DiagOsseux.Exognatie;
            if (Convert.ToBoolean(r["me_mol1"])) ep.ClasseDentaireMolaire = EntentePrealable.en_Class.Class_I;
            if (Convert.ToBoolean(r["me_mol2"])) ep.ClasseDentaireMolaire = EntentePrealable.en_Class.Class_II;
            if (Convert.ToBoolean(r["me_mol3"])) ep.ClasseDentaireMolaire = EntentePrealable.en_Class.Class_III;
            ep.ClasseDentaireMolaireTxt = Convert.ToString(r["me_moltext"]);
            if (Convert.ToBoolean(r["me_can1"])) ep.ClasseDentaireCanine = EntentePrealable.en_Class.Class_I;
            if (Convert.ToBoolean(r["me_can2"])) ep.ClasseDentaireCanine = EntentePrealable.en_Class.Class_II;
            if (Convert.ToBoolean(r["me_can3"])) ep.ClasseDentaireCanine = EntentePrealable.en_Class.Class_III;
            ep.ClasseDentaireCanineTxt = Convert.ToString(r["me_cantext"]);
            if (Convert.ToBoolean(r["me_occludroit"]) && Convert.ToBoolean(r["me_occlugauche"])) ep.occInverse = EntentePrealable.en_OccInverse.Droite_Et_Gauche;
            else
                if (Convert.ToBoolean(r["me_occlugauche"])) ep.occInverse = EntentePrealable.en_OccInverse.Gauche;
                else
                    if (Convert.ToBoolean(r["me_occludroit"])) ep.occInverse = EntentePrealable.en_OccInverse.Droite;
            if (Convert.ToBoolean(r["me_occluanter"])) ep.occInverse = EntentePrealable.en_OccInverse.Anterieur;
            ep.Agenesie = Convert.ToString(r["me_agnesie"]);
            ep.DentsIncluseSurnum = Convert.ToString(r["me_dentincl"]);
            ep.Malposition = Convert.ToString(r["me_malpos"]);
            ep.DDM = Convert.ToBoolean(r["me_dysharmo"]);
            ep.DDD = Convert.ToBoolean(r["me_dysharmodd"]);
            ep.FacteurFonctionnel = Convert.ToString(r["me_facteurfonc"]);
            ep.PlanDeTraitement = Convert.ToString(r["pat_objectif_trait2"]);
            ep.Commentaires = Convert.ToString(r["pat_objectif_comm2"]);
            ep.IdDiag = Convert.ToInt32(r["id"]);



        }

        public static void BuildEntenteWithoutDiag(DataRow r, ref EntentePrealable ep)
        {
            ep.NumDate = Convert.ToInt32(r["EE_NUMDATE"]);
            if (Convert.ToBoolean(r["ee_debuttraitement"])) ep.typetraitement = EntentePrealable.TypeDeTraitement.Debut;
            if (Convert.ToBoolean(r["ee_surveillance"])) ep.typetraitement = EntentePrealable.TypeDeTraitement.Surveillance;
            if (Convert.ToBoolean(r["ee_suite"])) ep.typetraitement = EntentePrealable.TypeDeTraitement.Semestre;
            if (Convert.ToBoolean(r["ee_contention"])) ep.typetraitement = EntentePrealable.TypeDeTraitement.Contention;
            if (Convert.ToBoolean(r["ee_autre"])) ep.typetraitement = EntentePrealable.TypeDeTraitement.Autre;

            int var = 0;

            int.TryParse(Convert.ToString(r["ee_annee"]), out var);
            ep.Contention = var;

            int.TryParse(Convert.ToString(r["ee_numsemestre"]), out var);
            ep.Semestre = var;

            ep.Autre = Convert.ToString(r["ee_autre"]);
            ep.ImmatAssure = Convert.ToString(r["ee_immat"]);
            ep.dateProposition = Convert.ToDateTime(r["ee_dateprop"]);
            ep.DateImpression = r["ee_DateImpression"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["ee_DateImpression"]);
            ep.DateAccord = r["EE_DATEACCORD"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["EE_DATEACCORD"]);


            ep.cotationDesActes = Convert.ToString(r["ee_cotation"]);
            ep.IsDevisSigned = Convert.ToBoolean(r["ee_devis"]);
            ep.Commentaires = Convert.ToString(r["ee_commentaire1"]) + "\n" + Convert.ToString(r["ee_commentaire2"]) + "\n" + Convert.ToString(r["ee_commentaire3"]);
            ep.PlanDeTraitement = Convert.ToString(r["ee_traitement1"]) + "\n" + Convert.ToString(r["ee_traitement2"]) + "\n" + Convert.ToString(r["ee_traitement3"]);

            if (Convert.ToInt32(r["ee_rmo"]) == 0)
                ep.ReferenceNationalOpposable = EntentePrealable.RNO.R;
            if (Convert.ToInt32(r["ee_rmo"]) == 1)
                ep.ReferenceNationalOpposable = EntentePrealable.RNO.HR;
            if (Convert.ToInt32(r["ee_rmo"]) == -1)
                ep.ReferenceNationalOpposable = EntentePrealable.RNO.None;


            ep.IdModele = Convert.ToInt32(r["id"]);
            ep.IdDiag = (r["ID_MODELE_ENVOI"] is DBNull)?-1: Convert.ToInt32(r["ID_MODELE_ENVOI"]);
            ep.idPatient = Convert.ToInt32(r["ee_patient"]);



            ep.Praticien = r["ID_PRATICIEN"] is DBNull ? null : UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["ID_PRATICIEN"]));
        }

    }
}
