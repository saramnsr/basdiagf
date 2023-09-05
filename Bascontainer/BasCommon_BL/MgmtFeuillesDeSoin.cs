using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using BasCommon_DAL;

namespace BasCommon_BL
{
    public static class MgmtFeuillesDeSoin
    {


        public static void Delete(FeuilleDeSoin fs)
        {

            DAC.DeleteFeuilleDeSoin(fs);



        }



        public static void Save(FeuilleDeSoin fs, FeuilleDeSoin.TypeEnvois typeEnvoi)
        {
            if (fs.Id == -1)
                Insertfs(fs, typeEnvoi);
            else
                Updatefs(fs);

        }

        public static void Insertfs(FeuilleDeSoin fs, FeuilleDeSoin.TypeEnvois typeEnvoi)
        {
            if (fs.Id == -1)
            {
                fs.typedenvois = typeEnvoi;
                DAC.InsertFeuilleDeSoin(fs);

                foreach (ActePG afs in fs.actes)
                {
                    afs.FeuilleDeSoinAssocier = fs;
                }
            }

        }


        public static void Updatefs(FeuilleDeSoin fs)
        {
            if (fs.Id != -1)
            {
                DAC.UpdateFeuilleDeSoin(fs);


                foreach (ActePG afs in fs.actes)
                {
                    afs.FeuilleDeSoinAssocier = fs;
                }

            }

        }


        public static FeuilleDeSoin GetFeuillesDeSoin(int Id)
        {
            List<FeuilleDeSoin> lst = new List<FeuilleDeSoin>();
            DataRow dr = DAC.getFeuillesDeSoin(Id);

            if (dr == null) return null;

            FeuilleDeSoin fs = Builders.BuildFeuilleDeSoin.Build(dr);


            return fs;
        }


        public static List<FeuilleDeSoin> GetFeuillesDeSoin(basePatient pat)
        {
            List<FeuilleDeSoin> lst = new List<FeuilleDeSoin>();
            DataTable dt = DAC.getFeuillesDeSoin(pat);

            foreach (DataRow dr in dt.Rows)
            {
                FeuilleDeSoin fs = Builders.BuildFeuilleDeSoin.Build(dr);
                AffectActeFeuillesDeSoin(fs);
                lst.Add(fs);
            }

            return lst;
        }

        public static void AffectActeFeuillesDeSoin(FeuilleDeSoin fs)
        {
            List<ActePG> lst = new List<ActePG>();
            DataTable dt = DAC.getActesPG(fs);

            foreach (DataRow dr in dt.Rows)
            {
                ActePG afs = Builders.BuildActePG.Build(dr);
                lst.Add(afs);
            }

            fs.actes = lst;
        }

    }
}
