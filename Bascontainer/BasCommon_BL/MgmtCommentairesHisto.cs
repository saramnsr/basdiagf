using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using BasCommon_DAL;
using System.Data;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class MgmtCommentairesHisto
    {



        private static List<CommentHisto> _CommentsParDefaut = null;
        public static List<CommentHisto> CommentsParDefaut
        {
            get
            {
                if (_CommentsParDefaut == null) _CommentsParDefaut = getCommentsParDefaut();
                return _CommentsParDefaut;
            }
            set
            {
                _CommentsParDefaut = value;
            }
        }

        public static void UpdateCommentairePatient(int idPatient,CommentHisto.CommentHistoType type)
        {
            DAC.UpdateCommentairePatient(idPatient, type);
        }
        public static void UpdateCommentaire(CommentHisto comment)
        {
            if (comment.Id == -1) return;
            DAC.UpdateCommentHisto(comment);
        }

        public static void InsertCommentaire(CommentHisto comment)
        {
            if (comment == null || comment.Id != -1) return;
            if ((comment.patient != null) && (comment.patient.CommentsHisto!=null))
                comment.patient.CommentsHisto.Add(comment);
            DAC.InsertCommentaires(comment);
        }


        public static List<CommentHisto> GetAllCommentaires(basePatient patient,CommentHisto.CommentHistoType tpe)
        {
            List<CommentHisto> lst = new List<CommentHisto>();
            foreach (CommentHisto comm in patient.CommentsHisto)
            {
                if (comm.typecomment == tpe) 
                    lst.Add(comm);
            }

            return lst;
 
        }
        public static List<CommentHisto> GetAllUnreadCommentaires()
        {
            List<CommentHisto> lst = new List<CommentHisto>();
            JArray json = DAC.getMethodeJsonArray("/allUnreadComments");
            foreach (JObject r in json)
            {
                CommentHisto ec = Builders.BuildCommentHisto.BuildUnreadJ(r);
                lst.Add(ec);

            }

            return lst;


        }
        public static List<CommentHisto> GetAllCommentaires(basePatient patient)
        {
            List<CommentHisto> lst = new List<CommentHisto>();
            JArray json = DAC.getMethodeJsonArray("/allCommentsByPatient/" + patient.Id);


            foreach (JObject r in json)
            {
                CommentHisto ec = Builders.BuildCommentHisto.BuildJ(r);
                lst.Add(ec);

            }

            return lst;


        }
        public static List<CommentHisto> GetAllCommentairesOLD(basePatient patient)
        {
            List<CommentHisto> lst = new List<CommentHisto>();
            DataTable dt = DAC.GetCommentaires(patient.Id);


            foreach (DataRow r in dt.Rows)
            {
                CommentHisto ec = Builders.BuildCommentHisto.Build(r);
                lst.Add(ec);

            }

            return lst;


        }



        public static List<CommentHisto> getCommentsParDefaut(CommentHisto.CommentHistoType typecomment)
        {
            List<CommentHisto> lst = new List<CommentHisto>();

            foreach (CommentHisto ch in CommentsParDefaut)
            {
                if (ch.typecomment == typecomment)
                    lst.Add(ch);
            }

            return lst;
        }

        private static List<CommentHisto> getCommentsParDefaut()
        {

            List<CommentHisto> lstAll = new List<CommentHisto>();

            DataTable dt = DAC.GetCommentairesWithoutPatient();


            foreach (DataRow dr in dt.Rows)
            {
                CommentHisto ch = Builders.BuildCommentHisto.Build(dr);
                lstAll.Add(ch);
            }

            return lstAll;


        }



        public static CommentHisto.CommentHistoImportance FindMostImportantFlag(basePatient patient, CommentHisto.CommentHistoType typecomment)
        {
            if (patient.CommentsHisto == null)
                patient.CommentsHisto = GetAllCommentaires(patient);

            CommentHisto.CommentHistoImportance lastimportance = CommentHisto.CommentHistoImportance.undefined;

            foreach (CommentHisto ch in patient.CommentsHisto)
            {
                if ((ch.typecomment == typecomment) &&
                    (ch.Importance > lastimportance) &&
                    (ch.DateDeFin == null) &&
                    ((ch.DateDeDebut == null) || (ch.DateDeDebut.Value.Date <= DateTime.Now.Date)))
                    lastimportance = ch.Importance;
            }

            return lastimportance;
        }



        public static void Delete(CommentHisto comment,basePatient pat)
        {
            DAC.DeleteCommentaires(comment);
            pat.CommentsHisto.Remove(comment);
        }

       


        public static CommentHisto GetLastCommentaire(basePatient patient, CommentHisto.CommentHistoType typecomment)
        {
            if (patient.CommentsHisto == null)
                patient.CommentsHisto = GetAllCommentaires(patient);


            CommentHisto selectedcomment = null;
            DateTime currentdte = DateTime.MinValue;
            foreach (CommentHisto ch in patient.CommentsHisto)
            {
                if ((ch.DateCommentaire > currentdte)
                    && (ch.typecomment == typecomment))
                {
                    currentdte = ch.DateCommentaire;
                    selectedcomment = ch;
                }
            }

            return selectedcomment;



        }

        public static CommentHisto GetLastCommentaire(basePatient patient, CommentHisto.CommentHistoType typecomment, out int nbcomments)
        {

            if (patient.CommentsHisto == null)
                patient.CommentsHisto = GetAllCommentaires(patient);

            nbcomments = 0;

            CommentHisto selectedcomment = null;
            DateTime currentdte = DateTime.MinValue;
            foreach (CommentHisto ch in patient.CommentsHisto)
            {
                if ((ch.DateCommentaire > currentdte)
                    && (ch.typecomment == typecomment))
                {
                    currentdte = ch.DateCommentaire;
                    selectedcomment = ch;
                    nbcomments++;

                }
            }

            return selectedcomment;



        }


        public static List<CommentHisto> GetallLastCommentaires(basePatient patient)
        {
            if (patient.CommentsHisto == null)
                patient.CommentsHisto = GetAllCommentaires(patient);

            Dictionary<CommentHisto.CommentHistoType, CommentHisto> dico = new Dictionary<CommentHisto.CommentHistoType, CommentHisto>();


            foreach (CommentHisto ch in patient.CommentsHisto)
            {
                bool ok = false;
                foreach (KeyValuePair<CommentHisto.CommentHistoType, CommentHisto> kv in dico)
                {
                    if (kv.Key == ch.typecomment)
                    {
                        if (ch.DateCommentaire > kv.Value.DateCommentaire)
                            dico[kv.Key] = ch;
                        ok = true;
                    }
                }

                if (!ok)
                    dico[ch.typecomment] = ch;
            }

            List<CommentHisto> lstFinal = new List<CommentHisto>();
            foreach (KeyValuePair<CommentHisto.CommentHistoType, CommentHisto> kv in dico)
                lstFinal.Add(kv.Value);

            return lstFinal;
        }




       

       
    }
}
