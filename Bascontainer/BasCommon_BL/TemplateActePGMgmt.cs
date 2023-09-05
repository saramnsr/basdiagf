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
    public static class TemplateApctePGMgmt
    {

        private static List<CodePrestation> _codeprestations;
        public static List<CodePrestation> codeprestations
        {
            get
            {
                if (_codeprestations == null) _codeprestations = GetCodesPresta();
                return _codeprestations;
            }
           

        }

        private static List<TemplateActePG> _templates;
        public static List<TemplateActePG> templates
        {
            get
            {
                if (_templates == null) _templates = GetAllTemplateActePG();
                return _templates;
            }
            set
            {
                _templates = value;
            }
          
        }

        public static List<TemplateActePG> getPhasesTraitement(BasCommon_BO.Traitement.EnumPhase phase)
        {
            List<TemplateActePG> lst = new List<TemplateActePG>();


            foreach (TemplateActePG tp in templates)
            {
                if (tp.phase == phase)
                {
                    lst.Add(tp);
                }
            }
            return lst;


        }


        public static List<TemplateActePG> getPhasesTraitement()
        {
            List<TemplateActePG> lst = new List<TemplateActePG>();


            foreach (TemplateActePG tp in templates)
            {
                if (tp.phase != BasCommon_BO.Traitement.EnumPhase.Aucune)
                {
                    lst.Add(tp);
                }
            }
            return lst;


        }


        static List<CodePrestation> GetCodesPresta()
        {
            List<CodePrestation> lst = new List<CodePrestation>();

            JArray json = DAC.getMethodeJsonArray("/codePrestations");
            foreach (JObject r in json)
            {
                CodePrestation cs = Builders.BuildCodePresta.Build(r);
                lst.Add(cs);

            }
            return lst;

        }

        static List<CodePrestation> GetCodesPrestaOLD()
        {
            List<CodePrestation> lst = new List<CodePrestation>();

            DataTable dt = DAC.GetCodesPresta();

            foreach (DataRow r in dt.Rows)
            {
                CodePrestation cs = Builders.BuildCodePresta.Build(r);
                lst.Add(cs);

            }
            return lst;

        }

        static List<TemplateActePG> GetAllTemplateActePG()
        {
            List<TemplateActePG> lst = new List<TemplateActePG>();

            JArray json = DAC.getMethodeJsonArray("/ActesGestion");
            foreach (JObject r in json)
            {
                TemplateActePG cs = Builders.BuildTemplateActePG.BuildJ(r);
                lst.Add(cs);

            }
            return lst;

        }
        static List<TemplateActePG> GetAllTemplateActePGOLD()
        {
            List<TemplateActePG> lst = new List<TemplateActePG>();

            DataTable dt = DAC.get_acte_gestion();

            foreach (DataRow r in dt.Rows)
            {
                TemplateActePG cs = Builders.BuildTemplateActePG.Build(r);
                lst.Add(cs);

            }
            return lst;

        }

        public static CodePrestation getCodePrestation(string code)
        {
            foreach (CodePrestation cp in codeprestations)
            {
                if (cp.Code.ToUpper() == code.ToUpper())
                    return cp;

            }
            return null;
        }

        public static TemplateActePG getCodeSecu(int id)
        {
            foreach (TemplateActePG cp in templates)
            {
                if (cp.Id == id)
                    return cp;

            }
            return null;
        }

        public static TemplateActePG getCodeSecu(string code)
        {
            foreach (TemplateActePG cp in templates)
            {
                if (cp.Nom == code)
                    return cp;

            }
            return null;
        }


        public static TemplateActePG getTemplatesActeGestion(string code)
        {
            foreach (TemplateActePG cp in templates)
            {
                if (cp.Nom == code)
                    return cp;

            }
            return null;
        }

        public static TemplateActePG getTemplatesActeGestion(int id)
        {
            foreach (TemplateActePG cp in templates)
            {
                if (cp.Id == id)
                    return cp;

            }
            throw new System.Exception("Cet acte n'existe pas : " + id.ToString());
            return null;
        }
        public static void insertTemplateActePG(TemplateActePG tmp)
        {
            DAC.insert(tmp);
        }
    }
}
