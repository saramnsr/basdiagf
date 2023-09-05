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

        public static void ReaffecterCorrespondant(int IdcorresASuppr, int IdcorresAReaffecter)
        {

            lock (lockobj)
            {

                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    string selectQuery = "update lienpers set ";
                    selectQuery += "id_personne = @id_nouvelle_personne";
                    selectQuery += " where (id_personne = @id_personne)";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandText = selectQuery;

                    command.Parameters.AddWithValue("@id_personne", IdcorresASuppr);
                    command.Parameters.AddWithValue("@id_nouvelle_personne", IdcorresAReaffecter);

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
        }



        public static void insertLienPers(LienCorrespondant lp)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string selectQuery = "insert into lienpers (id_personne, ";
                    selectQuery += "                      id_patient, ";
                    selectQuery += "                      typelien, ";
                    selectQuery += "                      relation)";
                    selectQuery += " values (@id_personne, ";
                    selectQuery += "         @id_patient, ";
                    selectQuery += "         @typelien, ";
                    selectQuery += "         @relation)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@id_personne", lp.IdCorrespondance);
                    command.Parameters.AddWithValue("@id_patient", lp.IdPatient);
                    command.Parameters.AddWithValue("@typelien", lp.Lien);
                    command.Parameters.AddWithValue("@relation", lp.TypeDeLien);
                    command.Parameters.AddWithValue("@date", DateTime.Now);

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
        }

        public static void deleteLienPers(int corresId, int patId)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string selectQuery = "DELETE from LIENPERS ";
                    selectQuery += "WHERE LIENPERS.ID_PERSONNE = @ID_PERSONNE ";
                    selectQuery += "AND LIENPERS.ID_PATIENT = @ID_PATIENT";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@ID_PERSONNE", corresId);
                    command.Parameters.AddWithValue("@ID_PATIENT", patId);

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
        }
    }
}
