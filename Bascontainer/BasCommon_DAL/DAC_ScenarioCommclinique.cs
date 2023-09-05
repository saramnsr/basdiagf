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

        public static DataTable getCommCliniqueScenarEnbouche(CommCliniqueDetailsScenario com)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += " typematerial, ";
                selectQuery += " dents,";
                selectQuery += " ID_APPAREIL,";
                selectQuery += " ID_COMM_DEBUT,";
                selectQuery += " ID_COMM_FIN,";
                selectQuery += " ID_APPAREIL,";
                selectQuery += " HAUT,";
                selectQuery += " BAS";
                selectQuery += " from BASE_SCENAR_ENBOUCHE";
                selectQuery += " where ID_COMM_DEBUT=@ID_COMM or ID_COMM_FIN=@ID_COMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID_COMM", com.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];

                return dt;

            }
            catch (System.SystemException ex)
            {
                throw ex;
            }
            finally
            {
               connection.Close();

            }

        }



        public static DataTable GetCommCliniqueScenarMateriels(CommCliniqueDetailsScenario com)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        id_baseproduit, ";
                selectQuery += "        libelle, ";
                selectQuery += "        qte, ";
                selectQuery += "        shortlib";
                selectQuery += " from BASE_SCENAR_COMM_MAT";

                selectQuery += " where id_comm = @IDCOMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];
                return dt;

            }
            catch (System.SystemException ex)
            {
                throw ex;
            }
            finally
            {
               connection.Close();
            }
        }

        public static DataTable GetCommCliniqueScenarRadios(CommCliniqueDetailsScenario com)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        typeradio ";
                selectQuery += " from BASE_SCENAR_COMM_RADIOS";

                selectQuery += " where id_comm = @IDCOMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];
                return dt;

            }
            catch (System.SystemException ex)
            {
                throw ex;
            }
            finally
            {
               connection.Close();
            }
        }


        public static DataTable GetCommCliniqueScenarPhotos(CommCliniqueDetailsScenario com)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_comm, ";
                selectQuery += "        typephoto ";
                selectQuery += " from BASE_SCENAR_COMM_PHOTOS";

                selectQuery += " where id_comm = @IDCOMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@IDCOMM", com.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];
                return dt;

            }
            catch (System.SystemException ex)
            {
                throw ex;
            }
            finally
            {
               connection.Close();
            }
        }



        public static DataTable GetScenariosCommClinique()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "        Libelle, ";
                selectQuery += "        NbSemestres, ";
                selectQuery += "        TypeTtmnt ";
                selectQuery += " from SCENARIOS_COMM";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];
                return dt;

            }
            catch (System.SystemException ex)
            {
                throw ex;
            }
            finally
            {
               connection.Close();
            }
        }




        public static DataTable GetScenariosCommCliniqueDetails(ScenarioCommClinique scenar)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id, ";
                selectQuery += "        id_acte, ";
                selectQuery += "        commentaires,";
                selectQuery += "        commentairesafaire,";
                selectQuery += "        ID_SCENARIO,";
                selectQuery += "        NBJOURS,";
                selectQuery += "        NBMOIS,";
                selectQuery += "        num_semestre,";
                selectQuery += "        id_parentcomment,";
                selectQuery += "        ordre,";
                selectQuery += "        refdate";
                selectQuery += " from scenarios_comm_detail";
                selectQuery += " where ID_SCENARIO = @ID";
                selectQuery += " order by ID_SCENARIO asc,ordre asc";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                command.Parameters.AddWithValue("@ID", scenar.Id);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
                transaction.Commit();

                DataTable dt = ds.Tables[0];
                return dt;

            }
            catch (System.SystemException ex)
            {
                throw ex;
            }
            finally
            {
               connection.Close();
            }
        }


    }
}
