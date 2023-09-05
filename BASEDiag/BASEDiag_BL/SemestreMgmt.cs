using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;
using BasCommon_BO;
using BasCommon_BL;

namespace BASEDiag_BL
{
    public static class SemestreMgmt
    {


        public static void AssocierDEP(Semestre sem, EntentePrealable entente)
        {
            DAC.AssocierDEP(sem, entente);
        }

        public static void AddSemestre(Semestre sem)
        {
            DAC.AddSemestre(sem);

        }

        public static void DelSemestre(Semestre sem)
        {
            DAC.DeleteSemestre(sem);
        }

        public static void UpdateSemestre(Semestre sem)
        {
            DAC.UpdateSemestre(sem);

        }





        public static List<Semestre> getSemestres(Traitement trmnt)
        {
            List<Semestre> lst = new List<Semestre>();
            DataTable dt = DAC.getSemestres(trmnt);

            foreach (DataRow dr in dt.Rows)
            {
                Semestre s = Builders.BuildSemestre(dr);
                lst.Add(s);
            }
            return lst;
        }


        public static double getTotalSansRemise(Semestre s)
        {
            double _TarifTotal = 0;
            TemplateActePG surv = TemplateApctePGMgmt.getCodeSecu("SURV");



            _TarifTotal += s.traitementSecu.Valeur;


            foreach (Surveillance su in s.surveillances)
                _TarifTotal += su.traitementSecu.Valeur;



            return _TarifTotal;
        }


       
      
      

                
       

        public static double getTotal(Semestre s)
        {
            double _TarifTotal = 0;




            //TemplateActePG surv = TemplateApctePGMgmt.getCodeSecu("SURV");


            _TarifTotal += s.Montant_Honoraire;

            foreach (Surveillance su in s.surveillances)
                _TarifTotal += su.Montant_Honoraire;



            return _TarifTotal;
        }

        public static double GetPartSecu(Semestre s)
        {
            double _TarifTotal = 0;


            _TarifTotal += s.traitementSecu.Coeff * s.traitementSecu.Code.Valeur;

            foreach (Surveillance su in s.surveillances)
                _TarifTotal += su.traitementSecu.Coeff * su.traitementSecu.Code.Valeur;




            return _TarifTotal;
        }



    }

}
