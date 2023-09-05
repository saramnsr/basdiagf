using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BasCommon_BL
{
    public static class Q1CSMgmt
    {

        public static string GetResume(int IdPatient)
        {
            string htmlEncoded = BasCommon_DAL.DAC.getMethodeJsonString("/GetResumeQ1CS/" + IdPatient);
            string original = HttpUtility.HtmlDecode(htmlEncoded).Replace("<br>", "\r\n").Replace("<br/>", "");
            return original;
        }

        public static string GetResumeOLD(int IdPatient)
        {
            string htmlEncoded = BasCommon_DAL.DAC.GetResumeQ1CS(IdPatient);
            string original = HttpUtility.HtmlDecode(htmlEncoded).Replace("<br>","\r\n").Replace("<br/>","");

            return original;
        }
    }
}
