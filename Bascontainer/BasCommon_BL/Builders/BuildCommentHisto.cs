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

    public static class BuildCommentHisto
    {

        public static CommentHisto Build(DataRow r)
        {
            CommentHisto pa = new CommentHisto();
            pa.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
            pa.Id_Ecrivain = r["ID_WRITER"] is DBNull ? -1 : Convert.ToInt32(r["ID_WRITER"]);
            pa.IdPatient = r["ID_PATIENT"] is DBNull ? -1 : Convert.ToInt32(r["ID_PATIENT"]);
            pa.typecomment = r["TYPE_COMMENT"] is DBNull ? CommentHisto.CommentHistoType.undefined : (CommentHisto.CommentHistoType)Convert.ToInt32(r["TYPE_COMMENT"]);
            pa.DateCommentaire = r["DATE_COMMENT"] is DBNull ? DateTime.Now : Convert.ToDateTime(r["DATE_COMMENT"]);
            pa.comment = r["COMMENT"] is DBNull ? "" : Convert.ToString(r["COMMENT"]).Trim();

            pa.DateDeFin = r["DATEFIN"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEFIN"]);
            pa.DateDeDebut = r["DATEDEBUT"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEDEBUT"]);
            pa.Importance = r["IMPORTANCE"] is DBNull ? CommentHisto.CommentHistoImportance.undefined : (CommentHisto.CommentHistoImportance)Convert.ToInt32(r["IMPORTANCE"]);
            pa.Code = r["CODECOMMENTAIRE"] is DBNull ? "" : Convert.ToString(r["CODECOMMENTAIRE"]);
            pa.IdParent = r["PARENT"] is DBNull ? -1 : Convert.ToInt32(r["PARENT"]);
            return pa;
        }
        public static CommentHisto BuildJ(JObject r)
        {
            CommentHisto pa = new CommentHisto();
            pa.Id = r["id"].ToString() == "" ? -1 : Convert.ToInt32(r["id"]);
            pa.Id_Ecrivain = r["id_Ecrivain"].ToString() == "" ? -1 : Convert.ToInt32(r["id_Ecrivain"]);
            pa.IdPatient = r["idPatient"].ToString() == "" ? -1 : Convert.ToInt32(r["idPatient"]);
            pa.typecomment = r["typecomment"].ToString() == "" ? CommentHisto.CommentHistoType.undefined : (CommentHisto.CommentHistoType)Convert.ToInt32(r["typecomment"]);
            pa.DateCommentaire = r["dateCommentaire"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(r["dateCommentaire"]);
            pa.comment =  Convert.ToString(r["comment"]).Trim();

            pa.DateDeFin = r["dateDeFin"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateDeFin"]);
            pa.DateDeDebut = r["dateDeDebut"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateDeDebut"]);
            pa.Importance = r["importance"].ToString() == "" ? CommentHisto.CommentHistoImportance.undefined : (CommentHisto.CommentHistoImportance)Convert.ToInt32(r["importance"]);
            pa.Code =  Convert.ToString(r["code"]);
            pa.sender = r["sender"].ToString() == "" ? CommentHisto.CommentHistosender.inside : (CommentHisto.CommentHistosender)Convert.ToInt32(r["sender"]);
         //   pa.rea = r["isread"].ToString() == "" ? false : (CommentHisto.CommentHistosender)Convert.ToInt32(r["sender"]);

            pa.IdParent = r["idParent"].ToString() == "" ? -1 : Convert.ToInt32(r["idParent"]);
            return pa;
        }
        public static CommentHisto BuildUnreadJ(JObject r)
        {
            CommentHisto pa = new CommentHisto();
            pa.Id = r["id"].ToString() == "" ? -1 : Convert.ToInt32(r["id"]);
            pa.Id_Ecrivain = r["id_Ecrivain"].ToString() == "" ? -1 : Convert.ToInt32(r["id_Ecrivain"]);
            pa.IdPatient = r["idPatient"].ToString() == "" ? -1 : Convert.ToInt32(r["idPatient"]);
            pa.typecomment = r["typecomment"].ToString() == "" ? CommentHisto.CommentHistoType.undefined : (CommentHisto.CommentHistoType)Convert.ToInt32(r["typecomment"]);
            pa.DateCommentaire = r["dateCommentaire"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(r["dateCommentaire"]);
            pa.comment = Convert.ToString(r["comment"]).Trim();

            pa.DateDeFin = r["dateDeFin"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateDeFin"]);
            pa.DateDeDebut = r["dateDeDebut"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateDeDebut"]);
            pa.Importance = r["importance"].ToString() == "" ? CommentHisto.CommentHistoImportance.undefined : (CommentHisto.CommentHistoImportance)Convert.ToInt32(r["importance"]);
            pa.Code = Convert.ToString(r["code"]);
            pa.sender = r["sender"].ToString() == "" ? CommentHisto.CommentHistosender.inside : (CommentHisto.CommentHistosender)Convert.ToInt32(r["sender"]);
             pa.isread  = r["isread"].ToString() == "" ?  true :Convert.ToBoolean(Convert.ToInt32(r["isread"]));
             pa.strpatient  = r["patient"].ToString() == "" ? "" : Convert.ToString(r["patient"]);
             if (pa.sender == CommentHisto.CommentHistosender.tocabinet)
                 if (pa.Id_Ecrivain != -1)
                 {
                     Correspondant cor = MgmtCorrespondants.getCorrespondant(pa.Id_Ecrivain);
                     pa.strsender = cor.Nom + " " + cor.Prenom;
                 }
                 else
                 {
                     pa.strsender = pa.strpatient;
                 }
            pa.IdParent = r["idParent"].ToString() == "" ? -1 : Convert.ToInt32(r["idParent"]);
            return pa;
        }

    }
}
