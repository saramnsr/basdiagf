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

            DataTable dt = DAC.GetCodesPresta();

            foreach (DataRow r in dt.Rows)
            {
                CodePrestation cs = Builders.BuildCodePresta(r);
                lst.Add(cs);

            }
            return lst;

        }



        static List<Appareil> GetAppareilsOf(TemplateActePG template)
        {
            List<Appareil> lst = new List<Appareil>();
            DataTable dt = DAC.FindAppareilsFor(template);
            foreach (DataRow r in dt.Rows)
            {
                if (!(r["ID_APPAREIL"] is DBNull))
                {
                    int idappareil = Convert.ToInt32(r["ID_APPAREIL"]);
                    lst.Add(AppareilMgmt.getAppareil(idappareil));
                }
            }
            return lst;
        }

        static List<TemplateActePG> GetAllTemplateActePG()
        {
            List<TemplateActePG> lst = new List<TemplateActePG>();

            DataTable dt = DAC.get_acte_gestion();

            foreach (DataRow r in dt.Rows)
            {
                TemplateActePG cs = Builders.BuildTemplateActePG(r);
                cs.SuggestedAppareils = GetAppareilsOf(cs);
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
            return null;
        }
    }
}
