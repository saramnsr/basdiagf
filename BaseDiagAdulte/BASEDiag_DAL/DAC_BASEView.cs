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

        private static MySqlConnection connection = null;
        
        private static void getConnection()
        {
            //    If the connection string is null, use a default.

            if (m_connectionString == "")
            {
                MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder();

                cs.Server = ConfigurationManager.AppSettings["KitView_DataSource"];
                cs.Database = ConfigurationManager.AppSettings["KitView_Database"];
                cs.UserID = ConfigurationManager.AppSettings["KitView_UserID"];
                cs.Password = ConfigurationManager.AppSettings["KitView_Password"];
                //cs.Dialect = Convert.ToInt32(ConfigurationManager.AppSettings["KitView_Dialect"]);
                //cs.Charset = "ISO8859_1";
                cs.Port = Convert.ToUInt32(ConfigurationManager.AppSettings["PORT"]);

                m_connectionString = cs.ToString();
            }

            connection = new MySqlConnection(m_connectionString);
        }
        


        public static string getOldRepertoireName(int Id)
        {

            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = " select CHEMIN ";
                selectQuery += " from personne";
                selectQuery += " where id_orthalis=@id_patient_orthalis";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

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
            MySqlTransaction transaction = connection.BeginTransaction();
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

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@id_patient_orthalis", p_Pat.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

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
            MySqlTransaction transaction = connection.BeginTransaction();
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

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ID_OBJET", img.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

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
