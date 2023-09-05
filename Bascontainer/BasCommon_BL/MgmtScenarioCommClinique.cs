using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using BasCommon_DAL;

namespace BasCommon_BL
{
    public static class MgmtScenarioCommClinique
    {


        private static List<ScenarioCommClinique> _scenarios = null;
        public static List<ScenarioCommClinique> scenarios
        {
            get
            {
                if (_scenarios == null) _scenarios = getscenarios();
                return _scenarios;
            }
            set
            {
                _scenarios = value;
            }
        }

        private static List<ScenarioCommClinique> getscenarios()
        {
            DataTable dt = DAC.GetScenariosCommClinique();
            List<ScenarioCommClinique> lst = new List<ScenarioCommClinique>();

            foreach (DataRow dr in dt.Rows)
            {
                ScenarioCommClinique scc = Builders.BuildCommcliniquescenario.Build(dr);
                lst.Add(scc);
            }
            return lst;
        }



        public static void FillCommCliniqueAfaire(ScenarioCommClinique scenario)
        {
            DataTable dt = DAC.GetScenariosCommCliniqueDetails(scenario);
            scenario.commentaires = new List<CommCliniqueDetailsScenario>();

            foreach (DataRow dr in dt.Rows)
            {
                CommCliniqueDetailsScenario cca = Builders.BuildCommCliniqueAfaire.Build(dr);
                scenario.commentaires.Add(cca);
            }


        }



        public static void FillCommCliniqueEnBouche(CommCliniqueDetailsScenario scenario)
        {
            DataTable dt = DAC.getCommCliniqueScenarEnbouche(scenario);
            scenario.EnBouches = new List<ScenarEnBouche>();

            foreach (DataRow dr in dt.Rows)
            {
                ScenarEnBouche cca = Builders.BuildScenarEnBouche.Build(dr);
                scenario.EnBouches.Add(cca);
            }


        }


        public static void FillCommCliniqueMateriel(CommCliniqueDetailsScenario scenario)
        {
            DataTable dt = DAC.GetCommCliniqueScenarMateriels(scenario);
            scenario.Materiels = new List<ScenarCommMateriel>();

            foreach (DataRow dr in dt.Rows)
            {
                ScenarCommMateriel cca = Builders.BuildScenarCommMateriel.Build(dr);
                scenario.Materiels.Add(cca);
            }


        }

        //public static void FillCommCliniqueRadios(CommCliniqueDetailsScenario scenario)
        //{
        //    DataTable dt = DAC.GetCommCliniqueScenarRadios(scenario);
        //    scenario.Radios = new List<ScenarCommRadio>();

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        ScenarCommRadio cca = Builders.BuildScenarCommRadio.Build(dr);
        //        scenario.Radios.Add(cca);
        //    }


        //}






        //public static void FillCommCliniquePhotos(CommCliniqueDetailsScenario scenario)
        //{
        //    DataTable dt = DAC.GetCommCliniqueScenarPhotos(scenario);
        //    scenario.photos = new List<ScenarCommPhoto>();

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        ScenarCommPhoto cca = Builders.BuildScenarCommPhotos.Build(dr);
        //        scenario.photos.Add(cca);
        //    }


        //}

        public static ScenarioCommClinique GetScenario(int id)
        {

            foreach (ScenarioCommClinique s in scenarios)
            {
                if (s.Id == id) return s;
            }
            return null;
        }


    }
}
