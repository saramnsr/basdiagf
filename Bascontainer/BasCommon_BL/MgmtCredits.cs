using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.IO;
using System.Reflection;

namespace BasCommon_BL
{
    public static class MgmtCredits
    {

        private static List<DefinitionCredit> _definitions;
        public static List<DefinitionCredit> definitions
        {
            get
            {
                if (_definitions == null) 
                    _definitions = GetAlldefinitions();
                return _definitions;
            }
        }


        public static List<DefinitionCredit> GetPnfs()
        {
            List<DefinitionCredit> lst = new List<DefinitionCredit>();
            foreach (DefinitionCredit dc in definitions)
                if (dc.Organisation == "PnF") lst.Add(dc);

            return lst;
        }

        public static List<DefinitionCredit> GetOptalions()
        {

            List<DefinitionCredit> lst = new List<DefinitionCredit>();
            foreach (DefinitionCredit dc in definitions)
                if (dc.Organisation == "Optalion") lst.Add(dc);
            
            return lst;
        }

        private static List<DefinitionCredit> GetAlldefinitions()
        {

            char separator = '\t';
            List<DefinitionCredit> lst = new List<DefinitionCredit>();
            string path = Path.GetDirectoryName(
                     Assembly.GetAssembly(typeof(MgmtCredits)).Location);

            string filePath = path + "\\Credits.ini";
            string line;

            if (File.Exists(filePath))
            {
                StreamReader file = null;
                try
                {
                    file = new StreamReader(filePath);
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] ss = line.Split(separator);


                        try
                        {
                            DefinitionCredit dc = new DefinitionCredit();
                            dc.Organisation = ss[0];
                            dc.Libelle = ss[1];
                            dc.NombreMensualite = Convert.ToInt32(ss[2]);
                            dc.Minimum = Convert.ToDouble(ss[3]);
                            dc.maximum = Convert.ToDouble(ss[4]);
                            lst.Add(dc);
                        }
                        catch (System.Exception) { }

                    }
                }
                finally
                {
                    if (file != null)
                        file.Close();
                }

            }

            return lst;
        }
    }
}
