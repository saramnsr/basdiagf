﻿using BasCommon_BO;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL
{
    public class AutreMgmt
    {
        private static List<Autre> _tim;
        public static List<Autre> tim
        {
            get
            {
                if (_tim == null) _tim = getAutres("getTimCL");
                return _tim;
            }
        }
        private static List<Autre> _tour;
        public static List<Autre> tour
        {
            get
            {
                if (_tour == null) _tour = getAutres("getTour");
                return _tour;
            }
        }
        private static List<Autre> _portGouttier;
        public static List<Autre> portGouttier
        {
            get
            {
                if (_portGouttier == null) _portGouttier = getAutres("getportGouttier");
                return _portGouttier;
            }
        }
        private static List<Autre> _chght;
        public static List<Autre> chght
        {
            get
            {
                if (_chght == null) _chght = getAutres("getChght");
                return _chght;
            }
        }
        private static List<Autre> _numLogiciel;
        public static List<Autre> numLogiciel
        {
            get
            {
                if (_numLogiciel == null) _numLogiciel = getAutres("getNumLogiciel");
                return _numLogiciel;
            }
        }
        private static List<Autre> _droit;
        public static List<Autre> droit
        {
            get
            {
                if (_droit == null) _droit = getAutres("getTimDroit");
                return _droit;
            }
        }
        private static List<Autre> _gauche;
        public static List<Autre> gauche
        {
            get
            {
                if (_gauche == null) _gauche = getAutres("getTimGauche");
                return _gauche;
            }
        }
        private static List<Autre> _diaporama;
        public static List<Autre> diaporama
        {
            get
            {
                if (_diaporama == null) _diaporama = getAutres("getDiaporama");
                return _diaporama;
            }
        }
        private static List<Autre> _accelerateur;
        public static List<Autre> accelerateur
        {
            get
            {
                if (_accelerateur == null) _accelerateur = getAutres("getAccelerateur");
                return _accelerateur;
            }
        }
        public static Autre GetAutreById(int id, string type = "")
        {

            DataRow dr = DAC.getAutreById(id, type);
            if (dr == null) return null;
            Autre autre = Builders.BuildersAutre.BuildAutre(dr);

            return autre;
        }
        public static List<Autre> GetAutre(string type = "")
        {

            DataTable dt = DAC.getAutre(type);

            List<Autre> lst = new List<Autre>();
            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildersAutre.BuildAutre(r));
            return lst;
        }
        public static List<Autre> getAutres(string from)
        {
            JArray json = DAC.getMethodeJsonArray("/" + from);
            List<Autre> lst = new List<Autre>();
            foreach (JObject r in json)
                lst.Add(Builders.BuildersAutre.BuildAutreJ(r));
            return lst;
        }
        public static List<Autre> getAutresOLD(string from)
        {
            DataTable dt = DAC.getAutres(from);

            List<Autre> lst = new List<Autre>();
            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildersAutre.BuildAutre(r));
            return lst;
        }
    }
}
