using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using BasCommon_BO;
using MySql.Data.MySqlClient;

namespace BasCommon_DAL
{
    public static partial class DAC
    {

        #region SQL

        public static string GetAppareilCodeBarre(int IdPatient)
        {


            if (connectionBL == null) getBaseLaboConnection();

            if (connectionBL.State == ConnectionState.Closed) connectionBL.Open();
            try
            {



                string selectQuery = "select id, ";
                selectQuery += "        codebarre ";
                selectQuery += " from base_labo_demande";
                selectQuery += " where base_labo_demande.PATIENT =@Id";

                MySqlCommand command = new MySqlCommand(selectQuery, connectionBL);
                command.Parameters.AddWithValue("@Id", IdPatient);

                return Convert.ToString(command.ExecuteScalar());


            }
            catch (System.IndexOutOfRangeException)
            {
                return null;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                connectionBL.Close();

            }
        }


        public static DataTable GetAllObjSuivi(DateTime dt1, DateTime dt2)
        {


            if (connectionBL == null) getBaseLaboConnection();

            if (connectionBL.State == ConnectionState.Closed) connectionBL.Open();
            try
            {

                string selectQuery = "select";

                selectQuery += " base_labo_suivi.id,";
                selectQuery += " base_labo_suivi.codebarre,";
                selectQuery += " nature, ";
                selectQuery += " Detail, ";
                selectQuery += " sortie_cab, ";
                selectQuery += " DATEEMPREINTE, ";
                selectQuery += " DEMANDE_ID,";

                selectQuery += " sortie_cab_with,";
                selectQuery += " sortie_labo_with,";
                selectQuery += " entree_cab_with,";
                selectQuery += " entree_labo_with,";

                selectQuery += " entre_labo,";
                selectQuery += " PatientName,";
                selectQuery += " PatientId,";
                selectQuery += " RequestorName,";
                selectQuery += " RequestorId,";
                selectQuery += " RecupereParName,";
                selectQuery += " WorkerName,";
                selectQuery += " ValidatorName,";
                selectQuery += " ValidatorId,";
                selectQuery += " sortie_labo, ";
                selectQuery += " reception_cab, ";
                selectQuery += " pose_app,";
                selectQuery += " dem.tarif,";
                selectQuery += " dem.aenvoye,";
                selectQuery += " base_labo_suivi.PAYMENTEFFECTUELE,";
                selectQuery += " max(com.date_comment) as LastCommentDate";

                selectQuery += " from base_labo_suivi";
                selectQuery += " inner join base_labo_demande dem on dem.id = base_labo_suivi.DEMANDE_ID";
                selectQuery += " left join base_labo_comments com on base_labo_suivi.id = com.id_demande";
                selectQuery += " where RECEPTION_CAB between @dt1 and @dt2 ";
                selectQuery += " and base_labo_suivi.DATE_ANNULATION is null and dem.DATE_ANNULATION is null";
                selectQuery += " group by sortie_cab_with,sortie_labo_with,entree_cab_with,entree_labo_with, base_labo_suivi.id, base_labo_suivi.codebarre, nature,  Detail,  sortie_cab,  DATEEMPREINTE,DEMANDE_ID,  entre_labo, PatientName, PatientId, RequestorName, RequestorId, RecupereParName, WorkerName, ValidatorName, ValidatorId, sortie_labo,  reception_cab,  pose_app, dem.tarif,dem.aenvoye, base_labo_suivi.PAYMENTEFFECTUELE";
                selectQuery += " order by DATEEMPREINTE";

                MySqlCommand command = new MySqlCommand(selectQuery, connectionBL);
                command.Parameters.AddWithValue("@dt1", dt1);
                command.Parameters.AddWithValue("@dt2", dt2);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt;


            }
            catch (System.IndexOutOfRangeException)
            {
                return null;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                connectionBL.Close();

            }
        }


        public static DataTable GetAllObjSuivis(int IdPatient)
        {


            if (connectionBL == null) 
                getBaseLaboConnection();

            if (connectionBL.State == ConnectionState.Closed) connectionBL.Open();
            try
            {

                string selectQuery = "select";

                selectQuery += " base_labo_suivi.id,";
                selectQuery += " base_labo_suivi.codebarre,";
                selectQuery += " nature, ";
                selectQuery += " Detail, ";
                selectQuery += " sortie_cab, ";
                selectQuery += " DATEEMPREINTE, ";
                selectQuery += " demande_id,";

                selectQuery += " sortie_cab_with,";
                selectQuery += " sortie_labo_with,";
                selectQuery += " entree_cab_with,";
                selectQuery += " entree_labo_with,";

                selectQuery += " entre_labo,";
                selectQuery += " PatientName,";
                selectQuery += " PatientId,";
                selectQuery += " requestorname,";
                selectQuery += " RequestorId,";
                selectQuery += " recupereparname,";
                selectQuery += " workername,";
                selectQuery += " validatorname,";
                selectQuery += " validatorid,";
                selectQuery += " sortie_labo, ";
                selectQuery += " reception_cab, ";
                selectQuery += " pose_app,";
                selectQuery += " dem.tarif,";
                selectQuery += " dem.aenvoye,";
                selectQuery += " base_labo_suivi.PAYMENTEFFECTUELE,";
                selectQuery += " max(com.date_comment) as lastcommentdate";

                selectQuery += " from base_labo_suivi";
                selectQuery += " inner join base_labo_demande dem on dem.id = base_labo_suivi.DEMANDE_ID";
                selectQuery += " left join base_labo_comments com on base_labo_suivi.id = com.id_demande";
                selectQuery += " where PatientId =  @id and STANDBY = 'False'";
                selectQuery += " and base_labo_suivi.DATE_ANNULATION is null and dem.DATE_ANNULATION is null";
                selectQuery += " group by sortie_cab_with,sortie_labo_with,entree_cab_with,entree_labo_with, base_labo_suivi.id, base_labo_suivi.codebarre, nature,  detail,  sortie_cab,  dateempreinte,demande_id,  entre_labo, patientname, patientid, requestorname, requestorid, recupereparname, workername, validatorname, validatorid, sortie_labo,  reception_cab,  pose_app, dem.tarif,dem.aenvoye, base_labo_suivi.paymenteffectuele";
                selectQuery += " order by DATEEMPREINTE desc";

                MySqlCommand command = new MySqlCommand(selectQuery, connectionBL);
                command.Parameters.AddWithValue("@id", IdPatient);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt;


            }
            catch (System.IndexOutOfRangeException)
            {
                return null;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                connectionBL.Close();

            }
        }

        #endregion
    }
}
