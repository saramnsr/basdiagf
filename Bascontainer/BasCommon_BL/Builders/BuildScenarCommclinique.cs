using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;
using BasCommon_BO.ElementsEnBouche.BO;

namespace BasCommon_BL.Builders
{

    public static class BuildScenarEnBouche
    {
        public static ScenarEnBouche Build(DataRow r)
        {
            ScenarEnBouche su = new ScenarEnBouche();
            su.IdCommDebut = SysTools.DataRow_ValueInt(r, "ID_COMM_DEBUT");
            su.IdCommFin = SysTools.DataRow_ValueInt(r, "ID_COMM_FIN");
            su.Id = SysTools.DataRow_ValueInt(r, "ID");
            su.type = (ElementDent.Materials)SysTools.DataRow_ValueInt(r, "TYPEMATERIAL");
            su.Dents = SysTools.DataRow_ValueString(r, "DENTS");
            su.IdAppareil = SysTools.DataRow_ValueInt(r, "ID_APPAREIL");
            su.Haut = SysTools.DataRow_ValueBool(r, "HAUT");
            su.Bas = SysTools.DataRow_ValueBool(r, "BAS");

            return su;

        }
    }

    
    public static class BuildScenarCommRadio
    {
        public static ScenarCommRadio Build(DataRow r)
        {
            ScenarCommRadio com = new ScenarCommRadio();

            com.typeradio = (CommRadio.TypeRadio)Convert.ToInt32(r["TYPERADIO"]);
            return com;
        }
    }
    
    public static class BuildScenarCommMateriel
    {
        public static ScenarCommMateriel Build(DataRow r)
        {
            ScenarCommMateriel com = new ScenarCommMateriel();

            com.IdBaseProduit = r["ID_BASEPRODUIT"] is DBNull ? -1 : Convert.ToInt32(r["ID_BASEPRODUIT"]);
            com.Libelle = Convert.ToString(r["LIBELLE"]).Trim();
            com.Qte = Convert.ToInt32(r["QTE"]);
            com.ShortLib = Convert.ToString(r["SHORTLIB"]).Trim();

            return com;
        }

    }

    public static class BuildScenarCommPhotos
    {
        public static ScenarCommPhoto Build(DataRow r)
        {
            ScenarCommPhoto com = new ScenarCommPhoto();

            com.typephoto = (CommPhoto.TypePhoto)Convert.ToInt32(r["TYPEPHOTO"]);
            return com;
        }
    }
    public static class BuildCommCliniqueAfaire
    {
        public static CommCliniqueDetailsScenario Build(DataRow r)
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
    }

    public static class BuildCommcliniquescenario
    {



        public static ScenarioCommClinique Build(DataRow dr)
        {
            ScenarioCommClinique cs = new ScenarioCommClinique();
            cs.Id = Convert.ToInt32(dr["ID"]);
            cs.Libelle = Convert.ToString(dr["LIBELLE"]);
            cs.NbSemestres = Convert.ToInt32(dr["NBSemestres"]);
            cs.TypeTtmnt = Convert.ToString(dr["TypeTtmnt"]);

            return cs;
        }

    }
}
