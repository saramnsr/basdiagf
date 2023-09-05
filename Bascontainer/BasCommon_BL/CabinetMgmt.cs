using BasCommon_BO;
using BasCommon_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace BasCommon_BL
{
    public partial class CabinetMgmt
    {
        private static List<Cabinet> _lstCabinet;
        public static List<Cabinet> Cabinet
        {
            get
            {
                if (_lstCabinet == null)
                    _lstCabinet = getAllCabinet();
                return _lstCabinet;
            }
            set
            {
                _lstCabinet = value;
            }
        }
        public static List<Cabinet> getAllCabinet()
        {
            List<Cabinet> lst = new List<BasCommon_BO.Cabinet>();
            
            string FILE_PATH = System.Configuration.ConfigurationManager.AppSettings["cabinets"];
            var xmlString = File.ReadAllText(FILE_PATH);
            var stringReader = new StringReader(xmlString);
            var dsSet = new DataSet();
            dsSet.ReadXml(stringReader);
            DataTable dt = dsSet.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                Cabinet b = Builders.BuildCabinet.Build(dr);
                lst.Add(b);
            }

            return lst;
        }
        public static string token()
        {
            return DAC.token;
        }
        public static string pathRest()
        {
            return DAC.PathRest;
        }
        public static string prefix()
        {
            return DAC.PathRest;
        }
    }
}
