﻿using BasCommon_BO;
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
        public static void AddAssurance(Assurance asr)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                selectQuery = "delete from assurance where ID_PATIENT = @id";

                command.Parameters.AddWithValue("@id", asr.idPatient);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                selectQuery = "select max(id)+1 as ID from assurance";
                command = new MySqlCommand(selectQuery, connection, transaction);
                object obj = command.ExecuteScalar();
                if (obj == DBNull.Value)
                    asr.Id = 1;
                else
                    asr.Id = Convert.ToInt32(obj);


                selectQuery = " insert into assurance (id, ";
                selectQuery += "                              id_patient, ";
                selectQuery += "                              MONTANT, ";
                selectQuery += "                              pourcentage, ";
                selectQuery += "                              libelle)";
                selectQuery += " values (@id, ";
                selectQuery += "        @id_patient, ";
                selectQuery += "        @MONTANT, ";
                selectQuery += "        @pourcentage, ";
                selectQuery += "        @libelle )";




                command.Parameters.Clear();
                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", asr.Id);
                command.Parameters.AddWithValue("@id_patient", asr.idPatient);
                command.Parameters.AddWithValue("@MONTANT", asr.montant);
                command.Parameters.AddWithValue("@pourcentage", asr.pourcentage);
                command.Parameters.AddWithValue("@libelle", asr.libelle);

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
        public static DataRow getAssurance(int idPatient)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            try
            {
                string selectQuery = @"select 
                                        id, 
                                        id_patient, 
                                        montant, 
                                        pourcentage, 
                                        libelle from assurance
                                        where id_patient = @ID";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@ID", idPatient);


                DataSet ds = new DataSet();
                MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                adapt.Fill(ds);

                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count == 0) return null;

                return dt.Rows[0];
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
