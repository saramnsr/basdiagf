using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using FirebirdSql.Data.FirebirdClient;
using BASEDiag_BO;
using BasCommon_BO;
using MySql.Data.MySqlClient;
using Microsoft.Win32;



namespace BASEDiag_DAL
{
    public static class DAC_BASeView
    {
        private static string m_connectionString = "";
        public static string ConnectionString
        {
            get
            {
                return m_connectionString;
            }

        }
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
        private static MySqlConnection connection = null;
        
        private static void getConnection()
        {
            //    If the connection string is null, use a default.

            if (m_connectionString == "")
            {

                MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder();

                cs.Server = ConfigurationManager.AppSettings["DataSource" + prefix];
                cs.Database = ConfigurationManager.AppSettings["Database" + prefix];
                cs.UserID = ConfigurationManager.AppSettings["UserID" + prefix];
                cs.Password = ConfigurationManager.AppSettings["Password" + prefix];
                cs.Port = Convert.ToUInt32(ConfigurationManager.AppSettings["PORT"]);

                m_connectionString = cs.ToString();
            }

            connection = new MySqlConnection(m_connectionString);
        }
        


        public static string getOldRepertoireName(int Id)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {


                string selectQuery = " select CHEMIN ";
                selectQuery += " from personne";
                selectQuery += " where id_orthalis=@id_patient_orthalis";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@id_patient_orthalis", Id);

                return Convert.ToString(command.ExecuteScalar());


            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();

            }
        }

        public static DataTable getObjectOf(basePatient p_Pat)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {

                
                string selectQuery = " select pk_objet, ";
                selectQuery += "       nom, ";
                //selectQuery += "       extension, ";
                //selectQuery += "       id_patient, ";
                //selectQuery += "       datas, ";
                //selectQuery += "       vignette, ";
                //selectQuery += "       width, ";
                //selectQuery += "       height, ";
                //selectQuery += "       taille, ";
                //selectQuery += "       estidentite, ";
                //selectQuery += "       datecreation, ";
                //selectQuery += "       echelle, ";
                selectQuery += "       fichier ";
                //selectQuery += "       last_modif, ";
                //selectQuery += "       rep_stockage, ";
                //selectQuery += "       syncpath, ";
                //selectQuery += "       dateinsertion, ";
                //selectQuery += "       auteur, ";
                //selectQuery += "       id_gabarit, ";
                //selectQuery += "       id_patient_orthalis";
                selectQuery += " from objet";
                selectQuery += " where ID_PATIENT_ORTHALIS=@id_patient_orthalis";                
                selectQuery += " order by DATECREATION";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@id_patient_orthalis", p_Pat.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt;

                
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();

            }
        }

        public static DataTable getAttribut(ObjImage img)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {


                string selectQuery = " select pk_attribut, ";
                selectQuery += "       nom, ";
                selectQuery += "       typeattribut, ";
                selectQuery += "       groupe,";
                 selectQuery += "       Valeur,";
                 selectQuery += "       Valeur_date,";
                 selectQuery += "       valeur_bool,";
                 selectQuery += "       valeur_string";
                selectQuery += " from attributs";
                selectQuery += " inner join LNK_ATTRIBUTS_OBJETS on LNK_ATTRIBUTS_OBJETS.ID_ATTRIBUT=attributs.pk_attribut";
                selectQuery += " where LNK_ATTRIBUTS_OBJETS.ID_OBJET = @ID_OBJET";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@ID_OBJET", img.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                return dt;


            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();

            }
        }
        
    }
}
