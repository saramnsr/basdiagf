using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class MgmtDiags
    {
        public static Diagnostic getDiag(int id) 
        {
            JObject obj = BasCommon_DAL.DAC.getMethodeJsonObjet("/DiagnosticById/"+id);

            return Builders.BuildDiag.Build(obj);
                        
        }

        public static Diagnostic getDiagOld(int id)
        {
            DataRow dr = DAC.getDiag(id);
            if (dr == null) return null;
            Diagnostic diag = Builders.BuildDiag.Build(dr);

            return diag;

        }

        public static List<Objectif> getObjectifs(int id) {

            List<Objectif> liste = new List<Objectif>();

            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray("/ObjectifsByIdDiag/"+id);
            foreach (JObject obj in jArray) {

                Objectif o = Builders.BuildDiag.BuildObjectif(obj);
                liste.Add(o);
            
            }
            return liste;        
        }

        public static List<Objectif> getObjectifsOld(int id)
        {
            List<Objectif> _objectifs = new List<Objectif>();
            DataTable dt = DAC.getObjectifs(id);
            foreach (DataRow r in dt.Rows)
            {
                Objectif objectif = Builders.BuildDiag.BuildObjectif(r);
                _objectifs.Add(objectif);
            }
            return _objectifs;
        }
        public static ObjectifDetail getObjcdetail(int id_diag, int id_obj)
        {
            DataRow dr = DAC.getDiagObj(id_diag, id_obj);
            if (dr == null) return null;
            ObjectifDetail obd = Builders.BuildDiag.BuildOD(dr);
            return obd;
        }
        public static void ajouterObjectif(Objectif objectif, Diagnostic diag)
        {
            DAC.InsertObjectif(objectif);
        }
    }
}
