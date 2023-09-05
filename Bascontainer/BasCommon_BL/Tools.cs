using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BL
{
    public static class Tools
    {


        public static string testconnection()
        {

            try
            {
                BasCommon_DAL.DAC.getConnection();

                return BasCommon_DAL.DAC.connectionString;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }


}
