using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.InteropServices;
using BasCommon_BO;
using BasCommon_BL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{
    public static class BuildFamillesActe
    {
        public static FamillesActe Build(JObject obj)
        {
            FamillesActe act = new FamillesActe();

            act.Id = obj["id"] == null ? -1 : Convert.ToInt32(obj["id"]);
            act.libelle = String.IsNullOrEmpty(obj["nom"].ToString()) ? "" : Convert.ToString(obj["nom"]).Trim();
            act.ParentFamillesActeId = obj["parentId"] == null ? -1 : Convert.ToInt32(obj["parentId"]);
            act.couleur = obj["couleur"] == null ? System.Drawing.Color.WhiteSmoke : System.Drawing.Color.FromArgb(Convert.ToInt32(obj["couleur"]));
            try
            {
                act.couleur = Convert.ToInt32(obj["couleur"]) == 0 ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(obj["couleur"]));
            }
            catch (Exception e)
            {
                act.couleur = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(obj["couleur"]));
            }

            act.ordre = obj["ordre"] == null ? -1 : Convert.ToInt32(obj["ordre"]);

            return act;
        }
        public static FamillesActe Build(DataRow r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            FamillesActe act = new FamillesActe();
            act.Id = r["id"] is DBNull ? -1 : Convert.ToInt32(r["id"]);
            act.libelle = r["Nom"] is DBNull ? "" : Convert.ToString(r["Nom"]).Trim();
            act.ParentFamillesActeId = r["Parent"] is DBNull ? -1 : Convert.ToInt32(r["Parent"]);
            act.couleur = r["couleur"] is DBNull ? System.Drawing.Color.WhiteSmoke : System.Drawing.Color.FromArgb(Convert.ToInt32(r["couleur"]));
            try
            {
                act.couleur = Convert.ToInt32(r["couleur"]) == 0 ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["couleur"]));
            }
            catch (Exception e)
            {
                act.couleur = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(r["couleur"]));
            }

            act.ordre = r["ordre"] is DBNull ? -1 : Convert.ToInt32(r["ordre"]);
            return act;
        }
        public static FamillesActe BuildFamilleActeJ(JObject r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            FamillesActe act = new FamillesActe();
            act.Id = Convert.ToInt32(r["id"]);
            act.libelle = r["nom"].ToString() == "null" ? "" : Convert.ToString(r["nom"]).Trim();
            act.ParentFamillesActeId = Convert.ToInt32(r["parent"]);
            act.couleur = Convert.ToInt32(r["couleur"]) == 0 ? System.Drawing.Color.WhiteSmoke : System.Drawing.Color.FromArgb(Convert.ToInt32(r["couleur"]));
            try
            {
                act.couleur = Convert.ToInt32(r["couleur"]) == 0 ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["couleur"]));
            }
            catch (Exception e)
            {
                act.couleur = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(r["couleur"]));
            }

            act.ordre = Convert.ToInt32(r["ordre"]);
            return act;
        }


    }




    public static class BuildActe
    {

        public static Acte BuildActeJ(JObject r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            Acte act = new Acte();
            act.id_acte = Convert.ToInt32(r["id_ACTE"]);
            act.acte_libelle = Convert.ToString(r["acte_LIBELLE"]).Trim();
            act.acte_durestd = r["acte_DURESTD"].ToString() == "" ? 0 : Convert.ToInt32(r["acte_DURESTD"]); act.acte_couleur = Convert.ToInt32(r["acte_COULEUR"]) == 0 ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["acte_COULEUR"]));
            try
            {
                act.acte_couleur = Convert.ToInt32(r["acte_COULEUR"]) == 0 ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["acte_COULEUR"]));
            }
            catch (Exception e)
            {
                act.acte_couleur = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(r["acte_COULEUR"]));
            }
            act.type_acte = Convert.ToInt32(r["type_ACTE"]);
            act.nb_fautbloc = r["nb_FAUTBLOC"].ToString() == "" ? -1 : Convert.ToDouble(r["nb_FAUTBLOC"]);
            act.code_planing = Convert.ToString(r["code_PLANNING"]).Trim();
            act.temps_chrono = r["temps_CHRONO"].ToString() == "" ? -1 : Convert.ToInt32(r["temps_CHRONO"]);
            act.id_famille = r["id_FAMILLE_ACTE"].ToString() == "" ? -1 : Convert.ToInt32(r["id_FAMILLE_ACTE"]);
            act.tps_ass = r["tps_ASS"].ToString() == "" ? 0 : Convert.ToInt32(r["tps_ASS"]);
            act.tps_prat = r["tps_PRAT"].ToString() == "" ? 0 : Convert.ToInt32(r["tps_PRAT"]);


            act.prix_acte = r["prix_ACTE"].ToString() == "" ? 0 : Convert.ToDouble(r["prix_ACTE"]);
            act.MailConfirmationAttachments = Convert.ToString(r["mailattachement"]);
            act.MailConfirmationRDVBody = Convert.ToString(r["mailbody"]);
            act.MailConfirmationSubject = Convert.ToString(r["mailsubject"]);
            act.acte_libelle_estimation = Convert.ToString(r["acte_LIBELLE_ESTIMATION"]).Trim();
            act.acte_libelle_facture = Convert.ToString(r["acte_LIBELLE_FACTURE"]).Trim();
            act.cotation = r["cotation"].ToString() == "" ? "0" : Convert.ToString(r["cotation"]).Trim();
            act.coefficient = r["coefficient"].ToString() == "" ? -1 : Convert.ToInt32(r["coefficient"]);
            act.nomenclature = Convert.ToString(r["nomenclature"]).Trim();
            act.nom_raccourci = Convert.ToString(r["shortlib"]).Trim();
            act.emplacement = Convert.ToString(r["id_FAUTEUIL"]);
            act.praticien = Convert.ToString(r["praticien"]);
            act.jour = Convert.ToString(r["jours"]);

            act.ordre = r["ordre"].ToString() == "" ? 0 : Convert.ToInt32(r["ordre"]);
            act.nombre_points = r["nombre_POINTS"].ToString() == "" ? "0" : Convert.ToString(Convert.ToDouble((r["nombre_POINTS"])));
            act.quantite = r["quantite"].ToString() == "" ? "1" : Convert.ToString(r["quantite"]);
            act.heure_debut = r["heure_DEBUT"].ToString() == "" ? "00:00" : Convert.ToString(r["heure_DEBUT"]);
            act.heure_fin = r["heure_FIN"].ToString() == "" ? "00:00" : Convert.ToString(r["heure_FIN"]);
            act.acte_libelle_estimation = Convert.ToString(r["acte_LIBELLE_ESTIMATION"]).Trim();
            act.acte_libelle_facture = Convert.ToString(r["acte_LIBELLE_FACTURE"]).Trim();
            act.BaseRemboursement = r["acte_BASE_REMBOURSEMENT"].ToString() == "" ? 0 : Convert.ToDouble(r["acte_BASE_REMBOURSEMENT"]);
            act.Remboursement = r["acte_REMBOURSEMENT"].ToString() == "" ? 0 : Convert.ToDouble(r["acte_REMBOURSEMENT"]);
            act.Depassement = r["acte_DEPASSEMENT"].ToString() == "" ? 0 : Convert.ToDouble(r["acte_DEPASSEMENT"]);
            act.CodeTransposition = Convert.ToString(r["acte_CODE_TRANSPOSOTION"]);
            act.Tarif = r["tarif"].ToString() == "" ? 0 : Convert.ToDouble(r["tarif"]);
            return act;
        }
        public static Acte Build(DataRow r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            Acte act = new Acte();
            act.id_acte = r["id_acte"] is DBNull ? -1 : Convert.ToInt32(r["id_acte"]);
            act.acte_libelle = r["acte_libelle"] is DBNull ? "" : Convert.ToString(r["acte_libelle"]).Trim();
            act.acte_durestd = r["acte_durestd"] is DBNull ? 0 : Convert.ToInt32(r["acte_durestd"]);
            act.acte_couleur = r["acte_couleur"] is DBNull ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["acte_couleur"]));
            try
            {
                act.acte_couleur = Convert.ToInt32(r["acte_COULEUR"]) == 0 ? System.Drawing.Color.Black : System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(r["acte_COULEUR"]));
            }
            catch (Exception e)
            {
                act.acte_couleur = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(r["acte_COULEUR"]));
            }
            act.type_acte = r["type_acte"] is DBNull ? -1 : Convert.ToInt32(r["type_acte"]);
            act.nb_fautbloc = r["nb_fautbloc"] is DBNull ? -1 : Convert.ToDouble(r["nb_fautbloc"]);
            act.code_planing = r["code_planing"] is DBNull ? "" : Convert.ToString(r["code_planing"]).Trim();
            act.temps_chrono = r["temps_chrono"] is DBNull ? -1 : Convert.ToInt32(r["temps_chrono"]);
            act.id_famille = r["id_famille_acte"] is DBNull ? -1 : Convert.ToInt32(r["id_famille_acte"]);
            act.tps_ass = r["tps_ass"] is DBNull ? 0 : Convert.ToInt32(r["tps_ass"]);
            act.tps_prat = r["tps_prat"] is DBNull ? 0 : Convert.ToInt32(r["tps_prat"]);


            act.prix_acte = r["prix_acte"] is DBNull ? 0 : Convert.ToDouble(r["prix_acte"]);
            act.MailConfirmationAttachments = r["mailattachement"] is DBNull ? "" : Convert.ToString(r["mailattachement"]);
            act.MailConfirmationRDVBody = r["mailbody"] is DBNull ? "" : Convert.ToString(r["mailbody"]);
            act.MailConfirmationSubject = r["mailsubject"] is DBNull ? "" : Convert.ToString(r["mailsubject"]);
            act.acte_libelle_estimation = r["ACTE_LIBELLE_ESTIMATION"] is DBNull ? "" : Convert.ToString(r["ACTE_LIBELLE_ESTIMATION"]).Trim();
            act.acte_libelle_facture = r["ACTE_LIBELLE_FACTURE"] is DBNull ? "" : Convert.ToString(r["ACTE_LIBELLE_FACTURE"]).Trim();
            act.cotation = r["COTATION"] is DBNull ? "0" : Convert.ToString(r["COTATION"]).Trim();
            act.coefficient = r["COEFFICIENT"] is DBNull ? -1 : Convert.ToInt32(r["COEFFICIENT"]);
            act.nomenclature = r["nomenclature"] is DBNull ? "" : Convert.ToString(r["nomenclature"]).Trim();
            act.nom_raccourci = r["SHORTLIB"] is DBNull ? "" : Convert.ToString(r["SHORTLIB"]).Trim();
            act.emplacement = r["Id_FAUTEUIL"] is DBNull ? "" : Convert.ToString(r["Id_FAUTEUIL"]);
            act.praticien = r["PRATICIEN"] is DBNull ? "" : Convert.ToString(r["PRATICIEN"]);
            act.jour = r["JOURS"] is DBNull ? "" : Convert.ToString(r["JOURS"]);

            act.ordre = r["ORDRE"] is DBNull ? 0 : Convert.ToInt32(r["ORDRE"]);
            act.nombre_points = r["NOMBRE_POINTS"] is DBNull ? "0" : Convert.ToString(Convert.ToDouble((r["NOMBRE_POINTS"])));
            act.quantite = r["QUANTITE"] is DBNull ? "1" : Convert.ToString(r["QUANTITE"]);
            act.heure_debut = r["HEURE_DEBUT"] is DBNull ? "00:00" : Convert.ToString(r["HEURE_DEBUT"]);
            act.heure_fin = r["HEURE_FIN"] is DBNull ? "00:00" : Convert.ToString(r["HEURE_FIN"]);
            act.acte_libelle_estimation = r["ACTE_LIBELLE_ESTIMATION"] is DBNull ? "" : Convert.ToString(r["ACTE_LIBELLE_ESTIMATION"]).Trim();
            act.acte_libelle_facture = r["ACTE_LIBELLE_FACTURE"] is DBNull ? "" : Convert.ToString(r["ACTE_LIBELLE_FACTURE"]).Trim();
            act.BaseRemboursement = r["ACTE_BASE_REMBOURSEMENT"] is DBNull ? 0 : Convert.ToDouble(r["ACTE_BASE_REMBOURSEMENT"]);
            act.Remboursement = r["ACTE_REMBOURSEMENT"] is DBNull ? 0 : Convert.ToDouble(r["ACTE_REMBOURSEMENT"]);
            act.Depassement = r["ACTE_DEPASSEMENT"] is DBNull ? 0 : Convert.ToDouble(r["ACTE_DEPASSEMENT"]);
            act.CodeTransposition = r["ACTE_CODE_TRANSPOSOTION"] is DBNull ? "" : Convert.ToString(r["ACTE_CODE_TRANSPOSOTION"]);
            act.Tarif = r["TARIF"] is DBNull ? 0 : Convert.ToDouble(r["TARIF"]);
            return act;
        }
        public static ActeTraitement BuildActeTraitement(DataRow r)
        {
            ActeTraitement act = new ActeTraitement();
            Build(r);
            act.prix_traitement = r["prix_acte"] is DBNull ? 0 : Convert.ToDouble(r["prix_acte"]);
            return act;
        }
        public static ActeGroupement BuildActeGroupement(DataRow r)
        {
            ActeGroupement act = null;
            act = new ActeGroupement(Build(r));
            act.prixTraitement = r["PRIX_TRAITEMENT"] is DBNull ? 0 : Convert.ToDouble(r["PRIX_TRAITEMENT"]);
            act.idParent = r["IDPARENT"] is DBNull ? 0 : Convert.ToInt32(r["IDPARENT"]);
            act.id_famille = act.idParent;
            act.qte = r["QTE"] is DBNull ? 0 : Convert.ToInt32(r["QTE"]);
            return act;
        }

        public static ActeGroupement BuildActeGroupement(JObject obj)
        {
            int idActe = Convert.ToInt32(obj["idActe"]);
            ActeGroupement acteGrp = new ActeGroupement(BasCommon_BL.ActesMgmt.Actes.Find(x => x.id_acte == idActe));

            acteGrp.id = Convert.ToInt32(obj["id"]);
            acteGrp.id_acte = Convert.ToInt32(obj["idActe"]);
            acteGrp.prixTraitement = Convert.ToDouble(obj["prixTraitement"]);
            acteGrp.idParent = obj["idParent"] == null || Convert.ToInt32(obj["idParent"]) == 0 ? -1 : Convert.ToInt32(obj["idParent"]);
            acteGrp.qte = obj["qte"] == null ? 0 : Convert.ToInt32(obj["qte"]);

            acteGrp.famille_Acte = acteGrp.idParent == -1 ? null : BasCommon_BL.ActesMgmt.famillesacte.Find(x => x.Id == acteGrp.idParent);

            return acteGrp;
        }
    }
}
