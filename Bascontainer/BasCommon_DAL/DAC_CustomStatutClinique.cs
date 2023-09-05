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

        public static void updateCustomStatusCliniqueBeToWas(CustomStatusClinique custo)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "update base_histo_status set ";
                selectQuery += "DATEFIN = @DATEFIN";
                selectQuery += " where (id = @id)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.CommandText = selectQuery;

                command.Parameters.AddWithValue("@id", custo.Id);
                command.Parameters.AddWithValue("@DATEFIN", DateTime.Now);

                command.ExecuteNonQuery();

                transaction.Commit();

            }
            catch (System.Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
               connection.Close();

            }



        }

        public static void updateCustomStatusCliniqueWasToBe(CustomStatusClinique custo)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "select MAX(ID)+1 as NEWID from base_histo_status";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;


                object obj = command.ExecuteScalar();

                if (obj is DBNull)
                    custo.Id = 1;
                else
                    custo.Id = Convert.ToInt32(obj);

                selectQuery = "insert into base_histo_status (";
                selectQuery += "ID,";
                selectQuery += "ID_PATIENT,";
                selectQuery += "ID_STATUS,";
                selectQuery += "DATEDEBUT,";
                selectQuery += "DATEFIN";
                selectQuery += ") values ( ";
                selectQuery += "@ID,";
                selectQuery += "@ID_PATIENT,";
                selectQuery += "@ID_STATUS,";
                selectQuery += "@DATEDEBUT,";
                selectQuery += "@DATEFIN)";

                command.CommandText = selectQuery;


                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID", custo.Id);
                command.Parameters.AddWithValue("@ID_PATIENT", custo.IdPersonne);
                command.Parameters.AddWithValue("@ID_STATUS", custo.status.Id);
                command.Parameters.AddWithValue("@DATEDEBUT", DateTime.Now);
                command.Parameters.AddWithValue("@DATEFIN", DBNull.Value);

                command.ExecuteNonQuery();






                selectQuery = "update patient set ID_STATUT=@IdStatus where ID_PERSONNE = @ID_PATIENT";

                command.CommandText = selectQuery;

                command.Parameters.Clear();
                command.CommandText = selectQuery;
                command.Parameters.AddWithValue("@ID_PATIENT", custo.IdPersonne);
                command.Parameters.AddWithValue("@IdStatus", custo.status.Id);

                command.ExecuteNonQuery();

                transaction.Commit();

            }
            catch (System.Exception e)
            {
                transaction.Rollback();
                throw e;
            }
            finally
            {
               connection.Close();

            }



        }




        public static DataTable getStatus()
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            //MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select ID_STATUT,LIBELLE,COULEUR,ORGANISATION,FAMILLE_STATUT from statuts";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
               // transaction.Commit();

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


        public static DataTable getCustomStatusCliniqueFromIdPersonne(int id_personne)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
         //   MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "SELECT ID,base_histo_status.ID_PATIENT, ";
                selectQuery += "base_histo_status.ID_STATUS, ";
                selectQuery += "base_histo_status.DATEDEBUT, ";
                selectQuery += "base_histo_status.DATEFIN ";
                selectQuery += "FROM base_histo_status ";
                selectQuery += "WHERE base_histo_status.ID_PATIENT = @id_personne ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("id_personne", id_personne);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
               // transaction.Commit();

                return ds.Tables[0];

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
               connection.Close();

            }
        }


        public static DataTable getCurrentCustomStatusCliniqueFromIdPersonne(int id_personne)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
           // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "SELECT ID,base_histo_status.ID_PATIENT, ";
                selectQuery += "base_histo_status.ID_STATUS, ";
                selectQuery += "base_histo_status.DATEDEBUT, ";
                selectQuery += "base_histo_status.DATEFIN ";
                selectQuery += "FROM base_histo_status ";
                selectQuery += "WHERE base_histo_status.ID_PATIENT = @id_personne ";
                selectQuery += "and base_histo_status.DATEFIN is null";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("id_personne", id_personne);

                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);
            //    transaction.Commit();

                return ds.Tables[0];

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
               connection.Close();

            }
        }

    }
}
