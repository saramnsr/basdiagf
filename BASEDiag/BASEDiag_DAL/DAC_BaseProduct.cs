using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using BASEDiag_BO;
using BasCommon_BO;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace BASEDiag_DAL
{
    public static class DAC_BaseProduct
    {
        private static string _RegistryKey = "Software\\BASE\\BASEPractice";

        private static string _RegistryKeyPref = _RegistryKey + "\\Preferences";
        private static string _CurrentCabRegistryKey = _RegistryKeyPref + "\\CurrentCab";

        public static void GetCurrentCabOnRegistry()
        {


            RegistryKey key = Registry.CurrentUser.OpenSubKey(_CurrentCabRegistryKey);

            // If the return value is null, the key doesn't exist
            if (key == null) return;

            string objValidityDate = (string)key.GetValue("ValidityDate");
            string objValidityUser = (string)key.GetValue("ValidityCab");

            key.Close();

            DateTime ValidityDate;
            int IdUser;

            if (DateTime.TryParse(objValidityDate, out ValidityDate))
            {
                //if (ValidityDate > DateTime.Now)
                //{
                prefix = objValidityUser;
                //}
            }
        }
        private static string _prefix = "";
        public static string prefix
        {
            get
            {
                if (_prefix == null || _prefix == "")
                    GetCurrentCabOnRegistry();
                return _prefix;
            }
            set
            {
                _prefix = "_" + value;
            }


        }

        private static string connectionString = "";
        private static MySqlConnection connectionBL = null;

        #region connection

        public static void getBaseLaboConnection()
        {
            //    If the connection string is null, use a default.

            if (connectionString == "")
            {
                MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder();

                cs.Server = ConfigurationManager.AppSettings["DataSource" + prefix];
                cs.Database = ConfigurationManager.AppSettings["Database" + prefix];
                cs.UserID = ConfigurationManager.AppSettings["UserID" + prefix];
                cs.Password = ConfigurationManager.AppSettings["Password" + prefix];
                cs.Port = Convert.ToUInt32(ConfigurationManager.AppSettings["PORT"]);

                connectionString = cs.ToString();
            }

            connectionBL = new MySqlConnection(connectionString);
        }

      
        #endregion

        public static DataTable GetAllObjSuivi(basePatient pat)
        {


            if (connectionBL == null) getBaseLaboConnection();

            if (connectionBL.State == ConnectionState.Closed) connectionBL.Open();
            MySqlTransaction transaction = connectionBL.BeginTransaction();
            try
            {

                string selectQuery = "select base_labo_suivi.Id,";
                selectQuery += " nature, ";
                selectQuery += " Detail, ";
                selectQuery += " DATEEMPREINTE, ";
                selectQuery += " pose_app";

                selectQuery += " from base_labo_suivi";
                selectQuery += " inner join base_labo_demande dem on dem.id = base_labo_suivi.DEMANDE_ID";
                selectQuery += " where PatientId = @Id ";
                selectQuery += " and base_labo_suivi.DATE_ANNULATION is null and dem.DATE_ANNULATION is null";
                selectQuery += " order by DATEEMPREINTE";

                MySqlCommand command = new MySqlCommand(selectQuery, connectionBL, transaction);
                command.Parameters.AddWithValue("@Id", pat.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;


            }
            catch (System.IndexOutOfRangeException)
            {
                transaction.Rollback();
                return null;
            }
            catch (System.Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
                connectionBL.Close();

            }
        }
        

    }
}
