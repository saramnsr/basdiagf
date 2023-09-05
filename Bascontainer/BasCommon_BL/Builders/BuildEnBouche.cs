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

    public static class BuildEnBouche
    {

        public static IElementDent BuildElementDent(DataRow r)
        {
            IElementDent resultat = null;
            ElementDent.Materials mat = (ElementDent.Materials)Convert.ToInt32(r["TYPEMATERIAL"]);
            resultat = EnBoucheMgmt.CreateElementFromType(mat);

            resultat.Id = Convert.ToInt32(r["ID"]);
            resultat.DateInstallation = r["DATEDEBUT"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEDEBUT"]);
            resultat.Datesuppression = r["DATEFIN"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEFIN"]);
            resultat.Dents = Convert.ToString(r["DENTS"]);
            resultat.IdPatient = Convert.ToInt32(r["ID_PATIENT"]);
            resultat.IdCommFin = r["ID_COMM_FIN"] is DBNull ? -1 : Convert.ToInt32(r["ID_COMM_FIN"]);
            resultat.IdCommDebut = r["ID_COMM_DEBUT"] is DBNull ? -1 : Convert.ToInt32(r["ID_COMM_DEBUT"]);

            return resultat;
        }


        public static ElementAppareil BuildElementAppareil(DataRow r)
        {
            ElementAppareil resultat = new ElementAppareil();



            resultat.Id = Convert.ToInt32(r["ID"]);
            resultat.DateInstallation = r["DATEDEBUT"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEDEBUT"]);
            resultat.Datesuppression = r["DATEFIN"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEFIN"]);
            resultat.IdPatient = Convert.ToInt32(r["ID_PATIENT"]);
            resultat.IdCommFin = r["ID_COMM_FIN"] is DBNull ? -1 : Convert.ToInt32(r["ID_COMM_FIN"]);
            resultat.IdCommDebut = r["ID_COMM_DEBUT"] is DBNull ? -1 : Convert.ToInt32(r["ID_COMM_DEBUT"]);
            resultat.Appareil = AppareilMgmt.getAppareil(Convert.ToInt32(r["ID_APPAREIL"]));
            resultat.Haut = r["HAUT"] is DBNull ? true : Convert.ToBoolean(r["HAUT"]);
            resultat.Bas = r["BAS"] is DBNull ? false : Convert.ToBoolean(r["BAS"]);

            return resultat;
        }

    }
}
