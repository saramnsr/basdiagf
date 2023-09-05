using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildLog
    {

        public static BaseLog Build(DataRow dr)
        {
            BaseLog b = new BaseLog();
            b.Category = Convert.ToString(dr["Category"]);
            b.CodeAction = Convert.ToString(dr["CodeAction"]);
            b.Commentaires = Convert.ToString(dr["commentaire"]);
            b.DteLog = Convert.ToDateTime(dr["dte_log"]);
            b.IdPatient = Convert.ToInt32(dr["id_patient"]);
            b.IdUser = Convert.ToInt32(dr["id_user"]);
            b.NomMachine = Convert.ToString(dr["nommachine"]);
            b.user = UtilisateursMgt.getUtilisateur(b.IdUser);

            return b;
        }
    }
}
