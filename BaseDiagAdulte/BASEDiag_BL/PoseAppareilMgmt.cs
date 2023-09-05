using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;

namespace BASEDiag_BL
{
    public static  class PoseAppareilMgmt
    {

        public static List<PoseAppareil> getPoseAppareils(Proposition prop)
        {
            List<PoseAppareil> lst = new List<PoseAppareil>();

            DataTable dt = DAC.getPoseAppareils(prop);

            foreach (DataRow dr in dt.Rows)
            {
                PoseAppareil pa = Builders.BuildPoseAppareil(dr);
                pa.Parent = prop;
                lst.Add(pa);

                foreach (int i in DAC.getIdSemestres(pa))
                    foreach (Traitement t in prop.traitements)
                        foreach (Semestre s in t.semestres)
                            if (s.Id == i)
                                pa.semestres.Add(s);

            }
            return lst;
        }

        public static void AddPoseAppareil(PoseAppareil pa)
        {
            DAC.AddPoseAppareil(pa);
        }
    }
}
