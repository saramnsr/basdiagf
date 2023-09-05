using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class MgmtControlFinance
    {


    
        public static void Insert(lnkControlFinancier lnkctrl)
        {
            DAC.InsertlnkControlFinancier(lnkctrl);
        }
               
        public static void Insert(ControlFinancier ctrl)
        {
            if (ctrl.Id == -1)
            {
                DAC.InsertControlFinancier(ctrl);

                InsertLinks(ctrl);
            }
        }

      


        private static void InsertLinks(ControlFinancier ctrl)
        {
            if (ctrl.lnkctrlPaiement != null)
            {
                foreach (lnkControlFinancier lnk in ctrl.lnkctrlPaiement)
                {
                    lnk.IdControlFinancier = ctrl.Id;
                    Insert(lnk);
                }
            }
        }

        public static void Delete(ControlFinancier ctrl)
        {
            if (ctrl.Id == -1)
                DAC.DeleteControlFinancier(ctrl);
        }

        public static void Update(ControlFinancier ctrl)
        {
            if (ctrl.Id > 0)
            {
                DAC.UpdateControlFinancier(ctrl);
                InsertLinks(ctrl);
            }

        }

        public static ControlFinancier GetControlFinancier(BordereauFinance bf)
        {

            DataRow dr = DAC.getControlFinancier(bf);

            if (dr == null) return null;
            return Builders.BuildControlFinancier.Build(dr);
                

        }
        public static List<ControlFinancier> GetControlFinancier(PaiementReel pr)
        {
            List<ControlFinancier> lst = new List<ControlFinancier>();

            JArray json = DAC.getMethodeJsonArray("/ControlsFinancierByPr/" + pr.Id);

            foreach (JObject dr in json)
            {
                lst.Add(Builders.BuildControlFinancier.Build(dr));
            }

            return lst;


        }
        public static List<ControlFinancier> GetControlFinancierOLD(PaiementReel pr)
        {
            List<ControlFinancier> lst = new List<ControlFinancier>();

            DataTable dt = DAC.getControlsFinancier(pr);

            foreach (DataRow dr in dt.Rows)
            {
              lst.Add( Builders.BuildControlFinancier.Build(dr));
            }

            return lst;


        }
        
    }
}
