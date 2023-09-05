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


        public static DataTable GetTypeDeDepenses()
        {
            if (connection == null) getConnection(); 

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @"select Id,
                                        organisation,
                                      CodeComptable,
                                      Libelle
                                      From typededepense";

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

        public static void DeleteTypeDeDepense(int IdTypeDeDepense)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "delete from TypeDeDepense where Id=@Id";


                

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.CommandText = selectQuery;



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

        public static void InsertTypeDeDepense(TypeDeDepense tdd)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "select MAX(ID)+1 as NEWID from typededepense";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;

                object obj = command.ExecuteScalar();

                if (obj == DBNull.Value)
                    tdd.Id = 1;
                else
                    tdd.Id = Convert.ToInt32(obj);


                selectQuery = "insert into typededepense (id,libelle,codecomptable,organisation) values (@id,@libelle,@codecomptable,@organisation)";


                MySqlCommand command2 = new MySqlCommand(selectQuery, connection, transaction);
                command2.CommandType = CommandType.Text;




                command2.CommandText = selectQuery;
                command2.Parameters.AddWithValue("@id", tdd.Id);
                command2.Parameters.AddWithValue("@libelle", tdd.Libelle);
                command2.Parameters.AddWithValue("@codecomptable", tdd.CodeComptable.Code);
                command2.Parameters.AddWithValue("@organisation", tdd.organisation);



                command2.ExecuteNonQuery();

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
