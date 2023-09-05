using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using BASEDiag_BO;
using BASEDiag_DAL;
using BasCommon_BO;

namespace BASEDiag_BL
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
                ScenarioCommClinique scc = Builders.BuildCommcliniquescenario(dr);
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
                CommCliniqueDetailsScenario cca = Builders.BuildCommCliniqueAfaire(dr);
                scenario.commentaires.Add(cca);
            }


        }


        public static CommCliniqueDetailsScenario BuildCommCliniqueAfaire(DataRow r)
        {
            CommCliniqueDetailsScenario com = new CommCliniqueDetailsScenario();

            com.Id = Convert.ToInt32(r["ID"]);
            com.IdActe = r["ID_ACTE"] is DBNull ? -1 : Convert.ToInt32(r["ID_ACTE"]);
            com.Commentaire = Convert.ToString(r["COMMENTAIRES"]);
            com.CommentaireAFaire = Convert.ToString(r["COMMENTAIRESAFAIRE"]);
            com.IdScenario = r["ID_SCENARIO"] is DBNull ? -1 : Convert.ToInt32(r["ID_SCENARIO"]);
            com.NbJours = r["NBJOURS"] is DBNull ? -1 : Convert.ToInt32(r["NBJOURS"]);
            com.NbMois = r["NBMOIS"] is DBNull ? -1 : Convert.ToInt32(r["NBMOIS"]);
            com.numSemestre = r["num_semestre"] is DBNull ? "" : Convert.ToString(r["num_semestre"]);
            com.IdParent = r["id_parentcomment"] is DBNull ? -1 : Convert.ToInt32(r["id_parentcomment"]);
            com.Ordre = r["ordre"] is DBNull ? -1 : Convert.ToInt32(r["ordre"]);
            com.IsReferenceDate = r["refdate"] is DBNull ? false : Convert.ToString(r["refdate"]) == "Y" || Convert.ToString(r["refdate"]) == "T" || Convert.ToString(r["refdate"]) == "0";

            return com;
        }


        public static void FillCommCliniqueEnBouche(CommCliniqueDetailsScenario scenario)
        {
            DataTable dt = DAC.getCommCliniqueScenarEnbouche(scenario);
            scenario.EnBouches = new List<ScenarEnBouche>();

            foreach (DataRow dr in dt.Rows)
            {
                ScenarEnBouche cca = Builders.BuildScenarEnBouche(dr);
                scenario.EnBouches.Add(cca);
            }


        }


        public static void FillCommCliniqueMateriel(CommCliniqueDetailsScenario scenario)
        {
            DataTable dt = DAC.GetCommCliniqueScenarMateriels(scenario);
            scenario.Materiels = new List<ScenarCommMateriel>();

            foreach (DataRow dr in dt.Rows)
            {
                ScenarCommMateriel cca = Builders.BuildScenarCommMateriel(dr);
                scenario.Materiels.Add(cca);
            }


        }

        public static void FillCommCliniqueRadios(CommCliniqueDetailsScenario scenario)
        {
            DataTable dt = DAC.GetCommCliniqueScenarRadios(scenario);
            scenario.Radios = new List<ScenarCommRadio>();

            foreach (DataRow dr in dt.Rows)
            {
                ScenarCommRadio cca = Builders.BuildScenarCommRadio(dr);
                scenario.Radios.Add(cca);
            }


        }






        public static void FillCommCliniquePhotos(CommCliniqueDetailsScenario scenario)
        {
            DataTable dt = DAC.GetCommCliniqueScenarPhotos(scenario);
            scenario.photos = new List<ScenarCommPhoto>();

            foreach (DataRow dr in dt.Rows)
            {
                ScenarCommPhoto cca = Builders.BuildScenarCommPhotos(dr);
                scenario.photos.Add(cca);
            }


        }

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
