using BasCommon_BO;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
namespace BasCommon_DAL
{
    public static partial class DAC
    {

       
        public static void insertSmiler(InfoSmilers smilers)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string selectQuery = "delete from   patient_smilers where idpatient = @idpatient";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@idpatient", smilers.idPatient);

                    command.ExecuteNonQuery();

                    selectQuery = "insert into patient_smilers (orderid,numdossier,idpatient,nom,prenom,genre,datenaissance) values (@orderid,@numdossier,@idpatient,@nom,@prenom,@genre,@datenaissance)";

                     command = new MySqlCommand(selectQuery, connection, transaction);
                     command.Parameters.Clear();
                    command.Parameters.AddWithValue("@orderid", smilers.orderid);
                    command.Parameters.AddWithValue("@numdossier", smilers.numdossier);
                    command.Parameters.AddWithValue("@idpatient", smilers.idPatient);
                    command.Parameters.AddWithValue("@nom", smilers.nom);
                    command.Parameters.AddWithValue("@prenom", smilers.prenom);
                    command.Parameters.AddWithValue("@genre", smilers.genre);
                    command.Parameters.AddWithValue("@datenaissance", smilers.dataNaissance);
               


                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (System.SystemException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                   connection.Close();

                }
            }
        }
        public static void updateSmiler(InfoSmilers smilers)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {


                    string selectQuery = "update patient_smilers set numdossier = @id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", smilers.numdossier);
                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (System.SystemException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    connection.Close();

                }
            }
        }
        public static void insertSmilerSuivi(InfoSmilers smilers)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    
                   //string selectQuery = "delete from   patient_smilers_suivi where numdossier = @numdossier";
                   // command.Parameters.AddWithValue("@numdossier", smilers.numdossier);

                    //command.ExecuteNonQuery();

                 string selectQuery = "insert into patient_smilers_suivi (orderid,numdossier,idpatient,nom,prenom,genre,datenaissance) values (@orderid,@numdossier,@idpatient,@nom,@prenom,@genre,@datenaissance)";
                   // MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                 MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@orderid", smilers.orderid);
                    command.Parameters.AddWithValue("@numdossier", smilers.numdossier);
                    command.Parameters.AddWithValue("@idpatient", smilers.idPatient);
                    command.Parameters.AddWithValue("@nom", smilers.nom);
                    command.Parameters.AddWithValue("@prenom", smilers.prenom);
                    command.Parameters.AddWithValue("@genre", smilers.genre);
                    command.Parameters.AddWithValue("@datenaissance", smilers.dataNaissance);
             


                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (System.SystemException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    connection.Close();

                }
            }
        }
        public static void updateSmilerSuivi(InfoSmilers smilers)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {


                    string selectQuery = "update patient_smilers_suivi set idpatient = @id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", smilers.idPatient);
                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (System.SystemException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    connection.Close();

                }
            }
        }
    }
}
