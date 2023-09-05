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

        public static int InsertSurveillance(Surveillance surv)
        {
            if (connection == null) getConnection();  

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQueryId = "select max(id)+1 as ID from base_surveillance";
                MySqlCommand commandid = new MySqlCommand(selectQueryId, connection, transaction);
                object obj = commandid.ExecuteScalar();
                if (obj == DBNull.Value)
                    surv.Id = 1;
                else
                    surv.Id = Convert.ToInt32(obj);



                string selectQuery = "insert into base_surveillance (id, ";
                selectQuery += " id_semestre, ";
                selectQuery += " id_traitmntsecu, ";
                selectQuery += " montant, ";
                selectQuery += " datedebut, ";
                selectQuery += " datefin)";
                selectQuery += " values (@id, ";
                selectQuery += " @id_semestre, ";
                selectQuery += " @id_traitmntsecu, ";
                selectQuery += " @montant, ";
                selectQuery += " @datedebut, ";
                selectQuery += " @datefin)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", surv.Id);
                command.Parameters.AddWithValue("@id_semestre", surv.IdSemestre);
                command.Parameters.AddWithValue("@id_traitmntsecu", surv.traitementSecu.Id);
                command.Parameters.AddWithValue("@montant", surv.Montant_Honoraire);
                command.Parameters.AddWithValue("@datedebut", surv.DateDebut);
                command.Parameters.AddWithValue("@datefin", surv.DateFin);


                object oj = command.ExecuteScalar();
                if (oj != null) return Convert.ToInt32(oj);


                transaction.Commit();

                return -1;

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

        public static void DeleteSurveillance(int IdSurv)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "delete  ";
                selectQuery += " from base_surveillance";
                selectQuery += " where ID = @ID ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@ID", IdSurv);


                object obj = command.ExecuteNonQuery();

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


        public static void DeleteSurveillance(Surveillance surv)
        {
            DeleteSurveillance(surv.Id);

        }

        public static int UpdateSurveillance(Surveillance surv)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {


                string selectQuery = "update base_surveillance set ";
                selectQuery += " id_semestre=@id_semestre, ";
                selectQuery += " id_traitmntsecu=@id_traitmntsecu, ";
                selectQuery += " montant=@montant, ";
                selectQuery += " datedebut=@datedebut, ";
                selectQuery += " datefin=@datefin";
                selectQuery += " where id= @id ";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", surv.Id);
                command.Parameters.AddWithValue("@id_semestre", surv.IdSemestre);
                command.Parameters.AddWithValue("@id_traitmntsecu", surv.traitementSecu.Id);
                command.Parameters.AddWithValue("@montant", surv.Montant_Honoraire);
                command.Parameters.AddWithValue("@datedebut", surv.DateDebut);
                command.Parameters.AddWithValue("@datefin", surv.DateFin);


                object oj = command.ExecuteScalar();
                if (oj != null) return Convert.ToInt32(oj);


                transaction.Commit();

                return -1;

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
