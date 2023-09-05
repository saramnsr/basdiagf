using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;
using BasCommon_BL;
using BasCommon_BO;

namespace BASEDiag_BL
{
    public static class MgmtCommentairesHisto
    {

        public static void UpdateCommentaire(CommentHisto comment)
        {
            if (comment.Id == -1) return;
            DAC.UpdateCommentHisto(comment);
        }

        public static void InsertCommentaire(CommentHisto comment)
        {
            if (comment.Id != -1) return;
            DAC.InsertCommentaires(comment);
        }

        public static List<CommentHisto> GetAllCommentaires(basePatient patient, CommentHisto.CommentHistoType typecomment)
        {
            DataTable dt = DAC.GettAllCommentaires(patient, typecomment);

            List<CommentHisto> lst = new List<CommentHisto>();

            foreach (DataRow dr in dt.Rows)
            {
                CommentHisto ch = Builders.BuildCommentHisto(dr);
                ch.Ecrivain = UtilisateursMgt.getUtilisateur(ch.Id_Ecrivain);
                lst.Add(ch);
            }

            return lst;
        }

        public static CommentHisto GetLastCommentaire(basePatient patient, CommentHisto.CommentHistoType typecomment)
        {
            DataRow dr = DAC.GetLastCommentaire(patient, typecomment);
            if (dr == null) return null;

            CommentHisto ch = Builders.BuildCommentHisto(dr);
            ch.Ecrivain = UtilisateursMgt.getUtilisateur(ch.Id_Ecrivain);

            return ch;
        }


        public static List<CommentHisto> GetAllCommentaires(basePatient patient)
        {
            DataTable dt = DAC.GettAllCommentaires(patient);

            List<CommentHisto> lst = new List<CommentHisto>();

            foreach (DataRow dr in dt.Rows)
            {
                CommentHisto ch = Builders.BuildCommentHisto(dr);
                lst.Add(ch);
            }

            return lst;
        }
    }
}
