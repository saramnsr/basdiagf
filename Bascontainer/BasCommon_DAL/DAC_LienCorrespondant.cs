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

        public static void InsertLienPers(LienCorrespondant lp)
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
                command.Parameters.AddWithValue("@typelien", lp.TypeDeLien);
                command.Parameters.AddWithValue("@relation", lp.LienLibelle);
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
}
