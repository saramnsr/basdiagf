using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildCommentaireOrthalis
    {


        public static CommentaireOrthalis Build(DataRow r)
        {
            CommentaireOrthalis com = new CommentaireOrthalis();
            com.Afaire = Convert.ToString(r["afaire"]);
            com.Date = Convert.ToDateTime(r["date_comm"]);
            com.Fait = Convert.ToString(r["fait"]).Trim();
            com.hygiene = Convert.ToString(r["code_hyg"]).Trim();
            com.Praticien = UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["ID_PRATICIEN"])).ToString();
            com.IdPraticien = Convert.ToInt32(r["ID_PRATICIEN"]);
            com.Libre = Convert.ToString(r["code_libre"]).Trim();

            return com;
        }
    }
}
