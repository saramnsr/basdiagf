using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_DAL;

namespace BasCommon_BL
{
    public static class ExportExcel
    {




        public static void Encaissement(List<object[]> array, string file)
        {
            DACExcel.CreateEncaissement(file);

            foreach (object[] obj in array)
                DACExcel.InsertEncaissement(obj, file);
        }


        public static void EncaissementTiers(List<object[]> array, string file)
        {
            DACExcel.CreateEncaissementTiers(file);

            foreach (object[] obj in array)
                DACExcel.InsertEncaissementTiers(obj, file);
        }

        public static void APrelever(List<object[]> array, string file)
        {
            DACExcel.CreateAPrelever(file);

            foreach (object[] obj in array)
                DACExcel.InsertAPrelever(obj, file);
        }

        public static void RemisEnBanque(List<object[]> array, string file)
        {
            DACExcel.CreateRemisEnBanque(file);

            foreach (object[] obj in array)
                DACExcel.InsertRemisEnBanque(obj, file);
        }

        public static void AremettreEnBanque(List<object[]> array, string file)
        {
            DACExcel.CreateAremettreEnBanque(file);

            foreach (object[] obj in array)
                DACExcel.InsertAremettreEnBanque(obj, file);
        }
    }
}
