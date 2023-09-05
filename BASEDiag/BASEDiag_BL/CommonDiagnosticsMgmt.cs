using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using BASEDiag_BO;
using BASEDiag_DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BASEDiag_BL
{
    public static class CommonDiagnosticsMgmt
    {

        private static List<CommonDiagnostic> _CommonDiagnostics;
        public static List<CommonDiagnostic> CommonDiagnostics
        {
            get
            {
                if (_CommonDiagnostics == null)
                    _CommonDiagnostics = getCommonDiagnostics();

                return _CommonDiagnostics;
            }

        }

        private static List<CommonDiagnostic> getCommonDiagnostics()
        {
            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/getCommonDiagnostics");

            List<CommonDiagnostic> lst = new List<CommonDiagnostic>();
            foreach (JObject r in json)
            {
                lst.Add(Builders.BuildCommonDiagnosticJson(r));
            }
            return lst;
        }


        private static List<CommonDiagnostic> getCommonDiagnosticsOld()
        {
            DataTable dt = DAC.getCommonDiagnostics();

            List<CommonDiagnostic> lst = new List<CommonDiagnostic>();
            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildCommonDiagnostic(r));
            }
            return lst;
        }

        public static CommonDiagnostic getCommonDiagnostics(int Id)
        {
            foreach (CommonDiagnostic f in CommonDiagnostics)
                if (f.Id == Id) return f;

            return null;

        }

        public static List<CommonObjectifFromDiag> getCommonObjectifsAdulte(List<CommonDiagnostic> diags)
        {


            DataTable dt = DAC.getObjectifsFromDiagnosticsAdulte(diags);

            List<CommonObjectifFromDiag> lst = new List<CommonObjectifFromDiag>();

            if ((dt == null)||(dt.Rows.Count == 0)) return lst;


            foreach (DataRow r in dt.Rows)
            {
                CommonObjectifFromDiag cod = Builders.BuildCommonObjectifFromDiag(r);
                lst.Add(cod);
            }
            return lst;
        }

        public static List<CommonObjectifFromDiag> getCommonObjectifsAdulte(CommonDiagnostic diag)
        {


            DataTable dt = DAC.getObjectifsFromDiagnosticsAdulte(diag);

            List<CommonObjectifFromDiag> lst = new List<CommonObjectifFromDiag>();
            foreach (DataRow r in dt.Rows)
            {
                CommonObjectifFromDiag cod = Builders.BuildCommonObjectifFromDiag(r);
                lst.Add(cod);
            }
            return lst;
        }

        public static List<CommonObjectifFromDiag> getCommonObjectifsEnfant(List<CommonDiagnostic> diags)
        {

            string IdDiags = "";
            List<long> ids = new List<long>();
            foreach (CommonDiagnostic d in diags)
            {
               // ids.Add((long)d.Id);
               // string tmpid = "";
                if (d != null)
                {
                    if (IdDiags != "")
                        IdDiags += ",";
                   // tmpid += "'" + d.Id.ToString() + "'";
                    IdDiags += d.Id.ToString();
                }
            }
            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/getObjectifsFromDiagnostics?str=" + IdDiags);

            List<CommonObjectifFromDiag> lst = new List<CommonObjectifFromDiag>();

            if ((json == null) || (json.Count == 0)) return lst;


            foreach (JObject r in json)
            {
                CommonObjectifFromDiag cod = Builders.BuildCommonObjectifFromDiagJson(r);
                lst.Add(cod);
            }
            return lst;
        }


        public static List<CommonObjectifFromDiag> getCommonObjectifsEnfantOLD(List<CommonDiagnostic> diags)
        {


            DataTable dt = DAC.getObjectifsFromDiagnosticsEnfant(diags);

            List<CommonObjectifFromDiag> lst = new List<CommonObjectifFromDiag>();

            if ((dt == null) || (dt.Rows.Count == 0)) return lst;


            foreach (DataRow r in dt.Rows)
            {
                CommonObjectifFromDiag cod = Builders.BuildCommonObjectifFromDiag(r);
                lst.Add(cod);
            }
            return lst;
        }

        public static List<CommonObjectifFromDiag> getCommonObjectifsEnfant(CommonDiagnostic diag)
        {
            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/getCommonObjectifsEnfant/" + diag.Id);

            List<CommonObjectifFromDiag> lst = new List<CommonObjectifFromDiag>();
            foreach (JObject r in json)
            {
                CommonObjectifFromDiag cod = Builders.BuildCommonObjectifFromDiagJson(r);
                lst.Add(cod);
            }
            return lst;
        }

        public static List<CommonObjectifFromDiag> getCommonObjectifsEnfantOld(CommonDiagnostic diag)
        {
            DataTable dt = DAC.getObjectifsFromDiagnosticsEnfant(diag);

            List<CommonObjectifFromDiag> lst = new List<CommonObjectifFromDiag>();
            foreach (DataRow r in dt.Rows)
            {
                CommonObjectifFromDiag cod = Builders.BuildCommonObjectifFromDiag(r);
                lst.Add(cod);
            }
            return lst;
        }

    }
}
